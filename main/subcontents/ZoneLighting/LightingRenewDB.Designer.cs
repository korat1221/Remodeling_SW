
namespace main.subcontents.ZoneLighting
{
    partial class LightingRenewDB
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
            Save_button = new Button();
            panel1 = new Panel();
            label5 = new Label();
            UserDB_A_textBox = new TextBox();
            label6 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            UserDBName_textBox = new TextBox();
            label2 = new Label();
            label29 = new Label();
            label13 = new Label();
            label30 = new Label();
            UserDB_Length2_textBox = new TextBox();
            label32 = new Label();
            UserDB_eff_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label12 = new Label();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            label3 = new Label();
            label4 = new Label();
            UserDB_Length1_textBox = new TextBox();
            RenewType_comboBox = new ComboBox();
            label8 = new Label();
            Renew_dataGridView = new DataGridView();
            infoRenewdb = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Renew_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(755, 527);
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
            panel1.Controls.Add(infoRenewdb);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(UserDB_A_textBox);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(UserDB_Length2_textBox);
            panel1.Controls.Add(label32);
            panel1.Controls.Add(UserDB_eff_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(UserDB_Length1_textBox);
            panel1.Controls.Add(RenewType_comboBox);
            panel1.Controls.Add(label8);
            panel1.Location = new Point(0, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(902, 133);
            panel1.TabIndex = 26;
            // 
            // 
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(599, 103);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 120;
            label5.Text = "산광부 면적";
            // 
            // UserDB_A_textBox
            // 
            UserDB_A_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserDB_A_textBox.BorderStyle = BorderStyle.None;
            UserDB_A_textBox.Location = new Point(712, 104);
            UserDB_A_textBox.Name = "UserDB_A_textBox";
            UserDB_A_textBox.Size = new Size(120, 16);
            UserDB_A_textBox.TabIndex = 121;
            UserDB_A_textBox.TextChanged += UserDB_A_textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(543, 73);
            label6.Name = "label6";
            label6.Size = new Size(20, 15);
            label6.TabIndex = 119;
            label6.Text = "[-]";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.Location = new Point(712, 10);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 115;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.Location = new Point(417, 10);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(599, 13);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 114;
            label2.Text = "제조사";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(850, 78);
            label29.Name = "label29";
            label29.Size = new Size(18, 15);
            label29.TabIndex = 113;
            label29.Text = "m";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(327, 13);
            label13.Name = "label13";
            label13.Size = new Size(83, 15);
            label13.TabIndex = 90;
            label13.Text = "집광채광 명칭";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(599, 73);
            label30.Name = "label30";
            label30.Size = new Size(99, 15);
            label30.TabIndex = 111;
            label30.Text = "산광부 세로 길이";
            // 
            // UserDB_Length2_textBox
            // 
            UserDB_Length2_textBox.Location = new Point(712, 70);
            UserDB_Length2_textBox.Name = "UserDB_Length2_textBox";
            UserDB_Length2_textBox.Size = new Size(120, 23);
            UserDB_Length2_textBox.TabIndex = 112;
            UserDB_Length2_textBox.TextChanged += UserDB_Length2_textBox_TextChanged;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(328, 73);
            label32.Name = "label32";
            label32.Size = new Size(83, 15);
            label32.TabIndex = 108;
            label32.Text = "집광채광 효율";
            // 
            // UserDB_eff_textBox
            // 
            UserDB_eff_textBox.Location = new Point(417, 70);
            UserDB_eff_textBox.Name = "UserDB_eff_textBox";
            UserDB_eff_textBox.Size = new Size(120, 23);
            UserDB_eff_textBox.TabIndex = 109;
            UserDB_eff_textBox.TextChanged += UserDB_eff_textBox_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(144, 17);
            label15.Name = "label15";
            label15.Size = new Size(31, 15);
            label15.TabIndex = 104;
            label15.Text = "번호";
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(190, 18);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(120, 16);
            UserNum_textBox.TabIndex = 105;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            label12.Location = new Point(11, 17);
            label12.Name = "label12";
            label12.Size = new Size(61, 16);
            label12.TabIndex = 103;
            label12.Text = "사용자DB";
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            Deletebutton.Location = new Point(106, 13);
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
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            AddUserDB_button.Location = new Point(79, 13);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 101;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(850, 51);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(18, 15);
            label3.TabIndex = 98;
            label3.Text = "m";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(599, 42);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 96;
            label4.Text = "산광부 가로 길이";
            // 
            // UserDB_Length1_textBox
            // 
            UserDB_Length1_textBox.Location = new Point(712, 39);
            UserDB_Length1_textBox.Name = "UserDB_Length1_textBox";
            UserDB_Length1_textBox.Size = new Size(120, 23);
            UserDB_Length1_textBox.TabIndex = 97;
            UserDB_Length1_textBox.TextChanged += UserDB_Length1_textBox_TextChanged;
            // 
            // RenewType_comboBox
            // 
            RenewType_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F);
            RenewType_comboBox.FormattingEnabled = true;
            RenewType_comboBox.Location = new Point(417, 39);
            RenewType_comboBox.Name = "RenewType_comboBox";
            RenewType_comboBox.Size = new Size(120, 24);
            RenewType_comboBox.TabIndex = 43;
            RenewType_comboBox.SelectedIndexChanged += RenewType_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(327, 42);
            label8.Name = "label8";
            label8.Size = new Size(83, 15);
            label8.TabIndex = 42;
            label8.Text = "집광채광 종류";
            // 
            // Renew_dataGridView
            // 
            Renew_dataGridView.AllowUserToAddRows = false;
            Renew_dataGridView.AllowUserToDeleteRows = false;
            Renew_dataGridView.AllowUserToResizeColumns = false;
            Renew_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Renew_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Renew_dataGridView.BackgroundColor = SystemColors.Control;
            Renew_dataGridView.BorderStyle = BorderStyle.None;
            Renew_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Renew_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Renew_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Renew_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Renew_dataGridView.Location = new Point(0, 142);
            Renew_dataGridView.Name = "Renew_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Renew_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Renew_dataGridView.RowHeadersVisible = false;
            Renew_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Renew_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Renew_dataGridView.Size = new Size(902, 344);
            Renew_dataGridView.TabIndex = 111;
            Renew_dataGridView.CellContentClick += Renew_dataGridView_CellContentClick;
            // 
            // infoRenewdb
            // 
            infoRenewdb.BackColor = SystemColors.ControlLight;
            infoRenewdb.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            infoRenewdb.FlatStyle = FlatStyle.System;
            infoRenewdb.Font = new Font("Microsoft Sans Serif", 9.75F);
            infoRenewdb.Location = new Point(870, 5);
            infoRenewdb.Margin = new Padding(0);
            infoRenewdb.Name = "infoRenewdb";
            infoRenewdb.Size = new Size(23, 23);
            infoRenewdb.TabIndex = 164;
            infoRenewdb.Text = "?";
            infoRenewdb.UseVisualStyleBackColor = false;
            infoRenewdb.Click += infoRenewdb_Click;
            // 
            // LightingRenewDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 564);
            Controls.Add(Renew_dataGridView);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Name = "LightingRenewDB";
            Text = "LightingRenewDB";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Renew_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button Save_button;
        private Panel panel1;
        private Label label15;
        private TextBox UserNum_textBox;
        private Label label12;
        private Button Deletebutton;
        private Button AddUserDB_button;
        private Label label3;
        private Label label4;
        private TextBox UserDB_Length1_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private ComboBox RenewType_comboBox;
        private Label label8;
        private DataGridView Renew_dataGridView;
        private Label label29;
        private Label label30;
        private TextBox UserDB_Length2_textBox;
        private Label label32;
        private TextBox UserDB_eff_textBox;
        private Label label2;
        private TextBox UserDB_Manufacture_textBox;
        private Label label6;
        private Label label5;
        private TextBox UserDB_A_textBox;
        private Button infoRenewdb;
    }
}