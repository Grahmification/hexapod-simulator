using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hexapod_Simulator.SimObject;
using GFunctions.Winforms.Controls;
using Hexapod_Simulator.Shared;

namespace Hexapod_Simulator.UserControls
{
    public partial class Control_RotationCenter : UserControl
    {
        private List<NumericalInputTextBox> Txts = new List<NumericalInputTextBox>();
        private IPlatform platform;
        
        
        public Control_RotationCenter()
        {
            InitializeComponent();
        }

        public void AssignPlatform(IPlatform platform)
        {
            this.platform = platform;

            label_title.Text = platform.Name + " Rotation Center";

            checkBox_fixedCenter.Checked = platform.FixedRotationCenter; 

            numericalInputTextBox_posX.Value = platform.RotationCenter[0];
            numericalInputTextBox_posY.Value = platform.RotationCenter[1];
            numericalInputTextBox_posZ.Value = platform.RotationCenter[2];
       
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
     
            double[] Position = new double[] { numericalInputTextBox_posX.Value, numericalInputTextBox_posY.Value, numericalInputTextBox_posZ.Value };

            platform.UpdateRotationCenter(Position, checkBox_fixedCenter.Checked);
        }

     
    }
}
