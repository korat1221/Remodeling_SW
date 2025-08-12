namespace main.subcontents.BuildingGeneral
{
    partial class Climate_info
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
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            localLabel = new Label();
            button1 = new Button();
            Cli_dataGridView = new DataGridView();
            panel1 = new Panel();
            longituLabel = new Label();
            coolTempLabel = new Label();
            heatTempLabel = new Label();
            heightLabel = new Label();
            coolRadiLabel = new Label();
            latituLabel = new Label();
            coolHumiLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Cli_dataGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(0, 49);
            webView21.Name = "webView21";
            webView21.Size = new Size(1021, 351);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // localLabel
            // 
            localLabel.AutoSize = true;
            localLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            localLabel.Location = new Point(28, 17);
            localLabel.Name = "localLabel";
            localLabel.Size = new Size(31, 15);
            localLabel.TabIndex = 1;
            localLabel.Text = "ㄹㅇ";
            localLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Location = new Point(934, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "닫기";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Cli_dataGridView
            // 
            Cli_dataGridView.AllowUserToAddRows = false;
            Cli_dataGridView.AllowUserToDeleteRows = false;
            Cli_dataGridView.AllowUserToResizeColumns = false;
            Cli_dataGridView.BackgroundColor = SystemColors.Window;
            Cli_dataGridView.BorderStyle = BorderStyle.None;
            Cli_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Cli_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            Cli_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Cli_dataGridView.Location = new Point(0, 410);
            Cli_dataGridView.Name = "Cli_dataGridView";
            Cli_dataGridView.ReadOnly = true;
            Cli_dataGridView.RowHeadersVisible = false;
            Cli_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Font = new Font("나눔고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            Cli_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            Cli_dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Cli_dataGridView.Size = new Size(1021, 213);
            Cli_dataGridView.TabIndex = 112;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(longituLabel);
            panel1.Controls.Add(coolTempLabel);
            panel1.Controls.Add(heatTempLabel);
            panel1.Controls.Add(heightLabel);
            panel1.Controls.Add(coolRadiLabel);
            panel1.Controls.Add(latituLabel);
            panel1.Controls.Add(coolHumiLabel);
            panel1.Controls.Add(localLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1021, 47);
            panel1.TabIndex = 4;
            // 
            // longituLabel
            // 
            longituLabel.AutoSize = true;
            longituLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            longituLabel.Location = new Point(239, 17);
            longituLabel.Name = "longituLabel";
            longituLabel.Size = new Size(31, 15);
            longituLabel.TabIndex = 8;
            longituLabel.Text = "경도";
            // 
            // coolTempLabel
            // 
            coolTempLabel.AutoSize = true;
            coolTempLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            coolTempLabel.Location = new Point(457, 17);
            coolTempLabel.Name = "coolTempLabel";
            coolTempLabel.Size = new Size(103, 15);
            coolTempLabel.TabIndex = 7;
            coolTempLabel.Text = "냉방설계외기온도";
            // 
            // heatTempLabel
            // 
            heatTempLabel.AutoSize = true;
            heatTempLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            heatTempLabel.Location = new Point(317, 17);
            heatTempLabel.Name = "heatTempLabel";
            heatTempLabel.Size = new Size(103, 15);
            heatTempLabel.TabIndex = 6;
            heatTempLabel.Text = "난방설계외기온도";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            heightLabel.Location = new Point(65, 17);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(55, 15);
            heightLabel.TabIndex = 5;
            heightLabel.Text = "해발고도";
            // 
            // coolRadiLabel
            // 
            coolRadiLabel.AutoSize = true;
            coolRadiLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            coolRadiLabel.Location = new Point(744, 17);
            coolRadiLabel.Name = "coolRadiLabel";
            coolRadiLabel.Size = new Size(103, 15);
            coolRadiLabel.TabIndex = 4;
            coolRadiLabel.Text = "냉방설계전일사량";
            // 
            // latituLabel
            // 
            latituLabel.AutoSize = true;
            latituLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            latituLabel.Location = new Point(165, 17);
            latituLabel.Name = "latituLabel";
            latituLabel.Size = new Size(31, 15);
            latituLabel.TabIndex = 3;
            latituLabel.Text = "위도";
            // 
            // coolHumiLabel
            // 
            coolHumiLabel.AutoSize = true;
            coolHumiLabel.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            coolHumiLabel.Location = new Point(591, 17);
            coolHumiLabel.Name = "coolHumiLabel";
            coolHumiLabel.Size = new Size(103, 15);
            coolHumiLabel.TabIndex = 2;
            coolHumiLabel.Text = "냉방설계절대습도";
            // 
            // Climate_info
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1021, 617);
            Controls.Add(Cli_dataGridView);
            Controls.Add(button1);
            Controls.Add(webView21);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Climate_info";
            Text = "Climate_info";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)Cli_dataGridView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Label localLabel;
        private Button button1;
        private DataGridView Cli_dataGridView;
        private Panel panel1;
        private Label longituLabel;
        private Label coolTempLabel;
        private Label heatTempLabel;
        private Label heightLabel;
        private Label coolRadiLabel;
        private Label latituLabel;
        private Label coolHumiLabel;
    }
}