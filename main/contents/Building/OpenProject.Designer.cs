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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            Open_button = new Button();
            dataGridView1 = new DataGridView();
            chk = new DataGridViewCheckBoxColumn();
            num = new DataGridViewTextBoxColumn();
            pnum = new DataGridViewTextBoxColumn();
            pname = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Open_button);
            GeneralPanel.Controls.Add(dataGridView1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 133;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Open_button
            // 
            Open_button.BackColor = SystemColors.ControlLight;
            Open_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Open_button.FlatStyle = FlatStyle.System;
            Open_button.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Open_button.Location = new Point(762, 104);
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel GeneralPanel;
        private Button Open_button;
        private DataGridView dataGridView1;
        private DataGridViewCheckBoxColumn chk;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn pnum;
        private DataGridViewTextBoxColumn pname;
        private DataGridViewTextBoxColumn type;
    }
}