namespace main.contents
{
    partial class Model
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
            Save_button = new Button();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            Import_button = new Button();
            Export_button = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // Save_button
            // 
            Save_button.BackColor = SystemColors.ButtonHighlight;
            Save_button.ForeColor = Color.Black;
            Save_button.Location = new Point(1018, 693);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(88, 25);
            Save_button.TabIndex = 92;
            Save_button.Text = "SAVE";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_button_Click;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new Size(971, 385);
            webView21.Source = new Uri("http://localhost:3000/anal3d/editor/", UriKind.Absolute);
            webView21.TabIndex = 97;
            webView21.ZoomFactor = 1D;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Left;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AllowDrop = true;
            splitContainer1.Panel1.Controls.Add(webView21);
            splitContainer1.Size = new Size(971, 730);
            splitContainer1.SplitterDistance = 385;
            splitContainer1.TabIndex = 96;
            // 
            // Import_button
            // 
            Import_button.BackColor = SystemColors.ButtonHighlight;
            Import_button.ForeColor = Color.Black;
            Import_button.Location = new Point(1075, 22);
            Import_button.Name = "Import_button";
            Import_button.Size = new Size(88, 25);
            Import_button.TabIndex = 97;
            Import_button.Text = "Import";
            Import_button.UseVisualStyleBackColor = true;
            Import_button.Click += Import_button_Click;
            // 
            // Export_button
            // 
            Export_button.BackColor = SystemColors.ButtonHighlight;
            Export_button.ForeColor = Color.Black;
            Export_button.Location = new Point(977, 22);
            Export_button.Name = "Export_button";
            Export_button.Size = new Size(88, 25);
            Export_button.TabIndex = 98;
            Export_button.Text = "Export";
            Export_button.UseVisualStyleBackColor = true;
            Export_button.Click += Export_button_Click;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(Export_button);
            Controls.Add(Import_button);
            Controls.Add(splitContainer1);
            Controls.Add(Save_button);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Model";
            Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button Save_button;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private SplitContainer splitContainer1;
        private Button Import_button;
        private Button Export_button;
    }
}