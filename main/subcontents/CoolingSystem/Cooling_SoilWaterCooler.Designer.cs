namespace main.subcontents.CoolingSystem
{
    partial class Cooling_SoilWaterCooler
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
            label4 = new Label();
            SoilWaterCooler_dataGridView = new DataGridView();
            Save_button = new Button();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SoilWaterCooler_dataGridView).BeginInit();
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
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(0, 7);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 74);
            GeneralPanel.TabIndex = 27;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(155, 15);
            label4.TabIndex = 103;
            label4.Text = "지하수히트펌프 장비일람표";
            // 
            // SoilWaterCooler_dataGridView
            // 
            SoilWaterCooler_dataGridView.AllowUserToAddRows = false;
            SoilWaterCooler_dataGridView.AllowUserToDeleteRows = false;
            SoilWaterCooler_dataGridView.AllowUserToResizeColumns = false;
            SoilWaterCooler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SoilWaterCooler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SoilWaterCooler_dataGridView.BackgroundColor = SystemColors.Control;
            SoilWaterCooler_dataGridView.BorderStyle = BorderStyle.None;
            SoilWaterCooler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            SoilWaterCooler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            SoilWaterCooler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            SoilWaterCooler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SoilWaterCooler_dataGridView.Location = new Point(0, 83);
            SoilWaterCooler_dataGridView.Name = "SoilWaterCooler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            SoilWaterCooler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            SoilWaterCooler_dataGridView.RowHeadersVisible = false;
            SoilWaterCooler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            SoilWaterCooler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            SoilWaterCooler_dataGridView.RowTemplate.Height = 25;
            SoilWaterCooler_dataGridView.Size = new Size(800, 358);
            SoilWaterCooler_dataGridView.TabIndex = 28;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(627, 451);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 29;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Cooling_SoilWaterCooler
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 483);
            Controls.Add(GeneralPanel);
            Controls.Add(SoilWaterCooler_dataGridView);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MinimizeBox = false;
            Name = "Cooling_SoilWaterCooler";
            Text = "Cooling_SoilWaterCooler";
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SoilWaterCooler_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Icon_pictureBox;
        private Panel GeneralPanel;
        private Label label4;
        private DataGridView SoilWaterCooler_dataGridView;
        private Button Save_button;
    }
}