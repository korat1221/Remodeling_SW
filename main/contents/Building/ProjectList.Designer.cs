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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            ProjectType_label = new Label();
            PreCopy_button = new Button();
            Save_button = new Button();
            Copy_button = new Button();
            Delete_button = new Button();
            New_button = new Button();
            dataGridView1 = new DataGridView();
            chk = new DataGridViewCheckBoxColumn();
            num = new DataGridViewTextBoxColumn();
            pnum = new DataGridViewTextBoxColumn();
            pname = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            CreateDate = new DataGridViewTextBoxColumn();
            Icon_pictureBox = new PictureBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(ProjectType_label);
            GeneralPanel.Controls.Add(PreCopy_button);
            GeneralPanel.Controls.Add(Save_button);
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
            // ProjectType_label
            // 
            ProjectType_label.AutoSize = true;
            ProjectType_label.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            ProjectType_label.Location = new Point(207, 106);
            ProjectType_label.Name = "ProjectType_label";
            ProjectType_label.Size = new Size(57, 19);
            ProjectType_label.TabIndex = 108;
            ProjectType_label.Text = "기존 건물";
            ProjectType_label.UseCompatibleTextRendering = true;
            // 
            // PreCopy_button
            // 
            PreCopy_button.BackColor = SystemColors.ControlLight;
            PreCopy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            PreCopy_button.FlatStyle = FlatStyle.System;
            PreCopy_button.Font = new Font("나눔고딕", 9.75F);
            PreCopy_button.Location = new Point(797, 105);
            PreCopy_button.Margin = new Padding(0);
            PreCopy_button.Name = "PreCopy_button";
            PreCopy_button.Size = new Size(97, 23);
            PreCopy_button.TabIndex = 106;
            PreCopy_button.Text = "기존건물 Copy";
            PreCopy_button.UseCompatibleTextRendering = true;
            PreCopy_button.UseVisualStyleBackColor = false;
            PreCopy_button.Visible = false;
            PreCopy_button.Click += PreCopy_button_Click;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ControlLight;
            Save_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Save_button.FlatStyle = FlatStyle.System;
            Save_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Save_button.Location = new Point(803, 500);
            Save_button.Margin = new Padding(0);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(67, 23);
            Save_button.TabIndex = 105;
            Save_button.Text = "Save";
            Save_button.UseVisualStyleBackColor = false;
            Save_button.Click += Save_button_Click;
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new Font("나눔고딕", 9.75F);
            Copy_button.Location = new Point(730, 105);
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
            Delete_button.Font = new Font("나눔고딕", 9.75F);
            Delete_button.Location = new Point(663, 105);
            Delete_button.Margin = new Padding(0);
            Delete_button.Name = "Delete_button";
            Delete_button.Size = new Size(67, 23);
            Delete_button.TabIndex = 101;
            Delete_button.Text = "Delete";
            Delete_button.UseVisualStyleBackColor = false;
            Delete_button.Click += Delete_button_Click;
            // 
            // New_button
            // 
            New_button.BackColor = SystemColors.ControlLight;
            New_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            New_button.FlatStyle = FlatStyle.System;
            New_button.Font = new Font("나눔고딕", 9.75F);
            New_button.Location = new Point(596, 105);
            New_button.Margin = new Padding(0);
            New_button.Name = "New_button";
            New_button.Size = new Size(67, 23);
            New_button.TabIndex = 100;
            New_button.Text = "New";
            New_button.UseVisualStyleBackColor = false;
            New_button.Click += New_button_Click;
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { chk, num, pnum, pname, type, CreateDate });
            dataGridView1.Location = new Point(151, 142);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Size = new Size(678, 355);
            dataGridView1.TabIndex = 99;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
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
            // CreateDate
            // 
            CreateDate.HeaderText = "생성 날짜";
            CreateDate.Name = "CreateDate";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(151, 78);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 98;
            Icon_pictureBox.TabStop = false;
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
        private Button Copy_button;
        private Button Delete_button;
        private Button New_button;
        private DataGridView dataGridView1;
        private PictureBox Icon_pictureBox;
        private Button Save_button;
        private Button PreCopy_button;
        private Label ProjectType_label;
        private DataGridViewCheckBoxColumn chk;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn pnum;
        private DataGridViewTextBoxColumn pname;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn CreateDate;
    }
}