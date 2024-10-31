using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.RESystem_PV
{
    public partial class PV_ModuleDB : Form
    {

        double Count_PVModuleDB;
        int SelectRow;
        public String[] Select_PVModule = new string[10];
        String UserDB_Name, UserDB_Manufacture, UserDB_year, UserDB_celltype;
        double UserDB_width, UserDB_height, UserDB_output, UserDB_Kpk;
        string DefaultUse;

        string 프로젝트유형;

        public PV_ModuleDB(string defaultUse)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            DefaultUse = defaultUse;
                       
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] cellimage = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지,종류", "대분류 = 'Cell'");
            
            for(int i = 0; i < cellimage.Length; i++)
            {
                switch(cellimage[i][1].ToString())
                {
                    case "단결정":
                        pictureBox2.Load(Program.gPath + cellimage[i][0]);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    case "다결정":
                        pictureBox3.Load(Program.gPath + cellimage[i][0]);
                        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    case "a_Si박막형":
                        pictureBox4.Load(Program.gPath + cellimage[i][0]);
                        pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    case "CIGS박막형":
                        pictureBox5.Load(Program.gPath + cellimage[i][0]);
                        pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    case "CdTe박막형":
                        pictureBox6.Load(Program.gPath + cellimage[i][0]);
                        pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
                        break;
                    default:
                        break;
                }
            }
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            프로젝트유형 = value[0][0].ToString();

            PV_table();
            load_PV_table();
        }

        private void PV_table()
        {
            new StackedHeaderDecorator(PVModule_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PVModule_dataGridView.Columns.Add(checkBoxColumn);
            
            PVModule_dataGridView.Columns.Add("A1", "번호");
            PVModule_dataGridView.Columns.Add("A2", "DB유형");
            PVModule_dataGridView.Columns.Add("A3", "제품명");
            PVModule_dataGridView.Columns.Add("A4", "제조사");
            PVModule_dataGridView.Columns.Add("A5", "Cell Type");
            PVModule_dataGridView.Columns.Add("A6", "Kpk.[kW/m2]");
            PVModule_dataGridView.Columns.Add("A7", "크기.길이[m]");
            PVModule_dataGridView.Columns.Add("A8", "크기.높이[m]");
            PVModule_dataGridView.Columns.Add("A9", "출력.[kW]");

            PVModule_dataGridView.Columns[0].Width = 40;
           
            PVModule_dataGridView.Columns[3].Width = 140;
            PVModule_dataGridView.Columns[5].Width = 110;

        }

        private void load_PV_table()
        {
            if (DefaultUse == "기본DB 적용")
            {

                string[][] UserModule = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,CELLTYPE,Kpk,길이,높이,정격출력", "");
                if (UserModule.Length > 0)
                {
                    for (int n = 0; n < UserModule.Length; n++)
                    {
                        PVModule_dataGridView.Rows.Add();
                        PVModule_dataGridView.Rows[n].Cells[1].Value = UserModule[n][0];
                        PVModule_dataGridView.Rows[n].Cells[2].Value = UserModule[n][1];
                        PVModule_dataGridView.Rows[n].Cells[3].Value = UserModule[n][2];
                        PVModule_dataGridView.Rows[n].Cells[4].Value = UserModule[n][3];
                        PVModule_dataGridView.Rows[n].Cells[5].Value = UserModule[n][4];
                        PVModule_dataGridView.Rows[n].Cells[6].Value = UserModule[n][5];
                        PVModule_dataGridView.Rows[n].Cells[7].Value = UserModule[n][6];
                        PVModule_dataGridView.Rows[n].Cells[8].Value = UserModule[n][7];
                        PVModule_dataGridView.Rows[n].Cells[9].Value = UserModule[n][8];
                    }
                }

                string[][] PVModule = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "번호,DB유형,제품명,제조사,CELLTYPE,Kpk,길이,높이,정격출력", "");
                if (PVModule.Length > 0)
                {
                    for (int n = 0; n < PVModule.Length; n++)
                    {
                        PVModule_dataGridView.Rows.Add();
                        PVModule_dataGridView.Rows[n].Cells[1].Value = PVModule[n][0];
                        PVModule_dataGridView.Rows[n].Cells[2].Value = PVModule[n][1];
                        PVModule_dataGridView.Rows[n].Cells[3].Value = PVModule[n][2];
                        PVModule_dataGridView.Rows[n].Cells[4].Value = PVModule[n][3];
                        PVModule_dataGridView.Rows[n].Cells[5].Value = PVModule[n][4];
                        PVModule_dataGridView.Rows[n].Cells[6].Value = PVModule[n][5];
                        PVModule_dataGridView.Rows[n].Cells[7].Value = PVModule[n][6];
                        PVModule_dataGridView.Rows[n].Cells[8].Value = PVModule[n][7];
                        PVModule_dataGridView.Rows[n].Cells[9].Value = PVModule[n][8];
                    }
                }
            }
            else
            {
                ////유저값 수정 필요함
                //string[][] User_PVModule = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력", "");
                //if (User_PVModule.Length > 0)
                //{
                //    for (int n = 0; n < User_PVModule.Length; n++)
                //    {
                //        PVModule_dataGridView.Rows.Add();
                //        PVModule_dataGridView.Rows[n].Cells[1].Value = User_PVModule[n][0];
                //        PVModule_dataGridView.Rows[n].Cells[2].Value = User_PVModule[n][1];
                //        PVModule_dataGridView.Rows[n].Cells[3].Value = User_PVModule[n][2];
                //        PVModule_dataGridView.Rows[n].Cells[4].Value = User_PVModule[n][3];
                //        PVModule_dataGridView.Rows[n].Cells[5].Value = User_PVModule[n][4];
                //        PVModule_dataGridView.Rows[n].Cells[6].Value = User_PVModule[n][5];
                //        PVModule_dataGridView.Rows[n].Cells[7].Value = User_PVModule[n][6];
                //        PVModule_dataGridView.Rows[n].Cells[8].Value = User_PVModule[n][7];
                //        PVModule_dataGridView.Rows[n].Cells[9].Value = User_PVModule[n][8];
                //        PVModule_dataGridView.Rows[n].Cells[10].Value = User_PVModule[n][9];
                //    }
                //}

            }
        }

        //SetValue
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            for(int k = 0;k<PVModule_dataGridView.Rows.Count ; k++)
            {
                if (PVModule_dataGridView.Rows[k].Cells[2].Value == "사용자")
                {
                    string[] value = new string[8];
                    for(int i = 1;i<10 ; i++)
                    {
                        value[i] = PVModule_dataGridView.Rows[k].Cells[i].Value.ToString();
                    }
                    
                    Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,프로젝트유형,DB유형,제품명,제조사,CELLTYPE,Kpk,길이,높이,정격출력",
                   "'" + value[1] + "','" + 프로젝트유형 + "','" + value[2] + "','" + value[3] + "','" + value[4] + "','" + value[5] + "','" + value[6] + "'," +
                   "'" + value[7] + "','" + value[8] + "','" + value[9] + "'", "번호");
                }
            }

            PVModule_dataGridView.Rows.Clear();
            PVModule_dataGridView.Columns.Clear();
            PV_table();
           
            PVModule_dataGridView.Rows.Add();
            string UserNum = Program.UTIL.CreateNum("User_PVModule", "번호", "UPV_0");
            PVModule_dataGridView.Rows[0].Cells[1].Value = UserNum;     
            load_PV_table();                      
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = PVModule_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (PVModule_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(PVModule_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = PVModule_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_PVModule", "번호 ='" + Delete_Num + "'");
                        load_PV_table();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        void Save()
        {
            //string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            //if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_year != null && UserDB_celltype != null && UserDB_width != 0 && UserDB_height != 0 && UserDB_output != 0)
            //{
            //    Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,프로젝트유형,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력",
            //        "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_year + "','" + UserDB_celltype + "','" + UserDB_Kpk.ToString() + "','" + UserDB_width.ToString() + "','" + UserDB_height.ToString() + "','" + UserDB_output.ToString() + "'", "번호");
            //    load_table_PVModuleDB();
            //}
            //else
            //{
            //    MessageBox.Show("모든 값을 입력해주세요.");
            //}
        }



        private void Save_button_Click(object sender, EventArgs e)
        {
            // 번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력
            DataGridViewRow row = PVModule_dataGridView.Rows[SelectRow];
            
            Select_PVModule[0] = row.Cells[1].Value.ToString(); //번호
            Select_PVModule[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_PVModule[2] = row.Cells[3].Value.ToString(); //제품명
            Select_PVModule[3] = row.Cells[4].Value.ToString(); //제조사
            Select_PVModule[4] = row.Cells[5].Value.ToString(); //제작년도
            Select_PVModule[5] = row.Cells[6].Value.ToString(); //CELLTYPE
            Select_PVModule[6] = row.Cells[7].Value.ToString(); //Kpk

            if (DefaultUse != "기본DB 적용")
            {
                Select_PVModule[7] = row.Cells[8].Value.ToString(); //가로길이
                Select_PVModule[8] = row.Cells[9].Value.ToString(); //세로길이
                Select_PVModule[9] = row.Cells[10].Value.ToString(); //정격출력
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
