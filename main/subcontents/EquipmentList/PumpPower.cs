using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.EquipmentList
{
    public partial class PumpPower : Form
    {
        double Qmax, Volume; public double Power;
        double L, B, hG, nG;
        double Lmax, PumpHead;
        double eta;

        public PumpPower(String PumpNum)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            PumpNum_textBox.Text = PumpNum;
            pictureBox1.Load(Program.gPath + "images/HeatingSystem/PumpArea.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Qmax_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Qmax_textBox.Text != null)
            {
                Qmax = Program.UTIL.textBox_doubleComa(Qmax_textBox, false, 1);
                Volume = Qmax * 3.6 / (5 * 4.18);
                Volume_textBox.Text = Volume.ToString("#,0.#") + " CMH";
                Clac_Power();
            }
        }
        private void eta_textBox_TextChanged(object sender, EventArgs e)
        {
            if (eta_textBox.Text != null)
            {
                eta = Program.UTIL.ToDoubleOrZero(eta_textBox.Text);
                Calc_head();
            }

        }
        private void L_textBox_TextChanged(object sender, EventArgs e)
        {
            if (L_textBox.Text != null)
            {
                L = Program.UTIL.ToDoubleOrZero(L_textBox.Text);
                Calc_head();
            }
        }

        private void B_textBox_TextChanged(object sender, EventArgs e)
        {
            if (B_textBox.Text != null)
            {
                B = Program.UTIL.ToDoubleOrZero(B_textBox.Text);
                Calc_head();
            }
        }

        private void hG_textBox_TextChanged(object sender, EventArgs e)
        {
            if (hG_textBox.Text != null)
            {
                hG = Program.UTIL.ToDoubleOrZero(hG_textBox.Text);
                Calc_head();
            }
        }

        private void nG_textBox_TextChanged(object sender, EventArgs e)
        {
            if (nG_textBox.Text != null)
            {
                nG = Program.UTIL.ToDoubleOrZero(nG_textBox.Text);
                Calc_head();
            }
        }
        private void Calc_head()
        {
            if (L != 0 && B != 0 && hG != 0 && nG != 0)
            {
                Lmax = 2 * (L + B / 2 + hG * nG + 10);
                PumpHead = Lmax * 0.25 * (1 + 0.3);
            }
            if (Lmax > 0 && PumpHead > 0)
            {
                Lmax_textBox.Text = string.Format("{0:F1}", Lmax) + " m";
                PumpHead_textBox.Text = string.Format("{0:F1}", PumpHead) + " m";
                Clac_Power();
            }
        }

        private void Clac_Power()
        {
            if (PumpHead > 0 && Volume > 0)
            {
                Power = (PumpHead * 1000 * 9.81) * Volume / 3600 / (eta / 100);
                PumpPower_textBox.Text = Power.ToString();
                Program.UTIL.textBox_doubleComa(PumpPower_textBox, true, 1);
                PumpPower_textBox.Text += " W";
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Power > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("펌프 동력을 계산 후 저장하세요.");
            }
        }

    }
}
