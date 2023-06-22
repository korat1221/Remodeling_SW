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
    public partial class Model : Form
    {
        public Model()
        {
            InitializeComponent();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
        void load_table_SpacerDB()
        {
            DataTable table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            table.Columns.Add("번호", typeof(string));
            table.Columns.Add("층", typeof(string));
            table.Columns.Add("존", typeof(string));
            table.Columns.Add("외피유형", typeof(string));
            table.Columns.Add("커튼월구분", typeof(string));
            table.Columns.Add("면적" + Environment.NewLine + "[m²]", typeof(string));
            table.Columns.Add("방위" + Environment.NewLine + "-", typeof(string));
            table.Columns.Add("기울기" + Environment.NewLine + "[°]", typeof(string));
            table.Columns.Add("구조체선택", typeof(string));

        }

    }
}
