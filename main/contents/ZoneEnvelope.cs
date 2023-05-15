using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ZoneEnvelope : Form
    {
        String SelectZone;

        String[] ConstructionType = { "커튼월창", "외벽", "지붕", "최하층바닥", "창호", "외부출입문", "내벽", "층간바닥" };
        String 선택구조체, 해당존유형;
        string[][] ZoneE = new String[8][];
        double Area_Celing, Area_Wall, Area_InWall, Area_Slab;
        double Cwirk_Celing, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab;
        double Cwirk_total;

        public ZoneEnvelope()
        {
            InitializeComponent();
            Zone_comboBox.Items.Add("1F_Zone02");
            Zone_comboBox.Items.Add("1F_Zone04");
            load_table_ZoneEnvelopeImport();


            //축열관련 콤보박스 만들기
            //천장
            string[][] SQL_index_Celing = Program.DB.getValue(DB.type.BaseDB, "축열", "축열유형", "구조체 = '천장'");
            int i = -1;
            while (++i < SQL_index_Celing.Length)
            {
                CeilingCwirk_comboBox.Items.Add(SQL_index_Celing[i][0]);
            }
            //외벽
            string[][] SQL_index_Wall = Program.DB.getValue(DB.type.BaseDB, "축열", "축열유형", "구조체 = '외벽'");
            i = -1;
            while (++i < SQL_index_Wall.Length)
            {
                WallCwirk_comboBox.Items.Add(SQL_index_Wall[i][0]);
            }
            //내벽
            string[][] SQL_index_InWall = Program.DB.getValue(DB.type.BaseDB, "축열", "축열유형", "구조체 = '간벽'");
            i = -1;
            while (++i < SQL_index_InWall.Length)
            {
                InWallCwirk_comboBox.Items.Add(SQL_index_InWall[i][0]);
            }
            //바닥
            string[][] SQL_index_Slab = Program.DB.getValue(DB.type.BaseDB, "축열", "축열유형", "구조체 = '바닥'");
            i = -1;
            while (++i < SQL_index_Slab.Length)
            {
                SlabCwirk_comboBox.Items.Add(SQL_index_Slab[i][0]);
            }

            //기밀관련 콤보박스 만들기
            InfiltrationType_comboBox.Items.Add("표준값");
            InfiltrationType_comboBox.Items.Add("기밀설계보고서");
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        //임시로 존번호 선택하도록 함 > 추후 생성자로 복붙 필요 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectZone = Zone_comboBox.SelectedItem.ToString();
            load_table_ZoneEnvelopeInfo(SelectZone);
            Calc_A(SelectZone);
            해당존유형 = Calc_ZoneType();
            Check_radioButton(해당존유형);
        }

        //천장 축열정보 선택 시 
        private void CeilingCwrik_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double CwirkA;
            string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB, "축열", "Cwirk", "구조체='천장' AND 축열유형='" + CeilingCwirk_comboBox.SelectedItem.ToString() + "'");
            CwirkA = Convert.ToDouble(CwirkDB[0][0]);
            Cwirk_Celing = Calc_Cwirk_Construction(Area_Celing, CwirkA);
            Cwirk_total = Calc_Cwirk(Cwirk_Celing, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);
        }

        private void WallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double CwirkA;
            string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB, "축열", "Cwirk", "구조체='외벽' AND 축열유형='" + WallCwirk_comboBox.SelectedItem.ToString() + "'");
            CwirkA = Convert.ToDouble(CwirkDB[0][0]);
            Cwirk_Wall = Calc_Cwirk_Construction(Area_Wall, CwirkA);
            Cwirk_total = Calc_Cwirk(Cwirk_Celing, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);
        }

        private void InWallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double CwirkA;
            string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB, "축열", "Cwirk", "구조체='간벽' AND 축열유형='" + InWallCwirk_comboBox.SelectedItem.ToString() + "'");
            CwirkA = Convert.ToDouble(CwirkDB[0][0]);
            Cwirk_InWall = Calc_Cwirk_Construction(Area_InWall, CwirkA);
            Cwirk_total = Calc_Cwirk(Cwirk_Celing, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);
        }

        private void SlabCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double CwirkA;
            string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB, "축열", "Cwirk", "구조체='바닥' AND 축열유형='" + SlabCwirk_comboBox.SelectedItem.ToString() + "'");
            CwirkA = Convert.ToDouble(CwirkDB[0][0]);
            Cwirk_Slab = Calc_Cwirk_Construction(Area_Slab, CwirkA);
            Cwirk_total = Calc_Cwirk(Cwirk_Celing, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);
        }

        //기밀적용유형 선택 시 
        private void InfiltrationType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            double q = Calc_q50(해당존유형);
            double n = Calc_n50(q);
            q50_textBox.Text = string.Format("{0:F0}", q);
            n50_textBox.Text = string.Format("{0:F1}", n);
        }

        //csv에서 해당 존의 외피정보 불러와서 저장하기
        void load_table_ZoneEnvelopeImport()
        {
            try
            {
                string filePath = Program.gPath + "ZoneSample\\건물모델링.csv";
                using (FileStream fileReader = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fileReader, Encoding.UTF8, false))
                    {
                        int n = 0;
                        while (!sr.EndOfStream)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                Program.DB.setValue(DB.type.CalcDB, "ZoneEnvelope", "번호,기호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출,좌측면돌출,상부돌출,주변요소,구조체,Ueff,α,g,직접간접",
                                 "'" + token[0] + "','" + token[1] + "','" + token[2] + "','" + token[3] + "','" + token[4] + "','" + token[5] + "','"
                             + token[6] + "','" + token[7] + "','" + token[8] + "','" + token[9] + "','" + token[10] + "','"
                             + token[11] + "','" + token[12] + "','" + token[13] + "','" + token[14] + "','" + token[15] + "','"
                             + token[16] + "','" + token[17] + "','" + token[18] + "'", "존,기호");

                            }
                            n++;
                        }

                    }
                }
            }
            catch (IOException e) { }

        }

        //존외피 왼쪽테이블 정보 만들기 
        void load_table_ZoneEnvelopeInfo(String SelectZone)
        {
            DataTable table_ZoneEnvelopeNum = new DataTable();
            // 체크박스 추가
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            // 컬럼 추가
            table_ZoneEnvelopeNum.Columns.Add("구조체", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("개수", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("A" + Environment.NewLine + "[m2]", typeof(string));
            table_ZoneEnvelopeNum.Columns.Add("Ueff" + Environment.NewLine + "[W/m2K]", typeof(string));

            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneEnvelope", "번호,기호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출,좌측면돌출,상부돌출,주변요소,구조체,Ueff,α,g", "존='" + SelectZone + "'");

            int[] Construction_Count = new int[8]; double[] Construction_AreaSum = new double[8]; double[] Construction_UeffAvg = new double[8]; double[] Construction_UeffSum = new double[8];

            int i = -1;
            while (++i < ZoneE.Length)
            {
                for (int k = 0; k < ConstructionType.Length; k++)
                {
                    if (ZoneE[i][4] == ConstructionType[k])
                    {
                        Construction_Count[k] = Construction_Count[k] + 1;
                        Construction_AreaSum[k] += Convert.ToDouble(ZoneE[i][6]);
                        Construction_UeffSum[k] += (Convert.ToDouble(ZoneE[i][15]) * Convert.ToDouble(ZoneE[i][6]));
                        Construction_UeffAvg[k] = Construction_UeffSum[k] / Construction_AreaSum[k]; //면적가중평균

                    }
                }
            }

            //존별 구조체별 종합정보 테이블 만들기
            for (int k = 0; k < ConstructionType.Length; k++)
            {
                table_ZoneEnvelopeNum.Rows.Add(ConstructionType[k], Construction_Count[k], string.Format("{0:F2}", Construction_AreaSum[k]), string.Format("{0:F2}", Construction_UeffAvg[k]));
            }
            dataGridView1.DataSource = table_ZoneEnvelopeNum;
            for (int k = 0; k < ConstructionType.Length; k++)
            {
                Construction_Count[k] = 0; Construction_AreaSum[k] = 0; Construction_UeffAvg[k] = 0; Construction_UeffSum[k] = 0;
            }

        }

        // 왼쪽테이블 중 체크박스 선택 시 오른쪽 테이블 생성됨 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewRow row2 = dataGridView1.Rows[e.RowIndex];
                선택구조체 = row.Cells["구조체"].Value.ToString();
                load_table_ZoneEnvelopeSelect(선택구조체);
                for (int k = 0; k < ConstructionType.Length; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView1.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = dataGridView1.Rows[e.RowIndex];
                    }

                }
            }
        }


        // 존외피 오른쪽테이블 정보 만들기 : 선택된 구조체에 대한 정보 
        void load_table_ZoneEnvelopeSelect(String 선택구조체)
        {
            DataTable table_ZoneEnvelopeSelect = new DataTable();
            table_ZoneEnvelopeSelect.Columns.Add("번호", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("기호", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("구조체종류", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("A" + Environment.NewLine + "[m2]", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("방위", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("기울기" + Environment.NewLine + "[˚]", typeof(string));
            table_ZoneEnvelopeSelect.Columns.Add("Ueff" + Environment.NewLine + "[W/m2K]", typeof(string));
            if (선택구조체 == "커튼월창" || 선택구조체 == "창호")
                table_ZoneEnvelopeSelect.Columns.Add("g", typeof(string));
            else { table_ZoneEnvelopeSelect.Columns.Add("α", typeof(string)); }

            //존별, 선택 구조체의 정보 불러오기 
            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneEnvelope", "번호,기호,외피유형,면적,방위,기울기,구조체,Ueff,α,g", "존='" + SelectZone + "' AND 외피유형='" + 선택구조체 + "'");


            for (int n = 0; n < ZoneE.Length; n++)
            {
                String 면적 = string.Format("{0:F2}", Convert.ToDouble(ZoneE[n][3]));
                String Ueff = string.Format("{0:F2}", Convert.ToDouble(ZoneE[n][7]));

                if (선택구조체 == "커튼월창" || 선택구조체 == "창호")
                {
                    String g = string.Format("{0:F2}", Convert.ToDouble(ZoneE[n][9]));
                    table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE[n][1], ZoneE[n][6], 면적, ZoneE[n][4], ZoneE[n][5], Ueff, g);
                }
                else
                {
                    String α;
                    try { α = string.Format("{0:F2}", Convert.ToDouble(ZoneE[n][8])); }
                    catch (Exception ex) { α = "-"; }
                    table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE[n][1], ZoneE[n][6], 면적, ZoneE[n][4], ZoneE[n][5], Ueff, α);
                }
            }
            dataGridView2.DataSource = table_ZoneEnvelopeSelect;
        }

        //축열 계산을 위한 면적 계산 
        void Calc_A(String SelectZone)
        {
            //외피별 면적 정보 불러오기
            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneEnvelope", "번호,기호,층,존,외피유형,면적", "존='" + SelectZone + "'");

            int[] Construction_Count = new int[8]; double[] Construction_AreaSum = new double[8];
            int i = -1;
            while (++i < ZoneE.Length)
            {
                for (int k = 0; k < ConstructionType.Length; k++)
                {
                    if (ZoneE[i][4] == ConstructionType[k])
                    {
                        Construction_Count[k] = Construction_Count[k] + 1;
                        Construction_AreaSum[k] += Convert.ToDouble(ZoneE[i][5]);
                    }
                }
            }
            if (Construction_AreaSum[2] == 0) //최상층 아닐 경우 천장 면적
            {
                Area_Celing = Construction_AreaSum[7];
            }
            else //최상층일 경우 천장 면적
            {
                Area_Celing = Construction_AreaSum[2];
            }

            Area_Wall = Construction_AreaSum[1]; //외벽 면적
            Area_InWall = Construction_AreaSum[6]; //내벽 면적

            if (Construction_AreaSum[3] == 0) //최하층 아닐 경우 바닥 면적
            {
                Area_Slab = Construction_AreaSum[7];
            }
            else //최하층일 경우 내벽 면적 
            {
                Area_Slab = Construction_AreaSum[3];
            }
        }

        //구조체별 cwirk계산 
        double Calc_Cwirk_Construction(double A, double CwirkA)
        {
            double Cwirk = A * CwirkA;

            return Cwirk;
        }

        //존의 cwirk/A 계산 
        double Calc_Cwirk(double Cwirk_Celing, double Cwirk_Wall, double Cwirk_InWall, double Cwirk_Slab)
        {
            double Cwirk = (Cwirk_Celing + Cwirk_Wall + Cwirk_InWall + Cwirk_Slab) / Area_Slab; //원래 순바닥면적으로 해야하는데 지금은 그냥 층간바닥면적으로 함  

            return Cwirk;
        }

        //존의 외부,내부,출입문 존 검토 
        String Calc_ZoneType()
        {
            String 존유형;

            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneEnvelope", "존,외피유형", "존='" + SelectZone + "'");
            int[] Construction_Count = new int[8];
            int i = -1;
            while (++i < ZoneE.Length)
            {
                for (int k = 0; k < ConstructionType.Length; k++)
                {
                    if (ZoneE[i][1] == ConstructionType[k])
                    {
                        Construction_Count[k] = Construction_Count[k] + 1;

                    }
                }
            }

            if (Construction_Count[5] > 0)
            {
                존유형 = "출입문존";
            }
            else if ((Construction_Count[0] + Construction_Count[1] + Construction_Count[2] + Construction_Count[3] + Construction_Count[4] + Construction_Count[5]) > 0)
            {
                존유형 = "외부존";
            }
            else
            {
                존유형 = "내부존";
            }
            return 존유형;
        }

        //존유형에 따라 라디오버튼 선택
        void Check_radioButton(String 해당존유형)
        {
            if (해당존유형 == "내부존")
            {
                InternalZone_radioButton.Checked = true;
            }
            else if (해당존유형 == "출입문존")
            {
                DoorZone_radioButton.Checked = true;
            }
            else
            {
                ExternalZone_radioButton.Checked = true;
            }
        }
        double Calc_q50(string 해당존유형)
        {
            double q50;
            string[][] InfiltrationDB = Program.DB.getValue(DB.type.BaseDB, "기밀", "q50", "존유형='" + 해당존유형 + "' AND 기밀적용유형='" + InfiltrationType_comboBox.SelectedItem.ToString() + "'");
            q50 = Convert.ToDouble(InfiltrationDB[0][0]);
            return q50;
        }
        double Calc_n50(double q50)
        {
            double n50 = 0;
            double AreaDirect_total = 0;

            //외피별, 직접외기 면적 정보 불러오기
            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneEnvelope", "번호,기호,층,존,외피유형,면적,직접간접", "존='" + SelectZone + "' AND 직접간접='직접외기'");
            int i = -1;
            while (++i < ZoneE.Length)
            { AreaDirect_total += Convert.ToDouble(ZoneE[i][5]); }

            n50 = AreaDirect_total * q50 / (Area_Slab * 2.5); //원래 순체적으로 해야하는데 지금은 그냥 임의로 계산로 함  
            MessageBox.Show(n50.ToString());

            return n50;

        }
    }
}
