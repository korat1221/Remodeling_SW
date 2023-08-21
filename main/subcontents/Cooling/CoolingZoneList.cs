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
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents
{
    public partial class CoolingZoneList : Form
    {

        List<CoolingZone> CZS = new List<CoolingZone>();
        string _systemName;

        public CoolingZoneList(string NameText)
        {
            InitializeComponent();
            _systemName = NameText;
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '냉방시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            new StackedHeaderDecorator(CoolingZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, dataGridView_RowHandle);
            CZL();
            datagridviewShow();
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
        //dwd_mth

        public void CZL() //전체 냉방존 이름 선택 및 저장
        {
            // CoolingZoneList_dataGridView.DataSource = DataTableClearEventArgs.Empty;

            string[][] zone_names = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed",
            "번호,이름", //값이있는 열
            "난방_냉방='" + "냉방" + //조건1
            "' AND 비이용일_이용일 = '" + "이용일" + //조건2
            "' AND 월 =  '" + "7월" + //조건3
            "' AND Qcb_a > '" + "0" + //조건4
            "'"); //마지막



            for (int i = 0; i < zone_names.Length; i++)
            {
                CoolingZone CZ = new CoolingZone();
                CZ.Num = Convert.ToString(zone_names[i][0]);
                CZ.Name = Convert.ToString(zone_names[i][1]);
                CZ.SystemName = _systemName;

                string[][] _CZL = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed",
                    "Qcb_mth,theta_i,dwd_mth,a", //값이있는 열
                    "난방_냉방= '" + "냉방" + //조건1
                    "' AND 비이용일_이용일 = '" + "이용일" + //조건2
                    "' AND 번호 = '" + CZ.Num +  //조건3
                    "'"); //마지막

                for (int j = 0; j < 12; j++)
                {
                    CZ.Qcb_mth[j] = Convert.ToDouble(_CZL[j][0]);
                    CZ.theta_i[j] = Convert.ToDouble(_CZL[j][1]);
                    CZ.dwd_mth[j] = Convert.ToDouble(_CZL[j][2]);
                }
                CZ.Area = Convert.ToDouble(_CZL[0][3]);
                CZS.Add(CZ);
            }
        }

        //쿨링존에 해당 시스템이 있는경우-->선택을 true로 하고 저장시 해당 시스템을 삭제하고 다시 추가함
        //쿨링존에 해당 시스템이 없는경우-->선택을 false로하고 CoolingZone테이블에 추가함

        public void datagridviewShow()
        {
            CoolingZone_dataGridView.Rows.Clear();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            CoolingZone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolingZone_dataGridView.Columns.Add(checkBoxColumn);

            CoolingZone_dataGridView.Columns.Add("A1", "번호");
            CoolingZone_dataGridView.Columns.Add("A2", "이름");
            CoolingZone_dataGridView.Columns.Add("A3", "시스템명칭");
            CoolingZone_dataGridView.Columns.Add("A4", "연간 정보.냉방에너지요구량.[kWh/m2·a]");
            CoolingZone_dataGridView.Columns.Add("A5", "연간 정보.실내 평균온도.[℃]");
            CoolingZone_dataGridView.Columns.Add("A6", "연간 정보.월 평균 이용일수.[d/mth]");
            CoolingZone_dataGridView.Columns.Add("A7", "순바닥면적.[m2]");
            CoolingZone_dataGridView.Columns.Add("A8", "최대냉방부하.[kW]");
            CoolingZone_dataGridView.Columns[0].FillWeight = 50;
            CoolingZone_dataGridView.Columns[1].FillWeight = 80;
            CoolingZone_dataGridView.Columns[2].FillWeight = 50;
            CoolingZone_dataGridView.Columns[7].FillWeight = 80;
            CoolingZone_dataGridView.Columns[8].FillWeight = 80;
            //DataTable CoolingZone_db = new DataTable();
            //CoolingZone_db.Columns.Add("선택", typeof(bool));
            //CoolingZone_db.Columns.Add("번호", typeof(string));
            //CoolingZone_db.Columns.Add("이름", typeof(string));
            //CoolingZone_db.Columns.Add("연간 냉방에너지" + Environment.NewLine + "요구량[kWh/m2·a]", typeof(double));
            //CoolingZone_db.Columns.Add("연간 실내" + Environment.NewLine + "평균온도[℃]", typeof(double));
            //CoolingZone_db.Columns.Add("연간 월 평균" + Environment.NewLine + "이용일수[d/mth]", typeof(double));
            //CoolingZone_db.Columns.Add("바닥면적" + Environment.NewLine + "[m2]", typeof(double));
            //CoolingZone_db.Columns.Add("최대냉방부하" + Environment.NewLine + "[kW]", typeof(double));
            foreach (CoolingZone CZ in CZS) //모든존을 가지고 있음
            {
                int nRow = CoolingZone_dataGridView.Rows.Add();
                CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = CZ.Num.ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = CZ.Name.ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = CZ.Qcb_a().ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = CZ.theta_i_ave().ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = CZ.dwd_mth_ave().ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = CZ.Area.ToString();
                CoolingZone_dataGridView.Rows[nRow].Cells[8].Value = CZ.MaxLoad().ToString();
            }


            for (int k = 0; k < CoolingZone_dataGridView.Rows.Count; k++)
            {
                string[][] _zc = Program.DB.getValue_SameCheck(DB.type.ProjDB, "CoolingZone", "번호,시스템명칭", "번호  = '" + CoolingZone_dataGridView.Rows[k].Cells[1].Value + "'");
                if (_zc.Length > 0)
                {
                    CoolingZone_dataGridView.Rows[k].Cells[3].Value = _zc[0][1].ToString();
                    if (_zc[0][1].ToString() == _systemName)
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

            {   //   foreach (CoolingZone CZ in CZS) //모든존을 가지고 있음
                //   {
                //       for (int i = 0; i < _zc.Length; i++)
                //       {
                //            if (CZ.Num == _zc[i][0].ToString() && CZ.SystemName == _zc[i][1].ToString()) //해당존만 보여주는 방법
                //            {
                //                CoolingZone_db.Rows.Add(true, CZ.Num, CZ.Name, CZ.Qcb_a(), CZ.theta_i_ave(), CZ.dwd_mth_ave(), CZ.Area, CZ.MaxLoad());
                //            }
                //            else { }
                //       }

                //            CoolingZone_db.Rows.Add(false, CZ.Num, CZ.Name, CZ.Qcb_a(), CZ.theta_i_ave(), CZ.dwd_mth_ave(), CZ.Area, CZ.MaxLoad());
                //   }

                //    foreach (CoolingZone CZ in CZS)
                //    {
                //        for (int i = 0; i < _zc.Length; i++)
                //        {
                //           if (CZ.Name == _zc[i][0])
                //            {
                //                CZS.Remove(CZ);
                //            }
                //        }

                //    }

                //}



                //CoolingZone_dataGridView.DataSource = CoolingZone_db;

                //데이터그리트열 폼 지정
            }

        }

        // 데이터그리드 텍스트폼 지정
        private void CoolingZoneList_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                if (e.Value != null)
                {
                    string num = e.Value.ToString();
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
        class CoolingZone
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





        } //쿨링존
        //데이터 저장하기
        private void button1_Click(object sender, EventArgs e)
        {
            _sqlsave();
            string a = "OK";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void _sqlsave() //SQL에 저장하기
        {
            string[][] check = Program.DB.getValue(DB.type.ProjDB, "CoolingZone", "시스템명칭", ""); //전체가져오기
            try
            {
                for (int i = 0; i < check.Length; i++)
                {
                    Program.DB.deleteValue(DB.type.ProjDB, "CoolingZone", "시스템명칭='" + _systemName + "'"); //이름이 같은게 있으면 먼저 지우기
                }
            }
            catch { }

            foreach (DataGridViewRow row in CoolingZone_dataGridView.Rows) //sql에 저장하기
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    foreach (CoolingZone CZ in CZS)
                    {
                        if (CZ.Num == row.Cells[1].Value)
                        {
                            for (int mth = 0; mth <= 11; mth++)
                            {
                                string MTH = (mth + 1).ToString() + "월";

                                Program.DB.setValue(DB.type.ProjDB, "CoolingZone",
                                "번호,시스템명칭,이름,면적,월,냉방에너지요구량,이용일수,실내온도,최대냉방부하,연간냉방에너지요구량",
                                "'" + CZ.Num + "','" +
                                CZ.SystemName + "','" +
                                CZ.Name + "','" +
                                CZ.Area.ToString() + "','" +
                                MTH + "','" +
                                CZ.Qcb_mth[mth].ToString() + "','" +
                                CZ.dwd_mth[mth].ToString() + "','" +
                                CZ.theta_i[mth].ToString() + "','" +
                                CZ.MaxLoad().ToString() + "','" +
                                CZ.Qcb_a().ToString() + "'", "번호,월,시스템명칭");
                            }

                        }
                    }
                }
            }
        }
    }
}





