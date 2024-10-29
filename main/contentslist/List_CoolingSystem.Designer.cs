namespace main.contentslist
{
    partial class List_CoolingSystem
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
            Copy_button = new Button();
            label4 = new Label();
            Remove_button = new Button();
            Icon_pictureBox = new PictureBox();
            Add_button = new Button();
            dataGridView1 = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Copy_button);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Remove_button);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Add_button);
            GeneralPanel.Controls.Add(dataGridView1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 661);
            GeneralPanel.TabIndex = 18;
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new System.Drawing.Font("나눔고딕", 8.999999F, FontStyle.Bold, GraphicsUnit.Point);
            Copy_button.Location = new Point(899, 52);
            Copy_button.Margin = new Padding(0);
            Copy_button.Name = "Copy_button";
            Copy_button.Size = new Size(47, 23);
            Copy_button.TabIndex = 95;
            Copy_button.Text = "Copy";
            Copy_button.UseVisualStyleBackColor = false;
            Copy_button.Click += Copy_button_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font =  new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(91, 32);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 97;
            label4.Text = "냉방설비";
            // 
            // Remove_button
            // 
            Remove_button.BackColor = SystemColors.ControlLight;
            Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Remove_button.FlatStyle = FlatStyle.System;
            Remove_button.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Remove_button.Location = new Point(866, 52);
            Remove_button.Margin = new Padding(0);
            Remove_button.Name = "Remove_button";
            Remove_button.Size = new Size(23, 23);
            Remove_button.TabIndex = 94;
            Remove_button.Text = "-";
            Remove_button.UseVisualStyleBackColor = false;
            Remove_button.Click += Remove_button_Click;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 0;
            Icon_pictureBox.TabStop = false;
            // 
            // Add_button
            // 
            Add_button.BackColor = SystemColors.ControlLight;
            Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Add_button.FlatStyle = FlatStyle.System;
            Add_button.Font = new System.Drawing.Font("나눔고딕", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Add_button.Location = new Point(833, 52);
            Add_button.Margin = new Padding(0);
            Add_button.Name = "Add_button";
            Add_button.Size = new Size(23, 23);
            Add_button.TabIndex = 93;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 83);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(954, 575);
            dataGridView1.TabIndex = 92;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;
            // 
            // List_CoolingSystem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "List_CoolingSystem";
            Text = "List_Cooling";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Button Copy_button;
        private Button Remove_button;
        private Button Add_button;
        private DataGridView dataGridView1;
    }
}