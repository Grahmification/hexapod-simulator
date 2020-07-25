using GFunctions.Winforms.Controls;

namespace Hexapod_Simulator.UserControls
{
    partial class Control_RotationCenter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_title = new System.Windows.Forms.Label();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.numericalInputTextBox_posZ = new GFunctions.Winforms.Controls.NumericalInputTextBox();
            this.numericalInputTextBox_posY = new GFunctions.Winforms.Controls.NumericalInputTextBox();
            this.numericalInputTextBox_posX = new GFunctions.Winforms.Controls.NumericalInputTextBox();
            this.checkBox_fixedCenter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(3, 3);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(122, 13);
            this.label_title.TabIndex = 6;
            this.label_title.Text = "Top Rotation Center";
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Relative Pos [mm]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(103, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Z";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(75, 139);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 19;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // numericalInputTextBox_posZ
            // 
            this.numericalInputTextBox_posZ.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_posZ.Location = new System.Drawing.Point(129, 106);
            this.numericalInputTextBox_posZ.Name = "numericalInputTextBox_posZ";
            this.numericalInputTextBox_posZ.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posZ.TabIndex = 18;
            this.numericalInputTextBox_posZ.Text = "0";
            this.numericalInputTextBox_posZ.Value = 0D;
            // 
            // numericalInputTextBox_posY
            // 
            this.numericalInputTextBox_posY.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_posY.Location = new System.Drawing.Point(129, 80);
            this.numericalInputTextBox_posY.Name = "numericalInputTextBox_posY";
            this.numericalInputTextBox_posY.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posY.TabIndex = 17;
            this.numericalInputTextBox_posY.Text = "0";
            this.numericalInputTextBox_posY.Value = 0D;
            // 
            // numericalInputTextBox_posX
            // 
            this.numericalInputTextBox_posX.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_posX.Location = new System.Drawing.Point(129, 54);
            this.numericalInputTextBox_posX.Name = "numericalInputTextBox_posX";
            this.numericalInputTextBox_posX.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posX.TabIndex = 16;
            this.numericalInputTextBox_posX.Text = "0";
            this.numericalInputTextBox_posX.Value = 0D;
            // 
            // checkBox_fixedCenter
            // 
            this.checkBox_fixedCenter.AutoSize = true;
            this.checkBox_fixedCenter.Location = new System.Drawing.Point(53, 28);
            this.checkBox_fixedCenter.Name = "checkBox_fixedCenter";
            this.checkBox_fixedCenter.Size = new System.Drawing.Size(128, 17);
            this.checkBox_fixedCenter.TabIndex = 20;
            this.checkBox_fixedCenter.Text = "Fixed Rotation Center";
            this.checkBox_fixedCenter.UseVisualStyleBackColor = true;
            // 
            // Control_RotationCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.checkBox_fixedCenter);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.numericalInputTextBox_posZ);
            this.Controls.Add(this.numericalInputTextBox_posY);
            this.Controls.Add(this.numericalInputTextBox_posX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_title);
            this.Name = "Control_RotationCenter";
            this.Size = new System.Drawing.Size(236, 170);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private NumericalInputTextBox numericalInputTextBox_posX;
        private NumericalInputTextBox numericalInputTextBox_posY;
        private NumericalInputTextBox numericalInputTextBox_posZ;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.CheckBox checkBox_fixedCenter;
    }
}
