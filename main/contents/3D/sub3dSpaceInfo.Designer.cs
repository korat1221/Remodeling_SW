namespace main.contents
{
    partial class sub3dSpaceInfo
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
            Name_textBox1 = new TextBox();
            Name_textBox = new TextBox();
            label14 = new Label();
            Area_textBox = new TextBox();
            label6 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // Name_textBox1
            // 
            Name_textBox1.BackColor = Color.White;
            Name_textBox1.BorderStyle = BorderStyle.None;
            Name_textBox1.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            Name_textBox1.Location = new Point(157, 38);
            Name_textBox1.Name = "Name_textBox1";
            Name_textBox1.ReadOnly = true;
            Name_textBox1.Size = new Size(100, 16);
            Name_textBox1.TabIndex = 115;
            Name_textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // Name_textBox
            // 
            Name_textBox.BackColor = Color.White;
            Name_textBox.BorderStyle = BorderStyle.None;
            Name_textBox.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            Name_textBox.Location = new Point(70, 38);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.ReadOnly = true;
            Name_textBox.Size = new Size(150, 16);
            Name_textBox.TabIndex = 114;
            Name_textBox.Text = "    ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(243, 73);
            label14.Name = "label14";
            label14.Size = new Size(23, 15);
            label14.TabIndex = 140;
            label14.Text = "m" + Program.UTIL.Subscript(2, true);
            label14.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Area_textBox
            // 
            Area_textBox.BackColor = Color.White;
            Area_textBox.BorderStyle = BorderStyle.None;
            Area_textBox.Location = new Point(157, 73);
            Area_textBox.Name = "Area_textBox";
            Area_textBox.ReadOnly = true;
            Area_textBox.Size = new Size(100, 16);
            Area_textBox.TabIndex = 139;
            Area_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(70, 74);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 138;
            label6.Text = "순바닥면적";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlDark;
            label1.Location = new Point(243, 104);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 143;
            label1.Text = "m";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(157, 104);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(100, 16);
            textBox1.TabIndex = 142;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(70, 105);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 141;
            label2.Text = "천장고";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(157, 135);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 16);
            textBox2.TabIndex = 145;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlDark;
            label3.Location = new Point(70, 136);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 144;
            label3.Text = "용도프로필";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // sub3dSpaceInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label14);
            Controls.Add(Area_textBox);
            Controls.Add(label6);
            Controls.Add(Name_textBox1);
            Controls.Add(Name_textBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dSpaceInfo";
            Text = "sub3dSpaceInfo";
            VisibleChanged += onVisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Name_textBox1;
        private TextBox Name_textBox;
        private Label label14;
        private TextBox Area_textBox;
        private Label label6;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
    }
}