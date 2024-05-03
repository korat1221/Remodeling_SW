namespace main.subcontents.Alt
{
    partial class AltWall
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
            Name_textBox = new TextBox();
            label1 = new Label();
            TotalPoint_textBox = new TextBox();
            EnergyPoint_textBox = new TextBox();
            label3 = new Label();
            Energy_textBox = new TextBox();
            CO2Point_textBox = new TextBox();
            label5 = new Label();
            CO2_textBox = new TextBox();
            RulePoint_textBox = new TextBox();
            label8 = new Label();
            Rule_textBox = new TextBox();
            MoneyPoint_textBox = new TextBox();
            label10 = new Label();
            Money_textBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(745, 531);
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
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Ucalc_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Ucalc_dataGridView.Location = new Point(-2, 145);
            Ucalc_dataGridView.Name = "Ucalc_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Ucalc_dataGridView.RowHeadersVisible = false;
            Ucalc_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Ucalc_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Ucalc_dataGridView.RowTemplate.Height = 25;
            Ucalc_dataGridView.Size = new Size(534, 189);
            Ucalc_dataGridView.TabIndex = 99;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(538, -3);
            webView21.Name = "webView21";
            webView21.Size = new Size(354, 337);
            webView21.Source = new Uri("http://localhost:3000/transmit.html", UriKind.Absolute);
            webView21.TabIndex = 100;
            webView21.ZoomFactor = 1D;
            // 
            // Alt_dataGridView
            // 
            Alt_dataGridView.AllowUserToAddRows = false;
            Alt_dataGridView.AllowUserToDeleteRows = false;
            Alt_dataGridView.AllowUserToResizeColumns = false;
            Alt_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Alt_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Alt_dataGridView.BackgroundColor = SystemColors.Control;
            Alt_dataGridView.BorderStyle = BorderStyle.None;
            Alt_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Alt_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            Alt_dataGridView.RowHeadersVisible = false;
            Alt_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Alt_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            Alt_dataGridView.RowTemplate.Height = 25;
            Alt_dataGridView.Size = new Size(891, 184);
            Alt_dataGridView.TabIndex = 101;
            Alt_dataGridView.CellContentClick += Alt_dataGridView_CellContentClick;
            // 
            // Name_textBox
            // 
            Name_textBox.BackColor = Color.White;
            Name_textBox.BorderStyle = BorderStyle.None;
            Name_textBox.Enabled = false;
            Name_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Name_textBox.ForeColor = SystemColors.ControlText;
            Name_textBox.Location = new Point(12, 19);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(371, 15);
            Name_textBox.TabIndex = 153;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(155, 124);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 156;
            label1.Text = "종합점수";
            // 
            // TotalPoint_textBox
            // 
            TotalPoint_textBox.BackColor = Color.White;
            TotalPoint_textBox.BorderStyle = BorderStyle.None;
            TotalPoint_textBox.Enabled = false;
            TotalPoint_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TotalPoint_textBox.ForeColor = SystemColors.ControlDark;
            TotalPoint_textBox.Location = new Point(272, 123);
            TotalPoint_textBox.Name = "TotalPoint_textBox";
            TotalPoint_textBox.Size = new Size(120, 15);
            TotalPoint_textBox.TabIndex = 155;
            TotalPoint_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // EnergyPoint_textBox
            // 
            EnergyPoint_textBox.BackColor = Color.White;
            EnergyPoint_textBox.BorderStyle = BorderStyle.None;
            EnergyPoint_textBox.Enabled = false;
            EnergyPoint_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EnergyPoint_textBox.ForeColor = SystemColors.ControlDark;
            EnergyPoint_textBox.Location = new Point(272, 39);
            EnergyPoint_textBox.Name = "EnergyPoint_textBox";
            EnergyPoint_textBox.Size = new Size(120, 15);
            EnergyPoint_textBox.TabIndex = 159;
            EnergyPoint_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 40);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 158;
            label3.Text = "에너지절감률";
            // 
            // Energy_textBox
            // 
            Energy_textBox.BackColor = Color.White;
            Energy_textBox.BorderStyle = BorderStyle.None;
            Energy_textBox.Enabled = false;
            Energy_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Energy_textBox.ForeColor = SystemColors.ControlDark;
            Energy_textBox.Location = new Point(122, 40);
            Energy_textBox.Name = "Energy_textBox";
            Energy_textBox.Size = new Size(120, 15);
            Energy_textBox.TabIndex = 157;
            Energy_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // CO2Point_textBox
            // 
            CO2Point_textBox.BackColor = Color.White;
            CO2Point_textBox.BorderStyle = BorderStyle.None;
            CO2Point_textBox.Enabled = false;
            CO2Point_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CO2Point_textBox.ForeColor = SystemColors.ControlDark;
            CO2Point_textBox.Location = new Point(272, 60);
            CO2Point_textBox.Name = "CO2Point_textBox";
            CO2Point_textBox.Size = new Size(120, 15);
            CO2Point_textBox.TabIndex = 163;
            CO2Point_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 61);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 162;
            label5.Text = "탄소절감률";
            // 
            // CO2_textBox
            // 
            CO2_textBox.BackColor = Color.White;
            CO2_textBox.BorderStyle = BorderStyle.None;
            CO2_textBox.Enabled = false;
            CO2_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CO2_textBox.ForeColor = SystemColors.ControlDark;
            CO2_textBox.Location = new Point(122, 61);
            CO2_textBox.Name = "CO2_textBox";
            CO2_textBox.Size = new Size(120, 15);
            CO2_textBox.TabIndex = 161;
            CO2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // RulePoint_textBox
            // 
            RulePoint_textBox.BackColor = Color.White;
            RulePoint_textBox.BorderStyle = BorderStyle.None;
            RulePoint_textBox.Enabled = false;
            RulePoint_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            RulePoint_textBox.ForeColor = SystemColors.ControlDark;
            RulePoint_textBox.Location = new Point(272, 81);
            RulePoint_textBox.Name = "RulePoint_textBox";
            RulePoint_textBox.Size = new Size(120, 15);
            RulePoint_textBox.TabIndex = 167;
            RulePoint_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(12, 82);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 166;
            label8.Text = "법규 대비 성능";
            // 
            // Rule_textBox
            // 
            Rule_textBox.BackColor = Color.White;
            Rule_textBox.BorderStyle = BorderStyle.None;
            Rule_textBox.Enabled = false;
            Rule_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Rule_textBox.ForeColor = SystemColors.ControlDark;
            Rule_textBox.Location = new Point(102, 82);
            Rule_textBox.Name = "Rule_textBox";
            Rule_textBox.Size = new Size(161, 15);
            Rule_textBox.TabIndex = 165;
            Rule_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // MoneyPoint_textBox
            // 
            MoneyPoint_textBox.BackColor = Color.White;
            MoneyPoint_textBox.BorderStyle = BorderStyle.None;
            MoneyPoint_textBox.Enabled = false;
            MoneyPoint_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MoneyPoint_textBox.ForeColor = SystemColors.ControlDark;
            MoneyPoint_textBox.Location = new Point(272, 102);
            MoneyPoint_textBox.Name = "MoneyPoint_textBox";
            MoneyPoint_textBox.Size = new Size(120, 15);
            MoneyPoint_textBox.TabIndex = 171;
            MoneyPoint_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(12, 103);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 170;
            label10.Text = "예상 공사비";
            // 
            // Money_textBox
            // 
            Money_textBox.BackColor = Color.White;
            Money_textBox.BorderStyle = BorderStyle.None;
            Money_textBox.Enabled = false;
            Money_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Money_textBox.ForeColor = SystemColors.ControlDark;
            Money_textBox.Location = new Point(122, 103);
            Money_textBox.Name = "Money_textBox";
            Money_textBox.Size = new Size(120, 15);
            Money_textBox.TabIndex = 169;
            Money_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // AltWall
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(892, 568);
            Controls.Add(MoneyPoint_textBox);
            Controls.Add(label10);
            Controls.Add(Money_textBox);
            Controls.Add(RulePoint_textBox);
            Controls.Add(label8);
            Controls.Add(Rule_textBox);
            Controls.Add(CO2Point_textBox);
            Controls.Add(label5);
            Controls.Add(CO2_textBox);
            Controls.Add(EnergyPoint_textBox);
            Controls.Add(label3);
            Controls.Add(Energy_textBox);
            Controls.Add(label1);
            Controls.Add(TotalPoint_textBox);
            Controls.Add(Name_textBox);
            Controls.Add(Alt_dataGridView);
            Controls.Add(Ucalc_dataGridView);
            Controls.Add(webView21);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AltWall";
            Text = "Review of Alternatives";
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Save_button;
        private TextBox d_ins_textBox;
        private DataGridView Ucalc_dataGridView;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private DataGridView Alt_dataGridView;
        private TextBox Name_textBox;
        private Label label1;
        private TextBox TotalPoint_textBox;
        private Label label2;
        private TextBox EnergyPoint_textBox;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private TextBox CO2Point_textBox;
        private Label label5;
        private TextBox textBox5;
        private Label label6;
        private TextBox RulePoint_textBox;
        private Label label8;
        private TextBox textBox7;
        private Label label9;
        private TextBox MoneyPoint_textBox;
        private Label label10;
        private TextBox Money_textBox;
        private TextBox Energy_textBox;
        private TextBox CO2_textBox;
        private TextBox Rule_textBox;
    }
}