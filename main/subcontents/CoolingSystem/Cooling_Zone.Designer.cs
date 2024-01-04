namespace main.subcontents
{
    partial class Cooling_Zone
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            Save_Button = new Button();
            panel1 = new Panel();
            CoolingZone_dataGridView = new DataGridView();
            CoolingZoneList_panel = new DataGridView();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ZoneCount = new Label();
            delete = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingZone_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CoolingZoneList_panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Save_Button
            // 
            Save_Button.Location = new Point(679, 51);
            Save_Button.Name = "Save_Button";
            Save_Button.Size = new Size(75, 23);
            Save_Button.TabIndex = 1;
            Save_Button.Text = "저장";
            Save_Button.UseVisualStyleBackColor = true;
            Save_Button.Click += Save_Button_Click;
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
            CoolingZone_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolingZone_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolingZone_dataGridView.BackgroundColor = SystemColors.Control;
            CoolingZone_dataGridView.BorderStyle = BorderStyle.None;
            CoolingZone_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolingZone_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            CoolingZone_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            CoolingZone_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingZone_dataGridView.Dock = DockStyle.Fill;
            CoolingZone_dataGridView.Location = new Point(0, 0);
            CoolingZone_dataGridView.Name = "CoolingZone_dataGridView";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            CoolingZone_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            CoolingZone_dataGridView.RowHeadersVisible = false;
            CoolingZone_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            CoolingZone_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            CoolingZone_dataGridView.RowTemplate.Height = 25;
            CoolingZone_dataGridView.Size = new Size(811, 452);
            CoolingZone_dataGridView.TabIndex = 20;
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
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            CoolingZoneList_panel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            CoolingZoneList_panel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingZoneList_panel.Dock = DockStyle.Fill;
            CoolingZoneList_panel.Location = new Point(0, 0);
            CoolingZoneList_panel.Name = "CoolingZoneList_panel";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = SystemColors.Control;
            dataGridViewCellStyle11.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            CoolingZoneList_panel.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            CoolingZoneList_panel.RowHeadersVisible = false;
            CoolingZoneList_panel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle12.ForeColor = Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            CoolingZoneList_panel.RowsDefaultCellStyle = dataGridViewCellStyle12;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(85, 41);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 4;
            label1.Text = "총 존개수:";
            // 
            // ZoneCount
            // 
            ZoneCount.AutoSize = true;
            ZoneCount.Location = new Point(153, 41);
            ZoneCount.Name = "ZoneCount";
            ZoneCount.Size = new Size(0, 15);
            ZoneCount.TabIndex = 5;
            // 
            // delete
            // 
            delete.Location = new Point(760, 51);
            delete.Name = "delete";
            delete.Size = new Size(75, 23);
            delete.TabIndex = 6;
            delete.Text = "삭제";
            delete.UseVisualStyleBackColor = true;
            delete.Click += delete_Click;
            // 
            // Cooling_Zone
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 559);
            Controls.Add(delete);
            Controls.Add(ZoneCount);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(Save_Button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Cooling_Zone";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CoolingZone 선택";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CoolingZone_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)CoolingZoneList_panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Save_Button;
        private Panel panel1;
        private DataGridView CoolingZoneList_panel;
        private PictureBox pictureBox1;
        private DataGridView CoolingZone_dataGridView;
        private DataGridView Zone_dataGridView;
        private Label label1;
        private Label ZoneCount;
        private Button delete;
    }
}