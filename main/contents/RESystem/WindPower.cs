using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using main.contentslist;
using main.subcontents;
using main.subcontents.ConstructionRoof;
using main.subcontents.HeatingSystem;
using main.subcontents.RESystem_WP;
using main.subcontents.ThermalBridge;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class WindPower : Form
    {

        String[][] 지역;
        String Num, Inverter, Inverter_num;
        double Euro, h2;
        String WP, Type, SubType;
        double RotateArea, HerbHeight, Install;
        string Condition, 프로젝트유형;
        bool scriptable = false;

        public double[] h_mth = new double[12];
        public double[] Pwind_mth = new double[12];
        public double[] Pwps_mth = new double[12];
        public double[] Qfwps_mth = new double[12];


        #region 폼

        public WindPower()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);     /////////////////////////////////////그래프수정


            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '풍력시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] 프로젝트 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호,프로젝트번호");
            if (프로젝트.Length > 0)
            {
                프로젝트유형 = 프로젝트[0][0];
            }

            //주변환경콤보박스
            Condition_ComboBox.Items.Clear();
            Condition_ComboBox.Items.Add("도심");
            Condition_ComboBox.Items.Add("도시주변 및 산림지역");
            Condition_ComboBox.Items.Add("개방된 평지/초원/바다");
            Condition_ComboBox.SelectedIndex = 0;

            label10.Visible = false;
            label12.Visible = false;

            label11.Visible = false;
            label13.Visible = false;

            label18.Visible = false;
            label5.Visible = false;

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

        }

        private void AdditionalPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

        }
        private void tableMake()
        {
            WP_dataGridView.Visible = true;

            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WP_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WP_dataGridView.Columns.Add(checkBoxColumn);
            WP_dataGridView.Columns.Add("A1", "번호");
            WP_dataGridView.Columns.Add("A2", "DB유형");
            WP_dataGridView.Columns.Add("A3", "제품명");
            WP_dataGridView.Columns.Add("A4", "제조사");
            WP_dataGridView.Columns.Add("A5", "정격출력.[Kw]");
            WP_dataGridView.Columns.Add("A6", "시동풍속.[m/s]");
            WP_dataGridView.Columns.Add("A7", "최적풍속.[m/s]");
            WP_dataGridView.Columns.Add("A8", "종단풍속.[m/s]");
            WP_dataGridView.Columns.Add("A9", "전력계수.시동풍속.[-]");
            WP_dataGridView.Columns.Add("A10", "전력계수.최적풍속.[-]");
            WP_dataGridView.Columns.Add("A11", "전력계수.종단풍속.[-]");
            WP_dataGridView.Columns.Add("A12", "설치");
            WP_dataGridView.Columns.Add("A13", "대수");
        }


        private void WPDB_button_Click(object sender, EventArgs e)
        {
            if (Name_textBox.Text == "" || Name_textBox.Text == null)
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요");
            }
            else
            {
                subcontents.WP_DB wp_DB = new subcontents.WP_DB();
                DialogResult result = wp_DB.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //테이블 생성
                    tableMake();

                    //string[] token = wp_DB.SelectWPnonsplit.Split('+');
                    //WPNameText.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";
                    //string[] token = wp_DB.SelectWPnonsplit.Split('+');
                    WPNameText.Text = wp_DB.SelectWPnonsplit;

                    WP_dataGridView.Rows.Clear();
                    //로드

                    //for (int i = 0; i < token.Length; i++)
                    // {
                    Load_WPDB(wp_DB.SelectWPnonsplit);

                    // }
                }
            }
        }

        private void Load_WPDB(string SelectWPnonsplit )
        {
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품명,제조사,정격출력,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수,신규기존,타입,세부타입,회전면적,허브높이", "번호 =  '" + SelectWPnonsplit + "'");
            if (value.Length > 0)
            {
                WP_dataGridView.Rows.Add();
                int n = WP_dataGridView.Rows.Count - 1;
                WP_dataGridView.Rows[n].Cells[1].Value = value[0][0];
                WP_dataGridView.Rows[n].Cells[2].Value = value[0][1];
                WP_dataGridView.Rows[n].Cells[3].Value = value[0][2];
                WP_dataGridView.Rows[n].Cells[4].Value = value[0][3];
                WP_dataGridView.Rows[n].Cells[5].Value = value[0][4];
                WP_dataGridView.Rows[n].Cells[6].Value = value[0][5];
                WP_dataGridView.Rows[n].Cells[7].Value = value[0][6];
                WP_dataGridView.Rows[n].Cells[8].Value = value[0][7];
                WP_dataGridView.Rows[n].Cells[9].Value = value[0][8];
                WP_dataGridView.Rows[n].Cells[10].Value = value[0][9];
                WP_dataGridView.Rows[n].Cells[11].Value = value[0][10];
                WP_dataGridView.Rows[n].Cells[12].Value = value[0][11];

                //풍력,타입,세부타입,회전면적,허브높이
                WP = value[0][0];
                Type = value[0][12];
                SubType = value[0][13];
                RotateArea = Convert.ToDouble(value[0][14]);
                HerbHeight = Convert.ToDouble(value[0][15]);

                Type_textBox.Text = Type;
                Typesub_textBox.Text = SubType;
                RotateArea_textBox.Text = RotateArea.ToString();
                HerbHeight_textBox.Text = HerbHeight.ToString();
                Load_WPType_image(Type, SubType);

                label10.Visible = true;
                label12.Visible = true;

                label11.Visible = true;
                label13.Visible = true;

                label18.Visible = true;
                label5.Visible = true;
            }

        }

        private void WPInverter_button_Click(object sender, EventArgs e)
        {
            if (Name_textBox.Text == "" || Name_textBox.Text == null)
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요");
            }
            else
            {
                WP_InverterDB wpinverter = new WP_InverterDB();
                DialogResult result = wpinverter.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Inverter = wpinverter.Select_WPInverter[2]; //제품명
                    Euro = Convert.ToDouble(wpinverter.Select_WPInverter[4]); //EURO효율
                    Inverter_num = wpinverter.Select_WPInverter[0]; //번호;
                    Inverter_textBox.Text = Inverter;
                    EURO_textBox.Text = Euro.ToString();
                }
            }
        }

        private void Load_WPType_image(string Type,string SubType)
        {
            if (Type == "수평형")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (9)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (SubType == "사보니우스")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (10)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (SubType == "다리우스")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (11)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (SubType == "H-Blade")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (12)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (SubType == "복합형")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (13)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else { }
        }


        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(55, OnLoadListProc);
            }
        }
        #endregion


        #region 세이브
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Name_textBox.Text == "")
            {
                MessageBox.Show("풍력시스템 명칭을 입력하세요.");
            }
            else if (WPNameText.Text == "")
            {
                MessageBox.Show("풍력발전 시스템을 선택하세요.");
            }
            else if(Inverter_textBox.Text == "")
            {
                MessageBox.Show("인버터 제품을 선택하세요.");
            }
            else if (h2_textBox.Text == "")
            {
                MessageBox.Show("설치높이를 입력하세요.");
            }
            else
            {
                Save();
                
            }

        }
        private void Save()
        {

            if (WP_dataGridView.Rows[0].Cells[13].Value == null || WP_dataGridView.Rows[0].Cells[13].Value == "")
            {
                MessageBox.Show("대수입력을 완료해주세요.");
            }
            else
            {
                Install = Program.UTIL.dataGridView_doubleComa(WP_dataGridView, 0, 13, 0);
                h2 = Convert.ToDouble(h2_textBox.Text);
                Condition = Condition_ComboBox.SelectedItem.ToString();

                Program.DB.setValue(DB.type.ProjDB, "WindPower_Form", "번호,프로젝트유형,명칭,풍력,주변환경,설치높이,인버터제품,인버터,설치대수",
                   "'" + Num + "','" + 프로젝트유형 + "','" + Name_textBox.Text + "','" + WP + "','" +
                   Condition + "','" + h2 + "','" +
                   Inverter + "','" + Inverter_num + "','" + Install + "'", "번호");

                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    CALC.WPCalc(Value[0][0]);
                    LoadGraph();
                }
                MessageBox.Show("풍력시스템" + "[" + Num + "] 정보를 저장하였습니다.");

                //this.DialogResult = DialogResult.OK;
                //this.Hide();
                List_WindPower f =  new List_WindPower();
                f.load_List();
            }
        }


        public static bool OnLoadListProc(Form form)
        {
            List_WindPower f = (List_WindPower)form;
            f.load_List();
            return true;
        }

        #endregion

        #region 로드
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            Reset();

            Num_textBox.Text = ID;
            Num = ID;

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "WindPower_Form", "프로젝트유형,명칭,풍력,주변환경,설치높이,인버터제품,인버터,설치대수", "번호='" + Num + "'");
            if (value.Length > 0)
            {
                Name_textBox.Text = value[0][1];
                WPNameText.Text = value[0][2];
                tableMake();
                Load_WPDB(value[0][2]);
                Condition_ComboBox.SelectedItem = value[0][3];
                h2_textBox.Text = value[0][4];
                Inverter_textBox.Text = value[0][5];
                Inverter = value[0][5];
                Inverter_num = value[0][6];
                if (WP_dataGridView.Rows.Count > 0)
                {
                    WP_dataGridView.Rows[0].Cells[13].Value = value[0][7];
                }               
            }

            //인버터 null 아니면 효율 매치해서 불러오기 
            if (Inverter_textBox.Text != null)
            {
                if (Inverter_num.Contains("U"))
                {
                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "User_WPInverter", "EURO효율", "제품명='" + Inverter + "'");
                    if (value2.Length > 0)
                    {
                        EURO_textBox.Text = value2[0][0];  
                        Euro =  Convert.ToDouble(value2[0][0]);
                    }
                }
                else
                {
                    string[][] value3 = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력인버터DB", "EURO효율", "제품명='" + Inverter + "'");
                    if (value3.Length > 0)
                    {
                        EURO_textBox.Text = value3[0][0];
                        Euro = Convert.ToDouble(value3[0][0]);
                    }
                }
            }
            LoadGraph();
        }
        #endregion

        #region 리셋
        private void Reset()
        {
            Num = null;  Inverter = null; Inverter_num = null;
            Euro = 0; h2 = 0;
            WP = null; Type = null; SubType = null; Condition = null;
            RotateArea = 0; HerbHeight = 0; 
            Install = 0;

            WP_dataGridView.Rows.Clear();
            WP_dataGridView.Visible = false;
            Name_textBox.Text = null;
            WPNameText.Text = null;
            h2_textBox.Text = null;
            Inverter_textBox.Text = null;
            EURO_textBox.Text = null;
            Condition_ComboBox.SelectedIndex = 0;

            Type_textBox.Text = null;
            Typesub_textBox.Text = null;
            RotateArea_textBox.Text = null;
            HerbHeight_textBox.Text = null;
         
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }
        #endregion

        #region 그래프
        /////////////////////////////////////////////////////그래프 로드 수정/////////////////////////////////////////////////////////////////
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

        private void LoadGraph()
        {
            if (Name_textBox.Text != null)
            {
                string s = "", s2 = "";
                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res1;
                string[][] res2;
                double max1 = 0;
                double max2 = 0;
                for (int mth = 0; mth < 12; mth++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "WindPower_Result", "h, Pwind, Pwps, Qfwps", "번호='" + Num + "' And 월 ='" + (mth + 1).ToString() + "월'");
                    if (Value.Length > 0)
                    {
                        h_mth[mth] = Convert.ToDouble(Value[0][0]);
                        Pwind_mth[mth] = Convert.ToDouble(Value[0][1]);
                        Pwps_mth[mth] = Convert.ToDouble(Value[0][2]);
                        Qfwps_mth[mth] = Convert.ToDouble(Value[0][3]);
                        s += Qfwps_mth[mth] + ",";
                    }
                }
               
                int n1 = ((int)Qfwps_mth.Max()).ToString().Length;
                max1 = Convert.ToInt64((Qfwps_mth.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) ;    
                if(s!="")
                {
                    runScript("drawChart_wp([{type:\"line\",label:\"전기생산량\",data:[" + s + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:" + max1 + "}])");
                }                
            }
            else
            {
                webView21.Visible = false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion

        }
    }
}
