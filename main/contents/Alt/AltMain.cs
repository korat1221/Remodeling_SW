using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using main.subcontents.Alt;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static main.DB;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Eagle._Components.Public;
using System.Drawing.Text;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Web.WebView2.Core;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using Eagle._Interfaces.Public;
using System.Runtime.Intrinsics.X86;
using main.subcontents.EquipmentList;
using Eagle._Hosts;

namespace main.contents.Alt
{
    public partial class AltMain : Form
    {
        String AltNum;
        double Cost_Total; double Cost_Net = 0;//총공사비, 순공사비
        bool scriptable = false;
        string SelectAlt_Wall;
        string SelectAlt_Roof;
        public AltMain()
        {
            InitializeComponent();
            InitializeAsync();
            webView22.Source = new Uri(Program.gPath + "chart_ctrl2.html", true);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            create_Alt_Table();
        }
        async void InitializeAsync()
        {
            await webView22.EnsureCoreWebView2Async(null);
            webView22.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
            Load_RuleResult();
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView22.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {

        }

        private void Save()
        {

        }

        private void reset()
        {

        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            AltNum_textBox.Text = ID;
            AltNum = ID;
        }

        public void LoadData(String ID)
        {
            Create_Wall_Old_table();
        }

        private void AltMainPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        #region 사용자 요소기술 우선순위
        private void create_Alt_Table()
        {
            DataGridViewCheckBoxColumn Alt_checkBoxColumn = new DataGridViewCheckBoxColumn();
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Alt_dataGridView.Columns.Clear();
            Alt_checkBoxColumn.HeaderText = "선택";
            Alt_checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(Alt_checkBoxColumn);

            Alt_dataGridView.Columns.Add("A1", "순위");
            Alt_dataGridView.Columns.Add("A2", "요소기술");
            Alt_dataGridView.Columns.Add("A3", "+");
            Alt_dataGridView.Columns.Add("A4", "리모델링안");
            Alt_dataGridView.Columns.Add("A5", "예상 순공사비.[원]");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 50;
            Alt_dataGridView.Columns[2].Width = 70;
            Alt_dataGridView.Columns[3].Width = 30;
            Alt_dataGridView.Columns[5].Width = 75;
        }

        private void Alt_Add_button_Click(object sender, EventArgs e)
        {
            if (Cost_Net > 0)
            {

                if (Alt_dataGridView.Rows.Count >= 1)
                {
                    if (Alt_dataGridView.Rows[Alt_dataGridView.Rows.Count - 1].Cells[2].Value == null)
                    {
                        MessageBox.Show("먼저 " + Alt_dataGridView.Rows.Count + "순위 요소기술을 선택해주세요.");
                    }
                    else
                    {
                        Add_Alt();
                    }
                }
                else
                {
                    Add_Alt();
                }
            }
            else
            {
                MessageBox.Show("총 예산을 입력하세요.");
            }

        }
        private void Add_Alt()
        {
            int nRow = Alt_dataGridView.Rows.Add();
            Load_Alt_Num();
            string[] Selectlist = null;
            if (Alt_dataGridView.Rows.Count > 1)
            {
                Selectlist = new string[Alt_dataGridView.Rows.Count - 1];
                for (int i = 0; i < Alt_dataGridView.Rows.Count - 1; i++)
                {
                    if (Alt_dataGridView.Rows[i].Cells[2].Value != null)
                    { Selectlist[i] = Alt_dataGridView.Rows[i].Cells[2].Value.ToString(); }
                }
            }
            string[] Newlist = Get_ElementList(Selectlist);

            DataGridViewComboBoxCell Combo = new DataGridViewComboBoxCell();
            for (int i = 0; i < Newlist.Length; i++)
            {
                Combo.Items.Add(Newlist[i]);
            }
            Alt_dataGridView.Rows[nRow].Cells[2] = Combo;
            DataGridViewButtonCell AltButtonCell = new DataGridViewButtonCell();
            Alt_dataGridView.Rows[nRow].Cells[3] = AltButtonCell;
            AltButtonCell.Value = "+";
        }
        private void Alt_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (e.ColumnIndex == 3)
                {
                    if (Alt_dataGridView.Rows[e.RowIndex].Cells[2].Value == null)
                    {
                        MessageBox.Show("먼저 요소기술을 선택해주세요.");
                    }
                    else
                    {
                        switch (Alt_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString())
                        {
                            case "외벽":
                                Open_WallAlt();
                                break;

                        }
                    }
                }
            }
        }
        private string[] Get_ElementList(string[] Selectlist)
        {
            string[] list = CALC.RuleAlt;
            string[] Newlist = null;
            if (Selectlist != null)
            {
                for (int i = 0; i < Selectlist.Length; i++)
                {
                    list = list.Where(num => num != Selectlist[i]).ToArray();
                }
                Newlist = list;
            }
            else
            {
                Newlist = list;
            }

            return Newlist;
        }

