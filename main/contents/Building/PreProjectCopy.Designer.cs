
namespace main.contents
{
    partial class PreProjectCopy
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
            panel1 = new Panel();
            label4 = new Label();
            System_checkBox = new CheckBox();
            System_pictureBox = new PictureBox();
            Zone_checkBox = new CheckBox();
            Zone_pictureBox = new PictureBox();
            Model_checkBox = new CheckBox();
            Model_pictureBox = new PictureBox();
            Construction_checkBox = new CheckBox();
            Construction_pictureBox = new PictureBox();
            Building_checkBox = new CheckBox();
            Building_pictureBox = new PictureBox();
            Save_button = new Button();
            dataGridView1 = new DataGridView();
            chk = new DataGridViewCheckBoxColumn();
            num = new DataGridViewTextBoxColumn();
            pnum = new DataGridViewTextBoxColumn();
            pname = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)System_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Zone_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Model_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Construction_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Building_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(System_checkBox);
            panel1.Controls.Add(System_pictureBox);
            panel1.Controls.Add(Zone_checkBox);
            panel1.Controls.Add(Zone_pictureBox);
            panel1.Controls.Add(Model_checkBox);
            panel1.Controls.Add(Model_pictureBox);
            panel1.Controls.Add(Construction_checkBox);
            panel1.Controls.Add(Construction_pictureBox);
            panel1.Controls.Add(Building_checkBox);
            panel1.Controls.Add(Building_pictureBox);
            panel1.Controls.Add(Save_button);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(713, 360);
            panel1.TabIndex = 19;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(12, 21);
            label4.Name = "label4";
            label4.Size = new Size(119, 14);
            label4.TabIndex = 130;
            label4.Text = "기존 건물 프로젝트";
            // 
            // System_checkBox
            // 
            System_checkBox.AutoSize = true;
            System_checkBox.Enabled = false;
            System_checkBox.Font = new Font(UTIL.Families[0], 9.75F);
            System_checkBox.Location = new Point(520, 260);
            System_checkBox.Name = "System_checkBox";
            System_checkBox.Size = new Size(77, 18);
            System_checkBox.TabIndex = 129;
            System_checkBox.Text = "설비 정보";
            System_checkBox.UseVisualStyleBackColor = true;
            System_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // System_pictureBox
            // 
            System_pictureBox.Location = new Point(524, 186);
            System_pictureBox.Name = "System_pictureBox";
            System_pictureBox.Size = new Size(70, 60);
            System_pictureBox.TabIndex = 128;
            System_pictureBox.TabStop = false;
            // 
            // Zone_checkBox
            // 
            Zone_checkBox.AutoSize = true;
            Zone_checkBox.Enabled = false;
            Zone_checkBox.Font = new Font(UTIL.Families[0], 9.75F);
            Zone_checkBox.Location = new Point(422, 260);
            Zone_checkBox.Name = "Zone_checkBox";
            Zone_checkBox.Size = new Size(65, 18);
            Zone_checkBox.TabIndex = 127;
            Zone_checkBox.Text = "존 정보";
            Zone_checkBox.UseVisualStyleBackColor = true;
            Zone_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Zone_pictureBox
            // 
            Zone_pictureBox.Location = new Point(420, 186);
            Zone_pictureBox.Name = "Zone_pictureBox";
            Zone_pictureBox.Size = new Size(70, 60);
            Zone_pictureBox.TabIndex = 126;
            Zone_pictureBox.TabStop = false;
            // 
            // Model_checkBox
            // 
            Model_checkBox.AutoSize = true;
            Model_checkBox.Enabled = false;
            Model_checkBox.Font = new Font(UTIL.Families[0], 9.75F);
            Model_checkBox.Location = new Point(316, 260);
            Model_checkBox.Name = "Model_checkBox";
            Model_checkBox.Size = new Size(70, 18);
            Model_checkBox.TabIndex = 125;
            Model_checkBox.Text = "3D 정보";
            Model_checkBox.UseVisualStyleBackColor = true;
            Model_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Model_pictureBox
            // 
            Model_pictureBox.Location = new Point(316, 186);
            Model_pictureBox.Name = "Model_pictureBox";
            Model_pictureBox.Size = new Size(70, 60);
            Model_pictureBox.TabIndex = 124;
            Model_pictureBox.TabStop = false;
            // 
            // Construction_checkBox
            // 
            Construction_checkBox.AutoSize = true;
            Construction_checkBox.Enabled = false;
            Construction_checkBox.Font = new Font(UTIL.Families[0], 9.75F);
            Construction_checkBox.Location = new Point(202, 260);
            Construction_checkBox.Name = "Construction_checkBox";
            Construction_checkBox.Size = new Size(89, 18);
            Construction_checkBox.TabIndex = 123;
            Construction_checkBox.Text = "구조체 정보";
            Construction_checkBox.UseVisualStyleBackColor = true;
            Construction_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Construction_pictureBox
            // 
            Construction_pictureBox.Location = new Point(212, 186);
            Construction_pictureBox.Name = "Construction_pictureBox";
            Construction_pictureBox.Size = new Size(70, 60);
            Construction_pictureBox.TabIndex = 122;
            Construction_pictureBox.TabStop = false;
            // 
            // Building_checkBox
            // 
            Building_checkBox.AutoSize = true;
            Building_checkBox.Checked = true;
            Building_checkBox.CheckState = CheckState.Checked;
            Building_checkBox.Font = new Font(UTIL.Families[0], 9.75F);
            Building_checkBox.Location = new Point(104, 260);
            Building_checkBox.Name = "Building_checkBox";
            Building_checkBox.Size = new Size(77, 18);
            Building_checkBox.TabIndex = 121;
            Building_checkBox.Text = "건물 정보";
            Building_checkBox.UseVisualStyleBackColor = true;
            Building_checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // Building_pictureBox
            // 
            Building_pictureBox.Location = new Point(108, 186);
            Building_pictureBox.Name = "Building_pictureBox";
            Building_pictureBox.Size = new Size(70, 60);
            Building_pictureBox.TabIndex = 120;
            Building_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(524, 315);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 119;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { chk, num, pnum, pname, type });
            dataGridView1.Location = new Point(12, 39);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Size = new Size(678, 135);
            dataGridView1.TabIndex = 100;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // chk
            // 
            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            chk.FillWeight = 91.37056F;
            chk.HeaderText = "";
            chk.Name = "chk";
            chk.Resizable = DataGridViewTriState.False;
            chk.Width = 24;
            // 
            // num
            // 
            num.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            num.FillWeight = 106.2909F;
            num.HeaderText = "번호";
            num.Name = "num";
            num.ReadOnly = true;
            num.Resizable = DataGridViewTriState.False;
            num.Width = 60;
            // 
            // pnum
            // 
            pnum.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            pnum.FillWeight = 22.2307777F;
            pnum.HeaderText = "프로젝트 번호";
            pnum.Name = "pnum";
            pnum.ReadOnly = true;
            pnum.Resizable = DataGridViewTriState.False;
            pnum.Width = 110;
            // 
            // pname
            // 
            pname.FillWeight = 257.877F;
            pname.HeaderText = "프로젝트명";
            pname.Name = "pname";
            // 
            // type
            // 
            type.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            type.FillWeight = 22.2307777F;
            type.HeaderText = "유형";
            type.Name = "type";
            type.ReadOnly = true;
            type.Resizable = DataGridViewTriState.False;
            type.Width = 96;
            // 
            // PreProjectCopy
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(710, 358);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PreProjectCopy";
            Text = "기존 건물 프로젝트 복사";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)System_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Zone_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Model_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Construction_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Building_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Panel panel1;
        private CheckBox System_checkBox;
        private PictureBox System_pictureBox;
        private CheckBox Zone_checkBox;
        private PictureBox Zone_pictureBox;
        private CheckBox Model_checkBox;
        private PictureBox Model_pictureBox;
        private CheckBox Construction_checkBox;
        private PictureBox Construction_pictureBox;
        private CheckBox Building_checkBox;
        private PictureBox Building_pictureBox;
        private Button Save_button;
        private DataGridView dataGridView1;
        private Label label4;
        private DataGridViewCheckBoxColumn chk;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn pnum;
        private DataGridViewTextBoxColumn pname;
        private DataGridViewTextBoxColumn type;
    }
}