using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class ZoneEnvelope : Form
    {
        String 선택구조체;

        public ZoneEnvelope()
        {
            InitializeComponent();
            load_table_ZoneEnvelopeNum();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        void load_table_ZoneEnvelopeNum()
        {
            DataTable table_ZoneEnvelopeNum = new DataTable();
            table_ZoneEnvelopeNum.Columns.Add("구조체", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("개수", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("A[m2]", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("Ueff[W/m2K]", typeof(string));


            //존별 구조체의 개수정보 불러오기 
            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\ZoneSample\ZoneSample.db";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);

                String query = "SELECT * fROM 존외피개수정보";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    table_ZoneEnvelopeNum.Rows.Add(rdr["구조체"], rdr["개수"], rdr["A"], rdr["Ueff"]);
                }
            }
            catch (Exception ex) { }

            dataGridView1.DataSource = table_ZoneEnvelopeNum;

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                선택구조체 = row.Cells["구조체"].Value.ToString();
                load_table_ZoneEnvelopeSelect(선택구조체);
            }

        }

        void load_table_ZoneEnvelopeSelect(String 선택구조체)
        {
            DataTable table_ZoneEnvelopeSelect = new DataTable();
            table_ZoneEnvelopeSelect.Columns.Add("번호", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("기호", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("구조체종류", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("A[m2]", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("방위", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("기울기[˚]", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("Ueff[W/m2K]", typeof(string));
            if(선택구조체=="커튼월창" || 선택구조체=="창호")
            table_ZoneEnvelopeSelect.Columns.Add("g", typeof(string));
            else { table_ZoneEnvelopeSelect.Columns.Add("α", typeof(string)); }




            //존별 구조체의 개수정보 불러오기 
            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\ZoneSample\ZoneSample.db";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);

                String query = "SELECT * fROM " + 선택구조체;
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    table_ZoneEnvelopeSelect.Rows.Add(rdr["번호"], rdr["기호"], rdr["구조체종류"], rdr["A"], rdr["방위"], rdr["기울기"], rdr["Ueff"], rdr["일사"]);
                }
            }
            catch (Exception ex) { }

            dataGridView2.DataSource = table_ZoneEnvelopeSelect;

        }






    }
}
