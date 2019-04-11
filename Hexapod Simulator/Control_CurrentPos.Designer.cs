namespace Hexapod_Simulator
{
    partial class Control_CurrentPos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_x = new System.Windows.Forms.Label();
            this.label_y = new System.Windows.Forms.Label();
            this.label_z = new System.Windows.Forms.Label();
            this.label_pitch = new System.Windows.Forms.Label();
            this.label_roll = new System.Windows.Forms.Label();
            this.label_yaw = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.Location = new System.Drawing.Point(3, 5);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(97, 13);
            this.label_title.TabIndex = 7;
            this.label_title.Text = "Current Position";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X [mm]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y [mm]:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Z [mm]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pitch [deg]:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Roll [deg]:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Yaw [deg]:";
            // 
            // label_x
            // 
            this.label_x.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_x.Location = new System.Drawing.Point(95, 25);
            this.label_x.Name = "label_x";
            this.label_x.Size = new System.Drawing.Size(80, 18);
            this.label_x.TabIndex = 14;
            this.label_x.Text = "-999.999";
            this.label_x.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_y
            // 
            this.label_y.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_y.Location = new System.Drawing.Point(95, 48);
            this.label_y.Name = "label_y";
            this.label_y.Size = new System.Drawing.Size(80, 18);
            this.label_y.TabIndex = 15;
            this.label_y.Text = "-999.999";
            this.label_y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_z
            // 
            this.label_z.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_z.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_z.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_z.Location = new System.Drawing.Point(95, 72);
            this.label_z.Name = "label_z";
            this.label_z.Size = new System.Drawing.Size(80, 18);
            this.label_z.TabIndex = 16;
            this.label_z.Text = "-999.999";
            this.label_z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_pitch
            // 
            this.label_pitch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_pitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_pitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pitch.Location = new System.Drawing.Point(95, 108);
            this.label_pitch.Name = "label_pitch";
            this.label_pitch.Size = new System.Drawing.Size(80, 18);
            this.label_pitch.TabIndex = 17;
            this.label_pitch.Text = "-999.999";
            this.label_pitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_roll
            // 
            this.label_roll.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_roll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_roll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_roll.Location = new System.Drawing.Point(95, 133);
            this.label_roll.Name = "label_roll";
            this.label_roll.Size = new System.Drawing.Size(80, 18);
            this.label_roll.TabIndex = 18;
            this.label_roll.Text = "-999.999";
            this.label_roll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_yaw
            // 
            this.label_yaw.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_yaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_yaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_yaw.Location = new System.Drawing.Point(95, 157);
            this.label_yaw.Name = "label_yaw";
            this.label_yaw.Size = new System.Drawing.Size(80, 18);
            this.label_yaw.TabIndex = 19;
            this.label_yaw.Text = "-999.999";
            this.label_yaw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Control_CurrentPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.label_yaw);
            this.Controls.Add(this.label_roll);
            this.Controls.Add(this.label_pitch);
            this.Controls.Add(this.label_z);
            this.Controls.Add(this.label_y);
            this.Controls.Add(this.label_x);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_title);
            this.Name = "Control_CurrentPos";
            this.Size = new System.Drawing.Size(236, 187);
            this.Load += new System.EventHandler(this.Control_CurrentPos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_x;
        private System.Windows.Forms.Label label_y;
        private System.Windows.Forms.Label label_z;
        private System.Windows.Forms.Label label_pitch;
        private System.Windows.Forms.Label label_roll;
        private System.Windows.Forms.Label label_yaw;
    }
}