        private void Alt_Remove_button_Click(object sender, EventArgs e)
        {
            int Boiler_SelectRow = GetSelectedIndex();
            Alt_dataGridView.Rows.Remove(Alt_dataGridView.Rows[Boiler_SelectRow]);
            Load_Alt_Num();
        }
        private void Load_Alt_Num()
        {
            for (int k = 0; k < Alt_dataGridView.RowCount; k++)
            {
                Alt_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString() + " 순위";
            }
        }
        private int GetSelectedIndex()
        {
            for (int k = 0; k < Alt_dataGridView.Rows.Count; k++)
            {
                if (Convert.ToBoolean(Alt_dataGridView.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }

        #endregion

        #region 법규기반 검토
        private void Load_RuleResult()
        {
            string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select 검토유형, 총에너지소요량,기저에너지 From FinalEnergy_Result_Rule Where 월='연간' and 연료='전체' Order By 총에너지소요량 ASC");
            string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량,기저에너지", "월='연간' and 연료='전체'");
            if (value.Length > 0 && value2.Length > 0)
            {
                label_rule.Visible = true;
                label_rule2.Visible = true;
                RuleResult_dataGridView.Visible = true;

                List<string> Type_List = new List<string>();
                List<double> Saving_List = new List<double>();
                for (int a = 0; a < value.Length; a++)
                {
                    double saving = Convert.ToDouble(value2[0][0]) - Convert.ToDouble(value[a][1]);
                    if (saving > 0)
                    {
                        Saving_List.Add(saving);
                        Type_List.Add(value[a][0]);
                    }
                }

                string s = "", s2 = "";
                for (int a = 0; a < Saving_List.Count; a++)
                {
                    s2 += Convert.ToDouble(Saving_List[a].ToString("0")) + ",";
                    s += "\"" + Type_List[a] + "\",";
                }

                runScript("drawChart5([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#9bc2e6\",min:0,max:150}],[" + s + "])");

                new StackedHeaderDecorator(RuleResult_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
                RuleResult_dataGridView.Columns.Clear();
                RuleResult_dataGridView.Columns.Add("A0", "순위");
                RuleResult_dataGridView.Columns.Add("A1", "요소기술");
                RuleResult_dataGridView.Columns.Add("A2", "예상 절감량.[kWh/a]");
                RuleResult_dataGridView.Columns.Add("A3", "예상 절감률.[%]");
                RuleResult_dataGridView.Columns[0].Width = 60;
                for (int a = 0; a < Saving_List.Count; a++)
                {
                    int nRow = RuleResult_dataGridView.Rows.Add();
                    RuleResult_dataGridView.Rows[nRow].Cells[0].Value = (a + 1).ToString() + " 순위";
                    RuleResult_dataGridView.Rows[nRow].Cells[1].Value = Type_List[a];
                    RuleResult_dataGridView.Rows[nRow].Cells[2].Value = Saving_List[a].ToString("#,##0");
                    RuleResult_dataGridView.Rows[nRow].Cells[3].Value = (Saving_List[a] / (Convert.ToDouble(value2[0][0]) - Convert.ToDouble(value2[0][1])) * 100).ToString("0.0") + " %";

                }
            }
        }
        #endregion

        #region 비용계산

        private void CostCalc_button_Click(object sender, EventArgs e)
        {
            if (Cost_Total != 0)
            {
                Calc_NetCost(Cost_Total);
            }

        }
        private void CostTotal_textBox_TextChanged(object sender, EventArgs e)
        {
            controls.ThousandsSeparator textbox = new controls.ThousandsSeparator(CostTotal_textBox, false, 0);
            Cost_Total = textbox.text;
        }
        private void Calc_NetCost(double CostTotal)
        {
            double Area = 0; double 일반관리비_비율 = 0, 이윤_비율 = 0;
            double 부가가치세_비율 = 0.1, 재료비_비율 = 0.65, 노무비_비율 = 0.25, 경비_비율 = 0.1;
            double 순공사비 = 0, 일반관리비 = 0, 이윤 = 0, 공급가액 = 0, 부가가치세 = 0, 폐기물처리비 = 0;
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='층간바닥' Or 외피유형='최하층바닥'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    Area += Convert.ToDouble(Value[a][0]);
                }
            }
            폐기물처리비 = Cal_CostWaste(Area);

            if(폐기물처리비 > CostTotal)
            {
                MessageBox.Show("예상 폐기물처리비("+ 폐기물처리비.ToString("#,##0")+ "원) 보다 많은 예산을 입력해주세요.");
            }
            else
            {
                Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "공사비비율", "공사비하한,공사비상한,일반관리비,이윤", "");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        if (Convert.ToDouble(Value[a][0]) < CostTotal && CostTotal < Convert.ToDouble(Value[a][1]))
                        {
                            일반관리비_비율 = Convert.ToDouble(Value[a][2]);
                            이윤_비율 = Convert.ToDouble(Value[a][3]);
                            break;
                        }
                    }
                    if (일반관리비_비율==0 && 이윤_비율 ==0)
                    {
                        일반관리비_비율 = Convert.ToDouble(Value[Value.Length - 1][2]);
                        이윤_비율 = Convert.ToDouble(Value[Value.Length - 1][3]);
                    }
                }
                부가가치세 = Math.Max(0, (CostTotal - 폐기물처리비) * 부가가치세_비율 / (1 + 부가가치세_비율));
                공급가액 = Math.Max(0, (CostTotal - 폐기물처리비) * 1 / (1 + 부가가치세_비율));
                순공사비 = Math.Max(0, 공급가액 * (재료비_비율 + 노무비_비율 + 경비_비율) / ((재료비_비율 + 노무비_비율 + 경비_비율) * (1 + 일반관리비_비율 + 일반관리비_비율 * 이윤_비율) + 이윤_비율 * (노무비_비율 + 경비_비율)));
                일반관리비 = 순공사비 * 일반관리비_비율;
                이윤 = (순공사비 * (노무비_비율 + 경비_비율) + 일반관리비) * 이윤_비율;
                Load_CostTable(순공사비, 일반관리비, 이윤, 부가가치세, 폐기물처리비, CostTotal);
                Cost_Net = 순공사비;
                Cal_BalanceCost(Cost_Net);
            }
        }
        private void Load_CostTable(double 순공사비, double 일반관리비, double 이윤, double 부가가치세, double 폐기물처리비, double 합계)
        {
            new StackedHeaderDecorator(Cost_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Cost_dataGridView.Columns.Clear();
            Cost_dataGridView.Rows.Clear();
            Cost_dataGridView.Columns.Add("A0", "항목");
            Cost_dataGridView.Columns.Add("A1", "예상비용[원]");

            int nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "순공사비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 순공사비.ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "일반관리비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 일반관리비.ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "이윤";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 이윤.ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "부가가치세";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 부가가치세.ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "폐기물처리비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 폐기물처리비.ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "합계";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = 합계.ToString("#,##0");
        }
        private double Cal_CostWaste(double Area)
        {
            double[] 폐기물원단위 = new double[3];//건설폐재류,금속철재류,혼합폐기물
            double[] 폐기물발생량 = new double[3];//건설폐재류,금속철재류,혼합폐기물
            double[] 중간처리비 = new double[3];//건설폐재류,금속철재류,혼합폐기물
            double[] 수집운반비 = new double[3];//건설폐재류,금속철재류,혼합폐기물

            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물원단위", "폐기물유형,원단위", "구조='RC조'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    if (Value[a][0] == "폐콘크리트")
                    {
                        폐기물원단위[0] = Convert.ToDouble(Value[a][1]);
                    }
                    else if (Value[a][0] == "폐금속류")
                    {
                        폐기물원단위[1] = Convert.ToDouble(Value[a][1]);
                    }
                    else
                    {
                        폐기물원단위[2] += Convert.ToDouble(Value[a][1]);
                    }
                }
            }
            for (int a = 0; a < 폐기물원단위.Length; a++)
            {
                폐기물발생량[a] = Area * 폐기물원단위[a];
            }

            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='중간처리단가' and 폐기물유형='건설폐재류'");
            if (Value.Length > 0)
            {
                중간처리비[0] = 폐기물발생량[0] * Convert.ToDouble(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='수집운반비' and 폐기물유형='건설폐재류'");
            if (Value.Length > 0)
            {
                수집운반비[0] = 폐기물발생량[0] * Convert.ToDouble(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='중간처리단가' and 폐기물유형='혼합건설폐기물'");
            if (Value.Length > 0)
            {
                중간처리비[2] = 폐기물발생량[2] * Convert.ToDouble(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='수집운반비' and 폐기물유형='혼합건설폐기물'");
            if (Value.Length > 0)
            {
                수집운반비[2] = 폐기물발생량[2] * Convert.ToDouble(Value[0][0]);
            }
            double 건설폐기물비 = (중간처리비.Sum() + 수집운반비.Sum()) * 1.1;

            return 건설폐기물비;
        }
        private double Cal_BalanceCost(double 순공사비)
        {
            if (순공사비 > 0)
            {
                double sum = 0;
                for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
                {
                    if (Alt_dataGridView.Rows[a].Cells[5].Value != null)
                    {
                        sum += Convert.ToDouble(Alt_dataGridView.Rows[a].Cells[5].Value.ToString());
                    }
                }
                BalanceCost_label.Visible = true;
                BalanceCost_textBox.Visible = true;
                BalanceCost_textBox.Text = (순공사비 - sum).ToString("#,##0") + " 원";

                return 순공사비 - sum;
            }
            else
            {
                MessageBox.Show("총 예산을 입력하세요.");
                return 0;
            }
        }
        #endregion

        #region 외벽
        private void Open_WallAlt()
        {
            AltWall form = new AltWall("");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {

                SelectAlt_Wall = form.SelectName;
                Create_Wall_New_table(SelectAlt_Wall);
                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='외벽' and 리모델링안='" + SelectAlt_Wall + "'");
                if (Value.Length > 0)
                {
                    WallCost_textBox.Text = Convert.ToDouble(Value[0][1]).ToString("#,##0") + " 원";
                    WallSavingPercent_textBox.Text = Convert.ToDouble(Value[0][3]).ToString("0.0") + " %";
                    WallPoint_textBox.Text = Convert.ToDouble(Value[0][4]).ToString("0.0") + " 점";
                }

                for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
                {
                    if (Alt_dataGridView.Rows[a].Cells[2].Value != null && Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "외벽")
                    {
                        Alt_dataGridView.Rows[a].Cells[4].Value = SelectAlt_Wall;
                        Alt_dataGridView.Rows[a].Cells[5].Value = Convert.ToDouble(Value[0][1]).ToString("#,##0");
                    }
                }
                Cal_BalanceCost(Cost_Net);
                string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='외벽' and a.최적안='" + SelectAlt_Wall + "'");
                if (Value2.Length > 0)
                {
                    Wall_new_label.Text = form.SelectName + ": " + Value2[0][0] + "_" + Value2[0][1];
                }
            }
        }

        private void WallCheck_button_Click(object sender, EventArgs e)
        {
            AltWall form = new AltWall(SelectAlt_Wall);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }
        private void Create_Wall_Old_table()
        {
            new StackedHeaderDecorator(Wall_Old_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Wall_Old_dataGridView.Columns.Clear();
            Wall_Old_dataGridView.Columns.Add("A0", "번호");
            Wall_Old_dataGridView.Columns.Add("A1", "명칭");
            Wall_Old_dataGridView.Columns.Add("A2", "유효열관류율.[W/m²·K]");
            Wall_Old_dataGridView.Columns.Add("A3", "면적.[m²]");
            Wall_Old_dataGridView.Columns[0].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율 From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Wall_Old_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Wall_Old_dataGridView.Rows.Add();
                    Wall_Old_dataGridView.Rows[nRow].Cells[0].Value = List[n][0];
                    Wall_Old_dataGridView.Rows[nRow].Cells[1].Value = List[n][1];
                    Wall_Old_dataGridView.Rows[nRow].Cells[2].Value = Convert.ToDouble(List[n][2]).ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Convert.ToDouble(Area[a][0]);
                        }
                        Wall_Old_dataGridView.Rows[nRow].Cells[3].Value = String.Format("{0:F2}", A);
                    }
                }
            }
        }
        private void Create_Wall_New_table(string SelectAlt_Wall)
        {
            new StackedHeaderDecorator(Wall_New_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            DataGridViewCheckBoxColumn Wall_New_checkBoxColumn = new DataGridViewCheckBoxColumn();
            Wall_New_dataGridView.Columns.Clear();
            Wall_New_checkBoxColumn.HeaderText = "선택";
            Wall_New_checkBoxColumn.Name = "check";
            Wall_New_dataGridView.Columns.Add(Wall_New_checkBoxColumn);

            Wall_New_dataGridView.Columns.Add("A1", "번호");
            Wall_New_dataGridView.Columns.Add("A2", "명칭");
            Wall_New_dataGridView.Columns.Add("A3", "유효열관류율.[W/m²·K]");
            Wall_New_dataGridView.Columns.Add("A4", "면적.[m²]");
            Wall_New_dataGridView.Columns[0].Width = 40;
            Wall_New_dataGridView.Columns[1].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율,a.직접간접 From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Wall_New_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Wall_New_dataGridView.Rows.Add();
                    Wall_New_dataGridView.Rows[nRow].Cells[1].Value = List[n][0];
                    Wall_New_dataGridView.Rows[nRow].Cells[2].Value = List[n][1];
                    double Ueff_new = Get_Wall_Ueff(SelectAlt_Wall, Convert.ToDouble(List[n][2]), List[n][3]);
                    Wall_New_dataGridView.Rows[nRow].Cells[3].Value = Ueff_new.ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Convert.ToDouble(Area[a][0]);
                        }
                        Wall_New_dataGridView.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", A);
                    }
                }
            }
        }
        private double Get_Wall_Ueff(string 리모델링안, double Uold, string 직접간접)
        {
            double Ueff = 0; double dU = 0;
            double R = 0;
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "불투명최적안", "열저항합계, 열교가산치,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                R = Convert.ToDouble(Value[0][0]);
                dU = Convert.ToDouble(Value[0][1]);
                if (직접간접 == "지면")
                {
                    if (Value[0][2] == "내부덧댐")
                    {
                        Ueff = 1 / (1 / Uold + R) + dU;
                    }
                    else
                    {
                        Ueff = Uold;
                    }
                }
                else
                {
                    if (Value[0][2] == "철거 후 신규")
                    {
                        Ueff = 1 / R + dU;
                    }
                    else
                    {
                        Ueff = 1 / (1 / Uold + R) + dU;
                    }

                }
            }
            return Ueff;
        }
        private void Wall_New_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Wall_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Wall_New_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Wall_New_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Wall_New_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = -1;
                for (int k = 0; k < Wall_New_dataGridView.Rows.Count; k++)
                {
                    if (Convert.ToBoolean(Wall_New_dataGridView.Rows[k].Cells[0].Value) == true)
                    {
                        row = k;
                    }
                }
                if (row > -1)
                {
                    string Select = Wall_New_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Wall != "" && SelectAlt_Wall != null)
                    {
                        Load_Graph(Select, SelectAlt_Wall);
                    }
                }
            }

        }
        private void Load_Graph(string SelectNum, string 리모델링안)
        {
            List<Material_Wall> Materials_Wall = new List<Material_Wall>();

            if (SelectNum != "" && SelectNum != null)
            {
                Wall_webView.Visible = true;

                double[] Material_T = new double[12]; //온도
                double Rsi = 0.13, Rse = 0.04;
                double dtot = 0; double Rtot = 0;
                string 직접간접 = "";
                string[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호 = '" + SelectNum + "'");
                if (Load.Length > 0)
                {
                    직접간접 = Load[0][0];
                }

                string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,재료유형,재료,열전도율,두께 from 불투명자재 Where 최적안='" + 리모델링안 + "' Order by ID");
                if (Alt.Length > 0)
                {
                    if (Alt[0][0] == "내부덧댐" || (Alt[0][0] == "외부덧댐" && 직접간접 != "지면"))
                    {
                        for (int a = 0; a < Alt.Length; a++)
                        {
                            if (Alt[a][1] == "기존 외벽") { Materials_Wall.AddRange(Load_Material_OldWall(SelectNum)); }
                            else
                            {
                                string Material_main = Alt[a][1];
                                string Material_sub = Alt[a][2];
                                double Material_d = 0;
                                double Material_R = 0;
                                if (Alt[a][4] != "")
                                {
                                    Material_d = Convert.ToDouble(Alt[a][4]);
                                    if ((Convert.ToDouble(Alt[a][3]) != 0) && Alt[a][1] != "외부마감재")
                                    { Material_R = Convert.ToDouble(Alt[a][4]) / 1000 / Convert.ToDouble(Alt[a][3]); }
                                }
                                string Material_Color = "e1dfdf";
                                if (Alt[a][1] == "단열재") { Material_Color = "FFDB58"; }
                                else if (Alt[a][1] == "공기층") { Material_Color = "DDEBF7"; }
                                Material_Wall w = new Material_Wall(Material_main, Material_sub, Material_d, Material_R, Material_Color);
                                Materials_Wall.Add(w);
                            }
                        }
                    }
                }
                for (int k = 0; k < Materials_Wall.Count; k++)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[k];
                    dtot += w.Material_d();
                    Rtot += w.Material_R();
                }
                Rtot = Rsi + Rse + Rtot;
                double Q = (20 - (-5)) / Rtot;
                Material_T[0] = (20 - Q * Rsi);
                for (int k = 1; k < Materials_Wall.Count + 1; k++)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[k - 1];
                    Material_T[k] = (Material_T[k - 1] - Q * w.Material_R());
                }
                Material_T[Materials_Wall.Count + 1] = Material_T[Materials_Wall.Count] - Q * Rse;
                int i = 0;
                string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 80,\"temper\":  " + Material_T[0] + "},";
                while (++i < Materials_Wall.Count + 1)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[i - 1];
                    var cate = w.Material_main() != null ? w.Material_main() : "---";
                    var color = w.Material_Color() != null ? w.Material_Color() : "DCDCDC";
                    s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + w.Material_d() + ",\"temper\":  " + Material_T[i] + "},";
                }

                s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 80,\"temper\":  " + Material_T[i] + "},";

                runScrip_Wall("drawWall([" + s + "])");
            }
            else
            {
                Wall_webView.Visible = false;
            }
        }
        public List<Material_Wall> Load_Material_OldWall(string SelectNum)
        {
            List<Material_Wall> Materials_OldWall = new List<Material_Wall>();
            String[] Material_main = new String[10];
            String[] Material_sub = new String[10];
            String[] Material_Color = new String[10];
            double[] Material_d = new double[10];//두께
            double[] Material_R = new double[10];
            double[] Material_T = new double[12]; //온도
            string[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall",
                     "재료1종류,재료1두께," +
                     "재료2종류,재료2두께," +
                     "재료3종류,재료3두께," +
                     "재료4종류,재료4두께," +
                     "재료5종류,재료5두께," +
                     "재료6종류,재료6두께," +
                     "재료7종류,재료7두께," +
                     "재료8종류,재료8두께," +
                     "재료9종류,재료9두께," +
                     "재료10종류,재료10두께", "번호 = '" + SelectNum + "'");
            if (Load.Length > 0)
            {
                for (int a = 0; a < 10; a++)
                {
                    Material_sub[a] = Load[0][(2 * a)];
                    Material_d[a] = Convert.ToDouble(Load[0][(2 * a + 1)]);
                }
            }

            for (int a = 0; a < 10; a++)
            {
                if (Material_sub[a] != "")
                {
                    string[][] Value;
                    string[][] OldWall_U;
                    Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material_sub[a] + "'");
                    if (Value.Length == 0)
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율,색상", "재료명 = '" + Material_sub[a] + "'");
                    }
                    if (Value.Length > 0)
                    {
                        if (Convert.ToDouble(Value[0][1]) != 0)
                        { Material_R[a] = Material_d[a] / 1000 / Convert.ToDouble(Value[0][1]); }
                        Material_main[a] = Value[0][0];
                        try
                        { Material_Color[a] = Value[0][2]; }
                        catch { Material_Color[a] = "FFFFFF"; }
                    };
                    Material_Wall w = new Material_Wall(Material_main[a], Material_sub[a], Material_d[a], Material_R[a], Material_Color[a]);
                    Materials_OldWall.Add(w);
                }
            }
            return Materials_OldWall;
        }
        public void runScrip_Wall(string script)
        {
            if (scriptable)
            {
                Wall_webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        #endregion


    }
    public class Material_Wall
    {
        string Material_main_;
        string Material_sub_;
        double Material_d_;
        double Material_R_;
        string Material_Color_;

        public Material_Wall(string Material_main, string Material_sub , double Material_d ,double Material_R, string Material_Color)
        {
            this.Material_main_ = Material_main;
            this.Material_sub_ = Material_sub;
            this.Material_d_ =Material_d;
            this.Material_R_ = Material_R;
            this.Material_Color_ = Material_Color;
        }

        public string Material_main()
        {
            if(Material_main_ != null && Material_main_ != "" && Material_main_ !="기존외벽" && Material_main_ != "기존지붕" && Material_main_ != "기존바닥" && Material_main_ != "덧댐커튼월" && Material_main_ != "공기층" && Material_main_ != "단열재" && Material_main_ != "콘크리트" && Material_main_ != "미장" && Material_main_ != "조적" && Material_main_ != "패널" && Material_main_ != "목재" && Material_main_ != "금속재" && Material_main_ != "타일" && Material_main_ != "지중")
            
            { 
                Material_main_ = "공기층";//그래프 로드되도록 아무 유형 넣음
            }
            
                return Material_main_;
        }

        public string Material_sub()
        {
            return Material_sub_;
        }

        public string Material_Color()
        {
            return Material_Color_;
        }

        public double Material_d()
        {
            return Material_d_;
        }
        public double Material_R()
        {
            return Material_R_;
        }
    }

    public class Material_Roof
    {
        string Material_main_;
        string Material_sub_;
        double Material_d_;
        double Material_R_;
        string Material_Color_;

        public Material_Roof(string Material_main, string Material_sub, double Material_d, double Material_R, string Material_Color)
        {
            this.Material_main_ = Material_main;
            this.Material_sub_ = Material_sub;
            this.Material_d_ = Material_d;
            this.Material_R_ = Material_R;
            this.Material_Color_ = Material_Color;
        }

        public string Material_main()
        {
            return Material_main_;
        }

        public string Material_sub()
        {
            return Material_sub_;
        }

        public string Material_Color()
        {
            return Material_Color_;
        }

        public double Material_d()
        {
            return Material_d_;
        }
        public double Material_R()
        {
            return Material_R_;
        }
    }

}
