using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace main.contents
{
    partial class ZoneGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoneGeneral));
            GeneralPanel = new Panel();
            Zone_comboBox = new System.Windows.Forms.ComboBox();
            ZoneName_textBox = new System.Windows.Forms.TextBox();
            Floor_textBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            label41 = new System.Windows.Forms.Label();
            textBox6 = new System.Windows.Forms.TextBox();
            label40 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label37 = new System.Windows.Forms.Label();
            AHU_comboBox = new System.Windows.Forms.ComboBox();
            Ventilation_checkBox = new System.Windows.Forms.CheckBox();
            Cooling_checkBox = new System.Windows.Forms.CheckBox();
            Heating_checkBox = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            panel2 = new Panel();
            label20 = new System.Windows.Forms.Label();
            PersonNum_textBox = new System.Windows.Forms.TextBox();
            OccupancyDensity_index_textBox = new System.Windows.Forms.TextBox();
            WeekUseDay_comboBox = new System.Windows.Forms.ComboBox();
            StartTime_comboBox = new System.Windows.Forms.ComboBox();
            label19 = new System.Windows.Forms.Label();
            CeilingHeight_textBox = new System.Windows.Forms.TextBox();
            label31 = new System.Windows.Forms.Label();
            EquipIHG_textBox = new System.Windows.Forms.TextBox();
            label32 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            EquipIHG_comboBox = new System.Windows.Forms.ComboBox();
            label34 = new System.Windows.Forms.Label();
            label35 = new System.Windows.Forms.Label();
            PersonIHG_textBox = new System.Windows.Forms.TextBox();
            label36 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            OccupancyDensity_textBox = new System.Windows.Forms.TextBox();
            label26 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            AnnualUseDay_textBox = new System.Windows.Forms.TextBox();
            label30 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            AHUTime_textBox = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            EndTime_comboBox = new System.Windows.Forms.ComboBox();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            UseTime_textBox = new System.Windows.Forms.TextBox();
            label22 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            HCTime_textBox = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            Usage_comboBox = new System.Windows.Forms.ComboBox();
            label25 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            DHWneed_textBox = new System.Windows.Forms.TextBox();
            label14 = new System.Windows.Forms.Label();
            label51 = new System.Windows.Forms.Label();
            textBox9 = new System.Windows.Forms.TextBox();
            label52 = new System.Windows.Forms.Label();
            label49 = new System.Windows.Forms.Label();
            textBox8 = new System.Windows.Forms.TextBox();
            label50 = new System.Windows.Forms.Label();
            label47 = new System.Windows.Forms.Label();
            textBox7 = new System.Windows.Forms.TextBox();
            label48 = new System.Windows.Forms.Label();
            label45 = new System.Windows.Forms.Label();
            textBox5 = new System.Windows.Forms.TextBox();
            label46 = new System.Windows.Forms.Label();
            label43 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            label44 = new System.Windows.Forms.Label();
            label42 = new System.Windows.Forms.Label();
            BuildingUse_comboBox = new System.Windows.Forms.ComboBox();
            label21 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            Area_textBox = new System.Windows.Forms.TextBox();
            label23 = new System.Windows.Forms.Label();
            textBox4 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            EquipIHG_image_textBox = new System.Windows.Forms.TextBox();
            DHWneed_image_textBox = new System.Windows.Forms.TextBox();
            textBox27 = new System.Windows.Forms.TextBox();
            textBox26 = new System.Windows.Forms.TextBox();
            textBox25 = new System.Windows.Forms.TextBox();
            textBox24 = new System.Windows.Forms.TextBox();
            textBox23 = new System.Windows.Forms.TextBox();
            textBox22 = new System.Windows.Forms.TextBox();
            PersonIHG_image_textBox = new System.Windows.Forms.TextBox();
            Em_textBox = new System.Windows.Forms.TextBox();
            theta_i_h_set_textBox = new System.Windows.Forms.TextBox();
            theta_i_c_set_textBox = new System.Windows.Forms.TextBox();
            EndTime_image_textBox = new System.Windows.Forms.TextBox();
            StartTime_image_textBox = new System.Windows.Forms.TextBox();
            textBox14 = new System.Windows.Forms.TextBox();
            textBox13 = new System.Windows.Forms.TextBox();
            textBox12 = new System.Windows.Forms.TextBox();
            textBox10 = new System.Windows.Forms.TextBox();
            AdditionalPanel = new Panel();
            pictureBox1 = new PictureBox();
            BuildingCategory_comboBox = new System.Windows.Forms.ComboBox();
            label53 = new System.Windows.Forms.Label();
            Save_button = new System.Windows.Forms.Button();
            label55 = new System.Windows.Forms.Label();
            label54 = new System.Windows.Forms.Label();
            NetVolume_textBox = new System.Windows.Forms.TextBox();
            label56 = new System.Windows.Forms.Label();
            label57 = new System.Windows.Forms.Label();
            Depth_textBox = new System.Windows.Forms.TextBox();
            label58 = new System.Windows.Forms.Label();
            label59 = new System.Windows.Forms.Label();
            Length_textBox = new System.Windows.Forms.TextBox();
            label60 = new System.Windows.Forms.Label();
            label61 = new System.Windows.Forms.Label();
            VentilationRate_textBox = new System.Windows.Forms.TextBox();
            label62 = new System.Windows.Forms.Label();
            label63 = new System.Windows.Forms.Label();
            VentilationVolume_textBox = new System.Windows.Forms.TextBox();
            label64 = new System.Windows.Forms.Label();
            GeneralPanel.SuspendLayout();
            panel2.SuspendLayout();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Zone_comboBox);
            GeneralPanel.Controls.Add(ZoneName_textBox);
            GeneralPanel.Controls.Add(Floor_textBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(label39);
            GeneralPanel.Controls.Add(label41);
            GeneralPanel.Controls.Add(textBox6);
            GeneralPanel.Controls.Add(label40);
            GeneralPanel.Controls.Add(label38);
            GeneralPanel.Controls.Add(textBox2);
            GeneralPanel.Controls.Add(comboBox1);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label37);
            GeneralPanel.Controls.Add(AHU_comboBox);
            GeneralPanel.Controls.Add(Ventilation_checkBox);
            GeneralPanel.Controls.Add(Cooling_checkBox);
            GeneralPanel.Controls.Add(Heating_checkBox);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Zone_comboBox
            // 
            Zone_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Zone_comboBox.FormattingEnabled = true;
            Zone_comboBox.Location = new Point(32, 52);
            Zone_comboBox.Name = "Zone_comboBox";
            Zone_comboBox.Size = new Size(120, 23);
            Zone_comboBox.TabIndex = 100;
            Zone_comboBox.SelectedIndexChanged += Zone_comboBox_SelectedIndexChanged;
            // 
            // ZoneName_textBox
            // 
            ZoneName_textBox.BackColor = SystemColors.Window;
            ZoneName_textBox.BorderStyle = BorderStyle.FixedSingle;
            ZoneName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ZoneName_textBox.Location = new Point(155, 52);
            ZoneName_textBox.Name = "ZoneName_textBox";
            ZoneName_textBox.Size = new Size(120, 22);
            ZoneName_textBox.TabIndex = 99;
            ZoneName_textBox.TextChanged += ZoneName_textBox_TextChanged;
            // 
            // Floor_textBox
            // 
            Floor_textBox.BackColor = SystemColors.Window;
            Floor_textBox.BorderStyle = BorderStyle.FixedSingle;
            Floor_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Floor_textBox.Location = new Point(155, 16);
            Floor_textBox.Name = "Floor_textBox";
            Floor_textBox.Size = new Size(120, 22);
            Floor_textBox.TabIndex = 88;
            Floor_textBox.TextChanged += Floor_textBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(130, 19);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 1;
            label1.Text = "층";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label39.Location = new Point(953, 51);
            label39.Name = "label39";
            label39.Size = new Size(21, 15);
            label39.TabIndex = 98;
            label39.Text = "%";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label41.Location = new Point(885, 16);
            label41.Name = "label41";
            label41.Size = new Size(79, 15);
            label41.TabIndex = 97;
            label41.Text = "전열교환효율";
            // 
            // textBox6
            // 
            textBox6.BackColor = SystemColors.InactiveBorder;
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.Enabled = false;
            textBox6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox6.Location = new Point(895, 47);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(58, 22);
            textBox6.TabIndex = 96;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label40.Location = new Point(856, 52);
            label40.Name = "label40";
            label40.Size = new Size(21, 15);
            label40.TabIndex = 95;
            label40.Text = "%";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label38.Location = new Point(785, 16);
            label38.Name = "label38";
            label38.Size = new Size(79, 15);
            label38.TabIndex = 92;
            label38.Text = "온도교환효율";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.InactiveBorder;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Enabled = false;
            textBox2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(795, 47);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(58, 22);
            textBox2.TabIndex = 91;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(344, 46);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(58, 23);
            comboBox1.TabIndex = 90;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(338, 19);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 89;
            label2.Text = "실 제어방식";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label37.Location = new Point(677, 19);
            label37.Name = "label37";
            label37.Size = new Size(55, 15);
            label37.TabIndex = 38;
            label37.Text = "환기방식";
            // 
            // AHU_comboBox
            // 
            AHU_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AHU_comboBox.FormattingEnabled = true;
            AHU_comboBox.Location = new Point(646, 46);
            AHU_comboBox.Name = "AHU_comboBox";
            AHU_comboBox.Size = new Size(120, 23);
            AHU_comboBox.TabIndex = 37;
            // 
            // Ventilation_checkBox
            // 
            Ventilation_checkBox.AutoSize = true;
            Ventilation_checkBox.Location = new Point(582, 51);
            Ventilation_checkBox.Name = "Ventilation_checkBox";
            Ventilation_checkBox.Size = new Size(15, 14);
            Ventilation_checkBox.TabIndex = 17;
            Ventilation_checkBox.UseVisualStyleBackColor = true;
            // 
            // Cooling_checkBox
            // 
            Cooling_checkBox.AutoSize = true;
            Cooling_checkBox.Location = new Point(510, 51);
            Cooling_checkBox.Name = "Cooling_checkBox";
            Cooling_checkBox.Size = new Size(15, 14);
            Cooling_checkBox.TabIndex = 16;
            Cooling_checkBox.UseVisualStyleBackColor = true;
            // 
            // Heating_checkBox
            // 
            Heating_checkBox.AutoSize = true;
            Heating_checkBox.Location = new Point(438, 51);
            Heating_checkBox.Name = "Heating_checkBox";
            Heating_checkBox.Size = new Size(15, 14);
            Heating_checkBox.TabIndex = 15;
            Heating_checkBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(562, 19);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 14;
            label6.Text = "기계환기";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(430, 19);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 13;
            label5.Text = "난방";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label20);
            panel2.Controls.Add(PersonNum_textBox);
            panel2.Controls.Add(OccupancyDensity_index_textBox);
            panel2.Controls.Add(WeekUseDay_comboBox);
            panel2.Controls.Add(StartTime_comboBox);
            panel2.Controls.Add(label19);
            panel2.Controls.Add(CeilingHeight_textBox);
            panel2.Controls.Add(label31);
            panel2.Controls.Add(EquipIHG_textBox);
            panel2.Controls.Add(label32);
            panel2.Controls.Add(label33);
            panel2.Controls.Add(EquipIHG_comboBox);
            panel2.Controls.Add(label34);
            panel2.Controls.Add(label35);
            panel2.Controls.Add(PersonIHG_textBox);
            panel2.Controls.Add(label36);
            panel2.Controls.Add(label24);
            panel2.Controls.Add(OccupancyDensity_textBox);
            panel2.Controls.Add(label26);
            panel2.Controls.Add(label27);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(label29);
            panel2.Controls.Add(AnnualUseDay_textBox);
            panel2.Controls.Add(label30);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(AHUTime_textBox);
            panel2.Controls.Add(label15);
            panel2.Controls.Add(EndTime_comboBox);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(label18);
            panel2.Controls.Add(UseTime_textBox);
            panel2.Controls.Add(label22);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(HCTime_textBox);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(Usage_comboBox);
            panel2.Controls.Add(label25);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(DHWneed_textBox);
            panel2.Controls.Add(label14);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 149);
            panel2.TabIndex = 18;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(468, 88);
            label20.Name = "label20";
            label20.Size = new Size(19, 15);
            label20.TabIndex = 84;
            label20.Text = "명";
            // 
            // PersonNum_textBox
            // 
            PersonNum_textBox.BackColor = SystemColors.Window;
            PersonNum_textBox.BorderStyle = BorderStyle.FixedSingle;
            PersonNum_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PersonNum_textBox.Location = new Point(342, 84);
            PersonNum_textBox.Name = "PersonNum_textBox";
            PersonNum_textBox.Size = new Size(120, 22);
            PersonNum_textBox.TabIndex = 83;
            PersonNum_textBox.TextAlign = HorizontalAlignment.Center;
            PersonNum_textBox.TextChanged += PersonNum_textBox_TextChanged;
            // 
            // OccupancyDensity_index_textBox
            // 
            OccupancyDensity_index_textBox.BackColor = SystemColors.Desktop;
            OccupancyDensity_index_textBox.BorderStyle = BorderStyle.None;
            OccupancyDensity_index_textBox.Enabled = false;
            OccupancyDensity_index_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OccupancyDensity_index_textBox.ForeColor = SystemColors.ControlDark;
            OccupancyDensity_index_textBox.Location = new Point(346, 121);
            OccupancyDensity_index_textBox.Name = "OccupancyDensity_index_textBox";
            OccupancyDensity_index_textBox.Size = new Size(120, 15);
            OccupancyDensity_index_textBox.TabIndex = 82;
            OccupancyDensity_index_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // WeekUseDay_comboBox
            // 
            WeekUseDay_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WeekUseDay_comboBox.FormattingEnabled = true;
            WeekUseDay_comboBox.Location = new Point(153, 84);
            WeekUseDay_comboBox.Name = "WeekUseDay_comboBox";
            WeekUseDay_comboBox.Size = new Size(120, 23);
            WeekUseDay_comboBox.TabIndex = 79;
            WeekUseDay_comboBox.SelectedIndexChanged += WeekUseDay_comboBox_SelectedIndexChanged;
            // 
            // StartTime_comboBox
            // 
            StartTime_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            StartTime_comboBox.FormattingEnabled = true;
            StartTime_comboBox.Location = new Point(153, 50);
            StartTime_comboBox.Name = "StartTime_comboBox";
            StartTime_comboBox.Size = new Size(120, 23);
            StartTime_comboBox.TabIndex = 78;
            StartTime_comboBox.SelectedIndexChanged += StartTime_comboBox_SelectedIndexChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(468, 20);
            label19.Name = "label19";
            label19.Size = new Size(19, 15);
            label19.TabIndex = 77;
            label19.Text = "m";
            // 
            // CeilingHeight_textBox
            // 
            CeilingHeight_textBox.BackColor = SystemColors.Window;
            CeilingHeight_textBox.BorderStyle = BorderStyle.FixedSingle;
            CeilingHeight_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CeilingHeight_textBox.Location = new Point(342, 16);
            CeilingHeight_textBox.Name = "CeilingHeight_textBox";
            CeilingHeight_textBox.Size = new Size(120, 22);
            CeilingHeight_textBox.TabIndex = 76;
            CeilingHeight_textBox.TextChanged += CeilingHeight_textBox_TextChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label31.ForeColor = SystemColors.ControlDark;
            label31.Location = new Point(914, 121);
            label31.Name = "label31";
            label31.Size = new Size(62, 15);
            label31.TabIndex = 72;
            label31.Text = "Wh/m²·d";
            // 
            // EquipIHG_textBox
            // 
            EquipIHG_textBox.BackColor = SystemColors.Desktop;
            EquipIHG_textBox.BorderStyle = BorderStyle.None;
            EquipIHG_textBox.Enabled = false;
            EquipIHG_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EquipIHG_textBox.ForeColor = SystemColors.ControlDark;
            EquipIHG_textBox.Location = new Point(798, 121);
            EquipIHG_textBox.Name = "EquipIHG_textBox";
            EquipIHG_textBox.Size = new Size(110, 15);
            EquipIHG_textBox.TabIndex = 74;
            EquipIHG_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label32.ForeColor = SystemColors.ControlDark;
            label32.Location = new Point(737, 121);
            label32.Name = "label32";
            label32.Size = new Size(55, 15);
            label32.TabIndex = 73;
            label32.Text = "기기발열";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ControlDark;
            label33.Location = new Point(279, 121);
            label33.Name = "label33";
            label33.Size = new Size(55, 15);
            label33.TabIndex = 70;
            label33.Text = "재실수준";
            // 
            // EquipIHG_comboBox
            // 
            EquipIHG_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EquipIHG_comboBox.FormattingEnabled = true;
            EquipIHG_comboBox.Location = new Point(153, 117);
            EquipIHG_comboBox.Name = "EquipIHG_comboBox";
            EquipIHG_comboBox.Size = new Size(120, 23);
            EquipIHG_comboBox.TabIndex = 69;
            EquipIHG_comboBox.SelectedIndexChanged += EquipIHG_comboBox_SelectedIndexChanged;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label34.Location = new Point(83, 121);
            label34.Name = "label34";
            label34.Size = new Size(55, 15);
            label34.TabIndex = 68;
            label34.Text = "기기발열";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label35.ForeColor = SystemColors.ControlDark;
            label35.Location = new Point(678, 121);
            label35.Name = "label35";
            label35.Size = new Size(62, 15);
            label35.TabIndex = 65;
            label35.Text = "Wh/m²·d";
            // 
            // PersonIHG_textBox
            // 
            PersonIHG_textBox.BackColor = SystemColors.Desktop;
            PersonIHG_textBox.BorderStyle = BorderStyle.None;
            PersonIHG_textBox.Enabled = false;
            PersonIHG_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PersonIHG_textBox.ForeColor = SystemColors.ControlDark;
            PersonIHG_textBox.Location = new Point(558, 121);
            PersonIHG_textBox.Name = "PersonIHG_textBox";
            PersonIHG_textBox.Size = new Size(117, 15);
            PersonIHG_textBox.TabIndex = 67;
            PersonIHG_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label36.ForeColor = SystemColors.ControlDark;
            label36.Location = new Point(498, 121);
            label36.Name = "label36";
            label36.Size = new Size(55, 15);
            label36.TabIndex = 66;
            label36.Text = "인체발열";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(922, 88);
            label24.Name = "label24";
            label24.Size = new Size(41, 15);
            label24.TabIndex = 62;
            label24.Text = "m²/인";
            // 
            // OccupancyDensity_textBox
            // 
            OccupancyDensity_textBox.BackColor = SystemColors.Desktop;
            OccupancyDensity_textBox.BorderStyle = BorderStyle.None;
            OccupancyDensity_textBox.Enabled = false;
            OccupancyDensity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OccupancyDensity_textBox.ForeColor = SystemColors.ControlDark;
            OccupancyDensity_textBox.Location = new Point(798, 88);
            OccupancyDensity_textBox.Name = "OccupancyDensity_textBox";
            OccupancyDensity_textBox.Size = new Size(120, 15);
            OccupancyDensity_textBox.TabIndex = 64;
            OccupancyDensity_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(737, 88);
            label26.Name = "label26";
            label26.Size = new Size(55, 15);
            label26.TabIndex = 63;
            label26.Text = "재실밀도";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.Location = new Point(279, 88);
            label27.Name = "label27";
            label27.Size = new Size(55, 15);
            label27.TabIndex = 60;
            label27.Text = "재실자수";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label28.Location = new Point(83, 88);
            label28.Name = "label28";
            label28.Size = new Size(55, 15);
            label28.TabIndex = 58;
            label28.Text = "주이용일";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label29.ForeColor = SystemColors.ControlDark;
            label29.Location = new Point(682, 88);
            label29.Name = "label29";
            label29.Size = new Size(34, 15);
            label29.TabIndex = 55;
            label29.Text = "days";
            // 
            // AnnualUseDay_textBox
            // 
            AnnualUseDay_textBox.BackColor = SystemColors.Desktop;
            AnnualUseDay_textBox.BorderStyle = BorderStyle.None;
            AnnualUseDay_textBox.Enabled = false;
            AnnualUseDay_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AnnualUseDay_textBox.ForeColor = SystemColors.ControlDark;
            AnnualUseDay_textBox.Location = new Point(558, 88);
            AnnualUseDay_textBox.Name = "AnnualUseDay_textBox";
            AnnualUseDay_textBox.Size = new Size(120, 15);
            AnnualUseDay_textBox.TabIndex = 57;
            AnnualUseDay_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label30.ForeColor = SystemColors.ControlDark;
            label30.Location = new Point(492, 88);
            label30.Name = "label30";
            label30.Size = new Size(67, 15);
            label30.TabIndex = 56;
            label30.Text = "연이용일수";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(922, 54);
            label12.Name = "label12";
            label12.Size = new Size(28, 15);
            label12.TabIndex = 52;
            label12.Text = "h/d";
            // 
            // AHUTime_textBox
            // 
            AHUTime_textBox.BackColor = SystemColors.Desktop;
            AHUTime_textBox.BorderStyle = BorderStyle.None;
            AHUTime_textBox.Enabled = false;
            AHUTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AHUTime_textBox.ForeColor = SystemColors.ControlDark;
            AHUTime_textBox.Location = new Point(798, 54);
            AHUTime_textBox.Name = "AHUTime_textBox";
            AHUTime_textBox.Size = new Size(120, 15);
            AHUTime_textBox.TabIndex = 54;
            AHUTime_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(737, 54);
            label15.Name = "label15";
            label15.Size = new Size(55, 15);
            label15.TabIndex = 53;
            label15.Text = "공조시간";
            // 
            // EndTime_comboBox
            // 
            EndTime_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EndTime_comboBox.FormattingEnabled = true;
            EndTime_comboBox.Location = new Point(342, 50);
            EndTime_comboBox.Name = "EndTime_comboBox";
            EndTime_comboBox.Size = new Size(120, 23);
            EndTime_comboBox.TabIndex = 51;
            EndTime_comboBox.SelectedIndexChanged += EndTime_comboBox_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(279, 54);
            label16.Name = "label16";
            label16.Size = new Size(55, 15);
            label16.TabIndex = 50;
            label16.Text = "종료시간";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(83, 54);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 48;
            label17.Text = "사용시간";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(682, 54);
            label18.Name = "label18";
            label18.Size = new Size(28, 15);
            label18.TabIndex = 45;
            label18.Text = "h/d";
            // 
            // UseTime_textBox
            // 
            UseTime_textBox.BackColor = SystemColors.Desktop;
            UseTime_textBox.BorderStyle = BorderStyle.None;
            UseTime_textBox.Enabled = false;
            UseTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UseTime_textBox.ForeColor = SystemColors.ControlDark;
            UseTime_textBox.Location = new Point(558, 54);
            UseTime_textBox.Name = "UseTime_textBox";
            UseTime_textBox.Size = new Size(120, 15);
            UseTime_textBox.TabIndex = 47;
            UseTime_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(498, 54);
            label22.Name = "label22";
            label22.Size = new Size(55, 15);
            label22.TabIndex = 46;
            label22.Text = "사용시간";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(922, 20);
            label10.Name = "label10";
            label10.Size = new Size(28, 15);
            label10.TabIndex = 42;
            label10.Text = "h/d";
            // 
            // HCTime_textBox
            // 
            HCTime_textBox.BackColor = SystemColors.Desktop;
            HCTime_textBox.BorderStyle = BorderStyle.None;
            HCTime_textBox.Enabled = false;
            HCTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            HCTime_textBox.ForeColor = SystemColors.ControlDark;
            HCTime_textBox.Location = new Point(798, 20);
            HCTime_textBox.Name = "HCTime_textBox";
            HCTime_textBox.Size = new Size(120, 15);
            HCTime_textBox.TabIndex = 44;
            HCTime_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.ControlDark;
            label11.Location = new Point(734, 20);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 43;
            label11.Text = "냉난방시간";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(279, 20);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 40;
            label7.Text = "천장고";
            // 
            // Usage_comboBox
            // 
            Usage_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Usage_comboBox.FormattingEnabled = true;
            Usage_comboBox.Location = new Point(153, 16);
            Usage_comboBox.Name = "Usage_comboBox";
            Usage_comboBox.Size = new Size(120, 23);
            Usage_comboBox.TabIndex = 36;
            Usage_comboBox.SelectedIndexChanged += Usage_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(83, 20);
            label25.Name = "label25";
            label25.Size = new Size(67, 15);
            label25.TabIndex = 32;
            label25.Text = "용도프로필";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(682, 20);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 22;
            label8.Text = "kWh/d";
            // 
            // DHWneed_textBox
            // 
            DHWneed_textBox.BackColor = SystemColors.Desktop;
            DHWneed_textBox.BorderStyle = BorderStyle.None;
            DHWneed_textBox.Enabled = false;
            DHWneed_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DHWneed_textBox.ForeColor = SystemColors.ControlDark;
            DHWneed_textBox.Location = new Point(560, 20);
            DHWneed_textBox.Name = "DHWneed_textBox";
            DHWneed_textBox.Size = new Size(116, 15);
            DHWneed_textBox.TabIndex = 29;
            DHWneed_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(492, 20);
            label14.Name = "label14";
            label14.Size = new Size(67, 15);
            label14.TabIndex = 26;
            label14.Text = "급탕요구량";
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label51.Location = new Point(152, 428);
            label51.Name = "label51";
            label51.Size = new Size(24, 15);
            label51.TabIndex = 101;
            label51.Text = "m²";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(0, 0);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 0;
            // 
            // label52
            // 
            label52.Location = new Point(0, 0);
            label52.Name = "label52";
            label52.Size = new Size(100, 23);
            label52.TabIndex = 0;
            // 
            // label49
            // 
            label49.Location = new Point(0, 0);
            label49.Name = "label49";
            label49.Size = new Size(100, 23);
            label49.TabIndex = 0;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(0, 0);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 0;
            // 
            // label50
            // 
            label50.Location = new Point(0, 0);
            label50.Name = "label50";
            label50.Size = new Size(100, 23);
            label50.TabIndex = 0;
            // 
            // label47
            // 
            label47.Location = new Point(0, 0);
            label47.Name = "label47";
            label47.Size = new Size(100, 23);
            label47.TabIndex = 0;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(0, 0);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 0;
            // 
            // label48
            // 
            label48.Location = new Point(0, 0);
            label48.Name = "label48";
            label48.Size = new Size(100, 23);
            label48.TabIndex = 0;
            // 
            // label45
            // 
            label45.Location = new Point(0, 0);
            label45.Name = "label45";
            label45.Size = new Size(100, 23);
            label45.TabIndex = 0;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(0, 0);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 0;
            // 
            // label46
            // 
            label46.Location = new Point(0, 0);
            label46.Name = "label46";
            label46.Size = new Size(100, 23);
            label46.TabIndex = 0;
            // 
            // label43
            // 
            label43.Location = new Point(0, 0);
            label43.Name = "label43";
            label43.Size = new Size(100, 23);
            label43.TabIndex = 0;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(0, 0);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 0;
            // 
            // label44
            // 
            label44.Location = new Point(0, 0);
            label44.Name = "label44";
            label44.Size = new Size(100, 23);
            label44.TabIndex = 0;
            // 
            // label42
            // 
            label42.Location = new Point(0, 0);
            label42.Name = "label42";
            label42.Size = new Size(100, 23);
            label42.TabIndex = 0;
            // 
            // BuildingUse_comboBox
            // 
            BuildingUse_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BuildingUse_comboBox.FormattingEnabled = true;
            BuildingUse_comboBox.Location = new Point(1054, 86);
            BuildingUse_comboBox.Name = "BuildingUse_comboBox";
            BuildingUse_comboBox.Size = new Size(120, 23);
            BuildingUse_comboBox.TabIndex = 85;
            BuildingUse_comboBox.SelectedIndexChanged += BuildingUse_comboBox_SelectedIndexChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label21.Location = new Point(993, 90);
            label21.Name = "label21";
            label21.Size = new Size(55, 15);
            label21.TabIndex = 84;
            label21.Text = "건물용도";
            // 
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            // 
            // Area_textBox
            // 
            Area_textBox.BackColor = SystemColors.Window;
            Area_textBox.BorderStyle = BorderStyle.FixedSingle;
            Area_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Area_textBox.Location = new Point(1057, 321);
            Area_textBox.Name = "Area_textBox";
            Area_textBox.Size = new Size(79, 22);
            Area_textBox.TabIndex = 82;
            Area_textBox.TextAlign = HorizontalAlignment.Center;
            Area_textBox.TextChanged += Area_textBox_TextChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(996, 325);
            label23.Name = "label23";
            label23.Size = new Size(55, 15);
            label23.TabIndex = 81;
            label23.Text = "바닥면적";
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.Window;
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.Location = new Point(83, 18);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(120, 22);
            textBox4.TabIndex = 87;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(437, 29);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 12;
            label4.Text = "냉방";
            // 
            // label9
            // 
            label9.Location = new Point(0, 0);
            label9.Name = "label9";
            label9.Size = new Size(100, 23);
            label9.TabIndex = 0;
            // 
            // label13
            // 
            label13.Location = new Point(0, 0);
            label13.Name = "label13";
            label13.Size = new Size(100, 23);
            label13.TabIndex = 0;
            // 
            // EquipIHG_image_textBox
            // 
            EquipIHG_image_textBox.BackColor = SystemColors.Window;
            EquipIHG_image_textBox.BorderStyle = BorderStyle.FixedSingle;
            EquipIHG_image_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            EquipIHG_image_textBox.Location = new Point(397, 210);
            EquipIHG_image_textBox.Name = "EquipIHG_image_textBox";
            EquipIHG_image_textBox.Size = new Size(69, 20);
            EquipIHG_image_textBox.TabIndex = 103;
            EquipIHG_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // DHWneed_image_textBox
            // 
            DHWneed_image_textBox.BackColor = SystemColors.Window;
            DHWneed_image_textBox.BorderStyle = BorderStyle.None;
            DHWneed_image_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            DHWneed_image_textBox.Location = new Point(342, 99);
            DHWneed_image_textBox.Name = "DHWneed_image_textBox";
            DHWneed_image_textBox.Size = new Size(65, 13);
            DHWneed_image_textBox.TabIndex = 102;
            DHWneed_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox27
            // 
            textBox27.BackColor = SystemColors.Window;
            textBox27.BorderStyle = BorderStyle.None;
            textBox27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox27.Location = new Point(851, 265);
            textBox27.Name = "textBox27";
            textBox27.Size = new Size(75, 15);
            textBox27.TabIndex = 101;
            textBox27.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox26
            // 
            textBox26.BackColor = SystemColors.Window;
            textBox26.BorderStyle = BorderStyle.None;
            textBox26.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox26.Location = new Point(851, 193);
            textBox26.Name = "textBox26";
            textBox26.Size = new Size(75, 15);
            textBox26.TabIndex = 100;
            textBox26.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox25
            // 
            textBox25.BackColor = SystemColors.Window;
            textBox25.BorderStyle = BorderStyle.None;
            textBox25.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox25.Location = new Point(851, 119);
            textBox25.Name = "textBox25";
            textBox25.Size = new Size(75, 15);
            textBox25.TabIndex = 99;
            textBox25.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox24
            // 
            textBox24.BackColor = SystemColors.Window;
            textBox24.BorderStyle = BorderStyle.None;
            textBox24.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox24.Location = new Point(757, 335);
            textBox24.Name = "textBox24";
            textBox24.Size = new Size(44, 15);
            textBox24.TabIndex = 98;
            textBox24.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox23
            // 
            textBox23.BackColor = SystemColors.Window;
            textBox23.BorderStyle = BorderStyle.None;
            textBox23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox23.Location = new Point(666, 335);
            textBox23.Name = "textBox23";
            textBox23.Size = new Size(44, 15);
            textBox23.TabIndex = 97;
            textBox23.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox22
            // 
            textBox22.BackColor = SystemColors.Window;
            textBox22.BorderStyle = BorderStyle.FixedSingle;
            textBox22.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox22.Location = new Point(527, 210);
            textBox22.Name = "textBox22";
            textBox22.Size = new Size(49, 22);
            textBox22.TabIndex = 96;
            textBox22.TextAlign = HorizontalAlignment.Center;
            // 
            // PersonIHG_image_textBox
            // 
            PersonIHG_image_textBox.BackColor = SystemColors.Window;
            PersonIHG_image_textBox.BorderStyle = BorderStyle.FixedSingle;
            PersonIHG_image_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            PersonIHG_image_textBox.Location = new Point(293, 210);
            PersonIHG_image_textBox.Name = "PersonIHG_image_textBox";
            PersonIHG_image_textBox.Size = new Size(69, 20);
            PersonIHG_image_textBox.TabIndex = 94;
            PersonIHG_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Em_textBox
            // 
            Em_textBox.BackColor = SystemColors.Window;
            Em_textBox.BorderStyle = BorderStyle.FixedSingle;
            Em_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            Em_textBox.Location = new Point(192, 210);
            Em_textBox.Name = "Em_textBox";
            Em_textBox.Size = new Size(49, 20);
            Em_textBox.TabIndex = 93;
            Em_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // theta_i_h_set_textBox
            // 
            theta_i_h_set_textBox.BackColor = SystemColors.Window;
            theta_i_h_set_textBox.BorderStyle = BorderStyle.FixedSingle;
            theta_i_h_set_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            theta_i_h_set_textBox.Location = new Point(247, 154);
            theta_i_h_set_textBox.Name = "theta_i_h_set_textBox";
            theta_i_h_set_textBox.Size = new Size(37, 20);
            theta_i_h_set_textBox.TabIndex = 92;
            theta_i_h_set_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // theta_i_c_set_textBox
            // 
            theta_i_c_set_textBox.BackColor = SystemColors.Window;
            theta_i_c_set_textBox.BorderStyle = BorderStyle.FixedSingle;
            theta_i_c_set_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            theta_i_c_set_textBox.Location = new Point(247, 128);
            theta_i_c_set_textBox.Name = "theta_i_c_set_textBox";
            theta_i_c_set_textBox.Size = new Size(39, 20);
            theta_i_c_set_textBox.TabIndex = 91;
            theta_i_c_set_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // EndTime_image_textBox
            // 
            EndTime_image_textBox.BackColor = SystemColors.Window;
            EndTime_image_textBox.BorderStyle = BorderStyle.FixedSingle;
            EndTime_image_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            EndTime_image_textBox.Location = new Point(247, 92);
            EndTime_image_textBox.Name = "EndTime_image_textBox";
            EndTime_image_textBox.Size = new Size(39, 20);
            EndTime_image_textBox.TabIndex = 90;
            EndTime_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // StartTime_image_textBox
            // 
            StartTime_image_textBox.BackColor = SystemColors.Window;
            StartTime_image_textBox.BorderStyle = BorderStyle.FixedSingle;
            StartTime_image_textBox.Font = new Font("나눔고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            StartTime_image_textBox.Location = new Point(192, 92);
            StartTime_image_textBox.Name = "StartTime_image_textBox";
            StartTime_image_textBox.Size = new Size(49, 20);
            StartTime_image_textBox.TabIndex = 89;
            StartTime_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox14
            // 
            textBox14.BackColor = SystemColors.Window;
            textBox14.BorderStyle = BorderStyle.None;
            textBox14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox14.Location = new Point(47, 275);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(74, 15);
            textBox14.TabIndex = 88;
            textBox14.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox13
            // 
            textBox13.BackColor = SystemColors.Window;
            textBox13.BorderStyle = BorderStyle.None;
            textBox13.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox13.Location = new Point(46, 224);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(74, 15);
            textBox13.TabIndex = 87;
            textBox13.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox12
            // 
            textBox12.BackColor = SystemColors.Window;
            textBox12.BorderStyle = BorderStyle.None;
            textBox12.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox12.Location = new Point(46, 149);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(74, 15);
            textBox12.TabIndex = 86;
            textBox12.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.Window;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox10.Location = new Point(46, 45);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(74, 15);
            textBox10.TabIndex = 85;
            textBox10.TextAlign = HorizontalAlignment.Center;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(EquipIHG_image_textBox);
            AdditionalPanel.Controls.Add(DHWneed_image_textBox);
            AdditionalPanel.Controls.Add(textBox27);
            AdditionalPanel.Controls.Add(textBox26);
            AdditionalPanel.Controls.Add(textBox25);
            AdditionalPanel.Controls.Add(textBox24);
            AdditionalPanel.Controls.Add(textBox23);
            AdditionalPanel.Controls.Add(textBox22);
            AdditionalPanel.Controls.Add(PersonIHG_image_textBox);
            AdditionalPanel.Controls.Add(Em_textBox);
            AdditionalPanel.Controls.Add(theta_i_h_set_textBox);
            AdditionalPanel.Controls.Add(theta_i_c_set_textBox);
            AdditionalPanel.Controls.Add(EndTime_image_textBox);
            AdditionalPanel.Controls.Add(StartTime_image_textBox);
            AdditionalPanel.Controls.Add(textBox14);
            AdditionalPanel.Controls.Add(textBox13);
            AdditionalPanel.Controls.Add(textBox12);
            AdditionalPanel.Controls.Add(textBox10);
            AdditionalPanel.Controls.Add(pictureBox1);
            AdditionalPanel.Location = new Point(12, 303);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 390);
            AdditionalPanel.TabIndex = 18;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(9, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(953, 370);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 104;
            pictureBox1.TabStop = false;
            // 
            // BuildingCategory_comboBox
            // 
            BuildingCategory_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BuildingCategory_comboBox.FormattingEnabled = true;
            BuildingCategory_comboBox.Location = new Point(1054, 50);
            BuildingCategory_comboBox.Name = "BuildingCategory_comboBox";
            BuildingCategory_comboBox.Size = new Size(120, 23);
            BuildingCategory_comboBox.TabIndex = 87;
            BuildingCategory_comboBox.SelectedIndexChanged += BuildingCategory_comboBox_SelectedIndexChanged;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label53.Location = new Point(993, 54);
            label53.Name = "label53";
            label53.Size = new Size(55, 15);
            label53.TabIndex = 86;
            label53.Text = "건물대상";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1031, 653);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 88;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = false;
            Save_button.Click += Save_button_Click;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label55.ForeColor = SystemColors.ControlDark;
            label55.Location = new Point(1142, 325);
            label55.Name = "label55";
            label55.Size = new Size(24, 15);
            label55.TabIndex = 91;
            label55.Text = "m²";
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label54.ForeColor = SystemColors.ControlDark;
            label54.Location = new Point(1142, 359);
            label54.Name = "label54";
            label54.Size = new Size(25, 15);
            label54.TabIndex = 94;
            label54.Text = "m³";
            // 
            // NetVolume_textBox
            // 
            NetVolume_textBox.BackColor = SystemColors.InactiveBorder;
            NetVolume_textBox.BorderStyle = BorderStyle.None;
            NetVolume_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            NetVolume_textBox.ForeColor = SystemColors.ControlDark;
            NetVolume_textBox.Location = new Point(1057, 355);
            NetVolume_textBox.Name = "NetVolume_textBox";
            NetVolume_textBox.Size = new Size(79, 15);
            NetVolume_textBox.TabIndex = 93;
            NetVolume_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label56.Location = new Point(996, 359);
            label56.Name = "label56";
            label56.Size = new Size(43, 15);
            label56.TabIndex = 92;
            label56.Text = "순체적";
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label57.ForeColor = SystemColors.ControlDark;
            label57.Location = new Point(1142, 297);
            label57.Name = "label57";
            label57.Size = new Size(19, 15);
            label57.TabIndex = 97;
            label57.Text = "m";
            // 
            // Depth_textBox
            // 
            Depth_textBox.BackColor = SystemColors.Window;
            Depth_textBox.BorderStyle = BorderStyle.FixedSingle;
            Depth_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Depth_textBox.Location = new Point(1057, 293);
            Depth_textBox.Name = "Depth_textBox";
            Depth_textBox.Size = new Size(79, 22);
            Depth_textBox.TabIndex = 96;
            Depth_textBox.TextAlign = HorizontalAlignment.Center;
            Depth_textBox.TextChanged += Depth_textBox_TextChanged;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label58.Location = new Point(996, 297);
            label58.Name = "label58";
            label58.Size = new Size(31, 15);
            label58.TabIndex = 95;
            label58.Text = "깊이";
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label59.ForeColor = SystemColors.ControlDark;
            label59.Location = new Point(1142, 269);
            label59.Name = "label59";
            label59.Size = new Size(19, 15);
            label59.TabIndex = 100;
            label59.Text = "m";
            // 
            // Length_textBox
            // 
            Length_textBox.BackColor = SystemColors.Window;
            Length_textBox.BorderStyle = BorderStyle.FixedSingle;
            Length_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Length_textBox.Location = new Point(1057, 265);
            Length_textBox.Name = "Length_textBox";
            Length_textBox.Size = new Size(79, 22);
            Length_textBox.TabIndex = 99;
            Length_textBox.TextAlign = HorizontalAlignment.Center;
            Length_textBox.TextChanged += Length_textBox_TextChanged;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label60.Location = new Point(996, 269);
            label60.Name = "label60";
            label60.Size = new Size(31, 15);
            label60.TabIndex = 98;
            label60.Text = "길이";
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label61.ForeColor = SystemColors.ControlDark;
            label61.Location = new Point(1142, 387);
            label61.Name = "label61";
            label61.Size = new Size(28, 15);
            label61.TabIndex = 103;
            label61.Text = "1/h";
            // 
            // VentilationRate_textBox
            // 
            VentilationRate_textBox.BackColor = SystemColors.InactiveBorder;
            VentilationRate_textBox.BorderStyle = BorderStyle.None;
            VentilationRate_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            VentilationRate_textBox.ForeColor = SystemColors.ControlDark;
            VentilationRate_textBox.Location = new Point(1057, 383);
            VentilationRate_textBox.Name = "VentilationRate_textBox";
            VentilationRate_textBox.Size = new Size(79, 15);
            VentilationRate_textBox.TabIndex = 102;
            VentilationRate_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label62.Location = new Point(996, 387);
            label62.Name = "label62";
            label62.Size = new Size(55, 15);
            label62.TabIndex = 101;
            label62.Text = "환기횟수";
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label63.ForeColor = SystemColors.ControlDark;
            label63.Location = new Point(1142, 415);
            label63.Name = "label63";
            label63.Size = new Size(38, 15);
            label63.TabIndex = 106;
            label63.Text = "m³/h";
            // 
            // VentilationVolume_textBox
            // 
            VentilationVolume_textBox.BackColor = SystemColors.InactiveBorder;
            VentilationVolume_textBox.BorderStyle = BorderStyle.None;
            VentilationVolume_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            VentilationVolume_textBox.ForeColor = SystemColors.ControlDark;
            VentilationVolume_textBox.Location = new Point(1057, 411);
            VentilationVolume_textBox.Name = "VentilationVolume_textBox";
            VentilationVolume_textBox.Size = new Size(79, 15);
            VentilationVolume_textBox.TabIndex = 105;
            VentilationVolume_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label64.Location = new Point(996, 415);
            label64.Name = "label64";
            label64.Size = new Size(43, 15);
            label64.TabIndex = 104;
            label64.Text = "환기량";
            // 
            // ZoneGeneral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label63);
            Controls.Add(VentilationVolume_textBox);
            Controls.Add(label64);
            Controls.Add(label61);
            Controls.Add(VentilationRate_textBox);
            Controls.Add(label62);
            Controls.Add(label59);
            Controls.Add(Length_textBox);
            Controls.Add(label60);
            Controls.Add(label57);
            Controls.Add(Depth_textBox);
            Controls.Add(label58);
            Controls.Add(label54);
            Controls.Add(NetVolume_textBox);
            Controls.Add(label56);
            Controls.Add(label55);
            Controls.Add(Save_button);
            Controls.Add(BuildingCategory_comboBox);
            Controls.Add(label53);
            Controls.Add(panel2);
            Controls.Add(BuildingUse_comboBox);
            Controls.Add(label21);
            Controls.Add(Area_textBox);
            Controls.Add(label23);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneGeneral";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Floor_textBox;
        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox DHWneed_textBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox Heating_checkBox;
        private System.Windows.Forms.CheckBox Ventilation_checkBox;
        private System.Windows.Forms.CheckBox Cooling_checkBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox EquipIHG_textBox;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox EquipIHG_comboBox;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox PersonIHG_textBox;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox OccupancyDensity_textBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox AnnualUseDay_textBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox AHUTime_textBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox EndTime_comboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox UseTime_textBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox HCTime_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Usage_comboBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox AHU_comboBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox CeilingHeight_textBox;
        private System.Windows.Forms.ComboBox StartTime_comboBox;
        private System.Windows.Forms.ComboBox WeekUseDay_comboBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox Area_textBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox BuildingUse_comboBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox PersonNum_textBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox DHWneed_image_textBox;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox PersonIHG_image_textBox;
        private System.Windows.Forms.TextBox Em_textBox;
        private System.Windows.Forms.TextBox theta_i_h_set_textBox;
        private System.Windows.Forms.TextBox theta_i_c_set_textBox;
        private System.Windows.Forms.TextBox EndTime_image_textBox;
        private System.Windows.Forms.TextBox StartTime_image_textBox;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox EquipIHG_image_textBox;
        private System.Windows.Forms.TextBox ZoneName_textBox;
        private System.Windows.Forms.TextBox OccupancyDensity_index_textBox;
        private PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox BuildingCategory_comboBox;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox NetVolume_textBox;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox Depth_textBox;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox Length_textBox;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox VentilationRate_textBox;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox VentilationVolume_textBox;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.ComboBox Zone_comboBox;
    }
}