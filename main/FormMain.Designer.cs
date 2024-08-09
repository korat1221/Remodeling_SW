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
            Element_Sim = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
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
            toolStripContainer1.TopToolStripPanel.BackColor = Color.FromArgb(166, 201, 232);
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            toolStripContainer1.TopToolStripPanel.MinimumSize = new Size(0, 32);
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.FromArgb(166, 201, 232);
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(35, 35);
            toolStrip1.Items.AddRange(new ToolStripItem[] { Home, toolStripSeparator2, ProjectOpen, toolStripSeparator1, EnergyNeed_Sim, toolStripSeparator3, FinalEnergy_Sim, toolStripSeparator4, Element_Sim });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(4);
            toolStrip1.Size = new Size(238, 50);
            toolStrip1.TabIndex = 0;
            // 
            // Home
            // 
            Home.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Home.Image = (Image)resources.GetObject("Home.Image");
            Home.ImageTransparentColor = Color.Magenta;
            Home.Name = "Home";
            Home.Size = new Size(39, 39);
            Home.Text = "Home";
            Home.Click += toolStripButton1_Click;
            // 
            // ProjectOpen
            // 
            ProjectOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ProjectOpen.Image = (Image)resources.GetObject("ProjectOpen.Image");
            ProjectOpen.ImageTransparentColor = Color.Magenta;
            ProjectOpen.Name = "ProjectOpen";
            ProjectOpen.Size = new Size(39, 39);
            ProjectOpen.Text = "ProjectOpenButton";
            ProjectOpen.ToolTipText = "Project List";
            ProjectOpen.Click += ProjectOpen_Click;
            // 
            // EnergyNeed_Sim
            // 
            EnergyNeed_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EnergyNeed_Sim.Image = (Image)resources.GetObject("EnergyNeed_Sim.Image");
            EnergyNeed_Sim.ImageTransparentColor = Color.Magenta;
            EnergyNeed_Sim.Name = "EnergyNeed_Sim";
            EnergyNeed_Sim.Size = new Size(39, 39);
            EnergyNeed_Sim.Text = "Energy needs";
            EnergyNeed_Sim.Click += EnergyNeed_Sim_Click;
            // 
            // FinalEnergy_Sim
            // 
            FinalEnergy_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FinalEnergy_Sim.Image = (Image)resources.GetObject("FinalEnergy_Sim.Image");
            FinalEnergy_Sim.ImageTransparentColor = Color.Magenta;
            FinalEnergy_Sim.Name = "FinalEnergy_Sim";
            FinalEnergy_Sim.Size = new Size(39, 39);
            FinalEnergy_Sim.Text = "Final Energy";
            FinalEnergy_Sim.Click += FinalEnergy_Sim_Click;
            // 
            // Element_Sim
            // 
            Element_Sim.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Element_Sim.Image = (Image)resources.GetObject("Element_Sim.Image");
            Element_Sim.ImageTransparentColor = Color.Magenta;
            Element_Sim.Name = "Element_Sim";
            Element_Sim.Size = new Size(39, 39);
            Element_Sim.Text = "Components result";
            Element_Sim.Click += Element_Sim_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 42);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 42);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 42);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 42);
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
        private ToolStripButton Element_Sim;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
    }
}