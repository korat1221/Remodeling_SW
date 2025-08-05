
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
            dUtbFloor_label = new Label();
            dUtbRoof_label = new Label();
            dUtbWall_label = new Label();
            dUtb_label = new Label();
            Save_button = new Button();
            Check_checkBox = new CheckBox();
            TB_comboBox = new CustomComboBox();
            TB_button = new Button();
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
            panel1.Controls.Add(dUtbFloor_label);
            panel1.Controls.Add(dUtbRoof_label);
            panel1.Controls.Add(dUtbWall_label);
            panel1.Controls.Add(dUtb_label);
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
            // dUtbFloor_label
            // 
            dUtbFloor_label.AutoSize = true;
            dUtbFloor_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            dUtbFloor_label.Location = new Point(811, 68);
            dUtbFloor_label.Name = "dUtbFloor_label";
            dUtbFloor_label.Size = new Size(31, 15);
            dUtbFloor_label.TabIndex = 128;
            dUtbFloor_label.Text = "바닥";
            // 
            // dUtbRoof_label
            // 
            dUtbRoof_label.AutoSize = true;
            dUtbRoof_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            dUtbRoof_label.Location = new Point(683, 68);
            dUtbRoof_label.Name = "dUtbRoof_label";
            dUtbRoof_label.Size = new Size(31, 15);
            dUtbRoof_label.TabIndex = 127;
            dUtbRoof_label.Text = "지붕";
            // 
            // dUtbWall_label
            // 
            dUtbWall_label.AutoSize = true;
            dUtbWall_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            dUtbWall_label.Location = new Point(555, 68);
            dUtbWall_label.Name = "dUtbWall_label";
            dUtbWall_label.Size = new Size(31, 15);
            dUtbWall_label.TabIndex = 126;
            dUtbWall_label.Text = "외벽";
            // 
            // dUtb_label
            // 
            dUtb_label.AutoSize = true;
            dUtb_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            dUtb_label.Location = new Point(482, 68);
            dUtb_label.Name = "dUtb_label";
            dUtb_label.Size = new Size(67, 15);
            dUtb_label.TabIndex = 125;
            dUtb_label.Text = "열교가산치";
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
            Check_checkBox.Font = new Font("나눔바른고딕", 9.75F);
            Check_checkBox.Location = new Point(616, 17);
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
            TB_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            TB_comboBox.FormattingEnabled = true;
            TB_comboBox.Location = new Point(663, 12);
            TB_comboBox.Name = "TB_comboBox";
            TB_comboBox.Size = new Size(120, 23);
            TB_comboBox.TabIndex = 122;
            TB_comboBox.Visible = false;
            TB_comboBox.DrawItem += TB_comboBox_DrawItem;
            TB_comboBox.SelectedIndexChanged += TB_comboBox_SelectedIndexChanged;
            // 
            // TB_button
            // 
            TB_button.Location = new Point(811, 12);
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
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
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
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
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
            dataGridView1.Size = new Size(902, 324);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            // 
            // info
            // 
            info.BackColor = SystemColors.ControlLight;
            info.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            info.FlatStyle = FlatStyle.System;
            info.Font = new Font("나눔바른고딕", 9.75F);
            info.Location = new Point(925, 12);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(23, 23);
            info.TabIndex = 129;
            info.Text = "?";
            info.UseVisualStyleBackColor = false;
            info.Click += info_Click;
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
        private Label dUtbRoof_label;
        private Label dUtbWall_label;
        private Label dUtb_label;
        private Label dUtbFloor_label;
        private Button info;
    }
}