using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CustomComboBox;
using static main.MainContents;

namespace main.contents
{
    public partial class sub3dBridgeInfo : Form
    {
        string sid = "";
        public sub3dBridgeInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;

                if (main.MainContents.selID.IndexOf("bridge-") >= 0)
                {
                    Dictionary<string, string> bridges = new Dictionary<string, string>()
                    {
                        {"RTB1", "평지붕+외벽[90]"},
                        {"RTB2", "평지붕+외벽[270]"},
                        {"RTB3", "평지붕+내벽"},
                        {"RTB4", "경사지붕"},
                        {"RTB5", "경사지붕+외벽[수평]"},
                        {"RTB6", "경사지붕+외벽[경사]"},
                        {"WTB1", "층간슬라브+외벽"},
                        {"WTB2", "외벽+내벽"},
                        {"WTB3", "외벽+외벽[90]"},
                        {"WTB4", "외벽+외벽[270]"},
                        {"WTB5", "바닥+외벽[90]"},
                        {"WTB6", "바닥+외벽[270]"},
                    };

                    int ID = Int32.Parse(main.MainContents.selID.Replace("bridge-", ""));
                    string num = ID > 6 ? ("WTB" + (ID - 6)) : "RTB" + ID;

                    string[][] rec = Program.DB.querySQL(DB.type.ProjDB, "SELECT 열교항목,SUM(열교길이) FROM ThermalBridge_3D WHERE 번호='" + num + "' GROUP BY 번호");

                    if (rec.Length > 0)
                    {
                        label72.Text = rec[0][0];
                        textBox23.Text = (rec[0][1] == "0.00" ? "0" : Double.Parse(rec[0][1]).ToString("#.##"));
                    }
                    else
                    {
                        label72.Text = bridges[num];
                        textBox23.Text = "0";
                    }

                    this.panel1.Hide();
                    this.panel2.Show();
                }
                else
                {
                    int i = -1;
                    Label[] labels = new Label[11] { label1, label6, label9, label12, label15, label18, label21, label24, label36, label33, label30 };
                    TextBox[] textboxes = new TextBox[11] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox12, textBox11, textBox10 };
                    string[][] rec = Program.DB.querySQL(DB.type.ProjDB, "SELECT 열교항목,SUM(열교길이) FROM ThermalBridge_3D GROUP BY 번호");

                    while (++i < rec.Length)
                    {
                        labels[i].Text = rec[i][0];
                        textboxes[i].Text = (rec[i][1] == "0.00" ? "0" : Double.Parse(rec[i][1]).ToString("#.##"));
                    }
                    this.panel1.Show();
                    this.panel2.Hide();
                }
            }
        }
    }
}
