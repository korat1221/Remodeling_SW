namespace main.subcontents.ConstructionRoof
{
    partial class Roof_TB
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label7 = new Label();
            TB_Type_comboBox = new ComboBox();
            StructureType_textBox = new TextBox();
            label6 = new Label();
            RoofType_textBox = new TextBox();
            label1 = new Label();
            Save_button = new Button();
            panel1 = new Panel();
            TBName_textBox = new TextBox();
            dU_textBox = new TextBox();
            Ueff_label2 = new Label();
            label2 = new Label();
            PerArea_label2 = new TextBox();
            PerArea_textBox = new TextBox();
            PerArea_label1 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            dy_textBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            dx_textBox = new TextBox();
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
            GeneralPanel.Controls.Add(label7);
            GeneralPanel.Controls.Add(TB_Type_comboBox);
            GeneralPanel.Controls.Add(StructureType_textBox);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(RoofType_textBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 47);
            GeneralPanel.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(526, 16);
            label7.Name = "label7";
            label7.Size = new Size(91, 15);
            label7.TabIndex = 101;
            label7.Text = "외장재고정방법";
            // 
            // TB_Type_comboBox
            // 
            TB_Type_comboBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            TB_Type_comboBox.FormattingEnabled = true;
            TB_Type_comboBox.Location = new Point(642, 11);
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
            StructureType_textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            StructureType_textBox.ForeColor = SystemColors.ControlDark;
            StructureType_textBox.Location = new Point(381, 15);
            StructureType_textBox.Name = "StructureType_textBox";
            StructureType_textBox.Size = new Size(120, 15);
            StructureType_textBox.TabIndex = 100;
            StructureType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(301, 15);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 99;
            label6.Text = "구조유형";
            // 
            // RoofType_textBox
            // 
            RoofType_textBox.BackColor = Color.AliceBlue;
            RoofType_textBox.BorderStyle = BorderStyle.None;
            RoofType_textBox.Enabled = false;
            RoofType_textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            RoofType_textBox.ForeColor = SystemColors.ControlDark;
            RoofType_textBox.Location = new Point(124, 15);
            RoofType_textBox.Name = "RoofType_textBox";
            RoofType_textBox.Size = new Size(120, 15);
            RoofType_textBox.TabIndex = 98;
            RoofType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(24, 15);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 97;
            label1.Text = "리모델링 유형";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(627, 442);
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
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(TBName_textBox);
            panel1.Controls.Add(dU_textBox);
            panel1.Controls.Add(Ueff_label2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(0, 264);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 31);
            panel1.TabIndex = 27;
            // 
            // TBName_textBox
            // 
            TBName_textBox.BackColor = SystemColors.GradientInactiveCaption;
            TBName_textBox.BorderStyle = BorderStyle.None;
            TBName_textBox.Enabled = false;
            TBName_textBox.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            TBName_textBox.ForeColor = SystemColors.ControlDark;
            TBName_textBox.Location = new Point(309, 7);
            TBName_textBox.Name = "TBName_textBox";
            TBName_textBox.ShortcutsEnabled = false;
            TBName_textBox.Size = new Size(120, 16);
            TBName_textBox.TabIndex = 135;
            TBName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // dU_textBox
            // 
            dU_textBox.BackColor = SystemColors.GradientInactiveCaption;
            dU_textBox.BorderStyle = BorderStyle.None;
            dU_textBox.Enabled = false;
            dU_textBox.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dU_textBox.ForeColor = Color.Black;
            dU_textBox.Location = new Point(458, 7);
            dU_textBox.Name = "dU_textBox";
            dU_textBox.Size = new Size(75, 16);
            dU_textBox.TabIndex = 134;
            dU_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Ueff_label2
            // 
            Ueff_label2.AutoSize = true;
            Ueff_label2.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Ueff_label2.ForeColor = SystemColors.ControlDark;
            Ueff_label2.Location = new Point(549, 8);
            Ueff_label2.Name = "Ueff_label2";
            Ueff_label2.Size = new Size(51, 15);
            Ueff_label2.TabIndex = 133;
            Ueff_label2.Text = "W/m²·K";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(193, 8);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 100;
            label2.Text = "1D 열교가산치";
            // 
            // PerArea_label2
            // 
            PerArea_label2.BackColor = Color.White;
            PerArea_label2.BorderStyle = BorderStyle.None;
            PerArea_label2.Enabled = false;
            PerArea_label2.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            PerArea_label2.ForeColor = SystemColors.ControlDark;
            PerArea_label2.Location = new Point(250, 180);
            PerArea_label2.Name = "PerArea_label2";
            PerArea_label2.Size = new Size(68, 15);
            PerArea_label2.TabIndex = 145;
            PerArea_label2.TextAlign = HorizontalAlignment.Center;
            // 
            // PerArea_textBox
            // 
            PerArea_textBox.BackColor = Color.White;
            PerArea_textBox.BorderStyle = BorderStyle.None;
            PerArea_textBox.Enabled = false;
            PerArea_textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            PerArea_textBox.ForeColor = SystemColors.ControlDark;
            PerArea_textBox.Location = new Point(124, 180);
            PerArea_textBox.Name = "PerArea_textBox";
            PerArea_textBox.Size = new Size(120, 15);
            PerArea_textBox.TabIndex = 144;
            PerArea_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PerArea_label1
            // 
            PerArea_label1.BackColor = Color.White;
            PerArea_label1.BorderStyle = BorderStyle.None;
            PerArea_label1.Enabled = false;
            PerArea_label1.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            PerArea_label1.ForeColor = SystemColors.ControlDark;
            PerArea_label1.Location = new Point(24, 179);
            PerArea_label1.Name = "PerArea_label1";
            PerArea_label1.Size = new Size(91, 16);
            PerArea_label1.TabIndex = 143;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(250, 62);
            label8.Name = "label8";
            label8.Size = new Size(18, 16);
            label8.TabIndex = 142;
            label8.Text = "m";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(24, 63);
            label9.Name = "label9";
            label9.Size = new Size(69, 15);
            label9.TabIndex = 141;
            label9.Text = "수평간격[y]";
            // 
            // dy_textBox
            // 
            dy_textBox.BackColor = Color.White;
            dy_textBox.BorderStyle = BorderStyle.FixedSingle;
            dy_textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dy_textBox.ForeColor = SystemColors.ControlText;
            dy_textBox.Location = new Point(124, 60);
            dy_textBox.Name = "dy_textBox";
            dy_textBox.Size = new Size(120, 22);
            dy_textBox.TabIndex = 140;
            dy_textBox.TextAlign = HorizontalAlignment.Center;
            dy_textBox.TextChanged += dy_textBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(250, 35);
            label5.Name = "label5";
            label5.Size = new Size(18, 16);
            label5.TabIndex = 139;
            label5.Text = "m";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(24, 36);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 138;
            label4.Text = "수직간격[x]";
            // 
            // dx_textBox
            // 
            dx_textBox.BackColor = Color.White;
            dx_textBox.BorderStyle = BorderStyle.FixedSingle;
            dx_textBox.Font = new System.Drawing.Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dx_textBox.ForeColor = SystemColors.ControlText;
            dx_textBox.Location = new Point(124, 32);
            dx_textBox.Name = "dx_textBox";
            dx_textBox.Size = new Size(120, 22);
            dx_textBox.TabIndex = 135;
            dx_textBox.TextAlign = HorizontalAlignment.Center;
            dx_textBox.TextChanged += dx_textBox_TextChanged;
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            TB_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            TB_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TB_dataGridView.Location = new Point(0, 295);
            TB_dataGridView.Name = "TB_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            TB_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            TB_dataGridView.RowHeadersVisible = false;
            TB_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            TB_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            TB_dataGridView.RowTemplate.Height = 25;
            TB_dataGridView.Size = new Size(800, 134);
            TB_dataGridView.TabIndex = 19;
            TB_dataGridView.CellContentClick += TB_dataGridView_CellContentClick;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(PerArea_label2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(PerArea_textBox);
            panel2.Controls.Add(PerArea_label1);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(dx_textBox);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(dy_textBox);
            panel2.Location = new Point(0, 44);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 220);
            panel2.TabIndex = 28;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(549, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(202, 215);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(324, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(202, 215);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Roof_TB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(797, 479);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(TB_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Roof_TB";
            Text = "Roof_TB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TB_dataGridView).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
        private TextBox RoofType_textBox;
        private Panel panel2;
        private Label label2;
        private TextBox dU_textBox;
        private Label Ueff_label2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label8;
        private Label label9;
        private TextBox dy_textBox;
        private Label label5;
        private Label label4;
        private TextBox dx_textBox;
        private TextBox PerArea_label2;
        private TextBox PerArea_textBox;
        private TextBox PerArea_label1;
        private TextBox TBName_textBox;
    }
}