namespace main.contents
{
    partial class Intro
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
            Save_button = new Button();
            label28 = new Label();
            panel5 = new Panel();
            ProjectName_textBox = new TextBox();
            Logo_pictureBox = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            panel1 = new Panel();
            ProjectType_pictureBox = new PictureBox();
            ProjectType_textBox = new TextBox();
            GeneralPanel = new Panel();
            Import_button = new Button();
            ((System.ComponentModel.ISupportInitialize)Logo_pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProjectType_pictureBox).BeginInit();
            GeneralPanel.SuspendLayout();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(856, 603);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 92;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label28.Location = new Point(62, 247);
            label28.Name = "label28";
            label28.Size = new Size(118, 30);
            label28.TabIndex = 104;
            label28.Text = "프로젝트명";
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Location = new Point(68, 352);
            panel5.Name = "panel5";
            panel5.Size = new Size(415, 232);
            panel5.TabIndex = 105;
            panel5.Visible = false;
            panel5.Paint += panel5_Paint;
            // 
            // ProjectName_textBox
            // 
            ProjectName_textBox.BackColor = Color.White;
            ProjectName_textBox.BorderStyle = BorderStyle.FixedSingle;
            ProjectName_textBox.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            ProjectName_textBox.ForeColor = SystemColors.ControlText;
            ProjectName_textBox.Location = new Point(208, 247);
            ProjectName_textBox.Name = "ProjectName_textBox";
            ProjectName_textBox.Size = new Size(269, 29);
            ProjectName_textBox.TabIndex = 129;
            ProjectName_textBox.TextAlign = HorizontalAlignment.Center;
            ProjectName_textBox.TextChanged += ProjectName_textBox_TextChanged;
            // 
            // Logo_pictureBox
            // 
            Logo_pictureBox.Location = new Point(62, 97);
            Logo_pictureBox.Name = "Logo_pictureBox";
            Logo_pictureBox.Size = new Size(140, 126);
            Logo_pictureBox.TabIndex = 107;
            Logo_pictureBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(208, 117);
            label2.Name = "label2";
            label2.Size = new Size(230, 40);
            label2.TabIndex = 113;
            label2.Text = "공공건물 에너지";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(208, 162);
            label3.Name = "label3";
            label3.Size = new Size(269, 40);
            label3.TabIndex = 114;
            label3.Text = "성능 검토 프로그램";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(62, 282);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(415, 40);
            groupBox1.TabIndex = 130;
            groupBox1.TabStop = false;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(312, 15);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(73, 19);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "리모델링";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(171, 15);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(73, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "리트로핏";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(36, 15);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Location = new Point(483, 352);
            panel1.Name = "panel1";
            panel1.Size = new Size(415, 232);
            panel1.TabIndex = 128;
            panel1.Visible = false;
            panel1.Paint += panel1_Paint;
            // 
            // ProjectType_pictureBox
            // 
            ProjectType_pictureBox.Location = new Point(581, 97);
            ProjectType_pictureBox.Name = "ProjectType_pictureBox";
            ProjectType_pictureBox.Size = new Size(219, 179);
            ProjectType_pictureBox.TabIndex = 131;
            ProjectType_pictureBox.TabStop = false;
            // 
            // ProjectType_textBox
            // 
            ProjectType_textBox.BackColor = Color.White;
            ProjectType_textBox.BorderStyle = BorderStyle.None;
            ProjectType_textBox.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            ProjectType_textBox.ForeColor = SystemColors.ControlText;
            ProjectType_textBox.Location = new Point(581, 293);
            ProjectType_textBox.Name = "ProjectType_textBox";
            ProjectType_textBox.Size = new Size(219, 24);
            ProjectType_textBox.TabIndex = 132;
            ProjectType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Import_button);
            GeneralPanel.Controls.Add(ProjectType_textBox);
            GeneralPanel.Controls.Add(Logo_pictureBox);
            GeneralPanel.Controls.Add(ProjectType_pictureBox);
            GeneralPanel.Controls.Add(Save_button);
            GeneralPanel.Controls.Add(panel1);
            GeneralPanel.Controls.Add(panel5);
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Controls.Add(label28);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(ProjectName_textBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 133;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Import_button
            // 
            Import_button.BackColor = SystemColors.ButtonHighlight;
            Import_button.ForeColor = Color.Black;
            Import_button.Location = new Point(389, 323);
            Import_button.Name = "Import_button";
            Import_button.Size = new Size(88, 25);
            Import_button.TabIndex = 133;
            Import_button.Text = "Import";
            Import_button.UseVisualStyleBackColor = true;
            Import_button.Click += Import_button_Click;
            // 
            // Intro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Intro";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)Logo_pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ProjectType_pictureBox).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button Save_button;
        private Label label28;
        private Panel panel5;
        private TextBox ProjectName_textBox;
        private PictureBox Logo_pictureBox;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Panel panel1;
        private PictureBox ProjectType_pictureBox;
        private TextBox ProjectType_textBox;
        private Panel GeneralPanel;
        private Button Import_button;
    }
}