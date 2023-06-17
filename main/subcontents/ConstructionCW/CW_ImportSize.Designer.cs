namespace main.subcontents
{
    partial class CW_ImportSize
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
            Size_dataGridView = new DataGridView();
            Save_button = new Button();
            CSVImport_button = new Button();
            ((System.ComponentModel.ISupportInitialize)Size_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Size_dataGridView
            // 
            Size_dataGridView.AllowUserToAddRows = false;
            Size_dataGridView.AllowUserToDeleteRows = false;
            Size_dataGridView.AllowUserToResizeColumns = false;
            Size_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Size_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Size_dataGridView.BackgroundColor = SystemColors.Control;
            Size_dataGridView.BorderStyle = BorderStyle.None;
            Size_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Size_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Size_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Size_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Size_dataGridView.Location = new Point(3, 54);
            Size_dataGridView.Name = "Size_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Size_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Size_dataGridView.RowHeadersVisible = false;
            Size_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Size_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Size_dataGridView.RowTemplate.Height = 25;
            Size_dataGridView.Size = new Size(1144, 359);
            Size_dataGridView.TabIndex = 19;
            Size_dataGridView.CellContentClick += Size_dataGridView_CellContentClick_1;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1000, 422);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // CSVImport_button
            // 
            CSVImport_button.BackColor = SystemColors.ControlLight;
            CSVImport_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            CSVImport_button.FlatStyle = FlatStyle.System;
            CSVImport_button.Font = new Font("나눔고딕 ExtraBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            CSVImport_button.Location = new Point(9, 19);
            CSVImport_button.Margin = new Padding(0);
            CSVImport_button.Name = "CSVImport_button";
            CSVImport_button.Size = new Size(120, 23);
            CSVImport_button.TabIndex = 90;
            CSVImport_button.Text = "치수 정보 Import";
            CSVImport_button.UseVisualStyleBackColor = false;
            CSVImport_button.Click += CSVImport_button_Click;
            // 
            // CW_ImportSize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 459);
            Controls.Add(CSVImport_button);
            Controls.Add(Save_button);
            Controls.Add(Size_dataGridView);
            Name = "CW_ImportSize";
            Text = "CW_ImportSize";
            ((System.ComponentModel.ISupportInitialize)Size_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView Size_dataGridView;
        private Button Save_button;
        private Button CSVImport_button;
    }
}