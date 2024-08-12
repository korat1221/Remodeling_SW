using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Drawing;

namespace main.contents
{
    partial class ZoneEnvelope
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            checkBoxColumn = new DataGridViewCheckBoxColumn();
            GeneralPanel = new Panel();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Num_textBox = new System.Windows.Forms.TextBox();
            Icon_pictureBox = new PictureBox();
            ZoneName_textBox = new System.Windows.Forms.TextBox();
            Layer_textBox = new System.Windows.Forms.TextBox();
            panel2 = new Panel();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            AdditionalPanel = new Panel();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            Cwirk_textBox = new System.Windows.Forms.TextBox();
            SlabCwirk_comboBox = new CustomComboBox();
            label8 = new System.Windows.Forms.Label();
            InWallCwirk_comboBox = new CustomComboBox();
            label7 = new System.Windows.Forms.Label();
            CeilingCwirk_comboBox = new CustomComboBox();
            label6 = new System.Windows.Forms.Label();
            WallCwirk_comboBox = new CustomComboBox();
            label5 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            panel1 = new Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ExternalZone_radioButton = new System.Windows.Forms.RadioButton();
            DoorZone_radioButton = new System.Windows.Forms.RadioButton();
            InternalZone_radioButton = new System.Windows.Forms.RadioButton();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            Save_button = new System.Windows.Forms.Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            AdditionalPanel.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // checkBoxColumn
            // 
            checkBoxColumn.Name = "checkBoxColumn";
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(ZoneName_textBox);
            GeneralPanel.Controls.Add(Layer_textBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(124, 52);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 104;
            label3.Text = "명칭";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(130, 16);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 103;
            label1.Text = "층";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(30, 77);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(101, 15);
            Num_textBox.TabIndex = 92;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(30, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 93;
            Icon_pictureBox.TabStop = false;
            // 
            // ZoneName_textBox
            // 
            ZoneName_textBox.BackColor = SystemColors.Window;
            ZoneName_textBox.BorderStyle = BorderStyle.None;
            ZoneName_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ZoneName_textBox.ForeColor = Color.Black;
            ZoneName_textBox.Location = new Point(155, 52);
            ZoneName_textBox.Name = "ZoneName_textBox";
            ZoneName_textBox.Size = new Size(120, 15);
            ZoneName_textBox.TabIndex = 89;
            ZoneName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Layer_textBox
            // 
            Layer_textBox.BackColor = SystemColors.Window;
            Layer_textBox.BorderStyle = BorderStyle.None;
            Layer_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Layer_textBox.ForeColor = Color.Black;
            Layer_textBox.Location = new Point(155, 16);
            Layer_textBox.Name = "Layer_textBox";
            Layer_textBox.Size = new Size(120, 15);
            Layer_textBox.TabIndex = 88;
            Layer_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(dataGridView2);
            panel2.Controls.Add(dataGridView1);
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 387);
            panel2.TabIndex = 18;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.White;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView2.Location = new Point(406, 21);
            dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(551, 342);
            dataGridView2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = Color.White;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = Color.White;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Location = new Point(24, 21);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = Color.White;
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = Color.White;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(360, 342);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(label10);
            AdditionalPanel.Controls.Add(label9);
            AdditionalPanel.Controls.Add(Cwirk_textBox);
            AdditionalPanel.Controls.Add(SlabCwirk_comboBox);
            AdditionalPanel.Controls.Add(label8);
            AdditionalPanel.Controls.Add(InWallCwirk_comboBox);
            AdditionalPanel.Controls.Add(label7);
            AdditionalPanel.Controls.Add(CeilingCwirk_comboBox);
            AdditionalPanel.Controls.Add(label6);
            AdditionalPanel.Controls.Add(WallCwirk_comboBox);
            AdditionalPanel.Controls.Add(label5);
            AdditionalPanel.Location = new Point(12, 546);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(489, 147);
            AdditionalPanel.TabIndex = 18;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(415, 55);
            label10.Name = "label10";
            label10.Size = new Size(62, 15);
            label10.TabIndex = 101;
            label10.Text = "Wh/m²·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(288, 25);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 100;
            label9.Text = "Cwirk";
            // 
            // Cwirk_textBox
            // 
            Cwirk_textBox.BackColor = SystemColors.Window;
            Cwirk_textBox.BorderStyle = BorderStyle.None;
            Cwirk_textBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Cwirk_textBox.Location = new Point(288, 55);
            Cwirk_textBox.Name = "Cwirk_textBox";
            Cwirk_textBox.Size = new Size(120, 15);
            Cwirk_textBox.TabIndex = 99;
            // 
            // SlabCwirk_comboBox
            // 
            SlabCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            SlabCwirk_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            SlabCwirk_comboBox.FormattingEnabled = true;
            SlabCwirk_comboBox.Location = new Point(153, 108);
            SlabCwirk_comboBox.Name = "SlabCwirk_comboBox";
            SlabCwirk_comboBox.Size = new Size(120, 23);
            SlabCwirk_comboBox.TabIndex = 98;
            SlabCwirk_comboBox.SelectedIndexChanged += SlabCwirk_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(96, 112);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 97;
            label8.Text = "바닥";
            // 
            // InWallCwirk_comboBox
            // 
            InWallCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            InWallCwirk_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            InWallCwirk_comboBox.FormattingEnabled = true;
            InWallCwirk_comboBox.Location = new Point(153, 79);
            InWallCwirk_comboBox.Name = "InWallCwirk_comboBox";
            InWallCwirk_comboBox.Size = new Size(120, 23);
            InWallCwirk_comboBox.TabIndex = 96;
            InWallCwirk_comboBox.SelectedIndexChanged += InWallCwirk_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(96, 83);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 95;
            label7.Text = "내벽";
            // 
            // CeilingCwirk_comboBox
            // 
            CeilingCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            CeilingCwirk_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            CeilingCwirk_comboBox.FormattingEnabled = true;
            CeilingCwirk_comboBox.Location = new Point(153, 21);
            CeilingCwirk_comboBox.Name = "CeilingCwirk_comboBox";
            CeilingCwirk_comboBox.Size = new Size(120, 23);
            CeilingCwirk_comboBox.TabIndex = 94;
            CeilingCwirk_comboBox.SelectedIndexChanged += CeilingCwrik_comboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(96, 25);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 93;
            label6.Text = "천장";
            // 
            // WallCwirk_comboBox
            // 
            WallCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WallCwirk_comboBox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            WallCwirk_comboBox.FormattingEnabled = true;
            WallCwirk_comboBox.Location = new Point(153, 50);
            WallCwirk_comboBox.Name = "WallCwirk_comboBox";
            WallCwirk_comboBox.Size = new Size(120, 23);
            WallCwirk_comboBox.TabIndex = 92;
            WallCwirk_comboBox.SelectedIndexChanged += WallCwirk_comboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(96, 54);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 91;
            label5.Text = "외벽";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label17.Location = new Point(12, 116);
            label17.Name = "label17";
            label17.Size = new Size(75, 15);
            label17.TabIndex = 35;
            label17.Text = "존 외피 정보";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(501, 546);
            panel1.Name = "panel1";
            panel1.Size = new Size(488, 147);
            panel1.TabIndex = 36;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExternalZone_radioButton);
            groupBox1.Controls.Add(DoorZone_radioButton);
            groupBox1.Controls.Add(InternalZone_radioButton);
            groupBox1.Location = new Point(34, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(105, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "존 유형";
            // 
            // ExternalZone_radioButton
            // 
            ExternalZone_radioButton.AutoSize = true;
            ExternalZone_radioButton.Enabled = false;
            ExternalZone_radioButton.Location = new Point(6, 69);
            ExternalZone_radioButton.Name = "ExternalZone_radioButton";
            ExternalZone_radioButton.Size = new Size(61, 19);
            ExternalZone_radioButton.TabIndex = 2;
            ExternalZone_radioButton.TabStop = true;
            ExternalZone_radioButton.Text = "외부존";
            ExternalZone_radioButton.UseVisualStyleBackColor = true;
            // 
            // DoorZone_radioButton
            // 
            DoorZone_radioButton.AutoSize = true;
            DoorZone_radioButton.Enabled = false;
            DoorZone_radioButton.Location = new Point(6, 44);
            DoorZone_radioButton.Name = "DoorZone_radioButton";
            DoorZone_radioButton.Size = new Size(73, 19);
            DoorZone_radioButton.TabIndex = 1;
            DoorZone_radioButton.TabStop = true;
            DoorZone_radioButton.Text = "출입문존";
            DoorZone_radioButton.UseVisualStyleBackColor = true;
            // 
            // InternalZone_radioButton
            // 
            InternalZone_radioButton.AutoSize = true;
            InternalZone_radioButton.Enabled = false;
            InternalZone_radioButton.Location = new Point(6, 19);
            InternalZone_radioButton.Name = "InternalZone_radioButton";
            InternalZone_radioButton.Size = new Size(61, 19);
            InternalZone_radioButton.TabIndex = 0;
            InternalZone_radioButton.TabStop = true;
            InternalZone_radioButton.Text = "내부존";
            InternalZone_radioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(12, 526);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 37;
            label2.Text = "축열 성능";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(501, 526);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 38;
            label4.Text = "기밀 성능";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1020, 642);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 89;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // ZoneEnvelope
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Save_button);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(label17);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneEnvelope";
            Text = "Form3";
            VisibleChanged += ZoneEnvelope_VisibleChanged;
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Layer_textBox;
        private System.Windows.Forms.TextBox ZoneName_textBox;
        private System.Windows.Forms.Label label17;
        private Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridViewCheckBoxColumn checkBoxColumn;
        private CustomComboBox WallCwirk_comboBox;
        private System.Windows.Forms.Label label5;
        private CustomComboBox SlabCwirk_comboBox;
        private System.Windows.Forms.Label label8;
        private CustomComboBox InWallCwirk_comboBox;
        private System.Windows.Forms.Label label7;
        private CustomComboBox CeilingCwirk_comboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Cwirk_textBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ExternalZone_radioButton;
        private System.Windows.Forms.RadioButton DoorZone_radioButton;
        private System.Windows.Forms.RadioButton InternalZone_radioButton;
        private System.Windows.Forms.TextBox Num_textBox;
        private PictureBox Icon_pictureBox;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Label label3;
    }
}