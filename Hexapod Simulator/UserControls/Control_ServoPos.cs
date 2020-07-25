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
    public partial class Control_ServoPos : UserControl
    {

        private List<Label> lbls = new List<Label>();

        public Control_ServoPos()
        {
            InitializeComponent();

            lbls.Add(label_pos1);
            lbls.Add(label_pos2);
            lbls.Add(label_pos3);
            lbls.Add(label_pos4);
            lbls.Add(label_pos5);
            lbls.Add(label_pos6);
        }

        public void SetAngle(int MotorIndex, double angle, bool solnValid)
        {
            Label lbl = lbls[MotorIndex];
            
            FormatText(lbl, angle);

            if (solnValid)
            {
                lbl.BackColor = Color.White;
            }
            else
            {
                lbl.BackColor = Color.IndianRed; 
            }
        }
        private void FormatText(Label lbl, double value)
        {
            value = Math.Round(value, 2);
            lbl.Text = Convert.ToString(value);
        }

        private void Control_CurrentPos_Load(object sender, EventArgs e)
        {

        }
    }
}
