namespace main
{
    partial class FormDebug
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            openFileDialog1 = new OpenFileDialog();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button6 = new Button();
            button5 = new Button();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            comboBox3 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            button7 = new Button();
            button8 = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(53, 23);
            webView21.Name = "webView21";
            webView21.Size = new Size(537, 335);
            webView21.Source = new Uri("http://localhost:3000", UriKind.Absolute);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // button1
            // 
            button1.Location = new Point(662, 30);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(23, 400);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "값 저장";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(104, 400);
            button3.Name = "button3";
            button3.Size = new Size(106, 23);
            button3.TabIndex = 3;
            button3.Text = "값 불러오기";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(23, 497);
            button4.Name = "button4";
            button4.Size = new Size(187, 23);
            button4.TabIndex = 4;
            button4.Text = "메모리DB에 데이터 로딩";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button6
            // 
            button6.Location = new Point(23, 541);
            button6.Name = "button6";
            button6.Size = new Size(187, 23);
            button6.TabIndex = 6;
            button6.Text = "계산하기";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(272, 541);
            button5.Name = "button5";
            button5.Size = new Size(187, 23);
            button5.TabIndex = 7;
            button5.Text = "로딩+계산하기";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(420, 401);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(120, 23);
            comboBox2.TabIndex = 8;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(420, 460);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(120, 23);
            comboBox1.TabIndex = 9;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(546, 460);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(120, 23);
            comboBox3.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(335, 408);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 11;
            label1.Text = "단일콤보박스";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(335, 463);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 12;
            label2.Text = "다중콤보박스";
            // 
            // button7
            // 
            button7.Location = new Point(23, 570);
            button7.Name = "button7";
            button7.Size = new Size(187, 23);
            button7.TabIndex = 13;
            button7.Text = "난방시스템 계산하기";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(491, 541);
            button8.Name = "button8";
            button8.Size = new Size(187, 23);
            button8.TabIndex = 14;
            button8.Text = "Alt계산하기";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // FormDebug
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 633);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox3);
            Controls.Add(comboBox1);
            Controls.Add(comboBox2);
            Controls.Add(button5);
            Controls.Add(button6);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(webView21);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormDebug";
            Text = "Form1";
            FormClosed += OnFormClosed;
            Shown += OnGormShown;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button6;
        private Button button5;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private ComboBox comboBox3;
        private Label label1;
        private Label label2;
        private Button button7;
        private Button button8;
    }
}