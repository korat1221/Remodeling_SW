using Eagle._Components.Public;
using Eagle._Interfaces.Public;

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

            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            InitializeAsync();
           
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
        public void load_List()
        {
            List<object> MainMenu = new List<object>();

            MainMenu.Add(new { text = "불투명구조체", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_Structure\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "투명구조체", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_Win\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "조명_기밀_환기", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_Lighting\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "전기HP", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_ElecHP\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "가스HP", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_GasHP\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "보일러_태양열", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_Boiler\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "냉동기", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_Chiler\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "신재생시스템", id = "{\\\"formID\\\":26,\\\"ID\\\":\\\"Element_RESystem\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

            Program.UTIL.resetMainTree(5, 1, MainMenu.ToArray(), "26"); // 예시 코드: 메인 메뉴 동적 할당
        }
        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
            string[][] 프로젝트유형 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if(ID !="0")
            {
                if (프로젝트유형[0][0] == "1")
                {
                    MessageBox.Show("리모델링 전 요소기술 레포트는 아직 준비 중입니다.");
                    if (ID == "Element_Structure")
                    {
                        Element_Structure Structure = new Element_Structure();
                        Structure.Report_Before();
                    }
                    else if (ID == "Element_Win")
                    {
                        Element_Win win = new Element_Win();
                        win.Report_Before();
                    }
                    else if (ID == "Element_Lighting")
                    {
                        Element_Lighting light = new Element_Lighting();
                        light.Report_Before();
                    }
                    else if (ID == "Element_ElecHP")
                    {
                        Element_ElecHP ehp = new Element_ElecHP();
                        ehp.Report_Before();
                    }
                    else if (ID == "Element_GasHP")
                    {
                        Element_GasHP re = new Element_GasHP();
                        re.Report_Before();
                    }
                    else if (ID == "Element_Boiler")
                    {
                        Element_Boiler boiler = new Element_Boiler();
                        boiler.Report_Before();
                    }
                    else if (ID == "Element_Chiler")
                    {
                        Element_Chiler chiler = new Element_Chiler();
                        chiler.Report_Before();
                    }
                    else if (ID == "Element_RESystem")
                    {
                        Element_RESystem re = new Element_RESystem();
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
                    if (ID == "Element_Structure")
                    {
                        Element_Structure Structure = new Element_Structure();
                        script = Structure.Report_After();
                    }
                    else if (ID == "Element_Win")
                    {
                        Element_Win win = new Element_Win();
                        script = win.Report_After();
                    }
                    else if (ID == "Element_Lighting")
                    {
                        Element_Lighting light = new Element_Lighting();
                        script = light.Report_After();
                    }
                    else if (ID == "Element_ElecHP")
                    {
                        Element_ElecHP ehp = new Element_ElecHP();
                        script = ehp.Report_After();
                    }
                    else if (ID == "Element_GasHP")
                    {
                        Element_GasHP re = new Element_GasHP();
                        script = re.Report_After();
                    }
                    else if (ID == "Element_Chiler")
                    {
                        Element_Chiler chiler = new Element_Chiler();
                        script = chiler.Report_After();
                    }
                    else if (ID == "Element_Boiler")
                    {
                        Element_Boiler boiler = new Element_Boiler();
                        script = boiler.Report_After();
                    }
                    else if (ID == "Element_RESystem")
                    {
                        Element_RESystem re = new Element_RESystem();
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


    }
}
