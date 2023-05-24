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
            label12 = new Label();
            label9 = new Label();
            Install_textBox = new TextBox();
            Install_button = new Button();
            SpacerName_textBox = new TextBox();
            Spacer_button = new Button();
            GlassName_textBox = new TextBox();
            GlassDB_button = new Button();
            FrameName_textBox = new TextBox();
            FrameDB_button = new Button();
            label35 = new Label();
            Uw_inst_textBox = new TextBox();
            label36 = new Label();
            label31 = new Label();
            Uw_textBox = new TextBox();
            label32 = new Label();
            label29 = new Label();
            Psi_g_open_textBox = new TextBox();
            label30 = new Label();
            label27 = new Label();
            Psi_g_fix_textBox = new TextBox();
            label28 = new Label();
            τD65_SNA_textBox = new TextBox();
            label26 = new Label();
            g_textBox = new TextBox();
            label23 = new Label();
            label20 = new Label();
            Ug_textBox = new TextBox();
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
            label4 = new Label();
            tabControl1 = new TabControl();
            Frame_tabPage = new TabPage();
            FrameC_Uf_textBox = new TextBox();
            FrameB_Uf_textBox = new TextBox();
            FrameA_Uf_textBox = new TextBox();
            label15 = new Label();
            textBox2 = new TextBox();
            label14 = new Label();
            pictureBox1 = new PictureBox();
            Install_tabPage = new TabPage();
            Size_tabPage = new TabPage();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            label22 = new Label();
            FrameC_df_textBox = new TextBox();
            FrameB_df_textBox = new TextBox();
            FrameA_df_textBox = new TextBox();
            GeneralPanel.SuspendLayout();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            Frame_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            panel2.Controls.Add(label12);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(Install_textBox);
            panel2.Controls.Add(Install_button);
            panel2.Controls.Add(SpacerName_textBox);
            panel2.Controls.Add(Spacer_button);
            panel2.Controls.Add(GlassName_textBox);
            panel2.Controls.Add(GlassDB_button);
            panel2.Controls.Add(FrameName_textBox);
            panel2.Controls.Add(FrameDB_button);
            panel2.Controls.Add(label35);
            panel2.Controls.Add(Uw_inst_textBox);
            panel2.Controls.Add(label36);
            panel2.Controls.Add(label31);
            panel2.Controls.Add(Uw_textBox);
            panel2.Controls.Add(label32);
            panel2.Controls.Add(label29);
            panel2.Controls.Add(Psi_g_open_textBox);
            panel2.Controls.Add(label30);
            panel2.Controls.Add(label27);
            panel2.Controls.Add(Psi_g_fix_textBox);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(τD65_SNA_textBox);
            panel2.Controls.Add(label26);
            panel2.Controls.Add(g_textBox);
            panel2.Controls.Add(label23);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(Ug_textBox);
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
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(821, 114);
            label12.Name = "label12";
            label12.Size = new Size(12, 15);
            label12.TabIndex = 97;
            label12.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlDark;
            label9.Location = new Point(821, 85);
            label9.Name = "label9";
            label9.Size = new Size(12, 15);
            label9.TabIndex = 96;
            label9.Text = "-";
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
            // Uw_inst_textBox
            // 
            Uw_inst_textBox.BackColor = SystemColors.Desktop;
            Uw_inst_textBox.BorderStyle = BorderStyle.None;
            Uw_inst_textBox.Enabled = false;
            Uw_inst_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_inst_textBox.ForeColor = SystemColors.ControlDark;
            Uw_inst_textBox.Location = new Point(675, 259);
            Uw_inst_textBox.Name = "Uw_inst_textBox";
            Uw_inst_textBox.Size = new Size(116, 15);
            Uw_inst_textBox.TabIndex = 87;
            Uw_inst_textBox.TextAlign = HorizontalAlignment.Center;
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
            // Uw_textBox
            // 
            Uw_textBox.BackColor = SystemColors.Desktop;
            Uw_textBox.BorderStyle = BorderStyle.None;
            Uw_textBox.Enabled = false;
            Uw_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_textBox.ForeColor = SystemColors.ControlDark;
            Uw_textBox.Location = new Point(675, 201);
            Uw_textBox.Name = "Uw_textBox";
            Uw_textBox.Size = new Size(116, 15);
            Uw_textBox.TabIndex = 81;
            Uw_textBox.TextAlign = HorizontalAlignment.Center;
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
            // Psi_g_open_textBox
            // 
            Psi_g_open_textBox.BackColor = SystemColors.Desktop;
            Psi_g_open_textBox.BorderStyle = BorderStyle.None;
            Psi_g_open_textBox.Enabled = false;
            Psi_g_open_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_open_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_open_textBox.Location = new Point(675, 172);
            Psi_g_open_textBox.Name = "Psi_g_open_textBox";
            Psi_g_open_textBox.Size = new Size(116, 15);
            Psi_g_open_textBox.TabIndex = 78;
            Psi_g_open_textBox.TextAlign = HorizontalAlignment.Center;
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
            // Psi_g_fix_textBox
            // 
            Psi_g_fix_textBox.BackColor = SystemColors.Desktop;
            Psi_g_fix_textBox.BorderStyle = BorderStyle.None;
            Psi_g_fix_textBox.Enabled = false;
            Psi_g_fix_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_fix_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_fix_textBox.Location = new Point(675, 143);
            Psi_g_fix_textBox.Name = "Psi_g_fix_textBox";
            Psi_g_fix_textBox.Size = new Size(116, 15);
            Psi_g_fix_textBox.TabIndex = 75;
            Psi_g_fix_textBox.TextAlign = HorizontalAlignment.Center;
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
            // τD65_SNA_textBox
            // 
            τD65_SNA_textBox.BackColor = SystemColors.Desktop;
            τD65_SNA_textBox.BorderStyle = BorderStyle.None;
            τD65_SNA_textBox.Enabled = false;
            τD65_SNA_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            τD65_SNA_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA_textBox.Location = new Point(675, 114);
            τD65_SNA_textBox.Name = "τD65_SNA_textBox";
            τD65_SNA_textBox.Size = new Size(116, 15);
            τD65_SNA_textBox.TabIndex = 72;
            τD65_SNA_textBox.TextAlign = HorizontalAlignment.Center;
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
            // g_textBox
            // 
            g_textBox.BackColor = SystemColors.Desktop;
            g_textBox.BorderStyle = BorderStyle.None;
            g_textBox.Enabled = false;
            g_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            g_textBox.ForeColor = SystemColors.ControlDark;
            g_textBox.Location = new Point(675, 85);
            g_textBox.Name = "g_textBox";
            g_textBox.Size = new Size(116, 15);
            g_textBox.TabIndex = 69;
            g_textBox.TextAlign = HorizontalAlignment.Center;
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
            // Ug_textBox
            // 
            Ug_textBox.BackColor = SystemColors.Desktop;
            Ug_textBox.BorderStyle = BorderStyle.None;
            Ug_textBox.Enabled = false;
            Ug_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_textBox.ForeColor = SystemColors.ControlDark;
            Ug_textBox.Location = new Point(675, 56);
            Ug_textBox.Name = "Ug_textBox";
            Ug_textBox.Size = new Size(116, 15);
            Ug_textBox.TabIndex = 66;
            Ug_textBox.TextAlign = HorizontalAlignment.Center;
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
            // tabControl1
            // 
            tabControl1.Controls.Add(Frame_tabPage);
            tabControl1.Controls.Add(Install_tabPage);
            tabControl1.Controls.Add(Size_tabPage);
            tabControl1.Location = new Point(12, 447);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 239);
            tabControl1.TabIndex = 19;
            // 
            // Frame_tabPage
            // 
            Frame_tabPage.BorderStyle = BorderStyle.FixedSingle;
            Frame_tabPage.Controls.Add(label22);
            Frame_tabPage.Controls.Add(FrameC_df_textBox);
            Frame_tabPage.Controls.Add(FrameB_df_textBox);
            Frame_tabPage.Controls.Add(FrameA_df_textBox);
            Frame_tabPage.Controls.Add(label19);
            Frame_tabPage.Controls.Add(label18);
            Frame_tabPage.Controls.Add(label17);
            Frame_tabPage.Controls.Add(FrameC_Uf_textBox);
            Frame_tabPage.Controls.Add(FrameB_Uf_textBox);
            Frame_tabPage.Controls.Add(FrameA_Uf_textBox);
            Frame_tabPage.Controls.Add(label15);
            Frame_tabPage.Controls.Add(textBox2);
            Frame_tabPage.Controls.Add(label14);
            Frame_tabPage.Controls.Add(pictureBox1);
            Frame_tabPage.Location = new Point(4, 24);
            Frame_tabPage.Name = "Frame_tabPage";
            Frame_tabPage.Padding = new Padding(3);
            Frame_tabPage.Size = new Size(969, 211);
            Frame_tabPage.TabIndex = 0;
            Frame_tabPage.Text = "프레임 세부정보";
            Frame_tabPage.UseVisualStyleBackColor = true;
            // 
            // FrameC_Uf_textBox
            // 
            FrameC_Uf_textBox.BackColor = SystemColors.Desktop;
            FrameC_Uf_textBox.BorderStyle = BorderStyle.None;
            FrameC_Uf_textBox.Enabled = false;
            FrameC_Uf_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameC_Uf_textBox.ForeColor = SystemColors.ControlDark;
            FrameC_Uf_textBox.Location = new Point(672, 111);
            FrameC_Uf_textBox.Name = "FrameC_Uf_textBox";
            FrameC_Uf_textBox.Size = new Size(116, 15);
            FrameC_Uf_textBox.TabIndex = 97;
            FrameC_Uf_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameB_Uf_textBox
            // 
            FrameB_Uf_textBox.BackColor = SystemColors.Desktop;
            FrameB_Uf_textBox.BorderStyle = BorderStyle.None;
            FrameB_Uf_textBox.Enabled = false;
            FrameB_Uf_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameB_Uf_textBox.ForeColor = SystemColors.ControlDark;
            FrameB_Uf_textBox.Location = new Point(533, 111);
            FrameB_Uf_textBox.Name = "FrameB_Uf_textBox";
            FrameB_Uf_textBox.Size = new Size(116, 15);
            FrameB_Uf_textBox.TabIndex = 96;
            FrameB_Uf_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameA_Uf_textBox
            // 
            FrameA_Uf_textBox.BackColor = SystemColors.Desktop;
            FrameA_Uf_textBox.BorderStyle = BorderStyle.None;
            FrameA_Uf_textBox.Enabled = false;
            FrameA_Uf_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameA_Uf_textBox.ForeColor = SystemColors.ControlDark;
            FrameA_Uf_textBox.Location = new Point(394, 111);
            FrameA_Uf_textBox.Name = "FrameA_Uf_textBox";
            FrameA_Uf_textBox.Size = new Size(116, 15);
            FrameA_Uf_textBox.TabIndex = 95;
            FrameA_Uf_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(426, 80);
            label15.Name = "label15";
            label15.Size = new Size(52, 15);
            label15.TabIndex = 94;
            label15.Text = "프레임A";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Desktop;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Enabled = false;
            textBox2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ControlDark;
            textBox2.Location = new Point(394, 42);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(116, 15);
            textBox2.TabIndex = 93;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(294, 41);
            label14.Name = "label14";
            label14.Size = new Size(31, 15);
            label14.TabIndex = 92;
            label14.Text = "재료";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(36, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 179);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Install_tabPage
            // 
            Install_tabPage.Location = new Point(4, 24);
            Install_tabPage.Name = "Install_tabPage";
            Install_tabPage.Padding = new Padding(3);
            Install_tabPage.Size = new Size(969, 211);
            Install_tabPage.TabIndex = 1;
            Install_tabPage.Text = "설치열교 정보";
            Install_tabPage.UseVisualStyleBackColor = true;
            // 
            // Size_tabPage
            // 
            Size_tabPage.Location = new Point(4, 24);
            Size_tabPage.Name = "Size_tabPage";
            Size_tabPage.Padding = new Padding(3);
            Size_tabPage.Size = new Size(969, 211);
            Size_tabPage.TabIndex = 2;
            Size_tabPage.Text = "이미지 정보";
            Size_tabPage.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.ForeColor = SystemColors.ControlDark;
            label17.Location = new Point(565, 80);
            label17.Name = "label17";
            label17.Size = new Size(51, 15);
            label17.TabIndex = 98;
            label17.Text = "프레임B";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(704, 80);
            label18.Name = "label18";
            label18.Size = new Size(52, 15);
            label18.TabIndex = 99;
            label18.Text = "프레임C";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(282, 115);
            label19.Name = "label19";
            label19.Size = new Size(55, 15);
            label19.TabIndex = 100;
            label19.Text = "열관류율";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(294, 153);
            label22.Name = "label22";
            label22.Size = new Size(31, 15);
            label22.TabIndex = 104;
            label22.Text = "두께";
            // 
            // FrameC_df_textBox
            // 
            FrameC_df_textBox.BackColor = SystemColors.Desktop;
            FrameC_df_textBox.BorderStyle = BorderStyle.None;
            FrameC_df_textBox.Enabled = false;
            FrameC_df_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameC_df_textBox.ForeColor = SystemColors.ControlDark;
            FrameC_df_textBox.Location = new Point(672, 149);
            FrameC_df_textBox.Name = "FrameC_df_textBox";
            FrameC_df_textBox.Size = new Size(116, 15);
            FrameC_df_textBox.TabIndex = 103;
            FrameC_df_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameB_df_textBox
            // 
            FrameB_df_textBox.BackColor = SystemColors.Desktop;
            FrameB_df_textBox.BorderStyle = BorderStyle.None;
            FrameB_df_textBox.Enabled = false;
            FrameB_df_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameB_df_textBox.ForeColor = SystemColors.ControlDark;
            FrameB_df_textBox.Location = new Point(533, 149);
            FrameB_df_textBox.Name = "FrameB_df_textBox";
            FrameB_df_textBox.Size = new Size(116, 15);
            FrameB_df_textBox.TabIndex = 102;
            FrameB_df_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameA_df_textBox
            // 
            FrameA_df_textBox.BackColor = SystemColors.Desktop;
            FrameA_df_textBox.BorderStyle = BorderStyle.None;
            FrameA_df_textBox.Enabled = false;
            FrameA_df_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameA_df_textBox.ForeColor = SystemColors.ControlDark;
            FrameA_df_textBox.Location = new Point(394, 149);
            FrameA_df_textBox.Name = "FrameA_df_textBox";
            FrameA_df_textBox.Size = new Size(116, 15);
            FrameA_df_textBox.TabIndex = 101;
            FrameA_df_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ConstructionWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl1);
            Controls.Add(label4);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConstructionWindow";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            Frame_tabPage.ResumeLayout(false);
            Frame_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private TextBox Type_textBox;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label4;
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
        private TextBox Uw_inst_textBox;
        private Label label36;
        private Label label31;
        private TextBox Uw_textBox;
        private Label label32;
        private Label label29;
        private TextBox Psi_g_open_textBox;
        private Label label30;
        private Label label27;
        private TextBox Psi_g_fix_textBox;
        private Label label28;
        private TextBox τD65_SNA_textBox;
        private Label label26;
        private TextBox g_textBox;
        private Label label23;
        private Label label20;
        private TextBox Ug_textBox;
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
        private TabControl tabControl1;
        private TabPage Frame_tabPage;
        private TabPage Install_tabPage;
        private TabPage Size_tabPage;
        private Label label12;
        private Label label9;
        private TextBox FrameC_Uf_textBox;
        private TextBox FrameB_Uf_textBox;
        private TextBox FrameA_Uf_textBox;
        private Label label15;
        private TextBox textBox2;
        private Label label14;
        private PictureBox pictureBox1;
        private Label label22;
        private TextBox FrameC_df_textBox;
        private TextBox FrameB_df_textBox;
        private TextBox FrameA_df_textBox;
        private Label label19;
        private Label label18;
        private Label label17;
    }
}
