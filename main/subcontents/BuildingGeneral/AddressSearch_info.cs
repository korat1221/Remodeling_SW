using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;
using main.info;

namespace main.subcontents.BuildingGeneral;

public partial class AddressSearch_info : Form
{
    public string RoadAddress;

    public AddressSearch_info()
    {
        InitializeComponent();
        this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        InitializeAsync();
    }

    async void InitializeAsync()
    {
        await webView21.EnsureCoreWebView2Async(null);
        webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
        string fileUrl = new Uri(Path.Combine(Program.gPath, "postcodesearch.html")).AbsoluteUri;
        webView21.Source = new Uri(fileUrl);
    }

    void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        JObject data = JObject.Parse(args.TryGetWebMessageAsString());
        RoadAddress = data["address"]?.ToString();
        DialogResult = DialogResult.OK;
        Close();
    }
}
