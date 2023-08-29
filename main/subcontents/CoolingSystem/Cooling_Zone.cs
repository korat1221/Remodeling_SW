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

        List<CoolingZone> CZS = new List<CoolingZone>();
        string _SystemName;
        string _SystemNum;

        public Cooling_Zone(string Num, string Name)
        {
            InitializeComponent();
            _SystemNum = Num;
            _SystemName = Name;
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //new StackedHeaderDecorator(CoolingZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, dataGridView_RowHandle);


            //실행함
            List<string> CoolingZones = new List<string>();
            coolingzone(coolingzonelist());
            datagridviewShow();
        }

       
        //dwd_mth
        public string[][] coolingzonelist() //list 찾기
        {
            string[][] coolingzonename = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed",
           "번호,이름,a", //값이있는 열
           "난방_냉방='" + "냉방" + //조건1
           "' AND 비이용일_이용일 = '" + "이용일" + //조건2
             "'"); //마지막
            return coolingzonename;
        }
        public void coolingzone(string[][] czl) //냉방존 보여주기
        {
            for (int i = 0; i < czl.Length; i++)
            {
                CoolingZone CZ = new CoolingZone();
                CZ.Num = czl[i][0]; //존번호
                CZ.Name = czl[i][1]; //존이름
                CZ.Area = Convert.ToDouble(czl[i][2]);

                string[][] coolingzone = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed",
                  "Qcb_mth,theta_i,dwd_mth", //값이있는 열
                    "난방_냉방= '" + "냉방" + //조건1
                    "' AND 비이용일_이용일 = '" + "이용일" + //조건2
                    "' AND 번호 = '" + czl[i][0] +  //조건3
                    "'"); //마지막
                for(int mth = 0;mth<12; mth++)
                {
                    CZ.Qcb_mth[mth] = Convert.ToDouble(coolingzone[mth][0]);
                    CZ.theta_i[mth] = Convert.ToDouble(coolingzone[mth][0]);
                    CZ.dwd_mth[mth] = Convert.ToDouble(coolingzone[mth][0]);
                }
                CZS.Add(CZ);
            }
           
        }

        private bool dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 0)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            else return false;
        }
        public void datagridviewShow()
        {
            CoolingZone_dataGridView.Rows.Clear();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            CoolingZone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolingZone_dataGridView.Columns.Add(checkBoxColumn);

            CoolingZone_dataGridView.Columns.Add("A1", "존번호");
            CoolingZone_dataGridView.Columns.Add("A2", "존이름");
            CoolingZone_dataGridView.Columns.Add("A3", "번호");
            CoolingZone_dataGridView.Columns.Add("A4", "연간 정보.냉방에너지요구량.[kWh/m2·a]");
            CoolingZone_dataGridView.Columns.Add("A5", "연간 정보.실내 평균온도.[℃]");
            CoolingZone_dataGridView.Columns.Add("A6", "연간 정보.월 평균 이용일수.[d/mth]");
            CoolingZone_dataGridView.Columns.Add("A7", "순바닥면적.[m2]");
            CoolingZone_dataGridView.Columns.Add("A8", "최대냉방부하.[kW]");
            CoolingZone_dataGridView.Columns[0].FillWeight = 50;
            CoolingZone_dataGridView.Columns[1].FillWeight = 80;
            CoolingZone_dataGridView.Columns[2].FillWeight = 50;
            CoolingZone_dataGridView.Columns[3].FillWeight = 50;
            CoolingZone_dataGridView.Columns[7].FillWeight = 80;
            CoolingZone_dataGridView.Columns[8].FillWeight = 80;
          
            foreach (CoolingZone CZ in CZS) //모든존을 가지고 있음( 0, 3번만 별도 지정함 )
            {
                int nRow = CoolingZone_dataGridView.Rows.Add();
                CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = CZ.Num.ToString(); //존번호
                CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = CZ.Name.ToString(); //존이름
                CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = CZ.Qcb_a().ToString(); //
                CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = CZ.theta_i_ave().ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = CZ.dwd_mth_ave().ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = CZ.Area.ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = CZ.MaxLoad().ToString();
            }


            for (int k = 0; k < CoolingZone_dataGridView.Rows.Count; k++)
            {
                //냉방설비가 같은번호인것을 추출
                string[][] _zc = Program.DB.getValue_SameCheck(DB.type.ProjDB, "CoolingZone", "번호", "존번호  = '" + CoolingZone_dataGridView.Rows[k].Cells[1].Value + "'"); //한개씩 검토 진행함
                //같은번호가 있으면
                if (_zc.Length > 0)
                {
                    CoolingZone_dataGridView.Rows[k].Cells[3].Value = _zc[0][0].ToString();//번호넣기

                    if (_zc[0][0].ToString() == _SystemNum)
                    {
                        CoolingZone_dataGridView.Rows[k].Cells[0].Value = true;
                    }
                    else
                    {
                        CoolingZone_dataGridView.Rows[k].Cells[0].Value = false;
                    }
                }
                else
                {
                    CoolingZone_dataGridView.Rows[k].Cells[3].Value = null;
                    CoolingZone_dataGridView.Rows[k].Cells[0].Value = false;
                }
            }

        }

        // 데이터그리드 텍스트폼 지정
        private void CoolingZoneList_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for(int i =0; i < 5; i++)
            {
                int chk = 4+i;
                if (e.ColumnIndex == chk && e.Value != null)
                {
                   string num = Convert.ToString(e.Value);
                   double doubleNum;
                   if (double.TryParse(num, out doubleNum))
                   {
                      e.Value = string.Format("{0:0.##}", doubleNum);
                   }
                   else
                   {
                       e.Value = num;
                   }
                  
                }
            }
           
        }
        public void _sqlsave() //SQL에 저장하기
        {
            for (int k = 0; k < CoolingZone_dataGridView.Rows.Count; k++)
            {

                //냉방설비가 같은번호인것을 추출
                string[][] _zc = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "번호,존번호", "존번호  = '" + CoolingZone_dataGridView.Rows[k].Cells[1].Value + "'"); //한개씩 검토 진행함
                if (_zc.Length > 0 && _zc[0][0] == _SystemNum)
                {
                    Program.DB.deleteValue(DB.type.ProjDB, "CoolingZone", "존번호='" + CoolingZone_dataGridView.Rows[k].Cells[1].Value + "'");
                }
            }

            for (int k = 0; k < CoolingZone_dataGridView.Rows.Count; k++) //sql에 저장하기
            {
                string[][] _zc1 = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "번호,명칭,존번호,존이름", "존번호  = '" + CoolingZone_dataGridView.Rows[k].Cells[1].Value + "'");
                if (Convert.ToBoolean(CoolingZone_dataGridView.Rows[k].Cells[0].Value) == true)
                {
                    Program.DB.setValue(DB.type.ProjDB, "CoolingZone",
                      "번호,명칭,존번호,존이름",
                                "'" + 
                                _SystemNum + "','" +
                                _SystemName + "','" +
                                CoolingZone_dataGridView.Rows[k].Cells[1].Value + "','" +
                                CoolingZone_dataGridView.Rows[k].Cells[2].Value + "'", "존번호");
                }
                else if (Convert.ToBoolean(CoolingZone_dataGridView.Rows[k].Cells[0].Value) == false)
                {
                    if(_zc1.Length>0) 
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone",
                     "번호,명칭,존번호,존이름",
                               "'" +
                             _zc1[0][0] + "','" +
                             _zc1[0][1] +"','"  +
                             _zc1[0][2] + "','" +
                             _zc1[0][3] + "'", "존번호");
                    }
                    else
                    {
                        Program.DB.setValue(DB.type.ProjDB, "CoolingZone",
                     "번호,명칭,존번호,존이름",
                               "'" +
                              CoolingZone_dataGridView.Rows[k].Cells[3].Value + "','" +
                              "','" +
                              CoolingZone_dataGridView.Rows[k].Cells[1].Value + "','" +
                              CoolingZone_dataGridView.Rows[k].Cells[2].Value + "'", "존번호");
                    }
                }
            }
        }
    
       
        private void button1_Click(object sender, EventArgs e)
        {
            _sqlsave();
            string a = "OK";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }


    public class CoolingZone
    {
        public string Num, Name, SystemName;
        public double Area; //MaxLoad 없음
        private double _Qcb_a, _dwd_mth_ave, _theta_i_ave;
        public double[] Qcb_mth = new double[12], dwd_mth = new double[12], theta_i = new double[12];

        public double Qcb_a()
        {
            double j = 0;
            for (int i = 0; i < 12; i++)
            {
                j += Qcb_mth[i];
            }
            return j;
        }
        public double dwd_mth_ave()
        {
            double j = 0;
            for (int i = 0; i < 12; i++)
            {
                j += dwd_mth[i];
            }
            double k = j / 12;
            return k;
        }
        public double theta_i_ave()
        {
            double j = 0;
            for (int i = 0; i < 12; i++)
            {
                j += theta_i[i];
            }
            double k = j / 12;
            return k;
        }

        public double MaxLoad()
        {
            List<double> maxselect = new List<double>();
            double k = 0;
            for (int i = 0; i < 12; i++)
            {
                double j = Qcb_mth[i];
                maxselect.Add(j);
            }
            for (int i = 0; i < 12; i++)
            {
                if (maxselect.Max() == Qcb_mth[i])
                {
                    k = maxselect.Max() / dwd_mth[i] / 11;
                    break;
                }
            }
            return k;
        }

    } 
}
 




