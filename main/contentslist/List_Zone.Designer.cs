namespace main.contentslist
{
    partial class List_Zone
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
            GeneralPanel = new Panel();
            label4 = new Label();
            Copy_button = new Button();
            Remove_button = new Button();
            Add_button = new Button();
            dataGridView1 = new DataGridView();
            Icon_pictureBox = new PictureBox();
            Num_textBox = new TextBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Copy_button);
            GeneralPanel.Controls.Add(Remove_button);
            GeneralPanel.Controls.Add(Add_button);
            GeneralPanel.Controls.Add(dataGridView1);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(94, 32);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 98;
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Copy_button.Location = new Point(901, 52);
            Copy_button.Margin = new Padding(0);
            Copy_button.Name = "Copy_button";
            Copy_button.Size = new Size(47, 23);
            Copy_button.TabIndex = 91;
            Copy_button.Text = "Copy";
            Copy_button.UseVisualStyleBackColor = false;
            Copy_button.Click += Copy_button_Click;
            // 
            // Remove_button
            // 
            Remove_button.BackColor = SystemColors.ControlLight;
            Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Remove_button.FlatStyle = FlatStyle.System;
            Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Remove_button.Location = new Point(868, 52);
            Remove_button.Margin = new Padding(0);
            Remove_button.Name = "Remove_button";
            Remove_button.Size = new Size(23, 23);
            Remove_button.TabIndex = 90;
            Remove_button.Text = "-";
            Remove_button.UseVisualStyleBackColor = false;
            Remove_button.Click += Remove_button_Click;
            // 
            // Add_button
            // 
            Add_button.BackColor = SystemColors.ControlLight;
            Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Add_button.FlatStyle = FlatStyle.System;
            Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Add_button.Location = new Point(835, 52);
            Add_button.Margin = new Padding(0);
            Add_button.Name = "Add_button";
            Add_button.Size = new Size(23, 23);
            Add_button.TabIndex = 89;
            Add_button.Text = "+";
            Add_button.UseVisualStyleBackColor = false;
            Add_button.Click += Add_button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 83);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(954, 575);
            dataGridView1.TabIndex = 19;
            dataGridView1.CellContentClick += dataGridView_CellContentClick;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 0;
            Icon_pictureBox.TabStop = false;
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(86, 31);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(67, 15);
            Num_textBox.TabIndex = 100;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // List_Zone
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "List_Zone";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private PictureBox Icon_pictureBox;
        private DataGridView dataGridView1;
        private Button Remove_button;
        private Button Add_button;
        private Button Copy_button;
        private Label label4;
        private TextBox Num_textBox;
    }
}