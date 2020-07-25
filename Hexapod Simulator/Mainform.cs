using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GFunctions.Mathematics;
using GFunctions.Timing;
using GFunctions.OpenTK;
using Hexapod_Simulator.SimObject;

namespace Hexapod_Simulator
{
    public partial class Mainform : Form
    {
        public GLControlDraggable GLCont;
        public Hexapod Hexa;
        
        //public Ball_Test BTest;
        public Ball_Local_Test BTest;
        public TimeSimulation Sim = new TimeSimulation();
        public TimeSimulation TrajSim = new TimeSimulation();

        public PIDController XController = new PIDController(3, 1, 1, -0.5, 30);
        public PIDController YController = new PIDController(-3, 1, 1, -0.5, 30);

        private bool servoActive = false;

        //-------------- Initialization and cleanup ------------------

        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            
            
            Hexa = new Hexapod(30, 30, 10, 30, 5);
            //BTest = new Ball_Test(0.1, 9800, Hexa.Top.Position);
            BTest = new Ball_Local_Test(0.1, 9800, new double[]{0,0,0});
            

            GLCont = new GLControlDraggable(glControl_main);

            GLCont.DrawnObjects.Add(Hexa);
            GLCont.DrawnObjects.Add(BTest);

            GLCont.Refresh();

            platformConfigControl_base.AssignPlatform(Hexa.Base);
            platformConfigControl_top.AssignPlatform(Hexa.Top);
            control_RotationCenter1.AssignPlatform(Hexa.Top); 

            Hexa.RedrawRequired += this.RefreshView;
            Hexa.ServosCalculated += this.ServosCalculated; 
            Hexa.Top.PositionChanged += this.TopPosChanged;


            control_ManualDrag_main.PositionChanged += this.ManualDragControlPosChange;
            control_ManualDrag_main.RotationChanged += this.ManualDragControlRotChange;

            control_ManualDrag_main.ResetPos();


            Sim.SimulationDoWorkRequest += this.SimulationTimeStepDoWork;
            Sim.RunFreqUpdated += this.SimulationFrequencyReported;


            TrajSim.SimulationDoWorkRequest += this.TrajSimulationTimeStepDoWork;
        }
        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            GLCont.Dispose(); 
            
