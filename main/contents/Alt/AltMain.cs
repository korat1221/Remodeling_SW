using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using main.subcontents.Alt;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static main.DB;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Eagle._Components.Public;
using System.Drawing.Text;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Web.WebView2.Core;
using main.subcontents.ConstructionWall;

namespace main.contents.Alt
{
    public partial class AltMain : Form
    {
        bool scriptable = false;
        DataGridViewCheckBoxColumn Alt_checkBoxColumn = new DataGridViewCheckBoxColumn();
        public AltMain()
        {
            InitializeComponent();
            InitializeAsync();
            webView22.Source = new Uri(Program.gPath + "chart_ctrl2.html", true);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            create_Alt_Table();
        }
        async void InitializeAsync()
        {
            await webView22.EnsureCoreWebView2Async(null);
            webView22.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
            Load_RuleResult();
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView22.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {

        }

        private void Save()
        {

        }

        private void reset()
        {

        }
        public void LoadData(String ID)
        {
            create_wall_table();
        }

        private void AltMainPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        #region 사용자 요소기술 우선순위
        private void create_Alt_Table()
        {
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Alt_dataGridView.Columns.Clear();
            Alt_checkBoxColumn.HeaderText = "선택";
            Alt_checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(Alt_checkBoxColumn);

            Alt_dataGridView.Columns.Add("A1", "순위");
            Alt_dataGridView.Columns.Add("A2", "요소기술");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 60;
        }

        private void Alt_Add_button_Click(object sender, EventArgs e)
        {
            if (Alt_dataGridView.Rows.Count > 1)
            {
                if (Alt_dataGridView.Rows[Alt_dataGridView.Rows.Count - 1].Cells[2].Value == null)
                {
                    MessageBox.Show("먼저 " + Alt_dataGridView.Rows.Count + "순위 요소기술을 선택해주세요.");
                }
                else
                {
                    Add_Alt();
                }
            }
            else
            {
                Add_Alt();
            }

        }
        private void Add_Alt()
        {
            int nRow = Alt_dataGridView.Rows.Add();
            Load_Alt_Num();
            string[] Selectlist = null;
            if (Alt_dataGridView.Rows.Count > 1)
            {
                Selectlist = new string[Alt_dataGridView.Rows.Count - 1];
                for (int i = 0; i < Alt_dataGridView.Rows.Count - 1; i++)
                {
                    if (Alt_dataGridView.Rows[i].Cells[2].Value != null)
                    { Selectlist[i] = Alt_dataGridView.Rows[i].Cells[2].Value.ToString(); }
                }
            }
            string[] Newlist = Get_ElementList(Selectlist);

            DataGridViewComboBoxCell Combo = new DataGridViewComboBoxCell();
            for (int i = 0; i < Newlist.Length; i++)
            {
                Combo.Items.Add(Newlist[i]);
            }
            Alt_dataGridView.Rows[nRow].Cells[2] = Combo;
        }

        private string[] Get_ElementList(string[] Selectlist)
        {
            string[] list = { "외벽", "지붕", "최하층바닥", "창호", "커튼월창", "외부출입문", "기밀환기", "난방설비", "냉방설비", "급탕설비", "조명" };
            string[] Newlist = null;
            if (Selectlist != null)
            {
                for (int i = 0; i < Selectlist.Length; i++)
                {
                    list = list.Where(num => num != Selectlist[i]).ToArray();
                }
                Newlist = list;
            }
            else
            {
                Newlist = list;
            }

            return Newlist;
        }

        private void Alt_Remove_button_Click(object sender, EventArgs e)
        {
            int Boiler_SelectRow = GetSelectedIndex();
            Alt_dataGridView.Rows.Remove(Alt_dataGridView.Rows[Boiler_SelectRow]);
            Load_Alt_Num();
        }
        private void Load_Alt_Num()
        {
            for (int k = 0; k < Alt_dataGridView.RowCount; k++)
            {
                Alt_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString() + " 순위";
            }
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

        #endregion

        #region 법규기반 검토
        private void Load_RuleResult()
        {
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "검토유형, 총에너지소요량", "월='연간' and 연료='전체'");
            if (value.Length > 0)
            {
                string[] ElementType = new string[value.Length];
                double[] Energy = new double[value.Length];
                double[] Saving = new double[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    Energy[i] = Convert.ToDouble(value[i][1]);
                }
                Array.Sort(Energy);

                for (int a = 0; a < Energy.Length; a++)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (Energy[a] == Convert.ToDouble(value[i][1]))
                        {
                            ElementType[a] = value[i][0].Substring(3, value[i][0].Length - 3);
                            break;
                        }
                    }
                }

                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                if (value2.Length > 0)
                {
                    for (int a = 0; a < Energy.Length; a++)
                    {
                        Saving[a] = Convert.ToDouble(value2[0][0]) - Energy[a];
                    }
                }


                string s = "", s2 = "";
                for (int a = 1; a < Saving.Length; a++)
                {
                    s2 += Convert.ToDouble(Saving[a].ToString("0")) + ",";
                    s += "\"" + ElementType[a] + "\",";
                }

                runScript("drawChart5([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:150}],[" + s + "])");

                new StackedHeaderDecorator(RuleResult_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
                RuleResult_dataGridView.Columns.Clear();
                RuleResult_dataGridView.Columns.Add("A0", "순위");
                RuleResult_dataGridView.Columns.Add("A1", "요소기술");
                RuleResult_dataGridView.Columns.Add("A2", "절감량");
                RuleResult_dataGridView.Columns[0].Width = 60;
                for (int a = 1; a < Saving.Length; a++)
                {
                    int nRow = RuleResult_dataGridView.Rows.Add();
                    RuleResult_dataGridView.Rows[nRow].Cells[0].Value = a + " 순위";
                    RuleResult_dataGridView.Rows[nRow].Cells[1].Value = ElementType[a];
                    RuleResult_dataGridView.Rows[nRow].Cells[2].Value = Saving[a].ToString("0") + " kWh";

                }
            }
        }
        #endregion

