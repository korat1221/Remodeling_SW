using HidSharp.Reports.Units;
using main.contentslist;
using main.subcontents.EquipmentList;
using main.subcontents.RESystem_WP;
using Microsoft.Web.WebView2.Core;
using System.Data;
using System.Drawing.Drawing2D;

namespace main.contents
{
    public partial class WindPower : Form
    {

        String[][] 지역;
        String Num;
        double Euro, h2;
        String WP, Type;
        double v_start, v_end;
        double RotateArea, HerbHeight, Install;
        string Condition, 프로젝트유형, 적용유형;
        bool scriptable = false;
        public double[] Qfwps = new double[12];


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
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "지면거칠기계수", "지형구분");
            if (Value.Length > 0)
            {
                for (int a = 0; a < Value.Length; a++)
                {
                    Condition_ComboBox.Items.Add(Value[a][0]);
                }
                Condition_ComboBox.SelectedIndex = 0;

            }
            label10.Visible = false;
            label12.Visible = false;

            label11.Visible = false;
            label13.Visible = false;

            label18.Visible = false;

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
            WP_dataGridView.Columns.Add("A3", "일반정보.타입");
            WP_dataGridView.Columns.Add("A4", "일반정보.R.[m]");
            WP_dataGridView.Columns.Add("A5", "일반정보.D.[m]");
            WP_dataGridView.Columns.Add("A6", "일반정보.H.[m]");
            WP_dataGridView.Columns.Add("A7", "일반정보.회전면적.[m²]");
            WP_dataGridView.Columns.Add("A8", "일반정보.허브높이.[m]");
            WP_dataGridView.Columns.Add("A9", "성능정보.시동풍속.[m/s]");
            WP_dataGridView.Columns.Add("A10", "성능정보.종단풍속.[m/s]");
            WP_dataGridView.Columns.Add("A11", "성능정보.정격출력.출력.[W]");
            WP_dataGridView.Columns.Add("A12", "성능정보.정격출력.풍속.[m/s]");
            WP_dataGridView.Columns.Add("A13", "설치");
            WP_dataGridView.Columns.Add("A14", "대수");
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

                    WPNameText.Text = wp_DB.SelectWPnonsplit;

