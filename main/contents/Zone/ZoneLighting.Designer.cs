namespace main.contents
{
    partial class ZoneLighting
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
            AdditionalPanel = new Panel();
            Icon_pictureBox = new PictureBox();
            Type_textBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            Name_textBox = new TextBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(Icon_pictureBox);
            GeneralPanel.Controls.Add(Type_textBox);
            GeneralPanel.Controls.Add(label2);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(Name_textBox);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Location = new Point(12, 136);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 149);
            panel2.TabIndex = 18;
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
            // Icon_pictureBox
            // 
            Icon_pictureBox.Location = new Point(38, 19);
            Icon_pictureBox.Name = "Icon_pictureBox";
            Icon_pictureBox.Size = new Size(50, 50);
            Icon_pictureBox.TabIndex = 96;
            Icon_pictureBox.TabStop = false;
            // 
            // Type_textBox
            // 
            Type_textBox.BackColor = SystemColors.Desktop;
            Type_textBox.BorderStyle = BorderStyle.None;
            Type_textBox.Enabled = false;
            Type_textBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Type_textBox.ForeColor = SystemColors.ControlDark;
            Type_textBox.Location = new Point(185, 63);
            Type_textBox.Name = "Type_textBox";
            Type_textBox.Size = new Size(120, 15);
            Type_textBox.TabIndex = 95;
            Type_textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(150, 24);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 92;
            label2.Text = "명칭";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(150, 63);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 93;
            label3.Text = "TYPE";
            // 
            // Name_textBox
            // 
            Name_textBox.Location = new Point(185, 21);
            Name_textBox.Name = "Name_textBox";
            Name_textBox.Size = new Size(120, 23);
            Name_textBox.TabIndex = 94;
            // 
            // ZoneLighting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ZoneLighting";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Icon_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private PictureBox Icon_pictureBox;
        private TextBox Type_textBox;
        private Label label2;
        private Label label3;
        private TextBox Name_textBox;
    }
}