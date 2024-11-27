
namespace main.contents.Building
{
    partial class EnergyUse
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
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle25 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle26 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle27 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle28 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle29 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle30 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle31 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle32 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle33 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle34 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle35 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle36 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle37 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle38 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle39 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle40 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Gas_tabPage = new TabPage();
            label4 = new Label();
            Gas_EndDay_comboBox = new CustomComboBox();
            label5 = new Label();
            Gas_StartDay_comboBox = new CustomComboBox();
            label6 = new Label();
            Gas_kWh_dataGridView = new DataGridView();
            Gas_Remove_button = new Button();
            Gas_Add_button = new Button();
            Gas_m3_dataGridView = new DataGridView();
            Save_button = new Button();
            Elec_tabPage = new TabPage();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            label2 = new Label();
            Elec_EndDay_comboBox = new CustomComboBox();
            label1 = new Label();
            Elec_StartDay_comboBox = new CustomComboBox();
            label25 = new Label();
            Elec_Remove_button = new Button();
            Elec_Add_button = new Button();
            Elec_dataGridView = new DataGridView();
            tabControl = new CustomTabControl();
            DH_tabPage = new TabPage();
            label7 = new Label();
            DH_EndDay_comboBox = new CustomComboBox();
            label8 = new Label();
            DH_StartDay_comboBox = new CustomComboBox();
            label9 = new Label();
            DH_kWh_dataGridView = new DataGridView();
            DH_Remove_button = new Button();
            DH_Add_button = new Button();
            DH_Mcal_dataGridView = new DataGridView();
            webView23 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new Panel();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            Gas_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Gas_kWh_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gas_m3_dataGridView).BeginInit();
            Elec_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Elec_dataGridView).BeginInit();
            tabControl.SuspendLayout();
            DH_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DH_kWh_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DH_Mcal_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView23).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = SystemColors.GradientActiveCaption;
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Location = new Point(0, 4);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1000, 80);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(18, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 91;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.White;
            label3.Location = new Point(80, 32);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 3;
            label3.Text = "에너지사용량";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(3, 311);
            webView21.Name = "webView21";
            webView21.Size = new Size(986, 249);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // Gas_tabPage
            // 
            Gas_tabPage.BackColor = Color.White;
            Gas_tabPage.Controls.Add(label4);
            Gas_tabPage.Controls.Add(Gas_EndDay_comboBox);
            Gas_tabPage.Controls.Add(label5);
            Gas_tabPage.Controls.Add(Gas_StartDay_comboBox);
            Gas_tabPage.Controls.Add(label6);
            Gas_tabPage.Controls.Add(Gas_kWh_dataGridView);
            Gas_tabPage.Controls.Add(Gas_Remove_button);
            Gas_tabPage.Controls.Add(Gas_Add_button);
            Gas_tabPage.Controls.Add(Gas_m3_dataGridView);
            Gas_tabPage.Controls.Add(webView21);
            Gas_tabPage.Location = new Point(4, 25);
            Gas_tabPage.Name = "Gas_tabPage";
            Gas_tabPage.Padding = new Padding(3);
            Gas_tabPage.Size = new Size(992, 563);
            Gas_tabPage.TabIndex = 6;
            Gas_tabPage.Text = "가스";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font(UTIL.Families[0], 9.75F);
            label4.Location = new Point(308, 23);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 131;
            label4.Text = "일";
            // 
            // Gas_EndDay_comboBox
            // 
            Gas_EndDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Gas_EndDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Gas_EndDay_comboBox.FormattingEnabled = true;
            Gas_EndDay_comboBox.Location = new Point(242, 19);
            Gas_EndDay_comboBox.Name = "Gas_EndDay_comboBox";
            Gas_EndDay_comboBox.Size = new Size(60, 23);
            Gas_EndDay_comboBox.TabIndex = 130;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font(UTIL.Families[0], 9.75F);
            label5.Location = new Point(197, 23);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 129;
            label5.Text = "일  ~ ";
            // 
            // Gas_StartDay_comboBox
            // 
            Gas_StartDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Gas_StartDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Gas_StartDay_comboBox.FormattingEnabled = true;
            Gas_StartDay_comboBox.Location = new Point(133, 19);
            Gas_StartDay_comboBox.Name = "Gas_StartDay_comboBox";
            Gas_StartDay_comboBox.Size = new Size(60, 23);
            Gas_StartDay_comboBox.TabIndex = 128;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font(UTIL.Families[0], 9.75F);
            label6.Location = new Point(7, 23);
            label6.Name = "label6";
            label6.Size = new Size(121, 15);
            label6.TabIndex = 127;
            label6.Text = "고지서 기준 사용기간";
            // 
            // Gas_kWh_dataGridView
            // 
            Gas_kWh_dataGridView.AllowUserToAddRows = false;
            Gas_kWh_dataGridView.AllowUserToDeleteRows = false;
            Gas_kWh_dataGridView.AllowUserToResizeColumns = false;
            Gas_kWh_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Gas_kWh_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Gas_kWh_dataGridView.BackgroundColor = SystemColors.Window;
            Gas_kWh_dataGridView.BorderStyle = BorderStyle.None;
            Gas_kWh_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Gas_kWh_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle21.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle21.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle21.SelectionForeColor = Color.Black;
            dataGridViewCellStyle21.WrapMode = DataGridViewTriState.True;
            Gas_kWh_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            Gas_kWh_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = SystemColors.Window;
            dataGridViewCellStyle22.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle22.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = DataGridViewTriState.False;
            Gas_kWh_dataGridView.DefaultCellStyle = dataGridViewCellStyle22;
            Gas_kWh_dataGridView.Location = new Point(-1, 180);
            Gas_kWh_dataGridView.Name = "Gas_kWh_dataGridView";
            dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = SystemColors.Control;
            dataGridViewCellStyle23.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle23.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle23.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = DataGridViewTriState.True;
            Gas_kWh_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            Gas_kWh_dataGridView.RowHeadersVisible = false;
            Gas_kWh_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle24.ForeColor = Color.Black;
            dataGridViewCellStyle24.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle24.SelectionForeColor = Color.Black;
            Gas_kWh_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle24;
            Gas_kWh_dataGridView.Size = new Size(990, 132);
            Gas_kWh_dataGridView.TabIndex = 120;
            // 
            // Gas_Remove_button
            // 
            Gas_Remove_button.BackColor = SystemColors.ControlLight;
            Gas_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Gas_Remove_button.FlatStyle = FlatStyle.System;
            Gas_Remove_button.Font = new Font(UTIL.Families[0], 9.75F);
            Gas_Remove_button.Location = new Point(897, 19);
            Gas_Remove_button.Margin = new Padding(0);
            Gas_Remove_button.Name = "Gas_Remove_button";
            Gas_Remove_button.Size = new Size(23, 23);
            Gas_Remove_button.TabIndex = 119;
            Gas_Remove_button.Text = "-";
            Gas_Remove_button.UseVisualStyleBackColor = false;
            Gas_Remove_button.Click += Gas_Remove_button_Click;
            // 
            // Gas_Add_button
            // 
            Gas_Add_button.BackColor = SystemColors.ControlLight;
            Gas_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Gas_Add_button.FlatStyle = FlatStyle.System;
            Gas_Add_button.Font = new Font(UTIL.Families[0], 9.75F);
            Gas_Add_button.Location = new Point(868, 19);
            Gas_Add_button.Margin = new Padding(0);
            Gas_Add_button.Name = "Gas_Add_button";
            Gas_Add_button.Size = new Size(23, 23);
            Gas_Add_button.TabIndex = 118;
            Gas_Add_button.Text = "+";
            Gas_Add_button.UseVisualStyleBackColor = false;
            Gas_Add_button.Click += Gas_Add_button_Click;
            // 
            // Gas_m3_dataGridView
            // 
            Gas_m3_dataGridView.AllowUserToAddRows = false;
            Gas_m3_dataGridView.AllowUserToDeleteRows = false;
            Gas_m3_dataGridView.AllowUserToResizeColumns = false;
            Gas_m3_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Gas_m3_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Gas_m3_dataGridView.BackgroundColor = SystemColors.Window;
            Gas_m3_dataGridView.BorderStyle = BorderStyle.None;
            Gas_m3_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Gas_m3_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle25.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle25.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle25.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle25.SelectionForeColor = Color.Black;
            dataGridViewCellStyle25.WrapMode = DataGridViewTriState.True;
            Gas_m3_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            Gas_m3_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle26.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = SystemColors.Window;
            dataGridViewCellStyle26.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle26.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = DataGridViewTriState.False;
            Gas_m3_dataGridView.DefaultCellStyle = dataGridViewCellStyle26;
            Gas_m3_dataGridView.Location = new Point(-1, 54);
            Gas_m3_dataGridView.Name = "Gas_m3_dataGridView";
            dataGridViewCellStyle27.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle27.BackColor = SystemColors.Control;
            dataGridViewCellStyle27.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle27.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle27.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = DataGridViewTriState.True;
            Gas_m3_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            Gas_m3_dataGridView.RowHeadersVisible = false;
            Gas_m3_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle28.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle28.ForeColor = Color.Black;
            dataGridViewCellStyle28.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle28.SelectionForeColor = Color.Black;
            Gas_m3_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle28;
            Gas_m3_dataGridView.Size = new Size(990, 120);
            Gas_m3_dataGridView.TabIndex = 116;
            Gas_m3_dataGridView.CellContentClick += Gas_m3_dataGridView_CellContentClick;
            Gas_m3_dataGridView.CellValueChanged += Gas_m3_dataGridView_CellValueChanged;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1015, 650);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 117;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Elec_tabPage
            // 
            Elec_tabPage.BackColor = Color.White;
            Elec_tabPage.Controls.Add(webView22);
            Elec_tabPage.Controls.Add(label2);
            Elec_tabPage.Controls.Add(Elec_EndDay_comboBox);
            Elec_tabPage.Controls.Add(label1);
            Elec_tabPage.Controls.Add(Elec_StartDay_comboBox);
            Elec_tabPage.Controls.Add(label25);
            Elec_tabPage.Controls.Add(Elec_Remove_button);
            Elec_tabPage.Controls.Add(Elec_Add_button);
            Elec_tabPage.Controls.Add(Elec_dataGridView);
            Elec_tabPage.Location = new Point(4, 25);
            Elec_tabPage.Name = "Elec_tabPage";
            Elec_tabPage.Padding = new Padding(3);
            Elec_tabPage.Size = new Size(992, 563);
            Elec_tabPage.TabIndex = 2;
            Elec_tabPage.Text = "전기";
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Dock = DockStyle.Bottom;
            webView22.Location = new Point(3, 275);
            webView22.Name = "webView22";
            webView22.Size = new Size(986, 285);
            webView22.TabIndex = 127;
            webView22.ZoomFactor = 1D;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font(UTIL.Families[0], 9.75F);
            label2.Location = new Point(310, 27);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 126;
            label2.Text = "일";
            // 
            // Elec_EndDay_comboBox
            // 
            Elec_EndDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Elec_EndDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Elec_EndDay_comboBox.FormattingEnabled = true;
            Elec_EndDay_comboBox.Location = new Point(244, 23);
            Elec_EndDay_comboBox.Name = "Elec_EndDay_comboBox";
            Elec_EndDay_comboBox.Size = new Size(60, 23);
            Elec_EndDay_comboBox.TabIndex = 125;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font(UTIL.Families[0], 9.75F);
            label1.Location = new Point(199, 27);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 124;
            label1.Text = "일  ~ ";
            // 
            // Elec_StartDay_comboBox
            // 
            Elec_StartDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Elec_StartDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Elec_StartDay_comboBox.FormattingEnabled = true;
            Elec_StartDay_comboBox.Location = new Point(135, 23);
            Elec_StartDay_comboBox.Name = "Elec_StartDay_comboBox";
            Elec_StartDay_comboBox.Size = new Size(60, 23);
            Elec_StartDay_comboBox.TabIndex = 123;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font(UTIL.Families[0], 9.75F);
            label25.Location = new Point(10, 27);
            label25.Name = "label25";
            label25.Size = new Size(121, 15);
            label25.TabIndex = 122;
            label25.Text = "고지서 기준 사용기간";
            // 
            // Elec_Remove_button
            // 
            Elec_Remove_button.BackColor = SystemColors.ControlLight;
            Elec_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Elec_Remove_button.FlatStyle = FlatStyle.System;
            Elec_Remove_button.Font = new Font(UTIL.Families[0], 9.75F);
            Elec_Remove_button.Location = new Point(903, 27);
            Elec_Remove_button.Margin = new Padding(0);
            Elec_Remove_button.Name = "Elec_Remove_button";
            Elec_Remove_button.Size = new Size(23, 23);
            Elec_Remove_button.TabIndex = 115;
            Elec_Remove_button.Text = "-";
            Elec_Remove_button.UseVisualStyleBackColor = false;
            Elec_Remove_button.Click += Elec_Remove_button_Click;
            // 
            // Elec_Add_button
            // 
            Elec_Add_button.BackColor = SystemColors.ControlLight;
            Elec_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Elec_Add_button.FlatStyle = FlatStyle.System;
            Elec_Add_button.Font = new Font(UTIL.Families[0], 9.75F);
            Elec_Add_button.Location = new Point(874, 27);
            Elec_Add_button.Margin = new Padding(0);
            Elec_Add_button.Name = "Elec_Add_button";
            Elec_Add_button.Size = new Size(23, 23);
            Elec_Add_button.TabIndex = 114;
            Elec_Add_button.Text = "+";
            Elec_Add_button.UseVisualStyleBackColor = false;
            Elec_Add_button.Click += Elec_Add_button_Click;
            // 
            // Elec_dataGridView
            // 
            Elec_dataGridView.AllowUserToAddRows = false;
            Elec_dataGridView.AllowUserToDeleteRows = false;
            Elec_dataGridView.AllowUserToResizeColumns = false;
            Elec_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Elec_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Elec_dataGridView.BackgroundColor = SystemColors.Window;
            Elec_dataGridView.BorderStyle = BorderStyle.None;
            Elec_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Elec_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle29.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle29.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle29.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle29.SelectionForeColor = Color.Black;
            dataGridViewCellStyle29.WrapMode = DataGridViewTriState.True;
            Elec_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
            Elec_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle30.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = SystemColors.Window;
            dataGridViewCellStyle30.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle30.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle30.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = DataGridViewTriState.False;
            Elec_dataGridView.DefaultCellStyle = dataGridViewCellStyle30;
            Elec_dataGridView.Location = new Point(3, 64);
            Elec_dataGridView.Name = "Elec_dataGridView";
            dataGridViewCellStyle31.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = SystemColors.Control;
            dataGridViewCellStyle31.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle31.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle31.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = DataGridViewTriState.True;
            Elec_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle31;
            Elec_dataGridView.RowHeadersVisible = false;
            Elec_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle32.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle32.ForeColor = Color.Black;
            dataGridViewCellStyle32.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle32.SelectionForeColor = Color.Black;
            Elec_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle32;
            Elec_dataGridView.Size = new Size(986, 213);
            Elec_dataGridView.TabIndex = 112;
            Elec_dataGridView.CellContentClick += Elec_dataGridView_CellContentClick;
            Elec_dataGridView.CellValueChanged += Elec_dataGridView_CellValueChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(Elec_tabPage);
            tabControl.Controls.Add(Gas_tabPage);
            tabControl.Controls.Add(DH_tabPage);
            tabControl.DisplayStyleProvider.BorderColor = SystemColors.Control;
            tabControl.DisplayStyleProvider.BorderColorHot = SystemColors.Control;
            tabControl.DisplayStyleProvider.CloserColor = Color.Empty;
            tabControl.DisplayStyleProvider.FocusTrack = true;
            tabControl.DisplayStyleProvider.HotTrack = true;
            tabControl.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            tabControl.DisplayStyleProvider.Opacity = 1F;
            tabControl.DisplayStyleProvider.Overlap = 0;
            tabControl.DisplayStyleProvider.Padding = new Point(6, 3);
            tabControl.DisplayStyleProvider.ShowTabCloser = false;
            tabControl.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            tabControl.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            tabControl.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl.HotTrack = true;
            tabControl.ItemSize = new Size(128, 20);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1000, 592);
            tabControl.SizeMode = TabSizeMode.Fixed;
            tabControl.TabIndex = 146;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Controls.Add(label7);
            DH_tabPage.Controls.Add(DH_EndDay_comboBox);
            DH_tabPage.Controls.Add(label8);
            DH_tabPage.Controls.Add(DH_StartDay_comboBox);
            DH_tabPage.Controls.Add(label9);
            DH_tabPage.Controls.Add(DH_kWh_dataGridView);
            DH_tabPage.Controls.Add(DH_Remove_button);
            DH_tabPage.Controls.Add(DH_Add_button);
            DH_tabPage.Controls.Add(DH_Mcal_dataGridView);
            DH_tabPage.Controls.Add(webView23);
            DH_tabPage.Location = new Point(4, 25);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(992, 563);
            DH_tabPage.TabIndex = 7;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font(UTIL.Families[0], 9.75F);
            label7.Location = new Point(315, 16);
            label7.Name = "label7";
            label7.Size = new Size(19, 15);
            label7.TabIndex = 149;
            label7.Text = "일";
            // 
            // DH_EndDay_comboBox
            // 
            DH_EndDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            DH_EndDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            DH_EndDay_comboBox.FormattingEnabled = true;
            DH_EndDay_comboBox.Location = new Point(249, 12);
            DH_EndDay_comboBox.Name = "DH_EndDay_comboBox";
            DH_EndDay_comboBox.Size = new Size(60, 23);
            DH_EndDay_comboBox.TabIndex = 148;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font(UTIL.Families[0], 9.75F);
            label8.Location = new Point(204, 16);
            label8.Name = "label8";
            label8.Size = new Size(37, 15);
            label8.TabIndex = 147;
            label8.Text = "일  ~ ";
            // 
            // DH_StartDay_comboBox
            // 
            DH_StartDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            DH_StartDay_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            DH_StartDay_comboBox.FormattingEnabled = true;
            DH_StartDay_comboBox.Location = new Point(140, 12);
            DH_StartDay_comboBox.Name = "DH_StartDay_comboBox";
            DH_StartDay_comboBox.Size = new Size(60, 23);
            DH_StartDay_comboBox.TabIndex = 146;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font(UTIL.Families[0], 9.75F);
            label9.Location = new Point(14, 16);
            label9.Name = "label9";
            label9.Size = new Size(121, 15);
            label9.TabIndex = 145;
            label9.Text = "고지서 기준 사용기간";
            // 
            // DH_kWh_dataGridView
            // 
            DH_kWh_dataGridView.AllowUserToAddRows = false;
            DH_kWh_dataGridView.AllowUserToDeleteRows = false;
            DH_kWh_dataGridView.AllowUserToResizeColumns = false;
            DH_kWh_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DH_kWh_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DH_kWh_dataGridView.BackgroundColor = SystemColors.Window;
            DH_kWh_dataGridView.BorderStyle = BorderStyle.None;
            DH_kWh_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DH_kWh_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle33.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle33.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle33.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle33.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle33.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle33.SelectionForeColor = Color.Black;
            dataGridViewCellStyle33.WrapMode = DataGridViewTriState.True;
            DH_kWh_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle33;
            DH_kWh_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle34.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = SystemColors.Window;
            dataGridViewCellStyle34.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle34.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle34.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = DataGridViewTriState.False;
            DH_kWh_dataGridView.DefaultCellStyle = dataGridViewCellStyle34;
            DH_kWh_dataGridView.Location = new Point(0, 185);
            DH_kWh_dataGridView.Name = "DH_kWh_dataGridView";
            dataGridViewCellStyle35.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = SystemColors.Control;
            dataGridViewCellStyle35.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle35.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle35.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = DataGridViewTriState.True;
            DH_kWh_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
            DH_kWh_dataGridView.RowHeadersVisible = false;
            DH_kWh_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle36.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle36.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle36.ForeColor = Color.Black;
            dataGridViewCellStyle36.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle36.SelectionForeColor = Color.Black;
            DH_kWh_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle36;
            DH_kWh_dataGridView.Size = new Size(990, 120);
            DH_kWh_dataGridView.TabIndex = 144;
            // 
            // DH_Remove_button
            // 
            DH_Remove_button.BackColor = SystemColors.ControlLight;
            DH_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DH_Remove_button.FlatStyle = FlatStyle.System;
            DH_Remove_button.Font = new Font(UTIL.Families[0], 9.75F);
            DH_Remove_button.Location = new Point(904, 12);
            DH_Remove_button.Margin = new Padding(0);
            DH_Remove_button.Name = "DH_Remove_button";
            DH_Remove_button.Size = new Size(23, 23);
            DH_Remove_button.TabIndex = 143;
            DH_Remove_button.Text = "-";
            DH_Remove_button.UseVisualStyleBackColor = false;
            DH_Remove_button.Click += DH_Remove_button_Click;
            // 
            // DH_Add_button
            // 
            DH_Add_button.BackColor = SystemColors.ControlLight;
            DH_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            DH_Add_button.FlatStyle = FlatStyle.System;
            DH_Add_button.Font = new Font(UTIL.Families[0], 9.75F);
            DH_Add_button.Location = new Point(875, 12);
            DH_Add_button.Margin = new Padding(0);
            DH_Add_button.Name = "DH_Add_button";
            DH_Add_button.Size = new Size(23, 23);
            DH_Add_button.TabIndex = 142;
            DH_Add_button.Text = "+";
            DH_Add_button.UseVisualStyleBackColor = false;
            DH_Add_button.Click += DH_Add_button_Click;
            // 
            // DH_Mcal_dataGridView
            // 
            DH_Mcal_dataGridView.AllowUserToAddRows = false;
            DH_Mcal_dataGridView.AllowUserToDeleteRows = false;
            DH_Mcal_dataGridView.AllowUserToResizeColumns = false;
            DH_Mcal_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DH_Mcal_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DH_Mcal_dataGridView.BackgroundColor = SystemColors.Window;
            DH_Mcal_dataGridView.BorderStyle = BorderStyle.None;
            DH_Mcal_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DH_Mcal_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle37.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle37.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle37.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle37.SelectionForeColor = Color.Black;
            dataGridViewCellStyle37.WrapMode = DataGridViewTriState.True;
            DH_Mcal_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            DH_Mcal_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle38.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = SystemColors.Window;
            dataGridViewCellStyle38.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle38.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle38.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = DataGridViewTriState.False;
            DH_Mcal_dataGridView.DefaultCellStyle = dataGridViewCellStyle38;
            DH_Mcal_dataGridView.Location = new Point(0, 54);
            DH_Mcal_dataGridView.Name = "DH_Mcal_dataGridView";
            dataGridViewCellStyle39.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle39.BackColor = SystemColors.Control;
            dataGridViewCellStyle39.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle39.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle39.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = DataGridViewTriState.True;
            DH_Mcal_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            DH_Mcal_dataGridView.RowHeadersVisible = false;
            DH_Mcal_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle40.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle40.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle40.ForeColor = Color.Black;
            dataGridViewCellStyle40.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle40.SelectionForeColor = Color.Black;
            DH_Mcal_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle40;
            DH_Mcal_dataGridView.Size = new Size(990, 120);
            DH_Mcal_dataGridView.TabIndex = 141;
            DH_Mcal_dataGridView.CellContentClick += DH_Mcal_dataGridView_CellContentClick;
            DH_Mcal_dataGridView.CellValueChanged += DH_Mcal_dataGridView_CellValueChanged;
            // 
            // webView23
            // 
            webView23.AllowExternalDrop = true;
            webView23.BackColor = Color.White;
            webView23.CreationProperties = null;
            webView23.DefaultBackgroundColor = Color.White;
            webView23.Dock = DockStyle.Bottom;
            webView23.Location = new Point(3, 311);
            webView23.Name = "webView23";
            webView23.Size = new Size(986, 249);
            webView23.TabIndex = 136;
            webView23.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tabControl);
            panel1.Location = new Point(0, 83);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 592);
            panel1.TabIndex = 147;
            // 
            // EnergyUse
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EnergyUse";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            Gas_tabPage.ResumeLayout(false);
            Gas_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Gas_kWh_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gas_m3_dataGridView).EndInit();
            Elec_tabPage.ResumeLayout(false);
            Elec_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            ((System.ComponentModel.ISupportInitialize)Elec_dataGridView).EndInit();
            tabControl.ResumeLayout(false);
            DH_tabPage.ResumeLayout(false);
            DH_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DH_kWh_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)DH_Mcal_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView23).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label3;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private TabPage Gas_tabPage;
        private TabPage Elec_tabPage;
        private DataGridView Elec_dataGridView;
        private CustomTabControl tabControl;
        private Button Elec_Remove_button;
        private Button Elec_Add_button;
        private Button Gas_Remove_button;
        private Button Gas_Add_button;
        private DataGridView Gas_m3_dataGridView;
        private Button Save_button;
        private DataGridView Gas_kWh_dataGridView;
        private CustomComboBox Elec_EndDay_comboBox;
        private Label label1;
        private CustomComboBox Elec_StartDay_comboBox;
        private Label label25;
        private Label label4;
        private CustomComboBox Gas_EndDay_comboBox;
        private Label label5;
        private CustomComboBox Gas_StartDay_comboBox;
        private Label label6;
        private Label label2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private TabPage DH_tabPage;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView23;
        private Label label7;
        private CustomComboBox DH_EndDay_comboBox;
        private Label label8;
        private CustomComboBox DH_StartDay_comboBox;
        private Label label9;
        private DataGridView DH_kWh_dataGridView;
        private Button DH_Remove_button;
        private Button DH_Add_button;
        private DataGridView DH_Mcal_dataGridView;
        private Panel panel1;
        private PictureBox pictureBox1;
    }
}