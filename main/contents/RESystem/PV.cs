using main.contentslist;
using main.info;
using main.subcontents.RESystem_PV;
using Microsoft.Web.WebView2.Core;
using System;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace main.contents
{
    public partial class PV : Form, IConfirmable
    {

        bool scriptable = false;
        //일반정보
        string Num, Name;
        string 지역, 프로젝트유형, Ins, beforePV;


        //PVModuleDB
        string PVModuleNumber, PVModuleName;
        double PVarea, PVtotalarea, PVpower, Kpk, Ppk, PVwidth, PVheight; //단위면적당 출력(kW), 총출력


        //PVInverterDB 
        string Inverter;
        double InverterEff;

        //PVBatteryDB
        string Battery, BatteryType;
        double BatteryCa, BatteryEff;

        //방위와 향
        string orientation, slope, installType, connect;

        //계산
        public double fperf;
        #region 폼
        public PV()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            string[][] val = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            if (val.Length > 0)
            {
                지역 = val[0][0].ToString();
            }



            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (value.Length > 0)
            {
                프로젝트유형 = value[0][0].ToString();
            }

            Num = Num_textBox.Text;

            PVType_ComboBox.Items.Clear();

            PVType_ComboBox.Items.AddRange(new string[] { "독립형", "계통연계형" });

            Battery_label.Visible = false;
            Battery_textBox.Visible = false;
            BatteryDB_button.Visible = false;


            pvname.Visible = false;
            pvsize.Visible = false;
            pvpower.Visible = false;
            pvtotal.Visible = false;

            InverterEff_textbox.Visible = false;
            BatteryEff_textbox.Visible = false;
            batterypower.Visible = false;
        }

        private void PV_Table()
        {
            new StackedHeaderDecorator(PV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            PV_dataGridView.Columns.Clear();
            PV_dataGridView.Columns.Add("A0", "번호");
            PV_dataGridView.Columns.Add("A1", "개수");
            PV_dataGridView.Columns.Add("A2", "면적.[m2]");

            DataGridViewComboBoxColumn direction = new DataGridViewComboBoxColumn();
            direction.HeaderText = "설치정보.방위";
            direction.Items.AddRange(new string[] { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" });
            PV_dataGridView.Columns.Add(direction);

            DataGridViewComboBoxColumn slope = new DataGridViewComboBoxColumn();
            slope.HeaderText = "설치정보.기울기";
            slope.Items.AddRange(new string[] { "0", "30", "45", "60", "90" });
            PV_dataGridView.Columns.Add(slope);

            DataGridViewComboBoxColumn type = new DataGridViewComboBoxColumn();
            type.HeaderText = "설치정보.후면유형";
            type.Items.AddRange(new string[] { "통기없음", "미세통기층", "통기층" }); // PVfperf = 0.76 , 0.8, 0.82
            PV_dataGridView.Columns.Add(type);

            PV_dataGridView.Columns.Add("A6", "음영정보.거리[m]"); //width_combo
            PV_dataGridView.Columns.Add("A7", "음영정보.높이[m]"); //height_combo
            PV_dataGridView.Columns.Add("A8", "음영정보.Array높이[m]"); //Arrayheight_combo

            PV_dataGridView.Columns[0].Width = 60;
        }
        private void PVType_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PVType_ComboBox.Text == "계통연계형")
            {
                Battery_label.Visible = false;
                Battery_textBox.Visible = false;
                BatteryDB_button.Visible = false;
                BatteryEff_textbox.Visible = false;
                batterypower.Visible = false;
                connect = "계통연계형";
                MainPVimage(connect);
            }
            else if (PVType_ComboBox.Text == "독립형")
            {
                Battery_label.Visible = true;
                Battery_textBox.Visible = true;
                BatteryDB_button.Visible = true;
                Battery_textBox.Text = null;

                connect = "독립형";
                MainPVimage(connect);
            }
        }
        void MainPVimage(string type)
        {
            tabload("input");
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "종류 = '" + type + "'");
            if (Image.Length > 0)
            {
                PVpictureBox.Size = new System.Drawing.Size(610, 300);
                PVpictureBox.Location = new Point(0, 28);
                PVpictureBox.Load(Program.gPath + Image[0][0]);
                PVpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                PVpictureBox.BackColor = Color.Transparent;
            }
            pvsize.Location = new Point(210, 95);
            pvpower.Location = new Point(210, 142);
            pvtotal.Location = new Point(210, 292);
            pvname.Location = new Point(537, 44);

            string[][] Ima = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "종류 = '기본'");
            if (Ima.Length > 0)
            {
                PVTypepictureBox.Size = new System.Drawing.Size(305, 245);
                PVTypepictureBox.Location = new Point(343, 13);
                PVTypepictureBox.Load(Program.gPath + Ima[0][0]);
                PVTypepictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                PVTypepictureBox.BackColor = Color.Transparent;
                PVTypepictureBox.Parent = PVpictureBox;
            }
        }
        void PVimage(string type)
        {
            tabload("input");
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "종류 = '" + type + "'");
            if (Image.Length > 0)
            {
                PVTypepictureBox.Size = new System.Drawing.Size(305, 245);
                PVTypepictureBox.Location = new Point(343, 13);
                PVTypepictureBox.Load(Program.gPath + Image[0][0]);
                PVTypepictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                PVTypepictureBox.BackColor = Color.Transparent;
                PVTypepictureBox.Parent = PVpictureBox;
            }
        }

        void shadingimage()
        {
            tabload("input");
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "종류 = '음영'");
            if (Image.Length > 0)
            {
                ShpictureBox.Size = new System.Drawing.Size(360, 310);
                ShpictureBox.Location = new Point(0, 15);
                ShpictureBox.Load(Program.gPath + Image[0][0]);
                ShpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                ShpictureBox.BackColor = Color.Transparent;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "기존PV", "설치 = '기존'");
            OldPVSystem_ComboBox.Items.AddRange(Value);
        }

        void tabload(string oper)
        {
            if (oper == "input")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["PVinstall_tabPage"];
            }
            else if (oper == "output")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["PVCalc_tabPage"];

            }
            else tabControl1.SelectedTab = tabControl1.TabPages["PVinstall_tabPage"];
        }

        private void PVModuleDB_button_Click(object sender, EventArgs e)
        {
            PV_ModuleDB PV_ModuleDB_form = new PV_ModuleDB("장비일람표 DB");
            DialogResult result = PV_ModuleDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string num = PV_ModuleDB_form.SelectPV;
                PV_Table();
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_PV", "번호,명칭,길이,높이,정격출력", "번호 = '" + num + "'");
                if (User_Value.Length > 0)
                {
                    int nRow = PV_dataGridView.Rows.Add();
                    PVModuleNumber = User_Value[0][0].ToString();
                    PV_dataGridView.Rows[nRow].Cells[0].Value = PVModuleNumber;
                    PVModuleName = User_Value[0][1];
                    PVpower = Program.UTIL.ToDoubleOrZero(User_Value[0][4]);
                    PVwidth = Program.UTIL.ToDoubleOrZero(User_Value[0][2]);
                    PVheight = Program.UTIL.ToDoubleOrZero(User_Value[0][3]);
                    PVarea = PVwidth * PVheight;
                    PVMoudle_textBox.Text = num;
                    Kpk = PVpower / PVarea; //단위면적당 출력
                    shadingimage();
                }
            }
        }

        private void InverterDB_button_Click_1(object sender, EventArgs e)
        {
            PV_InverterDB PV_InverterDB_form = new PV_InverterDB();

            DialogResult result = PV_InverterDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Inverter = PV_InverterDB_form.SelectInverter;
                InverterEff = Program.UTIL.ToDoubleOrZero(PV_InverterDB_form.SelectInverterEff);
            }
            Inverter_textBox.Text = Inverter;
            InverterEff_textbox.Visible = true;
            InverterEff_textbox.Location = new Point(382, 207); //382,180
            InverterEff_textbox.Text = string.Format("{0:F0}%", InverterEff);
            //InverterEff_textbox.Parent = PVpictureBox;
        }

        private void BatteryDB_button_Click_1(object sender, EventArgs e)
        {
            PV_BatteryDB PV_BatteryDB_form = new PV_BatteryDB();

            DialogResult result = PV_BatteryDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Battery = PV_BatteryDB_form.SelectBattery;
                BatteryCa = Program.UTIL.ToDoubleOrZero(PV_BatteryDB_form.SelectBatteryCa);
                BatteryType = PV_BatteryDB_form.SelectBatteryType;
                string[][] value = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리계수", "시스템효율", "배터리타입 = '" + BatteryType + "'");
                BatteryEff = Program.UTIL.ToDoubleOrZero(value[0][0]) * 100;
            }

            Battery_label.Visible = true;
            Battery_textBox.Text = Battery;
            BatteryEff_textbox.Visible = true;
            BatteryEff_textbox.Location = new Point(433, 207);
            BatteryEff_textbox.Text = string.Format("{0:F0}%", BatteryEff);
            //BatteryEff_textbox.Parent = PVpictureBox;

            batterypower.Visible = true;
            batterypower.Location = new Point(430, 294);
            batterypower.Text = string.Format("{0:F0} kW", BatteryCa);
        }
        private void PV_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && PV_dataGridView.Rows[e.RowIndex].Cells[1].Value != null)
            {
                if (Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 1, 1))
                {
                    double val = 0;
                    val = Program.UTIL.ToDoubleOrZero(PV_dataGridView.Rows[e.RowIndex].Cells[1].Value);
                    Ppk = val * PVpower / 1000; //kW 25년후 성능저하를 반영함

                    pvtotal.Visible = true;
                    pvtotal.Text = string.Format("{0:F1} kW", Ppk);
                    pvtotal.Location = new Point(210, 295);
                    pvtotal.BackColor = Color.Transparent;

                    pvname.Visible = true;
                    pvname.Text = string.Format("{0}", PVModuleName);
                    pvname.Location = new Point(535, 44);
                    pvname.BackColor = Color.Transparent;

                    pvsize.Visible = true;
                    pvsize.Text = string.Format("{0}m X {1}m", PVwidth, PVheight);
                    pvsize.Location = new Point(200, 97);
                    pvsize.BackColor = Color.Transparent;

                    pvpower.Visible = true;
                    pvpower.Text = string.Format("{0} W", PVpower);
                    pvpower.Location = new Point(215, 147);
                    pvpower.BackColor = Color.Transparent;

                    PVtotalarea = PVarea * val;

                    PV_dataGridView.Rows[e.RowIndex].Cells[2].Value = string.Format("{0:F2}", PVtotalarea);
                }
            }

            if (e.ColumnIndex == 4 && PV_dataGridView.Rows[e.RowIndex].Cells[4].Value != null)
            {
                slope = PV_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() + "˚";
                PVimage(PV_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
            }

            if (e.ColumnIndex == 3 && PV_dataGridView.Rows[e.RowIndex].Cells[3].Value != null)
            {
                orientation = PV_dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            }

            if (e.ColumnIndex == 5 && PV_dataGridView.Rows[e.RowIndex].Cells[5].Value != null)
            {
                installType = PV_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();

                switch (installType)
                {
                    case "통기없음":
                        fperf = 0.76 + 0.1; //인버터 효율 0.9반영함
                        break;
                    case "미세통기층":
                        fperf = 0.8 + 0.1;
                        break;
                    case "통기층":
                        fperf = 0.82 + 0.1;
                        break;
                    default:
                        break;
                }
            }

            if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                Program.UTIL.dataGridView_doubleComa(PV_dataGridView, e.RowIndex, e.ColumnIndex, 1);
            }
            if (e.ColumnIndex == 8)
            {
                double k = Program.UTIL.dataGridView_doubleComa(PV_dataGridView, e.RowIndex, e.ColumnIndex, 2);
                if (k <= 0)
                {
                    MessageBox.Show("어레이길이는 0보다 커야합니다.");
                }
            }
        }
        private void OldPVSystem_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            beforePV = OldPVSystem_ComboBox.Text;
        }

        #endregion

        #region 세이브

        private void Save_button_Click(object sender, EventArgs e)
        {
            ValidateAndSave(true);
        }

        // 화면 전환 / 프로그램 종료 / 툴바 저장 시 자동 호출된다. 편집 중인 PV가 있으면 저장한다.
        // Save()가 검증 실패 시 자체 메시지를 띄우고 저장을 건너뛴다. 화면 전환은 막지 않는다(항상 true).
        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(Num) && Save())
                {
                    Calc();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
            }
            return true;
        }
        public static bool OnLoadListProc(Form form)
        {
            List_PV f = (List_PV)form;
            f.load_List();
            return true;
        }
        private bool Save()
        {
            if (Name_textBox.Text == null)
            {
                MessageBox.Show("명칭을 입력하세요.");
                return false;
            }
            else Name = Name_textBox.Text;

            if (slope == null || orientation == null)
            {
                MessageBox.Show("방위 또는 향을 선택해 주세요.");
                return false;
            }


            RadioButton[] rB = { radioButton1, radioButton2, radioButton3, radioButton4 };

            foreach (var item in rB)
            {
                if (item.Checked == true)
                {
                    Ins = item.Text;
                }
            }

            if (Ins == null)
            {
                MessageBox.Show("설치정보를 선택하세요!");
                return false;

            }
            else if (Ins == "보수" && beforePV == null)
            {
                MessageBox.Show("기존 PV를 선택하세요!");
                return false;
            }

            string[] v = new string[3];
            v[0] = string.Format("{0:F2}", Ppk);
            v[1] = string.Format("{0:F0}", InverterEff);
            v[2] = string.Format("{0:F2}", BatteryCa);

            Program.DB.setValue(DB.type.ProjDB, "PV_Form", "번호,프로젝트유형,명칭,모듈번호,용량,인버터번호,인버터효율,배터리번호,배터리용량,계통유형", "'" + Num + "','" + 프로젝트유형 + "','" + Name + "','" + PVModuleNumber + "','" + v[0] + "','" + Inverter + "','" + v[1] + "','" + Battery + "','" + v[2] + "','" + connect + "'", "번호");

            string[] val = new string[9];
            for (int k = 0; k < 9; k++)
            {
                val[k] = PV_dataGridView.Rows[0].Cells[k].Value.ToString();
            }
            Program.DB.setValue(DB.type.ProjDB, "PV_Form", "번호,개수,면적, 방위,기울기,통풍유무,지형물거리,지형물높이,어레이높이,설치,기존PV,fperf", "'" + Num + "','" + val[1] + "','" + val[2] + "'," +
                "'" + val[3] + "','" + val[4] + "','" + val[5] + "','" + val[6] + "','" + val[7] + "','" + val[8] + "','" + Ins + "','" + beforePV + "','" + fperf + "'", "번호");
            
            return true;
        }

        private void Calc()
        {
            tabload("output");
            Cal_RESystem cal = new Cal_RESystem(Num);
            cal.PVcalReady();
            cal.PVcal();
            LoadGraph(cal.Qfpvm_kWh, cal.Esol);

            allcapacity_textBox.Text = string.Format("{0:n0}", cal.Qfpva_kWh) + " kWh/년";
            double[] Qeff = new double[12];
            for (int i = 0; i < 12; i++)
            {
                Qeff[i] = cal.Qfpvm_m2_kWh[i] / cal.Esol[i];
            }
            averagecpacity_textBox.Text = string.Format("{0:F1}", Qeff.Average() * 100) + " %";
        }
        #endregion

        #region 로드

        public void LoadData(String ID)      // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            Reset();
            Num_textBox.Text = ID;
            Num = ID;

            PVType_ComboBox.Items.AddRange(new string[] { "독립형", "계통연계형" });

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "프로젝트유형,명칭,모듈번호,인버터번호,인버터효율,배터리번호,배터리용량,계통유형,개수,면적,방위,기울기,통풍유무,지형물거리,지형물높이,어레이높이,설치,기존PV", "번호='" + Num + "'");
            if (Value.Length > 0)
            {
                프로젝트유형 = Value[0][0];
                Name = Value[0][1];
                Name_textBox.Text = Name;

                PVType_ComboBox.SelectedItem = Value[0][7].ToString();
                if (PVType_ComboBox.SelectedItem.ToString() == "계통연계형")
                {
                    Battery_label.Visible = false;
                    Battery_textBox.Visible = false;
                    BatteryDB_button.Visible = false;
                    connect = "계통연계형";
                    MainPVimage(connect);
                }
                else if (PVType_ComboBox.SelectedItem.ToString() == "독립형")
                {
                    Battery_label.Visible = true;
                    Battery_textBox.Visible = true;
                    BatteryDB_button.Visible = true;
                    connect = "독립형";
                    MainPVimage(connect);

                    Battery = Value[0][5];

                    string[][] Va = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "정격전력,배터리타입", "번호 = '" + Battery + "'");
                    BatteryCa = Program.UTIL.ToDoubleOrZero(Va[0][0]);
                    BatteryType = Va[0][1];
                    string[][] lue = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리계수", "시스템효율", "배터리타입 = '" + BatteryType + "'");
                    BatteryEff = Program.UTIL.ToDoubleOrZero(lue[0][0]) * 100;

                    Battery_textBox.Text = Battery;
                    BatteryEff_textbox.Visible = true;
                    BatteryEff_textbox.Location = new Point(433, 207);
                    BatteryEff_textbox.Text = string.Format("{0:F0}%", BatteryEff);
                    //BatteryEff_textbox.Parent = PVpictureBox;

                    batterypower.Visible = true;
                    batterypower.Location = new Point(430, 294);
                    batterypower.Text = string.Format("{0:F0} kW", BatteryCa);
                }

                PV_Table();
                PVModuleNumber = Value[0][2];
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_PV", "번호,명칭,길이,높이,정격출력", "번호 = '" + PVModuleNumber + "'");
                if (User_Value.Length > 0)
                {
                    int nRow = PV_dataGridView.Rows.Add();

                    PV_dataGridView.Rows[nRow].Cells[0].Value = PVModuleNumber;
                    PVModuleName = User_Value[0][1];
                    PVpower = Program.UTIL.ToDoubleOrZero(User_Value[0][4]);
                    PVwidth = Program.UTIL.ToDoubleOrZero(User_Value[0][2]);
                    PVheight = Program.UTIL.ToDoubleOrZero(User_Value[0][3]);
                    PVarea = PVwidth * PVheight;
                    PVMoudle_textBox.Text = PVModuleNumber;
                    Kpk = PVpower / PVarea; //단위면적당 출력
                    shadingimage();
                }

                Inverter = Value[0][3];
                InverterEff = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                Inverter_textBox.Text = Inverter;
                InverterEff_textbox.Visible = true;
                InverterEff_textbox.Location = new Point(382, 207);
                InverterEff_textbox.Text = string.Format("{0:F0}%", InverterEff);
                // InverterEff_textbox.Parent = PVpictureBox;

                for (int i = 1; i < 9; i++)
                {
                    PV_dataGridView.Rows[0].Cells[i].Value = Value[0][i + 7].ToString();
                }

                RadioButton[] rB = { radioButton1, radioButton2, radioButton3, radioButton4 };
                foreach (var a in rB)
                {
                    if (a.Text == Value[0][16].ToString())
                    {
                        a.Checked = true;
                    }
                }
                if (Value[0][16] == "보수" || Value[0][16] == "철거 후 신규")
                {
                    OldPVSystem_ComboBox.Text = Value[0][17].ToString();
                }

                Cal_RESystem cal = new Cal_RESystem(Num);
                cal.PVcalReady();
                cal.PVcal();
                LoadGraph(cal.Qfpvm_kWh, cal.Esol);

                allcapacity_textBox.Text = string.Format("{0:n0}", cal.Qfpva_kWh) + " kWh/년";
                double[] Qeff = new double[12];
                for (int i = 0; i < 12; i++)
                {
                    Qeff[i] = cal.Qfpvm_m2_kWh[i] / cal.Esol[i];
                }
                averagecpacity_textBox.Text = string.Format("{0:F1}", Qeff.Average() * 100) + " %";
            }
        }

        #endregion

        #region 리셋
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }
        private void Reset()
        {
            Num = null; Name = null;
            지역 = null; 프로젝트유형 = null;

            PVModuleNumber = null; PVModuleName = null;
            PVarea = 0; PVtotalarea = 0; PVpower = 0; Kpk = 0; Ppk = 0; PVwidth = 0; PVheight = 0;

            Inverter = null; InverterEff = 0;
            Battery = null; BatteryType = null;
            BatteryCa = 0; BatteryEff = 0;

            //방위와 향
            orientation = null; slope = null; installType = null; connect = null;
            fperf = 0;

            Name_textBox.Text = null;
            PVType_ComboBox.Items.Clear();

            Battery_label.Text = null;
            Battery_textBox.Text = null;

            pvname.Text = null;
            pvsize.Text = null;
            pvpower.Text = null;
            pvtotal.Text = null;

            InverterEff_textbox.Text = null;
            BatteryEff_textbox.Text = null;
            batterypower.Text = null;

            PV_dataGridView.Columns.Clear();
            PV_dataGridView.Rows.Clear();

            Inverter_textBox.Text = null;

            averagecpacity_textBox.Text = null;
            allcapacity_textBox.Text = null;
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
        private void LoadGraph(double[] Qfpvm, double[] Esolm)
        {
            double[] PVm2_kWh = new double[12], Solm2_kWh = new double[12];
            try
            {
                for (int j = 0; j < 12; j++)
                {
                    PVm2_kWh[j] = Qfpvm[j] / PVtotalarea;
                    Solm2_kWh[j] = Esolm[j];
                }

                string s = "", s2 = "";

                double max1 = 0, max2 = 0;
                for (int mth = 0; mth < 11; mth++)
                {

                    s += PVm2_kWh[mth] + ",";
                    s2 += Solm2_kWh[mth] + ",";
                }

                s += PVm2_kWh[11];
                s2 += Solm2_kWh[11];

                int n2 = ((int)Solm2_kWh.Max()).ToString().Length;
                max2 = Convert.ToInt64((Solm2_kWh.Max()) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1) / 2;
                int n1 = ((int)PVm2_kWh.Max()).ToString().Length;
                max1 = Convert.ToInt64((PVm2_kWh.Max()) / Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1) / 2;
                string unit = "kWh/m" + Program.UTIL.Subscript(2, true) + "·mth";
                runScript("drawChart_pv([{type:\"line\",label:\"일사량(" + unit + ")\",data:[" + s2 + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:" + max2 + "},{type:\"bar\",label:\"생산량(kWh/m2·mth)\",data:[" + s + "],borderColor:\"#ffffee0\",backgroundColor:\"#FFF6A3\",min:0,max:" + max2 + ",dash:false,barPercentage:0.4}])");
            }
            catch { }
        }
        #endregion

        private void infoPV_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\21.PV";

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
    }
}
