namespace main.subcontents.DHWSystem
{
    partial class DHW_PumpDB
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
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            Save_button = new Button();
            Pump_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 74);
            GeneralPanel.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font =  new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(135, 15);
            label4.TabIndex = 103;
            label4.Text = "급탕펌프";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(627, 442);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Pump_dataGridView
            // 
            Pump_dataGridView.AllowUserToAddRows = false;
            Pump_dataGridView.AllowUserToDeleteRows = false;
            Pump_dataGridView.AllowUserToResizeColumns = false;
            Pump_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Pump_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Pump_dataGridView.BackgroundColor = SystemColors.Control;
            Pump_dataGridView.BorderStyle = BorderStyle.None;
            Pump_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Pump_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Pump_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Pump_dataGridView.Location = new Point(0, 74);
            Pump_dataGridView.Name = "Pump_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Pump_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Pump_dataGridView.RowHeadersVisible = false;
            Pump_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Pump_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Pump_dataGridView.RowTemplate.Height = 25;
            Pump_dataGridView.Size = new Size(800, 358);
            Pump_dataGridView.TabIndex = 19;
            // 
            // DHW_PumpDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(797, 479);
            Controls.Add(Pump_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DHW_PumpDB";
            Text = "DHW_PumpDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pump_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView Pump_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label4;
        private PictureBox Icon_pictureBox;
    }
}