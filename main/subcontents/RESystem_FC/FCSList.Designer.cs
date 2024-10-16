namespace main.subcontents.RESystem_FC
{
    partial class FCSList
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
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            Icon_pictureBox = new PictureBox();
            titleText = new Label();
            HW_dataGridView = new DataGridView();
            Save_button = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HW_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = SystemColors.InactiveBorder;
            GeneralPanel.BackgroundImageLayout = ImageLayout.None;
            GeneralPanel.Controls.Add(Save_button);
            GeneralPanel.Controls.Add(HW_dataGridView);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(titleText);
            GeneralPanel.Dock = DockStyle.Fill;
            GeneralPanel.Location = new Point(0, 0);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(384, 461);
            GeneralPanel.TabIndex = 18;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(11, 8);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 138;
            Icon_pictureBox.TabStop = false;
            // 
            // titleText
            // 
            titleText.AutoSize = true;
            titleText.Font = new Font("맑은 고딕", 10F);
            titleText.Location = new Point(67, 25);
            titleText.Name = "titleText";
            titleText.Size = new Size(37, 19);
            titleText.TabIndex = 137;
            titleText.Text = "명칭";
            // 
            // HW_dataGridView
            // 
            HW_dataGridView.AllowUserToAddRows = false;
            HW_dataGridView.AllowUserToDeleteRows = false;
            HW_dataGridView.AllowUserToResizeColumns = false;
            HW_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HW_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            HW_dataGridView.BackgroundColor = Color.White;
            HW_dataGridView.BorderStyle = BorderStyle.None;
            HW_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            HW_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.Font = new Font("맑은 고딕", 8.25F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            HW_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            HW_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HW_dataGridView.Location = new Point(0, 82);
            HW_dataGridView.Name = "HW_dataGridView";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = SystemColors.Control;
            dataGridViewCellStyle11.Font = new Font("맑은 고딕", 9F);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            HW_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            HW_dataGridView.RowHeadersVisible = false;
            HW_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new Font("맑은 고딕", 9F);
            dataGridViewCellStyle12.ForeColor = Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            HW_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            HW_dataGridView.Size = new Size(384, 323);
            HW_dataGridView.TabIndex = 155;
            // 
            // Save_button
            // 
            Save_button.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            Save_button.Location = new Point(303, 435);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(78, 23);
            Save_button.TabIndex = 156;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            // 
            // FCSList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(384, 461);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FCSList";
            Text = "Heating&Hotwater Systemlist";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HW_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private PictureBox Icon_pictureBox;
        private Label titleText;
        private DataGridView HW_dataGridView;
        private Button Save_button;
    }
}