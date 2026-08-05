using main.contentslist;
using main.subcontents.RESystem_PV;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Reflection.Emit;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace main.contents
{
    public partial class FuelCell : Form
    {

        bool scriptable = false;
        //일반정보
        string Num;
        string 지역, 프로젝트번호;
        ArrayList SelectZone_split = new ArrayList();

        #region 폼

        public FuelCell()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            
            string[][] val = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (val.Length > 0)
            {
                지역 = val[0][0].ToString();
            }

            pictureBox1.Load(Program.gPath + "images/2ndicon/6_2FuelCell.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            Create_Table();
        }

        private void Create_Table()
        {
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            FC_dataGridView.Columns.Clear();
            FC_dataGridView.Columns.Add("A0", "번호");
            FC_dataGridView.Columns.Add("A1", "명칭");
            FC_dataGridView.Columns.Add("A2", "연료");
            FC_dataGridView.Columns.Add("A3", "전기.출력.[kW]");
            FC_dataGridView.Columns.Add("A4", "전기.효율.[%]");
            FC_dataGridView.Columns.Add("A5", "열.출력.[kW]");
            FC_dataGridView.Columns.Add("A6", "열.효율.[%]");
            FC_dataGridView.Columns.Add("A7", "대수.[EA]");
            FC_dataGridView.Columns.Add("A8", "설치유형");
            FC_dataGridView.Columns.Add("A9", "생산유형");
        }

        private void Load_Table()
        {
            FC_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "FC_Form", "연료전지번호,연료전지대수,연료전지설치유형,연료전지생산유형", "번호 = '" + Num + "'");
           if(Value.Length > 0)
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율", "번호 = '" + Value[0][0] + "'");
                if (User_Value.Length > 0)
                {
                    int nRow = FC_dataGridView.Rows.Add();
                    for (int k = 0; k < 7; k++)
                    {
                        FC_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][k];
                    }

                    for(int k =1; k<=3;  k++)
                    {
                        FC_dataGridView.Rows[nRow].Cells[k + 6].Value = Value[0][k];
                    }
                }
            }

        }


        #endregion
        #region 세이브

        private void Save_button_Click(object sender, EventArgs e)
        {
           
        }
        public static bool OnLoadListProc(Form form)
        {
            List_FuelCell f = (List_FuelCell)form;
            f.load_List();
            return true;
        }
       
        #endregion

        #region 로드
        public void LoadData(String ID)
        {
            Reset();
            Num_textBox.Text = ID;
            Num = ID;
            Create_Table();
            Load_Table();
            //LoadGraph(ene,sol);
            Load_Textbox();
        }
        private void Split_Zone_heating(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
                Zone_textBox.Text = 내용;

                if (SelectZone_split.Count > 0 && SelectZone_split[0] != "")
                {
                    double Qba = 0, Qmax = 0, Area = 0;
                    for (int a = 0; a < SelectZone_split.Count; a++)
                    {
                        string[][] 요구량 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a, Q_max", "번호 ='" + SelectZone_split[a].ToString() + "' AND 난방_냉방 = '난방'");
                        if (요구량.Length > 0)
                        {
                            Qba += Program.UTIL.ToDoubleOrZero(요구량[0][0]);
                            Qmax += Program.UTIL.ToDoubleOrZero(요구량[0][1]) / 1000;
                        }
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적", "존번호 ='" + SelectZone_split[a].ToString() + "'");
                        if (Value.Length > 0)
                        {
                            Area += Program.UTIL.ToDoubleOrZero(Value[0][0]);
                        }
                    }
                    Zone_Qba_textBox.Text = string.Empty;
                    Zone_Qba_textBox.Text = Qba.ToString();
                    Program.UTIL.textBox_doubleComa(Zone_Qba_textBox, true, 0);
                    Zone_Qmax_textBox.Text = string.Empty;
                    Zone_Qmax_textBox.Text = Qmax.ToString();
                    Program.UTIL.textBox_doubleComa(Zone_Qmax_textBox, true, 2);
                }

            }
            else { 내용 = ""; }
        }

        private void Split_Zone_DHW(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
                Zone_textBox.Text = 내용;

                if (SelectZone_split.Count > 0 && SelectZone_split[0] != "")
                {
                    double Qba = 0, Qmax = 0, Area = 0;
                    for (int a = 0; a < SelectZone_split.Count; a++)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,일일급탕요구량,용도프로필", "존번호 ='" + SelectZone_split[a].ToString() + "'");
                        if (Value.Length > 0)
                        {
                            double Qwb_day = 0, dop_a = 0; double[] theta_e = new double[12]; double[] dmth = new double[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                            if (Value[0][1] != "")
                            {
                                Qwb_day = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                            }
                            for (int mth = 0; mth < 12; mth++)
                            {
                                string[][] 급탕부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "theta_e, dwd_mth", "번호 ='" + SelectZone_split[a].ToString() + "' AND 난방_냉방 = '난방' and 비이용일_이용일='이용일' and 월='" + (mth + 1) + "월'");
                                theta_e[mth] = Program.UTIL.ToDoubleOrZero(급탕부하[0][0]);
                                dop_a += Program.UTIL.ToDoubleOrZero(급탕부하[0][1]);
                            }
                            double[] Qwb_mth = new double[12];
                            for (int mth = 0; mth < 12; mth++)
                            {
                                Qwb_mth[mth] = Qwb_day * dop_a * dmth[mth] / 365 * (-0.02 * theta_e[mth] + 1.25);
                                Qba += Qwb_mth[mth];
                            }
                            string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + Value[0][2] + "'");
                            if (Usage.Length > 0)
                            { Qmax += (Qwb_day * Program.UTIL.ToDoubleOrZero(Usage[0][0])); }

                            Area += Program.UTIL.ToDoubleOrZero(Value[0][0]);
                        }
                    }
                    Zone_Qba_textBox.Text = string.Empty;
                    Zone_Qba_textBox.Text = Qba.ToString();
                    Program.UTIL.textBox_doubleComa(Zone_Qba_textBox, true, 0);
                    Zone_Qmax_textBox.Text = string.Empty;
                    Zone_Qmax_textBox.Text = Qmax.ToString();
                    Program.UTIL.textBox_doubleComa(Zone_Qmax_textBox, true, 2);
                }

            }
            else { 내용 = ""; }

        }
        void Load_Textbox()
        {
            double[] elec = new double[12], heat = new double[12], gas = new double[12];
            String[][] db = Program.DB.getValue(DB.type.ProjDB, "FC_Form", "연료전지번호,설비번호,적용설비","번호='"+Num+"'");
            if(db.Length >0)
            {
                SystemNum_textBox.Text = db[0][1];
                if (db[0][2] =="난방")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "존", "번호 = '" + db[0][1] + "'");
                    if(v.Length > 0)
                    {
                        Split_Zone_heating(v[0][0]);
                    }
                    
                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기'");
                    if (v.Length > 0 && v[0][0] != "")
                    { elec_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0") ; }
                   
                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열'");
                    if (v.Length > 0 && v[0][0] != "")
                    { heat_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0"); }
                   
                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스'");
                    if (v.Length > 0 && v[0][0] != "")
                    { gas_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0"); }

                    for(int mth =0; mth <12; mth++)
                    {
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기' and 월 ='"+(mth+1)+ "월'");
                        if(v.Length >0)
                        {
                            elec[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                           heat[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            gas[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                    }
                    

                }
                else if (db[0][2] == "급탕")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "존", "번호 = '" + db[0][1] + "'");
                    if (v.Length > 0)
                    {
                        Split_Zone_DHW(v[0][0]);
                    }

                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기'");
                    if (v.Length > 0 && v[0][0] != "")
                    { elec_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0"); }

                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열'");
                    if (v.Length > 0 && v[0][0] != "")
                    { heat_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0"); }

                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스'");
                    if (v.Length > 0 && v[0][0] != "")
                    { gas_textBox.Text = Program.UTIL.ToDoubleOrZero(v[0][0]).ToString("#,##0"); }

                    for (int mth = 0; mth < 12; mth++)
                    {
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            elec[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            heat[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            gas[mth] = Program.UTIL.ToDoubleOrZero(v[0][0]);
                        }
                    }

                }

                LoadGraph(elec, heat,gas);

            }
           
        }

        private void Reset()
        {
            Num = null; Name = null;
            지역 = null; 프로젝트번호 = null;


            FC_dataGridView.Columns.Clear();
            FC_dataGridView.Rows.Clear();
            elec_textBox.Text = null;
            heat_textBox.Text = null;

        }
        #endregion
        
        #region 그래프
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }
        private void LoadImage()
        {
            string[][] 프로젝트번호 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
        }
        private void LoadGraph(double[] elec, double[] heat, double[] gas)
        {
            try
            {
                string s3 = null; string s32 = null; string s33 = null;
                double max1 = 0, max2 = 0, max3 = 0;
                for (int mth = 0; mth <= 11; mth++)
                {
                    s3 += elec[mth].ToString("0.0") + ",";
                    s32 += heat[mth].ToString("0.0") + ",";
                    s33 += gas[mth].ToString("0.0") + ",";
                }

                string s2 = "[" + s3 + "]";
                string s22 = "[" + s32 + "]";
                string s23 = "[" + s33 + "]";

                int n2 = ((int)elec.Max()).ToString().Length;
                max1 = Convert.ToInt64((elec.Max()) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1) / 2;
                int n1 = ((int)heat.Max()).ToString().Length;
                max2 = Convert.ToInt64((heat.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) / 2;
                int n3 = ((int)gas.Max()).ToString().Length;
                max3 = Convert.ToInt64((gas.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) / 2;

                double max = Math.Max(Math.Max(max1, max2), max3);
                string unit = "kWh/mth";
                string s = "";

                string randomOrangeColor = GetRandomOrangeColor();
                s += "{label:\"" + "전기생산량" + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"에너지[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomOrangeColor + "\",backgroundColor:\"" + randomOrangeColor + "\",dash:true,tension: 0.4},";
              
                string randomBlueColor = GetRandomBlueColor();
                s += "{label:\"" + "열생산량" + "\",type:\"line\",data:" + s22 + ",yAxisTitle:\"에너지[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomBlueColor + "\",backgroundColor:\"" + randomBlueColor + "\",dash:true,tension: 0.4},";

                string randomGrayColor = GetRandomGrayColor();
                s += "{label:\"" + "연료소비량" + "\",type:\"line\",data:" + s23 + ",yAxisTitle:\"에너지[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomGrayColor + "\",backgroundColor:\"" + randomGrayColor + "\",dash:true,tension: 0.4},";


                runScript("drawChart_energyuse([" + s + "]," + max.ToString() + ")");
            }
            catch { }
        }
        private Random random = new Random();
        public string GetRandomOrangeColor()
        {
            int r = random.Next(200, 256); // 빨강: 높음
            int g = random.Next(80, 130);  // 초록: 중간
            int b = random.Next(0, 50);    // 파랑: 낮음
            return $"rgba({r}, {g}, {b}, 1)";
        }
        public string GetRandomBlueColor()
        {
            int r = random.Next(0, 70);     // 빨강: 낮음
            int g = random.Next(80, 160);   // 초록: 중간
            int b = random.Next(180, 256);  // 파랑: 높음

            return $"rgba({r}, {g}, {b}, 1)";
        }
        public string GetRandomGrayColor()
        {
            int gray = random.Next(80, 200); // 너무 어둡거나 너무 밝지 않게

            return $"rgba({gray}, {gray}, {gray}, 1)";
        }
        #endregion
    }
}
