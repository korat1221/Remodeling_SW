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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ConstructionCW : Form
    {
        private String CWNum;
        String CWName, Type, OldWindow, UwMehod, DiIndi, FrameType, SingleDoubleType, FrameMaterial, FrameName, GlassName, SpacerName, InstallType, InstallName, LE_CL_V;
        String check_FrameType, check_SingleDoubleType, check_FrameMaterial, check_LE_CL_V, check_InstallType;
        String[][] Size;
        double Ug, g, τD65_SNA, Psi_g_fix, Psi_g_open, Uw, Uw_inst, dUinst;// dUinst는 열교가산치, Uw_inst는 유효열관류율(창호열관류율+열교가산치)
        double Uf_open, Uf_fix, Uf_btw, df_open, df_fix, df_btw;
        double Psi_InstallTop, Psi_InstallSide, Psi_InstallButtom;
        double Area, Width, Height, Ag_fix, Ag_open, Af_open, Af_fix, Af_btw, Lg_fix, Lg_open;
        String[][] Old; String[][] f_shgc; String[][] f_τ;

        public ConstructionCW()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(29, OnLoadListProc);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (CWName == null)
            {
                MessageBox.Show("창호 명칭을 입력하세요.");
            }
            else if (Type == null)
            {
                MessageBox.Show("창호 리모델링 유형을 선택하세요.");
            }
            else if (GlassName == null)
            {
                MessageBox.Show("유리를 선택하세요.");
            }
            else if (InstallName == null)
            {
                MessageBox.Show("설치열교를 선택하세요.");
            }
            else if (UwMehod == "계산")
            {
                if (FrameName == null)
                {
                    MessageBox.Show("프레임을 선택하세요.");
                }
                else if (SpacerName == null)
                {
                    MessageBox.Show("간봉을 선택하세요.");
                }
                else
                {
                    Save();
                }
            }
            else
            {
                Save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionCW f = (List_ConstructionCW)form;

            f.load_List();

            return true;
        }

        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,기존창호,Uw적용방법,직접간접,프레임유형,이중단창,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,LE_CL_V," +
                  "창호면적,창호너비,창호높이,고정유리면적,개폐유리면적,개폐프레임면적,고정프레임면적,중간프레임면적,고정유리둘레길이,개폐유리둘레길이," +
                  "유리열관류율,태양열취득률,빛투과율,고정유리선형열관류율,개폐유리선형열관류율," +
                  "개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께," +
                  "상부설치열관류율,측면설치열관류율,하부설치열관류율," +
                  "창호열관류율,설치열교가산치,창호유효열관류율",
                "'" + WinNum_textBox.Text + "','" + CWName + "','" + Type + "','" + OldWindow + "','" + UwMehod + "','" + DiIndi + "','" + FrameType + "','" + SingleDoubleType + "','" + FrameMaterial + "','" + FrameName + "','" + GlassName + "','" + SpacerName + "','" + InstallType + "','" + InstallName + "','" + LE_CL_V + "','" +
                Area.ToString() + "','" + Width.ToString() + "','" + Height.ToString() + "','" + Ag_fix.ToString() + "','" + Ag_open.ToString() + "','" + Af_open.ToString() + "','" + Af_fix.ToString() + "','" + Af_btw.ToString() + "','" + Lg_fix.ToString() + "','" + Lg_open.ToString() + "','" +
                Ug.ToString() + "','" + g.ToString() + "','" + τD65_SNA.ToString() + "','" + Psi_g_fix.ToString() + "','" + Psi_g_open.ToString() + "','" +
                Uf_open.ToString() + "','" + Uf_fix.ToString() + "','" + Uf_btw.ToString() + "','" + df_open.ToString() + "','" + df_fix.ToString() + "','" + df_btw.ToString() + "','" +
                Psi_InstallTop.ToString() + "','" + Psi_InstallSide.ToString() + "','" + Psi_InstallButtom.ToString() + "','" +
                Uw.ToString() + "','" + dUinst.ToString() + "','" + Uw_inst.ToString()
                + "'", "번호");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(29, OnLoadListProc);
        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            WinNum_textBox.Text = ID;
            if (Name_textBox.Text != "")
            {
                CWName = Name_textBox.Text + "_복사";
                Name_textBox.Text = CWName;
            }
        }

        public void CopyForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            WinNum_textBox.Text = ID;
            if (Name_textBox.Text != "")
            {
                CWName = Name_textBox.Text + "_복사";
                Name_textBox.Text = CWName;
            }

        }

    }
}
