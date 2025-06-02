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
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;


namespace main.subcontents
{
    public partial class Cooling_Zone : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList();
        ArrayList SelectZone_split = new ArrayList();
        ArrayList SelectAhu_split = new ArrayList();
        String SystemNum;
        public string SelectZone;
        public string SelectAhu;
        string SelectType;
        public Cooling_Zone(string Num, string Select_nonsplit, string selectType)
        {

            SelectType = selectType; //Zone 와 Ahu 임
            SystemNum = Num;
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
                        
            if (Select_nonsplit != null)
            {
                Load_SaveValue(Select_nonsplit);
            }
            else
            {
                makeTabel();
                load_table_DB();
            }
        }

        void makeTabel()
        {
            new StackedHeaderDecorator(CoolingZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            CoolingZone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolingZone_dataGridView.Columns.Add(checkBoxColumn);
            CoolingZone_dataGridView.Columns.Add("A1", "번호");
            if (SelectType == "Zone")
            {
                CoolingZone_dataGridView.Columns.Add("A2", "층");
                CoolingZone_dataGridView.Columns.Add("A3", "존명칭");
                CoolingZone_dataGridView.Columns.Add("A4", "용도프로필");
                CoolingZone_dataGridView.Columns.Add("A5", "냉방요구량.[kWh/a]");
                CoolingZone_dataGridView.Columns.Add("A6", "최대냉방부하.[kW]");
                CoolingZone_dataGridView.Columns.Add("A7", "면적.[m"+Program.UTIL.Subscript(2, true)+"]");
                CoolingZone_dataGridView.Columns.Add("A8", "냉방설비.1");
                CoolingZone_dataGridView.Columns.Add("A9", "냉방설비.2");
            }
            else 
            {
                CoolingZone_dataGridView.Columns.Add("A2", "존개수");
                CoolingZone_dataGridView.Columns.Add("A3", "공조기명칭");
                CoolingZone_dataGridView.Columns.Add("A4", "유형");
                CoolingZone_dataGridView.Columns.Add("A5", "냉방요구량.[kWh/a]");
                CoolingZone_dataGridView.Columns.Add("A6", "최대냉방부하.[kW]");
                CoolingZone_dataGridView.Columns.Add("A7", "면적.[m"+Program.UTIL.Subscript(2, true)+"]");
                CoolingZone_dataGridView.Columns[3].Visible = false;
            }
           
            CoolingZone_dataGridView.Columns[0].Width = 40;
            CoolingZone_dataGridView.Columns[2].Width = 60;
        }

        void load_table_DB()
        {
            if(SelectType == "Zone")
            {
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적", "냉난방유무 ='냉난방' OR 냉난방유무 = '냉방'");

                if (Value.Length > 0)
                {
                    for (int n = 0; n < Value.Length; n++) //존번호로 작성함
                    {
                        string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "층", "존번호 ='" + Value[n][0] + "'");
                        string[][] 부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '냉방'");
                        string[][] 설비 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템,공급설비종류", "존번호 ='" + Value[n][0] + "'");

                        CoolingZone_dataGridView.Rows.Add();
                        int nRow = CoolingZone_dataGridView.Rows.Count - 1;
                        CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                        CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = 층[0][0];
                        CoolingZone_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1];
                        CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2];
                        CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F0}", Convert.ToDouble(부하[0][0]));
                        CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Convert.ToDouble(부하[0][1]) / 1000);
                        CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
                        
