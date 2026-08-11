using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using main.info;

namespace main.subcontents.BuildingGeneral;

public partial class SiteCoord_info : Form
{
    // 카카오 디벨로퍼스(developers.kakao.com) > 내 애플리케이션 > 앱 키 > REST API 키
    const string KAKAO_REST_API_KEY = "여기에_카카오_REST_API_키_입력";

    public double Latitude;
    public double Longitude;

    public SiteCoord_info(double latitude, double longitude)
    {
        InitializeComponent();
        this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

        Latitude = latitude;
        Longitude = longitude;
        if (Latitude != 0) Lat_textBox.Text = Latitude.ToString("0.00");
        if (Longitude != 0) Lon_textBox.Text = Longitude.ToString("0.00");
    }

    private void Save_button_Click(object sender, EventArgs e)
    {
        double lat = Program.UTIL.ToDoubleOrZero(Lat_textBox.Text);
        double lon = Program.UTIL.ToDoubleOrZero(Lon_textBox.Text);

        if (lat < 33 || lat > 39 || lon < 124 || lon > 132)
        {
            MessageBox.Show("대한민국 범위(위도 33~39°, 경도 124~132°)의 좌표를 입력해주세요");
            return;
        }

        Latitude = lat;
        Longitude = lon;
        DialogResult = DialogResult.OK;
        Close();
    }

    private async void AddressSearch_button_Click(object sender, EventArgs e)
    {
        AddressSearch_info form = new AddressSearch_info();
        DialogResult result = form.ShowDialog();
        if (result != DialogResult.OK || string.IsNullOrEmpty(form.RoadAddress))
        {
            return;
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "KakaoAK " + KAKAO_REST_API_KEY);
        string url = "https://dapi.kakao.com/v2/local/search/address.json?query=" + Uri.EscapeDataString(form.RoadAddress);

        string json;
        try
        {
            json = await client.GetStringAsync(url);
        }
        catch (Exception ex)
        {
            MessageBox.Show("좌표 변환 요청에 실패했습니다: " + ex.Message);
            return;
        }

        JArray documents = (JArray)JObject.Parse(json)["documents"];
        if (documents.Count == 0)
        {
            MessageBox.Show("이 주소로 좌표를 찾지 못했습니다: " + form.RoadAddress);
            return;
        }

        Longitude = documents[0]["x"].Value<double>();
        Latitude = documents[0]["y"].Value<double>();
        Lat_textBox.Text = Latitude.ToString("0.00");
        Lon_textBox.Text = Longitude.ToString("0.00");
    }

    private void MapSelect_button_Click(object sender, EventArgs e)
    {
        double lat = Program.UTIL.ToDoubleOrZero(Lat_textBox.Text);
        double lon = Program.UTIL.ToDoubleOrZero(Lon_textBox.Text);
        MapPick_info form = new MapPick_info(lat, lon);
        DialogResult result = form.ShowDialog();
        if (result == DialogResult.OK)
        {
            Latitude = form.Latitude;
            Longitude = form.Longitude;
            Lat_textBox.Text = Latitude.ToString("0.00");
            Lon_textBox.Text = Longitude.ToString("0.00");
        }
    }

    private void Cancel_button_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
