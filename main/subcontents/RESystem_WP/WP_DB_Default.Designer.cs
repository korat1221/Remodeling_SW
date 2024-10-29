namespace main.subcontents.RESystem_WP
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            pictureBox8 = new PictureBox();
            pictureBox7 = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label1 = new Label();
            panel1 = new Panel();
            Save_button = new Button();
            WP_dataGridView = new DataGridView();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(pictureBox8);
            GeneralPanel.Controls.Add(pictureBox7);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(pictureBox6);
            GeneralPanel.Controls.Add(pictureBox5);
            GeneralPanel.Controls.Add(pictureBox4);
            GeneralPanel.Controls.Add(pictureBox3);
            GeneralPanel.Controls.Add(pictureBox2);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(textBox6);
            GeneralPanel.Controls.Add(textBox5);
            GeneralPanel.Controls.Add(textBox4);
            GeneralPanel.Controls.Add(textBox3);
            GeneralPanel.Controls.Add(textBox2);
            GeneralPanel.Controls.Add(textBox1);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(panel1);
            GeneralPanel.Location = new Point(0, -2);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1055, 636);
            GeneralPanel.TabIndex = 18;
            // 
            // pictureBox8
            // 
            pictureBox8.Location = new Point(379, 395);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(299, 175);
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.TabIndex = 35;
            pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Location = new Point(36, 366);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(250, 204);
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.TabIndex = 34;
            pictureBox7.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(343, 291);
            label3.Name = "label3";
            label3.Size = new Size(632, 75);
            label3.TabIndex = 33;
            label3.Text = "<전력계수>\r\n기본 값은 0.2로 한다.\r\n시험 값을 적용하고자 한다면 시동풍속 지점 전력계수, 최적풍속 지점 전력계수, 종단풍속 지점 전력계수를 입력한다.\r\n입력한 값을 선형보간하여 풍속에 따른 전력계수 값을 적용하여 계산한다. \r\n전력계수에 대한 값을 입력하지 않으면 0.2로 자동 적용된다.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 291);
            label2.Name = "label2";
            label2.Size = new Size(280, 60);
            label2.TabIndex = 32;
            label2.Text = "<시동풍속, 종단풍속>\r\n: 1m/s 단위로 입력하시오.\r\n- 제품 값이 없는 경우, \r\n시동풍속 : 4m/s / 종단풍속 : 16m/s를 입력하시오.";
            // 
            // pictureBox6
            // 
            pictureBox6.Location = new Point(919, 88);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(100, 167);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 31;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(344, 88);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(192, 167);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 30;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(795, 88);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 167);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 29;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(667, 88);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(100, 167);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 28;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(549, 88);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 167);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 27;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(21, 70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 196);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 26;
            pictureBox1.TabStop = false;
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.GhostWhite;
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.Location = new Point(905, 51);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 23);
            textBox6.TabIndex = 25;
            textBox6.Text = "복합형";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.GhostWhite;
            textBox5.BorderStyle = BorderStyle.FixedSingle;
            textBox5.Location = new Point(781, 51);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(125, 23);
            textBox5.TabIndex = 24;
            textBox5.Text = "H-Blade";
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.GhostWhite;
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.Location = new Point(657, 51);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 23);
            textBox4.TabIndex = 23;
            textBox4.Text = "다리우스";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.GhostWhite;
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(533, 51);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 23);
            textBox3.TabIndex = 22;
            textBox3.Text = "사보니우스";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.GhostWhite;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(533, 29);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(497, 23);
            textBox2.TabIndex = 21;
            textBox2.Text = "수직형";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.GhostWhite;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(343, 29);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 45);
            textBox1.TabIndex = 20;
            textBox1.Text = "\r\n수평형";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 37);
            label1.Name = "label1";
            label1.Size = new Size(174, 15);
            label1.TabIndex = 0;
            label1.Text = "<타입 및 회전면적, 허브 높이>\r\n";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(343, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(687, 237);
            panel1.TabIndex = 36;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(905, 794);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            WP_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WP_dataGridView.Location = new Point(0, 599);
            WP_dataGridView.Name = "WP_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            WP_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            WP_dataGridView.RowHeadersVisible = false;
            WP_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            WP_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            WP_dataGridView.Size = new Size(1052, 151);
            WP_dataGridView.TabIndex = 21;
            WP_dataGridView.CellContentClick += WP_dataGridView_CellContentClick;
            // 
            // WP_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1052, 831);
            Controls.Add(WP_dataGridView);
            Controls.Add(Save_button);
            Controls.Add(GeneralPanel);
            Name = "WP_DB";
            Text = "WP_DB";
            Load += WP_DB_Load;
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)WP_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private Button Save_button;
        private TextBox textBox1;
        private TextBox textBox6;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox8;
        private PictureBox pictureBox7;
        private DataGridView WP_dataGridView;
        private Panel panel1;
    }
}