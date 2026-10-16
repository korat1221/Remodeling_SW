
namespace main.subcontents.EquipmentList
{
    partial class CTopCal
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
            Fan_comboBox = new ComboBox();
            label3 = new Label();
            Save_button = new Button();
            fluid = new Label();
            ctpower = new Label();
            intemp = new Label();
            outtemp = new Label();
            calButton = new Button();
            pictureBox2 = new PictureBox();
            CG_comboBox = new ComboBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            Power_textBox = new TextBox();
            dPanel_label = new Label();
            label4 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.AliceBlue;
            GeneralPanel.Controls.Add(Fan_comboBox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Save_button);
            GeneralPanel.Controls.Add(fluid);
            GeneralPanel.Controls.Add(ctpower);
            GeneralPanel.Controls.Add(intemp);
            GeneralPanel.Controls.Add(outtemp);
            GeneralPanel.Controls.Add(calButton);
            GeneralPanel.Controls.Add(pictureBox2);
            GeneralPanel.Controls.Add(CG_comboBox);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(Power_textBox);
            GeneralPanel.Controls.Add(dPanel_label);
            GeneralPanel.Controls.Add(label4);
            GeneralPanel.Location = new Point(-1, -1);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(508, 394);
            GeneralPanel.TabIndex = 19;
            // 
            // Fan_comboBox
            // 
            Fan_comboBox.FormattingEnabled = true;
            Fan_comboBox.Location = new Point(108, 57);
            Fan_comboBox.Name = "Fan_comboBox";
            Fan_comboBox.Size = new Size(138, 23);
            Fan_comboBox.TabIndex = 147;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label3.Location = new Point(38, 61);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 146;
            label3.Text = "팬 종류";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(314, 355);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(135, 25);
            Save_button.TabIndex = 21;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // fluid
            // 
            fluid.AutoSize = true;
            fluid.BackColor = Color.Transparent;
            fluid.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Italic);
            fluid.Location = new Point(211, 273);
            fluid.Name = "fluid";
            fluid.Size = new Size(33, 15);
            fluid.TabIndex = 145;
            fluid.Text = "fluid";
            // 
            // ctpower
            // 
            ctpower.AutoSize = true;
            ctpower.BackColor = Color.Transparent;
            ctpower.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Italic);
            ctpower.Location = new Point(211, 239);
            ctpower.Name = "ctpower";
            ctpower.Size = new Size(56, 15);
            ctpower.TabIndex = 143;
            ctpower.Text = "ctpower";
            // 
            // intemp
            // 
            intemp.AutoSize = true;
            intemp.BackColor = Color.Transparent;
            intemp.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Italic);
            intemp.Location = new Point(211, 161);
            intemp.Name = "intemp";
            intemp.Size = new Size(50, 15);
            intemp.TabIndex = 142;
            intemp.Text = "intemp";
            // 
            // outtemp
            // 
            outtemp.AutoSize = true;
            outtemp.BackColor = Color.Transparent;
            outtemp.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Italic);
            outtemp.Location = new Point(211, 116);
            outtemp.Name = "outtemp";
            outtemp.Size = new Size(60, 15);
            outtemp.TabIndex = 141;
            outtemp.Text = "outtemp";
            // 
            // calButton
            // 
            calButton.Location = new Point(333, 57);
            calButton.Name = "calButton";
            calButton.Size = new Size(109, 23);
            calButton.TabIndex = 140;
            calButton.Text = "계산";
            calButton.UseVisualStyleBackColor = true;
            calButton.Click += calButton_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(59, 83);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(390, 250);
            pictureBox2.TabIndex = 139;
            pictureBox2.TabStop = false;
            // 
            // CG_comboBox
            // 
            CG_comboBox.FormattingEnabled = true;
            CG_comboBox.Location = new Point(108, 32);
            CG_comboBox.Name = "CG_comboBox";
            CG_comboBox.Size = new Size(138, 23);
            CG_comboBox.TabIndex = 138;
            CG_comboBox.SelectedIndexChanged += CG_comboBox_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.Window;
            pictureBox1.Location = new Point(-2, 83);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(510, 252);
            pictureBox1.TabIndex = 137;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(276, 36);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 129;
            label1.Text = "냉방출력";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("나눔바른고딕", 9.75F);
            label2.ForeColor = SystemColors.ControlDark;
            label2.Location = new Point(444, 36);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 128;
            label2.Text = "kW";
            // 
            // Power_textBox
            // 
            Power_textBox.BorderStyle = BorderStyle.FixedSingle;
            Power_textBox.Location = new Point(333, 32);
            Power_textBox.Name = "Power_textBox";
            Power_textBox.Size = new Size(109, 23);
            Power_textBox.TabIndex = 127;
            Power_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // dPanel_label
            // 
            dPanel_label.AutoSize = true;
            dPanel_label.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            dPanel_label.Location = new Point(38, 36);
            dPanel_label.Name = "dPanel_label";
            dPanel_label.Size = new Size(70, 15);
            dPanel_label.TabIndex = 126;
            dPanel_label.Text = "냉동기 종류";
            dPanel_label.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("나눔바른고딕", 9.75F, FontStyle.Bold);
            label4.Location = new Point(13, 10);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 95;
            label4.Text = "냉각탑 계산";
            // 
            // CTopCal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 391);
            Controls.Add(GeneralPanel);
            Name = "CTopCal";
            Text = "CTopCal";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private ComboBox CG_comboBox;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private TextBox Power_textBox;
        private Label dPanel_label;
        private Label label4;
        private PictureBox pictureBox2;
        private Label outtemp;
        private Button calButton;
        private Label ctpower;
        private Label intemp;
        private Label fluid;
        private Button Save_button;
        private Label label3;
        private ComboBox Fan_comboBox;
    }
}