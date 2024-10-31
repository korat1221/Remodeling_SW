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
            Logo_pictureBox = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            GeneralPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)Logo_pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            GeneralPanel.SuspendLayout();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(590, 480);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 92;
            Save_button.Text = "Next";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Logo_pictureBox
            // 
            Logo_pictureBox.Location = new Point(332, 195);
            Logo_pictureBox.Name = "Logo_pictureBox";
            Logo_pictureBox.Size = new Size(195, 173);
            Logo_pictureBox.TabIndex = 107;
            Logo_pictureBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font(UTIL.Families[0], 21.75F, FontStyle.Italic);
            label2.Location = new Point(533, 275);
            label2.Name = "label2";
            label2.Size = new Size(452, 32);
            label2.TabIndex = 113;
            label2.Text = "Building energy design program ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font(UTIL.Families[0], 21.75F, FontStyle.Italic);
            label3.Location = new Point(533, 320);
            label3.Name = "label3";
            label3.Size = new Size(242, 32);
            label3.TabIndex = 114;
            label3.Text = "for fixing to zero ";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            groupBox1.Location = new Point(332, 404);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(605, 40);
            groupBox1.TabIndex = 130;
            groupBox1.TabStop = false;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(489, 15);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(49, 18);
            radioButton4.TabIndex = 3;
            radioButton4.Text = "신규";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(340, 15);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(73, 18);
            radioButton3.TabIndex = 2;
            radioButton3.Text = "리모델링";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(191, 15);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(73, 18);
            radioButton2.TabIndex = 1;
            radioButton2.Text = "리트로핏";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(66, 15);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 18);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Logo_pictureBox);
            GeneralPanel.Controls.Add(Save_button);
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1268, 684);
            GeneralPanel.TabIndex = 133;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Intro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1337, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Intro";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)Logo_pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button Save_button;
        private PictureBox Logo_pictureBox;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Panel GeneralPanel;
        private RadioButton radioButton4;
    }
}