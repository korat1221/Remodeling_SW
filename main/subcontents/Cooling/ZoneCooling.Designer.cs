namespace main.subcontents
{
    partial class ZoneCooling
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
            button1 = new Button();
            DatagridviewName = new Label();
            panel1 = new Panel();
            ZoneCoolingInfo = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ZoneCoolingInfo).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(760, 51);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "선택";
            button1.UseVisualStyleBackColor = true;
            // 
            // DatagridviewName
            // 
            DatagridviewName.AutoSize = true;
            DatagridviewName.Location = new Point(24, 55);
            DatagridviewName.Name = "DatagridviewName";
            DatagridviewName.Size = new Size(39, 15);
            DatagridviewName.TabIndex = 3;
            DatagridviewName.Text = "label1";
            // 
            // panel1
            // 
            panel1.Controls.Add(ZoneCoolingInfo);
            panel1.Location = new Point(24, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(811, 452);
            panel1.TabIndex = 2;
            // 
            // ZoneCoolingInfo
            // 
            ZoneCoolingInfo.AllowUserToAddRows = false;
            ZoneCoolingInfo.AllowUserToDeleteRows = false;
            ZoneCoolingInfo.AllowUserToResizeColumns = false;
            ZoneCoolingInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ZoneCoolingInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ZoneCoolingInfo.BackgroundColor = SystemColors.ActiveCaption;
            ZoneCoolingInfo.BorderStyle = BorderStyle.None;
            ZoneCoolingInfo.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ZoneCoolingInfo.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            ZoneCoolingInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            ZoneCoolingInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ZoneCoolingInfo.Dock = DockStyle.Fill;
            ZoneCoolingInfo.Location = new Point(0, 0);
            ZoneCoolingInfo.Name = "ZoneCoolingInfo";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            ZoneCoolingInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            ZoneCoolingInfo.RowHeadersVisible = false;
            ZoneCoolingInfo.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            ZoneCoolingInfo.RowsDefaultCellStyle = dataGridViewCellStyle3;
            ZoneCoolingInfo.RowTemplate.Height = 25;
            ZoneCoolingInfo.Size = new Size(811, 452);
            ZoneCoolingInfo.TabIndex = 100;
            // 
            // ZoneCooling
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 559);
            Controls.Add(DatagridviewName);
            Controls.Add(panel1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneCooling";
            Text = "ZoneCooling";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ZoneCoolingInfo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Label DatagridviewName;
        private Panel panel1;
        private DataGridView ZoneCoolingInfo;
    }
}