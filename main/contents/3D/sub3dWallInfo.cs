using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class sub3dWallInfo : Form
    {
        public sub3dWallInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            String ID = main.MainContents.selID.Replace("board-", "");
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "아이디 = '" + ID + "'");

            if (rec.Length > 0)
            {
                textBox1.Text = Double.Parse(rec[0][0]).ToString("#.##");
            }

            if (ID.IndexOf("_INWALL_") > 0)
            {
                label3.Show();
                label1.Hide();
            }
            else
            {
                label1.Show();
                label3.Hide();
            }
        }
    }
}