            if (Sim.Running)
                Sim.Stop();
        }

        //-------------- Events to Set Controls ------------------

        private void RefreshView(object sender, EventArgs e)
        {
            GLCont.Refresh();
        }
        private void TopPosChanged(object sender, EventArgs e)
        {
            Platform plat = (Platform)sender;

            control_CurrentPos1.SetPosition(plat.Translation);
            control_CurrentPos1.SetRotation(plat.Rotation);


            XController.SetTarget(plat.Position[0]);
            YController.SetTarget(plat.Position[1]);
        }
        private void ServosCalculated(object sender, EventArgs e)
        {
            Hexapod hex = (Hexapod)sender;

            for (int i = 0; i < hex.Actuators.Length; i++)
            {
                control_ServoPos1.SetAngle(i, hex.Actuators[i].TravelPosition, hex.Actuators[i].SolutionValid);
            }
        }

        //-------------- Controls Updating ------------------

        private void ManualDragControlPosChange(object sender, EventArgs e)
        {
            if (Sim.Running)
            {
                Hexa.Top.TranslateAbsServo(control_ManualDrag_main.Position);
            }
            else
            {
                Hexa.Top.TranslateAbs(control_ManualDrag_main.Position);
            }         
        }
        private void ManualDragControlRotChange(object sender, EventArgs e)
        {          
            if (Sim.Running)
            {
                Hexa.Top.RotateAbsServo(control_ManualDrag_main.Rotation);
            }
            else
            {
                Hexa.Top.RotateAbs(control_ManualDrag_main.Rotation);
            }          
        }

        //-------------- Ball Balancing Simulation ------------------

        private void toolStripButton_toggleSimulation_Click(object sender, EventArgs e)
        {
            ToolStripButton Btn = (ToolStripButton)sender;
            
            if (Sim.Running)
            {
                Sim.Stop();
                Btn.Text = "Start Simulation";

            }
            else
            {
                Sim.Start(Convert.ToDouble(toolStripTextBox_simInterval.Text));
                Btn.Text = "Stop Simulation";
            }
        }
        private void toolStripButton_servoActive_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripButton Btn = (ToolStripButton)sender;
            servoActive = Btn.Checked; 
        }

        private void SimulationTimeStepDoWork(object sender, TimeSimulationStepEventArgs e)
        {
            //------------ Do Calculations -------------------------
            
            BTest.CalculateTimeStep(e.TimeIncrement, Hexa.Top.NormalVector);
            Hexa.Top.CalculateTimeStep(e.TimeIncrement);
            BTest.UpdateGlobalCoords(Hexa.Top.CalcGlobalCoord(BTest.Position));

            if(servoActive)
                Hexa.Top.RotateAbsServo(new double[] { XController.CalcOutput(BTest.Position[0], e.TimeIncrement), YController.CalcOutput(BTest.Position[1], e.TimeIncrement), 0 });

            //------------ Draw Screen -------------------------
            
            GLCont.Refresh(); //needs to occur in foreground thread

            //------------ Cleanup -------------------------

            e.WorkDoneCallback.Set(); //allow simulation to proceed
        }
        private void SimulationFrequencyReported(object sender, int e)
        {
            toolStripStatusLabel_simFreq.Text = "FPS: " + e.ToString();
        }

        //-------------- Trajectory Simulation ------------------

        int counter = 0; //counts which step of the trajectory we are on

        private List<double[]> TrajPosPoints = new List<double[]>(); //contains all calculated trajectory XYZ coordinates from start to end
        private List<double[]> TrajRotPoints = new List<double[]>(); //contains all calculated trajectory PRY rotation coordinates from start to end

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int simSpeed = 30; //30 updates/second

            Trajectory Trag = new Trajectory(-1, -1, simSpeed); //30 updates/second

            double[] startPos = new double[] { 0, 0, 0, 0, 0, 0 }; //XYZ, PRY
            double[] endPos = new double[] { 10, 10, 10, 0, 0, 0 }; //XYZ, PRY

            List<double[]> TrajPoints = new List<double[]>();

            for(int i =0; i< 6; i++)
            {
                TrajPoints.Add(Trag.CalculatePositions(startPos[i], endPos[i], 2)); //calculate all interpolated points
            }

            //---------------- sort by time in list ------------------------------

            for (int i = 0; i < TrajPoints[0].Length; i++)
            {
                TrajPosPoints.Add(new double[] { TrajPoints[0][i], TrajPoints[1][i], TrajPoints[2][i] });
                TrajRotPoints.Add(new double[] { TrajPoints[3][i], TrajPoints[4][i], TrajPoints[5][i] });
            }

            //---------------- start simulation ------------------------------

            if (TrajSim.Running)
            {
                TrajSim.Stop(); 
            }
            else
            {
                counter = 0;
                TrajSim.Start(Convert.ToDouble(1.0 / simSpeed));
            }
        }


        private void TrajSimulationTimeStepDoWork(object sender, TimeSimulationStepEventArgs e)
        {
            //------------ Do Calculations -------------------------

            Hexa.Top.TranslateAbs(TrajPosPoints[counter]);
            Hexa.Top.RotateAbs(TrajRotPoints[counter]);

            counter += 1;

            if(counter == TrajPosPoints.Count)
            {
                TrajSim.Stop(); 
            }

            //------------ Draw Screen -------------------------

            GLCont.Refresh(); //needs to occur in foreground thread

            //------------ Cleanup -------------------------

            e.WorkDoneCallback.Set(); //allow simulation to proceed
        }

        private void numericalInputTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
