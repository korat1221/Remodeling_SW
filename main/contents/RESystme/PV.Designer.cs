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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PV));
            PVModule_textBox = new TextBox();
            GeneralPanel = new Panel();
            label1 = new Label();
            Num_textBox = new TextBox();
            Name_textBox = new TextBox();
            label8 = new Label();
            Oldsystem_comboBox = new CustomComboBox();
            label9 = new Label();
            label11 = new Label();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton1 = new RadioButton();
            계통유형 = new Label();
            panel1 = new Panel();
            PVsystem_combobox = new CustomComboBox();
            label4 = new Label();
            BatteryDB_button = new Button();
            InverterDB_button = new Button();
            Batterycapacity_s = new Label();
            Batterycapacity_textBox = new TextBox();
            InverterEfficiency_textBox = new TextBox();
            Batterycapacity_n = new Label();
            label10 = new Label();
            Battery_textBox = new TextBox();
            Inverter_textBox = new TextBox();
            Battery_label = new Label();
            label7 = new Label();
            PVModuleDB_button = new Button();
            label6 = new Label();
            AdditionalPanel = new Panel();
            VentilationType_comboBox = new CustomComboBox();
            slope_comboBox = new CustomComboBox();
            orientation_comboBox = new CustomComboBox();
            label23 = new Label();
            height_label2 = new Label();
            width_label2 = new Label();
            label2 = new Label();
            label26 = new Label();
            PVLshobst_m_image_textBox = new TextBox();
            label25 = new Label();
            PVHshobst_m_imge_textBox = new TextBox();
            pictureBox2 = new PictureBox();
            height_n_textBox = new TextBox();
            width_label = new Label();
            height_label = new Label();
            label21 = new Label();
            label20 = new Label();
            PVHshobst_m_textBox = new TextBox();
            PVLshobst_m_textBox = new TextBox();
            label14 = new Label();
            PVArea_m2_textBox = new TextBox();
            label17 = new Label();
            label18 = new Label();
            install_label = new Label();
            label19 = new Label();
            width_n_textBox = new TextBox();
            label15 = new Label();
            label5 = new Label();
            label13 = new Label();
            Previous_button = new Button();
            Save_button = new Button();
            label12 = new Label();
            averagecpacity_textBox = new TextBox();
            label29 = new Label();
            label28 = new Label();
            allcapacity_textBox = new TextBox();
            label27 = new Label();
            panel2 = new Panel();
            PV_dataGridView = new DataGridView();
            label36 = new Label();
            panel3 = new Panel();
            label3 = new Label();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            label22 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // PVModule_textBox
            // 
            PVModule_textBox.BackColor = SystemColors.Window;
            PVModule_textBox.BorderStyle = BorderStyle.None;
            PVModule_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVModule_textBox.Location = new Point(206, 14);
            PVModule_textBox.Name = "PVModule_textBox";
            PVModule_textBox.ReadOnly = true;
            PVModule_textBox.Size = new Size(120, 15);
            PVModule_textBox.TabIndex = 94;
            PVModule_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Controls.Add(label8);
            GeneralPanel.Controls.Add(Oldsystem_comboBox);
            GeneralPanel.Controls.Add(label9);
            GeneralPanel.Controls.Add(label11);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(136, 33);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 133;
            label1.Text = "명칭";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(74, 33);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(56, 15);
            Num_textBox.TabIndex = 132;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(173, 30);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 131;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(425, 16);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 130;
            label8.Text = "기존";
            // 
            // Oldsystem_comboBox
            // 
            Oldsystem_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Oldsystem_comboBox.FormattingEnabled = true;
            Oldsystem_comboBox.Location = new Point(755, 59);
            Oldsystem_comboBox.Name = "Oldsystem_comboBox";
            Oldsystem_comboBox.Size = new Size(120, 24);
            Oldsystem_comboBox.TabIndex = 126;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(425, 72);
            label9.Name = "label9";
            label9.Size = new Size(31, 15);
            label9.TabIndex = 129;
            label9.Text = "신규";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(425, 44);
            label11.Name = "label11";
            label11.Size = new Size(31, 15);
            label11.TabIndex = 128;
            label11.Text = "보수";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(18, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 90;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(461, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(212, 95);
            groupBox1.TabIndex = 127;
            groupBox1.TabStop = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(17, 41);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(49, 19);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "보수";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(98, 69);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(93, 19);
            radioButton4.TabIndex = 2;
            radioButton4.TabStop = true;
            radioButton4.Text = "철거 후 신규";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(17, 69);
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
            radioButton1.Location = new Point(17, 13);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // 계통유형
            // 
            계통유형.AutoSize = true;
            계통유형.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            계통유형.Location = new Point(16, 12);
            계통유형.Name = "계통유형";
            계통유형.Size = new Size(55, 15);
            계통유형.TabIndex = 91;
            계통유형.Text = "계통유형";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(PVsystem_combobox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(BatteryDB_button);
            panel1.Controls.Add(InverterDB_button);
            panel1.Controls.Add(Batterycapacity_s);
            panel1.Controls.Add(Batterycapacity_textBox);
            panel1.Controls.Add(계통유형);
            panel1.Controls.Add(InverterEfficiency_textBox);
            panel1.Controls.Add(Batterycapacity_n);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(Battery_textBox);
            panel1.Controls.Add(Inverter_textBox);
            panel1.Controls.Add(Battery_label);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(12, 296);
            panel1.Name = "panel1";
            panel1.Size = new Size(977, 113);
            panel1.TabIndex = 19;
            panel1.Paint += panel1_Paint;
            // 
            // PVsystem_combobox
            // 
            PVsystem_combobox.DrawMode = DrawMode.OwnerDrawFixed;
            PVsystem_combobox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVsystem_combobox.FormattingEnabled = true;
            PVsystem_combobox.Location = new Point(206, 8);
            PVsystem_combobox.Name = "PVsystem_combobox";
            PVsystem_combobox.Size = new Size(120, 23);
            PVsystem_combobox.TabIndex = 130;
            PVsystem_combobox.SelectedIndexChanged += PVsystem_combobox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlDark;
            label4.Location = new Point(877, 50);
            label4.Name = "label4";
            label4.Size = new Size(21, 15);
            label4.TabIndex = 129;
            label4.Text = "%";
            // 
            // BatteryDB_button
            // 
            BatteryDB_button.BackColor = SystemColors.ControlLight;
            BatteryDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            BatteryDB_button.FlatStyle = FlatStyle.System;
            BatteryDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            BatteryDB_button.Location = new Point(328, 80);
            BatteryDB_button.Margin = new Padding(0);
            BatteryDB_button.Name = "BatteryDB_button";
            BatteryDB_button.Size = new Size(23, 23);
            BatteryDB_button.TabIndex = 108;
            BatteryDB_button.Text = "+";
            BatteryDB_button.UseVisualStyleBackColor = false;
            BatteryDB_button.Visible = false;
            BatteryDB_button.Click += BatteryDB_button_Click;
            // 
            // InverterDB_button
            // 
            InverterDB_button.BackColor = SystemColors.ControlLight;
            InverterDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            InverterDB_button.FlatStyle = FlatStyle.System;
            InverterDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            InverterDB_button.Location = new Point(328, 46);
            InverterDB_button.Margin = new Padding(0);
            InverterDB_button.Name = "InverterDB_button";
            InverterDB_button.Size = new Size(23, 23);
            InverterDB_button.TabIndex = 107;
            InverterDB_button.Text = "+";
            InverterDB_button.UseVisualStyleBackColor = false;
            InverterDB_button.Click += InverterDB_button_Click;
            // 
            // Batterycapacity_s
            // 
            Batterycapacity_s.AutoSize = true;
            Batterycapacity_s.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity_s.ForeColor = SystemColors.ControlDark;
            Batterycapacity_s.Location = new Point(878, 84);
            Batterycapacity_s.Name = "Batterycapacity_s";
            Batterycapacity_s.Size = new Size(27, 15);
            Batterycapacity_s.TabIndex = 106;
            Batterycapacity_s.Text = "kW";
            Batterycapacity_s.Visible = false;
            // 
            // Batterycapacity_textBox
            // 
            Batterycapacity_textBox.BackColor = SystemColors.Window;
            Batterycapacity_textBox.BorderStyle = BorderStyle.None;
            Batterycapacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity_textBox.ForeColor = SystemColors.ScrollBar;
            Batterycapacity_textBox.Location = new Point(755, 84);
            Batterycapacity_textBox.Name = "Batterycapacity_textBox";
            Batterycapacity_textBox.ReadOnly = true;
            Batterycapacity_textBox.Size = new Size(120, 15);
            Batterycapacity_textBox.TabIndex = 105;
            Batterycapacity_textBox.TextAlign = HorizontalAlignment.Center;
            Batterycapacity_textBox.Visible = false;
            // 
            // InverterEfficiency_textBox
            // 
            InverterEfficiency_textBox.BackColor = SystemColors.Window;
            InverterEfficiency_textBox.BorderStyle = BorderStyle.None;
            InverterEfficiency_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            InverterEfficiency_textBox.ForeColor = SystemColors.ScrollBar;
            InverterEfficiency_textBox.Location = new Point(755, 50);
            InverterEfficiency_textBox.Name = "InverterEfficiency_textBox";
            InverterEfficiency_textBox.ReadOnly = true;
            InverterEfficiency_textBox.Size = new Size(120, 15);
            InverterEfficiency_textBox.TabIndex = 104;
            InverterEfficiency_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Batterycapacity_n
            // 
            Batterycapacity_n.AutoSize = true;
            Batterycapacity_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity_n.ForeColor = SystemColors.ControlDark;
            Batterycapacity_n.Location = new Point(625, 84);
            Batterycapacity_n.Name = "Batterycapacity_n";
            Batterycapacity_n.Size = new Size(71, 15);
            Batterycapacity_n.TabIndex = 103;
            Batterycapacity_n.Text = "배터리 용량";
            Batterycapacity_n.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(625, 50);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 102;
            label10.Text = "인버터 효율";
            // 
            // Battery_textBox
            // 
            Battery_textBox.BackColor = SystemColors.Window;
            Battery_textBox.BorderStyle = BorderStyle.None;
            Battery_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Battery_textBox.Location = new Point(203, 84);
            Battery_textBox.Name = "Battery_textBox";
            Battery_textBox.ReadOnly = true;
            Battery_textBox.Size = new Size(120, 15);
            Battery_textBox.TabIndex = 99;
            Battery_textBox.TextAlign = HorizontalAlignment.Center;
            Battery_textBox.Visible = false;
            // 
            // Inverter_textBox
            // 
            Inverter_textBox.BackColor = SystemColors.Window;
            Inverter_textBox.BorderStyle = BorderStyle.None;
            Inverter_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Inverter_textBox.Location = new Point(203, 50);
            Inverter_textBox.Name = "Inverter_textBox";
            Inverter_textBox.ReadOnly = true;
            Inverter_textBox.Size = new Size(120, 15);
            Inverter_textBox.TabIndex = 98;
            Inverter_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Battery_label
            // 
            Battery_label.AutoSize = true;
            Battery_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Battery_label.Location = new Point(16, 84);
            Battery_label.Name = "Battery_label";
            Battery_label.Size = new Size(43, 15);
            Battery_label.TabIndex = 97;
            Battery_label.Text = "배터리";
            Battery_label.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(16, 50);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 96;
            label7.Text = "인버터";
            // 
            // PVModuleDB_button
            // 
            PVModuleDB_button.BackColor = SystemColors.ControlLight;
            PVModuleDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PVModuleDB_button.FlatStyle = FlatStyle.System;
            PVModuleDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            PVModuleDB_button.Location = new Point(331, 10);
            PVModuleDB_button.Margin = new Padding(0);
            PVModuleDB_button.Name = "PVModuleDB_button";
            PVModuleDB_button.Size = new Size(23, 23);
            PVModuleDB_button.TabIndex = 89;
            PVModuleDB_button.Text = "+";
            PVModuleDB_button.UseVisualStyleBackColor = false;
            PVModuleDB_button.Click += PVDB_button_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(19, 14);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 95;
            label6.Text = "태양광 모듈";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.Controls.Add(VentilationType_comboBox);
            AdditionalPanel.Controls.Add(slope_comboBox);
            AdditionalPanel.Controls.Add(orientation_comboBox);
            AdditionalPanel.Controls.Add(label23);
            AdditionalPanel.Controls.Add(height_label2);
            AdditionalPanel.Controls.Add(width_label2);
            AdditionalPanel.Controls.Add(label2);
            AdditionalPanel.Controls.Add(label26);
            AdditionalPanel.Controls.Add(PVLshobst_m_image_textBox);
            AdditionalPanel.Controls.Add(label25);
            AdditionalPanel.Controls.Add(PVHshobst_m_imge_textBox);
            AdditionalPanel.Controls.Add(pictureBox2);
            AdditionalPanel.Controls.Add(height_n_textBox);
            AdditionalPanel.Controls.Add(width_label);
            AdditionalPanel.Controls.Add(height_label);
            AdditionalPanel.Controls.Add(label21);
            AdditionalPanel.Controls.Add(label20);
            AdditionalPanel.Controls.Add(PVHshobst_m_textBox);
            AdditionalPanel.Controls.Add(PVLshobst_m_textBox);
            AdditionalPanel.Controls.Add(label14);
            AdditionalPanel.Controls.Add(PVArea_m2_textBox);
            AdditionalPanel.Controls.Add(label17);
            AdditionalPanel.Controls.Add(label18);
            AdditionalPanel.Controls.Add(install_label);
            AdditionalPanel.Controls.Add(label19);
            AdditionalPanel.Controls.Add(width_n_textBox);
            AdditionalPanel.Controls.Add(label15);
            AdditionalPanel.Location = new Point(12, 429);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 221);
            AdditionalPanel.TabIndex = 18;
            AdditionalPanel.Paint += AdditionalPanel_Paint;
            // 
            // VentilationType_comboBox
            // 
            VentilationType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            VentilationType_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            VentilationType_comboBox.FormattingEnabled = true;
            VentilationType_comboBox.Location = new Point(203, 177);
            VentilationType_comboBox.Name = "VentilationType_comboBox";
            VentilationType_comboBox.Size = new Size(120, 23);
            VentilationType_comboBox.TabIndex = 134;
            VentilationType_comboBox.SelectedIndexChanged += VentilationType_comboBox_SelectedIndexChanged;
            // 
            // slope_comboBox
            // 
            slope_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            slope_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            slope_comboBox.FormattingEnabled = true;
            slope_comboBox.Location = new Point(460, 90);
            slope_comboBox.Name = "slope_comboBox";
            slope_comboBox.Size = new Size(120, 23);
            slope_comboBox.TabIndex = 133;
            slope_comboBox.SelectedIndexChanged += slope_comboBox_SelectedIndexChanged;
            // 
            // orientation_comboBox
            // 
            orientation_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            orientation_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            orientation_comboBox.FormattingEnabled = true;
            orientation_comboBox.Location = new Point(203, 90);
            orientation_comboBox.Name = "orientation_comboBox";
            orientation_comboBox.Size = new Size(120, 23);
            orientation_comboBox.TabIndex = 132;
            orientation_comboBox.SelectedIndexChanged += orientation_comboBox_SelectedIndexChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(16, 181);
            label23.Name = "label23";
            label23.Size = new Size(59, 15);
            label23.TabIndex = 131;
            label23.Text = "통기 유무";
            // 
            // height_label2
            // 
            height_label2.AutoSize = true;
            height_label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            height_label2.ForeColor = SystemColors.ControlText;
            height_label2.Location = new Point(586, 11);
            height_label2.Name = "height_label2";
            height_label2.Size = new Size(23, 15);
            height_label2.TabIndex = 129;
            height_label2.Text = "EA";
            // 
            // width_label2
            // 
            width_label2.AutoSize = true;
            width_label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            width_label2.ForeColor = SystemColors.ControlText;
            width_label2.Location = new Point(328, 11);
            width_label2.Name = "width_label2";
            width_label2.Size = new Size(23, 15);
            width_label2.TabIndex = 128;
            width_label2.Text = "EA";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(326, 52);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 127;
            label2.Text = "㎡";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(820, 145);
            label26.Name = "label26";
            label26.Size = new Size(19, 15);
            label26.TabIndex = 126;
            label26.Text = "m";
            // 
            // PVLshobst_m_image_textBox
            // 
            PVLshobst_m_image_textBox.BackColor = SystemColors.Window;
            PVLshobst_m_image_textBox.BorderStyle = BorderStyle.None;
            PVLshobst_m_image_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVLshobst_m_image_textBox.ForeColor = SystemColors.ScrollBar;
            PVLshobst_m_image_textBox.Location = new Point(755, 142);
            PVLshobst_m_image_textBox.Name = "PVLshobst_m_image_textBox";
            PVLshobst_m_image_textBox.Size = new Size(62, 15);
            PVLshobst_m_image_textBox.TabIndex = 125;
            PVLshobst_m_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.ForeColor = SystemColors.ControlDark;
            label25.Location = new Point(788, 78);
            label25.Name = "label25";
            label25.Size = new Size(19, 15);
            label25.TabIndex = 124;
            label25.Text = "m";
            // 
            // PVHshobst_m_imge_textBox
            // 
            PVHshobst_m_imge_textBox.BackColor = SystemColors.Window;
            PVHshobst_m_imge_textBox.BorderStyle = BorderStyle.None;
            PVHshobst_m_imge_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVHshobst_m_imge_textBox.ForeColor = SystemColors.ScrollBar;
            PVHshobst_m_imge_textBox.Location = new Point(723, 78);
            PVHshobst_m_imge_textBox.Name = "PVHshobst_m_imge_textBox";
            PVHshobst_m_imge_textBox.Size = new Size(62, 15);
            PVHshobst_m_imge_textBox.TabIndex = 123;
            PVHshobst_m_imge_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.그림1;
            pictureBox2.Location = new Point(625, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(349, 211);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 120;
            pictureBox2.TabStop = false;
            // 
            // height_n_textBox
            // 
            height_n_textBox.BackColor = SystemColors.Window;
            height_n_textBox.BorderStyle = BorderStyle.FixedSingle;
            height_n_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            height_n_textBox.Location = new Point(460, 7);
            height_n_textBox.Name = "height_n_textBox";
            height_n_textBox.Size = new Size(120, 22);
            height_n_textBox.TabIndex = 119;
            height_n_textBox.TextAlign = HorizontalAlignment.Center;
            height_n_textBox.TextChanged += height_n_textBox_TextChanged;
            // 
            // width_label
            // 
            width_label.AutoSize = true;
            width_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            width_label.Location = new Point(156, 11);
            width_label.Name = "width_label";
            width_label.Size = new Size(31, 15);
            width_label.TabIndex = 118;
            width_label.Text = "가로";
            // 
            // height_label
            // 
            height_label.AutoSize = true;
            height_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            height_label.ForeColor = SystemColors.ControlText;
            height_label.Location = new Point(414, 11);
            height_label.Name = "height_label";
            height_label.Size = new Size(31, 15);
            height_label.TabIndex = 117;
            height_label.Text = "세로";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label21.ForeColor = SystemColors.ControlText;
            label21.Location = new Point(328, 140);
            label21.Name = "label21";
            label21.Size = new Size(19, 15);
            label21.TabIndex = 115;
            label21.Text = "m";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label20.ForeColor = SystemColors.ControlText;
            label20.Location = new Point(583, 140);
            label20.Name = "label20";
            label20.Size = new Size(19, 15);
            label20.TabIndex = 109;
            label20.Text = "m";
            // 
            // PVHshobst_m_textBox
            // 
            PVHshobst_m_textBox.BackColor = SystemColors.Window;
            PVHshobst_m_textBox.BorderStyle = BorderStyle.FixedSingle;
            PVHshobst_m_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVHshobst_m_textBox.ForeColor = SystemColors.WindowText;
            PVHshobst_m_textBox.Location = new Point(460, 136);
            PVHshobst_m_textBox.Name = "PVHshobst_m_textBox";
            PVHshobst_m_textBox.Size = new Size(120, 22);
            PVHshobst_m_textBox.TabIndex = 114;
            PVHshobst_m_textBox.TextAlign = HorizontalAlignment.Center;
            PVHshobst_m_textBox.TextChanged += PVHshobst_m_textBox_TextChanged;
            // 
            // PVLshobst_m_textBox
            // 
            PVLshobst_m_textBox.BackColor = SystemColors.Window;
            PVLshobst_m_textBox.BorderStyle = BorderStyle.FixedSingle;
            PVLshobst_m_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVLshobst_m_textBox.Location = new Point(203, 136);
            PVLshobst_m_textBox.Name = "PVLshobst_m_textBox";
            PVLshobst_m_textBox.Size = new Size(120, 22);
            PVLshobst_m_textBox.TabIndex = 111;
            PVLshobst_m_textBox.TextAlign = HorizontalAlignment.Center;
            PVLshobst_m_textBox.TextChanged += PVLshobst_m_textBox_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(18, 140);
            label14.Name = "label14";
            label14.Size = new Size(107, 15);
            label14.TabIndex = 111;
            label14.Text = "지형물까지의 거리";
            // 
            // PVArea_m2_textBox
            // 
            PVArea_m2_textBox.BackColor = SystemColors.Window;
            PVArea_m2_textBox.BorderStyle = BorderStyle.None;
            PVArea_m2_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVArea_m2_textBox.ForeColor = SystemColors.ScrollBar;
            PVArea_m2_textBox.Location = new Point(203, 52);
            PVArea_m2_textBox.Name = "PVArea_m2_textBox";
            PVArea_m2_textBox.Size = new Size(120, 15);
            PVArea_m2_textBox.TabIndex = 109;
            PVArea_m2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(371, 142);
            label17.Name = "label17";
            label17.Size = new Size(83, 15);
            label17.TabIndex = 112;
            label17.Text = "지형물의 높이";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(415, 94);
            label18.Name = "label18";
            label18.Size = new Size(43, 15);
            label18.TabIndex = 111;
            label18.Text = "기울기";
            // 
            // install_label
            // 
            install_label.AutoSize = true;
            install_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            install_label.Location = new Point(16, 11);
            install_label.Name = "install_label";
            install_label.Size = new Size(59, 15);
            install_label.TabIndex = 109;
            install_label.Text = "설치 개수";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(156, 52);
            label19.Name = "label19";
            label19.Size = new Size(47, 15);
            label19.TabIndex = 110;
            label19.Text = "총 면적";
            // 
            // width_n_textBox
            // 
            width_n_textBox.BackColor = SystemColors.Window;
            width_n_textBox.BorderStyle = BorderStyle.FixedSingle;
            width_n_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            width_n_textBox.Location = new Point(203, 7);
            width_n_textBox.Name = "width_n_textBox";
            width_n_textBox.Size = new Size(120, 22);
            width_n_textBox.TabIndex = 109;
            width_n_textBox.TextAlign = HorizontalAlignment.Center;
            width_n_textBox.TextChanged += width_n_textBox_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(18, 94);
            label15.Name = "label15";
            label15.Size = new Size(59, 15);
            label15.TabIndex = 110;
            label15.Text = "설치 방위";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(12, 278);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 94;
            label5.Text = "구성요소정보";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(12, 412);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 95;
            label13.Text = "설치정보";
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
            Save_button.Click += Save_button_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            averagecpacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            averagecpacity_textBox.ForeColor = SystemColors.ScrollBar;
            averagecpacity_textBox.Location = new Point(1071, 59);
            averagecpacity_textBox.Name = "averagecpacity_textBox";
            averagecpacity_textBox.Size = new Size(60, 15);
            averagecpacity_textBox.TabIndex = 129;
            averagecpacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label29.Location = new Point(1006, 59);
            label29.Name = "label29";
            label29.Size = new Size(59, 15);
            label29.TabIndex = 128;
            label29.Text = "평균 효율";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(1148, 28);
            label28.Name = "label28";
            label28.Size = new Size(27, 15);
            label28.TabIndex = 109;
            label28.Text = "kW";
            // 
            // allcapacity_textBox
            // 
            allcapacity_textBox.BackColor = SystemColors.InactiveBorder;
            allcapacity_textBox.BorderStyle = BorderStyle.None;
            allcapacity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            allcapacity_textBox.ForeColor = SystemColors.ScrollBar;
            allcapacity_textBox.Location = new Point(1071, 28);
            allcapacity_textBox.Name = "allcapacity_textBox";
            allcapacity_textBox.Size = new Size(60, 15);
            allcapacity_textBox.TabIndex = 127;
            allcapacity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.Location = new Point(1006, 28);
            label27.Name = "label27";
            label27.Size = new Size(59, 15);
            label27.TabIndex = 127;
            label27.Text = "설치 용량";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(PV_dataGridView);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(PVModule_textBox);
            panel2.Controls.Add(PVModuleDB_button);
            panel2.Location = new Point(12, 134);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 142);
            panel2.TabIndex = 139;
            panel2.Paint += panel2_Paint;
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
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PV_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PV_dataGridView.Location = new Point(18, 36);
            PV_dataGridView.Name = "PV_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PV_dataGridView.RowHeadersVisible = false;
            PV_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            PV_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            PV_dataGridView.RowTemplate.Height = 25;
            PV_dataGridView.Size = new Size(943, 103);
            PV_dataGridView.TabIndex = 96;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label36.Location = new Point(12, 116);
            label36.Name = "label36";
            label36.Size = new Size(71, 15);
            label36.TabIndex = 140;
            label36.Text = "태양광 모듈";
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(label3);
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(pictureBox4);
            panel3.Controls.Add(label22);
            panel3.Controls.Add(webView21);
            panel3.Location = new Point(12, 656);
            panel3.Name = "panel3";
            panel3.Size = new Size(977, 187);
            panel3.TabIndex = 141;
            panel3.Paint += panel3_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 64);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 158;
            label3.Text = "일사량(kWh/m²·mth)";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(143, 57);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(28, 26);
            pictureBox3.TabIndex = 157;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(120, 32);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(51, 19);
            pictureBox4.TabIndex = 156;
            pictureBox4.TabStop = false;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(19, 32);
            label22.Name = "label22";
            label22.Size = new Size(67, 15);
            label22.TabIndex = 155;
            label22.Text = "전기생산량";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(177, 7);
            webView21.Name = "webView21";
            webView21.Size = new Size(781, 177);
            webView21.TabIndex = 154;
            webView21.ZoomFactor = 1D;
            // 
            // PV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 896);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label36);
            Controls.Add(label12);
            Controls.Add(averagecpacity_textBox);
            Controls.Add(label29);
            Controls.Add(label28);
            Controls.Add(allcapacity_textBox);
            Controls.Add(label27);
            Controls.Add(Previous_button);
            Controls.Add(Save_button);
            Controls.Add(panel1);
            Controls.Add(label13);
            Controls.Add(label5);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PV";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel AdditionalPanel;
        private PictureBox pictureBox1;
        private Label 계통유형;
        private Panel panel1;
        private Label label5;
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
        private Button BatteryDB_button;
        private Button InverterDB_button;
        private Button PVModuleDB_button;
        private Label label13;
        private Label width_label;
        private Label height_label;
        private Label label21;
        private Label label20;
        private TextBox PVHshobst_m_textBox;
        private TextBox PVLshobst_m_textBox;
        private Label label14;
        private TextBox PVArea_m2_textBox;
        private Label label17;
        private Label label18;
        private Label install_label;
        private Label label19;
        private TextBox width_n_textBox;
        private Label label15;
        private TextBox height_n_textBox;
        private PictureBox pictureBox2;
        private Button Previous_button;
        private Button Save_button;
        private Label label25;
        private TextBox PVHshobst_m_imge_textBox;
        private Label label26;
        private TextBox PVLshobst_m_image_textBox;
        private Label label2;
        private Label label4;
        private TextBox averagecpacity_textBox;
        private Label label29;
        private Label label28;
        private TextBox allcapacity_textBox;
        private Label label27;
        private Label label12;
        private Label height_label2;
        private Label width_label2;
        private Panel panel2;
        private Label label36;
        private DataGridView PV_dataGridView;
        private Label label8;
        private CustomComboBox Oldsystem_comboBox;
        private Label label9;
        private Label label11;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton1;
        private Label label1;
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private Label label23;
        private Panel panel3;
        private Label label3;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label22;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private CustomComboBox PVsystem_combobox;
        private CustomComboBox VentilationType_comboBox;
        private CustomComboBox slope_comboBox;
        private CustomComboBox orientation_comboBox;
    }
}