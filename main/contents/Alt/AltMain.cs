using main.contentslist;
using main.subcontents.Alt;
using Microsoft.Web.WebView2.Core;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
namespace main.contents.Alt
{
    public partial class AltMain : Form
    {
        string AltNum, AltName;
        double TotalPoint; double TotalSavingPercent;
        double Cost_Total; double Cost_Net = 0;//총공사비, 순공사비
        bool scriptable = false;
        bool scriptable_Wall = false;
        bool scriptable_Roof = false;
        bool scriptable_Floor = false;
        string SelectAlt_Wall;
        string SelectAlt_Roof;
        string SelectAlt_Floor;
        string SelectAlt_Win;
        public AltMain()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView22.Source = new Uri(Program.gPath + "chart_ctrl2.html", true);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            create_Alt_Table();
            tabControl.TabPages.Remove(Wall_tabPage);
            tabControl.TabPages.Remove(Roof_tabPage);
            tabControl.TabPages.Remove(Floor_tabPage);
            tabControl.TabPages.Remove(Win_tabPage);
            Create_Wall_Old_table();
            Create_Roof_Old_table();
            Create_Floor_Old_table();
            Create_Win_Old_table();
        }
        async void InitializeAsync()
        {
            await webView22.EnsureCoreWebView2Async(null);
            webView22.CoreWebView2.NavigationCompleted += OnNaviCompleted;
            await Wall_webView.EnsureCoreWebView2Async(null);
            Wall_webView.CoreWebView2.NavigationCompleted += OnNaviCompleted_Wall;
            await Roof_webView.EnsureCoreWebView2Async(null);
            Roof_webView.CoreWebView2.NavigationCompleted += OnNaviCompleted_Roof;
            await Floor_webView.EnsureCoreWebView2Async(null);
            Floor_webView.CoreWebView2.NavigationCompleted += OnNaviCompleted_Floor;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
            Load_RuleResult();
        }
        void OnNaviCompleted_Wall(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable_Wall = true;
        }
        void OnNaviCompleted_Roof(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable_Roof = true;
        }
        void OnNaviCompleted_Floor(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable_Floor = true;
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
            if (AltNum == null || AltName == null || Cost_Total == 0 || Cost_Net == 0 || TotalPoint == 0)
            {
                MessageBox.Show("필수 입력항목들을 입력하세요.");
            }
            else
            {
                Save();
            }

        }

