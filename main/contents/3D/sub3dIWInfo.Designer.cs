namespace main.contents
{
    partial class sub3dIWInfo
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
            NearZone_textBox = new TextBox();
            label1 = new Label();
            Area_textBox = new TextBox();
            label6 = new Label();
            Name_textBox = new TextBox();
            SuspendLayout();
            // 
            // NearZone_textBox
            // 
            NearZone_textBox.BackColor = Color.White;
            NearZone_textBox.BorderStyle = BorderStyle.None;
            NearZone_textBox.ForeColor = Color.Black;
            NearZone_textBox.Location = new Point(91, 79);
            NearZone_textBox.Name = "NearZone_textBox";
            NearZone_textBox.ReadOnly = true;
            NearZone_textBox.Size = new Size(116, 16);
            NearZone_textBox.TabIndex = 143;
            NearZone_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlDark;
            label1.Location = new Point(36, 80);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 142;
            label1.Text = "인접존";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Area_textBox
            // 
            Area_textBox.BackColor = Color.White;
            Area_textBox.BorderStyle = BorderStyle.None;
            Area_textBox.ForeColor = Color.Black;
            Area_textBox.Location = new Point(91, 50);
            Area_textBox.Name = "Area_textBox";
            Area_textBox.ReadOnly = true;
            Area_textBox.Size = new Size(116, 16);
            Area_textBox.TabIndex = 141;
            Area_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(36, 51);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 140;
            label6.Text = "면적";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Name_textBox
            // 
            Name_textBox.BackColor = Color.White;
            Name_textBox.BorderStyle = BorderStyle.None;
            Name_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            Name_textBox.Location = new Point(36, 22);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.ReadOnly = true;
            Name_textBox.Size = new Size(171, 15);
            Name_textBox.TabIndex = 139;
            Name_textBox.Text = "    ";
            // 
            // sub3dIWInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(NearZone_textBox);
            Controls.Add(label1);
            Controls.Add(Area_textBox);
            Controls.Add(label6);
            Controls.Add(Name_textBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dIWInfo";
            Text = "sub3dIWInfo";
            VisibleChanged += onVisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Name_textBox1;
        private TextBox Name_textBox;
        private TextBox Area_textBox;
        private Label label1;
        private TextBox near_textBox;
        private Label label2;
        private Label label3;
        private TextBox NearZone_textBox;
        private Label label6;
    }
}