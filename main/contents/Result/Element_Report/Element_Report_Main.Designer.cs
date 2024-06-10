namespace main.contents
{
    partial class Element_Report_Main
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
            Element_comboBox = new CustomComboBox();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
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
            webView21.Size = new Size(1200, 730);
            webView21.Source = new Uri("http://localhost:3000/report.html", UriKind.Absolute);
            webView21.TabIndex = 2;
            webView21.ZoomFactor = 1D;
            // 
            // Element_comboBox
            // 
            Element_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            Element_comboBox.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Element_comboBox.FormattingEnabled = true;
            Element_comboBox.Location = new Point(867, 12);
            Element_comboBox.Name = "Element_comboBox";
            Element_comboBox.Size = new Size(165, 23);
            Element_comboBox.TabIndex = 122;
            Element_comboBox.SelectedIndexChanged += Element_comboBox_SelectedIndexChanged;
            // 
            // Element_Report
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Element_comboBox);
            Controls.Add(webView21);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Element_Report";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private CustomComboBox Element_comboBox;
    }
}