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


        int SelectRow;
        string DefaultUse;

        string 프로젝트유형;
        public string SelectPV;
        int PVModule_SelectRow;

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

            for (int i = 0; i < cellimage.Length; i++)
            {
                switch (cellimage[i][1].ToString())
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
            if (DefaultUse == "기본DB 적용")
            {
                new StackedHeaderDecorator(PVModule_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
                DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn();
                PVModule_dataGridView.Columns.Clear();

                checkBox.HeaderText = "선택";
                checkBox.Name = "check";
                PVModule_dataGridView.Columns.Add(checkBox);

                PVModule_dataGridView.Columns.Add("A1", "번호");
                PVModule_dataGridView.Columns.Add("A2", "DB유형");
                PVModule_dataGridView.Columns.Add("A3", "제품명");
                PVModule_dataGridView.Columns.Add("A4", "제조사");

                PVModule_dataGridView.Columns.Add("A5", "Cell Type");

                PVModule_dataGridView.Columns.Add("A6", "출력.[W]");
                PVModule_dataGridView.Columns.Add("A7", "크기.길이[m]");
                PVModule_dataGridView.Columns.Add("A8", "크기.높이[m]");
                PVModule_dataGridView.Columns.Add("A9", "Kpk.[kW/m2]");

                PVModule_dataGridView.Columns[0].Width = 40;

                PVModule_dataGridView.Columns[3].Width = 140;
                PVModule_dataGridView.Columns[5].Width = 110;

            }
            else if(DefaultUse == "장비일람표 DB")
            {
                new StackedHeaderDecorator(PVModule_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                PVModule_dataGridView.Columns.Clear();

                checkBoxColumn.HeaderText = "선택";
                checkBoxColumn.Name = "check";
                PVModule_dataGridView.Columns.Add(checkBoxColumn);

                PVModule_dataGridView.Columns.Add("A1", "번호");
                PVModule_dataGridView.Columns.Add("A2", "DB유형");
                PVModule_dataGridView.Columns.Add("A3", "명칭");
                PVModule_dataGridView.Columns.Add("A4", "Cell Type");
                PVModule_dataGridView.Columns.Add("A5", "출력.[W]");
                PVModule_dataGridView.Columns.Add("A6", "크기.길이[m]");
                PVModule_dataGridView.Columns.Add("A7", "크기.높이[m]");
                PVModule_dataGridView.Columns.Add("A8", "Kpk.[kW/m2]");

                PVModule_dataGridView.Columns[0].Width = 40;
            }
        }

        private void load_PV_table()
        {
            if (DefaultUse == "기본DB 적용")
            {
                string[][] UserModule = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,CELLTYPE,정격출력,길이,높이,Kpk", "");
                if (UserModule.Length > 0)
                {
                    for (int n = 0; n < UserModule.Length; n++)
                    {

                        PVModule_dataGridView.Rows.Add();
                        int nRow = PVModule_dataGridView.Rows.Count - 1;
                        PVModule_dataGridView.Rows[nRow].Cells[1].Value = UserModule[n][0];
                        PVModule_dataGridView.Rows[nRow].Cells[2].Value = UserModule[n][1];
                        PVModule_dataGridView.Rows[nRow].Cells[3].Value = UserModule[n][2];
                        PVModule_dataGridView.Rows[nRow].Cells[4].Value = UserModule[n][3];

                        DataGridViewComboBoxCell Cell = new DataGridViewComboBoxCell();
                        Cell.Items.AddRange(new string[] { "단결정", "다결정", "a_Si박막형", "화합물CIGS박막형", "화합물CdTe박막형" });
                        PVModule_dataGridView.Rows[nRow].Cells[5] = Cell;

                        PVModule_dataGridView.Rows[nRow].Cells[5].Value = UserModule[n][4];
                        PVModule_dataGridView.Rows[nRow].Cells[6].Value = UserModule[n][5];
                        PVModule_dataGridView.Rows[nRow].Cells[7].Value = UserModule[n][6];
                        PVModule_dataGridView.Rows[nRow].Cells[8].Value = UserModule[n][7];
                        PVModule_dataGridView.Rows[nRow].Cells[9].Value = UserModule[n][8];
                    }
                }

                string[][] PVModule = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "번호,DB유형,제품명,제조사,CELLTYPE,정격출력,길이,높이,Kpk", "");
                if (PVModule.Length > 0)
                {
                    for (int n = 0; n < PVModule.Length; n++)
                    {
                        PVModule_dataGridView.Rows.Add();
                        int nRow = PVModule_dataGridView.Rows.Count - 1;
                        PVModule_dataGridView.Rows[nRow].Cells[1].Value = PVModule[n][0];
                        PVModule_dataGridView.Rows[nRow].Cells[2].Value = PVModule[n][1];
                        PVModule_dataGridView.Rows[nRow].Cells[3].Value = PVModule[n][2];
                        PVModule_dataGridView.Rows[nRow].Cells[4].Value = PVModule[n][3];
                        PVModule_dataGridView.Rows[nRow].Cells[5].Value = PVModule[n][4];
                        PVModule_dataGridView.Rows[nRow].Cells[6].Value = PVModule[n][5];
                        PVModule_dataGridView.Rows[nRow].Cells[7].Value = PVModule[n][6];
                        PVModule_dataGridView.Rows[nRow].Cells[8].Value = PVModule[n][7];
                        PVModule_dataGridView.Rows[nRow].Cells[9].Value = PVModule[n][8];
                    }
                }
            }
            else if(DefaultUse == "장비일람표 DB")
            {
                string[][] UserModule = Program.DB.getValue(DB.type.ProjDB, "User_PV", "번호,DB유형,명칭,CELLTYPE,정격출력,길이,높이,Kpk", "");
                if (UserModule.Length > 0)
                {
                    for (int n = 0; n < UserModule.Length; n++)
                    {
                        PVModule_dataGridView.Rows.Add();
                        int nRow = PVModule_dataGridView.Rows.Count - 1;
                        PVModule_dataGridView.Rows[nRow].Cells[1].Value = UserModule[n][0];
                        PVModule_dataGridView.Rows[nRow].Cells[2].Value = UserModule[n][1];
                        PVModule_dataGridView.Rows[nRow].Cells[3].Value = UserModule[n][2];
                        PVModule_dataGridView.Rows[nRow].Cells[4].Value = UserModule[n][3];
                        PVModule_dataGridView.Rows[nRow].Cells[5].Value = UserModule[n][4];
                        PVModule_dataGridView.Rows[nRow].Cells[6].Value = UserModule[n][5];
                        PVModule_dataGridView.Rows[nRow].Cells[7].Value = UserModule[n][6];
                        PVModule_dataGridView.Rows[nRow].Cells[8].Value = UserModule[n][7];
                    }
                }
            }
        }

        //SetValue
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (DefaultUse == "기본DB 적용")
            {
                string UserNum = Program.UTIL.CreateNum("User_PVModule", "번호", "UPV_0");
                Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,프로젝트유형,DB유형",
                       "'" + UserNum + "','" + 프로젝트유형 + "','사용자'", "번호");
                PV_table();
                load_PV_table();
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (DefaultUse == "기본DB 적용")
            {
                string DeletNum = PVModule_dataGridView.Rows[PVModule_SelectRow].Cells[1].Value.ToString();
                Program.DB.deleteValue(DB.type.ProjDB, "User_PVModule", "번호 ='" + DeletNum + "'");
                PVModule_dataGridView.Rows.Remove(PVModule_dataGridView.Rows[PVModule_SelectRow]);
                PV_table();
                load_PV_table();
            }
        }

        void Save()
        {
            if (DefaultUse == "기본DB 적용")
            {
                for (int i = 0; i < PVModule_dataGridView.RowCount; i++)
                {
                    if (PVModule_dataGridView.Rows[i].Cells[2].Value.ToString() == "사용자")
                    {
                        string[] value = new string[9];

                        for (int k = 1; k < 10; k++)
                        {

                            if (PVModule_dataGridView.Rows[i].Cells[k].Value == null || PVModule_dataGridView.Rows[i].Cells[k].Value == "")
                            {
                                MessageBox.Show("빈칸을 채워주세요");
                                return;
                            }
                            else
                            {
                                value[k - 1] = PVModule_dataGridView.Rows[i].Cells[k].Value.ToString();
                            }
                        }
                        Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,프로젝트유형,DB유형,제품명,제조사,CELLTYPE,정격출력,길이,높이,Kpk",
                            "'" + value[0] + "','" + 프로젝트유형 + "','" + value[1] + "','" + value[2] + "','" + value[3] + "','" + value[4] + "','" + value[5] + "','" + value[6] + "','" + value[7] + "','" + value[8] + "'", "번호");
                    }

                    if (SelectCheckBox())
                    {
                        SelectPV = PVModule_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();
                    }
                    else return;
                }
            }
            else
            {
                SelectPV = PVModule_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();
            }
               


        }
        private bool SelectCheckBox()
        {
            foreach (DataGridViewRow row in PVModule_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow = row.Index;
                }
            }
            return true;
        }

        private void PVModule_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVModule_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                PVModule_SelectRow = e.RowIndex;
            }

        }

        private void Save_button_Click_1(object sender, EventArgs e)
        {
            Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PVModule_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(DefaultUse == "기본DB 적용")
            {
                if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    double a = 0, b = 0;
                    string aa = null;
                    string bb = null;
                    if (PVModule_dataGridView.Rows[e.RowIndex].Cells[7].Value != null) aa = PVModule_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                    if (PVModule_dataGridView.Rows[e.RowIndex].Cells[8].Value != null) bb = PVModule_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();

                    if (double.TryParse(aa, out a) && double.TryParse(bb, out b) && aa != null && bb != null)
                    {
                        double c = Convert.ToDouble(PVModule_dataGridView.Rows[e.RowIndex].Cells[6].Value) / (a * b * 1000);
                        PVModule_dataGridView.Rows[e.RowIndex].Cells[9].Value = string.Format("{0:F3}", c);
                    }

                }
            }
            
        }
    }
}
