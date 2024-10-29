namespace main.contents
{
    partial class PV
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            PVModule_textBox = new TextBox();
            PVMainPanel = new Panel();
            panel4 = new Panel();
            OldPVSystem_ComboBox = new CustomComboBox();
            radioButton4 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton1 = new RadioButton();
            label8 = new Label();
            label9 = new Label();
            label11 = new Label();
            label3 = new Label();
            label1 = new Label();
            Num_textBox = new TextBox();
            Name_textBox = new TextBox();
            pictureBox1 = new PictureBox();
            계통유형 = new Label();
            label4 = new Label();
            Batterycapacity_s = new Label();
            Batterycapacity_textBox = new TextBox();
            InverterEfficiency_textBox = new TextBox();
            Batterycapacity_n = new Label();
            label10 = new Label();
            Battery_textBox = new TextBox();
            Inverter_textBox = new TextBox();
            Battery_label = new Label();
            label7 = new Label();
            label6 = new Label();
            Previous_button = new Button();
            Save_button = new Button();
            label12 = new Label();
            averagecpacity_textBox = new TextBox();
            label29 = new Label();
            label28 = new Label();
            allcapacity_textBox = new TextBox();
            label27 = new Label();
            panel2 = new Panel();
            PVMoudle_textBox = new TextBox();
            PVType_ComboBox = new CustomComboBox();
            BatteryDB_button = new Button();
            InverterDB_button = new Button();
            PVModuleDB_button = new Button();
            PV_dataGridView = new DataGridView();
            tabControl1 = new CustomTabControl();
            tabPage1 = new TabPage();
            DoorH2_textBox = new TextBox();
            label18 = new Label();
            tabPage2 = new TabPage();
            label24 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            PVMainPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // PVModule_textBox
            // 
            PVModule_textBox.BackColor = Color.White;
            PVModule_textBox.BorderStyle = BorderStyle.None;
            PVModule_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PVModule_textBox.ForeColor = SystemColors.WindowFrame;
            PVModule_textBox.Location = new Point(187, 13);
            PVModule_textBox.Name = "PVModule_textBox";
            PVModule_textBox.ReadOnly = true;
            PVModule_textBox.Size = new Size(116, 18);
            PVModule_textBox.TabIndex = 94;
            PVModule_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PVMainPanel
            // 
            PVMainPanel.BackColor = Color.FromArgb(32, 77, 112);
            PVMainPanel.Controls.Add(panel4);
            PVMainPanel.Controls.Add(label3);
            PVMainPanel.Controls.Add(label1);
            PVMainPanel.Controls.Add(Num_textBox);
            PVMainPanel.Controls.Add(Name_textBox);
            PVMainPanel.Controls.Add(pictureBox1);
            PVMainPanel.Location = new Point(0, 0);
            PVMainPanel.Name = "PVMainPanel";
            PVMainPanel.Size = new Size(1000, 80);
            PVMainPanel.TabIndex = 17;
            // 
            // panel4
            // 
            panel4.Controls.Add(OldPVSystem_ComboBox);
            panel4.Controls.Add(radioButton4);
            panel4.Controls.Add(radioButton2);
            panel4.Controls.Add(radioButton3);
            panel4.Controls.Add(radioButton1);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label11);
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(472, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(528, 80);
            panel4.TabIndex = 135;
            // 
            // OldPVSystem_ComboBox
            // 
            OldPVSystem_ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            OldPVSystem_ComboBox.FormattingEnabled = true;
            OldPVSystem_ComboBox.Location = new Point(365, 47);
            OldPVSystem_ComboBox.Name = "OldPVSystem_ComboBox";
            OldPVSystem_ComboBox.Size = new Size(120, 24);
            OldPVSystem_ComboBox.TabIndex = 144;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton4.ForeColor = Color.White;
            radioButton4.Location = new Point(137, 50);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(107, 23);
            radioButton4.TabIndex = 2;
            radioButton4.TabStop = true;
            radioButton4.Text = "철거 후 신규";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton2.ForeColor = Color.White;
            radioButton2.Location = new Point(70, 30);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(55, 23);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "보수";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton3.ForeColor = Color.White;
            radioButton3.Location = new Point(70, 50);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(55, 23);
            radioButton3.TabIndex = 1;
            radioButton3.TabStop = true;
            radioButton3.Text = "신규";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton1.ForeColor = Color.White;
            radioButton1.Location = new Point(70, 9);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(55, 23);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(13, 11);
            label8.Name = "label8";
            label8.Size = new Size(42, 19);
            label8.TabIndex = 130;
            label8.Text = "기 존";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(13, 52);
            label9.Name = "label9";
            label9.Size = new Size(42, 19);
            label9.TabIndex = 129;
            label9.Text = "신 규";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            label11.ForeColor = Color.White;
            label11.Location = new Point(13, 32);
            label11.Name = "label11";
            label11.Size = new Size(42, 19);
            label11.TabIndex = 128;
            label11.Text = "보 수";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.IBeam;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.White;
            label3.Location = new Point(114, 18);
            label3.Name = "label3";
            label3.Size = new Size(47, 19);
            label3.TabIndex = 134;
            label3.Text = "번  호";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.White;
            label1.Location = new Point(114, 43);
            label1.Name = "label1";
            label1.Size = new Size(47, 19);
            label1.TabIndex = 133;
            label1.Text = "명  칭";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.FromArgb(32, 77, 112);
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Num_textBox.ForeColor = Color.White;
            Num_textBox.Location = new Point(164, 17);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(139, 18);
            Num_textBox.TabIndex = 132;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Name_textBox
            // 
            Name_textBox.BackColor = Color.White;
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Name_textBox.Location = new Point(164, 40);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(139, 25);
            Name_textBox.TabIndex = 131;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(18, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 90;
            pictureBox1.TabStop = false;
            // 
            // 계통유형
            // 
            계통유형.AutoSize = true;
            계통유형.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            계통유형.Location = new Point(88, 49);
            계통유형.Name = "계통유형";
            계통유형.Size = new Size(75, 19);
            계통유형.TabIndex = 91;
            계통유형.Text = "계통  유형";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 10F);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(598, 83);
            label4.Name = "label4";
            label4.Size = new Size(20, 17);
            label4.TabIndex = 129;
            label4.Text = "%";
            // 
            // Batterycapacity_s
            // 
            Batterycapacity_s.AutoSize = true;
            Batterycapacity_s.Font = new Font("나눔고딕", 10F);
            Batterycapacity_s.ForeColor = SystemColors.ActiveCaptionText;
            Batterycapacity_s.Location = new Point(598, 113);
            Batterycapacity_s.Name = "Batterycapacity_s";
            Batterycapacity_s.Size = new Size(28, 17);
            Batterycapacity_s.TabIndex = 106;
            Batterycapacity_s.Text = "kW";
            Batterycapacity_s.Visible = false;
            // 
            // Batterycapacity_textBox
            // 
            Batterycapacity_textBox.BackColor = Color.White;
            Batterycapacity_textBox.BorderStyle = BorderStyle.None;
            Batterycapacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Batterycapacity_textBox.ForeColor = SystemColors.WindowFrame;
            Batterycapacity_textBox.Location = new Point(530, 112);
            Batterycapacity_textBox.Name = "Batterycapacity_textBox";
            Batterycapacity_textBox.ReadOnly = true;
            Batterycapacity_textBox.Size = new Size(64, 18);
            Batterycapacity_textBox.TabIndex = 105;
            Batterycapacity_textBox.TextAlign = HorizontalAlignment.Center;
            Batterycapacity_textBox.Visible = false;
            // 
            // InverterEfficiency_textBox
            // 
            InverterEfficiency_textBox.BackColor = Color.White;
            InverterEfficiency_textBox.BorderStyle = BorderStyle.None;
            InverterEfficiency_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InverterEfficiency_textBox.ForeColor = SystemColors.WindowFrame;
            InverterEfficiency_textBox.Location = new Point(530, 82);
            InverterEfficiency_textBox.Name = "InverterEfficiency_textBox";
            InverterEfficiency_textBox.ReadOnly = true;
            InverterEfficiency_textBox.Size = new Size(64, 18);
            InverterEfficiency_textBox.TabIndex = 104;
            InverterEfficiency_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Batterycapacity_n
            // 
            Batterycapacity_n.AutoSize = true;
            Batterycapacity_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Batterycapacity_n.ForeColor = SystemColors.ActiveCaptionText;
            Batterycapacity_n.Location = new Point(441, 112);
            Batterycapacity_n.Name = "Batterycapacity_n";
            Batterycapacity_n.Size = new Size(84, 19);
            Batterycapacity_n.TabIndex = 103;
            Batterycapacity_n.Text = "배터리 용량";
            Batterycapacity_n.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Location = new Point(441, 82);
            label10.Name = "label10";
            label10.Size = new Size(84, 19);
            label10.TabIndex = 102;
            label10.Text = "인버터 효율";
            // 
            // Battery_textBox
            // 
            Battery_textBox.BackColor = Color.White;
            Battery_textBox.BorderStyle = BorderStyle.None;
            Battery_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Battery_textBox.ForeColor = SystemColors.WindowFrame;
            Battery_textBox.Location = new Point(187, 112);
            Battery_textBox.Name = "Battery_textBox";
            Battery_textBox.ReadOnly = true;
            Battery_textBox.Size = new Size(116, 18);
            Battery_textBox.TabIndex = 99;
            Battery_textBox.TextAlign = HorizontalAlignment.Center;
            Battery_textBox.Visible = false;
            // 
            // Inverter_textBox
            // 
            Inverter_textBox.BackColor = Color.White;
            Inverter_textBox.BorderStyle = BorderStyle.None;
            Inverter_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Inverter_textBox.ForeColor = SystemColors.WindowFrame;
            Inverter_textBox.Location = new Point(187, 82);
            Inverter_textBox.Name = "Inverter_textBox";
            Inverter_textBox.ReadOnly = true;
            Inverter_textBox.Size = new Size(116, 18);
            Inverter_textBox.TabIndex = 98;
            Inverter_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Battery_label
            // 
            Battery_label.AutoSize = true;
            Battery_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Battery_label.Location = new Point(101, 112);
            Battery_label.Name = "Battery_label";
            Battery_label.Size = new Size(61, 19);
            Battery_label.TabIndex = 97;
            Battery_label.Text = "배 터 리";
            Battery_label.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(101, 82);
            label7.Name = "label7";
            label7.Size = new Size(61, 19);
            label7.TabIndex = 96;
            label7.Text = "인 버 터";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(80, 16);
            label6.Name = "label6";
            label6.Size = new Size(84, 19);
            label6.TabIndex = 95;
            label6.Text = "태양광 모듈";
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(995, 818);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 99;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            Previous_button.Click += Previous_button_Click;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1089, 818);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 98;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
           
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(1148, 59);
            label12.Name = "label12";
            label12.Size = new Size(21, 19);
            label12.TabIndex = 138;
            label12.Text = "%";
            // 
            // averagecpacity_textBox
            // 
            averagecpacity_textBox.BackColor = SystemColors.InactiveBorder;
            averagecpacity_textBox.BorderStyle = BorderStyle.None;
            averagecpacity_textBox.Font = new Font("나눔고딕", 9.75F);
            averagecpacity_textBox.ForeColor = SystemColors.ScrollBar;
            averagecpacity_textBox.Location = new Point(1071, 59);
            averagecpacity_textBox.Name = "averagecpacity_textBox";
            averagecpacity_textBox.Size = new Size(60, 18);
            averagecpacity_textBox.TabIndex = 129;
            averagecpacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F);
            label29.Location = new Point(1006, 59);
            label29.Name = "label29";
            label29.Size = new Size(70, 19);
            label29.TabIndex = 128;
            label29.Text = "평균 효율";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔고딕", 9.75F);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(1148, 28);
            label28.Name = "label28";
            label28.Size = new Size(29, 19);
            label28.TabIndex = 109;
            label28.Text = "kW";
            // 
            // allcapacity_textBox
            // 
            allcapacity_textBox.BackColor = SystemColors.InactiveBorder;
            allcapacity_textBox.BorderStyle = BorderStyle.None;
            allcapacity_textBox.Font = new Font("나눔고딕", 9.75F);
            allcapacity_textBox.ForeColor = SystemColors.ScrollBar;
            allcapacity_textBox.Location = new Point(1071, 28);
            allcapacity_textBox.Name = "allcapacity_textBox";
            allcapacity_textBox.Size = new Size(60, 18);
            allcapacity_textBox.TabIndex = 127;
            allcapacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F);
            label27.Location = new Point(1006, 28);
            label27.Name = "label27";
            label27.Size = new Size(70, 19);
            label27.TabIndex = 127;
            label27.Text = "설치 용량";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(PVMoudle_textBox);
            panel2.Controls.Add(PVType_ComboBox);
            panel2.Controls.Add(BatteryDB_button);
            panel2.Controls.Add(InverterDB_button);
            panel2.Controls.Add(Batterycapacity_s);
            panel2.Controls.Add(PVModuleDB_button);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(Batterycapacity_textBox);
            panel2.Controls.Add(Batterycapacity_n);
            panel2.Controls.Add(PV_dataGridView);
            panel2.Controls.Add(InverterEfficiency_textBox);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(PVModule_textBox);
            panel2.Controls.Add(계통유형);
            panel2.Controls.Add(Battery_textBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(Battery_label);
            panel2.Controls.Add(Inverter_textBox);
            panel2.Location = new Point(0, 79);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 235);
            panel2.TabIndex = 139;
            // 
            // PVMoudle_textBox
            // 
            PVMoudle_textBox.BackColor = Color.White;
            PVMoudle_textBox.BorderStyle = BorderStyle.None;
            PVMoudle_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PVMoudle_textBox.ForeColor = SystemColors.WindowFrame;
            PVMoudle_textBox.Location = new Point(187, 16);
            PVMoudle_textBox.Name = "PVMoudle_textBox";
            PVMoudle_textBox.ReadOnly = true;
            PVMoudle_textBox.Size = new Size(116, 18);
            PVMoudle_textBox.TabIndex = 145;
            PVMoudle_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PVType_ComboBox
            // 
            PVType_ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            PVType_ComboBox.FormattingEnabled = true;
            PVType_ComboBox.Location = new Point(164, 46);
            PVType_ComboBox.Name = "PVType_ComboBox";
            PVType_ComboBox.Size = new Size(139, 24);
            PVType_ComboBox.TabIndex = 144;
            PVType_ComboBox.SelectedIndexChanged += PVType_ComboBox_SelectedIndexChanged;
            // 
            // BatteryDB_button
            // 
            BatteryDB_button.BackColor = SystemColors.ControlLight;
            BatteryDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            BatteryDB_button.FlatStyle = FlatStyle.System;
            BatteryDB_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            BatteryDB_button.Location = new Point(164, 110);
            BatteryDB_button.Margin = new Padding(0);
            BatteryDB_button.Name = "BatteryDB_button";
            BatteryDB_button.Size = new Size(23, 23);
            BatteryDB_button.TabIndex = 143;
            BatteryDB_button.Text = "+";
            BatteryDB_button.UseVisualStyleBackColor = false;
            // 
            // InverterDB_button
            // 
            InverterDB_button.BackColor = SystemColors.ControlLight;
            InverterDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            InverterDB_button.FlatStyle = FlatStyle.System;
            InverterDB_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            InverterDB_button.Location = new Point(164, 80);
            InverterDB_button.Margin = new Padding(0);
            InverterDB_button.Name = "InverterDB_button";
            InverterDB_button.Size = new Size(23, 23);
            InverterDB_button.TabIndex = 142;
            InverterDB_button.Text = "+";
            InverterDB_button.UseVisualStyleBackColor = false;
            // 
            // PVModuleDB_button
            // 
            PVModuleDB_button.BackColor = SystemColors.ControlLight;
            PVModuleDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PVModuleDB_button.FlatStyle = FlatStyle.System;
            PVModuleDB_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            PVModuleDB_button.Location = new Point(164, 14);
            PVModuleDB_button.Margin = new Padding(0);
            PVModuleDB_button.Name = "PVModuleDB_button";
            PVModuleDB_button.Size = new Size(23, 23);
            PVModuleDB_button.TabIndex = 141;
            PVModuleDB_button.Text = "+";
            PVModuleDB_button.UseVisualStyleBackColor = false;
            PVModuleDB_button.Click += PVModuleDB_button_Click;
            // 
            // PV_dataGridView
            // 
            PV_dataGridView.AllowUserToAddRows = false;
            PV_dataGridView.AllowUserToDeleteRows = false;
            PV_dataGridView.AllowUserToResizeColumns = false;
            PV_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PV_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PV_dataGridView.BackgroundColor = Color.White;
            PV_dataGridView.BorderStyle = BorderStyle.None;
            PV_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PV_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PV_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PV_dataGridView.Dock = DockStyle.Bottom;
            PV_dataGridView.Location = new Point(0, 147);
            PV_dataGridView.Name = "PV_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PV_dataGridView.RowHeadersVisible = false;
            PV_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            PV_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            PV_dataGridView.Size = new Size(1000, 88);
            PV_dataGridView.TabIndex = 96;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.DisplayStyleProvider.BorderColor = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.BorderColorHot = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.CloserColor = Color.Empty;
            tabControl1.DisplayStyleProvider.FocusTrack = true;
            tabControl1.DisplayStyleProvider.HotTrack = true;
            tabControl1.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            tabControl1.DisplayStyleProvider.Opacity = 1F;
            tabControl1.DisplayStyleProvider.Overlap = 0;
            tabControl1.DisplayStyleProvider.Padding = new Point(6, 3);
            tabControl1.DisplayStyleProvider.ShowTabCloser = false;
            tabControl1.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            tabControl1.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            tabControl1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(0, 320);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 300);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 140;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(DoorH2_textBox);
            tabPage1.Controls.Add(label18);
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(992, 271);
            tabPage1.TabIndex = 4;
            tabPage1.Text = "태양광 설치 정보";
            // 
            // DoorH2_textBox
            // 
            DoorH2_textBox.BackColor = Color.White;
            DoorH2_textBox.BorderStyle = BorderStyle.None;
            DoorH2_textBox.Enabled = false;
            DoorH2_textBox.Font = new Font("나눔고딕", 9.75F);
            DoorH2_textBox.ForeColor = SystemColors.ControlDark;
            DoorH2_textBox.Location = new Point(72, 6);
            DoorH2_textBox.Name = "DoorH2_textBox";
            DoorH2_textBox.Size = new Size(60, 18);
            DoorH2_textBox.TabIndex = 187;
            DoorH2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔고딕", 9.75F);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(7, 7);
            label18.Name = "label18";
            label18.Size = new Size(81, 19);
            label18.TabIndex = 182;
            label18.Text = "[Ad] 문면적";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Controls.Add(label24);
            tabPage2.Controls.Add(webView21);
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(992, 271);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "에너지 생산량 정보";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("나눔고딕", 9.75F);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(8, 15);
            label24.Name = "label24";
            label24.Size = new Size(56, 19);
            label24.TabIndex = 163;
            label24.Text = "W/m²·K";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(3, 33);
            webView21.Name = "webView21";
            webView21.Size = new Size(986, 235);
            webView21.TabIndex = 154;
            webView21.ZoomFactor = 1D;
            // 
            // PV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 239, 239);
            ClientSize = new Size(1200, 896);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Controls.Add(label12);
            Controls.Add(averagecpacity_textBox);
            Controls.Add(label29);
            Controls.Add(label28);
            Controls.Add(allcapacity_textBox);
            Controls.Add(label27);
            Controls.Add(Previous_button);
            Controls.Add(Save_button);
            Controls.Add(PVMainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PV";
            Text = "Form3";
            PVMainPanel.ResumeLayout(false);
            PVMainPanel.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PVMainPanel;
        private PictureBox pictureBox1;
        private Label 계통유형;
        private Label label6;
        private Label Battery_label;
        private Label label7;
        private TextBox Battery_textBox;
        private TextBox Inverter_textBox;
        private TextBox PVModule_textBox;
        private TextBox Batterycapacity_textBox;
        private TextBox InverterEfficiency_textBox;
        private Label Batterycapacity_n;
        private Label label10;
        private Label Batterycapacity_s;
        private Button InverterDB1_button;
        private Button Previous_button;
        private Button Save_button;
        private Label label4;
        private TextBox averagecpacity_textBox;
        private Label label29;
        private Label label28;
        private TextBox allcapacity_textBox;
        private Label label27;
        private Label label12;
        private Panel panel2;
        private DataGridView PV_dataGridView;
        private Label label8;
        private CustomComboBox Oldsystem_comboBox;
        private Label label9;
        private Label label11;
        private RadioButton radioButton2;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton1;
        private Label label1;
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private CustomComboBox PVsystem_combobox;
        private Label label3;
        private Panel panel4;
        private Button PVModuleDB_button;
        private Button BatteryDB_button;
        private Button InverterDB_button;
        private CustomComboBox OldPVSystem_ComboBox;
        private CustomComboBox PVType_ComboBox;
        private CustomTabControl tabControl1;
        private TabPage tabPage1;
        private TextBox DoorH2_textBox;
        private Label label18;
        private TabPage tabPage2;
        private Label label24;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private TextBox PVMoudle_textBox;
    }
}