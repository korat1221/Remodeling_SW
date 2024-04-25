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
            Load_RuleRsult();
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
            new StackedHeaderDecorator(Boiler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Boiler_dataGridView.Columns.Clear();
            Alt_checkBoxColumn.HeaderText = "선택";
            Alt_checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(Alt_checkBoxColumn);

            Boiler_dataGridView.Columns.Add("A1", "순위");
            Boiler_dataGridView.Columns.Add("A2", "요소기술");
            Boiler_dataGridView.Columns[0].Width = 40;
            Boiler_dataGridView.Columns[1].Width = 60;
        }

        private void Alt_Add_button_Click(object sender, EventArgs e)
        {
           
            int nRow = Boiler_dataGridView.Rows.Add();
            Load_Alt_Num();
            string[] Selectlist =null;
            if (Boiler_dataGridView.Rows.Count > 1)
            {
                Selectlist = new string[Boiler_dataGridView.Rows.Count - 1];
                for (int i = 0; i < Boiler_dataGridView.Rows.Count - 1; i++)
                {
                    if (Boiler_dataGridView.Rows[i].Cells[2].Value != null)
                    { Selectlist[i] = Boiler_dataGridView.Rows[i].Cells[2].Value.ToString(); }
                }
            }
            string[] Newlist = Get_ElementList(Selectlist);

            DataGridViewComboBoxCell Combo = new DataGridViewComboBoxCell();
            for(int i = 0; i < Newlist.Length; i++)
            {
                Combo.Items.Add(Newlist[i]);
            }           
            Boiler_dataGridView.Rows[nRow].Cells[2] = Combo;
        }
        private string[] Get_ElementList(string[] Selectlist)
        {
            string[] list = { "외벽", "지붕", "최하층바닥", "창호", "커튼월창", "외부출입문", "기밀환기", "난방설비", "냉방설비", "급탕설비", "조명" };
            string[] Newlist = null;
            if(Selectlist != null)
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
            Boiler_dataGridView.Rows.Remove(Boiler_dataGridView.Rows[Boiler_SelectRow]);
            Load_Alt_Num();
        }
        private void Load_Alt_Num()
        {
            for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
            {
                Boiler_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString() + " 순위";
            }
        }
        private int GetSelectedIndex()
        {
            for (int k = 0; k < Boiler_dataGridView.Rows.Count; k++)
            {
                if (Convert.ToBoolean(Boiler_dataGridView.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }

        #endregion
       
        private void Load_RuleRsult()
        {            
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result_Alt", "검토유형, 총에너지소요량", "월='연간' and 연료='전체'");
            if(value.Length > 0)
            {
                string[] ElementType = new string[value.Length];
                double[] Energy  = new double[value.Length];
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
                            ElementType[a] = value[i][0].Substring(3, value[i][0].Length -3);
                            break;
                        }
                    }
                }

                string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "월='연간' and 연료='전체'");
                if(value2.Length > 0)
                {
                    for (int a = 0; a < Energy.Length; a++)
                    {
                        Saving[a] = Convert.ToDouble(value2[0][0])- Energy[a];
                    }
                }


                string s = "", s2 = ""; 
                for (int a = 1; a < Saving.Length ; a++)
                {
                        s2 += Convert.ToDouble(Saving[a].ToString("0")) + ",";
                        s += "\""+ElementType[a] + "\",";
                }
                

                runScript("drawChart5([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:150}],["+s+"])");

            }
        }


    }
}
