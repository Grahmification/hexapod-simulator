using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexapod_Simulator
{
    public partial class Control_ManualDrag : UserControl
    {
        private bool ignoreDrag = false; //slider position can be changed programmatically without throwing events
        private double posTravelRange = 50.0; //full travel range for each slider [mm]
        private double rotTravelRange = 100.0; //full travel range for each slider [deg]


        private double[] _position = new double[] { 0, 0, 0 };
        private double[] _rotation = new double[] { 0, 0, 0 };

        public double[] Position
        {
            get { return this._position; }
            set
            {
                ignoreDrag = true;
                this._position = value;

                calcPositionSlider(trackBar_x, _position[0]);
                calcPositionSlider(trackBar_y, _position[1]);
                calcPositionSlider(trackBar_z, _position[2]);

                ignoreDrag = false;

                if (PositionChanged != null)
                    PositionChanged(this, new EventArgs());
            }
        }
        public double[] Rotation
        {
            get { return this._rotation; }
            set
            {
                ignoreDrag = true;
                this._rotation = value;

                calcRotationSlider(trackBar_pitch, _rotation[0]);
                calcRotationSlider(trackBar_roll, _rotation[1]);
                calcRotationSlider(trackBar_yaw, _rotation[2]);

                ignoreDrag = false;

                if (RotationChanged != null)
                    RotationChanged(this, new EventArgs());
            }
        }


        public event EventHandler PositionChanged;
        public event EventHandler RotationChanged;
   
        public Control_ManualDrag()
        {
            InitializeComponent();

            this.trackBar_x.Scroll += new System.EventHandler(this.trackBar_pos_Scroll);
            this.trackBar_y.Scroll += new System.EventHandler(this.trackBar_pos_Scroll);
            this.trackBar_z.Scroll += new System.EventHandler(this.trackBar_pos_Scroll);

            this.trackBar_pitch.Scroll += new System.EventHandler(this.trackBar_rotate_Scroll);
            this.trackBar_roll.Scroll += new System.EventHandler(this.trackBar_rotate_Scroll);
            this.trackBar_yaw.Scroll += new System.EventHandler(this.trackBar_rotate_Scroll);
        }
        public void ResetPos()
        {
            button_reset.PerformClick();
        }

    



        private void trackBar_pos_Scroll(object sender, EventArgs e)
        {
            if (this.ignoreDrag == false)
            {
                this._position = new double[] { calcPosition(trackBar_x), calcPosition(trackBar_y), calcPosition(trackBar_z) };

                if (PositionChanged != null)
                    PositionChanged(this, new EventArgs());
            }
            
            
        }
        private void trackBar_rotate_Scroll(object sender, EventArgs e)
        {
            if (this.ignoreDrag == false)
            {
                this._rotation = new double[] { calcRotation(trackBar_pitch), calcRotation(trackBar_roll), calcRotation(trackBar_yaw) };

                if (RotationChanged != null)
                    RotationChanged(this, new EventArgs());
            }
        }
        private double calcPosition(TrackBar Bar)
        {
            return (Bar.Value - Bar.Maximum / 2.0) * posTravelRange / (double)Bar.Maximum; 
        }
        private double calcRotation(TrackBar Bar)
        {
            return (Bar.Value - Bar.Maximum / 2.0) * rotTravelRange / (double)Bar.Maximum; 
        }



        private void calcPositionSlider(TrackBar Bar, double Pos)
        {
            double val = (Pos * Bar.Maximum / posTravelRange) + Bar.Maximum / 2.0;    
            int intVal = (int)Math.Round(val, 0);

            if (intVal > Bar.Maximum)
                intVal = Bar.Maximum;

            if (intVal < Bar.Minimum)
                intVal = Bar.Minimum;

            Bar.Value = intVal;                  
        }
        private void calcRotationSlider(TrackBar Bar, double Rot)
        {
            double val = (Rot * Bar.Maximum / rotTravelRange) + Bar.Maximum / 2.0;
            int intVal = (int)Math.Round(val, 0);

            if (intVal > Bar.Maximum)
                intVal = Bar.Maximum;

            if (intVal < Bar.Minimum)
                intVal = Bar.Minimum;

            Bar.Value = intVal;     
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            this.Position = new double[] { 0, 0, 0 };
            this.Rotation = new double[] { 0, 0, 0 };
        }

        private void Control_ManualDrag_Load(object sender, EventArgs e)
        {

        }


    }
}
