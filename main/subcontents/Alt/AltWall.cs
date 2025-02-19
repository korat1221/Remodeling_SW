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
    public partial class AltWall : Form
    {
        string WallRemodelingType, WallEx;
        int SelectRow;
        public string SelectName;

        bool scriptable = false;
        public AltWall(String SelectValue)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\transmit.html", true);
            WallRemodelingType_comboBox.Items.Clear();
            WallRemodelingType_comboBox.Items.Add("외부덧댐");
            WallRemodelingType_comboBox.Items.Add("내부덧댐");
            WallRemodelingType_comboBox.Items.Add("철거 후 신규");
            create_table_DB();
            InitializeAsync();

            WallRemodelingType_comboBox.SelectedIndex = 0;
            WallEx_comboBox.SelectedIndex = 1;
            if (SelectValue == null || SelectValue == "")
            {
                if ((MessageBox.Show("외벽 리모델링안을 검토합니다", "외벽 리모델링안 검토", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    Cal_Optimal cal = new Cal_Optimal();
                    cal.Calc_Optimal_Wall();
                    MessageBox.Show("리모델링안 검토가 완료되었습니다.");
                    Save_WallOptimal();
                    load_table_DB(WallRemodelingType, WallEx);
                }
            }
            else
            {
                string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='외벽' and a.최적안='" + SelectValue + "'");
                if (Value2.Length > 0)
                {
                    WallRemodelingType_comboBox.SelectedItem = Value2[0][0];
                    WallEx_comboBox.SelectedItem = Value2[0][1];
                    load_table_DB(WallRemodelingType, WallEx);
                    for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                    {
                        Alt_dataGridView.Rows[i].Cells[0].Value = false;
                    }
                    if (Alt_dataGridView.Rows.Count > 0)
                    {
                        for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                        {
                            Alt_dataGridView.Rows[i].Cells[0].Value = false;
                        }
                        for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                        {
                            if ( Alt_dataGridView.Rows[i].Cells[1].Value.ToString() == SelectValue)
                            {
                                Alt_dataGridView.Rows[i].Cells[0].Value = true;
                                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                SelectName = SelectValue;
                                if (SelectName != null && SelectName != "")
                                {
                                    Load_Select_Remodling(SelectName);
                                    Load_TBImage(SelectName);
                                }
                            }
                        }
                    }
                }
            }
          
        }

        #region 최적안 자재 리스트
        private void change_comboBox_WallEx()
        {
            string[][] value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select Distinct a.마감재분류 From 마감재 as a Inner Join 불투명최적안 as b on a.마감재= b.마감재  Where b.구조체='외벽' and b.리모델링유형='" + WallRemodelingType + "'");
            if(value.Length > 0)
            {
                WallEx_label.Visible = true;
                WallEx_comboBox.Visible = true;
                WallEx_comboBox.Items.Clear();
                for(int a = 0; a < value.Length; a++)
                {
                    WallEx_comboBox.Items.Add(value[a][0]);
                }
            }
        }
        private void WallRemodelingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallRemodelingType_comboBox.SelectedItem != null)
            {
                WallRemodelingType = WallRemodelingType_comboBox.SelectedItem.ToString();
                change_comboBox_WallEx();
                if (WallRemodelingType != null && WallRemodelingType != "" && WallEx != null && WallEx != "")
                {
                    load_table_DB(WallRemodelingType, WallEx);
                }
            }
        }
        private void WallEx_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallEx_comboBox.SelectedItem != null)
            {
                WallEx = WallEx_comboBox.SelectedItem.ToString();
                if (WallRemodelingType != null && WallRemodelingType != "" && WallEx != null && WallEx != "")
                {
                    load_table_DB(WallRemodelingType, WallEx);
                }
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
            Alt_dataGridView.Columns.Add("A2", "유효열관류율.[W/m"+Program.UTIL.Subscript(2, true)+"·K]");
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
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링안,리모델링값,순공사비,재료비,노무비,경비,에너지절감량,에너지절감률,에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수 From Optimal_PreResult Where 검토유형='외벽' ORDER BY 종합점수 DESC");          
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    string[][] Value2 = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select a.리모델링유형,b.마감재분류 From 불투명최적안 as a Inner Join 마감재 as b on a.마감재=b.마감재 where a.구조체='외벽' and a.최적안='" + Value[a][0] + "'");
                    if(Value2.Length > 0 && WallRemodelingType ==Value2[0][0] && WallEx == Value2[0][1])
                    {
                        int nRow = Alt_dataGridView.Rows.Add();
                        Alt_dataGridView.Rows[nRow].Cells[1].Value = Value[a][0];
                        Alt_dataGridView.Rows[nRow].Cells[2].Value = Convert.ToDouble(Value[a][1]).ToString("0.00");
                        Alt_dataGridView.Rows[nRow].Cells[3].Value = Convert.ToDouble(Value[a][12]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[4].Value = Convert.ToDouble(Value[a][8]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[5].Value = Convert.ToDouble(Value[a][9]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[6].Value = Convert.ToDouble(Value[a][10]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[7].Value = Convert.ToDouble(Value[a][11]).ToString("0.0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[8].Value = Convert.ToDouble(Value[a][7]).ToString("0.0") + " %";
                        Alt_dataGridView.Rows[nRow].Cells[9].Value = Convert.ToDouble(Value[a][2]).ToString("#,##0"); //직접공사비
                    }
                }
            }
            if (Alt_dataGridView.Rows.Count > 0)
            {
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    Alt_dataGridView.Rows[i].Cells[0].Value = false;
                }
                if (Alt_dataGridView.Columns.Count > 1 && Alt_dataGridView.Rows[0].Cells[1].Value != null)
                {
                    Alt_dataGridView.Rows[0].Cells[0].Value = true;
                    Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    SelectRow = 0;
                    SelectName = Alt_dataGridView.Rows[0].Cells[1].Value.ToString();
                    if (SelectName != null && SelectName != "")
                    {
                        Load_Select_Remodling(SelectName);
                        Load_TBImage(SelectName);
                    }
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

        private void Load_Select_Remodling(string 리모델링안)
        {
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);
            Ucalc_dataGridView.Columns.Clear();
            Ucalc_dataGridView.Rows.Clear();
            Ucalc_dataGridView.Columns.Add("A0", "번호");
            Ucalc_dataGridView.Columns.Add("A1", "재료명         ");
            Ucalc_dataGridView.Columns.Add("A2", "열전도율.[W/m·K]");
            Ucalc_dataGridView.Columns.Add("A3", "두께.[mm]");
            Ucalc_dataGridView.Columns.Add("A4", "열저항.[m"+Program.UTIL.Subscript(2, true)+"·K/W]");
            Ucalc_dataGridView.Columns[0].Width = 40;
            Ucalc_dataGridView.Columns[2].Width = 70;
            Ucalc_dataGridView.Columns[3].Width = 70;
            Ucalc_dataGridView.Columns[4].Width = 70;

            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 재료,열전도율,두께,재료유형 from 불투명자재 Where 최적안='" + 리모델링안 + "' Order by ID");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    if (Value[a][0] == "기존 외벽") { Add_OldWall(); }
                    else
                    {
                        int nRow = Ucalc_dataGridView.Rows.Add();
                        Ucalc_dataGridView.Rows[nRow].Cells[0].Value = nRow + 1;
                        Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[a][0];
                        if (Value[a][1] != "" && Convert.ToDouble(Value[a][1]) != 0) { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[a][1]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "-"; }
                        if (Value[a][2] != "" && Convert.ToDouble(Value[a][2]) != 0) { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Value[a][2]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = "-"; }
                        if (Value[a][1] != "" && Convert.ToDouble(Value[a][1]) != 0 && Value[a][3] != "외부마감재") { Ucalc_dataGridView.Rows[nRow].Cells[4].Value = (Convert.ToDouble(Value[a][2]) / 1000 / Convert.ToDouble(Value[a][1])).ToString("0.00"); }
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
            string[][] WList = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호", "");
            if (WList.Length > 0)
            {
                SelectNum = WList[0][0];
                double[] area = new double[WList.Length];
                for (int a = 0; a < WList.Length; a++)
                {
                    string[][] WArea = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "Sum(면적)", "구조체번호='" + WList[a][0] + "'");
                    if (WArea.Length > 0 && WArea[0][0]!="")
                    {
                        area[a] = Convert.ToDouble(WArea[0][0]);
                    }
                }
                double MaxArea = area.Max();
                for (int a = 0; a < WList.Length; a++)
                {
                    if (area[a] == MaxArea)
                    {
                        SelectNum = WList[a][0];
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

                string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,재료유형,재료,열전도율,두께 from 불투명자재 Where 최적안='" + 리모델링안 + "' Order by ID");
                if (Alt.Length > 0)
                {
                    if (Alt[0][0] == "내부덧댐" || (Alt[0][0] == "외부덧댐" && 직접간접 != "지면") || (Alt[0][0] == "철거 후 신규" && 직접간접 != "지면"))
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
                //Material_T[0] = (20 - Q * Rsi);
                Material_T[0] = 20;
                for (int k = 1; k < Materials_Wall.Count + 1; k++)
                {
                    Material_Wall w = (Material_Wall)Materials_Wall[k - 1];
                    Material_T[k] = (Material_T[k - 1] - Q * w.Material_R());
                }
                //  Material_T[Materials_Wall.Count + 1] = Material_T[Materials_Wall.Count] - Q * Rse
                Material_T[Materials_Wall.Count ] = -5;
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
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;

           // runScript("drawWall([{\"cate\":-1,\"width\": 80,\"temper\": 18.660557954943386},{\"cate\":2,\"width\": 80,\"temper\": -4.684837165869034},{\"cate\":-1,\"width\": 80,\"temper\": -5.000000000000002}])");

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
            string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "불투명최적안", "열교,열교가산치", "최적안='" + 리모델링안 + "'");
            if (Value.Length > 0 && Value[0][0] != "")
            {
                string TB_Type = null; string TBName = null;

                if (Value[0][0] == "직접고정" || Value[0][0] == "트러스(점형)")
                {
                    TB_Type = Value[0][0];
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
                    TBName = Value[0][0];
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

                double dU = Convert.ToDouble(Value[0][1]);
                TB_textBox.Visible = true;
                dU_textBox.Visible = true;
                if (TBName == "외단열미장")
                {
                    TB_textBox.Text = "열교없음";
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                }
                else { TB_textBox.Text = TB_Type + "_" + TBName; }
                dU_textBox.Text = "열교가산치 : " + dU.ToString("0.00") + " W/m"+Program.UTIL.Subscript(2, true)+"·K";
            }
        }

        #endregion

        #region 비용 및 절감량 계산

        private void Save_WallOptimal()
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 최적안 From 불투명최적안  where 구조체='외벽'");
            if(Value.Length > 0)
            {
                for(int a=0; a<Value.Length; a++)
                {
                    //ID, 프로젝트번호, 프로젝트유형, 검토유형, 리모델링안, 유효열관류율,순공사비,에너지절감량, 에너지절감률 
                    string 리모델링안 = Value[a][0];
                    double ueff = Cal_Ueff(리모델링안);
                    double[] cost = Cal_Cost(리모델링안);//직접공사비, 재료비, 노무비, 경비 순 
                    double Saving= Cal_Saving(리모델링안);
                    double SavingPercent = Cal_SavingPercent(리모델링안);

                    double[] point = new double[5];
                    point[0] = Cal_SavingPoint(Saving); //에너지
                    point[1] = Cal_ComfortPoint(리모델링안); //쾌적성
                    point[2] = Cal_RulePoint(ueff);//적법성
                    point[3] = Cal_CostPoint(cost[0]); //경제성
                    point[4] = (point[0] + point[1] + point[2] + point[3]) / 4; //종합

                    Program.DB.setValue(DB.type.ProjDB, "Optimal_PreResult", "프로젝트번호,프로젝트유형,검토유형,리모델링안," +
                   "리모델링값유형,리모델링값,순공사비,재료비,노무비,경비,에너지절감량,에너지절감률," +
                   "에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수",
                   "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','외벽','" + 리모델링안 + "','유효열관류율','" + ueff.ToString() + "','" +
                   cost[0].ToString() + "','" + cost[1].ToString() + "','" + cost[2].ToString() + "','" + cost[3].ToString() + "','" +
                   Saving.ToString() + "','" + SavingPercent.ToString() + "','" +
                   point[0].ToString() + "','" + point[1].ToString() + "','" + point[2].ToString() + "','" + point[3].ToString() + "','" + point[4].ToString() 
                   + "'", "검토유형,리모델링안");
                }
                string[][] Value2 = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링안,에너지점수,쾌적성점수,적법성점수,경제성점수 From Optimal_PreResult  where 검토유형='외벽'");
                if(Value2.Length  > 0)
                {
                    double[] Point1 = new double[Value2.Length]; //에너지
                    double[] Point2 = new double[Value2.Length]; //쾌적성
                    double[] Point3 = new double[Value2.Length]; //적법성
                    double[] Point4 = new double[Value2.Length]; //경제성
                    double[] Point5 = new double[Value2.Length]; //종합
                    for (int a=0 ; a<Value2.Length; a++)
                    {
                        Point1[a] = Convert.ToDouble(Value2[a][1]);
                        Point2[a] = Convert.ToDouble(Value2[a][2]);
                        Point3[a] = Convert.ToDouble(Value2[a][3]);
                        Point4[a] = Convert.ToDouble(Value2[a][4]);
                    }
                    double Avg1, Avg2, Avg3, Avg4;
                    Avg1 = Point1.ToArray().Average();
                    Avg2 = Point2.ToArray().Average();
                    Avg3 = Point3.ToArray().Average();
                    Avg4 = Point4.ToArray().Average();

                    for (int a = 0; a < Value2.Length; a++)
                    {
                        Point1[a] = Convert.ToDouble(Value2[a][1]) / Avg1 * 100;
                        Point2[a] = Convert.ToDouble(Value2[a][2]) / Avg2 * 100;
                        Point3[a] = Convert.ToDouble(Value2[a][3]) / Avg3 * 100;
                        Point4[a] = Convert.ToDouble(Value2[a][4]) / Avg4 * 100;
                        Point5[a] = (Point1[a] + Point2[a] + Point3[a] + Point4[a]) / 4; //종합
                        Program.DB.setValue(DB.type.ProjDB, "Optimal_PreResult", "프로젝트번호,프로젝트유형,검토유형,리모델링안," +
                        "에너지점수,쾌적성점수,적법성점수,경제성점수,종합점수",
                        "'" + 프로젝트유형[0][1] + "','" + 프로젝트유형[0][0] + "','외벽','" + Value2[a][0] + "','" +
                         Point1[a].ToString() + "','" + Point2[a].ToString() + "','" + Point3[a].ToString() + "','" + Point4[a].ToString() + "','" + Point5[a].ToString()
                         + "'", "검토유형,리모델링안");
                    }
                }
            }
        }
        private double[] Cal_Cost(string 리모델링안)
        {
            double[] cost = new double[4];//직접공사비, 재료비, 노무비, 경비 순 
            double Area = 0;
            string[][] Value = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형,직접공사비,재료비,노무비,경비 from 불투명최적안 Where 최적안='" + 리모델링안 + "'");
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
                SavingPercent = Math.Max((Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0])) / pre * 100, 0);
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
                Saving = Math.Max((Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0])), 0);
            }
            return Saving;
        }
        #endregion

        #region 점수계산
        private double Cal_Ueff(string 리모델링안)
        {
            double R = 0; double dU = 0; string 리모델링유형 = "";
            string[][] Alt = Program.DB.getValue(DB.type.BaseDB_Optimal, "불투명최적안", "열저항합계,열교가산치,리모델링유형", "최적안='" + 리모델링안 + "'");
            if (Alt.Length > 0)
            {
                R = Convert.ToDouble(Alt[0][0]);
                dU = Convert.ToDouble(Alt[0][1]);
                리모델링유형 = Alt[0][2];
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
                        if (리모델링유형 == "철거 후 신규")
                        {
                            Ueff = 1 / R + dU;
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
            double Total_Area = 0, RuleValue = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                }
                RuleValue = RuleValue / Total_Area;
                point = (RuleValue / Ueff * 100);
            }
            return point;
        }
        private double Cal_SavingPoint(double Saving_Optimal)
        {
            double point = 0; double RuleSaving = 0;
            string[][] PreValue = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,기저에너지 from FinalEnergy_Result Where 연료='전체'and 월='연간'");
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 from FinalEnergy_Result_Rule Where 검토유형='외벽' and 연료='전체'");
            if (Value.Length > 0 && PreValue.Length > 0)
            {
                RuleSaving = Math.Max((Convert.ToDouble(PreValue[0][0]) - Convert.ToDouble(Value[0][0])), 0);
                point = (Saving_Optimal / RuleSaving * 100);
            }
            return point;
        }
        private double Cal_ComfortPoint(string 리모델링안)
        {
            double point = 0;
            //double Ti = 25, Te = -15, TDR = 0.26; //서울기준
            //double Tis = (Ti - TDR * (Ti - Te));
            //double Ucomfort = 1 / 0.13 * (Ti - Tis) / (Ti - Te);
            //point = Math.Min(100, Ucomfort / Ueff * 100);
            string[][] Alt = Program.DB.querySQL(DB.type.BaseDB_Optimal, "Select 리모델링유형 From 불투명최적안 Where 최적안='" + 리모델링안 + "'");
            if (Alt.Length > 0)
            {
                if (Alt[0][0] == "내부덧댐") { point = 70; }
                else { point = 100; }
            }
            return point;

        }
        private double Cal_CostPoint(double Cost_Optimal)
        {
            double point = 0;
            double CostAVG = 0;

            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 순공사비 From Optimal_PreResult Where 검토유형='외벽'");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    CostAVG += Convert.ToDouble(Value[a][0]);
                }
                CostAVG = CostAVG / Value.Length;
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

