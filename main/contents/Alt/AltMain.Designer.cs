namespace main.contents.Alt
{
    partial class AltMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltMain));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            AltMainPanel = new Panel();
            label1 = new Label();
            Type_textBox = new TextBox();
            Icon_pictureBox = new PictureBox();
            Save_button = new Button();
            panel5 = new Panel();
            label2 = new Label();
            Boiler_dataGridView = new DataGridView();
            Alt_Remove_button = new Button();
            Alt_Add_button = new Button();
            q50_textBox = new TextBox();
            q50_label2 = new Label();
            q50_label1 = new Label();
            label4 = new Label();
            tabControl1 = new CustomTabControl();
            Main_tabPage = new TabPage();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Wall_tabPage = new TabPage();
            Rse_textBox = new TextBox();
            label12 = new Label();
            Rsi_textBox = new TextBox();
            label10 = new Label();
            Material_Rtot_textBox = new TextBox();
            Material_dtot_textBox = new TextBox();
            label8 = new Label();
            DiIndi2_comboBox = new CustomComboBox();
            label3 = new Label();
            ISO_KS_comboBox = new CustomComboBox();
            MaterialDown_button = new Button();
            MaterialUP_button = new Button();
            Deletebutton = new Button();
            Ucalc_dataGridView = new DataGridView();
            AddMaterial_button = new Button();
            AltMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
            tabControl1.SuspendLayout();
            Main_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            Wall_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // AltMainPanel
            // 
            AltMainPanel.BackColor = Color.White;
            AltMainPanel.Controls.Add(label1);
            AltMainPanel.Controls.Add(Type_textBox);
            AltMainPanel.Controls.Add(Icon_pictureBox);
            AltMainPanel.Location = new Point(12, 12);
            AltMainPanel.Name = "AltMainPanel";
            AltMainPanel.Size = new Size(977, 73);
            AltMainPanel.TabIndex = 17;
            AltMainPanel.Paint += AltMainPanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(116, 27);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 112;
            label1.Text = "리모델링안";
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = Color.White;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(177, 57);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 96;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 9);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 91;
            Icon_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1025, 615);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 92;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(label2);
            panel5.Controls.Add(Boiler_dataGridView);
            panel5.Controls.Add(Alt_Remove_button);
            panel5.Controls.Add(Alt_Add_button);
            panel5.Controls.Add(q50_textBox);
            panel5.Controls.Add(q50_label2);
            panel5.Controls.Add(q50_label1);
            panel5.Location = new Point(14, 109);
            panel5.Name = "panel5";
            panel5.Size = new Size(976, 159);
            panel5.TabIndex = 105;
            panel5.Paint += panel5_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(477, 13);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 142;
            label2.Text = "요소기술 결정";
            // 
            // Boiler_dataGridView
            // 
            Boiler_dataGridView.AllowUserToAddRows = false;
            Boiler_dataGridView.AllowUserToDeleteRows = false;
            Boiler_dataGridView.AllowUserToResizeColumns = false;
            Boiler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Boiler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Boiler_dataGridView.BackgroundColor = SystemColors.Window;
            Boiler_dataGridView.BorderStyle = BorderStyle.None;
            Boiler_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Boiler_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Boiler_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Boiler_dataGridView.Location = new Point(565, 13);
            Boiler_dataGridView.Name = "Boiler_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Boiler_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Boiler_dataGridView.RowHeadersVisible = false;
            Boiler_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Boiler_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Boiler_dataGridView.RowTemplate.Height = 25;
            Boiler_dataGridView.Size = new Size(224, 129);
            Boiler_dataGridView.TabIndex = 141;
            // 
            // Alt_Remove_button
            // 
            Alt_Remove_button.BackColor = SystemColors.ControlLight;
            Alt_Remove_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Alt_Remove_button.FlatStyle = FlatStyle.System;
            Alt_Remove_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Alt_Remove_button.Location = new Point(841, 11);
            Alt_Remove_button.Margin = new Padding(0);
            Alt_Remove_button.Name = "Alt_Remove_button";
            Alt_Remove_button.Size = new Size(23, 23);
            Alt_Remove_button.TabIndex = 140;
            Alt_Remove_button.Text = "-";
            Alt_Remove_button.UseVisualStyleBackColor = false;
            Alt_Remove_button.Click += Alt_Remove_button_Click;
            // 
            // Alt_Add_button
            // 
            Alt_Add_button.BackColor = SystemColors.ControlLight;
            Alt_Add_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Alt_Add_button.FlatStyle = FlatStyle.System;
            Alt_Add_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Alt_Add_button.Location = new Point(804, 11);
            Alt_Add_button.Margin = new Padding(0);
            Alt_Add_button.Name = "Alt_Add_button";
            Alt_Add_button.Size = new Size(23, 23);
            Alt_Add_button.TabIndex = 139;
            Alt_Add_button.Text = "+";
            Alt_Add_button.UseVisualStyleBackColor = false;
            Alt_Add_button.Click += Alt_Add_button_Click;
            // 
            // q50_textBox
            // 
            q50_textBox.BackColor = Color.White;
            q50_textBox.BorderStyle = BorderStyle.FixedSingle;
            q50_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            q50_textBox.ForeColor = SystemColors.ControlText;
            q50_textBox.Location = new Point(114, 13);
            q50_textBox.Name = "q50_textBox";
            q50_textBox.Size = new Size(120, 22);
            q50_textBox.TabIndex = 137;
            q50_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // q50_label2
            // 
            q50_label2.AutoSize = true;
            q50_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            q50_label2.ForeColor = SystemColors.ControlText;
            q50_label2.Location = new Point(234, 16);
            q50_label2.Name = "q50_label2";
            q50_label2.Size = new Size(18, 16);
            q50_label2.TabIndex = 138;
            q50_label2.Text = "원";
            // 
            // q50_label1
            // 
            q50_label1.AutoSize = true;
            q50_label1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            q50_label1.Location = new Point(27, 16);
            q50_label1.Name = "q50_label1";
            q50_label1.Size = new Size(47, 15);
            q50_label1.TabIndex = 136;
            q50_label1.Text = "총 예산";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 90);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 94;
            label4.Text = "리모델링 의사결정";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Main_tabPage);
            tabControl1.Controls.Add(Wall_tabPage);
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
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(12, 274);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 388);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 106;
            // 
            // Main_tabPage
            // 
            Main_tabPage.Controls.Add(label5);
            Main_tabPage.Controls.Add(pictureBox2);
            Main_tabPage.Controls.Add(webView22);
            Main_tabPage.Location = new Point(4, 25);
            Main_tabPage.Name = "Main_tabPage";
            Main_tabPage.Padding = new Padding(3);
            Main_tabPage.Size = new Size(969, 359);
            Main_tabPage.TabIndex = 1;
            Main_tabPage.Text = "법규기반 검토";
            Main_tabPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(2, 246);
            label5.Name = "label5";
            label5.Size = new Size(122, 15);
            label5.TabIndex = 25;
            label5.Text = "에너지절감량(kWh/a)";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(128, 240);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 26);
            pictureBox2.TabIndex = 24;
            pictureBox2.TabStop = false;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Location = new Point(173, 35);
            webView22.Name = "webView22";
            webView22.Size = new Size(781, 235);
            webView22.TabIndex = 23;
            webView22.ZoomFactor = 1D;
            // 
            // Wall_tabPage
            // 
            Wall_tabPage.BackColor = Color.White;
            Wall_tabPage.Controls.Add(Rse_textBox);
            Wall_tabPage.Controls.Add(label12);
            Wall_tabPage.Controls.Add(Rsi_textBox);
            Wall_tabPage.Controls.Add(label10);
            Wall_tabPage.Controls.Add(Material_Rtot_textBox);
            Wall_tabPage.Controls.Add(Material_dtot_textBox);
            Wall_tabPage.Controls.Add(label8);
            Wall_tabPage.Controls.Add(DiIndi2_comboBox);
            Wall_tabPage.Controls.Add(label3);
            Wall_tabPage.Controls.Add(ISO_KS_comboBox);
            Wall_tabPage.Controls.Add(MaterialDown_button);
            Wall_tabPage.Controls.Add(MaterialUP_button);
            Wall_tabPage.Controls.Add(Deletebutton);
            Wall_tabPage.Controls.Add(Ucalc_dataGridView);
            Wall_tabPage.Controls.Add(AddMaterial_button);
            Wall_tabPage.Location = new Point(4, 25);
            Wall_tabPage.Name = "Wall_tabPage";
            Wall_tabPage.Padding = new Padding(3);
            Wall_tabPage.Size = new Size(969, 359);
            Wall_tabPage.TabIndex = 0;
            Wall_tabPage.Text = "외벽";
            // 
            // Rse_textBox
            // 
            Rse_textBox.BackColor = Color.White;
            Rse_textBox.BorderStyle = BorderStyle.None;
            Rse_textBox.Enabled = false;
            Rse_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Rse_textBox.ForeColor = SystemColors.ControlDark;
            Rse_textBox.Location = new Point(474, 303);
            Rse_textBox.Name = "Rse_textBox";
            Rse_textBox.Size = new Size(80, 15);
            Rse_textBox.TabIndex = 118;
            Rse_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(301, 306);
            label12.Name = "label12";
            label12.Size = new Size(141, 15);
            label12.TabIndex = 117;
            label12.Text = "실외표면열전달저항[Rse]";
            // 
            // Rsi_textBox
            // 
            Rsi_textBox.BackColor = Color.White;
            Rsi_textBox.BorderStyle = BorderStyle.None;
            Rsi_textBox.Enabled = false;
            Rsi_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Rsi_textBox.ForeColor = SystemColors.ControlDark;
            Rsi_textBox.Location = new Point(474, 40);
            Rsi_textBox.Name = "Rsi_textBox";
            Rsi_textBox.Size = new Size(80, 15);
            Rsi_textBox.TabIndex = 116;
            Rsi_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(302, 43);
            label10.Name = "label10";
            label10.Size = new Size(138, 15);
            label10.TabIndex = 114;
            label10.Text = "실내표면열전달저항[Rsi]";
            // 
            // Material_Rtot_textBox
            // 
            Material_Rtot_textBox.BackColor = Color.White;
            Material_Rtot_textBox.BorderStyle = BorderStyle.None;
            Material_Rtot_textBox.Enabled = false;
            Material_Rtot_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Material_Rtot_textBox.ForeColor = SystemColors.ControlDark;
            Material_Rtot_textBox.Location = new Point(474, 330);
            Material_Rtot_textBox.Name = "Material_Rtot_textBox";
            Material_Rtot_textBox.Size = new Size(80, 15);
            Material_Rtot_textBox.TabIndex = 112;
            Material_Rtot_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Material_dtot_textBox
            // 
            Material_dtot_textBox.BackColor = Color.White;
            Material_dtot_textBox.BorderStyle = BorderStyle.None;
            Material_dtot_textBox.Enabled = false;
            Material_dtot_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Material_dtot_textBox.ForeColor = SystemColors.ControlDark;
            Material_dtot_textBox.Location = new Point(390, 330);
            Material_dtot_textBox.Name = "Material_dtot_textBox";
            Material_dtot_textBox.Size = new Size(80, 15);
            Material_dtot_textBox.TabIndex = 111;
            Material_dtot_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(356, 333);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 110;
            label8.Text = "합계";
            // 
            // DiIndi2_comboBox
            // 
            DiIndi2_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            DiIndi2_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DiIndi2_comboBox.FormattingEnabled = true;
            DiIndi2_comboBox.Location = new Point(309, 3);
            DiIndi2_comboBox.Name = "DiIndi2_comboBox";
            DiIndi2_comboBox.Size = new Size(120, 23);
            DiIndi2_comboBox.TabIndex = 102;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(48, 11);
            label3.Name = "label3";
            label3.Size = new Size(127, 15);
            label3.TabIndex = 101;
            label3.Text = "실내외표면열전달저항";
            // 
            // ISO_KS_comboBox
            // 
            ISO_KS_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            ISO_KS_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ISO_KS_comboBox.FormattingEnabled = true;
            ISO_KS_comboBox.Location = new Point(172, 3);
            ISO_KS_comboBox.Name = "ISO_KS_comboBox";
            ISO_KS_comboBox.Size = new Size(120, 23);
            ISO_KS_comboBox.TabIndex = 100;
            // 
            // MaterialDown_button
            // 
            MaterialDown_button.BackColor = SystemColors.ControlLight;
            MaterialDown_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            MaterialDown_button.FlatStyle = FlatStyle.System;
            MaterialDown_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            MaterialDown_button.Location = new Point(472, 4);
            MaterialDown_button.Margin = new Padding(0);
            MaterialDown_button.Name = "MaterialDown_button";
            MaterialDown_button.Size = new Size(23, 23);
            MaterialDown_button.TabIndex = 99;
            MaterialDown_button.Text = "▼";
            MaterialDown_button.UseVisualStyleBackColor = false;
            // 
            // MaterialUP_button
            // 
            MaterialUP_button.BackColor = SystemColors.ControlLight;
            MaterialUP_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            MaterialUP_button.FlatStyle = FlatStyle.System;
            MaterialUP_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            MaterialUP_button.Location = new Point(447, 4);
            MaterialUP_button.Margin = new Padding(0);
            MaterialUP_button.Name = "MaterialUP_button";
            MaterialUP_button.Size = new Size(23, 23);
            MaterialUP_button.TabIndex = 98;
            MaterialUP_button.Text = "▲";
            MaterialUP_button.UseVisualStyleBackColor = false;
            // 
            // Deletebutton
            // 
            Deletebutton.BackColor = SystemColors.ControlLight;
            Deletebutton.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Deletebutton.FlatStyle = FlatStyle.System;
            Deletebutton.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Deletebutton.Location = new Point(530, 4);
            Deletebutton.Margin = new Padding(0);
            Deletebutton.Name = "Deletebutton";
            Deletebutton.Size = new Size(23, 23);
            Deletebutton.TabIndex = 97;
            Deletebutton.Text = "-";
            Deletebutton.UseVisualStyleBackColor = false;
            // 
            // Ucalc_dataGridView
            // 
            Ucalc_dataGridView.AllowUserToAddRows = false;
            Ucalc_dataGridView.AllowUserToDeleteRows = false;
            Ucalc_dataGridView.AllowUserToResizeColumns = false;
            Ucalc_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Ucalc_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Ucalc_dataGridView.BackgroundColor = SystemColors.InactiveBorder;
            Ucalc_dataGridView.BorderStyle = BorderStyle.None;
            Ucalc_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Ucalc_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            Ucalc_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Ucalc_dataGridView.Location = new Point(-1, 61);
            Ucalc_dataGridView.Name = "Ucalc_dataGridView";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            Ucalc_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            Ucalc_dataGridView.RowHeadersVisible = false;
            Ucalc_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Ucalc_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            Ucalc_dataGridView.RowTemplate.Height = 25;
            Ucalc_dataGridView.Size = new Size(555, 234);
            Ucalc_dataGridView.TabIndex = 96;
            // 
            // AddMaterial_button
            // 
            AddMaterial_button.BackColor = SystemColors.ControlLight;
            AddMaterial_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            AddMaterial_button.FlatStyle = FlatStyle.System;
            AddMaterial_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            AddMaterial_button.Location = new Point(505, 4);
            AddMaterial_button.Margin = new Padding(0);
            AddMaterial_button.Name = "AddMaterial_button";
            AddMaterial_button.Size = new Size(23, 23);
            AddMaterial_button.TabIndex = 95;
            AddMaterial_button.Text = "+";
            AddMaterial_button.UseVisualStyleBackColor = false;
            // 
            // AltMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl1);
            Controls.Add(panel5);
            Controls.Add(label4);
            Controls.Add(Save_button);
            Controls.Add(AltMainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AltMain";
            Text = "Form3";
            AltMainPanel.ResumeLayout(false);
            AltMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            tabControl1.ResumeLayout(false);
            Main_tabPage.ResumeLayout(false);
            Main_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            Wall_tabPage.ResumeLayout(false);
            Wall_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Ucalc_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel AltMainPanel;
        private PictureBox Icon_pictureBox;
        private TextBox Type_textBox;
        private Button Previous_button;
        private Button Save_button;
        private Label label1;
        private ComboBox Diagnosis_comboBox;
        private ComboBox Target_comboBox;
        private ComboBox RoofType_comboBox;
        private ComboBox WallType_comboBox;
        private Panel panel5;
        private Label label4;
        private TextBox q50_textBox;
        private Label q50_label2;
        private Label q50_label1;
        private Label label2;
        private DataGridView Boiler_dataGridView;
        private Button Alt_Remove_button;
        private Button Alt_Add_button;
        private CustomTabControl tabControl1;
        private TabPage Wall_tabPage;
        private TextBox Rse_textBox;
        private Label label12;
        private TextBox Rsi_textBox;
        private Label label10;
        private TextBox Material_Rtot_textBox;
        private TextBox Material_dtot_textBox;
        private Label label8;
        private CustomComboBox DiIndi2_comboBox;
        private Label label3;
        private CustomComboBox ISO_KS_comboBox;
        private Button MaterialDown_button;
        private Button MaterialUP_button;
        private Button Deletebutton;
        private DataGridView Ucalc_dataGridView;
        private Button AddMaterial_button;
        private TabPage Main_tabPage;
        private Label label5;
        private PictureBox pictureBox2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
    }
}