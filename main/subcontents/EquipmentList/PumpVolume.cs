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
    public partial class PumpVolume : Form
    {
        public double Volume;
        public PumpVolume(string num)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(PumpVolume_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            PumpVolume_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PumpVolume_dataGridView.Columns.Add(checkBoxColumn);

            PumpVolume_dataGridView.Columns.Add("A1", "번호");
            PumpVolume_dataGridView.Columns.Add("A2", "층");
            PumpVolume_dataGridView.Columns.Add("A3", "존명칭");
            PumpVolume_dataGridView.Columns.Add("A4", "용도프로필");
            PumpVolume_dataGridView.Columns.Add("A5", "면적.[m" + Program.UTIL.Subscript(2, true) + "]");
            PumpVolume_dataGridView.Columns.Add("A6", "연간 요구량.급탕.[kWh/a]");
            PumpVolume_dataGridView.Columns.Add("A7", "최대부하.급탕.[kW]");

            PumpVolume_dataGridView.Columns[0].Width = 40;
            PumpVolume_dataGridView.Columns[2].Width = 60;

            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 존번호,존이름,용도프로필,순바닥면적,일일급탕요구량 from ZoneGeneral_Form where Not 냉난방유무 ='비냉난방'  Order by  존번호");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 ='" + Value[n][0] + "'");


                    int nRow = PumpVolume_dataGridView.Rows.Add();
                    PumpVolume_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    if (층.Length > 0)
                    {
                        PumpVolume_dataGridView.Rows[nRow].Cells[2].Value = 층[0][0];
                    }
                    PumpVolume_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1];
                    PumpVolume_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2];
                    PumpVolume_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));


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
                    PumpVolume_dataGridView.Rows[nRow].Cells[6].Value = Qwb_a.ToString("0");
                    string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + Value[n][2] + "'");
                    if (Usage.Length > 0)
                    { PumpVolume_dataGridView.Rows[nRow].Cells[7].Value = (Qwb_day * Convert.ToDouble(Usage[0][0])).ToString("0.00"); ; }

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
            double Qwmax = 0;
            foreach (DataGridViewRow row in PumpVolume_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    if (row.Cells[7].Value != null)
                    { Qwmax += Convert.ToDouble(row.Cells[7].Value.ToString()); }
                }
            }
            if (Qwmax > 0)
            {
                Qwmax_textBox.Text = Qwmax.ToString();
                Program.UTIL.textBox_doubleComa(Qwmax_textBox, true, 2);
            }
            if (Qwmax > 0)
            {
                double Volume = Qwmax * 3.6 / (4.18 * 5) ; // m3/h
                PumpVolume_textBox.Text = Volume.ToString();
                Program.UTIL.textBox_doubleComa(PumpVolume_textBox, true, 1);
            }
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            this.Volume = Program.UTIL.textBox_doubleComa(PumpVolume_textBox, true, 1);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void PumpVolume_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                SelectCalc();
            }
        }

    }
}