                    WP_dataGridView.Rows.Clear();
                    Load_WPDB(wp_DB.SelectWPnonsplit);
                }
            }
        }

        private void Load_WPDB(string SelectWPnonsplit)
        {
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품유형,R,D,H,회전면적,허브높이,시동풍속,종단풍속,정격출력,정격출력풍속,신규기존,적용유형", "번호 =  '" + SelectWPnonsplit + "'");
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
                WP_dataGridView.Rows[n].Cells[13].Value = value[0][12];

                //풍력,타입,세부타입,회전면적,허브높이
                WP = value[0][0];
                Type = value[0][2];
                v_start = Program.UTIL.ToDoubleOrZero( value[0][8]);
                v_end = Program.UTIL.ToDoubleOrZero(value[0][9]);
                RotateArea = Program.UTIL.ToDoubleOrZero(value[0][6]);
                HerbHeight = Program.UTIL.ToDoubleOrZero(value[0][7]);
                적용유형 = value[0][13];
                if(적용유형=="제품값")
                {
                    Range_button.Visible = true;
                }else
                {
                    Range_button.Visible = false;
                }

                Type_textBox.Text = Type;
                RotateArea_textBox.Text = RotateArea.ToString();
                HerbHeight_textBox.Text = HerbHeight.ToString();

                label10.Visible = true;
                label12.Visible = true;

                label11.Visible = true;
                label13.Visible = true;

                label18.Visible = true;
                Load_WPType_image(Type);
            }

        }


        private void Load_WPType_image(string Type)
        {
            if (Type == "수평형")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (9)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (Type == "다리우스")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (11)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (Type == "H-다리우스")
            {
                WPtype_pictureBox.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (12)" + @".png");
                WPtype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else { }
        }


        private void Range_button_Click(object sender, EventArgs e)
        {
            WPPower form = new WPPower(WP, v_start, v_end);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                
            }
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
                Install = Program.UTIL.dataGridView_doubleComa(WP_dataGridView, 0, 14, 0);
                h2 = Program.UTIL.ToDoubleOrZero(h2_textBox.Text);
                Condition = Condition_ComboBox.SelectedItem.ToString();

                Program.DB.setValue(DB.type.ProjDB, "WindPower_Form", "번호,프로젝트유형,명칭,풍력,주변환경,설치높이,설치대수",
                   "'" + Num + "','" + 프로젝트유형 + "','" + Name_textBox.Text + "','" + WP + "','" +
                   Condition + "','" + h2 + "','" + Install + "'", "번호");

                Program.DB.saveProject();

                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    CALC.WPCalc(Value[0][0]);
                    LoadGraph();
                }

                MessageBox.Show("풍력시스템" + "[" + Num + "] 정보를 저장하였습니다.");

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

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "WindPower_Form", "프로젝트유형,명칭,풍력,주변환경,설치높이,설치대수", "번호='" + Num + "'");
            if (value.Length > 0)
            {
                Name_textBox.Text = value[0][1];
                WPNameText.Text = value[0][2];
                tableMake();
                Load_WPDB(value[0][2]);
                Condition_ComboBox.SelectedItem = value[0][3];
                h2_textBox.Text = value[0][4];
                if (WP_dataGridView.Rows.Count > 0)
                {
                    WP_dataGridView.Rows[0].Cells[14].Value = value[0][5];
                }
            }

            LoadGraph();
        }
        #endregion

        #region 리셋
        private void Reset()
        {
            Num = null;
            Euro = 0; h2 = 0;
            WP = null; Type = null; Condition = null;
            RotateArea = 0; HerbHeight = 0;
            Install = 0;

            WP_dataGridView.Rows.Clear();
            WP_dataGridView.Visible = false;
            Name_textBox.Text = null;
            WPNameText.Text = null;
            h2_textBox.Text = null;
            Condition_ComboBox.SelectedIndex = 0;

            Type_textBox.Text = null;
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
                string s = "", s2 = ""; string v = "";
                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res1;
                string[][] res2;
                double max1 = 0;
                double max2 = 0;
                double sum = 0;
                double[] v_mth = new double[12];
                for (int mth = 0; mth <= 11; mth++)
                {
                    string[][] Value = Program.DB.getValue(DB.type.ProjDB, "WindPower_Result", "h, Pwind, Pwps, Qfwps", "번호='" + Num + "' And 월 ='" + (mth + 1).ToString() + "월'");
                    if (Value.Length > 0)
                    {
                        Qfwps[mth] = Program.UTIL.ToDoubleOrZero(Value[0][3]);
                        sum += Qfwps[mth];
                        s += Qfwps[mth] + ",";
                    }
                }
                annual_textBox.Text = sum.ToString("0.00");

                #region 풍속 평균 
                string[][] Value3 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                if (Value3.Length > 0)
                {
                    string 지역 = Value3[0][0];
                    for (int mth = 1; mth <= 12; mth++)
                    {
                        string[][] Value2 = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "AVG(풍속)", "지역='" + 지역 + "' and 월='" + mth + "'");
                        if (Value2.Length > 0)
                        {
                            v_mth[mth - 1] = Program.UTIL.ToDoubleOrZero(Value2[0][0]);
                            v += v_mth[mth - 1] + ",";
                        }
                    }
                }
                #endregion 

                int n1 = ((int)Qfwps.Max()).ToString().Length;
                max1 = Convert.ToInt64((Qfwps.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1);
                int n2 = ((int)v_mth.Max()).ToString().Length;
                max2 = 5;
                if (s != "")
                {
                    runScript("drawChart_WindPower([{type:\"line\",label:\"전기생산량[kWh]\",data:[" + s + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:" + max1 + "},{type:\"bar\",label:\"평균풍속[m/s]\",data:[" + v + "],tension: 0.4,borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:" + max2 + "}])");
                }
            }
            else
            {
                webView21.Visible = false;
            }

        }
        #endregion

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

    }
}
