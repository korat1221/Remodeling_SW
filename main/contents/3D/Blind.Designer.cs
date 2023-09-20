namespace main.contents
{
    partial class Blind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Blind));
            GeneralPanel = new Panel();
            textBox2 = new TextBox();
            label3 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            panel2 = new Panel();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            GeneralPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(155, 52);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 22);
            textBox2.TabIndex = 89;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(74, 56);
            label3.Name = "label3";
            label3.Size = new Size(51, 16);
            label3.TabIndex = 3;
            label3.Text = "차양정보";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(102, 19);
            label1.Name = "label1";
            label1.Size = new Size(18, 16);
            label1.TabIndex = 1;
            label1.Text = "층";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(188, 291);
            webView21.Name = "webView21";
            webView21.Size = new Size(790, 407);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 453);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 19;
            label2.Text = "차양가동율(on)";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(140, 449);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 19);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // Blind
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(webView21);
            Controls.Add(panel2);
            Controls.Add(GeneralPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Blind";
            Text = "Form3";
            GeneralPanel.ResumeLayout(false);
            GeneralPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel GeneralPanel;
        private Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Label label2;
        private PictureBox pictureBox1;
    }
}