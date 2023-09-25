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
            pictureBox1 = new PictureBox();
            PVtype_Combobox = new ComboBox();
            PVname_texbox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            AdditionalPanel = new Panel();
            label5 = new Label();
            Batteryname = new TextBox();
            Inverter = new TextBox();
            PVModuleType = new TextBox();
            Battery = new Label();
            label7 = new Label();
            label6 = new Label();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(pictureBox1);
            GeneralPanel.Controls.Add(PVtype_Combobox);
            GeneralPanel.Controls.Add(PVname_texbox);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 67);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // pictureBox1
            // 
          //  pictureBox1.Image = Properties.Resources.pv;
            pictureBox1.Location = new Point(18, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 35);
            pictureBox1.TabIndex = 91;
            pictureBox1.TabStop = false;
            // 
            // PVtype_Combobox
            // 
            PVtype_Combobox.FormattingEnabled = true;
            PVtype_Combobox.Location = new Point(205, 37);
            PVtype_Combobox.Name = "PVtype_Combobox";
            PVtype_Combobox.Size = new Size(121, 23);
            PVtype_Combobox.TabIndex = 98;
            // 
            // PVname_texbox
            // 
            PVname_texbox.BackColor = SystemColors.Window;
            PVname_texbox.BorderStyle = BorderStyle.None;
            PVname_texbox.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            PVname_texbox.Location = new Point(205, 8);
            PVname_texbox.Name = "PVname_texbox";
            PVname_texbox.Size = new Size(120, 15);
            PVname_texbox.TabIndex = 97;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(126, 40);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 96;
            label3.Text = "Type";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(126, 11);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 95;
            label1.Text = "명칭";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
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
            label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(12, 81);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 95;
            label5.Text = "구성요소정보";
            // 
            // Batteryname
            // 
            Batteryname.BackColor = SystemColors.Window;
            Batteryname.BorderStyle = BorderStyle.None;
            Batteryname.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Batteryname.Location = new Point(203, 63);
            Batteryname.Name = "Batteryname";
            Batteryname.ReadOnly = true;
            Batteryname.Size = new Size(120, 15);
            Batteryname.TabIndex = 105;
            Batteryname.TextAlign = HorizontalAlignment.Center;
            Batteryname.Visible = false;
            // 
            // Inverter
            // 
            Inverter.BackColor = SystemColors.Window;
            Inverter.BorderStyle = BorderStyle.None;
            Inverter.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Inverter.Location = new Point(203, 34);
            Inverter.Name = "Inverter";
            Inverter.ReadOnly = true;
            Inverter.Size = new Size(120, 15);
            Inverter.TabIndex = 104;
            Inverter.TextAlign = HorizontalAlignment.Center;
            // 
            // PVModuleType
            // 
            PVModuleType.BackColor = SystemColors.Window;
            PVModuleType.BorderStyle = BorderStyle.None;
            PVModuleType.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            Battery.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Battery.Location = new Point(16, 65);
            Battery.Name = "Battery";
            Battery.Size = new Size(43, 15);
            Battery.TabIndex = 103;
            Battery.Text = "배터리";
            Battery.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(16, 36);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 102;
            label7.Text = "인버터";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(16, 8);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 101;
            label6.Text = "태양광 모듈";
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private ComboBox PVtype_Combobox;
        private TextBox PVname_texbox;
        private Label label3;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label5;
        private TextBox Batteryname;
        private Label label6;
        private TextBox Inverter;
        private Label label7;
        private TextBox PVModuleType;
        private Label Battery;
    }
}