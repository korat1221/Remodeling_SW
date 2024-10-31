
namespace main.subcontents.CoolingSystem
{
    partial class Cooling_Top
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
            CoolerTop_dataGridView = new DataGridView();
            Save_button = new Button();
            GeneralPanel = new Panel();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)CoolerTop_dataGridView).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // CoolerTop_dataGridView
            // 
            CoolerTop_dataGridView.AllowUserToAddRows = false;
            CoolerTop_dataGridView.AllowUserToDeleteRows = false;
            CoolerTop_dataGridView.AllowUserToResizeColumns = false;
            CoolerTop_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolerTop_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolerTop_dataGridView.BackgroundColor = SystemColors.Control;
            CoolerTop_dataGridView.BorderStyle = BorderStyle.None;
            CoolerTop_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolerTop_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            CoolerTop_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            CoolerTop_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolerTop_dataGridView.Location = new Point(0, 83);
            CoolerTop_dataGridView.Name = "CoolerTop_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            CoolerTop_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            CoolerTop_dataGridView.RowHeadersVisible = false;
            CoolerTop_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font =  new Font(UTIL.Families[0], 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            CoolerTop_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            CoolerTop_dataGridView.RowTemplate.Height = 25;
            CoolerTop_dataGridView.Size = new Size(800, 358);
            CoolerTop_dataGridView.TabIndex = 25;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(627, 451);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 26;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(0, 7);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 74);
            GeneralPanel.TabIndex = 24;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font =  new Font(UTIL.Families[0], 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 103;
            label4.Text = "냉각탑 장비일람표";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // Cooling_Top
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 483);
            Controls.Add(CoolerTop_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MinimizeBox = false;
            Name = "Cooling_Top";
            Text = "Cooling_Top";
            ((System.ComponentModel.ISupportInitialize)CoolerTop_dataGridView).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView CoolerTop_dataGridView;
        private Button Save_button;
        private Panel GeneralPanel;
        private Label label4;
        private PictureBox Icon_pictureBox;
    }
}