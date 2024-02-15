using main.contentslist;
using main.subcontents.ConstructionCW;
using main.subcontents.HeatingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static main.DB;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class AHUSystem : Form
    {
        String Num, Name; String SelectZone_nonsplit;
        string[][] 프로젝트유형;
        public AHUSystem()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '공조시스템'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            //복합설비 콤보박스  
            Complex_comboBox.Items.Clear();
            Complex_comboBox.Items.Add("단일설비가동");
            Complex_comboBox.Items.Add("복합설비가동");
            Complex_comboBox.SelectedIndex = 0;

            Insulation_comboBox.Items.Clear();
            Insulation_comboBox.Items.Add("보통");

        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
        }
        #region 세이브
        private void Save_button_Click(object sender, EventArgs e)
        {
            Save();

        }
        private void Save()
        {

            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,프로젝트유형,명칭,존", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + Name + "','" + SelectZone_nonsplit + "'", "번호");


            this.DialogResult = DialogResult.OK;
            this.Hide();
            //Program.getMenuForm().DoLoadForm(39, OnLoadListProc);
        }

        public static bool OnLoadListProc(Form form)
        {
            //List_HeatingSystem f = (List_HeatingSystem)form;
            //f.load_List();
            return true;
        }

        private void reset()
        {

        }

        #endregion

        #region 로드
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            Num_textBox.Text = ID;
            Num = ID;

        }

        #endregion

        #region 리셋 
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        #endregion


    }
}
