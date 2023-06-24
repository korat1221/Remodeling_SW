namespace main.subcontents.ConstructionCW
{
    partial class CW_DoorFrameDB
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
            GeneralPanel = new Panel();
            label1 = new Label();
            Save_button = new Button();
            label5 = new Label();
            label10 = new Label();
            UserDB_dfd_textBox = new TextBox();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            AddUserDB_button = new Button();
            label11 = new Label();
            label9 = new Label();
            UserDB_Ufd_textBox = new TextBox();
            panel1 = new Panel();
            UserDB_Type_comboBox = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Deletebutton = new Button();
            Door_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Door_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(862, 71);
            GeneralPanel.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(410, 45);
            label1.TabIndex = 0;
            label1.Text = "- 기본DB는 ISO 10077기준, ISO 12631 기준 시뮬레이션 결과값 입니다.\r\n\r\n- DB를 추가하고자 하는 경우 각 항목의 값을 입력하고, + 버튼을 누르세요.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(713, 332);
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
            label5.Location = new Point(830, 47);
            label5.Name = "label5";
            label5.Size = new Size(18, 15);
            label5.TabIndex = 94;
            label5.Text = "m";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(628, 47);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 92;
            label10.Text = "프레임 두께";
            // 
            // UserDB_dfd_textBox
            // 
            UserDB_dfd_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_dfd_textBox.Location = new Point(701, 43);
            UserDB_dfd_textBox.Name = "UserDB_dfd_textBox";
            UserDB_dfd_textBox.Size = new Size(120, 23);
            UserDB_dfd_textBox.TabIndex = 93;
            UserDB_dfd_textBox.TextChanged += UserDB_dfd_textBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(240, 16);
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
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            label11.Location = new Point(434, 48);
            label11.Name = "label11";
            label11.Size = new Size(48, 15);
            label11.TabIndex = 48;
            label11.Text = "W/m²·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(214, 48);
            label9.Name = "label9";
            label9.Size = new Size(95, 15);
            label9.TabIndex = 44;
            label9.Text = "프레임 열관류율";
            // 
            // UserDB_Ufd_textBox
            // 
            UserDB_Ufd_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Ufd_textBox.Location = new Point(310, 44);
            UserDB_Ufd_textBox.Name = "UserDB_Ufd_textBox";
            UserDB_Ufd_textBox.Size = new Size(120, 23);
            UserDB_Ufd_textBox.TabIndex = 45;
            UserDB_Ufd_textBox.TextChanged += UserDB_Ufd_textBox_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(UserDB_Type_comboBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(UserDB_dfd_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(UserDB_Ufd_textBox);
            panel1.Location = new Point(0, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(862, 82);
            panel1.TabIndex = 27;
            // 
            // UserDB_Type_comboBox
            // 
            UserDB_Type_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            UserDB_Type_comboBox.FormattingEnabled = true;
            UserDB_Type_comboBox.Location = new Point(701, 10);
            UserDB_Type_comboBox.Name = "UserDB_Type_comboBox";
            UserDB_Type_comboBox.Size = new Size(120, 24);
            UserDB_Type_comboBox.TabIndex = 53;
            UserDB_Type_comboBox.SelectedIndexChanged += UserDB_Type_comboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(642, 15);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 110;
            label2.Text = "구분";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(453, 15);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 108;
            label3.Text = "제조사";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Manufacture_textBox.Location = new Point(505, 11);
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
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            Deletebutton.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(98, 11);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Delete_button_Click;
            // 
            // Door_dataGridView
            // 
            Door_dataGridView.AllowUserToAddRows = false;
            Door_dataGridView.AllowUserToDeleteRows = false;
            Door_dataGridView.AllowUserToResizeColumns = false;
            Door_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Door_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Door_dataGridView.BackgroundColor = SystemColors.Control;
            Door_dataGridView.BorderStyle = BorderStyle.None;
            Door_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Door_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Door_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Door_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Door_dataGridView.Location = new Point(0, 153);
            Door_dataGridView.Name = "Door_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Door_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Door_dataGridView.RowHeadersVisible = false;
            Door_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Door_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Door_dataGridView.RowTemplate.Height = 25;
            Door_dataGridView.Size = new Size(862, 171);
            Door_dataGridView.TabIndex = 19;
            Door_dataGridView.CellContentClick += Spacer_dataGridView_CellContentClick;
            // 
            // CW_DoorFrameDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(860, 365);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Controls.Add(Door_dataGridView);
            Name = "CW_DoorFrameDB";
            Text = "CW_DoorDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Door_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private Button Save_button;
        private Label label5;
        private Label label10;
        private TextBox UserDB_dfd_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Button AddUserDB_button;
        private Label label11;
        private Label label9;
        private TextBox UserDB_Ufd_textBox;
        private Panel panel1;
        private Button Deletebutton;
        private Label label4;
        private Label label15;
        private TextBox UserNum_textBox;
        private Label label3;
        private TextBox UserDB_Manufacture_textBox;
        private DataGridView Door_dataGridView;
        private Label label2;
        private ComboBox UserDB_Type_comboBox;
    }
}