using main.subcontents.RESystem_PV;


namespace main.subcontents
{
    partial class WP_DB
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
            WP_dataGridView = new DataGridView();
            GeneralPanel = new Panel();
            label2 = new Label();
            Icon_pictureBox = new PictureBox();
            panel1 = new Panel();
            UserNum_textBox = new TextBox();
            label4 = new Label();
            Save_button = new Button();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // WP_dataGridView
            // 
            WP_dataGridView.AllowUserToAddRows = false;
            WP_dataGridView.AllowUserToDeleteRows = false;
            WP_dataGridView.AllowUserToResizeColumns = false;
            WP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            WP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            WP_dataGridView.BackgroundColor = SystemColors.Control;
            WP_dataGridView.BorderStyle = BorderStyle.None;
            WP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            WP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            WP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WP_dataGridView.Location = new Point(0, 45);
            WP_dataGridView.Name = "WP_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            WP_dataGridView.RowHeadersVisible = false;
            WP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            WP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            WP_dataGridView.Size = new Size(932, 296);
            WP_dataGridView.TabIndex = 19;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(panel1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(932, 401);
            GeneralPanel.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label2.Location = new Point(64, 21);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 105;
            label2.Text = "풍력 장비일람표";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(8, 3);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 104;
            Icon_pictureBox.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(WP_dataGridView);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(0, 58);
            panel1.Name = "panel1";
            panel1.Size = new Size(932, 343);
            panel1.TabIndex = 27;
            // 
            // UserNum_textBox
            // 
            UserNum_textBox.BackColor = SystemColors.GradientInactiveCaption;
            UserNum_textBox.BorderStyle = BorderStyle.None;
            UserNum_textBox.Location = new Point(173, 15);
            UserNum_textBox.Name = "UserNum_textBox";
            UserNum_textBox.Size = new Size(68, 16);
            UserNum_textBox.TabIndex = 107;
            UserNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label4.Location = new Point(8, 15);
            label4.Name = "label4";
            label4.Size = new Size(117, 15);
            label4.TabIndex = 96;
            label4.Text = "기준 DB 및 인증 DB";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(785, 405);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // WP2_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(932, 437);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "WP2_DB";
            Text = "WP_DB";
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private Button Save_button;
        private Button AddUserDB_button;
        private Panel panel1;
        private Button Deletebutton;
        private Label label4;
        private Label label15;
        private TextBox UserNum_textBox;
        private DataGridView WP_dataGridView;
        private Label label2;
        private PictureBox Icon_pictureBox;
    }
}