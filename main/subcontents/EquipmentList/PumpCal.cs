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
    public partial class PumpCal : Form
    {
        double L, B, hG, nG;
        public double Lmax, PumpHead;

        public PumpCal(String PumpNum)
        {
            InitializeComponent();
            PumpNum_textBox.Text = PumpNum;
        }

        private void L_textBox_TextChanged(object sender, EventArgs e)
        {
            if (L_textBox.Text != null)
            {
                L = Convert.ToDouble(L_textBox.Text);
                Calc();
            }
        }

        private void B_textBox_TextChanged(object sender, EventArgs e)
        {
            if (B_textBox.Text != null)
            {
                B = Convert.ToDouble(B_textBox.Text);
                Calc();
            }
        }

        private void hG_textBox_TextChanged(object sender, EventArgs e)
        {
            if (hG_textBox.Text != null)
            {
                hG = Convert.ToDouble(hG_textBox.Text);
                Calc();
            }
        }

        private void nG_textBox_TextChanged(object sender, EventArgs e)
        {
            if (nG_textBox.Text != null)
            {
                nG = Convert.ToDouble(nG_textBox.Text);
                Calc();
            }
        }
        private void Calc()
        {
            if (L != 0 && B != 0 && hG != 0 && nG != 0)
            {
                Lmax = 2 * (L + B / 2 + hG * nG + 10);
                PumpHead = Lmax * 0.25 * (1 + 0.3);
            }
            if (Lmax > 0 && PumpHead > 0)
            {
                Lmax_textBox.Text = string.Format("{0:F1}", Lmax);
                PumpHead_textBox.Text = string.Format("{0:F1}", PumpHead);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
