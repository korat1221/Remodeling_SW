using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using main.info;


namespace main.subcontents.BuildingGeneral
{
    public partial class Climate_info : Form
    {
        bool scriptable = false;
        string s, s2;
        string local, projectnum;
        
        double[] month = new double[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public Climate_info(string local_name)
        {
            InitializeComponent();
            this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            local = local_name;

            InitializeAsync();  // LoadData는 여기서 실행
        }

        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);

            string fileUrl = new Uri(Path.Combine(Program.gPath, "chart_ctrl4.html")).AbsoluteUri;
            webView21.Source = new Uri(fileUrl);
            await webView21.CoreWebView2.ExecuteScriptAsync("console.log('WebView2 ready');");
            LabelWrite();
            LoadData();
        }
        void tableMake()
        {
            new StackedHeaderDecorator(Cli_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Cli_dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Cli_dataGridView.Columns.Clear();
            Cli_dataGridView.Columns.Add("A0", "항목");
            Cli_dataGridView.Columns.Add("A1", "1월");
            Cli_dataGridView.Columns.Add("A2", "2월");
            Cli_dataGridView.Columns.Add("A3", "3월");
            Cli_dataGridView.Columns.Add("A4", "4월");
            Cli_dataGridView.Columns.Add("A5", "5월");
            Cli_dataGridView.Columns.Add("A6", "6월");
            Cli_dataGridView.Columns.Add("A7", "7월");
            Cli_dataGridView.Columns.Add("A8", "8월");
            Cli_dataGridView.Columns.Add("A9", "9월");
            Cli_dataGridView.Columns.Add("A10", "10월");
            Cli_dataGridView.Columns.Add("A11", "11월");
            Cli_dataGridView.Columns.Add("A12", "12월");
        }


        void LabelWrite()
        {
            string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_HCneed, "기후데이터_부하", "관측고도, 경도, 위도, 해석설계외기온도,최고온도,최고절대습도,최고수평전일사량,해당월", "지역명 = '" + local + "'");

            if (Value.Length > 0)
            {
                double 습도 = Program.UTIL.ToDoubleOrZero(Value[0][5].ToString()) * 1000;
                localLabel.Text = local;
                heightLabel.Text = "해발고도: " + Program.UTIL.doubleComa(Value[0][0].ToString(), 1) + "m";
                latituLabel.Text = "위도: " + Program.UTIL.doubleComa(Value[0][1].ToString(), 1) + "˚";
                longituLabel.Text = "경도: " + Program.UTIL.doubleComa(Value[0][2].ToString(), 1) + "˚";
                heatTempLabel.Text = "난방실외온도: " + Program.UTIL.doubleComa(Value[0][3].ToString(), 1) + "˚";
                coolTempLabel.Text = "냉방실외온도: " + Program.UTIL.doubleComa(Value[0][4].ToString(), 1) + "˚";
                coolHumiLabel.Text = "냉방절대습도: " + Program.UTIL.doubleComa(습도.ToString(), 1) + "g/kg'";
                coolRadiLabel.Text = "냉방전일사량: " + Program.UTIL.doubleComa(Value[0][6].ToString(), 0) + "Wh/m"+Program.UTIL.Subscript(2,true);
            }
        }

        public void LoadData()  // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s = "";

            //상위라벨작성

            double[] temp = new double[12], humi = new double[12];
            double[] south = new double[12], north = new double[12], west = new double[12], east = new double[12], hori = new double[12];


            string[] 방위 = new string[] { "동", "서", "남", "북", "수평" };
            for (int i = 0; i < 12; i++)
            {
                string mth = (i + 1).ToString() + "월";
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "온도,상대습도", "지역명 = '" + local + "' And 기간 = '" + mth + "'");
                temp[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString());
                humi[i] = Program.UTIL.ToDoubleOrZero(Value[0][1].ToString());

                double Pa = 611.2 * Math.Pow(Math.E, 17.62 * temp[i] / (243.12 + temp[i])); //현재온도 포화수증기압
                Pa = Pa * humi[i];
                double lnRatio = Math.Log(Pa / 611.2);
                humi[i] = (243.12 * lnRatio) / (17.62 - lnRatio);

