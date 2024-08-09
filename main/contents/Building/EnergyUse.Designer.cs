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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnergyUse));
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
            label3 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            Gas_tabPage = new TabPage();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
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
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            pictureBox2 = new PictureBox();
            checkBox8 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox7 = new CheckBox();
            checkBox5 = new CheckBox();
            label2 = new Label();
            Elec_EndDay_comboBox = new CustomComboBox();
            label1 = new Label();
            Elec_StartDay_comboBox = new CustomComboBox();
            label25 = new Label();
            Elec_Remove_button = new Button();
            Elec_Add_button = new Button();
            Elec_dataGridView = new DataGridView();
            Gas_Save_button = new CustomTabControl();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            Gas_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Gas_kWh_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gas_m3_dataGridView).BeginInit();
            Elec_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Elec_dataGridView).BeginInit();
            Gas_Save_button.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(87, 43);
            label3.Name = "label3";
            label3.Size = new Size(73, 16);
            label3.TabIndex = 3;
            label3.Text = "에너지사용량";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(205, 321);
            webView21.Name = "webView21";
            webView21.Size = new Size(743, 200);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(124, 404);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 115);
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(101, 404);
            checkBox1.Name = "checkBox1";
            checkBox1.RightToLeft = RightToLeft.Yes;
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 20;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(101, 431);
            checkBox2.Name = "checkBox2";
            checkBox2.RightToLeft = RightToLeft.Yes;
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 21;
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(101, 485);
            checkBox4.Name = "checkBox4";
            checkBox4.RightToLeft = RightToLeft.Yes;
            checkBox4.Size = new Size(15, 14);
            checkBox4.TabIndex = 24;
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(101, 458);
            checkBox3.Name = "checkBox3";
            checkBox3.RightToLeft = RightToLeft.Yes;
            checkBox3.Size = new Size(15, 14);
            checkBox3.TabIndex = 23;
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // Gas_tabPage
            // 
            Gas_tabPage.BackColor = Color.White;
            Gas_tabPage.Controls.Add(textBox4);
            Gas_tabPage.Controls.Add(textBox3);
            Gas_tabPage.Controls.Add(textBox2);
            Gas_tabPage.Controls.Add(textBox1);
            Gas_tabPage.Controls.Add(webView21);
            Gas_tabPage.Controls.Add(pictureBox1);
            Gas_tabPage.Controls.Add(label4);
            Gas_tabPage.Controls.Add(Gas_EndDay_comboBox);
            Gas_tabPage.Controls.Add(checkBox4);
            Gas_tabPage.Controls.Add(label5);
            Gas_tabPage.Controls.Add(Gas_StartDay_comboBox);
            Gas_tabPage.Controls.Add(checkBox2);
            Gas_tabPage.Controls.Add(label6);
            Gas_tabPage.Controls.Add(checkBox3);
            Gas_tabPage.Controls.Add(Gas_kWh_dataGridView);
            Gas_tabPage.Controls.Add(checkBox1);
            Gas_tabPage.Controls.Add(Gas_Remove_button);
            Gas_tabPage.Controls.Add(Gas_Add_button);
            Gas_tabPage.Controls.Add(Gas_m3_dataGridView);
            Gas_tabPage.Location = new Point(4, 25);
            Gas_tabPage.Name = "Gas_tabPage";
            Gas_tabPage.Padding = new Padding(3);
            Gas_tabPage.Size = new Size(969, 527);
            Gas_tabPage.TabIndex = 6;
            Gas_tabPage.Text = "가스사용량";
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.White;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Enabled = false;
            textBox4.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.ForeColor = SystemColors.ControlDark;
            textBox4.Location = new Point(12, 485);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(86, 15);
            textBox4.TabIndex = 135;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.White;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Enabled = false;
            textBox3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.ControlDark;
            textBox3.Location = new Point(12, 458);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(86, 15);
            textBox3.TabIndex = 134;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Enabled = false;
            textBox2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ControlDark;
            textBox2.Location = new Point(12, 431);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(86, 15);
            textBox2.TabIndex = 133;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Enabled = false;
            textBox1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.ControlDark;
            textBox1.Location = new Point(12, 404);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(86, 15);
            textBox1.TabIndex = 132;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(308, 23);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 131;
            label4.Text = "일";
            // 
            // Gas_EndDay_comboBox
            // 
            Gas_EndDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Gas_EndDay_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Gas_EndDay_comboBox.FormattingEnabled = true;
            Gas_EndDay_comboBox.Location = new Point(242, 19);
            Gas_EndDay_comboBox.Name = "Gas_EndDay_comboBox";
            Gas_EndDay_comboBox.Size = new Size(60, 23);
            Gas_EndDay_comboBox.TabIndex = 130;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(197, 23);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 129;
            label5.Text = "일  ~ ";
            // 
            // Gas_StartDay_comboBox
            // 
            Gas_StartDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Gas_StartDay_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Gas_StartDay_comboBox.FormattingEnabled = true;
            Gas_StartDay_comboBox.Location = new Point(133, 19);
            Gas_StartDay_comboBox.Name = "Gas_StartDay_comboBox";
            Gas_StartDay_comboBox.Size = new Size(60, 23);
            Gas_StartDay_comboBox.TabIndex = 128;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(7, 23);
            label6.Name = "label6";
            label6.Size = new Size(123, 15);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Gas_kWh_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Gas_kWh_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Gas_kWh_dataGridView.Location = new Point(20, 180);
            Gas_kWh_dataGridView.Name = "Gas_kWh_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Gas_kWh_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Gas_kWh_dataGridView.RowHeadersVisible = false;
            Gas_kWh_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Gas_kWh_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Gas_kWh_dataGridView.RowTemplate.Height = 25;
            Gas_kWh_dataGridView.Size = new Size(928, 125);
            Gas_kWh_dataGridView.TabIndex = 120;
            // 
            // Gas_Remove_button
            // 
            Gas_Remove_button.BackColor = SystemColors.ControlLight;
            Gas_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Gas_Remove_button.FlatStyle = FlatStyle.System;
            Gas_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            Gas_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Gas_Add_button.Location = new Point(860, 19);
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            Gas_m3_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            Gas_m3_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Gas_m3_dataGridView.Location = new Point(20, 54);
            Gas_m3_dataGridView.Name = "Gas_m3_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            Gas_m3_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            Gas_m3_dataGridView.RowHeadersVisible = false;
            Gas_m3_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Gas_m3_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            Gas_m3_dataGridView.RowTemplate.Height = 25;
            Gas_m3_dataGridView.Size = new Size(928, 125);
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
            Elec_tabPage.Controls.Add(textBox8);
            Elec_tabPage.Controls.Add(textBox7);
            Elec_tabPage.Controls.Add(textBox6);
            Elec_tabPage.Controls.Add(textBox5);
            Elec_tabPage.Controls.Add(webView22);
            Elec_tabPage.Controls.Add(pictureBox2);
            Elec_tabPage.Controls.Add(checkBox8);
            Elec_tabPage.Controls.Add(checkBox6);
            Elec_tabPage.Controls.Add(checkBox7);
            Elec_tabPage.Controls.Add(checkBox5);
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
            Elec_tabPage.Size = new Size(969, 527);
            Elec_tabPage.TabIndex = 2;
            Elec_tabPage.Text = "전기사용량";
            // 
            // textBox8
            // 
            textBox8.BackColor = Color.White;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Enabled = false;
            textBox8.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox8.ForeColor = SystemColors.ControlDark;
            textBox8.Location = new Point(10, 473);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new Size(86, 15);
            textBox8.TabIndex = 139;
            textBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            textBox7.BackColor = Color.White;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Enabled = false;
            textBox7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox7.ForeColor = SystemColors.ControlDark;
            textBox7.Location = new Point(10, 446);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new Size(86, 15);
            textBox7.TabIndex = 138;
            textBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.White;
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Enabled = false;
            textBox6.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox6.ForeColor = SystemColors.ControlDark;
            textBox6.Location = new Point(10, 419);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(86, 15);
            textBox6.TabIndex = 137;
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.White;
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Enabled = false;
            textBox5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox5.ForeColor = SystemColors.ControlDark;
            textBox5.Location = new Point(10, 392);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(86, 15);
            textBox5.TabIndex = 136;
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Location = new Point(206, 320);
            webView22.Name = "webView22";
            webView22.Size = new Size(743, 199);
            webView22.TabIndex = 127;
            webView22.ZoomFactor = 1D;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(125, 394);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(75, 115);
            pictureBox2.TabIndex = 128;
            pictureBox2.TabStop = false;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Checked = true;
            checkBox8.CheckState = CheckState.Checked;
            checkBox8.Location = new Point(102, 475);
            checkBox8.Name = "checkBox8";
            checkBox8.RightToLeft = RightToLeft.Yes;
            checkBox8.Size = new Size(15, 14);
            checkBox8.TabIndex = 132;
            checkBox8.UseVisualStyleBackColor = true;
            checkBox8.CheckedChanged += checkBox8_CheckedChanged;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Checked = true;
            checkBox6.CheckState = CheckState.Checked;
            checkBox6.Location = new Point(102, 421);
            checkBox6.Name = "checkBox6";
            checkBox6.RightToLeft = RightToLeft.Yes;
            checkBox6.Size = new Size(15, 14);
            checkBox6.TabIndex = 130;
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Checked = true;
            checkBox7.CheckState = CheckState.Checked;
            checkBox7.Location = new Point(102, 448);
            checkBox7.Name = "checkBox7";
            checkBox7.RightToLeft = RightToLeft.Yes;
            checkBox7.Size = new Size(15, 14);
            checkBox7.TabIndex = 131;
            checkBox7.UseVisualStyleBackColor = true;
            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Checked = true;
            checkBox5.CheckState = CheckState.Checked;
            checkBox5.Location = new Point(102, 394);
            checkBox5.Name = "checkBox5";
            checkBox5.RightToLeft = RightToLeft.Yes;
            checkBox5.Size = new Size(15, 14);
            checkBox5.TabIndex = 129;
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(310, 27);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 126;
            label2.Text = "일";
            // 
            // Elec_EndDay_comboBox
            // 
            Elec_EndDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Elec_EndDay_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Elec_EndDay_comboBox.FormattingEnabled = true;
            Elec_EndDay_comboBox.Location = new Point(244, 23);
            Elec_EndDay_comboBox.Name = "Elec_EndDay_comboBox";
            Elec_EndDay_comboBox.Size = new Size(60, 23);
            Elec_EndDay_comboBox.TabIndex = 125;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(199, 27);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 124;
            label1.Text = "일  ~ ";
            // 
            // Elec_StartDay_comboBox
            // 
            Elec_StartDay_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Elec_StartDay_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Elec_StartDay_comboBox.FormattingEnabled = true;
            Elec_StartDay_comboBox.Location = new Point(135, 23);
            Elec_StartDay_comboBox.Name = "Elec_StartDay_comboBox";
            Elec_StartDay_comboBox.Size = new Size(60, 23);
            Elec_StartDay_comboBox.TabIndex = 123;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(10, 27);
            label25.Name = "label25";
            label25.Size = new Size(123, 15);
            label25.TabIndex = 122;
            label25.Text = "고지서 기준 사용기간";
            // 
            // Elec_Remove_button
            // 
            Elec_Remove_button.BackColor = SystemColors.ControlLight;
            Elec_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Elec_Remove_button.FlatStyle = FlatStyle.System;
            Elec_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            Elec_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Elec_Add_button.Location = new Point(866, 27);
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
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            Elec_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            Elec_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Elec_dataGridView.Location = new Point(21, 64);
            Elec_dataGridView.Name = "Elec_dataGridView";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            Elec_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            Elec_dataGridView.RowHeadersVisible = false;
            Elec_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            Elec_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            Elec_dataGridView.RowTemplate.Height = 25;
            Elec_dataGridView.Size = new Size(928, 188);
            Elec_dataGridView.TabIndex = 112;
            Elec_dataGridView.CellContentClick += Elec_dataGridView_CellContentClick;
            Elec_dataGridView.CellValueChanged += Elec_dataGridView_CellValueChanged;
            // 
            // Gas_Save_button
            // 
            Gas_Save_button.Controls.Add(Gas_tabPage);
            Gas_Save_button.Controls.Add(Elec_tabPage);
            Gas_Save_button.DisplayStyleProvider.BorderColor = SystemColors.ControlDark;
            Gas_Save_button.DisplayStyleProvider.BorderColorHot = SystemColors.ControlDark;
            Gas_Save_button.DisplayStyleProvider.CloserColor = Color.Empty;
            Gas_Save_button.DisplayStyleProvider.FocusTrack = true;
            Gas_Save_button.DisplayStyleProvider.HotTrack = true;
            Gas_Save_button.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            Gas_Save_button.DisplayStyleProvider.Opacity = 1F;
            Gas_Save_button.DisplayStyleProvider.Overlap = 0;
            Gas_Save_button.DisplayStyleProvider.Padding = new Point(6, 3);
            Gas_Save_button.DisplayStyleProvider.ShowTabCloser = false;
            Gas_Save_button.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            Gas_Save_button.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            Gas_Save_button.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            Gas_Save_button.HotTrack = true;
            Gas_Save_button.ItemSize = new Size(128, 20);
            Gas_Save_button.Location = new Point(12, 119);
            Gas_Save_button.Name = "Gas_Save_button";
            Gas_Save_button.SelectedIndex = 0;
            Gas_Save_button.Size = new Size(977, 556);
            Gas_Save_button.SizeMode = TabSizeMode.Fixed;
            Gas_Save_button.TabIndex = 146;
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
            Controls.Add(Gas_Save_button);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EnergyUse";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            Gas_tabPage.ResumeLayout(false);
            Gas_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Gas_kWh_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gas_m3_dataGridView).EndInit();
            Elec_tabPage.ResumeLayout(false);
            Elec_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Elec_dataGridView).EndInit();
            Gas_Save_button.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label3;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private TabPage Gas_tabPage;
        private TabPage Elec_tabPage;
        private DataGridView Elec_dataGridView;
        private CustomTabControl Gas_Save_button;
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
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private PictureBox pictureBox2;
        private CheckBox checkBox8;
        private CheckBox checkBox6;
        private CheckBox checkBox7;
        private CheckBox checkBox5;
        private TextBox textBox8;
        private TextBox textBox7;
        private TextBox textBox6;
        private TextBox textBox5;
    }
}