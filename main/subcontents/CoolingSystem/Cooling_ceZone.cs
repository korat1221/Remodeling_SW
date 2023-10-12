using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.CoolingSystem
{
    public partial class Cooling_ceZone : Form
    {
        String SystemNum, ceType;
        List<String> NameList = new List<string>();

        public Cooling_ceZone(string _SystemNum, List<string> zonenames, string CEType)
        {
            InitializeComponent();
            SystemNum = _SystemNum;
            ceType = CEType;
            ceType_textBox.Text = ceType;


            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            try
            {
                if (zonenames.Count > 0)
                {
                    NameList = zonenames;
                }
            }
            catch { }

            load_table_DB();
            //Load_SaveValue();
        }

        private void load_table_DB()
        {
            new StackedHeaderDecorator(ceZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            ceZone_dataGridView.Columns.Clear();
            ceZone_dataGridView.Columns.Add("A0", "번호");
            ceZone_dataGridView.Columns.Add("A1", "존 번호");
            ceZone_dataGridView.Columns.Add("A2", "존 명칭");
            ceZone_dataGridView.Columns.Add("A3", "층");
            ceZone_dataGridView.Columns.Add("A4", "용도프로필");
            ceZone_dataGridView.Columns.Add("A5", "대수");
            ceZone_dataGridView.Columns.Add("A6", "");
            ceZone_dataGridView.Columns.Add("A7", ceType + "종류");
            ceZone_dataGridView.Columns[0].Width = 40;
            ceZone_dataGridView.Columns[6].Width = 30;



            for (int n = 0; n < NameList.Count; n++)
            {
                String[][] ZoneValve = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필", "존번호 = '" + NameList[n].ToString() + "'");
                //Program.DB.querySQL(DB.type.ProjDB, "존번호,존이름,용도프로필 FROM Heating_ce_Form  AS a INNER JOIN  ZoneGeneral_Form AS b ON a.존번호 = b.존번호 where a.난방시스템 = '" + SystemNum + "'");


                ceZone_dataGridView.Rows.Add();
                String[][] 층 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + ZoneValve[0][0] + "'");
                ceZone_dataGridView.Rows[n].Cells[0].Value = n + 1; //번호
                ceZone_dataGridView.Rows[n].Cells[1].Value = ZoneValve[0][0]; //존번호
                ceZone_dataGridView.Rows[n].Cells[2].Value = ZoneValve[0][1]; //존이름
                ceZone_dataGridView.Rows[n].Cells[3].Value = 층[0][0]; //층
                ceZone_dataGridView.Rows[n].Cells[4].Value = ZoneValve[0][2]; //용도프로필
                DataGridViewButtonCell ButtonCell = new DataGridViewButtonCell();
                ceZone_dataGridView.Rows[n].Cells[6] = ButtonCell;//적용 
                ButtonCell.Value = "+";
            }

        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (ceZone_dataGridView.Rows[row].Cells[6].GetType() == typeof(DataGridViewButtonCell))
            {
                if (column == 5 && cell.Value == null)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 243);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
            }
            else
            {
                if (column == 7 && cell.Value == null)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 243);
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
        }
        private void ceZone_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6)
                {
                    int sum = 0;
                    for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++) //아래체계 만들기
                    {
                        int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_"); //해당인덱스 위치정보를 반환함
                        if (index > 0)
                        {
                            String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

                            if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                            {
                                sum += 1;
                            }
                        }
                    }
                    if (sum > 0) //이미 입력된게 있을 경우 
                    {
                        if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
                        {
                            for (int i = 0; i < sum; i++)
                            {
                                ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
                            }
                        }
                        else { return; }
                    }

                    int typeNum = Convert.ToInt16(ceZone_dataGridView.Rows[e.RowIndex].Cells[5].Value);

                    for (int k = (typeNum - 1); k > -1; k--)
                    {
                        ceZone_dataGridView.Rows.Add();
                        int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
                        ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

                        DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
                        try
                        {
                            String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
                            for (int j = 0; j < 일람표.Length; j++)
                            {
                                일람표comboBox.Items.Add(일람표[j][0]);
                            }
                        }
                        catch { }
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;

                        DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
                        ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
                        ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

                    }

                }
                else if (e.ColumnIndex == 7)
                {

                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            String[][] Size = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
            if (Size.Length > 0)
            {
                Program.DB.deleteValue(DB.type.ProjDB, "Heating_ce_Form", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
            }

            int sum = 1;
            for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++)
            {
                int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_");
                if (index > 0)
                {
                    String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);
                    String substring2 = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(index + 1, ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Length - index - 1);
                    String 존번호 = null;
                    for (int k = 0; k < ceZone_dataGridView.Rows.Count; k++)
                    {
                        if (ceZone_dataGridView.Rows[k].Cells[0].Value.ToString() == substring)
                        {
                            존번호 = ceZone_dataGridView.Rows[k].Cells[1].Value.ToString();
                        }
                    }

                    try
                    {
                        string[][] 공급설비일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호", "명칭 = '" + ceZone_dataGridView.Rows[i].Cells[7].Value + "'");
                        Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비",
                         "'" + 존번호 + "','"
                         + SystemNum + "','"
                         + ceType + "','"
                         + 공급설비일람표[0][0] + "_" + substring2 + "'", "");
                    }
                    catch { }

                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            NameList.Clear();
        }

        private void Load_SaveValue()
        {
            reset();
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");

                for (int n = 0; n < Value.Length; n++)
                {
                    int ZoneRow = 0;
                    ceZone_dataGridView.Rows.Add();
                    int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
                    for (int a = 0; a < ceZone_dataGridView.Rows.Count; a++)
                    {
                        if (ceZone_dataGridView.Rows[a].Cells[1].Value != null && ceZone_dataGridView.Rows[a].Cells[1].Value.ToString() == Value[n][0].ToString())
                        {
                            ZoneRow = a;
                        }
                    }
                    String[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "공급설비", "존번호 = '" + ceZone_dataGridView.Rows[ZoneRow].Cells[1].Value.ToString() + "' And  냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
                    ceZone_dataGridView.Rows[ZoneRow].Cells[5].Value = Value2.Length;
                    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[ZoneRow].Cells[0].Value + "_" + Value[n][3].Substring(Value[n][3].IndexOf("_") + 1, 1);

                    DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
                    try
                    {
                        String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
                        for (int j = 0; j < 일람표.Length; j++)
                        {
                            일람표comboBox.Items.Add(일람표[j][0]);
                        }
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;
                        일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "번호 = '" + Value[n][3].Substring(0, Value[n][3].IndexOf("_")) + "'");
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7].Value = 일람표[0][0];
                    }
                    catch { }

                    DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
                    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
                    ceZone_dataGridView.Rows.Insert(ZoneRow + 1, AddRow);
                }
            }
            catch { }
        }
    }
}
