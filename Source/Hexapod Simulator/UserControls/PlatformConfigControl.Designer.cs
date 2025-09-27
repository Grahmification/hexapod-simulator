using GFunctions.Winforms.Input;

namespace Hexapod_Simulator.UserControls
{
    partial class PlatformConfigControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.numericalInputTextBox_posZ = new GFunctions.Winforms.Input.NumericalInputTextBox();
            this.numericalInputTextBox_posY = new GFunctions.Winforms.Input.NumericalInputTextBox();
            this.numericalInputTextBox_posX = new GFunctions.Winforms.Input.NumericalInputTextBox();
            this.numericalInputTextBox_jointAngle = new GFunctions.Winforms.Input.NumericalInputTextBox();
            this.numericalInputTextBox_radius = new GFunctions.Winforms.Input.NumericalInputTextBox();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(3, 3);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(108, 13);
            this.label_title.TabIndex = 6;
            this.label_title.Text = "Top Configuration";
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Radius [mm]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Joint Offset Angle [deg]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Default Pos [mm]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(103, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Z";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(75, 173);
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
            this.numericalInputTextBox_posZ.Location = new System.Drawing.Point(129, 140);
            this.numericalInputTextBox_posZ.Name = "numericalInputTextBox_posZ";
            this.numericalInputTextBox_posZ.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posZ.TabIndex = 18;
            this.numericalInputTextBox_posZ.Text = "0";
            this.numericalInputTextBox_posZ.Value = 0D;
            // 
            // numericalInputTextBox_posY
            // 
            this.numericalInputTextBox_posY.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_posY.Location = new System.Drawing.Point(129, 114);
            this.numericalInputTextBox_posY.Name = "numericalInputTextBox_posY";
            this.numericalInputTextBox_posY.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posY.TabIndex = 17;
            this.numericalInputTextBox_posY.Text = "0";
            this.numericalInputTextBox_posY.Value = 0D;
            // 
            // numericalInputTextBox_posX
            // 
            this.numericalInputTextBox_posX.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_posX.Location = new System.Drawing.Point(129, 88);
            this.numericalInputTextBox_posX.Name = "numericalInputTextBox_posX";
            this.numericalInputTextBox_posX.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_posX.TabIndex = 16;
            this.numericalInputTextBox_posX.Text = "0";
            this.numericalInputTextBox_posX.Value = 0D;
            // 
            // numericalInputTextBox_jointAngle
            // 
            this.numericalInputTextBox_jointAngle.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_jointAngle.Location = new System.Drawing.Point(129, 55);
            this.numericalInputTextBox_jointAngle.Name = "numericalInputTextBox_jointAngle";
            this.numericalInputTextBox_jointAngle.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_jointAngle.TabIndex = 12;
            this.numericalInputTextBox_jointAngle.Text = "0";
            this.numericalInputTextBox_jointAngle.Value = 0D;
            // 
            // numericalInputTextBox_radius
            // 
            this.numericalInputTextBox_radius.BackColor = System.Drawing.Color.White;
            this.numericalInputTextBox_radius.Location = new System.Drawing.Point(129, 29);
            this.numericalInputTextBox_radius.Name = "numericalInputTextBox_radius";
            this.numericalInputTextBox_radius.Size = new System.Drawing.Size(100, 20);
            this.numericalInputTextBox_radius.TabIndex = 7;
            this.numericalInputTextBox_radius.Text = "0";
            this.numericalInputTextBox_radius.Value = 0D;
            // 
            // PlatformConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.numericalInputTextBox_posZ);
            this.Controls.Add(this.numericalInputTextBox_posY);
            this.Controls.Add(this.numericalInputTextBox_posX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericalInputTextBox_jointAngle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericalInputTextBox_radius);
            this.Controls.Add(this.label_title);
            this.Name = "PlatformConfigControl";
            this.Size = new System.Drawing.Size(236, 205);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private NumericalInputTextBox numericalInputTextBox_radius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private NumericalInputTextBox numericalInputTextBox_jointAngle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private NumericalInputTextBox numericalInputTextBox_posX;
        private NumericalInputTextBox numericalInputTextBox_posY;
        private NumericalInputTextBox numericalInputTextBox_posZ;
        private System.Windows.Forms.Button button_apply;
    }
}
