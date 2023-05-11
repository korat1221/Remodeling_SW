using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class ConstructionRoof : Form
    {
        Thermal1D thermalForm = new Thermal1D();
        public ConstructionRoof()
        {
            InitializeComponent();
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form1 openForm in Application.OpenForms)
            {
                if (openForm.Name == "Form1")
                {
                    thermalForm.Show();
                    return;
                }
            }
        }
    }
}
