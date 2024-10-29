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
    public partial class SupplyRatio : Form
    {
        public SupplyRatio()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
            MessageBox.Show("공급의무비율 화면은 아직 작업 중입니다.");
        }
    }
}
