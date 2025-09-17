using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hexapod_Simulator.SimObject;
using GFunctions.Winforms.Input;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.UserControls
{
    public partial class PlatformConfigControl : UserControl
    {
        private List<NumericalInputTextBox> Txts = new List<NumericalInputTextBox>();
        private IPlatform platform;
        
        
        public PlatformConfigControl()
        {
            InitializeComponent();
        }

        public void AssignPlatform(IPlatform platform)
        {
            this.platform = platform;

            label_title.Text = platform.Name + " Configuration";

            numericalInputTextBox_radius.Value = platform.Radius;
            numericalInputTextBox_jointAngle.Value = platform.JointAngle;
            numericalInputTextBox_posX.Value = platform.DefaultPos[0];
            numericalInputTextBox_posY.Value = platform.DefaultPos[1];
            numericalInputTextBox_posZ.Value = platform.DefaultPos[2];
       
            foreach (Control cont in this.Controls)
            {
                if (cont is NumericalInputTextBox)                
                    Txts.Add((NumericalInputTextBox)cont);              
            }
        }
        private void button_apply_Click(object sender, EventArgs e)
        {
            foreach (NumericalInputTextBox txt in Txts) //make sure all textboxes are valid
            {
                if (txt.TextValid == false)
                    return;
            }

            double rad = numericalInputTextBox_radius.Value;
            double angle = numericalInputTextBox_jointAngle.Value;
            double[] defaultPos = new double[] { numericalInputTextBox_posX.Value, numericalInputTextBox_posY.Value, numericalInputTextBox_posZ.Value };

            platform.UpdateConfig(rad, angle, defaultPos);
        }
    }
}
