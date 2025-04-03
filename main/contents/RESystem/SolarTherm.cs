using main.contentslist;
using main.subcontents.RESystem_PV;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace main.contents
{
    public partial class SolarTherm : Form
    {

        bool scriptable = false;
        //일반정보
        string Num;
        string 지역, 프로젝트유형, Ins;

        //태양열 정보
        string install, installpart, installsystem, solarnum, systemnum, direction, slope; //설치, 적용설비, 적용유형(난방,급탕), 태양열번호, 설비번호
        double installednumber, installedarea; //모듈개수

        double[] ene = new double[12], sol = new double[12], wgen = new double[12];
        double[] dmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        #region 폼

        public SolarTherm()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            
            string[][] val = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (val.Length > 0)
            {
                지역 = val[0][0].ToString();
            }

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양열시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
          
            Create_Table();
        }

        private void Create_Table()
        {
            new StackedHeaderDecorator(st_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            st_dataGridView.Columns.Clear();
            st_dataGridView.Columns.Add("A0", "방위");
            st_dataGridView.Columns.Add("A1", "기울기");
            st_dataGridView.Columns.Add("A2", "음영정보.거리[m]"); //width_combo
            st_dataGridView.Columns.Add("A3", "음영정보.높이[m]"); //height_combo
            st_dataGridView.Columns.Add("A4", "음영정보.집열판높이[m]"); //Arrayheight_combo

            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("A0", "유형"); //보조설비1, 주요설비, 보조설비2
            dataGridView1.Columns.Add("A1", "설비");  //난방, 급탕
            dataGridView1.Columns.Add("A2", "면적.[m" + Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A3", "용량.[kW]"); //height_combo
            dataGridView1.Columns.Add("A4", "정격효율.[%]"); //Arrayheight_combo
        }

        private void Load_Table()
        {
            string[][] stdb = Program.DB.getValue(DB.type.ProjDB, "SolarTherm_Form", "방위,기울기,지형물거리,지형물높이,모듈높이,적용유형,적용설비,모듈개수,태양열번호,설비번호", "번호 = '" + Num + "'");

            //태양열 집열판 설치정보
            st_dataGridView.Rows.Add();
            st_dataGridView.Rows[0].Cells[0].Value = stdb[0][0];
            st_dataGridView.Rows[0].Cells[1].Value = stdb[0][1];
            st_dataGridView.Rows[0].Cells[2].Value = stdb[0][2];
            st_dataGridView.Rows[0].Cells[3].Value = stdb[0][3];
            st_dataGridView.Rows[0].Cells[4].Value = stdb[0][4];

            direction = stdb[0][0];
            slope = stdb[0][1];
            installpart = stdb[0][5];
            installsystem = stdb[0][6];
            solarnum = stdb[0][8];
            systemnum = stdb[0][9];  
            installednumber = Convert.ToDouble(stdb[0][7]);

            //태양열집열판 종합 정보
            string[][] solartherminfo = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "모듈면적,유효열용량,효율,신규기존", "번호 = '" +  solarnum + "'");

            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = installpart;
            dataGridView1.Rows[0].Cells[1].Value = installsystem;
            dataGridView1.Rows[0].Cells[2].Value = string.Format("{0:N2}" , installednumber * Convert.ToDouble(solartherminfo[0][0].ToString()));
            dataGridView1.Rows[0].Cells[3].Value = string.Format("{0:N2}", installednumber * Convert.ToDouble(solartherminfo[0][1].ToString()));
            dataGridView1.Rows[0].Cells[4].Value = string.Format("{0:N0}", 100 * Convert.ToDouble(solartherminfo[0][2].ToString()));
            
            installedarea = installednumber * Convert.ToDouble(solartherminfo[0][0].ToString());
            install = solartherminfo[0][3];
            if (install =="기존") radioButton1.Checked = true;
            else if(install == "신규") radioButton3.Checked = true;

            MainSTimage(stdb[0][1]);
            shadingimage();
        }

        void Qsolinfo()
        {
            string[][] val = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            지역 = val[0][0];

            for (int mth = 0; mth < 12; mth++)
            {
                string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역 + "' AND 방향 ='" + direction + "' AND  각도 = '" + slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
                sol[mth] = Convert.ToDouble(token[0][0]) * 0.024 * dmth[mth];
            }

            if (installsystem == "난방")
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "Qh_sol,Wh_g", "번호 = '" + systemnum + "' and 월 ='" + (mth + 1).ToString() + "월'");
                    if (token[0][0].ToString()==null || token[0][0].ToString() == "")
                    {
                        ene[mth] = 0;
                    } else ene[mth] = Convert.ToDouble(token[0][0].ToString())/installedarea;

                    if (token[0][1].ToString() == null || token[0][1].ToString() == "")
                    {
                        wgen[mth] = 0;
                    }
                    else wgen[mth] = Convert.ToDouble(token[0][1].ToString()) / installedarea;
                }
            }
            else if(installsystem == "급탕")
            {
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] token = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Result", "Qw_sol, Ww_g", "번호 = '" + systemnum + "' and 월 ='" + (mth + 1).ToString() + "월'");
                    if (token[0][0].ToString() == null || token[0][0].ToString() == "")
                    {
                        ene[mth] = 0;
                    } else ene[mth] = Convert.ToDouble(token[0][0].ToString()) / installedarea;
                    
                    if (token[0][1].ToString() == null || token[0][1].ToString() == "")
                    {
                        wgen[mth] = 0;
                    }
                    else wgen[mth] = Convert.ToDouble(token[0][1].ToString()) / installedarea;
                }
            }
        }

        void MainSTimage(string type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양열이미지", "이미지", "유형 = '" + type + "'");
            if (Image.Length > 0)
            {
                STpictureBox.Size = new System.Drawing.Size(250, 250);
                STpictureBox.Location = new Point(750, 0);
                STpictureBox.Load(Program.gPath + Image[0][0]);
                STpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                STpictureBox.BackColor = Color.Transparent;
            }
        }
      
        void shadingimage()
        {
            string type = "shading";
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양열이미지", "이미지", "유형 = '"+type+"'");
            if (Image.Length > 0)
            {
                ShpictureBox.Size = new System.Drawing.Size(250, 250);
                ShpictureBox.Location = new Point(500,0);
                ShpictureBox.Load(Program.gPath + Image[0][0]);
                ShpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                ShpictureBox.BackColor = Color.Transparent;
            }
        }

        #endregion
        #region 세이브

        private void Save_button_Click(object sender, EventArgs e)
        {
           
        }
        public static bool OnLoadListProc(Form form)
        {
            List_SolarTherm f = (List_SolarTherm)form;
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
            Qsolinfo();
            LoadGraph(ene,sol);
            Load_Textbox();
        }
        void Load_Textbox()
        {
            double a = 0, b = 0, c=0;

            for(int i = 0; i<12; i++)
            {
                a += ene[i];
                b += sol[i];
                c += wgen[i];
            }

            allcapacity.Text = string.Format("{0:N0}", a * installedarea);
            auxcapacity.Text = string.Format("{0:N0}", c * installedarea);
            averagecpacity.Text = string.Format("{0:N0}", 100*a/b);
        }

        private void Reset()
        {
            Num = null; Name = null;
            지역 = null; 프로젝트유형 = null;

            st_dataGridView.Columns.Clear();
            st_dataGridView.Rows.Clear();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            install = null;
            installpart = null;
            installsystem = null;
            solarnum = null;
            systemnum = null;
            installednumber = 0;

            allcapacity.Text = null;
            auxcapacity.Text = null;
            averagecpacity.Text = null;

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
        private void LoadGraph(double[] Qsolar, double[] Sol)
        {
            double[] Qsolarm2_kWh = new double[12], Solm2_kWh = new double[12];
            try
            {
                for (int j = 0; j < 12; j++)
                {
                    Qsolarm2_kWh[j] = Qsolar[j];
                    Solm2_kWh[j] = Sol[j];
                }

                string s = "", s2 = "";

                double max1 = 0, max2 = 0;
                for (int mth = 0; mth < 11; mth++)
                {

                    s += Qsolarm2_kWh[mth] + ",";
                    s2 += Solm2_kWh[mth] + ",";
                }

                s += Qsolarm2_kWh[11];
                s2 += Solm2_kWh[11];

                int n2 = ((int)Solm2_kWh.Max()).ToString().Length;
                max2 = Convert.ToInt64((Solm2_kWh.Max()) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1) / 2;
                int n1 = ((int)Qsolarm2_kWh.Max()).ToString().Length;
                max1 = Convert.ToInt64((Qsolarm2_kWh.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) / 2;
                string unit = "kWh/m" + Program.UTIL.Subscript(2, true) + "·mth";
                runScript("drawChart_pv([{type:\"line\",label:\"일사량(" + unit + ")\",data:[" + s2 + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:" + max2 + "},{type:\"bar\",label:\"생산량(" + unit + ")\",data:[" + s + "],borderColor:\"#ffffee0\",backgroundColor:\"#FFF6A3\",min:0,max:" + max2 + ",dash:false,barPercentage:0.4}])");
            }
            catch { }
        }
        #endregion
    }
}
