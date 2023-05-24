namespace main.contents
{
    partial class ConstructionFloor
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
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            panel2 = new Panel();
            AdditionalPanel = new Panel();
            textBox2 = new System.Windows.Forms.TextBox();
            GeneralPanel.SuspendLayout();
            SuspendLayout();
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = Color.White;
            GeneralPanel.Controls.Add(textBox2);
            GeneralPanel.Controls.Add(label3);
            GeneralPanel.Controls.Add(label1);
            GeneralPanel.Controls.Add(textBox1);
            GeneralPanel.Location = new Point(12, 12);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(977, 101);
            GeneralPanel.TabIndex = 17;
            GeneralPanel.Paint += GeneralPanel_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("나눔고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(74, 56);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 3;
            label3.Text = "1F_Zone01";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(102, 19);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 1;
            label1.Text = "층";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(155, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 22);
            textBox1.TabIndex = 88;
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
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(155, 52);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 22);
            textBox2.TabIndex = 89;
            // 
            // ConstructionFloor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ConstructionFloor";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private Panel AdditionalPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
    }
}