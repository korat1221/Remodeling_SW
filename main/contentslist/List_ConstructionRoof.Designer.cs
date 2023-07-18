namespace main.contentslist
{
    partial class List_ConstructionRoof
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GeneralPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Copy_button = new System.Windows.Forms.Button();
            this.Remove_button = new System.Windows.Forms.Button();
            this.Add_button = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GeneralPanel
            // 
            this.GeneralPanel.BackColor = System.Drawing.Color.White;
            this.GeneralPanel.Controls.Add(this.label4);
            this.GeneralPanel.Controls.Add(this.Copy_button);
            this.GeneralPanel.Controls.Add(this.Remove_button);
            this.GeneralPanel.Controls.Add(this.Add_button);
            this.GeneralPanel.Controls.Add(this.dataGridView1);
            this.GeneralPanel.Controls.Add(this.Icon_pictureBox);
            this.GeneralPanel.Location = new System.Drawing.Point(12, 12);
            this.GeneralPanel.Name = "GeneralPanel";
            this.GeneralPanel.Size = new System.Drawing.Size(977, 661);
            this.GeneralPanel.TabIndex = 17;
            this.GeneralPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GeneralPanel_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(91, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 97;
            this.label4.Text = "지붕";
            // 
            // Copy_button
            // 
            this.Copy_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Copy_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.Copy_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Copy_button.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Copy_button.Location = new System.Drawing.Point(901, 52);
            this.Copy_button.Margin = new System.Windows.Forms.Padding(0);
            this.Copy_button.Name = "Copy_button";
            this.Copy_button.Size = new System.Drawing.Size(47, 23);
            this.Copy_button.TabIndex = 91;
            this.Copy_button.Text = "Copy";
            this.Copy_button.UseVisualStyleBackColor = false;
            this.Copy_button.Click += new System.EventHandler(this.Copy_button_Click);
            // 
            // Remove_button
            // 
            this.Remove_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Remove_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.Remove_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Remove_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Remove_button.Location = new System.Drawing.Point(868, 52);
            this.Remove_button.Margin = new System.Windows.Forms.Padding(0);
            this.Remove_button.Name = "Remove_button";
            this.Remove_button.Size = new System.Drawing.Size(23, 23);
            this.Remove_button.TabIndex = 90;
            this.Remove_button.Text = "-";
            this.Remove_button.UseVisualStyleBackColor = false;
            this.Remove_button.Click += new System.EventHandler(this.Remove_button_Click);
            // 
            // Add_button
            // 
            this.Add_button.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Add_button.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.Add_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Add_button.Location = new System.Drawing.Point(835, 52);
            this.Add_button.Margin = new System.Windows.Forms.Padding(0);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(23, 23);
            this.Add_button.TabIndex = 89;
            this.Add_button.Text = "+";
            this.Add_button.UseVisualStyleBackColor = false;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 83);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(954, 575);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;
            // 
            // Icon_pictureBox
            // 
            this.Icon_pictureBox.Location = new System.Drawing.Point(30, 14);
            this.Icon_pictureBox.Name = "Icon_pictureBox";
            this.Icon_pictureBox.Size = new System.Drawing.Size(50, 50);
            this.Icon_pictureBox.TabIndex = 0;
            this.Icon_pictureBox.TabStop = false;
            // 
            // List_ConstructionRoof
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(1200, 730);
            this.Controls.Add(this.GeneralPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "List_ConstructionRoof";
            this.Text = "Form3";
            this.GeneralPanel.ResumeLayout(false);
            this.GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel GeneralPanel;
        private PictureBox Icon_pictureBox;
        private DataGridView dataGridView1;
        private Button Remove_button;
        private Button Add_button;
        private Button Copy_button;
        private Label label4;
    }
}