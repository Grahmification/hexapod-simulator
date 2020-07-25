using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexapod_Simulator.UserControls
{
    public partial class Control_CurrentPos : UserControl
    {
       
        public Control_CurrentPos()
        {
            InitializeComponent();
        }

        public void SetPosition(double[] Pos){
            FormatText(label_x, Pos[0]);
            FormatText(label_y, Pos[1]);
            FormatText(label_z, Pos[2]);
        }
        public void SetRotation(double[] Rot)
        {
            FormatText(label_pitch, Rot[0]);
            FormatText(label_roll, Rot[1]);
            FormatText(label_yaw, Rot[2]);
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
