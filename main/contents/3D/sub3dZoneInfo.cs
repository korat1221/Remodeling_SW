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
    public partial class sub3dZoneInfo : Form
    {
        public sub3dZoneInfo()
        {
            InitializeComponent();

            dataGridView1.Columns[7].HeaderText = "면적" + Environment.NewLine + "[m²]";
            dataGridView1.Columns[8].HeaderText = "방위" + Environment.NewLine + " - ";
            dataGridView1.Columns[9].HeaderText = "기울기" + Environment.NewLine + "[°]";
        }

        private String _fixed(string v)
        {
            return (v == "0" ? "0" : Double.Parse(v).ToString("#.##"));
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "존번호,바닥면적,주향,주광너비,주광깊이,상인방높이");

                while (++i < rec.Length)
                {
                    dataGridView2.Rows.Add(null, rec[i][0], _fixed(rec[i][1]), rec[i][2], _fixed(rec[i][3]), _fixed(rec[i][4]), _fixed(rec[i][5]));
                }
            }
            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,구조체");

                while (++i < rec.Length)
                {
                    dataGridView1.Rows.Add(null, rec[i][0], rec[i][1], rec[i][2],rec[i][3], rec[i][4], rec[i][6], _fixed(rec[i][5]), rec[i][7], _fixed(rec[i][8]), rec[i][9]);
                }
            }
        }
    }
}
