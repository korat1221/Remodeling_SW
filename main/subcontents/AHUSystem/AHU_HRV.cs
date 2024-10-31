using main.contents;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.AHUSystem
{
    public partial class AHU_HRV : Form
    {
        int SelectRow;
        String DefaultUse;
        public string SelectSystem;
        public AHU_HRV(string AHUOptions, String SelectSystem)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            if (AHUOptions == "공조기")
            {
                load_table_AHU();
                Title_label.Text = AHUOptions;
            }
            else
            {
                load_table_HRV();
                Title_label.Text = "열회수기";
            }
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (SelectSystem != null)
            {
                Load_SaveValue(SelectSystem);
            }
        }
        private void load_table_AHU()
        {

            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.None);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HRV_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HRV_dataGridView.Columns.Add(checkBoxColumn);

            HRV_dataGridView.Columns.Add("A1", "번호");
            HRV_dataGridView.Columns.Add("A2", "명칭");
            HRV_dataGridView.Columns.Add("A3", "설치");
            HRV_dataGridView.Columns.Add("A4", "공조방식");
            HRV_dataGridView.Columns.Add("A5", "열회수.유형");
            HRV_dataGridView.Columns.Add("A6", "열회수.온도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A7", "열회수.온도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A8", "열회수.유효전열교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A9", "열회수.유효전열교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A10", "열회수.습도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A11", "열회수.습도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A12", "냉각코일.출력.[kW]");
            HRV_dataGridView.Columns.Add("A13", "냉각코일.입구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A14", "냉각코일.입구온도.[℃_WB]");
            HRV_dataGridView.Columns.Add("A15", "냉각코일.출구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A16", "냉각코일.출구온도.[℃_WB]");
            HRV_dataGridView.Columns.Add("A17", "난방코일.출력.[kW]");
            HRV_dataGridView.Columns.Add("A18", "난방코일.입구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A19", "난방코일.출구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A20", "가습기.유형");
            HRV_dataGridView.Columns.Add("A21", "가습기.습도수준");
            HRV_dataGridView.Columns.Add("A22", "가습기.용량.[kg/h]");
            HRV_dataGridView.Columns.Add("A23", "송풍기.풍량.급기.[CMH]");
            HRV_dataGridView.Columns.Add("A24", "송풍기.풍량.배기.[CMH]");
            HRV_dataGridView.Columns.Add("A25", "송풍기.정압.급기.[Pa]");
            HRV_dataGridView.Columns.Add("A26", "송풍기.정압.배기.[Pa]");
            HRV_dataGridView.Columns.Add("A27", "송풍기.팬동력.급기.[kW]");
            HRV_dataGridView.Columns.Add("A28", "송풍기.팬동력.배기.[kW]");
            HRV_dataGridView.Columns.Add("A29", "송풍기.모터제어");
            HRV_dataGridView.Columns[0].Width = 40;
            HRV_dataGridView.Columns[1].Width = 60;
            HRV_dataGridView.Columns[2].Width = 60;
            HRV_dataGridView.Columns[3].Width = 60;
            HRV_dataGridView.Columns[4].Width = 80;
            HRV_dataGridView.Columns[5].Width = 100;
            HRV_dataGridView.Columns[6].Width = 40;
            HRV_dataGridView.Columns[7].Width = 40;
            HRV_dataGridView.Columns[8].Width = 40;
            HRV_dataGridView.Columns[9].Width = 40;
            HRV_dataGridView.Columns[10].Width = 40;
            HRV_dataGridView.Columns[11].Width = 40;
            HRV_dataGridView.Columns[12].Width = 40;
            HRV_dataGridView.Columns[13].Width = 50;
            HRV_dataGridView.Columns[14].Width = 50;
            HRV_dataGridView.Columns[15].Width = 50;
            HRV_dataGridView.Columns[16].Width = 50;
            HRV_dataGridView.Columns[17].Width = 40;
            HRV_dataGridView.Columns[18].Width = 50;
            HRV_dataGridView.Columns[19].Width = 50;
            HRV_dataGridView.Columns[20].Width = 100;
            HRV_dataGridView.Columns[21].Width = 80;
            HRV_dataGridView.Columns[22].Width = 40;
            HRV_dataGridView.Columns[23].Width = 40;
            HRV_dataGridView.Columns[24].Width = 40;
            HRV_dataGridView.Columns[25].Width = 40;
            HRV_dataGridView.Columns[26].Width = 40;
            HRV_dataGridView.Columns[27].Width = 40;
            HRV_dataGridView.Columns[28].Width = 40;
            HRV_dataGridView.Columns[29].Width = 100;


            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "번호,명칭,설치유형,공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = HRV_dataGridView.Rows.Add();
                    for (int i = 0; i < 29; i++)
                    { HRV_dataGridView.Rows[nRow].Cells[i + 1].Value = Value[n][i]; }

                }
            }

        }
        private void load_table_HRV()
        {

            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HRV_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HRV_dataGridView.Columns.Add(checkBoxColumn);

            HRV_dataGridView.Columns.Add("A1", "번호");
            HRV_dataGridView.Columns.Add("A2", "명칭");
            HRV_dataGridView.Columns.Add("A3", "열회수.유형");
            HRV_dataGridView.Columns.Add("A4", "열회수.온도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A5", "열회수.온도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A6", "열회수.습도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A7", "열회수.습도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A8", "팬.풍량.[CMH]");
            HRV_dataGridView.Columns.Add("A9", "팬.정압.[Pa]");
            HRV_dataGridView.Columns.Add("A10", "팬.모터제어");
            HRV_dataGridView.Columns.Add("A11", "소비전력.[W]");


            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "번호,명칭,열회수유형, 온도교환효율_냉방, 온도교환효율_난방, 습도교환효율_냉방, 습도교환효율_난방, 팬풍량, 팬정압, 모터제어, 팬동력");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    HRV_dataGridView.Rows.Add();
                    int nRow = HRV_dataGridView.Rows.Count - 1;
                    HRV_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    HRV_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    HRV_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    HRV_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    HRV_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    HRV_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    HRV_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    HRV_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    HRV_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    HRV_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    HRV_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                }
            }
        }


        private void HRV_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HRV_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = HRV_dataGridView.Rows[SelectRow];

            SelectSystem = row.Cells[1].Value.ToString(); //번호
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Load_SaveValue(string SelectSystem)
        {
            for (int n = 0; n < HRV_dataGridView.Rows.Count; n++)
            {
                if (HRV_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectSystem.ToString())
                {
                    HRV_dataGridView.Rows[n].Cells[0].Value = true;
                }
            }
        }

    }

}
