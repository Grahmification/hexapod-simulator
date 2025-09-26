using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GFunctions.Mathnet;

namespace Hexapod_Simulator.UserControls
{
    public partial class Control_ManualDrag : UserControl
    {
        private bool ignoreDrag = false; //slider position can be changed programmatically without throwing events
        private double posTravelRange = 50.0; //full travel range for each slider [mm]
        private double rotTravelRange = 100.0; //full travel range for each slider [deg]


        private Vector3 _position = new(0, 0, 0);
        private RotationPRY _rotation = new(0, 0, 0);

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                ignoreDrag = true;
                _position = value;

                calcPositionSlider(trackBar_x, _position.X);
                calcPositionSlider(trackBar_y, _position.Y);
                calcPositionSlider(trackBar_z, _position.Z);

                ignoreDrag = false;

                PositionChanged?.Invoke(this, new EventArgs());
            }
        }
        public RotationPRY Rotation
        {
            get { return _rotation; }
            set
            {
                ignoreDrag = true;
                _rotation = value;

                calcRotationSlider(trackBar_pitch, _rotation.Pitch);
                calcRotationSlider(trackBar_roll, _rotation.Roll);
                calcRotationSlider(trackBar_yaw, _rotation.Yaw);

                ignoreDrag = false;

                RotationChanged?.Invoke(this, new EventArgs());
            }
        }


        public event EventHandler? PositionChanged;
        public event EventHandler? RotationChanged;
   
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


        private void trackBar_pos_Scroll(object? sender, EventArgs e)
        {
            if (!ignoreDrag)
            {
                _position = new(calcPosition(trackBar_x), calcPosition(trackBar_y), calcPosition(trackBar_z));

                PositionChanged?.Invoke(this, new EventArgs());
            }
            
            
        }
        private void trackBar_rotate_Scroll(object? sender, EventArgs e)
        {
            if (!ignoreDrag)
            {
                _rotation = new(calcRotation(trackBar_pitch), calcRotation(trackBar_roll), calcRotation(trackBar_yaw));

                RotationChanged?.Invoke(this, new EventArgs());
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
            Position = new(0, 0, 0);
            Rotation = new(0, 0, 0);
        }

        private void Control_ManualDrag_Load(object sender, EventArgs e)
        {

        }
    }
}
