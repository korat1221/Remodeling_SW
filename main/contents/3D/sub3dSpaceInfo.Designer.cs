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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            textBox3 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            textBox4 = new TextBox();
            label9 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(117, 117);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 1;
            label1.Text = "주광 너비:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(201, 114);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(307, 117);
            label2.Name = "label2";
            label2.Size = new Size(18, 15);
            label2.TabIndex = 3;
            label2.Text = "m";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(59, 35);
            label3.Name = "label3";
            label3.Size = new Size(74, 20);
            label3.TabIndex = 4;
            label3.Text = "공간 정보";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(627, 122);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 7;
            label4.Text = "m";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(521, 119);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(437, 122);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 5;
            label5.Text = "주광 깊이:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(307, 212);
            label6.Name = "label6";
            label6.Size = new Size(18, 15);
            label6.TabIndex = 10;
            label6.Text = "m";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(201, 209);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(117, 212);
            label7.Name = "label7";
            label7.Size = new Size(74, 15);
            label7.TabIndex = 8;
            label7.Text = "상인방 높이:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(627, 212);
            label8.Name = "label8";
            label8.Size = new Size(22, 15);
            label8.TabIndex = 13;
            label8.Text = "m²";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(521, 209);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 12;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(437, 212);
            label9.Name = "label9";
            label9.Size = new Size(62, 15);
            label9.TabIndex = 11;
            label9.Text = "바닥 면적:";
            // 
            // sub3dSpaceInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label8);
            Controls.Add(textBox4);
            Controls.Add(label9);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dSpaceInfo";
            Text = "sub3dSpaceInfo";
            VisibleChanged += onVisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox2;
        private Label label5;
        private Label label6;
        private TextBox textBox3;
        private Label label7;
        private Label label8;
        private TextBox textBox4;
        private Label label9;
    }
}