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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            PVMainPanel = new Panel();
            Zone_textBox = new TextBox();
            label7 = new Label();
            Zone_Qmax_textBox = new TextBox();
            Zone_Qba_textBox = new TextBox();
            label21 = new Label();
            label22 = new Label();
            label10 = new Label();
            SystemNum_textBox = new TextBox();
            panel4 = new Panel();
            radioButton4 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton1 = new RadioButton();
            label8 = new Label();
            label9 = new Label();
            label11 = new Label();
            label3 = new Label();
            Num_textBox = new TextBox();
            pictureBox1 = new PictureBox();
            Save_button = new Button();
            label28 = new Label();
            elec_textBox = new TextBox();
            label27 = new Label();
            panel2 = new Panel();
            label6 = new Label();
            FC_dataGridView = new DataGridView();
            tabControl1 = new CustomTabControl();
            PVCalc_tabPage = new TabPage();
            label24 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new Panel();
            label2 = new Label();
            heat_textBox = new TextBox();
            label4 = new Label();
            label1 = new Label();
            gas_textBox = new TextBox();
            label5 = new Label();
            PVMainPanel.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            PVCalc_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // PVMainPanel
            // 
            PVMainPanel.BackColor = SystemColors.GradientActiveCaption;
            PVMainPanel.Controls.Add(Zone_textBox);
            PVMainPanel.Controls.Add(label7);
            PVMainPanel.Controls.Add(Zone_Qmax_textBox);
            PVMainPanel.Controls.Add(Zone_Qba_textBox);
            PVMainPanel.Controls.Add(label21);
            PVMainPanel.Controls.Add(label22);
            PVMainPanel.Controls.Add(label10);
            PVMainPanel.Controls.Add(SystemNum_textBox);
            PVMainPanel.Controls.Add(panel4);
            PVMainPanel.Controls.Add(label3);
            PVMainPanel.Controls.Add(Num_textBox);
            PVMainPanel.Controls.Add(pictureBox1);
            PVMainPanel.Location = new Point(0, 4);
            PVMainPanel.Name = "PVMainPanel";
            PVMainPanel.Size = new Size(1000, 80);
            PVMainPanel.TabIndex = 17;
            // 
            // Zone_textBox
            // 
            Zone_textBox.BackColor = SystemColors.GradientActiveCaption;
            Zone_textBox.BorderStyle = BorderStyle.None;
            Zone_textBox.Enabled = false;
            Zone_textBox.Font = new Font("나눔바른고딕", 9.75F);
            Zone_textBox.ForeColor = SystemColors.ControlDark;
            Zone_textBox.Location = new Point(258, 52);
            Zone_textBox.Name = "Zone_textBox";
            Zone_textBox.Size = new Size(120, 15);
            Zone_textBox.TabIndex = 165;
            Zone_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔바른고딕", 10F);
            label7.Location = new Point(295, 16);
            label7.Name = "label7";
            label7.Size = new Size(46, 16);
            label7.TabIndex = 164;
            label7.Text = "공급 존";
            // 
            // Zone_Qmax_textBox
            // 
            Zone_Qmax_textBox.BackColor = SystemColors.GradientActiveCaption;
            Zone_Qmax_textBox.BorderStyle = BorderStyle.None;
            Zone_Qmax_textBox.Enabled = false;
            Zone_Qmax_textBox.Font = new Font("나눔바른고딕", 9.75F);
            Zone_Qmax_textBox.ForeColor = SystemColors.ControlDark;
            Zone_Qmax_textBox.Location = new Point(552, 52);
            Zone_Qmax_textBox.Name = "Zone_Qmax_textBox";
            Zone_Qmax_textBox.Size = new Size(120, 15);
            Zone_Qmax_textBox.TabIndex = 163;
            Zone_Qmax_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Zone_Qba_textBox
            // 
            Zone_Qba_textBox.BackColor = SystemColors.GradientActiveCaption;
            Zone_Qba_textBox.BorderStyle = BorderStyle.None;
            Zone_Qba_textBox.Enabled = false;
            Zone_Qba_textBox.Font = new Font("나눔바른고딕", 9.75F);
            Zone_Qba_textBox.ForeColor = SystemColors.ControlDark;
            Zone_Qba_textBox.Location = new Point(405, 52);
            Zone_Qba_textBox.Name = "Zone_Qba_textBox";
            Zone_Qba_textBox.Size = new Size(120, 15);
            Zone_Qba_textBox.TabIndex = 162;
            Zone_Qba_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("나눔바른고딕", 10F);
            label21.Location = new Point(567, 16);
            label21.Name = "label21";
            label21.Size = new Size(90, 16);
            label21.TabIndex = 160;
            label21.Text = "최대부하 [kW]";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("나눔바른고딕", 10F);
            label22.Location = new Point(384, 16);
            label22.Name = "label22";
            label22.Size = new Size(163, 16);
            label22.TabIndex = 159;
            label22.Text = "연간 에너지요구량 [kWh/a]";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Cursor = Cursors.IBeam;
            label10.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label10.ForeColor = Color.White;
            label10.Location = new Point(114, 53);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 137;
            label10.Text = "설비번호";
            // 
            // SystemNum_textBox
            // 
            SystemNum_textBox.BackColor = SystemColors.GradientActiveCaption;
            SystemNum_textBox.BorderStyle = BorderStyle.None;
            SystemNum_textBox.Enabled = false;
            SystemNum_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            SystemNum_textBox.ForeColor = Color.White;
            SystemNum_textBox.Location = new Point(168, 52);
            SystemNum_textBox.Name = "SystemNum_textBox";
            SystemNum_textBox.Size = new Size(75, 15);
            SystemNum_textBox.TabIndex = 136;
            SystemNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel4
            // 
            panel4.Controls.Add(radioButton4);
            panel4.Controls.Add(radioButton2);
            panel4.Controls.Add(radioButton3);
            panel4.Controls.Add(radioButton1);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label11);
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(683, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(317, 80);
            panel4.TabIndex = 135;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton4.ForeColor = Color.White;
            radioButton4.Location = new Point(137, 50);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(91, 19);
            radioButton4.TabIndex = 2;
            radioButton4.TabStop = true;
            radioButton4.Text = "철거 후 신규";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton2.ForeColor = Color.White;
            radioButton2.Location = new Point(70, 30);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(49, 19);
            radioButton2.TabIndex = 3;
            radioButton2.TabStop = true;
            radioButton2.Text = "보수";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton3.ForeColor = Color.White;
            radioButton3.Location = new Point(70, 50);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(49, 19);
            radioButton3.TabIndex = 1;
            radioButton3.TabStop = true;
            radioButton3.Text = "신규";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            radioButton1.ForeColor = Color.White;
            radioButton1.Location = new Point(70, 9);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label8.ForeColor = Color.White;
            label8.Location = new Point(13, 11);
            label8.Name = "label8";
            label8.Size = new Size(34, 15);
            label8.TabIndex = 130;
            label8.Text = "기 존";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(13, 52);
            label9.Name = "label9";
            label9.Size = new Size(34, 15);
            label9.TabIndex = 129;
            label9.Text = "신 규";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label11.ForeColor = Color.White;
            label11.Location = new Point(13, 32);
            label11.Name = "label11";
            label11.Size = new Size(34, 15);
            label11.TabIndex = 128;
            label11.Text = "보 수";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.IBeam;
            label3.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.White;
            label3.Location = new Point(114, 18);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 134;
            label3.Text = "번  호";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = SystemColors.GradientActiveCaption;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Num_textBox.ForeColor = Color.White;
            Num_textBox.Location = new Point(168, 17);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(75, 15);
            Num_textBox.TabIndex = 132;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(18, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 90;
            pictureBox1.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1006, 638);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 99;
            Save_button.Text = "BACK";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("나눔바른고딕", 9.75F);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(1148, 28);
            label28.Name = "label28";
            label28.Size = new Size(53, 15);
            label28.TabIndex = 109;
            label28.Text = "kWh/년";
            // 
            // elec_textBox
            // 
            elec_textBox.BackColor = SystemColors.InactiveBorder;
            elec_textBox.BorderStyle = BorderStyle.None;
            elec_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            elec_textBox.ForeColor = Color.Black;
            elec_textBox.Location = new Point(1085, 28);
            elec_textBox.Name = "elec_textBox";
            elec_textBox.Size = new Size(60, 15);
            elec_textBox.TabIndex = 127;
            elec_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔바른고딕", 9.75F);
            label27.Location = new Point(1006, 28);
            label27.Name = "label27";
            label27.Size = new Size(67, 15);
            label27.TabIndex = 127;
            label27.Text = "전기생산량";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(FC_dataGridView);
            panel2.Location = new Point(0, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 280);
            panel2.TabIndex = 139;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(13, 16);
            label6.Name = "label6";
            label6.Size = new Size(82, 15);
            label6.TabIndex = 136;
            label6.Text = "연료전지 정보";
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            FC_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FC_dataGridView.Location = new Point(0, 56);
            FC_dataGridView.Name = "FC_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            FC_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            FC_dataGridView.RowHeadersVisible = false;
            FC_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            FC_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            FC_dataGridView.Size = new Size(939, 106);
            FC_dataGridView.TabIndex = 195;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(PVCalc_tabPage);
            tabControl1.DisplayStyleProvider.BorderColor = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.BorderColorHot = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.CloserColor = Color.Empty;
            tabControl1.DisplayStyleProvider.FocusTrack = true;
            tabControl1.DisplayStyleProvider.HotTrack = true;
            tabControl1.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;
            tabControl1.DisplayStyleProvider.Opacity = 1F;
            tabControl1.DisplayStyleProvider.Overlap = 0;
            tabControl1.DisplayStyleProvider.Padding = new Point(6, 3);
            tabControl1.DisplayStyleProvider.ShowTabCloser = false;
            tabControl1.DisplayStyleProvider.TextColor = SystemColors.ControlText;
            tabControl1.DisplayStyleProvider.TextColorDisabled = SystemColors.ControlDark;
            tabControl1.DisplayStyleProvider.TextColorSelected = SystemColors.ControlText;
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 350);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 140;
            // 
            // PVCalc_tabPage
            // 
            PVCalc_tabPage.BackColor = Color.White;
            PVCalc_tabPage.Controls.Add(label24);
            PVCalc_tabPage.Controls.Add(webView21);
            PVCalc_tabPage.Location = new Point(4, 25);
            PVCalc_tabPage.Name = "PVCalc_tabPage";
            PVCalc_tabPage.Padding = new Padding(3);
            PVCalc_tabPage.Size = new Size(992, 321);
            PVCalc_tabPage.TabIndex = 1;
            PVCalc_tabPage.Text = "에너지 정보";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("나눔바른고딕", 9.75F);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(935, 3);
            label24.Name = "label24";
            label24.Size = new Size(0, 15);
            label24.TabIndex = 163;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Bottom;
            webView21.Location = new Point(3, 21);
            webView21.Name = "webView21";
            webView21.Size = new Size(986, 297);
            webView21.TabIndex = 154;
            webView21.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tabControl1);
            panel1.Location = new Point(0, 313);
            panel1.Name = "panel1";
            panel1.Size = new Size(1000, 350);
            panel1.TabIndex = 141;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔바른고딕", 9.75F);
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(1148, 56);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 144;
            label2.Text = "kWh/년";
            // 
            // heat_textBox
            // 
            heat_textBox.BackColor = SystemColors.InactiveBorder;
            heat_textBox.BorderStyle = BorderStyle.None;
            heat_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            heat_textBox.ForeColor = Color.Black;
            heat_textBox.Location = new Point(1085, 56);
            heat_textBox.Name = "heat_textBox";
            heat_textBox.Size = new Size(60, 15);
            heat_textBox.TabIndex = 143;
            heat_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F);
            label4.Location = new Point(1006, 56);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 142;
            label4.Text = "열생산량";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F);
            label1.ForeColor = SystemColors.ControlDark;
            label1.Location = new Point(1148, 84);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 147;
            label1.Text = "kWh/년";
            // 
            // gas_textBox
            // 
            gas_textBox.BackColor = SystemColors.InactiveBorder;
            gas_textBox.BorderStyle = BorderStyle.None;
            gas_textBox.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            gas_textBox.ForeColor = Color.Black;
            gas_textBox.Location = new Point(1085, 84);
            gas_textBox.Name = "gas_textBox";
            gas_textBox.Size = new Size(60, 15);
            gas_textBox.TabIndex = 146;
            gas_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔바른고딕", 9.75F);
            label5.Location = new Point(1006, 84);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 145;
            label5.Text = "연료소비량";
            // 
            // FuelCell
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 896);
            Controls.Add(label1);
            Controls.Add(gas_textBox);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(heat_textBox);
            Controls.Add(label4);
            Controls.Add(label28);
            Controls.Add(elec_textBox);
            Controls.Add(label27);
            Controls.Add(Save_button);
            Controls.Add(PVMainPanel);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FuelCell";
            Text = "Form3";
            PVMainPanel.ResumeLayout(false);
            PVMainPanel.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FC_dataGridView).EndInit();
            tabControl1.ResumeLayout(false);
            PVCalc_tabPage.ResumeLayout(false);
            PVCalc_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel PVMainPanel;
        private PictureBox pictureBox1;
        private Label 계통유형;
        private Label label6;
        private Label Battery_label;
        private Label label7;
        private TextBox Battery_textBox;
        private TextBox Inverter_textBox;
        private TextBox PVModule_textBox;
        private Button InverterDB1_button;
        private Button Save_button;
        private Label label28;
        private TextBox elec_textBox;
        private Label label27;
        private Panel panel2;
        private DataGridView PV_dataGridView;
        private Label label8;
        private CustomComboBox Oldsystem_comboBox;
        private Label label9;
        private Label label11;
        private RadioButton radioButton2;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton1;
        private Label label1;
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private CustomComboBox PVsystem_combobox;
        private Label label3;
        private Panel panel4;
        private Button PVModuleDB_button;
        private Button BatteryDB_button;
        private Button InverterDB_button;
        private CustomComboBox OldPVSystem_ComboBox;
        private CustomComboBox PVType_ComboBox;
        private CustomTabControl tabControl1;
        private TabPage PVinstall_tabPage;
        private TextBox DoorH2_textBox;
        private Label label18;
        private TabPage PVCalc_tabPage;
        private Label label24;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private TextBox PVMoudle_textBox;
        private Panel panel1;
        private Panel panel3;
        private PictureBox PVpictureBox;
        private PictureBox PVTypepictureBox;
        private Label pvname;
        private Label pvtotal;
        private Label pvpower;
        private Label pvsize;
        private Panel panel5;
        private Label InverterEff_textbox;
        private Label BatteryEff_textbox;
        private Label batterypower;
        private PictureBox STTypepictureBox;
        private DataGridView FC_dataGridView;
        private Label label2;
        private TextBox heat_textBox;
        private Label label4;
        private TextBox gas_textBox;
        private Label label5;
        private Label label10;
        private TextBox SystemNum_textBox;
        private TextBox Zone_Qmax_textBox;
        private TextBox Zone_Qba_textBox;
        private Label label21;
        private Label label22;
        private TextBox Zone_textBox;
    }
}