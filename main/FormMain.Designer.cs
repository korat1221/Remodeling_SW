namespace main
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            splitContainer1 = new SplitContainer();
            toolStripContainer1 = new ToolStripContainer();
            toolStrip1 = new ToolStrip();
            Home = new ToolStripButton();
            ProjectOpen = new ToolStripButton();
            EnergyNeed_Sim = new ToolStripButton();
            FinalEnergy_Sim = new ToolStripButton();
            Report_Rule = new ToolStripButton();
            Report_Remodeling = new ToolStripButton();
            Report_Detail = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Size = new Size(1259, 811);
            splitContainer1.SplitterDistance = 325;
            splitContainer1.TabIndex = 0;
            splitContainer1.Resize += OnResize;
            // 
            // toolStripContainer1
            // 
            toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(splitContainer1);
            toolStripContainer1.ContentPanel.Size = new Size(1259, 811);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.LeftToolStripPanelVisible = false;
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.RightToolStripPanelVisible = false;
            toolStripContainer1.Size = new Size(1259, 861);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.BackColor = SystemColors.GradientInactiveCaption;
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            toolStripContainer1.TopToolStripPanel.MinimumSize = new Size(0, 32);
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = SystemColors.GradientInactiveCaption;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(35, 35);
            toolStrip1.Items.AddRange(new ToolStripItem[] { Home, ProjectOpen, EnergyNeed_Sim, FinalEnergy_Sim, Report_Rule, Report_Remodeling, Report_Detail });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(4);
            toolStrip1.Size = new Size(323, 50);
            toolStrip1.TabIndex = 0;
            // 
            // Home
            // 
            Home.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Home.Image = (Image)resources.GetObject("Home.Image");
            Home.ImageTransparentColor = Color.Magenta;
            Home.Name = "Home";
            Home.Size = new Size(39, 39);
            Home.Text = "toolStripButton1";
            Home.Click += toolStripButton1_Click;
            // 
            // ProjectOpen
            // 
            ProjectOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ProjectOpen.Image = (Image)resources.GetObject("ProjectOpen.Image");
            ProjectOpen.ImageTransparentColor = Color.Magenta;
            ProjectOpen.Name = "ProjectOpen";
            ProjectOpen.Size = new Size(39, 39);
            ProjectOpen.Text = "toolStripButton4";
            ProjectOpen.Click += toolStripButton4_Click;
            // 
            // EnergyNeed_Sim
            // 
            EnergyNeed_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EnergyNeed_Sim.Image = (Image)resources.GetObject("EnergyNeed_Sim.Image");
            EnergyNeed_Sim.ImageTransparentColor = Color.Magenta;
            EnergyNeed_Sim.Name = "EnergyNeed_Sim";
            EnergyNeed_Sim.Size = new Size(39, 39);
            EnergyNeed_Sim.Text = "toolStripButton2";
            // 
            // FinalEnergy_Sim
            // 
            FinalEnergy_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FinalEnergy_Sim.Image = (Image)resources.GetObject("FinalEnergy_Sim.Image");
            FinalEnergy_Sim.ImageTransparentColor = Color.Magenta;
            FinalEnergy_Sim.Name = "FinalEnergy_Sim";
            FinalEnergy_Sim.Size = new Size(39, 39);
            FinalEnergy_Sim.Text = "toolStripButton5";
            // 
            // Report_Rule
            // 
            Report_Rule.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Report_Rule.Image = (Image)resources.GetObject("Report_Rule.Image");
            Report_Rule.ImageTransparentColor = Color.Magenta;
            Report_Rule.Name = "Report_Rule";
            Report_Rule.Size = new Size(39, 39);
            Report_Rule.Text = "toolStripButton6";
            // 
            // Report_Remodeling
            // 
            Report_Remodeling.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Report_Remodeling.Image = (Image)resources.GetObject("Report_Remodeling.Image");
            Report_Remodeling.ImageTransparentColor = Color.Magenta;
            Report_Remodeling.Name = "Report_Remodeling";
            Report_Remodeling.Size = new Size(39, 39);
            Report_Remodeling.Text = "toolStripButton7";
            // 
            // Report_Detail
            // 
            Report_Detail.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Report_Detail.Image = (Image)resources.GetObject("Report_Detail.Image");
            Report_Detail.ImageTransparentColor = Color.Magenta;
            Report_Detail.Name = "Report_Detail";
            Report_Detail.Size = new Size(39, 39);
            Report_Detail.Text = "toolStripButton8";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1259, 861);
            Controls.Add(toolStripContainer1);
            Name = "FormMain";
            Text = "FormMain";
            FormClosed += OnClosed;
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public SplitContainer splitContainer1;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        private ToolStripButton Home;
        private ToolStripButton ProjectOpen;
        private ToolStripButton EnergyNeed_Sim;
        private ToolStripButton FinalEnergy_Sim;
        private ToolStripButton Report_Rule;
        private ToolStripButton Report_Remodeling;
        private ToolStripButton Report_Detail;
    }
}