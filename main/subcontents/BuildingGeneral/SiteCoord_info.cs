using System;
using System.Drawing;
using System.Windows.Forms;
using main.info;

namespace main.subcontents.BuildingGeneral;

public partial class SiteCoord_info : Form
{
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

    private void Cancel_button_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
