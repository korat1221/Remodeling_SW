using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ZoneLighting
{
    public partial class LightingNatural_facade : Form
    {


        //변수
        string ZoneNum;
        string NaturalType;
        public String facadetype, doubleskinglasstype, atriumglasstype;
        public string rooftype;
        List<String> GlassList = new List<String>();

        public double W, L, H, Tao;



        public LightingNatural_facade(string NaturalType, string ZoneNum)
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            this.ZoneNum = ZoneNum;
            this.NaturalType = NaturalType;
            NaturalType_textBox.Text = NaturalType;

            LoadData(ZoneNum);

            //파사드 세부유형 콤보박스
            NaturalLight_comboBox.Items.Clear();
            NaturalLight_comboBox.Items.Add("일반 파사드");
            NaturalLight_comboBox.Items.Add("이중외피");
            NaturalLight_comboBox.Items.Add("중정");
            NaturalLight_comboBox.Items.Add("아트리움");
            NaturalLight_comboBox.SelectedIndex = 0;

            GlassList.Clear();
            //유리 콤보박스
            string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명", "");
            if (User_WinGlass.Length > 0)
            {
                for (int n = 0; n < User_WinGlass.Length; n++)
                { GlassList.Add(User_WinGlass[n][1]); }
            }
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명", "");
            if (WinGlass.Length > 0)
            {
                for (int n = 0; n < WinGlass.Length; n++)
                {
                    GlassList.Add(WinGlass[n][1]);
                }
            }
            string[] GlassArray = GlassList.ToArray();
            glass1_comboBox.Items.Clear();
            glass1_comboBox.Items.AddRange(GlassArray);
        }




        //파사드 세부유형 선택
        private void NaturalLight_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            facadetype = NaturalLight_comboBox.SelectedItem.ToString();
            Load_facadetype_image(facadetype);

            Act_Glass();
            Act_Dim();
        }





        //계산ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ


        //세부유형에 맞게 유리 정보 조건부 
        private void Act_Glass()
        {
            if (facadetype == "이중외피")
            {
                glass1_label.Visible = true;
                glass1_comboBox.Visible = true;
                glass_label.Visible = true;
                Tao_textBox.Visible = true;
                glaassinfo_label.Visible = true;


            }

            else if (facadetype == "아트리움")
            {
                glass1_label.Visible = false;
                glass1_comboBox.Visible = false;
                glass_label.Visible = true;
                Tao_textBox.Visible = true;
                glaassinfo_label.Visible = true;


            }

            else
            {
                glass1_label.Visible = false;
                glass1_comboBox.Visible = false;
                glass_label.Visible = false;
                Tao_textBox.Visible = false;
                glaassinfo_label.Visible = false;

            }

        }



        //세부유형에 맞게 치수 정보 조건부 
        private void Act_Dim()
        {
            if (facadetype == "중정" || facadetype == "아트리움")
            {
                W_label.Visible = true;
                W_textBox.Visible = true;
                Wm_label.Visible = true;
                L_label.Visible = true;
                L_textBox.Visible = true;
                Lm_label.Visible = true;
                H_label.Visible = true;
                H_textBox.Visible = true;
                Hm_label.Visible = true;
                Dim_label.Visible = true;

            }


            else
            {
                W_label.Visible = false;
                W_textBox.Visible = false;
                Wm_label.Visible = false;
                L_label.Visible = false;
                L_textBox.Visible = false;
                Lm_label.Visible = false;
                H_label.Visible = false;
                H_textBox.Visible = false;
                Hm_label.Visible = false;
                Dim_label.Visible = false;


            }

        }



        //파사드 중분류에 맞는 이미지
        private void Load_facadetype_image(String Type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광중분류이미지", "이미지", "자연채광중분류 = '" + Type + "'");
            if (Image.Length > 0)
            {
                Middle_pictureBox.Load(Program.gPath + Image[0][0]);
                Middle_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }



        private void Save_button_Click_1(object sender, EventArgs e)
        {
            facadetype = facadetype.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();

            glass();
            dim();



        }


        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        private void glass1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            doubleskinglasstype = glass1_comboBox.SelectedItem.ToString();
            atriumglasstype = glass1_comboBox.SelectedItem.ToString();
            string[][] User_WinGlass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "번호,제품명,빛투과율", "제품명 ='" + glass1_comboBox.SelectedItem.ToString() + "'");
            if (User_WinGlass.Length > 0)
            {
                Tao = Convert.ToDouble(User_WinGlass[0][2]);
            }
            string[][] WinGlass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "번호,제품명,빛투과율", "제품명 ='" + glass1_comboBox.SelectedItem.ToString() + "'");
            if (WinGlass.Length > 0)
            {
                Tao = Convert.ToDouble(WinGlass[0][2]);
            }
            Tao_textBox.Text = string.Format("{0:F3}", Tao);

        }
        //파사드 이중외피 아트리움 유리 저장 값 
        private void glass()
        {
            if (facadetype == "일반 파사드" || facadetype == "중정")
            {
                doubleskinglasstype = null;
                atriumglasstype = null;
                Tao = 0;

            }

            else if (facadetype == "이중외피")
            {
                atriumglasstype = null;
            }

            else if (facadetype == "아트리움")
            {
                atriumglasstype = null;
            }

            else;
        }


        //파사드 치수정보 저장값
        private void dim()
        {


            if (facadetype == "중정" || facadetype == "아트리움")
            {
                W = Convert.ToDouble(W_textBox.Text);
                L = Convert.ToDouble(L_textBox.Text);
                H = Convert.ToDouble(H_textBox.Text);
            }

            else
            {
                W = 0;
                L = 0;
                H = 0;

            }
        }

        private void LoadData(String ZoneNum)
        {
            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "자연채광유형,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,서브유형",
                "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                facadetype = Load[0][0];
                NaturalLight_comboBox.SelectedItem = facadetype;

                doubleskinglasstype = Load[0][1];
                atriumglasstype = Load[0][2];
                Tao = Convert.ToDouble(Load[0][3]);
                glass1_comboBox.SelectedItem = doubleskinglasstype;
                Tao_textBox.Text = Tao.ToString();

                W = Convert.ToDouble(Load[0][4]);
                L = Convert.ToDouble(Load[0][5]);
                H = Convert.ToDouble(Load[0][6]);

                W_textBox.Text = W.ToString();
                L_textBox.Text = L.ToString();
                H_textBox.Text = H.ToString();

                Load_facadetype_image(facadetype);
            }
        }

    }
}
