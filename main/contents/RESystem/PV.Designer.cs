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
            Battery_textBox = new TextBox();
            Inverter_textBox = new TextBox();
            Battery_label = new Label();
            label7 = new Label();
            label6 = new Label();
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
            PVinstall_tabPage = new TabPage();
            panel5 = new Panel();
            ShpictureBox = new PictureBox();
            panel3 = new Panel();
            batterypower = new Label();
            BatteryEff_textbox = new Label();
            InverterEff_textbox = new Label();
            pvname = new Label();
            pvtotal = new Label();
            pvpower = new Label();
            pvsize = new Label();
            PVTypepictureBox = new PictureBox();
            PVpictureBox = new PictureBox();
            DoorH2_textBox = new TextBox();
            label18 = new Label();
            PVCalc_tabPage = new TabPage();
            label24 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new Panel();
            PVMainPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            PVinstall_tabPage.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ShpictureBox).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PVTypepictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PVpictureBox).BeginInit();
            PVCalc_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            panel1.SuspendLayout();
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
            PVModule_textBox.Size = new Size(116, 15);
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
            OldPVSystem_ComboBox.SelectedIndexChanged += OldPVSystem_ComboBox_SelectedIndexChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton4.ForeColor = Color.White;
            radioButton4.Location = new Point(137, 50);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(93, 19);
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
            radioButton2.Size = new Size(49, 19);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "보수";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton3.ForeColor = Color.White;
            radioButton3.Location = new Point(70, 50);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(49, 19);
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
            radioButton1.Size = new Size(49, 19);
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
            label8.Size = new Size(35, 15);
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
            label9.Size = new Size(35, 15);
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
            label11.Size = new Size(35, 15);
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
            label3.Size = new Size(39, 15);
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
            label1.Size = new Size(39, 15);
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
            Num_textBox.Size = new Size(139, 15);
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
            Name_textBox.Size = new Size(139, 22);
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
            계통유형.Size = new Size(63, 15);
            계통유형.TabIndex = 91;
            계통유형.Text = "계통  유형";
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
            Battery_textBox.Size = new Size(116, 15);
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
            Inverter_textBox.Size = new Size(116, 15);
            Inverter_textBox.TabIndex = 98;
            Inverter_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Battery_label
            // 
            Battery_label.AutoSize = true;
            Battery_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Battery_label.Location = new Point(101, 112);
            Battery_label.Name = "Battery_label";
            Battery_label.Size = new Size(51, 15);
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
            label7.Size = new Size(51, 15);
            label7.TabIndex = 96;
            label7.Text = "인 버 터";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(80, 16);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 95;
            label6.Text = "태양광 모듈";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1033, 638);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 99;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(1148, 59);
            label12.Name = "label12";
            label12.Size = new Size(21, 15);
            label12.TabIndex = 138;
            label12.Text = "%";
            // 
            // averagecpacity_textBox
            // 
            averagecpacity_textBox.BackColor = SystemColors.InactiveBorder;
            averagecpacity_textBox.BorderStyle = BorderStyle.None;
            averagecpacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            averagecpacity_textBox.ForeColor = Color.Black;
            averagecpacity_textBox.Location = new Point(1085, 59);
            averagecpacity_textBox.Name = "averagecpacity_textBox";
            averagecpacity_textBox.Size = new Size(60, 15);
            averagecpacity_textBox.TabIndex = 129;
            averagecpacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F);
            label29.Location = new Point(1006, 59);
            label29.Name = "label29";
            label29.Size = new Size(59, 15);
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
            label28.Size = new Size(52, 15);
            label28.TabIndex = 109;
            label28.Text = "kWh/년";
            // 
            // allcapacity_textBox
            // 
            allcapacity_textBox.BackColor = SystemColors.InactiveBorder;
            allcapacity_textBox.BorderStyle = BorderStyle.None;
            allcapacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            allcapacity_textBox.ForeColor = Color.Black;
            allcapacity_textBox.Location = new Point(1085, 28);
            allcapacity_textBox.Name = "allcapacity_textBox";
            allcapacity_textBox.Size = new Size(60, 15);
            allcapacity_textBox.TabIndex = 127;
            allcapacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F);
            label27.Location = new Point(1006, 28);
            label27.Name = "label27";
            label27.Size = new Size(71, 15);
            label27.TabIndex = 127;
            label27.Text = "연간 생산량";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(PVMoudle_textBox);
            panel2.Controls.Add(PVType_ComboBox);
            panel2.Controls.Add(BatteryDB_button);
            panel2.Controls.Add(InverterDB_button);
            panel2.Controls.Add(PVModuleDB_button);
            panel2.Controls.Add(PV_dataGridView);
            panel2.Controls.Add(label6);
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
            PVMoudle_textBox.Size = new Size(116, 15);
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
            BatteryDB_button.Click += BatteryDB_button_Click_1;
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
            InverterDB_button.Click += InverterDB_button_Click_1;
            // 
            // PVModuleDB_button
            // 
            PVModuleDB_button.BackColor = SystemColors.ControlLight;
            PVModuleDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PVModuleDB_button.FlatStyle = FlatStyle.System;
            PVModuleDB_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            PVModuleDB_button.Location = new Point(164, 12);
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
            PV_dataGridView.CellValueChanged += PV_dataGridView_CellValueChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(PVinstall_tabPage);
            tabControl1.Controls.Add(PVCalc_tabPage);
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
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 350);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 140;
            // 
            // PVinstall_tabPage
            // 
            PVinstall_tabPage.BackColor = Color.White;
            PVinstall_tabPage.Controls.Add(panel5);
            PVinstall_tabPage.Controls.Add(panel3);
            PVinstall_tabPage.Controls.Add(DoorH2_textBox);
            PVinstall_tabPage.Controls.Add(label18);
            PVinstall_tabPage.Location = new Point(4, 25);
            PVinstall_tabPage.Name = "PVinstall_tabPage";
            PVinstall_tabPage.Padding = new Padding(3);
            PVinstall_tabPage.Size = new Size(992, 321);
            PVinstall_tabPage.TabIndex = 4;
            PVinstall_tabPage.Text = "태양광 설치 정보";
            // 
            // panel5
            // 
            panel5.Controls.Add(ShpictureBox);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(622, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(367, 315);
            panel5.TabIndex = 190;
            // 
            // ShpictureBox
            // 
            ShpictureBox.Location = new Point(81, 52);
            ShpictureBox.Name = "ShpictureBox";
            ShpictureBox.Size = new Size(109, 108);
            ShpictureBox.TabIndex = 194;
            ShpictureBox.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(batterypower);
            panel3.Controls.Add(BatteryEff_textbox);
            panel3.Controls.Add(InverterEff_textbox);
            panel3.Controls.Add(pvname);
            panel3.Controls.Add(pvtotal);
            panel3.Controls.Add(pvpower);
            panel3.Controls.Add(pvsize);
            panel3.Controls.Add(PVTypepictureBox);
            panel3.Controls.Add(PVpictureBox);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(619, 315);
            panel3.TabIndex = 189;
            // 
            // batterypower
            // 
            batterypower.AutoSize = true;
            batterypower.Location = new Point(380, 287);
            batterypower.Name = "batterypower";
            batterypower.Size = new Size(77, 15);
            batterypower.TabIndex = 195;
            batterypower.Text = "batterypower";
            batterypower.Visible = false;
            // 
            // BatteryEff_textbox
            // 
            BatteryEff_textbox.AutoSize = true;
            BatteryEff_textbox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            BatteryEff_textbox.Location = new Point(53, 87);
            BatteryEff_textbox.Name = "BatteryEff_textbox";
            BatteryEff_textbox.Size = new Size(0, 15);
            BatteryEff_textbox.TabIndex = 194;
            BatteryEff_textbox.Visible = false;
            // 
            // InverterEff_textbox
            // 
            InverterEff_textbox.AutoSize = true;
            InverterEff_textbox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            InverterEff_textbox.Location = new Point(47, 73);
            InverterEff_textbox.Name = "InverterEff_textbox";
            InverterEff_textbox.Size = new Size(0, 15);
            InverterEff_textbox.TabIndex = 146;
            InverterEff_textbox.Visible = false;
            // 
            // pvname
            // 
            pvname.AutoSize = true;
            pvname.Location = new Point(523, 22);
            pvname.Name = "pvname";
            pvname.Size = new Size(50, 15);
            pvname.TabIndex = 193;
            pvname.Text = "pvname";
            // 
            // pvtotal
            // 
            pvtotal.AutoSize = true;
            pvtotal.Location = new Point(231, 287);
            pvtotal.Name = "pvtotal";
            pvtotal.Size = new Size(44, 15);
            pvtotal.TabIndex = 192;
            pvtotal.Text = "pvtotal";
            // 
            // pvpower
            // 
            pvpower.AutoSize = true;
            pvpower.Location = new Point(231, 121);
            pvpower.Name = "pvpower";
            pvpower.Size = new Size(53, 15);
            pvpower.TabIndex = 191;
            pvpower.Text = "pvpower";
            // 
            // pvsize
            // 
            pvsize.AutoSize = true;
            pvsize.Location = new Point(231, 64);
            pvsize.Name = "pvsize";
            pvsize.Size = new Size(40, 15);
            pvsize.TabIndex = 190;
            pvsize.Text = "pvsize";
            // 
            // PVTypepictureBox
            // 
            PVTypepictureBox.Location = new Point(404, 87);
            PVTypepictureBox.Name = "PVTypepictureBox";
            PVTypepictureBox.Size = new Size(109, 108);
            PVTypepictureBox.TabIndex = 189;
            PVTypepictureBox.TabStop = false;
            // 
            // PVpictureBox
            // 
            PVpictureBox.Location = new Point(47, 121);
            PVpictureBox.Name = "PVpictureBox";
            PVpictureBox.Size = new Size(109, 108);
            PVpictureBox.TabIndex = 188;
            PVpictureBox.TabStop = false;
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
            DoorH2_textBox.Size = new Size(60, 15);
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
            label18.Size = new Size(74, 15);
            label18.TabIndex = 182;
            label18.Text = "[Ad] 문면적";
            // 
            // PVCalc_tabPage
            // 
            PVCalc_tabPage.BackColor = Color.White;
            PVCalc_tabPage.Controls.Add(label24);
            PVCalc_tabPage.Controls.Add(webView21);
            PVCalc_tabPage.Location = new Point(4, 25);
            PVCalc_tabPage.Name = "PVCalc_tabPage";
            PVCalc_tabPage.Padding = new Padding(3);
            PVCalc_tabPage.Size = new Size(992, 321);
            PVCalc_tabPage.TabIndex = 1;
            PVCalc_tabPage.Text = "에너지 생산량 정보";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("나눔고딕", 9.75F);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(935, 3);
            label24.Name = "label24";
            label24.Size = new Size(57, 15);
            label24.TabIndex = 163;
            label24.Text = "kWh/m²";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(3, 21);
            webView21.Name = "webView21";
            webView21.Size = new Size(986, 297);
            webView21.TabIndex = 154;
            webView21.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tabControl1);
            panel1.Location = new Point(0, 313);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 350);
            panel1.TabIndex = 141;
            // 
            // PV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 239, 239);
            ClientSize = new Size(1200, 896);
            Controls.Add(panel2);
            Controls.Add(label12);
            Controls.Add(averagecpacity_textBox);
            Controls.Add(label29);
            Controls.Add(label28);
            Controls.Add(allcapacity_textBox);
            Controls.Add(label27);
            Controls.Add(Save_button);
            Controls.Add(PVMainPanel);
            Controls.Add(panel1);
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
            PVinstall_tabPage.ResumeLayout(false);
            PVinstall_tabPage.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ShpictureBox).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PVTypepictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PVpictureBox).EndInit();
            PVCalc_tabPage.ResumeLayout(false);
            PVCalc_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            panel1.ResumeLayout(false);
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
        private Button InverterDB1_button;
        private Button Save_button;
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
        private TabPage PVinstall_tabPage;
        private TextBox DoorH2_textBox;
        private Label label18;
        private TabPage PVCalc_tabPage;
        private Label label24;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private TextBox PVMoudle_textBox;
        private Panel panel1;
        private Panel panel3;
        private PictureBox PVpictureBox;
        private PictureBox PVTypepictureBox;
        private Label pvname;
        private Label pvtotal;
        private Label pvpower;
        private Label pvsize;
        private Panel panel5;
        private PictureBox ShpictureBox;
        private Label InverterEff_textbox;
        private Label BatteryEff_textbox;
        private Label batterypower;
    }
}