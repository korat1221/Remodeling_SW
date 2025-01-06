namespace main.subcontents
{
    partial class Window_ImportSize
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
            Size_dataGridView = new DataGridView();
            Save_button = new Button();
            CSVImport_button = new Button();
            label11 = new Label();
            label9 = new Label();
            width_textBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            height_textBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            n_ver_textBox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            n_hori_textBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            percent_open_textBox = new TextBox();
            label10 = new Label();
            label12 = new Label();
            Calc_button = new Button();
            label13 = new Label();
            Name_textBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Size_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Size_dataGridView
            // 
            Size_dataGridView.AllowUserToAddRows = false;
            Size_dataGridView.AllowUserToDeleteRows = false;
            Size_dataGridView.AllowUserToResizeColumns = false;
            Size_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Size_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Size_dataGridView.BackgroundColor = Color.White;
            Size_dataGridView.BorderStyle = BorderStyle.None;
            Size_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Size_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Size_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Size_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Size_dataGridView.Location = new Point(3, 110);
            Size_dataGridView.Name = "Size_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Size_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Size_dataGridView.RowHeadersVisible = false;
            Size_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Size_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Size_dataGridView.RowTemplate.Height = 25;
            Size_dataGridView.Size = new Size(1144, 303);
            Size_dataGridView.TabIndex = 19;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1000, 422);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // CSVImport_button
            // 
            CSVImport_button.BackColor = SystemColors.ControlLight;
            CSVImport_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            CSVImport_button.FlatStyle = FlatStyle.System;
            CSVImport_button.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            CSVImport_button.Location = new Point(212, 13);
            CSVImport_button.Margin = new Padding(0);
            CSVImport_button.Name = "CSVImport_button";
            CSVImport_button.Size = new Size(80, 23);
            CSVImport_button.TabIndex = 90;
            CSVImport_button.Text = "Import";
            CSVImport_button.UseVisualStyleBackColor = false;
            CSVImport_button.Click += CSVImport_button_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(736, 45);
            label11.Name = "label11";
            label11.Size = new Size(18, 15);
            label11.TabIndex = 98;
            label11.Text = "m";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(547, 45);
            label9.Name = "label9";
            label9.Size = new Size(59, 15);
            label9.TabIndex = 96;
            label9.Text = "창호 너비";
            // 
            // width_textBox
            // 
            width_textBox.BorderStyle = BorderStyle.FixedSingle;
            width_textBox.Location = new Point(612, 41);
            width_textBox.Name = "width_textBox";
            width_textBox.Size = new Size(120, 23);
            width_textBox.TabIndex = 97;
            width_textBox.TextAlign = HorizontalAlignment.Center;
            width_textBox.TextChanged += width_textBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(958, 49);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 103;
            label1.Text = "m";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(766, 45);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 101;
            label2.Text = "창호 높이";
            // 
            // height_textBox
            // 
            height_textBox.BorderStyle = BorderStyle.FixedSingle;
            height_textBox.Location = new Point(832, 41);
            height_textBox.Name = "height_textBox";
            height_textBox.Size = new Size(120, 23);
            height_textBox.TabIndex = 102;
            height_textBox.TextAlign = HorizontalAlignment.Center;
            height_textBox.TextChanged += height_textBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(958, 82);
            label3.Name = "label3";
            label3.Size = new Size(21, 15);
            label3.TabIndex = 109;
            label3.Text = "EA";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(766, 82);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 107;
            label4.Text = "세로 칸 수";
            // 
            // n_ver_textBox
            // 
            n_ver_textBox.BorderStyle = BorderStyle.FixedSingle;
            n_ver_textBox.Location = new Point(832, 78);
            n_ver_textBox.Name = "n_ver_textBox";
            n_ver_textBox.Size = new Size(120, 23);
            n_ver_textBox.TabIndex = 108;
            n_ver_textBox.TextAlign = HorizontalAlignment.Center;
            n_ver_textBox.TextChanged += n_ver_textBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(736, 82);
            label5.Name = "label5";
            label5.Size = new Size(21, 15);
            label5.TabIndex = 106;
            label5.Text = "EA";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(547, 82);
            label6.Name = "label6";
            label6.Size = new Size(63, 15);
            label6.TabIndex = 104;
            label6.Text = "가로 칸 수";
            // 
            // n_hori_textBox
            // 
            n_hori_textBox.BorderStyle = BorderStyle.FixedSingle;
            n_hori_textBox.Location = new Point(612, 78);
            n_hori_textBox.Name = "n_hori_textBox";
            n_hori_textBox.Size = new Size(120, 23);
            n_hori_textBox.TabIndex = 105;
            n_hori_textBox.TextAlign = HorizontalAlignment.Center;
            n_hori_textBox.TextChanged += n_hori_textBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(518, 82);
            label7.Name = "label7";
            label7.Size = new Size(17, 15);
            label7.TabIndex = 112;
            label7.Text = "%";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(318, 82);
            label8.Name = "label8";
            label8.Size = new Size(71, 15);
            label8.TabIndex = 110;
            label8.Text = "개폐창 비율";
            // 
            // percent_open_textBox
            // 
            percent_open_textBox.BorderStyle = BorderStyle.FixedSingle;
            percent_open_textBox.Location = new Point(392, 78);
            percent_open_textBox.Name = "percent_open_textBox";
            percent_open_textBox.Size = new Size(120, 23);
            percent_open_textBox.TabIndex = 111;
            percent_open_textBox.TextAlign = HorizontalAlignment.Center;
            percent_open_textBox.TextChanged += percent_open_textBox_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(93, 49);
            label10.Name = "label10";
            label10.Size = new Size(99, 15);
            label10.TabIndex = 113;
            label10.Text = "계산을 통한 입력";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(93, 17);
            label12.Name = "label12";
            label12.Size = new Size(98, 15);
            label12.TabIndex = 114;
            label12.Text = "CSV를 통한 입력";
            // 
            // Calc_button
            // 
            Calc_button.BackColor = SystemColors.ControlLight;
            Calc_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Calc_button.FlatStyle = FlatStyle.System;
            Calc_button.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Calc_button.Location = new Point(212, 45);
            Calc_button.Margin = new Padding(0);
            Calc_button.Name = "Calc_button";
            Calc_button.Size = new Size(80, 23);
            Calc_button.TabIndex = 115;
            Calc_button.Text = "Calc";
            Calc_button.UseVisualStyleBackColor = false;
            Calc_button.Click += Calc_button_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(318, 45);
            label13.Name = "label13";
            label13.Size = new Size(59, 15);
            label13.TabIndex = 117;
            label13.Text = "창호 명칭";
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(392, 41);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 118;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // Window_ImportSize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1147, 459);
            Controls.Add(label13);
            Controls.Add(Name_textBox);
            Controls.Add(Calc_button);
            Controls.Add(label12);
            Controls.Add(label10);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(percent_open_textBox);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(n_ver_textBox);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(n_hori_textBox);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(height_textBox);
            Controls.Add(label11);
            Controls.Add(label9);
            Controls.Add(width_textBox);
            Controls.Add(CSVImport_button);
            Controls.Add(Save_button);
            Controls.Add(Size_dataGridView);
            Name = "Window_ImportSize";
            Text = "Window_ImportSize";
            ((System.ComponentModel.ISupportInitialize)Size_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView Size_dataGridView;
        private Button Save_button;
        private Button CSVImport_button;
        private Label label11;
        private Label label9;
        private TextBox width_textBox;
        private Label label1;
        private Label label2;
        private TextBox height_textBox;
        private Label label3;
        private Label label4;
        private TextBox n_ver_textBox;
        private Label label5;
        private Label label6;
        private TextBox n_hori_textBox;
        private Label label7;
        private Label label8;
        private TextBox percent_open_textBox;
        private Label label10;
        private Label label12;
        private Button Calc_button;
        private Label label13;
        private TextBox Name_textBox;
    }
}