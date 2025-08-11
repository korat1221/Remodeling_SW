using main.contentslist;
using main.info;
using main.subcontents;
using main.subcontents.ConstructionBlind;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.contents
{
    public partial class ConstructionBlind : Form
    {
        bool scriptable = false;
        //번호,프로젝트유형,DB유형,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율
        String Num, Type;
        String BlindDBNum, BlindDBType, BlindName, BlindType, BlindInstall, BlindTrans, BlindColor;
        double BlindEx, BlindIn, BlindSHGC, BlindAlpha;
        String ControlType, ControlType2;

        public ConstructionBlind()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '차양정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호", "");
            if (value.Length > 0)
            {
                if (value[0][0] == "1")
                {
                    radioButton1.Checked = true;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                }
                else if (value[0][0] == "4")
                {
                    radioButton1.Enabled = false;
                    radioButton2.Checked = true;
                    radioButton3.Enabled = false;
                }
            }

            ControlType_comboBox.Items.Clear();
            ControlType_comboBox.Items.Add("일사제어차양(수동)");
            ControlType_comboBox.Items.Add("일사제어차양(자동)");
            ControlType_comboBox.Items.Add("채광제어차양");

        }
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존 차양";
            Type_textBox.Text = Type;
            Changed_Type(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규 차양";
            Type_textBox.Text = Type;
            Changed_Type(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Changed_Type(Type);
        }
        private void Changed_Type(String Type)
        {
            switch (Type)
            {
                case "기존 차양":
                    OldBlind_textBox.Visible = false;
                    OldBlind_comboBox.Visible = false;
                    Load_OldBlind(Type);
                    break;

                case "신규 차양":
                    OldBlind_textBox.Visible = false;
                    OldBlind_comboBox.Visible = false;
                    Load_OldBlind(Type);
                    break;

                case "철거 후 신규":
                    OldBlind_textBox.Visible = true;
                    OldBlind_comboBox.Visible = true;
                    Load_OldBlind(Type);
                    break;
            }
        }
        private void Load_OldBlind(String Type)
        {
            string def_value;
            String[][] Table;

            if (Type == "철거 후 신규")
            {
                def_value = "Type = '기존 차양'";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "명칭", def_value);

            }
            else
            {
                def_value = "Type = ''";
                Table = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "명칭", def_value);
            }

            if (Table.Length > 0)
            {
                int i = -1;
                DataTable sources = new DataTable();
                sources.Columns.Add("Text");
                sources.Columns.Add("Value");
                sources.Columns.Add("ID");

                while (++i < Table.Length)
                {
                    DataRow dr = sources.NewRow();
                    dr["Text"] = Table[i][0];
                    sources.Rows.Add(dr);
                }

                OldBlind_comboBox.DataSource = sources.DefaultView;
                OldBlind_comboBox.DisplayMember = "Text";
                for (i = 0; i < OldBlind_comboBox.Items.Count; i++)
                {
                    var arr = ((DataRowView)OldBlind_comboBox.Items[i]).Row.ItemArray;
                    if (arr.Length > 1 && arr[1].ToString() == def_value)
                    {
                        OldBlind_comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }

        }
        private void BlindDB_button_Click(object sender, EventArgs e)
        {
            BlindDB DB_form = new BlindDB(BlindDBNum);
            DialogResult result = DB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                BlindDBNum = DB_form.Select_Blind[0];
                BlindDBType = DB_form.Select_Blind[1];
                BlindName = DB_form.Select_Blind[2];
                BlindType = DB_form.Select_Blind[3];
                BlindInstall = DB_form.Select_Blind[4];
                BlindTrans = DB_form.Select_Blind[5];
                BlindColor = DB_form.Select_Blind[6];
                if (BlindName == "없음")
                {
                    BlindEx = 0;
                    BlindIn = 0;
                    BlindSHGC = 0;
                    BlindAlpha = 0;
                }
                else
                {
                    BlindEx = Convert.ToDouble(DB_form.Select_Blind[7]);
                    BlindIn = Convert.ToDouble(DB_form.Select_Blind[8]);
                    BlindSHGC = Convert.ToDouble(DB_form.Select_Blind[9]);
                    BlindAlpha = Convert.ToDouble(DB_form.Select_Blind[10]);
                }

                BlindName_textBox.Text = BlindName;
                BlindType_textBox.Text = BlindType;
                BlindInstall_textBox.Text = BlindInstall;
                BlindTrans_textBox.Text = BlindTrans;
                BlindColor_textBox.Text = BlindColor;
                BlindEx_textBox.Text = BlindEx.ToString();
                BlindIn_textBox.Text = BlindIn.ToString();
                BlindSHGC_textBox.Text = BlindSHGC.ToString();
                BlindAlpha_textBox.Text = BlindAlpha.ToString();
                Load_Image();
            }
        }

        private void Load_Image()
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "차양이미지", "이미지", "제품종류 = '" + BlindType + "'  AND 설치유형 ='제품'");
            if (Image.Length > 0)
            {
                pictureBox3.Load(Program.gPath + Image[0][0]);
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }


            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "차양이미지", "이미지", "제품종류 = '" + BlindType + "'  AND 설치유형 ='" + BlindInstall + "'");
            if (Image.Length > 0)
            {
                pictureBox4.Load(Program.gPath + Image[0][0]);
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }
        private void ControlType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControlType_comboBox.SelectedItem != null)
            {
                if (ControlType_comboBox.SelectedItem.ToString() == "일사제어차양(수동)")
                {
                    ControlType = ControlType_comboBox.SelectedItem.ToString();
                    ControlType2_textBox.Text = "수동제어";
                    ControlType2 = ControlType2_textBox.Text.ToString();
                }
                else if (ControlType_comboBox.SelectedItem.ToString() == "일사제어차양(자동)")
                {
                    ControlType = ControlType_comboBox.SelectedItem.ToString();
                    ControlType2_textBox.Text = "자동제어";
                    ControlType2 = ControlType2_textBox.Text.ToString();
                }
                else if (ControlType_comboBox.SelectedItem.ToString() == "채광제어차양")
                {
                    ControlType = ControlType_comboBox.SelectedItem.ToString();
                    ControlType2_textBox.Text = "채광제어";
                    ControlType2 = ControlType2_textBox.Text.ToString();
                }
                else
                {
                    ControlType = null;
                    ControlType2_textBox.Text = null;
                    ControlType2 = null;
                }
                LoadGraph();
            }
            else { }

        }

        private void LoadGraph()
        {
            if (ControlType2 != null)
            {
                webView21.Visible = true;
                string s = "", s2 = "";
                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res1;
                for (int mth = 1; mth < 12; mth++)
                {
                    res1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + ControlType2, "계수", "지역명= '" + Location[0][0] + "' And 방향 ='남' And 기간 = '" + mth.ToString() + "월'");
                    if (res1.Length > 0)
                    {
                        s += Convert.ToDouble(res1[0][0]) * 100 + ",";
                    }
                }
                res1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + ControlType2, "계수", "지역명= '" + Location[0][0] + "' And 방향 ='남' And 기간 = '" + 12.ToString() + "월'");
                if (res1.Length > 0)
                {
                    s += Convert.ToDouble(res1[0][0]) * 100;
                }



              
                 double[] day = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                for (int mth = 1; mth <= 12; mth++)
                {
                    string[][] res2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT 일사량 FROM 기후데이터_전일사량 WHERE 지역명 = '" + Location[0][0] + "' AND 방향 ='남' And 각도='90˚' and  기간 = '" + mth.ToString() + "월'");
                    if (res2.Length > 0)
                    {
                        if (res2.Length > 0)
                        {
                            s2 += (Convert.ToDouble(res2[0][0]) * 24 * day[mth -1] /1000)+ ",";
                        }
                    }
                }
                   
                string [][] res3 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT Max(일사량) From 기후데이터_전일사량 Where 지역명 ='" + Location[0][0] + "' AND 방향='남' And 각도='90˚'and not 기간='연간값'");
                double max = 0;
                if (res3.Length > 0)
                {
                    int n2 = ((int)Convert.ToDouble(res3[0][0])).ToString().Length;
                    max = Convert.ToInt64(Convert.ToDouble(res3[0][0]) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1);

                }
                string unit = "kWh/m" + Program.UTIL.Subscript(2, true) + "·mth";
                runScript("drawChart_blind([{type:\"line\",label:\"차양가동율(남향)\",data:[" + s + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",label:\"일사량(" + unit + ")\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:" + max + ",dash:false,barPercentage:0.7}])");
                // runScript("drawChart_blind([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:150}])");
            }
            else
            {
                webView21.Visible = false;
            }


        }
        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionBlind f = (List_ConstructionBlind)form;

            f.load_List();

            return true;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (Name_textBox.Text == null) { MessageBox.Show("제품명을 입력하세요."); }
            else
            {
                Program.DB.setValue(DB.type.ProjDB, "ConstructionBlind", "번호,프로젝트유형,명칭,제품번호,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율,제어방식1,제어방식2",
                              "'" + Num + "','" +
                              프로젝트유형[0][0] + "','" +
                              Name_textBox.Text.ToString() + "','" + BlindDBNum + "','" + BlindName + "','" + BlindType + "','" + BlindInstall + "','" + BlindTrans + "','" + BlindColor + "','" +
                              BlindEx.ToString() + "','" + BlindIn.ToString() + "','" + BlindSHGC.ToString() + "','" + BlindAlpha.ToString() + "','" +
                              ControlType + "','" + ControlType2 + "'", "번호");
            }
            Program.DB.saveProject();
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(50, OnLoadListProc);
        }

        private void reset()
        {
            Num_textBox.Text = null;
            Num = null;
            Name_textBox.Text = null;
            Name = null;
            Type_textBox.Text = null;

            BlindName_textBox.Text = null;
            BlindName = null;
            BlindType_textBox.Text = null;
            BlindType = null;
            BlindInstall_textBox.Text = null;
            BlindInstall = null;
            BlindTrans_textBox.Text = null;
            BlindTrans = null;
            BlindColor_textBox.Text = null;
            BlindColor = null;
            ControlType_comboBox.SelectedItem = null;
            ControlType = null;

            BlindEx_textBox.Text = null;
            BlindEx = 0;
            BlindIn_textBox.Text = null;
            BlindIn = 0;
            BlindSHGC_textBox.Text = null;
            BlindSHGC = 0;
            BlindAlpha_textBox.Text = null;
            BlindAlpha = 0;
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "번호,프로젝트유형,명칭,제품번호,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율,제어방식1,제어방식2", "번호 ='" + ID + "'");

            if (Value.Length > 0)
            {
                Num_textBox.Text = ID;
                Num = ID;

                Name_textBox.Text = Value[0][2];
                Type_textBox.Text = null;

                BlindDBNum = Value[0][3];
                BlindName_textBox.Text = Value[0][4];
                BlindName = Value[0][4];
                BlindType_textBox.Text = Value[0][5];
                BlindType = Value[0][5];
                BlindInstall_textBox.Text = Value[0][6];
                BlindInstall = Value[0][6];
                BlindTrans_textBox.Text = Value[0][7];
                BlindTrans = Value[0][7];
                BlindColor_textBox.Text = Value[0][8];
                BlindColor = Value[0][8];
                ControlType_comboBox.SelectedItem = Value[0][13];
                ControlType = Value[0][13];

                BlindEx_textBox.Text = Value[0][9];
                BlindEx = Convert.ToDouble(Value[0][9]);
                BlindIn_textBox.Text = Value[0][10];
                BlindIn = Convert.ToDouble(Value[0][10]);
                BlindSHGC_textBox.Text = Value[0][11];
                BlindSHGC = Convert.ToDouble(Value[0][11]);
                BlindAlpha_textBox.Text = Value[0][12];
                BlindAlpha = Convert.ToDouble(Value[0][12]);
                Load_Image();

            }


        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\8.Blind";

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
