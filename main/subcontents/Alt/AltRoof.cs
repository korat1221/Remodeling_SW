using main.contents.Alt;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.subcontents.Alt
{
    public partial class AltRoof : Form
    {
        string RoofRemodelingType, RoofEx;
        int SelectRow;
        public string SelectName;
        public double SelectUeff;
        public double SelectSavingPoint;
        public double SelectComfortPoint;
        public double SelectRulePoint;
        public double SelectCostPoint;
        public double SelectTotalPoint;
        public double SelectSavingPercent;
        public double SelectCost_DirectTotal;
        public double SelectCost_material;
        public double SelectCost_labor;
        public double SelectCost_etc;

        bool scriptable = false;
        public AltRoof(String SelectValue)
        {
            InitializeComponent();

            RoofRemodelingType_comboBox.Items.Clear();
            RoofRemodelingType_comboBox.Items.Add("내부덧댐");
            RoofRemodelingType_comboBox.Items.Add("외부덧댐");
           // RoofRemodelingType_comboBox.Items.Add("철거 후 신규");
            create_table_DB();
            InitializeAsync();
        }

        #region 최적안 자재 리스트
        private void change_comboBox_WallEx()
        {
            if (RoofRemodelingType == "외부덧댐")
            {
                WallEx_label.Visible = true;
                WallEx_comboBox.Visible = true;
                WallEx_comboBox.Items.Clear();
                WallEx_comboBox.Items.Add("외단열미장");
                WallEx_comboBox.Items.Add("석재");
                WallEx_comboBox.Items.Add("금속패널");
                WallEx_comboBox.Items.Add("목재패널");
                WallEx_comboBox.Items.Add("시멘트패널");
            }
            else if (RoofRemodelingType == "신규")
            {
                WallEx_label.Visible = true;
                WallEx_comboBox.Visible = true;
                WallEx_comboBox.Items.Clear();
                WallEx_comboBox.Items.Add("석재");
                WallEx_comboBox.Items.Add("금속패널");
                WallEx_comboBox.Items.Add("목재패널");
                WallEx_comboBox.Items.Add("시멘트패널");
            }
            else
            {
                WallEx_label.Visible = false;
                WallEx_comboBox.Visible = false;
                RoofEx = "내부덧댐";
            }
        }
        private void WallRemodelingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoofRemodelingType_comboBox.SelectedItem != null)
            {
                RoofRemodelingType = RoofRemodelingType_comboBox.SelectedItem.ToString();
                if (RoofRemodelingType == "철거 후 신규") { RoofRemodelingType = "신규"; }
                change_comboBox_WallEx();
            }
        }
        private void WallEx_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallEx_comboBox.SelectedItem != null)
            {
                RoofEx = WallEx_comboBox.SelectedItem.ToString();
            }
        }
        private void SIM_button_Click(object sender, EventArgs e)
        {
            if (RoofRemodelingType != null && RoofRemodelingType != "" && RoofEx != null && RoofEx != "")
            {
                Cal_Optimal cal = new Cal_Optimal();
                cal.Calc_Optimal_Wall();
                MessageBox.Show("리모델링안 검토가 완료되었습니다.");
                load_table_DB(RoofRemodelingType, RoofEx);
            }
            else
            {
                MessageBox.Show("리모델링 유형부터 선택해주세요");
            }
        }
        private void create_table_DB()
        {
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Alt_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(checkBoxColumn);
            Alt_dataGridView.Columns.Add("A1", "리모델링안");
            Alt_dataGridView.Columns.Add("A2", "유효열관류율.[W/m²·K]");
            Alt_dataGridView.Columns.Add("A3", "종합 점수");
            Alt_dataGridView.Columns.Add("A4", "점수.에너지절감");
            Alt_dataGridView.Columns.Add("A5", "점수.쾌적성");
            Alt_dataGridView.Columns.Add("A6", "점수.적법성");
            Alt_dataGridView.Columns.Add("A7", "점수.경제성");
            Alt_dataGridView.Columns.Add("A8", "에너지절감률.[%]");
            Alt_dataGridView.Columns.Add("A9", "예상 순공사비.[원]");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[2].Width = 50;
            Alt_dataGridView.Columns[3].Width = 60;
            Alt_dataGridView.Columns[4].Width = 50;
            Alt_dataGridView.Columns[5].Width = 50;
            Alt_dataGridView.Columns[6].Width = 50;
            Alt_dataGridView.Columns[7].Width = 50;
            Alt_dataGridView.Columns[8].Width = 70;
            Alt_dataGridView.Columns[9].Width = 110;
        }
        void load_table_DB(string WallRemodelingType, string WallEx)
        {
            Alt_dataGridView.Rows.Clear();
            string[][] Pre_tot = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전체' and 월 ='연간'");
            if (Pre_tot.Length > 0)
            {
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "외벽_최적안", "최적안", "리모델링유형='" + WallRemodelingType + "' and 외부마감재대분류='" + WallEx + "'");
                if (Value.Length > 0)
                {
                    DataTable data = new DataTable();
                    data.Columns.Add("리모델링안", typeof(string));
                    data.Columns.Add("유효열관류율", typeof(string));
                    data.Columns.Add("종합 점수", typeof(double));
                    data.Columns.Add("에너지절감 점수", typeof(string));
                    data.Columns.Add("쾌적성 점수", typeof(string));
                    data.Columns.Add("적법성 점수", typeof(string));
                    data.Columns.Add("경제성 점수", typeof(string));
                    data.Columns.Add("에너지절감률", typeof(string));
                    data.Columns.Add("예상 순공사비", typeof(string));

                    for (int a = 0; a < Value.Length; a++)
                    {
                        DataRow row = data.NewRow();
                        row["리모델링안"] = Value[a][0];

                        double ueff = Cal_Ueff(Value[a][0]);
                        row["유효열관류율"] = ueff.ToString("0.00");
                        row["에너지절감률"] = Cal_SavingPercent(Value[a][0]).ToString("0.0") + " %";

                        double[] cost = Cal_Cost(Value[a][0]);
                        row["예상 순공사비"] = cost[0].ToString("#,##0"); //직접공사비

                        double[] point = new double[4];
                        point[0] = Cal_SavingPoint(Cal_Saving(Value[a][0])); //에너지
                        point[1] = Cal_ComfortPoint(ueff); //쾌적성
                        point[2] = Cal_RulePoint(ueff);//적법성
                        point[3] = Cal_CostPoint(cost[0]); //경제성
                        row["종합 점수"] = (point[0] + point[1] + point[2] + point[3]) / 4;
                        row["에너지절감 점수"] = point[0].ToString("0.0") + " 점";
                        row["쾌적성 점수"] = point[1].ToString("0.0") + " 점";
                        row["적법성 점수"] = point[2].ToString("0.0") + " 점";
                        row["경제성 점수"] = point[3].ToString("0.0") + " 점";
                        data.Rows.Add(row);
                    }

                    DataTable sortdata = new DataTable(); //종합점수 기준 정렬 
                    sortdata = OrderByTable(data);

                    foreach (DataRow rows in sortdata.Rows)
                    {
                        int nRow = Alt_dataGridView.Rows.Add();
                        for (int i = 0; i < sortdata.Columns.Count; i++)
                        {
                            Alt_dataGridView.Rows[nRow].Cells[i + 1].Value = rows[i];
                        }
                        Alt_dataGridView.Rows[nRow].Cells[3].Value = Convert.ToDouble(rows[2]).ToString("0.0") + " 점";
                    }

                    Alt_dataGridView.Rows[0].Cells[0].Value = true;
                }
            }
        }

        private DataTable OrderByTable(DataTable dt) //종합점수 기준 정렬
        {
            DataTable sortDt = dt.Clone();

            foreach (DataRow rows in dt.Rows)
            {
                sortDt.ImportRow(rows);
            }
            sortDt.AcceptChanges();
            DataView dv = sortDt.DefaultView;
            dv.Sort = "종합 점수 DESC";
            return dv.ToTable();
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
        #endregion

        #region 특정자재 선택
        private void Alt_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Alt_dataGridView.Rows[i].Cells[0].Value = false; }
                    else { Alt_dataGridView.Rows[i].Cells[0].Value = true; }
                }
                int row = GetSelectedIndex();
                if (row > -1)
                {
                    SelectRow = row;
                    SelectName = Alt_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (SelectName != null && SelectName != "")
                    {
                        Load_Select_Remodling(SelectName);
                        Load_TBImage(SelectName);
                    }
                }
            }
        }
        private void Alt_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    int row = e.RowIndex;
                    SelectRow = row;
                    SelectName = Alt_dataGridView.Rows[row].Cells[1].Value.ToString();
                    if (SelectName != null && SelectName != "")
                    {
                        Load_Select_Remodling(SelectName);
                        Load_TBImage(SelectName);
                    }
                }
            }
        }
        private void Load_Select_Remodling(string 리모델링안)
        {
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);
            Ucalc_dataGridView.Columns.Clear();
            Ucalc_dataGridView.Rows.Clear();
            Ucalc_dataGridView.Columns.Add("A0", "번호");
            Ucalc_dataGridView.Columns.Add("A1", "재료명         ");
            Ucalc_dataGridView.Columns.Add("A2", "열전도율.[W/m·K]");
            Ucalc_dataGridView.Columns.Add("A3", "두께.[mm]");
            Ucalc_dataGridView.Columns.Add("A4", "열저항.[m²·K/W]");
            Ucalc_dataGridView.Columns[0].Width = 40;
            Ucalc_dataGridView.Columns[2].Width = 70;
            Ucalc_dataGridView.Columns[3].Width = 70;
            Ucalc_dataGridView.Columns[4].Width = 70;

            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.재료,a.열전도율,a.두께,a.열저항,b.외부마감재 from 외벽_최적안유형 as a  Inner Join  외벽_최적안 as b  on a.구분=b.최적안구분 Where b.최적안='" + 리모델링안 + "' Order by a.번호");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    if (Value[a][0] == "기존 외벽") { Add_OldWall(); }
                    else
                    {
                        int nRow = Ucalc_dataGridView.Rows.Add();
                        Ucalc_dataGridView.Rows[nRow].Cells[0].Value = nRow + 1;
                        if (Value[a][0] == "외부 마감재") { Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[a][4]; } else { Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[a][0]; }
                        if (Value[a][1] != "" && Convert.ToDouble(Value[a][1]) != 0) { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[a][1]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "-"; }
                        if (Value[a][2] != "" && Convert.ToDouble(Value[a][2]) != 0) { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Value[a][2]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = "-"; }
                        if (Value[a][3] != "" && Convert.ToDouble(Value[a][3]) != 0) { Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Convert.ToDouble(Value[a][3]).ToString("0.00"); }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[4].Value = "-"; }
                    }

                }
                Load_Graph(리모델링안);
            }
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        private void Load_Graph(string 리모델링안)
        {
            string SelectNum = "";
            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct a.번호,Sum(b.면적) From ConstructionWall as a  Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호");
            if (List.Length > 0)
            {
                SelectNum = List[0][0];
                for (int a = 0; a < List.Length - 1; a++)
                {
                    if (Convert.ToDouble(List[a][1]) < Convert.ToDouble(List[a + 1][1]))
                    {
                        SelectNum = List[a + 1][0];
                    }
                }
            }

            List<Material_Wall> Materials_Wall = new List<Material_Wall>();

            if (SelectNum != "" && SelectNum != null)
            {
                webView21.Visible = true;
                Graph_label.Visible = true;

                double[] Material_T = new double[12]; //온도
                double Rsi = 0.13, Rse = 0.04;
                double dtot = 0; double Rtot = 0;
                string 직접간접 = "";
                string[][] Load = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "직접간접", "번호 = '" + SelectNum + "'");
                if (Load.Length > 0)
                {
                    직접간접 = Load[0][0];
                }

                string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,a.재료유형,a.재료,a.열전도율,a.두께,a.열저항 from 외벽_최적안유형 as a  Inner Join  외벽_최적안 as b  on a.구분=b.최적안구분 Where b.최적안='" + 리모델링안 + "' Order by a.번호");
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
                                if (Alt[a][4] != "") { Material_d = Convert.ToDouble(Alt[a][4]); }
                                double Material_R = 0;
                                if (Alt[a][5] != "") { Material_R = Convert.ToDouble(Alt[a][5]); }
                                string Material_Color = "DDEBF7";
                                if (Material_main == "단열재") { Material_Color = "FFDB58"; }
                                Material_Wall w = new Material_Wall(Material_main, Material_sub, Material_d, Material_R, Material_Color);
                                Materials_Wall.Add(w);
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < Alt.Length; a++)
                        {
                            string Material_main = Alt[a][1];
                            string Material_sub = Alt[a][2];
                            double Material_d = 0;
                            if (Alt[a][4] != "") { Material_d = Convert.ToDouble(Alt[a][4]); }
                            double Material_R = 0;
                            if (Alt[a][5] != "") { Material_R = Convert.ToDouble(Alt[a][5]); }
                            string Material_Color = "FFFFFF";
                            if (Material_main == "단열재") { Material_Color = "FFDB58"; }
                            Material_Wall w = new Material_Wall(Material_main, Material_sub, Material_d, Material_R, Material_Color);
                            Materials_Wall.Add(w);
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

                runScript("drawWall([" + s + "])");
            }
            else
            {
                webView21.Visible = false;
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
                    if (Value.Length == 0)
                    {
                        OldWall_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "열관류율", "명칭 = '" + Material_sub[a] + "'");
                        if (OldWall_U.Length == 0)
                        {
                            string[][] CW_U = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "유리부분열관류율", "명칭 = '" + Material_sub[a] + "'");
                            if (CW_U.Length > 0)
                            {
                                Material_main[a] = "덧댐커튼월";
                                Material_sub[a] = "덧댐커튼월";
                                Material_d[a] = 150;
                                Material_R[a] = 1 / Convert.ToDouble(CW_U[0][0]);
                                Material_Color[a] = "97C0D6";
                            }
                        }
                        else
                        {
                            Material_main[a] = "기존외벽";
                            Material_sub[a] = "기존외벽";
                            Material_d[a] = 200;
                            Material_R[a] = 1 / Convert.ToDouble(OldWall_U[0][0]);
                            Material_Color[a] = "6e6e6e";
                        }
                    }
                    else
                    {
                        Material_R[a] = Material_d[a] / 1000 / Convert.ToDouble(Value[0][1]);
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
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;

            runScript("drawWall([{\"cate\":-1,\"width\": 80,\"temper\": 18.660557954943386},{\"cate\":2,\"width\": 80,\"temper\": -4.684837165869034},{\"cate\":-1,\"width\": 80,\"temper\": -5.000000000000002}])");

        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        private void Add_OldWall()
        {
            int nRow = Ucalc_dataGridView.Rows.Add();
            Ucalc_dataGridView.Rows[nRow].Cells[0].Value = nRow + 1;
            Ucalc_dataGridView.Rows[nRow].Cells[1].Value = "기존외벽";
            Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "Var";
            Ucalc_dataGridView.Rows[nRow].Cells[3].Value = "Var";
            Ucalc_dataGridView.Rows[nRow].Cells[4].Value = "Var";
        }
        private bool Ucalc_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (Ucalc_dataGridView.Rows[row].Cells[1].Value != null && Ucalc_dataGridView.Rows[row].Cells[1].Value.ToString() == "기존외벽")
            {
                if (column == 4)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else return false;
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
        private void Load_TBImage(string 리모델링안)
        {
            string[][] V = Program.DB.getValue(DB.type.BaseDB_Optimal, "외벽_최적안", "최적안구분,열교유형", "최적안='" + 리모델링안 + "'");
            if (V.Length > 0)
            {
                string[][] Value2 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "외벽_최적안", "열교유형", "최적안구분='" + V[0][0] + "'");
                if (Value2.Length > 0 && Value2[0][0] != "")
                {
                    string TB_Type = null; string TBName = null;

                    if (Value2[0][0] == "직접고정" || Value2[0][0] == "트러스(점형)")
                    {
                        TB_Type = Value2[0][0];
                        TBName = "단열앙카";
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교이미지", "이미지_구조유형, 이미지_고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TB_Type + "'");
                        if (Image.Length > 0)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image[0][1]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    else
                    {
                        TBName = Value2[0][0];
                        string[][] TValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "열교유형", "제품명='" + TBName + "'");
                        if (TValue.Length > 0)
                        { TB_Type = TValue[0][0]; }
                        string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교이미지", "이미지_구조유형, 이미지_고정유형", "제품명 = '" + TBName + "' And 열교유형 = '" + TB_Type + "'");
                        if (Image.Length > 0)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Load(Program.gPath + Image[0][0]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox2.Visible = true;
                            pictureBox2.Load(Program.gPath + Image[0][1]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }

                    double dU = Get_Wall_Utb(V[0][0]);
                    TB_textBox.Visible = true;
                    dU_textBox.Visible = true;
                    TB_textBox.Text = TB_Type+"_" +TBName;
                    dU_textBox.Text = "열교가산치 : " + dU.ToString("0.00") + " W/m²·K";
                }
            }
        }

        #endregion

        #region 비용 및 절감량 계산
        private double[] Cal_Cost(string 리모델링안)
        {
            double[] cost = new double[4];//직접공사비, 재료비, 노무비, 경비 순 
            double Area = 0;
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,a.직접공사비,a.재료비,a.노무비,a.경비,b.외부마감재 from 외벽_최적안유형 as a  Inner Join  외벽_최적안 as b  on a.구분=b.최적안구분 Where b.최적안='" + 리모델링안 + "' Order by a.번호");
            if (Value.Length > 0)
            {
                if (Value[0][0] == "내부덧댐")
                {
                    string[][] ar = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(a.면적) From ZoneEnvelope_3D as a Inner JoIn ConstructionWall as b on a.구조체번호=b.번호  where a.외피유형='외벽'");
                    if (ar.Length > 0)
                    {
                        Area = Convert.ToDouble(ar[0][0]);
                    }
                }
                else
                {
                    string[][] ar = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(a.면적) From ZoneEnvelope_3D as a Inner JoIn ConstructionWall as b on a.구조체번호=b.번호 where a.외피유형='외벽'and Not b.직접간접 ='지면'");
                    if (ar.Length > 0)
                    {
                        Area = Convert.ToDouble(ar[0][0]);
                    }
                }
                cost[0] = Convert.ToDouble(Value[0][1]) * Area;
                cost[1] = Convert.ToDouble(Value[0][2]) * Area;
                cost[2] = Convert.ToDouble(Value[0][3]) * Area;
                cost[3] = Convert.ToDouble(Value[0][4]) * Area;

                string[][] EValue = Program.DB.getValue(DB.type.BaseDB_Optimal, "외벽마감재", "직접공사비,재료비,노무비,경비", "외부마감재='" + Value[0][5] + "'");
                if (EValue.Length > 0)
                {
                    cost[0] = cost[0] + Convert.ToDouble(EValue[0][0]) * Area;
                    cost[1] = cost[1] + Convert.ToDouble(EValue[0][1]) * Area;
                    cost[2] = cost[2] + Convert.ToDouble(EValue[0][2]) * Area;
                    cost[3] = cost[3] + Convert.ToDouble(EValue[0][3]) * Area;
                }
            }
            return cost;
        }
        private double Cal_SavingPercent(string 리모델링안)
        {
            double SavingPercent = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Optimal Where 리모델링안='" + 리모델링안 + "' and 검토유형='외벽' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                double pre = Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(PreValue[0][1]);
                SavingPercent = (Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0])) / pre * 100;
            }
            return SavingPercent;
        }
        private double Cal_Saving(string 리모델링안)
        {
            double Saving = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Optimal Where 리모델링안='" + 리모델링안 + "' and 검토유형='외벽' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                Saving = (Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0]));
            }
            return Saving;
        }
        #endregion

        #region 점수계산
        private double Cal_Ueff(string 리모델링안)
        {
            double R = 0; double dU = 0; string 리모델링유형 = "";
            string[][] V = Program.DB.getValue(DB.type.BaseDB_Optimal, "외벽_최적안", "외벽유형,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (V.Length > 0)
            {
                string[][] R_value = Program.DB.getValue(DB.type.BaseDB_Optimal, "외벽_최적안유형", "열저항합계", "구분='" + V[0][0] + "'");
                if (R_value.Length > 0)
                {
                    R = Convert.ToDouble(R_value[0][0]);
                }
                dU = Get_Wall_Utb(V[0][0]);
                리모델링유형 = V[0][1];
            }
            double Total_Area = 0, Ueff_avg = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    double Ueff = 0;
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    if (Value[k][2] == "지면")
                    {
                        if (리모델링유형 == "내부덧댐")
                        {
                            Ueff = 1 / (1 / Convert.ToDouble(Value[k][1]) + R) + dU;
                        }
                        else
                        {
                            Ueff = Convert.ToDouble(Value[k][1]);
                        }
                    }
                    else
                    {
                        if (리모델링유형 == "신규")
                        {
                            Ueff = Convert.ToDouble(Value[k][1]);
                        }
                        else
                        {
                            Ueff = 1 / (1 / Convert.ToDouble(Value[k][1]) + R) + dU;
                        }
                    }

                    Ueff_avg += Convert.ToDouble(Value[k][0]) * Ueff;
                }
                Ueff_avg = Ueff_avg / Total_Area;
            }
            return Ueff_avg;
        }
        private double Cal_RulePoint(double Ueff)
        {
            double point = 0;
            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                    RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                }
                Uvalue = Uvalue / Total_Area;
                RuleValue = RuleValue / Total_Area;
                point = (RuleValue / Ueff * 100);
            }
            return point;
        }
        private double Get_Wall_Utb(string 유형)
        {
            double dU = 0; double d_Ins = 0;
            string[][] Value1 = Program.DB.getValue(DB.type.BaseDB_Optimal, "외벽_최적안유형", "열전도율,두께", "구분='" + 유형 + "'");
            if (Value1.Length > 0)
            {
                for (int aa = 0; aa < Value1.Length; aa++)
                {
                    if (Value1[aa][0] != "" && Convert.ToDouble(Value1[aa][0]) < 0.04)
                    {
                        d_Ins = Convert.ToDouble(Value1[aa][1]);
                    }
                }
            }
            string[][] Value2 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "외벽_최적안", "열교유형", "최적안구분='" + 유형 + "'");
            if (Value2.Length > 0 && Value2[0][0] != "")
            {
                string TB_Type = null; string TBName = null;

                if (Value2[0][0] == "직접고정" || Value2[0][0] == "트러스(점형)")
                {
                    TB_Type = Value2[0][0];
                    TBName = "단열앙카";
                    string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "A,B,C,수직간격,수평간격", "열교유형 ='" + TB_Type + "' and 제품명='" + TBName + "'");
                    if (TB.Length > 0)
                    {
                        double A = Convert.ToDouble(TB[0][0]);
                        double B = Convert.ToDouble(TB[0][1]);
                        double C = Convert.ToDouble(TB[0][2]);
                        double Kai = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                        double PerArea = 0;
                        if (Value2[0][0] == "직접고정")
                        {
                            PerArea = 2 * (Convert.ToDouble(TB[0][3]) / 1000) * (Convert.ToDouble(TB[0][4]) / 1000);
                        }
                        else
                        {
                            PerArea = 1 / (Convert.ToDouble(TB[0][3]) / 1000) / (Convert.ToDouble(TB[0][4]) / 1000);
                        }
                        dU = Kai * PerArea;
                    }
                }
                else
                {
                    TBName = Value2[0][0];
                    string[][] TValue = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "열교유형", "제품명='" + TBName + "'");
                    if (TValue.Length > 0)
                    { TB_Type = TValue[0][0]; }

                    string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽선형열교", "A,B,C,수직간격,수평간격", "제품명 = '" + Value2[0][0] + "'");
                    if (TB.Length > 0)
                    {
                        double A = Convert.ToDouble(TB[0][0]);
                        double B = Convert.ToDouble(TB[0][1]);
                        double C = Convert.ToDouble(TB[0][2]);
                        double Psi = (A * Math.Pow(d_Ins, 2) + B * d_Ins + C) / 1000;
                        double PerArea = 0;
                        PerArea = 1 / (Convert.ToDouble(TB[0][3]) / 1000 + Convert.ToDouble(TB[0][4]) / 1000);
                        dU = Psi * PerArea;
                    }
                }
            }
            else { }
            return dU;
        }
        private double Cal_SavingPoint(double Saving_Optimal)
        {
            double point = 0; double RuleSaving = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Rule Where 검토유형='외벽' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                RuleSaving = (Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0]));
                point = (Saving_Optimal / RuleSaving * 100);
            }
            return point;
        }
        private double Cal_ComfortPoint(double Ueff)
        {
            double point = 0;
            double Ti = 25, Te = -15, TDR = 0.26; //서울기준
            double Tis = (Ti - TDR * (Ti - Te));
            double Ucomfort = 1 / 0.13 * (Ti - Tis) / (Ti - Te);
            point = Math.Min(100, Ucomfort / Ueff * 100);
            return point;
        }
        private double Cal_CostPoint(double Cost_Optimal)
        {
            double point = 0;
            double CostAVG = 0;
            
            string[][] Value1 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "외벽_최적안", "최적안", "Not 리모델링유형='신규'");
            if (Value1.Length > 0)
            {
                double[] cost_data = new double[Value1.Length];
                for (int a = 0; a < Value1.Length; a++)
                {
                    double[] cost_arr = Cal_Cost(Value1[a][0]);
                    cost_data[a] = cost_arr[0]; //직접공사비
                }
                CostAVG = cost_data.Sum() / cost_data.Length;
            }
            point = (CostAVG / Cost_Optimal * 100);
            return point;
        }

        #endregion

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (SelectRow > -1)
            {
                SelectName = Alt_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();
                SelectUeff = Cal_Ueff(SelectName);
                SelectSavingPoint = Cal_SavingPoint(Cal_Saving(SelectName));
                SelectComfortPoint = Cal_ComfortPoint(Cal_Ueff(SelectName));
                SelectRulePoint = Cal_RulePoint(Cal_Ueff(SelectName));

                double[] cost = Cal_Cost(SelectName);
                SelectCost_DirectTotal = cost[0];
                SelectCost_material = cost[1];
                SelectCost_labor = cost[2];
                SelectCost_etc = cost[3];
                SelectCostPoint = Cal_CostPoint(SelectCost_DirectTotal);

                SelectTotalPoint = (SelectSavingPoint + SelectComfortPoint + SelectRulePoint + SelectCostPoint) / 4;
                SelectSavingPercent = Cal_SavingPercent(SelectName);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("외벽 리모델링안을 선택해주세요.");
            }
        }

    }
}

