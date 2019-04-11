namespace Hexapod_Simulator
{
    partial class Mainform
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.statusStrip_bottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_simFreq = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_top = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_toggleSimulation = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_servoActive = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_simInterval = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_movement = new System.Windows.Forms.TabPage();
            this.control_ServoPos1 = new Hexapod_Simulator.Control_ServoPos();
            this.control_CurrentPos1 = new Hexapod_Simulator.Control_CurrentPos();
            this.control_ManualDrag_main = new Hexapod_Simulator.Control_ManualDrag();
            this.tabPage_config = new System.Windows.Forms.TabPage();
            this.control_RotationCenter1 = new Hexapod_Simulator.Control_RotationCenter();
            this.platformConfigControl_base = new Hexapod_Simulator.PlatformConfigControl();
            this.platformConfigControl_top = new Hexapod_Simulator.PlatformConfigControl();
            this.glControl_main = new OpenTK.GLControl();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip_bottom.SuspendLayout();
            this.toolStrip_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_movement.SuspendLayout();
            this.tabPage_config.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip_bottom
            // 
            this.statusStrip_bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_simFreq});
            this.statusStrip_bottom.Location = new System.Drawing.Point(0, 720);
            this.statusStrip_bottom.Name = "statusStrip_bottom";
            this.statusStrip_bottom.Size = new System.Drawing.Size(1049, 22);
            this.statusStrip_bottom.TabIndex = 0;
            this.statusStrip_bottom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_simFreq
            // 
            this.toolStripStatusLabel_simFreq.Name = "toolStripStatusLabel_simFreq";
            this.toolStripStatusLabel_simFreq.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel_simFreq.Text = "FPS: 0";
            // 
            // toolStrip_top
            // 
            this.toolStrip_top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_toggleSimulation,
            this.toolStripButton_servoActive,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBox_simInterval,
            this.toolStripButton1});
            this.toolStrip_top.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_top.Name = "toolStrip_top";
            this.toolStrip_top.Size = new System.Drawing.Size(1049, 25);
            this.toolStrip_top.TabIndex = 1;
            this.toolStrip_top.Text = "toolStrip1";
            // 
            // toolStripButton_toggleSimulation
            // 
            this.toolStripButton_toggleSimulation.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_toggleSimulation.Image")));
            this.toolStripButton_toggleSimulation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_toggleSimulation.Name = "toolStripButton_toggleSimulation";
            this.toolStripButton_toggleSimulation.Size = new System.Drawing.Size(111, 22);
            this.toolStripButton_toggleSimulation.Text = "Start Simulation";
            this.toolStripButton_toggleSimulation.Click += new System.EventHandler(this.toolStripButton_toggleSimulation_Click);
            // 
            // toolStripButton_servoActive
            // 
            this.toolStripButton_servoActive.CheckOnClick = true;
            this.toolStripButton_servoActive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_servoActive.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_servoActive.Image")));
            this.toolStripButton_servoActive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_servoActive.Name = "toolStripButton_servoActive";
            this.toolStripButton_servoActive.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton_servoActive.Text = "Servo Active";
            this.toolStripButton_servoActive.CheckStateChanged += new System.EventHandler(this.toolStripButton_servoActive_CheckStateChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(76, 22);
            this.toolStripLabel1.Text = "Interval [ms]:";
            // 
            // toolStripTextBox_simInterval
            // 
            this.toolStripTextBox_simInterval.Name = "toolStripTextBox_simInterval";
            this.toolStripTextBox_simInterval.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox_simInterval.Text = "50";
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 25);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.tabControl_main);
            this.splitContainer_main.Panel1MinSize = 260;
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.glControl_main);
            this.splitContainer_main.Panel2MinSize = 260;
            this.splitContainer_main.Size = new System.Drawing.Size(1049, 695);
            this.splitContainer_main.SplitterDistance = 260;
            this.splitContainer_main.TabIndex = 2;
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_movement);
            this.tabControl_main.Controls.Add(this.tabPage_config);
            this.tabControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_main.Location = new System.Drawing.Point(0, 0);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(260, 695);
            this.tabControl_main.TabIndex = 0;
            // 
            // tabPage_movement
            // 
            this.tabPage_movement.Controls.Add(this.control_ServoPos1);
            this.tabPage_movement.Controls.Add(this.control_CurrentPos1);
            this.tabPage_movement.Controls.Add(this.control_ManualDrag_main);
            this.tabPage_movement.Location = new System.Drawing.Point(4, 22);
            this.tabPage_movement.Name = "tabPage_movement";
            this.tabPage_movement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_movement.Size = new System.Drawing.Size(252, 669);
            this.tabPage_movement.TabIndex = 0;
            this.tabPage_movement.Text = "Move";
            this.tabPage_movement.UseVisualStyleBackColor = true;
            // 
            // control_ServoPos1
            // 
            this.control_ServoPos1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.control_ServoPos1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.control_ServoPos1.Location = new System.Drawing.Point(6, 420);
            this.control_ServoPos1.Name = "control_ServoPos1";
            this.control_ServoPos1.Size = new System.Drawing.Size(236, 171);
            this.control_ServoPos1.TabIndex = 3;
            // 
            // control_CurrentPos1
            // 
            this.control_CurrentPos1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.control_CurrentPos1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.control_CurrentPos1.Location = new System.Drawing.Point(6, 227);
            this.control_CurrentPos1.Name = "control_CurrentPos1";
            this.control_CurrentPos1.Size = new System.Drawing.Size(236, 187);
            this.control_CurrentPos1.TabIndex = 2;
            // 
            // control_ManualDrag_main
            // 
            this.control_ManualDrag_main.BackColor = System.Drawing.SystemColors.ControlDark;
            this.control_ManualDrag_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.control_ManualDrag_main.Location = new System.Drawing.Point(6, 6);
            this.control_ManualDrag_main.Name = "control_ManualDrag_main";
            this.control_ManualDrag_main.Position = new double[] {
        0D,
        0D,
        0D};
            this.control_ManualDrag_main.Rotation = new double[] {
        0D,
        0D,
        0D};
            this.control_ManualDrag_main.Size = new System.Drawing.Size(236, 215);
            this.control_ManualDrag_main.TabIndex = 1;
            // 
            // tabPage_config
            // 
            this.tabPage_config.Controls.Add(this.control_RotationCenter1);
            this.tabPage_config.Controls.Add(this.platformConfigControl_base);
            this.tabPage_config.Controls.Add(this.platformConfigControl_top);
            this.tabPage_config.Location = new System.Drawing.Point(4, 22);
            this.tabPage_config.Name = "tabPage_config";
            this.tabPage_config.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_config.Size = new System.Drawing.Size(252, 669);
            this.tabPage_config.TabIndex = 1;
            this.tabPage_config.Text = "Config";
            this.tabPage_config.UseVisualStyleBackColor = true;
            // 
            // control_RotationCenter1
            // 
            this.control_RotationCenter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.control_RotationCenter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.control_RotationCenter1.Location = new System.Drawing.Point(6, 428);
            this.control_RotationCenter1.Name = "control_RotationCenter1";
            this.control_RotationCenter1.Size = new System.Drawing.Size(236, 170);
            this.control_RotationCenter1.TabIndex = 2;
            // 
            // platformConfigControl_base
            // 
            this.platformConfigControl_base.BackColor = System.Drawing.SystemColors.ControlDark;
            this.platformConfigControl_base.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.platformConfigControl_base.Location = new System.Drawing.Point(6, 217);
            this.platformConfigControl_base.Name = "platformConfigControl_base";
            this.platformConfigControl_base.Size = new System.Drawing.Size(236, 205);
            this.platformConfigControl_base.TabIndex = 1;
            // 
            // platformConfigControl_top
            // 
            this.platformConfigControl_top.BackColor = System.Drawing.SystemColors.ControlDark;
            this.platformConfigControl_top.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.platformConfigControl_top.Location = new System.Drawing.Point(6, 6);
            this.platformConfigControl_top.Name = "platformConfigControl_top";
            this.platformConfigControl_top.Size = new System.Drawing.Size(236, 205);
            this.platformConfigControl_top.TabIndex = 0;
            // 
            // glControl_main
            // 
            this.glControl_main.BackColor = System.Drawing.Color.Black;
            this.glControl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl_main.Location = new System.Drawing.Point(0, 0);
            this.glControl_main.Name = "glControl_main";
            this.glControl_main.Size = new System.Drawing.Size(785, 695);
            this.glControl_main.TabIndex = 0;
            this.glControl_main.VSync = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(80, 22);
            this.toolStripButton1.Text = "Trajectory";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 742);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.toolStrip_top);
            this.Controls.Add(this.statusStrip_bottom);
            this.Name = "Mainform";
            this.Text = "Hexapod Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainform_FormClosing);
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.statusStrip_bottom.ResumeLayout(false);
            this.statusStrip_bottom.PerformLayout();
            this.toolStrip_top.ResumeLayout(false);
            this.toolStrip_top.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_movement.ResumeLayout(false);
            this.tabPage_config.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip_bottom;
        private System.Windows.Forms.ToolStrip toolStrip_top;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private OpenTK.GLControl glControl_main;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_movement;
        private System.Windows.Forms.TabPage tabPage_config;
        private PlatformConfigControl platformConfigControl_top;
        private PlatformConfigControl platformConfigControl_base;
        private Control_ManualDrag control_ManualDrag_main;
        private Control_CurrentPos control_CurrentPos1;
        private Control_ServoPos control_ServoPos1;
        private System.Windows.Forms.ToolStripButton toolStripButton_toggleSimulation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_simFreq;
        private System.Windows.Forms.ToolStripButton toolStripButton_servoActive;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_simInterval;
        private Control_RotationCenter control_RotationCenter1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

