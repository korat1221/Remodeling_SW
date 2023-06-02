namespace main.subcontents
{
    partial class Window_DoubleGlassDB
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
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            Save_button = new Button();
            panel1 = new Panel();
            label29 = new Label();
            label30 = new Label();
            UserDB_RInternal_textBox = new TextBox();
            label31 = new Label();
            label32 = new Label();
            UserDB_RExternal_textBox = new TextBox();
            label6 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            label15 = new Label();
            UserNum_textBox = new TextBox();
            label12 = new Label();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            LE_CL_V_comboBox = new ComboBox();
            label14 = new Label();
            label3 = new Label();
            label4 = new Label();
            UserDB_Tao_textBox = new TextBox();
            label5 = new Label();
            label10 = new Label();
            UserDB_g_textBox = new TextBox();
            label13 = new Label();
            UserDBName_textBox = new TextBox();
            label11 = new Label();
            label9 = new Label();
            UserDB_Ug_textBox = new TextBox();
            ArAir_comboBox = new ComboBox();
            label8 = new Label();
            SingleDoubleTriple_comboBox = new ComboBox();
            label7 = new Label();
            Glass_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Glass_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(Glass_dataGridView);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 181);
            GeneralPanel.TabIndex = 18;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(631, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(175, 152);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(500, 6);
            label2.Name = "label2";
            label2.Size = new Size(76, 165);
            label2.TabIndex = 2;
            label2.Text = "CL: 맑은유리\r\n\r\nLE: 로이유리\r\n\r\nCLE: 색유리\r\n\r\nV: 진공유리\r\n\r\nA: 공기층\r\n\r\nR: 아르곤";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 43);
            label1.Name = "label1";
            label1.Size = new Size(457, 75);
            label1.TabIndex = 0;
            label1.Text = "- EN 674 기준 시뮬레이션 값을 적용합니다.\r\n\r\n- 오른쪽 그림을 참조하여 해당하는 값을 입력하시오.\r\n\r\n- 이중유리는 표준DB or 사용자 DB에서 2개의 유리를 조합해서 작성할 수 있습니다.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(830, 781);
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
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(UserDB_RInternal_textBox);
            panel1.Controls.Add(label31);
            panel1.Controls.Add(label32);
            panel1.Controls.Add(UserDB_RExternal_textBox);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(LE_CL_V_comboBox);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(UserDB_Tao_textBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(UserDB_g_textBox);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(UserDB_Ug_textBox);
            panel1.Controls.Add(ArAir_comboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(SingleDoubleTriple_comboBox);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(0, 319);
            panel1.Name = "panel1";
            panel1.Size = new Size(977, 123);
            panel1.TabIndex = 26;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(883, 97);
            label29.Name = "label29";
            label29.Size = new Size(12, 15);
            label29.TabIndex = 113;
            label29.Text = "-";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(692, 97);
            label30.Name = "label30";
            label30.Size = new Size(67, 15);
            label30.TabIndex = 111;
            label30.Text = "내부반사율";
            // 
            // UserDB_RInternal_textBox
            // 
            UserDB_RInternal_textBox.Location = new Point(760, 93);
            UserDB_RInternal_textBox.Name = "UserDB_RInternal_textBox";
            UserDB_RInternal_textBox.Size = new Size(120, 23);
            UserDB_RInternal_textBox.TabIndex = 112;
            UserDB_RInternal_textBox.TextChanged += UserDB_RInternal_textBox_TextChanged;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(605, 97);
            label31.Name = "label31";
            label31.Size = new Size(12, 15);
            label31.TabIndex = 110;
            label31.Text = "-";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(401, 97);
            label32.Name = "label32";
            label32.Size = new Size(67, 15);
            label32.TabIndex = 108;
            label32.Text = "외부반사율";
            // 
            // UserDB_RExternal_textBox
            // 
            UserDB_RExternal_textBox.Location = new Point(481, 93);
            UserDB_RExternal_textBox.Name = "UserDB_RExternal_textBox";
            UserDB_RExternal_textBox.Size = new Size(120, 23);
            UserDB_RExternal_textBox.TabIndex = 109;
            UserDB_RExternal_textBox.TextChanged += UserDB_RExternal_textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(704, 10);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 106;
            label6.Text = "제조사";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.Location = new Point(760, 6);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 107;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(143, 10);
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
            Deletebutton.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            AddUserDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(79, 6);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 101;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // LE_CL_V_comboBox
            // 
            LE_CL_V_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LE_CL_V_comboBox.FormattingEnabled = true;
            LE_CL_V_comboBox.Location = new Point(760, 35);
            LE_CL_V_comboBox.Name = "LE_CL_V_comboBox";
            LE_CL_V_comboBox.Size = new Size(120, 23);
            LE_CL_V_comboBox.TabIndex = 100;
            LE_CL_V_comboBox.SelectedIndexChanged += LE_CL_V_comboBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(700, 39);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 99;
            label14.Text = "LE/CL/V";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(883, 68);
            label3.Name = "label3";
            label3.Size = new Size(12, 15);
            label3.TabIndex = 98;
            label3.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(698, 68);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 96;
            label4.Text = "빛투과율";
            // 
            // UserDB_Tao_textBox
            // 
            UserDB_Tao_textBox.Location = new Point(760, 64);
            UserDB_Tao_textBox.Name = "UserDB_Tao_textBox";
            UserDB_Tao_textBox.Size = new Size(120, 23);
            UserDB_Tao_textBox.TabIndex = 97;
            UserDB_Tao_textBox.TextChanged += UserDB_Tao_textBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(605, 68);
            label5.Name = "label5";
            label5.Size = new Size(12, 15);
            label5.TabIndex = 94;
            label5.Text = "-";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(395, 68);
            label10.Name = "label10";
            label10.Size = new Size(79, 15);
            label10.TabIndex = 92;
            label10.Text = "태양열취득률";
            // 
            // UserDB_g_textBox
            // 
            UserDB_g_textBox.Location = new Point(481, 64);
            UserDB_g_textBox.Name = "UserDB_g_textBox";
            UserDB_g_textBox.Size = new Size(120, 23);
            UserDB_g_textBox.TabIndex = 93;
            UserDB_g_textBox.TextChanged += UserDB_g_textBox_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(413, 10);
            label13.Name = "label13";
            label13.Size = new Size(43, 15);
            label13.TabIndex = 90;
            label13.Text = "제품명";
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.Location = new Point(481, 6);
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
            label11.Size = new Size(48, 15);
            label11.TabIndex = 48;
            label11.Text = "W/m²·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(131, 68);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 44;
            label9.Text = "열관류율";
            // 
            // UserDB_Ug_textBox
            // 
            UserDB_Ug_textBox.Location = new Point(202, 64);
            UserDB_Ug_textBox.Name = "UserDB_Ug_textBox";
            UserDB_Ug_textBox.Size = new Size(120, 23);
            UserDB_Ug_textBox.TabIndex = 45;
            UserDB_Ug_textBox.TextChanged += UserDB_Ug_textBox_TextChanged;
            // 
            // ArAir_comboBox
            // 
            ArAir_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ArAir_comboBox.FormattingEnabled = true;
            ArAir_comboBox.Location = new Point(481, 35);
            ArAir_comboBox.Name = "ArAir_comboBox";
            ArAir_comboBox.Size = new Size(120, 23);
            ArAir_comboBox.TabIndex = 43;
            ArAir_comboBox.SelectedIndexChanged += ArAir_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(398, 39);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 42;
            label8.Text = "아르곤/공기";
            // 
            // SingleDoubleTriple_comboBox
            // 
            SingleDoubleTriple_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SingleDoubleTriple_comboBox.FormattingEnabled = true;
            SingleDoubleTriple_comboBox.Location = new Point(202, 35);
            SingleDoubleTriple_comboBox.Name = "SingleDoubleTriple_comboBox";
            SingleDoubleTriple_comboBox.Size = new Size(120, 23);
            SingleDoubleTriple_comboBox.TabIndex = 41;
            SingleDoubleTriple_comboBox.SelectedIndexChanged += SingleDoubleTriple_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(114, 39);
            label7.Name = "label7";
            label7.Size = new Size(89, 15);
            label7.TabIndex = 40;
            label7.Text = "복층/삼중/단창";
            // 
            // Glass_dataGridView
            // 
            Glass_dataGridView.AllowUserToAddRows = false;
            Glass_dataGridView.AllowUserToDeleteRows = false;
            Glass_dataGridView.AllowUserToResizeColumns = false;
            Glass_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Glass_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Glass_dataGridView.BackgroundColor = SystemColors.Control;
            Glass_dataGridView.BorderStyle = BorderStyle.None;
            Glass_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Glass_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Glass_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Glass_dataGridView.Location = new Point(0, 174);
            Glass_dataGridView.Name = "Glass_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Glass_dataGridView.RowHeadersVisible = false;
            Glass_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Glass_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Glass_dataGridView.RowTemplate.Height = 25;
            Glass_dataGridView.Size = new Size(977, 148);
            Glass_dataGridView.TabIndex = 111;
            // 
            // Window_DoubleGlassDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 818);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "Window_DoubleGlassDB";
            Text = "Window_DoubleGlassDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Glass_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private Label label2;
        private Button Save_button;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label15;
        private TextBox UserNum_textBox;
        private Label label12;
        private Button Deletebutton;
        private Button AddUserDB_button;
        private ComboBox LE_CL_V_comboBox;
        private Label label14;
        private Label label3;
        private Label label4;
        private TextBox UserDB_Tao_textBox;
        private Label label5;
        private Label label10;
        private TextBox UserDB_g_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Label label11;
        private Label label9;
        private TextBox UserDB_Ug_textBox;
        private ComboBox ArAir_comboBox;
        private Label label8;
        private ComboBox SingleDoubleTriple_comboBox;
        private Label label7;
        private DataGridView Glass_dataGridView;
        private Label label6;
        private TextBox UserDB_Manufacture_textBox;
        private Label label29;
        private Label label30;
        private TextBox UserDB_RInternal_textBox;
        private Label label31;
        private Label label32;
        private TextBox UserDB_RExternal_textBox;
    }
}