using main.Properties;
using System.Windows.Forms;

namespace main.contents
{
    partial class ConstructionWindow
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
            groupBox1 = new GroupBox();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            Type_textBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label3 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            panel2 = new Panel();
            Install_textBox = new TextBox();
            Install_button = new Button();
            SpacerName_textBox = new TextBox();
            Spacer_button = new Button();
            GlassName_textBox = new TextBox();
            GlassDB_button = new Button();
            FrameName_textBox = new TextBox();
            FrameDB_button = new Button();
            label35 = new Label();
            textBox10 = new TextBox();
            label36 = new Label();
            label31 = new Label();
            textBox8 = new TextBox();
            label32 = new Label();
            label29 = new Label();
            textBox7 = new TextBox();
            label30 = new Label();
            label27 = new Label();
            textBox6 = new TextBox();
            label28 = new Label();
            textBox5 = new TextBox();
            label26 = new Label();
            textBox4 = new TextBox();
            label23 = new Label();
            label20 = new Label();
            textBox3 = new TextBox();
            label21 = new Label();
            DiIndiCal_comboBox = new ComboBox();
            Install_comboBox = new ComboBox();
            label16 = new Label();
            label13 = new Label();
            label11 = new Label();
            Frame_comboBox = new ComboBox();
            label10 = new Label();
            Uw_comboBox = new ComboBox();
            label25 = new Label();
            AdditionalPanel = new Panel();
            label4 = new Label();
            label9 = new Label();
            GeneralPanel.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Controls.Add(Type_textBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label8);
            GeneralPanel.Controls.Add(label7);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(comboBox1);
            GeneralPanel.Controls.Add(textBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(365, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(259, 95);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(146, 69);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(73, 19);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "내부덧댐";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(17, 69);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(117, 19);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "외부(커튼월)덧댐";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(146, 43);
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
            radioButton2.Location = new Point(17, 43);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(49, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "신규";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(17, 17);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(73, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존창호";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = SystemColors.Desktop;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(177, 58);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 23;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 19);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "WIN12_D";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(142, 19);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "명칭";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(673, 45);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 22;
            label8.Text = "기존창호:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(328, 71);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 21;
            label7.Text = "덧댐";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(328, 45);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 20;
            label6.Text = "신규";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(142, 58);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "TYPE";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(327, 19);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 11;
            label5.Text = "기존";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(673, 64);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(120, 23);
            comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(177, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 23);
            textBox1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(Install_textBox);
            panel2.Controls.Add(Install_button);
            panel2.Controls.Add(SpacerName_textBox);
            panel2.Controls.Add(Spacer_button);
            panel2.Controls.Add(GlassName_textBox);
            panel2.Controls.Add(GlassDB_button);
            panel2.Controls.Add(FrameName_textBox);
            panel2.Controls.Add(FrameDB_button);
            panel2.Controls.Add(label35);
            panel2.Controls.Add(textBox10);
            panel2.Controls.Add(label36);
            panel2.Controls.Add(label31);
            panel2.Controls.Add(textBox8);
            panel2.Controls.Add(label32);
            panel2.Controls.Add(label29);
            panel2.Controls.Add(textBox7);
            panel2.Controls.Add(label30);
            panel2.Controls.Add(label27);
            panel2.Controls.Add(textBox6);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(label26);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(label23);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label21);
            panel2.Controls.Add(DiIndiCal_comboBox);
            panel2.Controls.Add(Install_comboBox);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(Frame_comboBox);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(Uw_comboBox);
            panel2.Controls.Add(label25);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 307);
            panel2.TabIndex = 18;
            // 
            // Install_textBox
            // 
            Install_textBox.BackColor = SystemColors.Desktop;
            Install_textBox.BorderStyle = BorderStyle.None;
            Install_textBox.Enabled = false;
            Install_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Install_textBox.ForeColor = SystemColors.ControlDark;
            Install_textBox.Location = new Point(329, 230);
            Install_textBox.Name = "Install_textBox";
            Install_textBox.Size = new Size(116, 15);
            Install_textBox.TabIndex = 95;
            Install_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Install_button
            // 
            Install_button.BackColor = SystemColors.ControlLight;
            Install_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Install_button.FlatStyle = FlatStyle.System;
            Install_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Install_button.Location = new Point(303, 226);
            Install_button.Margin = new Padding(0);
            Install_button.Name = "Install_button";
            Install_button.Size = new Size(23, 23);
            Install_button.TabIndex = 94;
            Install_button.Text = "+";
            Install_button.UseVisualStyleBackColor = false;
            Install_button.Click += Install_button_Click;
            // 
            // SpacerName_textBox
            // 
            SpacerName_textBox.BackColor = SystemColors.Desktop;
            SpacerName_textBox.BorderStyle = BorderStyle.None;
            SpacerName_textBox.Enabled = false;
            SpacerName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SpacerName_textBox.ForeColor = SystemColors.ControlDark;
            SpacerName_textBox.Location = new Point(177, 143);
            SpacerName_textBox.Name = "SpacerName_textBox";
            SpacerName_textBox.Size = new Size(116, 15);
            SpacerName_textBox.TabIndex = 93;
            SpacerName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Spacer_button
            // 
            Spacer_button.BackColor = SystemColors.ControlLight;
            Spacer_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Spacer_button.FlatStyle = FlatStyle.System;
            Spacer_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Spacer_button.Location = new Point(303, 139);
            Spacer_button.Margin = new Padding(0);
            Spacer_button.Name = "Spacer_button";
            Spacer_button.Size = new Size(23, 23);
            Spacer_button.TabIndex = 92;
            Spacer_button.Text = "+";
            Spacer_button.UseVisualStyleBackColor = false;
            Spacer_button.Click += Spacer_button_Click;
            // 
            // GlassName_textBox
            // 
            GlassName_textBox.BackColor = SystemColors.Desktop;
            GlassName_textBox.BorderStyle = BorderStyle.None;
            GlassName_textBox.Enabled = false;
            GlassName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GlassName_textBox.ForeColor = SystemColors.ControlDark;
            GlassName_textBox.Location = new Point(177, 85);
            GlassName_textBox.Name = "GlassName_textBox";
            GlassName_textBox.Size = new Size(116, 15);
            GlassName_textBox.TabIndex = 91;
            GlassName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // GlassDB_button
            // 
            GlassDB_button.BackColor = SystemColors.ControlLight;
            GlassDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GlassDB_button.FlatStyle = FlatStyle.System;
            GlassDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            GlassDB_button.Location = new Point(303, 81);
            GlassDB_button.Margin = new Padding(0);
            GlassDB_button.Name = "GlassDB_button";
            GlassDB_button.Size = new Size(23, 23);
            GlassDB_button.TabIndex = 90;
            GlassDB_button.Text = "+";
            GlassDB_button.UseVisualStyleBackColor = false;
            GlassDB_button.Click += Glass_button_Click;
            // 
            // FrameName_textBox
            // 
            FrameName_textBox.BackColor = SystemColors.Desktop;
            FrameName_textBox.BorderStyle = BorderStyle.None;
            FrameName_textBox.Enabled = false;
            FrameName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameName_textBox.ForeColor = SystemColors.ControlDark;
            FrameName_textBox.Location = new Point(329, 56);
            FrameName_textBox.Name = "FrameName_textBox";
            FrameName_textBox.Size = new Size(116, 15);
            FrameName_textBox.TabIndex = 89;
            FrameName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameDB_button
            // 
            FrameDB_button.BackColor = SystemColors.ControlLight;
            FrameDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FrameDB_button.FlatStyle = FlatStyle.System;
            FrameDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            FrameDB_button.Location = new Point(303, 52);
            FrameDB_button.Margin = new Padding(0);
            FrameDB_button.Name = "FrameDB_button";
            FrameDB_button.Size = new Size(23, 23);
            FrameDB_button.TabIndex = 88;
            FrameDB_button.Text = "+";
            FrameDB_button.UseVisualStyleBackColor = false;
            FrameDB_button.Click += FrameDB_button_Click;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label35.ForeColor = SystemColors.ControlDark;
            label35.Location = new Point(800, 259);
            label35.Name = "label35";
            label35.Size = new Size(54, 15);
            label35.TabIndex = 85;
            label35.Text = "W/m²·K";
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.Desktop;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Enabled = false;
            textBox10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox10.ForeColor = SystemColors.ControlDark;
            textBox10.Location = new Point(675, 259);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(116, 15);
            textBox10.TabIndex = 87;
            textBox10.TextAlign = HorizontalAlignment.Right;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label36.ForeColor = SystemColors.ControlDark;
            label36.Location = new Point(484, 259);
            label36.Name = "label36";
            label36.Size = new Size(143, 15);
            label36.TabIndex = 86;
            label36.Text = "[Uw,inst.] 유효열관류율";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label31.ForeColor = SystemColors.ControlDark;
            label31.Location = new Point(800, 201);
            label31.Name = "label31";
            label31.Size = new Size(54, 15);
            label31.TabIndex = 79;
            label31.Text = "W/m²·K";
            // 
            // textBox8
            // 
            textBox8.BackColor = SystemColors.Desktop;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Enabled = false;
            textBox8.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox8.ForeColor = SystemColors.ControlDark;
            textBox8.Location = new Point(675, 201);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(116, 15);
            textBox8.TabIndex = 81;
            textBox8.TextAlign = HorizontalAlignment.Right;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label32.ForeColor = SystemColors.ControlDark;
            label32.Location = new Point(484, 201);
            label32.Name = "label32";
            label32.Size = new Size(171, 15);
            label32.TabIndex = 80;
            label32.Text = "[Uw] 창호열관류율(덧댐포함)";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label29.ForeColor = SystemColors.ControlDark;
            label29.Location = new Point(800, 172);
            label29.Name = "label29";
            label29.Size = new Size(49, 15);
            label29.TabIndex = 76;
            label29.Text = "W/m·K";
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.Desktop;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Enabled = false;
            textBox7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox7.ForeColor = SystemColors.ControlDark;
            textBox7.Location = new Point(675, 172);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(116, 15);
            textBox7.TabIndex = 78;
            textBox7.TextAlign = HorizontalAlignment.Right;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label30.ForeColor = SystemColors.ControlDark;
            label30.Location = new Point(484, 172);
            label30.Name = "label30";
            label30.Size = new Size(157, 15);
            label30.TabIndex = 77;
            label30.Text = "[Ψg] 선형열관류율(개폐창)";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.ForeColor = SystemColors.ControlDark;
            label27.Location = new Point(800, 143);
            label27.Name = "label27";
            label27.Size = new Size(49, 15);
            label27.TabIndex = 73;
            label27.Text = "W/m·K";
            // 
            // textBox6
            // 
            textBox6.BackColor = SystemColors.Desktop;
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Enabled = false;
            textBox6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox6.ForeColor = SystemColors.ControlDark;
            textBox6.Location = new Point(675, 143);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(116, 15);
            textBox6.TabIndex = 75;
            textBox6.TextAlign = HorizontalAlignment.Right;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(484, 143);
            label28.Name = "label28";
            label28.Size = new Size(157, 15);
            label28.TabIndex = 74;
            label28.Text = "[Ψg] 선형열관류율(고정창)";
            // 
            // textBox5
            // 
            textBox5.BackColor = SystemColors.Desktop;
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Enabled = false;
            textBox5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox5.ForeColor = SystemColors.ControlDark;
            textBox5.Location = new Point(675, 114);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(116, 15);
            textBox5.TabIndex = 72;
            textBox5.TextAlign = HorizontalAlignment.Right;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(484, 114);
            label26.Name = "label26";
            label26.Size = new Size(187, 15);
            label26.TabIndex = 71;
            label26.Text = "[τD65,SNA] 빛투과율(덧댐포함)";
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.Desktop;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Enabled = false;
            textBox4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.ForeColor = SystemColors.ControlDark;
            textBox4.Location = new Point(675, 85);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(116, 15);
            textBox4.TabIndex = 69;
            textBox4.TextAlign = HorizontalAlignment.Right;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.ForeColor = SystemColors.ControlDark;
            label23.Location = new Point(484, 85);
            label23.Name = "label23";
            label23.Size = new Size(163, 15);
            label23.TabIndex = 68;
            label23.Text = "[g] 태양열 취득율(덧댐포함)";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label20.ForeColor = SystemColors.ControlDark;
            label20.Location = new Point(800, 56);
            label20.Name = "label20";
            label20.Size = new Size(54, 15);
            label20.TabIndex = 64;
            label20.Text = "W/m²·K";
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.Desktop;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Enabled = false;
            textBox3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.ControlDark;
            textBox3.Location = new Point(675, 56);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(116, 15);
            textBox3.TabIndex = 66;
            textBox3.TextAlign = HorizontalAlignment.Right;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label21.ForeColor = SystemColors.ControlDark;
            label21.Location = new Point(484, 56);
            label21.Name = "label21";
            label21.Size = new Size(172, 15);
            label21.TabIndex = 65;
            label21.Text = "[Ug] 유리 열관류율(덧댐제외)";
            // 
            // DiIndiCal_comboBox
            // 
            DiIndiCal_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DiIndiCal_comboBox.FormattingEnabled = true;
            DiIndiCal_comboBox.Location = new Point(327, 23);
            DiIndiCal_comboBox.Name = "DiIndiCal_comboBox";
            DiIndiCal_comboBox.Size = new Size(120, 23);
            DiIndiCal_comboBox.TabIndex = 55;
            // 
            // Install_comboBox
            // 
            Install_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Install_comboBox.FormattingEnabled = true;
            Install_comboBox.Location = new Point(175, 226);
            Install_comboBox.Name = "Install_comboBox";
            Install_comboBox.Size = new Size(120, 23);
            Install_comboBox.TabIndex = 52;
            Install_comboBox.SelectedIndexChanged += Install_comboBox_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(78, 230);
            label16.Name = "label16";
            label16.Size = new Size(31, 15);
            label16.TabIndex = 51;
            label16.Text = "설치";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(78, 143);
            label13.Name = "label13";
            label13.Size = new Size(31, 15);
            label13.TabIndex = 45;
            label13.Text = "간봉";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(78, 85);
            label11.Name = "label11";
            label11.Size = new Size(31, 15);
            label11.TabIndex = 41;
            label11.Text = "유리";
            // 
            // Frame_comboBox
            // 
            Frame_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Frame_comboBox.FormattingEnabled = true;
            Frame_comboBox.Location = new Point(175, 52);
            Frame_comboBox.Name = "Frame_comboBox";
            Frame_comboBox.Size = new Size(120, 23);
            Frame_comboBox.TabIndex = 40;
            Frame_comboBox.SelectedIndexChanged += Frame_comboBox_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(78, 56);
            label10.Name = "label10";
            label10.Size = new Size(43, 15);
            label10.TabIndex = 39;
            label10.Text = "프레임";
            // 
            // Uw_comboBox
            // 
            Uw_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_comboBox.FormattingEnabled = true;
            Uw_comboBox.Location = new Point(175, 23);
            Uw_comboBox.Name = "Uw_comboBox";
            Uw_comboBox.Size = new Size(120, 23);
            Uw_comboBox.TabIndex = 38;
            Uw_comboBox.SelectedIndexChanged += Uw_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(78, 27);
            label25.Name = "label25";
            label25.Size = new Size(79, 15);
            label25.TabIndex = 37;
            label25.Text = "Uw 적용방법";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Location = new Point(12, 464);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 229);
            AdditionalPanel.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 118);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 0;
            label4.Text = "창호 구성요소";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(12, 446);
            label9.Name = "label9";
            label9.Size = new Size(99, 15);
            label9.TabIndex = 19;
            label9.Text = "프레임 세부 정보";
            // 
            // ConstructionWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label9);
            Controls.Add(label4);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConstructionWindow";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private ComboBox comboBox1;
        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private TextBox Type_textBox;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label4;
        private Label label9;
        private ComboBox DiIndiCal_comboBox;
        private ComboBox comboBox9;
        private ComboBox Install_comboBox;
        private Label label16;
        private Label label13;
        private Label label11;
        private Label label10;
        private ComboBox Uw_comboBox;
        private Label label25;
        private Label label35;
        private TextBox textBox10;
        private Label label36;
        private Label label31;
        private TextBox textBox8;
        private Label label32;
        private Label label29;
        private TextBox textBox7;
        private Label label30;
        private Label label27;
        private TextBox textBox6;
        private Label label28;
        private TextBox textBox5;
        private Label label26;
        private TextBox textBox4;
        private Label label23;
        private Label label20;
        private TextBox textBox3;
        private Label label21;
        private Button FrameDB_button;
        private TextBox FrameName_textBox;
        private ComboBox Frame_comboBox;
        private TextBox GlassName_textBox;
        private Button GlassDB_button;
        private TextBox SpacerName_textBox;
        private Button Spacer_button;
        private TextBox Install_textBox;
        private Button Install_button;
    }
}
