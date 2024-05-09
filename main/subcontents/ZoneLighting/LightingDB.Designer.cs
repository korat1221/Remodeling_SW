namespace main.subcontents.ZoneLighting
{
    partial class LightingDB
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
            label6 = new Label();
            UserDB_W_textBox = new TextBox();
            UserDB_Manufacture_textBox = new TextBox();
            UserDBName_textBox = new TextBox();
            label2 = new Label();
            label5 = new Label();
            Converter_comboBox = new ComboBox();
            label29 = new Label();
            label13 = new Label();
            label30 = new Label();
            UserDB_FL_textBox = new TextBox();
            label31 = new Label();
            label32 = new Label();
            UserDB_lm_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label12 = new Label();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            label3 = new Label();
            label4 = new Label();
            UserDB_eff_textBox = new TextBox();
            label10 = new Label();
            LampType_comboBox = new ComboBox();
            label8 = new Label();
            Light_dataGridView = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Light_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(830, 525);
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
            panel1.Controls.Add(label6);
            panel1.Controls.Add(UserDB_W_textBox);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(Converter_comboBox);
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(UserDB_FL_textBox);
            panel1.Controls.Add(label31);
            panel1.Controls.Add(label32);
            panel1.Controls.Add(UserDB_lm_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(UserDB_eff_textBox);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(LampType_comboBox);
            panel1.Controls.Add(label8);
            panel1.Location = new Point(0, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1008, 123);
            panel1.TabIndex = 26;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(607, 51);
            label6.Name = "label6";
            label6.Size = new Size(21, 15);
            label6.TabIndex = 119;
            label6.Text = "lm";
            // 
            // UserDB_W_textBox
            // 
            UserDB_W_textBox.Location = new Point(481, 77);
            UserDB_W_textBox.Name = "UserDB_W_textBox";
            UserDB_W_textBox.Size = new Size(120, 23);
            UserDB_W_textBox.TabIndex = 118;
            UserDB_W_textBox.TextChanged += UserDB_W_textBox_TextChanged;
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.Location = new Point(758, 13);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 115;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.Location = new Point(481, 17);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(687, 17);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 114;
            label2.Text = "제조사";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(390, 78);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 117;
            label5.Text = "소비전력";
            // 
            // Converter_comboBox
            // 
            Converter_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Converter_comboBox.FormattingEnabled = true;
            Converter_comboBox.Location = new Point(234, 74);
            Converter_comboBox.Name = "Converter_comboBox";
            Converter_comboBox.Size = new Size(120, 23);
            Converter_comboBox.TabIndex = 116;
            Converter_comboBox.SelectedIndexChanged += Converter_comboBox_SelectedIndexChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(884, 75);
            label29.Name = "label29";
            label29.Size = new Size(12, 15);
            label29.TabIndex = 113;
            label29.Text = "-";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(390, 21);
            label13.Name = "label13";
            label13.Size = new Size(67, 15);
            label13.TabIndex = 90;
            label13.Text = "등기구명칭";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(681, 80);
            label30.Name = "label30";
            label30.Size = new Size(55, 15);
            label30.TabIndex = 111;
            label30.Text = "조명계수";
            // 
            // UserDB_FL_textBox
            // 
            UserDB_FL_textBox.Location = new Point(758, 76);
            UserDB_FL_textBox.Name = "UserDB_FL_textBox";
            UserDB_FL_textBox.Size = new Size(120, 23);
            UserDB_FL_textBox.TabIndex = 112;
            UserDB_FL_textBox.TextChanged += UserDB_FL_textBox_TextChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(610, 80);
            label31.Name = "label31";
            label31.Size = new Size(18, 15);
            label31.TabIndex = 110;
            label31.Text = "W";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(390, 48);
            label32.Name = "label32";
            label32.Size = new Size(31, 15);
            label32.TabIndex = 108;
            label32.Text = "광속";
            // 
            // UserDB_lm_textBox
            // 
            UserDB_lm_textBox.Location = new Point(481, 47);
            UserDB_lm_textBox.Name = "UserDB_lm_textBox";
            UserDB_lm_textBox.Size = new Size(120, 23);
            UserDB_lm_textBox.TabIndex = 109;
            UserDB_lm_textBox.TextChanged += UserDB_lm_textBox_TextChanged;
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
            UserNum_textBox.Location = new Point(234, 17);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(120, 16);
            UserNum_textBox.TabIndex = 105;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(11, 10);
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
            Deletebutton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(106, 6);
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
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(79, 6);
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
            label3.Location = new Point(884, 48);
            label3.Name = "label3";
            label3.Size = new Size(12, 15);
            label3.TabIndex = 98;
            label3.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(687, 51);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 96;
            label4.Text = "광효율";
            // 
            // UserDB_eff_textBox
            // 
            UserDB_eff_textBox.Location = new Point(758, 44);
            UserDB_eff_textBox.Name = "UserDB_eff_textBox";
            UserDB_eff_textBox.Size = new Size(120, 23);
            UserDB_eff_textBox.TabIndex = 97;
            UserDB_eff_textBox.TextChanged += UserDB_eff_textBox_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(144, 77);
            label10.Name = "label10";
            label10.Size = new Size(84, 15);
            label10.TabIndex = 92;
            label10.Text = "안정기/컨버터";
            // 
            // LampType_comboBox
            // 
            LampType_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LampType_comboBox.FormattingEnabled = true;
            LampType_comboBox.Location = new Point(234, 45);
            LampType_comboBox.Name = "LampType_comboBox";
            LampType_comboBox.Size = new Size(120, 23);
            LampType_comboBox.TabIndex = 43;
            LampType_comboBox.SelectedIndexChanged += LampType_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(144, 47);
            label8.Name = "label8";
            label8.Size = new Size(55, 15);
            label8.TabIndex = 42;
            label8.Text = "램프유형";
            // 
            // Light_dataGridView
            // 
            Light_dataGridView.AllowUserToAddRows = false;
            Light_dataGridView.AllowUserToDeleteRows = false;
            Light_dataGridView.AllowUserToResizeColumns = false;
            Light_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Light_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Light_dataGridView.BackgroundColor = SystemColors.Control;
            Light_dataGridView.BorderStyle = BorderStyle.None;
            Light_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Light_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Light_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Light_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Light_dataGridView.Location = new Point(0, 132);
            Light_dataGridView.Name = "Light_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Light_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Light_dataGridView.RowHeadersVisible = false;
            Light_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Light_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Light_dataGridView.RowTemplate.Height = 25;
            Light_dataGridView.Size = new Size(1008, 354);
            Light_dataGridView.TabIndex = 111;
            Light_dataGridView.CellContentClick += Light_dataGridView_CellContentClick;
            // 
            // LightingDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1009, 564);
            Controls.Add(Light_dataGridView);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Name = "LightingDB";
            Text = "LightingDB";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Light_dataGridView).EndInit();
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
        private TextBox UserDB_eff_textBox;
        private Label label10;
        private Label label13;
        private TextBox UserDBName_textBox;
        private ComboBox LampType_comboBox;
        private Label label8;
        private DataGridView Light_dataGridView;
        private Label label29;
        private Label label30;
        private TextBox UserDB_FL_textBox;
        private Label label31;
        private Label label32;
        private TextBox UserDB_lm_textBox;
        private Label label2;
        private TextBox UserDB_Manufacture_textBox;
        private Label label6;
        private TextBox UserDB_W_textBox;
        private Label label5;
        private ComboBox Converter_comboBox;
    }
}