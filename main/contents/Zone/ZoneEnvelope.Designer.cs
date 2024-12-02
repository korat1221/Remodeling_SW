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
            checkBoxColumn = new DataGridViewCheckBoxColumn();
            GeneralPanel = new Panel();
            Num_textBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Icon_pictureBox = new PictureBox();
            ZoneName_textBox = new System.Windows.Forms.TextBox();
            panel2 = new Panel();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            panel3 = new Panel();
            AdditionalPanel = new Panel();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            Cwirk_textBox = new System.Windows.Forms.TextBox();
            SlabCwirk_comboBox = new CustomComboBox();
            label8 = new System.Windows.Forms.Label();
            InWallCwirk_comboBox = new CustomComboBox();
            label7 = new System.Windows.Forms.Label();
            CeilingCwirk_comboBox = new CustomComboBox();
            label6 = new System.Windows.Forms.Label();
            WallCwirk_comboBox = new CustomComboBox();
            label5 = new System.Windows.Forms.Label();
            panel1 = new Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ExternalZone_radioButton = new System.Windows.Forms.RadioButton();
            DoorZone_radioButton = new System.Windows.Forms.RadioButton();
            InternalZone_radioButton = new System.Windows.Forms.RadioButton();
            label4 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            Save_button = new System.Windows.Forms.Button();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel3.SuspendLayout();
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
            GeneralPanel.BackColor = SystemColors.GradientActiveCaption;
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(ZoneName_textBox);
            GeneralPanel.Location = new Point(0, 4);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1000, 80);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = SystemColors.GradientActiveCaption;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Num_textBox.ForeColor = Color.Black;
            Num_textBox.Location = new Point(153, 9);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(120, 15);
            Num_textBox.TabIndex = 191;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font(UTIL.Families[0], 9.75F);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(111, 50);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 104;
            label3.Text = "명칭";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font(UTIL.Families[0], 9.75F);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(111, 12);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 103;
            label1.Text = "번호";
            // 
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(18, 14);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 93;
            Icon_pictureBox.TabStop = false;
            // 
            // ZoneName_textBox
            // 
            ZoneName_textBox.BackColor = SystemColors.GradientActiveCaption;
            ZoneName_textBox.BorderStyle = BorderStyle.None;
            ZoneName_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            ZoneName_textBox.ForeColor = Color.Black;
            ZoneName_textBox.Location = new Point(153, 50);
            ZoneName_textBox.Name = "ZoneName_textBox";
            ZoneName_textBox.Size = new Size(120, 15);
            ZoneName_textBox.TabIndex = 89;
            ZoneName_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(dataGridView2);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label17);
            panel2.Location = new Point(0, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(1000, 535);
            panel2.TabIndex = 18;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.BackgroundColor = SystemColors.Window;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(448, 39);
            dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView2.Size = new Size(520, 285);
            dataGridView2.TabIndex = 114;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 39);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new Font(UTIL.Families[0], 9.75F);
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Size = new Size(393, 285);
            dataGridView1.TabIndex = 113;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(AdditionalPanel);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 331);
            panel3.Name = "panel3";
            panel3.Size = new Size(1000, 204);
            panel3.TabIndex = 36;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.Controls.Add(label10);
            AdditionalPanel.Controls.Add(label9);
            AdditionalPanel.Controls.Add(label2);
            AdditionalPanel.Controls.Add(Cwirk_textBox);
            AdditionalPanel.Controls.Add(SlabCwirk_comboBox);
            AdditionalPanel.Controls.Add(label8);
            AdditionalPanel.Controls.Add(InWallCwirk_comboBox);
            AdditionalPanel.Controls.Add(label7);
            AdditionalPanel.Controls.Add(CeilingCwirk_comboBox);
            AdditionalPanel.Controls.Add(label6);
            AdditionalPanel.Controls.Add(WallCwirk_comboBox);
            AdditionalPanel.Controls.Add(label5);
            AdditionalPanel.Dock = DockStyle.Left;
            AdditionalPanel.Location = new Point(0, 0);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(500, 204);
            AdditionalPanel.TabIndex = 18;
            AdditionalPanel.Paint += AdditionalPanel_Paint;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font(UTIL.Families[0], 9.75F);
            label10.Location = new Point(418, 84);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 101;
            label10.Text = "Wh/m" + Program.UTIL.Subscript(2, true) + "·K";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font(UTIL.Families[0], 9.75F);
            label9.Location = new Point(291, 54);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 100;
            label9.Text = "Cwirk";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label2.Location = new Point(18, 10);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 37;
            label2.Text = "축열 성능";
            // 
            // Cwirk_textBox
            // 
            Cwirk_textBox.BackColor = SystemColors.Window;
            Cwirk_textBox.BorderStyle = BorderStyle.None;
            Cwirk_textBox.Font = new Font(UTIL.Families[0], 9.75F);
            Cwirk_textBox.Location = new Point(291, 84);
            Cwirk_textBox.Name = "Cwirk_textBox";
            Cwirk_textBox.Size = new Size(120, 15);
            Cwirk_textBox.TabIndex = 99;
            // 
            // SlabCwirk_comboBox
            // 
            SlabCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            SlabCwirk_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            SlabCwirk_comboBox.FormattingEnabled = true;
            SlabCwirk_comboBox.Location = new Point(101, 137);
            SlabCwirk_comboBox.Name = "SlabCwirk_comboBox";
            SlabCwirk_comboBox.Size = new Size(175, 23);
            SlabCwirk_comboBox.TabIndex = 98;
            SlabCwirk_comboBox.SelectedIndexChanged += SlabCwirk_comboBox_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font(UTIL.Families[0], 9.75F);
            label8.Location = new Point(36, 141);
            label8.Name = "label8";
            label8.Size = new Size(31, 15);
            label8.TabIndex = 97;
            label8.Text = "바닥";
            // 
            // InWallCwirk_comboBox
            // 
            InWallCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            InWallCwirk_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            InWallCwirk_comboBox.FormattingEnabled = true;
            InWallCwirk_comboBox.Location = new Point(101, 108);
            InWallCwirk_comboBox.Name = "InWallCwirk_comboBox";
            InWallCwirk_comboBox.Size = new Size(175, 23);
            InWallCwirk_comboBox.TabIndex = 96;
            InWallCwirk_comboBox.SelectedIndexChanged += InWallCwirk_comboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font(UTIL.Families[0], 9.75F);
            label7.Location = new Point(36, 112);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 95;
            label7.Text = "내벽";
            // 
            // CeilingCwirk_comboBox
            // 
            CeilingCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            CeilingCwirk_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            CeilingCwirk_comboBox.FormattingEnabled = true;
            CeilingCwirk_comboBox.Location = new Point(101, 50);
            CeilingCwirk_comboBox.Name = "CeilingCwirk_comboBox";
            CeilingCwirk_comboBox.Size = new Size(175, 23);
            CeilingCwirk_comboBox.TabIndex = 94;
            CeilingCwirk_comboBox.SelectedIndexChanged += CeilingCwrik_comboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font(UTIL.Families[0], 9.75F);
            label6.Location = new Point(36, 54);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 93;
            label6.Text = "천장";
            // 
            // WallCwirk_comboBox
            // 
            WallCwirk_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            WallCwirk_comboBox.Font = new Font(UTIL.Families[0], 9.75F);
            WallCwirk_comboBox.FormattingEnabled = true;
            WallCwirk_comboBox.Location = new Point(101, 79);
            WallCwirk_comboBox.Name = "WallCwirk_comboBox";
            WallCwirk_comboBox.Size = new Size(175, 23);
            WallCwirk_comboBox.TabIndex = 92;
            WallCwirk_comboBox.SelectedIndexChanged += WallCwirk_comboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font(UTIL.Families[0], 9.75F);
            label5.Location = new Point(36, 83);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 91;
            label5.Text = "외벽";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(500, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 204);
            panel1.TabIndex = 36;
            panel1.Paint += panel1_Paint;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExternalZone_radioButton);
            groupBox1.Controls.Add(DoorZone_radioButton);
            groupBox1.Controls.Add(InternalZone_radioButton);
            groupBox1.Location = new Point(55, 47);
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label4.Location = new Point(19, 10);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 38;
            label4.Text = "기밀 성능";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Bold);
            label17.Location = new Point(18, 7);
            label17.Name = "label17";
            label17.Size = new Size(73, 15);
            label17.TabIndex = 35;
            label17.Text = "존 외피 정보";
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1020, 594);
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
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneEnvelope";
            Text = "Form3";
            VisibleChanged += ZoneEnvelope_VisibleChanged;
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel3.ResumeLayout(false);
            AdditionalPanel.ResumeLayout(false);
            AdditionalPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ZoneName_textBox;
        private System.Windows.Forms.Label label17;
        private Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
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
        private PictureBox Icon_pictureBox;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Num_textBox;
        private Panel panel3;
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
    }
}