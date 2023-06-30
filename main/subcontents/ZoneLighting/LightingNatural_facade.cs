using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ZoneLighting
{
    public partial class LightingNatural_facade : Form
    {


        //변수
        string NaturalType;
        public String facadetype;
        public string rooftype;




        public LightingNatural_facade(string NaturalType) 
        {
            InitializeComponent();
            this.NaturalType = NaturalType;
            NaturalType_textBox.Text = NaturalType;
            
           


            //파사드 세부유형 콤보박스
            NaturalLight_comboBox.Items.Clear();
            NaturalLight_comboBox.Items.Add("일반 파사드");
            NaturalLight_comboBox.Items.Add("이중외피");
            NaturalLight_comboBox.Items.Add("중정");
            NaturalLight_comboBox.Items.Add("아트리움");
            NaturalLight_comboBox.SelectedIndex = 0;

            //이중외피 아트리움 유리 콤보박스
            glass1_comboBox.Items.Clear();
            glass1_comboBox.Items.Add("LE/12R/CL/12R/LE");
            glass1_comboBox.Items.Add("LE/12R/CL/13R/LE");
            glass1_comboBox.Items.Add("LE/12R/CL/14R/LE");
            glass1_comboBox.Items.Add("LE/12R/CL/15R/LE");
            glass1_comboBox.SelectedIndex = 0;

            glass2_comboBox.Items.Clear();
            glass2_comboBox.Items.Add("LE/12R/CL/12R/LE");
            glass2_comboBox.Items.Add("LE/12R/CL/13R/LE");
            glass2_comboBox.Items.Add("LE/12R/CL/14R/LE");
            glass2_comboBox.Items.Add("LE/12R/CL/15R/LE");
            glass2_comboBox.SelectedIndex = 0;



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
                glass2_label.Visible = false;
                glass2_comboBox.Visible = false;
                glass1_label.Visible = true;
                glass1_comboBox.Visible = true;
                glass_label.Visible = true;
                glass_textBox.Visible = true;
                glaassinfo_label.ForeColor = Color.Black;

            }

            else if (facadetype == "아트리움")
            {
                glass1_label.Visible = false;
                glass1_comboBox.Visible = false;
                glass2_label.Visible = true;
                glass2_comboBox.Visible = true;
                glass_label.Visible = true;
                glass_textBox.Visible = true;
                glaassinfo_label.ForeColor = Color.Black;

            }

            else
            {
                glass1_label.Visible = false;
                glass1_comboBox.Visible = false;
                glass2_label.Visible = false;
                glass2_comboBox.Visible = false;
                glass_label.Visible = false;
                glass_textBox.Visible = false;
                glaassinfo_label.ForeColor= Color.Gray;


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
                Dim_label.ForeColor = Color.Black;

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
                Dim_label.ForeColor = Color.Gray;


            }

        }



        //파사드 중분류에 맞는 이미지
        private void Load_facadetype_image(String Type)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광중분류이미지", "이미지", "자연채광중분류 = '" + Type + "'");
            Middle_pictureBox.Load(Program.gPath + Image[0][0]);
            Middle_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }



        private void Save_button_Click_1(object sender, EventArgs e)
        {
           facadetype = facadetype.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
            

        }





    }
}
