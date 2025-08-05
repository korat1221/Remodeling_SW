
namespace main.contents
{
    partial class TB_List
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
            Uw2_unit_label = new Label();
            length_textBox = new TextBox();
            label1 = new Label();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            dataGridView1 = new DataGridView();
            info = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(info);
            panel1.Controls.Add(Uw2_unit_label);
            panel1.Controls.Add(length_textBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Icon_pictureBox);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(971, 730);
            panel1.TabIndex = 0;
            // 
            // Uw2_unit_label
            // 
            Uw2_unit_label.AutoSize = true;
            Uw2_unit_label.Font = new Font("나눔바른고딕", 9.75F);
            Uw2_unit_label.ForeColor = SystemColors.ControlDark;
            Uw2_unit_label.Location = new Point(224, 89);
            Uw2_unit_label.Name = "Uw2_unit_label";
            Uw2_unit_label.Size = new Size(19, 15);
            Uw2_unit_label.TabIndex = 101;
            Uw2_unit_label.Text = "m";
            // 
            // length_textBox
            // 
            length_textBox.BackColor = Color.White;
            length_textBox.BorderStyle = BorderStyle.None;
            length_textBox.Enabled = false;
            length_textBox.Font = new Font("나눔바른고딕", 9.75F);
            length_textBox.ForeColor = SystemColors.ControlDark;
            length_textBox.Location = new Point(148, 89);
            length_textBox.Name = "length_textBox";
            length_textBox.Size = new Size(70, 15);
            length_textBox.TabIndex = 102;
            length_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label1.Location = new Point(95, 89);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 100;
            label1.Text = "총 길이";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(95, 51);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 99;
            label4.Text = "RTB1.";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(34, 33);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 98;
            Icon_pictureBox.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(34, 112);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(902, 323);
            dataGridView1.TabIndex = 1;
            // 
            // info
            // 
            info.BackColor = SystemColors.ControlLight;
            info.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            info.FlatStyle = FlatStyle.System;
            info.Font = new Font("나눔바른고딕", 9.75F);
            info.Location = new Point(940, 9);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(23, 23);
            info.TabIndex = 114;
            info.Text = "?";
            info.UseVisualStyleBackColor = false;
            info.Click += info_Click;
            // 
            // TB_List
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 641);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TB_List";
            Text = "sub3dBridgeInfo_sub";
            VisibleChanged += onVisibleChanged;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label67;
        private Label label68;
        private TextBox textBox23;
        private Label label72;
        private DataGridView dataGridView1;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Button button2;
        private Label label1;
        private Label Uw2_unit_label;
        private TextBox length_textBox;
        private Button info;
    }
}