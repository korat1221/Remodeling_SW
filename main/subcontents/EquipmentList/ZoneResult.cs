using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common; 
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;


namespace main.subcontents
{
    public partial class ZoneResult : Form
    {
        public ZoneResult()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(ZoneResult_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            ZoneResult_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            ZoneResult_dataGridView.Columns.Add(checkBoxColumn);

            ZoneResult_dataGridView.Columns.Add("A1", "번호");
            ZoneResult_dataGridView.Columns.Add("A2", "층");
            ZoneResult_dataGridView.Columns.Add("A3", "존명칭");
            ZoneResult_dataGridView.Columns.Add("A4", "용도프로필");
            ZoneResult_dataGridView.Columns.Add("A5", "면적.[m" + Program.UTIL.Subscript(2, true) + "]");
            ZoneResult_dataGridView.Columns.Add("A6", "연간 요구량.난방.[kWh/a]");
            ZoneResult_dataGridView.Columns.Add("A7", "연간 요구량.냉방.[kWh/a]");
            ZoneResult_dataGridView.Columns.Add("A8", "연간 요구량.급탕.[kWh/a]");
            ZoneResult_dataGridView.Columns.Add("A9", "최대부하.난방.[kW]");
            ZoneResult_dataGridView.Columns.Add("A10", "최대부하.냉방.[kW]");
            ZoneResult_dataGridView.Columns.Add("A11", "최대부하.급탕.[kW]");

            ZoneResult_dataGridView.Columns[0].Width = 40;
            ZoneResult_dataGridView.Columns[2].Width = 60;

            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존번호,존이름,용도프로필,순바닥면적,일일급탕요구량 from ZoneGeneral_Form where Not 냉난방유무 ='비냉난방'  Order by  존번호");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 ='" + Value[n][0] + "'");


                    int nRow = ZoneResult_dataGridView.Rows.Add();
                    ZoneResult_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    if (층.Length > 0)
                    {
                        ZoneResult_dataGridView.Rows[nRow].Cells[2].Value = 층[0][0];
                    }
                    ZoneResult_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1];
                    ZoneResult_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2];
                    ZoneResult_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));


                    string[][] 난방부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '난방'");
                    if (난방부하.Length > 0)
                    {
                        ZoneResult_dataGridView.Rows[nRow].Cells[6].Value = Convert.ToDouble(난방부하[0][0]).ToString("0");
                        ZoneResult_dataGridView.Rows[nRow].Cells[9].Value = (Convert.ToDouble(난방부하[0][1]) / 1000).ToString("0.00");
                    }
                    string[][] 냉방부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '냉방'");
                    if (냉방부하.Length > 0)
                    {
                        ZoneResult_dataGridView.Rows[nRow].Cells[7].Value = Convert.ToDouble(냉방부하[0][0]).ToString("0");
                        ZoneResult_dataGridView.Rows[nRow].Cells[10].Value = (Convert.ToDouble(냉방부하[0][1]) / 1000).ToString("0.00");
                    }
                    double Qwb_day = 0, dop_a = 0; double[] theta_e = new double[12]; double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                    if (Value[n][4] != "")
                    {
                        Qwb_day = Convert.ToDouble(Value[n][4]);
                    }
                    for (int mth = 0; mth < 12; mth++)
                    {
                        string[][] 급탕부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "theta_e, dwd_mth", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '난방' and 비이용일_이용일='이용일' and 월='" + (mth + 1) + "월'");
                        theta_e[mth] = Convert.ToDouble(급탕부하[0][0]);
                        dop_a += Convert.ToDouble(급탕부하[0][1]);
                    }
                    double[] Qwb_mth = new double[12];
                    double Qwb_a = 0;
                    for (int mth = 0; mth < 12; mth++)
                    {
                        Qwb_mth[mth] = Qwb_day * dop_a * dmth[mth] / 365 * (-0.02 * theta_e[mth] + 1.25);
                        Qwb_a += Qwb_mth[mth];
                    }
                    ZoneResult_dataGridView.Rows[nRow].Cells[8].Value = Qwb_a.ToString("0");
                    string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + Value[n][2] + "'");
                    if (Usage.Length > 0)
                    { ZoneResult_dataGridView.Rows[nRow].Cells[11].Value = (Qwb_day * Convert.ToDouble(Usage[0][0])).ToString("0.00"); ; }

                }
            }
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }

        private void SelectCalc()
        {
            double Qhb = 0; double Qcb = 0; double Qwb = 0; double Qhmax = 0; double Qcmax = 0; double Qwmax = 0;
            foreach (DataGridViewRow row in ZoneResult_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    if (row.Cells[6].Value != null)
                    { Qhb += Convert.ToDouble(row.Cells[6].Value.ToString()); }
                    if (row.Cells[7].Value != null)
                    { Qcb += Convert.ToDouble(row.Cells[7].Value.ToString()); }
                    if (row.Cells[8].Value != null)
                    { Qwb += Convert.ToDouble(row.Cells[8].Value.ToString()); }
                    if (row.Cells[9].Value != null)
                    { Qhmax += Convert.ToDouble(row.Cells[9].Value.ToString()); }
                    if (row.Cells[10].Value != null)
                    { Qcmax += Convert.ToDouble(row.Cells[10].Value.ToString()); }
                    if (row.Cells[11].Value != null)
                    { Qwmax += Convert.ToDouble(row.Cells[11].Value.ToString()); }
                }
            }
            if (Qhb > 0)
            {
                Qhb_textBox.Text = Qhb.ToString();
                Program.UTIL.textBox_doubleComa(Qhb_textBox, true, 0);
            }
            if (Qcb > 0)
            {
                Qcb_textBox.Text = Qcb.ToString();
                Program.UTIL.textBox_doubleComa(Qcb_textBox, true, 0);
            }
            if (Qwb > 0)
            {
                Qwb_textBox.Text = Qwb.ToString();
                Program.UTIL.textBox_doubleComa(Qwb_textBox, true, 0);
            }
            if (Qhmax > 0)
            {
                Qhmax_textBox.Text = Qhmax.ToString();
                Program.UTIL.textBox_doubleComa(Qhmax_textBox, true, 2);
            }
            if (Qcmax > 0)
            {
                Qcmax_textBox.Text = Qcmax.ToString();
                Program.UTIL.textBox_doubleComa(Qcmax_textBox, true, 2);
            }
            if (Qwmax > 0)
            {
                Qwmax_textBox.Text = Qwmax.ToString();
                Program.UTIL.textBox_doubleComa(Qwmax_textBox, true, 2);
            }
        }

        private void Qhmax_Copy_button_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Qhmax_textBox.Text.ToString());
            MessageBox.Show("최대 난방 부하 값이 복사되었습니다.");
        }

        private void Qcmax_Copy_button_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Qcmax_textBox.Text.ToString());
            MessageBox.Show("최대 냉방 부하 값이 복사되었습니다.");
        }

        private void Qwmax_Copy_button_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Qwmax_textBox.Text.ToString());
            MessageBox.Show("최대 급탕 부하 값이 복사되었습니다.");
        }
        private void ZoneResult_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                SelectCalc();
            }
        }

    }
}