                foreach (string k in 방위)
                {
                    if (k == "수평")
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 = '" + local + "' And 기간 = '" + mth + "' And 방향 = '" + k + "' And 각도 = '0˚'");
                        hori[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString()) * 24 * month[i] / 1000;
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 = '" + local + "' And 기간 = '" + mth + "' And 방향 = '" + k + "' And 각도 = '90˚'");
                        switch (k)
                        {
                            case "남":
                                south[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString()) * 24 * month[i] / 1000;
                                break;
                            case "동":
                                east[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString()) * 24 * month[i] / 1000;
                                break;
                            case "서":
                                west[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString()) * 24 * month[i] / 1000;
                                break;
                            case "북":
                                north[i] = Program.UTIL.ToDoubleOrZero(Value[0][0].ToString()) * 24 * month[i] / 1000;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }


            // 그래프 그리기
            string[] label = new string[] { $"실외온도", $"이슬점온도", $"수평일사량", $"남측일사량", $"북측일사량", $"서측일사량", $"동측일사량" };
            string[] Color = new string[] { $"rgba({255}, {0}, {0}, 1)", $"rgba({0}, {0}, {255}, 1)", $"rgba({0}, {255}, {0}, 1)", $"rgba({255}, {180}, {0}, 1)", $"rgba({0}, {255}, {255}, 1)", $"rgba({255}, {0}, {255}, 1)", $"rgba({10}, {50}, {100}, 1)" };
            string Gray = $"rgba({165}, {165}, {165}, 1)";

            string[] data = new string[7];
            for (int m = 0; m < 12; m++)
            {
                if (m == 11)
                {
                    data[0] += Program.UTIL.doubleComa(temp[m].ToString(), 1);
                    data[1] += Program.UTIL.doubleComa(humi[m].ToString(), 0);
                    data[2] += Program.UTIL.doubleComa(hori[m].ToString(), 0);
                    data[3] += Program.UTIL.doubleComa(south[m].ToString(), 0);
                    data[4] += Program.UTIL.doubleComa(north[m].ToString(), 0);
                    data[5] += Program.UTIL.doubleComa(west[m].ToString(), 0);
                    data[6] += Program.UTIL.doubleComa(east[m].ToString(), 0);
                }
                else
                {
                    data[0] += Program.UTIL.doubleComa(temp[m].ToString(), 1) + ",";
                    data[1] += Program.UTIL.doubleComa(humi[m].ToString(), 0) + ",";
                    data[2] += Program.UTIL.doubleComa(hori[m].ToString(), 0) + ",";
                    data[3] += Program.UTIL.doubleComa(south[m].ToString(), 0) + ",";
                    data[4] += Program.UTIL.doubleComa(north[m].ToString(), 0) + ",";
                    data[5] += Program.UTIL.doubleComa(west[m].ToString(), 0) + ",";
                    data[6] += Program.UTIL.doubleComa(east[m].ToString(), 0) + ",";
                }

            }
            //데이터그리드뷰그리기
            tableMake();
            string[] 이름 = new string[] { "실외온도", "이슬점온도", "수평일사량", "남측일사량", "북측일사량", "서측일사량", "동측일사량" };
            
            for (int i = 0; i < 7; i++)
            {
                Cli_dataGridView.Rows.Add();
                for (int k = 0; k < 13; k++)
                {
                    if(k == 0)
                    {
                        Cli_dataGridView.Rows[i].Cells[k].Value = 이름[i];
                    }
                    else
                    {
                        switch (이름[i])
                        {
                            case "실외온도":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(temp[k-1].ToString(), 1);
                                break;
                            case "이슬점온도":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(humi[k - 1].ToString(), 1);
                                break;
                            case "수평일사량":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(hori[k - 1].ToString(), 1);
                                break;
                            case "남측일사량":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(south[k - 1].ToString(), 1);
                                break;
                            case "북측일사량":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(north[k - 1].ToString(), 1);
                                break;
                            case "동측일사량":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(east[k - 1].ToString(), 1);
                                break;
                            case "서측일사량":
                                Cli_dataGridView.Rows[i].Cells[k].Value = Program.UTIL.doubleComa(west[k - 1].ToString(), 1);
                                break;
                            default:
                                break;

                        }
                    }
                       
                }
            }


            //그래프 JS로 보내기
            var dataset = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                string la = label[i];
                string co = Color[i];
                string da = data[i];

                string yAxisID = (i == 0 || i == 1) ? "y2" : "y";

                string ds = "{"
                    + $"label:\"{la}\","
                    + "type:\"line\","
                    + $"data:[{da}],"
                    + "pointStyle:\"circle\","
                    + "pointRadius:2.5,"
                    + "borderWidth:0.5,"
                    + $"borderColor:\"{co}\","
                    + "backgroundColor:\"rgba(220,220,220,0.1)\","
                    + "dash:false,"
                    + "tension:0.4,"
                    + $"yAxisID:\"{yAxisID}\""
                    + "}";

                dataset.Add(ds);
            }

            string jsArray = "[" + string.Join(",", dataset) + "]";

            double Elec_max = Math.Ceiling(hori.Max() / 20) * 20;
            string jsCmd = $"drawChart_energyuse({jsArray}, {Elec_max});";
            webView21.CoreWebView2.ExecuteScriptAsync(jsCmd);
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
