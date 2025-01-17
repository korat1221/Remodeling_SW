
namespace main.subcontents
{
    partial class GlassDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlassDB));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            Save_button = new Button();
            panel1 = new Panel();
            label16 = new Label();
            label12 = new Label();
            Deletebutton = new Button();
            AddUserDB_button = new Button();
            LE_CL_V_comboBox = new CustomComboBox();
            label14 = new Label();
            ArAir_comboBox = new CustomComboBox();
            label8 = new Label();
            SingleDoubleTriple_comboBox = new CustomComboBox();
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
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1209, 181);
            GeneralPanel.TabIndex = 18;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(805, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(175, 152);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(566, 6);
            label2.Name = "label2";
            label2.Size = new Size(76, 165);
            label2.TabIndex = 2;
            label2.Text = "CL: 맑은유리\r\n\r\nLE: 로이유리\r\n\r\nCLE: 색유리\r\n\r\nV: 진공유리\r\n\r\nA: 공기층\r\n\r\nR: 아르곤";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 41);
            label1.Name = "label1";
            label1.Size = new Size(392, 90);
            label1.TabIndex = 0;
            label1.Text = "- EN 674 기준 시뮬레이션 값을 적용합니다.\r\n\r\n- 오른쪽 그림을 참조하여 해당하는 값을 입력하시오.\r\n\r\n- 이중유리는 WINDOW에서 직접 작성한 값을 입력하거나, \r\n  표준DB or 사용자 DB에서 2개의 유리를 조합해서 작성할 수 있습니다.";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1054, 603);
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
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Controls.Add(LE_CL_V_comboBox);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(ArAir_comboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(SingleDoubleTriple_comboBox);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(0, 172);
            panel1.Name = "panel1";
            panel1.Size = new Size(1209, 56);
            panel1.TabIndex = 26;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label16.Location = new Point(354, 25);
            label16.Name = "label16";
            label16.Size = new Size(58, 15);
            label16.TabIndex = 116;
            label16.Text = "필터 기능";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label12.Location = new Point(1071, 24);
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
            Deletebutton.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            Deletebutton.Location = new Point(1166, 20);
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
            AddUserDB_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            AddUserDB_button.Location = new Point(1139, 20);
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
            LE_CL_V_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            LE_CL_V_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            LE_CL_V_comboBox.FormattingEnabled = true;
            LE_CL_V_comboBox.Location = new Point(670, 21);
            LE_CL_V_comboBox.Name = "LE_CL_V_comboBox";
            LE_CL_V_comboBox.Size = new Size(120, 23);
            LE_CL_V_comboBox.TabIndex = 100;
            LE_CL_V_comboBox.SelectedIndexChanged += LE_CL_V_comboBox_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(705, 3);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 99;
            label14.Text = "LE/CL/V";
            // 
            // ArAir_comboBox
            // 
            ArAir_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            ArAir_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            ArAir_comboBox.FormattingEnabled = true;
            ArAir_comboBox.Location = new Point(544, 21);
            ArAir_comboBox.Name = "ArAir_comboBox";
            ArAir_comboBox.Size = new Size(120, 23);
            ArAir_comboBox.TabIndex = 43;
            ArAir_comboBox.SelectedIndexChanged += ArAir_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(568, 4);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 42;
            label8.Text = "아르곤/공기";
            // 
            // SingleDoubleTriple_comboBox
            // 
            SingleDoubleTriple_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            SingleDoubleTriple_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            SingleDoubleTriple_comboBox.FormattingEnabled = true;
            SingleDoubleTriple_comboBox.Location = new Point(418, 21);
            SingleDoubleTriple_comboBox.Name = "SingleDoubleTriple_comboBox";
            SingleDoubleTriple_comboBox.Size = new Size(120, 23);
            SingleDoubleTriple_comboBox.TabIndex = 41;
            SingleDoubleTriple_comboBox.SelectedIndexChanged += SingleDoubleTriple_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(434, 5);
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
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Glass_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Glass_dataGridView.Location = new Point(0, 225);
            Glass_dataGridView.Name = "Glass_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Glass_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Glass_dataGridView.RowHeadersVisible = false;
            Glass_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Glass_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Glass_dataGridView.Size = new Size(1209, 362);
            Glass_dataGridView.TabIndex = 111;
            Glass_dataGridView.CellContentClick += Glass_dataGridView_CellContentClick;
            // 
            // GlassDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 640);
            Controls.Add(Glass_dataGridView);
            Controls.Add(panel1);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "GlassDB";
            Text = "Window_GlassDB";
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
        private Label label12;
        private Button Deletebutton;
        private Button AddUserDB_button;
        private CustomComboBox LE_CL_V_comboBox;
        private Label label14;
        private CustomComboBox ArAir_comboBox;
        private Label label8;
        private CustomComboBox SingleDoubleTriple_comboBox;
        private Label label7;
        private DataGridView Glass_dataGridView;
        private Label label16;
    }
}