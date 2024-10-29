namespace main.subcontents.HeatingSystem
{
    partial class DoorDefault
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
            this.Icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.Save_button = new System.Windows.Forms.Button();
            this.Door_dataGridView = new System.Windows.Forms.DataGridView();
            this.GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Door_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GeneralPanel
            // 
            this.GeneralPanel.BackColor = System.Drawing.Color.AliceBlue;
            this.GeneralPanel.Controls.Add(this.label4);
            this.GeneralPanel.Controls.Add(this.Icon_pictureBox);
            this.GeneralPanel.Location = new System.Drawing.Point(0, -2);
            this.GeneralPanel.Name = "GeneralPanel";
            this.GeneralPanel.Size = new System.Drawing.Size(800, 74);
            this.GeneralPanel.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font =  new Font("나눔고딕", 9.75F,System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(68, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 103;
            this.label4.Text = "문 기본 DB";
            // 
            // Icon_pictureBox
            // 
            this.Icon_pictureBox.Location = new System.Drawing.Point(12, 14);
            this.Icon_pictureBox.Name = "Icon_pictureBox";
            this.Icon_pictureBox.Size = new System.Drawing.Size(50, 50);
            this.Icon_pictureBox.TabIndex = 102;
            this.Icon_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            this.Save_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Save_button.ForeColor = System.Drawing.Color.Black;
            this.Save_button.Location = new System.Drawing.Point(627, 442);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(135, 25);
            this.Save_button.TabIndex = 20;
            this.Save_button.Text = "SAVE";
            this.Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Pump_dataGridView
            // 
            this.Door_dataGridView.AllowUserToAddRows = false;
            this.Door_dataGridView.AllowUserToDeleteRows = false;
            this.Door_dataGridView.AllowUserToResizeColumns = false;
            this.Door_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Door_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Door_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Door_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Door_dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.Door_dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F,System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Door_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Door_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Door_dataGridView.Location = new System.Drawing.Point(0, 74);
            this.Door_dataGridView.Name = "Pump_dataGridView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font =  new Font("나눔고딕", 9.75F,System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Door_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Door_dataGridView.RowHeadersVisible = false;
            this.Door_dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font =  new Font("나눔고딕", 9.75F,System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.Door_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Door_dataGridView.RowTemplate.Height = 25;
            this.Door_dataGridView.Size = new System.Drawing.Size(800, 358);
            this.Door_dataGridView.TabIndex = 19;
            // 
            // DoorDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(797, 479);
            this.Controls.Add(this.Door_dataGridView);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.GeneralPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DoorDefault";
            this.Text = "DoorDefault";
            this.GeneralPanel.ResumeLayout(false);
            this.GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Door_dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView Door_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label4;
        private PictureBox Icon_pictureBox;
    }
}