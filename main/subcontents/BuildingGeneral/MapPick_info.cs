using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;
using main.info;

namespace main.subcontents.BuildingGeneral;

public partial class MapPick_info : Form
{
    public double Latitude;
    public double Longitude;

    public MapPick_info(double latitude, double longitude)
    {
        InitializeComponent();
        this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        Latitude = latitude;
        Longitude = longitude;
        InitializeAsync();
    }

    async void InitializeAsync()
    {
        await webView21.EnsureCoreWebView2Async(null);
        webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
        string fileUrl = new Uri(Path.Combine(Program.gPath, "mapPicker.html")).AbsoluteUri;
        if (Latitude != 0 || Longitude != 0)
        {
            fileUrl += "?lat=" + Latitude + "&lng=" + Longitude;
        }
        webView21.Source = new Uri(fileUrl);
    }

    void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        JObject data = JObject.Parse(args.TryGetWebMessageAsString());
        Latitude = data["lat"].Value<double>();
        Longitude = data["lng"].Value<double>();
        DialogResult = DialogResult.OK;
        Close();
    }
}
