using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.MainContents;

namespace main.contents
{
    public partial class sub3dBridgeInfo : Form
    {
        public sub3dBridgeInfo()
        {
            InitializeComponent();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID.IndexOf("bridge-") >= 0)
            {
                int ID = Int32.Parse(main.MainContents.selID.Replace("bridge-", ""));

                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "열교항목,열교길이", "ID=" + ID);

                if (rec.Length > 0)
                {
                    label72.Text = rec[0][0];
                    textBox23.Text = (rec[0][1] == "0.00" ? "0" : Double.Parse(rec[0][1]).ToString("#.##"));
                }

                this.panel1.Hide();
                this.panel2.Show();
            }
            else
            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "열교항목,열교길이", "열교항목<>'---'");
                Label[] labels = new Label[11] { label1, label6, label9, label12, label15, label18, label21, label24, label36, label33, label30 };
                TextBox[] textboxes = new TextBox[11] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox12, textBox11, textBox10 };

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
