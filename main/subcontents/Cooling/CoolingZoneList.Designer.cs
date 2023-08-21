namespace main.subcontents
{
    partial class CoolingZoneList
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            button1 = new Button();
            panel1 = new Panel();
            CoolingZone_dataGridView = new DataGridView();
            CoolingZoneList_panel = new DataGridView();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingZone_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CoolingZoneList_panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(CoolingZone_dataGridView);
            panel1.Controls.Add(CoolingZoneList_panel);
            panel1.Location = new Point(24, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(811, 452);
            panel1.TabIndex = 2;
            // 
            // CoolingZone_dataGridView
            // 
            CoolingZone_dataGridView.AllowUserToAddRows = false;
            CoolingZone_dataGridView.AllowUserToDeleteRows = false;
            CoolingZone_dataGridView.AllowUserToResizeColumns = false;
            CoolingZone_dataGridView.AllowUserToResizeRows = false;        
            CoolingZone_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolingZone_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolingZone_dataGridView.BackgroundColor = Color.White;
            CoolingZone_dataGridView.BorderStyle = BorderStyle.None;
            CoolingZone_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolingZone_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            CoolingZone_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            CoolingZone_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingZone_dataGridView.Dock = DockStyle.Fill;
            CoolingZone_dataGridView.Location = new Point(0, 0);
            CoolingZone_dataGridView.Name = "CoolingZone_dataGridView";
            CoolingZone_dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            CoolingZone_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            CoolingZone_dataGridView.RowHeadersVisible = false;
            CoolingZone_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            CoolingZone_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            CoolingZone_dataGridView.RowTemplate.Height = 25;
            CoolingZone_dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CoolingZone_dataGridView.Size = new Size(811, 452);
            CoolingZone_dataGridView.TabIndex = 23;
            CoolingZone_dataGridView.CellFormatting += CoolingZoneList_dataGridView_CellFormatting;
            // 
            // CoolingZoneList_panel
            // 
            CoolingZoneList_panel.AllowUserToAddRows = false;
            CoolingZoneList_panel.AllowUserToDeleteRows = false;
            CoolingZoneList_panel.AllowUserToResizeColumns = false;
            CoolingZoneList_panel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolingZoneList_panel.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolingZoneList_panel.BackgroundColor = SystemColors.ActiveCaption;
            CoolingZoneList_panel.BorderStyle = BorderStyle.None;
            CoolingZoneList_panel.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolingZoneList_panel.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            CoolingZoneList_panel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            CoolingZoneList_panel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingZoneList_panel.Dock = DockStyle.Fill;
            CoolingZoneList_panel.Location = new Point(0, 0);
            CoolingZoneList_panel.Name = "CoolingZoneList_panel";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            CoolingZoneList_panel.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            CoolingZoneList_panel.RowHeadersVisible = false;
            CoolingZoneList_panel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            CoolingZoneList_panel.RowsDefaultCellStyle = dataGridViewCellStyle7;
            CoolingZoneList_panel.RowTemplate.Height = 25;
            CoolingZoneList_panel.Size = new Size(811, 452);
            CoolingZoneList_panel.TabIndex = 100;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(27, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(52, 50);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // CoolingZoneList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 559);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CoolingZoneList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZoneCooling";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CoolingZone_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)CoolingZoneList_panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Panel panel1;
        private DataGridView CoolingZoneList_panel;
        private PictureBox pictureBox1;
        private DataGridView CoolingZone_dataGridView;
    }
}