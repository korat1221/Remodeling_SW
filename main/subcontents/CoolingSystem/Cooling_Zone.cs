using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents
{
    public partial class Cooling_Zone : Form
    {

        string SystemNum, SystemName;

        public Cooling_Zone(string[] coolingzone_conn)
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            new StackedHeaderDecorator(CoolingZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            SystemNum = coolingzone_conn[0];
            SystemName = coolingzone_conn[1];

            Reset();


            string[][] CZ_Check = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "존번호", "");
            if (CZ_Check.Length > 0)
            {
                CoolingZoneList(CoolingZoneNames());
            }
            else
            {
                ZoneList(ZoneNames());
            }



            int n = ZoneNames().Length;


            ZoneCount.Text = string.Format("{0} 개", n);

        }

        public string[][] ZoneNames() //?
        {
            string[][] _ZoneNames = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,순바닥면적,연이용일수", "냉난방유무 = '냉난방' or 냉난방유무 = '냉방'");
            return _ZoneNames;
        }

        public string[][] CoolingZoneNames() //냉방존 이름만 저장함
        {
            string[][] _CoolingZoneNames = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "존번호,번호,명칭,복합설비", "");
            return _CoolingZoneNames;
        }


        private void ZoneList(string[][] zonenames) //존번호, 존이름, 번호, 순바닥면적, 냉방시간, 연간이용일수, 에너지성능.최대냉방부하, 에너지성능.연간총에너지요구량
        {
            datagridviewShow();

            for (int i = 0; i < zonenames.Length; i++)
            {

                //존번호, 존이름, 번호, 에너지성능.최대냉방부하, 에너지성능.연간총에너지요구량
                string[][] ZoneGet0 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "번호,이름,Qb_a,Q_max", "번호='" + zonenames[i][0] + "' AND 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'"); //마지막

                //순바닥면적, 냉방시간, 연간이용일수
                string[][] ZoneGet1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,연이용일수", "존번호='" + zonenames[i][0] + "'");

                ZDataGridViewAdd(ZoneGet0, ZoneGet1);
            }
        }

        private void CoolingZoneList(string[][] coolingzonenames) //존번호, 존이름, 번호, 순바닥면적, 냉방시간, 연간이용일수, 에너지성능.최대냉방부하, 에너지성능.연간총에너지요구량
        {
            datagridviewShow();

            for (int i = 0; i < coolingzonenames.Length; i++)
            {

                //존번호, 존이름, 번호, 에너지성능.최대냉방부하, 에너지성능.연간총에너지요구량
                string[][] ZoneGet0 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "번호,이름,Qb_a,Q_max", "번호='" + coolingzonenames[i][0] + "' AND 비이용일_이용일 = '이용일' And 난방_냉방 = '냉방'"); //마지막

                //순바닥면적, 냉방시간, 연간이용일수
                string[][] ZoneGet1 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "순바닥면적,연이용일수", "존번호='" + coolingzonenames[i][0] + "'");
                string cznum = coolingzonenames[i][1].ToString();
                string czname = coolingzonenames[i][2].ToString();
                Boolean czmul = Convert.ToBoolean(coolingzonenames[i][3].ToString());

                CZDataGridViewAdd(ZoneGet0, ZoneGet1, cznum, czname, czmul);
            }
        }



        #region // 그리드 디자인

        private void ZDataGridViewAdd(string[][] _ZoneGet0, string[][] _ZoneGet1) //그리드 내용 구성
        {
            int nRow = CoolingZone_dataGridView.Rows.Add();

            CoolingZone_dataGridView.Rows[nRow].Cells[0].Value = false;                      //선택
            CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = _ZoneGet0[0][0].ToString(); //존번호
            CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = _ZoneGet0[0][1].ToString(); //존이름   
            CoolingZone_dataGridView.Rows[nRow].Cells[3].Value = null;                       //시스템번호
            CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = null;                       //시스템이름
            CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet1[0][0])); //순바닥면적
            CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet1[0][1])); //연이용일수
            CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet0[0][2])); //연간냉방에너지요구량
            CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet0[0][3]) / 1000); //최대냉방부하           
            CoolingZone_dataGridView.Rows[nRow].Cells[9].Value = false;
        }

        private void CZDataGridViewAdd(string[][] _ZoneGet0, string[][] _ZoneGet1, string _cznum, string _czname, Boolean _czmul) //그리드 내용 구성
        {
            int nRow = CoolingZone_dataGridView.Rows.Add();

            if (_cznum == SystemNum)
            {
                CoolingZone_dataGridView.Rows[nRow].Cells[0].Value = true;
            }
            else
            {
                CoolingZone_dataGridView.Rows[nRow].Cells[0].Value = false;
            }
            if (_ZoneGet0.Length > 0)
            {
                if (_ZoneGet1.Length > 0)
                {
                    CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = _ZoneGet0[0][0].ToString(); //존번호
                    CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = _ZoneGet0[0][0].ToString(); //존번호
                    CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = _ZoneGet0[0][1].ToString(); //존이름   
                    CoolingZone_dataGridView.Rows[nRow].Cells[3].Value = _cznum;      //시스템번호
                    CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = _czname;     //시스템이름
                    CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet1[0][0])); //순바닥면적
                    CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet1[0][1])); //연이용일수
                    CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet0[0][2])); //연간냉방에너지요구량
                    CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(_ZoneGet0[0][3]) / 1000); //최대냉방부하
                    CoolingZone_dataGridView.Rows[nRow].Cells[9].Value = _czmul;
                }
            }

        }


        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row) //그리드 디자인
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }



        public void datagridviewShow()
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolingZone_dataGridView.Columns.Add(checkBoxColumn);

            CoolingZone_dataGridView.Columns.Add("A1", "존.번호");
            CoolingZone_dataGridView.Columns.Add("A2", "존.이름");
            CoolingZone_dataGridView.Columns.Add("A3", "시스템.번호");
            CoolingZone_dataGridView.Columns.Add("A4", "시스템.이름");
            CoolingZone_dataGridView.Columns.Add("A5", "순바닥면적.[m2]");
            CoolingZone_dataGridView.Columns.Add("A6", "연이용일수.[d/년]");
            CoolingZone_dataGridView.Columns.Add("A7", "연간냉방에너지요구량.[kWh/m2·년]");
            CoolingZone_dataGridView.Columns.Add("A8", "최대냉방부하.[kW]");

            DataGridViewCheckBoxColumn MulticheckBoxColumn = new DataGridViewCheckBoxColumn();
            MulticheckBoxColumn.HeaderText = "복합설비";
            MulticheckBoxColumn.Name = "Mcheck";
            CoolingZone_dataGridView.Columns.Add(MulticheckBoxColumn);

            CoolingZone_dataGridView.Columns[0].FillWeight = 50;
            CoolingZone_dataGridView.Columns[1].FillWeight = 70;
            CoolingZone_dataGridView.Columns[2].FillWeight = 100;
            CoolingZone_dataGridView.Columns[3].FillWeight = 70;
            CoolingZone_dataGridView.Columns[4].FillWeight = 50;
            CoolingZone_dataGridView.Columns[7].FillWeight = 130;
            CoolingZone_dataGridView.Columns[8].FillWeight = 90;
            CoolingZone_dataGridView.Columns[9].FillWeight = 50;


        }
        #endregion

        private void Save_Button_Click(object sender, EventArgs e)
        {
            _sqlsave();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void _sqlsave() //SQL에 저장하기
        {
            List<string> Value = new List<string>();
            for (int i = 0; i < CoolingZone_dataGridView.Rows.Count; i++)
            {
                Value.Clear();
                for (int k = 0; k < CoolingZone_dataGridView.Columns.Count; k++)
                {
                    if (CoolingZone_dataGridView.Rows[i].Cells[k].Value == null)
                    {
                        Value.Add(null);
                    }
                    else
                    {
                        Value.Add(CoolingZone_dataGridView.Rows[i].Cells[k].Value.ToString());
                    }
                }

                if (Convert.ToBoolean(Value[0]) == true)
                {
                    if (Convert.ToBoolean(Value[9]) == true)
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름,복합설비", "'" + Value[3] + "','" + Value[4] + "','" + Value[1] + "','" + Value[2] + "','True'", "존번호");
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름,복합설비", "'" + SystemNum + "','" + SystemName + "','" + Value[1] + "','" + Value[2] + "','True'", "번호,존번호");

                    }
                    else
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름,복합설비", "'" + SystemNum + "','" + SystemName + "','" + Value[1] + "','" + Value[2] + "','" + Value[9] + "'", "존번호");
                    }

                }

                else
                {
                    if (Value[3] == SystemNum)
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름, 복합설비", "'" + null + "','" + null + "','" + Value[1] + "','" + Value[2] + "','" + Value[9] + "'", "존번호");
                    }
                    else
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름, 복합설비", "'" + Value[3] + "','" + Value[4] + "','" + Value[1] + "','" + Value[2] + "','" + Value[9] + "'", "존번호");
                    }

                }
            }
        }
        //냉방설비 기존 명칭을 그대로 돌려주는 매소드

        public void Reset()
        {
            CoolingZone_dataGridView.Rows.Clear();
            CoolingZone_dataGridView.Columns.Clear();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
