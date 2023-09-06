namespace main.subcontents

{
    partial class MaterialDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Save_button = new Button();
            label5 = new Label();
            label10 = new Label();
            UserDB_Density_textBox = new TextBox();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            AddUserDB_button = new Button();
            label11 = new Label();
            label9 = new Label();
            UserDB_Conductivity_textBox = new TextBox();
            label6 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            MaterialType_comboBox = new CustomComboBox();
            label24 = new Label();
            UserDB_Type1_comboBox = new CustomComboBox();
            label12 = new Label();
            UserDB_Note_textBox = new TextBox();
            label18 = new Label();
            label16 = new Label();
            label17 = new Label();
            UserDB_c_textBox = new TextBox();
            label14 = new Label();
            UserDB_dry_textBox = new TextBox();
            label8 = new Label();
            UserDB_wet_textBox = new TextBox();
            UserDB_Type2_textBox = new TextBox();
            label3 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Deletebutton = new Button();
            dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
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
            GeneralPanel.Size = new Size(985, 98);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(690, 11);
            label2.Name = "label2";
            label2.Size = new Size(219, 75);
            label2.TabIndex = 2;
            label2.Text = "λ2 : 적용 열전도율 (W/mK)\r\nλ1 : 초기열전도율 (W/mK)\r\nFT: 온도 조건 변동 성능저하 보정계수\r\nFm: 습기 조건 변동 성능 저하 보정계수\r\nFa:  시간 경과 성능 저하 보정계수\r\n";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(470, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(201, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(410, 45);
            label1.TabIndex = 0;
            label1.Text = "- DB를 추가하고자 하는 경우 각 항목의 값을 입력하고, + 버튼을 누르세요.\r\n\r\n- 재료명, 종류1, 열전도율은 필수 입력값 입니다.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(838, 630);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(681, 44);
            label5.Name = "label5";
            label5.Size = new Size(40, 15);
            label5.TabIndex = 94;
            label5.Text = "kg/m³";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(521, 44);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 92;
            label10.Text = "밀도";
            // 
            // UserDB_Density_textBox
            // 
            UserDB_Density_textBox.Location = new Point(558, 40);
            UserDB_Density_textBox.Name = "UserDB_Density_textBox";
            UserDB_Density_textBox.Size = new Size(120, 23);
            UserDB_Density_textBox.TabIndex = 93;
            UserDB_Density_textBox.TextChanged += UserDB_Density_textBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(282, 15);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "재료명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDBName_textBox.BorderStyle = BorderStyle.None;
            UserDBName_textBox.Location = new Point(327, 14);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 16);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(919, 69);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 89;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(450, 44);
            label11.Name = "label11";
            label11.Size = new Size(44, 15);
            label11.TabIndex = 48;
            label11.Text = "W/m·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(270, 44);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 44;
            label9.Text = "열전도율";
            // 
            // UserDB_Conductivity_textBox
            // 
            UserDB_Conductivity_textBox.Location = new Point(327, 40);
            UserDB_Conductivity_textBox.Name = "UserDB_Conductivity_textBox";
            UserDB_Conductivity_textBox.Size = new Size(120, 23);
            UserDB_Conductivity_textBox.TabIndex = 45;
            UserDB_Conductivity_textBox.TextChanged += UserDB_Conductivity_textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(746, 15);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 7;
            label6.Text = "종류2";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(UserDB_Type1_comboBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(UserDB_Note_textBox);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(UserDB_c_textBox);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(UserDB_dry_textBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(UserDB_wet_textBox);
            panel1.Controls.Add(UserDB_Type2_textBox);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(UserDB_Density_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(UserDB_Conductivity_textBox);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(0, 96);
            panel1.Name = "panel1";
            panel1.Size = new Size(985, 98);
            panel1.TabIndex = 27;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.GradientActiveCaption;
            panel2.Controls.Add(MaterialType_comboBox);
            panel2.Controls.Add(label24);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(140, 98);
            panel2.TabIndex = 123;
            // 
            // MaterialType_comboBox
            // 
            MaterialType_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MaterialType_comboBox.FormattingEnabled = true;
            MaterialType_comboBox.Location = new Point(10, 39);
            MaterialType_comboBox.Name = "MaterialType_comboBox";
            MaterialType_comboBox.Size = new Size(120, 24);
            MaterialType_comboBox.TabIndex = 56;
            MaterialType_comboBox.SelectedIndexChanged += MaterialType_comboBox_SelectedIndexChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label24.Location = new Point(10, 14);
            label24.Name = "label24";
            label24.Size = new Size(60, 17);
            label24.TabIndex = 108;
            label24.Text = "재료유형";
            // 
            // UserDB_Type1_comboBox
            // 
            UserDB_Type1_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDB_Type1_comboBox.FormattingEnabled = true;
            UserDB_Type1_comboBox.Location = new Point(558, 10);
            UserDB_Type1_comboBox.Name = "UserDB_Type1_comboBox";
            UserDB_Type1_comboBox.Size = new Size(120, 24);
            UserDB_Type1_comboBox.TabIndex = 56;
            UserDB_Type1_comboBox.SelectedIndexChanged += UserDB_Type1_comboBox_SelectedIndexChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(750, 73);
            label12.Name = "label12";
            label12.Size = new Size(31, 15);
            label12.TabIndex = 121;
            label12.Text = "비고";
            // 
            // UserDB_Note_textBox
            // 
            UserDB_Note_textBox.Location = new Point(790, 69);
            UserDB_Note_textBox.Name = "UserDB_Note_textBox";
            UserDB_Note_textBox.Size = new Size(120, 23);
            UserDB_Note_textBox.TabIndex = 122;
            UserDB_Note_textBox.TextChanged += UserDB_Note_textBox_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(212, 73);
            label18.Name = "label18";
            label18.Size = new Size(79, 15);
            label18.TabIndex = 120;
            label18.Text = "투습저항계수";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(913, 44);
            label16.Name = "label16";
            label16.Size = new Size(45, 15);
            label16.TabIndex = 119;
            label16.Text = "kJ/kg·K";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(750, 44);
            label17.Name = "label17";
            label17.Size = new Size(31, 15);
            label17.TabIndex = 117;
            label17.Text = "비열";
            // 
            // UserDB_c_textBox
            // 
            UserDB_c_textBox.Location = new Point(790, 40);
            UserDB_c_textBox.Name = "UserDB_c_textBox";
            UserDB_c_textBox.Size = new Size(120, 23);
            UserDB_c_textBox.TabIndex = 118;
            UserDB_c_textBox.TextChanged += UserDB_c_textBox_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(293, 73);
            label14.Name = "label14";
            label14.Size = new Size(32, 15);
            label14.TabIndex = 114;
            label14.Text = "(dry)";
            // 
            // UserDB_dry_textBox
            // 
            UserDB_dry_textBox.Location = new Point(327, 69);
            UserDB_dry_textBox.Name = "UserDB_dry_textBox";
            UserDB_dry_textBox.Size = new Size(120, 23);
            UserDB_dry_textBox.TabIndex = 115;
            UserDB_dry_textBox.TextChanged += UserDB_dry_textBox_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(521, 73);
            label8.Name = "label8";
            label8.Size = new Size(34, 15);
            label8.TabIndex = 111;
            label8.Text = "(wet)";
            // 
            // UserDB_wet_textBox
            // 
            UserDB_wet_textBox.Location = new Point(558, 69);
            UserDB_wet_textBox.Name = "UserDB_wet_textBox";
            UserDB_wet_textBox.Size = new Size(120, 23);
            UserDB_wet_textBox.TabIndex = 112;
            UserDB_wet_textBox.TextChanged += UserDB_wet_textBox_TextChanged;
            // 
            // UserDB_Type2_textBox
            // 
            UserDB_Type2_textBox.Location = new Point(789, 11);
            UserDB_Type2_textBox.Name = "UserDB_Type2_textBox";
            UserDB_Type2_textBox.Size = new Size(120, 23);
            UserDB_Type2_textBox.TabIndex = 110;
            UserDB_Type2_textBox.TextChanged += UserDB_Type2_textBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(517, 15);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 108;
            label3.Text = "종류1";
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(208, 14);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(68, 16);
            UserNum_textBox.TabIndex = 107;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(142, 15);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 96;
            label4.Text = "사용자DB";
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(946, 69);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Delete_button_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.BackgroundColor = SystemColors.Control;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(0, 194);
            dataGridView.Name = "dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.Size = new Size(985, 430);
            dataGridView.TabIndex = 19;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // MaterialDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(985, 664);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Controls.Add(dataGridView);
            Name = "MaterialDB";
            Text = "MaterialDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button Save_button;
        private Label label5;
        private Label label10;
        private TextBox UserDB_Density_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Button AddUserDB_button;
        private Label label11;
        private Label label9;
        private TextBox UserDB_Conductivity_textBox;
        private Label label6;
        private Panel panel1;
        private DataGridView dataGridView;
        private Button Deletebutton;
        private Label label4;
        private TextBox UserNum_textBox;
        private TextBox UserDB_Type2_textBox;
        private Label label3;
        private Label label18;
        private Label label16;
        private Label label17;
        private TextBox UserDB_c_textBox;
        private Label label14;
        private TextBox UserDB_dry_textBox;
        private Label label8;
        private TextBox UserDB_wet_textBox;
        private Label label7;
        private Label label12;
        private TextBox UserDB_Note_textBox;
        private CustomComboBox UserDB_Type1_comboBox;
        private Panel panel2;
        private CustomComboBox MaterialType_comboBox;
        private Label label24;
    }
}