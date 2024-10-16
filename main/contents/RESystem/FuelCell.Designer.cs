namespace main.contents
{
    partial class FuelCell
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
            Icon_pictureBox = new PictureBox();
            label1 = new Label();
            Num_textBox = new TextBox();
            Name_textBox = new TextBox();
            panel2 = new Panel();
            WLabel = new Label();
            FC_dataGridView = new DataGridView();
            Week_comboBox = new ComboBox();
            UseTime_TextBox = new Label();
            label8 = new Label();
            End_comboBox = new ComboBox();
            Start_comboBox = new ComboBox();
            label7 = new Label();
            label4 = new Label();
            label3 = new Label();
            H_textBox = new TextBox();
            W_textBox = new TextBox();
            H_button = new Button();
            W_button = new Button();
            FCTypeComboBox = new ComboBox();
            label2 = new Label();
            FCNameText = new TextBox();
            FCDB_button = new Button();
            label6 = new Label();
            Inverter = new TextBox();
            PVModuleType = new TextBox();
            AdditionalPanel = new Panel();
            panel4 = new Panel();
            SupplypictureBox = new PictureBox();
            panel3 = new Panel();
            GenpictureBox = new PictureBox();
            panel1 = new Panel();
            SourcepictureBox = new PictureBox();
            label5 = new Label();
            Save_button = new Button();
            HLabel = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).BeginInit();
            AdditionalPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SupplypictureBox).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GenpictureBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SourcepictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 67);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(11, 8);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 138;
            Icon_pictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 10F);
            label1.Location = new Point(127, 25);
            label1.Name = "label1";
            label1.Size = new Size(37, 19);
            label1.TabIndex = 137;
            label1.Text = "명칭";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("맑은 고딕", 9F);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(65, 26);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(56, 16);
            Num_textBox.TabIndex = 136;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(164, 23);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 135;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(HLabel);
            panel2.Controls.Add(WLabel);
            panel2.Controls.Add(FC_dataGridView);
            panel2.Controls.Add(Week_comboBox);
            panel2.Controls.Add(UseTime_TextBox);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(End_comboBox);
            panel2.Controls.Add(Start_comboBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(H_textBox);
            panel2.Controls.Add(W_textBox);
            panel2.Controls.Add(H_button);
            panel2.Controls.Add(W_button);
            panel2.Controls.Add(FCTypeComboBox);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(FCNameText);
            panel2.Controls.Add(FCDB_button);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(Inverter);
            panel2.Controls.Add(PVModuleType);
            panel2.Location = new Point(12, 99);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 276);
            panel2.TabIndex = 18;
            panel2.Paint += panel2_Paint;
            // 
            // WLabel
            // 
            WLabel.AutoSize = true;
            WLabel.Font = new Font("Microsoft Sans Serif", 10F);
            WLabel.Location = new Point(299, 37);
            WLabel.Name = "WLabel";
            WLabel.Size = new Size(56, 17);
            WLabel.TabIndex = 155;
            WLabel.Text = "급탕설비";
            // 
            // FC_dataGridView
            // 
            FC_dataGridView.AllowUserToAddRows = false;
            FC_dataGridView.AllowUserToDeleteRows = false;
            FC_dataGridView.AllowUserToResizeColumns = false;
            FC_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FC_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            FC_dataGridView.BackgroundColor = Color.White;
            FC_dataGridView.BorderStyle = BorderStyle.None;
            FC_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            FC_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            FC_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FC_dataGridView.Location = new Point(20, 161);
            FC_dataGridView.Name = "FC_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            FC_dataGridView.RowHeadersVisible = false;
            FC_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            FC_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            FC_dataGridView.Size = new Size(943, 97);
            FC_dataGridView.TabIndex = 154;
            FC_dataGridView.CellValueChanged += FC_dataGridView_CellValueChanged;
            // 
            // Week_comboBox
            // 
            Week_comboBox.FormattingEnabled = true;
            Week_comboBox.Location = new Point(163, 112);
            Week_comboBox.Name = "Week_comboBox";
            Week_comboBox.Size = new Size(121, 23);
            Week_comboBox.TabIndex = 153;
            Week_comboBox.SelectedIndexChanged += Week_comboBox_SelectedIndexChanged;
            // 
            // UseTime_TextBox
            // 
            UseTime_TextBox.AutoSize = true;
            UseTime_TextBox.Font = new Font("맑은 고딕", 9F, FontStyle.Italic);
            UseTime_TextBox.ForeColor = SystemColors.ControlDark;
            UseTime_TextBox.Location = new Point(554, 88);
            UseTime_TextBox.Name = "UseTime_TextBox";
            UseTime_TextBox.Size = new Size(0, 15);
            UseTime_TextBox.TabIndex = 152;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 10F);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(484, 87);
            label8.Name = "label8";
            label8.Size = new Size(56, 17);
            label8.TabIndex = 151;
            label8.Text = "사용시간";
            // 
            // End_comboBox
            // 
            End_comboBox.FormattingEnabled = true;
            End_comboBox.Location = new Point(357, 85);
            End_comboBox.Name = "End_comboBox";
            End_comboBox.Size = new Size(121, 23);
            End_comboBox.TabIndex = 150;
            End_comboBox.SelectedIndexChanged += End_comboBox_SelectedIndexChanged;
            // 
            // Start_comboBox
            // 
            Start_comboBox.FormattingEnabled = true;
            Start_comboBox.Location = new Point(164, 85);
            Start_comboBox.Name = "Start_comboBox";
            Start_comboBox.Size = new Size(121, 23);
            Start_comboBox.TabIndex = 149;
            Start_comboBox.SelectedIndexChanged += Start_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 10F);
            label7.Location = new Point(299, 88);
            label7.Name = "label7";
            label7.Size = new Size(56, 17);
            label7.TabIndex = 148;
            label7.Text = "종료시간";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10F);
            label4.Location = new Point(102, 114);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 147;
            label4.Text = "주이용일";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10F);
            label3.Location = new Point(102, 88);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 146;
            label3.Text = "시작시간";
            // 
            // H_textBox
            // 
            H_textBox.BackColor = Color.White;
            H_textBox.BorderStyle = BorderStyle.None;
            H_textBox.Location = new Point(603, 38);
            H_textBox.Name = "H_textBox";
            H_textBox.ReadOnly = true;
            H_textBox.Size = new Size(136, 16);
            H_textBox.TabIndex = 145;
            H_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // W_textBox
            // 
            W_textBox.BackColor = Color.White;
            W_textBox.BorderStyle = BorderStyle.None;
            W_textBox.Location = new Point(357, 38);
            W_textBox.Name = "W_textBox";
            W_textBox.ReadOnly = true;
            W_textBox.Size = new Size(148, 16);
            W_textBox.TabIndex = 144;
            W_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // H_button
            // 
            H_button.BackColor = SystemColors.ControlLight;
            H_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            H_button.FlatStyle = FlatStyle.System;
            H_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold);
            H_button.Location = new Point(742, 34);
            H_button.Margin = new Padding(0);
            H_button.Name = "H_button";
            H_button.Size = new Size(23, 23);
            H_button.TabIndex = 143;
            H_button.Text = "+";
            H_button.UseVisualStyleBackColor = false;
            H_button.Click += H_button_Click;
            // 
            // W_button
            // 
            W_button.BackColor = SystemColors.ControlLight;
            W_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            W_button.FlatStyle = FlatStyle.System;
            W_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold);
            W_button.Location = new Point(504, 34);
            W_button.Margin = new Padding(0);
            W_button.Name = "W_button";
            W_button.Size = new Size(23, 23);
            W_button.TabIndex = 142;
            W_button.Text = "+";
            W_button.UseVisualStyleBackColor = false;
            W_button.Click += W_button_Click;
            // 
            // FCTypeComboBox
            // 
            FCTypeComboBox.FormattingEnabled = true;
            FCTypeComboBox.Location = new Point(164, 34);
            FCTypeComboBox.Name = "FCTypeComboBox";
            FCTypeComboBox.Size = new Size(121, 23);
            FCTypeComboBox.TabIndex = 141;
            FCTypeComboBox.SelectedIndexChanged += FCTypeComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10F);
            label2.Location = new Point(102, 37);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 140;
            label2.Text = "생산유형";
            // 
            // FCNameText
            // 
            FCNameText.BackColor = Color.White;
            FCNameText.BorderStyle = BorderStyle.None;
            FCNameText.Location = new Point(164, 8);
            FCNameText.Name = "FCNameText";
            FCNameText.ReadOnly = true;
            FCNameText.Size = new Size(120, 16);
            FCNameText.TabIndex = 139;
            FCNameText.TextAlign = HorizontalAlignment.Center;
            // 
            // FCDB_button
            // 
            FCDB_button.BackColor = SystemColors.ControlLight;
            FCDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FCDB_button.FlatStyle = FlatStyle.System;
            FCDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold);
            FCDB_button.Location = new Point(287, 5);
            FCDB_button.Margin = new Padding(0);
            FCDB_button.Name = "FCDB_button";
            FCDB_button.Size = new Size(23, 23);
            FCDB_button.TabIndex = 96;
            FCDB_button.Text = "+";
            FCDB_button.UseVisualStyleBackColor = false;
            FCDB_button.Click += FCDB_button_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 10F);
            label6.Location = new Point(102, 8);
            label6.Name = "label6";
            label6.Size = new Size(56, 17);
            label6.TabIndex = 101;
            label6.Text = "연료전지";
            // 
            // Inverter
            // 
            Inverter.BackColor = SystemColors.Window;
            Inverter.BorderStyle = BorderStyle.None;
            Inverter.Font = new Font("Microsoft Sans Serif", 9.75F);
            Inverter.Location = new Point(203, 34);
            Inverter.Name = "Inverter";
            Inverter.ReadOnly = true;
            Inverter.Size = new Size(120, 15);
            Inverter.TabIndex = 104;
            Inverter.TextAlign = HorizontalAlignment.Center;
            // 
            // PVModuleType
            // 
            PVModuleType.BackColor = SystemColors.Window;
            PVModuleType.BorderStyle = BorderStyle.None;
            PVModuleType.Font = new Font("Microsoft Sans Serif", 9.75F);
            PVModuleType.Location = new Point(203, 6);
            PVModuleType.Name = "PVModuleType";
            PVModuleType.ReadOnly = true;
            PVModuleType.Size = new Size(120, 15);
            PVModuleType.TabIndex = 100;
            PVModuleType.TextAlign = HorizontalAlignment.Center;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(panel4);
            AdditionalPanel.Controls.Add(panel3);
            AdditionalPanel.Controls.Add(panel1);
            AdditionalPanel.Location = new Point(12, 381);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 312);
            AdditionalPanel.TabIndex = 18;
            // 
            // panel4
            // 
            panel4.BackgroundImageLayout = ImageLayout.None;
            panel4.Controls.Add(SupplypictureBox);
            panel4.Location = new Point(511, 68);
            panel4.Name = "panel4";
            panel4.Size = new Size(459, 240);
            panel4.TabIndex = 7;
            // 
            // SupplypictureBox
            // 
            SupplypictureBox.Location = new Point(1, 2);
            SupplypictureBox.Name = "SupplypictureBox";
            SupplypictureBox.Size = new Size(100, 100);
            SupplypictureBox.TabIndex = 1;
            SupplypictureBox.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackgroundImageLayout = ImageLayout.None;
            panel3.Controls.Add(GenpictureBox);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(292, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(678, 308);
            panel3.TabIndex = 6;
            // 
            // GenpictureBox
            // 
            GenpictureBox.Location = new Point(0, -2);
            GenpictureBox.Name = "GenpictureBox";
            GenpictureBox.Size = new Size(100, 100);
            GenpictureBox.TabIndex = 0;
            GenpictureBox.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(SourcepictureBox);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 308);
            panel1.TabIndex = 5;
            // 
            // SourcepictureBox
            // 
            SourcepictureBox.Location = new Point(0, 0);
            SourcepictureBox.Name = "SourcepictureBox";
            SourcepictureBox.Size = new Size(100, 100);
            SourcepictureBox.TabIndex = 2;
            SourcepictureBox.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            label5.Location = new Point(12, 81);
            label5.Name = "label5";
            label5.Size = new Size(73, 16);
            label5.TabIndex = 95;
            label5.Text = "구성요소정보";
            // 
            // Save_button
            // 
            Save_button.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            Save_button.Location = new Point(995, 670);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(78, 23);
            Save_button.TabIndex = 96;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // HLabel
            // 
            HLabel.AutoSize = true;
            HLabel.Font = new Font("Microsoft Sans Serif", 10F);
            HLabel.Location = new Point(543, 37);
            HLabel.Name = "HLabel";
            HLabel.Size = new Size(56, 17);
            HLabel.TabIndex = 156;
            HLabel.Text = "난방설비";
            // 
            // FuelCell
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Save_button);
            Controls.Add(label5);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FuelCell";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).EndInit();
            AdditionalPanel.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SupplypictureBox).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GenpictureBox).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SourcepictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private Label label5;
        private Label label6;
        private TextBox Inverter;
        private TextBox PVModuleType;
        private Button FCDB_button;
        private Label label1;
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private PictureBox Icon_pictureBox;
        private TextBox FCNameText;
        private TextBox H_textBox;
        private TextBox W_textBox;
        private Button H_button;
        private Button W_button;
        private ComboBox FCTypeComboBox;
        private Label label2;
        private Label label4;
        private Label label3;
        private ComboBox Week_comboBox;
        private Label UseTime_TextBox;
        private Label label8;
        private ComboBox End_comboBox;
        private ComboBox Start_comboBox;
        private Label label7;
        private DataGridView FC_dataGridView;
        private Button Save_button;
        private Panel panel3;
        private PictureBox GenpictureBox;
        private Panel panel1;
        private PictureBox SourcepictureBox;
        private PictureBox SupplypictureBox;
        private Panel panel4;
        private Label WLabel;
        private Label HLabel;
    }
}