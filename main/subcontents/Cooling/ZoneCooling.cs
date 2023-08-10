using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents
{
    public partial class ZoneCooling : Form
    {

        public ZoneCooling()
        {
            InitializeComponent();
            datagridview();
        }

        //리스트 보여주는 매서드
        DataGridView DGV = new DataGridView();
        
        public void datagridview()
        {
            
            DataTable ZoneCooling_datatable = new DataTable();
            ZoneCooling_datatable.Columns.Add("선택", typeof(bool));
            ZoneCooling_datatable.Columns.Add("존이름", typeof(string));
            ZoneCooling_datatable.Columns.Add("연간냉방에너지요구량" + Environment.NewLine + "[kWh/m2·a]", typeof(double));
            ZoneCooling_datatable.Columns.Add("연간실내평균온도" + Environment.NewLine + "[℃]", typeof(double));
            //Zpme
            //oneCoolingInfo.RowHeadersVisible = false;

            string[][] test = Program.DB.getValue(DB.type.ProjDB, "Zone", "zoneNum");//[행][열], "연습필드4", "연습필드3 = '4'");





            for (int i = 0; i < test.Length; i++) // --> 모든 존 선택하여 리스트로 보여줌 (기존 선택된 존은 제외되도록 조치 필요)
            {
                //zonecooling zc = new zonecooling(); //존정보에서 제공하면 필요없음
                //zc.ZoneName = test[i][0];
                //zc.CoolingSystemName = FormConnect.CoolingSystemNameText.Text;
                //zc.CoolingSystemNumber = FormConnect.CoolingSystemNumText.Text;
                //zc.CoolingSystemType = FormConnect.CoolingSystemTypeSelectCombobox.Text;
                //zonecoolings.Add(zc);
            }
        }

        private void ZoneCoolingList_datagridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        //public void zonecoolingdb() // 냉방설비유형에 따른 zonecoolings 작성
        //{


    }
    //    foreach (var zone in zonecoolings)
    //    {
    //        string[][] zone_need_wd = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed", "Qcb_we_mth,dwd_mth,theta_i", "번호='" + zone.ZoneName + "' AND 난방_냉방 = '" + "냉방" + "'  AND  비이용일_이용일 =  '" + "이용일" + "'");
    //        string[][] zone_need_we = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed", "Qcb_wd_mth", "번호='" + zone.ZoneName + "' AND 난방_냉방 = '" + "냉방" + "' AND 비이용일_이용일 =  '" + "비이용일" + "'");
    //        for (int j = 0; j < 12; j++)
    //        {
    //            zone.QC_nd_zt_j[j] = Convert.ToDouble(zone_need_we[j][0]) + Convert.ToDouble(zone_need_wd[j][0]);
    //            zone.θi_c[j] = Convert.ToDouble(zone_need_wd[j][2]);
    //            zone.dwd[j] = Convert.ToDouble(zone_need_wd[j][1]);
    //        }
    //        zone.SumQC_nd_zt_j(zone.QC_nd_zt_j);
    //        zone.Averθi_c(zone.θi_c);
    //    }

    //}

    //public void CoolingZoneTable()
    //{
    //    Program.DB.initTable(DB.type.ProjDB, "CoolingZone"); //데이터테이블 작성

    //    DataTable datatable_Zonecooling = new DataTable();
    //    datatable_Zonecooling.Columns.Add("선택", typeof(bool));
    //    datatable_Zonecooling.Columns.Add("존이름", typeof(string));
    //    datatable_Zonecooling.Columns.Add("연간냉방에너지요구량" + Environment.NewLine + "[kWh/m2·a]", typeof(double));
    //    datatable_Zonecooling.Columns.Add("연간실내평균온도" + Environment.NewLine + "[℃]", typeof(double));
    //    ZoneCoolingInfo.RowHeadersVisible = false;

    //    foreach (var k in zonecoolings)
    //    {
    //        datatable_Zonecooling.Rows.Add(false, k.ZoneName, k.SumQC_nd_zt_j(k.QC_nd_zt_j), k.Averθi_c(k.θi_c));
    //    }
    //    ZoneCoolingInfo.DataSource = datatable_Zonecooling;

    //}

    //private void SelectCheckBox()
    //{
    //    foreach (DataGridViewRow row in ZoneCoolingInfo.Rows)
    //    {
    //        if (Convert.ToBoolean(row.Cells[0].Value))
    //        {
    //            row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
    //            SelectRow.Add(row.Index);
    //        }
    //    }
    //}

    ////저장하는 함수
    //private void button1_Click(object sender, EventArgs e) //선택한 존 리스트 DB에 저장함
    //{
    //    SelectRow.Clear();
    //    SelectCheckBox();

    //    FormConnect.ZoneListName.Text = null;

    //    for (int j = 0; j < SelectRow.Count; j++)
    //    {
    //        if (j != (SelectRow.Count - 1))
    //        { FormConnect.ZoneListName.Text += Convert.ToString(ZoneCoolingInfo.Rows[j].Cells[1].Value) + ", "; }
    //        else
    //        {
    //            FormConnect.ZoneListName.Text += Convert.ToString(ZoneCoolingInfo.Rows[j].Cells[1].Value);
    //        }
    //    }
    //    this.DialogResult = DialogResult.OK;
    //    this.Close();
    //}
}





