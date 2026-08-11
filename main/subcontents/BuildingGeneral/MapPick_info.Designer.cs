namespace main.subcontents.BuildingGeneral;

partial class MapPick_info
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
        ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
        SuspendLayout();
        //
        // webView21
        //
        webView21.AllowExternalDrop = true;
        webView21.CreationProperties = null;
        webView21.DefaultBackgroundColor = Color.White;
        webView21.Dock = DockStyle.Fill;
        webView21.Location = new Point(0, 0);
        webView21.Name = "webView21";
        webView21.Size = new Size(500, 500);
        webView21.TabIndex = 0;
        webView21.ZoomFactor = 1D;
        //
        // MapPick_info
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(500, 500);
        Controls.Add(webView21);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MapPick_info";
        StartPosition = FormStartPosition.CenterParent;
        Text = "지도에서 선택";
        ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
        ResumeLayout(false);
    }

    #endregion
    private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
}