        #region 외벽
        string WallRemodelingType, WallEx;
        private void create_wall_table()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("A0", "번호");
            dataGridView1.Columns.Add("A1", "명칭");
            dataGridView1.Columns.Add("A2", "유효열관류율.[W/m²·K]");
            dataGridView1.Columns.Add("A3", "흡수율.[-]");
            dataGridView1.Columns.Add("A4", "면적.[m²]");
            dataGridView1.Columns[0].Width = 40;

            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭,유효열관류율,흡수율", "");
            if (List.Length > 0)
            {
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = dataGridView1.Rows.Add();
                    dataGridView1.Rows[nRow].Cells[0].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][1];
                    dataGridView1.Rows[nRow].Cells[2].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][2]));
                    dataGridView1.Rows[nRow].Cells[3].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][3]));
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    double A = 0;
                    if (Area.Length > 0)
                    {
                        for (int a = 0; a < Area.Length; a++)
                        {
                            A += Convert.ToDouble(Area[a][0]);
                        }
                        dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", A);
                    }
                }
            }

            WallRemodelingType_comboBox.Items.Clear();
            WallRemodelingType_comboBox.Items.Add("내부덧댐");
            WallRemodelingType_comboBox.Items.Add("외부덧댐");
            WallRemodelingType_comboBox.Items.Add("철거 후 신규");
        }
        private void WallRemodelingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallRemodelingType_comboBox.SelectedItem != null)
            {
                WallRemodelingType = WallRemodelingType_comboBox.SelectedItem.ToString();
                change_comboBox_WallEx();
            }
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
        private void WallEx_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallEx_comboBox.SelectedItem != null)
            {
                WallEx = WallEx_comboBox.SelectedItem.ToString();
            }
        }
        private void WallAlt_button_Click(object sender, EventArgs e)
        {
            if (WallRemodelingType != null && WallEx != null)
            {
              //  Cal_Alt cal = new Cal_Alt();
              //  cal.Get_Optimal_WallData(WallRemodelingType, WallEx);
                AltWall TB_form = new AltWall(WallRemodelingType);
                DialogResult result = TB_form.ShowDialog();
                if (result == DialogResult.OK)
                {

                }
            }
            else
            {
                if (WallRemodelingType == null) { MessageBox.Show("외벽 리모델링 방식을 선택하세요."); }
                else if (WallEx == null) { MessageBox.Show("외벽 마감재 유형을 선택하세요."); }
            }
            
        }
        #endregion




       
    }
}
