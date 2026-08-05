using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.CoolingSystem
{
    public partial class Pipe_Diameter : Form
    {
        public double _tempDiffer, _ceNumber;
        public Pipe_Diameter()
        {
            InitializeComponent();
            this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (check() == false)
            {
                return;
            }
            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private bool check()
        {
            if (!double.TryParse(tempDiffer_textBox.Text?.ToString(), out _tempDiffer))
            {
                MessageBox.Show("냉수 온도차를 입력해 주세요");
                return false;
            }
            if (!double.TryParse(ceNumber_textBox.Text?.ToString(), out _ceNumber))
            {
                MessageBox.Show("공급설비 개수를 입력해 주세요");
                return false;
            }
            return true;
        }
    }
}
