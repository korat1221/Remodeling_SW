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
            AdditionalWindow_comboBox = new ComboBox();
            Name_textBox = new TextBox();
            panel2 = new Panel();
            Size_textBox = new TextBox();
            label1 = new Label();
            ImportSize_button = new Button();
            Uw2_unit_label = new Label();
            Uw2_textBox = new TextBox();
            Uw2_label = new Label();
            label12 = new Label();
            label9 = new Label();
            Install_textBox = new TextBox();
            Install_button = new Button();
            SpacerName_textBox = new TextBox();
            Spacer_button = new Button();
            GlassName_textBox = new TextBox();
            GlassDB_button = new Button();
            FrameName_textBox = new TextBox();
            FrameDB_button = new Button();
            Psi_open_unit_label = new Label();
            Psi_g_open_textBox = new TextBox();
            Psi_open_label = new Label();
            Psi_fix_unit_label = new Label();
            Psi_g_fix_textBox = new TextBox();
            Psi_fix_label = new Label();
            τD65_SNA_textBox = new TextBox();
            label26 = new Label();
            g_textBox = new TextBox();
            label23 = new Label();
            Ug_unit_label = new Label();
            Ug_textBox = new TextBox();
            Ug_label = new Label();
            DiIndi_comboBox = new ComboBox();
            Install_comboBox = new ComboBox();
            label16 = new Label();
            Spacer_label = new Label();
            label11 = new Label();
            Frame_comboBox = new ComboBox();
            Frame_label = new Label();
            Uw_comboBox = new ComboBox();
            label25 = new Label();
            label4 = new Label();
            WindowType_pictureBox = new PictureBox();
            Save_button = new Button();
            Previous_button = new Button();
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
            tabControl1 = new TabControl();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).BeginInit();
            Install_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowInstall_pictureBox).BeginInit();
            Frame_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
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
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // AdditionalWindow_textBox
            // 
            AdditionalWindow_textBox.BackColor = Color.White;
            AdditionalWindow_textBox.BorderStyle = BorderStyle.None;
            AdditionalWindow_textBox.Enabled = false;
            AdditionalWindow_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AdditionalWindow_textBox.ForeColor = SystemColors.ControlText;
            AdditionalWindow_textBox.Location = new Point(673, 42);
            AdditionalWindow_textBox.Name = "AdditionalWindow_textBox";
            AdditionalWindow_textBox.Size = new Size(67, 15);
            AdditionalWindow_textBox.TabIndex = 93;
            AdditionalWindow_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 91;
            Icon_pictureBox.TabStop = false;
            // 
            // WinNum_textBox
            // 
            WinNum_textBox.BackColor = Color.White;
            WinNum_textBox.BorderStyle = BorderStyle.None;
            WinNum_textBox.Enabled = false;
            WinNum_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WinNum_textBox.ForeColor = SystemColors.ControlDark;
            WinNum_textBox.Location = new Point(80, 19);
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
            groupBox1.Location = new Point(365, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(259, 95);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(146, 69);
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
            radioButton4.Location = new Point(17, 69);
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
            radioButton3.Location = new Point(146, 43);
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
            radioButton2.Location = new Point(17, 43);
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
            radioButton1.Location = new Point(17, 17);
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
            Type_textBox.BackColor = Color.White;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(177, 58);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 23;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(142, 19);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 2;
            label2.Text = "명칭";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(328, 71);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 21;
            label7.Text = "덧댐";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(328, 45);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 20;
            label6.Text = "신규";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(142, 58);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "TYPE";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(327, 19);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 11;
            label5.Text = "기존";
            // 
            // AdditionalWindow_comboBox
            // 
            AdditionalWindow_comboBox.FormattingEnabled = true;
            AdditionalWindow_comboBox.Location = new Point(673, 64);
            AdditionalWindow_comboBox.Name = "AdditionalWindow_comboBox";
            AdditionalWindow_comboBox.Size = new Size(120, 23);
            AdditionalWindow_comboBox.TabIndex = 0;
            AdditionalWindow_comboBox.SelectedIndexChanged += AdditionalWindow_comboBox_SelectedIndexChanged;
            // 
            // Name_textBox
            // 
            Name_textBox.Location = new Point(177, 16);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 4;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(Size_textBox);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(ImportSize_button);
            panel2.Controls.Add(Uw2_unit_label);
            panel2.Controls.Add(Uw2_textBox);
            panel2.Controls.Add(Uw2_label);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(Install_textBox);
            panel2.Controls.Add(Install_button);
            panel2.Controls.Add(SpacerName_textBox);
            panel2.Controls.Add(Spacer_button);
            panel2.Controls.Add(GlassName_textBox);
            panel2.Controls.Add(GlassDB_button);
            panel2.Controls.Add(FrameName_textBox);
            panel2.Controls.Add(FrameDB_button);
            panel2.Controls.Add(Psi_open_unit_label);
            panel2.Controls.Add(Psi_g_open_textBox);
            panel2.Controls.Add(Psi_open_label);
            panel2.Controls.Add(Psi_fix_unit_label);
            panel2.Controls.Add(Psi_g_fix_textBox);
            panel2.Controls.Add(Psi_fix_label);
            panel2.Controls.Add(τD65_SNA_textBox);
            panel2.Controls.Add(label26);
            panel2.Controls.Add(g_textBox);
            panel2.Controls.Add(label23);
            panel2.Controls.Add(Ug_unit_label);
            panel2.Controls.Add(Ug_textBox);
            panel2.Controls.Add(Ug_label);
            panel2.Controls.Add(DiIndi_comboBox);
            panel2.Controls.Add(Install_comboBox);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(Spacer_label);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(Frame_comboBox);
            panel2.Controls.Add(Frame_label);
            panel2.Controls.Add(Uw_comboBox);
            panel2.Controls.Add(label25);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 307);
            panel2.TabIndex = 18;
            // 
            // Size_textBox
            // 
            Size_textBox.BackColor = Color.White;
            Size_textBox.BorderStyle = BorderStyle.None;
            Size_textBox.Enabled = false;
            Size_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(78, 263);
            label1.Name = "label1";
            label1.Size = new Size(54, 16);
            label1.TabIndex = 102;
            label1.Text = "창호 치수";
            // 
            // ImportSize_button
            // 
            ImportSize_button.BackColor = SystemColors.ControlLight;
            ImportSize_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            ImportSize_button.FlatStyle = FlatStyle.System;
            ImportSize_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
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
            Uw2_unit_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw2_unit_label.ForeColor = SystemColors.ControlDark;
            Uw2_unit_label.Location = new Point(800, 27);
            Uw2_unit_label.Name = "Uw2_unit_label";
            Uw2_unit_label.Size = new Size(50, 16);
            Uw2_unit_label.TabIndex = 98;
            Uw2_unit_label.Text = "W/m²·K";
            Uw2_unit_label.Visible = false;
            // 
            // Uw2_textBox
            // 
            Uw2_textBox.BackColor = Color.White;
            Uw2_textBox.BorderStyle = BorderStyle.None;
            Uw2_textBox.Enabled = false;
            Uw2_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw2_textBox.ForeColor = SystemColors.ControlDark;
            Uw2_textBox.Location = new Point(675, 27);
            Uw2_textBox.Name = "Uw2_textBox";
            Uw2_textBox.Size = new Size(116, 15);
            Uw2_textBox.TabIndex = 100;
            Uw2_textBox.TextAlign = HorizontalAlignment.Center;
            Uw2_textBox.TextChanged += Uw2_textBox_TextChanged;
            // 
            // Uw2_label
            // 
            Uw2_label.AutoSize = true;
            Uw2_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw2_label.ForeColor = SystemColors.ControlDark;
            Uw2_label.Location = new Point(484, 27);
            Uw2_label.Name = "Uw2_label";
            Uw2_label.Size = new Size(103, 16);
            Uw2_label.TabIndex = 99;
            Uw2_label.Text = "[Uw] 창호열관류율";
            Uw2_label.Visible = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(821, 114);
            label12.Name = "label12";
            label12.Size = new Size(11, 16);
            label12.TabIndex = 97;
            label12.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlDark;
            label9.Location = new Point(821, 85);
            label9.Name = "label9";
            label9.Size = new Size(11, 16);
            label9.TabIndex = 96;
            label9.Text = "-";
            // 
            // Install_textBox
            // 
            Install_textBox.BackColor = Color.White;
            Install_textBox.BorderStyle = BorderStyle.None;
            Install_textBox.Enabled = false;
            Install_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Install_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            SpacerName_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Spacer_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            GlassName_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            GlassDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
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
            FrameName_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameName_textBox.ForeColor = SystemColors.ControlDark;
            FrameName_textBox.Location = new Point(327, 56);
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
            FrameDB_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            FrameDB_button.Location = new Point(449, 52);
            FrameDB_button.Margin = new Padding(0);
            FrameDB_button.Name = "FrameDB_button";
            FrameDB_button.Size = new Size(23, 23);
            FrameDB_button.TabIndex = 88;
            FrameDB_button.Text = "+";
            FrameDB_button.UseVisualStyleBackColor = false;
            FrameDB_button.Click += FrameDB_button_Click;
            // 
            // Psi_open_unit_label
            // 
            Psi_open_unit_label.AutoSize = true;
            Psi_open_unit_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_open_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_open_unit_label.Location = new Point(800, 172);
            Psi_open_unit_label.Name = "Psi_open_unit_label";
            Psi_open_unit_label.Size = new Size(46, 16);
            Psi_open_unit_label.TabIndex = 76;
            Psi_open_unit_label.Text = "W/m·K";
            // 
            // Psi_g_open_textBox
            // 
            Psi_g_open_textBox.BackColor = Color.White;
            Psi_g_open_textBox.BorderStyle = BorderStyle.None;
            Psi_g_open_textBox.Enabled = false;
            Psi_g_open_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_open_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_open_textBox.Location = new Point(675, 172);
            Psi_g_open_textBox.Name = "Psi_g_open_textBox";
            Psi_g_open_textBox.Size = new Size(116, 15);
            Psi_g_open_textBox.TabIndex = 78;
            Psi_g_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_open_label
            // 
            Psi_open_label.AutoSize = true;
            Psi_open_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_open_label.ForeColor = SystemColors.ControlDark;
            Psi_open_label.Location = new Point(484, 172);
            Psi_open_label.Name = "Psi_open_label";
            Psi_open_label.Size = new Size(144, 16);
            Psi_open_label.TabIndex = 77;
            Psi_open_label.Text = "[Ψg] 선형열관류율(개폐창)";
            // 
            // Psi_fix_unit_label
            // 
            Psi_fix_unit_label.AutoSize = true;
            Psi_fix_unit_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_fix_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_unit_label.Location = new Point(800, 143);
            Psi_fix_unit_label.Name = "Psi_fix_unit_label";
            Psi_fix_unit_label.Size = new Size(46, 16);
            Psi_fix_unit_label.TabIndex = 73;
            Psi_fix_unit_label.Text = "W/m·K";
            // 
            // Psi_g_fix_textBox
            // 
            Psi_g_fix_textBox.BackColor = Color.White;
            Psi_g_fix_textBox.BorderStyle = BorderStyle.None;
            Psi_g_fix_textBox.Enabled = false;
            Psi_g_fix_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_fix_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_fix_textBox.Location = new Point(675, 143);
            Psi_g_fix_textBox.Name = "Psi_g_fix_textBox";
            Psi_g_fix_textBox.Size = new Size(116, 15);
            Psi_g_fix_textBox.TabIndex = 75;
            Psi_g_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_fix_label
            // 
            Psi_fix_label.AutoSize = true;
            Psi_fix_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_fix_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_label.Location = new Point(484, 143);
            Psi_fix_label.Name = "Psi_fix_label";
            Psi_fix_label.Size = new Size(144, 16);
            Psi_fix_label.TabIndex = 74;
            Psi_fix_label.Text = "[Ψg] 선형열관류율(고정창)";
            // 
            // τD65_SNA_textBox
            // 
            τD65_SNA_textBox.BackColor = Color.White;
            τD65_SNA_textBox.BorderStyle = BorderStyle.None;
            τD65_SNA_textBox.Enabled = false;
            τD65_SNA_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            τD65_SNA_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA_textBox.Location = new Point(675, 114);
            τD65_SNA_textBox.Name = "τD65_SNA_textBox";
            τD65_SNA_textBox.Size = new Size(116, 15);
            τD65_SNA_textBox.TabIndex = 72;
            τD65_SNA_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(484, 114);
            label26.Name = "label26";
            label26.Size = new Size(176, 16);
            label26.TabIndex = 71;
            label26.Text = "[τD65,SNA] 빛투과율(덧댐포함)";
            // 
            // g_textBox
            // 
            g_textBox.BackColor = Color.White;
            g_textBox.BorderStyle = BorderStyle.None;
            g_textBox.Enabled = false;
            g_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            g_textBox.ForeColor = SystemColors.ControlDark;
            g_textBox.Location = new Point(675, 85);
            g_textBox.Name = "g_textBox";
            g_textBox.Size = new Size(116, 15);
            g_textBox.TabIndex = 69;
            g_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.ForeColor = SystemColors.ControlDark;
            label23.Location = new Point(484, 85);
            label23.Name = "label23";
            label23.Size = new Size(144, 16);
            label23.TabIndex = 68;
            label23.Text = "[g] 태양열취득률(덧댐포함)";
            // 
            // Ug_unit_label
            // 
            Ug_unit_label.AutoSize = true;
            Ug_unit_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_unit_label.ForeColor = SystemColors.ControlDark;
            Ug_unit_label.Location = new Point(800, 56);
            Ug_unit_label.Name = "Ug_unit_label";
            Ug_unit_label.Size = new Size(50, 16);
            Ug_unit_label.TabIndex = 64;
            Ug_unit_label.Text = "W/m²·K";
            // 
            // Ug_textBox
            // 
            Ug_textBox.BackColor = Color.White;
            Ug_textBox.BorderStyle = BorderStyle.None;
            Ug_textBox.Enabled = false;
            Ug_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_textBox.ForeColor = SystemColors.ControlDark;
            Ug_textBox.Location = new Point(675, 56);
            Ug_textBox.Name = "Ug_textBox";
            Ug_textBox.Size = new Size(116, 15);
            Ug_textBox.TabIndex = 66;
            Ug_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Ug_label
            // 
            Ug_label.AutoSize = true;
            Ug_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_label.ForeColor = SystemColors.ControlDark;
            Ug_label.Location = new Point(484, 56);
            Ug_label.Name = "Ug_label";
            Ug_label.Size = new Size(157, 16);
            Ug_label.TabIndex = 65;
            Ug_label.Text = "[Ug] 유리 열관류율(덧댐제외)";
            // 
            // DiIndi_comboBox
            // 
            DiIndi_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            DiIndi_comboBox.FormattingEnabled = true;
            DiIndi_comboBox.Location = new Point(327, 23);
            DiIndi_comboBox.Name = "DiIndi_comboBox";
            DiIndi_comboBox.Size = new Size(120, 24);
            DiIndi_comboBox.TabIndex = 55;
            DiIndi_comboBox.SelectedIndexChanged += DiIndil_comboBox_SelectedIndexChanged;
            // 
            // Install_comboBox
            // 
            Install_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Install_comboBox.FormattingEnabled = true;
            Install_comboBox.Location = new Point(175, 226);
            Install_comboBox.Name = "Install_comboBox";
            Install_comboBox.Size = new Size(120, 24);
            Install_comboBox.TabIndex = 52;
            Install_comboBox.SelectedIndexChanged += Install_comboBox_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(78, 230);
            label16.Name = "label16";
            label16.Size = new Size(29, 16);
            label16.TabIndex = 51;
            label16.Text = "설치";
            // 
            // Spacer_label
            // 
            Spacer_label.AutoSize = true;
            Spacer_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Spacer_label.Location = new Point(78, 143);
            Spacer_label.Name = "Spacer_label";
            Spacer_label.Size = new Size(29, 16);
            Spacer_label.TabIndex = 45;
            Spacer_label.Text = "간봉";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(78, 85);
            label11.Name = "label11";
            label11.Size = new Size(29, 16);
            label11.TabIndex = 41;
            label11.Text = "유리";
            // 
            // Frame_comboBox
            // 
            Frame_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Frame_comboBox.FormattingEnabled = true;
            Frame_comboBox.Location = new Point(175, 52);
            Frame_comboBox.Name = "Frame_comboBox";
            Frame_comboBox.Size = new Size(120, 24);
            Frame_comboBox.TabIndex = 40;
            Frame_comboBox.SelectedIndexChanged += Frame_comboBox_SelectedIndexChanged;
            // 
            // Frame_label
            // 
            Frame_label.AutoSize = true;
            Frame_label.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Frame_label.Location = new Point(78, 56);
            Frame_label.Name = "Frame_label";
            Frame_label.Size = new Size(40, 16);
            Frame_label.TabIndex = 39;
            Frame_label.Text = "프레임";
            // 
            // Uw_comboBox
            // 
            Uw_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_comboBox.FormattingEnabled = true;
            Uw_comboBox.Location = new Point(175, 23);
            Uw_comboBox.Name = "Uw_comboBox";
            Uw_comboBox.Size = new Size(120, 24);
            Uw_comboBox.TabIndex = 38;
            Uw_comboBox.SelectedIndexChanged += UwMethod_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(78, 27);
            label25.Name = "label25";
            label25.Size = new Size(73, 16);
            label25.TabIndex = 37;
            label25.Text = "Uw 적용방법";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 118);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 0;
            label4.Text = "창호 구성요소";
            // 
            // WindowType_pictureBox
            // 
            WindowType_pictureBox.Location = new Point(999, 54);
            WindowType_pictureBox.Name = "WindowType_pictureBox";
            WindowType_pictureBox.Size = new Size(151, 200);
            WindowType_pictureBox.TabIndex = 36;
            WindowType_pictureBox.TabStop = false;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1093, 661);
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
            Previous_button.Location = new Point(999, 661);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 91;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            Previous_button.Click += Previous_button_Click;
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
            Install_tabPage.Location = new Point(4, 24);
            Install_tabPage.Name = "Install_tabPage";
            Install_tabPage.Padding = new Padding(3);
            Install_tabPage.Size = new Size(969, 211);
            Install_tabPage.TabIndex = 1;
            Install_tabPage.Text = "설치열교 정보";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label44.ForeColor = SystemColors.ControlDark;
            label44.Location = new Point(455, 169);
            label44.Name = "label44";
            label44.Size = new Size(46, 16);
            label44.TabIndex = 125;
            label44.Text = "W/m·K";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label45.ForeColor = SystemColors.ControlDark;
            label45.Location = new Point(232, 169);
            label45.Name = "label45";
            label45.Size = new Size(29, 16);
            label45.TabIndex = 124;
            label45.Text = "하부";
            // 
            // Psi_InstallButtom_textBox
            // 
            Psi_InstallButtom_textBox.BackColor = Color.White;
            Psi_InstallButtom_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallButtom_textBox.Enabled = false;
            Psi_InstallButtom_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Psi_InstallSide_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Psi_InstallTop_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label41.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label41.ForeColor = SystemColors.ControlDark;
            label41.Location = new Point(455, 133);
            label41.Name = "label41";
            label41.Size = new Size(46, 16);
            label41.TabIndex = 119;
            label41.Text = "W/m·K";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label40.ForeColor = SystemColors.ControlDark;
            label40.Location = new Point(455, 97);
            label40.Name = "label40";
            label40.Size = new Size(46, 16);
            label40.TabIndex = 118;
            label40.Text = "W/m·K";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(232, 133);
            label24.Name = "label24";
            label24.Size = new Size(29, 16);
            label24.TabIndex = 117;
            label24.Text = "측면";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ControlDark;
            label33.Location = new Point(232, 97);
            label33.Name = "label33";
            label33.Size = new Size(29, 16);
            label33.TabIndex = 113;
            label33.Text = "상부";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label38.ForeColor = SystemColors.ControlDark;
            label38.Location = new Point(320, 61);
            label38.Name = "label38";
            label38.Size = new Size(73, 16);
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
            // Frame_tabPage
            // 
            Frame_tabPage.BackColor = Color.White;
            Frame_tabPage.BorderStyle = BorderStyle.FixedSingle;
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
            Frame_tabPage.Location = new Point(4, 24);
            Frame_tabPage.Name = "Frame_tabPage";
            Frame_tabPage.Padding = new Padding(3);
            Frame_tabPage.Size = new Size(969, 211);
            Frame_tabPage.TabIndex = 0;
            Frame_tabPage.Text = "프레임 세부정보";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = SystemColors.ControlDark;
            label10.Location = new Point(354, 148);
            label10.Name = "label10";
            label10.Size = new Size(18, 16);
            label10.TabIndex = 106;
            label10.Text = "m";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ControlDark;
            label8.Location = new Point(338, 110);
            label8.Name = "label8";
            label8.Size = new Size(50, 16);
            label8.TabIndex = 105;
            label8.Text = "W/m²·K";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(294, 148);
            label22.Name = "label22";
            label22.Size = new Size(29, 16);
            label22.TabIndex = 104;
            label22.Text = "두께";
            // 
            // df_btw_textBox
            // 
            df_btw_textBox.BackColor = Color.White;
            df_btw_textBox.BorderStyle = BorderStyle.None;
            df_btw_textBox.Enabled = false;
            df_btw_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            df_fix_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            df_open_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Uf_btw_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Uf_fix_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Uf_open_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            FrameMaterial_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label19.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(282, 110);
            label19.Name = "label19";
            label19.Size = new Size(51, 16);
            label19.TabIndex = 100;
            label19.Text = "열관류율";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.ForeColor = SystemColors.ControlDark;
            label18.Location = new Point(704, 80);
            label18.Name = "label18";
            label18.Size = new Size(49, 16);
            label18.TabIndex = 99;
            label18.Text = "프레임C";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.ForeColor = SystemColors.ControlDark;
            label17.Location = new Point(565, 80);
            label17.Name = "label17";
            label17.Size = new Size(49, 16);
            label17.TabIndex = 98;
            label17.Text = "프레임B";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(426, 80);
            label15.Name = "label15";
            label15.Size = new Size(49, 16);
            label15.TabIndex = 94;
            label15.Text = "프레임A";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label14.ForeColor = SystemColors.ControlDark;
            label14.Location = new Point(294, 41);
            label14.Name = "label14";
            label14.Size = new Size(29, 16);
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
            // tabControl1
            // 
            tabControl1.Controls.Add(Frame_tabPage);
            tabControl1.Controls.Add(Install_tabPage);
            tabControl1.Location = new Point(12, 447);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 239);
            tabControl1.TabIndex = 19;
            // 
            // ConstructionWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Previous_button);
            Controls.Add(Save_button);
            Controls.Add(WindowType_pictureBox);
            Controls.Add(tabControl1);
            Controls.Add(label4);
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
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).EndInit();
            Install_tabPage.ResumeLayout(false);
            Install_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowInstall_pictureBox).EndInit();
            Frame_tabPage.ResumeLayout(false);
            Frame_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).EndInit();
            tabControl1.ResumeLayout(false);
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
        private ComboBox AdditionalWindow_comboBox;
        private Panel GeneralPanel;
        private Panel panel2;
        private TextBox Type_textBox;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label4;
        private ComboBox DiIndi_comboBox;
        private ComboBox comboBox9;
        private ComboBox Install_comboBox;
        private Label label16;
        private Label Spacer_label;
        private Label label11;
        private Label Frame_label;
        private ComboBox Uw_comboBox;
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
        private ComboBox Frame_comboBox;
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
        private TabControl tabControl1;
        private TextBox Size_textBox;
        private Label label1;
        private Button ImportSize_button;
        private Label label10;
        private Label label8;
    }
}
