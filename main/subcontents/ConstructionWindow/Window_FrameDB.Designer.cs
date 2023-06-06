using System.Windows.Forms;

namespace main.subcontents
{
    partial class Window_FrameDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_FrameDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Frame_dataGridView = new DataGridView();
            Save_button = new Button();
            panel1 = new Panel();
            UserDB_FrameShape_textBox = new TextBox();
            Import_button = new Button();
            label14 = new Label();
            label36 = new Label();
            UserDBCertification_pictureBox = new PictureBox();
            UserDB_Frame_pictureBox = new PictureBox();
            label15 = new Label();
            UserDBFramedA_textBox = new TextBox();
            label25 = new Label();
            label20 = new Label();
            label21 = new Label();
            UserDBFramedC_textBox = new TextBox();
            label22 = new Label();
            label23 = new Label();
            UserDBFramedB_textBox = new TextBox();
            label24 = new Label();
            label11 = new Label();
            label5 = new Label();
            label3 = new Label();
            UserDBGlass_comboBox = new ComboBox();
            UserDB_FrameMaterial_comboBox = new ComboBox();
            UserDB_FrameShape_comboBox = new ComboBox();
            UserDB_FrameType_comboBox = new ComboBox();
            label19 = new Label();
            UserDBUw_textBox = new TextBox();
            label16 = new Label();
            UserDB_PsiFix_textBox = new TextBox();
            label17 = new Label();
            label18 = new Label();
            UserDB_PsiOpen_textBox = new TextBox();
            UserDBSpacer_comboBox = new ComboBox();
            label8 = new Label();
            label29 = new Label();
            label30 = new Label();
            UserDB_Ug_textBox = new TextBox();
            label32 = new Label();
            label6 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            label4 = new Label();
            label10 = new Label();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            label9 = new Label();
            label7 = new Label();
            panel3 = new Panel();
            UserNum_textBox = new TextBox();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            label35 = new Label();
            UserDB_UfA_textBox = new TextBox();
            label26 = new Label();
            label27 = new Label();
            label28 = new Label();
            UserDB_UfC_textBox = new TextBox();
            label31 = new Label();
            label33 = new Label();
            UserDB_UfB_textBox = new TextBox();
            label34 = new Label();
            splitter1 = new Splitter();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Frame_dataGridView).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UserDBCertification_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)UserDB_Frame_pictureBox).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1119, 181);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(767, 6);
            label2.Name = "label2";
            label2.Size = new Size(249, 165);
            label2.TabIndex = 2;
            label2.Text = "Uf,A: 길이 가중 ①②③ 프레임 평균 열관류율\r\n\r\nUf,B: 길이 가중 ④⑤⑥ 프레임 평균 열관류율\r\n\r\nUf,C: ⑦ 프레임 열관류율\r\n\r\ndA: ①②③ 길이 가중 프레임 평균 두께\r\n\r\ndB:  ④⑤⑥ 길이 가중 프레임 평균 두께\r\n\r\ndC: ⑦ 프레임 두께";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(508, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(201, 153);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 66);
            label1.Name = "label1";
            label1.Size = new Size(490, 45);
            label1.TabIndex = 0;
            label1.Text = "- 표준DB는 국내 일반적인 프레임의 시뮬레이션 결과(ISO 10077기준)입니다.\r\n\r\n- 적용 프레임 사양(시험성적서)이 있다면 직접 입력하시고, 없다면 표준DB 중 선택하세요.";
            // 
            // Frame_dataGridView
            // 
            Frame_dataGridView.AllowUserToAddRows = false;
            Frame_dataGridView.AllowUserToDeleteRows = false;
            Frame_dataGridView.AllowUserToResizeColumns = false;
            Frame_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Frame_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Frame_dataGridView.BackgroundColor = SystemColors.Control;
            Frame_dataGridView.BorderStyle = BorderStyle.None;
            Frame_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Frame_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Frame_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Frame_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Frame_dataGridView.Location = new Point(0, 442);
            Frame_dataGridView.Name = "Frame_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Frame_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Frame_dataGridView.RowHeadersVisible = false;
            Frame_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Frame_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Frame_dataGridView.RowTemplate.Height = 25;
            Frame_dataGridView.Size = new Size(1119, 224);
            Frame_dataGridView.TabIndex = 19;
            Frame_dataGridView.CellContentClick += Frame_dataGridView_CellContentClick;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(984, 672);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(splitter1);
            panel1.Controls.Add(UserDB_FrameShape_textBox);
            panel1.Controls.Add(Import_button);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label36);
            panel1.Controls.Add(UserDBCertification_pictureBox);
            panel1.Controls.Add(UserDB_Frame_pictureBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserDBFramedA_textBox);
            panel1.Controls.Add(label25);
            panel1.Controls.Add(label20);
            panel1.Controls.Add(label21);
            panel1.Controls.Add(UserDBFramedC_textBox);
            panel1.Controls.Add(label22);
            panel1.Controls.Add(label23);
            panel1.Controls.Add(UserDBFramedB_textBox);
            panel1.Controls.Add(label24);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserDBGlass_comboBox);
            panel1.Controls.Add(UserDB_FrameMaterial_comboBox);
            panel1.Controls.Add(UserDB_FrameShape_comboBox);
            panel1.Controls.Add(UserDB_FrameType_comboBox);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(UserDBUw_textBox);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(UserDB_PsiFix_textBox);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(UserDB_PsiOpen_textBox);
            panel1.Controls.Add(UserDBSpacer_comboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(UserDB_Ug_textBox);
            panel1.Controls.Add(label32);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(0, 179);
            panel1.Name = "panel1";
            panel1.Size = new Size(1121, 225);
            panel1.TabIndex = 27;
            // 
            // UserDB_FrameShape_textBox
            // 
            UserDB_FrameShape_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_FrameShape_textBox.BorderStyle = BorderStyle.None;
            UserDB_FrameShape_textBox.Enabled = false;
            UserDB_FrameShape_textBox.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            UserDB_FrameShape_textBox.Location = new Point(831, 32);
            UserDB_FrameShape_textBox.Name = "UserDB_FrameShape_textBox";
            UserDB_FrameShape_textBox.Size = new Size(120, 18);
            UserDB_FrameShape_textBox.TabIndex = 171;
            UserDB_FrameShape_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Import_button
            // 
            Import_button.BackColor = SystemColors.ControlLight;
            Import_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Import_button.FlatStyle = FlatStyle.System;
            Import_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Import_button.Location = new Point(978, 0);
            Import_button.Margin = new Padding(0);
            Import_button.Name = "Import_button";
            Import_button.Size = new Size(141, 28);
            Import_button.TabIndex = 163;
            Import_button.Text = "성적서 Import";
            Import_button.UseVisualStyleBackColor = false;
            Import_button.Click += Import_button_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(758, 183);
            label14.Name = "label14";
            label14.Size = new Size(48, 15);
            label14.TabIndex = 170;
            label14.Text = "W/m²·K";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label36.Location = new Point(18, 19);
            label36.Name = "label36";
            label36.Size = new Size(60, 15);
            label36.TabIndex = 169;
            label36.Text = "사용자DB";
            // 
            // UserDBCertification_pictureBox
            // 
            UserDBCertification_pictureBox.BackColor = SystemColors.Control;
            UserDBCertification_pictureBox.Location = new Point(978, 25);
            UserDBCertification_pictureBox.Name = "UserDBCertification_pictureBox";
            UserDBCertification_pictureBox.Size = new Size(141, 199);
            UserDBCertification_pictureBox.TabIndex = 162;
            UserDBCertification_pictureBox.TabStop = false;
            // 
            // UserDB_Frame_pictureBox
            // 
            UserDB_Frame_pictureBox.Location = new Point(807, 56);
            UserDB_Frame_pictureBox.Name = "UserDB_Frame_pictureBox";
            UserDB_Frame_pictureBox.Size = new Size(166, 142);
            UserDB_Frame_pictureBox.TabIndex = 161;
            UserDB_Frame_pictureBox.TabStop = false;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(281, 175);
            label15.Name = "label15";
            label15.Size = new Size(55, 30);
            label15.TabIndex = 156;
            label15.Text = "선형\r\n열관류율";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserDBFramedA_textBox
            // 
            UserDBFramedA_textBox.Location = new Point(149, 84);
            UserDBFramedA_textBox.Name = "UserDBFramedA_textBox";
            UserDBFramedA_textBox.Size = new Size(120, 23);
            UserDBFramedA_textBox.TabIndex = 144;
            UserDBFramedA_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(272, 88);
            label25.Name = "label25";
            label25.Size = new Size(18, 15);
            label25.TabIndex = 143;
            label25.Text = "m";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(758, 88);
            label20.Name = "label20";
            label20.Size = new Size(18, 15);
            label20.TabIndex = 141;
            label20.Text = "m";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(586, 88);
            label21.Name = "label21";
            label21.Size = new Size(47, 15);
            label21.TabIndex = 139;
            label21.Text = "중간(C)";
            // 
            // UserDBFramedC_textBox
            // 
            UserDBFramedC_textBox.Location = new Point(633, 84);
            UserDBFramedC_textBox.Name = "UserDBFramedC_textBox";
            UserDBFramedC_textBox.Size = new Size(120, 23);
            UserDBFramedC_textBox.TabIndex = 140;
            UserDBFramedC_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(514, 88);
            label22.Name = "label22";
            label22.Size = new Size(18, 15);
            label22.TabIndex = 138;
            label22.Text = "m";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(339, 88);
            label23.Name = "label23";
            label23.Size = new Size(46, 15);
            label23.TabIndex = 136;
            label23.Text = "고정(B)";
            // 
            // UserDBFramedB_textBox
            // 
            UserDBFramedB_textBox.Location = new Point(391, 84);
            UserDBFramedB_textBox.Name = "UserDBFramedB_textBox";
            UserDBFramedB_textBox.Size = new Size(120, 23);
            UserDBFramedB_textBox.TabIndex = 137;
            UserDBFramedB_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(68, 88);
            label24.Name = "label24";
            label24.Size = new Size(75, 15);
            label24.TabIndex = 134;
            label24.Text = "두께 개폐(A)";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(30, 182);
            label11.Name = "label11";
            label11.Size = new Size(34, 17);
            label11.TabIndex = 133;
            label11.Text = "간봉";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(30, 133);
            label5.Name = "label5";
            label5.Size = new Size(34, 17);
            label5.TabIndex = 132;
            label5.Text = "유리";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(24, 59);
            label3.Name = "label3";
            label3.Size = new Size(47, 17);
            label3.TabIndex = 131;
            label3.Text = "프레임";
            // 
            // UserDBGlass_comboBox
            // 
            UserDBGlass_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBGlass_comboBox.FormattingEnabled = true;
            UserDBGlass_comboBox.Location = new Point(149, 130);
            UserDBGlass_comboBox.Name = "UserDBGlass_comboBox";
            UserDBGlass_comboBox.Size = new Size(120, 23);
            UserDBGlass_comboBox.TabIndex = 130;
            UserDBGlass_comboBox.SelectedIndexChanged += UserDBGlass_comboBox_SelectedIndexChanged;
            // 
            // UserDB_FrameMaterial_comboBox
            // 
            UserDB_FrameMaterial_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDB_FrameMaterial_comboBox.FormattingEnabled = true;
            UserDB_FrameMaterial_comboBox.Location = new Point(633, 56);
            UserDB_FrameMaterial_comboBox.Name = "UserDB_FrameMaterial_comboBox";
            UserDB_FrameMaterial_comboBox.Size = new Size(120, 23);
            UserDB_FrameMaterial_comboBox.TabIndex = 129;
            UserDB_FrameMaterial_comboBox.SelectedIndexChanged += UserDB_FrameMaterial_comboBox_SelectedIndexChanged;
            // 
            // UserDB_FrameShape_comboBox
            // 
            UserDB_FrameShape_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDB_FrameShape_comboBox.FormattingEnabled = true;
            UserDB_FrameShape_comboBox.Location = new Point(391, 56);
            UserDB_FrameShape_comboBox.Name = "UserDB_FrameShape_comboBox";
            UserDB_FrameShape_comboBox.Size = new Size(120, 23);
            UserDB_FrameShape_comboBox.TabIndex = 128;
            UserDB_FrameShape_comboBox.SelectedIndexChanged += UserDB_FrameShape_comboBox_SelectedIndexChanged;
            // 
            // UserDB_FrameType_comboBox
            // 
            UserDB_FrameType_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDB_FrameType_comboBox.FormattingEnabled = true;
            UserDB_FrameType_comboBox.Location = new Point(149, 56);
            UserDB_FrameType_comboBox.Name = "UserDB_FrameType_comboBox";
            UserDB_FrameType_comboBox.Size = new Size(120, 23);
            UserDB_FrameType_comboBox.TabIndex = 127;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(758, 19);
            label19.Name = "label19";
            label19.Size = new Size(48, 15);
            label19.TabIndex = 125;
            label19.Text = "W/m²·K";
            // 
            // UserDBUw_textBox
            // 
            UserDBUw_textBox.Location = new Point(633, 15);
            UserDBUw_textBox.Name = "UserDBUw_textBox";
            UserDBUw_textBox.Size = new Size(120, 23);
            UserDBUw_textBox.TabIndex = 124;
            UserDBUw_textBox.TextAlign = HorizontalAlignment.Center;
            UserDBUw_textBox.TextChanged += UserDBUw_textBox_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(594, 183);
            label16.Name = "label16";
            label16.Size = new Size(39, 15);
            label16.TabIndex = 121;
            label16.Text = "(고정)";
            // 
            // UserDB_PsiFix_textBox
            // 
            UserDB_PsiFix_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_PsiFix_textBox.BorderStyle = BorderStyle.None;
            UserDB_PsiFix_textBox.Enabled = false;
            UserDB_PsiFix_textBox.Location = new Point(633, 182);
            UserDB_PsiFix_textBox.Name = "UserDB_PsiFix_textBox";
            UserDB_PsiFix_textBox.Size = new Size(120, 16);
            UserDB_PsiFix_textBox.TabIndex = 122;
            UserDB_PsiFix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(514, 183);
            label17.Name = "label17";
            label17.Size = new Size(48, 15);
            label17.TabIndex = 120;
            label17.Text = "W/m²·K";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(346, 183);
            label18.Name = "label18";
            label18.Size = new Size(39, 15);
            label18.TabIndex = 118;
            label18.Text = "(개폐)";
            // 
            // UserDB_PsiOpen_textBox
            // 
            UserDB_PsiOpen_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_PsiOpen_textBox.BorderStyle = BorderStyle.None;
            UserDB_PsiOpen_textBox.Enabled = false;
            UserDB_PsiOpen_textBox.Location = new Point(387, 182);
            UserDB_PsiOpen_textBox.Name = "UserDB_PsiOpen_textBox";
            UserDB_PsiOpen_textBox.Size = new Size(120, 16);
            UserDB_PsiOpen_textBox.TabIndex = 119;
            UserDB_PsiOpen_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // UserDBSpacer_comboBox
            // 
            UserDBSpacer_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBSpacer_comboBox.FormattingEnabled = true;
            UserDBSpacer_comboBox.Location = new Point(149, 179);
            UserDBSpacer_comboBox.Name = "UserDBSpacer_comboBox";
            UserDBSpacer_comboBox.Size = new Size(120, 23);
            UserDBSpacer_comboBox.TabIndex = 117;
            UserDBSpacer_comboBox.SelectedIndexChanged += UserDBSpacer_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(112, 183);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 116;
            label8.Text = "유형";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(514, 134);
            label29.Name = "label29";
            label29.Size = new Size(48, 15);
            label29.TabIndex = 113;
            label29.Text = "W/m²·K";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(302, 134);
            label30.Name = "label30";
            label30.Size = new Size(83, 15);
            label30.TabIndex = 111;
            label30.Text = "유리 열관류율";
            // 
            // UserDB_Ug_textBox
            // 
            UserDB_Ug_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_Ug_textBox.BorderStyle = BorderStyle.None;
            UserDB_Ug_textBox.Enabled = false;
            UserDB_Ug_textBox.Location = new Point(387, 133);
            UserDB_Ug_textBox.Name = "UserDB_Ug_textBox";
            UserDB_Ug_textBox.Size = new Size(120, 16);
            UserDB_Ug_textBox.TabIndex = 112;
            UserDB_Ug_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(112, 134);
            label32.Name = "label32";
            label32.Size = new Size(31, 15);
            label32.TabIndex = 108;
            label32.Text = "유형";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(342, 19);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 106;
            label6.Text = "제조사";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.Location = new Point(391, 15);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 107;
            UserDB_Manufacture_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(602, 60);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 96;
            label4.Text = "재료";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(354, 60);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 92;
            label10.Text = "형태";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(100, 19);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "제품명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.Location = new Point(149, 15);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextAlign = HorizontalAlignment.Center;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(112, 60);
            label9.Name = "label9";
            label9.Size = new Size(31, 15);
            label9.TabIndex = 44;
            label9.Text = "유형";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(550, 19);
            label7.Name = "label7";
            label7.Size = new Size(83, 15);
            label7.TabIndex = 40;
            label7.Text = "창호 열관류율";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientActiveCaption;
            panel3.Controls.Add(UserNum_textBox);
            panel3.Controls.Add(Deletebutton);
            panel3.Controls.Add(AddUserDB_button);
            panel3.Controls.Add(label35);
            panel3.Controls.Add(UserDB_UfA_textBox);
            panel3.Controls.Add(label26);
            panel3.Controls.Add(label27);
            panel3.Controls.Add(label28);
            panel3.Controls.Add(UserDB_UfC_textBox);
            panel3.Controls.Add(label31);
            panel3.Controls.Add(label33);
            panel3.Controls.Add(UserDB_UfB_textBox);
            panel3.Controls.Add(label34);
            panel3.Location = new Point(0, 404);
            panel3.Name = "panel3";
            panel3.Size = new Size(1119, 37);
            panel3.TabIndex = 29;
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientActiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(81, 10);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(73, 16);
            UserNum_textBox.TabIndex = 168;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(55, 7);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 167;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(28, 7);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 166;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label35.Location = new Point(171, 1);
            label35.Name = "label35";
            label35.Size = new Size(60, 34);
            label35.TabIndex = 165;
            label35.Text = "프레임\r\n열관류율";
            label35.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UserDB_UfA_textBox
            // 
            UserDB_UfA_textBox.BackColor = SystemColors.GradientActiveCaption;
            UserDB_UfA_textBox.BorderStyle = BorderStyle.None;
            UserDB_UfA_textBox.Enabled = false;
            UserDB_UfA_textBox.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            UserDB_UfA_textBox.Location = new Point(303, 10);
            UserDB_UfA_textBox.Name = "UserDB_UfA_textBox";
            UserDB_UfA_textBox.Size = new Size(120, 16);
            UserDB_UfA_textBox.TabIndex = 164;
            UserDB_UfA_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(426, 11);
            label26.Name = "label26";
            label26.Size = new Size(48, 15);
            label26.TabIndex = 163;
            label26.Text = "W/m²·K";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(912, 11);
            label27.Name = "label27";
            label27.Size = new Size(48, 15);
            label27.TabIndex = 162;
            label27.Text = "W/m²·K";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(740, 11);
            label28.Name = "label28";
            label28.Size = new Size(47, 15);
            label28.TabIndex = 160;
            label28.Text = "중간(C)";
            // 
            // UserDB_UfC_textBox
            // 
            UserDB_UfC_textBox.BackColor = SystemColors.GradientActiveCaption;
            UserDB_UfC_textBox.BorderStyle = BorderStyle.None;
            UserDB_UfC_textBox.Enabled = false;
            UserDB_UfC_textBox.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            UserDB_UfC_textBox.Location = new Point(787, 10);
            UserDB_UfC_textBox.Name = "UserDB_UfC_textBox";
            UserDB_UfC_textBox.Size = new Size(120, 16);
            UserDB_UfC_textBox.TabIndex = 161;
            UserDB_UfC_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(668, 11);
            label31.Name = "label31";
            label31.Size = new Size(48, 15);
            label31.TabIndex = 159;
            label31.Text = "W/m²·K";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(493, 11);
            label33.Name = "label33";
            label33.Size = new Size(46, 15);
            label33.TabIndex = 157;
            label33.Text = "고정(B)";
            // 
            // UserDB_UfB_textBox
            // 
            UserDB_UfB_textBox.BackColor = SystemColors.GradientActiveCaption;
            UserDB_UfB_textBox.BorderStyle = BorderStyle.None;
            UserDB_UfB_textBox.Enabled = false;
            UserDB_UfB_textBox.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            UserDB_UfB_textBox.Location = new Point(545, 10);
            UserDB_UfB_textBox.Name = "UserDB_UfB_textBox";
            UserDB_UfB_textBox.Size = new Size(120, 16);
            UserDB_UfB_textBox.TabIndex = 158;
            UserDB_UfB_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(250, 11);
            label34.Name = "label34";
            label34.Size = new Size(47, 15);
            label34.TabIndex = 156;
            label34.Text = "개폐(A)";
            // 
            // splitter1
            // 
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 225);
            splitter1.TabIndex = 172;
            splitter1.TabStop = false;
            // 
            // Window_FrameDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1119, 709);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(Frame_dataGridView);
            Controls.Add(GeneralPanel);
            Name = "Window_FrameDB";
            Text = "Window_FrameDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Frame_dataGridView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)UserDBCertification_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)UserDB_Frame_pictureBox).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private DataGridView Frame_dataGridView;
        private Button Save_button;
        private Panel panel1;
        private TextBox UserDBFramedA_textBox;
        private Label label25;
        private Label label20;
        private Label label21;
        private TextBox UserDBFramedC_textBox;
        private Label label22;
        private Label label23;
        private TextBox UserDBFramedB_textBox;
        private Label label24;
        private Label label11;
        private Label label5;
        private Label label3;
        private ComboBox UserDBGlass_comboBox;
        private ComboBox UserDB_FrameMaterial_comboBox;
        private ComboBox UserDB_FrameShape_comboBox;
        private ComboBox UserDB_FrameType_comboBox;
        private Label label19;
        private TextBox UserDBUw_textBox;
        private Label label16;
        private TextBox UserDB_PsiFix_textBox;
        private Label label17;
        private Label label18;
        private TextBox UserDB_PsiOpen_textBox;
        private ComboBox UserDBSpacer_comboBox;
        private Label label8;
        private Label label29;
        private Label label30;
        private TextBox UserDB_Ug_textBox;
        private Label label32;
        private Label label6;
        private TextBox UserDB_Manufacture_textBox;
        private Label label4;
        private Label label10;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Label label9;
        private Label label7;
        private Label label15;
        private PictureBox UserDB_Frame_pictureBox;
        private Panel panel3;
        private Label label35;
        private TextBox UserDB_UfA_textBox;
        private Label label26;
        private Label label27;
        private Label label28;
        private TextBox UserDB_UfC_textBox;
        private Label label31;
        private Label label33;
        private TextBox UserDB_UfB_textBox;
        private Label label34;
        private Label label36;
        private Button Import_button;
        private PictureBox UserDBCertification_pictureBox;
        private Button Deletebutton;
        private Button AddUserDB_button;
        private TextBox UserNum_textBox;
        private Label label14;
        private TextBox UserDB_FrameShape_textBox;
        private Splitter splitter1;
    }
}