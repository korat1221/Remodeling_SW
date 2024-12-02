using main.Properties;
using System.Windows.Forms;


namespace main.contents
{
    partial class ConstructionWindow
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
            GeneralPanel = new Panel();
            AdditionalWindow_textBox = new TextBox();
            Icon_pictureBox = new PictureBox();
            WinNum_textBox = new TextBox();
            groupBox1 = new GroupBox();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            Type_textBox = new TextBox();
            label2 = new Label();
            label7 = new Label();
            label6 = new Label();
            label3 = new Label();
            label5 = new Label();
            AdditionalWindow_comboBox = new CustomComboBox();
            Name_textBox = new TextBox();
            panel2 = new Panel();
            Uw2_label = new Label();
            label21 = new Label();
            g3_textBox = new TextBox();
            label28 = new Label();
            label20 = new Label();
            Size_textBox = new TextBox();
            label1 = new Label();
            ImportSize_button = new Button();
            Uw2_unit_label = new Label();
            Uw2_textBox = new TextBox();
            tabControl1 = new CustomTabControl();
            Glass_tabPage = new TabPage();
            SpacerName_textBox2 = new TextBox();
            GlassName_textBox2 = new TextBox();
            Ug_textBox = new TextBox();
            Ug_label = new Label();
            Ug_unit_label = new Label();
            label23 = new Label();
            g_textBox = new TextBox();
            label26 = new Label();
            τD65_SNA_textBox = new TextBox();
            label12 = new Label();
            label9 = new Label();
            Psi_g_fix_textBox = new TextBox();
            Psi_fix_label = new Label();
            Psi_fix_unit_label = new Label();
            Psi_open_label = new Label();
            Psi_g_open_textBox = new TextBox();
            Psi_open_unit_label = new Label();
            Frame_tabPage = new TabPage();
            label10 = new Label();
            label8 = new Label();
            label22 = new Label();
            df_btw_textBox = new TextBox();
            df_fix_textBox = new TextBox();
            df_open_textBox = new TextBox();
            Uf_btw_textBox = new TextBox();
            Uf_fix_textBox = new TextBox();
            Uf_open_textBox = new TextBox();
            FrameMaterial_textBox = new TextBox();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label15 = new Label();
            label14 = new Label();
            WindowFrame_pictureBox = new PictureBox();
            Install_tabPage = new TabPage();
            label44 = new Label();
            label45 = new Label();
            Psi_InstallButtom_textBox = new TextBox();
            Psi_InstallSide_textBox = new TextBox();
            Psi_InstallTop_textBox = new TextBox();
            label41 = new Label();
            label40 = new Label();
            label24 = new Label();
            label33 = new Label();
            label38 = new Label();
            WindowInstall_pictureBox = new PictureBox();
            Install_textBox = new TextBox();
            Install_button = new Button();
            SpacerName_textBox = new TextBox();
            Spacer_button = new Button();
            GlassName_textBox = new TextBox();
            GlassDB_button = new Button();
            FrameName_textBox = new TextBox();
            FrameDB_button = new Button();
            DiIndi_comboBox = new CustomComboBox();
            Install_comboBox = new CustomComboBox();
            label16 = new Label();
            Spacer_label = new Label();
            label11 = new Label();
            Frame_comboBox = new CustomComboBox();
            Frame_label = new Label();
            Uw_comboBox = new CustomComboBox();
            label25 = new Label();
            WindowType_pictureBox = new PictureBox();
            Save_button = new Button();
            Previous_button = new Button();
            g2_textBox = new TextBox();
            label13 = new Label();
            Uw3_textBox = new TextBox();
            Uw3_unit_label = new Label();
            Uw3_label = new Label();
            τD65_SNA2_textBox = new TextBox();
            label27 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            Glass_tabPage.SuspendLayout();
            Frame_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).BeginInit();
            Install_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowInstall_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = SystemColors.GradientActiveCaption;
            GeneralPanel.Controls.Add(AdditionalWindow_textBox);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(WinNum_textBox);
            GeneralPanel.Controls.Add(groupBox1);
            GeneralPanel.Controls.Add(Type_textBox);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label7);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label5);
            GeneralPanel.Controls.Add(AdditionalWindow_comboBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Location = new Point(0, 4);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1000, 80);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // AdditionalWindow_textBox
            // 
            AdditionalWindow_textBox.BackColor = SystemColors.GradientActiveCaption;
            AdditionalWindow_textBox.BorderStyle = BorderStyle.None;
            AdditionalWindow_textBox.Enabled = false;
            AdditionalWindow_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            AdditionalWindow_textBox.ForeColor = Color.Black;
            AdditionalWindow_textBox.Location = new Point(699, 28);
            AdditionalWindow_textBox.Name = "AdditionalWindow_textBox";
            AdditionalWindow_textBox.Size = new Size(67, 15);
            AdditionalWindow_textBox.TabIndex = 93;
            AdditionalWindow_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(18, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 91;
            Icon_pictureBox.TabStop = false;
            // 
            // WinNum_textBox
            // 
            WinNum_textBox.BackColor = SystemColors.GradientActiveCaption;
            WinNum_textBox.BorderStyle = BorderStyle.None;
            WinNum_textBox.Enabled = false;
            WinNum_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            WinNum_textBox.ForeColor = Color.Black;
            WinNum_textBox.Location = new Point(72, 32);
            WinNum_textBox.Name = "WinNum_textBox";
            WinNum_textBox.Size = new Size(67, 15);
            WinNum_textBox.TabIndex = 90;
            WinNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(378, -3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(283, 82);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.ForeColor = Color.Black;
            radioButton5.Location = new Point(146, 58);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(73, 19);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "내부덧댐";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.ForeColor = Color.Black;
            radioButton4.Location = new Point(17, 58);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(117, 19);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "외부(커튼월)덧댐";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.ForeColor = Color.Black;
            radioButton3.Location = new Point(126, 34);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(93, 19);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "철거 후 신규";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.ForeColor = Color.Black;
            radioButton2.Location = new Point(17, 34);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(49, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "신규";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.ForeColor = Color.Black;
            radioButton1.Location = new Point(17, 10);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(73, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "기존창호";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = SystemColors.GradientActiveCaption;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Type_textBox.ForeColor = Color.Black;
            Type_textBox.Location = new Point(177, 50);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 23;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(142, 12);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "명칭";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Black;
            label7.Location = new Point(341, 60);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 21;
            label7.Text = "덧댐";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Black;
            label6.Location = new Point(341, 36);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 20;
            label6.Text = "신규";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Black;
            label3.Location = new Point(141, 50);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "TYPE";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Black;
            label5.Location = new Point(340, 12);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 11;
            label5.Text = "기존";
            // 
            // AdditionalWindow_comboBox
            // 
            AdditionalWindow_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            AdditionalWindow_comboBox.ForeColor = Color.Black;
            AdditionalWindow_comboBox.FormattingEnabled = true;
            AdditionalWindow_comboBox.Location = new Point(699, 45);
            AdditionalWindow_comboBox.Name = "AdditionalWindow_comboBox";
            AdditionalWindow_comboBox.Size = new Size(120, 24);
            AdditionalWindow_comboBox.TabIndex = 0;
            AdditionalWindow_comboBox.SelectedIndexChanged += AdditionalWindow_comboBox_SelectedIndexChanged;
            // 
            // Name_textBox
            // 
            Name_textBox.ForeColor = Color.Black;
            Name_textBox.Location = new Point(177, 9);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 4;
            Name_textBox.TextAlign = HorizontalAlignment.Center;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(Uw2_label);
            panel2.Controls.Add(label21);
            panel2.Controls.Add(g3_textBox);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(Size_textBox);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(ImportSize_button);
            panel2.Controls.Add(Uw2_unit_label);
            panel2.Controls.Add(Uw2_textBox);
            panel2.Controls.Add(tabControl1);
            panel2.Controls.Add(Install_textBox);
            panel2.Controls.Add(Install_button);
            panel2.Controls.Add(SpacerName_textBox);
            panel2.Controls.Add(Spacer_button);
            panel2.Controls.Add(GlassName_textBox);
            panel2.Controls.Add(GlassDB_button);
            panel2.Controls.Add(FrameName_textBox);
            panel2.Controls.Add(FrameDB_button);
            panel2.Controls.Add(DiIndi_comboBox);
            panel2.Controls.Add(Install_comboBox);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(Spacer_label);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(Frame_comboBox);
            panel2.Controls.Add(Frame_label);
            panel2.Controls.Add(Uw_comboBox);
            panel2.Controls.Add(label25);
            panel2.Location = new Point(0, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 541);
            panel2.TabIndex = 18;
            panel2.Paint += panel2_Paint;
            // 
            // Uw2_label
            // 
            Uw2_label.AutoSize = true;
            Uw2_label.Font = new Font(UTIL.Families[0], 9.75F);
            Uw2_label.ForeColor = SystemColors.ControlDark;
            Uw2_label.Location = new Point(484, 55);
            Uw2_label.Name = "Uw2_label";
            Uw2_label.Size = new Size(113, 15);
            Uw2_label.TabIndex = 99;
            Uw2_label.Text = "[Uw] 창호열관류율";
            Uw2_label.Visible = false;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font(UTIL.Families[0], 9.75F);
            label21.ForeColor = SystemColors.ControlDark;
            label21.Location = new Point(484, 85);
            label21.Name = "label21";
            label21.Size = new Size(102, 15);
            label21.TabIndex = 105;
            label21.Text = "[g] 태양열취득률";
            // 
            // g3_textBox
            // 
            g3_textBox.BackColor = Color.White;
            g3_textBox.BorderStyle = BorderStyle.None;
            g3_textBox.Enabled = false;
            g3_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            g3_textBox.ForeColor = SystemColors.ControlDark;
            g3_textBox.Location = new Point(675, 86);
            g3_textBox.Name = "g3_textBox";
            g3_textBox.Size = new Size(116, 15);
            g3_textBox.TabIndex = 106;
            g3_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font(UTIL.Families[0], 9.75F);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(820, 85);
            label28.Name = "label28";
            label28.Size = new Size(15, 15);
            label28.TabIndex = 107;
            label28.Text = "-";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font(UTIL.Families[0], 9.75F);
            label20.Location = new Point(78, 55);
            label20.Name = "label20";
            label20.Size = new Size(58, 15);
            label20.TabIndex = 104;
            label20.Text = "창호 유형";
            // 
            // Size_textBox
            // 
            Size_textBox.BackColor = Color.White;
            Size_textBox.BorderStyle = BorderStyle.None;
            Size_textBox.Enabled = false;
            Size_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Size_textBox.ForeColor = SystemColors.ControlDark;
            Size_textBox.Location = new Point(175, 264);
            Size_textBox.Name = "Size_textBox";
            Size_textBox.Size = new Size(120, 15);
            Size_textBox.TabIndex = 103;
            Size_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font(UTIL.Families[0], 9.75F);
            label1.Location = new Point(78, 263);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 102;
            label1.Text = "창호 치수";
            // 
            // ImportSize_button
            // 
            ImportSize_button.BackColor = SystemColors.ControlLight;
            ImportSize_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ImportSize_button.FlatStyle = FlatStyle.System;
            ImportSize_button.Font = new Font(UTIL.Families[0], 12F);
            ImportSize_button.Location = new Point(296, 260);
            ImportSize_button.Margin = new Padding(0);
            ImportSize_button.Name = "ImportSize_button";
            ImportSize_button.Size = new Size(23, 23);
            ImportSize_button.TabIndex = 101;
            ImportSize_button.Text = "+";
            ImportSize_button.UseVisualStyleBackColor = false;
            ImportSize_button.Click += ImportSize_button_Click;
            // 
            // Uw2_unit_label
            // 
            Uw2_unit_label.AutoSize = true;
            Uw2_unit_label.Font = new Font(UTIL.Families[0], 9.75F);
            Uw2_unit_label.ForeColor = SystemColors.ControlDark;
            Uw2_unit_label.Location = new Point(800, 55);
            Uw2_unit_label.Name = "Uw2_unit_label";
            Uw2_unit_label.Size = new Size(58, 15);
            Uw2_unit_label.TabIndex = 98;
            Uw2_unit_label.Text = "W/m2·K";
            Uw2_unit_label.Visible = false;
            // 
            // Uw2_textBox
            // 
            Uw2_textBox.BackColor = Color.White;
            Uw2_textBox.BorderStyle = BorderStyle.None;
            Uw2_textBox.Enabled = false;
            Uw2_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uw2_textBox.ForeColor = SystemColors.ControlDark;
            Uw2_textBox.Location = new Point(675, 56);
            Uw2_textBox.Name = "Uw2_textBox";
            Uw2_textBox.Size = new Size(116, 15);
            Uw2_textBox.TabIndex = 100;
            Uw2_textBox.TextAlign = HorizontalAlignment.Center;
            Uw2_textBox.TextChanged += Uw2_textBox_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Glass_tabPage);
            tabControl1.Controls.Add(Frame_tabPage);
            tabControl1.Controls.Add(Install_tabPage);
            tabControl1.DisplayStyleProvider.BorderColor = SystemColors.Control;
            tabControl1.DisplayStyleProvider.BorderColorHot = SystemColors.Control;
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
            tabControl1.Dock = DockStyle.Bottom;
            tabControl1.HotTrack = true;
            tabControl1.ItemSize = new Size(128, 20);
            tabControl1.Location = new Point(0, 302);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 239);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 19;
            // 
            // Glass_tabPage
            // 
            Glass_tabPage.Controls.Add(SpacerName_textBox2);
            Glass_tabPage.Controls.Add(GlassName_textBox2);
            Glass_tabPage.Controls.Add(Ug_textBox);
            Glass_tabPage.Controls.Add(Ug_label);
            Glass_tabPage.Controls.Add(Ug_unit_label);
            Glass_tabPage.Controls.Add(label23);
            Glass_tabPage.Controls.Add(g_textBox);
            Glass_tabPage.Controls.Add(label26);
            Glass_tabPage.Controls.Add(τD65_SNA_textBox);
            Glass_tabPage.Controls.Add(label12);
            Glass_tabPage.Controls.Add(label9);
            Glass_tabPage.Controls.Add(Psi_g_fix_textBox);
            Glass_tabPage.Controls.Add(Psi_fix_label);
            Glass_tabPage.Controls.Add(Psi_fix_unit_label);
            Glass_tabPage.Controls.Add(Psi_open_label);
            Glass_tabPage.Controls.Add(Psi_g_open_textBox);
            Glass_tabPage.Controls.Add(Psi_open_unit_label);
            Glass_tabPage.Location = new Point(4, 25);
            Glass_tabPage.Name = "Glass_tabPage";
            Glass_tabPage.Padding = new Padding(3);
            Glass_tabPage.Size = new Size(992, 210);
            Glass_tabPage.TabIndex = 0;
            Glass_tabPage.Text = "유리 및 간봉";
            Glass_tabPage.UseVisualStyleBackColor = true;
            // 
            // SpacerName_textBox2
            // 
            SpacerName_textBox2.BackColor = Color.White;
            SpacerName_textBox2.BorderStyle = BorderStyle.None;
            SpacerName_textBox2.Enabled = false;
            SpacerName_textBox2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            SpacerName_textBox2.ForeColor = Color.Black;
            SpacerName_textBox2.Location = new Point(480, 36);
            SpacerName_textBox2.Name = "SpacerName_textBox2";
            SpacerName_textBox2.Size = new Size(120, 15);
            SpacerName_textBox2.TabIndex = 99;
            // 
            // GlassName_textBox2
            // 
            GlassName_textBox2.BackColor = Color.White;
            GlassName_textBox2.BorderStyle = BorderStyle.None;
            GlassName_textBox2.Enabled = false;
            GlassName_textBox2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            GlassName_textBox2.ForeColor = Color.Black;
            GlassName_textBox2.Location = new Point(36, 36);
            GlassName_textBox2.Name = "GlassName_textBox2";
            GlassName_textBox2.Size = new Size(120, 15);
            GlassName_textBox2.TabIndex = 98;
            // 
            // Ug_textBox
            // 
            Ug_textBox.BackColor = Color.White;
            Ug_textBox.BorderStyle = BorderStyle.None;
            Ug_textBox.Enabled = false;
            Ug_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Ug_textBox.ForeColor = SystemColors.ControlDark;
            Ug_textBox.Location = new Point(177, 129);
            Ug_textBox.Name = "Ug_textBox";
            Ug_textBox.Size = new Size(116, 15);
            Ug_textBox.TabIndex = 66;
            Ug_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Ug_label
            // 
            Ug_label.AutoSize = true;
            Ug_label.Font = new Font(UTIL.Families[0], 9.75F);
            Ug_label.ForeColor = SystemColors.ControlDark;
            Ug_label.Location = new Point(36, 128);
            Ug_label.Name = "Ug_label";
            Ug_label.Size = new Size(114, 15);
            Ug_label.TabIndex = 65;
            Ug_label.Text = "[Ug] 유리 열관류율";
            // 
            // Ug_unit_label
            // 
            Ug_unit_label.AutoSize = true;
            Ug_unit_label.Font = new Font(UTIL.Families[0], 9.75F);
            Ug_unit_label.ForeColor = SystemColors.ControlDark;
            Ug_unit_label.Location = new Point(296, 128);
            Ug_unit_label.Name = "Ug_unit_label";
            Ug_unit_label.Size = new Size(58, 15);
            Ug_unit_label.TabIndex = 64;
            Ug_unit_label.Text = "W/m2·K";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font(UTIL.Families[0], 9.75F);
            label23.ForeColor = SystemColors.ControlDark;
            label23.Location = new Point(36, 97);
            label23.Name = "label23";
            label23.Size = new Size(102, 15);
            label23.TabIndex = 68;
            label23.Text = "[g] 태양열취득률";
            // 
            // g_textBox
            // 
            g_textBox.BackColor = Color.White;
            g_textBox.BorderStyle = BorderStyle.None;
            g_textBox.Enabled = false;
            g_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            g_textBox.ForeColor = SystemColors.ControlDark;
            g_textBox.Location = new Point(177, 98);
            g_textBox.Name = "g_textBox";
            g_textBox.Size = new Size(116, 15);
            g_textBox.TabIndex = 69;
            g_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font(UTIL.Families[0], 9.75F);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(36, 66);
            label26.Name = "label26";
            label26.Size = new Size(132, 15);
            label26.TabIndex = 71;
            label26.Text = "[τD65,SNA] 빛투과율";
            // 
            // τD65_SNA_textBox
            // 
            τD65_SNA_textBox.BackColor = Color.White;
            τD65_SNA_textBox.BorderStyle = BorderStyle.None;
            τD65_SNA_textBox.Enabled = false;
            τD65_SNA_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            τD65_SNA_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA_textBox.Location = new Point(177, 67);
            τD65_SNA_textBox.Name = "τD65_SNA_textBox";
            τD65_SNA_textBox.Size = new Size(116, 15);
            τD65_SNA_textBox.TabIndex = 72;
            τD65_SNA_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font(UTIL.Families[0], 9.75F);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(316, 66);
            label12.Name = "label12";
            label12.Size = new Size(15, 15);
            label12.TabIndex = 97;
            label12.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font(UTIL.Families[0], 9.75F);
            label9.ForeColor = SystemColors.ControlDark;
            label9.Location = new Point(316, 97);
            label9.Name = "label9";
            label9.Size = new Size(15, 15);
            label9.TabIndex = 96;
            label9.Text = "-";
            // 
            // Psi_g_fix_textBox
            // 
            Psi_g_fix_textBox.BackColor = Color.White;
            Psi_g_fix_textBox.BorderStyle = BorderStyle.None;
            Psi_g_fix_textBox.Enabled = false;
            Psi_g_fix_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_g_fix_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_fix_textBox.Location = new Point(675, 67);
            Psi_g_fix_textBox.Name = "Psi_g_fix_textBox";
            Psi_g_fix_textBox.Size = new Size(116, 15);
            Psi_g_fix_textBox.TabIndex = 75;
            Psi_g_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_fix_label
            // 
            Psi_fix_label.AutoSize = true;
            Psi_fix_label.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_fix_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_label.Location = new Point(484, 66);
            Psi_fix_label.Name = "Psi_fix_label";
            Psi_fix_label.Size = new Size(162, 15);
            Psi_fix_label.TabIndex = 74;
            Psi_fix_label.Text = "[Ψg] 선형열관류율(고정창)";
            // 
            // Psi_fix_unit_label
            // 
            Psi_fix_unit_label.AutoSize = true;
            Psi_fix_unit_label.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_fix_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_unit_label.Location = new Point(802, 66);
            Psi_fix_unit_label.Name = "Psi_fix_unit_label";
            Psi_fix_unit_label.Size = new Size(52, 15);
            Psi_fix_unit_label.TabIndex = 73;
            Psi_fix_unit_label.Text = "W/m·K";
            // 
            // Psi_open_label
            // 
            Psi_open_label.AutoSize = true;
            Psi_open_label.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_open_label.ForeColor = SystemColors.ControlDark;
            Psi_open_label.Location = new Point(484, 97);
            Psi_open_label.Name = "Psi_open_label";
            Psi_open_label.Size = new Size(162, 15);
            Psi_open_label.TabIndex = 77;
            Psi_open_label.Text = "[Ψg] 선형열관류율(개폐창)";
            // 
            // Psi_g_open_textBox
            // 
            Psi_g_open_textBox.BackColor = Color.White;
            Psi_g_open_textBox.BorderStyle = BorderStyle.None;
            Psi_g_open_textBox.Enabled = false;
            Psi_g_open_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_g_open_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_open_textBox.Location = new Point(675, 98);
            Psi_g_open_textBox.Name = "Psi_g_open_textBox";
            Psi_g_open_textBox.Size = new Size(116, 15);
            Psi_g_open_textBox.TabIndex = 78;
            Psi_g_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_open_unit_label
            // 
            Psi_open_unit_label.AutoSize = true;
            Psi_open_unit_label.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_open_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_open_unit_label.Location = new Point(802, 97);
            Psi_open_unit_label.Name = "Psi_open_unit_label";
            Psi_open_unit_label.Size = new Size(52, 15);
            Psi_open_unit_label.TabIndex = 76;
            Psi_open_unit_label.Text = "W/m·K";
            // 
            // Frame_tabPage
            // 
            Frame_tabPage.BackColor = Color.White;
            Frame_tabPage.Controls.Add(label10);
            Frame_tabPage.Controls.Add(label8);
            Frame_tabPage.Controls.Add(label22);
            Frame_tabPage.Controls.Add(df_btw_textBox);
            Frame_tabPage.Controls.Add(df_fix_textBox);
            Frame_tabPage.Controls.Add(df_open_textBox);
            Frame_tabPage.Controls.Add(Uf_btw_textBox);
            Frame_tabPage.Controls.Add(Uf_fix_textBox);
            Frame_tabPage.Controls.Add(Uf_open_textBox);
            Frame_tabPage.Controls.Add(FrameMaterial_textBox);
            Frame_tabPage.Controls.Add(label19);
            Frame_tabPage.Controls.Add(label18);
            Frame_tabPage.Controls.Add(label17);
            Frame_tabPage.Controls.Add(label15);
            Frame_tabPage.Controls.Add(label14);
            Frame_tabPage.Controls.Add(WindowFrame_pictureBox);
            Frame_tabPage.Location = new Point(4, 25);
            Frame_tabPage.Name = "Frame_tabPage";
            Frame_tabPage.Padding = new Padding(3);
            Frame_tabPage.Size = new Size(992, 210);
            Frame_tabPage.TabIndex = 1;
            Frame_tabPage.Text = "프레임";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font(UTIL.Families[0], 9.75F);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(354, 148);
            label10.Name = "label10";
            label10.Size = new Size(19, 15);
            label10.TabIndex = 106;
            label10.Text = "m";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font(UTIL.Families[0], 9.75F);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(338, 110);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 105;
            label8.Text = "W/m2·K";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font(UTIL.Families[0], 9.75F);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(294, 148);
            label22.Name = "label22";
            label22.Size = new Size(31, 15);
            label22.TabIndex = 104;
            label22.Text = "두께";
            // 
            // df_btw_textBox
            // 
            df_btw_textBox.BackColor = Color.White;
            df_btw_textBox.BorderStyle = BorderStyle.None;
            df_btw_textBox.Enabled = false;
            df_btw_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            df_btw_textBox.ForeColor = SystemColors.ControlDark;
            df_btw_textBox.Location = new Point(672, 149);
            df_btw_textBox.Name = "df_btw_textBox";
            df_btw_textBox.Size = new Size(116, 15);
            df_btw_textBox.TabIndex = 103;
            df_btw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // df_fix_textBox
            // 
            df_fix_textBox.BackColor = Color.White;
            df_fix_textBox.BorderStyle = BorderStyle.None;
            df_fix_textBox.Enabled = false;
            df_fix_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            df_fix_textBox.ForeColor = SystemColors.ControlDark;
            df_fix_textBox.Location = new Point(533, 149);
            df_fix_textBox.Name = "df_fix_textBox";
            df_fix_textBox.Size = new Size(116, 15);
            df_fix_textBox.TabIndex = 102;
            df_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // df_open_textBox
            // 
            df_open_textBox.BackColor = Color.White;
            df_open_textBox.BorderStyle = BorderStyle.None;
            df_open_textBox.Enabled = false;
            df_open_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            df_open_textBox.ForeColor = SystemColors.ControlDark;
            df_open_textBox.Location = new Point(394, 149);
            df_open_textBox.Name = "df_open_textBox";
            df_open_textBox.Size = new Size(116, 15);
            df_open_textBox.TabIndex = 101;
            df_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uf_btw_textBox
            // 
            Uf_btw_textBox.BackColor = Color.White;
            Uf_btw_textBox.BorderStyle = BorderStyle.None;
            Uf_btw_textBox.Enabled = false;
            Uf_btw_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uf_btw_textBox.ForeColor = SystemColors.ControlDark;
            Uf_btw_textBox.Location = new Point(672, 111);
            Uf_btw_textBox.Name = "Uf_btw_textBox";
            Uf_btw_textBox.Size = new Size(116, 15);
            Uf_btw_textBox.TabIndex = 97;
            Uf_btw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uf_fix_textBox
            // 
            Uf_fix_textBox.BackColor = Color.White;
            Uf_fix_textBox.BorderStyle = BorderStyle.None;
            Uf_fix_textBox.Enabled = false;
            Uf_fix_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uf_fix_textBox.ForeColor = SystemColors.ControlDark;
            Uf_fix_textBox.Location = new Point(533, 111);
            Uf_fix_textBox.Name = "Uf_fix_textBox";
            Uf_fix_textBox.Size = new Size(116, 15);
            Uf_fix_textBox.TabIndex = 96;
            Uf_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uf_open_textBox
            // 
            Uf_open_textBox.BackColor = Color.White;
            Uf_open_textBox.BorderStyle = BorderStyle.None;
            Uf_open_textBox.Enabled = false;
            Uf_open_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uf_open_textBox.ForeColor = SystemColors.ControlDark;
            Uf_open_textBox.Location = new Point(394, 111);
            Uf_open_textBox.Name = "Uf_open_textBox";
            Uf_open_textBox.Size = new Size(116, 15);
            Uf_open_textBox.TabIndex = 95;
            Uf_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameMaterial_textBox
            // 
            FrameMaterial_textBox.BackColor = Color.White;
            FrameMaterial_textBox.BorderStyle = BorderStyle.None;
            FrameMaterial_textBox.Enabled = false;
            FrameMaterial_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            FrameMaterial_textBox.ForeColor = SystemColors.ControlDark;
            FrameMaterial_textBox.Location = new Point(394, 42);
            FrameMaterial_textBox.Name = "FrameMaterial_textBox";
            FrameMaterial_textBox.Size = new Size(116, 15);
            FrameMaterial_textBox.TabIndex = 93;
            FrameMaterial_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font(UTIL.Families[0], 9.75F);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(282, 110);
            label19.Name = "label19";
            label19.Size = new Size(55, 15);
            label19.TabIndex = 100;
            label19.Text = "열관류율";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font(UTIL.Families[0], 9.75F);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(704, 80);
            label18.Name = "label18";
            label18.Size = new Size(51, 15);
            label18.TabIndex = 99;
            label18.Text = "프레임C";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font(UTIL.Families[0], 9.75F);
            label17.ForeColor = SystemColors.ControlDark;
            label17.Location = new Point(565, 80);
            label17.Name = "label17";
            label17.Size = new Size(51, 15);
            label17.TabIndex = 98;
            label17.Text = "프레임B";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font(UTIL.Families[0], 9.75F);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(426, 80);
            label15.Name = "label15";
            label15.Size = new Size(51, 15);
            label15.TabIndex = 94;
            label15.Text = "프레임A";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font(UTIL.Families[0], 9.75F);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(294, 41);
            label14.Name = "label14";
            label14.Size = new Size(31, 15);
            label14.TabIndex = 92;
            label14.Text = "재료";
            // 
            // WindowFrame_pictureBox
            // 
            WindowFrame_pictureBox.Location = new Point(36, 19);
            WindowFrame_pictureBox.Name = "WindowFrame_pictureBox";
            WindowFrame_pictureBox.Size = new Size(200, 179);
            WindowFrame_pictureBox.TabIndex = 0;
            WindowFrame_pictureBox.TabStop = false;
            // 
            // Install_tabPage
            // 
            Install_tabPage.BackColor = Color.White;
            Install_tabPage.Controls.Add(label44);
            Install_tabPage.Controls.Add(label45);
            Install_tabPage.Controls.Add(Psi_InstallButtom_textBox);
            Install_tabPage.Controls.Add(Psi_InstallSide_textBox);
            Install_tabPage.Controls.Add(Psi_InstallTop_textBox);
            Install_tabPage.Controls.Add(label41);
            Install_tabPage.Controls.Add(label40);
            Install_tabPage.Controls.Add(label24);
            Install_tabPage.Controls.Add(label33);
            Install_tabPage.Controls.Add(label38);
            Install_tabPage.Controls.Add(WindowInstall_pictureBox);
            Install_tabPage.Location = new Point(4, 25);
            Install_tabPage.Name = "Install_tabPage";
            Install_tabPage.Padding = new Padding(3);
            Install_tabPage.Size = new Size(992, 210);
            Install_tabPage.TabIndex = 3;
            Install_tabPage.Text = "설치열교";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Font = new Font(UTIL.Families[0], 9.75F);
            label44.ForeColor = SystemColors.ControlDark;
            label44.Location = new Point(455, 169);
            label44.Name = "label44";
            label44.Size = new Size(52, 15);
            label44.TabIndex = 125;
            label44.Text = "W/m·K";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new Font(UTIL.Families[0], 9.75F);
            label45.ForeColor = SystemColors.ControlDark;
            label45.Location = new Point(232, 169);
            label45.Name = "label45";
            label45.Size = new Size(31, 15);
            label45.TabIndex = 124;
            label45.Text = "하부";
            // 
            // Psi_InstallButtom_textBox
            // 
            Psi_InstallButtom_textBox.BackColor = Color.White;
            Psi_InstallButtom_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallButtom_textBox.Enabled = false;
            Psi_InstallButtom_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_InstallButtom_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallButtom_textBox.Location = new Point(301, 169);
            Psi_InstallButtom_textBox.Name = "Psi_InstallButtom_textBox";
            Psi_InstallButtom_textBox.Size = new Size(116, 15);
            Psi_InstallButtom_textBox.TabIndex = 122;
            Psi_InstallButtom_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_InstallSide_textBox
            // 
            Psi_InstallSide_textBox.BackColor = Color.White;
            Psi_InstallSide_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallSide_textBox.Enabled = false;
            Psi_InstallSide_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_InstallSide_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallSide_textBox.Location = new Point(301, 133);
            Psi_InstallSide_textBox.Name = "Psi_InstallSide_textBox";
            Psi_InstallSide_textBox.Size = new Size(116, 15);
            Psi_InstallSide_textBox.TabIndex = 114;
            Psi_InstallSide_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_InstallTop_textBox
            // 
            Psi_InstallTop_textBox.BackColor = Color.White;
            Psi_InstallTop_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallTop_textBox.Enabled = false;
            Psi_InstallTop_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Psi_InstallTop_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallTop_textBox.Location = new Point(301, 97);
            Psi_InstallTop_textBox.Name = "Psi_InstallTop_textBox";
            Psi_InstallTop_textBox.Size = new Size(116, 15);
            Psi_InstallTop_textBox.TabIndex = 108;
            Psi_InstallTop_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font(UTIL.Families[0], 9.75F);
            label41.ForeColor = SystemColors.ControlDark;
            label41.Location = new Point(455, 133);
            label41.Name = "label41";
            label41.Size = new Size(52, 15);
            label41.TabIndex = 119;
            label41.Text = "W/m·K";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font(UTIL.Families[0], 9.75F);
            label40.ForeColor = SystemColors.ControlDark;
            label40.Location = new Point(455, 97);
            label40.Name = "label40";
            label40.Size = new Size(52, 15);
            label40.TabIndex = 118;
            label40.Text = "W/m·K";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font(UTIL.Families[0], 9.75F);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(232, 133);
            label24.Name = "label24";
            label24.Size = new Size(31, 15);
            label24.TabIndex = 117;
            label24.Text = "측면";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font(UTIL.Families[0], 9.75F);
            label33.ForeColor = SystemColors.ControlDark;
            label33.Location = new Point(232, 97);
            label33.Name = "label33";
            label33.Size = new Size(31, 15);
            label33.TabIndex = 113;
            label33.Text = "상부";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font(UTIL.Families[0], 9.75F);
            label38.ForeColor = SystemColors.ControlDark;
            label38.Location = new Point(320, 61);
            label38.Name = "label38";
            label38.Size = new Size(79, 15);
            label38.TabIndex = 107;
            label38.Text = "선형열관류율";
            // 
            // WindowInstall_pictureBox
            // 
            WindowInstall_pictureBox.Location = new Point(74, 25);
            WindowInstall_pictureBox.Name = "WindowInstall_pictureBox";
            WindowInstall_pictureBox.Size = new Size(71, 180);
            WindowInstall_pictureBox.TabIndex = 0;
            WindowInstall_pictureBox.TabStop = false;
            // 
            // Install_textBox
            // 
            Install_textBox.BackColor = Color.White;
            Install_textBox.BorderStyle = BorderStyle.None;
            Install_textBox.Enabled = false;
            Install_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Install_textBox.ForeColor = SystemColors.ControlDark;
            Install_textBox.Location = new Point(327, 230);
            Install_textBox.Name = "Install_textBox";
            Install_textBox.Size = new Size(120, 15);
            Install_textBox.TabIndex = 95;
            Install_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Install_button
            // 
            Install_button.BackColor = SystemColors.ControlLight;
            Install_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Install_button.FlatStyle = FlatStyle.System;
            Install_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            Install_button.Location = new Point(449, 226);
            Install_button.Margin = new Padding(0);
            Install_button.Name = "Install_button";
            Install_button.Size = new Size(23, 23);
            Install_button.TabIndex = 94;
            Install_button.Text = "+";
            Install_button.UseVisualStyleBackColor = false;
            Install_button.Click += Install_button_Click;
            // 
            // SpacerName_textBox
            // 
            SpacerName_textBox.BackColor = Color.White;
            SpacerName_textBox.BorderStyle = BorderStyle.None;
            SpacerName_textBox.Enabled = false;
            SpacerName_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            SpacerName_textBox.ForeColor = SystemColors.ControlDark;
            SpacerName_textBox.Location = new Point(175, 143);
            SpacerName_textBox.Name = "SpacerName_textBox";
            SpacerName_textBox.Size = new Size(120, 15);
            SpacerName_textBox.TabIndex = 93;
            SpacerName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Spacer_button
            // 
            Spacer_button.BackColor = SystemColors.ControlLight;
            Spacer_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Spacer_button.FlatStyle = FlatStyle.System;
            Spacer_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            Spacer_button.Location = new Point(296, 139);
            Spacer_button.Margin = new Padding(0);
            Spacer_button.Name = "Spacer_button";
            Spacer_button.Size = new Size(23, 23);
            Spacer_button.TabIndex = 92;
            Spacer_button.Text = "+";
            Spacer_button.UseVisualStyleBackColor = false;
            Spacer_button.Click += Spacer_button_Click;
            // 
            // GlassName_textBox
            // 
            GlassName_textBox.BackColor = Color.White;
            GlassName_textBox.BorderStyle = BorderStyle.None;
            GlassName_textBox.Enabled = false;
            GlassName_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            GlassName_textBox.ForeColor = SystemColors.ControlDark;
            GlassName_textBox.Location = new Point(175, 85);
            GlassName_textBox.Name = "GlassName_textBox";
            GlassName_textBox.Size = new Size(120, 15);
            GlassName_textBox.TabIndex = 91;
            GlassName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // GlassDB_button
            // 
            GlassDB_button.BackColor = SystemColors.ControlLight;
            GlassDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            GlassDB_button.FlatStyle = FlatStyle.System;
            GlassDB_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            GlassDB_button.Location = new Point(296, 81);
            GlassDB_button.Margin = new Padding(0);
            GlassDB_button.Name = "GlassDB_button";
            GlassDB_button.Size = new Size(23, 23);
            GlassDB_button.TabIndex = 90;
            GlassDB_button.Text = "+";
            GlassDB_button.UseVisualStyleBackColor = false;
            GlassDB_button.Click += Glass_button_Click;
            // 
            // FrameName_textBox
            // 
            FrameName_textBox.BackColor = Color.White;
            FrameName_textBox.BorderStyle = BorderStyle.None;
            FrameName_textBox.Enabled = false;
            FrameName_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            FrameName_textBox.ForeColor = SystemColors.ControlDark;
            FrameName_textBox.Location = new Point(175, 115);
            FrameName_textBox.Name = "FrameName_textBox";
            FrameName_textBox.Size = new Size(120, 15);
            FrameName_textBox.TabIndex = 89;
            FrameName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameDB_button
            // 
            FrameDB_button.BackColor = SystemColors.ControlLight;
            FrameDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FrameDB_button.FlatStyle = FlatStyle.System;
            FrameDB_button.Font = new Font(UTIL.Families[0], 11.9999981F, FontStyle.Bold);
            FrameDB_button.Location = new Point(297, 111);
            FrameDB_button.Margin = new Padding(0);
            FrameDB_button.Name = "FrameDB_button";
            FrameDB_button.Size = new Size(23, 23);
            FrameDB_button.TabIndex = 88;
            FrameDB_button.Text = "+";
            FrameDB_button.UseVisualStyleBackColor = false;
            FrameDB_button.Click += FrameDB_button_Click;
            // 
            // DiIndi_comboBox
            // 
            DiIndi_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            DiIndi_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            DiIndi_comboBox.FormattingEnabled = true;
            DiIndi_comboBox.Location = new Point(327, 23);
            DiIndi_comboBox.Name = "DiIndi_comboBox";
            DiIndi_comboBox.Size = new Size(120, 23);
            DiIndi_comboBox.TabIndex = 55;
            DiIndi_comboBox.SelectedIndexChanged += DiIndil_comboBox_SelectedIndexChanged;
            // 
            // Install_comboBox
            // 
            Install_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Install_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Install_comboBox.FormattingEnabled = true;
            Install_comboBox.Location = new Point(175, 226);
            Install_comboBox.Name = "Install_comboBox";
            Install_comboBox.Size = new Size(120, 23);
            Install_comboBox.TabIndex = 52;
            Install_comboBox.SelectedIndexChanged += Install_comboBox_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font(UTIL.Families[0], 9.75F);
            label16.Location = new Point(78, 230);
            label16.Name = "label16";
            label16.Size = new Size(31, 15);
            label16.TabIndex = 51;
            label16.Text = "설치";
            // 
            // Spacer_label
            // 
            Spacer_label.AutoSize = true;
            Spacer_label.Font = new Font(UTIL.Families[0], 9.75F);
            Spacer_label.Location = new Point(78, 143);
            Spacer_label.Name = "Spacer_label";
            Spacer_label.Size = new Size(31, 15);
            Spacer_label.TabIndex = 45;
            Spacer_label.Text = "간봉";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font(UTIL.Families[0], 9.75F);
            label11.Location = new Point(78, 85);
            label11.Name = "label11";
            label11.Size = new Size(31, 15);
            label11.TabIndex = 41;
            label11.Text = "유리";
            // 
            // Frame_comboBox
            // 
            Frame_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Frame_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Frame_comboBox.FormattingEnabled = true;
            Frame_comboBox.Location = new Point(175, 52);
            Frame_comboBox.Name = "Frame_comboBox";
            Frame_comboBox.Size = new Size(120, 23);
            Frame_comboBox.TabIndex = 40;
            Frame_comboBox.SelectedIndexChanged += Frame_comboBox_SelectedIndexChanged;
            // 
            // Frame_label
            // 
            Frame_label.AutoSize = true;
            Frame_label.Font = new Font(UTIL.Families[0], 9.75F);
            Frame_label.Location = new Point(78, 114);
            Frame_label.Name = "Frame_label";
            Frame_label.Size = new Size(43, 15);
            Frame_label.TabIndex = 39;
            Frame_label.Text = "프레임";
            // 
            // Uw_comboBox
            // 
            Uw_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Uw_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uw_comboBox.FormattingEnabled = true;
            Uw_comboBox.Location = new Point(175, 23);
            Uw_comboBox.Name = "Uw_comboBox";
            Uw_comboBox.Size = new Size(120, 23);
            Uw_comboBox.TabIndex = 38;
            Uw_comboBox.SelectedIndexChanged += UwMethod_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font(UTIL.Families[0], 9.75F);
            label25.Location = new Point(78, 27);
            label25.Name = "label25";
            label25.Size = new Size(77, 15);
            label25.TabIndex = 37;
            label25.Text = "Uw 적용방법";
            // 
            // WindowType_pictureBox
            // 
            WindowType_pictureBox.Location = new Point(1016, 84);
            WindowType_pictureBox.Name = "WindowType_pictureBox";
            WindowType_pictureBox.Size = new Size(151, 200);
            WindowType_pictureBox.TabIndex = 36;
            WindowType_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1096, 600);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 90;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(1002, 600);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 91;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            Previous_button.Click += Previous_button_Click;
            // 
            // g2_textBox
            // 
            g2_textBox.BackColor = SystemColors.InactiveBorder;
            g2_textBox.BorderStyle = BorderStyle.None;
            g2_textBox.Enabled = false;
            g2_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            g2_textBox.ForeColor = SystemColors.ControlDark;
            g2_textBox.Location = new Point(1082, 50);
            g2_textBox.Name = "g2_textBox";
            g2_textBox.ReadOnly = true;
            g2_textBox.Size = new Size(66, 15);
            g2_textBox.TabIndex = 145;
            g2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font(UTIL.Families[0], 9.75F);
            label13.ForeColor = SystemColors.ControlDark;
            label13.Location = new Point(1007, 50);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 144;
            label13.Text = "태양열취득률";
            // 
            // Uw3_textBox
            // 
            Uw3_textBox.BackColor = SystemColors.InactiveBorder;
            Uw3_textBox.BorderStyle = BorderStyle.None;
            Uw3_textBox.Enabled = false;
            Uw3_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Uw3_textBox.ForeColor = SystemColors.ControlDark;
            Uw3_textBox.Location = new Point(1082, 285);
            Uw3_textBox.Name = "Uw3_textBox";
            Uw3_textBox.ReadOnly = true;
            Uw3_textBox.Size = new Size(66, 15);
            Uw3_textBox.TabIndex = 142;
            Uw3_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uw3_unit_label
            // 
            Uw3_unit_label.AutoSize = true;
            Uw3_unit_label.Font = new Font(UTIL.Families[0], 9.75F);
            Uw3_unit_label.ForeColor = SystemColors.ControlDark;
            Uw3_unit_label.Location = new Point(1148, 284);
            Uw3_unit_label.Name = "Uw3_unit_label";
            Uw3_unit_label.Size = new Size(58, 15);
            Uw3_unit_label.TabIndex = 143;
            Uw3_unit_label.Text = "W/m2·K";
            // 
            // Uw3_label
            // 
            Uw3_label.AutoSize = true;
            Uw3_label.Font = new Font(UTIL.Families[0], 9.75F);
            Uw3_label.ForeColor = SystemColors.ControlDark;
            Uw3_label.Location = new Point(1007, 285);
            Uw3_label.Name = "Uw3_label";
            Uw3_label.Size = new Size(79, 15);
            Uw3_label.TabIndex = 141;
            Uw3_label.Text = "창호열관류율";
            // 
            // τD65_SNA2_textBox
            // 
            τD65_SNA2_textBox.BackColor = SystemColors.InactiveBorder;
            τD65_SNA2_textBox.BorderStyle = BorderStyle.None;
            τD65_SNA2_textBox.Enabled = false;
            τD65_SNA2_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            τD65_SNA2_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA2_textBox.Location = new Point(1082, 71);
            τD65_SNA2_textBox.Name = "τD65_SNA2_textBox";
            τD65_SNA2_textBox.ReadOnly = true;
            τD65_SNA2_textBox.Size = new Size(66, 15);
            τD65_SNA2_textBox.TabIndex = 147;
            τD65_SNA2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font(UTIL.Families[0], 9.75F);
            label27.ForeColor = SystemColors.ControlDark;
            label27.Location = new Point(1019, 71);
            label27.Name = "label27";
            label27.Size = new Size(55, 15);
            label27.TabIndex = 146;
            label27.Text = "빛투과율";
            // 
            // ConstructionWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(τD65_SNA2_textBox);
            Controls.Add(label27);
            Controls.Add(g2_textBox);
            Controls.Add(label13);
            Controls.Add(Uw3_textBox);
            Controls.Add(Uw3_unit_label);
            Controls.Add(Uw3_label);
            Controls.Add(Previous_button);
            Controls.Add(Save_button);
            Controls.Add(WindowType_pictureBox);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConstructionWindow";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            Glass_tabPage.ResumeLayout(false);
            Glass_tabPage.PerformLayout();
            Frame_tabPage.ResumeLayout(false);
            Frame_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).EndInit();
            Install_tabPage.ResumeLayout(false);
            Install_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowInstall_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Name_textBox;
        private Label label2;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private CustomComboBox AdditionalWindow_comboBox;
        private Panel GeneralPanel;
        private Panel panel2;
        private TextBox Type_textBox;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private CustomComboBox DiIndi_comboBox;
        private CustomComboBox comboBox9;
        private CustomComboBox Install_comboBox;
        private Label label16;
        private Label Spacer_label;
        private Label label11;
        private Label Frame_label;
        private CustomComboBox Uw_comboBox;
        private Label label25;
        private Label Psi_open_unit_label;
        private TextBox Psi_g_open_textBox;
        private Label Psi_open_label;
        private Label Psi_fix_unit_label;
        private TextBox Psi_g_fix_textBox;
        private Label Psi_fix_label;
        private TextBox τD65_SNA_textBox;
        private Label label26;
        private TextBox g_textBox;
        private Label label23;
        private Label Ug_unit_label;
        private TextBox Ug_textBox;
        private Label Ug_label;
        private Button FrameDB_button;
        private TextBox FrameName_textBox;
        private CustomComboBox Frame_comboBox;
        private TextBox GlassName_textBox;
        private Button GlassDB_button;
        private TextBox SpacerName_textBox;
        private Button Spacer_button;
        private TextBox Install_textBox;
        private Button Install_button;
        private Label label12;
        private Label label9;
        private PictureBox WindowType_pictureBox;
        private Button Save_button;
        private TextBox WinNum_textBox;
        private PictureBox Icon_pictureBox;
        private Label Uw2_unit_label;
        private TextBox Uw2_textBox;
        private Label Uw2_label;
        private TextBox AdditionalWindow_textBox;
        private Button Previous_button;
        private TabPage Install_tabPage;
        private Label label44;
        private Label label45;
        private TextBox Psi_InstallButtom_textBox;
        private TextBox Psi_InstallSide_textBox;
        private TextBox Psi_InstallTop_textBox;
        private Label label41;
        private Label label40;
        private Label label24;
        private Label label33;
        private Label label38;
        private PictureBox WindowInstall_pictureBox;
        private TabPage Frame_tabPage;
        private Label label22;
        private TextBox df_btw_textBox;
        private TextBox df_fix_textBox;
        private TextBox df_open_textBox;
        private TextBox Uf_btw_textBox;
        private TextBox Uf_fix_textBox;
        private TextBox Uf_open_textBox;
        private TextBox FrameMaterial_textBox;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label15;
        private Label label14;
        private PictureBox WindowFrame_pictureBox;
        private CustomTabControl tabControl1;
        private TextBox Size_textBox;
        private Label label1;
        private Button ImportSize_button;
        private Label label10;
        private Label label8;
        private TextBox g2_textBox;
        private Label label13;
        private TextBox Uw3_textBox;
        private Label Uw3_unit_label;
        private Label Uw3_label;
        private TextBox τD65_SNA2_textBox;
        private Label label27;
        private Label label20;
        private TabPage Glass_tabPage;
        private TextBox SpacerName_textBox2;
        private TextBox GlassName_textBox2;
        private Label label21;
        private TextBox g3_textBox;
        private Label label28;
    }
}
