namespace main.subcontents.Alt
{
    partial class AltRoof
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Save_button = new Button();
            Ucalc_dataGridView = new DataGridView();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Alt_dataGridView = new DataGridView();
            WallEx_comboBox = new CustomComboBox();
            WallEx_label = new Label();
            RoofRemodelingType_comboBox = new CustomComboBox();
            label11 = new Label();
            Graph_label = new Label();
            SIM_button = new Button();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            TB_textBox = new TextBox();
            dU_textBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(918, 530);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Ucalc_dataGridView
            // 
            Ucalc_dataGridView.AllowUserToAddRows = false;
            Ucalc_dataGridView.AllowUserToDeleteRows = false;
            Ucalc_dataGridView.AllowUserToResizeColumns = false;
            Ucalc_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Ucalc_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Ucalc_dataGridView.BackgroundColor = Color.White;
            Ucalc_dataGridView.BorderStyle = BorderStyle.None;
            Ucalc_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Ucalc_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Ucalc_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Ucalc_dataGridView.Location = new Point(1, 48);
            Ucalc_dataGridView.Name = "Ucalc_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Ucalc_dataGridView.RowHeadersVisible = false;
            Ucalc_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Ucalc_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Ucalc_dataGridView.RowTemplate.Height = 25;
            Ucalc_dataGridView.Size = new Size(534, 286);
            Ucalc_dataGridView.TabIndex = 99;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(702, 48);
            webView21.Name = "webView21";
            webView21.Size = new Size(354, 286);
            webView21.Source = new Uri("http://localhost:3000/transmit.html", UriKind.Absolute);
            webView21.TabIndex = 100;
            webView21.Visible = false;
            webView21.ZoomFactor = 1D;
            // 
            // Alt_dataGridView
            // 
            Alt_dataGridView.AllowUserToAddRows = false;
            Alt_dataGridView.AllowUserToDeleteRows = false;
            Alt_dataGridView.AllowUserToResizeColumns = false;
            Alt_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Alt_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Alt_dataGridView.BackgroundColor = Color.White;
            Alt_dataGridView.BorderStyle = BorderStyle.None;
            Alt_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Alt_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            Alt_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Alt_dataGridView.Location = new Point(1, 340);
            Alt_dataGridView.Name = "Alt_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            Alt_dataGridView.RowHeadersVisible = false;
            Alt_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Alt_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            Alt_dataGridView.RowTemplate.Height = 25;
            Alt_dataGridView.Size = new Size(1066, 184);
            Alt_dataGridView.TabIndex = 101;
            Alt_dataGridView.CellContentClick += Alt_dataGridView_CellContentClick;
            Alt_dataGridView.CellValueChanged += Alt_dataGridView_CellValueChanged;
            // 
            // WallEx_comboBox
            // 
            WallEx_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WallEx_comboBox.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
            WallEx_comboBox.FormattingEnabled = true;
            WallEx_comboBox.Location = new Point(336, 12);
            WallEx_comboBox.Name = "WallEx_comboBox";
            WallEx_comboBox.Size = new Size(120, 23);
            WallEx_comboBox.TabIndex = 155;
            WallEx_comboBox.Visible = false;
            WallEx_comboBox.SelectedIndexChanged += WallEx_comboBox_SelectedIndexChanged;
            // 
            // WallEx_label
            // 
            WallEx_label.AutoSize = true;
            WallEx_label.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            WallEx_label.Location = new Point(245, 16);
            WallEx_label.Name = "WallEx_label";
            WallEx_label.Size = new Size(67, 15);
            WallEx_label.TabIndex = 154;
            WallEx_label.Text = "외장재유형";
            WallEx_label.Visible = false;
            // 
            // WallRemodelingType_comboBox
            // 
            RoofRemodelingType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            RoofRemodelingType_comboBox.Font = new System.Drawing.Font("맑은 고딕", 9F,FontStyle.Regular, GraphicsUnit.Point);
            RoofRemodelingType_comboBox.FormattingEnabled = true;
            RoofRemodelingType_comboBox.Location = new Point(109, 12);
            RoofRemodelingType_comboBox.Name = "WallRemodelingType_comboBox";
            RoofRemodelingType_comboBox.Size = new Size(120, 23);
            RoofRemodelingType_comboBox.TabIndex = 153;
            RoofRemodelingType_comboBox.SelectedIndexChanged += WallRemodelingType_comboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(8, 16);
            label11.Name = "label11";
            label11.Size = new Size(83, 15);
            label11.TabIndex = 152;
            label11.Text = "리모델링 방식";
            // 
            // Graph_label
            // 
            Graph_label.AutoSize = true;
            Graph_label.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Graph_label.Location = new Point(786, 30);
            Graph_label.Name = "Graph_label";
            Graph_label.Size = new Size(187, 15);
            Graph_label.TabIndex = 157;
            Graph_label.Text = "대표(면적이 가장 큰) 외벽 그래프";
            Graph_label.Visible = false;
            // 
            // SIM_button
            // 
            SIM_button.BackColor = SystemColors.ControlLight;
            SIM_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            SIM_button.FlatStyle = FlatStyle.System;
            SIM_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SIM_button.Location = new Point(459, 12);
            SIM_button.Margin = new Padding(0);
            SIM_button.Name = "SIM_button";
            SIM_button.Size = new Size(23, 23);
            SIM_button.TabIndex = 158;
            SIM_button.Text = "+";
            SIM_button.UseVisualStyleBackColor = false;
            SIM_button.Click += SIM_button_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(550, 196);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(130, 138);
            pictureBox2.TabIndex = 160;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(550, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 138);
            pictureBox1.TabIndex = 159;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // TB_textBox
            // 
            TB_textBox.BackColor = Color.White;
            TB_textBox.BorderStyle = BorderStyle.None;
            TB_textBox.Enabled = false;
            TB_textBox.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TB_textBox.ForeColor = Color.Black;
            TB_textBox.Location = new Point(544, 11);
            TB_textBox.Name = "TB_textBox";
            TB_textBox.Size = new Size(142, 16);
            TB_textBox.TabIndex = 162;
            TB_textBox.TextAlign = HorizontalAlignment.Center;
            TB_textBox.Visible = false;
            // 
            // dU_textBox
            // 
            dU_textBox.BackColor = Color.White;
            dU_textBox.BorderStyle = BorderStyle.None;
            dU_textBox.Enabled = false;
            dU_textBox.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dU_textBox.ForeColor = Color.Black;
            dU_textBox.Location = new Point(544, 33);
            dU_textBox.Name = "dU_textBox";
            dU_textBox.Size = new Size(142, 16);
            dU_textBox.TabIndex = 163;
            dU_textBox.TextAlign = HorizontalAlignment.Center;
            dU_textBox.Visible = false;
            // 
            // AltRoof
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1065, 568);
            Controls.Add(dU_textBox);
            Controls.Add(TB_textBox);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(SIM_button);
            Controls.Add(WallEx_comboBox);
            Controls.Add(WallEx_label);
            Controls.Add(RoofRemodelingType_comboBox);
            Controls.Add(label11);
            Controls.Add(Alt_dataGridView);
            Controls.Add(Ucalc_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(Graph_label);
            Controls.Add(webView21);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AltRoof";
            Text = "Review of Alternatives";
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Save_button;
        private TextBox d_ins_textBox;
        private DataGridView Ucalc_dataGridView;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private DataGridView Alt_dataGridView;
        private Label label2;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox5;
        private Label label6;
        private TextBox textBox7;
        private Label label9;
        private CustomComboBox WallEx_comboBox;
        private Label WallEx_label;
        private CustomComboBox RoofRemodelingType_comboBox;
        private Label label11;
        private Label Graph_label;
        private Button SIM_button;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private TextBox TB_textBox;
        private TextBox dU_textBox;
    }
}