                        if (설비.Length > 0)
                        {
                            List<string> check = new List<string>();
                            for (int i = 0; i < 설비.Length; i++)
                            {
                                if (설비[i][1] != "VAV유닛" && 설비[i][1] != "CAV유닛" && 설비[i][1] != "팬파워유닛")
                                {
                                    check.Add(설비[i][0]);
                                }
                            }
                            if (check.Count == 1)
                            {
                                CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = check[0];
                            }
                            else if (check.Count > 1)
                            {
                                CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = check[0];
                                CoolingZone_dataGridView.Rows[nRow].Cells[9].Value = check[1];
                            }
                            else
                            {
                                CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = null;
                                CoolingZone_dataGridView.Rows[nRow].Cells[9].Value = null;
                            }
                        }
                    }
                }
            }
            else
            {
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.ProjDB, "AHUSystem_Form", "번호,명칭,유형", "유형='공조기'"); //시스템번호는 반영안됨
                if (Value.Length > 0)
                {
                    for (int n = 0; n < Value.Length; n++) //존번호로 작성함
                    {
                        double need = 0;
                        double area = 0;
                        double Power = 0;
                        for (int j = 0; j < 12; j++)
                        {
                            string mth = string.Format("{0}월", j+1);
                            string[][] 요구량 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "AHUSystem_Result", "공조요구량, Qmax_tot", " 번호 ='" + Value[n][0] + "' And 난방_냉방 = '냉방' And 월 = '"+mth+"'");
                            double v = 0;
                            if (요구량.Length > 0)
                            {
                                if (double.TryParse(요구량[0][0], out v))
                                {
                                    need += v;
                                }
                            }
                            else
                            {
                                need += 0;
                            }
                        }
                        string[][]  공조출력 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "AHUSystem_Result", "Qmax_tot", " 번호 ='" + Value[n][0] + "' And 난방_냉방 = '냉방'");
                        if (공조출력.Length > 0)
                        {
                            Power = Convert.ToDouble(공조출력[0][0].ToString());
                        }
                        else Power = 0;

                        string[][] 존 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,순바닥면적", "선택열회수기 = '" + Value[n][0] + "'");
                        for(int h= 0;h<존.Length ; h++)
                        {
                            SelectAhu_split.Add(존[h][0]);
                            area += Convert.ToDouble(존[h][1]);
                        }
                      
                        CoolingZone_dataGridView.Rows.Add();
                        int nRow = CoolingZone_dataGridView.Rows.Count - 1;
                        CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                        CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = string.Format("{0}외{1}개", SelectAhu_split[0], SelectAhu_split.Count-1);   //존개수
                        CoolingZone_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1]; //공조기명칭
                        CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2]; //유형
                        CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F0}", need);
                        CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Power / 1000);
                        CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F2}", area); //면적
                    }
                } 
            }
        }
        private void Split(string nonSplit, List<string> type)
        {
            type.Clear();

            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    type.Add(item);
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
        private void SelectCheckBox()
        {
            SelectRow.Clear();
            foreach (DataGridViewRow row in CoolingZone_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            SelectCheckBox();
            if (SelectType == "Zone")
            {
                for (int k = 0; k < SelectRow.Count; k++)
                {
                    if (k == SelectRow.Count - 1)
                    {
                        this.SelectZone += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                    }
                    else
                    {
                        this.SelectZone += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                    }
                }
            }
            else if (SelectType == "Ahu")
            {
                for (int k = 0; k < SelectRow.Count; k++)
                {
                    if (k == SelectRow.Count - 1)
                    {
                        this.SelectAhu += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                    }
                    else
                    {
                        this.SelectAhu += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void reset()
        {
            SelectRow.Clear();
            SelectZone_split.Clear();
            SelectAhu_split.Clear();
            this.SelectZone = null;

            for (int n = 0; n < CoolingZone_dataGridView.Rows.Count; n++)
            {
                CoolingZone_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SaveValue(String Select_nonsplit)
        {
            reset();
            makeTabel();
            load_table_DB();
            if (SelectType == "Zone")
            {
                string[] token = Select_nonsplit.Split('+');
                SelectZone_split.Clear();
                foreach (var item in token)
                {
                    SelectZone_split.Add(item.ToString());
                }
                for (int k = 0; k < SelectZone_split.Count; k++)
                {
                    for (int n = 0; n < CoolingZone_dataGridView.Rows.Count; n++)
                    {
                        if (CoolingZone_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectZone_split[k].ToString())
                        {
                            CoolingZone_dataGridView.Rows[n].Cells[0].Value = true;
                        }
                    }
                }
            }
            else if (SelectType == "Ahu")
            {
                string[] token = Select_nonsplit.Split('+');
                SelectAhu_split.Clear();
                foreach (var item in token)
                {
                    SelectAhu_split.Add(item.ToString());
                }
                for (int k = 0; k < SelectAhu_split.Count; k++)
                {
                    for (int n = 0; n < CoolingZone_dataGridView.Rows.Count; n++)
                    {
                        if (CoolingZone_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectAhu_split[k].ToString())
                        {
                            CoolingZone_dataGridView.Rows[n].Cells[0].Value = true;
                        }
                    }
                }
            }
        }
    }
    class ahu설비
    {
        public List<string> _ahunum_sum = new List<string>();
        public string _num;
    }
}
