namespace main.contents
{
    partial class sub3dBridgeInfo
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
            Save_button = new Button();
            Check_checkBox = new CheckBox();
            TB_comboBox = new CustomComboBox();
            TB_button = new Button();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(Save_button);
            panel1.Controls.Add(Check_checkBox);
            panel1.Controls.Add(TB_comboBox);
            panel1.Controls.Add(TB_button);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Icon_pictureBox);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(971, 730);
            panel1.TabIndex = 0;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(848, 419);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 124;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Check_checkBox
            // 
            Check_checkBox.AutoSize = true;
            Check_checkBox.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            Check_checkBox.Location = new Point(281, 60);
            Check_checkBox.Name = "Check_checkBox";
            Check_checkBox.Size = new Size(15, 14);
            Check_checkBox.TabIndex = 123;
            Check_checkBox.UseVisualStyleBackColor = true;
            Check_checkBox.Visible = false;
            Check_checkBox.CheckedChanged += Check_checkBox_CheckedChanged;
            // 
            // TB_comboBox
            // 
            TB_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            TB_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TB_comboBox.FormattingEnabled = true;
            TB_comboBox.Location = new Point(328, 60);
            TB_comboBox.Name = "TB_comboBox";
            TB_comboBox.Size = new Size(120, 23);
            TB_comboBox.TabIndex = 122;
            TB_comboBox.Visible = false;
            TB_comboBox.SelectedIndexChanged += TB_comboBox_SelectedIndexChanged;
            // 
            // TB_button
            // 
            TB_button.Location = new Point(476, 60);
            TB_button.Name = "TB_button";
            TB_button.Size = new Size(75, 23);
            TB_button.TabIndex = 104;
            TB_button.Text = "입력";
            TB_button.UseVisualStyleBackColor = true;
            TB_button.Visible = false;
            TB_button.Click += TB_button_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(95, 51);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 99;
            label4.Text = "열교정보";
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(34, 89);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(902, 324);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            // 
            // sub3dBridgeInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 641);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "sub3dBridgeInfo";
            Text = "sub3dBridgeInfo";
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
        private Button TB_button;
        private CustomComboBox TB_comboBox;
        private CheckBox Check_checkBox;
        private Button Save_button;
    }
}