namespace main.contents
{
    partial class OpenProject
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
            File_button = new Button();
            info = new Button();
            ProjectType_label = new Label();
            Open_button = new Button();
            dataGridView1 = new DataGridView();
            chk = new DataGridViewCheckBoxColumn();
            num = new DataGridViewTextBoxColumn();
            pnum = new DataGridViewTextBoxColumn();
            pname = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            CreateDate = new DataGridViewTextBoxColumn();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(File_button);
            GeneralPanel.Controls.Add(info);
            GeneralPanel.Controls.Add(ProjectType_label);
            GeneralPanel.Controls.Add(Open_button);
            GeneralPanel.Controls.Add(dataGridView1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 133;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // File_button
            // 
            File_button.BackColor = SystemColors.ControlLight;
            File_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            File_button.FlatStyle = FlatStyle.System;
            File_button.Font = new Font("나눔바른고딕", 9.75F);
            File_button.Location = new Point(751, 105);
            File_button.Margin = new Padding(0);
            File_button.Name = "File_button";
            File_button.Size = new Size(78, 23);
            File_button.TabIndex = 150;
            File_button.Text = "찾아보기";
            File_button.UseVisualStyleBackColor = false;
            File_button.Click += File_button_Click;
            // 
            // info
            // 
            info.BackColor = SystemColors.ControlLight;
            info.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            info.FlatStyle = FlatStyle.System;
            info.Font = new Font("나눔바른고딕", 9.75F);
            info.Location = new Point(940, 12);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(23, 23);
            info.TabIndex = 149;
            info.Text = "?";
            info.UseVisualStyleBackColor = false;
            info.Click += info_Click;
            // 
            // ProjectType_label
            // 
            ProjectType_label.AutoSize = true;
            ProjectType_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            ProjectType_label.Location = new Point(151, 106);
            ProjectType_label.Name = "ProjectType_label";
            ProjectType_label.Size = new Size(80, 20);
            ProjectType_label.TabIndex = 109;
            ProjectType_label.Text = "프로젝트 열기";
            ProjectType_label.UseCompatibleTextRendering = true;
            // 
            // Open_button
            // 
            Open_button.BackColor = SystemColors.ControlLight;
            Open_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Open_button.FlatStyle = FlatStyle.System;
            Open_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            Open_button.Location = new Point(762, 519);
            Open_button.Margin = new Padding(0);
            Open_button.Name = "Open_button";
            Open_button.Size = new Size(67, 23);
            Open_button.TabIndex = 100;
            Open_button.Text = "Open";
            Open_button.UseVisualStyleBackColor = false;
            Open_button.Click += Open_button_Click;
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
            dataGridViewCellStyle4.Font = new Font("나눔바른고딕", 9.75F);
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
            dataGridViewCellStyle5.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Size = new Size(678, 355);
            dataGridView1.TabIndex = 99;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
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
            pname.ReadOnly = true;
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
            CreateDate.ReadOnly = true;
            // 
            // OpenProject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OpenProject";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel GeneralPanel;
        private Button Open_button;
        private DataGridView dataGridView1;
        private Label ProjectType_label;
        private DataGridViewCheckBoxColumn chk;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn pnum;
        private DataGridViewTextBoxColumn pname;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn CreateDate;
        private Button info;
        private Button File_button;
    }
}