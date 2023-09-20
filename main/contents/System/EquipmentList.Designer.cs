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
            DataGridViewCellStyle dataGridViewCellStyle64 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle65 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle66 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle55 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle56 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle57 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle58 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle59 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle60 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle61 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle62 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle63 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle67 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle68 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle69 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle70 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle71 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle72 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            tabControl1 = new CustomTabControl();
            Boiler_tabPage = new TabPage();
            label2 = new Label();
            DefaultBoiler_Add_button = new Button();
            label1 = new Label();
            Boiler_Save_button = new Button();
            Boiler_dataGridView = new DataGridView();
            Boiler_Copy_button = new Button();
            Boiler_Remove_button = new Button();
            UserBoiler_Add_button = new Button();
            HP_tabPage = new TabPage();
            label10 = new Label();
            GWHP_dataGridView = new DataGridView();
            label11 = new Label();
            GWHP_Copy_button = new Button();
            GWHP_Remove_button = new Button();
            UserGWHP_Add_button = new Button();
            label7 = new Label();
            GroundHP_dataGridView = new DataGridView();
            label8 = new Label();
            GroundHP_Copy_button = new Button();
            GroundHP_Remove_button = new Button();
            UserGroundHP_Add_button = new Button();
            label6 = new Label();
            HP_Save_button = new Button();
            AirHP_dataGridView = new DataGridView();
            label3 = new Label();
            DefaultAirHP_Add_button = new Button();
            label5 = new Label();
            AirHP_Copy_button = new Button();
            AirHP_Remove_button = new Button();
            UserAirHP_Add_button = new Button();
            AS_tabPage = new TabPage();
            DH_tabPage = new TabPage();
            Solar_tabPage = new TabPage();
            Pump_tabPage = new TabPage();
            Pump_Save_button = new Button();
            Pump_dataGridView = new DataGridView();
            Pump_Copy_button = new Button();
            Pump_Remove_button = new Button();
            Pump_Add_button = new Button();
            ce_tabPage = new TabPage();
            ce_Save_button = new Button();
            ce_dataGridView = new DataGridView();
            ce_Copy_button = new Button();
            ce_Remove_button = new Button();
            ce_Add_button = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            tabControl1.SuspendLayout();
            Boiler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
            HP_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GWHP_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GroundHP_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AirHP_dataGridView).BeginInit();
            Pump_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).BeginInit();
            ce_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ce_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 57);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(70, 22);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 101;
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
            tabControl1.Controls.Add(Boiler_tabPage);
            tabControl1.Controls.Add(HP_tabPage);
            tabControl1.Controls.Add(AS_tabPage);
            tabControl1.Controls.Add(DH_tabPage);
            tabControl1.Controls.Add(Solar_tabPage);
            tabControl1.Controls.Add(Pump_tabPage);
            tabControl1.Controls.Add(ce_tabPage);
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
            tabControl1.Size = new Size(977, 643);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 145;
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.Controls.Add(label2);
            Boiler_tabPage.Controls.Add(DefaultBoiler_Add_button);
            Boiler_tabPage.Controls.Add(label1);
            Boiler_tabPage.Controls.Add(Boiler_Save_button);
            Boiler_tabPage.Controls.Add(Boiler_dataGridView);
            Boiler_tabPage.Controls.Add(Boiler_Copy_button);
            Boiler_tabPage.Controls.Add(Boiler_Remove_button);
            Boiler_tabPage.Controls.Add(UserBoiler_Add_button);
            Boiler_tabPage.Location = new Point(4, 25);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(969, 614);
            Boiler_tabPage.TabIndex = 6;
            Boiler_tabPage.Text = "보일러";
            Boiler_tabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(740, 26);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 105;
            label2.Text = "도면 기반 입력";
            // 
            // DefaultBoiler_Add_button
            // 
            DefaultBoiler_Add_button.BackColor = SystemColors.ControlLight;
            DefaultBoiler_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultBoiler_Add_button.FlatStyle = FlatStyle.System;
            DefaultBoiler_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultBoiler_Add_button.Location = new Point(703, 22);
            DefaultBoiler_Add_button.Margin = new Padding(0);
            DefaultBoiler_Add_button.Name = "DefaultBoiler_Add_button";
            DefaultBoiler_Add_button.Size = new Size(23, 23);
            DefaultBoiler_Add_button.TabIndex = 104;
            DefaultBoiler_Add_button.Text = "+";
            DefaultBoiler_Add_button.UseVisualStyleBackColor = false;
            DefaultBoiler_Add_button.Click += DefaultBoiler_Add_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(596, 26);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 103;
            label1.Text = "기본 DB기반 입력";
            // 
            // Boiler_Save_button
            // 
            Boiler_Save_button.BackColor = SystemColors.ButtonHighlight;
            Boiler_Save_button.ForeColor = Color.Black;
            Boiler_Save_button.Location = new Point(863, 569);
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
            dataGridViewCellStyle64.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle64.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle64.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle64.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle64.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle64.SelectionForeColor = Color.Black;
            dataGridViewCellStyle64.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle64;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(19, 65);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle65.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle65.BackColor = SystemColors.Control;
            dataGridViewCellStyle65.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle65.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle65.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle65.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle65.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle65;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle66.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle66.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle66.ForeColor = Color.Black;
            dataGridViewCellStyle66.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle66.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle66;
            Boiler_dataGridView.RowTemplate.Height = 25;
            Boiler_dataGridView.Size = new Size(932, 467);
            Boiler_dataGridView.TabIndex = 101;
            Boiler_dataGridView.CellContentClick += Boiler_dataGridView_CellContentClick;
            Boiler_dataGridView.CellValueChanged += Boiler_dataGridView_CellValueChanged;
            // 
            // Boiler_Copy_button
            // 
            Boiler_Copy_button.BackColor = SystemColors.ControlLight;
            Boiler_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Boiler_Copy_button.FlatStyle = FlatStyle.System;
            Boiler_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Boiler_Copy_button.Location = new Point(904, 22);
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
            Boiler_Remove_button.Location = new Point(867, 22);
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
            UserBoiler_Add_button.Location = new Point(830, 22);
            UserBoiler_Add_button.Margin = new Padding(0);
            UserBoiler_Add_button.Name = "UserBoiler_Add_button";
            UserBoiler_Add_button.Size = new Size(23, 23);
            UserBoiler_Add_button.TabIndex = 98;
            UserBoiler_Add_button.Text = "+";
            UserBoiler_Add_button.UseVisualStyleBackColor = false;
            UserBoiler_Add_button.Click += UserBoiler_Add_button_Click;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Controls.Add(label10);
            HP_tabPage.Controls.Add(GWHP_dataGridView);
            HP_tabPage.Controls.Add(label11);
            HP_tabPage.Controls.Add(GWHP_Copy_button);
            HP_tabPage.Controls.Add(GWHP_Remove_button);
            HP_tabPage.Controls.Add(UserGWHP_Add_button);
            HP_tabPage.Controls.Add(label7);
            HP_tabPage.Controls.Add(GroundHP_dataGridView);
            HP_tabPage.Controls.Add(label8);
            HP_tabPage.Controls.Add(GroundHP_Copy_button);
            HP_tabPage.Controls.Add(GroundHP_Remove_button);
            HP_tabPage.Controls.Add(UserGroundHP_Add_button);
            HP_tabPage.Controls.Add(label6);
            HP_tabPage.Controls.Add(HP_Save_button);
            HP_tabPage.Controls.Add(AirHP_dataGridView);
            HP_tabPage.Controls.Add(label3);
            HP_tabPage.Controls.Add(DefaultAirHP_Add_button);
            HP_tabPage.Controls.Add(label5);
            HP_tabPage.Controls.Add(AirHP_Copy_button);
            HP_tabPage.Controls.Add(AirHP_Remove_button);
            HP_tabPage.Controls.Add(UserAirHP_Add_button);
            HP_tabPage.Location = new Point(4, 25);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(969, 614);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "히트펌프";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(29, 389);
            label10.Name = "label10";
            label10.Size = new Size(95, 15);
            label10.TabIndex = 130;
            label10.Text = "지하수 히트펌프";
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
            dataGridViewCellStyle55.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle55.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle55.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle55.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle55.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle55.SelectionForeColor = Color.Black;
            dataGridViewCellStyle55.WrapMode = DataGridViewTriState.True;
            GWHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle55;
            GWHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GWHP_dataGridView.Location = new Point(18, 414);
            GWHP_dataGridView.Name = "GWHP_dataGridView";
            dataGridViewCellStyle56.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle56.BackColor = SystemColors.Control;
            dataGridViewCellStyle56.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle56.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle56.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = DataGridViewTriState.True;
            GWHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle56;
            GWHP_dataGridView.RowHeadersVisible = false;
            GWHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle57.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle57.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle57.ForeColor = Color.Black;
            dataGridViewCellStyle57.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle57.SelectionForeColor = Color.Black;
            GWHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle57;
            GWHP_dataGridView.RowTemplate.Height = 25;
            GWHP_dataGridView.Size = new Size(932, 111);
            GWHP_dataGridView.TabIndex = 129;
            GWHP_dataGridView.CellContentClick += GWHP_dataGridView_CellContentClick;
            GWHP_dataGridView.CellValueChanged += GWHP_dataGridView_CellValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(739, 389);
            label11.Name = "label11";
            label11.Size = new Size(87, 15);
            label11.TabIndex = 128;
            label11.Text = "도면 기반 입력";
            // 
            // GWHP_Copy_button
            // 
            GWHP_Copy_button.BackColor = SystemColors.ControlLight;
            GWHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GWHP_Copy_button.FlatStyle = FlatStyle.System;
            GWHP_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            GWHP_Copy_button.Location = new Point(903, 385);
            GWHP_Copy_button.Margin = new Padding(0);
            GWHP_Copy_button.Name = "GWHP_Copy_button";
            GWHP_Copy_button.Size = new Size(47, 23);
            GWHP_Copy_button.TabIndex = 125;
            GWHP_Copy_button.Text = "Copy";
            GWHP_Copy_button.UseVisualStyleBackColor = false;
            GWHP_Copy_button.Click += GWHP_Copy_button_Click;
            // 
            // GWHP_Remove_button
            // 
            GWHP_Remove_button.BackColor = SystemColors.ControlLight;
            GWHP_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GWHP_Remove_button.FlatStyle = FlatStyle.System;
            GWHP_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            GWHP_Remove_button.Location = new Point(866, 385);
            GWHP_Remove_button.Margin = new Padding(0);
            GWHP_Remove_button.Name = "GWHP_Remove_button";
            GWHP_Remove_button.Size = new Size(23, 23);
            GWHP_Remove_button.TabIndex = 124;
            GWHP_Remove_button.Text = "-";
            GWHP_Remove_button.UseVisualStyleBackColor = false;
            GWHP_Remove_button.Click += GWHP_Remove_button_Click;
            // 
            // UserGWHP_Add_button
            // 
            UserGWHP_Add_button.BackColor = SystemColors.ControlLight;
            UserGWHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            UserGWHP_Add_button.FlatStyle = FlatStyle.System;
            UserGWHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            UserGWHP_Add_button.Location = new Point(829, 385);
            UserGWHP_Add_button.Margin = new Padding(0);
            UserGWHP_Add_button.Name = "UserGWHP_Add_button";
            UserGWHP_Add_button.Size = new Size(23, 23);
            UserGWHP_Add_button.TabIndex = 123;
            UserGWHP_Add_button.Text = "+";
            UserGWHP_Add_button.UseVisualStyleBackColor = false;
            UserGWHP_Add_button.Click += UserGWHP_Add_button_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(29, 229);
            label7.Name = "label7";
            label7.Size = new Size(83, 15);
            label7.TabIndex = 122;
            label7.Text = "지열 히트펌프";
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
            dataGridViewCellStyle58.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle58.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle58.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle58.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle58.SelectionForeColor = Color.Black;
            dataGridViewCellStyle58.WrapMode = DataGridViewTriState.True;
            GroundHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle58;
            GroundHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GroundHP_dataGridView.Location = new Point(18, 254);
            GroundHP_dataGridView.Name = "GroundHP_dataGridView";
            dataGridViewCellStyle59.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle59.BackColor = SystemColors.Control;
            dataGridViewCellStyle59.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle59.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle59.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle59.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle59.WrapMode = DataGridViewTriState.True;
            GroundHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle59;
            GroundHP_dataGridView.RowHeadersVisible = false;
            GroundHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle60.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle60.ForeColor = Color.Black;
            dataGridViewCellStyle60.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle60.SelectionForeColor = Color.Black;
            GroundHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle60;
            GroundHP_dataGridView.RowTemplate.Height = 25;
            GroundHP_dataGridView.Size = new Size(932, 111);
            GroundHP_dataGridView.TabIndex = 121;
            GroundHP_dataGridView.CellContentClick += GroundHP_dataGridView_CellContentClick;
            GroundHP_dataGridView.CellValueChanged += GroundHP_dataGridView_CellValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(739, 229);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 120;
            label8.Text = "도면 기반 입력";
            // 
            // GroundHP_Copy_button
            // 
            GroundHP_Copy_button.BackColor = SystemColors.ControlLight;
            GroundHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GroundHP_Copy_button.FlatStyle = FlatStyle.System;
            GroundHP_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            GroundHP_Copy_button.Location = new Point(903, 225);
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
            GroundHP_Remove_button.Location = new Point(866, 225);
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
            UserGroundHP_Add_button.Location = new Point(829, 225);
            UserGroundHP_Add_button.Margin = new Padding(0);
            UserGroundHP_Add_button.Name = "UserGroundHP_Add_button";
            UserGroundHP_Add_button.Size = new Size(23, 23);
            UserGroundHP_Add_button.TabIndex = 115;
            UserGroundHP_Add_button.Text = "+";
            UserGroundHP_Add_button.UseVisualStyleBackColor = false;
            UserGroundHP_Add_button.Click += UserGroundHP_Add_button_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(29, 27);
            label6.Name = "label6";
            label6.Size = new Size(111, 15);
            label6.TabIndex = 114;
            label6.Text = "외기 열원 히트펌프";
            // 
            // HP_Save_button
            // 
            HP_Save_button.BackColor = SystemColors.ButtonHighlight;
            HP_Save_button.ForeColor = Color.Black;
            HP_Save_button.Location = new Point(862, 569);
            HP_Save_button.Name = "HP_Save_button";
            HP_Save_button.Size = new Size(88, 25);
            HP_Save_button.TabIndex = 113;
            HP_Save_button.Text = "SAVE";
            HP_Save_button.UseVisualStyleBackColor = true;
            HP_Save_button.Click += AirHP_Save_button_Click;
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
            dataGridViewCellStyle61.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle61.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle61.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle61.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle61.SelectionForeColor = Color.Black;
            dataGridViewCellStyle61.WrapMode = DataGridViewTriState.True;
            AirHP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle61;
            AirHP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AirHP_dataGridView.Location = new Point(18, 52);
            AirHP_dataGridView.Name = "AirHP_dataGridView";
            dataGridViewCellStyle62.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = SystemColors.Control;
            dataGridViewCellStyle62.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle62.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle62.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle62.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle62.WrapMode = DataGridViewTriState.True;
            AirHP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle62;
            AirHP_dataGridView.RowHeadersVisible = false;
            AirHP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle63.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle63.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle63.ForeColor = Color.Black;
            dataGridViewCellStyle63.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle63.SelectionForeColor = Color.Black;
            AirHP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle63;
            AirHP_dataGridView.RowTemplate.Height = 25;
            AirHP_dataGridView.Size = new Size(932, 156);
            AirHP_dataGridView.TabIndex = 112;
            AirHP_dataGridView.CellContentClick += AirHP_dataGridView_CellContentClick;
            AirHP_dataGridView.CellValueChanged += AirHP_dataGridView_CellValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(739, 27);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 111;
            label3.Text = "도면 기반 입력";
            // 
            // DefaultAirHP_Add_button
            // 
            DefaultAirHP_Add_button.BackColor = SystemColors.ControlLight;
            DefaultAirHP_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DefaultAirHP_Add_button.FlatStyle = FlatStyle.System;
            DefaultAirHP_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            DefaultAirHP_Add_button.Location = new Point(702, 23);
            DefaultAirHP_Add_button.Margin = new Padding(0);
            DefaultAirHP_Add_button.Name = "DefaultAirHP_Add_button";
            DefaultAirHP_Add_button.Size = new Size(23, 23);
            DefaultAirHP_Add_button.TabIndex = 110;
            DefaultAirHP_Add_button.Text = "+";
            DefaultAirHP_Add_button.UseVisualStyleBackColor = false;
            DefaultAirHP_Add_button.Click += DefaultAirHP_Add_button_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(595, 27);
            label5.Name = "label5";
            label5.Size = new Size(104, 15);
            label5.TabIndex = 109;
            label5.Text = "기본 DB기반 입력";
            // 
            // AirHP_Copy_button
            // 
            AirHP_Copy_button.BackColor = SystemColors.ControlLight;
            AirHP_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AirHP_Copy_button.FlatStyle = FlatStyle.System;
            AirHP_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            AirHP_Copy_button.Location = new Point(903, 23);
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
            AirHP_Remove_button.Location = new Point(866, 23);
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
            UserAirHP_Add_button.Location = new Point(829, 23);
            UserAirHP_Add_button.Margin = new Padding(0);
            UserAirHP_Add_button.Name = "UserAirHP_Add_button";
            UserAirHP_Add_button.Size = new Size(23, 23);
            UserAirHP_Add_button.TabIndex = 106;
            UserAirHP_Add_button.Text = "+";
            UserAirHP_Add_button.UseVisualStyleBackColor = false;
            UserAirHP_Add_button.Click += UserAirHP_Add_button_Click;
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 25);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(969, 614);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식냉온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 25);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(969, 614);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 25);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(969, 614);
            Solar_tabPage.TabIndex = 5;
            Solar_tabPage.Text = "태양열시스템";
            Solar_tabPage.UseVisualStyleBackColor = true;
            // 
            // Pump_tabPage
            // 
            Pump_tabPage.Controls.Add(Pump_Save_button);
            Pump_tabPage.Controls.Add(Pump_dataGridView);
            Pump_tabPage.Controls.Add(Pump_Copy_button);
            Pump_tabPage.Controls.Add(Pump_Remove_button);
            Pump_tabPage.Controls.Add(Pump_Add_button);
            Pump_tabPage.Location = new Point(4, 25);
            Pump_tabPage.Name = "Pump_tabPage";
            Pump_tabPage.Padding = new Padding(3);
            Pump_tabPage.Size = new Size(969, 614);
            Pump_tabPage.TabIndex = 7;
            Pump_tabPage.Text = "펌프";
            Pump_tabPage.UseVisualStyleBackColor = true;
            // 
            // Pump_Save_button
            // 
            Pump_Save_button.BackColor = SystemColors.ButtonHighlight;
            Pump_Save_button.ForeColor = Color.Black;
            Pump_Save_button.Location = new Point(862, 568);
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
            Pump_dataGridView.Location = new Point(18, 64);
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
            Pump_dataGridView.Size = new Size(932, 467);
            Pump_dataGridView.TabIndex = 109;
            Pump_dataGridView.CellContentClick += Pump_dataGridView_CellContentClick;
            Pump_dataGridView.CellValueChanged += Pump_dataGridView_CellValueChanged;
            // 
            // Pump_Copy_button
            // 
            Pump_Copy_button.BackColor = SystemColors.ControlLight;
            Pump_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump_Copy_button.FlatStyle = FlatStyle.System;
            Pump_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Pump_Copy_button.Location = new Point(903, 21);
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
            Pump_Remove_button.Location = new Point(873, 21);
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
            Pump_Add_button.Location = new Point(840, 21);
            Pump_Add_button.Margin = new Padding(0);
            Pump_Add_button.Name = "Pump_Add_button";
            Pump_Add_button.Size = new Size(23, 23);
            Pump_Add_button.TabIndex = 106;
            Pump_Add_button.Text = "+";
            Pump_Add_button.UseVisualStyleBackColor = false;
            Pump_Add_button.Click += Pump_Add_button_Click;
            // 
            // ce_tabPage
            // 
            ce_tabPage.Controls.Add(ce_Save_button);
            ce_tabPage.Controls.Add(ce_dataGridView);
            ce_tabPage.Controls.Add(ce_Copy_button);
            ce_tabPage.Controls.Add(ce_Remove_button);
            ce_tabPage.Controls.Add(ce_Add_button);
            ce_tabPage.Location = new Point(4, 25);
            ce_tabPage.Name = "ce_tabPage";
            ce_tabPage.Padding = new Padding(3);
            ce_tabPage.Size = new Size(969, 614);
            ce_tabPage.TabIndex = 8;
            ce_tabPage.Text = "공급설비";
            ce_tabPage.UseVisualStyleBackColor = true;
            // 
            // ce_Save_button
            // 
            ce_Save_button.BackColor = SystemColors.ButtonHighlight;
            ce_Save_button.ForeColor = Color.Black;
            ce_Save_button.Location = new Point(862, 568);
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
            ce_dataGridView.Location = new Point(18, 64);
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
            ce_dataGridView.Size = new Size(932, 467);
            ce_dataGridView.TabIndex = 114;
            ce_dataGridView.CellValueChanged += ce_dataGridView_CellValueChanged;
            // 
            // ce_Copy_button
            // 
            ce_Copy_button.BackColor = SystemColors.ControlLight;
            ce_Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ce_Copy_button.FlatStyle = FlatStyle.System;
            ce_Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            ce_Copy_button.Location = new Point(903, 21);
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
            ce_Remove_button.Location = new Point(873, 21);
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
            ce_Add_button.Location = new Point(840, 21);
            ce_Add_button.Margin = new Padding(0);
            ce_Add_button.Name = "ce_Add_button";
            ce_Add_button.Size = new Size(23, 23);
            ce_Add_button.TabIndex = 111;
            ce_Add_button.Text = "+";
            ce_Add_button.UseVisualStyleBackColor = false;
            ce_Add_button.Click += ce_Add_button_Click;
            // 
            // EquipmentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl1);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EquipmentList";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            tabControl1.ResumeLayout(false);
            Boiler_tabPage.ResumeLayout(false);
            Boiler_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            HP_tabPage.ResumeLayout(false);
            HP_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GWHP_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)GroundHP_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)AirHP_dataGridView).EndInit();
            Pump_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).EndInit();
            ce_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ce_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private CustomTabControl tabControl1;
        private TabPage Boiler_tabPage;
        private TabPage HP_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage Solar_tabPage;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Button Boiler_Copy_button;
        private Button Boiler_Remove_button;
        private Button UserBoiler_Add_button;
        private DataGridView Boiler_dataGridView;
        private Button Boiler_Save_button;
        private Label label2;
        private Button DefaultBoiler_Add_button;
        private Label label1;
        private TabPage Pump_tabPage;
        private Button Pump_Save_button;
        private DataGridView Pump_dataGridView;
        private Button Pump_Copy_button;
        private Button Pump_Remove_button;
        private Button Pump_Add_button;
        private TabPage ce_tabPage;
        private Button ce_Save_button;
        private DataGridView ce_dataGridView;
        private Button ce_Copy_button;
        private Button ce_Remove_button;
        private Button ce_Add_button;
        private Button HP_Save_button;
        private DataGridView AirHP_dataGridView;
        private Label label3;
        private Button DefaultAirHP_Add_button;
        private Label label5;
        private Button AirHP_Copy_button;
        private Button AirHP_Remove_button;
        private Button UserAirHP_Add_button;
        private Label label10;
        private DataGridView GWHP_dataGridView;
        private Label label11;
        private Button GWHP_Copy_button;
        private Button GWHP_Remove_button;
        private Button UserGWHP_Add_button;
        private Label label7;
        private DataGridView GroundHP_dataGridView;
        private Label label8;
        private Button GroundHP_Copy_button;
        private Button GroundHP_Remove_button;
        private Button UserGroundHP_Add_button;
        private Label label6;
    }
}