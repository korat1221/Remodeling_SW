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
            New = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            ProjectOpen = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            EnergyNeed_Sim = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            AHUNeed_Sim = new ToolStripButton();
            FinalEnergy_Sim = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            Element_Sim = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
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
            splitContainer1.Size = new Size(1664, 709);
            splitContainer1.SplitterDistance = 360;
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
            toolStripContainer1.ContentPanel.Size = new Size(1664, 709);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.LeftToolStripPanelVisible = false;
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.RightToolStripPanelVisible = false;
            toolStripContainer1.Size = new Size(1664, 771);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.BackColor = Color.FromArgb(39, 97, 143);
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            toolStripContainer1.TopToolStripPanel.MinimumSize = new Size(0, 32);
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.Transparent;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(35, 35);
            toolStrip1.Items.AddRange(new ToolStripItem[] { New, toolStripSeparator2, ProjectOpen, toolStripSeparator1, EnergyNeed_Sim, toolStripSeparator3, AHUNeed_Sim, toolStripSeparator5, FinalEnergy_Sim, toolStripSeparator4, Element_Sim });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(427, 62);
            toolStrip1.TabIndex = 0;
            // 
            // New
            // 
            New.DisplayStyle = ToolStripItemDisplayStyle.Image;
            New.Image = (Image)resources.GetObject("New.Image");
            New.ImageTransparentColor = Color.Magenta;
            New.Name = "New";
            New.Padding = new Padding(0, 0, 20, 20);
            New.Size = new Size(59, 59);
            New.Text = "Home";
            New.Click += toolStripButton1_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 62);
            // 
            // ProjectOpen
            // 
            ProjectOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ProjectOpen.Image = (Image)resources.GetObject("ProjectOpen.Image");
            ProjectOpen.ImageTransparentColor = Color.Magenta;
            ProjectOpen.Name = "ProjectOpen";
            ProjectOpen.Padding = new Padding(0, 0, 20, 20);
            ProjectOpen.Size = new Size(59, 59);
            ProjectOpen.Text = "ProjectOpen";
            ProjectOpen.ToolTipText = "ProjectOpen";
            ProjectOpen.Click += ProjectOpen_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 62);
            // 
            // EnergyNeed_Sim
            // 
            EnergyNeed_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EnergyNeed_Sim.Image = (Image)resources.GetObject("EnergyNeed_Sim.Image");
            EnergyNeed_Sim.ImageTransparentColor = Color.Magenta;
            EnergyNeed_Sim.Name = "EnergyNeed_Sim";
            EnergyNeed_Sim.Padding = new Padding(0, 0, 20, 20);
            EnergyNeed_Sim.Size = new Size(59, 59);
            EnergyNeed_Sim.Text = "Energy needs";
            EnergyNeed_Sim.Click += EnergyNeed_Sim_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 62);
            // 
            // AHUNeed_Sim
            // 
            AHUNeed_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AHUNeed_Sim.Image = (Image)resources.GetObject("AHUNeed_Sim.Image");
            AHUNeed_Sim.ImageTransparentColor = Color.Magenta;
            AHUNeed_Sim.Name = "AHUNeed_Sim";
            AHUNeed_Sim.Padding = new Padding(0, 0, 20, 20);
            AHUNeed_Sim.Size = new Size(59, 59);
            AHUNeed_Sim.Text = "toolStripButton1";
            AHUNeed_Sim.Click += AHUNeed_Sim_Click;
            // 
            // FinalEnergy_Sim
            // 
            FinalEnergy_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FinalEnergy_Sim.Image = (Image)resources.GetObject("FinalEnergy_Sim.Image");
            FinalEnergy_Sim.ImageTransparentColor = Color.Magenta;
            FinalEnergy_Sim.Name = "FinalEnergy_Sim";
            FinalEnergy_Sim.Padding = new Padding(0, 0, 20, 20);
            FinalEnergy_Sim.Size = new Size(59, 59);
            FinalEnergy_Sim.Text = "Final Energy";
            FinalEnergy_Sim.Click += FinalEnergy_Sim_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 62);
            // 
            // Element_Sim
            // 
            Element_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Element_Sim.Image = (Image)resources.GetObject("Element_Sim.Image");
            Element_Sim.ImageTransparentColor = Color.Magenta;
            Element_Sim.Name = "Element_Sim";
            Element_Sim.Padding = new Padding(0, 0, 20, 20);
            Element_Sim.Size = new Size(59, 59);
            Element_Sim.Text = "Components result";
            Element_Sim.Click += Element_Sim_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 62);
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1664, 771);
            Controls.Add(toolStripContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Text = "ZEROFIX";
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
        private ToolStripButton New;
        private ToolStripButton ProjectOpen;
        private ToolStripButton EnergyNeed_Sim;
        private ToolStripButton FinalEnergy_Sim;
        private ToolStripButton Element_Sim;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButton1;
        private ToolStripButton AHUNeed_Sim;
        private ToolStripSeparator toolStripSeparator5;
    }
}