
namespace main.contents
{
    partial class ConstructionBlind
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
            label2 = new Label();
            label4 = new Label();
            OldBlind_textBox = new TextBox();
            OldBlind_comboBox = new CustomComboBox();
            label3 = new Label();
            Name_textBox = new TextBox();
            Type_textBox = new TextBox();
            label9 = new Label();
            groupBox1 = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            Icon_pictureBox = new PictureBox();
            Num_textBox = new TextBox();
            panel2 = new Panel();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox4 = new PictureBox();
            BlindAlpha_textBox = new TextBox();
            label18 = new Label();
            BlindIn_textBox = new TextBox();
            label19 = new Label();
            BlindSHGC_textBox = new TextBox();
            label13 = new Label();
            BlindEx_textBox = new TextBox();
            label14 = new Label();
            ControlType2_textBox = new TextBox();
            pictureBox3 = new PictureBox();
            BlindColor_textBox = new TextBox();
            label8 = new Label();
            BlindTrans_textBox = new TextBox();
            label7 = new Label();
            BlindInstall_textBox = new TextBox();
            label6 = new Label();
            BlindType_textBox = new TextBox();
            label5 = new Label();
            ControlType_comboBox = new CustomComboBox();
            label25 = new Label();
            BlindName_textBox = new TextBox();
            label11 = new Label();
            BlindDB_button = new Button();
            Previous_button = new Button();
            Save_button = new Button();
            GeneralPanel.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = SystemColors.GradientActiveCaption;
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(OldBlind_textBox);
            GeneralPanel.Controls.Add(OldBlind_comboBox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Controls.Add(Type_textBox);
            GeneralPanel.Controls.Add(label9);
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Location = new Point(0, 4);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1000, 80);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(341, 50);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 106;
            label2.Text = "신규";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Black;
            label4.Location = new Point(341, 18);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 105;
            label4.Text = "기존";
            // 
            // OldBlind_textBox
            // 
            OldBlind_textBox.BackColor = SystemColors.GradientActiveCaption;
            OldBlind_textBox.BorderStyle = BorderStyle.None;
            OldBlind_textBox.Enabled = false;
            OldBlind_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            OldBlind_textBox.ForeColor = Color.Black;
            OldBlind_textBox.Location = new Point(699, 28);
            OldBlind_textBox.Name = "OldBlind_textBox";
            OldBlind_textBox.Size = new Size(67, 15);
            OldBlind_textBox.TabIndex = 104;
            OldBlind_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // OldBlind_comboBox
            // 
            OldBlind_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            OldBlind_comboBox.ForeColor = Color.Black;
            OldBlind_comboBox.FormattingEnabled = true;
            OldBlind_comboBox.Location = new Point(699, 45);
            OldBlind_comboBox.Name = "OldBlind_comboBox";
            OldBlind_comboBox.Size = new Size(120, 24);
            OldBlind_comboBox.TabIndex = 103;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Black;
            label3.Location = new Point(142, 12);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 100;
            label3.Text = "명칭";
            // 
            // Name_textBox
            // 
            Name_textBox.ForeColor = Color.Black;
            Name_textBox.Location = new Point(177, 9);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 101;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = SystemColors.GradientActiveCaption;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Type_textBox.ForeColor = Color.Black;
            Type_textBox.Location = new Point(177, 50);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 99;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Black;
            label9.Location = new Point(141, 50);
            label9.Name = "label9";
            label9.Size = new Size(33, 15);
            label9.TabIndex = 98;
            label9.Text = "TYPE";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(378, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(283, 75);
            groupBox1.TabIndex = 97;
            groupBox1.TabStop = false;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.ForeColor = Color.Black;
            radioButton3.Location = new Point(146, 48);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(93, 19);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "철거 후 신규";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.ForeColor = Color.Black;
            radioButton2.Location = new Point(17, 48);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(77, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "신규 차양";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.ForeColor = Color.Black;
            radioButton1.Location = new Point(17, 16);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(77, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존 차양";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(18, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 92;
            Icon_pictureBox.TabStop = false;
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = SystemColors.GradientActiveCaption;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Num_textBox.ForeColor = Color.Black;
            Num_textBox.Location = new Point(72, 32);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(67, 15);
            Num_textBox.TabIndex = 102;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(webView21);
            panel2.Controls.Add(pictureBox4);
            panel2.Controls.Add(BlindAlpha_textBox);
            panel2.Controls.Add(label18);
            panel2.Controls.Add(BlindIn_textBox);
            panel2.Controls.Add(label19);
            panel2.Controls.Add(BlindSHGC_textBox);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(BlindEx_textBox);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(ControlType2_textBox);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(BlindColor_textBox);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(BlindTrans_textBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(BlindInstall_textBox);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(BlindType_textBox);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(ControlType_comboBox);
            panel2.Controls.Add(label25);
            panel2.Controls.Add(BlindName_textBox);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(BlindDB_button);
            panel2.Location = new Point(0, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 589);
            panel2.TabIndex = 18;
            panel2.Paint += panel2_Paint;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.BackColor = Color.White;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(0, 336);
            webView21.Name = "webView21";
            webView21.Size = new Size(1000, 253);
            webView21.TabIndex = 126;
            webView21.ZoomFactor = 1D;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(381, 171);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(77, 150);
            pictureBox4.TabIndex = 106;
            pictureBox4.TabStop = false;
            // 
            // BlindAlpha_textBox
            // 
            BlindAlpha_textBox.BackColor = Color.White;
            BlindAlpha_textBox.BorderStyle = BorderStyle.None;
            BlindAlpha_textBox.Enabled = false;
            BlindAlpha_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindAlpha_textBox.ForeColor = SystemColors.ControlDark;
            BlindAlpha_textBox.Location = new Point(658, 163);
            BlindAlpha_textBox.Name = "BlindAlpha_textBox";
            BlindAlpha_textBox.Size = new Size(120, 15);
            BlindAlpha_textBox.TabIndex = 125;
            BlindAlpha_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font(UTIL.Families[0], 9.75F);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(561, 163);
            label18.Name = "label18";
            label18.Size = new Size(43, 15);
            label18.TabIndex = 124;
            label18.Text = "흡수율";
            // 
            // BlindIn_textBox
            // 
            BlindIn_textBox.BackColor = Color.White;
            BlindIn_textBox.BorderStyle = BorderStyle.None;
            BlindIn_textBox.Enabled = false;
            BlindIn_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindIn_textBox.ForeColor = SystemColors.ControlDark;
            BlindIn_textBox.Location = new Point(658, 127);
            BlindIn_textBox.Name = "BlindIn_textBox";
            BlindIn_textBox.Size = new Size(120, 15);
            BlindIn_textBox.TabIndex = 123;
            BlindIn_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font(UTIL.Families[0], 9.75F);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(561, 127);
            label19.Name = "label19";
            label19.Size = new Size(67, 15);
            label19.TabIndex = 122;
            label19.Text = "내부반사율";
            // 
            // BlindSHGC_textBox
            // 
            BlindSHGC_textBox.BackColor = Color.White;
            BlindSHGC_textBox.BorderStyle = BorderStyle.None;
            BlindSHGC_textBox.Enabled = false;
            BlindSHGC_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindSHGC_textBox.ForeColor = SystemColors.ControlDark;
            BlindSHGC_textBox.Location = new Point(658, 200);
            BlindSHGC_textBox.Name = "BlindSHGC_textBox";
            BlindSHGC_textBox.Size = new Size(120, 15);
            BlindSHGC_textBox.TabIndex = 110;
            BlindSHGC_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font(UTIL.Families[0], 9.75F);
            label13.ForeColor = SystemColors.ControlDark;
            label13.Location = new Point(561, 199);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 109;
            label13.Text = "투과율";
            // 
            // BlindEx_textBox
            // 
            BlindEx_textBox.BackColor = Color.White;
            BlindEx_textBox.BorderStyle = BorderStyle.None;
            BlindEx_textBox.Enabled = false;
            BlindEx_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindEx_textBox.ForeColor = SystemColors.ControlDark;
            BlindEx_textBox.Location = new Point(658, 91);
            BlindEx_textBox.Name = "BlindEx_textBox";
            BlindEx_textBox.Size = new Size(120, 15);
            BlindEx_textBox.TabIndex = 108;
            BlindEx_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font(UTIL.Families[0], 9.75F);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(561, 90);
            label14.Name = "label14";
            label14.Size = new Size(67, 15);
            label14.TabIndex = 107;
            label14.Text = "외부반사율";
            // 
            // ControlType2_textBox
            // 
            ControlType2_textBox.BackColor = Color.White;
            ControlType2_textBox.BorderStyle = BorderStyle.None;
            ControlType2_textBox.Enabled = false;
            ControlType2_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            ControlType2_textBox.ForeColor = SystemColors.ControlDark;
            ControlType2_textBox.Location = new Point(250, 239);
            ControlType2_textBox.Name = "ControlType2_textBox";
            ControlType2_textBox.Size = new Size(120, 15);
            ControlType2_textBox.TabIndex = 106;
            ControlType2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(303, 17);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(155, 148);
            pictureBox3.TabIndex = 105;
            pictureBox3.TabStop = false;
            // 
            // BlindColor_textBox
            // 
            BlindColor_textBox.BackColor = Color.White;
            BlindColor_textBox.BorderStyle = BorderStyle.None;
            BlindColor_textBox.Enabled = false;
            BlindColor_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindColor_textBox.ForeColor = SystemColors.ControlDark;
            BlindColor_textBox.Location = new Point(124, 200);
            BlindColor_textBox.Name = "BlindColor_textBox";
            BlindColor_textBox.Size = new Size(120, 15);
            BlindColor_textBox.TabIndex = 104;
            BlindColor_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font(UTIL.Families[0], 9.75F);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(27, 199);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 103;
            label8.Text = "색깔";
            // 
            // BlindTrans_textBox
            // 
            BlindTrans_textBox.BackColor = Color.White;
            BlindTrans_textBox.BorderStyle = BorderStyle.None;
            BlindTrans_textBox.Enabled = false;
            BlindTrans_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindTrans_textBox.ForeColor = SystemColors.ControlDark;
            BlindTrans_textBox.Location = new Point(124, 164);
            BlindTrans_textBox.Name = "BlindTrans_textBox";
            BlindTrans_textBox.Size = new Size(120, 15);
            BlindTrans_textBox.TabIndex = 102;
            BlindTrans_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font(UTIL.Families[0], 9.75F);
            label7.ForeColor = SystemColors.ControlDark;
            label7.Location = new Point(27, 163);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 101;
            label7.Text = "투과수준";
            // 
            // BlindInstall_textBox
            // 
            BlindInstall_textBox.BackColor = Color.White;
            BlindInstall_textBox.BorderStyle = BorderStyle.None;
            BlindInstall_textBox.Enabled = false;
            BlindInstall_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindInstall_textBox.ForeColor = SystemColors.ControlDark;
            BlindInstall_textBox.Location = new Point(124, 128);
            BlindInstall_textBox.Name = "BlindInstall_textBox";
            BlindInstall_textBox.Size = new Size(120, 15);
            BlindInstall_textBox.TabIndex = 100;
            BlindInstall_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font(UTIL.Families[0], 9.75F);
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(27, 127);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 99;
            label6.Text = "설치";
            // 
            // BlindType_textBox
            // 
            BlindType_textBox.BackColor = Color.White;
            BlindType_textBox.BorderStyle = BorderStyle.None;
            BlindType_textBox.Enabled = false;
            BlindType_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindType_textBox.ForeColor = SystemColors.ControlDark;
            BlindType_textBox.Location = new Point(124, 92);
            BlindType_textBox.Name = "BlindType_textBox";
            BlindType_textBox.Size = new Size(120, 15);
            BlindType_textBox.TabIndex = 98;
            BlindType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font(UTIL.Families[0], 9.75F);
            label5.ForeColor = SystemColors.ControlDark;
            label5.Location = new Point(27, 91);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 97;
            label5.Text = "종류";
            // 
            // ControlType_comboBox
            // 
            ControlType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            ControlType_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            ControlType_comboBox.FormattingEnabled = true;
            ControlType_comboBox.Location = new Point(124, 236);
            ControlType_comboBox.Name = "ControlType_comboBox";
            ControlType_comboBox.Size = new Size(120, 23);
            ControlType_comboBox.TabIndex = 96;
            ControlType_comboBox.SelectedIndexChanged += ControlType_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font(UTIL.Families[0], 9.75F);
            label25.Location = new Point(27, 239);
            label25.Name = "label25";
            label25.Size = new Size(55, 15);
            label25.TabIndex = 95;
            label25.Text = "제어방법";
            // 
            // BlindName_textBox
            // 
            BlindName_textBox.BackColor = Color.White;
            BlindName_textBox.BorderStyle = BorderStyle.None;
            BlindName_textBox.Enabled = false;
            BlindName_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            BlindName_textBox.ForeColor = SystemColors.ControlDark;
            BlindName_textBox.Location = new Point(124, 56);
            BlindName_textBox.Name = "BlindName_textBox";
            BlindName_textBox.Size = new Size(120, 15);
            BlindName_textBox.TabIndex = 94;
            BlindName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font(UTIL.Families[0], 9.75F);
            label11.Location = new Point(27, 55);
            label11.Name = "label11";
            label11.Size = new Size(31, 15);
            label11.TabIndex = 92;
            label11.Text = "제품";
            // 
            // BlindDB_button
            // 
            BlindDB_button.BackColor = SystemColors.ControlLight;
            BlindDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            BlindDB_button.FlatStyle = FlatStyle.System;
            BlindDB_button.Font = new Font(UTIL.Families[0], 9.75F);
            BlindDB_button.Location = new Point(245, 52);
            BlindDB_button.Margin = new Padding(0);
            BlindDB_button.Name = "BlindDB_button";
            BlindDB_button.Size = new Size(23, 23);
            BlindDB_button.TabIndex = 93;
            BlindDB_button.Text = "+";
            BlindDB_button.UseVisualStyleBackColor = false;
            BlindDB_button.Click += BlindDB_button_Click;
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(1006, 648);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 95;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1100, 648);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 94;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // ConstructionBlind
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Previous_button);
            Controls.Add(Save_button);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConstructionBlind";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private PictureBox Icon_pictureBox;
        private TextBox BlindName_textBox;
        private Label label11;
        private Button BlindDB_button;
        private TextBox textBox9;
        private Label label12;
        private TextBox textBox10;
        private Label label13;
        private TextBox textBox11;
        private Label label14;
        private TextBox ControlType2_textBox;
        private PictureBox pictureBox3;
        private TextBox BlindColor_textBox;
        private Label label8;
        private TextBox BlindTrans_textBox;
        private Label label7;
        private TextBox BlindInstall_textBox;
        private Label label6;
        private TextBox BlindType_textBox;
        private Label label5;
        private CustomComboBox ControlType_comboBox;
        private Label label25;
        private TextBox textBox13;
        private Label label18;
        private TextBox BlindEx_textBox;
        private TextBox BlindSHGC_textBox;
        private TextBox BlindAlpha_textBox;
        private TextBox BlindIn_textBox;
        private Label label19;
        private PictureBox pictureBox4;
        private Button Previous_button;
        private Button Save_button;
        private TextBox Type_textBox;
        private Label label9;
        private GroupBox groupBox1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label3;
        private TextBox Name_textBox;
        private TextBox Num_textBox;
        private TextBox OldBlind_textBox;
        private CustomComboBox OldBlind_comboBox;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Label label2;
        private Label label4;
    }
}