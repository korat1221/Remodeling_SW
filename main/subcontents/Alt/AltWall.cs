using Microsoft.Office.Interop.Excel;
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
        double Count_DB;
        string WallRemodelingType, WallEx;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectZone_split = new ArrayList();
        public string SelectZone;
        bool scriptable = false;
        public AltWall(String SelectValue)
        {
            InitializeComponent();

            WallRemodelingType_comboBox.Items.Clear();
            WallRemodelingType_comboBox.Items.Add("내부덧댐");
            WallRemodelingType_comboBox.Items.Add("외부덧댐");
            WallRemodelingType_comboBox.Items.Add("철거 후 신규");
            InitializeAsync();
        }

        private void change_comboBox_WallEx()
        {
            if (WallRemodelingType == "외부덧댐")
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
            else if (WallRemodelingType == "철거 후 신규")
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
                WallEx = "내부덧댐";
            }
        }
        private void WallRemodelingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallRemodelingType_comboBox.SelectedItem != null)
            {
                WallRemodelingType = WallRemodelingType_comboBox.SelectedItem.ToString();                
                change_comboBox_WallEx();
            }
        }
        private void WallEx_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallEx_comboBox.SelectedItem != null)
            {
                WallEx = WallEx_comboBox.SelectedItem.ToString();
            }
        }
        private void SIM_button_Click(object sender, EventArgs e)
        {
            if (WallRemodelingType != null && WallRemodelingType != "" && WallEx != null && WallEx != "")
            {
                Cal_Optimal cal = new Cal_Optimal();
                cal.Calc_Optimal_Wall();
                MessageBox.Show("리모델링안 검토가 완료되었습니다.");
                load_table_DB(WallRemodelingType, WallEx);
            }
            else
            {
                MessageBox.Show("리모델링 유형부터 선택해주세요");
            }
        }
        void load_table_DB(string WallRemodelingType, string WallEx)
        {
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Alt_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(checkBoxColumn);

            Alt_dataGridView.Columns.Add("A1", "번호");
            Alt_dataGridView.Columns.Add("A2", "리모델링안");
            Alt_dataGridView.Columns.Add("A3", "평균 유효열관류율.[W/m²·K]");
            Alt_dataGridView.Columns.Add("A4", "점수.에너지절감");
            Alt_dataGridView.Columns.Add("A5", "점수.쾌적성");
            Alt_dataGridView.Columns.Add("A6", "점수.법규");
            Alt_dataGridView.Columns.Add("A7", "점수.경제성");
            Alt_dataGridView.Columns.Add("A8", "종합 점수");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 30;
            Alt_dataGridView.Columns[3].Width = 60;
            Alt_dataGridView.Columns[4].Width = 50;
            Alt_dataGridView.Columns[5].Width = 50;
            Alt_dataGridView.Columns[6].Width = 50;
            Alt_dataGridView.Columns[7].Width = 50;
            Alt_dataGridView.Columns[8].Width = 60;

            string[][] Pre_tot = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전체' and 월 ='연간'");
            if (Pre_tot.Length > 0)
            {
                string[][] Value1 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "구분", "리모델링유형='"+WallRemodelingType+"' and 외부마감재대분류='"+WallEx+"'");
                if(Value1.Length > 0)
                {
                    for(int a=0; a<Value1.Length; a++)
                    {
                        string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량 From FinalEnergy_Result_Optimal Where 검토유형='외벽' and 연료='전체' and 리모델링안='" + Value1[a][0] +"' Order by 총에너지소요량 DESC");
                        if (Value.Length > 0)
                        {
                            string[] name = new string[Value.Length]; double[] Ueff = new double[Value.Length]; double[] Saving = new double[Value.Length];
                            double[] Point_tot = new double[Value.Length]; double[] Point_energy = new double[Value.Length]; double[] Point_comfort = new double[Value.Length]; double[] Point_rule = new double[Value.Length]; double[] Point_money = new double[Value.Length];
                            int nRow = Alt_dataGridView.Rows.Add();
                            Alt_dataGridView.Rows[nRow].Cells[1].Value = (a + 1).ToString();
                            Alt_dataGridView.Rows[nRow].Cells[2].Value = Value1[a][0];
                            Alt_dataGridView.Rows[nRow].Cells[3].Value = Cal_UValue(Value1[a][0]).ToString("0.00");
                            // Alt_dataGridView.Rows[nRow].Cells[4].Value = ((Convert.ToDouble(Pre_tot[0][0]) - Convert.ToDouble(Value[i][0])) / Convert.ToDouble(Pre_tot[0][0]) * 1000).ToString("0") + " 점";
                            //  Alt_dataGridView.Rows[nRow].Cells[5].Value = "";
                            Alt_dataGridView.Rows[nRow].Cells[6].Value = (Cal_RuleUvalue() / Cal_UValue(Value1[a][0]) * 100).ToString("0") + " 점";
                        }
                    }
                }
            }
        }
        private double Cal_UValue(string 리모델링안)
        {
            double R = 0;  double dU = 0; string 열교유형 = "";
            string[][] V = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "외벽유형,열교유형", "구분='" + 리모델링안 + "'");
            if (V.Length > 0)
            {
                string[][] R_value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "열저항합계", "구분='" + V[0][0] + "'");
                if (R_value.Length > 0)
                {
                    R = Convert.ToDouble(R_value[0][0]);
                }
                dU = Get_Wall_Utb(V[0][0]);
            }
            double Total_Area = 0, Ueff_avg = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.열관류율,b.직접간접 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    double Ueff = 0;
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    if (Value[k][2]=="지면")
                    {
                        if(열교유형 =="내부덧댐")
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
                        Ueff = 1 / (1 / Convert.ToDouble(Value[k][1]) + R) + dU;
                    }

                    Ueff_avg += Convert.ToDouble(Value[k][0]) * Ueff;
                }
                Ueff_avg = Ueff_avg / Total_Area;
            }
            return Ueff_avg;
        }
        private double Cal_RuleUvalue()
        {
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
            }
            return RuleValue;
        }
        private double Get_Wall_Utb(string 유형)
        {
            double dU = 0; double d_Ins = 0;
            string[][] Value1 = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "열전도율,두께", "구분='" + 유형 + "'");
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
            string[][] Value2 = Program.DB.getValue_SameCheck(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "열교유형", "외벽유형='" + 유형 + "'");
            if (Value2.Length > 0 && Value2[0][0] != "")
            {
                if (Value2[0][0] == "직접고정" || Value2[0][0] == "트러스(점형)")
                {
                    string[][] TB = Program.DB.getValue(DB.type.BaseDB_HCneed, "외벽점형열교", "A,B,C,수직간격,수평간격", "열교유형 ='" + Value2[0][0] + "' and 제품명='단열앙카'");
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
        private void Alt_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Alt_dataGridView.Rows[i].Cells[0].Value = false; }
                    else {  Alt_dataGridView.Rows[i].Cells[0].Value = true;   }
                }
                int row = GetSelectedIndex();
                if (row > -1)
                {
                    string 리모델링안 = Alt_dataGridView.Rows[row].Cells[2].Value.ToString();
                    if(리모델링안!=null&& 리모델링안!="")
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "외벽유형", "구분='" + 리모델링안 + "'");
                        if (Value.Length > 0 && Value[0][0] != "")
                        {
                            Load_Select_Remodling(Value[0][0]);
                        }
                    }
                }
            }
        }
        private void Load_Select_Remodling(string 외벽유형)
        {
            new StackedHeaderDecorator(Ucalc_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, Ucalc_dataGridView_RowHandle);
            Ucalc_dataGridView.Columns.Clear();
            Ucalc_dataGridView.Rows.Clear();
            Ucalc_dataGridView.Columns.Add("A0", "번호");
            Ucalc_dataGridView.Columns.Add("A1", "재료명         ");
            Ucalc_dataGridView.Columns.Add("A2", "열전도율.[W/m·K]");
            Ucalc_dataGridView.Columns.Add("A3", "두께.[mm]");
            Ucalc_dataGridView.Columns.Add("A4", "열저항.[m²·K/W]");
            Ucalc_dataGridView.Columns.Add("A5", "Color");
            Ucalc_dataGridView.Columns[0].Width = 40;
            Ucalc_dataGridView.Columns[2].Width = 70;
            Ucalc_dataGridView.Columns[3].Width = 70;
            Ucalc_dataGridView.Columns[4].Width = 70;
            Ucalc_dataGridView.Columns[5].Visible = false;

            string[][] Value = Program.DB.getValue(DB.type.BaseDB_Optimal,"최적안_외벽", "재료,열전도율,두께,열저항", "구분='" + 외벽유형 + "'");
            if (Value.Length > 0)
            {
                for(int a= 0; a < Value.Length; a++)
                {
                    if(Value[a][0]== "기존 외벽")  { Add_OldWall(); }
                    else
                    {
                        int nRow = Ucalc_dataGridView.Rows.Add();
                        Ucalc_dataGridView.Rows[nRow].Cells[0].Value = nRow + 1;
                        Ucalc_dataGridView.Rows[nRow].Cells[1].Value = Value[a][0];
                        if (Value[a][1] != "") { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = Value[a][1]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[2].Value = "-"; }
                        if (Value[a][2] != "") { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = Value[a][2]; }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[3].Value = "-"; }
                        if (Value[a][3] != "") { Ucalc_dataGridView.Rows[nRow].Cells[4].Value = Convert.ToDouble(Value[a][3]).ToString("0.00"); }
                        else { Ucalc_dataGridView.Rows[nRow].Cells[4].Value = "-"; }
                        Ucalc_dataGridView.Rows[nRow].Cells[5].Value = "DDEBF7";
                    }
                    
                }
                Load_Graph();
            }
        }
        private void Load_Graph()
        {
            webView21.Visible = true;
            String[] Material = new String[10];
            double[] Material_d = new double[10];//두께
            double[] Material_R = new double[10];
            double[] Material_T = new double[12]; //온도
            double Rsi = 0.13, Rse = 0.04;
            double dtot = 0; double Rtot = 0; 
            for (int k = 0; k < Ucalc_dataGridView.RowCount; k++)
            {
                if (Ucalc_dataGridView.Rows[k].Cells[1].Value != null)
                {
                    Material[k] = Ucalc_dataGridView.Rows[k].Cells[1].Value.ToString();
                }

                if (Ucalc_dataGridView.Rows[k].Cells[3].Value != null && Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString() !="-" && Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString() != "Var")
                {
                    Material_d[k] = Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[3].Value);
                }else if (Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString() == "Var")
                {
                    Material_d[k] = 200;
                }

                if (Ucalc_dataGridView.Rows[k].Cells[4].Value != null && Ucalc_dataGridView.Rows[k].Cells[4].Value.ToString() != "-" && Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString() != "Var")
                {
                    Material_R[k] = Convert.ToDouble(Ucalc_dataGridView.Rows[k].Cells[4].Value);
                }
                else if (Ucalc_dataGridView.Rows[k].Cells[3].Value.ToString() == "Var")
                {
                    Material_R[k] = 1 / 0.58;
                }
                dtot += Material_d[k];
                Rtot += Material_R[k];
            }
            Rtot = Rsi + Rse + Rtot;

            double Q = (20 - (-5)) / Rtot;

            Material_T[0] = (20 - Q * Rsi);
            for (int k = 1; k < Ucalc_dataGridView.RowCount + 1; k++)
            {
                Material_T[k] = (Material_T[k - 1] - Q * Material_R[k - 1]);
            }
            Material_T[Ucalc_dataGridView.RowCount + 1] = Material_T[Ucalc_dataGridView.RowCount] - Q * Rse;
            int i = 0;
            int count = Ucalc_dataGridView.RowCount + 1;
            string s = "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 80,\"temper\":  " + Material_T[0] + "},";

            while (++i < count)
            {
                var cate = Ucalc_dataGridView.Rows[i - 1].Cells[2].Value != null ? Ucalc_dataGridView.Rows[i - 1].Cells[2].Value.ToString() : "---";
                var color = Ucalc_dataGridView.Rows[i - 1].Cells[5].Value != null ? Ucalc_dataGridView.Rows[i - 1].Cells[5].Value.ToString() : "DCDCDC";
                s += "{\"cate\":\"" + cate + "\",\"bgcolor\":\"" + color + "\",\"width\": " + Material_d[i - 1] + ",\"temper\":  " + Material_T[i] + "},";
            }

            s += "{\"cate\":\"---\",\"bgcolor\":\"FFFFFF\",\"width\": 80,\"temper\":  " + Material_T[i] + "},";

            runScript("drawWall([" + s + "])");
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
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
            Ucalc_dataGridView.Rows[nRow].Cells[5].Value = "6e6e6e";
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
        private void Save_button_Click(object sender, EventArgs e)
        {

        }

    }
}

