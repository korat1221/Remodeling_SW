namespace main.subcontents.Info
{
    partial class Info_Roof
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
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            Previous_button = new Button();
            Next_button = new Button();
            GeneralPanel = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            GeneralPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.BackColor = SystemColors.ActiveBorder;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new Size(1250, 764);
            webView21.TabIndex = 2;
            webView21.ZoomFactor = 1D;
            // 
            // Previous_button
            // 
            Previous_button.BackColor = SystemColors.ButtonHighlight;
            Previous_button.ForeColor = Color.Black;
            Previous_button.Location = new Point(12, 12);
            Previous_button.Name = "Previous_button";
            Previous_button.Size = new Size(88, 25);
            Previous_button.TabIndex = 95;
            Previous_button.Text = "<<PREVIOUS";
            Previous_button.UseVisualStyleBackColor = true;
            Previous_button.Click += Previous_button_Click;
            // 
            // Next_button
            // 
            Next_button.BackColor = SystemColors.ButtonHighlight;
            Next_button.ForeColor = Color.Black;
            Next_button.Location = new Point(106, 12);
            Next_button.Name = "Next_button";
            Next_button.Size = new Size(88, 25);
            Next_button.TabIndex = 94;
            Next_button.Text = "NEXT>>";
            Next_button.UseVisualStyleBackColor = true;
            Next_button.Click += Next_button_Click;
            // 
            // GeneralPanel
            // 
            GeneralPanel.BackColor = SystemColors.GradientActiveCaption;
            GeneralPanel.Controls.Add(Previous_button);
            GeneralPanel.Controls.Add(Next_button);
            GeneralPanel.Dock = DockStyle.Top;
            GeneralPanel.Location = new Point(0, 0);
            GeneralPanel.Name = "GeneralPanel";
            GeneralPanel.Size = new Size(1250, 54);
            GeneralPanel.TabIndex = 96;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(webView21);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1250, 764);
            panel1.TabIndex = 97;
            // 
            // Info_Roof
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 764);
            Controls.Add(GeneralPanel);
            Controls.Add(panel1);
            Name = "Info_Roof";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            GeneralPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Button Previous_button;
        private Button Next_button;
        private Panel GeneralPanel;
        private Panel panel1;
    }
}