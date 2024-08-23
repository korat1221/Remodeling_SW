using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace main.subcontents.ConstructionWindow
{
    partial class SubWindow
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
            label21 = new Label();
            g3_textBox = new TextBox();
            label28 = new Label();
            label1 = new Label();
            Install_textBox = new TextBox();
            SpacerName_textBox = new TextBox();
            GlassName_textBox = new TextBox();
            FrameName_textBox = new TextBox();
            label35 = new Label();
            Uw_inst_textBox = new TextBox();
            label36 = new Label();
            Uw_unit_label = new Label();
            Uw_label = new Label();
            DiIndi_comboBox = new CustomComboBox();
            Install_comboBox = new CustomComboBox();
            label16 = new Label();
            Spacer_label = new Label();
            label11 = new Label();
            Frame_comboBox = new CustomComboBox();
            Frame_label = new Label();
            Uw_comboBox = new CustomComboBox();
            label25 = new Label();
            Uw_textBox = new TextBox();
            label12 = new Label();
            label9 = new Label();
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
            label4 = new Label();
            tabControl1 = new CustomTabControl();
            Glass_tabPage = new TabPage();
            SpacerName_textBox2 = new TextBox();
            GlassName_textBox2 = new TextBox();
            Frame_tabPage = new TabPage();
            label22 = new Label();
            df_btw_textBox = new TextBox();
            df_fix_textBox = new TextBox();
            df_open_textBox = new TextBox();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            Uf_btw_textBox = new TextBox();
            Uf_fix_textBox = new TextBox();
            Uf_open_textBox = new TextBox();
            label15 = new Label();
            FrameMaterial_textBox = new TextBox();
            label14 = new Label();
            WindowFrame_pictureBox = new PictureBox();
            Install_tabPage = new TabPage();
            label43 = new Label();
            label44 = new Label();
            label45 = new Label();
            d_InstallButtom_textBox = new TextBox();
            Psi_InstallButtom_textBox = new TextBox();
            label37 = new Label();
            label42 = new Label();
            label41 = new Label();
            label40 = new Label();
            label24 = new Label();
            d_InstallSide_textBox = new TextBox();
            Psi_InstallSide_textBox = new TextBox();
            label33 = new Label();
            label34 = new Label();
            d_InstallTop_textBox = new TextBox();
            Psi_InstallTop_textBox = new TextBox();
            label38 = new Label();
            dUinst_textBox = new TextBox();
            label39 = new Label();
            WindowInstall_pictureBox = new PictureBox();
            Size_tabPage = new TabPage();
            label56 = new Label();
            label57 = new Label();
            Lg_open_textBox = new TextBox();
            label58 = new Label();
            label59 = new Label();
            Af_open_textBox = new TextBox();
            label60 = new Label();
            label61 = new Label();
            Lg_fix_textBox = new TextBox();
            label62 = new Label();
            label63 = new Label();
            label64 = new Label();
            Af_btw_textBox = new TextBox();
            label65 = new Label();
            Af_fix_textBox = new TextBox();
            label54 = new Label();
            label55 = new Label();
            Ag_open_textBox = new TextBox();
            label52 = new Label();
            label53 = new Label();
            Area_textBox = new TextBox();
            label46 = new Label();
            label47 = new Label();
            Ag_fix_textBox = new TextBox();
            label48 = new Label();
            label49 = new Label();
            label50 = new Label();
            Height_textBox = new TextBox();
            label51 = new Label();
            Width_textBox = new TextBox();
            WindowType_pictureBox = new PictureBox();
            Previous_button = new Button();
            τD65_SNA2_textBox = new TextBox();
            label27 = new Label();
            g2_textBox = new TextBox();
            label13 = new Label();
            Uw_inst2_textBox = new TextBox();
            Uw3_unit_label = new Label();
            Uw3_label = new Label();
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
            Size_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).BeginInit();
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
            AdditionalWindow_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            WinNum_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            radioButton5.Enabled = false;
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
            radioButton4.Enabled = false;
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
            radioButton3.Enabled = false;
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
            radioButton2.Enabled = false;
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
            radioButton1.Enabled = false;
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
            Type_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(177, 58);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.ReadOnly = true;
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
            AdditionalWindow_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            AdditionalWindow_comboBox.Enabled = false;
            AdditionalWindow_comboBox.FormattingEnabled = true;
            AdditionalWindow_comboBox.Location = new Point(673, 64);
            AdditionalWindow_comboBox.Name = "AdditionalWindow_comboBox";
            AdditionalWindow_comboBox.Size = new Size(120, 24);
            AdditionalWindow_comboBox.TabIndex = 0;
            AdditionalWindow_comboBox.SelectedIndexChanged += AdditionalWindow_comboBox_SelectedIndexChanged;
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(177, 16);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.ReadOnly = true;
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 4;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label21);
            panel2.Controls.Add(g3_textBox);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(Install_textBox);
            panel2.Controls.Add(SpacerName_textBox);
            panel2.Controls.Add(GlassName_textBox);
            panel2.Controls.Add(FrameName_textBox);
            panel2.Controls.Add(label35);
            panel2.Controls.Add(Uw_inst_textBox);
            panel2.Controls.Add(label36);
            panel2.Controls.Add(Uw_unit_label);
            panel2.Controls.Add(Uw_label);
            panel2.Controls.Add(DiIndi_comboBox);
            panel2.Controls.Add(Install_comboBox);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(Spacer_label);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(Frame_comboBox);
            panel2.Controls.Add(Frame_label);
            panel2.Controls.Add(Uw_comboBox);
            panel2.Controls.Add(label25);
            panel2.Controls.Add(Uw_textBox);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 307);
            panel2.TabIndex = 18;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label21.ForeColor = SystemColors.ControlDark;
            label21.Location = new Point(484, 84);
            label21.Name = "label21";
            label21.Size = new Size(92, 16);
            label21.TabIndex = 108;
            label21.Text = "[g] 태양열취득률";
            // 
            // g3_textBox
            // 
            g3_textBox.BackColor = Color.White;
            g3_textBox.BorderStyle = BorderStyle.None;
            g3_textBox.Enabled = false;
            g3_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            g3_textBox.ForeColor = SystemColors.ControlDark;
            g3_textBox.Location = new Point(675, 85);
            g3_textBox.Name = "g3_textBox";
            g3_textBox.Size = new Size(116, 15);
            g3_textBox.TabIndex = 109;
            g3_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label28.ForeColor = SystemColors.ControlDark;
            label28.Location = new Point(820, 84);
            label28.Name = "label28";
            label28.Size = new Size(11, 16);
            label28.TabIndex = 110;
            label28.Text = "-";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(78, 114);
            label1.Name = "label1";
            label1.Size = new Size(40, 16);
            label1.TabIndex = 101;
            label1.Text = "프레임";
            // 
            // Install_textBox
            // 
            Install_textBox.BackColor = Color.White;
            Install_textBox.BorderStyle = BorderStyle.None;
            Install_textBox.Enabled = false;
            Install_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Install_textBox.ForeColor = SystemColors.ControlDark;
            Install_textBox.Location = new Point(327, 230);
            Install_textBox.Name = "Install_textBox";
            Install_textBox.ReadOnly = true;
            Install_textBox.Size = new Size(120, 15);
            Install_textBox.TabIndex = 95;
            Install_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // SpacerName_textBox
            // 
            SpacerName_textBox.BackColor = Color.White;
            SpacerName_textBox.BorderStyle = BorderStyle.None;
            SpacerName_textBox.Enabled = false;
            SpacerName_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SpacerName_textBox.ForeColor = SystemColors.ControlDark;
            SpacerName_textBox.Location = new Point(175, 143);
            SpacerName_textBox.Name = "SpacerName_textBox";
            SpacerName_textBox.ReadOnly = true;
            SpacerName_textBox.Size = new Size(120, 15);
            SpacerName_textBox.TabIndex = 93;
            SpacerName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // GlassName_textBox
            // 
            GlassName_textBox.BackColor = Color.White;
            GlassName_textBox.BorderStyle = BorderStyle.None;
            GlassName_textBox.Enabled = false;
            GlassName_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GlassName_textBox.ForeColor = SystemColors.ControlDark;
            GlassName_textBox.Location = new Point(175, 85);
            GlassName_textBox.Name = "GlassName_textBox";
            GlassName_textBox.ReadOnly = true;
            GlassName_textBox.Size = new Size(120, 15);
            GlassName_textBox.TabIndex = 91;
            GlassName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // FrameName_textBox
            // 
            FrameName_textBox.BackColor = Color.White;
            FrameName_textBox.BorderStyle = BorderStyle.None;
            FrameName_textBox.Enabled = false;
            FrameName_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameName_textBox.ForeColor = SystemColors.ControlDark;
            FrameName_textBox.Location = new Point(175, 115);
            FrameName_textBox.Name = "FrameName_textBox";
            FrameName_textBox.ReadOnly = true;
            FrameName_textBox.Size = new Size(120, 15);
            FrameName_textBox.TabIndex = 89;
            FrameName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label35.ForeColor = SystemColors.ControlDark;
            label35.Location = new Point(800, 229);
            label35.Name = "label35";
            label35.Size = new Size(50, 16);
            label35.TabIndex = 85;
            label35.Text = "W/m²·K";
            // 
            // Uw_inst_textBox
            // 
            Uw_inst_textBox.BackColor = Color.White;
            Uw_inst_textBox.BorderStyle = BorderStyle.None;
            Uw_inst_textBox.Enabled = false;
            Uw_inst_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_inst_textBox.ForeColor = SystemColors.ControlDark;
            Uw_inst_textBox.Location = new Point(675, 230);
            Uw_inst_textBox.Name = "Uw_inst_textBox";
            Uw_inst_textBox.ReadOnly = true;
            Uw_inst_textBox.Size = new Size(116, 15);
            Uw_inst_textBox.TabIndex = 87;
            Uw_inst_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label36.ForeColor = SystemColors.ControlDark;
            label36.Location = new Point(484, 229);
            label36.Name = "label36";
            label36.Size = new Size(129, 16);
            label36.TabIndex = 86;
            label36.Text = "[Uw,inst.] 유효열관류율";
            // 
            // Uw_unit_label
            // 
            Uw_unit_label.AutoSize = true;
            Uw_unit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_unit_label.ForeColor = SystemColors.ControlDark;
            Uw_unit_label.Location = new Point(800, 55);
            Uw_unit_label.Name = "Uw_unit_label";
            Uw_unit_label.Size = new Size(50, 16);
            Uw_unit_label.TabIndex = 79;
            Uw_unit_label.Text = "W/m²·K";
            // 
            // Uw_label
            // 
            Uw_label.AutoSize = true;
            Uw_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_label.ForeColor = SystemColors.ControlDark;
            Uw_label.Location = new Point(484, 55);
            Uw_label.Name = "Uw_label";
            Uw_label.Size = new Size(103, 16);
            Uw_label.TabIndex = 80;
            Uw_label.Text = "[Uw] 창호열관류율";
            // 
            // DiIndi_comboBox
            // 
            DiIndi_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            DiIndi_comboBox.Enabled = false;
            DiIndi_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Install_comboBox.Enabled = false;
            Install_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(78, 230);
            label16.Name = "label16";
            label16.Size = new Size(29, 16);
            label16.TabIndex = 51;
            label16.Text = "설치";
            // 
            // Spacer_label
            // 
            Spacer_label.AutoSize = true;
            Spacer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Spacer_label.Location = new Point(78, 143);
            Spacer_label.Name = "Spacer_label";
            Spacer_label.Size = new Size(29, 16);
            Spacer_label.TabIndex = 45;
            Spacer_label.Text = "간봉";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(78, 85);
            label11.Name = "label11";
            label11.Size = new Size(29, 16);
            label11.TabIndex = 41;
            label11.Text = "유리";
            // 
            // Frame_comboBox
            // 
            Frame_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Frame_comboBox.Enabled = false;
            Frame_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Frame_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Frame_label.Location = new Point(78, 56);
            Frame_label.Name = "Frame_label";
            Frame_label.Size = new Size(54, 16);
            Frame_label.TabIndex = 39;
            Frame_label.Text = "창호 유형";
            // 
            // Uw_comboBox
            // 
            Uw_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Uw_comboBox.Enabled = false;
            Uw_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(78, 27);
            label25.Name = "label25";
            label25.Size = new Size(73, 16);
            label25.TabIndex = 37;
            label25.Text = "Uw 적용방법";
            // 
            // Uw_textBox
            // 
            Uw_textBox.BackColor = Color.White;
            Uw_textBox.BorderStyle = BorderStyle.None;
            Uw_textBox.Enabled = false;
            Uw_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_textBox.ForeColor = SystemColors.ControlDark;
            Uw_textBox.Location = new Point(675, 56);
            Uw_textBox.Name = "Uw_textBox";
            Uw_textBox.ReadOnly = true;
            Uw_textBox.Size = new Size(116, 15);
            Uw_textBox.TabIndex = 81;
            Uw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = SystemColors.ControlDark;
            label12.Location = new Point(316, 66);
            label12.Name = "label12";
            label12.Size = new Size(11, 16);
            label12.TabIndex = 97;
            label12.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ControlDark;
            label9.Location = new Point(316, 97);
            label9.Name = "label9";
            label9.Size = new Size(11, 16);
            label9.TabIndex = 96;
            label9.Text = "-";
            // 
            // Psi_open_unit_label
            // 
            Psi_open_unit_label.AutoSize = true;
            Psi_open_unit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_open_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_open_unit_label.Location = new Point(802, 97);
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
            Psi_g_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_open_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_open_textBox.Location = new Point(675, 98);
            Psi_g_open_textBox.Name = "Psi_g_open_textBox";
            Psi_g_open_textBox.ReadOnly = true;
            Psi_g_open_textBox.Size = new Size(116, 15);
            Psi_g_open_textBox.TabIndex = 78;
            Psi_g_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_open_label
            // 
            Psi_open_label.AutoSize = true;
            Psi_open_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_open_label.ForeColor = SystemColors.ControlDark;
            Psi_open_label.Location = new Point(484, 97);
            Psi_open_label.Name = "Psi_open_label";
            Psi_open_label.Size = new Size(144, 16);
            Psi_open_label.TabIndex = 77;
            Psi_open_label.Text = "[Ψg] 선형열관류율(개폐창)";
            // 
            // Psi_fix_unit_label
            // 
            Psi_fix_unit_label.AutoSize = true;
            Psi_fix_unit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_fix_unit_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_unit_label.Location = new Point(802, 66);
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
            Psi_g_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_g_fix_textBox.ForeColor = SystemColors.ControlDark;
            Psi_g_fix_textBox.Location = new Point(675, 67);
            Psi_g_fix_textBox.Name = "Psi_g_fix_textBox";
            Psi_g_fix_textBox.ReadOnly = true;
            Psi_g_fix_textBox.Size = new Size(116, 15);
            Psi_g_fix_textBox.TabIndex = 75;
            Psi_g_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_fix_label
            // 
            Psi_fix_label.AutoSize = true;
            Psi_fix_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_fix_label.ForeColor = SystemColors.ControlDark;
            Psi_fix_label.Location = new Point(484, 66);
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
            τD65_SNA_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            τD65_SNA_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA_textBox.Location = new Point(177, 67);
            τD65_SNA_textBox.Name = "τD65_SNA_textBox";
            τD65_SNA_textBox.ReadOnly = true;
            τD65_SNA_textBox.Size = new Size(116, 15);
            τD65_SNA_textBox.TabIndex = 72;
            τD65_SNA_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label26.ForeColor = SystemColors.ControlDark;
            label26.Location = new Point(36, 66);
            label26.Name = "label26";
            label26.Size = new Size(124, 16);
            label26.TabIndex = 71;
            label26.Text = "[τD65,SNA] 빛투과율";
            // 
            // g_textBox
            // 
            g_textBox.BackColor = Color.White;
            g_textBox.BorderStyle = BorderStyle.None;
            g_textBox.Enabled = false;
            g_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            g_textBox.ForeColor = SystemColors.ControlDark;
            g_textBox.Location = new Point(177, 98);
            g_textBox.Name = "g_textBox";
            g_textBox.ReadOnly = true;
            g_textBox.Size = new Size(116, 15);
            g_textBox.TabIndex = 69;
            g_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label23.ForeColor = SystemColors.ControlDark;
            label23.Location = new Point(36, 97);
            label23.Name = "label23";
            label23.Size = new Size(92, 16);
            label23.TabIndex = 68;
            label23.Text = "[g] 태양열취득률";
            // 
            // Ug_unit_label
            // 
            Ug_unit_label.AutoSize = true;
            Ug_unit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_unit_label.ForeColor = SystemColors.ControlDark;
            Ug_unit_label.Location = new Point(296, 128);
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
            Ug_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_textBox.ForeColor = SystemColors.ControlDark;
            Ug_textBox.Location = new Point(177, 129);
            Ug_textBox.Name = "Ug_textBox";
            Ug_textBox.ReadOnly = true;
            Ug_textBox.Size = new Size(116, 15);
            Ug_textBox.TabIndex = 66;
            Ug_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Ug_label
            // 
            Ug_label.AutoSize = true;
            Ug_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ug_label.ForeColor = SystemColors.ControlDark;
            Ug_label.Location = new Point(36, 128);
            Ug_label.Name = "Ug_label";
            Ug_label.Size = new Size(105, 16);
            Ug_label.TabIndex = 65;
            Ug_label.Text = "[Ug] 유리 열관류율";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(12, 118);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 0;
            label4.Text = "창호 구성요소";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Glass_tabPage);
            tabControl1.Controls.Add(Frame_tabPage);
            tabControl1.Controls.Add(Install_tabPage);
            tabControl1.Controls.Add(Size_tabPage);
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
            tabControl1.Location = new Point(12, 447);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 239);
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
            Glass_tabPage.Controls.Add(label12);
            Glass_tabPage.Controls.Add(label23);
            Glass_tabPage.Controls.Add(label9);
            Glass_tabPage.Controls.Add(g_textBox);
            Glass_tabPage.Controls.Add(label26);
            Glass_tabPage.Controls.Add(τD65_SNA_textBox);
            Glass_tabPage.Controls.Add(Psi_fix_label);
            Glass_tabPage.Controls.Add(Psi_g_fix_textBox);
            Glass_tabPage.Controls.Add(Psi_fix_unit_label);
            Glass_tabPage.Controls.Add(Psi_open_label);
            Glass_tabPage.Controls.Add(Psi_open_unit_label);
            Glass_tabPage.Controls.Add(Psi_g_open_textBox);
            Glass_tabPage.Location = new Point(4, 25);
            Glass_tabPage.Name = "Glass_tabPage";
            Glass_tabPage.Padding = new Padding(3);
            Glass_tabPage.Size = new Size(969, 210);
            Glass_tabPage.TabIndex = 3;
            Glass_tabPage.Text = "유리 및 간봉";
            Glass_tabPage.UseVisualStyleBackColor = true;
            // 
            // SpacerName_textBox2
            // 
            SpacerName_textBox2.BackColor = Color.White;
            SpacerName_textBox2.BorderStyle = BorderStyle.None;
            SpacerName_textBox2.Enabled = false;
            SpacerName_textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            SpacerName_textBox2.ForeColor = Color.Black;
            SpacerName_textBox2.Location = new Point(480, 36);
            SpacerName_textBox2.Name = "SpacerName_textBox2";
            SpacerName_textBox2.Size = new Size(120, 15);
            SpacerName_textBox2.TabIndex = 100;
            // 
            // GlassName_textBox2
            // 
            GlassName_textBox2.BackColor = Color.White;
            GlassName_textBox2.BorderStyle = BorderStyle.None;
            GlassName_textBox2.Enabled = false;
            GlassName_textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            GlassName_textBox2.ForeColor = Color.Black;
            GlassName_textBox2.Location = new Point(36, 36);
            GlassName_textBox2.Name = "GlassName_textBox2";
            GlassName_textBox2.Size = new Size(120, 15);
            GlassName_textBox2.TabIndex = 99;
            // 
            // Frame_tabPage
            // 
            Frame_tabPage.BackColor = Color.White;
            Frame_tabPage.Controls.Add(label22);
            Frame_tabPage.Controls.Add(df_btw_textBox);
            Frame_tabPage.Controls.Add(df_fix_textBox);
            Frame_tabPage.Controls.Add(df_open_textBox);
            Frame_tabPage.Controls.Add(label19);
            Frame_tabPage.Controls.Add(label18);
            Frame_tabPage.Controls.Add(label17);
            Frame_tabPage.Controls.Add(Uf_btw_textBox);
            Frame_tabPage.Controls.Add(Uf_fix_textBox);
            Frame_tabPage.Controls.Add(Uf_open_textBox);
            Frame_tabPage.Controls.Add(label15);
            Frame_tabPage.Controls.Add(FrameMaterial_textBox);
            Frame_tabPage.Controls.Add(label14);
            Frame_tabPage.Controls.Add(WindowFrame_pictureBox);
            Frame_tabPage.Location = new Point(4, 25);
            Frame_tabPage.Name = "Frame_tabPage";
            Frame_tabPage.Padding = new Padding(3);
            Frame_tabPage.Size = new Size(969, 210);
            Frame_tabPage.TabIndex = 0;
            Frame_tabPage.Text = "프레임";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label22.ForeColor = SystemColors.ControlDark;
            label22.Location = new Point(294, 153);
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
            df_btw_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            df_btw_textBox.ForeColor = SystemColors.ControlDark;
            df_btw_textBox.Location = new Point(672, 149);
            df_btw_textBox.Name = "df_btw_textBox";
            df_btw_textBox.ReadOnly = true;
            df_btw_textBox.Size = new Size(116, 15);
            df_btw_textBox.TabIndex = 103;
            df_btw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // df_fix_textBox
            // 
            df_fix_textBox.BackColor = Color.White;
            df_fix_textBox.BorderStyle = BorderStyle.None;
            df_fix_textBox.Enabled = false;
            df_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            df_fix_textBox.ForeColor = SystemColors.ControlDark;
            df_fix_textBox.Location = new Point(533, 149);
            df_fix_textBox.Name = "df_fix_textBox";
            df_fix_textBox.ReadOnly = true;
            df_fix_textBox.Size = new Size(116, 15);
            df_fix_textBox.TabIndex = 102;
            df_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // df_open_textBox
            // 
            df_open_textBox.BackColor = Color.White;
            df_open_textBox.BorderStyle = BorderStyle.None;
            df_open_textBox.Enabled = false;
            df_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            df_open_textBox.ForeColor = SystemColors.ControlDark;
            df_open_textBox.Location = new Point(394, 149);
            df_open_textBox.Name = "df_open_textBox";
            df_open_textBox.ReadOnly = true;
            df_open_textBox.Size = new Size(116, 15);
            df_open_textBox.TabIndex = 101;
            df_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ForeColor = SystemColors.ControlDark;
            label19.Location = new Point(282, 115);
            label19.Name = "label19";
            label19.Size = new Size(51, 16);
            label19.TabIndex = 100;
            label19.Text = "열관류율";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label17.ForeColor = SystemColors.ControlDark;
            label17.Location = new Point(565, 80);
            label17.Name = "label17";
            label17.Size = new Size(49, 16);
            label17.TabIndex = 98;
            label17.Text = "프레임B";
            // 
            // Uf_btw_textBox
            // 
            Uf_btw_textBox.BackColor = Color.White;
            Uf_btw_textBox.BorderStyle = BorderStyle.None;
            Uf_btw_textBox.Enabled = false;
            Uf_btw_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uf_btw_textBox.ForeColor = SystemColors.ControlDark;
            Uf_btw_textBox.Location = new Point(672, 111);
            Uf_btw_textBox.Name = "Uf_btw_textBox";
            Uf_btw_textBox.ReadOnly = true;
            Uf_btw_textBox.Size = new Size(116, 15);
            Uf_btw_textBox.TabIndex = 97;
            Uf_btw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uf_fix_textBox
            // 
            Uf_fix_textBox.BackColor = Color.White;
            Uf_fix_textBox.BorderStyle = BorderStyle.None;
            Uf_fix_textBox.Enabled = false;
            Uf_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uf_fix_textBox.ForeColor = SystemColors.ControlDark;
            Uf_fix_textBox.Location = new Point(533, 111);
            Uf_fix_textBox.Name = "Uf_fix_textBox";
            Uf_fix_textBox.ReadOnly = true;
            Uf_fix_textBox.Size = new Size(116, 15);
            Uf_fix_textBox.TabIndex = 96;
            Uf_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uf_open_textBox
            // 
            Uf_open_textBox.BackColor = Color.White;
            Uf_open_textBox.BorderStyle = BorderStyle.None;
            Uf_open_textBox.Enabled = false;
            Uf_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uf_open_textBox.ForeColor = SystemColors.ControlDark;
            Uf_open_textBox.Location = new Point(394, 111);
            Uf_open_textBox.Name = "Uf_open_textBox";
            Uf_open_textBox.ReadOnly = true;
            Uf_open_textBox.Size = new Size(116, 15);
            Uf_open_textBox.TabIndex = 95;
            Uf_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ForeColor = SystemColors.ControlDark;
            label15.Location = new Point(426, 80);
            label15.Name = "label15";
            label15.Size = new Size(49, 16);
            label15.TabIndex = 94;
            label15.Text = "프레임A";
            // 
            // FrameMaterial_textBox
            // 
            FrameMaterial_textBox.BackColor = Color.White;
            FrameMaterial_textBox.BorderStyle = BorderStyle.None;
            FrameMaterial_textBox.Enabled = false;
            FrameMaterial_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FrameMaterial_textBox.ForeColor = SystemColors.ControlDark;
            FrameMaterial_textBox.Location = new Point(394, 42);
            FrameMaterial_textBox.Name = "FrameMaterial_textBox";
            FrameMaterial_textBox.ReadOnly = true;
            FrameMaterial_textBox.Size = new Size(116, 15);
            FrameMaterial_textBox.TabIndex = 93;
            FrameMaterial_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            // Install_tabPage
            // 
            Install_tabPage.BackColor = Color.White;
            Install_tabPage.Controls.Add(label43);
            Install_tabPage.Controls.Add(label44);
            Install_tabPage.Controls.Add(label45);
            Install_tabPage.Controls.Add(d_InstallButtom_textBox);
            Install_tabPage.Controls.Add(Psi_InstallButtom_textBox);
            Install_tabPage.Controls.Add(label37);
            Install_tabPage.Controls.Add(label42);
            Install_tabPage.Controls.Add(label41);
            Install_tabPage.Controls.Add(label40);
            Install_tabPage.Controls.Add(label24);
            Install_tabPage.Controls.Add(d_InstallSide_textBox);
            Install_tabPage.Controls.Add(Psi_InstallSide_textBox);
            Install_tabPage.Controls.Add(label33);
            Install_tabPage.Controls.Add(label34);
            Install_tabPage.Controls.Add(d_InstallTop_textBox);
            Install_tabPage.Controls.Add(Psi_InstallTop_textBox);
            Install_tabPage.Controls.Add(label38);
            Install_tabPage.Controls.Add(dUinst_textBox);
            Install_tabPage.Controls.Add(label39);
            Install_tabPage.Controls.Add(WindowInstall_pictureBox);
            Install_tabPage.Location = new Point(4, 25);
            Install_tabPage.Name = "Install_tabPage";
            Install_tabPage.Padding = new Padding(3);
            Install_tabPage.Size = new Size(969, 210);
            Install_tabPage.TabIndex = 1;
            Install_tabPage.Text = "설치열교";
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label43.ForeColor = SystemColors.ControlDark;
            label43.Location = new Point(696, 169);
            label43.Name = "label43";
            label43.Size = new Size(18, 16);
            label43.TabIndex = 126;
            label43.Text = "m";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label45.ForeColor = SystemColors.ControlDark;
            label45.Location = new Point(232, 169);
            label45.Name = "label45";
            label45.Size = new Size(29, 16);
            label45.TabIndex = 124;
            label45.Text = "하부";
            // 
            // d_InstallButtom_textBox
            // 
            d_InstallButtom_textBox.BackColor = Color.White;
            d_InstallButtom_textBox.BorderStyle = BorderStyle.None;
            d_InstallButtom_textBox.Enabled = false;
            d_InstallButtom_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            d_InstallButtom_textBox.ForeColor = SystemColors.ControlDark;
            d_InstallButtom_textBox.Location = new Point(542, 169);
            d_InstallButtom_textBox.Name = "d_InstallButtom_textBox";
            d_InstallButtom_textBox.ReadOnly = true;
            d_InstallButtom_textBox.Size = new Size(116, 15);
            d_InstallButtom_textBox.TabIndex = 123;
            d_InstallButtom_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_InstallButtom_textBox
            // 
            Psi_InstallButtom_textBox.BackColor = Color.White;
            Psi_InstallButtom_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallButtom_textBox.Enabled = false;
            Psi_InstallButtom_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_InstallButtom_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallButtom_textBox.Location = new Point(301, 169);
            Psi_InstallButtom_textBox.Name = "Psi_InstallButtom_textBox";
            Psi_InstallButtom_textBox.ReadOnly = true;
            Psi_InstallButtom_textBox.Size = new Size(116, 15);
            Psi_InstallButtom_textBox.TabIndex = 122;
            Psi_InstallButtom_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label37.ForeColor = SystemColors.ControlDark;
            label37.Location = new Point(696, 133);
            label37.Name = "label37";
            label37.Size = new Size(18, 16);
            label37.TabIndex = 121;
            label37.Text = "m";
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label42.ForeColor = SystemColors.ControlDark;
            label42.Location = new Point(696, 97);
            label42.Name = "label42";
            label42.Size = new Size(18, 16);
            label42.TabIndex = 120;
            label42.Text = "m";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label24.ForeColor = SystemColors.ControlDark;
            label24.Location = new Point(232, 133);
            label24.Name = "label24";
            label24.Size = new Size(29, 16);
            label24.TabIndex = 117;
            label24.Text = "측면";
            // 
            // d_InstallSide_textBox
            // 
            d_InstallSide_textBox.BackColor = Color.White;
            d_InstallSide_textBox.BorderStyle = BorderStyle.None;
            d_InstallSide_textBox.Enabled = false;
            d_InstallSide_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            d_InstallSide_textBox.ForeColor = SystemColors.ControlDark;
            d_InstallSide_textBox.Location = new Point(542, 133);
            d_InstallSide_textBox.Name = "d_InstallSide_textBox";
            d_InstallSide_textBox.ReadOnly = true;
            d_InstallSide_textBox.Size = new Size(116, 15);
            d_InstallSide_textBox.TabIndex = 116;
            d_InstallSide_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_InstallSide_textBox
            // 
            Psi_InstallSide_textBox.BackColor = Color.White;
            Psi_InstallSide_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallSide_textBox.Enabled = false;
            Psi_InstallSide_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_InstallSide_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallSide_textBox.Location = new Point(301, 133);
            Psi_InstallSide_textBox.Name = "Psi_InstallSide_textBox";
            Psi_InstallSide_textBox.ReadOnly = true;
            Psi_InstallSide_textBox.Size = new Size(116, 15);
            Psi_InstallSide_textBox.TabIndex = 114;
            Psi_InstallSide_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label33.ForeColor = SystemColors.ControlDark;
            label33.Location = new Point(232, 97);
            label33.Name = "label33";
            label33.Size = new Size(29, 16);
            label33.TabIndex = 113;
            label33.Text = "상부";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label34.ForeColor = SystemColors.ControlDark;
            label34.Location = new Point(573, 61);
            label34.Name = "label34";
            label34.Size = new Size(51, 16);
            label34.TabIndex = 112;
            label34.Text = "설치길이";
            // 
            // d_InstallTop_textBox
            // 
            d_InstallTop_textBox.BackColor = Color.White;
            d_InstallTop_textBox.BorderStyle = BorderStyle.None;
            d_InstallTop_textBox.Enabled = false;
            d_InstallTop_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            d_InstallTop_textBox.ForeColor = SystemColors.ControlDark;
            d_InstallTop_textBox.Location = new Point(542, 97);
            d_InstallTop_textBox.Name = "d_InstallTop_textBox";
            d_InstallTop_textBox.ReadOnly = true;
            d_InstallTop_textBox.Size = new Size(116, 15);
            d_InstallTop_textBox.TabIndex = 110;
            d_InstallTop_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Psi_InstallTop_textBox
            // 
            Psi_InstallTop_textBox.BackColor = Color.White;
            Psi_InstallTop_textBox.BorderStyle = BorderStyle.None;
            Psi_InstallTop_textBox.Enabled = false;
            Psi_InstallTop_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Psi_InstallTop_textBox.ForeColor = SystemColors.ControlDark;
            Psi_InstallTop_textBox.Location = new Point(301, 97);
            Psi_InstallTop_textBox.Name = "Psi_InstallTop_textBox";
            Psi_InstallTop_textBox.ReadOnly = true;
            Psi_InstallTop_textBox.Size = new Size(116, 15);
            Psi_InstallTop_textBox.TabIndex = 108;
            Psi_InstallTop_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label38.ForeColor = SystemColors.ControlDark;
            label38.Location = new Point(320, 61);
            label38.Name = "label38";
            label38.Size = new Size(73, 16);
            label38.TabIndex = 107;
            label38.Text = "선형열관류율";
            // 
            // dUinst_textBox
            // 
            dUinst_textBox.BackColor = Color.White;
            dUinst_textBox.BorderStyle = BorderStyle.None;
            dUinst_textBox.Enabled = false;
            dUinst_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dUinst_textBox.ForeColor = SystemColors.ControlDark;
            dUinst_textBox.Location = new Point(301, 25);
            dUinst_textBox.Name = "dUinst_textBox";
            dUinst_textBox.ReadOnly = true;
            dUinst_textBox.Size = new Size(116, 15);
            dUinst_textBox.TabIndex = 106;
            dUinst_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label39.ForeColor = SystemColors.ControlDark;
            label39.Location = new Point(199, 24);
            label39.Name = "label39";
            label39.Size = new Size(89, 16);
            label39.TabIndex = 105;
            label39.Text = "열교가산치[ΔU]";
            // 
            // WindowInstall_pictureBox
            // 
            WindowInstall_pictureBox.Location = new Point(74, 25);
            WindowInstall_pictureBox.Name = "WindowInstall_pictureBox";
            WindowInstall_pictureBox.Size = new Size(71, 180);
            WindowInstall_pictureBox.TabIndex = 0;
            WindowInstall_pictureBox.TabStop = false;
            // 
            // Size_tabPage
            // 
            Size_tabPage.BackColor = Color.White;
            Size_tabPage.Controls.Add(label56);
            Size_tabPage.Controls.Add(label57);
            Size_tabPage.Controls.Add(Lg_open_textBox);
            Size_tabPage.Controls.Add(label58);
            Size_tabPage.Controls.Add(label59);
            Size_tabPage.Controls.Add(Af_open_textBox);
            Size_tabPage.Controls.Add(label60);
            Size_tabPage.Controls.Add(label61);
            Size_tabPage.Controls.Add(Lg_fix_textBox);
            Size_tabPage.Controls.Add(label62);
            Size_tabPage.Controls.Add(label63);
            Size_tabPage.Controls.Add(label64);
            Size_tabPage.Controls.Add(Af_btw_textBox);
            Size_tabPage.Controls.Add(label65);
            Size_tabPage.Controls.Add(Af_fix_textBox);
            Size_tabPage.Controls.Add(label54);
            Size_tabPage.Controls.Add(label55);
            Size_tabPage.Controls.Add(Ag_open_textBox);
            Size_tabPage.Controls.Add(label52);
            Size_tabPage.Controls.Add(label53);
            Size_tabPage.Controls.Add(Area_textBox);
            Size_tabPage.Controls.Add(label46);
            Size_tabPage.Controls.Add(label47);
            Size_tabPage.Controls.Add(Ag_fix_textBox);
            Size_tabPage.Controls.Add(label48);
            Size_tabPage.Controls.Add(label49);
            Size_tabPage.Controls.Add(label50);
            Size_tabPage.Controls.Add(Height_textBox);
            Size_tabPage.Controls.Add(label51);
            Size_tabPage.Controls.Add(Width_textBox);
            Size_tabPage.Location = new Point(4, 25);
            Size_tabPage.Name = "Size_tabPage";
            Size_tabPage.Padding = new Padding(3);
            Size_tabPage.Size = new Size(969, 210);
            Size_tabPage.TabIndex = 2;
            Size_tabPage.Text = "사이즈";
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label56.ForeColor = SystemColors.ControlDark;
            label56.Location = new Point(836, 164);
            label56.Name = "label56";
            label56.Size = new Size(18, 16);
            label56.TabIndex = 155;
            label56.Text = "m";
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label57.ForeColor = SystemColors.ControlDark;
            label57.Location = new Point(549, 164);
            label57.Name = "label57";
            label57.Size = new Size(106, 16);
            label57.TabIndex = 154;
            label57.Text = "개폐창유리둘레길이";
            // 
            // Lg_open_textBox
            // 
            Lg_open_textBox.BackColor = Color.White;
            Lg_open_textBox.BorderStyle = BorderStyle.None;
            Lg_open_textBox.Enabled = false;
            Lg_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Lg_open_textBox.ForeColor = SystemColors.ControlDark;
            Lg_open_textBox.Location = new Point(692, 164);
            Lg_open_textBox.Name = "Lg_open_textBox";
            Lg_open_textBox.ReadOnly = true;
            Lg_open_textBox.Size = new Size(116, 15);
            Lg_open_textBox.TabIndex = 153;
            Lg_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label58.ForeColor = SystemColors.ControlDark;
            label58.Location = new Point(836, 24);
            label58.Name = "label58";
            label58.Size = new Size(22, 16);
            label58.TabIndex = 152;
            label58.Text = "m²";
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label59.ForeColor = SystemColors.ControlDark;
            label59.Location = new Point(551, 24);
            label59.Name = "label59";
            label59.Size = new Size(101, 16);
            label59.TabIndex = 151;
            label59.Text = "개폐프레임(A)면적";
            // 
            // Af_open_textBox
            // 
            Af_open_textBox.BackColor = Color.White;
            Af_open_textBox.BorderStyle = BorderStyle.None;
            Af_open_textBox.Enabled = false;
            Af_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Af_open_textBox.ForeColor = SystemColors.ControlDark;
            Af_open_textBox.Location = new Point(692, 24);
            Af_open_textBox.Name = "Af_open_textBox";
            Af_open_textBox.ReadOnly = true;
            Af_open_textBox.Size = new Size(116, 15);
            Af_open_textBox.TabIndex = 150;
            Af_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label60.ForeColor = SystemColors.ControlDark;
            label60.Location = new Point(836, 129);
            label60.Name = "label60";
            label60.Size = new Size(18, 16);
            label60.TabIndex = 149;
            label60.Text = "m";
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label61.ForeColor = SystemColors.ControlDark;
            label61.Location = new Point(549, 129);
            label61.Name = "label61";
            label61.Size = new Size(106, 16);
            label61.TabIndex = 148;
            label61.Text = "고정창유리둘레길이";
            // 
            // Lg_fix_textBox
            // 
            Lg_fix_textBox.BackColor = Color.White;
            Lg_fix_textBox.BorderStyle = BorderStyle.None;
            Lg_fix_textBox.Enabled = false;
            Lg_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Lg_fix_textBox.ForeColor = SystemColors.ControlDark;
            Lg_fix_textBox.Location = new Point(692, 129);
            Lg_fix_textBox.Name = "Lg_fix_textBox";
            Lg_fix_textBox.ReadOnly = true;
            Lg_fix_textBox.Size = new Size(116, 15);
            Lg_fix_textBox.TabIndex = 147;
            Lg_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label62.ForeColor = SystemColors.ControlDark;
            label62.Location = new Point(836, 94);
            label62.Name = "label62";
            label62.Size = new Size(22, 16);
            label62.TabIndex = 146;
            label62.Text = "m²";
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label63.ForeColor = SystemColors.ControlDark;
            label63.Location = new Point(836, 59);
            label63.Name = "label63";
            label63.Size = new Size(22, 16);
            label63.TabIndex = 145;
            label63.Text = "m²";
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label64.ForeColor = SystemColors.ControlDark;
            label64.Location = new Point(551, 94);
            label64.Name = "label64";
            label64.Size = new Size(101, 16);
            label64.TabIndex = 144;
            label64.Text = "중간프레임(C)면적";
            // 
            // Af_btw_textBox
            // 
            Af_btw_textBox.BackColor = Color.White;
            Af_btw_textBox.BorderStyle = BorderStyle.None;
            Af_btw_textBox.Enabled = false;
            Af_btw_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Af_btw_textBox.ForeColor = SystemColors.ControlDark;
            Af_btw_textBox.Location = new Point(692, 94);
            Af_btw_textBox.Name = "Af_btw_textBox";
            Af_btw_textBox.ReadOnly = true;
            Af_btw_textBox.Size = new Size(116, 15);
            Af_btw_textBox.TabIndex = 143;
            Af_btw_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label65.ForeColor = SystemColors.ControlDark;
            label65.Location = new Point(552, 59);
            label65.Name = "label65";
            label65.Size = new Size(101, 16);
            label65.TabIndex = 142;
            label65.Text = "고정프레임(B)면적";
            // 
            // Af_fix_textBox
            // 
            Af_fix_textBox.BackColor = Color.White;
            Af_fix_textBox.BorderStyle = BorderStyle.None;
            Af_fix_textBox.Enabled = false;
            Af_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Af_fix_textBox.ForeColor = SystemColors.ControlDark;
            Af_fix_textBox.Location = new Point(692, 59);
            Af_fix_textBox.Name = "Af_fix_textBox";
            Af_fix_textBox.ReadOnly = true;
            Af_fix_textBox.Size = new Size(116, 15);
            Af_fix_textBox.TabIndex = 141;
            Af_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label54.ForeColor = SystemColors.ControlDark;
            label54.Location = new Point(497, 164);
            label54.Name = "label54";
            label54.Size = new Size(22, 16);
            label54.TabIndex = 140;
            label54.Text = "m²";
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label55.ForeColor = SystemColors.ControlDark;
            label55.Location = new Point(230, 164);
            label55.Name = "label55";
            label55.Size = new Size(87, 16);
            label55.TabIndex = 139;
            label55.Text = "개폐창 유리면적";
            // 
            // Ag_open_textBox
            // 
            Ag_open_textBox.BackColor = Color.White;
            Ag_open_textBox.BorderStyle = BorderStyle.None;
            Ag_open_textBox.Enabled = false;
            Ag_open_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ag_open_textBox.ForeColor = SystemColors.ControlDark;
            Ag_open_textBox.Location = new Point(353, 164);
            Ag_open_textBox.Name = "Ag_open_textBox";
            Ag_open_textBox.ReadOnly = true;
            Ag_open_textBox.Size = new Size(116, 15);
            Ag_open_textBox.TabIndex = 138;
            Ag_open_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label52.ForeColor = SystemColors.ControlDark;
            label52.Location = new Point(497, 24);
            label52.Name = "label52";
            label52.Size = new Size(22, 16);
            label52.TabIndex = 137;
            label52.Text = "m²";
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label53.ForeColor = SystemColors.ControlDark;
            label53.Location = new Point(250, 24);
            label53.Name = "label53";
            label53.Size = new Size(51, 16);
            label53.TabIndex = 136;
            label53.Text = "창호면적";
            // 
            // Area_textBox
            // 
            Area_textBox.BackColor = Color.White;
            Area_textBox.BorderStyle = BorderStyle.None;
            Area_textBox.Enabled = false;
            Area_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Area_textBox.ForeColor = SystemColors.ControlDark;
            Area_textBox.Location = new Point(353, 24);
            Area_textBox.Name = "Area_textBox";
            Area_textBox.ReadOnly = true;
            Area_textBox.Size = new Size(116, 15);
            Area_textBox.TabIndex = 135;
            Area_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label46.ForeColor = SystemColors.ControlDark;
            label46.Location = new Point(497, 129);
            label46.Name = "label46";
            label46.Size = new Size(22, 16);
            label46.TabIndex = 134;
            label46.Text = "m²";
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label47.ForeColor = SystemColors.ControlDark;
            label47.Location = new Point(230, 129);
            label47.Name = "label47";
            label47.Size = new Size(87, 16);
            label47.TabIndex = 133;
            label47.Text = "고정창 유리면적";
            // 
            // Ag_fix_textBox
            // 
            Ag_fix_textBox.BackColor = Color.White;
            Ag_fix_textBox.BorderStyle = BorderStyle.None;
            Ag_fix_textBox.Enabled = false;
            Ag_fix_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ag_fix_textBox.ForeColor = SystemColors.ControlDark;
            Ag_fix_textBox.Location = new Point(353, 129);
            Ag_fix_textBox.Name = "Ag_fix_textBox";
            Ag_fix_textBox.ReadOnly = true;
            Ag_fix_textBox.Size = new Size(116, 15);
            Ag_fix_textBox.TabIndex = 132;
            Ag_fix_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label48.ForeColor = SystemColors.ControlDark;
            label48.Location = new Point(497, 94);
            label48.Name = "label48";
            label48.Size = new Size(18, 16);
            label48.TabIndex = 131;
            label48.Text = "m";
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label49.ForeColor = SystemColors.ControlDark;
            label49.Location = new Point(497, 59);
            label49.Name = "label49";
            label49.Size = new Size(18, 16);
            label49.TabIndex = 130;
            label49.Text = "m";
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label50.ForeColor = SystemColors.ControlDark;
            label50.Location = new Point(250, 94);
            label50.Name = "label50";
            label50.Size = new Size(51, 16);
            label50.TabIndex = 129;
            label50.Text = "창호높이";
            // 
            // Height_textBox
            // 
            Height_textBox.BackColor = Color.White;
            Height_textBox.BorderStyle = BorderStyle.None;
            Height_textBox.Enabled = false;
            Height_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Height_textBox.ForeColor = SystemColors.ControlDark;
            Height_textBox.Location = new Point(353, 94);
            Height_textBox.Name = "Height_textBox";
            Height_textBox.ReadOnly = true;
            Height_textBox.Size = new Size(116, 15);
            Height_textBox.TabIndex = 128;
            Height_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label51.ForeColor = SystemColors.ControlDark;
            label51.Location = new Point(250, 59);
            label51.Name = "label51";
            label51.Size = new Size(51, 16);
            label51.TabIndex = 127;
            label51.Text = "창호너비";
            // 
            // Width_textBox
            // 
            Width_textBox.BackColor = Color.White;
            Width_textBox.BorderStyle = BorderStyle.None;
            Width_textBox.Enabled = false;
            Width_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Width_textBox.ForeColor = SystemColors.ControlDark;
            Width_textBox.Location = new Point(353, 59);
            Width_textBox.Name = "Width_textBox";
            Width_textBox.ReadOnly = true;
            Width_textBox.Size = new Size(116, 15);
            Width_textBox.TabIndex = 126;
            Width_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // WindowType_pictureBox
            // 
            WindowType_pictureBox.Location = new Point(999, 84);
            WindowType_pictureBox.Name = "WindowType_pictureBox";
            WindowType_pictureBox.Size = new Size(151, 200);
            WindowType_pictureBox.TabIndex = 36;
            WindowType_pictureBox.TabStop = false;
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
            // τD65_SNA2_textBox
            // 
            τD65_SNA2_textBox.BackColor = SystemColors.InactiveBorder;
            τD65_SNA2_textBox.BorderStyle = BorderStyle.None;
            τD65_SNA2_textBox.Enabled = false;
            τD65_SNA2_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            τD65_SNA2_textBox.ForeColor = SystemColors.ControlDark;
            τD65_SNA2_textBox.Location = new Point(1070, 76);
            τD65_SNA2_textBox.Name = "τD65_SNA2_textBox";
            τD65_SNA2_textBox.ReadOnly = true;
            τD65_SNA2_textBox.Size = new Size(66, 15);
            τD65_SNA2_textBox.TabIndex = 154;
            τD65_SNA2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label27.ForeColor = SystemColors.ControlDark;
            label27.Location = new Point(1007, 76);
            label27.Name = "label27";
            label27.Size = new Size(55, 15);
            label27.TabIndex = 153;
            label27.Text = "빛투과율";
            // 
            // g2_textBox
            // 
            g2_textBox.BackColor = SystemColors.InactiveBorder;
            g2_textBox.BorderStyle = BorderStyle.None;
            g2_textBox.Enabled = false;
            g2_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            g2_textBox.ForeColor = SystemColors.ControlDark;
            g2_textBox.Location = new Point(1070, 55);
            g2_textBox.Name = "g2_textBox";
            g2_textBox.ReadOnly = true;
            g2_textBox.Size = new Size(66, 15);
            g2_textBox.TabIndex = 152;
            g2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label13.ForeColor = SystemColors.ControlDark;
            label13.Location = new Point(995, 55);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 151;
            label13.Text = "태양열취득률";
            // 
            // Uw_inst2_textBox
            // 
            Uw_inst2_textBox.BackColor = SystemColors.InactiveBorder;
            Uw_inst2_textBox.BorderStyle = BorderStyle.None;
            Uw_inst2_textBox.Enabled = false;
            Uw_inst2_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw_inst2_textBox.ForeColor = SystemColors.ControlDark;
            Uw_inst2_textBox.Location = new Point(1070, 283);
            Uw_inst2_textBox.Name = "Uw_inst2_textBox";
            Uw_inst2_textBox.ReadOnly = true;
            Uw_inst2_textBox.Size = new Size(66, 15);
            Uw_inst2_textBox.TabIndex = 149;
            Uw_inst2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Uw3_unit_label
            // 
            Uw3_unit_label.AutoSize = true;
            Uw3_unit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Uw3_unit_label.ForeColor = SystemColors.ControlDark;
            Uw3_unit_label.Location = new Point(1136, 282);
            Uw3_unit_label.Name = "Uw3_unit_label";
            Uw3_unit_label.Size = new Size(50, 16);
            Uw3_unit_label.TabIndex = 150;
            Uw3_unit_label.Text = "W/m²·K";
            // 
            // Uw3_label
            // 
            Uw3_label.AutoSize = true;
            Uw3_label.Font = new System.Drawing.Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Uw3_label.ForeColor = SystemColors.ControlDark;
            Uw3_label.Location = new Point(995, 283);
            Uw3_label.Name = "Uw3_label";
            Uw3_label.Size = new Size(79, 15);
            Uw3_label.TabIndex = 148;
            Uw3_label.Text = "유효열관류율";
            // 
            // SubWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(τD65_SNA2_textBox);
            Controls.Add(label27);
            Controls.Add(g2_textBox);
            Controls.Add(label13);
            Controls.Add(Uw_inst2_textBox);
            Controls.Add(Uw3_unit_label);
            Controls.Add(Uw3_label);
            Controls.Add(Previous_button);
            Controls.Add(WindowType_pictureBox);
            Controls.Add(tabControl1);
            Controls.Add(label4);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SubWindow";
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
            Size_tabPage.ResumeLayout(false);
            Size_tabPage.PerformLayout();
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
        private Label label4;
        private CustomComboBox DiIndi_comboBox;
        private CustomComboBox comboBox9;
        private CustomComboBox Install_comboBox;
        private Label label16;
        private Label Spacer_label;
        private Label label11;
        private Label Frame_label;
        private CustomComboBox Uw_comboBox;
        private Label label25;
        private Label label35;
        private TextBox Uw_inst_textBox;
        private Label label36;
        private Label Uw_unit_label;
        private TextBox Uw_textBox;
        private Label Uw_label;
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
        private TextBox FrameName_textBox;
        private CustomComboBox Frame_comboBox;
        private TextBox GlassName_textBox;
        private TextBox SpacerName_textBox;
        private TextBox Install_textBox;
        private CustomTabControl tabControl1;
        private TabPage Frame_tabPage;
        private TabPage Install_tabPage;
        private TabPage Size_tabPage;
        private Label label12;
        private Label label9;
        private TextBox Uf_btw_textBox;
        private TextBox Uf_fix_textBox;
        private TextBox Uf_open_textBox;
        private Label label15;
        private TextBox FrameMaterial_textBox;
        private Label label14;
        private PictureBox WindowFrame_pictureBox;
        private Label label22;
        private TextBox df_btw_textBox;
        private TextBox df_fix_textBox;
        private TextBox df_open_textBox;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label43;
        private Label label44;
        private Label label45;
        private TextBox d_InstallButtom_textBox;
        private TextBox Psi_InstallButtom_textBox;
        private Label label37;
        private Label label42;
        private Label label41;
        private Label label40;
        private Label label24;
        private TextBox d_InstallSide_textBox;
        private TextBox Psi_InstallSide_textBox;
        private Label label33;
        private Label label34;
        private TextBox d_InstallTop_textBox;
        private TextBox Psi_InstallTop_textBox;
        private Label label38;
        private TextBox dUinst_textBox;
        private Label label39;
        private PictureBox WindowInstall_pictureBox;
        private PictureBox WindowType_pictureBox;
        private Label label56;
        private Label label57;
        private TextBox Lg_open_textBox;
        private Label label58;
        private Label label59;
        private TextBox Af_open_textBox;
        private Label label60;
        private Label label61;
        private TextBox Lg_fix_textBox;
        private Label label62;
        private Label label63;
        private Label label64;
        private TextBox Af_btw_textBox;
        private Label label65;
        private TextBox Af_fix_textBox;
        private Label label54;
        private Label label55;
        private TextBox Ag_open_textBox;
        private Label label52;
        private Label label53;
        private TextBox Area_textBox;
        private Label label46;
        private Label label47;
        private TextBox Ag_fix_textBox;
        private Label label48;
        private Label label49;
        private Label label50;
        private TextBox Height_textBox;
        private Label label51;
        private TextBox Width_textBox;
        private TextBox WinNum_textBox;
        private PictureBox Icon_pictureBox;
        private TextBox AdditionalWindow_textBox;
        private Button Previous_button;
        private TextBox τD65_SNA2_textBox;
        private Label label27;
        private TextBox g2_textBox;
        private Label label13;
        private TextBox Uw_inst2_textBox;
        private Label Uw3_unit_label;
        private Label Uw3_label;
        private TabPage Glass_tabPage;
        private TextBox GlassName_textBox2;
        private TextBox SpacerName_textBox2;
        private Label label1;
        private Label label21;
        private TextBox g3_textBox;
        private Label label28;
    }
}
