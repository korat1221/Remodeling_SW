namespace main.contents
{
    partial class WindPower
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
            annual_textBox = new TextBox();
            label5 = new Label();
            Num_textBox = new TextBox();
            label14 = new Label();
            label1 = new Label();
            Name_textBox = new TextBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            Range_button = new Button();
            label4 = new Label();
            Condition_ComboBox = new CustomComboBox();
            label8 = new Label();
            h2_textBox = new TextBox();
            label3 = new Label();
            주변환경 = new Label();
            WP_dataGridView = new DataGridView();
            WPDB_button = new Button();
            WPNameText = new TextBox();
            label2 = new Label();
            HerbHeight_textBox = new TextBox();
            RotateArea_textBox = new TextBox();
            Type_textBox = new TextBox();
            FCDB_button = new Button();
            label6 = new Label();
            AdditionalPanel = new Panel();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Save_button = new Button();
            WPtype_pictureBox = new PictureBox();
            Previous_button = new Button();
            label18 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).BeginInit();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WPtype_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(annual_textBox);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(label14);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            //
            // annual_textBox
            // 
            annual_textBox.BackColor = Color.White;
            annual_textBox.BorderStyle = BorderStyle.None;
            annual_textBox.Enabled = false;
            annual_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            annual_textBox.ForeColor = Color.White;
            annual_textBox.Location = new Point(534, 55);
            annual_textBox.Name = "annual_textBox";
            annual_textBox.Size = new Size(120, 15);
            annual_textBox.TabIndex = 143;
            annual_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔바른고딕", 9.75F);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(435, 55);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 142;
            label5.Text = "연간 생산량";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            Num_textBox.ForeColor = Color.White;
            Num_textBox.Location = new Point(173, 26);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(120, 15);
            Num_textBox.TabIndex = 141;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("나눔바른고딕", 9.75F);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(108, 25);
            label14.Name = "label14";
            label14.Size = new Size(34, 15);
            label14.TabIndex = 140;
            label14.Text = "번 호";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F);
            label1.Location = new Point(108, 54);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 137;
            label1.Text = "명 칭";
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(173, 51);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 135;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(11, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 134;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(Range_button);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(Condition_ComboBox);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(h2_textBox);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(주변환경);
            panel2.Controls.Add(WP_dataGridView);
            panel2.Controls.Add(WPDB_button);
            panel2.Controls.Add(WPNameText);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 112);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 276);
            panel2.TabIndex = 18;
            panel2.Paint += panel2_Paint;
            // 
            // Range_button
            // 
            Range_button.BackColor = SystemColors.ControlLight;
            Range_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Range_button.FlatStyle = FlatStyle.System;
            Range_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            Range_button.Location = new Point(322, 18);
            Range_button.Margin = new Padding(0);
            Range_button.Name = "Range_button";
            Range_button.Size = new Size(23, 23);
            Range_button.TabIndex = 170;
            Range_button.Text = "!";
            Range_button.UseVisualStyleBackColor = false;
            Range_button.Click += Range_button_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F);
            label4.Location = new Point(435, 52);
            label4.Name = "label4";
            label4.Size = new Size(208, 15);
            label4.TabIndex = 169;
            label4.Text = "* 지면으로부터 풍력 발전기 설치 높이";
            // 
            // Condition_ComboBox
            // 
            Condition_ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Condition_ComboBox.Font = new Font("나눔바른고딕", 9.75F);
            Condition_ComboBox.FormattingEnabled = true;
            Condition_ComboBox.Location = new Point(173, 80);
            Condition_ComboBox.Name = "Condition_ComboBox";
            Condition_ComboBox.Size = new Size(481, 23);
            Condition_ComboBox.TabIndex = 168;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔바른고딕", 9.75F);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(660, 22);
            label8.Name = "label8";
            label8.Size = new Size(19, 15);
            label8.TabIndex = 160;
            label8.Text = "m";
            // 
            // h2_textBox
            // 
            h2_textBox.BorderStyle = BorderStyle.FixedSingle;
            h2_textBox.Location = new Point(534, 18);
            h2_textBox.Name = "h2_textBox";
            h2_textBox.Size = new Size(120, 23);
            h2_textBox.TabIndex = 159;
            h2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔바른고딕", 9.75F);
            label3.Location = new Point(435, 22);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 158;
            label3.Text = "설치높이";
            // 
            // 주변환경
            // 
            주변환경.AutoSize = true;
            주변환경.Font = new Font("나눔바른고딕", 9.75F);
            주변환경.Location = new Point(99, 84);
            주변환경.Name = "주변환경";
            주변환경.Size = new Size(55, 15);
            주변환경.TabIndex = 156;
            주변환경.Text = "지형구분";
            // 
            // WP_dataGridView
            // 
            WP_dataGridView.AllowUserToAddRows = false;
            WP_dataGridView.AllowUserToDeleteRows = false;
            WP_dataGridView.AllowUserToResizeColumns = false;
            WP_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            WP_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            WP_dataGridView.BackgroundColor = Color.White;
            WP_dataGridView.BorderStyle = BorderStyle.None;
            WP_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            WP_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            WP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WP_dataGridView.Dock = DockStyle.Bottom;
            WP_dataGridView.Location = new Point(0, 127);
            WP_dataGridView.Name = "WP_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            WP_dataGridView.RowHeadersVisible = false;
            WP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            WP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            WP_dataGridView.Size = new Size(977, 149);
            WP_dataGridView.TabIndex = 155;
            // 
            // WPDB_button
            // 
            WPDB_button.BackColor = SystemColors.ControlLight;
            WPDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WPDB_button.FlatStyle = FlatStyle.System;
            WPDB_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            WPDB_button.Location = new Point(173, 18);
            WPDB_button.Margin = new Padding(0);
            WPDB_button.Name = "WPDB_button";
            WPDB_button.Size = new Size(23, 23);
            WPDB_button.TabIndex = 141;
            WPDB_button.Text = "+";
            WPDB_button.UseVisualStyleBackColor = false;
            WPDB_button.Click += WPDB_button_Click;
            // 
            // WPNameText
            // 
            WPNameText.BackColor = Color.White;
            WPNameText.BorderStyle = BorderStyle.None;
            WPNameText.Location = new Point(199, 21);
            WPNameText.Name = "WPNameText";
            WPNameText.ReadOnly = true;
            WPNameText.Size = new Size(120, 16);
            WPNameText.TabIndex = 140;
            WPNameText.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔바른고딕", 9.75F);
            label2.Location = new Point(99, 22);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 138;
            label2.Text = "풍력발전";
            // 
            // HerbHeight_textBox
            // 
            HerbHeight_textBox.BackColor = SystemColors.InactiveBorder;
            HerbHeight_textBox.BorderStyle = BorderStyle.None;
            HerbHeight_textBox.Location = new Point(1126, 183);
            HerbHeight_textBox.Name = "HerbHeight_textBox";
            HerbHeight_textBox.ReadOnly = true;
            HerbHeight_textBox.Size = new Size(50, 16);
            HerbHeight_textBox.TabIndex = 170;
            HerbHeight_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // RotateArea_textBox
            // 
            RotateArea_textBox.BackColor = SystemColors.InactiveBorder;
            RotateArea_textBox.BorderStyle = BorderStyle.None;
            RotateArea_textBox.Location = new Point(1100, 38);
            RotateArea_textBox.Name = "RotateArea_textBox";
            RotateArea_textBox.ReadOnly = true;
            RotateArea_textBox.Size = new Size(50, 16);
            RotateArea_textBox.TabIndex = 169;
            RotateArea_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = SystemColors.InactiveBorder;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Location = new Point(1064, 286);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.ReadOnly = true;
            Type_textBox.Size = new Size(120, 16);
            Type_textBox.TabIndex = 167;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FCDB_button
            // 
            FCDB_button.BackColor = SystemColors.ControlLight;
            FCDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FCDB_button.FlatStyle = FlatStyle.System;
            FCDB_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            FCDB_button.Location = new Point(327, 15);
            FCDB_button.Margin = new Padding(0);
            FCDB_button.Name = "FCDB_button";
            FCDB_button.Size = new Size(23, 23);
            FCDB_button.TabIndex = 102;
            FCDB_button.Text = "+";
            FCDB_button.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔바른고딕", 9.75F);
            label6.Location = new Point(17, 15);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 103;
            label6.Text = "풍력";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.Controls.Add(webView21);
            AdditionalPanel.Location = new Point(12, 389);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 206);
            AdditionalPanel.TabIndex = 18;
            AdditionalPanel.Paint += AdditionalPanel_Paint;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new Size(977, 206);
            webView21.TabIndex = 155;
            webView21.ZoomFactor = 1D;
            // 
            // Save_button
            // 
            Save_button.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            Save_button.Location = new Point(1064, 629);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(78, 23);
            Save_button.TabIndex = 96;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // WPtype_pictureBox
            // 
            WPtype_pictureBox.Location = new Point(1011, 20);
            WPtype_pictureBox.Name = "WPtype_pictureBox";
            WPtype_pictureBox.Size = new Size(165, 226);
            WPtype_pictureBox.TabIndex = 97;
            WPtype_pictureBox.TabStop = false;
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(970, 628);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 168;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            Previous_button.Click += Previous_button_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("나눔바른고딕", 9.75F);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(1013, 287);
            label18.Name = "label18";
            label18.Size = new Size(31, 15);
            label18.TabIndex = 171;
            label18.Text = "유형";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔바른고딕", 9.75F);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(1115, 20);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 173;
            label10.Text = "회전면적";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔바른고딕", 9.75F);
            label11.ForeColor = SystemColors.ControlDark;
            label11.Location = new Point(1133, 164);
            label11.Name = "label11";
            label11.Size = new Size(55, 15);
            label11.TabIndex = 174;
            label11.Text = "허브높이";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("나눔바른고딕", 9.75F);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(1151, 38);
            label12.Name = "label12";
            label12.Size = new Size(0, 15);
            label12.TabIndex = 175;
            // 
            // 
            // 
            // WindPower
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label18);
            Controls.Add(HerbHeight_textBox);
            Controls.Add(Previous_button);
            Controls.Add(RotateArea_textBox);
            Controls.Add(WPtype_pictureBox);
            Controls.Add(Save_button);
            Controls.Add(Type_textBox);
            Controls.Add(AdditionalPanel);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WindPower";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).EndInit();
            AdditionalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)WPtype_pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Button FCDB_button;
        private Label label6;
        private Label label1;
        private TextBox Name_textBox;
        private PictureBox pictureBox1;
        private Panel AdditionalPanel;
        private Button Save_button;
        private Label label2;
        private TextBox WPNameText;
        private Button WPDB_button;
        private DataGridView WP_dataGridView;
        private Label 주변환경;
        private TextBox h2_textBox;
        private Label label3;
        private Label label8;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private PictureBox WPtype_pictureBox;
        private TextBox Type_textBox;
        private CustomComboBox Condition_ComboBox;
        private Button Previous_button;
        private TextBox RotateArea_textBox;
        private TextBox HerbHeight_textBox;
        private Label label18;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label14;
        private TextBox Num_textBox;
        private Label label4;
        private TextBox annual_textBox;
        private Label label5;
        private Button Range_button;
    }
}