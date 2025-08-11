
namespace main.subcontents
{
    partial class AirCooler_DB
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            AirCooler_dataGridView = new DataGridView();
            Save_button = new Button();
            GeneralPanel = new Panel();
            AC_textBox = new TextBox();
            AirCooler_text = new Label();
            infoACdb = new Button();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AirCooler_dataGridView).BeginInit();
            GeneralPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 103;
            label4.Text = "공냉식 냉동기";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // AirCooler_dataGridView
            // 
            AirCooler_dataGridView.AllowUserToAddRows = false;
            AirCooler_dataGridView.AllowUserToDeleteRows = false;
            AirCooler_dataGridView.AllowUserToResizeColumns = false;
            AirCooler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AirCooler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AirCooler_dataGridView.BackgroundColor = SystemColors.Control;
            AirCooler_dataGridView.BorderStyle = BorderStyle.None;
            AirCooler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AirCooler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            AirCooler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            AirCooler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AirCooler_dataGridView.Location = new Point(0, 79);
            AirCooler_dataGridView.Name = "AirCooler_dataGridView";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            AirCooler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            AirCooler_dataGridView.RowHeadersVisible = false;
            AirCooler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            AirCooler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            AirCooler_dataGridView.Size = new Size(593, 364);
            AirCooler_dataGridView.TabIndex = 22;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(458, 449);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 23;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(infoACdb);
            GeneralPanel.Controls.Add(AC_textBox);
            GeneralPanel.Controls.Add(AirCooler_text);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Location = new Point(0, 5);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(784, 74);
            GeneralPanel.TabIndex = 21;
            // 
            // AC_textBox
            // 
            AC_textBox.BackColor = Color.AliceBlue;
            AC_textBox.BorderStyle = BorderStyle.FixedSingle;
            AC_textBox.Location = new Point(211, 15);
            AC_textBox.Multiline = true;
            AC_textBox.Name = "AC_textBox";
            AC_textBox.ReadOnly = true;
            AC_textBox.Size = new Size(359, 48);
            AC_textBox.TabIndex = 105;
            // 
            // AirCooler_text
            // 
            AirCooler_text.AutoSize = true;
            AirCooler_text.Location = new Point(171, 14);
            AirCooler_text.Name = "AirCooler_text";
            AirCooler_text.Size = new Size(34, 15);
            AirCooler_text.TabIndex = 104;
            AirCooler_text.Text = "주의:";
            // 
            // infoACdb
            // 
            infoACdb.BackColor = SystemColors.ControlLight;
            infoACdb.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            infoACdb.FlatStyle = FlatStyle.System;
            infoACdb.Font = new Font("Microsoft Sans Serif", 9.75F);
            infoACdb.Location = new Point(573, 4);
            infoACdb.Margin = new Padding(0);
            infoACdb.Name = "infoACdb";
            infoACdb.Size = new Size(23, 23);
            infoACdb.TabIndex = 149;
            infoACdb.Text = "?";
            infoACdb.UseVisualStyleBackColor = false;
            infoACdb.Click += infoACdb_Click;
            // 
            // AirCooler_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 479);
            Controls.Add(AirCooler_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AirCooler_DB";
            Text = "AirCooler_DB";
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)AirCooler_dataGridView).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label4;
        private PictureBox Icon_pictureBox;
        private DataGridView AirCooler_dataGridView;
        private Button Save_button;
        private Panel GeneralPanel;
        private Label AirCooler_text;
        private TextBox AC_textBox;
        private Button infoACdb;
    }
}