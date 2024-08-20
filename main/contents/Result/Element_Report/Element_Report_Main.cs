using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using Microsoft.Office.Interop.Excel;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using main.contents.Result.Element_Report;
using System.Collections;

namespace main.contents
{
    public partial class Element_Report_Main : Form
    {
        bool scriptable = false;
        public Element_Report_Main()
        {

            InitializeComponent();
            InitializeAsync();
            Element_comboBox.Items.Clear();
            Element_comboBox.Items.Add("외벽_지붕_바닥");
            Element_comboBox.Items.Add("창호_커튼월창_출입문");
            Element_comboBox.Items.Add("조명_기밀_환기");
            Element_comboBox.Items.Add("전기 히트펌프");
            Element_comboBox.Items.Add("보일러_태양열_급탕HP");
            Element_comboBox.Items.Add("공냉식냉동기_수냉식냉동기");
            Element_comboBox.Items.Add("가스HP_흡수식냉온수기");
            Element_comboBox.Items.Add("태양광_풍력_연료전지");
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                string s = args.TryGetWebMessageAsString();
            }
            catch (Exception ex)
            {

            }
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
        private void Element_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Element_comboBox.SelectedItem != null && Element_comboBox.SelectedItem.ToString() != "")
            {
                string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
                if (프로젝트유형[0][0] == "1")
                {
                    MessageBox.Show("리모델링 전 요소기술 레포트는 아직 준비 중입니다.");
                    if (Element_comboBox.SelectedItem.ToString() == "외벽_지붕_바닥")
                    {
                        Element_Structure Structure = new Element_Structure();
                        Structure.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "창호_커튼월창_출입문")
                    {
                        Element_Win win = new Element_Win();
                        win.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "전기 히트펌프")
                    {
                        Element_ElecHP ehp = new Element_ElecHP();
                        ehp.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "공냉식냉동기_수냉식냉동기")
                    {
                        Element_Chiler chiler = new Element_Chiler();
                        chiler.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "보일러_태양열_급탕HP")
                    {
                        Element_Boiler boiler = new Element_Boiler();
                        boiler.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "조명_기밀_환기")
                    {
                        Element_Lighting light = new Element_Lighting();
                        light.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "태양광_풍력_연료전지")
                    {
                        Element_RESystem re = new Element_RESystem();
                        re.Report_Before();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "가스HP_흡수식냉온수기")
                    {
                        Element_GasHP re = new Element_GasHP();
                        re.Report_Before();
                    }
                    else
                    {
                        Element_Structure Structure = new Element_Structure();
                        Structure.Report_Before();
                    }
                }
                else
                {
                    string script = null;
                    if (Element_comboBox.SelectedItem.ToString() == "외벽_지붕_바닥")
                    {
                        Element_Structure Structure = new Element_Structure();
                        script = Structure.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "창호_커튼월창_출입문")
                    {
                        Element_Win win = new Element_Win();
                        script = win.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "전기 히트펌프")
                    {
                        Element_ElecHP ehp = new Element_ElecHP();
                        script = ehp.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "공냉식냉동기_수냉식냉동기")
                    {
                        Element_Chiler chiler = new Element_Chiler();
                        script = chiler.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "보일러_태양열_급탕HP")
                    {
                        Element_Boiler boiler = new Element_Boiler();
                        script = boiler.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "조명_기밀_환기")
                    {
                        Element_Lighting light = new Element_Lighting();
                        script = light.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "태양광_풍력_연료전지")
                    {
                        Element_RESystem re = new Element_RESystem();
                        script = re.Report_After();
                    }
                    else if (Element_comboBox.SelectedItem.ToString() == "가스HP_흡수식냉온수기")
                    {
                        Element_GasHP re = new Element_GasHP();
                        script = re.Report_After();
                    }
                    else
                    {
                        Element_Structure Structure = new Element_Structure();
                       script = Structure.Report_After();
                    }
                    runScript(script);
                }
            }
        }
        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            Element_comboBox.SelectedIndex = 0;
        }


    }
}
