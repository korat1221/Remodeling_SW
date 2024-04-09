using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class Element_Report : Form
    {
        public Element_Report()
        {
            InitializeComponent();
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
        public void load_List()
        {
            List<object> MainMenu = new List<object>();

            MainMenu.Add(new { text = "불투명구조체", id = "{\\\"formID\\\":56,\\\"ID\\\":\\\"Result_1\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            MainMenu.Add(new { text = "투명구조체", id = "{\\\"formID\\\":56,\\\"ID\\\":\\\"Result_2\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            Program.UTIL.resetMainTree(5, 1, MainMenu.ToArray(), "56"); // 예시 코드: 메인 메뉴 동적 할당
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();            
        }
    }
}
