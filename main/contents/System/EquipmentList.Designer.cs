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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            tabControl1 = new TabControl();
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
            tabControl1.Location = new Point(12, 75);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 643);
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
            Boiler_tabPage.Location = new Point(4, 24);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(969, 615);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(19, 65);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
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
            HP_tabPage.Location = new Point(4, 24);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(969, 615);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "히트펌프";
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 24);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(969, 615);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식냉온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 24);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(969, 615);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 24);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(969, 615);
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
            Pump_tabPage.Location = new Point(4, 24);
            Pump_tabPage.Name = "Pump_tabPage";
            Pump_tabPage.Padding = new Padding(3);
            Pump_tabPage.Size = new Size(969, 615);
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            Pump_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Pump_dataGridView.Location = new Point(18, 64);
            Pump_dataGridView.Name = "Pump_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            Pump_dataGridView.RowHeadersVisible = false;
            Pump_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Pump_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
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
            ce_tabPage.Location = new Point(4, 24);
            ce_tabPage.Name = "ce_tabPage";
            ce_tabPage.Padding = new Padding(3);
            ce_tabPage.Size = new Size(969, 615);
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
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            ce_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            ce_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ce_dataGridView.Location = new Point(18, 64);
            ce_dataGridView.Name = "ce_dataGridView";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            ce_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            ce_dataGridView.RowHeadersVisible = false;
            ce_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            ce_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            ce_dataGridView.RowTemplate.Height = 25;
            ce_dataGridView.Size = new Size(932, 467);
            ce_dataGridView.TabIndex = 114;
            ce_dataGridView.CellContentClick += ce_dataGridView_CellContentClick;
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
            Pump_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).EndInit();
            ce_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ce_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private TabControl tabControl1;
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
    }
}