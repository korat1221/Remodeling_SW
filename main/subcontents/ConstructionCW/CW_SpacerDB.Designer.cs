namespace main.subcontents.ConstructionCW
{
    partial class CW_SpacerDB
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
            label5 = new Label();
            label10 = new Label();
            UserDB_Psi_open_textBox = new TextBox();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            AddUserDB_button = new Button();
            label11 = new Label();
            label9 = new Label();
            UserDB_Psi_fix_textBox = new TextBox();
            label6 = new Label();
            panel1 = new Panel();
            label3 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Deletebutton = new Button();
            Spacer_dataGridView = new DataGridView();
            UserDBType2_comboBox = new CustomComboBox();
            label8 = new Label();
            UserDBType1_comboBox = new CustomComboBox();
            info = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Spacer_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(828, 255);
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
            label5.Location = new Point(781, 48);
            label5.Name = "label5";
            label5.Size = new Size(44, 15);
            label5.TabIndex = 94;
            label5.Text = "W/m·K";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(512, 48);
            label10.Name = "label10";
            label10.Size = new Size(131, 15);
            label10.TabIndex = 92;
            label10.Text = "개폐유리 선형열관류율";
            // 
            // UserDB_Psi_open_textBox
            // 
            UserDB_Psi_open_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Psi_open_textBox.Location = new Point(652, 44);
            UserDB_Psi_open_textBox.Name = "UserDB_Psi_open_textBox";
            UserDB_Psi_open_textBox.Size = new Size(120, 23);
            UserDB_Psi_open_textBox.TabIndex = 93;
            UserDB_Psi_open_textBox.TextChanged += UserDB_Psi_open_textBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(243, 16);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "제품명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDBName_textBox.Location = new Point(295, 12);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold);
            AddUserDB_button.Location = new Point(71, 11);
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
            label11.Location = new Point(424, 48);
            label11.Name = "label11";
            label11.Size = new Size(44, 15);
            label11.TabIndex = 48;
            label11.Text = "W/m·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(155, 48);
            label9.Name = "label9";
            label9.Size = new Size(131, 15);
            label9.TabIndex = 44;
            label9.Text = "고정유리 선형열관류율";
            // 
            // UserDB_Psi_fix_textBox
            // 
            UserDB_Psi_fix_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Psi_fix_textBox.Location = new Point(295, 44);
            UserDB_Psi_fix_textBox.Name = "UserDB_Psi_fix_textBox";
            UserDB_Psi_fix_textBox.Size = new Size(120, 23);
            UserDB_Psi_fix_textBox.TabIndex = 45;
            UserDB_Psi_fix_textBox.TextChanged += UserDB_Psi_fix_textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(605, 16);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 7;
            label6.Text = "구분1";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(info);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(Spacer_dataGridView);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(UserDB_Psi_open_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(UserDB_Psi_fix_textBox);
            panel1.Controls.Add(UserDBType2_comboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(UserDBType1_comboBox);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(985, 249);
            panel1.TabIndex = 27;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(424, 16);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 108;
            label3.Text = "제조사";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Manufacture_textBox.Location = new Point(476, 12);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 109;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
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
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(8, 15);
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
            Deletebutton.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold);
            Deletebutton.Location = new Point(98, 11);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Delete_button_Click;
            // 
            // Spacer_dataGridView
            // 
            Spacer_dataGridView.AllowUserToAddRows = false;
            Spacer_dataGridView.AllowUserToDeleteRows = false;
            Spacer_dataGridView.AllowUserToResizeColumns = false;
            Spacer_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Spacer_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Spacer_dataGridView.BackgroundColor = Color.White;
            Spacer_dataGridView.BorderStyle = BorderStyle.None;
            Spacer_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Spacer_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Spacer_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Spacer_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Spacer_dataGridView.Location = new Point(2, 79);
            Spacer_dataGridView.Name = "Spacer_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Spacer_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Spacer_dataGridView.RowHeadersVisible = false;
            Spacer_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Spacer_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Spacer_dataGridView.Size = new Size(982, 171);
            Spacer_dataGridView.TabIndex = 19;
            Spacer_dataGridView.CellContentClick += Spacer_dataGridView_CellContentClick;
            // 
            // UserDBType2_comboBox
            // 
            UserDBType2_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            UserDBType2_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            UserDBType2_comboBox.FormattingEnabled = true;
            UserDBType2_comboBox.Location = new Point(828, 12);
            UserDBType2_comboBox.Name = "UserDBType2_comboBox";
            UserDBType2_comboBox.Size = new Size(120, 23);
            UserDBType2_comboBox.TabIndex = 43;
            UserDBType2_comboBox.SelectedIndexChanged += UserDBType2_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(781, 16);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 42;
            label8.Text = "구분2";
            // 
            // UserDBType1_comboBox
            // 
            UserDBType1_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            UserDBType1_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            UserDBType1_comboBox.FormattingEnabled = true;
            UserDBType1_comboBox.Location = new Point(652, 12);
            UserDBType1_comboBox.Name = "UserDBType1_comboBox";
            UserDBType1_comboBox.Size = new Size(120, 23);
            UserDBType1_comboBox.TabIndex = 39;
            UserDBType1_comboBox.SelectedIndexChanged += UserDBType1_comboBox_SelectedIndexChanged;
            // 
            // info
            // 
            info.BackColor = SystemColors.ControlLight;
            info.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            info.FlatStyle = FlatStyle.System;
            info.Font = new Font("나눔바른고딕", 9.75F);
            info.Location = new Point(953, 12);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(23, 23);
            info.TabIndex = 159;
            info.Text = "?";
            info.UseVisualStyleBackColor = false;
            info.Click += info_Click;
            // 
            // CW_SpacerDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(985, 295);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "CW_SpacerDB";
            Text = "CW_SpacerDB";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Spacer_dataGridView).EndInit();
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
        private TextBox UserDB_Psi_open_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Button AddUserDB_button;
        private Label label11;
        private Label label9;
        private TextBox UserDB_Psi_fix_textBox;
        private Label label6;
        private Panel panel1;
        private CustomComboBox UserDBType2_comboBox;
        private Label label8;
        private CustomComboBox UserDBType1_comboBox;
        private DataGridView Spacer_dataGridView;
        private Button Deletebutton;
        private Label label4;
        private Label label15;
        private TextBox UserNum_textBox;
        private Label label3;
        private TextBox UserDB_Manufacture_textBox;
        private Button info;
    }
}