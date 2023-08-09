namespace main.contents
{
    partial class HeatingSystem
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
            Zone_button = new Button();
            label1 = new Label();
            Type_textBox = new TextBox();
            label3 = new Label();
            Num_textBox = new TextBox();
            Icon_pictureBox = new PictureBox();
            Name_textBox = new TextBox();
            tabControl1 = new TabControl();
            Qhg_tabPage = new TabPage();
            label9 = new Label();
            Complex_comboBox = new ComboBox();
            Sub2UserList_textBox = new TextBox();
            Sub2UserList_button = new Button();
            Sub2UserList_Label = new Label();
            Sub1UserList_textBox = new TextBox();
            Sub1UserList_button = new Button();
            Sub1UserList_Label = new Label();
            MainUserList_textBox = new TextBox();
            MainUserList_button = new Button();
            MainUserList_Label = new Label();
            SLRL_comboBox = new ComboBox();
            label8 = new Label();
            SystemLoacation_comboBox = new ComboBox();
            Sub2System_label = new Label();
            Sub2System_comboBox = new ComboBox();
            label6 = new Label();
            Sub1System_label = new Label();
            Sub1System_comboBox = new ComboBox();
            MainSystem_label = new Label();
            MainSystem_comboBox = new ComboBox();
            Qhs_tabPage = new TabPage();
            Qhd_tabPage = new TabPage();
            Pump2Num_label1 = new Label();
            Pump2Num_label2 = new Label();
            Pump2Num_textBox = new TextBox();
            Pump1Num_label1 = new Label();
            Pump1Num_label2 = new Label();
            Pump1Num_textBox = new TextBox();
            Pump2Control_label = new Label();
            Pump2Control_comboBox = new ComboBox();
            Pump2_textBox = new TextBox();
            Pump2_button = new Button();
            Pump2_label = new Label();
            Pump2Valve_label = new Label();
            Pump2Valve_comboBox = new ComboBox();
            PumpMethod_label = new Label();
            PumpMethod_comboBox = new ComboBox();
            Pump1Control_label = new Label();
            Pump1Control_comboBox = new ComboBox();
            label2 = new Label();
            PumpUse_comboBox = new ComboBox();
            Pump1_textBox = new TextBox();
            Pump1_button = new Button();
            Pump1_label = new Label();
            Pump1Valve_label = new Label();
            Pump1Valve_comboBox = new ComboBox();
            Qhce_tabPage = new TabPage();
            panel2 = new Panel();
            tabControl2 = new TabControl();
            Boiler_tabPage = new TabPage();
            Boiler_dataGridView = new DataGridView();
            HP_tabPage = new TabPage();
            AS_tabPage = new TabPage();
            DH_tabPage = new TabPage();
            Solar_tabPage = new TabPage();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            tabControl1.SuspendLayout();
            Qhg_tabPage.SuspendLayout();
            Qhd_tabPage.SuspendLayout();
            tabControl2.SuspendLayout();
            Boiler_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Zone_button);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Type_textBox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Zone_button
            // 
            Zone_button.BackColor = SystemColors.ControlLight;
            Zone_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Zone_button.FlatStyle = FlatStyle.System;
            Zone_button.Font = new Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            Zone_button.Location = new Point(288, 59);
            Zone_button.Margin = new Padding(0);
            Zone_button.Name = "Zone_button";
            Zone_button.Size = new Size(23, 23);
            Zone_button.TabIndex = 119;
            Zone_button.Text = "+";
            Zone_button.UseVisualStyleBackColor = false;
            Zone_button.Click += Zone_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(112, 25);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 118;
            label1.Text = "명칭";
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = Color.White;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(165, 63);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 117;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(112, 63);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 116;
            label3.Text = "공급 존";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(68, 25);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(67, 15);
            Num_textBox.TabIndex = 114;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(18, 20);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 115;
            Icon_pictureBox.TabStop = false;
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(165, 22);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 113;
            Name_textBox.TextChanged += Name_textBox_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Qhg_tabPage);
            tabControl1.Controls.Add(Qhs_tabPage);
            tabControl1.Controls.Add(Qhd_tabPage);
            tabControl1.Controls.Add(Qhce_tabPage);
            tabControl1.Location = new Point(12, 119);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(977, 155);
            tabControl1.TabIndex = 97;
            // 
            // Qhg_tabPage
            // 
            Qhg_tabPage.BackColor = Color.White;
            Qhg_tabPage.Controls.Add(label9);
            Qhg_tabPage.Controls.Add(Complex_comboBox);
            Qhg_tabPage.Controls.Add(Sub2UserList_textBox);
            Qhg_tabPage.Controls.Add(Sub2UserList_button);
            Qhg_tabPage.Controls.Add(Sub2UserList_Label);
            Qhg_tabPage.Controls.Add(Sub1UserList_textBox);
            Qhg_tabPage.Controls.Add(Sub1UserList_button);
            Qhg_tabPage.Controls.Add(Sub1UserList_Label);
            Qhg_tabPage.Controls.Add(MainUserList_textBox);
            Qhg_tabPage.Controls.Add(MainUserList_button);
            Qhg_tabPage.Controls.Add(MainUserList_Label);
            Qhg_tabPage.Controls.Add(SLRL_comboBox);
            Qhg_tabPage.Controls.Add(label8);
            Qhg_tabPage.Controls.Add(SystemLoacation_comboBox);
            Qhg_tabPage.Controls.Add(Sub2System_label);
            Qhg_tabPage.Controls.Add(Sub2System_comboBox);
            Qhg_tabPage.Controls.Add(label6);
            Qhg_tabPage.Controls.Add(Sub1System_label);
            Qhg_tabPage.Controls.Add(Sub1System_comboBox);
            Qhg_tabPage.Controls.Add(MainSystem_label);
            Qhg_tabPage.Controls.Add(MainSystem_comboBox);
            Qhg_tabPage.Location = new Point(4, 24);
            Qhg_tabPage.Name = "Qhg_tabPage";
            Qhg_tabPage.Padding = new Padding(3);
            Qhg_tabPage.Size = new Size(969, 127);
            Qhg_tabPage.TabIndex = 0;
            Qhg_tabPage.Text = "생산";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(19, 24);
            label9.Name = "label9";
            label9.Size = new Size(79, 15);
            label9.TabIndex = 154;
            label9.Text = "복합설비유무";
            // 
            // Complex_comboBox
            // 
            Complex_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Complex_comboBox.FormattingEnabled = true;
            Complex_comboBox.Location = new Point(108, 19);
            Complex_comboBox.Name = "Complex_comboBox";
            Complex_comboBox.Size = new Size(120, 24);
            Complex_comboBox.TabIndex = 153;
            Complex_comboBox.SelectedIndexChanged += Complex_comboBox_SelectedIndexChanged;
            // 
            // Sub2UserList_textBox
            // 
            Sub2UserList_textBox.BackColor = Color.White;
            Sub2UserList_textBox.BorderStyle = BorderStyle.None;
            Sub2UserList_textBox.Enabled = false;
            Sub2UserList_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Sub2UserList_textBox.ForeColor = SystemColors.ControlDark;
            Sub2UserList_textBox.Location = new Point(794, 86);
            Sub2UserList_textBox.Name = "Sub2UserList_textBox";
            Sub2UserList_textBox.Size = new Size(120, 15);
            Sub2UserList_textBox.TabIndex = 152;
            Sub2UserList_textBox.TextAlign = HorizontalAlignment.Center;
            Sub2UserList_textBox.Visible = false;
            // 
            // Sub2UserList_button
            // 
            Sub2UserList_button.BackColor = SystemColors.ControlLight;
            Sub2UserList_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Sub2UserList_button.FlatStyle = FlatStyle.System;
            Sub2UserList_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Sub2UserList_button.Location = new Point(917, 82);
            Sub2UserList_button.Margin = new Padding(0);
            Sub2UserList_button.Name = "Sub2UserList_button";
            Sub2UserList_button.Size = new Size(23, 23);
            Sub2UserList_button.TabIndex = 151;
            Sub2UserList_button.Text = "+";
            Sub2UserList_button.UseVisualStyleBackColor = false;
            Sub2UserList_button.Visible = false;
            Sub2UserList_button.Click += Sub2UserList_button_Click;
            // 
            // Sub2UserList_Label
            // 
            Sub2UserList_Label.AutoSize = true;
            Sub2UserList_Label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Sub2UserList_Label.Location = new Point(703, 86);
            Sub2UserList_Label.Name = "Sub2UserList_Label";
            Sub2UserList_Label.Size = new Size(71, 15);
            Sub2UserList_Label.TabIndex = 150;
            Sub2UserList_Label.Text = "Sub2일람표";
            Sub2UserList_Label.Visible = false;
            // 
            // Sub1UserList_textBox
            // 
            Sub1UserList_textBox.BackColor = Color.White;
            Sub1UserList_textBox.BorderStyle = BorderStyle.None;
            Sub1UserList_textBox.Enabled = false;
            Sub1UserList_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Sub1UserList_textBox.ForeColor = SystemColors.ControlDark;
            Sub1UserList_textBox.Location = new Point(347, 86);
            Sub1UserList_textBox.Name = "Sub1UserList_textBox";
            Sub1UserList_textBox.Size = new Size(120, 15);
            Sub1UserList_textBox.TabIndex = 149;
            Sub1UserList_textBox.TextAlign = HorizontalAlignment.Center;
            Sub1UserList_textBox.Visible = false;
            // 
            // Sub1UserList_button
            // 
            Sub1UserList_button.BackColor = SystemColors.ControlLight;
            Sub1UserList_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Sub1UserList_button.FlatStyle = FlatStyle.System;
            Sub1UserList_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Sub1UserList_button.Location = new Point(469, 82);
            Sub1UserList_button.Margin = new Padding(0);
            Sub1UserList_button.Name = "Sub1UserList_button";
            Sub1UserList_button.Size = new Size(23, 23);
            Sub1UserList_button.TabIndex = 148;
            Sub1UserList_button.Text = "+";
            Sub1UserList_button.UseVisualStyleBackColor = false;
            Sub1UserList_button.Visible = false;
            Sub1UserList_button.Click += Sub1UserList_button_Click;
            // 
            // Sub1UserList_Label
            // 
            Sub1UserList_Label.AutoSize = true;
            Sub1UserList_Label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Sub1UserList_Label.Location = new Point(256, 86);
            Sub1UserList_Label.Name = "Sub1UserList_Label";
            Sub1UserList_Label.Size = new Size(72, 15);
            Sub1UserList_Label.TabIndex = 147;
            Sub1UserList_Label.Text = "SUb1일람표";
            Sub1UserList_Label.Visible = false;
            // 
            // MainUserList_textBox
            // 
            MainUserList_textBox.BackColor = Color.White;
            MainUserList_textBox.BorderStyle = BorderStyle.None;
            MainUserList_textBox.Enabled = false;
            MainUserList_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MainUserList_textBox.ForeColor = SystemColors.ControlDark;
            MainUserList_textBox.Location = new Point(347, 56);
            MainUserList_textBox.Name = "MainUserList_textBox";
            MainUserList_textBox.Size = new Size(120, 15);
            MainUserList_textBox.TabIndex = 146;
            MainUserList_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // MainUserList_button
            // 
            MainUserList_button.BackColor = SystemColors.ControlLight;
            MainUserList_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            MainUserList_button.FlatStyle = FlatStyle.System;
            MainUserList_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            MainUserList_button.Location = new Point(469, 52);
            MainUserList_button.Margin = new Padding(0);
            MainUserList_button.Name = "MainUserList_button";
            MainUserList_button.Size = new Size(23, 23);
            MainUserList_button.TabIndex = 145;
            MainUserList_button.Text = "+";
            MainUserList_button.UseVisualStyleBackColor = false;
            MainUserList_button.Click += MainUserList_button_Click;
            // 
            // MainUserList_Label
            // 
            MainUserList_Label.AutoSize = true;
            MainUserList_Label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MainUserList_Label.Location = new Point(256, 56);
            MainUserList_Label.Name = "MainUserList_Label";
            MainUserList_Label.Size = new Size(70, 15);
            MainUserList_Label.TabIndex = 144;
            MainUserList_Label.Text = "Main일람표";
            // 
            // SLRL_comboBox
            // 
            SLRL_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SLRL_comboBox.FormattingEnabled = true;
            SLRL_comboBox.Location = new Point(576, 19);
            SLRL_comboBox.Name = "SLRL_comboBox";
            SLRL_comboBox.Size = new Size(120, 24);
            SLRL_comboBox.TabIndex = 141;
            SLRL_comboBox.SelectedIndexChanged += SLRL_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(491, 24);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 138;
            label8.Text = "공급/환수온도";
            // 
            // SystemLoacation_comboBox
            // 
            SystemLoacation_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SystemLoacation_comboBox.FormattingEnabled = true;
            SystemLoacation_comboBox.Location = new Point(347, 19);
            SystemLoacation_comboBox.Name = "SystemLoacation_comboBox";
            SystemLoacation_comboBox.Size = new Size(120, 24);
            SystemLoacation_comboBox.TabIndex = 137;
            SystemLoacation_comboBox.SelectedIndexChanged += SystemLoacation_comboBox_SelectedIndexChanged;
            // 
            // Sub2System_label
            // 
            Sub2System_label.AutoSize = true;
            Sub2System_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Sub2System_label.Location = new Point(497, 86);
            Sub2System_label.Name = "Sub2System_label";
            Sub2System_label.Size = new Size(59, 15);
            Sub2System_label.TabIndex = 136;
            Sub2System_label.Text = "Sub설비2";
            Sub2System_label.Visible = false;
            // 
            // Sub2System_comboBox
            // 
            Sub2System_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Sub2System_comboBox.FormattingEnabled = true;
            Sub2System_comboBox.Location = new Point(576, 81);
            Sub2System_comboBox.Name = "Sub2System_comboBox";
            Sub2System_comboBox.Size = new Size(120, 24);
            Sub2System_comboBox.TabIndex = 135;
            Sub2System_comboBox.Visible = false;
            Sub2System_comboBox.SelectedIndexChanged += Sub2System_comboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(256, 24);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 134;
            label6.Text = "설치위치";
            // 
            // Sub1System_label
            // 
            Sub1System_label.AutoSize = true;
            Sub1System_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Sub1System_label.Location = new Point(19, 86);
            Sub1System_label.Name = "Sub1System_label";
            Sub1System_label.Size = new Size(59, 15);
            Sub1System_label.TabIndex = 132;
            Sub1System_label.Text = "Sub설비1";
            Sub1System_label.Visible = false;
            // 
            // Sub1System_comboBox
            // 
            Sub1System_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Sub1System_comboBox.FormattingEnabled = true;
            Sub1System_comboBox.Location = new Point(108, 81);
            Sub1System_comboBox.Name = "Sub1System_comboBox";
            Sub1System_comboBox.Size = new Size(120, 24);
            Sub1System_comboBox.TabIndex = 131;
            Sub1System_comboBox.Visible = false;
            Sub1System_comboBox.SelectedIndexChanged += SubSystem1_comboBox_SelectedIndexChanged;
            // 
            // MainSystem_label
            // 
            MainSystem_label.AutoSize = true;
            MainSystem_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MainSystem_label.Location = new Point(19, 56);
            MainSystem_label.Name = "MainSystem_label";
            MainSystem_label.Size = new Size(58, 15);
            MainSystem_label.TabIndex = 128;
            MainSystem_label.Text = "Main설비";
            // 
            // MainSystem_comboBox
            // 
            MainSystem_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MainSystem_comboBox.FormattingEnabled = true;
            MainSystem_comboBox.Location = new Point(108, 51);
            MainSystem_comboBox.Name = "MainSystem_comboBox";
            MainSystem_comboBox.Size = new Size(120, 24);
            MainSystem_comboBox.TabIndex = 127;
            MainSystem_comboBox.SelectedIndexChanged += MainSystem_comboBox_SelectedIndexChanged;
            // 
            // Qhs_tabPage
            // 
            Qhs_tabPage.BackColor = Color.White;
            Qhs_tabPage.Location = new Point(4, 24);
            Qhs_tabPage.Name = "Qhs_tabPage";
            Qhs_tabPage.Padding = new Padding(3);
            Qhs_tabPage.Size = new Size(969, 127);
            Qhs_tabPage.TabIndex = 2;
            Qhs_tabPage.Text = "저장";
            // 
            // Qhd_tabPage
            // 
            Qhd_tabPage.Controls.Add(Pump2Num_label1);
            Qhd_tabPage.Controls.Add(Pump2Num_label2);
            Qhd_tabPage.Controls.Add(Pump2Num_textBox);
            Qhd_tabPage.Controls.Add(Pump1Num_label1);
            Qhd_tabPage.Controls.Add(Pump1Num_label2);
            Qhd_tabPage.Controls.Add(Pump1Num_textBox);
            Qhd_tabPage.Controls.Add(Pump2Control_label);
            Qhd_tabPage.Controls.Add(Pump2Control_comboBox);
            Qhd_tabPage.Controls.Add(Pump2_textBox);
            Qhd_tabPage.Controls.Add(Pump2_button);
            Qhd_tabPage.Controls.Add(Pump2_label);
            Qhd_tabPage.Controls.Add(Pump2Valve_label);
            Qhd_tabPage.Controls.Add(Pump2Valve_comboBox);
            Qhd_tabPage.Controls.Add(PumpMethod_label);
            Qhd_tabPage.Controls.Add(PumpMethod_comboBox);
            Qhd_tabPage.Controls.Add(Pump1Control_label);
            Qhd_tabPage.Controls.Add(Pump1Control_comboBox);
            Qhd_tabPage.Controls.Add(label2);
            Qhd_tabPage.Controls.Add(PumpUse_comboBox);
            Qhd_tabPage.Controls.Add(Pump1_textBox);
            Qhd_tabPage.Controls.Add(Pump1_button);
            Qhd_tabPage.Controls.Add(Pump1_label);
            Qhd_tabPage.Controls.Add(Pump1Valve_label);
            Qhd_tabPage.Controls.Add(Pump1Valve_comboBox);
            Qhd_tabPage.Location = new Point(4, 24);
            Qhd_tabPage.Name = "Qhd_tabPage";
            Qhd_tabPage.Padding = new Padding(3);
            Qhd_tabPage.Size = new Size(969, 127);
            Qhd_tabPage.TabIndex = 3;
            Qhd_tabPage.Text = "분배";
            Qhd_tabPage.UseVisualStyleBackColor = true;
            // 
            // Pump2Num_label1
            // 
            Pump2Num_label1.AutoSize = true;
            Pump2Num_label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Num_label1.Location = new Point(705, 89);
            Pump2Num_label1.Name = "Pump2Num_label1";
            Pump2Num_label1.Size = new Size(61, 16);
            Pump2Num_label1.TabIndex = 192;
            Pump2Num_label1.Text = "펌프2 대수";
            // 
            // Pump2Num_label2
            // 
            Pump2Num_label2.AutoSize = true;
            Pump2Num_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Num_label2.ForeColor = SystemColors.ControlDark;
            Pump2Num_label2.Location = new Point(891, 89);
            Pump2Num_label2.Name = "Pump2Num_label2";
            Pump2Num_label2.Size = new Size(25, 16);
            Pump2Num_label2.TabIndex = 191;
            Pump2Num_label2.Text = "EA";
            // 
            // Pump2Num_textBox
            // 
            Pump2Num_textBox.BorderStyle = BorderStyle.FixedSingle;
            Pump2Num_textBox.Location = new Point(768, 86);
            Pump2Num_textBox.Name = "Pump2Num_textBox";
            Pump2Num_textBox.Size = new Size(120, 23);
            Pump2Num_textBox.TabIndex = 190;
            Pump2Num_textBox.TextAlign = HorizontalAlignment.Center;
            Pump2Num_textBox.TextChanged += Pump2Num_textBox_TextChanged;
            // 
            // Pump1Num_label1
            // 
            Pump1Num_label1.AutoSize = true;
            Pump1Num_label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Num_label1.Location = new Point(705, 56);
            Pump1Num_label1.Name = "Pump1Num_label1";
            Pump1Num_label1.Size = new Size(61, 16);
            Pump1Num_label1.TabIndex = 189;
            Pump1Num_label1.Text = "펌프1 대수";
            // 
            // Pump1Num_label2
            // 
            Pump1Num_label2.AutoSize = true;
            Pump1Num_label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Num_label2.ForeColor = SystemColors.ControlDark;
            Pump1Num_label2.Location = new Point(891, 56);
            Pump1Num_label2.Name = "Pump1Num_label2";
            Pump1Num_label2.Size = new Size(25, 16);
            Pump1Num_label2.TabIndex = 188;
            Pump1Num_label2.Text = "EA";
            // 
            // Pump1Num_textBox
            // 
            Pump1Num_textBox.BorderStyle = BorderStyle.FixedSingle;
            Pump1Num_textBox.Location = new Point(768, 53);
            Pump1Num_textBox.Name = "Pump1Num_textBox";
            Pump1Num_textBox.Size = new Size(120, 23);
            Pump1Num_textBox.TabIndex = 187;
            Pump1Num_textBox.TextAlign = HorizontalAlignment.Center;
            Pump1Num_textBox.TextChanged += Pump1Num_textBox_TextChanged;
            // 
            // Pump2Control_label
            // 
            Pump2Control_label.AutoSize = true;
            Pump2Control_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Control_label.Location = new Point(507, 90);
            Pump2Control_label.Name = "Pump2Control_label";
            Pump2Control_label.Size = new Size(66, 15);
            Pump2Control_label.TabIndex = 186;
            Pump2Control_label.Text = "펌프2 제어";
            Pump2Control_label.Visible = false;
            // 
            // Pump2Control_comboBox
            // 
            Pump2Control_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Control_comboBox.FormattingEnabled = true;
            Pump2Control_comboBox.Location = new Point(575, 85);
            Pump2Control_comboBox.Name = "Pump2Control_comboBox";
            Pump2Control_comboBox.Size = new Size(120, 24);
            Pump2Control_comboBox.TabIndex = 185;
            Pump2Control_comboBox.Visible = false;
            Pump2Control_comboBox.SelectedIndexChanged += Pump2Control_comboBox_SelectedIndexChanged;
            // 
            // Pump2_textBox
            // 
            Pump2_textBox.BackColor = Color.White;
            Pump2_textBox.BorderStyle = BorderStyle.None;
            Pump2_textBox.Enabled = false;
            Pump2_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2_textBox.ForeColor = SystemColors.ControlDark;
            Pump2_textBox.Location = new Point(94, 90);
            Pump2_textBox.Name = "Pump2_textBox";
            Pump2_textBox.Size = new Size(120, 15);
            Pump2_textBox.TabIndex = 184;
            Pump2_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Pump2_button
            // 
            Pump2_button.BackColor = SystemColors.ControlLight;
            Pump2_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump2_button.FlatStyle = FlatStyle.System;
            Pump2_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Pump2_button.Location = new Point(220, 86);
            Pump2_button.Margin = new Padding(0);
            Pump2_button.Name = "Pump2_button";
            Pump2_button.Size = new Size(23, 23);
            Pump2_button.TabIndex = 183;
            Pump2_button.Text = "+";
            Pump2_button.UseVisualStyleBackColor = false;
            Pump2_button.Click += Pump2_button_Click;
            // 
            // Pump2_label
            // 
            Pump2_label.AutoSize = true;
            Pump2_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2_label.Location = new Point(33, 90);
            Pump2_label.Name = "Pump2_label";
            Pump2_label.Size = new Size(38, 15);
            Pump2_label.TabIndex = 182;
            Pump2_label.Text = "펌프2";
            // 
            // Pump2Valve_label
            // 
            Pump2Valve_label.AutoSize = true;
            Pump2Valve_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Valve_label.Location = new Point(249, 90);
            Pump2Valve_label.Name = "Pump2Valve_label";
            Pump2Valve_label.Size = new Size(102, 15);
            Pump2Valve_label.TabIndex = 181;
            Pump2Valve_label.Text = "펌프2 정유량밸브";
            // 
            // Pump2Valve_comboBox
            // 
            Pump2Valve_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump2Valve_comboBox.FormattingEnabled = true;
            Pump2Valve_comboBox.Location = new Point(351, 85);
            Pump2Valve_comboBox.Name = "Pump2Valve_comboBox";
            Pump2Valve_comboBox.Size = new Size(120, 24);
            Pump2Valve_comboBox.TabIndex = 180;
            Pump2Valve_comboBox.SelectedIndexChanged += Pump2Valve_comboBox_SelectedIndexChanged;
            // 
            // PumpMethod_label
            // 
            PumpMethod_label.AutoSize = true;
            PumpMethod_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PumpMethod_label.Location = new Point(249, 25);
            PumpMethod_label.Name = "PumpMethod_label";
            PumpMethod_label.Size = new Size(59, 15);
            PumpMethod_label.TabIndex = 179;
            PumpMethod_label.Text = "펌프 방식";
            // 
            // PumpMethod_comboBox
            // 
            PumpMethod_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PumpMethod_comboBox.FormattingEnabled = true;
            PumpMethod_comboBox.Location = new Point(351, 20);
            PumpMethod_comboBox.Name = "PumpMethod_comboBox";
            PumpMethod_comboBox.Size = new Size(120, 24);
            PumpMethod_comboBox.TabIndex = 178;
            PumpMethod_comboBox.SelectedIndexChanged += PumpMethod_comboBox_SelectedIndexChanged;
            // 
            // Pump1Control_label
            // 
            Pump1Control_label.AutoSize = true;
            Pump1Control_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Control_label.Location = new Point(507, 57);
            Pump1Control_label.Name = "Pump1Control_label";
            Pump1Control_label.Size = new Size(66, 15);
            Pump1Control_label.TabIndex = 177;
            Pump1Control_label.Text = "펌프1 제어";
            Pump1Control_label.Visible = false;
            // 
            // Pump1Control_comboBox
            // 
            Pump1Control_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Control_comboBox.FormattingEnabled = true;
            Pump1Control_comboBox.Location = new Point(575, 52);
            Pump1Control_comboBox.Name = "Pump1Control_comboBox";
            Pump1Control_comboBox.Size = new Size(120, 24);
            Pump1Control_comboBox.TabIndex = 176;
            Pump1Control_comboBox.Visible = false;
            Pump1Control_comboBox.SelectedIndexChanged += Pump1Control_comboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(33, 25);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 175;
            label2.Text = "펌프 유무";
            // 
            // PumpUse_comboBox
            // 
            PumpUse_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PumpUse_comboBox.FormattingEnabled = true;
            PumpUse_comboBox.Location = new Point(94, 20);
            PumpUse_comboBox.Name = "PumpUse_comboBox";
            PumpUse_comboBox.Size = new Size(120, 24);
            PumpUse_comboBox.TabIndex = 174;
            PumpUse_comboBox.SelectedIndexChanged += PumpUse_comboBox_SelectedIndexChanged;
            // 
            // Pump1_textBox
            // 
            Pump1_textBox.BackColor = Color.White;
            Pump1_textBox.BorderStyle = BorderStyle.None;
            Pump1_textBox.Enabled = false;
            Pump1_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1_textBox.ForeColor = SystemColors.ControlDark;
            Pump1_textBox.Location = new Point(94, 57);
            Pump1_textBox.Name = "Pump1_textBox";
            Pump1_textBox.Size = new Size(120, 15);
            Pump1_textBox.TabIndex = 167;
            Pump1_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Pump1_button
            // 
            Pump1_button.BackColor = SystemColors.ControlLight;
            Pump1_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            Pump1_button.FlatStyle = FlatStyle.System;
            Pump1_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Pump1_button.Location = new Point(220, 53);
            Pump1_button.Margin = new Padding(0);
            Pump1_button.Name = "Pump1_button";
            Pump1_button.Size = new Size(23, 23);
            Pump1_button.TabIndex = 166;
            Pump1_button.Text = "+";
            Pump1_button.UseVisualStyleBackColor = false;
            Pump1_button.Click += Pump1_button_Click;
            // 
            // Pump1_label
            // 
            Pump1_label.AutoSize = true;
            Pump1_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1_label.Location = new Point(33, 57);
            Pump1_label.Name = "Pump1_label";
            Pump1_label.Size = new Size(38, 15);
            Pump1_label.TabIndex = 165;
            Pump1_label.Text = "펌프1";
            // 
            // Pump1Valve_label
            // 
            Pump1Valve_label.AutoSize = true;
            Pump1Valve_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Valve_label.Location = new Point(249, 57);
            Pump1Valve_label.Name = "Pump1Valve_label";
            Pump1Valve_label.Size = new Size(102, 15);
            Pump1Valve_label.TabIndex = 156;
            Pump1Valve_label.Text = "펌프1 정유량밸브";
            // 
            // Pump1Valve_comboBox
            // 
            Pump1Valve_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Pump1Valve_comboBox.FormattingEnabled = true;
            Pump1Valve_comboBox.Location = new Point(351, 52);
            Pump1Valve_comboBox.Name = "Pump1Valve_comboBox";
            Pump1Valve_comboBox.Size = new Size(120, 24);
            Pump1Valve_comboBox.TabIndex = 155;
            Pump1Valve_comboBox.SelectedIndexChanged += Pump1Valve_comboBox_SelectedIndexChanged;
            // 
            // Qhce_tabPage
            // 
            Qhce_tabPage.Location = new Point(4, 24);
            Qhce_tabPage.Name = "Qhce_tabPage";
            Qhce_tabPage.Padding = new Padding(3);
            Qhce_tabPage.Size = new Size(969, 127);
            Qhce_tabPage.TabIndex = 4;
            Qhce_tabPage.Text = "공급";
            Qhce_tabPage.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Location = new Point(12, 439);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 221);
            panel2.TabIndex = 143;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(Boiler_tabPage);
            tabControl2.Controls.Add(HP_tabPage);
            tabControl2.Controls.Add(AS_tabPage);
            tabControl2.Controls.Add(DH_tabPage);
            tabControl2.Controls.Add(Solar_tabPage);
            tabControl2.Location = new Point(12, 280);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(977, 153);
            tabControl2.TabIndex = 144;
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.Controls.Add(Boiler_dataGridView);
            Boiler_tabPage.Location = new Point(4, 24);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(969, 125);
            Boiler_tabPage.TabIndex = 6;
            Boiler_tabPage.Text = "보일러";
            Boiler_tabPage.UseVisualStyleBackColor = true;
            // 
            // Boiler_dataGridView
            // 
            Boiler_dataGridView.AllowUserToAddRows = false;
            Boiler_dataGridView.AllowUserToDeleteRows = false;
            Boiler_dataGridView.AllowUserToResizeColumns = false;
            Boiler_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Boiler_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Boiler_dataGridView.BackgroundColor = Color.White;
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
            Boiler_dataGridView.Location = new Point(19, 6);
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
            Boiler_dataGridView.Size = new Size(921, 113);
            Boiler_dataGridView.TabIndex = 22;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Location = new Point(4, 24);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(969, 125);
            HP_tabPage.TabIndex = 2;
            HP_tabPage.Text = "히트펌프";
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 24);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(969, 125);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 24);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(969, 125);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 24);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(969, 125);
            Solar_tabPage.TabIndex = 5;
            Solar_tabPage.Text = "태양열시스템";
            Solar_tabPage.UseVisualStyleBackColor = true;
            // 
            // HeatingSystem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(tabControl2);
            Controls.Add(panel2);
            Controls.Add(tabControl1);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "HeatingSystem";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            tabControl1.ResumeLayout(false);
            Qhg_tabPage.ResumeLayout(false);
            Qhg_tabPage.PerformLayout();
            Qhd_tabPage.ResumeLayout(false);
            Qhd_tabPage.PerformLayout();
            tabControl2.ResumeLayout(false);
            Boiler_tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Boiler_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Label label1;
        private TextBox Type_textBox;
        private Label label3;
        private TextBox Num_textBox;
        private PictureBox Icon_pictureBox;
        private TextBox Name_textBox;
        private Button Zone_button;
        private TabControl tabControl1;
        private TabPage Qhg_tabPage;
        private TabPage Qhs_tabPage;
        private TabPage Qhd_tabPage;
        private TabPage Qhce_tabPage;
        private ComboBox SLRL_comboBox;
        private Label label8;
        private ComboBox SystemLoacation_comboBox;
        private Label Sub2System_label;
        private ComboBox Sub2System_comboBox;
        private Label label6;
        private Label Sub1System_label;
        private ComboBox Sub1System_comboBox;
        private Label MainSystem_label;
        private ComboBox MainSystem_comboBox;
        private Panel panel2;
        private TabControl tabControl2;
        private TabPage HP_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage Solar_tabPage;
        private TextBox Sub2UserList_textBox;
        private Button Sub2UserList_button;
        private Label Sub2UserList_Label;
        private TextBox Sub1UserList_textBox;
        private Button Sub1UserList_button;
        private Label Sub1UserList_Label;
        private TextBox MainUserList_textBox;
        private Button MainUserList_button;
        private Label MainUserList_Label;
        private TabPage Boiler_tabPage;
        private Label label9;
        private ComboBox Complex_comboBox;
        private DataGridView Boiler_dataGridView;
        private Label Pump2Control_label;
        private ComboBox Pump2Control_comboBox;
        private TextBox Pump2_textBox;
        private Button Pump2_button;
        private Label Pump2_label;
        private Label Pump2Valve_label;
        private ComboBox Pump2Valve_comboBox;
        private Label PumpMethod_label;
        private ComboBox PumpMethod_comboBox;
        private Label Pump1Control_label;
        private ComboBox Pump1Control_comboBox;
        private Label label2;
        private ComboBox PumpUse_comboBox;
        private TextBox Pump1_textBox;
        private Button Pump1_button;
        private Label Pump1_label;
        private Label Pump1Valve_label;
        private ComboBox Pump1Valve_comboBox;
        private Label Pump2Num_label1;
        private Label Pump2Num_label2;
        private TextBox Pump2Num_textBox;
        private Label Pump1Num_label1;
        private Label Pump1Num_label2;
        private TextBox Pump1Num_textBox;
    }
}