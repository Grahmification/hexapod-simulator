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
    public partial class Control_CurrentPos : UserControl
    {
       
        public Control_CurrentPos()
        {
            InitializeComponent();
        }

        public void SetPosition(Vector3 position){
            FormatText(label_x, position.X);
            FormatText(label_y, position.Y);
            FormatText(label_z, position.Z);
        }
        public void SetRotation(RotationPRY rotation)
        {
            FormatText(label_pitch, rotation.Pitch);
            FormatText(label_roll, rotation.Roll);
            FormatText(label_yaw, rotation.Yaw);
        }
        private void FormatText(Label lbl, double value)
        {
            value = Math.Round(value, 3);
            lbl.Text = Convert.ToString(value);
        }

        private void Control_CurrentPos_Load(object sender, EventArgs e)
        {

        }
    }
}
