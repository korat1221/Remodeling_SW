using main.contents.Result;
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

namespace main.contents
{
    public partial class Algorithm_Report : Form
    {
        public Algorithm_Report()
        {

            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
        }
        public void load_List()
        {
            List<object> MainMenu = new List<object>();

            MainMenu.Add(new { text = "에너지요구량", id = "{\\\"formID\\\":37,\\\"ID\\\":\\\"Algorithm_EnergyNeed\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "난방 에너지소요량", id = "{\\\"formID\\\":45,\\\"ID\\\":\\\"Algorithm_Heating\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "냉방 에너지소요량", id = "{\\\"formID\\\":46,\\\"ID\\\":\\\"Algorithm_Cooling\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "급탕 에너지소요량", id = "{\\\"formID\\\":47,\\\"ID\\\":\\\"Algorithm_DHW\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "조명 에너지소요량", id = "{\\\"formID\\\":44,\\\"ID\\\":\\\"Algorithm_Lighting\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "공조 에너지소요량", id = "{\\\"formID\\\":48,\\\"ID\\\":\\\"Algorithm_AHU\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
          //  MainMenu.Add(new { text = "신재생 에너지생산량", id = "{\\\"formID\\\":37,\\\"ID\\\":\\\"Algorithm_RESystem\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

            Program.UTIL.resetMainTree(6, 2, MainMenu.ToArray(), "37"); // 예시 코드: 메인 메뉴 동적 할당
        }
        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }


    }
}