        public static bool OnLoadListProc(Form form)
        {
            List_Alt f = (List_Alt)form;
            f.load_List();
            return true;
        }
        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "Optimal_Form", "번호,명칭,총공사비,순공사비,종합점수",
                "'" + AltNum + "','" + AltName + "','" + Cost_Total.ToString() + "','" + Cost_Net.ToString() + "','" + TotalPoint.ToString()
                  + "'", "번호");
            string[] 요소기술 = new string[10];
            string[] 리모델링안 = new string[10];
            for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
            {
                요소기술[a] = Alt_dataGridView.Rows[a].Cells[2].Value.ToString();
                리모델링안[a] = Alt_dataGridView.Rows[a].Cells[4].Value.ToString();
            }
            for (int a = 0; a < 10; a++)
            {
                if (요소기술[a] == null) { 요소기술[a] = ""; }
                if (리모델링안[a] == null) { 리모델링안[a] = ""; }
            }
            Program.DB.setValue(DB.type.ProjDB, "Optimal_Form", "번호,요소기술1,리모델링안1,요소기술2,리모델링안2,요소기술3,리모델링안3,요소기술4,리모델링안4,요소기술5,리모델링안5," +
               "요소기술6,리모델링안6,요소기술7,리모델링안7,요소기술8,리모델링안8,요소기술9,리모델링안9,요소기술10,리모델링안10",
                "'" + AltNum + "','" +
                  요소기술[0] + "','" + 리모델링안[0] + "','" +
                  요소기술[1] + "','" + 리모델링안[1] + "','" +
                  요소기술[2] + "','" + 리모델링안[2] + "','" +
                  요소기술[3] + "','" + 리모델링안[3] + "','" +
                  요소기술[4] + "','" + 리모델링안[4] + "','" +
                  요소기술[5] + "','" + 리모델링안[5] + "','" +
                  요소기술[6] + "','" + 리모델링안[6] + "','" +
                  요소기술[7] + "','" + 리모델링안[7] + "','" +
                  요소기술[8] + "','" + 리모델링안[8] + "','" +
                  요소기술[9] + "','" + 리모델링안[9]
                  + "'", "번호");
            
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(58, OnLoadListProc);
        }

        private void reset()
        {
            Alt_dataGridView.Rows.Clear();
            tabControl.TabPages.Remove(Wall_tabPage);
            tabControl.TabPages.Remove(Roof_tabPage);
            tabControl.TabPages.Remove(Floor_tabPage);
            tabControl.TabPages.Remove(Win_tabPage);

        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            AltNum_textBox.Text = ID;
            AltNum = ID;
        }

        public void LoadData(String ID)
        {
            AltNum_textBox.Text = ID;
            AltNum = ID;
            reset();
            Create_Wall_Old_table();
            Create_Roof_Old_table();
            Create_Floor_Old_table();
            Create_Win_Old_table();

            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "Optimal_Form", "명칭,총공사비,순공사비,종합점수", "번호 = '" + AltNum + "'");
            if (Load.Length > 0)
            {
                Name_textBox.Text = Load[0][0];
                AltName = Load[0][0];
                Cost_Total = Program.UTIL.ToDoubleOrZero(Load[0][1]);
                CostTotal_textBox.Text = Cost_Total.ToString();
                Program.UTIL.textBox_doubleComa(CostTotal_textBox, true, 0);
                Program.UTIL.textBox_doubleComa(CostTotal_textBox, true, 0);
                if (Cost_Total > 0)
                {
                    Calc_NetCost(Cost_Total);
                }
            }
            Load = Program.DB.getValue(DB.type.ProjDB, "Optimal_Form", "요소기술1,리모델링안1,요소기술2,리모델링안2,요소기술3,리모델링안3,요소기술4,리모델링안4,요소기술5,리모델링안5," +
                "요소기술6,리모델링안6,요소기술7,리모델링안7,요소기술8,리모델링안8,요소기술9,리모델링안9,요소기술10,리모델링안10", "번호 = '" + AltNum + "'");
            if (Load.Length > 0)
            {
                for (int a = 0; a < 10; a++)
                {
                    if (Load[0][a * 2] != null && Load[0][a * 2] != "")
                    {
                        Add_Alt();
                        Alt_dataGridView.Rows[a].Cells[2].Value = Load[0][a * 2];
                        Alt_dataGridView.Rows[a].Cells[4].Value = Load[0][a * 2 + 1];
                    }
                    else
                    {
                        break;
                    }
                }
                for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
                {
                    if (Alt_dataGridView.Rows[a].Cells[2].Value != null)
                    {
                        if (Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "외벽")
                        {
                            SelectAlt_Wall = Alt_dataGridView.Rows[a].Cells[4].Value.ToString();
                            Create_Wall_New_table(SelectAlt_Wall);
                        }
                        else if (Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "지붕")
                        {
                            SelectAlt_Roof = Alt_dataGridView.Rows[a].Cells[4].Value.ToString();
                            Create_Roof_New_table(SelectAlt_Roof);
                        }
                        else if (Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "최하층바닥")
                        {
                            SelectAlt_Floor = Alt_dataGridView.Rows[a].Cells[4].Value.ToString();
                            Create_Floor_New_table(SelectAlt_Floor);
                        }
                        else if (Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "창호")
                        {
                            SelectAlt_Win = Alt_dataGridView.Rows[a].Cells[4].Value.ToString();
                            Create_Win_New_table(SelectAlt_Win);
                        }
                    }
                }
            }

        }

        private void AltMainPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                AltName = Name_textBox.Text.ToString();
            }
        }

        private void Calc_TotalPoint()
        {
            #region 점수
            double point = 0; int count = 0;
            if (WallPoint_textBox.Text != null && WallPoint_textBox.Text.ToString().Contains(" 점"))
            {
                string a = WallPoint_textBox.Text.ToString().Substring(0, WallPoint_textBox.Text.ToString().IndexOf(" 점"));
                point += Program.UTIL.ToDoubleOrZero(a);
                count = count + 1;
            }
            if (RoofPoint_textBox.Text != null && RoofPoint_textBox.Text.ToString().Contains(" 점"))
            {
                string a = RoofPoint_textBox.Text.ToString().Substring(0, RoofPoint_textBox.Text.ToString().IndexOf(" 점"));
                point += Program.UTIL.ToDoubleOrZero(a);
                count = count + 1;
            }
            if (FloorPoint_textBox.Text != null && FloorPoint_textBox.Text.ToString().Contains(" 점"))
            {
                string a = FloorPoint_textBox.Text.ToString().Substring(0, FloorPoint_textBox.Text.ToString().IndexOf(" 점"));
                point += Program.UTIL.ToDoubleOrZero(a);
                count = count + 1;
            }
            if (WinPoint_textBox.Text != null && WinPoint_textBox.Text.ToString().Contains(" 점"))
            {
                string a = WinPoint_textBox.Text.ToString().Substring(0, WinPoint_textBox.Text.ToString().IndexOf(" 점"));
                point += Program.UTIL.ToDoubleOrZero(a);
                count = count + 1;
            }
            #endregion
          
            if (count > 0)
            {
                TotalPoint_label.Visible = true;
                TotalPoint_textBox.Visible = true;
                TotalPoint = point / count;
                TotalPoint_textBox.Text = TotalPoint.ToString("0.0") + " 점";
            }
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
            Alt_dataGridView.Columns.Add("A5", "예상 순공사비.[천원]");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 50;
            Alt_dataGridView.Columns[2].Width = 70;
            Alt_dataGridView.Columns[3].Width = 30;
            Alt_dataGridView.Columns[4].Width = 150;
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
                            case "지붕":
                                Open_RoofAlt();
                                break;
                            case "최하층바닥":
                                Open_FloorAlt();
                                break;
                            case "창호":
                                Open_WinAlt();
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
                    double saving = Program.UTIL.ToDoubleOrZero(value2[0][0]) - Program.UTIL.ToDoubleOrZero(value[a][1]);
                    if (saving > 0)
                    {
                        Saving_List.Add(saving);
                        Type_List.Add(value[a][0]);
                    }
                }

                string s = "", s2 = "";
                for (int a = 0; a < Saving_List.Count; a++)
                {
                    s2 += Program.UTIL.ToDoubleOrZero(Saving_List[a].ToString("0")) + ",";
                    s += "\"" + Type_List[a] + "\",";
                }

                runScript("drawChart5([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#9bc2e6\",min:0,max:150}],[" + s + "])");

                new StackedHeaderDecorator(RuleResult_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
                RuleResult_dataGridView.Columns.Clear();
                RuleResult_dataGridView.Columns.Add("A0", "순위");
                RuleResult_dataGridView.Columns.Add("A1", "요소기술");
                RuleResult_dataGridView.Columns.Add("A2", "예상 절감량.[kWh/a]");
                RuleResult_dataGridView.Columns.Add("A3", "절감률.[%]");
                RuleResult_dataGridView.Columns[0].Width = 60;
                for (int a = 0; a < Saving_List.Count; a++)
                {
                    int nRow = RuleResult_dataGridView.Rows.Add();
                    RuleResult_dataGridView.Rows[nRow].Cells[0].Value = (a + 1).ToString() + " 순위";
                    RuleResult_dataGridView.Rows[nRow].Cells[1].Value = Type_List[a];
                    RuleResult_dataGridView.Rows[nRow].Cells[2].Value = Saving_List[a].ToString("#,##0");
                    RuleResult_dataGridView.Rows[nRow].Cells[3].Value = (Saving_List[a] / (Program.UTIL.ToDoubleOrZero(value2[0][0]) - Program.UTIL.ToDoubleOrZero(value2[0][1])) * 100).ToString("0.0") + " %";
                    RuleResult_dataGridView.Columns[2].Visible = false;
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
            Cost_Total = Program.UTIL.textBox_doubleComa(CostTotal_textBox, false, 0);
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
                    Area += Program.UTIL.ToDoubleOrZero(Value[a][0]);
                }
            }
            폐기물처리비 = Cal_CostWaste(Area);

            if (폐기물처리비 > CostTotal)
            {
                MessageBox.Show("예상 폐기물처리비(" + (폐기물처리비 / 1000).ToString("#,##0") + "천원) 보다 많은 예산을 입력해주세요.");
            }
            else
            {
                Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "공사비비율", "공사비하한,공사비상한,일반관리비,이윤", "");
                if (Value.Length > 0)
                {
                    for (int a = 0; a < Value.Length; a++)
                    {
                        if (Program.UTIL.ToDoubleOrZero(Value[a][0]) < CostTotal && CostTotal < Program.UTIL.ToDoubleOrZero(Value[a][1]))
                        {
                            일반관리비_비율 = Program.UTIL.ToDoubleOrZero(Value[a][2]);
                            이윤_비율 = Program.UTIL.ToDoubleOrZero(Value[a][3]);
                            break;
                        }
                    }
                    if (일반관리비_비율 == 0 && 이윤_비율 == 0)
                    {
                        일반관리비_비율 = Program.UTIL.ToDoubleOrZero(Value[Value.Length - 1][2]);
                        이윤_비율 = Program.UTIL.ToDoubleOrZero(Value[Value.Length - 1][3]);
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
            Cost_dataGridView.Columns.Add("A1", "예상비용[천원]");

            int nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "순공사비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (순공사비 / 1000).ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "일반관리비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (일반관리비 / 1000).ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "이윤";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (이윤 / 1000).ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "부가가치세";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (부가가치세 / 1000).ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "폐기물처리비";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (폐기물처리비 / 1000).ToString("#,##0");

            nRow = Cost_dataGridView.Rows.Add();
            Cost_dataGridView.Rows[nRow].Cells[0].Value = "합계";
            Cost_dataGridView.Rows[nRow].Cells[1].Value = (합계 / 1000).ToString("#,##0");
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
                        폐기물원단위[0] = Program.UTIL.ToDoubleOrZero(Value[a][1]);
                    }
                    else if (Value[a][0] == "폐금속류")
                    {
                        폐기물원단위[1] = Program.UTIL.ToDoubleOrZero(Value[a][1]);
                    }
                    else
                    {
                        폐기물원단위[2] += Program.UTIL.ToDoubleOrZero(Value[a][1]);
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
                중간처리비[0] = 폐기물발생량[0] * Program.UTIL.ToDoubleOrZero(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='수집운반비' and 폐기물유형='건설폐재류'");
            if (Value.Length > 0)
            {
                수집운반비[0] = 폐기물발생량[0] * Program.UTIL.ToDoubleOrZero(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='중간처리단가' and 폐기물유형='혼합건설폐기물'");
            if (Value.Length > 0)
            {
                중간처리비[2] = 폐기물발생량[2] * Program.UTIL.ToDoubleOrZero(Value[0][0]);
            }
            Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "폐기물적용단가", "적용단가", "비용유형='수집운반비' and 폐기물유형='혼합건설폐기물'");
            if (Value.Length > 0)
            {
                수집운반비[2] = 폐기물발생량[2] * Program.UTIL.ToDoubleOrZero(Value[0][0]);
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
                        sum += Program.UTIL.ToDoubleOrZero(Alt_dataGridView.Rows[a].Cells[5].Value.ToString());
                    }
                }
                BalanceCost_label.Visible = true;
                BalanceCost_textBox.Visible = true;
                BalanceCost_textBox.Text = (순공사비 / 1000 - sum).ToString("#,##0") + " 천원";

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
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Wall_Old_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Wall_Old_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
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
                    Wall_Old_dataGridView.Rows[nRow].Cells[2].Value = Program.UTIL.ToDoubleOrZero(List[n][2]).ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Wall_Old_dataGridView.Rows[nRow].Cells[3].Value = String.Format("{0:F2}", A);
                    }
                }
            }
        }
        private void Create_Wall_New_table(string SelectAlt_Wall)
        {
            tabControl.TabPages.Add(Wall_tabPage);
            new StackedHeaderDecorator(Wall_New_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            DataGridViewCheckBoxColumn Wall_New_checkBoxColumn = new DataGridViewCheckBoxColumn();
            Wall_New_dataGridView.Columns.Clear();
            Wall_New_checkBoxColumn.HeaderText = "선택";
            Wall_New_checkBoxColumn.Name = "check";
            Wall_New_dataGridView.Columns.Add(Wall_New_checkBoxColumn);

            Wall_New_dataGridView.Columns.Add("A1", "번호");
            Wall_New_dataGridView.Columns.Add("A2", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Wall_New_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Wall_New_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
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
                    double Ueff_new = Get_Wall_Ueff(SelectAlt_Wall, Program.UTIL.ToDoubleOrZero(List[n][2]), List[n][3]);
                    Wall_New_dataGridView.Rows[nRow].Cells[3].Value = Ueff_new.ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Wall_New_dataGridView.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", A);
                    }
                }
                tabControl.SelectedTab = tabControl.TabPages["Wall_tabPage"];

            }
            if (Wall_New_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Wall_New_dataGridView.Rows.Count; i++)
                {
                    Wall_New_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Wall_New_dataGridView.Columns.Count > 1 && Wall_New_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Wall_New_dataGridView.Rows[0].Cells[0].Value = true;
                    Wall_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    string Select = Wall_New_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Wall != "" && SelectAlt_Wall != null)
                    {
                        Load_Graph_Wall(Select, SelectAlt_Wall);
                    }
                }
            }
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='외벽' and 리모델링안='" + SelectAlt_Wall + "'");
            if (Value.Length > 0)
            {
                WallCost_textBox.Text = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0") + " 천원";
                WallSavingPercent_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][3]).ToString("0.0") + " %";
                WallPoint_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][4]).ToString("0.0") + " 점";
                Calc_TotalPoint();
            }

            for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
            {
                if (Alt_dataGridView.Rows[a].Cells[2].Value != null && Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "외벽")
                {
                    Alt_dataGridView.Rows[a].Cells[4].Value = SelectAlt_Wall;
                    Alt_dataGridView.Rows[a].Cells[5].Value = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0");
                }
            }
            Cal_BalanceCost(Cost_Net);
            string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='외벽' and a.최적안='" + SelectAlt_Wall + "'");
            if (Value2.Length > 0)
            {
                Wall_new_label.Text = SelectAlt_Wall + ": " + Value2[0][0];
            }

        }
        private double Get_Wall_Ueff(string 리모델링안, double Uold, string 직접간접)
        {
            double Ueff = 0; double dU = 0;
            double R = 0;
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "불투명최적안", "열저항합계, 열교가산치,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                R = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                dU = Program.UTIL.ToDoubleOrZero(Value[0][1]);
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
                        Load_Graph_Wall(Select, SelectAlt_Wall);
                    }
                }
            }

        }
        private void Load_Graph_Wall(string SelectNum, string 리모델링안)
        {
            Wall_webView.Source = new Uri(Program.gPath + "transmit.html", true);
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
                                    Material_d = Program.UTIL.ToDoubleOrZero(Alt[a][4]);
                                    if ((Program.UTIL.ToDoubleOrZero(Alt[a][3]) != 0) && Alt[a][1] != "외부마감재")
                                    { Material_R = Program.UTIL.ToDoubleOrZero(Alt[a][4]) / 1000 / Program.UTIL.ToDoubleOrZero(Alt[a][3]); }
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
                // Material_T[0] = (20 - Q * Rsi)
                Material_T[0] = 20;
                for (int k = 1; k < Materials_Wall.Count + 1; k++)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[k - 1];
                    Material_T[k] = (Material_T[k - 1] - Q * w.Material_R());
                }
                //Material_T[Materials_Wall.Count + 1] = Material_T[Materials_Wall.Count] - Q * Rse;
                Material_T[Materials_Wall.Count] = -5;
                Material_T[Materials_Wall.Count + 1] = -5;
                int i = 0;
                string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[0] + "},";
                while (++i < Materials_Wall.Count + 1)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[i - 1];
                    var cate = w.Material_main() != null ? w.Material_main() : "---";
                    var color = w.Material_Color() != null ? w.Material_Color() : "DCDCDC";
                    s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + w.Material_d() + ",\"temper\":  " + Material_T[i] + "},";
                }

                s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[i] + "},";

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
                    Material_d[a] = Program.UTIL.ToDoubleOrZero(Load[0][(2 * a + 1)]);
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
                        if (Program.UTIL.ToDoubleOrZero(Value[0][1]) != 0)
                        { Material_R[a] = Material_d[a] / 1000 / Program.UTIL.ToDoubleOrZero(Value[0][1]); }
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
            if (scriptable_Wall)
            {
                Wall_webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        #endregion

        #region 지붕
        private void Open_RoofAlt()
        {
            AltRoof form = new AltRoof("");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {

                SelectAlt_Roof = form.SelectName;
                Create_Roof_New_table(SelectAlt_Roof);
            }
        }

        private void RoofCheck_button_Click(object sender, EventArgs e)
        {
            AltRoof form = new AltRoof(SelectAlt_Roof);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }
        private void Create_Roof_Old_table()
        {
            new StackedHeaderDecorator(Roof_Old_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Roof_Old_dataGridView.Columns.Clear();
            Roof_Old_dataGridView.Columns.Add("A0", "번호");
            Roof_Old_dataGridView.Columns.Add("A1", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Roof_Old_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Roof_Old_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
            Roof_Old_dataGridView.Columns[0].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율 From ConstructionRoof as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Roof_Old_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Roof_Old_dataGridView.Rows.Add();
                    Roof_Old_dataGridView.Rows[nRow].Cells[0].Value = List[n][0];
                    Roof_Old_dataGridView.Rows[nRow].Cells[1].Value = List[n][1];
                    Roof_Old_dataGridView.Rows[nRow].Cells[2].Value = Program.UTIL.ToDoubleOrZero(List[n][2]).ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Roof_Old_dataGridView.Rows[nRow].Cells[3].Value = String.Format("{0:F2}", A);
                    }
                }
            }
        }
        private void Create_Roof_New_table(string SelectAlt_Roof)
        {
            tabControl.TabPages.Add(Roof_tabPage);
            new StackedHeaderDecorator(Roof_New_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            DataGridViewCheckBoxColumn Roof_New_checkBoxColumn = new DataGridViewCheckBoxColumn();
            Roof_New_dataGridView.Columns.Clear();
            Roof_New_checkBoxColumn.HeaderText = "선택";
            Roof_New_checkBoxColumn.Name = "check";
            Roof_New_dataGridView.Columns.Add(Roof_New_checkBoxColumn);

            Roof_New_dataGridView.Columns.Add("A1", "번호");
            Roof_New_dataGridView.Columns.Add("A2", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Roof_New_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Roof_New_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
            Roof_New_dataGridView.Columns[0].Width = 40;
            Roof_New_dataGridView.Columns[1].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율,a.직접간접 From ConstructionRoof as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Roof_New_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Roof_New_dataGridView.Rows.Add();
                    Roof_New_dataGridView.Rows[nRow].Cells[1].Value = List[n][0];
                    Roof_New_dataGridView.Rows[nRow].Cells[2].Value = List[n][1];
                    double Ueff_new = Get_Roof_Ueff(SelectAlt_Roof, Program.UTIL.ToDoubleOrZero(List[n][2]), List[n][3]);
                    Roof_New_dataGridView.Rows[nRow].Cells[3].Value = Ueff_new.ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Roof_New_dataGridView.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", A);
                    }
                }
                tabControl.SelectedTab = tabControl.TabPages["Roof_tabPage"];
            }
            if (Roof_New_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Roof_New_dataGridView.Rows.Count; i++)
                {
                    Roof_New_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Roof_New_dataGridView.Columns.Count > 1 && Roof_New_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Roof_New_dataGridView.Rows[0].Cells[0].Value = true;
                    Roof_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    string Select = Roof_New_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Roof != "" && SelectAlt_Roof != null)
                    {
                        Load_Graph_Roof(Select, SelectAlt_Roof);
                    }
                }
            }
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='지붕' and 리모델링안='" + SelectAlt_Roof + "'");
            if (Value.Length > 0)
            {
                RoofCost_textBox.Text = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0") + " 천원";
                RoofSavingPercent_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][3]).ToString("0.0") + " %";
                RoofPoint_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][4]).ToString("0.0") + " 점";
                Calc_TotalPoint();
            }

            for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
            {
                if (Alt_dataGridView.Rows[a].Cells[2].Value != null && Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "지붕")
                {
                    Alt_dataGridView.Rows[a].Cells[4].Value = SelectAlt_Roof;
                    Alt_dataGridView.Rows[a].Cells[5].Value = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0");
                }
            }
            Cal_BalanceCost(Cost_Net);
            string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='지붕' and a.최적안='" + SelectAlt_Roof + "'");
            if (Value2.Length > 0)
            {
                Roof_new_label.Text = SelectAlt_Roof + ": " + Value2[0][0];
            }
        }
        private double Get_Roof_Ueff(string 리모델링안, double Uold, string 직접간접)
        {
            double Ueff = 0; double dU = 0;
            double R = 0;
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "불투명최적안", "열저항합계, 열교가산치,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                R = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                dU = Program.UTIL.ToDoubleOrZero(Value[0][1]);
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
        private void Roof_New_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Roof_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Roof_New_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Roof_New_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Roof_New_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = -1;
                for (int k = 0; k < Roof_New_dataGridView.Rows.Count; k++)
                {
                    if (Convert.ToBoolean(Roof_New_dataGridView.Rows[k].Cells[0].Value) == true)
                    {
                        row = k;
                    }
                }
                if (row > -1)
                {
                    string Select = Roof_New_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Roof != "" && SelectAlt_Roof != null)
                    {
                        Load_Graph_Roof(Select, SelectAlt_Roof);
                    }
                }
            }

        }
        private void Load_Graph_Roof(string SelectNum, string 리모델링안)
        {
            Roof_webView.Source = new Uri(Program.gPath + "transmit.html?type=roof", true);

            List<Material_Roof> Materials_Roof = new List<Material_Roof>();

            if (SelectNum != "" && SelectNum != null)
            {
                Roof_webView.Visible = true;

                double[] Material_T = new double[12]; //온도
                double Rsi = 0.13, Rse = 0.04;
                double dtot = 0; double Rtot = 0;


                string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,재료유형,재료,열전도율,두께 from 불투명자재 Where 최적안='" + 리모델링안 + "' Order by ID");
                if (Alt.Length > 0)
                {
                    for (int a = 0; a < Alt.Length; a++)
                    {
                        if (Alt[a][1] == "기존 지붕") { Materials_Roof.AddRange(Load_Material_OldRoof(SelectNum)); }
                        else
                        {
                            string Material_main = Alt[a][1];
                            string Material_sub = Alt[a][2];
                            double Material_d = 0;
                            double Material_R = 0;
                            if (Alt[a][4] != "")
                            {
                                Material_d = Program.UTIL.ToDoubleOrZero(Alt[a][4]);
                                if ((Program.UTIL.ToDoubleOrZero(Alt[a][3]) != 0) && Alt[a][1] != "외부마감재")
                                { Material_R = Program.UTIL.ToDoubleOrZero(Alt[a][4]) / 1000 / Program.UTIL.ToDoubleOrZero(Alt[a][3]); }
                            }
                            string Material_Color = "e1dfdf";
                            if (Alt[a][1] == "단열재") { Material_Color = "FFDB58"; }
                            else if (Alt[a][1] == "공기층") { Material_Color = "DDEBF7"; }
                            Material_Roof w = new Material_Roof(Material_main, Material_sub, Material_d, Material_R, Material_Color);
                            Materials_Roof.Add(w);
                        }
                    }
                }
                for (int k = 0; k < Materials_Roof.Count; k++)
                {
                    Material_Roof w = (Material_Roof)Materials_Roof[k];
                    dtot += w.Material_d();
                    Rtot += w.Material_R();
                }
                Rtot = Rsi + Rse + Rtot;
                double Q = (20 - (-5)) / Rtot;
                //  Material_T[0] = (20 - Q * Rsi);
                Material_T[0] = 20;
                for (int k = 1; k < Materials_Roof.Count + 1; k++)
                {
                    Material_Roof w = (Material_Roof)Materials_Roof[k - 1];
                    Material_T[k] = (Material_T[k - 1] - Q * w.Material_R());
                }
                // Material_T[Materials_Roof.Count + 1] = Material_T[Materials_Roof.Count] - Q * Rse;
                Material_T[Materials_Roof.Count] = -5;
                Material_T[Materials_Roof.Count + 1] = -5;
                int i = 0;
                string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[0] + "},";
                while (++i < Materials_Roof.Count + 1)
                {
                    Material_Roof w = (Material_Roof)Materials_Roof[i - 1];
                    var cate = w.Material_main() != null ? w.Material_main() : "---";
                    var color = w.Material_Color() != null ? w.Material_Color() : "DCDCDC";
                    s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + w.Material_d() + ",\"temper\":  " + Material_T[i] + "},";
                }

                s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[i] + "},";

                runScrip_Roof("drawWall([" + s + "])");
            }
            else
            {
                Roof_webView.Visible = false;
            }
        }
        public List<Material_Roof> Load_Material_OldRoof(string SelectNum)
        {
            List<Material_Roof> Materials_OldRoof = new List<Material_Roof>();
            String[] Material_main = new String[10];
            String[] Material_sub = new String[10];
            String[] Material_Color = new String[10];
            double[] Material_d = new double[10];//두께
            double[] Material_R = new double[10];
            double[] Material_T = new double[12]; //온도
            string[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof",
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
                    Material_d[a] = Program.UTIL.ToDoubleOrZero(Load[0][(2 * a + 1)]);
                }
            }

            for (int a = 0; a < 10; a++)
            {
                if (Material_sub[a] != "")
                {
                    string[][] Value;
                    string[][] OldRoof_U;
                    Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material_sub[a] + "'");
                    if (Value.Length == 0)
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율,색상", "재료명 = '" + Material_sub[a] + "'");
                    }
                    if (Value.Length > 0)
                    {
                        if (Program.UTIL.ToDoubleOrZero(Value[0][1]) != 0)
                        { Material_R[a] = Material_d[a] / 1000 / Program.UTIL.ToDoubleOrZero(Value[0][1]); }
                        Material_main[a] = Value[0][0];
                        try
                        { Material_Color[a] = Value[0][2]; }
                        catch { Material_Color[a] = "FFFFFF"; }
                    };
                    Material_Roof w = new Material_Roof(Material_main[a], Material_sub[a], Material_d[a], Material_R[a], Material_Color[a]);
                    Materials_OldRoof.Add(w);
                }
            }
            return Materials_OldRoof;
        }
        public void runScrip_Roof(string script)
        {
            if (scriptable_Roof)
            {
                Roof_webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        #endregion


        #region 최하층바닥
        private void Open_FloorAlt()
        {
            AltFloor form = new AltFloor("");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {

                SelectAlt_Floor = form.SelectName;
                Create_Floor_New_table(SelectAlt_Floor);
            }
        }

        private void FloorCheck_button_Click(object sender, EventArgs e)
        {
            AltFloor form = new AltFloor(SelectAlt_Floor);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }
        private void Create_Floor_Old_table()
        {
            new StackedHeaderDecorator(Floor_Old_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Floor_Old_dataGridView.Columns.Clear();
            Floor_Old_dataGridView.Columns.Add("A0", "번호");
            Floor_Old_dataGridView.Columns.Add("A1", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Floor_Old_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Floor_Old_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
            Floor_Old_dataGridView.Columns[0].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율 From ConstructionFloor as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Floor_Old_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Floor_Old_dataGridView.Rows.Add();
                    Floor_Old_dataGridView.Rows[nRow].Cells[0].Value = List[n][0];
                    Floor_Old_dataGridView.Rows[nRow].Cells[1].Value = List[n][1];
                    Floor_Old_dataGridView.Rows[nRow].Cells[2].Value = Program.UTIL.ToDoubleOrZero(List[n][2]).ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Floor_Old_dataGridView.Rows[nRow].Cells[3].Value = String.Format("{0:F2}", A);
                    }
                }
            }
        }
        private void Create_Floor_New_table(string SelectAlt_Floor)
        {
            tabControl.TabPages.Add(Floor_tabPage);
            new StackedHeaderDecorator(Floor_New_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            DataGridViewCheckBoxColumn Floor_New_checkBoxColumn = new DataGridViewCheckBoxColumn();
            Floor_New_dataGridView.Columns.Clear();
            Floor_New_checkBoxColumn.HeaderText = "선택";
            Floor_New_checkBoxColumn.Name = "check";
            Floor_New_dataGridView.Columns.Add(Floor_New_checkBoxColumn);

            Floor_New_dataGridView.Columns.Add("A1", "번호");
            Floor_New_dataGridView.Columns.Add("A2", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Floor_New_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Floor_New_dataGridView.Columns.Add("A3", "면적.[" + unit + "]");
            Floor_New_dataGridView.Columns[0].Width = 40;
            Floor_New_dataGridView.Columns[1].Width = 40;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.명칭,a.유효열관류율,a.기초설치 From ConstructionFloor as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                Floor_New_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Floor_New_dataGridView.Rows.Add();
                    Floor_New_dataGridView.Rows[nRow].Cells[1].Value = List[n][0];
                    Floor_New_dataGridView.Rows[nRow].Cells[2].Value = List[n][1];
                    double Ueff_new = Get_Floor_Ueff(SelectAlt_Floor, Program.UTIL.ToDoubleOrZero(List[n][2]), List[n][3]);
                    Floor_New_dataGridView.Rows[nRow].Cells[3].Value = Ueff_new.ToString("0.00");
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Program.UTIL.ToDoubleOrZero(Area[a][0]);
                        }
                        Floor_New_dataGridView.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", A);
                    }
                }
                tabControl.SelectedTab = tabControl.TabPages["Floor_tabPage"];
            }
            if (Floor_New_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Floor_New_dataGridView.Rows.Count; i++)
                {
                    Floor_New_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Floor_New_dataGridView.Columns.Count > 1 && Floor_New_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Floor_New_dataGridView.Rows[0].Cells[0].Value = true;
                    Floor_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    string Select = Floor_New_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Floor != "" && SelectAlt_Floor != null)
                    {
                        Load_Graph_Floor(Select, SelectAlt_Floor);
                    }
                }
            }
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='최하층바닥' and 리모델링안='" + SelectAlt_Floor + "'");
            if (Value.Length > 0)
            {
                FloorCost_textBox.Text = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0") + " 천원";
                FloorSavingPercent_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][3]).ToString("0.0") + " %";
                FloorPoint_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][4]).ToString("0.0") + " 점";
                Calc_TotalPoint();
            }

            for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
            {
                if (Alt_dataGridView.Rows[a].Cells[2].Value != null && Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "최하층바닥")
                {
                    Alt_dataGridView.Rows[a].Cells[4].Value = SelectAlt_Floor;
                    Alt_dataGridView.Rows[a].Cells[5].Value = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0");
                }
            }
            Cal_BalanceCost(Cost_Net);
            string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='최하층바닥' and a.최적안='" + SelectAlt_Floor + "'");
            if (Value2.Length > 0)
            {
                Floor_new_label.Text = SelectAlt_Floor + ": " + Value2[0][0];
            }
        }
        private double Get_Floor_Ueff(string 리모델링안, double Uold, string 기초설치)
        {
            double Ueff = Uold; double dU = 0;
            double R = 0;
            string 리모델링유형 = "";
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "불투명최적안", "열저항합계, 열교가산치,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                R = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                dU = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                리모델링유형 = Value[0][2];
                if (리모델링유형 == "내부덧댐" || (리모델링유형 == "외부덧댐" && 기초설치 == "바닥(외기)"))
                {
                    Ueff = 1 / (1 / Uold + R) + dU;
                }
            }
            return Ueff;
        }
        private void Floor_New_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Floor_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Floor_New_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Floor_New_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Floor_New_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = -1;
                for (int k = 0; k < Floor_New_dataGridView.Rows.Count; k++)
                {
                    if (Convert.ToBoolean(Floor_New_dataGridView.Rows[k].Cells[0].Value) == true)
                    {
                        row = k;
                    }
                }
                if (row > -1)
                {
                    string Select = Floor_New_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Floor != "" && SelectAlt_Floor != null)
                    {
                        Load_Graph_Floor(Select, SelectAlt_Floor);
                    }
                }
            }
        }

        private void Load_Graph_Floor(string SelectNum, string 리모델링안)
        {
            Floor_webView.Source = new Uri(Program.gPath + "transmit.html?type=floor", true);

            List<Material_Floor> Materials_Floor = new List<Material_Floor>();

            if (SelectNum != "" && SelectNum != null)
            {
                Floor_webView.Visible = true;

                double[] Material_T = new double[12]; //온도
                double Rsi = 0.13, Rse = 0.04;
                double dtot = 0; double Rtot = 0;

                string[][] old = Program.DB.querySQL(DB.type.ProjDB, "Select 기초설치 From ConstructionFloor where 번호='" + SelectNum + "'");
                string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,재료유형,재료,열전도율,두께 from 불투명자재 Where 최적안='" + 리모델링안 + "' Order by ID");
                if (Alt.Length > 0 && old.Length > 0)
                {
                    for (int a = 0; a < Alt.Length; a++)
                    {
                        if (Alt[a][1] == "기존 바닥") { Materials_Floor.AddRange(Load_Material_OldFloor(SelectNum)); }
                        else if (Alt[a][0] == "내부덧댐" || (Alt[a][0] == "외부덧댐" && old[0][0] == "바닥(외기)"))
                        {
                            string Material_main = Alt[a][1];
                            string Material_sub = Alt[a][2];
                            double Material_d = 0;
                            double Material_R = 0;
                            if (Alt[a][4] != "")
                            {
                                Material_d = Program.UTIL.ToDoubleOrZero(Alt[a][4]);
                                if ((Program.UTIL.ToDoubleOrZero(Alt[a][3]) != 0) && Alt[a][1] != "외부마감재")
                                { Material_R = Program.UTIL.ToDoubleOrZero(Alt[a][4]) / 1000 / Program.UTIL.ToDoubleOrZero(Alt[a][3]); }
                            }
                            string Material_Color = "e1dfdf";
                            if (Alt[a][1] == "단열재") { Material_Color = "FFDB58"; }
                            else if (Alt[a][1] == "공기층") { Material_Color = "DDEBF7"; }
                            Material_Floor w = new Material_Floor(Material_main, Material_sub, Material_d, Material_R, Material_Color);
                            Materials_Floor.Add(w);
                        }
                    }
                }
                for (int k = 0; k < Materials_Floor.Count; k++)
                {
                    Material_Floor w = (Material_Floor)Materials_Floor[k];
                    dtot += w.Material_d();
                    Rtot += w.Material_R();
                }
                Rtot = Rsi + Rse + Rtot;
                double Q = (20 - (-5)) / Rtot;
                //  Material_T[0] = (20 - Q * Rsi);
                Material_T[0] = 20;
                for (int k = 1; k < Materials_Floor.Count + 1; k++)
                {
                    Material_Floor w = (Material_Floor)Materials_Floor[k - 1];
                    Material_T[k] = (Material_T[k - 1] - Q * w.Material_R());
                }
                //Material_T[Materials_Floor.Count + 1] = Material_T[Materials_Floor.Count] - Q * Rse;
                Material_T[Materials_Floor.Count] = -5;
                Material_T[Materials_Floor.Count + 1] = -5;
                int i = 0;
                int count = Materials_Floor.Count + 1;
                string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + Material_T[count] + "},";
                while (++i < count)
                {
                    Material_Floor w = (Material_Floor)Materials_Floor[-i + count - 1];
                    var cate = w.Material_main() != null ? w.Material_main() : "---";
                    var color = w.Material_Color() != null ? w.Material_Color() : "DCDCDC";
                    s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + w.Material_d() + ",\"temper\":  " + Material_T[-i + count - 1] + "},";
                }

                s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 50,\"temper\":  " + "20" + "},";

                runScrip_Floor("drawWall([" + s + "])");
            }
            else
            {
                Floor_webView.Visible = false;
            }
        }
        public List<Material_Floor> Load_Material_OldFloor(string SelectNum)
        {
            List<Material_Floor> Materials_OldFloor = new List<Material_Floor>();
            String[] Material_main = new String[10];
            String[] Material_sub = new String[10];
            String[] Material_Color = new String[10];
            double[] Material_d = new double[10];//두께
            double[] Material_R = new double[10];
            double[] Material_T = new double[12]; //온도
            string[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor",
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
                    Material_d[a] = Program.UTIL.ToDoubleOrZero(Load[0][(2 * a + 1)]);
                }
            }

            for (int a = 0; a < 10; a++)
            {
                if (Material_sub[a] != "")
                {
                    string[][] Value;
                    string[][] OldFloor_U;
                    Value = Program.DB.getValue(DB.type.ProjDB, "User_Material", "구분,열전도율", "재료명 = '" + Material_sub[a] + "'");
                    if (Value.Length == 0)
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분,열전도율,색상", "재료명 = '" + Material_sub[a] + "'");
                    }
                    if (Value.Length > 0)
                    {
                        if (Program.UTIL.ToDoubleOrZero(Value[0][1]) != 0)
                        { Material_R[a] = Material_d[a] / 1000 / Program.UTIL.ToDoubleOrZero(Value[0][1]); }
                        Material_main[a] = Value[0][0];
                        try
                        { Material_Color[a] = Value[0][2]; }
                        catch { Material_Color[a] = "FFFFFF"; }
                    };
                    Material_Floor w = new Material_Floor(Material_main[a], Material_sub[a], Material_d[a], Material_R[a], Material_Color[a]);
                    Materials_OldFloor.Add(w);
                }
            }
            return Materials_OldFloor;
        }
        public void runScrip_Floor(string script)
        {
            if (scriptable_Floor)
            {
                Floor_webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        #endregion

        #region 창호
        private void Open_WinAlt()
        {
            AltWin form = new AltWin("");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {

                SelectAlt_Win = form.SelectName;
                Create_Win_New_table(SelectAlt_Win);
            }
        }

        private void Win_New_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Win_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Win_New_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Win_New_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Win_New_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = -1;
                for (int k = 0; k < Win_New_dataGridView.Rows.Count; k++)
                {
                    if (Convert.ToBoolean(Win_New_dataGridView.Rows[k].Cells[0].Value) == true)
                    {
                        row = k;
                    }
                }
                if (row > -1)
                {
                    string Select = Win_New_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Win != "" && SelectAlt_Win != null)
                    {
                        Load_Image_Win(Select, SelectAlt_Win, row);
                    }
                }
            }
        }

        private void Load_Image_Win(string SelectNum, string 리모델링안, int row)
        {
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형 From 투명최적안 where 구조체='창호' and 최적안='" + 리모델링안 + "'");
            if (Value.Length > 0)
            {
                string WinRemodelingType = Value[0][0];
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + WinRemodelingType + "'");
                if (Image.Length > 0)
                {
                    WindowType_pictureBox.Visible = true;
                    WindowType_pictureBox.Load(Program.gPath + Image[0][0]);
                    WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }

                Ueff_textBox.Text = Win_New_dataGridView.Rows[row].Cells[3].Value.ToString();
                Program.UTIL.textBox_doubleComa(Ueff_textBox, true, 2);
                dU_textBox.Text = Win_New_dataGridView.Rows[row].Cells[6].Value.ToString();
                Program.UTIL.textBox_doubleComa(dU_textBox, true, 2);
            }
        }
        private void WinCheck_button_Click(object sender, EventArgs e)
        {
            AltWin form = new AltWin(SelectAlt_Win);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }
        private void Create_Win_Old_table()
        {
            new StackedHeaderDecorator(Win_Old_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Win_Old_dataGridView.Columns.Clear();
            Win_Old_dataGridView.Columns.Add("A0", "번호");
            Win_Old_dataGridView.Columns.Add("A1", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Win_Old_dataGridView.Columns.Add("A2", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Win_Old_dataGridView.Columns.Add("A3", "면적합.[" + unit + "]");
            Win_Old_dataGridView.Columns.Add("A4", "개수.[EA]");
            Win_Old_dataGridView.Columns[0].Width = 50;
            Win_Old_dataGridView.Columns[4].Width = 50;

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.창호명칭 From ConstructionWindow as a  Inner Join SubWindow as b on a.번호=b.상위창호번호 Inner Join ZoneEnvelope_3D as c on b.번호=c.구조체번호");
            if (List.Length > 0)
            {
                Win_Old_dataGridView.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = Win_Old_dataGridView.Rows.Add();
                    Win_Old_dataGridView.Rows[nRow].Cells[0].Value = List[n][0];
                    Win_Old_dataGridView.Rows[nRow].Cells[1].Value = List[n][1];
                    string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select a.창호유효열관류율,b.면적 From SubWindow as a Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호 where a.상위창호번호='" + List[n][0] + "'");
                    if (value.Length > 0)
                    {
                        double Ueff = 0;
                        double Area_sum = 0;
                        for (int a = 0; a < value.Length; a++)
                        {
                            Ueff += Program.UTIL.ToDoubleOrZero(value[a][0]) * Program.UTIL.ToDoubleOrZero(value[a][1]);
                            Area_sum += Program.UTIL.ToDoubleOrZero(value[a][1]);
                        }
                        Ueff = Ueff / Area_sum;
                        Win_Old_dataGridView.Rows[nRow].Cells[2].Value = Ueff.ToString("0.00");
                        Win_Old_dataGridView.Rows[nRow].Cells[3].Value = Area_sum.ToString("0.00");
                        Win_Old_dataGridView.Rows[nRow].Cells[4].Value = value.Length;
                    }
                }
            }
        }

        private void Create_Win_New_table(string SelectAlt_Win)
        {
            tabControl.TabPages.Add(Win_tabPage);
            tabControl.SelectedTab = tabControl.TabPages["Win_tabPage"];

            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='창호' and 리모델링안='" + SelectAlt_Win + "'");
            if (Value.Length > 0)
            {
                Win_new_label.Text = SelectAlt_Win;
                WinCost_textBox.Text = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0") + " 천원";
                WinSavingPercent_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][3]).ToString("0.0") + " %";
                WinPoint_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][4]).ToString("0.0") + " 점";
                Calc_TotalPoint();

                for (int a = 0; a < Alt_dataGridView.Rows.Count; a++)
                {
                    if (Alt_dataGridView.Rows[a].Cells[2].Value != null && Alt_dataGridView.Rows[a].Cells[2].Value.ToString() == "창호")
                    {
                        Alt_dataGridView.Rows[a].Cells[4].Value = SelectAlt_Win;
                        Alt_dataGridView.Rows[a].Cells[5].Value = (Program.UTIL.ToDoubleOrZero(Value[0][1]) / 1000).ToString("#,##0");
                    }
                }
                Cal_BalanceCost(Cost_Net);
            }

            //테이블 생성 
            new StackedHeaderDecorator(Win_New_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            DataGridViewCheckBoxColumn Win_New_checkBoxColumn = new DataGridViewCheckBoxColumn();
            Win_New_dataGridView.Columns.Clear();
            Win_New_checkBoxColumn.HeaderText = "선택";
            Win_New_checkBoxColumn.Name = "check";
            Win_New_dataGridView.Columns.Add(Win_New_checkBoxColumn);

            Win_New_dataGridView.Columns.Add("A1", "번호");
            Win_New_dataGridView.Columns.Add("A2", "명칭");
            string unit = "W/m" + Program.UTIL.Subscript(2, true) + "·K";
            Win_New_dataGridView.Columns.Add("A3", "유효열관류율.[" + unit + "]");
            unit = "m" + Program.UTIL.Subscript(2, true);
            Win_New_dataGridView.Columns.Add("A4", "면적합.[" + unit + "]");
            Win_New_dataGridView.Columns.Add("A5", "개수.[EA]");
            Win_New_dataGridView.Columns.Add("A6", "설치열교가산치.[" + unit + "]");
            Win_New_dataGridView.Columns[6].Visible = false;
            Win_New_dataGridView.Columns[0].Width = 40;
            Win_New_dataGridView.Columns[1].Width = 50;
            Win_New_dataGridView.Columns[5].Width = 50;
            string[][] ALT = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형 From 투명최적안 where 구조체='창호' and 최적안='" + SelectAlt_Win + "'");
            if (ALT.Length > 0)
            {
                string WinRemodelingType = ALT[0][0];
                string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,a.창호명칭 From ConstructionWindow as a  Inner Join SubWindow as b on a.번호=b.상위창호번호 Inner Join ZoneEnvelope_3D as c on b.번호=c.구조체번호");
                if (List.Length > 0)
                {
                    Win_New_dataGridView.Rows.Clear();
                    for (int n = 0; n < List.Length; n++)
                    {
                        int nRow = Win_New_dataGridView.Rows.Add();
                        Win_New_dataGridView.Rows[nRow].Cells[1].Value = List[n][0];
                        Win_New_dataGridView.Rows[nRow].Cells[2].Value = List[n][1];
                        string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select b.번호,b.면적 From SubWindow as a Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호 where a.상위창호번호='" + List[n][0] + "'");
                        if (value.Length > 0)
                        {
                            double Ueff = 0; double dU = 0, g = 0, tao = 0;
                            double Area_sum = 0;
                            for (int a = 0; a < value.Length; a++)
                            {
                                string 외피번호 = value[a][0];

                                double[] result = Cal_Win_Ueff(SelectAlt_Win, WinRemodelingType, 외피번호);////유효열관류율, 태양열취득률, 빛투과율, 설치열교가산치
                                Ueff += result[0] * Program.UTIL.ToDoubleOrZero(value[a][1]);
                                g = result[1];
                                tao = result[2];
                                dU += result[3] * Program.UTIL.ToDoubleOrZero(value[a][1]);
                                Area_sum += Program.UTIL.ToDoubleOrZero(value[a][1]);
                            }
                            Ueff = Ueff / Area_sum;
                            dU = dU / Area_sum;
                            g_textBox.Text = g.ToString();
                            Program.UTIL.textBox_doubleComa(g_textBox, true, 3);
                            tao_textBox.Text = tao.ToString();
                            Program.UTIL.textBox_doubleComa(tao_textBox, true, 3);
                            Win_New_dataGridView.Rows[nRow].Cells[3].Value = Ueff.ToString("0.00");
                            Win_New_dataGridView.Rows[nRow].Cells[4].Value = Area_sum.ToString("0.00");
                            Win_New_dataGridView.Rows[nRow].Cells[5].Value = value.Length;
                            Win_New_dataGridView.Rows[nRow].Cells[6].Value = dU.ToString("0.00");
                        }
                    }
                }
            }

            if (Win_New_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Win_New_dataGridView.Rows.Count; i++)
                {
                    Win_New_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Win_New_dataGridView.Columns.Count > 1 && Win_New_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Win_New_dataGridView.Rows[0].Cells[0].Value = true;
                    Win_New_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    string Select = Win_New_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (Select != null && Select != "" && SelectAlt_Win != "" && SelectAlt_Win != null)
                    {
                        Load_Image_Win(Select, SelectAlt_Win, 0);
                    }
                }
            }
        }


        private double[] Cal_Win_Ueff(string 리모델링안, string WinRemodelingType, string 외피번호)
        {
            double[] result = new double[4]; //유효열관류율, 태양열취득률, 빛투과율, 설치열교가산치
            double Uw = 0, dU = 0, Ueff = 0;
            double[] WinValue = LoadData_Win(리모델링안);
            double ug = WinValue[0], g = WinValue[1], tao = WinValue[2];
            double Uf_open = WinValue[3], Uf_fix = WinValue[4], Uf_btw = WinValue[5];
            double Psi_g_fix = WinValue[6], Psi_g_open = WinValue[7];
            double Psi_InstallTop = WinValue[8], Psi_InstallSide = WinValue[9], Psi_InstallButtom = WinValue[10];
            String[][] ZoneWin = Program.DB.querySQL(DB.type.ProjDB, "select a.번호 As 번호a ,a.면적,b.번호 As 번호b ,b.창호열관류율,b.설치열교가산치,b.창호유효열관류율,b.유리면적비,b.상위창호번호,a.방위,a.기울기 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체번호 = b.번호  Where a.번호='" + 외피번호 + "'");
            if (ZoneWin.Length > 0)
            {
                String[][] ZoneWin_P = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "직접간접,태양열취득률,빛투과율", "번호='" + ZoneWin[0][7] + "'");
                if (ZoneWin_P.Length > 0)
                {
                    string[][] size = Program.DB.querySQL(DB.type.ProjDB, "Select  a.창호면적,a.창호너비,a.창호높이,a.고정유리면적,a.개폐유리면적,a.개폐프레임면적,a.고정프레임면적,a.중간프레임면적,a.고정유리둘레길이,a.개폐유리둘레길이,a.유리면적비 FROM SubWindow AS a INNER JOIN ZoneEnvelope_3D AS b ON b.구조체번호 = a.번호 where b.번호 = '" + ZoneWin[0][0] + "'");
                    if (size.Length > 0)
                    {
                        double NewUw = Calc_Uw(size, ug, Uf_open, Uf_fix, Uf_btw, Psi_g_fix, Psi_g_open);
                        double Newg = WinValue[1];
                        double Newtao = WinValue[2];
                        if (WinRemodelingType == "내부덧댐")
                        {
                            double[] v = Calc_AdditionalWindow(NewUw, Program.UTIL.ToDoubleOrZero(ZoneWin[0][3]), Newg, Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][1]), Newtao, Program.UTIL.ToDoubleOrZero(ZoneWin_P[0][2])); //double NewUw, double OldUw, double Newg, double Oldg, double Newtao, double Oldtao
                            Uw = v[0]; g = v[1]; tao = v[2];
                        }
                        else
                        {
                            Uw = NewUw; g = Newg; tao = Newtao;
                        }
                        dU = Calc_dUinst(size, Psi_InstallTop, Psi_InstallButtom, Psi_InstallSide);
                        Ueff = Uw + dU;
                    }
                }
            }
            result[0] = Ueff;
            result[1] = g;
            result[2] = tao;
            result[3] = dU;
            return result;
        }
        private double[] LoadData_Win(string 리모델링안)
        {
            double[] WinValue = new double[11];
            double ug = 0, g = 0, tao = 0;
            double Uf_open = 0, Uf_fix = 0, Uf_btw = 0;
            double Psi_g_fix = 0, Psi_g_open = 0;
            double Psi_InstallTop = 0, Psi_InstallSide = 0, Psi_InstallButtom = 0;
            string[][] value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안구분,프레임,유리 From 투명최적안 where 최적안='" + 리모델링안 + "' and 구조체='창호'");
            if (value.Length > 0)
            {
                string 프레임재료 = value[0][1]; string 단창이중창 = "단창"; string 유리 = value[0][2];
                if (value[0][0] == "이중창_SL") { 단창이중창 = "이중창"; 유리 = "LE/12R/CL"; }
                if (value[0][1] == "금속_단열바") { 프레임재료 = "금속"; }
                string[][] frameValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율  From 창호프레임  where 프레임종류='" + value[0][0] + "' and 프레임재료='" + value[0][1] + "' and DB유형='표준'");
                if (frameValue.Length > 0)
                {
                    Uf_open = Program.UTIL.ToDoubleOrZero(frameValue[0][0]); Uf_fix = Program.UTIL.ToDoubleOrZero(frameValue[0][1]); Uf_btw = Program.UTIL.ToDoubleOrZero(frameValue[0][2]);
                }
                string[][] glassValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율  From 유리  where 제품명='" + 유리 + "'and DB유형='표준'");
                if (glassValue.Length > 0)
                {
                    ug = Program.UTIL.ToDoubleOrZero(glassValue[0][7]); g = Program.UTIL.ToDoubleOrZero(glassValue[0][8]); tao = Program.UTIL.ToDoubleOrZero(glassValue[0][9]);
                }
                if (value[0][0] == "이중창_SL")
                {
                    double[] v = Calc_DoubleGlass(glassValue); ug = v[0]; g = v[1]; tao = v[2];
                }
                string[][] TBValue = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율  From 창호설치열교  where 구분1='외단열'and 구분2='" + 프레임재료 + "'and 구분3='" + 단창이중창 + "'and 구분4='외부측'");
                if (TBValue.Length > 0)
                {
                    Psi_InstallTop = Program.UTIL.ToDoubleOrZero(TBValue[0][0]); Psi_InstallSide = Program.UTIL.ToDoubleOrZero(TBValue[0][1]); Psi_InstallButtom = Program.UTIL.ToDoubleOrZero(TBValue[0][2]);
                }
                string[][] Spacer = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율  From 창호간봉  where 구분1='단열간봉'and 구분2='" + 단창이중창 + "'and 구분3='" + 프레임재료 + "'");
                if (Spacer.Length > 0) { Psi_g_fix = Program.UTIL.ToDoubleOrZero(Spacer[0][0]); Psi_g_open = Program.UTIL.ToDoubleOrZero(Spacer[0][1]); }

                WinValue[0] = ug; WinValue[1] = g; WinValue[2] = tao;
                WinValue[3] = Uf_open; WinValue[4] = Uf_fix; WinValue[5] = Uf_btw;
                WinValue[6] = Psi_g_fix; WinValue[7] = Psi_g_open;
                WinValue[8] = Psi_InstallTop; WinValue[9] = Psi_InstallSide; WinValue[10] = Psi_InstallButtom;
            }
            return WinValue;
        }
        private double Calc_Uw(string[][] Size, double Ug, double Uf_open, double Uf_fix, double Uf_btw, double Psi_g_fix, double Psi_g_open)
        {
            double Area = Program.UTIL.ToDoubleOrZero(Size[0][0]), Width = Program.UTIL.ToDoubleOrZero(Size[0][1]), Height = Program.UTIL.ToDoubleOrZero(Size[0][2]), Ag_fix = Program.UTIL.ToDoubleOrZero(Size[0][3]), Ag_open = Program.UTIL.ToDoubleOrZero(Size[0][4]), Af_open = Program.UTIL.ToDoubleOrZero(Size[0][5]), Af_fix = Program.UTIL.ToDoubleOrZero(Size[0][6]), Af_btw = Program.UTIL.ToDoubleOrZero(Size[0][7]), Lg_fix = Program.UTIL.ToDoubleOrZero(Size[0][8]), Lg_open = Program.UTIL.ToDoubleOrZero(Size[0][0]);
            double Uw = (Ug * (Ag_fix + Ag_open) + (Uf_open * Af_open) + (Uf_fix * Af_fix) + (Uf_btw * Af_btw) + (Psi_g_fix * Lg_fix) + (Psi_g_open * Lg_open)) / Area;
            return Uw;
        }
        private double[] Calc_AdditionalWindow(double NewUw, double OldUw, double Newg, double Oldg, double Newtao, double Oldtao)
        {
            double[] value = new double[3];
            double Uw = 1 / (0.019 + 1 / OldUw + 1 / NewUw); double g = 0, tao = 0;
            String 조합구성 = "LE+LE";
            string[][] f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '태양열취득률'");
            string[][] f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + 조합구성 + "' AND 보정유형 = '빛투과율'");
            if (f_shgc.Length > 0)
            { g = Program.UTIL.ToDoubleOrZero(f_shgc[0][0]) * Oldg * Newg; }
            if (f_τ.Length > 0)
            { tao = Program.UTIL.ToDoubleOrZero(f_τ[0][0]) * Oldtao * Newtao; }
            value[0] = Uw; value[1] = g; value[2] = tao;
            return value;
        }
        public double Calc_dUinst(string[][] Size, double Psi_InstallTop, double Psi_InstallButtom, double Psi_InstallSide)
        {
            double Area = Program.UTIL.ToDoubleOrZero(Size[0][0]), Width = Program.UTIL.ToDoubleOrZero(Size[0][1]), Height = Program.UTIL.ToDoubleOrZero(Size[0][2]), Ag_fix = Program.UTIL.ToDoubleOrZero(Size[0][3]), Ag_open = Program.UTIL.ToDoubleOrZero(Size[0][4]), Af_open = Program.UTIL.ToDoubleOrZero(Size[0][5]), Af_fix = Program.UTIL.ToDoubleOrZero(Size[0][6]), Af_btw = Program.UTIL.ToDoubleOrZero(Size[0][7]), Lg_fix = Program.UTIL.ToDoubleOrZero(Size[0][8]), Lg_open = Program.UTIL.ToDoubleOrZero(Size[0][0]);
            double dUinst = ((Psi_InstallTop * Width) + (Psi_InstallButtom * Width) + (Psi_InstallSide * Height * 2)) / Area;
            return dUinst;
        }
        private double[] Calc_DoubleGlass(string[][] GlassValue)
        {
            String LE_CL_V = GlassValue[0][6] + "+" + GlassValue[0][6];
            double[] value = new double[3];// Ug, g, Tao;
            value[0] = 1 / ((1 / Program.UTIL.ToDoubleOrZero(GlassValue[0][7])) - 0.04 + 0.189 - 0.13 + (1 / Program.UTIL.ToDoubleOrZero(GlassValue[0][7])));
            String[][] f_shgc = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + LE_CL_V + "' AND 보정유형 = '태양열취득률'");
            String[][] f_τ = Program.DB.getValue(DB.type.BaseDB_HCneed, "이중창보정계수", "계수", "조합구성 = '" + LE_CL_V + "' AND 보정유형 = '빛투과율'");
            if (f_shgc.Length > 0)
            {
                value[1] = Program.UTIL.ToDoubleOrZero(f_shgc[0][0]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][8]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][8]);
            }
            if (f_τ.Length > 0)
            { value[2] = Program.UTIL.ToDoubleOrZero(f_τ[0][0]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][9]) * Program.UTIL.ToDoubleOrZero(GlassValue[0][9]); }
            return value;
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
            if(Material_main_ != null && Material_main_ !="기존외벽" && Material_main_ != "기존지붕" && Material_main_ != "기존바닥" && Material_main_ != "덧댐커튼월" && Material_main_ != "공기층" && Material_main_ != "단열재" && Material_main_ != "콘크리트" && Material_main_ != "미장" && Material_main_ != "조적" && Material_main_ != "패널" && Material_main_ != "목재" && Material_main_ != "금속재" && Material_main_ != "타일" && Material_main_ != "지중")
            
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
            if (Material_main_ != null  && Material_main_ != "기존외벽" && Material_main_ != "기존지붕" && Material_main_ != "기존바닥" && Material_main_ != "덧댐커튼월" && Material_main_ != "공기층" && Material_main_ != "단열재" && Material_main_ != "콘크리트" && Material_main_ != "미장" && Material_main_ != "조적" && Material_main_ != "패널" && Material_main_ != "목재" && Material_main_ != "금속재" && Material_main_ != "타일" && Material_main_ != "지중")

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
    public class Material_Floor
    {
        string Material_main_;
        string Material_sub_;
        double Material_d_;
        double Material_R_;
        string Material_Color_;

        public Material_Floor(string Material_main, string Material_sub, double Material_d, double Material_R, string Material_Color)
        {
            this.Material_main_ = Material_main;
            this.Material_sub_ = Material_sub;
            this.Material_d_ = Material_d;
            this.Material_R_ = Material_R;
            this.Material_Color_ = Material_Color;
        }

        public string Material_main()
        {
            if (Material_main_ != null && Material_main_ != "기존외벽" && Material_main_ != "기존지붕" && Material_main_ != "기존바닥" && Material_main_ != "덧댐커튼월" && Material_main_ != "공기층" && Material_main_ != "단열재" && Material_main_ != "콘크리트" && Material_main_ != "미장" && Material_main_ != "조적" && Material_main_ != "패널" && Material_main_ != "목재" && Material_main_ != "금속재" && Material_main_ != "타일" && Material_main_ != "지중")

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

}
