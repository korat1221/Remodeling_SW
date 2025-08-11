namespace main.subcontents.HeatingSystem
{
    partial class AirHP_DB
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
            label1 = new Label();
            HC_comboBox = new CustomComboBox();
            label4 = new Label();
            Icon_pictureBox = new PictureBox();
            Carrier_label = new Label();
            Carrier_comboBox = new CustomComboBox();
            Save_button = new Button();
            HP_dataGridView = new DataGridView();
            infoHPdb = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HP_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(infoHPdb);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(HC_comboBox);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Carrier_label);
            GeneralPanel.Controls.Add(Carrier_comboBox);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(916, 74);
            GeneralPanel.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F);
            label1.Location = new Point(553, 45);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 158;
            label1.Text = "난방/냉방";
            // 
            // HC_comboBox
            // 
            HC_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            HC_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            HC_comboBox.FormattingEnabled = true;
            HC_comboBox.Location = new Point(621, 41);
            HC_comboBox.Name = "HC_comboBox";
            HC_comboBox.Size = new Size(120, 23);
            HC_comboBox.TabIndex = 157;
            HC_comboBox.SelectedIndexChanged += HC_comboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 103;
            label4.Text = "외기 히트펌프";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // Carrier_label
            // 
            Carrier_label.AutoSize = true;
            Carrier_label.Font = new Font("나눔바른고딕", 9.75F);
            Carrier_label.Location = new Point(747, 45);
            Carrier_label.Name = "Carrier_label";
            Carrier_label.Size = new Size(31, 15);
            Carrier_label.TabIndex = 156;
            Carrier_label.Text = "연료";
            // 
            // Carrier_comboBox
            // 
            Carrier_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Carrier_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            Carrier_comboBox.FormattingEnabled = true;
            Carrier_comboBox.Location = new Point(784, 41);
            Carrier_comboBox.Name = "Carrier_comboBox";
            Carrier_comboBox.Size = new Size(120, 23);
            Carrier_comboBox.TabIndex = 155;
            Carrier_comboBox.SelectedIndexChanged += Carrier_comboBox_SelectedIndexChanged;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(769, 438);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // HP_dataGridView
            // 
            HP_dataGridView.AllowUserToAddRows = false;
            HP_dataGridView.AllowUserToDeleteRows = false;
            HP_dataGridView.AllowUserToResizeColumns = false;
            HP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            HP_dataGridView.BackgroundColor = SystemColors.Control;
            HP_dataGridView.BorderStyle = BorderStyle.None;
            HP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            HP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            HP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            HP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HP_dataGridView.Location = new Point(0, 68);
            HP_dataGridView.Name = "HP_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            HP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            HP_dataGridView.RowHeadersVisible = false;
            HP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            HP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            HP_dataGridView.Size = new Size(916, 364);
            HP_dataGridView.TabIndex = 19;
            // 
            // infoHPdb
            // 
            infoHPdb.BackColor = SystemColors.ControlLight;
            infoHPdb.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            infoHPdb.FlatStyle = FlatStyle.System;
            infoHPdb.Font = new Font("Microsoft Sans Serif", 9.75F);
            infoHPdb.Location = new Point(884, 11);
            infoHPdb.Margin = new Padding(0);
            infoHPdb.Name = "infoHPdb";
            infoHPdb.Size = new Size(23, 23);
            infoHPdb.TabIndex = 159;
            infoHPdb.Text = "?";
            infoHPdb.UseVisualStyleBackColor = false;
            infoHPdb.Click += infoHPdb_Click;
            // 
            // AirHP_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(916, 479);
            Controls.Add(HP_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AirHP_DB";
            Text = "AirHP_DB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HP_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView HP_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Label Carrier_label;
        private CustomComboBox Carrier_comboBox;
        private Label label1;
        private CustomComboBox HC_comboBox;
        private Button infoHPdb;
    }
}