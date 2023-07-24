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
            GeneralPanel = new Panel();
            label65 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            Num_textBox = new System.Windows.Forms.TextBox();
            Icon_pictureBox = new PictureBox();
            ZoneName_textBox = new System.Windows.Forms.TextBox();
            Layer_textBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            η2_label2 = new System.Windows.Forms.Label();
            η2_label = new System.Windows.Forms.Label();
            η2_textBox = new System.Windows.Forms.TextBox();
            η_label2 = new System.Windows.Forms.Label();
            η_label = new System.Windows.Forms.Label();
            η_textBox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            AHU_label = new System.Windows.Forms.Label();
            AHU_comboBox = new System.Windows.Forms.ComboBox();
            Ventilation_checkBox = new System.Windows.Forms.CheckBox();
            Cooling_checkBox = new System.Windows.Forms.CheckBox();
            Heating_checkBox = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            RoomControl_comboBox = new System.Windows.Forms.ComboBox();
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
            label54 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            NetVolume_textBox = new System.Windows.Forms.TextBox();
            label56 = new System.Windows.Forms.Label();
            EquipIHG_comboBox = new System.Windows.Forms.ComboBox();
            label34 = new System.Windows.Forms.Label();
            label55 = new System.Windows.Forms.Label();
            label35 = new System.Windows.Forms.Label();
            PersonIHG_textBox = new System.Windows.Forms.TextBox();
            label36 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            OccupancyDensity_textBox = new System.Windows.Forms.TextBox();
            NetArea_textBox = new System.Windows.Forms.TextBox();
            label23 = new System.Windows.Forms.Label();
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
            label21 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            textBox4 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            EquipIHG_image_textBox = new System.Windows.Forms.TextBox();
            DHWneed_image_textBox = new System.Windows.Forms.TextBox();
            Door_textBox = new System.Windows.Forms.TextBox();
            InWall_textBox = new System.Windows.Forms.TextBox();
            CW_textBox = new System.Windows.Forms.TextBox();
            PersonIHG_image_textBox = new System.Windows.Forms.TextBox();
            Em_textBox = new System.Windows.Forms.TextBox();
            theta_i_h_set_textBox = new System.Windows.Forms.TextBox();
            theta_i_c_set_textBox = new System.Windows.Forms.TextBox();
            EndTime_image_textBox = new System.Windows.Forms.TextBox();
            StartTime_image_textBox = new System.Windows.Forms.TextBox();
            Floor_textBox = new System.Windows.Forms.TextBox();
            Window_textBox = new System.Windows.Forms.TextBox();
            Wall_textBox = new System.Windows.Forms.TextBox();
            Roof_textBox = new System.Windows.Forms.TextBox();
            AdditionalPanel = new Panel();
            SA_Volume_textBox = new System.Windows.Forms.TextBox();
            RA_Volume_textBox = new System.Windows.Forms.TextBox();
            label72 = new System.Windows.Forms.Label();
            label71 = new System.Windows.Forms.Label();
            label70 = new System.Windows.Forms.Label();
            label69 = new System.Windows.Forms.Label();
            label68 = new System.Windows.Forms.Label();
            label67 = new System.Windows.Forms.Label();
            label66 = new System.Windows.Forms.Label();
            AHU_pictureBox = new PictureBox();
            HC_pictureBox = new PictureBox();
            Ground_pictureBox = new PictureBox();
            RoomControl_pictureBox = new PictureBox();
            Main_pictureBox = new PictureBox();
            label53 = new System.Windows.Forms.Label();
            Save_button = new System.Windows.Forms.Button();
            label61 = new System.Windows.Forms.Label();
            VentilationRate_textBox = new System.Windows.Forms.TextBox();
            label62 = new System.Windows.Forms.Label();
            label63 = new System.Windows.Forms.Label();
            Volume_wd_textBox = new System.Windows.Forms.TextBox();
            label64 = new System.Windows.Forms.Label();
            BuildingCategory_textBox = new System.Windows.Forms.TextBox();
            BuildingUse_textBox = new System.Windows.Forms.TextBox();
            label73 = new System.Windows.Forms.Label();
            Ground_textBox = new System.Windows.Forms.TextBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel2.SuspendLayout();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AHU_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HC_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Ground_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RoomControl_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Main_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label65);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(ZoneName_textBox);
            GeneralPanel.Controls.Add(Layer_textBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(η2_label2);
            GeneralPanel.Controls.Add(η2_label);
            GeneralPanel.Controls.Add(η2_textBox);
            GeneralPanel.Controls.Add(η_label2);
            GeneralPanel.Controls.Add(η_label);
            GeneralPanel.Controls.Add(η_textBox);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(AHU_label);
            GeneralPanel.Controls.Add(AHU_comboBox);
            GeneralPanel.Controls.Add(Ventilation_checkBox);
            GeneralPanel.Controls.Add(Cooling_checkBox);
            GeneralPanel.Controls.Add(Heating_checkBox);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(RoomControl_comboBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label65.Location = new Point(541, 22);
            label65.Name = "label65";
            label65.Size = new Size(31, 15);
            label65.TabIndex = 103;
            label65.Text = "냉방";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(101, 54);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 102;
            label3.Text = "명칭";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(6, 76);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(90, 15);
            Num_textBox.TabIndex = 100;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(6, 18);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 101;
            Icon_pictureBox.TabStop = false;
            // 
            // ZoneName_textBox
            // 
            ZoneName_textBox.BackColor = SystemColors.Window;
            ZoneName_textBox.BorderStyle = BorderStyle.FixedSingle;
            ZoneName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ZoneName_textBox.Location = new Point(153, 50);
            ZoneName_textBox.Name = "ZoneName_textBox";
            ZoneName_textBox.Size = new Size(120, 22);
            ZoneName_textBox.TabIndex = 99;
            ZoneName_textBox.TextAlign = HorizontalAlignment.Center;
            ZoneName_textBox.TextChanged += ZoneName_textBox_TextChanged;
            // 
            // Layer_textBox
            // 
            Layer_textBox.BackColor = SystemColors.Window;
            Layer_textBox.BorderStyle = BorderStyle.None;
            Layer_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Layer_textBox.ForeColor = SystemColors.WindowText;
            Layer_textBox.Location = new Point(153, 22);
            Layer_textBox.Name = "Layer_textBox";
            Layer_textBox.Size = new Size(120, 15);
            Layer_textBox.TabIndex = 88;
            Layer_textBox.TextAlign = HorizontalAlignment.Center;
            Layer_textBox.TextChanged += Floor_textBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(107, 22);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 1;
            label1.Text = "층";
            // 
            // η2_label2
            // 
            η2_label2.AutoSize = true;
            η2_label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η2_label2.Location = new Point(953, 54);
            η2_label2.Name = "η2_label2";
            η2_label2.Size = new Size(21, 15);
            η2_label2.TabIndex = 98;
            η2_label2.Text = "%";
            // 
            // η2_label
            // 
            η2_label.AutoSize = true;
            η2_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η2_label.Location = new Point(885, 22);
            η2_label.Name = "η2_label";
            η2_label.Size = new Size(79, 15);
            η2_label.TabIndex = 97;
            η2_label.Text = "전열교환효율";
            // 
            // η2_textBox
            // 
            η2_textBox.BackColor = Color.White;
            η2_textBox.BorderStyle = BorderStyle.FixedSingle;
            η2_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η2_textBox.Location = new Point(895, 50);
            η2_textBox.Name = "η2_textBox";
            η2_textBox.Size = new Size(58, 22);
            η2_textBox.TabIndex = 96;
            η2_textBox.TextChanged += η2_textBox_TextChanged;
            // 
            // η_label2
            // 
            η_label2.AutoSize = true;
            η_label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η_label2.Location = new Point(856, 54);
            η_label2.Name = "η_label2";
            η_label2.Size = new Size(21, 15);
            η_label2.TabIndex = 95;
            η_label2.Text = "%";
            // 
            // η_label
            // 
            η_label.AutoSize = true;
            η_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η_label.Location = new Point(785, 22);
            η_label.Name = "η_label";
            η_label.Size = new Size(79, 15);
            η_label.TabIndex = 92;
            η_label.Text = "온도교환효율";
            // 
            // η_textBox
            // 
            η_textBox.BackColor = Color.White;
            η_textBox.BorderStyle = BorderStyle.FixedSingle;
            η_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            η_textBox.Location = new Point(795, 50);
            η_textBox.Name = "η_textBox";
            η_textBox.Size = new Size(58, 22);
            η_textBox.TabIndex = 91;
            η_textBox.TextChanged += η_textBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(338, 22);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 89;
            label2.Text = "실 제어방식";
            // 
            // AHU_label
            // 
            AHU_label.AutoSize = true;
            AHU_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AHU_label.Location = new Point(677, 22);
            AHU_label.Name = "AHU_label";
            AHU_label.Size = new Size(55, 15);
            AHU_label.TabIndex = 38;
            AHU_label.Text = "환기방식";
            // 
            // AHU_comboBox
            // 
            AHU_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AHU_comboBox.FormattingEnabled = true;
            AHU_comboBox.Location = new Point(646, 50);
            AHU_comboBox.Name = "AHU_comboBox";
            AHU_comboBox.Size = new Size(120, 23);
            AHU_comboBox.TabIndex = 37;
            AHU_comboBox.SelectedIndexChanged += AHU_comboBox_SelectedIndexChanged;
            // 
            // Ventilation_checkBox
            // 
            Ventilation_checkBox.AutoSize = true;
            Ventilation_checkBox.Location = new Point(603, 54);
            Ventilation_checkBox.Name = "Ventilation_checkBox";
            Ventilation_checkBox.Size = new Size(15, 14);
            Ventilation_checkBox.TabIndex = 17;
            Ventilation_checkBox.UseVisualStyleBackColor = true;
            Ventilation_checkBox.CheckedChanged += Ventilation_checkBox_CheckedChanged;
            // 
            // Cooling_checkBox
            // 
            Cooling_checkBox.AutoSize = true;
            Cooling_checkBox.Location = new Point(549, 54);
            Cooling_checkBox.Name = "Cooling_checkBox";
            Cooling_checkBox.Size = new Size(15, 14);
            Cooling_checkBox.TabIndex = 16;
            Cooling_checkBox.UseVisualStyleBackColor = true;
            Cooling_checkBox.CheckedChanged += Cooling_checkBox_CheckedChanged;
            // 
            // Heating_checkBox
            // 
            Heating_checkBox.AutoSize = true;
            Heating_checkBox.Location = new Point(495, 54);
            Heating_checkBox.Name = "Heating_checkBox";
            Heating_checkBox.Size = new Size(15, 14);
            Heating_checkBox.TabIndex = 15;
            Heating_checkBox.UseVisualStyleBackColor = true;
            Heating_checkBox.CheckedChanged += Heating_checkBox_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(583, 22);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 14;
            label6.Text = "기계환기";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(487, 22);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 13;
            label5.Text = "난방";
            // 
            // RoomControl_comboBox
            // 
            RoomControl_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            RoomControl_comboBox.FormattingEnabled = true;
            RoomControl_comboBox.Location = new Point(344, 50);
            RoomControl_comboBox.Name = "RoomControl_comboBox";
            RoomControl_comboBox.Size = new Size(120, 23);
            RoomControl_comboBox.TabIndex = 90;
            RoomControl_comboBox.SelectedIndexChanged += RoomControl_comboBox_SelectedIndexChanged;
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
            panel2.Controls.Add(label54);
            panel2.Controls.Add(label33);
            panel2.Controls.Add(NetVolume_textBox);
            panel2.Controls.Add(label56);
            panel2.Controls.Add(EquipIHG_comboBox);
            panel2.Controls.Add(label34);
            panel2.Controls.Add(label55);
            panel2.Controls.Add(label35);
            panel2.Controls.Add(PersonIHG_textBox);
            panel2.Controls.Add(label36);
            panel2.Controls.Add(label24);
            panel2.Controls.Add(OccupancyDensity_textBox);
            panel2.Controls.Add(NetArea_textBox);
            panel2.Controls.Add(label23);
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
            panel2.Size = new Size(977, 191);
            panel2.TabIndex = 18;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(468, 116);
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
            PersonNum_textBox.Location = new Point(342, 112);
            PersonNum_textBox.Name = "PersonNum_textBox";
            PersonNum_textBox.Size = new Size(120, 22);
            PersonNum_textBox.TabIndex = 83;
            PersonNum_textBox.TextAlign = HorizontalAlignment.Center;
            PersonNum_textBox.TextChanged += PersonNum_textBox_TextChanged;
            // 
            // OccupancyDensity_index_textBox
            // 
            OccupancyDensity_index_textBox.BackColor = Color.White;
            OccupancyDensity_index_textBox.BorderStyle = BorderStyle.None;
            OccupancyDensity_index_textBox.Enabled = false;
            OccupancyDensity_index_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OccupancyDensity_index_textBox.ForeColor = SystemColors.ControlDark;
            OccupancyDensity_index_textBox.Location = new Point(346, 149);
            OccupancyDensity_index_textBox.Name = "OccupancyDensity_index_textBox";
            OccupancyDensity_index_textBox.Size = new Size(120, 15);
            OccupancyDensity_index_textBox.TabIndex = 82;
            OccupancyDensity_index_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // WeekUseDay_comboBox
            // 
            WeekUseDay_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WeekUseDay_comboBox.FormattingEnabled = true;
            WeekUseDay_comboBox.Location = new Point(153, 112);
            WeekUseDay_comboBox.Name = "WeekUseDay_comboBox";
            WeekUseDay_comboBox.Size = new Size(120, 23);
            WeekUseDay_comboBox.TabIndex = 79;
            WeekUseDay_comboBox.SelectedIndexChanged += WeekUseDay_comboBox_SelectedIndexChanged;
            // 
            // StartTime_comboBox
            // 
            StartTime_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            StartTime_comboBox.FormattingEnabled = true;
            StartTime_comboBox.Location = new Point(153, 78);
            StartTime_comboBox.Name = "StartTime_comboBox";
            StartTime_comboBox.Size = new Size(120, 23);
            StartTime_comboBox.TabIndex = 78;
            StartTime_comboBox.SelectedIndexChanged += StartTime_comboBox_SelectedIndexChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(468, 18);
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
            CeilingHeight_textBox.Location = new Point(342, 14);
            CeilingHeight_textBox.Name = "CeilingHeight_textBox";
            CeilingHeight_textBox.Size = new Size(120, 22);
            CeilingHeight_textBox.TabIndex = 76;
            CeilingHeight_textBox.TextAlign = HorizontalAlignment.Center;
            CeilingHeight_textBox.TextChanged += CeilingHeight_textBox_TextChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label31.ForeColor = SystemColors.ControlDark;
            label31.Location = new Point(914, 149);
            label31.Name = "label31";
            label31.Size = new Size(62, 15);
            label31.TabIndex = 72;
            label31.Text = "Wh/m²·d";
            // 
            // EquipIHG_textBox
            // 
            EquipIHG_textBox.BackColor = Color.White;
            EquipIHG_textBox.BorderStyle = BorderStyle.None;
            EquipIHG_textBox.Enabled = false;
            EquipIHG_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EquipIHG_textBox.ForeColor = SystemColors.ControlDark;
            EquipIHG_textBox.Location = new Point(798, 149);
            EquipIHG_textBox.Name = "EquipIHG_textBox";
            EquipIHG_textBox.Size = new Size(110, 15);
            EquipIHG_textBox.TabIndex = 74;
            EquipIHG_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label32.ForeColor = SystemColors.ControlDark;
            label32.Location = new Point(737, 149);
            label32.Name = "label32";
            label32.Size = new Size(55, 15);
            label32.TabIndex = 73;
            label32.Text = "기기발열";
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label54.ForeColor = SystemColors.ControlDark;
            label54.Location = new Point(682, 18);
            label54.Name = "label54";
            label54.Size = new Size(25, 15);
            label54.TabIndex = 94;
            label54.Text = "m³";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ControlDark;
            label33.Location = new Point(286, 149);
            label33.Name = "label33";
            label33.Size = new Size(55, 15);
            label33.TabIndex = 70;
            label33.Text = "재실수준";
            // 
            // NetVolume_textBox
            // 
            NetVolume_textBox.BackColor = Color.White;
            NetVolume_textBox.BorderStyle = BorderStyle.None;
            NetVolume_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            NetVolume_textBox.ForeColor = SystemColors.ControlDark;
            NetVolume_textBox.Location = new Point(560, 18);
            NetVolume_textBox.Name = "NetVolume_textBox";
            NetVolume_textBox.Size = new Size(116, 15);
            NetVolume_textBox.TabIndex = 93;
            NetVolume_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label56.ForeColor = SystemColors.ControlDark;
            label56.Location = new Point(495, 18);
            label56.Name = "label56";
            label56.Size = new Size(43, 15);
            label56.TabIndex = 92;
            label56.Text = "순체적";
            // 
            // EquipIHG_comboBox
            // 
            EquipIHG_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EquipIHG_comboBox.FormattingEnabled = true;
            EquipIHG_comboBox.Location = new Point(153, 145);
            EquipIHG_comboBox.Name = "EquipIHG_comboBox";
            EquipIHG_comboBox.Size = new Size(120, 23);
            EquipIHG_comboBox.TabIndex = 69;
            EquipIHG_comboBox.SelectedIndexChanged += EquipIHG_comboBox_SelectedIndexChanged;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label34.Location = new Point(83, 149);
            label34.Name = "label34";
            label34.Size = new Size(55, 15);
            label34.TabIndex = 68;
            label34.Text = "기기발열";
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label55.ForeColor = SystemColors.ControlText;
            label55.Location = new Point(276, 18);
            label55.Name = "label55";
            label55.Size = new Size(24, 15);
            label55.TabIndex = 91;
            label55.Text = "m²";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label35.ForeColor = SystemColors.ControlDark;
            label35.Location = new Point(678, 149);
            label35.Name = "label35";
            label35.Size = new Size(62, 15);
            label35.TabIndex = 65;
            label35.Text = "Wh/m²·d";
            // 
            // PersonIHG_textBox
            // 
            PersonIHG_textBox.BackColor = Color.White;
            PersonIHG_textBox.BorderStyle = BorderStyle.None;
            PersonIHG_textBox.Enabled = false;
            PersonIHG_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PersonIHG_textBox.ForeColor = SystemColors.ControlDark;
            PersonIHG_textBox.Location = new Point(558, 149);
            PersonIHG_textBox.Name = "PersonIHG_textBox";
            PersonIHG_textBox.Size = new Size(117, 15);
            PersonIHG_textBox.TabIndex = 67;
            PersonIHG_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label36.ForeColor = SystemColors.ControlDark;
            label36.Location = new Point(495, 149);
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
            label24.Location = new Point(922, 116);
            label24.Name = "label24";
            label24.Size = new Size(41, 15);
            label24.TabIndex = 62;
            label24.Text = "m²/인";
            // 
            // OccupancyDensity_textBox
            // 
            OccupancyDensity_textBox.BackColor = Color.White;
            OccupancyDensity_textBox.BorderStyle = BorderStyle.None;
            OccupancyDensity_textBox.Enabled = false;
            OccupancyDensity_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            OccupancyDensity_textBox.ForeColor = SystemColors.ControlDark;
            OccupancyDensity_textBox.Location = new Point(798, 116);
            OccupancyDensity_textBox.Name = "OccupancyDensity_textBox";
            OccupancyDensity_textBox.Size = new Size(120, 15);
            OccupancyDensity_textBox.TabIndex = 64;
            OccupancyDensity_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // NetArea_textBox
            // 
            NetArea_textBox.BackColor = SystemColors.Window;
            NetArea_textBox.BorderStyle = BorderStyle.FixedSingle;
            NetArea_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            NetArea_textBox.Location = new Point(153, 14);
            NetArea_textBox.Name = "NetArea_textBox";
            NetArea_textBox.Size = new Size(120, 22);
            NetArea_textBox.TabIndex = 82;
            NetArea_textBox.TextAlign = HorizontalAlignment.Center;
            NetArea_textBox.TextChanged += NetArea_textBox_TextChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(83, 18);
            label23.Name = "label23";
            label23.Size = new Size(67, 15);
            label23.TabIndex = 81;
            label23.Text = "순바닥면적";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(737, 116);
            label26.Name = "label26";
            label26.Size = new Size(55, 15);
            label26.TabIndex = 63;
            label26.Text = "재실밀도";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.Location = new Point(286, 116);
            label27.Name = "label27";
            label27.Size = new Size(55, 15);
            label27.TabIndex = 60;
            label27.Text = "재실자수";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label28.Location = new Point(83, 116);
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
            label29.Location = new Point(682, 116);
            label29.Name = "label29";
            label29.Size = new Size(34, 15);
            label29.TabIndex = 55;
            label29.Text = "days";
            // 
            // AnnualUseDay_textBox
            // 
            AnnualUseDay_textBox.BackColor = Color.White;
            AnnualUseDay_textBox.BorderStyle = BorderStyle.None;
            AnnualUseDay_textBox.Enabled = false;
            AnnualUseDay_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AnnualUseDay_textBox.ForeColor = SystemColors.ControlDark;
            AnnualUseDay_textBox.Location = new Point(558, 116);
            AnnualUseDay_textBox.Name = "AnnualUseDay_textBox";
            AnnualUseDay_textBox.Size = new Size(120, 15);
            AnnualUseDay_textBox.TabIndex = 57;
            AnnualUseDay_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label30.ForeColor = SystemColors.ControlDark;
            label30.Location = new Point(495, 116);
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
            label12.Location = new Point(922, 82);
            label12.Name = "label12";
            label12.Size = new Size(28, 15);
            label12.TabIndex = 52;
            label12.Text = "h/d";
            // 
            // AHUTime_textBox
            // 
            AHUTime_textBox.BackColor = Color.White;
            AHUTime_textBox.BorderStyle = BorderStyle.None;
            AHUTime_textBox.Enabled = false;
            AHUTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AHUTime_textBox.ForeColor = SystemColors.ControlDark;
            AHUTime_textBox.Location = new Point(798, 82);
            AHUTime_textBox.Name = "AHUTime_textBox";
            AHUTime_textBox.Size = new Size(120, 15);
            AHUTime_textBox.TabIndex = 54;
            AHUTime_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(737, 82);
            label15.Name = "label15";
            label15.Size = new Size(55, 15);
            label15.TabIndex = 53;
            label15.Text = "공조시간";
            // 
            // EndTime_comboBox
            // 
            EndTime_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EndTime_comboBox.FormattingEnabled = true;
            EndTime_comboBox.Location = new Point(342, 78);
            EndTime_comboBox.Name = "EndTime_comboBox";
            EndTime_comboBox.Size = new Size(120, 23);
            EndTime_comboBox.TabIndex = 51;
            EndTime_comboBox.SelectedIndexChanged += EndTime_comboBox_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(286, 82);
            label16.Name = "label16";
            label16.Size = new Size(55, 15);
            label16.TabIndex = 50;
            label16.Text = "종료시간";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(83, 82);
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
            label18.Location = new Point(682, 82);
            label18.Name = "label18";
            label18.Size = new Size(28, 15);
            label18.TabIndex = 45;
            label18.Text = "h/d";
            // 
            // UseTime_textBox
            // 
            UseTime_textBox.BackColor = Color.White;
            UseTime_textBox.BorderStyle = BorderStyle.None;
            UseTime_textBox.Enabled = false;
            UseTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UseTime_textBox.ForeColor = SystemColors.ControlDark;
            UseTime_textBox.Location = new Point(558, 82);
            UseTime_textBox.Name = "UseTime_textBox";
            UseTime_textBox.Size = new Size(120, 15);
            UseTime_textBox.TabIndex = 47;
            UseTime_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(495, 82);
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
            label10.Location = new Point(922, 48);
            label10.Name = "label10";
            label10.Size = new Size(28, 15);
            label10.TabIndex = 42;
            label10.Text = "h/d";
            // 
            // HCTime_textBox
            // 
            HCTime_textBox.BackColor = Color.White;
            HCTime_textBox.BorderStyle = BorderStyle.None;
            HCTime_textBox.Enabled = false;
            HCTime_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            HCTime_textBox.ForeColor = SystemColors.ControlDark;
            HCTime_textBox.Location = new Point(798, 48);
            HCTime_textBox.Name = "HCTime_textBox";
            HCTime_textBox.Size = new Size(120, 15);
            HCTime_textBox.TabIndex = 44;
            HCTime_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = SystemColors.ControlDark;
            label11.Location = new Point(734, 48);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 43;
            label11.Text = "냉난방시간";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(298, 18);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 40;
            label7.Text = "천장고";
            // 
            // Usage_comboBox
            // 
            Usage_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Usage_comboBox.FormattingEnabled = true;
            Usage_comboBox.Location = new Point(153, 44);
            Usage_comboBox.Name = "Usage_comboBox";
            Usage_comboBox.Size = new Size(120, 23);
            Usage_comboBox.TabIndex = 36;
            Usage_comboBox.SelectedIndexChanged += Usage_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(83, 48);
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
            label8.Location = new Point(682, 48);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 22;
            label8.Text = "kWh/d";
            // 
            // DHWneed_textBox
            // 
            DHWneed_textBox.BackColor = Color.White;
            DHWneed_textBox.BorderStyle = BorderStyle.None;
            DHWneed_textBox.Enabled = false;
            DHWneed_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DHWneed_textBox.ForeColor = SystemColors.ControlDark;
            DHWneed_textBox.Location = new Point(560, 48);
            DHWneed_textBox.Name = "DHWneed_textBox";
            DHWneed_textBox.Size = new Size(116, 15);
            DHWneed_textBox.TabIndex = 29;
            DHWneed_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(495, 48);
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
            EquipIHG_image_textBox.BackColor = Color.OldLace;
            EquipIHG_image_textBox.BorderStyle = BorderStyle.None;
            EquipIHG_image_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            EquipIHG_image_textBox.ForeColor = SystemColors.ControlDarkDark;
            EquipIHG_image_textBox.Location = new Point(462, 277);
            EquipIHG_image_textBox.Name = "EquipIHG_image_textBox";
            EquipIHG_image_textBox.Size = new Size(69, 14);
            EquipIHG_image_textBox.TabIndex = 103;
            EquipIHG_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // DHWneed_image_textBox
            // 
            DHWneed_image_textBox.BackColor = Color.OldLace;
            DHWneed_image_textBox.BorderStyle = BorderStyle.None;
            DHWneed_image_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            DHWneed_image_textBox.ForeColor = SystemColors.ControlDarkDark;
            DHWneed_image_textBox.Location = new Point(348, 106);
            DHWneed_image_textBox.Name = "DHWneed_image_textBox";
            DHWneed_image_textBox.Size = new Size(65, 14);
            DHWneed_image_textBox.TabIndex = 102;
            DHWneed_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Door_textBox
            // 
            Door_textBox.BackColor = SystemColors.Window;
            Door_textBox.BorderStyle = BorderStyle.FixedSingle;
            Door_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Door_textBox.Location = new Point(851, 262);
            Door_textBox.Name = "Door_textBox";
            Door_textBox.ReadOnly = true;
            Door_textBox.Size = new Size(75, 22);
            Door_textBox.TabIndex = 101;
            Door_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // InWall_textBox
            // 
            InWall_textBox.BackColor = SystemColors.Window;
            InWall_textBox.BorderStyle = BorderStyle.FixedSingle;
            InWall_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            InWall_textBox.Location = new Point(851, 193);
            InWall_textBox.Name = "InWall_textBox";
            InWall_textBox.ReadOnly = true;
            InWall_textBox.Size = new Size(75, 22);
            InWall_textBox.TabIndex = 100;
            InWall_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // CW_textBox
            // 
            CW_textBox.BackColor = SystemColors.Window;
            CW_textBox.BorderStyle = BorderStyle.FixedSingle;
            CW_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CW_textBox.Location = new Point(851, 123);
            CW_textBox.Name = "CW_textBox";
            CW_textBox.ReadOnly = true;
            CW_textBox.Size = new Size(75, 22);
            CW_textBox.TabIndex = 99;
            CW_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PersonIHG_image_textBox
            // 
            PersonIHG_image_textBox.BackColor = Color.OldLace;
            PersonIHG_image_textBox.BorderStyle = BorderStyle.None;
            PersonIHG_image_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            PersonIHG_image_textBox.ForeColor = SystemColors.ControlDarkDark;
            PersonIHG_image_textBox.Location = new Point(262, 277);
            PersonIHG_image_textBox.Name = "PersonIHG_image_textBox";
            PersonIHG_image_textBox.Size = new Size(79, 14);
            PersonIHG_image_textBox.TabIndex = 94;
            PersonIHG_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Em_textBox
            // 
            Em_textBox.BackColor = Color.OldLace;
            Em_textBox.BorderStyle = BorderStyle.None;
            Em_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Em_textBox.ForeColor = SystemColors.ControlDarkDark;
            Em_textBox.Location = new Point(212, 213);
            Em_textBox.Name = "Em_textBox";
            Em_textBox.Size = new Size(49, 14);
            Em_textBox.TabIndex = 93;
            Em_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // theta_i_h_set_textBox
            // 
            theta_i_h_set_textBox.BackColor = Color.OldLace;
            theta_i_h_set_textBox.BorderStyle = BorderStyle.None;
            theta_i_h_set_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            theta_i_h_set_textBox.ForeColor = SystemColors.ControlDarkDark;
            theta_i_h_set_textBox.Location = new Point(254, 162);
            theta_i_h_set_textBox.Name = "theta_i_h_set_textBox";
            theta_i_h_set_textBox.Size = new Size(37, 14);
            theta_i_h_set_textBox.TabIndex = 92;
            theta_i_h_set_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // theta_i_c_set_textBox
            // 
            theta_i_c_set_textBox.BackColor = Color.OldLace;
            theta_i_c_set_textBox.BorderStyle = BorderStyle.None;
            theta_i_c_set_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            theta_i_c_set_textBox.ForeColor = SystemColors.ControlDarkDark;
            theta_i_c_set_textBox.Location = new Point(252, 136);
            theta_i_c_set_textBox.Name = "theta_i_c_set_textBox";
            theta_i_c_set_textBox.Size = new Size(39, 14);
            theta_i_c_set_textBox.TabIndex = 91;
            theta_i_c_set_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // EndTime_image_textBox
            // 
            EndTime_image_textBox.BackColor = Color.OldLace;
            EndTime_image_textBox.BorderStyle = BorderStyle.None;
            EndTime_image_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            EndTime_image_textBox.ForeColor = SystemColors.ControlDarkDark;
            EndTime_image_textBox.Location = new Point(252, 103);
            EndTime_image_textBox.Name = "EndTime_image_textBox";
            EndTime_image_textBox.Size = new Size(39, 14);
            EndTime_image_textBox.TabIndex = 90;
            EndTime_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // StartTime_image_textBox
            // 
            StartTime_image_textBox.BackColor = Color.OldLace;
            StartTime_image_textBox.BorderStyle = BorderStyle.None;
            StartTime_image_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            StartTime_image_textBox.ForeColor = SystemColors.ControlDarkDark;
            StartTime_image_textBox.Location = new Point(212, 103);
            StartTime_image_textBox.Name = "StartTime_image_textBox";
            StartTime_image_textBox.Size = new Size(39, 14);
            StartTime_image_textBox.TabIndex = 89;
            StartTime_image_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Floor_textBox
            // 
            Floor_textBox.BackColor = SystemColors.Window;
            Floor_textBox.BorderStyle = BorderStyle.FixedSingle;
            Floor_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Floor_textBox.Location = new Point(47, 275);
            Floor_textBox.Name = "Floor_textBox";
            Floor_textBox.ReadOnly = true;
            Floor_textBox.Size = new Size(74, 22);
            Floor_textBox.TabIndex = 88;
            Floor_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Window_textBox
            // 
            Window_textBox.BackColor = SystemColors.Window;
            Window_textBox.BorderStyle = BorderStyle.FixedSingle;
            Window_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Window_textBox.Location = new Point(46, 218);
            Window_textBox.Name = "Window_textBox";
            Window_textBox.ReadOnly = true;
            Window_textBox.Size = new Size(74, 22);
            Window_textBox.TabIndex = 87;
            Window_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Wall_textBox
            // 
            Wall_textBox.BackColor = SystemColors.Window;
            Wall_textBox.BorderStyle = BorderStyle.FixedSingle;
            Wall_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Wall_textBox.Location = new Point(46, 149);
            Wall_textBox.Name = "Wall_textBox";
            Wall_textBox.ReadOnly = true;
            Wall_textBox.Size = new Size(74, 22);
            Wall_textBox.TabIndex = 86;
            Wall_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Roof_textBox
            // 
            Roof_textBox.BackColor = SystemColors.Window;
            Roof_textBox.BorderStyle = BorderStyle.FixedSingle;
            Roof_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Roof_textBox.Location = new Point(46, 48);
            Roof_textBox.Name = "Roof_textBox";
            Roof_textBox.ReadOnly = true;
            Roof_textBox.Size = new Size(74, 22);
            Roof_textBox.TabIndex = 85;
            Roof_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(SA_Volume_textBox);
            AdditionalPanel.Controls.Add(RA_Volume_textBox);
            AdditionalPanel.Controls.Add(label72);
            AdditionalPanel.Controls.Add(label71);
            AdditionalPanel.Controls.Add(label70);
            AdditionalPanel.Controls.Add(label69);
            AdditionalPanel.Controls.Add(label68);
            AdditionalPanel.Controls.Add(label67);
            AdditionalPanel.Controls.Add(label66);
            AdditionalPanel.Controls.Add(EquipIHG_image_textBox);
            AdditionalPanel.Controls.Add(DHWneed_image_textBox);
            AdditionalPanel.Controls.Add(Door_textBox);
            AdditionalPanel.Controls.Add(InWall_textBox);
            AdditionalPanel.Controls.Add(CW_textBox);
            AdditionalPanel.Controls.Add(PersonIHG_image_textBox);
            AdditionalPanel.Controls.Add(Em_textBox);
            AdditionalPanel.Controls.Add(theta_i_h_set_textBox);
            AdditionalPanel.Controls.Add(theta_i_c_set_textBox);
            AdditionalPanel.Controls.Add(EndTime_image_textBox);
            AdditionalPanel.Controls.Add(StartTime_image_textBox);
            AdditionalPanel.Controls.Add(Floor_textBox);
            AdditionalPanel.Controls.Add(Window_textBox);
            AdditionalPanel.Controls.Add(Wall_textBox);
            AdditionalPanel.Controls.Add(Roof_textBox);
            AdditionalPanel.Controls.Add(AHU_pictureBox);
            AdditionalPanel.Controls.Add(HC_pictureBox);
            AdditionalPanel.Controls.Add(Ground_pictureBox);
            AdditionalPanel.Controls.Add(RoomControl_pictureBox);
            AdditionalPanel.Controls.Add(Main_pictureBox);
            AdditionalPanel.Location = new Point(12, 333);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 390);
            AdditionalPanel.TabIndex = 18;
            // 
            // SA_Volume_textBox
            // 
            SA_Volume_textBox.BackColor = Color.OldLace;
            SA_Volume_textBox.BorderStyle = BorderStyle.None;
            SA_Volume_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            SA_Volume_textBox.ForeColor = SystemColors.ControlDarkDark;
            SA_Volume_textBox.Location = new Point(506, 213);
            SA_Volume_textBox.Name = "SA_Volume_textBox";
            SA_Volume_textBox.Size = new Size(49, 14);
            SA_Volume_textBox.TabIndex = 115;
            SA_Volume_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // RA_Volume_textBox
            // 
            RA_Volume_textBox.BackColor = Color.OldLace;
            RA_Volume_textBox.BorderStyle = BorderStyle.None;
            RA_Volume_textBox.Font = new Font("굴림", 9F, FontStyle.Bold, GraphicsUnit.Point);
            RA_Volume_textBox.ForeColor = SystemColors.ControlDarkDark;
            RA_Volume_textBox.Location = new Point(439, 213);
            RA_Volume_textBox.Name = "RA_Volume_textBox";
            RA_Volume_textBox.Size = new Size(49, 14);
            RA_Volume_textBox.TabIndex = 114;
            RA_Volume_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label72
            // 
            label72.AutoSize = true;
            label72.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label72.ForeColor = SystemColors.ControlDark;
            label72.Location = new Point(851, 244);
            label72.Name = "label72";
            label72.Size = new Size(67, 15);
            label72.TabIndex = 113;
            label72.Text = "외부출입문";
            // 
            // label71
            // 
            label71.AutoSize = true;
            label71.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label71.ForeColor = SystemColors.ControlDark;
            label71.Location = new Point(851, 175);
            label71.Name = "label71";
            label71.Size = new Size(31, 15);
            label71.TabIndex = 112;
            label71.Text = "내벽";
            // 
            // label70
            // 
            label70.AutoSize = true;
            label70.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label70.ForeColor = SystemColors.ControlDark;
            label70.Location = new Point(851, 105);
            label70.Name = "label70";
            label70.Size = new Size(55, 15);
            label70.TabIndex = 111;
            label70.Text = "커튼월창";
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label69.ForeColor = SystemColors.ControlDark;
            label69.Location = new Point(48, 257);
            label69.Name = "label69";
            label69.Size = new Size(31, 15);
            label69.TabIndex = 110;
            label69.Text = "바닥";
            // 
            // label68
            // 
            label68.AutoSize = true;
            label68.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label68.ForeColor = SystemColors.ControlDark;
            label68.Location = new Point(47, 200);
            label68.Name = "label68";
            label68.Size = new Size(31, 15);
            label68.TabIndex = 109;
            label68.Text = "창호";
            // 
            // label67
            // 
            label67.AutoSize = true;
            label67.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label67.ForeColor = SystemColors.ControlDark;
            label67.Location = new Point(46, 131);
            label67.Name = "label67";
            label67.Size = new Size(31, 15);
            label67.TabIndex = 108;
            label67.Text = "외벽";
            // 
            // label66
            // 
            label66.AutoSize = true;
            label66.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label66.ForeColor = SystemColors.ControlDark;
            label66.Location = new Point(47, 30);
            label66.Name = "label66";
            label66.Size = new Size(31, 15);
            label66.TabIndex = 107;
            label66.Text = "지붕";
            // 
            // AHU_pictureBox
            // 
            AHU_pictureBox.Location = new Point(9, 16);
            AHU_pictureBox.Name = "AHU_pictureBox";
            AHU_pictureBox.Size = new Size(953, 370);
            AHU_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            AHU_pictureBox.TabIndex = 119;
            AHU_pictureBox.TabStop = false;
            // 
            // HC_pictureBox
            // 
            HC_pictureBox.Location = new Point(9, 16);
            HC_pictureBox.Name = "HC_pictureBox";
            HC_pictureBox.Size = new Size(953, 370);
            HC_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            HC_pictureBox.TabIndex = 118;
            HC_pictureBox.TabStop = false;
            // 
            // Ground_pictureBox
            // 
            Ground_pictureBox.Location = new Point(9, 16);
            Ground_pictureBox.Name = "Ground_pictureBox";
            Ground_pictureBox.Size = new Size(953, 370);
            Ground_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Ground_pictureBox.TabIndex = 117;
            Ground_pictureBox.TabStop = false;
            // 
            // RoomControl_pictureBox
            // 
            RoomControl_pictureBox.Location = new Point(9, 16);
            RoomControl_pictureBox.Name = "RoomControl_pictureBox";
            RoomControl_pictureBox.Size = new Size(953, 370);
            RoomControl_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            RoomControl_pictureBox.TabIndex = 116;
            RoomControl_pictureBox.TabStop = false;
            // 
            // Main_pictureBox
            // 
            Main_pictureBox.Location = new Point(9, 16);
            Main_pictureBox.Name = "Main_pictureBox";
            Main_pictureBox.Size = new Size(953, 370);
            Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Main_pictureBox.TabIndex = 115;
            Main_pictureBox.TabStop = false;
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
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label61.ForeColor = SystemColors.ControlDark;
            label61.Location = new Point(1142, 385);
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
            VentilationRate_textBox.Location = new Point(1061, 385);
            VentilationRate_textBox.Name = "VentilationRate_textBox";
            VentilationRate_textBox.Size = new Size(79, 15);
            VentilationRate_textBox.TabIndex = 102;
            VentilationRate_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label62.Location = new Point(996, 385);
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
            // Volume_wd_textBox
            // 
            Volume_wd_textBox.BackColor = SystemColors.InactiveBorder;
            Volume_wd_textBox.BorderStyle = BorderStyle.None;
            Volume_wd_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Volume_wd_textBox.ForeColor = SystemColors.ControlDark;
            Volume_wd_textBox.Location = new Point(1061, 415);
            Volume_wd_textBox.Name = "Volume_wd_textBox";
            Volume_wd_textBox.Size = new Size(79, 15);
            Volume_wd_textBox.TabIndex = 105;
            Volume_wd_textBox.TextAlign = HorizontalAlignment.Center;
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
            // BuildingCategory_textBox
            // 
            BuildingCategory_textBox.BackColor = SystemColors.InactiveBorder;
            BuildingCategory_textBox.BorderStyle = BorderStyle.None;
            BuildingCategory_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BuildingCategory_textBox.ForeColor = SystemColors.ControlDark;
            BuildingCategory_textBox.Location = new Point(1057, 54);
            BuildingCategory_textBox.Name = "BuildingCategory_textBox";
            BuildingCategory_textBox.Size = new Size(117, 15);
            BuildingCategory_textBox.TabIndex = 107;
            BuildingCategory_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // BuildingUse_textBox
            // 
            BuildingUse_textBox.BackColor = SystemColors.InactiveBorder;
            BuildingUse_textBox.BorderStyle = BorderStyle.None;
            BuildingUse_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BuildingUse_textBox.ForeColor = SystemColors.ControlDark;
            BuildingUse_textBox.Location = new Point(1057, 90);
            BuildingUse_textBox.Name = "BuildingUse_textBox";
            BuildingUse_textBox.Size = new Size(117, 15);
            BuildingUse_textBox.TabIndex = 108;
            BuildingUse_textBox.TextAlign = HorizontalAlignment.Center;
            BuildingUse_textBox.TextChanged += BuildingUse_textBox_TextChanged;
            // 
            // label73
            // 
            label73.AutoSize = true;
            label73.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label73.Location = new Point(993, 127);
            label73.Name = "label73";
            label73.Size = new Size(55, 15);
            label73.TabIndex = 109;
            label73.Text = "바닥유형";
            // 
            // Ground_textBox
            // 
            Ground_textBox.BackColor = SystemColors.InactiveBorder;
            Ground_textBox.BorderStyle = BorderStyle.None;
            Ground_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ground_textBox.ForeColor = SystemColors.ControlDark;
            Ground_textBox.Location = new Point(1057, 127);
            Ground_textBox.Name = "Ground_textBox";
            Ground_textBox.Size = new Size(117, 15);
            Ground_textBox.TabIndex = 110;
            Ground_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // ZoneGeneral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Ground_textBox);
            Controls.Add(label73);
            Controls.Add(BuildingUse_textBox);
            Controls.Add(BuildingCategory_textBox);
            Controls.Add(label63);
            Controls.Add(Volume_wd_textBox);
            Controls.Add(label64);
            Controls.Add(label61);
            Controls.Add(VentilationRate_textBox);
            Controls.Add(label62);
            Controls.Add(Save_button);
            Controls.Add(label53);
            Controls.Add(panel2);
            Controls.Add(label21);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneGeneral";
            Text = "Form3";
            VisibleChanged += ZoneGeneral_VisibleChanged;
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AHU_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HC_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Ground_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)RoomControl_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Main_pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Layer_textBox;
        private System.Windows.Forms.TextBox η_textBox;

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
        private System.Windows.Forms.Label AHU_label;
        private System.Windows.Forms.ComboBox AHU_comboBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox CeilingHeight_textBox;
        private System.Windows.Forms.ComboBox StartTime_comboBox;
        private System.Windows.Forms.ComboBox WeekUseDay_comboBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox NetArea_textBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox PersonNum_textBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label η2_label2;
        private System.Windows.Forms.Label η2_label;
        private System.Windows.Forms.TextBox η2_textBox;
        private System.Windows.Forms.Label η_label2;
        private System.Windows.Forms.Label η_label;
        private System.Windows.Forms.ComboBox RoomControl_comboBox;
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
        private System.Windows.Forms.TextBox Door_textBox;
        private System.Windows.Forms.TextBox InWall_textBox;
        private System.Windows.Forms.TextBox CW_textBox;
        private System.Windows.Forms.TextBox HeatingLoad_textBox;
        private System.Windows.Forms.TextBox CoolingLoad_textBox;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox PersonIHG_image_textBox;
        private System.Windows.Forms.TextBox Em_textBox;
        private System.Windows.Forms.TextBox theta_i_h_set_textBox;
        private System.Windows.Forms.TextBox theta_i_c_set_textBox;
        private System.Windows.Forms.TextBox EndTime_image_textBox;
        private System.Windows.Forms.TextBox StartTime_image_textBox;
        private System.Windows.Forms.TextBox Floor_textBox;
        private System.Windows.Forms.TextBox Window_textBox;
        private System.Windows.Forms.TextBox Wall_textBox;
        private System.Windows.Forms.TextBox Roof_textBox;
        private System.Windows.Forms.TextBox EquipIHG_image_textBox;
        private System.Windows.Forms.TextBox ZoneName_textBox;
        private System.Windows.Forms.TextBox OccupancyDensity_index_textBox;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox NetVolume_textBox;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox VentilationRate_textBox;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TextBox Volume_wd_textBox;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox Num_textBox;
        private PictureBox Icon_pictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox BuildingCategory_textBox;
        private System.Windows.Forms.TextBox BuildingUse_textBox;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox RA_Volume_textBox;
        private PictureBox Main_pictureBox;
        private System.Windows.Forms.Label label73;
        private PictureBox RoomControl_pictureBox;
        private PictureBox Ground_pictureBox;
        private PictureBox HC_pictureBox;
        private PictureBox AHU_pictureBox;
        private System.Windows.Forms.TextBox SA_Volume_textBox;
        private System.Windows.Forms.TextBox Ground_textBox;
    }
}