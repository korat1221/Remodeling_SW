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
using main.subcontents.ConstructionWall;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using Eagle._Interfaces.Public;

namespace main.contents.Alt
{
    public partial class AltMain : Form
    {
        double MoneyTotal; 
        bool scriptable = false;
        DataGridViewCheckBoxColumn Alt_checkBoxColumn = new DataGridViewCheckBoxColumn();
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
        public void LoadData(String ID)
        {
            create_wall_Old_table();
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
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Alt_dataGridView.Columns.Clear();
            Alt_checkBoxColumn.HeaderText = "선택";
            Alt_checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(Alt_checkBoxColumn);

            Alt_dataGridView.Columns.Add("A1", "순위");
            Alt_dataGridView.Columns.Add("A2", "요소기술");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 60;
        }

        private void Alt_Add_button_Click(object sender, EventArgs e)
        {
            if (Alt_dataGridView.Rows.Count > 1)
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
                RuleResult_dataGridView.Columns.Add("A2", "예상 절감량.[kWh]");
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

        #region 외벽
        private void create_wall_Old_table()
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
                    Wall_Old_dataGridView.Rows[nRow].Cells[2].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][2]));
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
        private void WallAlt_button_Click(object sender, EventArgs e)
        {
            AltWall form = new AltWall("");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }
        #endregion

        private void CostTotal_textBox_TextChanged(object sender, EventArgs e)
        {
            double result;
            if (CostTotal_textBox.Text == null || CostTotal_textBox.Text == "") { }
            else if (double.TryParse(CostTotal_textBox.Text, out result) == true)
            {
                MoneyTotal = Convert.ToDouble(CostTotal_textBox.Text.ToString());
                Calc_NetCost(MoneyTotal);
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        private void Calc_NetCost(double CostTotal)
        {
            double Area = 0; double 일반관리비_비율 = 0, 이윤_비율 = 0;
            double 부가가치세_비율 = 0.1, 재료비_비율 = 0.65, 노무비_비율 = 0.25, 경비_비율 = 0.1;
            double 순공사비=0, 일반관리비 = 0, 이윤 = 0, 공급가액 = 0, 부가가치세 = 0, 폐기물처리비 = 0;
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "외피유형='층간바닥' Or 외피유형='최하층바닥'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    Area += Convert.ToDouble(Value[a][0]);
                }
            }   
            폐기물처리비 = Cal_CostWaste(Area);

            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "공사비비율", "공사비하한,공사비상한,일반관리비,이윤", "");
            if(Value.Length > 0)
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
            }
            부가가치세 = (CostTotal - 폐기물처리비) * 부가가치세_비율 / (1 + 부가가치세_비율);
            공급가액 = (CostTotal - 폐기물처리비) * 1 / (1 + 부가가치세_비율);
            순공사비 = 공급가액 * (재료비_비율 + 노무비_비율 + 경비_비율) / ((재료비_비율 + 노무비_비율 + 경비_비율) * (1 + 일반관리비_비율 + 일반관리비_비율 * 이윤_비율) + 이윤_비율 * (노무비_비율 + 경비_비율));
            일반관리비 = 순공사비 * 일반관리비_비율;
            이윤 = (순공사비 * (노무비_비율 + 경비_비율) + 일반관리비) * 이윤_비율;
            Load_CostTable(순공사비,일반관리비, 이윤, 부가가치세, 폐기물처리비,CostTotal);
        }
        private void Load_CostTable(double 순공사비, double 일반관리비, double 이윤, double 부가가치세,double 폐기물처리비, double 합계 )
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

    }    
}
