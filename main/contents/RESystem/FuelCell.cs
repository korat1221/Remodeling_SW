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
        string 지역, 프로젝트유형;


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
        void Load_Textbox()
        {
            double[] elec = new double[12], heat = new double[12];
            String[][] db = Program.DB.getValue(DB.type.ProjDB, "FC_Form", "연료전지번호,설비번호,적용설비","번호='"+Num+"'");
            if(db.Length >0)
            {
                if (db[0][2] =="난방")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기'");
                    if (v.Length > 0 && v[0][0] != "")
                    { elec_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0") ; }
                   
                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열'");
                    if (v.Length > 0 && v[0][0] != "")
                    { heat_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0"); }
                   
                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스'");
                    if (v.Length > 0 && v[0][0] != "")
                    { gas_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0"); }

                    for(int mth =0; mth <12; mth++)
                    {
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기' and 월 ='"+(mth+1)+ "월'");
                        if(v.Length >0)
                        {
                            elec[mth] = Convert.ToDouble(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "난방설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                           heat[mth] = Convert.ToDouble(v[0][0]);
                        }
                    }
                    

                }
                else if (db[0][2] == "급탕")
                {
                    string[][] v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기'");
                    if (v.Length > 0 && v[0][0] != "")
                    { elec_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0"); }

                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열'");
                    if (v.Length > 0 && v[0][0] != "")
                    { heat_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0"); }

                    v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "SUM(총에너지)", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='소비' and 소비연료='가스'");
                    if (v.Length > 0 && v[0][0] != "")
                    { gas_textBox.Text = Convert.ToDouble(v[0][0]).ToString("#,##0"); }

                    for (int mth = 0; mth < 12; mth++)
                    {
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='전기' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            elec[mth] = Convert.ToDouble(v[0][0]);
                        }
                        v = Program.DB.getValue(DB.type.ProjDB, "RESystem_Result", "총에너지", "급탕설비 = '" + db[0][1] + "' and 신재생시스템= '" + db[0][0] + "' and 신재생시스템유형='연료전지' and 생산소비 ='생산' and 생산유형='열' and 월 ='" + (mth + 1) + "월'");
                        if (v.Length > 0)
                        {
                            heat[mth] = Convert.ToDouble(v[0][0]);
                        }
                    }

                }

                LoadGraph(elec, heat);

            }
           
        }

        private void Reset()
        {
            Num = null; Name = null;
            지역 = null; 프로젝트유형 = null;


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
        private void LoadGraph(double[] elec, double[] heat)
        {
            try
            {
                string s3 = null; string s32 = null;
                double max1 = 0, max2 = 0;
                for (int mth = 0; mth < 11; mth++)
                {
                    s3 += elec[mth].ToString("#,##0") + ",";
                    s32 += heat[mth].ToString("#,##0") + ",";
                }

                string s2 = "[" + s3 + "]";
                string s22 = "[" + s32 + "]";

                int n2 = ((int)elec.Max()).ToString().Length;
                max1 = Convert.ToInt64((elec.Max()) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1) / 2;
                int n1 = ((int)heat.Max()).ToString().Length;
                max2 = Convert.ToInt64((heat.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) / 2;

                double max = Math.Max(max1, max2);
                string unit = "kWh/mth";
                string s = "";

                string randomOrangeColor = GetRandomOrangeColor();
                s += "{label:\"" + "전기생산량" + "\",type:\"line\",data:" + s2 + ",yAxisTitle:\"에너지사용량[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomOrangeColor + "\",backgroundColor:\"" + randomOrangeColor + "\",dash:true,tension: 0.4},";
              
                randomOrangeColor = GetRandomOrangeColor();
                s += "{label:\"" + "열생산량" + "\",type:\"line\",data:" + s22 + ",yAxisTitle:\"에너지사용량[kWh]\",pointStyle:\"circle\",pointRadius:\"2.5\",borderWidth:\"0.5\",borderColor:\"" + randomOrangeColor + "\",backgroundColor:\"" + randomOrangeColor + "\",dash:true,tension: 0.4},";


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
        #endregion
    }
}
