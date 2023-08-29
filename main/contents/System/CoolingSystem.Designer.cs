namespace main.contents
{
    partial class CoolingSystem
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            panel2 = new Panel();
            tabControl1 = new TabControl();
            Generator_tabpage = new TabPage();
            CoolingGeneratorSum_dataGridView1 = new DataGridView();
            CoolingGeneratorSelect_comboBox = new ComboBox();
            button3 = new Button();
            label4 = new Label();
            tabControl2 = new TabControl();
            Boiler_tabPage = new TabPage();
            AirCondition_dataGridView = new DataGridView();
            HP_tabPage = new TabPage();
            AS_tabPage = new TabPage();
            DH_tabPage = new TabPage();
            Solar_tabPage = new TabPage();
            tabPage5 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            label7 = new Label();
            AdditionalPanel = new Panel();
            CoolingGeneratorImage = new PictureBox();
            button1 = new Button();
            CoolingSystemNameText = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label5 = new Label();
            GeneralPanel = new Panel();
            NumTextBox = new Label();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label9 = new Label();
            label8 = new Label();
            textBox1 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            CZ_MaxCoolingLoad_Textbox = new TextBox();
            CZ_AnnualCoolingNeed_Textbox = new TextBox();
            textBox2 = new TextBox();
            label24 = new Label();
            label25 = new Label();
            label26 = new Label();
            label23 = new Label();
            button2 = new Button();
            label6 = new Label();
            Zone_button = new Button();
            CZ_FloorArea_Textbox = new TextBox();
            label3 = new Label();
            Icon_pictureBox = new PictureBox();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            Generator_tabpage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorSum_dataGridView1).BeginInit();
            tabControl2.SuspendLayout();
            Boiler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AirCondition_dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorImage).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(tabControl1);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 287);
            panel2.TabIndex = 18;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Generator_tabpage);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(973, 283);
            tabControl1.TabIndex = 0;
            // 
            // Generator_tabpage
            // 
            Generator_tabpage.Controls.Add(CoolingGeneratorSum_dataGridView1);
            Generator_tabpage.Controls.Add(CoolingGeneratorSelect_comboBox);
            Generator_tabpage.Controls.Add(button3);
            Generator_tabpage.Controls.Add(label4);
            Generator_tabpage.Controls.Add(tabControl2);
            Generator_tabpage.Location = new Point(4, 24);
            Generator_tabpage.Name = "Generator_tabpage";
            Generator_tabpage.Padding = new Padding(3);
            Generator_tabpage.Size = new Size(965, 255);
            Generator_tabpage.TabIndex = 0;
            Generator_tabpage.Text = "생산";
            Generator_tabpage.UseVisualStyleBackColor = true;
            // 
            // CoolingGeneratorSum_dataGridView1
            // 
            CoolingGeneratorSum_dataGridView1.AllowUserToAddRows = false;
            CoolingGeneratorSum_dataGridView1.AllowUserToDeleteRows = false;
            CoolingGeneratorSum_dataGridView1.AllowUserToResizeColumns = false;
            CoolingGeneratorSum_dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolingGeneratorSum_dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolingGeneratorSum_dataGridView1.BackgroundColor = Color.White;
            CoolingGeneratorSum_dataGridView1.BorderStyle = BorderStyle.None;
            CoolingGeneratorSum_dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolingGeneratorSum_dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            CoolingGeneratorSum_dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            CoolingGeneratorSum_dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingGeneratorSum_dataGridView1.Location = new Point(3, 38);
            CoolingGeneratorSum_dataGridView1.Name = "CoolingGeneratorSum_dataGridView1";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            CoolingGeneratorSum_dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            CoolingGeneratorSum_dataGridView1.RowHeadersVisible = false;
            CoolingGeneratorSum_dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            CoolingGeneratorSum_dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle9;
            CoolingGeneratorSum_dataGridView1.RowTemplate.Height = 25;
            CoolingGeneratorSum_dataGridView1.Size = new Size(511, 53);
            CoolingGeneratorSum_dataGridView1.TabIndex = 151;
            // 
            // CoolingGeneratorSelect_comboBox
            // 
            CoolingGeneratorSelect_comboBox.FormattingEnabled = true;
            CoolingGeneratorSelect_comboBox.Location = new Point(73, 9);
            CoolingGeneratorSelect_comboBox.Name = "CoolingGeneratorSelect_comboBox";
            CoolingGeneratorSelect_comboBox.Size = new Size(121, 23);
            CoolingGeneratorSelect_comboBox.TabIndex = 150;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlLight;
            button3.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            button3.FlatStyle = FlatStyle.System;
            button3.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(198, 9);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(23, 23);
            button3.TabIndex = 147;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(12, 12);
            label4.Name = "label4";
            label4.Size = new Size(60, 17);
            label4.TabIndex = 146;
            label4.Text = "냉방설비";
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(Boiler_tabPage);
            tabControl2.Controls.Add(HP_tabPage);
            tabControl2.Controls.Add(AS_tabPage);
            tabControl2.Controls.Add(DH_tabPage);
            tabControl2.Controls.Add(Solar_tabPage);
            tabControl2.Controls.Add(tabPage5);
            tabControl2.Dock = DockStyle.Bottom;
            tabControl2.Location = new Point(3, 96);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(959, 156);
            tabControl2.TabIndex = 145;
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.Controls.Add(AirCondition_dataGridView);
            Boiler_tabPage.Location = new Point(4, 24);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(951, 128);
            Boiler_tabPage.TabIndex = 6;
            Boiler_tabPage.Text = "실외기12kW";
            Boiler_tabPage.UseVisualStyleBackColor = true;
            // 
            // AirCondition_dataGridView
            // 
            AirCondition_dataGridView.AllowUserToAddRows = false;
            AirCondition_dataGridView.AllowUserToDeleteRows = false;
            AirCondition_dataGridView.AllowUserToResizeColumns = false;
            AirCondition_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            AirCondition_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AirCondition_dataGridView.BackgroundColor = Color.White;
            AirCondition_dataGridView.BorderStyle = BorderStyle.None;
            AirCondition_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            AirCondition_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            AirCondition_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            AirCondition_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AirCondition_dataGridView.Location = new Point(6, 26);
            AirCondition_dataGridView.Name = "AirCondition_dataGridView";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = SystemColors.Control;
            dataGridViewCellStyle11.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            AirCondition_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            AirCondition_dataGridView.RowHeadersVisible = false;
            AirCondition_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle12.ForeColor = Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle12.SelectionForeColor = Color.Black;
            AirCondition_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            AirCondition_dataGridView.RowTemplate.Height = 25;
            AirCondition_dataGridView.Size = new Size(943, 96);
            AirCondition_dataGridView.TabIndex = 22;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Location = new Point(4, 24);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(951, 128);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "공냉식냉동기";
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 24);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(951, 128);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "수냉식냉동기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 24);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(951, 128);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "흡수식냉동기";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 24);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(951, 128);
            Solar_tabPage.TabIndex = 5;
            Solar_tabPage.Text = "흡수식냉온수기";
            Solar_tabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(951, 128);
            tabPage5.TabIndex = 7;
            tabPage5.Text = "지열히트펌프";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(965, 255);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "저장";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(965, 255);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "분배";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(965, 255);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "공급";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(24, 118);
            label7.Name = "label7";
            label7.Size = new Size(79, 15);
            label7.TabIndex = 98;
            label7.Text = "냉방설비유형";
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Location = new Point(12, 429);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 264);
            AdditionalPanel.TabIndex = 18;
            // 
            // CoolingGeneratorImage
            // 
            CoolingGeneratorImage.Location = new Point(995, 12);
            CoolingGeneratorImage.Name = "CoolingGeneratorImage";
            CoolingGeneratorImage.Size = new Size(196, 344);
            CoolingGeneratorImage.TabIndex = 19;
            CoolingGeneratorImage.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(909, 695);
            button1.Name = "button1";
            button1.Size = new Size(78, 23);
            button1.TabIndex = 20;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // CoolingSystemNameText
            // 
            CoolingSystemNameText.BackColor = SystemColors.Window;
            CoolingSystemNameText.BorderStyle = BorderStyle.FixedSingle;
            CoolingSystemNameText.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CoolingSystemNameText.Location = new Point(191, 14);
            CoolingSystemNameText.Name = "CoolingSystemNameText";
            CoolingSystemNameText.Size = new Size(120, 22);
            CoolingSystemNameText.TabIndex = 88;
            CoolingSystemNameText.TextChanged += CoolingSystemNameText_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(125, 18);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "명칭";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(444, 12);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 90;
            label2.Text = "연간냉방에너지요구량";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(615, 12);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 92;
            label5.Text = "최대냉방부하";
            label5.Click += label5_Click;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(NumTextBox);
            GeneralPanel.Controls.Add(radioButton2);
            GeneralPanel.Controls.Add(radioButton1);
            GeneralPanel.Controls.Add(label9);
            GeneralPanel.Controls.Add(label8);
            GeneralPanel.Controls.Add(textBox1);
            GeneralPanel.Controls.Add(textBox5);
            GeneralPanel.Controls.Add(textBox6);
            GeneralPanel.Controls.Add(textBox7);
            GeneralPanel.Controls.Add(CZ_MaxCoolingLoad_Textbox);
            GeneralPanel.Controls.Add(CZ_AnnualCoolingNeed_Textbox);
            GeneralPanel.Controls.Add(textBox2);
            GeneralPanel.Controls.Add(label24);
            GeneralPanel.Controls.Add(label25);
            GeneralPanel.Controls.Add(label26);
            GeneralPanel.Controls.Add(label23);
            GeneralPanel.Controls.Add(button2);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(Zone_button);
            GeneralPanel.Controls.Add(CZ_FloorArea_Textbox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(CoolingSystemNameText);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // NumTextBox
            // 
            NumTextBox.AutoSize = true;
            NumTextBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            NumTextBox.Location = new Point(77, 16);
            NumTextBox.Name = "NumTextBox";
            NumTextBox.Size = new Size(0, 15);
            NumTextBox.TabIndex = 142;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(162, 75);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(14, 13);
            radioButton2.TabIndex = 141;
            radioButton2.TabStop = true;
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.Click += radioButton2_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(162, 50);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(14, 13);
            radioButton1.TabIndex = 140;
            radioButton1.TabStop = true;
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Click += radioButton1_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(125, 74);
            label9.Name = "label9";
            label9.Size = new Size(34, 17);
            label9.TabIndex = 139;
            label9.Text = "신규";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(125, 49);
            label8.Name = "label8";
            label8.Size = new Size(34, 17);
            label8.TabIndex = 138;
            label8.Text = "기존";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Enabled = false;
            textBox1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.ControlDark;
            textBox1.Location = new Point(594, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 15);
            textBox1.TabIndex = 137;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.White;
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Enabled = false;
            textBox5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox5.ForeColor = SystemColors.ControlDark;
            textBox5.Location = new Point(447, 74);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(120, 15);
            textBox5.TabIndex = 136;
            textBox5.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.White;
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Enabled = false;
            textBox6.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox6.ForeColor = SystemColors.ControlDark;
            textBox6.Location = new Point(314, 74);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(120, 15);
            textBox6.TabIndex = 135;
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            textBox7.BackColor = Color.White;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Enabled = false;
            textBox7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox7.ForeColor = SystemColors.ControlDark;
            textBox7.Location = new Point(718, 74);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(120, 15);
            textBox7.TabIndex = 134;
            textBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // CZ_MaxCoolingLoad_Textbox
            // 
            CZ_MaxCoolingLoad_Textbox.BackColor = Color.White;
            CZ_MaxCoolingLoad_Textbox.BorderStyle = BorderStyle.None;
            CZ_MaxCoolingLoad_Textbox.Enabled = false;
            CZ_MaxCoolingLoad_Textbox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CZ_MaxCoolingLoad_Textbox.ForeColor = SystemColors.ControlDark;
            CZ_MaxCoolingLoad_Textbox.Location = new Point(594, 51);
            CZ_MaxCoolingLoad_Textbox.Name = "CZ_MaxCoolingLoad_Textbox";
            CZ_MaxCoolingLoad_Textbox.Size = new Size(120, 15);
            CZ_MaxCoolingLoad_Textbox.TabIndex = 133;
            CZ_MaxCoolingLoad_Textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // CZ_AnnualCoolingNeed_Textbox
            // 
            CZ_AnnualCoolingNeed_Textbox.BackColor = Color.White;
            CZ_AnnualCoolingNeed_Textbox.BorderStyle = BorderStyle.None;
            CZ_AnnualCoolingNeed_Textbox.Enabled = false;
            CZ_AnnualCoolingNeed_Textbox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CZ_AnnualCoolingNeed_Textbox.ForeColor = SystemColors.ControlDark;
            CZ_AnnualCoolingNeed_Textbox.Location = new Point(447, 51);
            CZ_AnnualCoolingNeed_Textbox.Name = "CZ_AnnualCoolingNeed_Textbox";
            CZ_AnnualCoolingNeed_Textbox.Size = new Size(120, 15);
            CZ_AnnualCoolingNeed_Textbox.TabIndex = 132;
            CZ_AnnualCoolingNeed_Textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Enabled = false;
            textBox2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.ForeColor = SystemColors.ControlDark;
            textBox2.Location = new Point(314, 51);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 15);
            textBox2.TabIndex = 131;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("나눔고딕", 8.999999F, FontStyle.Regular, GraphicsUnit.Point);
            label24.Location = new Point(765, 30);
            label24.Name = "label24";
            label24.Size = new Size(26, 14);
            label24.TabIndex = 130;
            label24.Text = "[㎡]";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("나눔고딕", 8.999999F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(638, 30);
            label25.Name = "label25";
            label25.Size = new Size(33, 14);
            label25.TabIndex = 129;
            label25.Text = "[kW]";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("나눔고딕", 8.999999F, FontStyle.Regular, GraphicsUnit.Point);
            label26.Location = new Point(481, 30);
            label26.Name = "label26";
            label26.Size = new Size(52, 14);
            label26.TabIndex = 128;
            label26.Text = "[kWh/a]";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(751, 12);
            label23.Name = "label23";
            label23.Size = new Size(55, 15);
            label23.TabIndex = 127;
            label23.Text = "바닥면적";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlLight;
            button2.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            button2.FlatStyle = FlatStyle.System;
            button2.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(288, 70);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(23, 23);
            button2.TabIndex = 126;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(207, 73);
            label6.Name = "label6";
            label6.Size = new Size(71, 17);
            label6.TabIndex = 124;
            label6.Text = "공급  AHU";
            // 
            // Zone_button
            // 
            Zone_button.BackColor = SystemColors.ControlLight;
            Zone_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Zone_button.FlatStyle = FlatStyle.System;
            Zone_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Zone_button.Location = new Point(288, 47);
            Zone_button.Margin = new Padding(0);
            Zone_button.Name = "Zone_button";
            Zone_button.Size = new Size(23, 23);
            Zone_button.TabIndex = 123;
            Zone_button.Text = "+";
            Zone_button.UseVisualStyleBackColor = false;
            Zone_button.Click += Zone_button_Click;
            // 
            // CZ_FloorArea_Textbox
            // 
            CZ_FloorArea_Textbox.BackColor = Color.White;
            CZ_FloorArea_Textbox.BorderStyle = BorderStyle.None;
            CZ_FloorArea_Textbox.Enabled = false;
            CZ_FloorArea_Textbox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CZ_FloorArea_Textbox.ForeColor = SystemColors.ControlDark;
            CZ_FloorArea_Textbox.Location = new Point(718, 51);
            CZ_FloorArea_Textbox.Name = "CZ_FloorArea_Textbox";
            CZ_FloorArea_Textbox.Size = new Size(120, 15);
            CZ_FloorArea_Textbox.TabIndex = 122;
            CZ_FloorArea_Textbox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(207, 50);
            label3.Name = "label3";
            label3.Size = new Size(52, 17);
            label3.TabIndex = 121;
            label3.Text = "공급 존";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(23, 22);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 120;
            Icon_pictureBox.TabStop = false;
            // 
            // CoolingSystem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label7);
            Controls.Add(button1);
            Controls.Add(CoolingGeneratorImage);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CoolingSystem";
            Text = "Form3";
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            Generator_tabpage.ResumeLayout(false);
            Generator_tabpage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorSum_dataGridView1).EndInit();
            tabControl2.ResumeLayout(false);
            Boiler_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AirCondition_dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorImage).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private Panel AdditionalPanel;
        private PictureBox CoolingGeneratorImage;
        private Label label7;
        private Button button1;
        private TabControl tabControl1;
        private TabPage Generator_tabpage;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        public TextBox CoolingSystemNameText;
        private Label label1;
        private Label label2;
        private Label label5;
        private Panel GeneralPanel;
        private TextBox textBox1;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox CZ_MaxCoolingLoad_Textbox;
        private TextBox CZ_AnnualCoolingNeed_Textbox;
        private TextBox textBox2;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label23;
        private Button button2;
        private Label label6;
        private Button Zone_button;
        private TextBox CZ_FloorArea_Textbox;
        private Label label3;
        private PictureBox Icon_pictureBox;
        private TabControl tabControl2;
        private TabPage Boiler_tabPage;
        private DataGridView AirCondition_dataGridView;
        private TabPage HP_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage Solar_tabPage;
        private TabPage tabPage5;
        private DataGridView CoolingGeneratorSum_dataGridView1;
        private ComboBox CoolingGeneratorSelect_comboBox;
        private Button button3;
        private Label label4;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label9;
        private Label label8;
        private Label NumTextBox;
    }
}