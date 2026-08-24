using main.info;
using main.subcontents.BuildingGeneral;

namespace main.contents
{
    public partial class General : Form, IConfirmable
    {
        String ProjectName, ProjectType, ProjectNum;
        String BuildingCategory, BuildingUse, BuildingName, BuildingLocation, Climate, BylawClimate;
        double Latitude, Longitude; // λw, φw — ISO 52010-1 태양시각 계산용 대지 고유 좌표(관측소 좌표와 별개)
        double Year, Month;
        double ConstrucitonDate, BylawDate;
        double GrossArea, BuildingArea;
        String AboveGround, UnderGround;
        String ReviewerName, ReviewerLocation, ReviewerCompany;
        double ReviewYear, ReviewMonth;
        double ReviewDate;
        double[] law = new double[11];
        string OldProject;
        string BlowDoorTest;
        string AirtightReport = "없음"; // 기밀 설계 보고서 유무 — 기밀 테스트 미실시일 때만 의미 있음
        string AirtightMethod = "기밀 시공 여부별"; // 보고서도 없을 때, 표준값을 어떻게 적용할지
        bool Door_Infil = false, Win_Infil = false, ElecWiring_Infil = false, Pipe_Infil = false;
        double n50;

        public General()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            label23.Text = "m" + Program.UTIL.Subscript(2, true);
            label24.Text = "m" + Program.UTIL.Subscript(2, true);
            //프로젝트명
            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT title FROM projects WHERE current='1'");
            ProjectName = res[0][0];
            if (ProjectName != null) { ProjectName_textBox.Text = ProjectName.ToString(); }
            else { }

            //프로젝트유형
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (번호.Length > 0)
            {
                switch (번호[0][0])
                {
                    case "1":
                        ProjectType = "기존";
                        OldProject_label.Visible = false;
                        OldProject_textBox.Visible = false;
                        break;
                    case "2":
                        ProjectType = "리트로핏";
                        OldProject_label.Visible = true;
                        OldProject_textBox.Visible = true;
                        break;
                    case "3":
                        ProjectType = "리모델링";
                        OldProject_label.Visible = true;
                        OldProject_textBox.Visible = true;
                        break;
                    case "4":
                        ProjectType = "신규";
                        OldProject_label.Visible = false;
                        OldProject_textBox.Visible = false;
                        break;
                }
                if (ProjectType != null) { ProjectType_textBox.Text = ProjectType.ToString(); }
                else { }
            }

            //건물대상 콤보박스
            LoadOption_BuildingCategory();

            //기후데이터 콤보박스
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "인덱스", "이름", "종류 = '2'");
            if (Value.Length > 0)
            {
                Climate_comboBox.Items.Clear();
                for (int i = 0; i < Value.Length; i++)
                {
                    Climate_comboBox.Items.Add(Value[i][0]);
                }
            }

