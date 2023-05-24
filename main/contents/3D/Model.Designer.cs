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
            panel2 = new Panel();
            AdditionalPanel = new Panel();
            dataGridView1 = new DataGridView();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            AdditionalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(977, 70);
            panel2.TabIndex = 18;
            // 
            // AdditionalPanel
            // 
            AdditionalPanel.BackColor = Color.White;
            AdditionalPanel.BorderStyle = BorderStyle.Fixed3D;
            AdditionalPanel.Controls.Add(webView21);
            AdditionalPanel.Controls.Add(dataGridView1);
            AdditionalPanel.Location = new Point(12, 88);
            AdditionalPanel.Name = "AdditionalPanel";
            AdditionalPanel.Size = new Size(977, 605);
            AdditionalPanel.TabIndex = 18;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(45, 26);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(883, 558);
            dataGridView1.TabIndex = 0;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(66, 42);
            webView21.Name = "webView21";
            webView21.Size = new Size(786, 478);
            webView21.Source = new Uri("http://localhost:3000", UriKind.Absolute);
            webView21.TabIndex = 1;
            webView21.ZoomFactor = 1D;
            // 
            // Model
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1200, 730);
            Controls.Add(panel2);
            Controls.Add(AdditionalPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Model";
            Text = "Form3";
            AdditionalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Panel AdditionalPanel;
        private DataGridView dataGridView1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
