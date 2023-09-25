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
            PVModuleType = new TextBox();
            PVsystem_Combobox = new ComboBox();
            GeneralPanel = new Panel();
            PVtype_Combobox = new ComboBox();
            BIPVType_Combobox = new ComboBox();
            BIPV = new Label();
            시스템 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label1 = new Label();
            PVname_texbox = new TextBox();
            panel1 = new Panel();
            label8 = new Label();
            label4 = new Label();
            BatteryDB_button = new Button();
            InverterDB_button = new Button();
            PVDB_button = new Button();
            Batterycapacity_s = new Label();
            Batterycapacity = new TextBox();
            InverterEfficiency = new TextBox();
            PVEfficiency = new TextBox();
            Batterycapacity_n = new Label();
            label10 = new Label();
            label11 = new Label();
            Batteryname = new TextBox();
            Inverter = new TextBox();
            Battery = new Label();
            label7 = new Label();
            label6 = new Label();
            AdditionalPanel = new Panel();
            label2 = new Label();
            label26 = new Label();
            distance_j = new TextBox();
            label25 = new Label();
            obsheight_j = new TextBox();
            pictureBox2 = new PictureBox();
            height_n = new TextBox();
            width = new Label();
            height = new Label();
            orientation = new ComboBox();
            slope = new ComboBox();
            label21 = new Label();
            label20 = new Label();
            obsheight = new TextBox();
            distance = new TextBox();
            label14 = new Label();
            textBox13 = new TextBox();
            label17 = new Label();
            label18 = new Label();
            install = new Label();
            label19 = new Label();
            width_n = new TextBox();
            label15 = new Label();
            label5 = new Label();
            label13 = new Label();
            Previous_button = new Button();
            Save_button = new Button();
            PVTypeBox = new PictureBox();
            panel2 = new Panel();
            Caculation_Button = new Button();
            label12 = new Label();
            matchingfacor_n = new TextBox();
            matchingfactor = new Label();
            pvgrid_s = new Label();
            PVgrid = new TextBox();
            label31 = new Label();
            PVusing = new TextBox();
            label30 = new Label();
            PVproduction = new TextBox();
            batteryimage = new PictureBox();
            averagecpacity = new TextBox();
            label29 = new Label();
            label28 = new Label();
            allcapacity = new TextBox();
            label27 = new Label();
            pictureBox3 = new PictureBox();
            label9 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PVTypeBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)batteryimage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // PVModuleType
            // 
            PVModuleType.BackColor = SystemColors.Window;
            PVModuleType.BorderStyle = BorderStyle.None;
            PVModuleType.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVModuleType.Location = new Point(203, 6);
            PVModuleType.Name = "PVModuleType";
            PVModuleType.ReadOnly = true;
            PVModuleType.Size = new Size(120, 15);
            PVModuleType.TabIndex = 94;
            PVModuleType.TextAlign = HorizontalAlignment.Center;
            PVModuleType.TextChanged += PVModuleType_TextChanged;
            // 
            // PVsystem_Combobox
            // 
            PVsystem_Combobox.FormattingEnabled = true;
            PVsystem_Combobox.Location = new Point(683, 8);
            PVsystem_Combobox.Name = "PVsystem_Combobox";
            PVsystem_Combobox.Size = new Size(121, 23);
            PVsystem_Combobox.TabIndex = 0;
            PVsystem_Combobox.SelectedIndexChanged += system_SelectedIndexChanged;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(PVtype_Combobox);
            GeneralPanel.Controls.Add(BIPVType_Combobox);
            GeneralPanel.Controls.Add(PVsystem_Combobox);
            GeneralPanel.Controls.Add(BIPV);
            GeneralPanel.Controls.Add(시스템);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(PVname_texbox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 67);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // PVtype_Combobox
            // 
            PVtype_Combobox.FormattingEnabled = true;
            PVtype_Combobox.Location = new Point(205, 37);
            PVtype_Combobox.Name = "PVtype_Combobox";
            PVtype_Combobox.Size = new Size(121, 23);
            PVtype_Combobox.TabIndex = 94;
            PVtype_Combobox.SelectedIndexChanged += Type_SelectedIndexChanged;
            // 
            // BIPVType_Combobox
            // 
            BIPVType_Combobox.FormattingEnabled = true;
            BIPVType_Combobox.Location = new Point(683, 37);
            BIPVType_Combobox.Name = "BIPVType_Combobox";
            BIPVType_Combobox.Size = new Size(121, 23);
            BIPVType_Combobox.TabIndex = 93;
            BIPVType_Combobox.Visible = false;
            BIPVType_Combobox.SelectedIndexChanged += BIPVType_SelectedIndexChanged;
            // 
            // BIPV
            // 
            BIPV.AutoSize = true;
            BIPV.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BIPV.Location = new Point(496, 38);
            BIPV.Name = "BIPV";
            BIPV.Size = new Size(63, 15);
            BIPV.TabIndex = 92;
            BIPV.Text = "BIPV 유형";
            BIPV.Visible = false;
            // 
            // 시스템
            // 
            시스템.AutoSize = true;
            시스템.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            시스템.Location = new Point(496, 9);
            시스템.Name = "시스템";
            시스템.Size = new Size(43, 15);
            시스템.TabIndex = 91;
            시스템.Text = "시스템";
            // 
            // pictureBox1
            // 
          //  pictureBox1.Image = Properties.Resources.pv;
            pictureBox1.Location = new Point(18, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 35);
            pictureBox1.TabIndex = 90;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(126, 40);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 3;
            label3.Text = "Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(126, 11);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "명칭";
            // 
            // PVname_texbox
            // 
            PVname_texbox.BackColor = SystemColors.Window;
            PVname_texbox.BorderStyle = BorderStyle.None;
            PVname_texbox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVname_texbox.Location = new Point(205, 8);
            PVname_texbox.Name = "PVname_texbox";
            PVname_texbox.Size = new Size(120, 15);
            PVname_texbox.TabIndex = 88;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(BatteryDB_button);
            panel1.Controls.Add(InverterDB_button);
            panel1.Controls.Add(PVDB_button);
            panel1.Controls.Add(Batterycapacity_s);
            panel1.Controls.Add(Batterycapacity);
            panel1.Controls.Add(InverterEfficiency);
            panel1.Controls.Add(PVEfficiency);
            panel1.Controls.Add(Batterycapacity_n);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(Batteryname);
            panel1.Controls.Add(Inverter);
            panel1.Controls.Add(PVModuleType);
            panel1.Controls.Add(Battery);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(12, 99);
            panel1.Name = "panel1";
            panel1.Size = new Size(977, 94);
            panel1.TabIndex = 19;
            panel1.Paint += panel1_Paint;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(805, 10);
            label8.Name = "label8";
            label8.Size = new Size(21, 15);
            label8.TabIndex = 130;
            label8.Text = "%";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlDark;
            label4.Location = new Point(804, 38);
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
            BatteryDB_button.Location = new Point(328, 63);
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
            InverterDB_button.Location = new Point(328, 33);
            InverterDB_button.Margin = new Padding(0);
            InverterDB_button.Name = "InverterDB_button";
            InverterDB_button.Size = new Size(23, 23);
            InverterDB_button.TabIndex = 107;
            InverterDB_button.Text = "+";
            InverterDB_button.UseVisualStyleBackColor = false;
            InverterDB_button.Click += InverterDB_button_Click;
            // 
            // PVDB_button
            // 
            PVDB_button.BackColor = SystemColors.ControlLight;
            PVDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PVDB_button.FlatStyle = FlatStyle.System;
            PVDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            PVDB_button.Location = new Point(328, 5);
            PVDB_button.Margin = new Padding(0);
            PVDB_button.Name = "PVDB_button";
            PVDB_button.Size = new Size(23, 23);
            PVDB_button.TabIndex = 89;
            PVDB_button.Text = "+";
            PVDB_button.UseVisualStyleBackColor = false;
            PVDB_button.Click += PVDB_button_Click;
            // 
            // Batterycapacity_s
            // 
            Batterycapacity_s.AutoSize = true;
            Batterycapacity_s.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity_s.ForeColor = SystemColors.ControlDark;
            Batterycapacity_s.Location = new Point(805, 66);
            Batterycapacity_s.Name = "Batterycapacity_s";
            Batterycapacity_s.Size = new Size(27, 15);
            Batterycapacity_s.TabIndex = 106;
            Batterycapacity_s.Text = "kW";
            Batterycapacity_s.Visible = false;
            // 
            // Batterycapacity
            // 
            Batterycapacity.BackColor = SystemColors.Window;
            Batterycapacity.BorderStyle = BorderStyle.None;
            Batterycapacity.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity.ForeColor = SystemColors.ScrollBar;
            Batterycapacity.Location = new Point(682, 63);
            Batterycapacity.Name = "Batterycapacity";
            Batterycapacity.ReadOnly = true;
            Batterycapacity.Size = new Size(120, 15);
            Batterycapacity.TabIndex = 105;
            Batterycapacity.TextAlign = HorizontalAlignment.Center;
            Batterycapacity.Visible = false;
            // 
            // InverterEfficiency
            // 
            InverterEfficiency.BackColor = SystemColors.Window;
            InverterEfficiency.BorderStyle = BorderStyle.None;
            InverterEfficiency.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            InverterEfficiency.ForeColor = SystemColors.ScrollBar;
            InverterEfficiency.Location = new Point(682, 34);
            InverterEfficiency.Name = "InverterEfficiency";
            InverterEfficiency.ReadOnly = true;
            InverterEfficiency.Size = new Size(120, 15);
            InverterEfficiency.TabIndex = 104;
            InverterEfficiency.TextAlign = HorizontalAlignment.Center;
            // 
            // PVEfficiency
            // 
            PVEfficiency.BackColor = SystemColors.Window;
            PVEfficiency.BorderStyle = BorderStyle.None;
            PVEfficiency.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVEfficiency.ForeColor = SystemColors.ControlDark;
            PVEfficiency.Location = new Point(682, 6);
            PVEfficiency.Name = "PVEfficiency";
            PVEfficiency.ReadOnly = true;
            PVEfficiency.Size = new Size(120, 15);
            PVEfficiency.TabIndex = 100;
            PVEfficiency.TextAlign = HorizontalAlignment.Center;
            // 
            // Batterycapacity_n
            // 
            Batterycapacity_n.AutoSize = true;
            Batterycapacity_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batterycapacity_n.ForeColor = SystemColors.ControlDark;
            Batterycapacity_n.Location = new Point(494, 65);
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
            label10.Location = new Point(494, 36);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 102;
            label10.Text = "인버터 효율";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.ControlDark;
            label11.Location = new Point(494, 8);
            label11.Name = "label11";
            label11.Size = new Size(99, 15);
            label11.TabIndex = 101;
            label11.Text = "태양광 모듈 효율";
            // 
            // Batteryname
            // 
            Batteryname.BackColor = SystemColors.Window;
            Batteryname.BorderStyle = BorderStyle.None;
            Batteryname.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batteryname.Location = new Point(203, 63);
            Batteryname.Name = "Batteryname";
            Batteryname.ReadOnly = true;
            Batteryname.Size = new Size(120, 15);
            Batteryname.TabIndex = 99;
            Batteryname.TextAlign = HorizontalAlignment.Center;
            Batteryname.Visible = false;
            // 
            // Inverter
            // 
            Inverter.BackColor = SystemColors.Window;
            Inverter.BorderStyle = BorderStyle.None;
            Inverter.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Inverter.Location = new Point(203, 34);
            Inverter.Name = "Inverter";
            Inverter.ReadOnly = true;
            Inverter.Size = new Size(120, 15);
            Inverter.TabIndex = 98;
            Inverter.TextAlign = HorizontalAlignment.Center;
            // 
            // Battery
            // 
            Battery.AutoSize = true;
            Battery.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Battery.Location = new Point(16, 65);
            Battery.Name = "Battery";
            Battery.Size = new Size(43, 15);
            Battery.TabIndex = 97;
            Battery.Text = "배터리";
            Battery.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(16, 36);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 96;
            label7.Text = "인버터";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(16, 8);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 95;
            label6.Text = "태양광 모듈";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.Controls.Add(label2);
            AdditionalPanel.Controls.Add(label26);
            AdditionalPanel.Controls.Add(distance_j);
            AdditionalPanel.Controls.Add(label25);
            AdditionalPanel.Controls.Add(obsheight_j);
            AdditionalPanel.Controls.Add(pictureBox2);
            AdditionalPanel.Controls.Add(height_n);
            AdditionalPanel.Controls.Add(width);
            AdditionalPanel.Controls.Add(height);
            AdditionalPanel.Controls.Add(orientation);
            AdditionalPanel.Controls.Add(slope);
            AdditionalPanel.Controls.Add(label21);
            AdditionalPanel.Controls.Add(label20);
            AdditionalPanel.Controls.Add(obsheight);
            AdditionalPanel.Controls.Add(distance);
            AdditionalPanel.Controls.Add(label14);
            AdditionalPanel.Controls.Add(textBox13);
            AdditionalPanel.Controls.Add(label17);
            AdditionalPanel.Controls.Add(label18);
            AdditionalPanel.Controls.Add(install);
            AdditionalPanel.Controls.Add(label19);
            AdditionalPanel.Controls.Add(width_n);
            AdditionalPanel.Controls.Add(label15);
            AdditionalPanel.Location = new Point(12, 211);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 221);
            AdditionalPanel.TabIndex = 18;
            AdditionalPanel.Paint += AdditionalPanel_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(326, 42);
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
            // distance_j
            // 
            distance_j.BackColor = SystemColors.Window;
            distance_j.BorderStyle = BorderStyle.None;
            distance_j.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            distance_j.ForeColor = SystemColors.ScrollBar;
            distance_j.Location = new Point(755, 142);
            distance_j.Name = "distance_j";
            distance_j.Size = new Size(62, 15);
            distance_j.TabIndex = 125;
            distance_j.TextAlign = HorizontalAlignment.Center;
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
            // obsheight_j
            // 
            obsheight_j.BackColor = SystemColors.Window;
            obsheight_j.BorderStyle = BorderStyle.None;
            obsheight_j.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            obsheight_j.ForeColor = SystemColors.ScrollBar;
            obsheight_j.Location = new Point(723, 78);
            obsheight_j.Name = "obsheight_j";
            obsheight_j.Size = new Size(62, 15);
            obsheight_j.TabIndex = 123;
            obsheight_j.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox2
            // 
           // pictureBox2.Image = Properties.Resources.그림1;
            pictureBox2.Location = new Point(625, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(349, 211);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 120;
            pictureBox2.TabStop = false;
            // 
            // height_n
            // 
            height_n.BackColor = SystemColors.Window;
            height_n.BorderStyle = BorderStyle.FixedSingle;
            height_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            height_n.Location = new Point(373, 9);
            height_n.Name = "height_n";
            height_n.Size = new Size(120, 22);
            height_n.TabIndex = 119;
            height_n.TextAlign = HorizontalAlignment.Center;
            height_n.TextChanged += height_n_TextChanged_1;
            // 
            // width
            // 
            width.AutoSize = true;
            width.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            width.Location = new Point(156, 11);
            width.Name = "width";
            width.Size = new Size(31, 15);
            width.TabIndex = 118;
            width.Text = "가로";
            // 
            // height
            // 
            height.AutoSize = true;
            height.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            height.ForeColor = SystemColors.ControlText;
            height.Location = new Point(330, 11);
            height.Name = "height";
            height.Size = new Size(31, 15);
            height.TabIndex = 117;
            height.Text = "세로";
            // 
            // orientation
            // 
            orientation.FormattingEnabled = true;
            orientation.Location = new Point(202, 73);
            orientation.Name = "orientation";
            orientation.Size = new Size(121, 23);
            orientation.TabIndex = 116;
            orientation.SelectedIndexChanged += orientation_SelectedIndexChanged_1;
            // 
            // slope
            // 
            slope.FormattingEnabled = true;
            slope.Location = new Point(374, 73);
            slope.Name = "slope";
            slope.Size = new Size(121, 23);
            slope.TabIndex = 95;
            slope.SelectedIndexChanged += slope_SelectedIndexChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label21.ForeColor = SystemColors.ControlText;
            label21.Location = new Point(328, 110);
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
            label20.Location = new Point(325, 139);
            label20.Name = "label20";
            label20.Size = new Size(19, 15);
            label20.TabIndex = 109;
            label20.Text = "m";
            // 
            // obsheight
            // 
            obsheight.BackColor = SystemColors.Window;
            obsheight.BorderStyle = BorderStyle.FixedSingle;
            obsheight.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            obsheight.ForeColor = SystemColors.WindowText;
            obsheight.Location = new Point(202, 136);
            obsheight.Name = "obsheight";
            obsheight.Size = new Size(120, 22);
            obsheight.TabIndex = 114;
            obsheight.TextAlign = HorizontalAlignment.Center;
            obsheight.TextChanged += obsheight_TextChanged;
            // 
            // distance
            // 
            distance.BackColor = SystemColors.Window;
            distance.BorderStyle = BorderStyle.FixedSingle;
            distance.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            distance.Location = new Point(202, 106);
            distance.Name = "distance";
            distance.Size = new Size(120, 22);
            distance.TabIndex = 111;
            distance.TextAlign = HorizontalAlignment.Center;
            distance.TextChanged += distance_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(18, 108);
            label14.Name = "label14";
            label14.Size = new Size(107, 15);
            label14.TabIndex = 111;
            label14.Text = "지형물까지의 거리";
            // 
            // textBox13
            // 
            textBox13.BackColor = SystemColors.Window;
            textBox13.BorderStyle = BorderStyle.None;
            textBox13.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox13.ForeColor = SystemColors.ScrollBar;
            textBox13.Location = new Point(203, 42);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(120, 15);
            textBox13.TabIndex = 109;
            textBox13.TextAlign = HorizontalAlignment.Center;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(18, 138);
            label17.Name = "label17";
            label17.Size = new Size(83, 15);
            label17.TabIndex = 112;
            label17.Text = "지형물의 높이";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(331, 76);
            label18.Name = "label18";
            label18.Size = new Size(31, 15);
            label18.TabIndex = 111;
            label18.Text = "경사";
            // 
            // install
            // 
            install.AutoSize = true;
            install.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            install.Location = new Point(16, 9);
            install.Name = "install";
            install.Size = new Size(85, 15);
            install.TabIndex = 109;
            install.Text = "설치 개수(EA)";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(156, 42);
            label19.Name = "label19";
            label19.Size = new Size(47, 15);
            label19.TabIndex = 110;
            label19.Text = "총 면적";
            // 
            // width_n
            // 
            width_n.BackColor = SystemColors.Window;
            width_n.BorderStyle = BorderStyle.FixedSingle;
            width_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            width_n.Location = new Point(203, 7);
            width_n.Name = "width_n";
            width_n.Size = new Size(120, 22);
            width_n.TabIndex = 109;
            width_n.TextAlign = HorizontalAlignment.Center;
            width_n.TextChanged += width_n_TextChanged_1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(18, 76);
            label15.Name = "label15";
            label15.Size = new Size(59, 15);
            label15.TabIndex = 110;
            label15.Text = "설치 방위";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(12, 81);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 94;
            label5.Text = "구성요소정보";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(12, 194);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 95;
            label13.Text = "설치정보";
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(1006, 704);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 99;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1100, 704);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 98;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            // 
            // PVTypeBox
            // 
            PVTypeBox.Location = new Point(999, 14);
            PVTypeBox.Name = "PVTypeBox";
            PVTypeBox.Size = new Size(190, 228);
            PVTypeBox.SizeMode = PictureBoxSizeMode.Zoom;
            PVTypeBox.TabIndex = 100;
            PVTypeBox.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.HighlightText;
            panel2.Controls.Add(Caculation_Button);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(matchingfacor_n);
            panel2.Controls.Add(matchingfactor);
            panel2.Controls.Add(pvgrid_s);
            panel2.Controls.Add(PVgrid);
            panel2.Controls.Add(label31);
            panel2.Controls.Add(PVusing);
            panel2.Controls.Add(label30);
            panel2.Controls.Add(PVproduction);
            panel2.Controls.Add(batteryimage);
            panel2.Controls.Add(averagecpacity);
            panel2.Controls.Add(label29);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(allcapacity);
            panel2.Controls.Add(label27);
            panel2.Controls.Add(pictureBox3);
            panel2.Location = new Point(12, 454);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 238);
            panel2.TabIndex = 123;
            panel2.Paint += panel2_Paint_1;
            // 
            // Caculation_Button
            // 
            Caculation_Button.Location = new Point(374, 11);
            Caculation_Button.Name = "Caculation_Button";
            Caculation_Button.Size = new Size(75, 23);
            Caculation_Button.TabIndex = 125;
            Caculation_Button.Text = "계산";
            Caculation_Button.UseVisualStyleBackColor = true;
            Caculation_Button.Click += Caculation_Button_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(328, 42);
            label12.Name = "label12";
            label12.Size = new Size(21, 15);
            label12.TabIndex = 138;
            label12.Text = "%";
            // 
            // matchingfacor_n
            // 
            matchingfacor_n.BackColor = SystemColors.Window;
            matchingfacor_n.BorderStyle = BorderStyle.None;
            matchingfacor_n.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            matchingfacor_n.Location = new Point(718, 199);
            matchingfacor_n.Name = "matchingfacor_n";
            matchingfacor_n.ScrollBars = ScrollBars.Vertical;
            matchingfacor_n.Size = new Size(51, 15);
            matchingfacor_n.TabIndex = 137;
            matchingfacor_n.UseWaitCursor = true;
            matchingfacor_n.Visible = false;
            // 
            // matchingfactor
            // 
            matchingfactor.AutoSize = true;
            matchingfactor.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            matchingfactor.Location = new Point(660, 199);
            matchingfactor.Name = "matchingfactor";
            matchingfactor.Size = new Size(55, 15);
            matchingfactor.TabIndex = 136;
            matchingfactor.Text = "매칭계수";
            matchingfactor.Visible = false;
            // 
            // pvgrid_s
            // 
            pvgrid_s.AutoSize = true;
            pvgrid_s.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            pvgrid_s.ForeColor = SystemColors.ControlDark;
            pvgrid_s.Location = new Point(775, 87);
            pvgrid_s.Name = "pvgrid_s";
            pvgrid_s.Size = new Size(47, 15);
            pvgrid_s.TabIndex = 135;
            pvgrid_s.Text = "kWh/a";
            pvgrid_s.Visible = false;
            // 
            // PVgrid
            // 
            PVgrid.BackColor = SystemColors.Window;
            PVgrid.BorderStyle = BorderStyle.None;
            PVgrid.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVgrid.Location = new Point(721, 85);
            PVgrid.Name = "PVgrid";
            PVgrid.ScrollBars = ScrollBars.Vertical;
            PVgrid.Size = new Size(51, 15);
            PVgrid.TabIndex = 134;
            PVgrid.UseWaitCursor = true;
            PVgrid.Visible = false;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label31.ForeColor = SystemColors.ControlDark;
            label31.Location = new Point(801, 163);
            label31.Name = "label31";
            label31.Size = new Size(47, 15);
            label31.TabIndex = 133;
            label31.Text = "kWh/a";
            // 
            // PVusing
            // 
            PVusing.BackColor = SystemColors.Window;
            PVusing.BorderStyle = BorderStyle.None;
            PVusing.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVusing.Location = new Point(747, 163);
            PVusing.Name = "PVusing";
            PVusing.ScrollBars = ScrollBars.Vertical;
            PVusing.Size = new Size(51, 15);
            PVusing.TabIndex = 132;
            PVusing.UseWaitCursor = true;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label30.ForeColor = SystemColors.ControlDark;
            label30.Location = new Point(542, 165);
            label30.Name = "label30";
            label30.Size = new Size(47, 15);
            label30.TabIndex = 131;
            label30.Text = "kWh/a";
            // 
            // PVproduction
            // 
            PVproduction.BackColor = SystemColors.Window;
            PVproduction.BorderStyle = BorderStyle.None;
            PVproduction.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVproduction.Location = new Point(488, 165);
            PVproduction.Name = "PVproduction";
            PVproduction.ScrollBars = ScrollBars.Vertical;
            PVproduction.Size = new Size(51, 15);
            PVproduction.TabIndex = 130;
            PVproduction.UseWaitCursor = true;
            // 
            // batteryimage
            // 
           // batteryimage.Image = Properties.Resources.배터리1;
            batteryimage.Location = new Point(494, 60);
            batteryimage.Name = "batteryimage";
            batteryimage.Size = new Size(71, 90);
            batteryimage.SizeMode = PictureBoxSizeMode.Zoom;
            batteryimage.TabIndex = 101;
            batteryimage.TabStop = false;
            batteryimage.Visible = false;
            // 
            // averagecpacity
            // 
            averagecpacity.BackColor = SystemColors.Window;
            averagecpacity.BorderStyle = BorderStyle.None;
            averagecpacity.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            averagecpacity.ForeColor = SystemColors.ScrollBar;
            averagecpacity.Location = new Point(202, 41);
            averagecpacity.Name = "averagecpacity";
            averagecpacity.Size = new Size(120, 15);
            averagecpacity.TabIndex = 129;
            averagecpacity.TextAlign = HorizontalAlignment.Center;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label29.Location = new Point(16, 42);
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
            label28.Location = new Point(328, 11);
            label28.Name = "label28";
            label28.Size = new Size(27, 15);
            label28.TabIndex = 109;
            label28.Text = "kW";
            // 
            // allcapacity
            // 
            allcapacity.BackColor = SystemColors.Window;
            allcapacity.BorderStyle = BorderStyle.None;
            allcapacity.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            allcapacity.ForeColor = SystemColors.ScrollBar;
            allcapacity.Location = new Point(203, 11);
            allcapacity.Name = "allcapacity";
            allcapacity.Size = new Size(119, 15);
            allcapacity.TabIndex = 127;
            allcapacity.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.Location = new Point(16, 11);
            label27.Name = "label27";
            label27.Size = new Size(59, 15);
            label27.TabIndex = 127;
            label27.Text = "설치 용량";
            // 
            // pictureBox3
            // 
           // pictureBox3.Image = Properties.Resources.그림3;
            pictureBox3.Location = new Point(369, 6);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(593, 227);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 122;
            pictureBox3.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(12, 436);
            label9.Name = "label9";
            label9.Size = new Size(117, 15);
            label9.TabIndex = 124;
            label9.Text = "태양광 계통도(연간)";
            // 
            // PV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label9);
            Controls.Add(panel2);
            Controls.Add(PVTypeBox);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)PVTypeBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)batteryimage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel AdditionalPanel;
        private Label label1;
        private TextBox PVname_texbox;
        private Label label3;
        private PictureBox pictureBox1;
        private Label BIPV;
        private Label 시스템;
        private ComboBox PVsystem_Combobox;
        private ComboBox BIPVType_Combobox;
        private Panel panel1;
        private Label label5;
        private Label label6;
        private Label Battery;
        private Label label7;
        private TextBox Batteryname;
        private TextBox Inverter;
        private TextBox PVModuleType;
        private TextBox Batterycapacity;
        private TextBox InverterEfficiency;
        private TextBox PVEfficiency;
        private Label Batterycapacity_n;
        private Label label10;
        private Label label11;
        private Label Batterycapacity_s;
        private ComboBox PVtype_Combobox;
        private Button BatteryDB_button;
        private Button InverterDB_button;
        private Button PVDB_button;
        private Label label13;
        private Label width;
        private Label height;
        private ComboBox orientation;
        private ComboBox slope;
        private Label label21;
        private Label label20;
        private TextBox obsheight;
        private TextBox distance;
        private Label label14;
        private TextBox textBox13;
        private Label label17;
        private Label label18;
        private Label install;
        private Label label19;
        private TextBox width_n;
        private Label label15;
        private TextBox height_n;
        private PictureBox pictureBox2;
        private Button Previous_button;
        private Button Save_button;
        private PictureBox PVTypeBox;
        private Label label25;
        private TextBox obsheight_j;
        private Label label26;
        private TextBox distance_j;
        private Label label2;
        private Label label4;
        private Label label8;
        private Panel panel2;
        private TextBox matchingfacor_n;
        private Label matchingfactor;
        private Label pvgrid_s;
        private TextBox PVgrid;
        private Label label31;
        private TextBox PVusing;
        private Label label30;
        private TextBox PVproduction;
        private PictureBox batteryimage;
        private TextBox averagecpacity;
        private Label label29;
        private Label label28;
        private TextBox allcapacity;
        private Label label27;
        private PictureBox pictureBox3;
        private Label label9;
        private Label label12;
        private Button Caculation_Button;
    }
}