            //준공연월 연 콤보박스
            Year_comboBox.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                Year_comboBox.Items.Add((2040 - i).ToString());
            }
            //준공연월 월 콤보박스
            Month_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                Month_comboBox.Items.Add((i + 1).ToString());
            }

            //작성시기 연 콤보박스
            ReviewYear_comboBox.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                ReviewYear_comboBox.Items.Add((2030 - i).ToString());
            }
            //작성시기 월 콤보박스
            ReviewMonth_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                ReviewMonth_comboBox.Items.Add((i + 1).ToString());
            }
            //지상층수
            AboveGround_comboBox.Items.Clear();
            for (int i = 0; i < 30; i++)
            {
                AboveGround_comboBox.Items.Add((i + 1).ToString());
            }
            AboveGround_comboBox.SelectedIndex = 0;

            //지하층수
            UnderGround_comboBox.Items.Clear();
            for (int i = 0; i < 11; i++)
            {
                UnderGround_comboBox.Items.Add((i).ToString());
            }
            UnderGround_comboBox.SelectedIndex = 0;

            //기밀측정여부
            BlowDoorTest_comboBox.Items.Clear();
            BlowDoorTest_comboBox.Items.Add("기밀 테스트 실시");
            BlowDoorTest_comboBox.Items.Add("기밀 테스트 미실시");
            BlowDoorTest_comboBox.SelectedIndex = 1;
            BlowDoorTest = "기밀 테스트 미실시";

            //기밀 설계 보고서 유무 — 테스트 미실시일 때만 노출
            AirtightReport_comboBox.Items.Clear();
            AirtightReport_comboBox.Items.Add("없음");
            AirtightReport_comboBox.Items.Add("있음");
            AirtightReport_comboBox.SelectedIndex = 0;

            //표준값 적용 방식 — 테스트 미실시 + 보고서 없음일 때만 노출
            AirtightMethod_comboBox.Items.Clear();
            AirtightMethod_comboBox.Items.Add("기밀 시공 여부별");
            AirtightMethod_comboBox.Items.Add("존별");
            AirtightMethod_comboBox.SelectedIndex = 0;

            Change_AirtightFlow();

            Door_False_radioButton.Checked = true;
            Win_False_radioButton.Checked = true;
            ElecWiring_False_radioButton.Checked = true;
            Pipe_False_radioButton.Checked = true;
        }

        private void BuildingCategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingCategory_comboBox.SelectedItem != null && BuildingCategory_comboBox.SelectedItem.ToString() != "")
            {
                BuildingCategory = BuildingCategory_comboBox.SelectedItem.ToString();
                LoadOption_BuildingUse();
            }
        }

        private void BuildingUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingUse_comboBox.SelectedItem != null)
            {
                BuildingUse = BuildingUse_comboBox.SelectedItem.ToString();
            }
        }

        private void LoadOption_BuildingCategory()
        {
            string[][] V = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select a.이름 From 인덱스 as a Inner Join 인덱스분류 as b  on a.종류=b.아이디 Where b.종류='존일반' and b.이름='건물용도' Order by a.값 ");
            if (V.Length > 0)
            {
                BuildingCategory_comboBox.Items.Clear();
                for (int a = 0; a < V.Length; a++)
                {
                    BuildingCategory_comboBox.Items.Add(V[a][0]);
                }
            }
        }
        private void LoadOption_BuildingUse()
        {
            BuildingUse_comboBox.Items.Clear();
            string[][] V1 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 아이디 From 인덱스 Where 이름='" + BuildingCategory_comboBox.SelectedItem.ToString() + "'");
            if (V1.Length > 0)
            {
                string[][] V2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 이름 From 인덱스 Where 부모아이디='" + V1[0][0] + "'");
                if (V2.Length > 0)
                {
                    for (int a = 0; a < V2.Length; a++)
                    {
                        BuildingUse_comboBox.Items.Add(V2[a][0]);
                    }
                    //if (BuildingCategory_comboBox.SelectedItem.ToString() == "기타유형")
                    //{
                    //    BuildingUse_comboBox.SelectedIndex = 0;
                    //    BuildingUse_label.Visible = false;
                    //    BuildingUse_comboBox.Visible = false;
                    //}
                    BuildingUse_comboBox.SelectedIndex = 0;
                    BuildingUse_label.Visible = true;
                    BuildingUse_comboBox.Visible = true;
                }
            }
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            //  ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            //  ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void BlowDoorTest_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlowDoorTest_comboBox.SelectedItem != null)
            {
                BlowDoorTest = BlowDoorTest_comboBox.SelectedItem.ToString();
                Change_BlowDoorTest();
            }
        }
        private void Change_BlowDoorTest()
        {
            BlowDoor_button.Visible = (BlowDoorTest == "기밀 테스트 실시");
            Change_AirtightFlow();
        }

        private void AirtightReport_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AirtightReport_comboBox.SelectedItem != null)
            {
                AirtightReport = AirtightReport_comboBox.SelectedItem.ToString();
                Change_AirtightFlow();
            }
        }

        private void AirtightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AirtightMethod_comboBox.SelectedItem != null)
            {
                AirtightMethod = AirtightMethod_comboBox.SelectedItem.ToString();
                Change_AirtightFlow();
            }
        }

        // 기밀 테스트 실시 여부 > 기밀 설계 보고서 유무 > 표준값 적용 방식 순으로, 앞 단계에서 실측/보고서가
        // 확인되면 뒷 단계는 물을 필요가 없어 숨김. 문/창/배선/배관 4개 판정은 "표준값 적용 방식"에서
        // "기밀 시공 여부별"을 골랐을 때만 나타남(기존 로직 그대로 재사용).
        private void Change_AirtightFlow()
        {
            bool testDone = BlowDoorTest == "기밀 테스트 실시";

            AirtightReport_label.Visible = !testDone;
            AirtightReport_comboBox.Visible = !testDone;

            bool showMethodStep = !testDone && AirtightReport == "없음";
            AirtightMethod_label.Visible = showMethodStep;
            AirtightMethod_comboBox.Visible = showMethodStep;

            bool showElementJudgement = showMethodStep && AirtightMethod == "기밀 시공 여부별";
            Door_label.Visible = showElementJudgement;
            Door_groupBox.Visible = showElementJudgement;
            Win_label.Visible = showElementJudgement;
            Win_groupBox.Visible = showElementJudgement;
            ElecWiring_label.Visible = showElementJudgement;
            ElecWiring_groupBox.Visible = showElementJudgement;
            Pipe_label.Visible = showElementJudgement;
            Pipe_groupBox.Visible = showElementJudgement;

            // n50은 실측값이거나 "기밀 시공 여부별" 판정 결과일 때만 존재 — 기밀 설계 보고서/존별 표준값
            // 경로는 존 데이터 기준 자동 산정 로직이 아직 없어 값을 비워둠
            bool n50Available = testDone || showElementJudgement;
            n50_label1.Visible = n50Available;
            n50_textBox.Visible = n50Available;
            n50_label2.Visible = n50Available;
            if (!n50Available) { n50_textBox.Text = ""; }
        }
        private void Cal_Infiltration(string BlowDoorTest, bool door, bool win, bool elec, bool pipe)
        {
            if (BlowDoorTest == "기밀 테스트 미실시")
            {
                string[] check = new string[4];
                if (door) { check[0] = "적용"; } else { check[0] = "미적용"; }
                if (win) { check[1] = "적용"; } else { check[1] = "미적용"; }
                if (elec) { check[2] = "적용"; } else { check[2] = "미적용"; }
                if (pipe) { check[3] = "적용"; } else { check[3] = "미적용"; }

                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기밀", "n50", "방풍출입문 ='" + check[0] + "' and 창호='" + check[1] + "' and 배선='" + check[2] + "' and 배관='" + check[3] + "'");
                if (Value.Length > 0)
                {
                    n50 = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    n50_textBox.Text = n50.ToString("0.0");
                }
            }
        }

        private void Door_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Door_True_radioButton.Checked == true)
            {
                Door_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Door_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Door_False_radioButton.Checked == true)
            {
                Door_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Win_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Win_True_radioButton.Checked == true)
            {
                Win_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Win_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Win_False_radioButton.Checked == true)
            {
                Win_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void ElecWiring_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ElecWiring_True_radioButton.Checked == true)
            {
                ElecWiring_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void ElecWiring_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ElecWiring_False_radioButton.Checked == true)
            {
                ElecWiring_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Duct_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Pipe_True_radioButton.Checked == true)
            {
                Pipe_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Pipe_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Pipe_False_radioButton.Checked == true)
            {
                Pipe_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }

        }


        private void Climate_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Climate_comboBox.SelectedItem != null)
            {
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후인덱스", "지역명,지역구분", "지역인덱스 = '" + Climate_comboBox.SelectedItem.ToString() + "'");
                if (Value.Length > 0)
                {
                    Climate = Value[0][0];
                    BylawClimate = Value[0][1];
                    ByRawClimate_textBox.Text = BylawClimate;

                    // 위도/경도는 지역 기본값을 채워주되, 사용자가 이미 값을 입력했으면 그대로 둠(사용자 입력값 우선)
                    if (Lat_textBox.Text == "" || Lon_textBox.Text == "")
                    {
                        string[][] LatLon = Program.DB.getValue(DB.type.BaseDB_RESystem, "시간별기후데이터", "위도,경도", "지역 = '" + Climate + "' LIMIT 1");
                        if (LatLon.Length > 0)
                        {
                            if (Lat_textBox.Text == "") Lat_textBox.Text = Program.UTIL.ToDoubleOrZero(LatLon[0][0]).ToString("0.00");
                            if (Lon_textBox.Text == "") Lon_textBox.Text = Program.UTIL.ToDoubleOrZero(LatLon[0][1]).ToString("0.00");
                        }
                    }
                }
            }
        }

        private void Year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Year_comboBox.SelectedItem != null && Year_comboBox.SelectedItem.ToString() != "")
            {
                Year = Program.UTIL.ToDoubleOrZero(Year_comboBox.SelectedItem.ToString());
                Calc_LawDate();

            }
        }

        private void Month_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Month_comboBox.SelectedItem != null && Month_comboBox.SelectedItem.ToString() != "")
            {
                Month = Program.UTIL.ToDoubleOrZero(Month_comboBox.SelectedItem.ToString());
                Calc_LawDate();
            }
        }

        private void Calc_LawDate()
        {
            if (Year != null && Month != null)
            {
                if (Program.UTIL.ToDoubleOrZero(Month) < 10)
                {
                    ConstrucitonDate = Program.UTIL.ToDoubleOrZero((Year + ".0" + Month));
                }
                else
                {
                    ConstrucitonDate = Program.UTIL.ToDoubleOrZero((Year + "." + Month));
                }
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_HCneed, "법규열관류율", "시기", "");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++)
                    {
                        law[i] = Program.UTIL.ToDoubleOrZero(Value[i][0]);
                    }
                }

                Array.Sort(law);
                for (int k = 0; k < 11; k++)
                {
                    if (ConstrucitonDate >= 2018.09)
                    {
                        BylawDate = 2018.09;
                        break;
                    }
                    else if (law[k] >= ConstrucitonDate)
                    {
                        if (k != 0)
                        {
                            BylawDate = law[k - 1];
                            break;
                        }
                        else
                        {
                            BylawDate = 1979.09;
                            break;
                        }
                    }
                    else { }
                }

                if (BylawDate == 2013.1)
                {
                    BylawDate_textBox.Text = "2013.10";
                }
                else { BylawDate_textBox.Text = Convert.ToString(BylawDate); }
            }

        }
        private void BuildingName_textBox_TextChanged(object sender, EventArgs e)
        {
            if (BuildingName_textBox.Text != null) { BuildingName = BuildingName_textBox.Text.ToString(); }
            else { }
        }

        private void BuildingLocation_textBox_TextChanged(object sender, EventArgs e)
        {
            if (BuildingLocation_textBox.Text != null) { BuildingLocation = BuildingLocation_textBox.Text.ToString(); }
            else { }
        }

        private void Lat_textBox_TextChanged(object sender, EventArgs e)
        {
            Latitude = Program.UTIL.ToDoubleOrZero(Lat_textBox.Text);
        }

        private void Lon_textBox_TextChanged(object sender, EventArgs e)
        {
            Longitude = Program.UTIL.ToDoubleOrZero(Lon_textBox.Text);
        }
        public void GrossArea_textBox_TextChanged(object sender, EventArgs e)
        {
            GrossArea = Program.UTIL.textBox_doubleComa(GrossArea_textBox, false, 2);
        }

        private void BuildingArea_textBox_TextChanged(object sender, EventArgs e)
        {
            BuildingArea = Program.UTIL.textBox_doubleComa(BuildingArea_textBox, false, 2);
        }
        private void AboveGround_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AboveGround_comboBox.SelectedItem != null)
            {
                AboveGround = AboveGround_comboBox.SelectedItem.ToString();
            }
        }

        private void UnderGround_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnderGround_comboBox.SelectedItem != null)
            {
                UnderGround = UnderGround_comboBox.SelectedItem.ToString();
            }
        }
        private void ReviewerName_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerName_textBox.Text != null) { ReviewerName = ReviewerName_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewerLocation_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerLocation_textBox.Text != null) { ReviewerLocation = ReviewerLocation_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewerCompany_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerCompany_textBox.Text != null) { ReviewerCompany = ReviewerCompany_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewYear_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReviewYear_comboBox.SelectedItem != null && ReviewYear_comboBox.SelectedItem.ToString() != "")
            {
                ReviewYear = Program.UTIL.ToDoubleOrZero(ReviewYear_comboBox.SelectedItem.ToString());
                Calc_ReviewDate();
            }
        }

        private void ReviewMonth_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReviewMonth_comboBox.SelectedItem != null && ReviewMonth_comboBox.SelectedItem.ToString() != "")
            {
                ReviewMonth = Program.UTIL.ToDoubleOrZero(ReviewMonth_comboBox.SelectedItem.ToString());
                Calc_ReviewDate();
            }
        }
        private void Calc_ReviewDate()
        {
            if (Program.UTIL.ToDoubleOrZero(ReviewMonth) < 10)
            {
                ReviewDate = Program.UTIL.ToDoubleOrZero((ReviewYear + ".0" + ReviewMonth));
            }
            else
            {
                ReviewDate = Program.UTIL.ToDoubleOrZero((ReviewYear + "." + ReviewMonth));
            }
        }

        private void BlowDoor_button_Click(object sender, EventArgs e)
        {
            BlowDoorTest form = new BlowDoorTest();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                n50 = form.n50;
                n50_textBox.Text = form.n50.ToString("0.0");
            }
        }

        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                if (BuildingName == null || BuildingLocation == null || GrossArea == 0 || BuildingArea == 0 || ReviewerName == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("모든 입력값을 입력하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Save(isManualSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return false;
            }
        }


        private void Save(bool isManualSave = false)
        {
            string ProjectTypeNum = null;
            switch (ProjectType)
            {
                case "기존":
                    ProjectTypeNum = "1";
                    break;
                case "리트로핏":
                    ProjectTypeNum = "2";
                    break;
                case "리모델링":
                    ProjectTypeNum = "3";
                    break;
                case "신규":
                    ProjectTypeNum = "4";
                    break;
            }

            if (int.TryParse(AboveGround_comboBox.Text, out int abovefloornum))
            {
                AboveGround = AboveGround_comboBox.Text;
            }
            else
            {
                try
                {
                    MessageBox.Show("지상층수를 입력해주세요");
                }
                catch (Exception ex)
                {
                    // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                    System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                }
            }

            if (int.TryParse(UnderGround_comboBox.Text, out int underfloornum))
            {
                UnderGround = UnderGround_comboBox.Text;
            }
            else
            {
                try
                {
                    MessageBox.Show("지하층수를 입력해주세요");
                }
                catch (Exception ex)
                {
                    // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                    System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                }
            }

            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트명,프로젝트유형,프로젝트유형번호,기존프로젝트," +
                "건물대상,건물용도,건물명,주소,위도,경도,지역인덱스,지역,지역구분," +
                "준공연도,준공월," +
                "준공시기,법규시기," +
                "연면적,건축면적," +
                "지상층수,지하층수," +
                "작성자,작성자주소,작성자회사,작성연도,작성월,작성시기",
            "'" + 번호[0][0] + "','" + ProjectName + "','" + ProjectType + "','" + ProjectTypeNum + "','" + OldProject + "','" +
            BuildingCategory + "','" + BuildingUse + "','" + BuildingName + "','" + BuildingLocation + "','" + Latitude + "','" + Longitude + "','" + Climate_comboBox.SelectedItem.ToString() + "','" + Climate + "','" + BylawClimate + "','" +
            Year + "','" + Month + "','" +
            ConstrucitonDate.ToString() + "','" + BylawDate.ToString() + "','" +
            GrossArea.ToString() + "','" + BuildingArea.ToString() + "','" +
            AboveGround + "','" + UnderGround + "','" +
            ReviewerName + "','" + ReviewerLocation + "','" + ReviewerCompany + "','" + ReviewYear + "','" + ReviewMonth + "','" +
            ReviewDate.ToString()
                 + "'", "프로젝트번호");

            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,기밀측정여부,n50," +
               "출입문기밀여부," +
               "창호기밀여부," +
               "배선기밀여부," +
               "배관기밀여부," +
               "기밀보고서," +
               "기밀적용방식",
                "'" + 번호[0][0] + "','" + BlowDoorTest + "','" + n50.ToString() + "','" +
                Door_Infil + "','" +
                Win_Infil + "','" +
                ElecWiring_Infil + "','" +
                Pipe_Infil + "','" +
                AirtightReport + "','" +
                AirtightMethod + "'", "프로젝트번호");

            Program.DB.saveProject();

            CALC.Run_Climate();
        }

        private void reset()
        {
            ProjectName = null;
            ProjectName_textBox.Text = null;

            ProjectType = null;
            ProjectType_textBox.Text = null;

            ProjectNum = null;
            ProjectNum_textBox.Text = null;

            BuildingName = null;
            BuildingName_textBox.Text = null;

            BuildingLocation = null;
            BuildingLocation_textBox.Text = null;

            Latitude = 0;
            Longitude = 0;
            Lat_textBox.Text = null;
            Lon_textBox.Text = null;

            GrossArea = 0;
            GrossArea_textBox.Text = null;
            BuildingArea = 0;
            BuildingArea_textBox.Text = null;

            AboveGround_comboBox.SelectedIndex = 0;

            UnderGround_comboBox.SelectedIndex = 0;

            ReviewerName = null;
            ReviewerName_textBox.Text = null;

            ReviewerLocation = null;
            ReviewerLocation_textBox.Text = null;

            ReviewerCompany = null;
            ReviewerCompany_textBox.Text = null;


        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            string[][] Value1 = Program.DB.querySQL(DB.type.ProjListDB, "Select type, title from projects where current = '1'");
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,프로젝트유형,프로젝트번호,건물진단실시," +
            "건물대상,건물용도,건물명,주소,지역인덱스,지역,지역구분," +
            "외벽구조유형,지붕구조유형,준공연도,준공월," +
            "준공시기,법규시기," +
            "연면적,건축면적," +
            "지상층수,지하층수," +
            "작성자,작성자주소,작성자회사,작성연도,작성월,작성시기,기존프로젝트,위도,경도", "");
            if (Value1.Length > 0)
            {
                ProjectName = Value1[0][1];
                ProjectName_textBox.Text = ProjectName.ToString();

                switch (Value1[0][0])
                {
                    case "1":
                        ProjectType = "기존";
                        break;
                    case "2":
                        ProjectType = "리트로핏";
                        break;
                    case "3":
                        ProjectType = "리모델링";
                        break;
                    case "4":
                        ProjectType = "신규";
                        break;

                }
                ProjectType_textBox.Text = ProjectType.ToString();

            }
            if (Value.Length > 0)
            {
                ProjectNum = Value[0][2];
                ProjectNum_textBox.Text = ProjectNum.ToString();

                LoadOption_BuildingCategory();
                if (Value[0][4] != "")
                {
                    BuildingCategory_comboBox.SelectedItem = Value[0][4];
                }

                if (Value[0][5] != "")
                {
                    BuildingUse_comboBox.SelectedItem = Value[0][5];
                }

                BuildingName = Value[0][6];
                BuildingName_textBox.Text = BuildingName;

                BuildingLocation = Value[0][7];
                BuildingLocation_textBox.Text = BuildingLocation;

                Latitude = Program.UTIL.ToDoubleOrZero(Value[0][28]);
                Longitude = Program.UTIL.ToDoubleOrZero(Value[0][29]);
                if (Latitude != 0) Lat_textBox.Text = Latitude.ToString("0.00");
                if (Longitude != 0) Lon_textBox.Text = Longitude.ToString("0.00");

                Climate_comboBox.SelectedItem = Value[0][8];
                Climate = Value[0][9];
                BylawClimate = Value[0][10];
                ByRawClimate_textBox.Text = BylawClimate;
                if (Value[0][13] != "")
                {
                    Year = Program.UTIL.ToDoubleOrZero(Value[0][13]);
                    Year_comboBox.SelectedItem = Year.ToString();
                }
                if (Value[0][14] != "")
                {
                    Month = Program.UTIL.ToDoubleOrZero(Value[0][14]);
                    Month_comboBox.SelectedItem = Month.ToString();
                }

                if (Value[0][15] != "")
                {
                    ConstrucitonDate = Program.UTIL.ToDoubleOrZero(Value[0][15]);
                    Calc_LawDate();
                }

                if (Value[0][17] != "")
                {
                    GrossArea = Program.UTIL.ToDoubleOrZero(Value[0][17]);
                    GrossArea_textBox.Text = GrossArea.ToString();
                    Program.UTIL.textBox_doubleComa(GrossArea_textBox, true, 2);
                }

                if (Value[0][18] != "")
                {
                    BuildingArea = Program.UTIL.ToDoubleOrZero(Value[0][18]);
                    BuildingArea_textBox.Text = BuildingArea.ToString();
                    Program.UTIL.textBox_doubleComa(BuildingArea_textBox, true, 2);
                }

                AboveGround = Value[0][19];
                AboveGround_comboBox.SelectedItem = AboveGround;

                UnderGround = Value[0][20];
                UnderGround_comboBox.SelectedItem = UnderGround;

                ReviewerName = Value[0][21];
                ReviewerName_textBox.Text = ReviewerName;

                ReviewerLocation = Value[0][22];
                ReviewerLocation_textBox.Text = ReviewerLocation;

                ReviewerCompany = Value[0][23];
                ReviewerCompany_textBox.Text = ReviewerCompany;
                if (Value[0][24] != "")
                {
                    ReviewYear = Program.UTIL.ToDoubleOrZero(Value[0][24]);
                    ReviewYear_comboBox.SelectedItem = ReviewYear.ToString();
                }

                if (Value[0][25] != "")
                {
                    ReviewMonth = Program.UTIL.ToDoubleOrZero(Value[0][25]);
                    ReviewMonth_comboBox.SelectedItem = ReviewMonth.ToString();
                }

                if (Value[0][26] != "")
                {
                    ReviewDate = Program.UTIL.ToDoubleOrZero(Value[0][26]);
                    Calc_ReviewDate();
                }
                OldProject = Value[0][27];
                OldProject_textBox.Text = OldProject;
            }


            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀측정여부," +
                "출입문기밀여부," +
                "창호기밀여부," +
                "배선기밀여부," +
                "배관기밀여부," +
                "기밀보고서," +
                "기밀적용방식", "");
            if (Value.Length > 0)
            {
                if (Value[0][0] != "")
                {
                    BlowDoorTest = Value[0][0];
                }
                else
                {
                    BlowDoorTest = "기밀 테스트 미실시";
                }
                BlowDoorTest_comboBox.SelectedItem = Value[0][0];

                // 새로 추가된 두 필드는 기존 프로젝트엔 저장돼 있지 않을 수 있음 — 없으면 기본값(보고서 없음/
                // 기밀 시공 여부별 표준값)을 유지해 기존 계산 흐름 그대로 이어지게 함
                if (Value[0][5] != null && Value[0][5] != "")
                {
                    AirtightReport = Value[0][5];
                    AirtightReport_comboBox.SelectedItem = AirtightReport;
                }
                if (Value[0][6] != null && Value[0][6] != "")
                {
                    AirtightMethod = Value[0][6];
                    AirtightMethod_comboBox.SelectedItem = AirtightMethod;
                }

                Change_BlowDoorTest();

                if (Value[0][1] != null && Value[0][1] != "")
                {
                    if (Convert.ToBoolean(Value[0][1]))
                    {
                        Door_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Door_False_radioButton.Checked = true;
                    }
                }

                if (Value[0][2] != null && Value[0][2] != "")
                {
                    if (Convert.ToBoolean(Value[0][2]))
                    {
                        Win_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Win_False_radioButton.Checked = true;
                    }
                }
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    if (Convert.ToBoolean(Value[0][3]))
                    {
                        ElecWiring_True_radioButton.Checked = true;
                    }
                    else
                    {
                        ElecWiring_False_radioButton.Checked = true;
                    }
                }
                if (Value[0][4] != null && Value[0][4] != "")
                {
                    if (Convert.ToBoolean(Value[0][4]))
                    {
                        Pipe_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Pipe_False_radioButton.Checked = true;
                    }
                }
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "n50", "");

            if (Value.Length > 0 && Value[0][0] != "")
            {
                n50 = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                n50_textBox.Text = Program.UTIL.ToDoubleOrZero(Value[0][0]).ToString("0.0");
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\1.General";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void climate_infobutton_Click(object sender, EventArgs e)
        {
            Climate_info form = new Climate_info(Climate);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            }
        }

    }
}
