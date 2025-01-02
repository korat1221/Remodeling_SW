
namespace main.subcontents.Alt
{
    partial class AltWin
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
            Save_button = new Button();
            Alt_dataGridView = new DataGridView();
            WinRemodelingType_comboBox = new CustomComboBox();
            label11 = new Label();
            label1 = new Label();
            GeneralPanel = new Panel();
            dU_textBox = new TextBox();
            WindowType_pictureBox = new PictureBox();
            label4 = new Label();
            WindowFrame_pictureBox = new PictureBox();
            Ueff_label = new Label();
            Ueff_textBox = new TextBox();
            label13 = new Label();
            g_textBox = new TextBox();
            label27 = new Label();
            tao_textBox = new TextBox();
            label2 = new Label();
            frame_label = new Label();
            label6 = new Label();
            glass_textBox = new TextBox();
            Spacer_textBox = new TextBox();
            frame_textBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).BeginInit();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(918, 530);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 20;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // Alt_dataGridView
            // 
            Alt_dataGridView.AllowUserToAddRows = false;
            Alt_dataGridView.AllowUserToDeleteRows = false;
            Alt_dataGridView.AllowUserToResizeColumns = false;
            Alt_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Alt_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Alt_dataGridView.BackgroundColor = Color.White;
            Alt_dataGridView.BorderStyle = BorderStyle.None;
            Alt_dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Alt_dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Alt_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Alt_dataGridView.Location = new Point(1, 330);
            Alt_dataGridView.Name = "Alt_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Alt_dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Alt_dataGridView.RowHeadersVisible = false;
            Alt_dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("나눔바른고딕", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Alt_dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle3;
            Alt_dataGridView.Size = new Size(1066, 194);
            Alt_dataGridView.TabIndex = 101;
            Alt_dataGridView.CellContentClick += Alt_dataGridView_CellContentClick;
            // 
            // WinRemodelingType_comboBox
            // 
            WinRemodelingType_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WinRemodelingType_comboBox.Font = new Font("나눔바른고딕", 9.75F);
            WinRemodelingType_comboBox.FormattingEnabled = true;
            WinRemodelingType_comboBox.Location = new Point(113, 11);
            WinRemodelingType_comboBox.Name = "WinRemodelingType_comboBox";
            WinRemodelingType_comboBox.Size = new Size(120, 23);
            WinRemodelingType_comboBox.TabIndex = 153;
            WinRemodelingType_comboBox.SelectedIndexChanged += WinRemodelingType_comboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("나눔바른고딕", 9.75F);
            label11.Location = new Point(12, 15);
            label11.Name = "label11";
            label11.Size = new Size(82, 15);
            label11.TabIndex = 152;
            label11.Text = "리모델링 방식";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F);
            label1.Location = new Point(8, 527);
            label1.Name = "label1";
            label1.Size = new Size(236, 15);
            label1.TabIndex = 164;
            label1.Text = "* 리모델링안의 평균 점수는 100점 입니다.";
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(dU_textBox);
            GeneralPanel.Controls.Add(WindowType_pictureBox);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Controls.Add(WindowFrame_pictureBox);
            GeneralPanel.Controls.Add(Ueff_label);
            GeneralPanel.Controls.Add(Ueff_textBox);
            GeneralPanel.Controls.Add(label13);
            GeneralPanel.Controls.Add(g_textBox);
            GeneralPanel.Controls.Add(label27);
            GeneralPanel.Controls.Add(tao_textBox);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(frame_label);
            GeneralPanel.Controls.Add(label6);
            GeneralPanel.Controls.Add(glass_textBox);
            GeneralPanel.Controls.Add(Spacer_textBox);
            GeneralPanel.Controls.Add(frame_textBox);
            GeneralPanel.Location = new Point(0, 40);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1066, 289);
            GeneralPanel.TabIndex = 193;
            GeneralPanel.Visible = false;
            // 
            // dU_textBox
            // 
            dU_textBox.BackColor = Color.White;
            dU_textBox.BorderStyle = BorderStyle.None;
            dU_textBox.Enabled = false;
            dU_textBox.Font = new Font("나눔바른고딕", 9.75F);
            dU_textBox.ForeColor = SystemColors.ControlDark;
            dU_textBox.Location = new Point(342, 268);
            dU_textBox.Name = "dU_textBox";
            dU_textBox.ReadOnly = true;
            dU_textBox.Size = new Size(66, 15);
            dU_textBox.TabIndex = 192;
            dU_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // WindowType_pictureBox
            // 
            WindowType_pictureBox.Location = new Point(276, 46);
            WindowType_pictureBox.Name = "WindowType_pictureBox";
            WindowType_pictureBox.Size = new Size(151, 200);
            WindowType_pictureBox.TabIndex = 166;
            WindowType_pictureBox.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F);
            label4.ForeColor = SystemColors.ControlDark;
            label4.Location = new Point(242, 268);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 191;
            label4.Text = "설치열교가산치";
            // 
            // WindowFrame_pictureBox
            // 
            WindowFrame_pictureBox.Location = new Point(483, 58);
            WindowFrame_pictureBox.Name = "WindowFrame_pictureBox";
            WindowFrame_pictureBox.Size = new Size(200, 179);
            WindowFrame_pictureBox.TabIndex = 165;
            WindowFrame_pictureBox.TabStop = false;
            // 
            // Ueff_label
            // 
            Ueff_label.AutoSize = true;
            Ueff_label.Font = new Font("나눔바른고딕", 9.75F);
            Ueff_label.ForeColor = SystemColors.ControlDark;
            Ueff_label.Location = new Point(254, 247);
            Ueff_label.Name = "Ueff_label";
            Ueff_label.Size = new Size(79, 15);
            Ueff_label.TabIndex = 167;
            Ueff_label.Text = "유효열관류율";
            // 
            // Ueff_textBox
            // 
            Ueff_textBox.BackColor = Color.White;
            Ueff_textBox.BorderStyle = BorderStyle.None;
            Ueff_textBox.Enabled = false;
            Ueff_textBox.Font = new Font("나눔바른고딕", 9.75F);
            Ueff_textBox.ForeColor = SystemColors.ControlDark;
            Ueff_textBox.Location = new Point(342, 247);
            Ueff_textBox.Name = "Ueff_textBox";
            Ueff_textBox.ReadOnly = true;
            Ueff_textBox.Size = new Size(66, 15);
            Ueff_textBox.TabIndex = 168;
            Ueff_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("나눔바른고딕", 9.75F);
            label13.ForeColor = SystemColors.ControlDark;
            label13.Location = new Point(267, 12);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 169;
            label13.Text = "태양열취득률";
            // 
            // g_textBox
            // 
            g_textBox.BackColor = Color.White;
            g_textBox.BorderStyle = BorderStyle.None;
            g_textBox.Enabled = false;
            g_textBox.Font = new Font("나눔바른고딕", 9.75F);
            g_textBox.ForeColor = SystemColors.ControlDark;
            g_textBox.Location = new Point(342, 12);
            g_textBox.Name = "g_textBox";
            g_textBox.ReadOnly = true;
            g_textBox.Size = new Size(66, 15);
            g_textBox.TabIndex = 170;
            g_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("나눔바른고딕", 9.75F);
            label27.ForeColor = SystemColors.ControlDark;
            label27.Location = new Point(279, 33);
            label27.Name = "label27";
            label27.Size = new Size(55, 15);
            label27.TabIndex = 171;
            label27.Text = "빛투과율";
            // 
            // tao_textBox
            // 
            tao_textBox.BackColor = Color.White;
            tao_textBox.BorderStyle = BorderStyle.None;
            tao_textBox.Enabled = false;
            tao_textBox.Font = new Font("나눔바른고딕", 9.75F);
            tao_textBox.ForeColor = SystemColors.ControlDark;
            tao_textBox.Location = new Point(342, 33);
            tao_textBox.Name = "tao_textBox";
            tao_textBox.ReadOnly = true;
            tao_textBox.Size = new Size(66, 15);
            tao_textBox.TabIndex = 172;
            tao_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(712, 140);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 173;
            label2.Text = "유리";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frame_label
            // 
            frame_label.AutoSize = true;
            frame_label.ForeColor = SystemColors.ControlDark;
            frame_label.Location = new Point(706, 101);
            frame_label.Name = "frame_label";
            frame_label.Size = new Size(43, 15);
            frame_label.TabIndex = 174;
            frame_label.Text = "프레임";
            frame_label.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlDark;
            label6.Location = new Point(712, 179);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 175;
            label6.Text = "간봉";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // glass_textBox
            // 
            glass_textBox.BackColor = Color.White;
            glass_textBox.BorderStyle = BorderStyle.None;
            glass_textBox.Location = new Point(782, 139);
            glass_textBox.Name = "glass_textBox";
            glass_textBox.ReadOnly = true;
            glass_textBox.Size = new Size(100, 16);
            glass_textBox.TabIndex = 176;
            glass_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Spacer_textBox
            // 
            Spacer_textBox.BackColor = Color.White;
            Spacer_textBox.BorderStyle = BorderStyle.None;
            Spacer_textBox.Location = new Point(782, 178);
            Spacer_textBox.Name = "Spacer_textBox";
            Spacer_textBox.ReadOnly = true;
            Spacer_textBox.Size = new Size(100, 16);
            Spacer_textBox.TabIndex = 178;
            Spacer_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // frame_textBox
            // 
            frame_textBox.BackColor = Color.White;
            frame_textBox.BorderStyle = BorderStyle.None;
            frame_textBox.Location = new Point(782, 100);
            frame_textBox.Name = "frame_textBox";
            frame_textBox.ReadOnly = true;
            frame_textBox.Size = new Size(100, 16);
            frame_textBox.TabIndex = 177;
            frame_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // AltWin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1065, 568);
            Controls.Add(label1);
            Controls.Add(Save_button);
            Controls.Add(label11);
            Controls.Add(Alt_dataGridView);
            Controls.Add(WinRemodelingType_comboBox);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AltWin";
            Text = "Review of Alternatives";
            ((System.ComponentModel.ISupportInitialize)Alt_dataGridView).EndInit();
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WindowType_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)WindowFrame_pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Save_button;
        private DataGridView Alt_dataGridView;
        private Label label11;
        private Label label1;
        private CustomComboBox WinRemodelingType_comboBox;
        private Panel GeneralPanel;
        private TextBox dU_textBox;
        private PictureBox WindowType_pictureBox;
        private Label label4;
        private PictureBox WindowFrame_pictureBox;
        private Label Ueff_label;
        private TextBox Ueff_textBox;
        private Label label13;
        private TextBox g_textBox;
        private Label label27;
        private TextBox tao_textBox;
        private Label label2;
        private Label frame_label;
        private Label label6;
        private TextBox glass_textBox;
        private TextBox Spacer_textBox;
        private TextBox frame_textBox;
    }
}