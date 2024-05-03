namespace main.contents.Alt
{
    partial class AltMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltMain));
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            AltMainPanel = new Panel();
            label1 = new Label();
            Type_textBox = new TextBox();
            Icon_pictureBox = new PictureBox();
            Save_button = new Button();
            panel5 = new Panel();
            label2 = new Label();
            Alt_dataGridView = new DataGridView();
            Alt_Remove_button = new Button();
            Alt_Add_button = new Button();
            q50_textBox = new TextBox();
            q50_label2 = new Label();
            q50_label1 = new Label();
            label4 = new Label();
            tabControl1 = new CustomTabControl();
            Main_tabPage = new TabPage();
            label6 = new Label();
            RuleResult_dataGridView = new DataGridView();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Wall_tabPage = new TabPage();
            label7 = new Label();
            WallEx_comboBox = new CustomComboBox();
            WallEx_label = new Label();
            WallRemodelingType_comboBox = new CustomComboBox();
            label11 = new Label();
            WallAlt_textBox = new TextBox();
            WallAlt_button = new Button();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            AltMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            Main_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RuleResult_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            Wall_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // AltMainPanel
            // 
            AltMainPanel.BackColor = Color.White;
            AltMainPanel.Controls.Add(label1);
            AltMainPanel.Controls.Add(Type_textBox);
            AltMainPanel.Controls.Add(Icon_pictureBox);
            AltMainPanel.Location = new Point(12, 12);
            AltMainPanel.Name = "AltMainPanel";
            AltMainPanel.Size = new Size(977, 73);
            AltMainPanel.TabIndex = 17;
            AltMainPanel.Paint += AltMainPanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(116, 27);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 112;
            label1.Text = "리모델링안";
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = Color.White;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(177, 57);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 96;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 9);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 91;
            Icon_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1020, 633);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 92;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(label2);
            panel5.Controls.Add(Alt_dataGridView);
            panel5.Controls.Add(Alt_Remove_button);
            panel5.Controls.Add(Alt_Add_button);
            panel5.Controls.Add(q50_textBox);
            panel5.Controls.Add(q50_label2);
            panel5.Controls.Add(q50_label1);
            panel5.Location = new Point(14, 109);
            panel5.Name = "panel5";
            panel5.Size = new Size(976, 159);
            panel5.TabIndex = 105;
            panel5.Paint += panel5_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(477, 13);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 142;
            label2.Text = "요소기술 결정";
            // 
            // Alt_dataGridView
            // 
            Alt_dataGridView.AllowUserToAddRows = false;
            Alt_dataGridView.AllowUserToDeleteRows = false;
            Alt_dataGridView.AllowUserToResizeColumns = false;
            Alt_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Alt_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Alt_dataGridView.BackgroundColor = SystemColors.Window;
            Alt_dataGridView.BorderStyle = BorderStyle.None;
            Alt_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Alt_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Alt_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Alt_dataGridView.Location = new Point(565, 13);
            Alt_dataGridView.Name = "Alt_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Alt_dataGridView.RowHeadersVisible = false;
            Alt_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Alt_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Alt_dataGridView.RowTemplate.Height = 25;
            Alt_dataGridView.Size = new Size(224, 129);
            Alt_dataGridView.TabIndex = 141;
            // 
            // Alt_Remove_button
            // 
            Alt_Remove_button.BackColor = SystemColors.ControlLight;
            Alt_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Alt_Remove_button.FlatStyle = FlatStyle.System;
            Alt_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Alt_Remove_button.Location = new Point(841, 11);
            Alt_Remove_button.Margin = new Padding(0);
            Alt_Remove_button.Name = "Alt_Remove_button";
            Alt_Remove_button.Size = new Size(23, 23);
            Alt_Remove_button.TabIndex = 140;
            Alt_Remove_button.Text = "-";
            Alt_Remove_button.UseVisualStyleBackColor = false;
            Alt_Remove_button.Click += Alt_Remove_button_Click;
            // 
            // Alt_Add_button
            // 
            Alt_Add_button.BackColor = SystemColors.ControlLight;
            Alt_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Alt_Add_button.FlatStyle = FlatStyle.System;
            Alt_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Alt_Add_button.Location = new Point(804, 11);
            Alt_Add_button.Margin = new Padding(0);
            Alt_Add_button.Name = "Alt_Add_button";
            Alt_Add_button.Size = new Size(23, 23);
            Alt_Add_button.TabIndex = 139;
            Alt_Add_button.Text = "+";
            Alt_Add_button.UseVisualStyleBackColor = false;
            Alt_Add_button.Click += Alt_Add_button_Click;
            // 
            // q50_textBox
            // 
            q50_textBox.BackColor = Color.White;
            q50_textBox.BorderStyle = BorderStyle.FixedSingle;
            q50_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            q50_textBox.ForeColor = SystemColors.ControlText;
            q50_textBox.Location = new Point(114, 13);
            q50_textBox.Name = "q50_textBox";
            q50_textBox.Size = new Size(120, 22);
            q50_textBox.TabIndex = 137;
            q50_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // q50_label2
            // 
            q50_label2.AutoSize = true;
            q50_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            q50_label2.ForeColor = SystemColors.ControlText;
            q50_label2.Location = new Point(234, 16);
            q50_label2.Name = "q50_label2";
            q50_label2.Size = new Size(18, 16);
            q50_label2.TabIndex = 138;
            q50_label2.Text = "원";
            // 
            // q50_label1
            // 
            q50_label1.AutoSize = true;
            q50_label1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            q50_label1.Location = new Point(27, 16);
            q50_label1.Name = "q50_label1";
            q50_label1.Size = new Size(47, 15);
            q50_label1.TabIndex = 136;
            q50_label1.Text = "총 예산";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 90);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 94;
            label4.Text = "리모델링 의사결정";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Main_tabPage);
            tabControl1.Controls.Add(Wall_tabPage);
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
            tabControl1.Location = new Point(12, 274);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 388);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 106;
            // 
            // Main_tabPage
            // 
            Main_tabPage.Controls.Add(label6);
            Main_tabPage.Controls.Add(RuleResult_dataGridView);
            Main_tabPage.Controls.Add(label5);
            Main_tabPage.Controls.Add(pictureBox2);
            Main_tabPage.Controls.Add(webView22);
            Main_tabPage.Location = new Point(4, 25);
            Main_tabPage.Name = "Main_tabPage";
            Main_tabPage.Padding = new Padding(3);
            Main_tabPage.Size = new Size(969, 359);
            Main_tabPage.TabIndex = 1;
            Main_tabPage.Text = "법규기반 검토";
            Main_tabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(6, 20);
            label6.Name = "label6";
            label6.Size = new Size(231, 15);
            label6.TabIndex = 143;
            label6.Text = "권장[법규] 성능 적용 시 에너지 절감 순위";
            // 
            // RuleResult_dataGridView
            // 
            RuleResult_dataGridView.AllowUserToAddRows = false;
            RuleResult_dataGridView.AllowUserToDeleteRows = false;
            RuleResult_dataGridView.AllowUserToResizeColumns = false;
            RuleResult_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RuleResult_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            RuleResult_dataGridView.BackgroundColor = SystemColors.Window;
            RuleResult_dataGridView.BorderStyle = BorderStyle.None;
            RuleResult_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            RuleResult_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            RuleResult_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            RuleResult_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RuleResult_dataGridView.Location = new Point(652, 53);
            RuleResult_dataGridView.Name = "RuleResult_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            RuleResult_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            RuleResult_dataGridView.RowHeadersVisible = false;
            RuleResult_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            RuleResult_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            RuleResult_dataGridView.RowTemplate.Height = 25;
            RuleResult_dataGridView.Size = new Size(294, 288);
            RuleResult_dataGridView.TabIndex = 142;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 53);
            label5.Name = "label5";
            label5.Size = new Size(122, 15);
            label5.TabIndex = 25;
            label5.Text = "에너지절감량(kWh/a)";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(134, 53);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 26);
            pictureBox2.TabIndex = 24;
            pictureBox2.TabStop = false;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Location = new Point(6, 85);
            webView22.Name = "webView22";
            webView22.Size = new Size(610, 193);
            webView22.TabIndex = 23;
            webView22.ZoomFactor = 1D;
            // 
            // Wall_tabPage
            // 
            Wall_tabPage.BackColor = Color.White;
            Wall_tabPage.Controls.Add(label7);
            Wall_tabPage.Controls.Add(WallEx_comboBox);
            Wall_tabPage.Controls.Add(WallEx_label);
            Wall_tabPage.Controls.Add(WallRemodelingType_comboBox);
            Wall_tabPage.Controls.Add(label11);
            Wall_tabPage.Controls.Add(WallAlt_textBox);
            Wall_tabPage.Controls.Add(WallAlt_button);
            Wall_tabPage.Controls.Add(label3);
            Wall_tabPage.Controls.Add(dataGridView1);
            Wall_tabPage.Location = new Point(4, 25);
            Wall_tabPage.Name = "Wall_tabPage";
            Wall_tabPage.Padding = new Padding(3);
            Wall_tabPage.Size = new Size(969, 359);
            Wall_tabPage.TabIndex = 0;
            Wall_tabPage.Text = "외벽";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(470, 55);
            label7.Name = "label7";
            label7.Size = new Size(95, 15);
            label7.TabIndex = 152;
            label7.Text = "리모델링안 선택";
            // 
            // WallEx_ComboBox
            // 
            WallEx_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WallEx_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WallEx_comboBox.FormattingEnabled = true;
            WallEx_comboBox.Location = new Point(821, 20);
            WallEx_comboBox.Name = "WallEx_ComboBox";
            WallEx_comboBox.Size = new Size(120, 23);
            WallEx_comboBox.TabIndex = 151;
            WallEx_comboBox.Visible = false;
            WallEx_comboBox.SelectedIndexChanged += WallEx_comboBox_SelectedIndexChanged;
            // 
            // WallEx_label
            // 
            WallEx_label.AutoSize = true;
            WallEx_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            WallEx_label.Location = new Point(727, 24);
            WallEx_label.Name = "WallEx_label";
            WallEx_label.Size = new Size(67, 15);
            WallEx_label.TabIndex = 150;
            WallEx_label.Text = "외장재유형";
            WallEx_label.Visible = false;
            // 
            // WallRemodelingType_comboBox
            // 
            WallRemodelingType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WallRemodelingType_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WallRemodelingType_comboBox.FormattingEnabled = true;
            WallRemodelingType_comboBox.Location = new Point(580, 20);
            WallRemodelingType_comboBox.Name = "WallRemodelingType_comboBox";
            WallRemodelingType_comboBox.Size = new Size(120, 23);
            WallRemodelingType_comboBox.TabIndex = 149;
            WallRemodelingType_comboBox.SelectedIndexChanged += WallRemodelingType_comboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(470, 24);
            label11.Name = "label11";
            label11.Size = new Size(83, 15);
            label11.TabIndex = 148;
            label11.Text = "리모델링 방식";
            // 
            // WallAlt_textBox
            // 
            WallAlt_textBox.BackColor = Color.White;
            WallAlt_textBox.BorderStyle = BorderStyle.None;
            WallAlt_textBox.Enabled = false;
            WallAlt_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WallAlt_textBox.ForeColor = SystemColors.ControlDark;
            WallAlt_textBox.Location = new Point(580, 55);
            WallAlt_textBox.Name = "WallAlt_textBox";
            WallAlt_textBox.Size = new Size(120, 15);
            WallAlt_textBox.TabIndex = 147;
            WallAlt_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // WallAlt_button
            // 
            WallAlt_button.BackColor = SystemColors.ControlLight;
            WallAlt_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WallAlt_button.FlatStyle = FlatStyle.System;
            WallAlt_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            WallAlt_button.Location = new Point(723, 51);
            WallAlt_button.Margin = new Padding(0);
            WallAlt_button.Name = "WallAlt_button";
            WallAlt_button.Size = new Size(23, 23);
            WallAlt_button.TabIndex = 145;
            WallAlt_button.Text = "+";
            WallAlt_button.UseVisualStyleBackColor = false;
            WallAlt_button.Click += WallAlt_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(26, 20);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 144;
            label3.Text = "기존 외벽";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(26, 38);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle9;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(379, 288);
            dataGridView1.TabIndex = 143;
            // 
            // AltMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl1);
            Controls.Add(panel5);
            Controls.Add(label4);
            Controls.Add(Save_button);
            Controls.Add(AltMainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AltMain";
            Text = "Form3";
            AltMainPanel.ResumeLayout(false);
            AltMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).EndInit();
            tabControl1.ResumeLayout(false);
            Main_tabPage.ResumeLayout(false);
            Main_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RuleResult_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            Wall_tabPage.ResumeLayout(false);
            Wall_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel AltMainPanel;
        private PictureBox Icon_pictureBox;
        private TextBox Type_textBox;
        private Button Previous_button;
        private Button Save_button;
        private Label label1;
        private ComboBox Diagnosis_comboBox;
        private ComboBox Target_comboBox;
        private ComboBox RoofType_comboBox;
        private ComboBox WallType_comboBox;
        private Panel panel5;
        private Label label4;
        private TextBox q50_textBox;
        private Label q50_label2;
        private Label q50_label1;
        private Label label2;
        private DataGridView Alt_dataGridView;
        private Button Alt_Remove_button;
        private Button Alt_Add_button;
        private CustomTabControl tabControl1;
        private TabPage Wall_tabPage;
        private TabPage Main_tabPage;
        private Label label5;
        private PictureBox pictureBox2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private DataGridView RuleResult_dataGridView;
        private Label label6;
        private DataGridView dataGridView1;
        private Label label3;
        private TextBox WallAlt_textBox;
        private Button WallAlt_button;
        private CustomComboBox WallRemodelingType_comboBox;
        private Label label11;
        private CustomComboBox WallEx_comboBox;
        private Label WallEx_label;
        private Label label7;
    }
}