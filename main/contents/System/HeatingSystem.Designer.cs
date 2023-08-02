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
            SLRL_comboBox = new ComboBox();
            label7 = new Label();
            InPump_comboBox = new ComboBox();
            label8 = new Label();
            SystemLoacation_comboBox = new ComboBox();
            label5 = new Label();
            SubSystem2_comboBox = new ComboBox();
            label6 = new Label();
            Combi_comboBox = new ComboBox();
            label2 = new Label();
            SubSystem1_comboBox = new ComboBox();
            label4 = new Label();
            HeatingDHW_comboBox = new ComboBox();
            UMethod_label = new Label();
            MainSystem_comboBox = new ComboBox();
            label25 = new Label();
            Qhs_tabPage = new TabPage();
            Qhd_tabPage = new TabPage();
            Qhce_tabPage = new TabPage();
            tabControl2 = new TabControl();
            HP_tabPage = new TabPage();
            Boiler_tabPage = new TabPage();
            PersonNum_textBox = new TextBox();
            label11 = new Label();
            label10 = new Label();
            textBox1 = new TextBox();
            label9 = new Label();
            TBName_textBox = new TextBox();
            TB_button = new Button();
            AS_tabPage = new TabPage();
            DH_tabPage = new TabPage();
            Solar_tabPage = new TabPage();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            tabControl1.SuspendLayout();
            Qhg_tabPage.SuspendLayout();
            tabControl2.SuspendLayout();
            Boiler_tabPage.SuspendLayout();
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
            tabControl1.Size = new Size(977, 137);
            tabControl1.TabIndex = 97;
            // 
            // Qhg_tabPage
            // 
            Qhg_tabPage.BackColor = Color.White;
            Qhg_tabPage.Controls.Add(SLRL_comboBox);
            Qhg_tabPage.Controls.Add(label7);
            Qhg_tabPage.Controls.Add(InPump_comboBox);
            Qhg_tabPage.Controls.Add(label8);
            Qhg_tabPage.Controls.Add(SystemLoacation_comboBox);
            Qhg_tabPage.Controls.Add(label5);
            Qhg_tabPage.Controls.Add(SubSystem2_comboBox);
            Qhg_tabPage.Controls.Add(label6);
            Qhg_tabPage.Controls.Add(Combi_comboBox);
            Qhg_tabPage.Controls.Add(label2);
            Qhg_tabPage.Controls.Add(SubSystem1_comboBox);
            Qhg_tabPage.Controls.Add(label4);
            Qhg_tabPage.Controls.Add(HeatingDHW_comboBox);
            Qhg_tabPage.Controls.Add(UMethod_label);
            Qhg_tabPage.Controls.Add(MainSystem_comboBox);
            Qhg_tabPage.Controls.Add(label25);
            Qhg_tabPage.Location = new Point(4, 24);
            Qhg_tabPage.Name = "Qhg_tabPage";
            Qhg_tabPage.Padding = new Padding(3);
            Qhg_tabPage.Size = new Size(969, 109);
            Qhg_tabPage.TabIndex = 0;
            Qhg_tabPage.Text = "생산";
            // 
            // SLRL_comboBox
            // 
            SLRL_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SLRL_comboBox.FormattingEnabled = true;
            SLRL_comboBox.Location = new Point(825, 30);
            SLRL_comboBox.Name = "SLRL_comboBox";
            SLRL_comboBox.Size = new Size(120, 24);
            SLRL_comboBox.TabIndex = 141;
            SLRL_comboBox.SelectedIndexChanged += SLRL_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(701, 64);
            label7.Name = "label7";
            label7.Size = new Size(123, 15);
            label7.TabIndex = 140;
            label7.Text = "생산설비 내 펌프유무";
            // 
            // InPump_comboBox
            // 
            InPump_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            InPump_comboBox.FormattingEnabled = true;
            InPump_comboBox.Location = new Point(825, 60);
            InPump_comboBox.Name = "InPump_comboBox";
            InPump_comboBox.Size = new Size(120, 24);
            InPump_comboBox.TabIndex = 139;
            InPump_comboBox.SelectedIndexChanged += InPump_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(701, 34);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 138;
            label8.Text = "공급/환수온도";
            // 
            // SystemLoacation_comboBox
            // 
            SystemLoacation_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SystemLoacation_comboBox.FormattingEnabled = true;
            SystemLoacation_comboBox.Location = new Point(576, 30);
            SystemLoacation_comboBox.Name = "SystemLoacation_comboBox";
            SystemLoacation_comboBox.Size = new Size(120, 24);
            SystemLoacation_comboBox.TabIndex = 137;
            SystemLoacation_comboBox.SelectedIndexChanged += SystemLoacation_comboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(497, 64);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 136;
            label5.Text = "보조설비2";
            // 
            // SubSystem2_comboBox
            // 
            SubSystem2_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SubSystem2_comboBox.FormattingEnabled = true;
            SubSystem2_comboBox.Location = new Point(576, 60);
            SubSystem2_comboBox.Name = "SubSystem2_comboBox";
            SubSystem2_comboBox.Size = new Size(120, 24);
            SubSystem2_comboBox.TabIndex = 135;
            SubSystem2_comboBox.SelectedIndexChanged += SubSystem2_comboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(497, 34);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 134;
            label6.Text = "설치위치";
            // 
            // Combi_comboBox
            // 
            Combi_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Combi_comboBox.FormattingEnabled = true;
            Combi_comboBox.Location = new Point(347, 30);
            Combi_comboBox.Name = "Combi_comboBox";
            Combi_comboBox.Size = new Size(120, 24);
            Combi_comboBox.TabIndex = 133;
            Combi_comboBox.SelectedIndexChanged += Combi_comboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(258, 64);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 132;
            label2.Text = "보조설비1";
            // 
            // SubSystem1_comboBox
            // 
            SubSystem1_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SubSystem1_comboBox.FormattingEnabled = true;
            SubSystem1_comboBox.Location = new Point(347, 60);
            SubSystem1_comboBox.Name = "SubSystem1_comboBox";
            SubSystem1_comboBox.Size = new Size(120, 24);
            SubSystem1_comboBox.TabIndex = 131;
            SubSystem1_comboBox.SelectedIndexChanged += SubSystem1_comboBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(258, 34);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 130;
            label4.Text = "콤비설비";
            // 
            // HeatingDHW_comboBox
            // 
            HeatingDHW_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            HeatingDHW_comboBox.FormattingEnabled = true;
            HeatingDHW_comboBox.Location = new Point(108, 30);
            HeatingDHW_comboBox.Name = "HeatingDHW_comboBox";
            HeatingDHW_comboBox.Size = new Size(120, 24);
            HeatingDHW_comboBox.TabIndex = 129;
            HeatingDHW_comboBox.SelectedIndexChanged += HeatingDHW_comboBox_SelectedIndexChanged;
            // 
            // UMethod_label
            // 
            UMethod_label.AutoSize = true;
            UMethod_label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UMethod_label.Location = new Point(19, 64);
            UMethod_label.Name = "UMethod_label";
            UMethod_label.Size = new Size(55, 15);
            UMethod_label.TabIndex = 128;
            UMethod_label.Text = "주요설비";
            // 
            // MainSystem_comboBox
            // 
            MainSystem_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            MainSystem_comboBox.FormattingEnabled = true;
            MainSystem_comboBox.Location = new Point(108, 60);
            MainSystem_comboBox.Name = "MainSystem_comboBox";
            MainSystem_comboBox.Size = new Size(120, 24);
            MainSystem_comboBox.TabIndex = 127;
            MainSystem_comboBox.SelectedIndexChanged += MainSystem_comboBox_SelectedIndexChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(19, 34);
            label25.Name = "label25";
            label25.Size = new Size(63, 15);
            label25.TabIndex = 126;
            label25.Text = "난방+급탕";
            // 
            // Qhs_tabPage
            // 
            Qhs_tabPage.BackColor = Color.White;
            Qhs_tabPage.Location = new Point(4, 24);
            Qhs_tabPage.Name = "Qhs_tabPage";
            Qhs_tabPage.Padding = new Padding(3);
            Qhs_tabPage.Size = new Size(969, 109);
            Qhs_tabPage.TabIndex = 2;
            Qhs_tabPage.Text = "축열";
            // 
            // Qhd_tabPage
            // 
            Qhd_tabPage.Location = new Point(4, 24);
            Qhd_tabPage.Name = "Qhd_tabPage";
            Qhd_tabPage.Padding = new Padding(3);
            Qhd_tabPage.Size = new Size(969, 109);
            Qhd_tabPage.TabIndex = 3;
            Qhd_tabPage.Text = "배관";
            Qhd_tabPage.UseVisualStyleBackColor = true;
            // 
            // Qhce_tabPage
            // 
            Qhce_tabPage.Location = new Point(4, 24);
            Qhce_tabPage.Name = "Qhce_tabPage";
            Qhce_tabPage.Padding = new Padding(3);
            Qhce_tabPage.Size = new Size(969, 109);
            Qhce_tabPage.TabIndex = 4;
            Qhce_tabPage.Text = "공급";
            Qhce_tabPage.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(HP_tabPage);
            tabControl2.Controls.Add(Boiler_tabPage);
            tabControl2.Controls.Add(AS_tabPage);
            tabControl2.Controls.Add(DH_tabPage);
            tabControl2.Controls.Add(Solar_tabPage);
            tabControl2.Location = new Point(12, 260);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(977, 368);
            tabControl2.TabIndex = 98;
            // 
            // HP_tabPage
            // 
            HP_tabPage.BackColor = Color.White;
            HP_tabPage.Location = new Point(4, 24);
            HP_tabPage.Name = "HP_tabPage";
            HP_tabPage.Padding = new Padding(3);
            HP_tabPage.Size = new Size(969, 340);
            HP_tabPage.TabIndex = 0;
            HP_tabPage.Text = "히트펌프";
            // 
            // Boiler_tabPage
            // 
            Boiler_tabPage.BackColor = Color.White;
            Boiler_tabPage.Controls.Add(PersonNum_textBox);
            Boiler_tabPage.Controls.Add(label11);
            Boiler_tabPage.Controls.Add(label10);
            Boiler_tabPage.Controls.Add(textBox1);
            Boiler_tabPage.Controls.Add(label9);
            Boiler_tabPage.Controls.Add(TBName_textBox);
            Boiler_tabPage.Controls.Add(TB_button);
            Boiler_tabPage.Location = new Point(4, 24);
            Boiler_tabPage.Name = "Boiler_tabPage";
            Boiler_tabPage.Padding = new Padding(3);
            Boiler_tabPage.Size = new Size(969, 340);
            Boiler_tabPage.TabIndex = 2;
            Boiler_tabPage.Text = "보일러";
            // 
            // PersonNum_textBox
            // 
            PersonNum_textBox.BackColor = SystemColors.Window;
            PersonNum_textBox.BorderStyle = BorderStyle.FixedSingle;
            PersonNum_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PersonNum_textBox.Location = new Point(108, 109);
            PersonNum_textBox.Name = "PersonNum_textBox";
            PersonNum_textBox.Size = new Size(120, 22);
            PersonNum_textBox.TabIndex = 143;
            PersonNum_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(19, 113);
            label11.Name = "label11";
            label11.Size = new Size(71, 15);
            label11.TabIndex = 142;
            label11.Text = "보일러 용량";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(19, 69);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 140;
            label10.Text = "연료";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Enabled = false;
            textBox1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.ControlDark;
            textBox1.Location = new Point(108, 69);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 15);
            textBox1.TabIndex = 139;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(19, 28);
            label9.Name = "label9";
            label9.Size = new Size(71, 15);
            label9.TabIndex = 138;
            label9.Text = "보일러 종류";
            // 
            // TBName_textBox
            // 
            TBName_textBox.BackColor = Color.White;
            TBName_textBox.BorderStyle = BorderStyle.None;
            TBName_textBox.Enabled = false;
            TBName_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TBName_textBox.ForeColor = SystemColors.ControlDark;
            TBName_textBox.Location = new Point(108, 28);
            TBName_textBox.Name = "TBName_textBox";
            TBName_textBox.Size = new Size(120, 15);
            TBName_textBox.TabIndex = 137;
            TBName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // TB_button
            // 
            TB_button.BackColor = SystemColors.ControlLight;
            TB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            TB_button.FlatStyle = FlatStyle.System;
            TB_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            TB_button.Location = new Point(230, 25);
            TB_button.Margin = new Padding(0);
            TB_button.Name = "TB_button";
            TB_button.Size = new Size(23, 23);
            TB_button.TabIndex = 136;
            TB_button.Text = "+";
            TB_button.UseVisualStyleBackColor = false;
            // 
            // AS_tabPage
            // 
            AS_tabPage.Location = new Point(4, 24);
            AS_tabPage.Name = "AS_tabPage";
            AS_tabPage.Padding = new Padding(3);
            AS_tabPage.Size = new Size(969, 340);
            AS_tabPage.TabIndex = 3;
            AS_tabPage.Text = "흡수식온수기";
            AS_tabPage.UseVisualStyleBackColor = true;
            // 
            // DH_tabPage
            // 
            DH_tabPage.Location = new Point(4, 24);
            DH_tabPage.Name = "DH_tabPage";
            DH_tabPage.Padding = new Padding(3);
            DH_tabPage.Size = new Size(969, 340);
            DH_tabPage.TabIndex = 4;
            DH_tabPage.Text = "지역난방";
            DH_tabPage.UseVisualStyleBackColor = true;
            // 
            // Solar_tabPage
            // 
            Solar_tabPage.Location = new Point(4, 24);
            Solar_tabPage.Name = "Solar_tabPage";
            Solar_tabPage.Padding = new Padding(3);
            Solar_tabPage.Size = new Size(969, 340);
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
            tabControl2.ResumeLayout(false);
            Boiler_tabPage.ResumeLayout(false);
            Boiler_tabPage.PerformLayout();
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
        private TabControl tabControl2;
        private TabPage HP_tabPage;
        private TabPage Boiler_tabPage;
        private TabPage AS_tabPage;
        private TabPage DH_tabPage;
        private TabPage Solar_tabPage;
        private ComboBox SLRL_comboBox;
        private Label label7;
        private ComboBox InPump_comboBox;
        private Label label8;
        private ComboBox SystemLoacation_comboBox;
        private Label label5;
        private ComboBox SubSystem2_comboBox;
        private Label label6;
        private ComboBox Combi_comboBox;
        private Label label2;
        private ComboBox SubSystem1_comboBox;
        private Label label4;
        private ComboBox HeatingDHW_comboBox;
        private Label UMethod_label;
        private ComboBox MainSystem_comboBox;
        private Label label25;
        private Label label11;
        private Label label10;
        private TextBox textBox1;
        private Label label9;
        private TextBox TBName_textBox;
        private Button TB_button;
        private TextBox PersonNum_textBox;
    }
}