namespace main.subcontents.RESystem_PV
{
    partial class PV_ModuleDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PV_ModuleDB));
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            UserDB_Manufacture_textBox = new TextBox();
            GeneralPanel = new Panel();
            label27 = new Label();
            label26 = new Label();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            pictureBox6 = new PictureBox();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            pictureBox5 = new PictureBox();
            label17 = new Label();
            label16 = new Label();
            pictureBox4 = new PictureBox();
            label14 = new Label();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            label12 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label1 = new Label();
            Save_button = new Button();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            AddUserDB_button = new Button();
            label9 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            UserDB_Kpk_textbox = new TextBox();
            UserDB_celltype_comboBox = new ComboBox();
            PVModule_dataGridView = new DataGridView();
            label32 = new Label();
            UserDB_year_comboBox = new ComboBox();
            label29 = new Label();
            label31 = new Label();
            UserDB_output_textBox = new TextBox();
            label2 = new Label();
            label30 = new Label();
            label3 = new Label();
            label28 = new Label();
            UserDB_height_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            label11 = new Label();
            Deletebutton = new Button();
            UserDB_width_textBox = new TextBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PVModule_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Manufacture_textBox.Location = new Point(542, 10);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 109;
            UserDB_Manufacture_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label27);
            GeneralPanel.Controls.Add(label26);
            GeneralPanel.Controls.Add(label25);
            GeneralPanel.Controls.Add(label24);
            GeneralPanel.Controls.Add(label23);
            GeneralPanel.Controls.Add(label22);
            GeneralPanel.Controls.Add(pictureBox6);
            GeneralPanel.Controls.Add(label21);
            GeneralPanel.Controls.Add(label20);
            GeneralPanel.Controls.Add(label19);
            GeneralPanel.Controls.Add(label18);
            GeneralPanel.Controls.Add(pictureBox5);
            GeneralPanel.Controls.Add(label17);
            GeneralPanel.Controls.Add(label16);
            GeneralPanel.Controls.Add(pictureBox4);
            GeneralPanel.Controls.Add(label14);
            GeneralPanel.Controls.Add(pictureBox3);
            GeneralPanel.Controls.Add(pictureBox2);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label12);
            GeneralPanel.Controls.Add(label8);
            GeneralPanel.Controls.Add(label7);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(932, 339);
            GeneralPanel.TabIndex = 18;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label27.Location = new Point(458, 51);
            label27.Name = "label27";
            label27.Size = new Size(72, 15);
            label27.TabIndex = 23;
            label27.Text = "[사용자 DB]";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label26.Location = new Point(8, 51);
            label26.Name = "label26";
            label26.Size = new Size(88, 15);
            label26.TabIndex = 22;
            label26.Text = "[표준 DB 활용]";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(458, 51);
            label25.Name = "label25";
            label25.Size = new Size(342, 45);
            label25.TabIndex = 21;
            label25.Text = "                  : 태양광 모듈에 대한 제품 사양을 알 경우.\r\n-정격출력 값과 모듈의 가로길이, 세로길이를 함께 입력하시오.\r\n (길이를 모르면 1m로 입력 합니다.)";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(12, 51);
            label24.Name = "label24";
            label24.Size = new Size(405, 45);
            label24.TabIndex = 20;
            label24.Text = "                    : 사용자 DB 정보 없이 CELLTYPE, 전체 설치 용량을 알 경우\r\n-EN 15316-4-3을 기준으로 6가지 셀의 종류의 Kpk값을 제공한다.\r\n-이 값을 활용하여 전체 설치 용량에대한 면적값을 산출하여 반영한다.";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(809, 164);
            label23.Name = "label23";
            label23.Size = new Size(55, 15);
            label23.TabIndex = 19;
            label23.Text = "가로길이";
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label22.Location = new Point(718, 135);
            label22.Name = "label22";
            label22.Size = new Size(55, 15);
            label22.TabIndex = 18;
            label22.Text = "세로길이";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(771, 126);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(123, 39);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 17;
            pictureBox6.TabStop = false;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label21.Location = new Point(736, 108);
            label21.Name = "label21";
            label21.Size = new Size(43, 15);
            label21.TabIndex = 16;
            label21.Text = "루버형";
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(575, 248);
            label20.Name = "label20";
            label20.Size = new Size(55, 15);
            label20.TabIndex = 15;
            label20.Text = "가로길이";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(490, 176);
            label19.Name = "label19";
            label19.Size = new Size(55, 15);
            label19.TabIndex = 14;
            label19.Text = "세로길이";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label18.Location = new Point(495, 108);
            label18.Name = "label18";
            label18.Size = new Size(106, 15);
            label18.TabIndex = 13;
            label18.Text = "유리(커튼월, 창호)";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(538, 126);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(124, 124);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 12;
            pictureBox5.TabStop = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(321, 311);
            label17.Name = "label17";
            label17.Size = new Size(55, 15);
            label17.TabIndex = 11;
            label17.Text = "가로길이";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(229, 211);
            label16.Name = "label16";
            label16.Size = new Size(55, 15);
            label16.TabIndex = 10;
            label16.Text = "세로길이";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(265, 126);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(163, 189);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(229, 108);
            label14.Name = "label14";
            label14.Size = new Size(31, 15);
            label14.TabIndex = 8;
            label14.Text = "일반";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 262);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(67, 59);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(12, 198);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(67, 59);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 132);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(67, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(79, 306);
            label12.Name = "label12";
            label12.Size = new Size(31, 15);
            label12.TabIndex = 4;
            label12.Text = "박막\r\n";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(79, 236);
            label8.Name = "label8";
            label8.Size = new Size(116, 15);
            label8.TabIndex = 3;
            label8.Text = "다결정(Poly Cry. Si.)\r\n";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(79, 174);
            label7.Name = "label7";
            label7.Size = new Size(126, 15);
            label7.TabIndex = 2;
            label7.Text = "단결정(Single Cry. Si.)\r\n";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(12, 108);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 1;
            label6.Text = "CELL TYPE\r\n";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(410, 30);
            label1.TabIndex = 0;
            label1.Text = "- 표준 DB는 EN 15316-4-3 기준 태양광 모듈 CELL TYPE별 표준값 입니다.\r\n- DB를 추가하고자 하는 경우 각 항목의 값을 입력하고, + 버튼을 누르세요.\r\n";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(785, 747);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(249, 16);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "제품명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDBName_textBox.Location = new Point(310, 12);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextAlign = HorizontalAlignment.Center;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(71, 11);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 89;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(239, 54);
            label9.Name = "label9";
            label9.Size = new Size(63, 15);
            label9.TabIndex = 44;
            label9.Text = "CELL TYPE";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(UserDB_Kpk_textbox);
            panel1.Controls.Add(UserDB_celltype_comboBox);
            panel1.Controls.Add(PVModule_dataGridView);
            panel1.Controls.Add(label32);
            panel1.Controls.Add(UserDB_year_comboBox);
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label31);
            panel1.Controls.Add(UserDB_output_textBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label28);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(UserDB_height_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(UserDB_width_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label9);
            panel1.Location = new Point(0, 333);
            panel1.Name = "panel1";
            panel1.Size = new Size(932, 408);
            panel1.TabIndex = 27;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(481, 53);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 120;
            label5.Text = "Kpk";
            // 
            // UserDB_Kpk_textbox
            // 
            UserDB_Kpk_textbox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_Kpk_textbox.BorderStyle = BorderStyle.None;
            UserDB_Kpk_textbox.ForeColor = SystemColors.ScrollBar;
            UserDB_Kpk_textbox.Location = new Point(542, 50);
            UserDB_Kpk_textbox.Name = "UserDB_Kpk_textbox";
            UserDB_Kpk_textbox.Size = new Size(120, 16);
            UserDB_Kpk_textbox.TabIndex = 121;
            UserDB_Kpk_textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // UserDB_celltype_comboBox
            // 
            UserDB_celltype_comboBox.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
            UserDB_celltype_comboBox.Location = new Point(310, 50);
            UserDB_celltype_comboBox.Name = "UserDB_celltype_comboBox";
            UserDB_celltype_comboBox.Size = new Size(120, 24);
            UserDB_celltype_comboBox.TabIndex = 119;
            UserDB_celltype_comboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // PVModule_dataGridView
            // 
            PVModule_dataGridView.AllowUserToAddRows = false;
            PVModule_dataGridView.AllowUserToDeleteRows = false;
            PVModule_dataGridView.AllowUserToResizeColumns = false;
            PVModule_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PVModule_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PVModule_dataGridView.BackgroundColor = SystemColors.Control;
            PVModule_dataGridView.BorderStyle = BorderStyle.None;
            PVModule_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PVModule_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            PVModule_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            PVModule_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PVModule_dataGridView.Location = new Point(0, 127);
            PVModule_dataGridView.Name = "PVModule_dataGridView";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            PVModule_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            PVModule_dataGridView.RowHeadersVisible = false;
            PVModule_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            PVModule_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            PVModule_dataGridView.RowTemplate.Height = 25;
            PVModule_dataGridView.Size = new Size(932, 281);
            PVModule_dataGridView.TabIndex = 19;
            PVModule_dataGridView.CellContentClick += Door_dataGridView_CellContentClick;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(897, 93);
            label32.Name = "label32";
            label32.Size = new Size(18, 15);
            label32.TabIndex = 118;
            label32.Text = "W";
            // 
            // UserDB_year_comboBox
            // 
            UserDB_year_comboBox.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
            UserDB_year_comboBox.FormattingEnabled = true;
            UserDB_year_comboBox.Location = new Point(774, 8);
            UserDB_year_comboBox.Name = "UserDB_year_comboBox";
            UserDB_year_comboBox.Size = new Size(120, 24);
            UserDB_year_comboBox.TabIndex = 53;
            UserDB_year_comboBox.SelectedIndexChanged += UserDB_year_comboBox_SelectedIndexChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(713, 91);
            label29.Name = "label29";
            label29.Size = new Size(55, 15);
            label29.TabIndex = 115;
            label29.Text = "정격출력";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(665, 93);
            label31.Name = "label31";
            label31.Size = new Size(18, 15);
            label31.TabIndex = 117;
            label31.Text = "m";
            // 
            // UserDB_output_textBox
            // 
            UserDB_output_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_output_textBox.Location = new Point(774, 88);
            UserDB_output_textBox.Name = "UserDB_output_textBox";
            UserDB_output_textBox.Size = new Size(120, 23);
            UserDB_output_textBox.TabIndex = 116;
            UserDB_output_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_output_textBox.TextChanged += UserDB_output_textBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(715, 13);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 110;
            label2.Text = "제작년도";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(433, 93);
            label30.Name = "label30";
            label30.Size = new Size(18, 15);
            label30.TabIndex = 111;
            label30.Text = "m";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(481, 14);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 108;
            label3.Text = "제조사";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(481, 91);
            label28.Name = "label28";
            label28.Size = new Size(55, 15);
            label28.TabIndex = 113;
            label28.Text = "세로길이";
            // 
            // UserDB_height_textBox
            // 
            UserDB_height_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_height_textBox.Location = new Point(542, 88);
            UserDB_height_textBox.Name = "UserDB_height_textBox";
            UserDB_height_textBox.Size = new Size(120, 23);
            UserDB_height_textBox.TabIndex = 114;
            UserDB_height_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_height_textBox.TextChanged += textBox2_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(130, 16);
            label15.Name = "label15";
            label15.Size = new Size(31, 15);
            label15.TabIndex = 106;
            label15.Text = "번호";
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(173, 15);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(68, 16);
            UserNum_textBox.TabIndex = 107;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(8, 15);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 96;
            label4.Text = "사용자DB";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(249, 91);
            label11.Name = "label11";
            label11.Size = new Size(55, 15);
            label11.TabIndex = 111;
            label11.Text = "가로길이";
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(98, 11);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // UserDB_width_textBox
            // 
            UserDB_width_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_width_textBox.Location = new Point(310, 88);
            UserDB_width_textBox.Name = "UserDB_width_textBox";
            UserDB_width_textBox.Size = new Size(120, 23);
            UserDB_width_textBox.TabIndex = 112;
            UserDB_width_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_width_textBox.TextChanged += textBox1_TextChanged;
            // 
            // PV_ModuleDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(932, 784);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "PV_ModuleDB";
            Text = "PV_ModuleDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PVModule_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private Button Save_button;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Button AddUserDB_button;
        private Label label9;
        private Panel panel1;
        private Button Deletebutton;
        private Label label4;
        private Label label15;
        private TextBox UserNum_textBox;
        private Label label3;
        private TextBox UserDB_Manufacture_textBox;
        private DataGridView PVModule_dataGridView;
        private Label label2;
        private ComboBox UserDB_year_comboBox;
        private Label label7;
        private Label label6;
        private Label label12;
        private Label label8;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label label19;
        private Label label18;
        private PictureBox pictureBox5;
        private Label label17;
        private Label label16;
        private PictureBox pictureBox4;
        private Label label14;
        private PictureBox pictureBox6;
        private Label label21;
        private Label label20;
        private Label label23;
        private Label label22;
        private Label label24;
        private Label label25;
        private Label label27;
        private Label label26;
        private Label label11;
        private TextBox UserDB_width_textBox;
        private Label label28;
        private TextBox UserDB_height_textBox;
        private Label label29;
        private TextBox UserDB_output_textBox;
        private Label label30;
        private Label label31;
        private Label label32;
        private ComboBox UserDB_celltype_comboBox;
        private Label label5;
        private TextBox UserDB_Kpk_textbox;
    }
}