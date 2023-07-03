namespace main.contents
{
    partial class sub3dWinInfo
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label5 = new Label();
            label6 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label67 = new Label();
            label68 = new Label();
            textBox23 = new TextBox();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabPage2 = new TabPage();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(label67);
            tabPage1.Controls.Add(label68);
            tabPage1.Controls.Add(textBox23);
            tabPage1.Controls.Add(webView21);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "음영 정보";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(447, 95);
            label5.Name = "label5";
            label5.Size = new Size(124, 15);
            label5.TabIndex = 23;
            label5.Text = "주변요소음영각도[γh]";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(683, 95);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 22;
            label6.Text = "°";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(577, 92);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 95);
            label3.Name = "label3";
            label3.Size = new Size(155, 15);
            label3.TabIndex = 20;
            label3.Text = "좌측면돌출음영각도[γv,left]";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(371, 95);
            label4.Name = "label4";
            label4.Size = new Size(12, 15);
            label4.TabIndex = 19;
            label4.Text = "°";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(265, 92);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(447, 55);
            label1.Name = "label1";
            label1.Size = new Size(124, 15);
            label1.TabIndex = 17;
            label1.Text = "상부돌출음영각도[γo]";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(683, 55);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 16;
            label2.Text = "°";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(577, 52);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 15;
            // 
            // label67
            // 
            label67.AutoSize = true;
            label67.Location = new Point(96, 55);
            label67.Name = "label67";
            label67.Size = new Size(163, 15);
            label67.TabIndex = 14;
            label67.Text = "우측면돌출음영각도[γv,right]";
            // 
            // label68
            // 
            label68.AutoSize = true;
            label68.Location = new Point(371, 55);
            label68.Name = "label68";
            label68.Size = new Size(12, 15);
            label68.TabIndex = 13;
            label68.Text = "°";
            // 
            // textBox23
            // 
            textBox23.Location = new Point(265, 52);
            textBox23.Name = "textBox23";
            textBox23.ReadOnly = true;
            textBox23.Size = new Size(100, 23);
            textBox23.TabIndex = 12;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(3, 163);
            webView21.Name = "webView21";
            webView21.Size = new Size(786, 256);
            webView21.TabIndex = 2;
            webView21.ZoomFactor = 1D;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(webView22);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "차양 정보";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Dock = DockStyle.Bottom;
            webView22.Location = new Point(3, 207);
            webView22.Name = "webView22";
            webView22.Size = new Size(786, 212);
            webView22.TabIndex = 0;
            webView22.ZoomFactor = 1D;
            // 
            // sub3dWinInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dWinInfo";
            Text = "sub3dWinInfo";
            VisibleChanged += onVisibleChanged;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private Label label5;
        private Label label6;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label67;
        private Label label68;
        private TextBox textBox23;
    }
}