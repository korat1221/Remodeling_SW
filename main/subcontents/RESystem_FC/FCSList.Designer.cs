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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            Save_button = new Button();
            HW_dataGridView = new DataGridView();
            Icon_pictureBox = new PictureBox();
            titleText = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)HW_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
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
            // Save_button
            // 
            Save_button.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold);
            Save_button.Location = new Point(303, 435);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(78, 23);
            Save_button.TabIndex = 156;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font =  new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            HW_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            HW_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HW_dataGridView.Location = new Point(7, 82);
            HW_dataGridView.Name = "HW_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            HW_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            HW_dataGridView.RowHeadersVisible = false;
            HW_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            HW_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            HW_dataGridView.Size = new Size(369, 323);
            HW_dataGridView.TabIndex = 155;
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
            titleText.Font = new Font("나눔고딕", 9.75F);
            titleText.Location = new Point(67, 25);
            titleText.Name = "titleText";
            titleText.Size = new Size(37, 19);
            titleText.TabIndex = 137;
            titleText.Text = "명칭";
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
            ((System.ComponentModel.ISupportInitialize)HW_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
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