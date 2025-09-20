using Hexapod_Simulator.UserControls;

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
            statusStrip_bottom = new StatusStrip();
            toolStripStatusLabel_simFreq = new ToolStripStatusLabel();
            toolStrip_top = new ToolStrip();
            toolStripButton_toggleSimulation = new ToolStripButton();
            toolStripButton_servoActive = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            toolStripTextBox_simInterval = new ToolStripTextBox();
            toolStripButton1 = new ToolStripButton();
            splitContainer_main = new SplitContainer();
            tabControl_main = new TabControl();
            tabPage_movement = new TabPage();
            control_ServoPos1 = new Control_ServoPos();
            control_CurrentPos1 = new Control_CurrentPos();
            control_ManualDrag_main = new Control_ManualDrag();
            tabPage_config = new TabPage();
            control_RotationCenter1 = new Control_RotationCenter();
            platformConfigControl_base = new PlatformConfigControl();
            platformConfigControl_top = new PlatformConfigControl();
            glControl_main = new OpenTK.GLControl.GLControl();
            statusStrip_bottom.SuspendLayout();
            toolStrip_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_main).BeginInit();
            splitContainer_main.Panel1.SuspendLayout();
            splitContainer_main.Panel2.SuspendLayout();
            splitContainer_main.SuspendLayout();
            tabControl_main.SuspendLayout();
            tabPage_movement.SuspendLayout();
            tabPage_config.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip_bottom
            // 
            statusStrip_bottom.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_simFreq });
            statusStrip_bottom.Location = new Point(0, 834);
            statusStrip_bottom.Name = "statusStrip_bottom";
            statusStrip_bottom.Padding = new Padding(1, 0, 16, 0);
            statusStrip_bottom.Size = new Size(1224, 22);
            statusStrip_bottom.TabIndex = 0;
            statusStrip_bottom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_simFreq
            // 
            toolStripStatusLabel_simFreq.Name = "toolStripStatusLabel_simFreq";
            toolStripStatusLabel_simFreq.Size = new Size(38, 17);
            toolStripStatusLabel_simFreq.Text = "FPS: 0";
            // 
            // toolStrip_top
            // 
            toolStrip_top.Items.AddRange(new ToolStripItem[] { toolStripButton_toggleSimulation, toolStripButton_servoActive, toolStripSeparator1, toolStripLabel1, toolStripTextBox_simInterval, toolStripButton1 });
            toolStrip_top.Location = new Point(0, 0);
            toolStrip_top.Name = "toolStrip_top";
            toolStrip_top.Size = new Size(1224, 25);
            toolStrip_top.TabIndex = 1;
            toolStrip_top.Text = "toolStrip1";
            // 
            // toolStripButton_toggleSimulation
            // 
            toolStripButton_toggleSimulation.Image = (Image)resources.GetObject("toolStripButton_toggleSimulation.Image");
            toolStripButton_toggleSimulation.ImageTransparentColor = Color.Magenta;
            toolStripButton_toggleSimulation.Name = "toolStripButton_toggleSimulation";
            toolStripButton_toggleSimulation.Size = new Size(111, 22);
            toolStripButton_toggleSimulation.Text = "Start Simulation";
            toolStripButton_toggleSimulation.Click += toolStripButton_toggleSimulation_Click;
            // 
            // toolStripButton_servoActive
            // 
            toolStripButton_servoActive.CheckOnClick = true;
            toolStripButton_servoActive.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton_servoActive.Image = (Image)resources.GetObject("toolStripButton_servoActive.Image");
            toolStripButton_servoActive.ImageTransparentColor = Color.Magenta;
            toolStripButton_servoActive.Name = "toolStripButton_servoActive";
            toolStripButton_servoActive.Size = new Size(76, 22);
            toolStripButton_servoActive.Text = "Servo Active";
            toolStripButton_servoActive.CheckStateChanged += toolStripButton_servoActive_CheckStateChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(76, 22);
            toolStripLabel1.Text = "Interval [ms]:";
            // 
            // toolStripTextBox_simInterval
            // 
            toolStripTextBox_simInterval.Name = "toolStripTextBox_simInterval";
            toolStripTextBox_simInterval.Size = new Size(116, 25);
            toolStripTextBox_simInterval.Text = "50";
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(79, 22);
            toolStripButton1.Text = "Trajectory";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // splitContainer_main
            // 
            splitContainer_main.Dock = DockStyle.Fill;
            splitContainer_main.FixedPanel = FixedPanel.Panel1;
            splitContainer_main.Location = new Point(0, 25);
            splitContainer_main.Margin = new Padding(4, 3, 4, 3);
            splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            splitContainer_main.Panel1.Controls.Add(tabControl_main);
            splitContainer_main.Panel1MinSize = 260;
            // 
            // splitContainer_main.Panel2
            // 
            splitContainer_main.Panel2.Controls.Add(glControl_main);
            splitContainer_main.Panel2MinSize = 260;
            splitContainer_main.Size = new Size(1224, 809);
            splitContainer_main.SplitterDistance = 303;
            splitContainer_main.SplitterWidth = 5;
            splitContainer_main.TabIndex = 2;
            // 
            // tabControl_main
            // 
            tabControl_main.Controls.Add(tabPage_movement);
            tabControl_main.Controls.Add(tabPage_config);
            tabControl_main.Dock = DockStyle.Fill;
            tabControl_main.Location = new Point(0, 0);
            tabControl_main.Margin = new Padding(4, 3, 4, 3);
            tabControl_main.Name = "tabControl_main";
            tabControl_main.SelectedIndex = 0;
            tabControl_main.Size = new Size(303, 809);
            tabControl_main.TabIndex = 0;
            // 
            // tabPage_movement
            // 
            tabPage_movement.Controls.Add(control_ServoPos1);
            tabPage_movement.Controls.Add(control_CurrentPos1);
            tabPage_movement.Controls.Add(control_ManualDrag_main);
            tabPage_movement.Location = new Point(4, 24);
            tabPage_movement.Margin = new Padding(4, 3, 4, 3);
            tabPage_movement.Name = "tabPage_movement";
            tabPage_movement.Padding = new Padding(4, 3, 4, 3);
            tabPage_movement.Size = new Size(295, 781);
            tabPage_movement.TabIndex = 0;
            tabPage_movement.Text = "Move";
            tabPage_movement.UseVisualStyleBackColor = true;
            // 
            // control_ServoPos1
            // 
            control_ServoPos1.BackColor = SystemColors.ControlDark;
            control_ServoPos1.BorderStyle = BorderStyle.Fixed3D;
            control_ServoPos1.Location = new Point(7, 485);
            control_ServoPos1.Margin = new Padding(5, 3, 5, 3);
            control_ServoPos1.Name = "control_ServoPos1";
            control_ServoPos1.Size = new Size(275, 197);
            control_ServoPos1.TabIndex = 3;
            // 
            // control_CurrentPos1
            // 
            control_CurrentPos1.BackColor = SystemColors.ControlDark;
            control_CurrentPos1.BorderStyle = BorderStyle.Fixed3D;
            control_CurrentPos1.Location = new Point(12, 262);
            control_CurrentPos1.Margin = new Padding(5, 3, 5, 3);
            control_CurrentPos1.Name = "control_CurrentPos1";
            control_CurrentPos1.Size = new Size(275, 215);
            control_CurrentPos1.TabIndex = 2;
            // 
            // control_ManualDrag_main
            // 
            control_ManualDrag_main.BackColor = SystemColors.ControlDark;
            control_ManualDrag_main.BorderStyle = BorderStyle.Fixed3D;
            control_ManualDrag_main.Location = new Point(7, 7);
            control_ManualDrag_main.Margin = new Padding(5, 3, 5, 3);
            control_ManualDrag_main.Name = "control_ManualDrag_main";
            control_ManualDrag_main.Position = new double[]
    {
    0D,
    0D,
    0D
    };
            control_ManualDrag_main.Rotation = new double[]
    {
    0D,
    0D,
    0D
    };
            control_ManualDrag_main.Size = new Size(275, 247);
            control_ManualDrag_main.TabIndex = 1;
            // 
            // tabPage_config
            // 
            tabPage_config.Controls.Add(control_RotationCenter1);
            tabPage_config.Controls.Add(platformConfigControl_base);
            tabPage_config.Controls.Add(platformConfigControl_top);
            tabPage_config.Location = new Point(4, 24);
            tabPage_config.Margin = new Padding(4, 3, 4, 3);
            tabPage_config.Name = "tabPage_config";
            tabPage_config.Padding = new Padding(4, 3, 4, 3);
            tabPage_config.Size = new Size(295, 781);
            tabPage_config.TabIndex = 1;
            tabPage_config.Text = "Config";
            tabPage_config.UseVisualStyleBackColor = true;
            // 
            // control_RotationCenter1
            // 
            control_RotationCenter1.BackColor = SystemColors.ControlDark;
            control_RotationCenter1.BorderStyle = BorderStyle.Fixed3D;
            control_RotationCenter1.Location = new Point(7, 494);
            control_RotationCenter1.Margin = new Padding(5, 3, 5, 3);
            control_RotationCenter1.Name = "control_RotationCenter1";
            control_RotationCenter1.Size = new Size(275, 196);
            control_RotationCenter1.TabIndex = 2;
            // 
            // platformConfigControl_base
            // 
            platformConfigControl_base.BackColor = SystemColors.ControlDark;
            platformConfigControl_base.BorderStyle = BorderStyle.Fixed3D;
            platformConfigControl_base.Location = new Point(7, 250);
            platformConfigControl_base.Margin = new Padding(5, 3, 5, 3);
            platformConfigControl_base.Name = "platformConfigControl_base";
            platformConfigControl_base.Size = new Size(275, 236);
            platformConfigControl_base.TabIndex = 1;
            // 
            // platformConfigControl_top
            // 
            platformConfigControl_top.BackColor = SystemColors.ControlDark;
            platformConfigControl_top.BorderStyle = BorderStyle.Fixed3D;
            platformConfigControl_top.Location = new Point(7, 7);
            platformConfigControl_top.Margin = new Padding(5, 3, 5, 3);
            platformConfigControl_top.Name = "platformConfigControl_top";
            platformConfigControl_top.Size = new Size(275, 236);
            platformConfigControl_top.TabIndex = 0;
            // 
            // glControl_main
            // 
            glControl_main.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl_main.APIVersion = new Version(2, 1, 0, 0);
            glControl_main.BackColor = Color.Black;
            glControl_main.Dock = DockStyle.Fill;
            glControl_main.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl_main.IsEventDriven = true;
            glControl_main.Location = new Point(0, 0);
            glControl_main.Margin = new Padding(4, 3, 4, 3);
            glControl_main.Name = "glControl_main";
            glControl_main.Profile = OpenTK.Windowing.Common.ContextProfile.Any;
            glControl_main.SharedContext = null;
            glControl_main.Size = new Size(916, 809);
            glControl_main.TabIndex = 0;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1224, 856);
            Controls.Add(splitContainer_main);
            Controls.Add(toolStrip_top);
            Controls.Add(statusStrip_bottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Mainform";
            Text = "Hexapod Simulator";
            FormClosing += Mainform_FormClosing;
            Load += Mainform_Load;
            statusStrip_bottom.ResumeLayout(false);
            statusStrip_bottom.PerformLayout();
            toolStrip_top.ResumeLayout(false);
            toolStrip_top.PerformLayout();
            splitContainer_main.Panel1.ResumeLayout(false);
            splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_main).EndInit();
            splitContainer_main.ResumeLayout(false);
            tabControl_main.ResumeLayout(false);
            tabPage_movement.ResumeLayout(false);
            tabPage_config.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip_bottom;
        private System.Windows.Forms.ToolStrip toolStrip_top;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private OpenTK.GLControl.GLControl glControl_main;
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

