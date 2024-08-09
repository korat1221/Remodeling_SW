namespace main.contents
{
    partial class ProjectCopy
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
            GeneralPanel = new Panel();
            label4 = new Label();
            System_checkBox = new CheckBox();
            System_pictureBox = new PictureBox();
            Zone_checkBox = new CheckBox();
            Zone_pictureBox = new PictureBox();
            Model_checkBox = new CheckBox();
            Model_pictureBox = new PictureBox();
            Construction_checkBox = new CheckBox();
            Building_checkBox = new CheckBox();
            Building_pictureBox = new PictureBox();
            Save_button = new Button();
            Construction_pictureBox = new PictureBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)System_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Zone_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Model_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Building_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Construction_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(System_checkBox);
            GeneralPanel.Controls.Add(System_pictureBox);
            GeneralPanel.Controls.Add(Zone_checkBox);
            GeneralPanel.Controls.Add(Zone_pictureBox);
            GeneralPanel.Controls.Add(Model_checkBox);
            GeneralPanel.Controls.Add(Model_pictureBox);
            GeneralPanel.Controls.Add(Construction_checkBox);
            GeneralPanel.Controls.Add(Building_checkBox);
            GeneralPanel.Controls.Add(Building_pictureBox);
            GeneralPanel.Controls.Add(Save_button);
            GeneralPanel.Controls.Add(Construction_pictureBox);
            GeneralPanel.Location = new Point(0, 0);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(713, 245);
            GeneralPanel.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(53, 29);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 131;
            label4.Text = "프로젝트 복사";
            // 
            // System_checkBox
            // 
            System_checkBox.AutoSize = true;
            System_checkBox.Enabled = false;
            System_checkBox.Location = new Point(520, 155);
            System_checkBox.Name = "System_checkBox";
            System_checkBox.Size = new Size(78, 19);
            System_checkBox.TabIndex = 118;
            System_checkBox.Text = "설비 정보";
            System_checkBox.UseVisualStyleBackColor = true;
            System_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // System_pictureBox
            // 
            System_pictureBox.Location = new Point(524, 80);
            System_pictureBox.Name = "System_pictureBox";
            System_pictureBox.Size = new Size(70, 60);
            System_pictureBox.TabIndex = 117;
            System_pictureBox.TabStop = false;
            // 
            // Zone_checkBox
            // 
            Zone_checkBox.AutoSize = true;
            Zone_checkBox.Enabled = false;
            Zone_checkBox.Location = new Point(422, 155);
            Zone_checkBox.Name = "Zone_checkBox";
            Zone_checkBox.Size = new Size(66, 19);
            Zone_checkBox.TabIndex = 116;
            Zone_checkBox.Text = "존 정보";
            Zone_checkBox.UseVisualStyleBackColor = true;
            Zone_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Zone_pictureBox
            // 
            Zone_pictureBox.Location = new Point(420, 80);
            Zone_pictureBox.Name = "Zone_pictureBox";
            Zone_pictureBox.Size = new Size(70, 60);
            Zone_pictureBox.TabIndex = 115;
            Zone_pictureBox.TabStop = false;
            // 
            // Model_checkBox
            // 
            Model_checkBox.AutoSize = true;
            Model_checkBox.Enabled = false;
            Model_checkBox.Location = new Point(316, 155);
            Model_checkBox.Name = "Model_checkBox";
            Model_checkBox.Size = new Size(70, 19);
            Model_checkBox.TabIndex = 114;
            Model_checkBox.Text = "3D 정보";
            Model_checkBox.UseVisualStyleBackColor = true;
            Model_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Model_pictureBox
            // 
            Model_pictureBox.Location = new Point(316, 80);
            Model_pictureBox.Name = "Model_pictureBox";
            Model_pictureBox.Size = new Size(70, 60);
            Model_pictureBox.TabIndex = 113;
            Model_pictureBox.TabStop = false;
            // 
            // Construction_checkBox
            // 
            Construction_checkBox.AutoSize = true;
            Construction_checkBox.Enabled = false;
            Construction_checkBox.Location = new Point(202, 155);
            Construction_checkBox.Name = "Construction_checkBox";
            Construction_checkBox.Size = new Size(90, 19);
            Construction_checkBox.TabIndex = 112;
            Construction_checkBox.Text = "구조체 정보";
            Construction_checkBox.UseVisualStyleBackColor = true;
            Construction_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Building_checkBox
            // 
            Building_checkBox.AutoSize = true;
            Building_checkBox.Checked = true;
            Building_checkBox.CheckState = CheckState.Checked;
            Building_checkBox.Location = new Point(104, 155);
            Building_checkBox.Name = "Building_checkBox";
            Building_checkBox.Size = new Size(78, 19);
            Building_checkBox.TabIndex = 110;
            Building_checkBox.Text = "건물 정보";
            Building_checkBox.UseVisualStyleBackColor = true;
            Building_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Building_pictureBox
            // 
            Building_pictureBox.Location = new Point(108, 80);
            Building_pictureBox.Name = "Building_pictureBox";
            Building_pictureBox.Size = new Size(70, 60);
            Building_pictureBox.TabIndex = 109;
            Building_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(524, 203);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Construction_pictureBox
            // 
            Construction_pictureBox.Location = new Point(212, 80);
            Construction_pictureBox.Name = "Construction_pictureBox";
            Construction_pictureBox.Size = new Size(70, 60);
            Construction_pictureBox.TabIndex = 111;
            Construction_pictureBox.TabStop = false;
            // 
            // ProjectCopy
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(710, 243);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ProjectCopy";
            Text = "프로젝트 복사";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)System_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Zone_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Model_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Building_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Construction_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private CheckBox System_checkBox;
        private PictureBox System_pictureBox;
        private CheckBox Zone_checkBox;
        private PictureBox Zone_pictureBox;
        private CheckBox Model_checkBox;
        private PictureBox Model_pictureBox;
        private CheckBox Construction_checkBox;
        private PictureBox Construction_pictureBox;
        private CheckBox Building_checkBox;
        private PictureBox Building_pictureBox;
        private Label label4;
    }
}