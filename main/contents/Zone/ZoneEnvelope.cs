using main.contentslist;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.contents
{
    public partial class ZoneEnvelope : Form
    {
        String ZoneNum, ZoneName, Layer;
        static String currentID = "";
        String[] ConstructionType = { "커튼월창", "외벽", "지붕", "최하층바닥", "창호", "외부출입문", "내벽", "층간바닥" };
        String SelectConstruction, ZoneType;
        int[] Construction_Count = new int[8]; double[] Construction_AreaSum = new double[8];
        double[] Construction_UeffAvg = new double[8]; double[] Construction_UeffSum = new double[8];
        double Area_Ceiling, Area_Wall, Area_InWall, Area_Slab;
        double Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab;
        String Ceiling_index, Wall_index, InWall_index, Slab_index;
        double Cwirk_total;
        string[][] ZoneE;
        double NetArea;

        public ZoneEnvelope()
        {

            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 외피정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //축열관련 콤보박스 만들기
            //천장
            string[][] SQL_index_Celing = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '천장'");
            int i = -1;
            while (++i < SQL_index_Celing.Length)
            {
                CeilingCwirk_comboBox.Items.Add(SQL_index_Celing[i][0]);
            }
            //외벽
            string[][] SQL_index_Wall = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '외벽'");
            i = -1;
            while (++i < SQL_index_Wall.Length)
            {
                WallCwirk_comboBox.Items.Add(SQL_index_Wall[i][0]);
            }
            //내벽
            string[][] SQL_index_InWall = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '간벽'");
            i = -1;
            while (++i < SQL_index_InWall.Length)
            {
                InWallCwirk_comboBox.Items.Add(SQL_index_InWall[i][0]);
            }
            //바닥
            string[][] SQL_index_Slab = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '바닥'");
            i = -1;
            while (++i < SQL_index_Slab.Length)
            {
                SlabCwirk_comboBox.Items.Add(SQL_index_Slab[i][0]);
            }
            //기밀관련 콤보박스 만들기
            InfiltrationType_comboBox.Items.Clear();
            InfiltrationType_comboBox.Items.Add("표준값");
            InfiltrationType_comboBox.Items.Add("기밀설계보고서");
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        //천장 축열정보 선택 시 
        private void CeilingCwrik_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CeilingCwirk_comboBox.SelectedItem != null)
            {

                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='천장' AND 축열유형='" + CeilingCwirk_comboBox.SelectedItem.ToString() + "'");
                Ceiling_index = CeilingCwirk_comboBox.SelectedItem.ToString();
                CwirkA = Convert.ToDouble(CwirkDB[0][0]);
                Cwirk_Ceiling = Calc_Cwirk_Construction(Area_Ceiling, CwirkA);
                Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            }
        }

        private void WallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='외벽' AND 축열유형='" + WallCwirk_comboBox.SelectedItem.ToString() + "'");
                Wall_index = WallCwirk_comboBox.SelectedItem.ToString();
                CwirkA = Convert.ToDouble(CwirkDB[0][0]);
                Cwirk_Wall = Calc_Cwirk_Construction(Area_Wall, CwirkA);
                Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            }
        }

        private void InWallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InWallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='간벽' AND 축열유형='" + InWallCwirk_comboBox.SelectedItem.ToString() + "'");
                InWall_index = InWallCwirk_comboBox.SelectedItem.ToString();
                CwirkA = Convert.ToDouble(CwirkDB[0][0]);
                Cwirk_InWall = Calc_Cwirk_Construction(Area_InWall, CwirkA);
                Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            }
        }

        private void SlabCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SlabCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='바닥' AND 축열유형='" + SlabCwirk_comboBox.SelectedItem.ToString() + "'");
                Slab_index = SlabCwirk_comboBox.SelectedItem.ToString();
                CwirkA = Convert.ToDouble(CwirkDB[0][0]);
                Cwirk_Slab = Calc_Cwirk_Construction(Area_Slab, CwirkA);
                Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
            }
        }

        //기밀적용유형 선택 시 
        private void InfiltrationType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InfiltrationType_comboBox.SelectedItem != null)
            {
                double q = Calc_q50(ZoneType);
                double n = Calc_n50(q);
                q50_textBox.Text = string.Format("{0:F1}", q);
                n50_textBox.Text = string.Format("{0:F1}", n);
            }
        }

        //존외피 왼쪽테이블 정보 만들기 
        void load_table_ZoneEnvelopeInfo(String ZoneNum)
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

            for (int k = 0; k < ConstructionType.Length; k++)
            {
                Construction_AreaSum[k] = 0;
                Construction_UeffSum[k] = 0;
            }
            int i = -1;
            while (++i < ZoneE.Length)
            {
                for (int k = 0; k < ConstructionType.Length; k++)
                {
                    if (ZoneE[i][1] == ConstructionType[k])
                    {
                        Construction_Count[k] = Construction_Count[k] + 1;
                        Construction_AreaSum[k] += Convert.ToDouble(ZoneE[i][3]);
                    }
                }
            }

            i = -1;
            while (++i < ZoneE.Length)
            {
                if (ZoneE[i][1] == "커튼월창")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "커튼월창유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[0] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }

                }
                else if (ZoneE[i][1] == "외벽")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[1] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "지붕")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[2] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "최하층바닥")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[3] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "창호")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "창호유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[4] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "외부출입문")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length != 0)
                    { Construction_UeffSum[5] += (Convert.ToDouble(Value[0][0]) * Convert.ToDouble(ZoneE[i][3])); }
                }
                else { }
            }

            for (int k = 0; k < ConstructionType.Length; k++)
            {
                if (Construction_AreaSum[k] == 0)
                {
                    Construction_UeffAvg[k] = 0;
                }
                else
                {
                    Construction_UeffAvg[k] = Construction_UeffSum[k] / Construction_AreaSum[k]; //면적가중평균
                }
            }

            //존별 구조체별 종합정보 테이블 만들기
            for (int k = 0; k < ConstructionType.Length; k++)
            {
                table_ZoneEnvelopeNum.Rows.Add(ConstructionType[k], Construction_Count[k], string.Format("{0:F2}", Construction_AreaSum[k]), string.Format("{0:F2}", Construction_UeffAvg[k]));
            }
            dataGridView1.DataSource = table_ZoneEnvelopeNum;

        }

        // 왼쪽테이블 중 체크박스 선택 시 오른쪽 테이블 생성됨 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewRow row2 = dataGridView1.Rows[e.RowIndex];
                SelectConstruction = row.Cells["구조체"].Value.ToString();
                load_table_ZoneEnvelopeSelect(SelectConstruction);
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


            string[][] Value;

            string[][] ZoneE_Select = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,면적,방위,기울기,구조체,구조체번호", "존='" + ZoneNum + "' AND 외피유형='" + 선택구조체 + "'");

            for (int n = 0; n < ZoneE_Select.Length; n++)
            {
                try
                {
                    if (ZoneE_Select[n][1] == "커튼월창")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "커튼월창유효열관류율,태양열취득률", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else if (ZoneE_Select[n][1] == "외벽")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율,흡수율", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else if (ZoneE_Select[n][1] == "지붕")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "유효열관류율,흡수율", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else if (ZoneE_Select[n][1] == "최하층바닥")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "유효열관류율", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else if (ZoneE_Select[n][1] == "창호")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "창호유효열관류율,상위창호번호", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else if (ZoneE_Select[n][1] == "외부출입문")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율,흡수율", "명칭='" + ZoneE_Select[n][5] + "'");
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionzWall", "유효열관류율,흡수율", "명칭='" + ZoneE_Select[n][5] + "'");
                    }

                    if (Value.Length > 0)
                    {
                        String 면적 = string.Format("{0:F2}", Convert.ToDouble(ZoneE_Select[n][2]));
                        String Ueff = string.Format("{0:F2}", Convert.ToDouble(Value[0][0]));

                        if (선택구조체 == "커튼월창")
                        {
                            String g = string.Format("{0:F2}", Convert.ToDouble(Value[0][1]));
                            table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE_Select[n][1], ZoneE_Select[n][5], 면적, ZoneE_Select[n][3], ZoneE_Select[n][4], Ueff, g);
                        }
                        else if (선택구조체 == "창호")
                        {
                            string[][] gValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "태양열취득률", "번호='" + Value[0][1] + "'");
                            String g = string.Format("{0:F2}", Convert.ToDouble(gValue[0][0]));
                            table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE_Select[n][1], ZoneE_Select[n][5], 면적, ZoneE_Select[n][3], ZoneE_Select[n][4], Ueff, g);
                        }
                        else if (선택구조체 == "외벽" || 선택구조체 == "지붕" || 선택구조체 == "외부출입문")
                        {
                            String α = string.Format("{0:F2}", Convert.ToDouble(Value[0][1]));
                            table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE_Select[n][1], ZoneE_Select[n][5], 면적, ZoneE_Select[n][3], ZoneE_Select[n][4], Ueff, α);
                        }
                        else if (선택구조체 == "최하층바닥")
                        {
                            table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE_Select[n][1], ZoneE_Select[n][5], 면적, ZoneE_Select[n][3], ZoneE_Select[n][4], Ueff, "");
                        }
                    }
                    else { table_ZoneEnvelopeSelect.Rows.Add((n + 1).ToString(), ZoneE_Select[n][1], ZoneE_Select[n][5], string.Format("{0:F2}", Convert.ToDouble(ZoneE_Select[n][2])), ZoneE_Select[n][3], ZoneE_Select[n][4], "", ""); }
                }
                catch { }
            }
            dataGridView2.DataSource = table_ZoneEnvelopeSelect;
        }

        //축열 계산을 위한 면적 계산 
        void Calc_A(String ZoneNum)
        {

            if (Construction_AreaSum[2] == 0) //최상층 아닐 경우 천장 면적
            {
                Area_Ceiling = Construction_AreaSum[7];
            }
            else //최상층일 경우 천장 면적
            {
                Area_Ceiling = Construction_AreaSum[2];
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
        void Calc_Cwirk(double Cwirk_Celing, double Cwirk_Wall, double Cwirk_InWall, double Cwirk_Slab)
        {
            if (Ceiling_index != null && Wall_index != null && InWall_index != null && Slab_index != null && Ceiling_index != String.Empty && Wall_index != String.Empty && InWall_index != String.Empty && Slab_index != String.Empty)
            {
                
                Cwirk_total = (Cwirk_Celing + Cwirk_Wall + Cwirk_InWall + Cwirk_Slab) / NetArea;
                if (Cwirk_total > 150)
                {
                    Cwirk_total = 150;
                }
                Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);
            }
            else
            {
                Cwirk_textBox.Text = "축열성능 모두 선택";
            }
        }

        //존의 외부,내부,출입문 존 검토 
        String Calc_ZoneType()
        {
            String 존유형;

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
            string[][] InfiltrationDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "기밀", "q50", "존유형='" + 해당존유형 + "' AND 기밀적용유형='" + InfiltrationType_comboBox.SelectedItem.ToString() + "'");
            q50 = Convert.ToDouble(InfiltrationDB[0][0]);
            return q50;
        }
        double Calc_n50(double q50)
        {
            double n50 = 0;
            double AreaDirect_total = 0;

            string[][] Value;
            //존의 외피들의 각 구조체테이블에서 직접인지 간접인지 판정해서, 직접이면 면적 합산 
            for (int n = 0; n < ZoneE.Length; n++)
            {
                try
                {
                    if (ZoneE[n][1] == "커튼월창")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "직접간접", "번호='" + ZoneE[n][5] + "'");
                    }
                    else if (ZoneE[n][1] == "외벽")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호='" + ZoneE[n][5] + "'");
                    }
                    else if (ZoneE[n][1] == "지붕")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "직접간접", "번호='" + ZoneE[n][5] + "'");
                    }
                    else if (ZoneE[n][1] == "최하층바닥")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "직접간접", "번호='" + ZoneE[n][5] + "'");
                    }
                    else if (ZoneE[n][1] == "창호")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "직접간접", "번호='" + ZoneE[n][5] + "'");
                    }
                    else if (ZoneE[n][1] == "외부출입문")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "명칭='" + ZoneE[n][5] + "'");
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "명칭='" + ZoneE[n][5] + "'");
                    }

                    if (Value[0][0] == "직접외기")
                    {
                        AreaDirect_total += Convert.ToDouble(ZoneE[n][3]);
                    }
                }
                catch { }

            }


            n50 = AreaDirect_total * q50 / (Area_Slab * 2.5); //원래 순체적으로 해야하는데 지금은 그냥 임의로 계산로 함  

            return n50;

        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            if (CeilingCwirk_comboBox.SelectedItem == null)
            {
                MessageBox.Show("천장 축열 특성을 선택해주세요.");
            }
            else if (WallCwirk_comboBox.SelectedItem == null)
            {
                MessageBox.Show("외벽 축열 특성을 선택해주세요.");
            }
            else if (InWallCwirk_comboBox.SelectedItem == null)
            {
                MessageBox.Show("내벽 축열 특성을 선택해주세요.");
            }
            else if (SlabCwirk_comboBox.SelectedItem == null)
            {
                MessageBox.Show("바닥 축열 특성을 선택해주세요.");
            }
            else if (InfiltrationType_comboBox.SelectedItem == null)
            {
                MessageBox.Show("기밀 성능 적용 방식을 선택해주세요.");
            }
            else
            {
                Save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
        //    List_Zone f = (List_Zone)form;
        //    f.load_List(Layer);
          return true;
        }
        public static bool OnLoadProc(Form form)
        {
        //    ZoneGeneral f = (ZoneGeneral)form;
        //    f.LoadData(currentID);
          return true;
        }
        private void Save()
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,프로젝트유형," +
                "천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택," +
                "천장축열,외벽축열,내벽축열,바닥축열," +
                "천장면적,외벽면적,내벽면적,바닥면적," +
                "존축열성능," +
                "존기밀타입,기밀적용유형," +
                "q50,n50",
            "'" + ZoneNum + "','" + 프로젝트유형[0][0] + "','"
            + Ceiling_index + "','" + Wall_index + "','" + InWall_index + "','" + Slab_index + "','"
            + Cwirk_Ceiling.ToString() + "','" + Cwirk_Wall.ToString() + "','" + Cwirk_InWall.ToString() + "','" + Cwirk_Slab.ToString() + "','"
            + Area_Ceiling.ToString() + "','" + Area_Wall.ToString() + "','" + Area_InWall.ToString() + "','" + Area_Slab.ToString() + "','"
            + Cwirk_total.ToString() + "','"
            + ZoneType + "','" + InfiltrationType_comboBox.SelectedItem.ToString() + "','"
            + q50_textBox.Text.ToString() + "','" + n50_textBox.Text.ToString() + "'", "존번호");

            MessageBox.Show(ZoneNum + "[" + ZoneName + "] 정보를 저장하였습니다.");
            //this.DialogResult = DialogResult.OK;
            //this.Hide();
            //Program.getMenuForm().DoLoadForm(33, OnLoadListProc);
        }
        private void reset()
        {
            ZoneName_textBox.Text = "";
            Layer_textBox.Text = "";

            for (int k = 0; k < 8; k++)
            {
                Construction_Count[k] = 0; Construction_AreaSum[k] = 0; Construction_UeffSum[k] = 0; Construction_UeffAvg[k] = 0;
            }

            Ceiling_index = null;
            CeilingCwirk_comboBox.SelectedItem = null;
            Wall_index = null;
            WallCwirk_comboBox.SelectedItem = null;
            InWall_index = null;
            InWallCwirk_comboBox.SelectedItem = null;
            Slab_index = null;
            SlabCwirk_comboBox.SelectedItem = null;


            Cwirk_Ceiling = 0;
            Cwirk_Wall = 0;
            Cwirk_InWall = 0;
            Cwirk_Slab = 0;
            Area_Ceiling = 0;
            Area_Wall = 0;
            Area_InWall = 0;
            Area_Slab = 0;

            Cwirk_total = 0;
            Cwirk_textBox.Text = "";

            ZoneType = null;

            InfiltrationType_comboBox.SelectedItem = null;
            q50_textBox.Text = "";
            n50_textBox.Text = "";
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            Load_OtherFormData();
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form",
                "천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택," +
                "천장축열,외벽축열,내벽축열,바닥축열," +
                "천장면적,외벽면적,내벽면적,바닥면적," +
                "존축열성능," +
                "존기밀타입,기밀적용유형," +
                "q50,n50", "존번호 = '" + ZoneNum + "'");

                Ceiling_index = Value[0][0];
                CeilingCwirk_comboBox.SelectedItem = Ceiling_index;
                Wall_index = Value[0][1];
                WallCwirk_comboBox.SelectedItem = Wall_index;
                InWall_index = Value[0][2];
                InWallCwirk_comboBox.SelectedItem = InWall_index;
                Slab_index = Value[0][3];
                SlabCwirk_comboBox.SelectedItem = Slab_index;


                Cwirk_Ceiling = Convert.ToDouble(Value[0][4]);
                Cwirk_Wall = Convert.ToDouble(Value[0][5]);
                Cwirk_InWall = Convert.ToDouble(Value[0][6]);
                Cwirk_Slab = Convert.ToDouble(Value[0][7]);

                Area_Ceiling = Convert.ToDouble(Value[0][8]);
                Area_Wall = Convert.ToDouble(Value[0][9]);
                Area_InWall = Convert.ToDouble(Value[0][10]);
                Area_Slab = Convert.ToDouble(Value[0][11]);

                Cwirk_total = Convert.ToDouble(Value[0][12]);
                Cwirk_textBox.Text = string.Format("{0:F2}", Cwirk_total);

                ZoneType = Value[0][13];
                Check_radioButton(ZoneType);

                InfiltrationType_comboBox.SelectedItem = Value[0][14];
                q50_textBox.Text = string.Format("{0:F1}", Convert.ToDouble(Value[0][15]));
                n50_textBox.Text = string.Format("{0:F1}", Convert.ToDouble(Value[0][16]));

            }
            catch { }

        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            //Num_textBox.Text = ID;
            //ZoneNum = ID;
            //Load_OtherFormData();

        }
        private void ZoneEnvelope_VisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.currentForm == main.MainContents.FormID.ZoneEnvelope)
            {
                String ID = main.MainContents.selID;
                ID = ID.Substring(19, 10);
                Num_textBox.Text = ID;
                ZoneNum = ID;
                LoadData(ZoneNum);
                Calc_A(ZoneNum);
                ZoneType = Calc_ZoneType();
                Check_radioButton(ZoneType);
            }
        }
        private void Load_OtherFormData()
        {
            try
            {
                //존이름 불러오기
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름, 순바닥면적", "존번호 = '" + ZoneNum + "'");

                ZoneName = Value[0][0];
                ZoneName_textBox.Text = ZoneName;
                NetArea = Convert.ToDouble(Value[0][1]);
            }
            catch
            {
                MessageBox.Show("존 일반정보부터 입력하세요.");
                Program.getMenuForm().DoLoadFormDirect(12);
            }
            try
            {
                //존외피정보 불러오기
                ZoneE = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,커튼월부위,면적,구조체,구조체번호,층", "존='" + ZoneNum + "'");
                Layer = ZoneE[0][6];
                Layer_textBox.Text = Layer;
                load_table_ZoneEnvelopeInfo(ZoneNum);
            }
            catch { }



        }

    }
}
