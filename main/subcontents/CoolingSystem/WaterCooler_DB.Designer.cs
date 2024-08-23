namespace main.subcontents
{
    partial class WaterCooler_DB
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
            Icon_pictureBox = new PictureBox();
            GeneralPanel = new Panel();
            WC_textBox = new TextBox();
            AirCooler_text = new Label();
            label4 = new Label();
            WaterCooler_dataGridView = new DataGridView();
            Save_button = new Button();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WaterCooler_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(WC_textBox);
            GeneralPanel.Controls.Add(AirCooler_text);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(0, 5);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(605, 74);
            GeneralPanel.TabIndex = 24;
            // 
            // WC_textBox
            // 
            WC_textBox.BackColor = Color.AliceBlue;
            WC_textBox.BorderStyle = BorderStyle.FixedSingle;
            WC_textBox.Location = new Point(211, 15);
            WC_textBox.Multiline = true;
            WC_textBox.Name = "WC_textBox";
            WC_textBox.ReadOnly = true;
            WC_textBox.Size = new Size(359, 48);
            WC_textBox.TabIndex = 105;
            // 
            // AirCooler_text
            // 
            AirCooler_text.AutoSize = true;
            AirCooler_text.Location = new Point(171, 14);
            AirCooler_text.Name = "AirCooler_text";
            AirCooler_text.Size = new Size(34, 15);
            AirCooler_text.TabIndex = 104;
            AirCooler_text.Text = "주의:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 103;
            label4.Text = "수냉식 냉동기";
            // 
            // WaterCooler_dataGridView
            // 
            WaterCooler_dataGridView.AllowUserToAddRows = false;
            WaterCooler_dataGridView.AllowUserToDeleteRows = false;
            WaterCooler_dataGridView.AllowUserToResizeColumns = false;
            WaterCooler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            WaterCooler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            WaterCooler_dataGridView.BackgroundColor = SystemColors.Control;
            WaterCooler_dataGridView.BorderStyle = BorderStyle.None;
            WaterCooler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            WaterCooler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            WaterCooler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            WaterCooler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WaterCooler_dataGridView.Location = new Point(0, 79);
            WaterCooler_dataGridView.Name = "WaterCooler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            WaterCooler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            WaterCooler_dataGridView.RowHeadersVisible = false;
            WaterCooler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            WaterCooler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            WaterCooler_dataGridView.RowTemplate.Height = 25;
            WaterCooler_dataGridView.Size = new Size(593, 364);
            WaterCooler_dataGridView.TabIndex = 25;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(458, 449);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 26;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // WaterCooler_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 479);
            Controls.Add(GeneralPanel);
            Controls.Add(WaterCooler_dataGridView);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "WaterCooler_DB";
            Text = "WaterCooler_DB";
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WaterCooler_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Icon_pictureBox;
        private Panel GeneralPanel;
        private TextBox WC_textBox;
        private Label AirCooler_text;
        private Label label4;
        private DataGridView WaterCooler_dataGridView;
        private Button Save_button;
    }
}