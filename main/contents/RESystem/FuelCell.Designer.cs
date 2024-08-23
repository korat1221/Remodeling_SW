namespace main.contents
{
    partial class FuelCell
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
            panel2 = new Panel();
            FCDB_button = new Button();
            Batteryname = new TextBox();
            label6 = new Label();
            Inverter = new TextBox();
            label7 = new Label();
            PVModuleType = new TextBox();
            Battery = new Label();
            AdditionalPanel = new Panel();
            label5 = new Label();
            label1 = new Label();
            Num_textBox = new TextBox();
            Name_textBox = new TextBox();
            pictureBox1 = new PictureBox();
            GeneralPanel.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(Num_textBox);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 67);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(FCDB_button);
            panel2.Controls.Add(Batteryname);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(Inverter);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(PVModuleType);
            panel2.Controls.Add(Battery);
            panel2.Location = new Point(12, 99);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 149);
            panel2.TabIndex = 18;
            panel2.Paint += panel2_Paint;
            // 
            // FCDB_button
            // 
            FCDB_button.BackColor = SystemColors.ControlLight;
            FCDB_button.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            FCDB_button.FlatStyle = FlatStyle.System;
            FCDB_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point);
            FCDB_button.Location = new Point(326, 8);
            FCDB_button.Margin = new Padding(0);
            FCDB_button.Name = "FCDB_button";
            FCDB_button.Size = new Size(23, 23);
            FCDB_button.TabIndex = 96;
            FCDB_button.Text = "+";
            FCDB_button.UseVisualStyleBackColor = false;
            FCDB_button.Click += FCDB_button_Click;
            // 
            // Batteryname
            // 
            Batteryname.BackColor = SystemColors.Window;
            Batteryname.BorderStyle = BorderStyle.None;
            Batteryname.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batteryname.Location = new Point(203, 63);
            Batteryname.Name = "Batteryname";
            Batteryname.ReadOnly = true;
            Batteryname.Size = new Size(120, 15);
            Batteryname.TabIndex = 105;
            Batteryname.TextAlign = HorizontalAlignment.Center;
            Batteryname.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(16, 8);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 101;
            label6.Text = "연료전지";
            // 
            // Inverter
            // 
            Inverter.BackColor = SystemColors.Window;
            Inverter.BorderStyle = BorderStyle.None;
            Inverter.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Inverter.Location = new Point(203, 34);
            Inverter.Name = "Inverter";
            Inverter.ReadOnly = true;
            Inverter.Size = new Size(120, 15);
            Inverter.TabIndex = 104;
            Inverter.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(16, 36);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 102;
            label7.Text = "인버터";
            // 
            // PVModuleType
            // 
            PVModuleType.BackColor = SystemColors.Window;
            PVModuleType.BorderStyle = BorderStyle.None;
            PVModuleType.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVModuleType.Location = new Point(203, 6);
            PVModuleType.Name = "PVModuleType";
            PVModuleType.ReadOnly = true;
            PVModuleType.Size = new Size(120, 15);
            PVModuleType.TabIndex = 100;
            PVModuleType.TextAlign = HorizontalAlignment.Center;
            // 
            // Battery
            // 
            Battery.AutoSize = true;
            Battery.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Battery.Location = new Point(16, 65);
            Battery.Name = "Battery";
            Battery.Size = new Size(43, 15);
            Battery.TabIndex = 103;
            Battery.Text = "배터리";
            Battery.Visible = false;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Location = new Point(12, 303);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 390);
            AdditionalPanel.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(12, 81);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 95;
            label5.Text = "구성요소정보";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(127, 26);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 137;
            label1.Text = "명칭";
            // 
            // Num_textBox
            // 
            Num_textBox.BackColor = Color.White;
            Num_textBox.BorderStyle = BorderStyle.None;
            Num_textBox.Enabled = false;
            Num_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Num_textBox.ForeColor = SystemColors.ControlText;
            Num_textBox.Location = new Point(65, 26);
            Num_textBox.Name = "Num_textBox";
            Num_textBox.Size = new Size(56, 15);
            Num_textBox.TabIndex = 136;
            Num_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // Name_textBox
            // 
            Name_textBox.BorderStyle = BorderStyle.FixedSingle;
            Name_textBox.Location = new Point(164, 23);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 135;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(9, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.TabIndex = 134;
            pictureBox1.TabStop = false;
            // 
            // FuelCell
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(label5);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FuelCell";
            Text = "Form3";
            Load += FuelCell_Load;
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private Label label5;
        private TextBox Batteryname;
        private Label label6;
        private TextBox Inverter;
        private TextBox PVModuleType;
        private Button FCDB_button;
        private Label label7;
        private Label Battery;
        private Label label1;
        private TextBox Num_textBox;
        private TextBox Name_textBox;
        private PictureBox pictureBox1;
    }
}