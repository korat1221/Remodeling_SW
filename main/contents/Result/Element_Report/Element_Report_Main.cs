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

            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
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

            MainMenu.Add(new { text = "불투명구조체", id = "{\\\"formID\\\":60,\\\"ID\\\":\\\"Element_Structure\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "투명구조체", id = "{\\\"formID\\\":61,\\\"ID\\\":\\\"Element_Win\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "조명_기밀_환기", id = "{\\\"formID\\\":62,\\\"ID\\\":\\\"Element_Lighting\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "전기HP", id = "{\\\"formID\\\":63,\\\"ID\\\":\\\"Element_ElecHP\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "가스HP", id = "{\\\"formID\\\":64,\\\"ID\\\":\\\"Element_GasHP\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "보일러_태양열", id = "{\\\"formID\\\":65,\\\"ID\\\":\\\"Element_Boiler\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "냉동기", id = "{\\\"formID\\\":66,\\\"ID\\\":\\\"Element_Chiler\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "신재생시스템", id = "{\\\"formID\\\":67,\\\"ID\\\":\\\"Element_RESystem\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

            Program.UTIL.resetMainTree(5, 1, MainMenu.ToArray(), "60"); // 예시 코드: 메인 메뉴 동적 할당
        }
        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();           
        }
    }
}
