
namespace main.subcontents
{
    partial class Window_InstallDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_InstallDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            Save_button = new Button();
            entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            panel1 = new Panel();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            Install_dataGridView = new DataGridView();
            label12 = new Label();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            UserDBType4_comboBox = new CustomComboBox();
            label14 = new Label();
            label3 = new Label();
            label4 = new Label();
            UserDB_Psi_InstallButtom_textBox = new TextBox();
            label5 = new Label();
            label10 = new Label();
            UserDB_Psi_InstallSide_textBox = new TextBox();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            label11 = new Label();
            label9 = new Label();
            UserDB_Psi_InstallTop_textBox = new TextBox();
            UserDBType3_comboBox = new CustomComboBox();
            label8 = new Label();
            UserDBType2_comboBox = new CustomComboBox();
            label7 = new Label();
            UserDBType1_comboBox = new CustomComboBox();
            label6 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Install_dataGridView).BeginInit();
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
            GeneralPanel.Size = new Size(944, 132);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(676, 19);
            label2.Name = "label2";
            label2.Size = new Size(171, 90);
            label2.TabIndex = 2;
            label2.Text = "표준값 적용시\r\n왼쪽그림처럼 콘크리트에 의해\r\n단열이 끊어지는 경우 구분1에\r\n따라 다음 항목으로 적용한다.\r\n1. 내단열 --> 외부측 설치\r\n2. 외단열--> 내부측 설치\r\n";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(509, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 117);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 19);
            label1.Name = "label1";
            label1.Size = new Size(390, 90);
            label1.TabIndex = 0;
            label1.Text = "- ISO 10211 기준, KIAEBS S-8:2017 기준 시뮬레이션 값을 적용합니다.\r\n\r\n- 오른쪽 그림을 참조하여 해당하는 값을 입력하시오.\r\n\r\n- 설치 부위는 상부, 측면, 하부로 구분되며 차양장치에의한 추가 열교는\r\n   외피 정보에서 추가로 반영됩니다.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(797, 427);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // entityCommand1
            // 
            entityCommand1.CommandTimeout = 0;
            entityCommand1.CommandTree = null;
            entityCommand1.Connection = null;
            entityCommand1.EnablePlanCaching = true;
            entityCommand1.Transaction = null;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(Install_dataGridView);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(UserDBType4_comboBox);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(UserDB_Psi_InstallButtom_textBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(UserDB_Psi_InstallSide_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(UserDB_Psi_InstallTop_textBox);
            panel1.Controls.Add(UserDBType3_comboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(UserDBType2_comboBox);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(UserDBType1_comboBox);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(0, 127);
            panel1.Name = "panel1";
            panel1.Size = new Size(944, 278);
            panel1.TabIndex = 24;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(146, 10);
            label15.Name = "label15";
            label15.Size = new Size(31, 15);
            label15.TabIndex = 104;
            label15.Text = "번호";
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(202, 9);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(120, 16);
            UserNum_textBox.TabIndex = 105;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Install_dataGridView
            // 
            Install_dataGridView.AllowUserToAddRows = false;
            Install_dataGridView.AllowUserToDeleteRows = false;
            Install_dataGridView.AllowUserToResizeColumns = false;
            Install_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Install_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Install_dataGridView.BackgroundColor = SystemColors.Control;
            Install_dataGridView.BorderStyle = BorderStyle.None;
            Install_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Install_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Install_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Install_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Install_dataGridView.Location = new Point(-2, 90);
            Install_dataGridView.Name = "Install_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Install_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Install_dataGridView.RowHeadersVisible = false;
            Install_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Install_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Install_dataGridView.RowTemplate.Height = 25;
            Install_dataGridView.Size = new Size(944, 193);
            Install_dataGridView.TabIndex = 97;
            Install_dataGridView.CellContentClick += Install_dataGridView_CellContentClick;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(12, 10);
            label12.Name = "label12";
            label12.Size = new Size(60, 15);
            label12.TabIndex = 103;
            label12.Text = "사용자DB";
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new System.Drawing.Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(102, 6);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 102;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new System.Drawing.Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(75, 6);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 101;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // UserDBType4_comboBox
            // 
            UserDBType4_comboBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBType4_comboBox.FormattingEnabled = true;
            UserDBType4_comboBox.Location = new Point(760, 35);
            UserDBType4_comboBox.Name = "UserDBType4_comboBox";
            UserDBType4_comboBox.Size = new Size(120, 23);
            UserDBType4_comboBox.TabIndex = 100;
            UserDBType4_comboBox.SelectedIndexChanged += UserDBType4_comboBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(701, 39);
            label14.Name = "label14";
            label14.Size = new Size(38, 15);
            label14.TabIndex = 99;
            label14.Text = "구분4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(883, 68);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 98;
            label3.Text = "W/m·K";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(693, 68);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 96;
            label4.Text = "하부설치";
            // 
            // UserDB_Psi_InstallButtom_textBox
            // 
            UserDB_Psi_InstallButtom_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Psi_InstallButtom_textBox.Location = new Point(760, 64);
            UserDB_Psi_InstallButtom_textBox.Name = "UserDB_Psi_InstallButtom_textBox";
            UserDB_Psi_InstallButtom_textBox.Size = new Size(120, 23);
            UserDB_Psi_InstallButtom_textBox.TabIndex = 97;
            UserDB_Psi_InstallButtom_textBox.TextChanged += UserDB_Psi_InstallButtom_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(579, 68);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 94;
            label5.Text = "W/m·K";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(389, 68);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 92;
            label10.Text = "측면설치";
            // 
            // UserDB_Psi_InstallSide_textBox
            // 
            UserDB_Psi_InstallSide_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Psi_InstallSide_textBox.Location = new Point(456, 64);
            UserDB_Psi_InstallSide_textBox.Name = "UserDB_Psi_InstallSide_textBox";
            UserDB_Psi_InstallSide_textBox.Size = new Size(120, 23);
            UserDB_Psi_InstallSide_textBox.TabIndex = 93;
            UserDB_Psi_InstallSide_textBox.TextChanged += UserDB_Psi_InstallSide_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(400, 10);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "제품명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDBName_textBox.Location = new Point(456, 6);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(325, 68);
            label11.Name = "label11";
            label11.Size = new Size(44, 15);
            label11.TabIndex = 48;
            label11.Text = "W/m·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 68);
            label9.Name = "label9";
            label9.Size = new Size(175, 15);
            label9.TabIndex = 44;
            label9.Text = "선형열관류율           상부설치 ";
            // 
            // UserDB_Psi_InstallTop_textBox
            // 
            UserDB_Psi_InstallTop_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Psi_InstallTop_textBox.Location = new Point(202, 64);
            UserDB_Psi_InstallTop_textBox.Name = "UserDB_Psi_InstallTop_textBox";
            UserDB_Psi_InstallTop_textBox.Size = new Size(120, 23);
            UserDB_Psi_InstallTop_textBox.TabIndex = 45;
            UserDB_Psi_InstallTop_textBox.TextChanged += UserDB_Psi_InstallTop_TextChanged;
            // 
            // UserDBType3_comboBox
            // 
            UserDBType3_comboBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBType3_comboBox.FormattingEnabled = true;
            UserDBType3_comboBox.Location = new Point(456, 35);
            UserDBType3_comboBox.Name = "UserDBType3_comboBox";
            UserDBType3_comboBox.Size = new Size(120, 23);
            UserDBType3_comboBox.TabIndex = 43;
            UserDBType3_comboBox.SelectedIndexChanged += UserDBType3_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(397, 39);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 42;
            label8.Text = "구분3";
            // 
            // UserDBType2_comboBox
            // 
            UserDBType2_comboBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBType2_comboBox.FormattingEnabled = true;
            UserDBType2_comboBox.Location = new Point(202, 35);
            UserDBType2_comboBox.Name = "UserDBType2_comboBox";
            UserDBType2_comboBox.Size = new Size(120, 23);
            UserDBType2_comboBox.TabIndex = 41;
            UserDBType2_comboBox.SelectedIndexChanged += UserDBType2_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(143, 39);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 40;
            label7.Text = "구분2";
            // 
            // UserDBType1_comboBox
            // 
            UserDBType1_comboBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDBType1_comboBox.FormattingEnabled = true;
            UserDBType1_comboBox.Location = new Point(760, 6);
            UserDBType1_comboBox.Name = "UserDBType1_comboBox";
            UserDBType1_comboBox.Size = new Size(120, 23);
            UserDBType1_comboBox.TabIndex = 39;
            UserDBType1_comboBox.SelectedIndexChanged += UserDBType1_comboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(701, 10);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 7;
            label6.Text = "구분1";
            // 
            // Window_InstallDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 464);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Controls.Add(panel1);
            Name = "Window_InstallDB";
            Text = "Window_InstallDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Install_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button Save_button;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private Panel panel1;
        private Label label6;
        private TextBox Name_textBox;
        private Label label12;
        private Label label11;
        private Label label9;
        private TextBox UserDB_Psi_InstallTop_textBox;
        private CustomComboBox UserDBType3_comboBox;
        private Label label8;
        private CustomComboBox UserDBType2_comboBox;
        private Label label7;
        private CustomComboBox UserDBType1_comboBox;
        private Label label5;
        private Label label10;
        private TextBox UserDB_Psi_InstallSide_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private CustomComboBox UserDBType4_comboBox;
        private Label label14;
        private Label label3;
        private Label label4;
        private TextBox UserDB_Psi_InstallButtom_textBox;
        private Button Deletebutton;
        private Button AddUserDB_button;
        private DataGridView Install_dataGridView;
        private Label label15;
        private TextBox UserNum_textBox;
    }
}