
namespace main.subcontents.RESystem_PV
{
    partial class PV_BatteryDB
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
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            AddUserDB_button = new Button();
            label9 = new Label();
            panel1 = new Panel();
            BatteryType_Combobox = new ComboBox();
            PVBattery_dataGridView = new DataGridView();
            label8 = new Label();
            label6 = new Label();
            label3 = new Label();
            UserDB_Ah_TextBox = new TextBox();
            UserDB_V_TextBox = new TextBox();
            UserDB_Manufacture_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Deletebutton = new Button();
            label2 = new Label();
            label7 = new Label();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PVBattery_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(711, 45);
            GeneralPanel.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(410, 15);
            label1.TabIndex = 0;
            label1.Text = "- DB를 추가하고자 하는 경우 각 항목의 값을 입력하고, + 버튼을 누르세요.\r\n";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(562, 324);
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
            AddUserDB_button.Font = new System.Drawing.Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            label9.Location = new Point(249, 52);
            label9.Name = "label9";
            label9.Size = new Size(31, 15);
            label9.TabIndex = 44;
            label9.Text = "전력";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(BatteryType_Combobox);
            panel1.Controls.Add(PVBattery_dataGridView);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserDB_Ah_TextBox);
            panel1.Controls.Add(UserDB_V_TextBox);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label9);
            panel1.Location = new Point(0, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(711, 275);
            panel1.TabIndex = 27;
            // 
            // BatteryType_Combobox
            // 
            BatteryType_Combobox.FormattingEnabled = true;
            BatteryType_Combobox.Location = new Point(71, 49);
            BatteryType_Combobox.Name = "BatteryType_Combobox";
            BatteryType_Combobox.Size = new Size(121, 23);
            BatteryType_Combobox.TabIndex = 122;
            BatteryType_Combobox.SelectedIndexChanged += BatteryType_Combobox_SelectedIndexChanged;
            // 
            // PVBattery_dataGridView
            // 
            PVBattery_dataGridView.AllowUserToAddRows = false;
            PVBattery_dataGridView.AllowUserToDeleteRows = false;
            PVBattery_dataGridView.AllowUserToResizeColumns = false;
            PVBattery_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PVBattery_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PVBattery_dataGridView.BackgroundColor = SystemColors.Control;
            PVBattery_dataGridView.BorderStyle = BorderStyle.None;
            PVBattery_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PVBattery_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            PVBattery_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PVBattery_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PVBattery_dataGridView.Location = new Point(3, 80);
            PVBattery_dataGridView.Name = "PVBattery_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PVBattery_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PVBattery_dataGridView.RowHeadersVisible = false;
            PVBattery_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            PVBattery_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            PVBattery_dataGridView.RowTemplate.Height = 25;
            PVBattery_dataGridView.Size = new Size(708, 192);
            PVBattery_dataGridView.TabIndex = 19;
            PVBattery_dataGridView.CellContentClick += PVBattery_dataGridView_CellContentClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(665, 56);
            label8.Name = "label8";
            label8.Size = new Size(22, 15);
            label8.TabIndex = 111;
            label8.Text = "Ah";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(433, 55);
            label6.Name = "label6";
            label6.Size = new Size(15, 15);
            label6.TabIndex = 111;
            label6.Text = "V";
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
            // UserDB_Ah_TextBox
            // 
            UserDB_Ah_TextBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Ah_TextBox.Location = new Point(542, 50);
            UserDB_Ah_TextBox.Name = "UserDB_Ah_TextBox";
            UserDB_Ah_TextBox.Size = new Size(120, 23);
            UserDB_Ah_TextBox.TabIndex = 109;
            UserDB_Ah_TextBox.TextAlign = HorizontalAlignment.Center;
            UserDB_Ah_TextBox.TextChanged += UserDB_Ah_TextBox_TextChanged;
            // 
            // UserDB_V_TextBox
            // 
            UserDB_V_TextBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_V_TextBox.Location = new Point(310, 49);
            UserDB_V_TextBox.Name = "UserDB_V_TextBox";
            UserDB_V_TextBox.Size = new Size(120, 23);
            UserDB_V_TextBox.TabIndex = 109;
            UserDB_V_TextBox.TextAlign = HorizontalAlignment.Center;
            UserDB_V_TextBox.TextChanged += UserDB_V_TextBox_TextChanged;
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
            label4.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
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
            Deletebutton.Font = new System.Drawing.Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(98, 11);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 52);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 44;
            label2.Text = "type";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(481, 56);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 44;
            label7.Text = "암페어시";
            // 
            // PV_BatteryDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(709, 356);
            Controls.Add(Save_button);
            Controls.Add(panel1);
            Controls.Add(GeneralPanel);
            Name = "PV_BatteryDB";
            Text = "PV_BatteryDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PVBattery_dataGridView).EndInit();
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
        private DataGridView PVBattery_dataGridView;
        private Label label2;
        private ComboBox UserDB_year_comboBox;
        private Label label11;
        private TextBox UserDB_width_textBox;
        private Label label28;
        private TextBox UserDB_height_textBox;
        private Label label29;
        private TextBox UserDB_V_TextBox;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label5;
        private TextBox UserDB_Kpk_textbox;
        private Label label6;
        private Label label3;
        private TextBox UserDB_Manufacture_textBox;
        private TextBox UserDB_Euro_TextBox;
        private Label label8;
        private TextBox UserDB_Ah_TextBox;
        private Label label7;
        private ComboBox BatteryType_Combobox;
    }
}