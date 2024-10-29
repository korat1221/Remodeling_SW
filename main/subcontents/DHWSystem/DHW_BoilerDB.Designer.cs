namespace main.subcontents.DHWSystem
{
    partial class DHW_BoilerDB
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
            Boiler_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
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
            label4.Size = new Size(71, 15);
            label4.TabIndex = 103;
            label4.Text = "급탕 보일러";
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
            // Boiler_dataGridView
            // 
            Boiler_dataGridView.AllowUserToAddRows = false;
            Boiler_dataGridView.AllowUserToDeleteRows = false;
            Boiler_dataGridView.AllowUserToResizeColumns = false;
            Boiler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Boiler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Boiler_dataGridView.BackgroundColor = SystemColors.Control;
            Boiler_dataGridView.BorderStyle = BorderStyle.None;
            Boiler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Boiler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(0, 74);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Boiler_dataGridView.RowTemplate.Height = 25;
            Boiler_dataGridView.Size = new Size(800, 358);
            Boiler_dataGridView.TabIndex = 19;
            // 
            // DHW_BoilerDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(797, 479);
            Controls.Add(Boiler_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DHW_BoilerDB";
            Text = "DHW_BoilerDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView Boiler_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label4;
        private PictureBox Icon_pictureBox;
    }
}