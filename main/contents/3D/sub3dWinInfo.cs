using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class sub3dWINInfo : Form
    {
        public sub3dWINInfo()
        {
            InitializeComponent();
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
            String ID = main.MainContents.selID.Replace("board-", "");
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,번호", "아이디 = '" + ID + "'");

            if (rec.Length > 0)
            {
                textBox23.Text = (rec[0][0] == "0" ? "0" : Double.Parse(rec[0][0]).ToString("#.##"));
                textBox2.Text = (rec[0][1] == "0" ? "0" : Double.Parse(rec[0][1]).ToString("#.##"));
                textBox1.Text = (rec[0][2] == "0" ? "0" : Double.Parse(rec[0][2]).ToString("#.##"));
                textBox3.Text = (rec[0][3] == "0" ? "0" : Double.Parse(rec[0][3]).ToString("#.##"));
                textBox4.Text = (rec[0][4]);
            }

        }
    }
}
