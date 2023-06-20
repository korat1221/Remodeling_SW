namespace main.subcontents.ConstructionWall
{
    partial class Wall_TB
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
            d_Ins_textBox = new TextBox();
            label3 = new Label();
            label7 = new Label();
            TB_Type_comboBox = new ComboBox();
            StructureType_textBox = new TextBox();
            label6 = new Label();
            WallType_textBox = new TextBox();
            label1 = new Label();
            Save_button = new Button();
            panel1 = new Panel();
            PerArea_label2 = new TextBox();
            PerArea_textBox = new TextBox();
            PerArea_label1 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            dy_textBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            textBox3 = new TextBox();
            dx_textBox = new TextBox();
            dU_textBox = new TextBox();
            Ueff_label2 = new Label();
            label2 = new Label();
            TB_dataGridView = new DataGridView();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TB_dataGridView).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(d_Ins_textBox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label7);
            GeneralPanel.Controls.Add(TB_Type_comboBox);
            GeneralPanel.Controls.Add(StructureType_textBox);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(WallType_textBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(773, 101);
            GeneralPanel.TabIndex = 18;
            // 
            // d_Ins_textBox
            // 
            d_Ins_textBox.BackColor = Color.AliceBlue;
            d_Ins_textBox.BorderStyle = BorderStyle.None;
            d_Ins_textBox.Enabled = false;
            d_Ins_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            d_Ins_textBox.ForeColor = SystemColors.ControlDark;
            d_Ins_textBox.Location = new Point(221, 62);
            d_Ins_textBox.Name = "d_Ins_textBox";
            d_Ins_textBox.Size = new Size(120, 15);
            d_Ins_textBox.TabIndex = 103;
            d_Ins_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlDark;
            label3.Location = new Point(104, 62);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 102;
            label3.Text = "단열재 두께";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(449, 62);
            label7.Name = "label7";
            label7.Size = new Size(91, 15);
            label7.TabIndex = 101;
            label7.Text = "외장재고정방법";
            // 
            // TB_Type_comboBox
            // 
            TB_Type_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TB_Type_comboBox.FormattingEnabled = true;
            TB_Type_comboBox.Location = new Point(546, 57);
            TB_Type_comboBox.Name = "TB_Type_comboBox";
            TB_Type_comboBox.Size = new Size(120, 24);
            TB_Type_comboBox.TabIndex = 53;
            TB_Type_comboBox.SelectedIndexChanged += TB_Type_comboBox_SelectedIndexChanged;
            // 
            // StructureType_textBox
            // 
            StructureType_textBox.BackColor = Color.AliceBlue;
            StructureType_textBox.BorderStyle = BorderStyle.None;
            StructureType_textBox.Enabled = false;
            StructureType_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            StructureType_textBox.ForeColor = SystemColors.ControlDark;
            StructureType_textBox.Location = new Point(546, 27);
            StructureType_textBox.Name = "StructureType_textBox";
            StructureType_textBox.Size = new Size(120, 15);
            StructureType_textBox.TabIndex = 100;
            StructureType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(449, 27);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 99;
            label6.Text = "구조유형";
            // 
            // WallType_textBox
            // 
            WallType_textBox.BackColor = Color.AliceBlue;
            WallType_textBox.BorderStyle = BorderStyle.None;
            WallType_textBox.Enabled = false;
            WallType_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WallType_textBox.ForeColor = SystemColors.ControlDark;
            WallType_textBox.Location = new Point(221, 27);
            WallType_textBox.Name = "WallType_textBox";
            WallType_textBox.Size = new Size(120, 15);
            WallType_textBox.TabIndex = 98;
            WallType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlDark;
            label1.Location = new Point(104, 27);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 97;
            label1.Text = "외벽 리모델링 유형";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(625, 691);
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
            panel1.Controls.Add(PerArea_label2);
            panel1.Controls.Add(PerArea_textBox);
            panel1.Controls.Add(PerArea_label1);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(dy_textBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(dx_textBox);
            panel1.Controls.Add(dU_textBox);
            panel1.Controls.Add(Ueff_label2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 344);
            panel1.Name = "panel1";
            panel1.Size = new Size(773, 84);
            panel1.TabIndex = 27;
            // 
            // PerArea_label2
            // 
            PerArea_label2.BackColor = SystemColors.GradientInactiveCaption;
            PerArea_label2.BorderStyle = BorderStyle.None;
            PerArea_label2.Enabled = false;
            PerArea_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PerArea_label2.ForeColor = SystemColors.ControlDark;
            PerArea_label2.Location = new Point(672, 56);
            PerArea_label2.Name = "PerArea_label2";
            PerArea_label2.Size = new Size(68, 15);
            PerArea_label2.TabIndex = 145;
            PerArea_label2.TextAlign = HorizontalAlignment.Center;
            // 
            // PerArea_textBox
            // 
            PerArea_textBox.BackColor = SystemColors.GradientInactiveCaption;
            PerArea_textBox.BorderStyle = BorderStyle.None;
            PerArea_textBox.Enabled = false;
            PerArea_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PerArea_textBox.ForeColor = SystemColors.ControlDark;
            PerArea_textBox.Location = new Point(546, 56);
            PerArea_textBox.Name = "PerArea_textBox";
            PerArea_textBox.Size = new Size(120, 15);
            PerArea_textBox.TabIndex = 144;
            PerArea_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PerArea_label1
            // 
            PerArea_label1.BackColor = SystemColors.GradientInactiveCaption;
            PerArea_label1.BorderStyle = BorderStyle.None;
            PerArea_label1.Enabled = false;
            PerArea_label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PerArea_label1.ForeColor = SystemColors.ControlDark;
            PerArea_label1.Location = new Point(449, 56);
            PerArea_label1.Name = "PerArea_label1";
            PerArea_label1.Size = new Size(91, 15);
            PerArea_label1.TabIndex = 143;
            PerArea_label1.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(672, 20);
            label8.Name = "label8";
            label8.Size = new Size(18, 16);
            label8.TabIndex = 142;
            label8.Text = "m";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(449, 21);
            label9.Name = "label9";
            label9.Size = new Size(69, 15);
            label9.TabIndex = 141;
            label9.Text = "수평간격[y]";
            // 
            // dy_textBox
            // 
            dy_textBox.BackColor = SystemColors.GradientInactiveCaption;
            dy_textBox.BorderStyle = BorderStyle.FixedSingle;
            dy_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dy_textBox.ForeColor = SystemColors.ControlText;
            dy_textBox.Location = new Point(546, 18);
            dy_textBox.Name = "dy_textBox";
            dy_textBox.Size = new Size(120, 22);
            dy_textBox.TabIndex = 140;
            dy_textBox.TextAlign = HorizontalAlignment.Center;
            dy_textBox.KeyPress += dy_textBox_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(347, 21);
            label5.Name = "label5";
            label5.Size = new Size(18, 16);
            label5.TabIndex = 139;
            label5.Text = "m";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(104, 22);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 138;
            label4.Text = "수직간격[x]";
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.GradientInactiveCaption;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Enabled = false;
            textBox3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.ForeColor = SystemColors.ControlDark;
            textBox3.Location = new Point(546, 22);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(120, 15);
            textBox3.TabIndex = 136;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // dx_textBox
            // 
            dx_textBox.BackColor = SystemColors.GradientInactiveCaption;
            dx_textBox.BorderStyle = BorderStyle.FixedSingle;
            dx_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dx_textBox.ForeColor = SystemColors.ControlText;
            dx_textBox.Location = new Point(221, 18);
            dx_textBox.Name = "dx_textBox";
            dx_textBox.Size = new Size(120, 22);
            dx_textBox.TabIndex = 135;
            dx_textBox.TextAlign = HorizontalAlignment.Center;
            dx_textBox.KeyPress += dx_textBox_KeyPress;
            // 
            // dU_textBox
            // 
            dU_textBox.BackColor = SystemColors.GradientInactiveCaption;
            dU_textBox.BorderStyle = BorderStyle.None;
            dU_textBox.Enabled = false;
            dU_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dU_textBox.ForeColor = SystemColors.ControlText;
            dU_textBox.Location = new Point(221, 56);
            dU_textBox.Name = "dU_textBox";
            dU_textBox.Size = new Size(120, 15);
            dU_textBox.TabIndex = 134;
            dU_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Ueff_label2
            // 
            Ueff_label2.AutoSize = true;
            Ueff_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ueff_label2.ForeColor = SystemColors.ControlText;
            Ueff_label2.Location = new Point(347, 55);
            Ueff_label2.Name = "Ueff_label2";
            Ueff_label2.Size = new Size(50, 16);
            Ueff_label2.TabIndex = 133;
            Ueff_label2.Text = "W/m²·K";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(104, 55);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 100;
            label2.Text = "1D 열교가산치";
            // 
            // TB_dataGridView
            // 
            TB_dataGridView.AllowUserToAddRows = false;
            TB_dataGridView.AllowUserToDeleteRows = false;
            TB_dataGridView.AllowUserToResizeColumns = false;
            TB_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TB_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            TB_dataGridView.BackgroundColor = SystemColors.Control;
            TB_dataGridView.BorderStyle = BorderStyle.None;
            TB_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            TB_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            TB_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            TB_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TB_dataGridView.Location = new Point(0, 430);
            TB_dataGridView.Name = "TB_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            TB_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            TB_dataGridView.RowHeadersVisible = false;
            TB_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            TB_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            TB_dataGridView.RowTemplate.Height = 25;
            TB_dataGridView.Size = new Size(773, 242);
            TB_dataGridView.TabIndex = 19;
            TB_dataGridView.CellContentClick += Spacer_dataGridView_CellContentClick;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(0, 97);
            panel2.Name = "panel2";
            panel2.Size = new Size(773, 247);
            panel2.TabIndex = 28;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(464, 16);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(202, 215);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(86, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(202, 215);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Wall_TB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(772, 730);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(TB_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "Wall_TB";
            Text = "Wall_TB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TB_dataGridView).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private Panel panel1;
        private DataGridView TB_dataGridView;
        private ComboBox TB_Type_comboBox;
        private Label label1;
        private Label label7;
        private TextBox StructureType_textBox;
        private Label label6;
        private TextBox WallType_textBox;
        private Panel panel2;
        private Label label2;
        private TextBox dU_textBox;
        private Label Ueff_label2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private Label label3;
        private TextBox d_ins_textBox;
        private TextBox d_Ins_textBox;
        private Label label8;
        private Label label9;
        private TextBox dy_textBox;
        private Label label5;
        private Label label4;
        private TextBox textBox3;
        private TextBox dx_textBox;
        private TextBox PerArea_label2;
        private TextBox PerArea_textBox;
        private TextBox PerArea_label1;
    }
}