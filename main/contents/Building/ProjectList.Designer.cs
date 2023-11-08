namespace main.contents
{
    partial class ProjectList
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
            ProjectType_textBox = new TextBox();
            label4 = new Label();
            Copy_button = new Button();
            Delete_button = new Button();
            New_button = new Button();
            dataGridView1 = new DataGridView();
            Icon_pictureBox = new PictureBox();
            chk = new DataGridViewCheckBoxColumn();
            num = new DataGridViewTextBoxColumn();
            pnum = new DataGridViewTextBoxColumn();
            pname = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(ProjectType_textBox);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Copy_button);
            GeneralPanel.Controls.Add(Delete_button);
            GeneralPanel.Controls.Add(New_button);
            GeneralPanel.Controls.Add(dataGridView1);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 133;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // ProjectType_textBox
            // 
            ProjectType_textBox.BackColor = Color.White;
            ProjectType_textBox.BorderStyle = BorderStyle.None;
            ProjectType_textBox.Enabled = false;
            ProjectType_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ProjectType_textBox.ForeColor = SystemColors.ControlDark;
            ProjectType_textBox.Location = new Point(240, 109);
            ProjectType_textBox.Name = "ProjectType_textBox";
            ProjectType_textBox.ReadOnly = true;
            ProjectType_textBox.Size = new Size(116, 15);
            ProjectType_textBox.TabIndex = 104;
            ProjectType_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(151, 110);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 103;
            label4.Text = "프로젝트 유형";
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Copy_button.Location = new Point(762, 105);
            Copy_button.Margin = new Padding(0);
            Copy_button.Name = "Copy_button";
            Copy_button.Size = new Size(67, 23);
            Copy_button.TabIndex = 102;
            Copy_button.Text = "Copy";
            Copy_button.UseVisualStyleBackColor = false;
            Copy_button.Click += Copy_button_Click;
            // 
            // Delete_button
            // 
            Delete_button.BackColor = SystemColors.ControlLight;
            Delete_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Delete_button.FlatStyle = FlatStyle.System;
            Delete_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Delete_button.Location = new Point(687, 105);
            Delete_button.Margin = new Padding(0);
            Delete_button.Name = "Delete_button";
            Delete_button.Size = new Size(67, 23);
            Delete_button.TabIndex = 101;
            Delete_button.Text = "Delete";
            Delete_button.UseVisualStyleBackColor = false;
            // 
            // New_button
            // 
            New_button.BackColor = SystemColors.ControlLight;
            New_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            New_button.FlatStyle = FlatStyle.System;
            New_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            New_button.Location = new Point(612, 105);
            New_button.Margin = new Padding(0);
            New_button.Name = "New_button";
            New_button.Size = new Size(67, 23);
            New_button.TabIndex = 100;
            New_button.Text = "New";
            New_button.UseVisualStyleBackColor = false;
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
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { chk, num, pnum, pname, type });
            dataGridView1.Location = new Point(151, 142);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(678, 504);
            dataGridView1.TabIndex = 99;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(362, 86);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 98;
            Icon_pictureBox.TabStop = false;
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
            type.HeaderText = "유형 상세";
            type.Name = "type";
            type.ReadOnly = true;
            type.Resizable = DataGridViewTriState.False;
            type.Width = 96;
            // 
            // ProjectList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProjectList";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel GeneralPanel;
        private Label label4;
        private Button Copy_button;
        private Button Delete_button;
        private Button New_button;
        private DataGridView dataGridView1;
        private PictureBox Icon_pictureBox;
        private TextBox ProjectType_textBox;
        private DataGridViewCheckBoxColumn chk;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn pnum;
        private DataGridViewTextBoxColumn pname;
        private DataGridViewTextBoxColumn type;
    }
}