namespace main.subcontents.ConstructionBlind
{
    partial class BlindDB
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
            Save_button = new Button();
            Blind_dataGridView = new DataGridView();
            info = new Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Blind_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(info);
            GeneralPanel.Controls.Add(Copy_button);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(Remove_button);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Add_button);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(800, 74);
            GeneralPanel.TabIndex = 18;
            // 
            // Copy_button
            // 
            Copy_button.BackColor = SystemColors.ControlLight;
            Copy_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Copy_button.FlatStyle = FlatStyle.System;
            Copy_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            Copy_button.Location = new Point(741, 41);
            Copy_button.Margin = new Padding(0);
            Copy_button.Name = "Copy_button";
            Copy_button.Size = new Size(47, 23);
            Copy_button.TabIndex = 120;
            Copy_button.Text = "Copy";
            Copy_button.UseVisualStyleBackColor = false;
            Copy_button.Click += Copy_button_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(68, 32);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 103;
            label4.Text = "차양";
            // 
            // Remove_button
            // 
            Remove_button.BackColor = SystemColors.ControlLight;
            Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Remove_button.FlatStyle = FlatStyle.System;
            Remove_button.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold);
            Remove_button.Location = new Point(704, 41);
            Remove_button.Margin = new Padding(0);
            Remove_button.Name = "Remove_button";
            Remove_button.Size = new Size(23, 23);
            Remove_button.TabIndex = 119;
            Remove_button.Text = "-";
            Remove_button.UseVisualStyleBackColor = false;
            Remove_button.Click += Remove_button_Click;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(12, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 102;
            Icon_pictureBox.TabStop = false;
            // 
            // Add_button
            // 
            Add_button.BackColor = SystemColors.ControlLight;
            Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Add_button.FlatStyle = FlatStyle.System;
            Add_button.Font = new Font("나눔바른고딕", 11.9999981F, FontStyle.Bold);
            Add_button.Location = new Point(667, 41);
            Add_button.Margin = new Padding(0);
            Add_button.Name = "Add_button";
            Add_button.Size = new Size(23, 23);
            Add_button.TabIndex = 118;
            Add_button.Text = "+";
            Add_button.UseVisualStyleBackColor = false;
            Add_button.Click += Add_button_Click;
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
            // Blind_dataGridView
            // 
            Blind_dataGridView.AllowUserToAddRows = false;
            Blind_dataGridView.AllowUserToDeleteRows = false;
            Blind_dataGridView.AllowUserToResizeColumns = false;
            Blind_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Blind_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Blind_dataGridView.BackgroundColor = SystemColors.Control;
            Blind_dataGridView.BorderStyle = BorderStyle.None;
            Blind_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Blind_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Blind_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Blind_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Blind_dataGridView.Location = new Point(0, 74);
            Blind_dataGridView.Name = "Blind_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Blind_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Blind_dataGridView.RowHeadersVisible = false;
            Blind_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Blind_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Blind_dataGridView.Size = new Size(800, 358);
            Blind_dataGridView.TabIndex = 19;
            Blind_dataGridView.CellContentClick += Blind_dataGridView_CellContentClick;
            // 
            // info
            // 
            info.BackColor = SystemColors.ControlLight;
            info.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            info.FlatStyle = FlatStyle.System;
            info.Font = new Font("나눔바른고딕", 9.75F);
            info.Location = new Point(765, 9);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(23, 23);
            info.TabIndex = 156;
            info.Text = "?";
            info.UseVisualStyleBackColor = false;
            info.Click += info_Click;
            // 
            // BlindDB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(797, 479);
            Controls.Add(Blind_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "BlindDB";
            Text = "BlindDB";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)Blind_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Button Save_button;
        private DataGridView Blind_dataGridView;
        private TextBox textBox2;
        private TextBox d_ins_textBox;
        private Label label4;
        private PictureBox Icon_pictureBox;
        private Button Copy_button;
        private Button Remove_button;
        private Button Add_button;
        private Button info;
    }
}