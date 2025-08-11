using main.info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace main.subcontents.ZoneLighting
{
    public partial class LightingNatural_roof : Form

    {
        string ZoneNum;

        string NaturalType, facadetype;
        public string rooftype;
        public double roofangle1, roofangle2, rooflength1, rooflength2, rooflength3;





        public LightingNatural_roof(string NaturalType, string ZoneNum)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            this.NaturalType = NaturalType;
            this.ZoneNum = ZoneNum;
            NaturalType2_textBox.Text = NaturalType;

            LoadData(ZoneNum);

            //천창 세부유형 콤보박스 
            NaturalLight2_comboBox.Items.Clear();
            NaturalLight2_comboBox.Items.Add("일반형");
            NaturalLight2_comboBox.Items.Add("돔형");
            NaturalLight2_comboBox.Items.Add("톱니형");
            NaturalLight2_comboBox.SelectedIndex = 0;

            //천창 유리각 콤보박스 
            roofangle1_comboBox.Items.Clear();
            roofangle1_comboBox.Items.Add("30");
            roofangle1_comboBox.Items.Add("45");
            roofangle1_comboBox.Items.Add("60");
            roofangle1_comboBox.Items.Add("90");
            roofangle1_comboBox.SelectedIndex = 0;





        }



        //천창 세부유형 선택
        private void NaturalLight2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            rooftype = NaturalLight2_comboBox.SelectedItem.ToString();
            Load_rooftype_image(rooftype);

            roofangle();

        }



        private void Save_button_Click_1(object sender, EventArgs e)
        {
            rooftype = rooftype.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();

            glass();
            dim();

        }



        //계산ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ





        //천창 중분류에 맞는 이미지 
        private void Load_rooftype_image(String Type2)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광중분류이미지2", "이미지", "자연채광중분류 = '" + Type2 + "'");
            if (Image.Length > 0)
            {
                Middle2_pictureBox.Load(Program.gPath + Image[0][0]);
                Middle2_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        public void roofangle()
        {
            if (rooftype == "톱니형")
            {
                roofangle2_comboBox.Items.Clear();
                roofangle2_comboBox.Items.Add("30");
                roofangle2_comboBox.Items.Add("45");
                roofangle2_comboBox.Items.Add("60");
                roofangle2_comboBox.Items.Add("75");
                roofangle2_comboBox.SelectedIndex = 0;
            }


            else if (rooftype == "돔형" || rooftype == "일반형")
            {
                roofangle2_comboBox.Items.Clear();
                roofangle2_comboBox.Items.Add("30");
                roofangle2_comboBox.Items.Add("60");
                roofangle2_comboBox.Items.Add("90");
                roofangle2_comboBox.SelectedIndex = 0;
            }

            else;

        }




        //천창 유리정보 저장값 
        private void glass()
        {
            roofangle1 = Convert.ToDouble(roofangle1_comboBox.SelectedItem);
            roofangle2 = Convert.ToDouble(roofangle2_comboBox.SelectedItem);
        }


        //천창 치수정보 저장값 
        private void dim()
        {
            rooflength1 = Convert.ToDouble(rooflength1_textBox.Text);
            rooflength2 = Convert.ToDouble(rooflength2_textBox.Text);
            rooflength3 = Convert.ToDouble(rooflength3_textBox.Text);
        }

        private void LoadData(String ZoneNum)
        {
            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "자연채광유형,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,서브유형",
                "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                rooftype = Load[0][0];
                NaturalLight2_comboBox.SelectedItem = rooftype;

                roofangle1 = Convert.ToDouble(Load[0][1]);
                roofangle1_comboBox.SelectedItem = roofangle1;

                roofangle2 = Convert.ToDouble(Load[0][2]);
                roofangle2_comboBox.SelectedItem = roofangle2;

                rooflength1 = Convert.ToDouble(Load[0][3]);
                rooflength1_textBox.Text = rooflength1.ToString();

                rooflength2 = Convert.ToDouble(Load[0][4]);
                rooflength2_textBox.Text = rooflength2.ToString();

                rooflength3 = Convert.ToDouble(Load[0][5]);
                rooflength3_textBox.Text = rooflength3.ToString();

                Load_rooftype_image(rooftype);
            }
        }

        private void infoRoofdb_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\2.subcontents\\10.ZoneLight\\03 NaturalRoof";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
