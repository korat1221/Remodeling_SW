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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            GeneralPanel = new Panel();
            textBox1 = new TextBox();
            CoolingSystemNumText = new Label();
            pictureBox1 = new PictureBox();
            CoolingSystemTypeSelectCombobox = new ComboBox();
            label6 = new Label();
            text = new Label();
            ZoneSelection = new Button();
            label5 = new Label();
            ZoneListName = new TextBox();
            label3 = new Label();
            label1 = new Label();
            CoolingSystemNameText = new TextBox();
            panel2 = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            CGdesideBtu = new Button();
            panel3 = new Panel();
            CoolingGeneratorList = new DataGridView();
            label22 = new Label();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            panel1 = new Panel();
            label4 = new Label();
            label14 = new Label();
            label8 = new Label();
            label13 = new Label();
            label9 = new Label();
            label12 = new Label();
            label10 = new Label();
            label11 = new Label();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            label7 = new Label();
            AdditionalPanel = new Panel();
            CoolingGeneratorImage = new PictureBox();
            button1 = new Button();
            label23 = new Label();
            button2 = new Button();
            label2 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorList).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorImage).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(button2);
            GeneralPanel.Controls.Add(label23);
            GeneralPanel.Controls.Add(textBox1);
            GeneralPanel.Controls.Add(CoolingSystemNumText);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(CoolingSystemTypeSelectCombobox);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(text);
            GeneralPanel.Controls.Add(ZoneSelection);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(ZoneListName);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(CoolingSystemNameText);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Enabled = false;
            textBox1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(172, 70);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(420, 26);
            textBox1.TabIndex = 99;
            // 
            // CoolingSystemNumText
            // 
            CoolingSystemNumText.AutoSize = true;
            CoolingSystemNumText.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            CoolingSystemNumText.Location = new Point(97, 12);
            CoolingSystemNumText.Name = "CoolingSystemNumText";
            CoolingSystemNumText.Size = new Size(60, 15);
            CoolingSystemNumText.TabIndex = 98;
            CoolingSystemNumText.Text = "CS_001";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(16, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(61, 59);
            pictureBox1.TabIndex = 97;
            pictureBox1.TabStop = false;
            // 
            // CoolingSystemTypeSelectCombobox
            // 
            CoolingSystemTypeSelectCombobox.FormattingEnabled = true;
            CoolingSystemTypeSelectCombobox.Location = new Point(453, 14);
            CoolingSystemTypeSelectCombobox.Name = "CoolingSystemTypeSelectCombobox";
            CoolingSystemTypeSelectCombobox.Size = new Size(121, 23);
            CoolingSystemTypeSelectCombobox.TabIndex = 96;
            CoolingSystemTypeSelectCombobox.SelectedIndexChanged += CoolingGeneratorSelectCombobox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(367, 18);
            label6.Name = "label6";
            label6.Size = new Size(79, 15);
            label6.TabIndex = 95;
            label6.Text = "냉방설비유형";
            // 
            // text
            // 
            text.AutoSize = true;
            text.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            text.Location = new Point(93, 44);
            text.Name = "text";
            text.Size = new Size(43, 15);
            text.TabIndex = 94;
            text.Text = "냉방존";
            // 
            // ZoneSelection
            // 
            ZoneSelection.Font = new Font("맑은 고딕", 13F, FontStyle.Bold, GraphicsUnit.Point);
            ZoneSelection.ForeColor = Color.SteelBlue;
            ZoneSelection.Location = new Point(141, 43);
            ZoneSelection.Margin = new Padding(0);
            ZoneSelection.Name = "ZoneSelection";
            ZoneSelection.Size = new Size(25, 25);
            ZoneSelection.TabIndex = 93;
            ZoneSelection.Text = "+";
            ZoneSelection.UseVisualStyleBackColor = true;
            ZoneSelection.Click += ZoneSelection_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(797, 74);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 92;
            label5.Text = "최대냉방부하";
            label5.Click += label5_Click;
            // 
            // ZoneListName
            // 
            ZoneListName.BackColor = SystemColors.Window;
            ZoneListName.BorderStyle = BorderStyle.FixedSingle;
            ZoneListName.Enabled = false;
            ZoneListName.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ZoneListName.Location = new Point(172, 41);
            ZoneListName.Multiline = true;
            ZoneListName.Name = "ZoneListName";
            ZoneListName.ReadOnly = true;
            ZoneListName.ScrollBars = ScrollBars.Vertical;
            ZoneListName.Size = new Size(420, 26);
            ZoneListName.TabIndex = 89;
            ZoneListName.TextChanged += ZoneListName_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(12, 12);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 3;
            label3.Text = "냉방시스템";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(180, 17);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 1;
            label1.Text = "명칭";
            // 
            // CoolingSystemNameText
            // 
            CoolingSystemNameText.BackColor = SystemColors.Window;
            CoolingSystemNameText.BorderStyle = BorderStyle.FixedSingle;
            CoolingSystemNameText.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CoolingSystemNameText.Location = new Point(221, 13);
            CoolingSystemNameText.Name = "CoolingSystemNameText";
            CoolingSystemNameText.Size = new Size(120, 22);
            CoolingSystemNameText.TabIndex = 88;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(tabControl1);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 222);
            panel2.TabIndex = 18;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(973, 218);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(CGdesideBtu);
            tabPage1.Controls.Add(panel3);
            tabPage1.Controls.Add(label22);
            tabPage1.Controls.Add(label21);
            tabPage1.Controls.Add(label20);
            tabPage1.Controls.Add(label19);
            tabPage1.Controls.Add(label18);
            tabPage1.Controls.Add(label17);
            tabPage1.Controls.Add(label16);
            tabPage1.Controls.Add(label15);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(965, 190);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "생산";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // CGdesideBtu
            // 
            CGdesideBtu.Location = new Point(3, 69);
            CGdesideBtu.Name = "CGdesideBtu";
            CGdesideBtu.Size = new Size(75, 23);
            CGdesideBtu.TabIndex = 18;
            CGdesideBtu.Text = "적용";
            CGdesideBtu.UseVisualStyleBackColor = true;
            CGdesideBtu.Click += CGdesideBtu_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.AliceBlue;
            panel3.Controls.Add(CoolingGeneratorList);
            panel3.Location = new Point(80, 69);
            panel3.Name = "panel3";
            panel3.Size = new Size(882, 118);
            panel3.TabIndex = 17;
            // 
            // CoolingGeneratorList
            // 
            CoolingGeneratorList.AllowUserToAddRows = false;
            CoolingGeneratorList.AllowUserToDeleteRows = false;
            CoolingGeneratorList.AllowUserToResizeColumns = false;
            CoolingGeneratorList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CoolingGeneratorList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CoolingGeneratorList.BackgroundColor = SystemColors.Control;
            CoolingGeneratorList.BorderStyle = BorderStyle.None;
            CoolingGeneratorList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            CoolingGeneratorList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            CoolingGeneratorList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            CoolingGeneratorList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CoolingGeneratorList.Dock = DockStyle.Fill;
            CoolingGeneratorList.Location = new Point(0, 0);
            CoolingGeneratorList.Name = "CoolingGeneratorList";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            CoolingGeneratorList.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            CoolingGeneratorList.RowHeadersVisible = false;
            CoolingGeneratorList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            CoolingGeneratorList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            CoolingGeneratorList.RowTemplate.Height = 25;
            CoolingGeneratorList.Size = new Size(882, 118);
            CoolingGeneratorList.TabIndex = 99;
            CoolingGeneratorList.CellContentClick += CoolingGeneratorList_CellContentClick;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(896, 30);
            label22.Name = "label22";
            label22.Size = new Size(46, 15);
            label22.TabIndex = 16;
            label22.Text = "label22";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(771, 30);
            label21.Name = "label21";
            label21.Size = new Size(46, 15);
            label21.TabIndex = 15;
            label21.Text = "label21";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(646, 30);
            label20.Name = "label20";
            label20.Size = new Size(46, 15);
            label20.TabIndex = 14;
            label20.Text = "label20";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(521, 30);
            label19.Name = "label19";
            label19.Size = new Size(46, 15);
            label19.TabIndex = 13;
            label19.Text = "label19";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(396, 30);
            label18.Name = "label18";
            label18.Size = new Size(46, 15);
            label18.TabIndex = 12;
            label18.Text = "label18";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(271, 30);
            label17.Name = "label17";
            label17.Size = new Size(46, 15);
            label17.TabIndex = 11;
            label17.Text = "label17";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(146, 30);
            label16.Name = "label16";
            label16.Size = new Size(46, 15);
            label16.TabIndex = 10;
            label16.Text = "label16";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(21, 30);
            label15.Name = "label15";
            label15.Size = new Size(46, 15);
            label15.TabIndex = 9;
            label15.Text = "label15";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Khaki;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label11);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(959, 24);
            panel1.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 4);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 0;
            label4.Text = "냉방출력";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(888, 4);
            label14.Name = "label14";
            label14.Size = new Size(55, 15);
            label14.TabIndex = 7;
            label14.Text = "공급방식";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(130, 4);
            label8.Name = "label8";
            label8.Size = new Size(87, 15);
            label8.TabIndex = 1;
            label8.Text = "냉방성능[제품]";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(749, 4);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 6;
            label13.Text = "냉방설비대수";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(277, 4);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 2;
            label9.Text = "제어유형";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(598, 4);
            label12.Name = "label12";
            label12.Size = new Size(91, 15);
            label12.TabIndex = 5;
            label12.Text = "외기냉방시스템";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(392, 4);
            label10.Name = "label10";
            label10.Size = new Size(55, 15);
            label10.TabIndex = 3;
            label10.Text = "설치위치";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(507, 4);
            label11.Name = "label11";
            label11.Size = new Size(31, 15);
            label11.TabIndex = 4;
            label11.Text = "연료";
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(965, 190);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "저장";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(965, 190);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "분배";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(965, 190);
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
            AdditionalPanel.Location = new Point(12, 354);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 339);
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
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(93, 74);
            label23.Name = "label23";
            label23.Size = new Size(43, 15);
            label23.TabIndex = 100;
            label23.Text = "공조기";
            // 
            // button2
            // 
            button2.Font = new Font("맑은 고딕", 13F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.SteelBlue;
            button2.Location = new Point(141, 71);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(25, 25);
            button2.TabIndex = 101;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(749, 44);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 90;
            label2.Text = "연간냉방에너지요구량";
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
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorList).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CoolingGeneratorImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Label text;
        private Button ZoneSelection;
        private Label label5;
        private Label label6;
        private PictureBox CoolingGeneratorImage;
        private PictureBox pictureBox1;
        private Label label7;
        public TextBox ZoneListName;
        public Label CoolingSystemNumText;
        public TextBox CoolingSystemNameText;
        public ComboBox CoolingSystemTypeSelectCombobox;
        private Button button1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Panel panel3;
        private Label label22;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Panel panel1;
        private Label label4;
        private Label label14;
        private Label label8;
        private Label label13;
        private Label label9;
        private Label label12;
        private Label label10;
        private Label label11;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DataGridView CoolingGeneratorList;
        private Button CGdesideBtu;
        public TextBox textBox1;
        private Button button2;
        private Label label23;
        private Label label2;
    }
}