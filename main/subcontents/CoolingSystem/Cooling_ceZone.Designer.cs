namespace main.subcontents.CoolingSystem
{
    partial class Cooling_ceZone
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
            Icon_pictureBox = new PictureBox();
            GeneralPanel = new Panel();
            ceType_textBox = new TextBox();
            label4 = new Label();
            ceZone_dataGridView = new DataGridView();
            Save_button = new Button();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ceZone_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 11);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(ceType_textBox);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(2, 3);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 74);
            GeneralPanel.TabIndex = 21;
            // 
            // ceType_textBox
            // 
            ceType_textBox.BackColor = Color.AliceBlue;
            ceType_textBox.BorderStyle = BorderStyle.None;
            ceType_textBox.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ceType_textBox.Location = new Point(200, 45);
            ceType_textBox.Name = "ceType_textBox";
            ceType_textBox.Size = new Size(45, 16);
            ceType_textBox.TabIndex = 139;
            ceType_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(85, 46);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 103;
            label4.Text = "[존 공급설비 지정]";
            // 
            // ceZone_dataGridView
            // 
            ceZone_dataGridView.AllowUserToAddRows = false;
            ceZone_dataGridView.AllowUserToDeleteRows = false;
            ceZone_dataGridView.AllowUserToResizeColumns = false;
            ceZone_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ceZone_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ceZone_dataGridView.BackgroundColor = SystemColors.Control;
            ceZone_dataGridView.BorderStyle = BorderStyle.None;
            ceZone_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ceZone_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            ceZone_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            ceZone_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ceZone_dataGridView.Location = new Point(2, 82);
            ceZone_dataGridView.Name = "ceZone_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            ceZone_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            ceZone_dataGridView.RowHeadersVisible = false;
            ceZone_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            ceZone_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            ceZone_dataGridView.RowTemplate.Height = 25;
            ceZone_dataGridView.Size = new Size(800, 358);
            ceZone_dataGridView.TabIndex = 22;
            ceZone_dataGridView.CellContentClick += ceZone_dataGridView_CellContentClick;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(629, 450);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 23;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Cooling_ceZone
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 481);
            Controls.Add(GeneralPanel);
            Controls.Add(ceZone_dataGridView);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Cooling_ceZone";
            Text = "Cooling_ceZone";
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ceZone_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Icon_pictureBox;
        private Panel GeneralPanel;
        private TextBox ceType_textBox;
        private Label label4;
        private DataGridView ceZone_dataGridView;
        private Button Save_button;
    }
}