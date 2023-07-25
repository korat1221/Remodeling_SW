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
            label8 = new Label();
            textBox4 = new TextBox();
            label9 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(280, 89);
            label8.Name = "label8";
            label8.Size = new Size(22, 15);
            label8.TabIndex = 13;
            label8.Text = "m²";
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.White;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Location = new Point(154, 88);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(120, 16);
            textBox4.TabIndex = 12;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(70, 89);
            label9.Name = "label9";
            label9.Size = new Size(62, 15);
            label9.TabIndex = 11;
            label9.Text = "바닥 면적:";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBox1.Location = new Point(70, 60);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(120, 16);
            textBox1.TabIndex = 14;
            // 
            // sub3dSpaceInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(label8);
            Controls.Add(textBox4);
            Controls.Add(label9);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dSpaceInfo";
            Text = "sub3dSpaceInfo";
            VisibleChanged += onVisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label8;
        private TextBox textBox4;
        private Label label9;
        private TextBox textBox1;
    }
}