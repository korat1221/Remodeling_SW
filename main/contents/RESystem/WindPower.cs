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

namespace main.contents
{
    public partial class WindPower : Form
    {

        String[][] 지역;
        String Num, Name;

        public WindPower()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);

            #region getvalue

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '풍력시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            #endregion / getvalue

        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            Reset();

            Num_textBox.Text = ID;
            Num = ID;
        }
        public static bool OnLoadListProc(Form form)
        {
            List_PV f = (List_PV)form;
            f.load_List();
            return true;
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }
    }
}
