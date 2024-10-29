namespace main.subcontents.RESystem_PV
{
    partial class PV_InverterDB
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
            UserDB_Kpk_textbox = new TextBox();
            PVInverter_dataGridView = new DataGridView();
            label6 = new Label();
            label3 = new Label();
            UserDB_Euro_TextBox = new TextBox();
            UserDB_Manufacture_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Deletebutton = new Button();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PVInverter_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(678, 45);
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
            Save_button.Location = new Point(531, 324);
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
            AddUserDB_button.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            label9.Size = new Size(65, 15);
            label9.TabIndex = 44;
            label9.Text = "EURO 효율";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(UserDB_Kpk_textbox);
            panel1.Controls.Add(PVInverter_dataGridView);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(UserDB_Euro_TextBox);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label9);
            panel1.Location = new Point(0, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(678, 275);
            panel1.TabIndex = 27;
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
            UserDB_Kpk_textbox.TextChanged += UserDB_Kpk_textbox_TextChanged;
            // 
            // PVInverter_dataGridView
            // 
            PVInverter_dataGridView.AllowUserToAddRows = false;
            PVInverter_dataGridView.AllowUserToDeleteRows = false;
            PVInverter_dataGridView.AllowUserToResizeColumns = false;
            PVInverter_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PVInverter_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PVInverter_dataGridView.BackgroundColor = SystemColors.Control;
            PVInverter_dataGridView.BorderStyle = BorderStyle.None;
            PVInverter_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PVInverter_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            PVInverter_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PVInverter_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PVInverter_dataGridView.Location = new Point(3, 80);
            PVInverter_dataGridView.Name = "PVInverter_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PVInverter_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PVInverter_dataGridView.RowHeadersVisible = false;
            PVInverter_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            PVInverter_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            PVInverter_dataGridView.RowTemplate.Height = 25;
            PVInverter_dataGridView.Size = new Size(673, 192);
            PVInverter_dataGridView.TabIndex = 19;
            PVInverter_dataGridView.CellContentClick += PVInverter_dataGridView_CellContentClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(433, 54);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 111;
            label6.Text = "%";
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
            // UserDB_Euro_TextBox
            // 
            UserDB_Euro_TextBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Euro_TextBox.Location = new Point(310, 48);
            UserDB_Euro_TextBox.Name = "UserDB_Euro_TextBox";
            UserDB_Euro_TextBox.Size = new Size(120, 23);
            UserDB_Euro_TextBox.TabIndex = 109;
            UserDB_Euro_TextBox.TextAlign = HorizontalAlignment.Center;
            UserDB_Euro_TextBox.TextChanged += UserDB_Euro_TextBox_TextChanged;
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
            label4.Font =  new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
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
            Deletebutton.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(98, 11);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // PV_InverterDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(678, 356);
            Controls.Add(Save_button);
            Controls.Add(panel1);
            Controls.Add(GeneralPanel);
            Name = "PV_InverterDB";
            Text = "PV_InverterDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PVInverter_dataGridView).EndInit();
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
        private DataGridView PVInverter_dataGridView;
        private Label label2;
        private ComboBox UserDB_year_comboBox;
        private Label label11;
        private TextBox UserDB_width_textBox;
        private Label label28;
        private TextBox UserDB_height_textBox;
        private Label label29;
        private TextBox UserDB_output_textBox;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label5;
        private TextBox UserDB_Kpk_textbox;
        private Label label6;
        private Label label3;
        private TextBox UserDB_Manufacture_textBox;
        private TextBox UserDB_Euro_TextBox;
    }
}