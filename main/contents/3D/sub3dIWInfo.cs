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
    public partial class sub3dIWInfo : Form
    {
        double Area;
        string sid = "";


        public sub3dIWInfo()
        {
            
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                String key = sid.IndexOf("F_Zone") > 0 ? "번호" : "아이디";
                String ID = main.MainContents.selID.Replace("board-", "");
                string[][] value1 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체번호,면적,번호,인접존,방위", key + " = '" + ID + "'");

                if (value1.Length > 0)
                {
                    Name_textBox.Text = value1[0][2];
                    //Name_textBox1.Text= value1[0][0];  ---> 구조체 번호는 없지..
                    //di_textBox.Text = value1[0][4];
                    Area = Convert.ToDouble(value1[0][1]);
                    Area_textBox.Text = string.Format("{0:F2}", Area);
                    near_textBox.Text = value1[0][3];
                }
            }
        }
    }
}
