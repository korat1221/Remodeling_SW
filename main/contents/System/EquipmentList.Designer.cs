using System.Reflection.Emit;
using System.Windows.Forms;

namespace main.contents
{
    partial class EquipmentList
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
            DataGridViewCellStyle dataGridViewCellStyle46 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle47 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle48 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle49 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle50 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle51 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle52 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle53 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle54 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle55 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle56 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle57 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle58 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle59 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle60 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle61 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle62 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle63 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle64 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle65 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle66 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle67 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle68 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle69 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle70 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle71 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle72 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle73 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle74 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle75 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle76 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle77 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle78 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle79 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle80 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle81 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle82 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle83 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle84 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle85 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle86 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle87 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle88 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle89 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle90 = new DataGridViewCellStyle();
            PV_dataGridView = new DataGridView();
            GeneralPanel = new Panel();
            label4 = new System.Windows.Forms.Label();
            Icon_pictureBox = new PictureBox();
            tabControl1 = new CustomTabControl();
            HP_tabPage = new TabPage();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            AirHP_dataGridView = new DataGridView();
            AirHP_Save_button = new Button();
            DefaultAirHP_Add_button = new Button();
            AirHP_Copy_button = new Button();
            AirHP_Remove_button = new Button();
            UserAirHP_Add_button = new Button();
            Boiler_tabPage = new TabPage();
            label18 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            DefaultBoiler_Add_button = new Button();
            Boiler_Save_button = new Button();
            Boiler_dataGridView = new DataGridView();
            Boiler_Copy_button = new Button();
            Boiler_Remove_button = new Button();
            UserBoiler_Add_button = new Button();
            AS_tabPage = new TabPage();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            ABS_dataGridView = new DataGridView();
            ABS_Save_button = new Button();
            DefaultABS_Add_button = new Button();
            ABS_Copy_button = new Button();
            ABS_Remove_button = new Button();
            UserABS_Add_button = new Button();
            DH_tabPage = new TabPage();
            DH_dataGridView = new DataGridView();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            DH_Save_button = new Button();
            DefaultDH_Add_button = new Button();
            DH_Copy_button = new Button();
            DH_Remove_button = new Button();
            UserDH_Add_button = new Button();
            AirCooler_tabPage = new TabPage();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            AirCooler_dataGridView = new DataGridView();
            AirCooler_Save_button = new Button();
            DefaultAirCooler_Add_button = new Button();
            AirCooler_Copy_button = new Button();
            AirCooler_Remove_button = new Button();
            UserAirCooler_Add_button = new Button();
            Solar_Save_button = new Button();
            WaterCooler_tabPage = new TabPage();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            WaterCooler_dataGridView = new DataGridView();
            WaterCooler_Save_button = new Button();
            DefaultWaterCooler_Add_button = new Button();
            WaterCooler_Copy_button = new Button();
            WaterCooler_Remove_button = new Button();
            UserWaterCooler_Add_button = new Button();
            Pump_Save_button = new Button();
            Pump_dataGridView = new DataGridView();
            Pump_Copy_button = new Button();
            Pump_Remove_button = new Button();
            Pump_Add_button = new Button();
            ce_Save_button = new Button();
            ce_dataGridView = new DataGridView();
            ce_Copy_button = new Button();
            ce_Remove_button = new Button();
            ce_Add_button = new Button();
            tabPage6 = new TabPage();
            WP_dataGridView = new DataGridView();
            WP_Save_button = new Button();
            WP_Copy_button = new Button();
            WP_Remove_button = new Button();
            UserWP_Add_button = new Button();
            button15 = new Button();
            tabPage5 = new TabPage();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            FC_dataGridView = new DataGridView();
            FC_Save_button = new Button();
            DefaultFC_Add_button = new Button();
            FC_Copy_button = new Button();
            FC_Remove_button = new Button();
            UserFC_Add_button = new Button();
            tabPage4 = new TabPage();
            GWHP_Save_button = new Button();
            GWHP_dataGridView = new DataGridView();
            UserGWHP_Add_button = new Button();
            GWHP_Remove_button = new Button();
            GWHP_Copy_button = new Button();
            tabPage3 = new TabPage();
            GroundHP_Save_button = new Button();
            GroundHP_dataGridView = new DataGridView();
            GroundHP_Copy_button = new Button();
            GroundHP_Remove_button = new Button();
            UserGroundHP_Add_button = new Button();
            tabPage2 = new TabPage();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            button6 = new Button();
            DefaultSolar_Add_button = new Button();
            UserSolar_Add_button = new Button();
            Solar_Remove_button = new Button();
            Solar_dataGridView = new DataGridView();
            Solar_Copy_button = new Button();
            tabPage1 = new TabPage();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            PV_Save_button = new Button();
            DefaultPV_Add_button = new Button();
            PV_Copy_button = new Button();
            PV_Remove_button = new Button();
            UserPV_Add_button = new Button();
            customTabControl1 = new CustomTabControl();
            tabPage10 = new TabPage();
            DHU_checkBox = new CheckBox();
            label27 = new System.Windows.Forms.Label();
            AHU_dataGridView = new DataGridView();
            AHU_Save_button = new Button();
            AHU_Copy_button = new Button();
            AHU_Remove_button = new Button();
            UserAHU_Add_button = new Button();
            tabPage9 = new TabPage();
            tabPage8 = new TabPage();
            tabPage7 = new TabPage();
            customTabControl2 = new CustomTabControl();
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            tabControl1.SuspendLayout();
            HP_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AirHP_dataGridView).BeginInit();
            Boiler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
            AS_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ABS_dataGridView).BeginInit();
            DH_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DH_dataGridView).BeginInit();
            AirCooler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AirCooler_dataGridView).BeginInit();
            WaterCooler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WaterCooler_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ce_dataGridView).BeginInit();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).BeginInit();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GWHP_dataGridView).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroundHP_dataGridView).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Solar_dataGridView).BeginInit();
            tabPage1.SuspendLayout();
            customTabControl1.SuspendLayout();
            tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AHU_dataGridView).BeginInit();
            tabPage9.SuspendLayout();
            tabPage8.SuspendLayout();
            customTabControl2.SuspendLayout();
            SuspendLayout();
            // 
            // PV_dataGridView
            // 
            PV_dataGridView.AllowUserToAddRows = false;
            PV_dataGridView.AllowUserToDeleteRows = false;
            PV_dataGridView.AllowUserToResizeColumns = false;
            PV_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PV_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PV_dataGridView.BackgroundColor = SystemColors.Window;
            PV_dataGridView.BorderStyle = BorderStyle.None;
            PV_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PV_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle46.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle46.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle46.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle46.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle46.SelectionForeColor = Color.Black;
            dataGridViewCellStyle46.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
            PV_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PV_dataGridView.Location = new Point(18, 36);
            PV_dataGridView.Name = "PV_dataGridView";
            dataGridViewCellStyle47.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = SystemColors.Control;
            dataGridViewCellStyle47.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle47.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle47.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle47.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle47.WrapMode = DataGridViewTriState.True;
            PV_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle47;
            PV_dataGridView.RowHeadersVisible = false;
            PV_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle48.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle48.ForeColor = Color.Black;
            dataGridViewCellStyle48.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle48.SelectionForeColor = Color.Black;
            PV_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle48;
            PV_dataGridView.RowTemplate.Height = 25;
            PV_dataGridView.Size = new Size(1105, 150);
            PV_dataGridView.TabIndex = 120;
            PV_dataGridView.CellContentClick += PV_dataGridView_CellContentClick;
            PV_dataGridView.CellValueChanged += PV_dataGridView_CellValueChanged;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1150, 57);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(70, 25);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 102;
            label4.Text = "장비일람표";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(14, 4);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 97;
            Icon_pictureBox.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(HP_tabPage);
            tabControl1.Controls.Add(Boiler_tabPage);
            tabControl1.Controls.Add(AS_tabPage);
            tabControl1.Controls.Add(DH_tabPage);
            tabControl1.Controls.Add(AirCooler_tabPage);
            tabControl1.Controls.Add(WaterCooler_tabPage);
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
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(12, 75);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1150, 255);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 145;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Controls.Add(label3);
            HP_tabPage.Controls.Add(label5);
            HP_tabPage.Controls.Add(AirHP_dataGridView);
            HP_tabPage.Controls.Add(AirHP_Save_button);
            HP_tabPage.Controls.Add(DefaultAirHP_Add_button);
            HP_tabPage.Controls.Add(AirHP_Copy_button);
            HP_tabPage.Controls.Add(AirHP_Remove_button);
            HP_tabPage.Controls.Add(UserAirHP_Add_button);
            HP_tabPage.Location = new Point(4, 25);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(1142, 226);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "히트펌프";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(912, 14);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 115;
            label3.Text = "도면 기반 입력";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(768, 14);
            label5.Name = "label5";
            label5.Size = new Size(104, 15);
            label5.TabIndex = 114;
            label5.Text = "기본 DB기반 입력";
            // 
            // AirHP_dataGridView
            // 
            AirHP_dataGridView.AllowUserToAddRows = false;
            AirHP_dataGridView.AllowUserToDeleteRows = false;
            AirHP_dataGridView.AllowUserToResizeColumns = false;
            AirHP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AirHP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AirHP_dataGridView.BackgroundColor = SystemColors.Window;
            AirHP_dataGridView.BorderStyle = BorderStyle.None;
            AirHP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AirHP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle49.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle49.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle49.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle49.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle49.SelectionForeColor = Color.Black;
            dataGridViewCellStyle49.WrapMode = DataGridViewTriState.True;
            AirHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle49;
            AirHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AirHP_dataGridView.Location = new Point(18, 36);
            AirHP_dataGridView.Name = "AirHP_dataGridView";
            dataGridViewCellStyle50.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.BackColor = SystemColors.Control;
            dataGridViewCellStyle50.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle50.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle50.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle50.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = DataGridViewTriState.True;
            AirHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle50;
            AirHP_dataGridView.RowHeadersVisible = false;
            AirHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle51.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle51.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle51.ForeColor = Color.Black;
            dataGridViewCellStyle51.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle51.SelectionForeColor = Color.Black;
            AirHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle51;
            AirHP_dataGridView.RowTemplate.Height = 25;
            AirHP_dataGridView.Size = new Size(1105, 150);
            AirHP_dataGridView.TabIndex = 112;
            AirHP_dataGridView.CellContentClick += AirHP_dataGridView_CellContentClick;
            AirHP_dataGridView.CellValueChanged += AirHP_dataGridView_CellValueChanged;
            // 
            // AirHP_Save_button
            // 
            AirHP_Save_button.BackColor = SystemColors.ButtonHighlight;
            AirHP_Save_button.ForeColor = Color.Black;
            AirHP_Save_button.Location = new Point(1035, 192);
            AirHP_Save_button.Name = "AirHP_Save_button";
            AirHP_Save_button.Size = new Size(88, 25);
            AirHP_Save_button.TabIndex = 113;
            AirHP_Save_button.Text = "SAVE";
            AirHP_Save_button.UseVisualStyleBackColor = true;
            AirHP_Save_button.Click += AirHP_Save_button_Click;
            // 
            // DefaultAirHP_Add_button
            // 
            DefaultAirHP_Add_button.BackColor = SystemColors.ControlLight;
            DefaultAirHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultAirHP_Add_button.FlatStyle = FlatStyle.System;
            DefaultAirHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultAirHP_Add_button.Location = new Point(875, 10);
            DefaultAirHP_Add_button.Margin = new Padding(0);
            DefaultAirHP_Add_button.Name = "DefaultAirHP_Add_button";
            DefaultAirHP_Add_button.Size = new Size(23, 23);
            DefaultAirHP_Add_button.TabIndex = 110;
            DefaultAirHP_Add_button.Text = "+";
            DefaultAirHP_Add_button.UseVisualStyleBackColor = false;
            DefaultAirHP_Add_button.Click += DefaultAirHP_Add_button_Click;
            // 
            // AirHP_Copy_button
            // 
            AirHP_Copy_button.BackColor = SystemColors.ControlLight;
            AirHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AirHP_Copy_button.FlatStyle = FlatStyle.System;
            AirHP_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            AirHP_Copy_button.Location = new Point(1076, 10);
            AirHP_Copy_button.Margin = new Padding(0);
            AirHP_Copy_button.Name = "AirHP_Copy_button";
            AirHP_Copy_button.Size = new Size(47, 23);
            AirHP_Copy_button.TabIndex = 108;
            AirHP_Copy_button.Text = "Copy";
            AirHP_Copy_button.UseVisualStyleBackColor = false;
            AirHP_Copy_button.Click += AirHP_Copy_button_Click;
            // 
            // AirHP_Remove_button
            // 
            AirHP_Remove_button.BackColor = SystemColors.ControlLight;
            AirHP_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AirHP_Remove_button.FlatStyle = FlatStyle.System;
            AirHP_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AirHP_Remove_button.Location = new Point(1039, 10);
            AirHP_Remove_button.Margin = new Padding(0);
            AirHP_Remove_button.Name = "AirHP_Remove_button";
            AirHP_Remove_button.Size = new Size(23, 23);
            AirHP_Remove_button.TabIndex = 107;
            AirHP_Remove_button.Text = "-";
            AirHP_Remove_button.UseVisualStyleBackColor = false;
            AirHP_Remove_button.Click += AirHP_Remove_button_Click;
            // 
            // UserAirHP_Add_button
            // 
            UserAirHP_Add_button.BackColor = SystemColors.ControlLight;
            UserAirHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserAirHP_Add_button.FlatStyle = FlatStyle.System;
            UserAirHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserAirHP_Add_button.Location = new Point(1002, 10);
            UserAirHP_Add_button.Margin = new Padding(0);
            UserAirHP_Add_button.Name = "UserAirHP_Add_button";
            UserAirHP_Add_button.Size = new Size(23, 23);
            UserAirHP_Add_button.TabIndex = 106;
            UserAirHP_Add_button.Text = "+";
            UserAirHP_Add_button.UseVisualStyleBackColor = false;
            UserAirHP_Add_button.Click += UserAirHP_Add_button_Click;
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.Controls.Add(label18);
            Boiler_tabPage.Controls.Add(label20);
            Boiler_tabPage.Controls.Add(DefaultBoiler_Add_button);
            Boiler_tabPage.Controls.Add(Boiler_Save_button);
            Boiler_tabPage.Controls.Add(Boiler_dataGridView);
            Boiler_tabPage.Controls.Add(Boiler_Copy_button);
            Boiler_tabPage.Controls.Add(Boiler_Remove_button);
            Boiler_tabPage.Controls.Add(UserBoiler_Add_button);
            Boiler_tabPage.Location = new Point(4, 25);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(1142, 226);
            Boiler_tabPage.TabIndex = 6;
            Boiler_tabPage.Text = "보일러";
            Boiler_tabPage.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label18.Location = new Point(768, 8);
            label18.Name = "label18";
            label18.Size = new Size(104, 15);
            label18.TabIndex = 125;
            label18.Text = "기본 DB기반 입력";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label20.Location = new Point(912, 8);
            label20.Name = "label20";
            label20.Size = new Size(87, 15);
            label20.TabIndex = 127;
            label20.Text = "도면 기반 입력";
            // 
            // DefaultBoiler_Add_button
            // 
            DefaultBoiler_Add_button.BackColor = SystemColors.ControlLight;
            DefaultBoiler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultBoiler_Add_button.FlatStyle = FlatStyle.System;
            DefaultBoiler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultBoiler_Add_button.Location = new Point(875, 4);
            DefaultBoiler_Add_button.Margin = new Padding(0);
            DefaultBoiler_Add_button.Name = "DefaultBoiler_Add_button";
            DefaultBoiler_Add_button.Size = new Size(23, 23);
            DefaultBoiler_Add_button.TabIndex = 124;
            DefaultBoiler_Add_button.Text = "+";
            DefaultBoiler_Add_button.UseVisualStyleBackColor = false;
            DefaultBoiler_Add_button.Click += DefaultBoiler_Add_button_Click;
            // 
            // Boiler_Save_button
            // 
            Boiler_Save_button.BackColor = SystemColors.ButtonHighlight;
            Boiler_Save_button.ForeColor = Color.Black;
            Boiler_Save_button.Location = new Point(1035, 186);
            Boiler_Save_button.Name = "Boiler_Save_button";
            Boiler_Save_button.Size = new Size(88, 25);
            Boiler_Save_button.TabIndex = 102;
            Boiler_Save_button.Text = "SAVE";
            Boiler_Save_button.UseVisualStyleBackColor = true;
            Boiler_Save_button.Click += Boiler_Save_button_Click;
            // 
            // Boiler_dataGridView
            // 
            Boiler_dataGridView.AllowUserToAddRows = false;
            Boiler_dataGridView.AllowUserToDeleteRows = false;
            Boiler_dataGridView.AllowUserToResizeColumns = false;
            Boiler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Boiler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Boiler_dataGridView.BackgroundColor = SystemColors.Window;
            Boiler_dataGridView.BorderStyle = BorderStyle.None;
            Boiler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Boiler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle52.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle52.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle52.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle52.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle52.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle52.SelectionForeColor = Color.Black;
            dataGridViewCellStyle52.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle52;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(19, 30);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle53.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle53.BackColor = SystemColors.Control;
            dataGridViewCellStyle53.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle53.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle53.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle53.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle53.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle53;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle54.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle54.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle54.ForeColor = Color.Black;
            dataGridViewCellStyle54.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle54.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle54;
            Boiler_dataGridView.RowTemplate.Height = 25;
            Boiler_dataGridView.Size = new Size(1105, 150);
            Boiler_dataGridView.TabIndex = 101;
            Boiler_dataGridView.CellContentClick += Boiler_dataGridView_CellContentClick;
            Boiler_dataGridView.CellValueChanged += Boiler_dataGridView_CellValueChanged;
            // 
            // Boiler_Copy_button
            // 
            Boiler_Copy_button.BackColor = SystemColors.ControlLight;
            Boiler_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Boiler_Copy_button.FlatStyle = FlatStyle.System;
            Boiler_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Boiler_Copy_button.Location = new Point(1076, 4);
            Boiler_Copy_button.Margin = new Padding(0);
            Boiler_Copy_button.Name = "Boiler_Copy_button";
            Boiler_Copy_button.Size = new Size(47, 23);
            Boiler_Copy_button.TabIndex = 100;
            Boiler_Copy_button.Text = "Copy";
            Boiler_Copy_button.UseVisualStyleBackColor = false;
            Boiler_Copy_button.Click += Boiler_Copy_button_Click;
            // 
            // Boiler_Remove_button
            // 
            Boiler_Remove_button.BackColor = SystemColors.ControlLight;
            Boiler_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Boiler_Remove_button.FlatStyle = FlatStyle.System;
            Boiler_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Boiler_Remove_button.Location = new Point(1039, 4);
            Boiler_Remove_button.Margin = new Padding(0);
            Boiler_Remove_button.Name = "Boiler_Remove_button";
            Boiler_Remove_button.Size = new Size(23, 23);
            Boiler_Remove_button.TabIndex = 99;
            Boiler_Remove_button.Text = "-";
            Boiler_Remove_button.UseVisualStyleBackColor = false;
            Boiler_Remove_button.Click += Boiler_Remove_button_Click;
            // 
            // UserBoiler_Add_button
            // 
            UserBoiler_Add_button.BackColor = SystemColors.ControlLight;
            UserBoiler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserBoiler_Add_button.FlatStyle = FlatStyle.System;
            UserBoiler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserBoiler_Add_button.Location = new Point(1002, 4);
            UserBoiler_Add_button.Margin = new Padding(0);
            UserBoiler_Add_button.Name = "UserBoiler_Add_button";
            UserBoiler_Add_button.Size = new Size(23, 23);
            UserBoiler_Add_button.TabIndex = 98;
            UserBoiler_Add_button.Text = "+";
            UserBoiler_Add_button.UseVisualStyleBackColor = false;
            UserBoiler_Add_button.Click += UserBoiler_Add_button_Click;
            // 
            // AS_tabPage
            // 
            AS_tabPage.Controls.Add(label8);
            AS_tabPage.Controls.Add(label9);
            AS_tabPage.Controls.Add(ABS_dataGridView);
            AS_tabPage.Controls.Add(ABS_Save_button);
            AS_tabPage.Controls.Add(DefaultABS_Add_button);
            AS_tabPage.Controls.Add(ABS_Copy_button);
            AS_tabPage.Controls.Add(ABS_Remove_button);
            AS_tabPage.Controls.Add(UserABS_Add_button);
            AS_tabPage.Location = new Point(4, 25);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(1142, 226);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식냉온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(912, 11);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 127;
            label8.Text = "도면 기반 입력";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(768, 11);
            label9.Name = "label9";
            label9.Size = new Size(104, 15);
            label9.TabIndex = 126;
            label9.Text = "기본 DB기반 입력";
            // 
            // ABS_dataGridView
            // 
            ABS_dataGridView.AllowUserToAddRows = false;
            ABS_dataGridView.AllowUserToDeleteRows = false;
            ABS_dataGridView.AllowUserToResizeColumns = false;
            ABS_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ABS_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ABS_dataGridView.BackgroundColor = SystemColors.Window;
            ABS_dataGridView.BorderStyle = BorderStyle.None;
            ABS_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ABS_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle55.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle55.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle55.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle55.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle55.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle55.SelectionForeColor = Color.Black;
            dataGridViewCellStyle55.WrapMode = DataGridViewTriState.True;
            ABS_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle55;
            ABS_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ABS_dataGridView.Location = new Point(18, 38);
            ABS_dataGridView.Name = "ABS_dataGridView";
            dataGridViewCellStyle56.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle56.BackColor = SystemColors.Control;
            dataGridViewCellStyle56.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle56.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle56.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = DataGridViewTriState.True;
            ABS_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle56;
            ABS_dataGridView.RowHeadersVisible = false;
            ABS_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle57.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle57.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle57.ForeColor = Color.Black;
            dataGridViewCellStyle57.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle57.SelectionForeColor = Color.Black;
            ABS_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle57;
            ABS_dataGridView.RowTemplate.Height = 25;
            ABS_dataGridView.Size = new Size(1105, 150);
            ABS_dataGridView.TabIndex = 120;
            ABS_dataGridView.CellContentClick += ABS_dataGridView_CellContentClick;
            ABS_dataGridView.CellValueChanged += ABS_dataGridView_CellValueChanged;
            // 
            // ABS_Save_button
            // 
            ABS_Save_button.BackColor = SystemColors.ButtonHighlight;
            ABS_Save_button.ForeColor = Color.Black;
            ABS_Save_button.Location = new Point(1035, 194);
            ABS_Save_button.Name = "ABS_Save_button";
            ABS_Save_button.Size = new Size(88, 25);
            ABS_Save_button.TabIndex = 121;
            ABS_Save_button.Text = "SAVE";
            ABS_Save_button.UseVisualStyleBackColor = true;
            ABS_Save_button.Click += ABS_Save_button_Click;
            // 
            // DefaultABS_Add_button
            // 
            DefaultABS_Add_button.BackColor = SystemColors.ControlLight;
            DefaultABS_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultABS_Add_button.FlatStyle = FlatStyle.System;
            DefaultABS_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultABS_Add_button.Location = new Point(875, 7);
            DefaultABS_Add_button.Margin = new Padding(0);
            DefaultABS_Add_button.Name = "DefaultABS_Add_button";
            DefaultABS_Add_button.Size = new Size(23, 23);
            DefaultABS_Add_button.TabIndex = 118;
            DefaultABS_Add_button.Text = "+";
            DefaultABS_Add_button.UseVisualStyleBackColor = false;
            DefaultABS_Add_button.Click += DefaultABS_Add_button_Click;
            // 
            // ABS_Copy_button
            // 
            ABS_Copy_button.BackColor = SystemColors.ControlLight;
            ABS_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ABS_Copy_button.FlatStyle = FlatStyle.System;
            ABS_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            ABS_Copy_button.Location = new Point(1076, 7);
            ABS_Copy_button.Margin = new Padding(0);
            ABS_Copy_button.Name = "ABS_Copy_button";
            ABS_Copy_button.Size = new Size(47, 23);
            ABS_Copy_button.TabIndex = 116;
            ABS_Copy_button.Text = "Copy";
            ABS_Copy_button.UseVisualStyleBackColor = false;
            ABS_Copy_button.Click += ABS_Copy_button_Click;
            // 
            // ABS_Remove_button
            // 
            ABS_Remove_button.BackColor = SystemColors.ControlLight;
            ABS_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ABS_Remove_button.FlatStyle = FlatStyle.System;
            ABS_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            ABS_Remove_button.Location = new Point(1039, 7);
            ABS_Remove_button.Margin = new Padding(0);
            ABS_Remove_button.Name = "ABS_Remove_button";
            ABS_Remove_button.Size = new Size(23, 23);
            ABS_Remove_button.TabIndex = 115;
            ABS_Remove_button.Text = "-";
            ABS_Remove_button.UseVisualStyleBackColor = false;
            ABS_Remove_button.Click += ABS_Remove_button_Click;
            // 
            // UserABS_Add_button
            // 
            UserABS_Add_button.BackColor = SystemColors.ControlLight;
            UserABS_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserABS_Add_button.FlatStyle = FlatStyle.System;
            UserABS_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserABS_Add_button.Location = new Point(1002, 7);
            UserABS_Add_button.Margin = new Padding(0);
            UserABS_Add_button.Name = "UserABS_Add_button";
            UserABS_Add_button.Size = new Size(23, 23);
            UserABS_Add_button.TabIndex = 114;
            UserABS_Add_button.Text = "+";
            UserABS_Add_button.UseVisualStyleBackColor = false;
            UserABS_Add_button.Click += UserABS_Add_button_Click;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Controls.Add(DH_dataGridView);
            DH_tabPage.Controls.Add(label10);
            DH_tabPage.Controls.Add(label11);
            DH_tabPage.Controls.Add(DH_Save_button);
            DH_tabPage.Controls.Add(DefaultDH_Add_button);
            DH_tabPage.Controls.Add(DH_Copy_button);
            DH_tabPage.Controls.Add(DH_Remove_button);
            DH_tabPage.Controls.Add(UserDH_Add_button);
            DH_tabPage.Location = new Point(4, 25);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(1142, 226);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_dataGridView
            // 
            DH_dataGridView.AllowUserToAddRows = false;
            DH_dataGridView.AllowUserToDeleteRows = false;
            DH_dataGridView.AllowUserToResizeColumns = false;
            DH_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DH_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DH_dataGridView.BackgroundColor = SystemColors.Window;
            DH_dataGridView.BorderStyle = BorderStyle.None;
            DH_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DH_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle58.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle58.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle58.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle58.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle58.SelectionForeColor = Color.Black;
            dataGridViewCellStyle58.WrapMode = DataGridViewTriState.True;
            DH_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle58;
            DH_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DH_dataGridView.Location = new Point(19, 38);
            DH_dataGridView.Name = "DH_dataGridView";
            dataGridViewCellStyle59.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle59.BackColor = SystemColors.Control;
            dataGridViewCellStyle59.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle59.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle59.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle59.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle59.WrapMode = DataGridViewTriState.True;
            DH_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle59;
            DH_dataGridView.RowHeadersVisible = false;
            DH_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle60.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle60.ForeColor = Color.Black;
            dataGridViewCellStyle60.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle60.SelectionForeColor = Color.Black;
            DH_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle60;
            DH_dataGridView.RowTemplate.Height = 25;
            DH_dataGridView.Size = new Size(1105, 150);
            DH_dataGridView.TabIndex = 131;
            DH_dataGridView.CellContentClick += DH_dataGridView_CellContentClick;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(912, 11);
            label10.Name = "label10";
            label10.Size = new Size(87, 15);
            label10.TabIndex = 130;
            label10.Text = "도면 기반 입력";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(768, 11);
            label11.Name = "label11";
            label11.Size = new Size(104, 15);
            label11.TabIndex = 129;
            label11.Text = "기본 DB기반 입력";
            // 
            // DH_Save_button
            // 
            DH_Save_button.BackColor = SystemColors.ButtonHighlight;
            DH_Save_button.ForeColor = Color.Black;
            DH_Save_button.Location = new Point(1035, 194);
            DH_Save_button.Name = "DH_Save_button";
            DH_Save_button.Size = new Size(88, 25);
            DH_Save_button.TabIndex = 129;
            DH_Save_button.Text = "SAVE";
            DH_Save_button.UseVisualStyleBackColor = true;
            DH_Save_button.Click += DH_Save_button_Click;
            // 
            // DefaultDH_Add_button
            // 
            DefaultDH_Add_button.BackColor = SystemColors.ControlLight;
            DefaultDH_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultDH_Add_button.FlatStyle = FlatStyle.System;
            DefaultDH_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultDH_Add_button.Location = new Point(875, 7);
            DefaultDH_Add_button.Margin = new Padding(0);
            DefaultDH_Add_button.Name = "DefaultDH_Add_button";
            DefaultDH_Add_button.Size = new Size(23, 23);
            DefaultDH_Add_button.TabIndex = 126;
            DefaultDH_Add_button.Text = "+";
            DefaultDH_Add_button.UseVisualStyleBackColor = false;
            DefaultDH_Add_button.Click += DefaultDH_Add_button_Click;
            // 
            // DH_Copy_button
            // 
            DH_Copy_button.BackColor = SystemColors.ControlLight;
            DH_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DH_Copy_button.FlatStyle = FlatStyle.System;
            DH_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            DH_Copy_button.Location = new Point(1076, 7);
            DH_Copy_button.Margin = new Padding(0);
            DH_Copy_button.Name = "DH_Copy_button";
            DH_Copy_button.Size = new Size(47, 23);
            DH_Copy_button.TabIndex = 124;
            DH_Copy_button.Text = "Copy";
            DH_Copy_button.UseVisualStyleBackColor = false;
            DH_Copy_button.Click += DH_Copy_button_Click;
            // 
            // DH_Remove_button
            // 
            DH_Remove_button.BackColor = SystemColors.ControlLight;
            DH_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DH_Remove_button.FlatStyle = FlatStyle.System;
            DH_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DH_Remove_button.Location = new Point(1039, 7);
            DH_Remove_button.Margin = new Padding(0);
            DH_Remove_button.Name = "DH_Remove_button";
            DH_Remove_button.Size = new Size(23, 23);
            DH_Remove_button.TabIndex = 123;
            DH_Remove_button.Text = "-";
            DH_Remove_button.UseVisualStyleBackColor = false;
            DH_Remove_button.Click += DH_Remove_button_Click;
            // 
            // UserDH_Add_button
            // 
            UserDH_Add_button.BackColor = SystemColors.ControlLight;
            UserDH_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserDH_Add_button.FlatStyle = FlatStyle.System;
            UserDH_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserDH_Add_button.Location = new Point(1002, 7);
            UserDH_Add_button.Margin = new Padding(0);
            UserDH_Add_button.Name = "UserDH_Add_button";
            UserDH_Add_button.Size = new Size(23, 23);
            UserDH_Add_button.TabIndex = 122;
            UserDH_Add_button.Text = "+";
            UserDH_Add_button.UseVisualStyleBackColor = false;
            UserDH_Add_button.Click += UserDH_Add_button_Click;
            // 
            // AirCooler_tabPage
            // 
            AirCooler_tabPage.Controls.Add(label14);
            AirCooler_tabPage.Controls.Add(label15);
            AirCooler_tabPage.Controls.Add(AirCooler_dataGridView);
            AirCooler_tabPage.Controls.Add(AirCooler_Save_button);
            AirCooler_tabPage.Controls.Add(DefaultAirCooler_Add_button);
            AirCooler_tabPage.Controls.Add(AirCooler_Copy_button);
            AirCooler_tabPage.Controls.Add(AirCooler_Remove_button);
            AirCooler_tabPage.Controls.Add(UserAirCooler_Add_button);
            AirCooler_tabPage.Controls.Add(Solar_Save_button);
            AirCooler_tabPage.Location = new Point(4, 25);
            AirCooler_tabPage.Name = "AirCooler_tabPage";
            AirCooler_tabPage.Padding = new Padding(3);
            AirCooler_tabPage.Size = new Size(1142, 226);
            AirCooler_tabPage.TabIndex = 5;
            AirCooler_tabPage.Text = "공냉식냉동기";
            AirCooler_tabPage.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(912, 11);
            label14.Name = "label14";
            label14.Size = new Size(87, 15);
            label14.TabIndex = 123;
            label14.Text = "도면 기반 입력";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label15.Location = new Point(768, 11);
            label15.Name = "label15";
            label15.Size = new Size(104, 15);
            label15.TabIndex = 122;
            label15.Text = "기본 DB기반 입력";
            // 
            // AirCooler_dataGridView
            // 
            AirCooler_dataGridView.AllowUserToAddRows = false;
            AirCooler_dataGridView.AllowUserToDeleteRows = false;
            AirCooler_dataGridView.AllowUserToResizeColumns = false;
            AirCooler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AirCooler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AirCooler_dataGridView.BackgroundColor = SystemColors.Window;
            AirCooler_dataGridView.BorderStyle = BorderStyle.None;
            AirCooler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AirCooler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle61.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle61.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle61.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle61.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle61.SelectionForeColor = Color.Black;
            dataGridViewCellStyle61.WrapMode = DataGridViewTriState.True;
            AirCooler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle61;
            AirCooler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AirCooler_dataGridView.Location = new Point(18, 38);
            AirCooler_dataGridView.Name = "AirCooler_dataGridView";
            dataGridViewCellStyle62.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = SystemColors.Control;
            dataGridViewCellStyle62.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle62.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle62.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle62.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle62.WrapMode = DataGridViewTriState.True;
            AirCooler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle62;
            AirCooler_dataGridView.RowHeadersVisible = false;
            AirCooler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle63.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle63.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle63.ForeColor = Color.Black;
            dataGridViewCellStyle63.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle63.SelectionForeColor = Color.Black;
            AirCooler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle63;
            AirCooler_dataGridView.RowTemplate.Height = 25;
            AirCooler_dataGridView.Size = new Size(1105, 150);
            AirCooler_dataGridView.TabIndex = 120;
            AirCooler_dataGridView.CellValueChanged += AirCooler_dataGridView_CellValueChanged;
            // 
            // AirCooler_Save_button
            // 
            AirCooler_Save_button.BackColor = SystemColors.ButtonHighlight;
            AirCooler_Save_button.ForeColor = Color.Black;
            AirCooler_Save_button.Location = new Point(1035, 194);
            AirCooler_Save_button.Name = "AirCooler_Save_button";
            AirCooler_Save_button.Size = new Size(88, 25);
            AirCooler_Save_button.TabIndex = 121;
            AirCooler_Save_button.Text = "SAVE";
            AirCooler_Save_button.UseVisualStyleBackColor = true;
            AirCooler_Save_button.Click += AirCooler_Save_button_Click;
            // 
            // DefaultAirCooler_Add_button
            // 
            DefaultAirCooler_Add_button.BackColor = SystemColors.ControlLight;
            DefaultAirCooler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultAirCooler_Add_button.FlatStyle = FlatStyle.System;
            DefaultAirCooler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultAirCooler_Add_button.Location = new Point(875, 7);
            DefaultAirCooler_Add_button.Margin = new Padding(0);
            DefaultAirCooler_Add_button.Name = "DefaultAirCooler_Add_button";
            DefaultAirCooler_Add_button.Size = new Size(23, 23);
            DefaultAirCooler_Add_button.TabIndex = 119;
            DefaultAirCooler_Add_button.Text = "+";
            DefaultAirCooler_Add_button.UseVisualStyleBackColor = false;
            DefaultAirCooler_Add_button.Click += DefaultAirCooler_Add_button_Click;
            // 
            // AirCooler_Copy_button
            // 
            AirCooler_Copy_button.BackColor = SystemColors.ControlLight;
            AirCooler_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AirCooler_Copy_button.FlatStyle = FlatStyle.System;
            AirCooler_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            AirCooler_Copy_button.Location = new Point(1076, 7);
            AirCooler_Copy_button.Margin = new Padding(0);
            AirCooler_Copy_button.Name = "AirCooler_Copy_button";
            AirCooler_Copy_button.Size = new Size(47, 23);
            AirCooler_Copy_button.TabIndex = 118;
            AirCooler_Copy_button.Text = "Copy";
            AirCooler_Copy_button.UseVisualStyleBackColor = false;
            AirCooler_Copy_button.Click += AirCooler_Copy_button_Click;
            // 
            // AirCooler_Remove_button
            // 
            AirCooler_Remove_button.BackColor = SystemColors.ControlLight;
            AirCooler_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AirCooler_Remove_button.FlatStyle = FlatStyle.System;
            AirCooler_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AirCooler_Remove_button.Location = new Point(1039, 7);
            AirCooler_Remove_button.Margin = new Padding(0);
            AirCooler_Remove_button.Name = "AirCooler_Remove_button";
            AirCooler_Remove_button.Size = new Size(23, 23);
            AirCooler_Remove_button.TabIndex = 117;
            AirCooler_Remove_button.Text = "-";
            AirCooler_Remove_button.UseVisualStyleBackColor = false;
            AirCooler_Remove_button.Click += AirCooler_Remove_button_Click;
            // 
            // UserAirCooler_Add_button
            // 
            UserAirCooler_Add_button.BackColor = SystemColors.ControlLight;
            UserAirCooler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserAirCooler_Add_button.FlatStyle = FlatStyle.System;
            UserAirCooler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserAirCooler_Add_button.Location = new Point(1002, 7);
            UserAirCooler_Add_button.Margin = new Padding(0);
            UserAirCooler_Add_button.Name = "UserAirCooler_Add_button";
            UserAirCooler_Add_button.Size = new Size(23, 23);
            UserAirCooler_Add_button.TabIndex = 116;
            UserAirCooler_Add_button.Text = "+";
            UserAirCooler_Add_button.UseVisualStyleBackColor = false;
            UserAirCooler_Add_button.Click += UserAirCooler_Add_button_Click;
            // 
            // Solar_Save_button
            // 
            Solar_Save_button.BackColor = SystemColors.ButtonHighlight;
            Solar_Save_button.ForeColor = Color.Black;
            Solar_Save_button.Location = new Point(868, 571);
            Solar_Save_button.Name = "Solar_Save_button";
            Solar_Save_button.Size = new Size(88, 25);
            Solar_Save_button.TabIndex = 113;
            Solar_Save_button.Text = "SAVE";
            Solar_Save_button.UseVisualStyleBackColor = true;
            Solar_Save_button.Click += Solar_Save_button_Click;
            // 
            // WaterCooler_tabPage
            // 
            WaterCooler_tabPage.Controls.Add(label16);
            WaterCooler_tabPage.Controls.Add(label17);
            WaterCooler_tabPage.Controls.Add(WaterCooler_dataGridView);
            WaterCooler_tabPage.Controls.Add(WaterCooler_Save_button);
            WaterCooler_tabPage.Controls.Add(DefaultWaterCooler_Add_button);
            WaterCooler_tabPage.Controls.Add(WaterCooler_Copy_button);
            WaterCooler_tabPage.Controls.Add(WaterCooler_Remove_button);
            WaterCooler_tabPage.Controls.Add(UserWaterCooler_Add_button);
            WaterCooler_tabPage.Location = new Point(4, 25);
            WaterCooler_tabPage.Name = "WaterCooler_tabPage";
            WaterCooler_tabPage.Padding = new Padding(3);
            WaterCooler_tabPage.Size = new Size(1142, 226);
            WaterCooler_tabPage.TabIndex = 7;
            WaterCooler_tabPage.Text = "수냉식냉동기";
            WaterCooler_tabPage.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label16.Location = new Point(912, 11);
            label16.Name = "label16";
            label16.Size = new Size(87, 15);
            label16.TabIndex = 123;
            label16.Text = "도면 기반 입력";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label17.Location = new Point(768, 11);
            label17.Name = "label17";
            label17.Size = new Size(104, 15);
            label17.TabIndex = 122;
            label17.Text = "기본 DB기반 입력";
            // 
            // WaterCooler_dataGridView
            // 
            WaterCooler_dataGridView.AllowUserToAddRows = false;
            WaterCooler_dataGridView.AllowUserToDeleteRows = false;
            WaterCooler_dataGridView.AllowUserToResizeColumns = false;
            WaterCooler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            WaterCooler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            WaterCooler_dataGridView.BackgroundColor = SystemColors.Window;
            WaterCooler_dataGridView.BorderStyle = BorderStyle.None;
            WaterCooler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            WaterCooler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle64.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle64.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle64.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle64.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle64.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle64.SelectionForeColor = Color.Black;
            dataGridViewCellStyle64.WrapMode = DataGridViewTriState.True;
            WaterCooler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle64;
            WaterCooler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WaterCooler_dataGridView.Location = new Point(18, 38);
            WaterCooler_dataGridView.Name = "WaterCooler_dataGridView";
            dataGridViewCellStyle65.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle65.BackColor = SystemColors.Control;
            dataGridViewCellStyle65.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle65.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle65.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle65.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle65.WrapMode = DataGridViewTriState.True;
            WaterCooler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle65;
            WaterCooler_dataGridView.RowHeadersVisible = false;
            WaterCooler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle66.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle66.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle66.ForeColor = Color.Black;
            dataGridViewCellStyle66.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle66.SelectionForeColor = Color.Black;
            WaterCooler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle66;
            WaterCooler_dataGridView.RowTemplate.Height = 25;
            WaterCooler_dataGridView.Size = new Size(1105, 150);
            WaterCooler_dataGridView.TabIndex = 120;
            WaterCooler_dataGridView.CellValueChanged += WaterCooler_dataGridView_CellValueChanged;
            // 
            // WaterCooler_Save_button
            // 
            WaterCooler_Save_button.BackColor = SystemColors.ButtonHighlight;
            WaterCooler_Save_button.ForeColor = Color.Black;
            WaterCooler_Save_button.Location = new Point(1035, 194);
            WaterCooler_Save_button.Name = "WaterCooler_Save_button";
            WaterCooler_Save_button.Size = new Size(88, 25);
            WaterCooler_Save_button.TabIndex = 121;
            WaterCooler_Save_button.Text = "SAVE";
            WaterCooler_Save_button.UseVisualStyleBackColor = true;
            WaterCooler_Save_button.Click += WaterCooler_Save_button_Click;
            // 
            // DefaultWaterCooler_Add_button
            // 
            DefaultWaterCooler_Add_button.BackColor = SystemColors.ControlLight;
            DefaultWaterCooler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultWaterCooler_Add_button.FlatStyle = FlatStyle.System;
            DefaultWaterCooler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultWaterCooler_Add_button.Location = new Point(875, 7);
            DefaultWaterCooler_Add_button.Margin = new Padding(0);
            DefaultWaterCooler_Add_button.Name = "DefaultWaterCooler_Add_button";
            DefaultWaterCooler_Add_button.Size = new Size(23, 23);
            DefaultWaterCooler_Add_button.TabIndex = 119;
            DefaultWaterCooler_Add_button.Text = "+";
            DefaultWaterCooler_Add_button.UseVisualStyleBackColor = false;
            DefaultWaterCooler_Add_button.Click += DefaultWaterCooler_Add_button_Click;
            // 
            // WaterCooler_Copy_button
            // 
            WaterCooler_Copy_button.BackColor = SystemColors.ControlLight;
            WaterCooler_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WaterCooler_Copy_button.FlatStyle = FlatStyle.System;
            WaterCooler_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            WaterCooler_Copy_button.Location = new Point(1076, 7);
            WaterCooler_Copy_button.Margin = new Padding(0);
            WaterCooler_Copy_button.Name = "WaterCooler_Copy_button";
            WaterCooler_Copy_button.Size = new Size(47, 23);
            WaterCooler_Copy_button.TabIndex = 118;
            WaterCooler_Copy_button.Text = "Copy";
            WaterCooler_Copy_button.UseVisualStyleBackColor = false;
            WaterCooler_Copy_button.Click += WaterCooler_Copy_button_Click;
            // 
            // WaterCooler_Remove_button
            // 
            WaterCooler_Remove_button.BackColor = SystemColors.ControlLight;
            WaterCooler_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WaterCooler_Remove_button.FlatStyle = FlatStyle.System;
            WaterCooler_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            WaterCooler_Remove_button.Location = new Point(1039, 7);
            WaterCooler_Remove_button.Margin = new Padding(0);
            WaterCooler_Remove_button.Name = "WaterCooler_Remove_button";
            WaterCooler_Remove_button.Size = new Size(23, 23);
            WaterCooler_Remove_button.TabIndex = 117;
            WaterCooler_Remove_button.Text = "-";
            WaterCooler_Remove_button.UseVisualStyleBackColor = false;
            WaterCooler_Remove_button.Click += WaterCooler_Remove_button_Click;
            // 
            // UserWaterCooler_Add_button
            // 
            UserWaterCooler_Add_button.BackColor = SystemColors.ControlLight;
            UserWaterCooler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserWaterCooler_Add_button.FlatStyle = FlatStyle.System;
            UserWaterCooler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserWaterCooler_Add_button.Location = new Point(1002, 7);
            UserWaterCooler_Add_button.Margin = new Padding(0);
            UserWaterCooler_Add_button.Name = "UserWaterCooler_Add_button";
            UserWaterCooler_Add_button.Size = new Size(23, 23);
            UserWaterCooler_Add_button.TabIndex = 116;
            UserWaterCooler_Add_button.Text = "+";
            UserWaterCooler_Add_button.UseVisualStyleBackColor = false;
            UserWaterCooler_Add_button.Click += UserWaterCooler_Add_button_Click;
            // 
            // Pump_Save_button
            // 
            Pump_Save_button.BackColor = SystemColors.ButtonHighlight;
            Pump_Save_button.ForeColor = Color.Black;
            Pump_Save_button.Location = new Point(1035, 197);
            Pump_Save_button.Name = "Pump_Save_button";
            Pump_Save_button.Size = new Size(88, 25);
            Pump_Save_button.TabIndex = 110;
            Pump_Save_button.Text = "SAVE";
            Pump_Save_button.UseVisualStyleBackColor = true;
            Pump_Save_button.Click += Pump_Save_button_Click;
            // 
            // Pump_dataGridView
            // 
            Pump_dataGridView.AllowUserToAddRows = false;
            Pump_dataGridView.AllowUserToDeleteRows = false;
            Pump_dataGridView.AllowUserToResizeColumns = false;
            Pump_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Pump_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Pump_dataGridView.BackgroundColor = SystemColors.Window;
            Pump_dataGridView.BorderStyle = BorderStyle.None;
            Pump_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Pump_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle67.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle67.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle67.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle67.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle67.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle67.SelectionForeColor = Color.Black;
            dataGridViewCellStyle67.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle67;
            Pump_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Pump_dataGridView.Location = new Point(18, 39);
            Pump_dataGridView.Name = "Pump_dataGridView";
            dataGridViewCellStyle68.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle68.BackColor = SystemColors.Control;
            dataGridViewCellStyle68.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle68.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle68.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle68.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle68.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle68;
            Pump_dataGridView.RowHeadersVisible = false;
            Pump_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle69.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle69.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle69.ForeColor = Color.Black;
            dataGridViewCellStyle69.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle69.SelectionForeColor = Color.Black;
            Pump_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle69;
            Pump_dataGridView.RowTemplate.Height = 25;
            Pump_dataGridView.Size = new Size(1105, 150);
            Pump_dataGridView.TabIndex = 109;
            Pump_dataGridView.CellContentClick += Pump_dataGridView_CellContentClick;
            Pump_dataGridView.CellValueChanged += Pump_dataGridView_CellValueChanged;
            // 
            // Pump_Copy_button
            // 
            Pump_Copy_button.BackColor = SystemColors.ControlLight;
            Pump_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump_Copy_button.FlatStyle = FlatStyle.System;
            Pump_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Pump_Copy_button.Location = new Point(1076, 11);
            Pump_Copy_button.Margin = new Padding(0);
            Pump_Copy_button.Name = "Pump_Copy_button";
            Pump_Copy_button.Size = new Size(47, 23);
            Pump_Copy_button.TabIndex = 108;
            Pump_Copy_button.Text = "Copy";
            Pump_Copy_button.UseVisualStyleBackColor = false;
            Pump_Copy_button.Click += Pump_Copy_button_Click;
            // 
            // Pump_Remove_button
            // 
            Pump_Remove_button.BackColor = SystemColors.ControlLight;
            Pump_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump_Remove_button.FlatStyle = FlatStyle.System;
            Pump_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Pump_Remove_button.Location = new Point(1039, 11);
            Pump_Remove_button.Margin = new Padding(0);
            Pump_Remove_button.Name = "Pump_Remove_button";
            Pump_Remove_button.Size = new Size(23, 23);
            Pump_Remove_button.TabIndex = 107;
            Pump_Remove_button.Text = "-";
            Pump_Remove_button.UseVisualStyleBackColor = false;
            Pump_Remove_button.Click += Pump_Remove_button_Click;
            // 
            // Pump_Add_button
            // 
            Pump_Add_button.BackColor = SystemColors.ControlLight;
            Pump_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump_Add_button.FlatStyle = FlatStyle.System;
            Pump_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Pump_Add_button.Location = new Point(1002, 11);
            Pump_Add_button.Margin = new Padding(0);
            Pump_Add_button.Name = "Pump_Add_button";
            Pump_Add_button.Size = new Size(23, 23);
            Pump_Add_button.TabIndex = 106;
            Pump_Add_button.Text = "+";
            Pump_Add_button.UseVisualStyleBackColor = false;
            Pump_Add_button.Click += Pump_Add_button_Click;
            // 
            // ce_Save_button
            // 
            ce_Save_button.BackColor = SystemColors.ButtonHighlight;
            ce_Save_button.ForeColor = Color.Black;
            ce_Save_button.Location = new Point(1035, 195);
            ce_Save_button.Name = "ce_Save_button";
            ce_Save_button.Size = new Size(88, 25);
            ce_Save_button.TabIndex = 115;
            ce_Save_button.Text = "SAVE";
            ce_Save_button.UseVisualStyleBackColor = true;
            ce_Save_button.Click += ce_Save_button_Click;
            // 
            // ce_dataGridView
            // 
            ce_dataGridView.AllowUserToAddRows = false;
            ce_dataGridView.AllowUserToDeleteRows = false;
            ce_dataGridView.AllowUserToResizeColumns = false;
            ce_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ce_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ce_dataGridView.BackgroundColor = SystemColors.Window;
            ce_dataGridView.BorderStyle = BorderStyle.None;
            ce_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ce_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle70.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle70.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle70.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle70.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle70.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle70.SelectionForeColor = Color.Black;
            dataGridViewCellStyle70.WrapMode = DataGridViewTriState.True;
            ce_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle70;
            ce_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ce_dataGridView.Location = new Point(18, 40);
            ce_dataGridView.Name = "ce_dataGridView";
            dataGridViewCellStyle71.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle71.BackColor = SystemColors.Control;
            dataGridViewCellStyle71.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle71.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle71.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle71.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle71.WrapMode = DataGridViewTriState.True;
            ce_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle71;
            ce_dataGridView.RowHeadersVisible = false;
            ce_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle72.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle72.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle72.ForeColor = Color.Black;
            dataGridViewCellStyle72.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle72.SelectionForeColor = Color.Black;
            ce_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle72;
            ce_dataGridView.RowTemplate.Height = 25;
            ce_dataGridView.Size = new Size(1105, 150);
            ce_dataGridView.TabIndex = 114;
            ce_dataGridView.CellValueChanged += ce_dataGridView_CellValueChanged;
            // 
            // ce_Copy_button
            // 
            ce_Copy_button.BackColor = SystemColors.ControlLight;
            ce_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ce_Copy_button.FlatStyle = FlatStyle.System;
            ce_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            ce_Copy_button.Location = new Point(1076, 14);
            ce_Copy_button.Margin = new Padding(0);
            ce_Copy_button.Name = "ce_Copy_button";
            ce_Copy_button.Size = new Size(47, 23);
            ce_Copy_button.TabIndex = 113;
            ce_Copy_button.Text = "Copy";
            ce_Copy_button.UseVisualStyleBackColor = false;
            ce_Copy_button.Click += ce_Copy_button_Click;
            // 
            // ce_Remove_button
            // 
            ce_Remove_button.BackColor = SystemColors.ControlLight;
            ce_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ce_Remove_button.FlatStyle = FlatStyle.System;
            ce_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            ce_Remove_button.Location = new Point(1039, 14);
            ce_Remove_button.Margin = new Padding(0);
            ce_Remove_button.Name = "ce_Remove_button";
            ce_Remove_button.Size = new Size(23, 23);
            ce_Remove_button.TabIndex = 112;
            ce_Remove_button.Text = "-";
            ce_Remove_button.UseVisualStyleBackColor = false;
            ce_Remove_button.Click += ce_Remove_button_Click;
            // 
            // ce_Add_button
            // 
            ce_Add_button.BackColor = SystemColors.ControlLight;
            ce_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ce_Add_button.FlatStyle = FlatStyle.System;
            ce_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            ce_Add_button.Location = new Point(1002, 14);
            ce_Add_button.Margin = new Padding(0);
            ce_Add_button.Name = "ce_Add_button";
            ce_Add_button.Size = new Size(23, 23);
            ce_Add_button.TabIndex = 111;
            ce_Add_button.Text = "+";
            ce_Add_button.UseVisualStyleBackColor = false;
            ce_Add_button.Click += ce_Add_button_Click;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(WP_dataGridView);
            tabPage6.Controls.Add(WP_Save_button);
            tabPage6.Controls.Add(WP_Copy_button);
            tabPage6.Controls.Add(WP_Remove_button);
            tabPage6.Controls.Add(UserWP_Add_button);
            tabPage6.Controls.Add(button15);
            tabPage6.Location = new Point(4, 25);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(1142, 226);
            tabPage6.TabIndex = 7;
            tabPage6.Text = "소형풍력";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // WP_dataGridView
            // 
            WP_dataGridView.AllowUserToAddRows = false;
            WP_dataGridView.AllowUserToDeleteRows = false;
            WP_dataGridView.AllowUserToResizeColumns = false;
            WP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            WP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            WP_dataGridView.BackgroundColor = SystemColors.Window;
            WP_dataGridView.BorderStyle = BorderStyle.None;
            WP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            WP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle73.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle73.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle73.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle73.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle73.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle73.SelectionForeColor = Color.Black;
            dataGridViewCellStyle73.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle73;
            WP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WP_dataGridView.Location = new Point(18, 36);
            WP_dataGridView.Name = "WP_dataGridView";
            dataGridViewCellStyle74.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle74.BackColor = SystemColors.Control;
            dataGridViewCellStyle74.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle74.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle74.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle74.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle74.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle74;
            WP_dataGridView.RowHeadersVisible = false;
            WP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle75.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle75.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle75.ForeColor = Color.Black;
            dataGridViewCellStyle75.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle75.SelectionForeColor = Color.Black;
            WP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle75;
            WP_dataGridView.RowTemplate.Height = 25;
            WP_dataGridView.Size = new Size(1105, 150);
            WP_dataGridView.TabIndex = 128;
            WP_dataGridView.CellContentClick += WP_dataGridView_CellContentClick;
            WP_dataGridView.CellValueChanged += WP_dataGridView_CellValueChanged;
            // 
            // WP_Save_button
            // 
            WP_Save_button.BackColor = SystemColors.ButtonHighlight;
            WP_Save_button.ForeColor = Color.Black;
            WP_Save_button.Location = new Point(1035, 192);
            WP_Save_button.Name = "WP_Save_button";
            WP_Save_button.Size = new Size(88, 25);
            WP_Save_button.TabIndex = 129;
            WP_Save_button.Text = "SAVE";
            WP_Save_button.UseVisualStyleBackColor = true;
            WP_Save_button.Click += WP_Save_button_Click;
            // 
            // WP_Copy_button
            // 
            WP_Copy_button.BackColor = SystemColors.ControlLight;
            WP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WP_Copy_button.FlatStyle = FlatStyle.System;
            WP_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            WP_Copy_button.Location = new Point(1076, 9);
            WP_Copy_button.Margin = new Padding(0);
            WP_Copy_button.Name = "WP_Copy_button";
            WP_Copy_button.Size = new Size(47, 23);
            WP_Copy_button.TabIndex = 124;
            WP_Copy_button.Text = "Copy";
            WP_Copy_button.UseVisualStyleBackColor = false;
            WP_Copy_button.Click += WP_Copy_button_Click;
            // 
            // WP_Remove_button
            // 
            WP_Remove_button.BackColor = SystemColors.ControlLight;
            WP_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WP_Remove_button.FlatStyle = FlatStyle.System;
            WP_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            WP_Remove_button.Location = new Point(1039, 9);
            WP_Remove_button.Margin = new Padding(0);
            WP_Remove_button.Name = "WP_Remove_button";
            WP_Remove_button.Size = new Size(23, 23);
            WP_Remove_button.TabIndex = 123;
            WP_Remove_button.Text = "-";
            WP_Remove_button.UseVisualStyleBackColor = false;
            WP_Remove_button.Click += WP_Remove_button_Click;
            // 
            // UserWP_Add_button
            // 
            UserWP_Add_button.BackColor = SystemColors.ControlLight;
            UserWP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserWP_Add_button.FlatStyle = FlatStyle.System;
            UserWP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserWP_Add_button.Location = new Point(1002, 9);
            UserWP_Add_button.Margin = new Padding(0);
            UserWP_Add_button.Name = "UserWP_Add_button";
            UserWP_Add_button.Size = new Size(23, 23);
            UserWP_Add_button.TabIndex = 122;
            UserWP_Add_button.Text = "+";
            UserWP_Add_button.UseVisualStyleBackColor = false;
            UserWP_Add_button.Click += UserWP_Add_button_Click;
            // 
            // button15
            // 
            button15.BackColor = SystemColors.ButtonHighlight;
            button15.ForeColor = Color.Black;
            button15.Location = new Point(862, 568);
            button15.Name = "button15";
            button15.Size = new Size(88, 25);
            button15.TabIndex = 110;
            button15.Text = "SAVE";
            button15.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(label12);
            tabPage5.Controls.Add(label13);
            tabPage5.Controls.Add(FC_dataGridView);
            tabPage5.Controls.Add(FC_Save_button);
            tabPage5.Controls.Add(DefaultFC_Add_button);
            tabPage5.Controls.Add(FC_Copy_button);
            tabPage5.Controls.Add(FC_Remove_button);
            tabPage5.Controls.Add(UserFC_Add_button);
            tabPage5.Location = new Point(4, 25);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1142, 226);
            tabPage5.TabIndex = 5;
            tabPage5.Text = "연료전지";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(912, 11);
            label12.Name = "label12";
            label12.Size = new Size(87, 15);
            label12.TabIndex = 132;
            label12.Text = "도면 기반 입력";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(768, 11);
            label13.Name = "label13";
            label13.Size = new Size(104, 15);
            label13.TabIndex = 131;
            label13.Text = "기본 DB기반 입력";
            // 
            // FC_dataGridView
            // 
            FC_dataGridView.AllowUserToAddRows = false;
            FC_dataGridView.AllowUserToDeleteRows = false;
            FC_dataGridView.AllowUserToResizeColumns = false;
            FC_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FC_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            FC_dataGridView.BackgroundColor = SystemColors.Window;
            FC_dataGridView.BorderStyle = BorderStyle.None;
            FC_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            FC_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle76.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle76.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle76.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle76.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle76.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle76.SelectionForeColor = Color.Black;
            dataGridViewCellStyle76.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle76;
            FC_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FC_dataGridView.Location = new Point(18, 33);
            FC_dataGridView.Name = "FC_dataGridView";
            dataGridViewCellStyle77.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle77.BackColor = SystemColors.Control;
            dataGridViewCellStyle77.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle77.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle77.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle77.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle77.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle77;
            FC_dataGridView.RowHeadersVisible = false;
            FC_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle78.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle78.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle78.ForeColor = Color.Black;
            dataGridViewCellStyle78.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle78.SelectionForeColor = Color.Black;
            FC_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle78;
            FC_dataGridView.RowTemplate.Height = 25;
            FC_dataGridView.Size = new Size(1105, 150);
            FC_dataGridView.TabIndex = 128;
            FC_dataGridView.CellContentClick += FC_dataGridView_CellContentClick;
            // 
            // FC_Save_button
            // 
            FC_Save_button.BackColor = SystemColors.ButtonHighlight;
            FC_Save_button.ForeColor = Color.Black;
            FC_Save_button.Location = new Point(1035, 189);
            FC_Save_button.Name = "FC_Save_button";
            FC_Save_button.Size = new Size(88, 25);
            FC_Save_button.TabIndex = 129;
            FC_Save_button.Text = "SAVE";
            FC_Save_button.UseVisualStyleBackColor = true;
            FC_Save_button.Click += FC_Save_button_Click;
            // 
            // DefaultFC_Add_button
            // 
            DefaultFC_Add_button.BackColor = SystemColors.ControlLight;
            DefaultFC_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultFC_Add_button.FlatStyle = FlatStyle.System;
            DefaultFC_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultFC_Add_button.Location = new Point(875, 7);
            DefaultFC_Add_button.Margin = new Padding(0);
            DefaultFC_Add_button.Name = "DefaultFC_Add_button";
            DefaultFC_Add_button.Size = new Size(23, 23);
            DefaultFC_Add_button.TabIndex = 126;
            DefaultFC_Add_button.Text = "+";
            DefaultFC_Add_button.UseVisualStyleBackColor = false;
            DefaultFC_Add_button.Click += DefaultFC_Add_button_Click;
            // 
            // FC_Copy_button
            // 
            FC_Copy_button.BackColor = SystemColors.ControlLight;
            FC_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FC_Copy_button.FlatStyle = FlatStyle.System;
            FC_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            FC_Copy_button.Location = new Point(1076, 7);
            FC_Copy_button.Margin = new Padding(0);
            FC_Copy_button.Name = "FC_Copy_button";
            FC_Copy_button.Size = new Size(47, 23);
            FC_Copy_button.TabIndex = 124;
            FC_Copy_button.Text = "Copy";
            FC_Copy_button.UseVisualStyleBackColor = false;
            FC_Copy_button.Click += FC_Copy_button_Click;
            // 
            // FC_Remove_button
            // 
            FC_Remove_button.BackColor = SystemColors.ControlLight;
            FC_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FC_Remove_button.FlatStyle = FlatStyle.System;
            FC_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            FC_Remove_button.Location = new Point(1039, 7);
            FC_Remove_button.Margin = new Padding(0);
            FC_Remove_button.Name = "FC_Remove_button";
            FC_Remove_button.Size = new Size(23, 23);
            FC_Remove_button.TabIndex = 123;
            FC_Remove_button.Text = "-";
            FC_Remove_button.UseVisualStyleBackColor = false;
            FC_Remove_button.Click += FC_Remove_button_Click;
            // 
            // UserFC_Add_button
            // 
            UserFC_Add_button.BackColor = SystemColors.ControlLight;
            UserFC_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserFC_Add_button.FlatStyle = FlatStyle.System;
            UserFC_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserFC_Add_button.Location = new Point(1002, 7);
            UserFC_Add_button.Margin = new Padding(0);
            UserFC_Add_button.Name = "UserFC_Add_button";
            UserFC_Add_button.Size = new Size(23, 23);
            UserFC_Add_button.TabIndex = 122;
            UserFC_Add_button.Text = "+";
            UserFC_Add_button.UseVisualStyleBackColor = false;
            UserFC_Add_button.Click += UserFC_Add_button_Click;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(GWHP_Save_button);
            tabPage4.Controls.Add(GWHP_dataGridView);
            tabPage4.Controls.Add(UserGWHP_Add_button);
            tabPage4.Controls.Add(GWHP_Remove_button);
            tabPage4.Controls.Add(GWHP_Copy_button);
            tabPage4.Location = new Point(4, 25);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1142, 226);
            tabPage4.TabIndex = 4;
            tabPage4.Text = "지하수히트펌프";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // GWHP_Save_button
            // 
            GWHP_Save_button.BackColor = SystemColors.ButtonHighlight;
            GWHP_Save_button.ForeColor = Color.Black;
            GWHP_Save_button.Location = new Point(1035, 193);
            GWHP_Save_button.Name = "GWHP_Save_button";
            GWHP_Save_button.Size = new Size(88, 25);
            GWHP_Save_button.TabIndex = 130;
            GWHP_Save_button.Text = "SAVE";
            GWHP_Save_button.UseVisualStyleBackColor = true;
            GWHP_Save_button.Click += GWHP_Save_button_Click;
            // 
            // GWHP_dataGridView
            // 
            GWHP_dataGridView.AllowUserToAddRows = false;
            GWHP_dataGridView.AllowUserToDeleteRows = false;
            GWHP_dataGridView.AllowUserToResizeColumns = false;
            GWHP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GWHP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            GWHP_dataGridView.BackgroundColor = SystemColors.Window;
            GWHP_dataGridView.BorderStyle = BorderStyle.None;
            GWHP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            GWHP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle79.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle79.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle79.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle79.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle79.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle79.SelectionForeColor = Color.Black;
            dataGridViewCellStyle79.WrapMode = DataGridViewTriState.True;
            GWHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle79;
            GWHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GWHP_dataGridView.Location = new Point(25, 37);
            GWHP_dataGridView.Name = "GWHP_dataGridView";
            dataGridViewCellStyle80.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle80.BackColor = SystemColors.Control;
            dataGridViewCellStyle80.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle80.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle80.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle80.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle80.WrapMode = DataGridViewTriState.True;
            GWHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle80;
            GWHP_dataGridView.RowHeadersVisible = false;
            GWHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle81.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle81.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle81.ForeColor = Color.Black;
            dataGridViewCellStyle81.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle81.SelectionForeColor = Color.Black;
            GWHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle81;
            GWHP_dataGridView.RowTemplate.Height = 25;
            GWHP_dataGridView.Size = new Size(1105, 150);
            GWHP_dataGridView.TabIndex = 129;
            GWHP_dataGridView.CellContentClick += GWHP_dataGridView_CellContentClick;
            GWHP_dataGridView.CellValueChanged += GWHP_dataGridView_CellValueChanged;
            // 
            // UserGWHP_Add_button
            // 
            UserGWHP_Add_button.BackColor = SystemColors.ControlLight;
            UserGWHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserGWHP_Add_button.FlatStyle = FlatStyle.System;
            UserGWHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserGWHP_Add_button.Location = new Point(1002, 11);
            UserGWHP_Add_button.Margin = new Padding(0);
            UserGWHP_Add_button.Name = "UserGWHP_Add_button";
            UserGWHP_Add_button.Size = new Size(23, 23);
            UserGWHP_Add_button.TabIndex = 123;
            UserGWHP_Add_button.Text = "+";
            UserGWHP_Add_button.UseVisualStyleBackColor = false;
            UserGWHP_Add_button.Click += UserGWHP_Add_button_Click;
            // 
            // GWHP_Remove_button
            // 
            GWHP_Remove_button.BackColor = SystemColors.ControlLight;
            GWHP_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GWHP_Remove_button.FlatStyle = FlatStyle.System;
            GWHP_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            GWHP_Remove_button.Location = new Point(1039, 11);
            GWHP_Remove_button.Margin = new Padding(0);
            GWHP_Remove_button.Name = "GWHP_Remove_button";
            GWHP_Remove_button.Size = new Size(23, 23);
            GWHP_Remove_button.TabIndex = 124;
            GWHP_Remove_button.Text = "-";
            GWHP_Remove_button.UseVisualStyleBackColor = false;
            GWHP_Remove_button.Click += GWHP_Remove_button_Click;
            // 
            // GWHP_Copy_button
            // 
            GWHP_Copy_button.BackColor = SystemColors.ControlLight;
            GWHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GWHP_Copy_button.FlatStyle = FlatStyle.System;
            GWHP_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            GWHP_Copy_button.Location = new Point(1076, 11);
            GWHP_Copy_button.Margin = new Padding(0);
            GWHP_Copy_button.Name = "GWHP_Copy_button";
            GWHP_Copy_button.Size = new Size(47, 23);
            GWHP_Copy_button.TabIndex = 125;
            GWHP_Copy_button.Text = "Copy";
            GWHP_Copy_button.UseVisualStyleBackColor = false;
            GWHP_Copy_button.Click += GWHP_Copy_button_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(GroundHP_Save_button);
            tabPage3.Controls.Add(GroundHP_dataGridView);
            tabPage3.Controls.Add(GroundHP_Copy_button);
            tabPage3.Controls.Add(GroundHP_Remove_button);
            tabPage3.Controls.Add(UserGroundHP_Add_button);
            tabPage3.Location = new Point(4, 25);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1142, 226);
            tabPage3.TabIndex = 3;
            tabPage3.Text = "지열히트펌프";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // GroundHP_Save_button
            // 
            GroundHP_Save_button.BackColor = SystemColors.ButtonHighlight;
            GroundHP_Save_button.ForeColor = Color.Black;
            GroundHP_Save_button.Location = new Point(1035, 195);
            GroundHP_Save_button.Name = "GroundHP_Save_button";
            GroundHP_Save_button.Size = new Size(88, 25);
            GroundHP_Save_button.TabIndex = 122;
            GroundHP_Save_button.Text = "SAVE";
            GroundHP_Save_button.UseVisualStyleBackColor = true;
            GroundHP_Save_button.Click += GroundHP_Save_button_Click;
            // 
            // GroundHP_dataGridView
            // 
            GroundHP_dataGridView.AllowUserToAddRows = false;
            GroundHP_dataGridView.AllowUserToDeleteRows = false;
            GroundHP_dataGridView.AllowUserToResizeColumns = false;
            GroundHP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GroundHP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            GroundHP_dataGridView.BackgroundColor = SystemColors.Window;
            GroundHP_dataGridView.BorderStyle = BorderStyle.None;
            GroundHP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            GroundHP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle82.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle82.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle82.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle82.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle82.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle82.SelectionForeColor = Color.Black;
            dataGridViewCellStyle82.WrapMode = DataGridViewTriState.True;
            GroundHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle82;
            GroundHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GroundHP_dataGridView.Location = new Point(25, 38);
            GroundHP_dataGridView.Name = "GroundHP_dataGridView";
            dataGridViewCellStyle83.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle83.BackColor = SystemColors.Control;
            dataGridViewCellStyle83.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle83.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle83.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle83.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle83.WrapMode = DataGridViewTriState.True;
            GroundHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle83;
            GroundHP_dataGridView.RowHeadersVisible = false;
            GroundHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle84.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle84.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle84.ForeColor = Color.Black;
            dataGridViewCellStyle84.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle84.SelectionForeColor = Color.Black;
            GroundHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle84;
            GroundHP_dataGridView.RowTemplate.Height = 25;
            GroundHP_dataGridView.Size = new Size(1105, 150);
            GroundHP_dataGridView.TabIndex = 121;
            GroundHP_dataGridView.CellContentClick += GroundHP_dataGridView_CellContentClick;
            GroundHP_dataGridView.CellValueChanged += GroundHP_dataGridView_CellValueChanged;
            // 
            // GroundHP_Copy_button
            // 
            GroundHP_Copy_button.BackColor = SystemColors.ControlLight;
            GroundHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GroundHP_Copy_button.FlatStyle = FlatStyle.System;
            GroundHP_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            GroundHP_Copy_button.Location = new Point(1076, 12);
            GroundHP_Copy_button.Margin = new Padding(0);
            GroundHP_Copy_button.Name = "GroundHP_Copy_button";
            GroundHP_Copy_button.Size = new Size(47, 23);
            GroundHP_Copy_button.TabIndex = 117;
            GroundHP_Copy_button.Text = "Copy";
            GroundHP_Copy_button.UseVisualStyleBackColor = false;
            GroundHP_Copy_button.Click += GroundHP_Copy_button_Click;
            // 
            // GroundHP_Remove_button
            // 
            GroundHP_Remove_button.BackColor = SystemColors.ControlLight;
            GroundHP_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GroundHP_Remove_button.FlatStyle = FlatStyle.System;
            GroundHP_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            GroundHP_Remove_button.Location = new Point(1039, 12);
            GroundHP_Remove_button.Margin = new Padding(0);
            GroundHP_Remove_button.Name = "GroundHP_Remove_button";
            GroundHP_Remove_button.Size = new Size(23, 23);
            GroundHP_Remove_button.TabIndex = 116;
            GroundHP_Remove_button.Text = "-";
            GroundHP_Remove_button.UseVisualStyleBackColor = false;
            GroundHP_Remove_button.Click += GroundHP_Remove_button_Click;
            // 
            // UserGroundHP_Add_button
            // 
            UserGroundHP_Add_button.BackColor = SystemColors.ControlLight;
            UserGroundHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserGroundHP_Add_button.FlatStyle = FlatStyle.System;
            UserGroundHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserGroundHP_Add_button.Location = new Point(1002, 12);
            UserGroundHP_Add_button.Margin = new Padding(0);
            UserGroundHP_Add_button.Name = "UserGroundHP_Add_button";
            UserGroundHP_Add_button.Size = new Size(23, 23);
            UserGroundHP_Add_button.TabIndex = 115;
            UserGroundHP_Add_button.Text = "+";
            UserGroundHP_Add_button.UseVisualStyleBackColor = false;
            UserGroundHP_Add_button.Click += UserGroundHP_Add_button_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(button6);
            tabPage2.Controls.Add(DefaultSolar_Add_button);
            tabPage2.Controls.Add(UserSolar_Add_button);
            tabPage2.Controls.Add(Solar_Remove_button);
            tabPage2.Controls.Add(Solar_dataGridView);
            tabPage2.Controls.Add(Solar_Copy_button);
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1142, 226);
            tabPage2.TabIndex = 6;
            tabPage2.Text = "태양열시스템";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(912, 11);
            label6.Name = "label6";
            label6.Size = new Size(87, 15);
            label6.TabIndex = 125;
            label6.Text = "도면 기반 입력";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(768, 11);
            label7.Name = "label7";
            label7.Size = new Size(104, 15);
            label7.TabIndex = 124;
            label7.Text = "기본 DB기반 입력";
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ButtonHighlight;
            button6.ForeColor = Color.Black;
            button6.Location = new Point(1035, 189);
            button6.Name = "button6";
            button6.Size = new Size(88, 25);
            button6.TabIndex = 102;
            button6.Text = "SAVE";
            button6.UseVisualStyleBackColor = true;
            // 
            // DefaultSolar_Add_button
            // 
            DefaultSolar_Add_button.BackColor = SystemColors.ControlLight;
            DefaultSolar_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultSolar_Add_button.FlatStyle = FlatStyle.System;
            DefaultSolar_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultSolar_Add_button.Location = new Point(875, 7);
            DefaultSolar_Add_button.Margin = new Padding(0);
            DefaultSolar_Add_button.Name = "DefaultSolar_Add_button";
            DefaultSolar_Add_button.Size = new Size(23, 23);
            DefaultSolar_Add_button.TabIndex = 111;
            DefaultSolar_Add_button.Text = "+";
            DefaultSolar_Add_button.UseVisualStyleBackColor = false;
            DefaultSolar_Add_button.Click += DefaultSolar_Add_button_Click;
            // 
            // UserSolar_Add_button
            // 
            UserSolar_Add_button.BackColor = SystemColors.ControlLight;
            UserSolar_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserSolar_Add_button.FlatStyle = FlatStyle.System;
            UserSolar_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserSolar_Add_button.Location = new Point(1002, 7);
            UserSolar_Add_button.Margin = new Padding(0);
            UserSolar_Add_button.Name = "UserSolar_Add_button";
            UserSolar_Add_button.Size = new Size(23, 23);
            UserSolar_Add_button.TabIndex = 106;
            UserSolar_Add_button.Text = "+";
            UserSolar_Add_button.UseVisualStyleBackColor = false;
            UserSolar_Add_button.Click += UserSolar_Add_button_Click;
            // 
            // Solar_Remove_button
            // 
            Solar_Remove_button.BackColor = SystemColors.ControlLight;
            Solar_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Solar_Remove_button.FlatStyle = FlatStyle.System;
            Solar_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Solar_Remove_button.Location = new Point(1039, 7);
            Solar_Remove_button.Margin = new Padding(0);
            Solar_Remove_button.Name = "Solar_Remove_button";
            Solar_Remove_button.Size = new Size(23, 23);
            Solar_Remove_button.TabIndex = 107;
            Solar_Remove_button.Text = "-";
            Solar_Remove_button.UseVisualStyleBackColor = false;
            Solar_Remove_button.Click += Solar_Remove_button_Click;
            // 
            // Solar_dataGridView
            // 
            Solar_dataGridView.AllowUserToAddRows = false;
            Solar_dataGridView.AllowUserToDeleteRows = false;
            Solar_dataGridView.AllowUserToResizeColumns = false;
            Solar_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Solar_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Solar_dataGridView.BackgroundColor = SystemColors.Window;
            Solar_dataGridView.BorderStyle = BorderStyle.None;
            Solar_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Solar_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle85.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle85.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle85.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle85.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle85.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle85.SelectionForeColor = Color.Black;
            dataGridViewCellStyle85.WrapMode = DataGridViewTriState.True;
            Solar_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle85;
            Solar_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Solar_dataGridView.Location = new Point(14, 33);
            Solar_dataGridView.Name = "Solar_dataGridView";
            dataGridViewCellStyle86.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle86.BackColor = SystemColors.Control;
            dataGridViewCellStyle86.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle86.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle86.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle86.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle86.WrapMode = DataGridViewTriState.True;
            Solar_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle86;
            Solar_dataGridView.RowHeadersVisible = false;
            Solar_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle87.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle87.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle87.ForeColor = Color.Black;
            dataGridViewCellStyle87.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle87.SelectionForeColor = Color.Black;
            Solar_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle87;
            Solar_dataGridView.RowTemplate.Height = 25;
            Solar_dataGridView.Size = new Size(1105, 150);
            Solar_dataGridView.TabIndex = 109;
            Solar_dataGridView.CellContentClick += Solar_dataGridView_CellContentClick;
            // 
            // Solar_Copy_button
            // 
            Solar_Copy_button.BackColor = SystemColors.ControlLight;
            Solar_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Solar_Copy_button.FlatStyle = FlatStyle.System;
            Solar_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Solar_Copy_button.Location = new Point(1076, 7);
            Solar_Copy_button.Margin = new Padding(0);
            Solar_Copy_button.Name = "Solar_Copy_button";
            Solar_Copy_button.Size = new Size(47, 23);
            Solar_Copy_button.TabIndex = 108;
            Solar_Copy_button.Text = "Copy";
            Solar_Copy_button.UseVisualStyleBackColor = false;
            Solar_Copy_button.Click += Solar_Copy_button_Click;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(PV_dataGridView);
            tabPage1.Controls.Add(PV_Save_button);
            tabPage1.Controls.Add(DefaultPV_Add_button);
            tabPage1.Controls.Add(PV_Copy_button);
            tabPage1.Controls.Add(PV_Remove_button);
            tabPage1.Controls.Add(UserPV_Add_button);
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1142, 226);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "태양광시스템";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(912, 13);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 123;
            label1.Text = "도면 기반 입력";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(768, 13);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 122;
            label2.Text = "기본 DB기반 입력";
            // 
            // PV_Save_button
            // 
            PV_Save_button.BackColor = SystemColors.ButtonHighlight;
            PV_Save_button.ForeColor = Color.Black;
            PV_Save_button.Location = new Point(1035, 195);
            PV_Save_button.Name = "PV_Save_button";
            PV_Save_button.Size = new Size(88, 25);
            PV_Save_button.TabIndex = 121;
            PV_Save_button.Text = "SAVE";
            PV_Save_button.UseVisualStyleBackColor = true;
            PV_Save_button.Click += PV_Save_button_Click;
            // 
            // DefaultPV_Add_button
            // 
            DefaultPV_Add_button.BackColor = SystemColors.ControlLight;
            DefaultPV_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultPV_Add_button.FlatStyle = FlatStyle.System;
            DefaultPV_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultPV_Add_button.Location = new Point(875, 9);
            DefaultPV_Add_button.Margin = new Padding(0);
            DefaultPV_Add_button.Name = "DefaultPV_Add_button";
            DefaultPV_Add_button.Size = new Size(23, 23);
            DefaultPV_Add_button.TabIndex = 118;
            DefaultPV_Add_button.Text = "+";
            DefaultPV_Add_button.UseVisualStyleBackColor = false;
            DefaultPV_Add_button.Click += DefaultPV_Add_button_Click;
            // 
            // PV_Copy_button
            // 
            PV_Copy_button.BackColor = SystemColors.ControlLight;
            PV_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PV_Copy_button.FlatStyle = FlatStyle.System;
            PV_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            PV_Copy_button.Location = new Point(1076, 9);
            PV_Copy_button.Margin = new Padding(0);
            PV_Copy_button.Name = "PV_Copy_button";
            PV_Copy_button.Size = new Size(47, 23);
            PV_Copy_button.TabIndex = 116;
            PV_Copy_button.Text = "Copy";
            PV_Copy_button.UseVisualStyleBackColor = false;
            PV_Copy_button.Click += PV_Copy_button_Click;
            // 
            // PV_Remove_button
            // 
            PV_Remove_button.BackColor = SystemColors.ControlLight;
            PV_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PV_Remove_button.FlatStyle = FlatStyle.System;
            PV_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            PV_Remove_button.Location = new Point(1039, 9);
            PV_Remove_button.Margin = new Padding(0);
            PV_Remove_button.Name = "PV_Remove_button";
            PV_Remove_button.Size = new Size(23, 23);
            PV_Remove_button.TabIndex = 115;
            PV_Remove_button.Text = "-";
            PV_Remove_button.UseVisualStyleBackColor = false;
            PV_Remove_button.Click += PV_Remove_button_Click;
            // 
            // UserPV_Add_button
            // 
            UserPV_Add_button.BackColor = SystemColors.ControlLight;
            UserPV_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserPV_Add_button.FlatStyle = FlatStyle.System;
            UserPV_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserPV_Add_button.Location = new Point(1002, 9);
            UserPV_Add_button.Margin = new Padding(0);
            UserPV_Add_button.Name = "UserPV_Add_button";
            UserPV_Add_button.Size = new Size(23, 23);
            UserPV_Add_button.TabIndex = 114;
            UserPV_Add_button.Text = "+";
            UserPV_Add_button.UseVisualStyleBackColor = false;
            UserPV_Add_button.Click += UserPV_Add_button_Click;
            // 
            // customTabControl1
            // 
            customTabControl1.Controls.Add(tabPage1);
            customTabControl1.Controls.Add(tabPage2);
            customTabControl1.Controls.Add(tabPage3);
            customTabControl1.Controls.Add(tabPage4);
            customTabControl1.Controls.Add(tabPage5);
            customTabControl1.Controls.Add(tabPage6);
            customTabControl1.DisplayStyleProvider.BorderColor = SystemColors.ControlDark;
            customTabControl1.DisplayStyleProvider.BorderColorHot = SystemColors.ControlDark;
            customTabControl1.DisplayStyleProvider.CloserColor = Color.Empty;
            customTabControl1.DisplayStyleProvider.FocusTrack = true;
            customTabControl1.DisplayStyleProvider.HotTrack = true;
            customTabControl1.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            customTabControl1.DisplayStyleProvider.Opacity = 1F;
            customTabControl1.DisplayStyleProvider.Overlap = 0;
            customTabControl1.DisplayStyleProvider.Padding = new Point(6, 3);
            customTabControl1.DisplayStyleProvider.ShowTabCloser = false;
            customTabControl1.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            customTabControl1.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            customTabControl1.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            customTabControl1.HotTrack = true;
            customTabControl1.ItemSize = new Size(128, 20);
            customTabControl1.Location = new Point(12, 331);
            customTabControl1.Name = "customTabControl1";
            customTabControl1.SelectedIndex = 0;
            customTabControl1.Size = new Size(1150, 255);
            customTabControl1.SizeMode = TabSizeMode.Fixed;
            customTabControl1.TabIndex = 146;
            // 
            // tabPage10
            // 
            tabPage10.Controls.Add(DHU_checkBox);
            tabPage10.Controls.Add(label27);
            tabPage10.Controls.Add(AHU_dataGridView);
            tabPage10.Controls.Add(AHU_Save_button);
            tabPage10.Controls.Add(AHU_Copy_button);
            tabPage10.Controls.Add(AHU_Remove_button);
            tabPage10.Controls.Add(UserAHU_Add_button);
            tabPage10.Location = new Point(4, 25);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new Padding(3);
            tabPage10.Size = new Size(1142, 226);
            tabPage10.TabIndex = 4;
            tabPage10.Text = "공조기";
            tabPage10.UseVisualStyleBackColor = true;
            // 
            // Panel_checkBox
            // 
            DHU_checkBox.AutoSize = true;
            DHU_checkBox.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            DHU_checkBox.Location = new Point(63, 17);
            DHU_checkBox.Name = "Panel_checkBox";
            DHU_checkBox.Size = new Size(48, 17);
            DHU_checkBox.TabIndex = 124;
            DHU_checkBox.Text = "적용";
            DHU_checkBox.UseVisualStyleBackColor = true;
            DHU_checkBox.CheckedChanged += DHU_checkBox_CheckedChanged;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label27.Location = new Point(20, 17);
            label27.Name = "label27";
            label27.Size = new Size(40, 16);
            label27.TabIndex = 123;
            label27.Text = "가습기";
            // 
            // AHU_dataGridView
            // 
            AHU_dataGridView.AllowUserToAddRows = false;
            AHU_dataGridView.AllowUserToDeleteRows = false;
            AHU_dataGridView.AllowUserToResizeColumns = false;
            AHU_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AHU_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AHU_dataGridView.BackgroundColor = SystemColors.Window;
            AHU_dataGridView.BorderStyle = BorderStyle.None;
            AHU_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AHU_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle88.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle88.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle88.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle88.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle88.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle88.SelectionForeColor = Color.Black;
            dataGridViewCellStyle88.WrapMode = DataGridViewTriState.True;
            AHU_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle88;
            AHU_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AHU_dataGridView.Location = new Point(19, 36);
            AHU_dataGridView.Name = "AHU_dataGridView";
            dataGridViewCellStyle89.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle89.BackColor = SystemColors.Control;
            dataGridViewCellStyle89.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle89.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle89.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle89.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle89.WrapMode = DataGridViewTriState.True;
            AHU_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle89;
            AHU_dataGridView.RowHeadersVisible = false;
            AHU_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle90.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle90.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle90.ForeColor = Color.Black;
            dataGridViewCellStyle90.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle90.SelectionForeColor = Color.Black;
            AHU_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle90;
            AHU_dataGridView.RowTemplate.Height = 25;
            AHU_dataGridView.Size = new Size(1105, 150);
            AHU_dataGridView.TabIndex = 117;
            AHU_dataGridView.CellContentClick += AHU_dataGridView_CellContentClick;
            // 
            // AHU_Save_button
            // 
            AHU_Save_button.BackColor = SystemColors.ButtonHighlight;
            AHU_Save_button.ForeColor = Color.Black;
            AHU_Save_button.Location = new Point(1036, 192);
            AHU_Save_button.Name = "AHU_Save_button";
            AHU_Save_button.Size = new Size(88, 25);
            AHU_Save_button.TabIndex = 118;
            AHU_Save_button.Text = "SAVE";
            AHU_Save_button.UseVisualStyleBackColor = true;
            AHU_Save_button.Click += AHU_Save_button_Click;
            // 
            // AHU_Copy_button
            // 
            AHU_Copy_button.BackColor = SystemColors.ControlLight;
            AHU_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AHU_Copy_button.FlatStyle = FlatStyle.System;
            AHU_Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            AHU_Copy_button.Location = new Point(1077, 10);
            AHU_Copy_button.Margin = new Padding(0);
            AHU_Copy_button.Name = "AHU_Copy_button";
            AHU_Copy_button.Size = new Size(47, 23);
            AHU_Copy_button.TabIndex = 116;
            AHU_Copy_button.Text = "Copy";
            AHU_Copy_button.UseVisualStyleBackColor = false;
            AHU_Copy_button.Click += AHU_Copy_button_Click;
            // 
            // AHU_Remove_button
            // 
            AHU_Remove_button.BackColor = SystemColors.ControlLight;
            AHU_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AHU_Remove_button.FlatStyle = FlatStyle.System;
            AHU_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AHU_Remove_button.Location = new Point(1040, 10);
            AHU_Remove_button.Margin = new Padding(0);
            AHU_Remove_button.Name = "AHU_Remove_button";
            AHU_Remove_button.Size = new Size(23, 23);
            AHU_Remove_button.TabIndex = 115;
            AHU_Remove_button.Text = "-";
            AHU_Remove_button.UseVisualStyleBackColor = false;
            AHU_Remove_button.Click += AHU_Remove_button_Click;
            // 
            // UserAHU_Add_button
            // 
            UserAHU_Add_button.BackColor = SystemColors.ControlLight;
            UserAHU_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserAHU_Add_button.FlatStyle = FlatStyle.System;
            UserAHU_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserAHU_Add_button.Location = new Point(1003, 10);
            UserAHU_Add_button.Margin = new Padding(0);
            UserAHU_Add_button.Name = "UserAHU_Add_button";
            UserAHU_Add_button.Size = new Size(23, 23);
            UserAHU_Add_button.TabIndex = 114;
            UserAHU_Add_button.Text = "+";
            UserAHU_Add_button.UseVisualStyleBackColor = false;
            UserAHU_Add_button.Click += UserAHU_Add_button_Click;
            // 
            // tabPage9
            // 
            tabPage9.Controls.Add(ce_Save_button);
            tabPage9.Controls.Add(ce_dataGridView);
            tabPage9.Controls.Add(ce_Add_button);
            tabPage9.Controls.Add(ce_Copy_button);
            tabPage9.Controls.Add(ce_Remove_button);
            tabPage9.Location = new Point(4, 25);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(1142, 226);
            tabPage9.TabIndex = 3;
            tabPage9.Text = "공급설비";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            tabPage8.Controls.Add(Pump_Save_button);
            tabPage8.Controls.Add(Pump_dataGridView);
            tabPage8.Controls.Add(Pump_Copy_button);
            tabPage8.Controls.Add(Pump_Remove_button);
            tabPage8.Controls.Add(Pump_Add_button);
            tabPage8.Location = new Point(4, 25);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(1142, 226);
            tabPage8.TabIndex = 6;
            tabPage8.Text = "펌프";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            tabPage7.BackColor = Color.White;
            tabPage7.Location = new Point(4, 25);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(1142, 226);
            tabPage7.TabIndex = 2;
            tabPage7.Text = "팬";
            // 
            // customTabControl2
            // 
            customTabControl2.Controls.Add(tabPage7);
            customTabControl2.Controls.Add(tabPage8);
            customTabControl2.Controls.Add(tabPage9);
            customTabControl2.Controls.Add(tabPage10);
            customTabControl2.DisplayStyleProvider.BorderColor = SystemColors.ControlDark;
            customTabControl2.DisplayStyleProvider.BorderColorHot = SystemColors.ControlDark;
            customTabControl2.DisplayStyleProvider.CloserColor = Color.Empty;
            customTabControl2.DisplayStyleProvider.FocusTrack = true;
            customTabControl2.DisplayStyleProvider.HotTrack = true;
            customTabControl2.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            customTabControl2.DisplayStyleProvider.Opacity = 1F;
            customTabControl2.DisplayStyleProvider.Overlap = 0;
            customTabControl2.DisplayStyleProvider.Padding = new Point(6, 3);
            customTabControl2.DisplayStyleProvider.ShowTabCloser = false;
            customTabControl2.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            customTabControl2.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            customTabControl2.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            customTabControl2.HotTrack = true;
            customTabControl2.ItemSize = new Size(128, 20);
            customTabControl2.Location = new Point(12, 592);
            customTabControl2.Name = "customTabControl2";
            customTabControl2.SelectedIndex = 0;
            customTabControl2.Size = new Size(1150, 255);
            customTabControl2.SizeMode = TabSizeMode.Fixed;
            customTabControl2.TabIndex = 147;
            // 
            // EquipmentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(customTabControl2);
            Controls.Add(customTabControl1);
            Controls.Add(tabControl1);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EquipmentList";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)PV_dataGridView).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            tabControl1.ResumeLayout(false);
            HP_tabPage.ResumeLayout(false);
            HP_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AirHP_dataGridView).EndInit();
            Boiler_tabPage.ResumeLayout(false);
            Boiler_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            AS_tabPage.ResumeLayout(false);
            AS_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ABS_dataGridView).EndInit();
            DH_tabPage.ResumeLayout(false);
            DH_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DH_dataGridView).EndInit();
            AirCooler_tabPage.ResumeLayout(false);
            AirCooler_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AirCooler_dataGridView).EndInit();
            WaterCooler_tabPage.ResumeLayout(false);
            WaterCooler_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WaterCooler_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)ce_dataGridView).EndInit();
            tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).EndInit();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).EndInit();
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GWHP_dataGridView).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroundHP_dataGridView).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Solar_dataGridView).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            customTabControl1.ResumeLayout(false);
            tabPage10.ResumeLayout(false);
            tabPage10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AHU_dataGridView).EndInit();
            tabPage9.ResumeLayout(false);
            tabPage8.ResumeLayout(false);
            customTabControl2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private CustomTabControl tabControl1;
        private TabPage Boiler_tabPage;
        private TabPage HP_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage AirCooler_tabPage;
        private PictureBox Icon_pictureBox;
        private Button Boiler_Copy_button;
        private Button Boiler_Remove_button;
        private Button UserBoiler_Add_button;
        private DataGridView Boiler_dataGridView;
        private TabPage WaterCooler_tabPage;
        private Button Pump_Save_button;
        private DataGridView Pump_dataGridView;
        private Button Pump_Copy_button;
        private Button Pump_Remove_button;
        private Button Pump_Add_button;
        private Button ce_Save_button;
        private DataGridView ce_dataGridView;
        private Button ce_Copy_button;
        private Button ce_Remove_button;
        private Button ce_Add_button;
        private DataGridView AirHP_dataGridView;
        private Button DefaultAirHP_Add_button;
        private Button AirHP_Copy_button;
        private Button AirHP_Remove_button;
        private Button UserAirHP_Add_button;
        private Button Solar_Save_button;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button AirHP_Save_button;
        private Button Boiler_Save_button;
        private TabPage tabPage6;
        private DataGridView WP_dataGridView;
        private Button WP_Save_button;
        private Button WP_Copy_button;
        private Button WP_Remove_button;
        private Button UserWP_Add_button;
        private Button button15;
        private TabPage tabPage5;
        private DataGridView FC_dataGridView;
        private Button FC_Save_button;
        private Button DefaultFC_Add_button;
        private Button FC_Copy_button;
        private Button FC_Remove_button;
        private Button UserFC_Add_button;
        private TabPage tabPage4;
        private DataGridView GWHP_dataGridView;
        private Button UserGWHP_Add_button;
        private Button GWHP_Remove_button;
        private Button GWHP_Copy_button;
        private TabPage tabPage3;
        private DataGridView GroundHP_dataGridView;
        private Button GroundHP_Copy_button;
        private Button GroundHP_Remove_button;
        private Button UserGroundHP_Add_button;
        private TabPage tabPage2;
        private Button button6;
        private Button DefaultSolar_Add_button;
        private Button UserSolar_Add_button;
        private Button Solar_Remove_button;
        private DataGridView Solar_dataGridView;
        private Button Solar_Copy_button;
        private TabPage tabPage1;
        private DataGridView PV_dataGridView;
        private Button PV_Save_button;
        private Button DefaultPV_Add_button;
        private Button PV_Copy_button;
        private Button PV_Remove_button;
        private Button UserPV_Add_button;
        private CustomTabControl customTabControl1;
        private TabPage tabPage10;
        private TabPage tabPage9;
        private TabPage tabPage8;
        private TabPage tabPage7;
        private CustomTabControl customTabControl2;
        private DataGridView ABS_dataGridView;
        private Button ABS_Save_button;
        private Button DefaultABS_Add_button;
        private Button ABS_Copy_button;
        private Button ABS_Remove_button;
        private Button UserABS_Add_button;
        private Button DH_Copy_button;
        private Button DH_Remove_button;
        private Button UserDH_Add_button;
        private Button DefaultDH_Add_button;
        private Button DH_Save_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DataGridView AirCooler_dataGridView;
        private Button AirCooler_Save_button;
        private Button DefaultAirCooler_Add_button;
        private Button AirCooler_Copy_button;
        private Button AirCooler_Remove_button;
        private Button UserAirCooler_Add_button;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private DataGridView WaterCooler_dataGridView;
        private Button WaterCooler_Save_button;
        private Button DefaultWaterCooler_Add_button;
        private Button WaterCooler_Copy_button;
        private Button WaterCooler_Remove_button;
        private Button UserWaterCooler_Add_button;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private Button DefaultBoiler_Add_button;
        private Button GroundHP_Save_button;
        private Button GWHP_Save_button;
        private DataGridView DH_dataGridView;
        private DataGridView AHU_dataGridView;
        private Button AHU_Save_button;
        private Button AHU_Copy_button;
        private Button AHU_Remove_button;
        private Button UserAHU_Add_button;
        private CheckBox DHU_checkBox;
        private System.Windows.Forms.Label label27;
    }
}