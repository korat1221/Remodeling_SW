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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window_DoubleGlassDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            Save_button = new Button();
            Glass_dataGridView = new DataGridView();
            DoubleGlass_dataGridView = new DataGridView();
            UserDBName_textBox = new TextBox();
            label13 = new Label();
            AddUserDB_button = new Button();
            Deletebutton = new Button();
            label12 = new Label();
            UserNum_textBox = new TextBox();
            label15 = new Label();
            UserDB_Manufacture_textBox = new TextBox();
            label6 = new Label();
            label16 = new Label();
            SelectGlass1_comboBox = new CustomComboBox();
            label17 = new Label();
            SelectGlass2_comboBox = new CustomComboBox();
            panel1 = new Panel();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Glass_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DoubleGlass_dataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
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
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
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
            // Glass_dataGridView
            // 
            Glass_dataGridView.AllowUserToAddRows = false;
            Glass_dataGridView.AllowUserToDeleteRows = false;
            Glass_dataGridView.AllowUserToResizeColumns = false;
            Glass_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Glass_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Glass_dataGridView.BackgroundColor = SystemColors.ControlLight;
            Glass_dataGridView.BorderStyle = BorderStyle.None;
            Glass_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Glass_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Glass_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            Glass_dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            Glass_dataGridView.Location = new Point(0, 178);
            Glass_dataGridView.Name = "Glass_dataGridView";
            Glass_dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            Glass_dataGridView.RowHeadersVisible = false;
            Glass_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle4.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            Glass_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            Glass_dataGridView.RowTemplate.Height = 25;
            Glass_dataGridView.Size = new Size(977, 342);
            Glass_dataGridView.TabIndex = 112;
            // 
            // DoubleGlass_dataGridView
            // 
            DoubleGlass_dataGridView.AllowUserToAddRows = false;
            DoubleGlass_dataGridView.AllowUserToDeleteRows = false;
            DoubleGlass_dataGridView.AllowUserToResizeColumns = false;
            DoubleGlass_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DoubleGlass_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DoubleGlass_dataGridView.BackgroundColor = SystemColors.Control;
            DoubleGlass_dataGridView.BorderStyle = BorderStyle.None;
            DoubleGlass_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DoubleGlass_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            DoubleGlass_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            DoubleGlass_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DoubleGlass_dataGridView.Location = new Point(0, 584);
            DoubleGlass_dataGridView.Name = "DoubleGlass_dataGridView";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DoubleGlass_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            DoubleGlass_dataGridView.RowHeadersVisible = false;
            DoubleGlass_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            DoubleGlass_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle7;
            DoubleGlass_dataGridView.RowTemplate.Height = 25;
            DoubleGlass_dataGridView.Size = new Size(977, 191);
            DoubleGlass_dataGridView.TabIndex = 113;
            DoubleGlass_dataGridView.CellContentClick += DoubleGlass_dataGridView_CellContentClick;
            // 
            // UserDBName_textBox
            // 
            UserDBName_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDBName_textBox.Location = new Point(481, 6);
            UserDBName_textBox.Name = "UserDBName_textBox";
            UserDBName_textBox.Size = new Size(120, 23);
            UserDBName_textBox.TabIndex = 91;
            UserDBName_textBox.TextAlign = HorizontalAlignment.Center;
            UserDBName_textBox.TextChanged += UserDBName_textBox_TextChanged;
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
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddUserDB_button.Location = new Point(79, 6);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 101;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(106, 6);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 102;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font =  new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(11, 10);
            label12.Name = "label12";
            label12.Size = new Size(60, 15);
            label12.TabIndex = 103;
            label12.Text = "이중창DB";
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
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(143, 10);
            label15.Name = "label15";
            label15.Size = new Size(31, 15);
            label15.TabIndex = 104;
            label15.Text = "번호";
            // 
            // UserDB_Manufacture_textBox
            // 
            UserDB_Manufacture_textBox.BorderStyle = BorderStyle.FixedSingle;
            UserDB_Manufacture_textBox.Location = new Point(760, 6);
            UserDB_Manufacture_textBox.Name = "UserDB_Manufacture_textBox";
            UserDB_Manufacture_textBox.Size = new Size(120, 23);
            UserDB_Manufacture_textBox.TabIndex = 107;
            UserDB_Manufacture_textBox.TextAlign = HorizontalAlignment.Center;
            UserDB_Manufacture_textBox.TextChanged += UserDB_Manufacture_textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(690, 10);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 106;
            label6.Text = "제조사";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(387, 39);
            label16.Name = "label16";
            label16.Size = new Size(94, 15);
            label16.TabIndex = 114;
            label16.Text = "(외부)유리선택1";
            // 
            // SelectGlass1_comboBox
            // 
            SelectGlass1_comboBox.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SelectGlass1_comboBox.FormattingEnabled = true;
            SelectGlass1_comboBox.Location = new Point(481, 35);
            SelectGlass1_comboBox.Name = "SelectGlass1_comboBox";
            SelectGlass1_comboBox.Size = new Size(120, 23);
            SelectGlass1_comboBox.TabIndex = 115;
            SelectGlass1_comboBox.SelectedIndexChanged += SelectGlass1_comboBox_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(664, 39);
            label17.Name = "label17";
            label17.Size = new Size(94, 15);
            label17.TabIndex = 116;
            label17.Text = "(내부)유리선택2";
            // 
            // SelectGlass2_comboBox
            // 
            SelectGlass2_comboBox.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SelectGlass2_comboBox.FormattingEnabled = true;
            SelectGlass2_comboBox.Location = new Point(760, 35);
            SelectGlass2_comboBox.Name = "SelectGlass2_comboBox";
            SelectGlass2_comboBox.Size = new Size(120, 23);
            SelectGlass2_comboBox.TabIndex = 117;
            SelectGlass2_comboBox.SelectedIndexChanged += SelectGlass2_comboBox_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(SelectGlass2_comboBox);
            panel1.Controls.Add(label17);
            panel1.Controls.Add(SelectGlass1_comboBox);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(UserDB_Manufacture_textBox);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(UserDBName_textBox);
            panel1.Location = new Point(0, 520);
            panel1.Name = "panel1";
            panel1.Size = new Size(977, 65);
            panel1.TabIndex = 26;
            // 
            // Window_DoubleGlassDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 818);
            Controls.Add(DoubleGlass_dataGridView);
            Controls.Add(Glass_dataGridView);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "Window_DoubleGlassDB";
            Text = "Window_DoubleGlassDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Glass_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)DoubleGlass_dataGridView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Label label5;
        private Label label10;
        private TextBox UserDB_g_textBox;
        private Label label13;
        private TextBox UserDBName_textBox;
        private Label label8;
        private Label label6;
        private TextBox UserDB_Manufacture_textBox;
        private Label label31;
        private Label label32;
        private TextBox UserDB_RExternal_textBox;
        private TextBox ArAir_textBox;
        private CustomComboBox SelectGlass2_comboBox;
        private Label label17;
        private CustomComboBox SelectGlass1_comboBox;
        private Label label16;
        private DataGridView Glass_dataGridView;
        private DataGridView DoubleGlass_dataGridView;
    }
}