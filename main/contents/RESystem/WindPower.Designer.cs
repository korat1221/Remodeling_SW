
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
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            label1 = new Label();
            Num_textBox = new TextBox();
            Name_textBox = new TextBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            FCDB_button = new Button();
            label6 = new Label();
            label5 = new Label();
            AdditionalPanel = new Panel();
            Save_button = new Button();
            label2 = new Label();
            WPNameText = new TextBox();
            WPDB_button = new Button();
            WP_dataGridView = new DataGridView();
            주변환경 = new Label();
            FCTypeComboBox = new ComboBox();
            label3 = new Label();
            textBox1 = new TextBox();
            label8 = new Label();
            label4 = new Label();
            button1 = new Button();
            textBox2 = new TextBox();
            label7 = new Label();
            textBox3 = new TextBox();
            label9 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            WPtype_pictureBox = new PictureBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WPtype_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font(UTIL.Families[0], 9.75F);
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
            Num_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(75, 46);
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
            // pictureBox1
            // 
            pictureBox1.Location = new Point(11, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 134;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(FCTypeComboBox);
            panel2.Controls.Add(주변환경);
            panel2.Controls.Add(WP_dataGridView);
            panel2.Controls.Add(WPDB_button);
            panel2.Controls.Add(WPNameText);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 99);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 276);
            panel2.TabIndex = 18;
            // 
            // FCDB_button
            // 
            FCDB_button.BackColor = SystemColors.ControlLight;
            FCDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FCDB_button.FlatStyle = FlatStyle.System;
            FCDB_button.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Bold);
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
            label6.Font = new Font(UTIL.Families[0], 9.75F);
            label6.Location = new Point(17, 15);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 103;
            label6.Text = "풍력";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label5.Location = new Point(12, 81);
            label5.Name = "label5";
            label5.Size = new Size(73, 16);
            label5.TabIndex = 95;
            label5.Text = "구성요소정보";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(webView21);
            AdditionalPanel.Location = new Point(12, 381);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 206);
            AdditionalPanel.TabIndex = 18;
            // 
            // Save_button
            // 
            Save_button.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            Save_button.Location = new Point(1004, 564);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(78, 23);
            Save_button.TabIndex = 96;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font(UTIL.Families[0], 9.75F);
            label2.Location = new Point(99, 12);
            label2.Name = "label2";
            label2.Size = new Size(65, 19);
            label2.TabIndex = 138;
            label2.Text = "풍력발전";
            // 
            // WPNameText
            // 
            WPNameText.BackColor = Color.White;
            WPNameText.BorderStyle = BorderStyle.None;
            WPNameText.Location = new Point(173, 13);
            WPNameText.Name = "WPNameText";
            WPNameText.ReadOnly = true;
            WPNameText.Size = new Size(120, 16);
            WPNameText.TabIndex = 140;
            WPNameText.TextAlign = HorizontalAlignment.Center;
            // 
            // WPDB_button
            // 
            WPDB_button.BackColor = SystemColors.ControlLight;
            WPDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            WPDB_button.FlatStyle = FlatStyle.System;
            WPDB_button.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Bold);
            WPDB_button.Location = new Point(296, 8);
            WPDB_button.Margin = new Padding(0);
            WPDB_button.Name = "WPDB_button";
            WPDB_button.Size = new Size(23, 23);
            WPDB_button.TabIndex = 141;
            WPDB_button.Text = "+";
            WPDB_button.UseVisualStyleBackColor = false;
            WPDB_button.Click += WPDB_button_Click;
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
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            WP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WP_dataGridView.Location = new Point(11, 176);
            WP_dataGridView.Name = "WP_dataGridView";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = SystemColors.Control;
            dataGridViewCellStyle11.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            WP_dataGridView.RowHeadersVisible = false;
            WP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle12.ForeColor = Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            WP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            WP_dataGridView.Size = new Size(943, 97);
            WP_dataGridView.TabIndex = 155;
            // 
            // 주변환경
            // 
            주변환경.AutoSize = true;
            주변환경.Font = new Font(UTIL.Families[0], 9.75F);
            주변환경.Location = new Point(99, 44);
            주변환경.Name = "주변환경";
            주변환경.Size = new Size(65, 19);
            주변환경.TabIndex = 156;
            주변환경.Text = "주변환경";
            // 
            // FCTypeComboBox
            // 
            FCTypeComboBox.FormattingEnabled = true;
            FCTypeComboBox.Location = new Point(163, 42);
            FCTypeComboBox.Name = "FCTypeComboBox";
            FCTypeComboBox.Size = new Size(121, 23);
            FCTypeComboBox.TabIndex = 157;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font(UTIL.Families[0], 9.75F);
            label3.Location = new Point(435, 44);
            label3.Name = "label3";
            label3.Size = new Size(93, 19);
            label3.TabIndex = 158;
            label3.Text = "터빈설치높이";
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(534, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 23);
            textBox1.TabIndex = 159;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font(UTIL.Families[0], 9.75F);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(660, 48);
            label8.Name = "label8";
            label8.Size = new Size(19, 17);
            label8.TabIndex = 160;
            label8.Text = "m";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font(UTIL.Families[0], 9.75F);
            label4.Location = new Point(99, 82);
            label4.Name = "label4";
            label4.Size = new Size(84, 19);
            label4.TabIndex = 161;
            label4.Text = "인버터 제품";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            button1.FlatStyle = FlatStyle.System;
            button1.Font = new Font(UTIL.Families[0], 9.75F,FontStyle.Bold);
            button1.Location = new Point(322, 77);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(23, 23);
            button1.TabIndex = 163;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(199, 82);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(120, 16);
            textBox2.TabIndex = 162;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font(UTIL.Families[0], 9.75F);
            label7.Location = new Point(444, 82);
            label7.Name = "label7";
            label7.Size = new Size(37, 19);
            label7.TabIndex = 164;
            label7.Text = "효율";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.White;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(534, 85);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(120, 16);
            textBox3.TabIndex = 165;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font(UTIL.Families[0], 9.75F);
            label9.ForeColor = SystemColors.ControlDark;
            label9.Location = new Point(660, 85);
            label9.Name = "label9";
            label9.Size = new Size(20, 17);
            label9.TabIndex = 166;
            label9.Text = "%";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(12, 13);
            webView21.Name = "webView21";
            webView21.Size = new Size(940, 177);
            webView21.TabIndex = 155;
            webView21.ZoomFactor = 1D;
            // 
            // WPtype_pictureBox
            // 
            WPtype_pictureBox.Location = new Point(1004, 12);
            WPtype_pictureBox.Name = "WPtype_pictureBox";
            WPtype_pictureBox.Size = new Size(184, 254);
            WPtype_pictureBox.TabIndex = 97;
            WPtype_pictureBox.TabStop = false;
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.InactiveBorder;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Location = new Point(938, 119);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(120, 16);
            textBox4.TabIndex = 141;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.BackColor = SystemColors.InactiveBorder;
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Location = new Point(950, 185);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(120, 16);
            textBox5.TabIndex = 167;
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // WindPower
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(textBox5);
            Controls.Add(WPtype_pictureBox);
            Controls.Add(Save_button);
            Controls.Add(AdditionalPanel);
            Controls.Add(label5);
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
            AdditionalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).EndInit();
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
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private PictureBox pictureBox1;
        private Label label5;
        private Panel AdditionalPanel;
        private Button Save_button;
        private Label label2;
        private TextBox WPNameText;
        private Button WPDB_button;
        private DataGridView WP_dataGridView;
        private Label 주변환경;
        private ComboBox FCTypeComboBox;
        private TextBox textBox1;
        private Label label3;
        private Label label8;
        private Label label9;
        private TextBox textBox3;
        private Label label7;
        private Button button1;
        private TextBox textBox2;
        private Label label4;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private PictureBox WPtype_pictureBox;
        private TextBox textBox4;
        private TextBox textBox5;
    }
}