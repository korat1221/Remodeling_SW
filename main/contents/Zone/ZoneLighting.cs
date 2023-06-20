using main.subcontents.ZoneLighting;
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
    public partial class ZoneLighting : Form
    {

        //변수
        String LightMethod, ControlType;
        double UFF; //LightMethod에 따라 정해지는 값
        double Foc; //ControlType에 따라 정해지는 값



        public ZoneLighting()
        {

            // 화면 뜨자마자 있었으면 하는거 전부 콤보박스로 몰아 넣기 
            InitializeComponent();

            //조명 이미지 로드 
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //켜자마자 자동으로 층 및 명칭 불러오기 
            //https://agape93.tistory.com/6

            //조명방식 콤보박스
            Program.UTIL.FillComboBox(LightMethod_comboBox, "조명", "조명방식", "1");

            //제어방식 콤보박스
            Program.UTIL.FillComboBox(ControlType_comboBox, "조명", "제어종류", "1");

            //디밍유형 콤보박스
            Program.UTIL.FillComboBox(DimmingType_comboBox, "조명", "주광제어종류", "1");

            //집광채광 향 콤보박스
            Program.UTIL.FillComboBox(RenewDi_comboBox, "조명", "방위", "1");

            //집광채광 기울기 콤보박스
            Program.UTIL.FillComboBox(Slope_comboBox, "조명", "창기울기", "1");

        }



        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (LightMethod)
            {
                case "직접조명":
                    UFF = 0.1;
                    break;

                case "반직접조명":
                    UFF = 0.3;
                    break;

                case "반간접조명":
                    UFF = 0.7;
                    break;

                case "간접조명":
                    UFF = 0.9;
                    break;

            }

        }

        private void LightDB_button_Click(object sender, EventArgs e)
        {
            LightingDB lightingdb_form = new LightingDB();
            DialogResult result= lightingdb_form.ShowDialog();
            if (result == DialogResult.OK) { }
        }




        private void ControlType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ControlType)
            {
                case "일반제어":
                    Foc = 0.95;
                    break;

                case "스마트제어":
                    Foc = 0.8;
                    break;

            }
        }







    }
}
