using main.contentslist;
using main.info;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.contents
{
    public partial class ZoneEnvelope : Form, IConfirmable
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
        double NetArea, height;

        public ZoneEnvelope()
        {

            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 외피정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            //축열관련 콤보박스 만들기
            //천장
            string[][] SQL_index_Celing = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '천장'");
            int i = -1;
            if (SQL_index_Celing.Length > 0)
            {
                while (++i < SQL_index_Celing.Length)
                {
                    CeilingCwirk_comboBox.Items.Add(SQL_index_Celing[i][0]);
                }
            }
            //외벽
            string[][] SQL_index_Wall = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '외벽'");
            i = -1;
            if (SQL_index_Wall.Length > 0)
            {
                while (++i < SQL_index_Wall.Length)
                {
                    WallCwirk_comboBox.Items.Add(SQL_index_Wall[i][0]);
                }
            }
            //내벽
            string[][] SQL_index_InWall = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '간벽'");
            i = -1;
            if (SQL_index_InWall.Length > 0)
            {
                while (++i < SQL_index_InWall.Length)
                {
                    InWallCwirk_comboBox.Items.Add(SQL_index_InWall[i][0]);
                }
            }
            //바닥
            string[][] SQL_index_Slab = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "축열유형", "구조체 = '바닥'");
            i = -1;
            if (SQL_index_Slab.Length > 0)
            {
                while (++i < SQL_index_Slab.Length)
                {
                    SlabCwirk_comboBox.Items.Add(SQL_index_Slab[i][0]);
                }
            }
            label10.Text = "Wh/(m" + Program.UTIL.Subscript(2, true) + "K)";
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void AdditionalPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        //천장 축열정보 선택 시 
        private void CeilingCwrik_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CeilingCwirk_comboBox.SelectedItem != null)
            {

                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='천장' AND 축열유형='" + CeilingCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Ceiling_index = CeilingCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Ceiling = Calc_Cwirk_Construction(Area_Ceiling, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
        }

        private void WallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='외벽' AND 축열유형='" + WallCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Wall_index = WallCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Wall = Calc_Cwirk_Construction(Area_Wall, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
        }

        private void InWallCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InWallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='간벽' AND 축열유형='" + InWallCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    InWall_index = InWallCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_InWall = Calc_Cwirk_Construction(Area_InWall, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
        }

        private void SlabCwirk_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SlabCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='바닥' AND 축열유형='" + SlabCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Slab_index = SlabCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Slab = Calc_Cwirk_Construction(Area_Slab, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
        }

        //존외피 왼쪽테이블 정보 만들기 
        void load_table_ZoneEnvelopeInfo(String ZoneNum)
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            // 컬럼 추가
            dataGridView1.Columns.Add("A1", "구조체");
            dataGridView1.Columns.Add("A2", "개수");
            dataGridView1.Columns.Add("A3", "A.[m2]");
            dataGridView1.Columns.Add("A4", "Ueff.[W/m2K]");
            dataGridView1.Columns[0].Width = 40;

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
                        Construction_AreaSum[k] += Program.UTIL.ToDoubleOrZero(ZoneE[i][3]);
                    }
                }
            }

            i = -1;
            while (++i < ZoneE.Length)
            {
                if (ZoneE[i][1] == "커튼월창")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "커튼월창유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[0] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }

                }
                else if (ZoneE[i][1] == "외벽")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[1] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "지붕")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[2] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "최하층바닥")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[3] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "창호")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "창호유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[4] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }
                }
                else if (ZoneE[i][1] == "외부출입문")
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "문유효열관류율", "번호='" + ZoneE[i][5] + "'");
                    if (Value.Length > 0)
                    { Construction_UeffSum[5] += (Program.UTIL.ToDoubleOrZero(Value[0][0]) * Program.UTIL.ToDoubleOrZero(ZoneE[i][3])); }
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
                int nRow = dataGridView1.Rows.Add();
                dataGridView1.Rows[nRow].Cells[1].Value = ConstructionType[k];
                dataGridView1.Rows[nRow].Cells[2].Value = Construction_Count[k];
                dataGridView1.Rows[nRow].Cells[3].Value = Construction_AreaSum[k].ToString("0.00");
                dataGridView1.Rows[nRow].Cells[4].Value = Construction_UeffAvg[k].ToString("0.00");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int k = 0; k < dataGridView1.Rows.Count; k++)
                {
                    if (k != dataGridView1.CurrentCell.RowIndex)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                    }
                    else
                    {
                        dataGridView1.Rows[k].Cells[0].Value = true;
                    }

                }
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                load_table_ZoneEnvelopeSelect(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }
        // 존외피 오른쪽테이블 정보 만들기 : 선택된 구조체에 대한 정보 
        void load_table_ZoneEnvelopeSelect(String 선택구조체)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            new StackedHeaderDecorator(dataGridView2, DataGridViewAutoSizeColumnsMode.Fill, dataGridView_RowHandle);
            dataGridView2.Columns.Add("A0", "구분");
            dataGridView2.Columns.Add("A1", "번호");
            dataGridView2.Columns.Add("A2", "명칭");
            dataGridView2.Columns.Add("A3", "A.[m2]");
            dataGridView2.Columns.Add("A4", "방위");
            dataGridView2.Columns.Add("A5", "기울기");
            dataGridView2.Columns.Add("A6", "Ueff.[W/m2K]");
            if (선택구조체 == "커튼월창" || 선택구조체 == "창호")
            {
                dataGridView2.Columns.Add("A7", "g.[-]");
            }
            else
            {
                dataGridView2.Columns.Add("A7", "α.[-]");
            }
            dataGridView2.Columns[0].Width = 30;
            dataGridView2.Columns[2].Width = 100;
            string[][] Value;

            string[][] ZoneE_Select = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,면적,방위,기울기,구조체,구조체번호", "존='" + ZoneNum + "' AND 외피유형='" + 선택구조체 + "'");
            if (ZoneE_Select.Length > 0)
            {
                for (int n = 0; n < ZoneE_Select.Length; n++)
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
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "문유효열관류율,흡수율", "번호='" + ZoneE_Select[n][6] + "'");
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "유효열관류율,흡수율", "");
                    }

                    if (Value.Length > 0)
                    {
                        String 면적 = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(ZoneE_Select[n][2]));
                        String Ueff = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(Value[0][0]));
                        String g = "";
                        int nRow = dataGridView2.Rows.Add();
                        dataGridView2.Rows[nRow].Cells[0].Value = (n + 1).ToString();
                        dataGridView2.Rows[nRow].Cells[1].Value = ZoneE_Select[n][6];
                        dataGridView2.Rows[nRow].Cells[2].Value = ZoneE_Select[n][5];
                        dataGridView2.Rows[nRow].Cells[3].Value = 면적;
                        dataGridView2.Rows[nRow].Cells[4].Value = ZoneE_Select[n][3];
                        dataGridView2.Rows[nRow].Cells[5].Value = ZoneE_Select[n][4];
                        dataGridView2.Rows[nRow].Cells[6].Value = Ueff;
                        if (선택구조체 == "커튼월창")
                        {
                            g = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(Value[0][1]));
                        }
                        else if (선택구조체 == "창호")
                        {
                            string[][] gValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "태양열취득률", "번호='" + Value[0][1] + "'");
                            if (gValue.Length > 0)
                            {
                                g = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(gValue[0][0]));
                            }
                        }
                        else if (선택구조체 == "외벽" || 선택구조체 == "지붕" || 선택구조체 == "외부출입문")
                        {
                            g = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(Value[0][1]));
                        }
                        dataGridView2.Rows[nRow].Cells[7].Value = g;
                    }
                    else
                    {
                        String 면적 = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(ZoneE_Select[n][2]));
                        String Ueff = "";
                        String g = "";
                        int nRow = dataGridView2.Rows.Add();
                        dataGridView2.Rows[nRow].Cells[0].Value = (n + 1).ToString();
                        dataGridView2.Rows[nRow].Cells[1].Value = ZoneE_Select[n][6];
                        dataGridView2.Rows[nRow].Cells[2].Value = ZoneE_Select[n][5];
                        dataGridView2.Rows[nRow].Cells[3].Value = 면적;
                        dataGridView2.Rows[nRow].Cells[4].Value = ZoneE_Select[n][3];
                        dataGridView2.Rows[nRow].Cells[5].Value = ZoneE_Select[n][4];
                        dataGridView2.Rows[nRow].Cells[6].Value = Ueff;
                        dataGridView2.Rows[nRow].Cells[7].Value = g;
                    }

                }
            }
        }

        private bool dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            cell.Style.ForeColor = Color.Black;
            cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
            cell.Style.SelectionForeColor = Color.Black;
            return true;
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
                Cwirk_textBox.Text = Cwirk_total.ToString();
                Program.UTIL.textBox_doubleComa(Cwirk_textBox, true, 2);
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

            // 외부출입문이 아니라 커튼월창의 일부(출입문부분)로 출입문이 있는 경우도 출입문존으로 취급.
            // 3D 모델링 화면(sub3dZoneInfo.cs)에서 세그먼트별로 직접 고르는 값이라, 존에 실제로 놓인
            // 세그먼트 기준의 ZoneEnvelope_3D.커튼월부위가 맞음(ConstructionCW.출입문적용유무는 구조체
            // 자체의 옵션 존재 여부일 뿐 이 존에 실제 설치됐는지와 무관해서 부적합).
            bool hasCurtainWallDoor = false;
            for (int i = 0; i < ZoneE.Length; i++)
            {
                if (ZoneE[i][1] == "커튼월창" && ZoneE[i][2] == "출입문부분")
                {
                    hasCurtainWallDoor = true;
                    break;
                }
            }

            if (Construction_Count[5] > 0 || hasCurtainWallDoor)
            {
                존유형 = "출입문존";
            }
            else if ((Construction_Count[0] + Construction_Count[1] + Construction_Count[2] + Construction_Count[4] + Construction_Count[5]) > 0)
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

        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                if (CeilingCwirk_comboBox.SelectedItem == null || WallCwirk_comboBox.SelectedItem == null || InWallCwirk_comboBox.SelectedItem == null || SlabCwirk_comboBox.SelectedItem == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("모든 축열값을 선택하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Save(isManualSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return false;
            }
        }

        private void Save(bool isManualSave = false)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,프로젝트유형," +
                "천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택," +
                "천장축열,외벽축열,내벽축열,바닥축열," +
                "천장면적,외벽면적,내벽면적,바닥면적," +
                "존축열성능," +
                "존기밀타입",
            "'" + ZoneNum + "','" + 프로젝트유형[0][0] + "','"
            + Ceiling_index + "','" + Wall_index + "','" + InWall_index + "','" + Slab_index + "','"
            + Cwirk_Ceiling.ToString() + "','" + Cwirk_Wall.ToString() + "','" + Cwirk_InWall.ToString() + "','" + Cwirk_Slab.ToString() + "','"
            + Area_Ceiling.ToString() + "','" + Area_Wall.ToString() + "','" + Area_InWall.ToString() + "','" + Area_Slab.ToString() + "','"
            + Cwirk_total.ToString() + "','"
            + ZoneType + "'", "존번호");

            Program.DB.saveProject();
        }
        private void reset()
        {
            ZoneName_textBox.Text = "";

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

        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            Load_OtherFormData();
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form",
                "천장축열선택,외벽축열선택,내벽축열선택,바닥축열선택," +
                "천장축열,외벽축열,내벽축열,바닥축열," +
                "천장면적,외벽면적,내벽면적,바닥면적," +
                "존축열성능", "존번호 = '" + ZoneNum + "'");
            if (Value.Length > 0)
            {
                Ceiling_index = Value[0][0];
                CeilingCwirk_comboBox.SelectedItem = Ceiling_index;
                Wall_index = Value[0][1];
                WallCwirk_comboBox.SelectedItem = Wall_index;
                InWall_index = Value[0][2];
                InWallCwirk_comboBox.SelectedItem = InWall_index;
                Slab_index = Value[0][3];
                SlabCwirk_comboBox.SelectedItem = Slab_index;

                if (Value[0][4] != "")
                {
                    Cwirk_Ceiling = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                }
                if (Value[0][5] != "")
                {
                    Cwirk_Wall = Program.UTIL.ToDoubleOrZero(Value[0][5]);
                }
                if (Value[0][6] != "")
                {
                    Cwirk_InWall = Program.UTIL.ToDoubleOrZero(Value[0][6]);
                }
                if (Value[0][7] != "")
                {
                    Cwirk_Slab = Program.UTIL.ToDoubleOrZero(Value[0][7]);
                }
                if (Value[0][8] != "")
                {
                    Area_Ceiling = Program.UTIL.ToDoubleOrZero(Value[0][8]);
                }
                if (Value[0][9] != "")
                {
                    Area_Wall = Program.UTIL.ToDoubleOrZero(Value[0][9]);
                }
                if (Value[0][10] != "")
                {
                    Area_InWall = Program.UTIL.ToDoubleOrZero(Value[0][10]);
                }
                if (Value[0][11] != "")
                {
                    Area_Slab = Program.UTIL.ToDoubleOrZero(Value[0][11]);
                }
                if (Value[0][12] != "")
                {
                    Cwirk_total = Program.UTIL.ToDoubleOrZero(Value[0][12]);
                }
                Calc_Cwirk_all();
                Cwirk_textBox.Text = Cwirk_total.ToString();
                Program.UTIL.textBox_doubleComa(Cwirk_textBox, true, 2);

            }

        }

        private void ZoneEnvelope_VisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.currentForm == main.MainContents.FormID.ZoneEnvelope)
            {
                String ID = main.MainContents.selID;
                int v1 = ID.IndexOf("Zone") + 4; //Zone 번호 위치 
                int v2 = ID.IndexOf("_", v1); //Zone 다음 "_"의 위치 
                ID = ID.Substring(19, v2 - 19);
                Num_textBox.Text = ID;
                ZoneNum = ID;
                LoadData(ZoneNum);
                Calc_A(ZoneNum);
                ZoneType = Calc_ZoneType();
                Check_radioButton(ZoneType);
                Show_ZoneN50();
            }
        }

        // "존별 표준값 적용" 방식일 때만 이 존의 n50을 계산해서 보여줌 — 실측/기밀 시공 여부별 방식은 표시 안 함
        void Show_ZoneN50()
        {
            string[][] Building = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀측정여부,기밀보고서,기밀적용방식", "");
            bool showN50 = false;
            double n50Value = 0;

            if (Building.Length > 0 && Building[0][0] != "기밀 테스트 실시" && Building[0][2] == "존별")
            {
                showN50 = true;

                string[][] UseProfile = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "용도프로필", "존번호 = '" + ZoneNum + "'");
                string usage = UseProfile.Length > 0 ? UseProfile[0][0] : "";
                bool isResidential = usage == "단독주택" || usage == "공동주택";

                string criteria = Building[0][1] == "있음" ? "기밀설계보고서" : "표준값";

                string where = isResidential
                    ? "주거여부 = '주거' And 기준유형 = '" + criteria + "'"
                    : "주거여부 = '비주거' And 존유형 = '" + ZoneType + "' And 기준유형 = '" + criteria + "'";
                string[][] StdValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "기밀_존별", "n50,q50", where);

                if (StdValue.Length > 0)
                {
                    double n50Std = Program.UTIL.ToDoubleOrZero(StdValue[0][0]);
                    double q50Std = Program.UTIL.ToDoubleOrZero(StdValue[0][1]);

                    string[][] VolumeValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순체적", "존번호 = '" + ZoneNum + "'");
                    double volume = VolumeValue.Length > 0 && VolumeValue[0][0] != "" ? Program.UTIL.ToDoubleOrZero(VolumeValue[0][0]) : 0;

                    if (volume <= 1500)
                    {
                        n50Value = n50Std;
                    }
                    else if (volume > 0)
                    {
                        // q50 → n50 환산: n50 = q50 × 직접노출외피면적 / 순체적 (Cal_HCneed.cs의 Zone_n50()과 동일한 물리식)
                        n50Value = q50Std * Calc_AreaDirect() / volume;
                    }
                }
            }

            label12.Visible = showN50;
            label11.Visible = showN50;
            textBox1.Visible = showN50;
            if (showN50)
            {
                textBox1.Text = n50Value.ToString("0.00");
            }

            // 저장 버튼과 무관하게 항상 최신 계산값을 반영 — Cal_HCneed.cs가 이 값을 그대로 읽어다 씀.
            // 존별 방식이 아닐 땐 0으로 같이 저장해서 이전 설정의 값이 남아있지 않게 함
            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,n50", "'" + ZoneNum + "','" + n50Value + "'", "존번호");
            Program.DB.saveProject();
        }

        // 직접외기 노출 외피면적 합계 — Cal_HCneed.cs의 Zone_n50()과 동일한 필터링(직접외기만 합산)
        double Calc_AreaDirect()
        {
            double areaDirect = 0;
            string[][] Elements = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "외피유형,면적,구조체번호", "존 = '" + ZoneNum + "'");
            foreach (string[] element in Elements)
            {
                string type = element[0];
                double area = Program.UTIL.ToDoubleOrZero(element[1]);
                string structNum = element[2];

                if (type == "커튼월창")
                {
                    areaDirect += area;
                }
                else if (type == "외벽")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호='" + structNum + "'");
                    if (v.Length > 0 && v[0][0] == "직접외기") { areaDirect += area; }
                }
                else if (type == "지붕")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "직접간접", "번호='" + structNum + "'");
                    if (v.Length > 0 && v[0][0] == "직접외기") { areaDirect += area; }
                }
                else if (type == "창호")
                {
                    string[][] v = Program.DB.querySQL(DB.type.ProjDB, "select a.직접간접 FROM ConstructionWindow AS a INNER JOIN SubWindow AS b ON a.번호 = b.상위창호번호 where b.번호 = '" + structNum + "'");
                    if (v.Length > 0 && v[0][0] == "직접외기") { areaDirect += area; }
                }
                else if (type == "외부출입문")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "ConstructionDoor", "직접간접", "번호='" + structNum + "'");
                    if (v.Length > 0 && v[0][0] == "직접외기") { areaDirect += area; }
                }
            }
            return areaDirect;
        }
        private void Load_OtherFormData()
        {
            try
            {
                //존이름 불러오기
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름, 순바닥면적,천장고", "존번호 = '" + ZoneNum + "'");
                if (Value.Length > 0)
                {
                    ZoneName = Value[0][0];
                    ZoneName_textBox.Text = ZoneName;
                    NetArea = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                    height = Program.UTIL.ToDoubleOrZero(Value[0][2]);
                }
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
                if (ZoneE.Length > 0)
                {
                    Layer = ZoneE[0][6];
                    load_table_ZoneEnvelopeInfo(ZoneNum);
                }
            }
            catch { }

        }

        private void Calc_Cwirk_all()
        {
            if (CeilingCwirk_comboBox.SelectedItem != null)
            {

                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='천장' AND 축열유형='" + CeilingCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Ceiling_index = CeilingCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Ceiling = Calc_Cwirk_Construction(Area_Ceiling, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }

            if (WallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='외벽' AND 축열유형='" + WallCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Wall_index = WallCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Wall = Calc_Cwirk_Construction(Area_Wall, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }

            if (InWallCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='간벽' AND 축열유형='" + InWallCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    InWall_index = InWallCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_InWall = Calc_Cwirk_Construction(Area_InWall, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
            if (SlabCwirk_comboBox.SelectedItem != null)
            {
                double CwirkA;
                string[][] CwirkDB = Program.DB.getValue(DB.type.BaseDB_HCneed, "축열", "Cwirk", "구조체='바닥' AND 축열유형='" + SlabCwirk_comboBox.SelectedItem.ToString() + "'");
                if (CwirkDB.Length > 0)
                {
                    Slab_index = SlabCwirk_comboBox.SelectedItem.ToString();
                    CwirkA = Program.UTIL.ToDoubleOrZero(CwirkDB[0][0]);
                    Cwirk_Slab = Calc_Cwirk_Construction(Area_Slab, CwirkA);
                    Calc_Cwirk(Cwirk_Ceiling, Cwirk_Wall, Cwirk_InWall, Cwirk_Slab);
                }
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\14.ZoneEnvelope";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

    }
}
