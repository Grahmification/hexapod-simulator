using Hexapod_Simulator.Shared;
using GFunctions.Timing;
using GFunctions.Mathematics;
using System.Windows.Input;

namespace Hexapod_Simulator.Helix.ViewModels
{
    /// <summary>
    /// The top level view model for the main window
    /// </summary>
    public class MainVM : BaseViewModel
    {
        /// <summary>
        /// The window's hexapod
        /// </summary>
        public HexapodVM Hexapod { get; private set; }

        /// <summary>
        /// The window's ball
        /// </summary>
        public BallVM Ball { get; private set; }

        /// <summary>
        /// The simulation refresh interval in ms
        /// </summary>
        public int SimulationInterval { get; set; } = 50;

        /// <summary>
        /// Returns true if the simulation is running
        /// </summary>
        public bool SimulationRunning { get { return SimModel.Running; } }

        /// <summary>
        /// The simulation frames per second
        /// </summary>
        public int SimulationFPS { get; set; } = 0;

        /// <summary>
        /// True if hexapod servoing is active to correct the ball's position
        /// </summary>
        public bool ServoActive { get; set; } = false;


        /// <summary>
        /// The model for managing the time-based simulation
        /// </summary>
        private TimeSimulation SimModel = new TimeSimulation();

        /// <summary>
        /// The hexapod model for this class
        /// </summary>
        private Hexapod HexaPodModel = new Hexapod(15, 12, 8, 30, 5);

        /// <summary>
        /// The ball model for this class
        /// </summary>
        private IBall BallModel = new Ball_Local_Test(0.005, 9800, new double[] { 0, 0, 0 });

        /// <summary>
        /// PID Controller Tracking Monitoring the X Position of the <see cref="BallModel"/>
        /// </summary>
        private PIDController XController = new PIDController(3, 1, 1, -0.5, 30);

        /// <summary>
        /// PID Controller Tracking Monitoring the Y Position of the <see cref="BallModel"/>
        /// </summary>
        public PIDController YController = new PIDController(-3, 1, 1, -0.5, 30);


        /// <summary>
        /// RelayCommand for <see cref="UpdateRotationCenter"/>
        /// </summary>
        public ICommand ToggleSimulationCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainVM()
        {
            InitializeModels();
        }

        public void ToggleSimulation()
        {
            if (SimulationRunning)
            {
                SimModel.Stop();
            }
            else
            {
                SimModel.Start(SimulationInterval);
            }

            OnPropertyChanged("SimulationRunning");
        }




        /// <summary>
        /// Initializes any sub VMs in this class
        /// </summary>
        private void InitializeModels()
        {
            //setup hexapod
            Hexapod = new HexapodVM(HexaPodModel);

            //setup ball
            Ball = new BallVM(BallModel);
            
            //setup simulation model
            SimModel.SimulationDoWorkRequest += this.SimulationTimeStepDoWork;
            SimModel.RunFreqUpdated += this.SimulationFrequencyReported;

            //Setup all relay commands
            ToggleSimulationCommand = new RelayCommand(ToggleSimulation);
        }

        
        private void SimulationTimeStepDoWork(object sender, TimeSimulationStepEventArgs e)
        {
            //------------ Do Calculations -------------------------

            BallModel.CalculateTimeStep(e.TimeIncrement, HexaPodModel.Top.NormalVector);
            HexaPodModel.Top.CalculateTimeStep(e.TimeIncrement);
            BallModel.UpdateGlobalCoords(HexaPodModel.Top.CalcGlobalCoord(BallModel.Position));

            if (ServoActive)
                HexaPodModel.Top.RotateAbsServo(new double[] { XController.CalcOutput(BallModel.Position[0], e.TimeIncrement), YController.CalcOutput(BallModel.Position[1], e.TimeIncrement), 0 });

            //------------ Draw Screen -------------------------

            //GLCont.Refresh(); //needs to occur in foreground thread

            //------------ Cleanup -------------------------

            e.WorkDoneCallback.Set(); //allow simulation to proceed
        }
        

        /// <summary>
        /// Gets called whenever the simulation FPS is reported
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulationFrequencyReported(object sender, int e)
        {
            SimulationFPS = e;
        }

    }
}
