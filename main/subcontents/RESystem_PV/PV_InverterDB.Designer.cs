
namespace main.subcontents.RESystem_PV
{
    partial class PV_InverterDB
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
            Save_button = new Button();
            AddUserDB_button = new Button();
            panel1 = new Panel();
            PVInverter_dataGridView = new DataGridView();
            UserNum_textBox = new TextBox();
            Deletebutton = new Button();
            pictureBox1 = new PictureBox();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PVInverter_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Dock = DockStyle.Top;
            GeneralPanel.Location = new Point(0, 0);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(678, 76);
            GeneralPanel.TabIndex = 18;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(531, 324);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // AddUserDB_button
            // 
            AddUserDB_button.BackColor = SystemColors.ControlLight;
            AddUserDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddUserDB_button.FlatStyle = FlatStyle.System;
            AddUserDB_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            AddUserDB_button.Location = new Point(620, 3);
            AddUserDB_button.Margin = new Padding(0);
            AddUserDB_button.Name = "AddUserDB_button";
            AddUserDB_button.Size = new Size(23, 23);
            AddUserDB_button.TabIndex = 89;
            AddUserDB_button.Text = "+";
            AddUserDB_button.UseVisualStyleBackColor = false;
            AddUserDB_button.Click += AddUserDB_button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(PVInverter_dataGridView);
            panel1.Controls.Add(UserNum_textBox);
            panel1.Controls.Add(Deletebutton);
            panel1.Controls.Add(AddUserDB_button);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 76);
            panel1.Name = "panel1";
            panel1.Size = new Size(678, 238);
            panel1.TabIndex = 27;
            // 
            // PVInverter_dataGridView
            // 
            PVInverter_dataGridView.AllowUserToAddRows = false;
            PVInverter_dataGridView.AllowUserToDeleteRows = false;
            PVInverter_dataGridView.AllowUserToResizeColumns = false;
            PVInverter_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PVInverter_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            PVInverter_dataGridView.BackgroundColor = SystemColors.Control;
            PVInverter_dataGridView.BorderStyle = BorderStyle.None;
            PVInverter_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            PVInverter_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            PVInverter_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            PVInverter_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PVInverter_dataGridView.Dock = DockStyle.Bottom;
            PVInverter_dataGridView.Location = new Point(0, 29);
            PVInverter_dataGridView.Name = "PVInverter_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            PVInverter_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            PVInverter_dataGridView.RowHeadersVisible = false;
            PVInverter_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            PVInverter_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            PVInverter_dataGridView.Size = new Size(678, 209);
            PVInverter_dataGridView.TabIndex = 19;
       
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
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            Deletebutton.Location = new Point(643, 3);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 95;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            Deletebutton.Click += Deletebutton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(9, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(67, 59);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // PV_InverterDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(678, 356);
            Controls.Add(Save_button);
            Controls.Add(panel1);
            Controls.Add(GeneralPanel);
            Name = "PV_InverterDB";
            Text = "PV_InverterDB";
            GeneralPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PVInverter_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private Button AddUserDB_button;
        private Panel panel1;
        private Button Deletebutton;
        private TextBox UserNum_textBox;
        private DataGridView PVInverter_dataGridView;
        private Label label2;
        private ComboBox UserDB_year_comboBox;
        private Label label11;
        private TextBox UserDB_width_textBox;
        private Label label28;
        private TextBox UserDB_height_textBox;
        private Label label29;
        private TextBox UserDB_output_textBox;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label5;
        private PictureBox pictureBox1;
    }
}