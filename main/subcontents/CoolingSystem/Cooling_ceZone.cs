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
        ArrayList SelectZone_split = new ArrayList();
        public Cooling_ceZone(String SystemNum, String SelectZone_nonsplit, String CEType) //공조기, 바닥매립형기기 등
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Program.UTIL.IgnoreGridError(this);
            this.SystemNum = SystemNum;
            ceType = CEType;
            ceType_textBox.Text = ceType;

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            if (SelectZone_nonsplit != null)
            {
                string[] token = SelectZone_nonsplit.Split('+');
                SelectZone_split.Clear();
                foreach (var item in token)
                {
                    SelectZone_split.Add(item.ToString());
                }
            }
            load_table_DB();
            Load_SaveValue();
        }

        void load_table_DB()
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


            for (int n = 0; n < SelectZone_split.Count; n++)
            {
                String[][] ZoneValve = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필", "존번호 = '" + SelectZone_split[n].ToString() + "'");
                //Program.DB.querySQL(DB.type.ProjDB, "존번호,존이름,용도프로필 FROM Heating_ce_Form  AS a INNER JOIN  ZoneGeneral_Form AS b ON a.존번호 = b.존번호 where a.난방시스템 = '" + SystemNum + "'");
                if (ZoneValve.Length > 0)
                {
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
                    for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++)
                    {
                        int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_");
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

                    int 대수 = Convert.ToInt16(Program.UTIL.dataGridView_doubleComa(ceZone_dataGridView, e.RowIndex, 5, 0));
                    for (int k = (대수 - 1); k > -1; k--)
                    {
                        ceZone_dataGridView.Rows.Add();
                        int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
                        ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

                        DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();

                        //ceType가 공조기인 경우 VAV유닛, CAV유닛, 파워팬유닛
                        // And(공급설비종류 = 'VAV유닛' OR 공급설비종류 = 'CAV유닛' OR 공급설비종류 = '파워팬유닛')");

                        if (ceType == "공조기")
                        {
                            String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "난방냉방 != '난방' AND (종류 = 'VAV유닛' OR 종류 = 'CAV유닛' OR 종류 = '파워팬유닛')");
                            if (일람표.Length > 0)
                            {
                                for (int j = 0; j < 일람표.Length; j++)
                                {
                                    일람표comboBox.Items.Add(일람표[j][0]);
                                }
                            }
                        }
                        else
                        {
                            String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
                            if (일람표.Length > 0)
                            {
                                for (int j = 0; j < 일람표.Length; j++)
                                {
                                    일람표comboBox.Items.Add(일람표[j][0]);
                                }
                            }

                        }
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;

                        DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
                        ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
                        ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if(ceType == "공조기")
            {
                String[][] Size = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + SystemNum + "' And (공급설비종류 = 'VAV유닛' OR  공급설비종류 = 'CAV유닛' OR  공급설비종류 = '파워팬유닛')");
                if (Size.Length > 0)
                {
                    Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + SystemNum + "' And (공급설비종류 = 'VAV유닛' OR  공급설비종류 = 'CAV유닛' OR  공급설비종류 = '파워팬유닛')");
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

                        string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                        string[][] 공급설비일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,종류", "명칭 = '" + ceZone_dataGridView.Rows[i].Cells[7].Value + "'");
                        if (공급설비일람표.Length > 0)
                        {
                            Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비종류,공급설비",
                             "'" + 존번호 + "','" + 프로젝트유형[0][0] + "','"
                             + SystemNum + "','"
                             + 공급설비일람표[0][1] + "','"
                             + 공급설비일람표[0][0] + "_" + substring2 + "'", "");
                        }

                    }
                }
            }
            else
            {
                String[][] Size = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
                if (Size.Length > 0)
                {
                    Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
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

                        string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                        string[][] 공급설비일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호", "명칭 = '" + ceZone_dataGridView.Rows[i].Cells[7].Value + "'");
                        if (공급설비일람표.Length > 0)
                        {
                            Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비종류,공급설비",
                             "'" + 존번호 + "','" + 프로젝트유형[0][0] + "','"
                             + SystemNum + "','"
                             + ceType + "','"
                             + 공급설비일람표[0][0] + "_" + substring2 + "'", "");
                        }

                    }
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectZone_split.Clear();
        }

        private void Load_SaveValue()
        {
            reset();
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비", "난방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
            if (Value.Length > 0)
            {
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
                    String[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "공급설비", "존번호 = '" + ceZone_dataGridView.Rows[ZoneRow].Cells[1].Value.ToString() + "' And 냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
                    ceZone_dataGridView.Rows[ZoneRow].Cells[5].Value = Value2.Length;
                    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[ZoneRow].Cells[0].Value + "_" + Value[n][3].Substring(Value[n][3].IndexOf("_") + 1, 1);

                    DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
                    String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
                    if (일람표.Length > 0)
                    {
                        for (int j = 0; j < 일람표.Length; j++)
                        {
                            일람표comboBox.Items.Add(일람표[j][0]);
                        }
                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;
                        일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "번호 = '" + Value[n][3].Substring(0, Value[n][3].IndexOf("_")) + "'");
                        if (일람표.Length > 0)
                        {
                            ceZone_dataGridView.Rows[AddRowNum].Cells[7].Value = 일람표[0][0];
                        }
                    }

                    DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
                    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
                    ceZone_dataGridView.Rows.Insert(ZoneRow + 1, AddRow);
                }
            }
        }
    }

    //public partial class Cooling_ceZone : Form
    //{
    //    String SystemNum, ceType;
    //    List<String> Select_split = new List<string>();
    //    string CE_TYPE;

    //    public Cooling_ceZone(string _SystemNum, string Select_nonsplit, string CEType) //냉방시스템번호, AHU리스트합친거,공조기
    //    {
    //        InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
    //        this.SystemNum = _SystemNum;
    //        ceType = CEType;
    //        ceType_textBox.Text = ceType;

    //        if (ceType == "공조기")
    //        {
    //            CE_TYPE = "Ahu";
    //        }
    //        else CE_TYPE = "Zone";

    //        if (Select_nonsplit != null && Select_nonsplit != "") 
    //        {
    //            Split(Select_nonsplit, Select_split);
    //        }

    //        Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
    //        Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

    //        table();

    //        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,공급설비종류,공급설비", "냉방시스템= '" + SystemNum + "'");

    //        if (Value.Length > 0)
    //        {
    //            Load_SaveValue(CE_TYPE);
    //        }
    //        else load_table_DB(CE_TYPE);
    //    }

    //    private void Split(string nonSplit, List<string> type)
    //    {
    //        type.Clear();

    //        if (nonSplit != null)
    //        {
    //            string[] token = nonSplit.Split('+');
    //            foreach (string item in token)
    //            {
    //                string _item = item.Trim();
    //                type.Add(_item);
    //            }
    //        }
    //    }

    //    private void table()
    //    {
    //        new StackedHeaderDecorator(ceZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
    //        ceZone_dataGridView.Columns.Add("A0", "번호");
    //        ceZone_dataGridView.Columns.Add("A1", "공조기 번호");
    //        ceZone_dataGridView.Columns.Add("A2", "공조방식");
    //        ceZone_dataGridView.Columns.Add("A3", "존 번호");
    //        ceZone_dataGridView.Columns.Add("A4", "존 명칭");
    //        ceZone_dataGridView.Columns.Add("A5", "층");
    //        ceZone_dataGridView.Columns.Add("A6", "용도프로필");
    //        ceZone_dataGridView.Columns.Add("A7", "대수");
    //        ceZone_dataGridView.Columns.Add("A8", "");
    //        ceZone_dataGridView.Columns.Add("A9", ceType + "종류");
    //        ceZone_dataGridView.Columns[0].Width = 40;
    //        ceZone_dataGridView.Columns[8].Width = 30;
    //    }

    //    private void load_table_DB(string _TYPE)
    //    {
    //        ceZone_dataGridView.Columns.Clear();
    //        ceZone_dataGridView.Rows.Clear();

    //        if (_TYPE == "Ahu")
    //        {
    //            for (int n = 0; n < Select_split.Count; n++)
    //            {
    //                string[][] ZoneValve = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,냉난방유무", "선택열회수기 = '" + Select_split[n] + "'");
    //                for (int k = 0; k < ZoneValve.Length; k++)
    //                {
    //                    if (ZoneValve[k][3] == "냉난방" || ZoneValve[k][3] == "냉방")
    //                    {
    //                        int h = ceZone_dataGridView.Rows.Add();
    //                        String[][] 층 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + ZoneValve[k][0] + "'");
    //                        ceZone_dataGridView.Rows[h].Cells[0].Value = h + 1; //번호
    //                        ceZone_dataGridView.Rows[h].Cells[1].Value = Select_split[n]; //공조기번호

    //                        String[][] 공조방식 = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "공조방식", "번호 = '" + Select_split[n] + "'");
    //                        ceZone_dataGridView.Rows[h].Cells[2].Value = 공조방식[0][0]; //공조방식
    //                        ceZone_dataGridView.Rows[h].Cells[3].Value = ZoneValve[k][0]; //존번호
    //                        ceZone_dataGridView.Rows[h].Cells[4].Value = ZoneValve[k][1]; //존이름
    //                        ceZone_dataGridView.Rows[h].Cells[5].Value = 층[0][0]; //층
    //                        ceZone_dataGridView.Rows[h].Cells[6].Value = ZoneValve[k][2]; //용도프로필
    //                        DataGridViewButtonCell ButtonCell = new DataGridViewButtonCell();
    //                        ceZone_dataGridView.Rows[h].Cells[8] = ButtonCell;//적용 
    //                        ButtonCell.Value = "+";
    //                    }
    //                }
    //            }
    //        }
    //        else  //zone인 경우
    //        {
    //            for (int n = 0; n < Select_split.Count; n++)
    //            {
    //                String[][] ZoneValve = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,냉난방시간,환기방식,용도프로필", "존번호 = '" + Select_split[n].ToString() + "'");
    //                //Program.DB.querySQL(DB.type.ProjDB, "존번호,존이름,용도프로필 FROM Heating_ce_Form  AS a INNER JOIN  ZoneGeneral_Form AS b ON a.존번호 = b.존번호 where a.난방시스템 = '" + SystemNum + "'");
    //                ceZone_dataGridView.Rows.Add();
    //                String[][] 층 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + ZoneValve[0][0] + "'");
    //                ceZone_dataGridView.Rows[n].Cells[0].Value = n + 1; //번호
    //                ceZone_dataGridView.Rows[n].Cells[1].Value = ZoneValve[0][0]; //존번호
    //                ceZone_dataGridView.Rows[n].Cells[2].Value = ZoneValve[0][1]; //존이름
    //                ceZone_dataGridView.Rows[n].Cells[3].Value = ZoneValve[0][2]; //냉난방시간
    //                ceZone_dataGridView.Rows[n].Cells[4].Value = ZoneValve[0][3]; //환기방식
    //                ceZone_dataGridView.Rows[n].Cells[5].Value = 층[0][0]; //층
    //                ceZone_dataGridView.Rows[n].Cells[6].Value = ZoneValve[0][4]; //용도프로필
    //                DataGridViewButtonCell ButtonCell = new DataGridViewButtonCell();
    //                ceZone_dataGridView.Rows[n].Cells[8] = ButtonCell;//적용 
    //                ButtonCell.Value = "+";
    //            }
    //        }
    //    }

    //    private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
    //    {
    //        if (ceZone_dataGridView.Rows[row].Cells[8].GetType() == typeof(DataGridViewButtonCell))
    //        {
    //            if (column == 7 && cell.Value == null)
    //            {
    //                cell.Style.BackColor = Color.FromArgb(255, 255, 243);
    //                cell.Style.ForeColor = Color.Black;
    //                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 243);
    //                cell.Style.SelectionForeColor = Color.Black;
    //                return true;
    //            }
    //            else
    //            {
    //                cell.Style.BackColor = SystemColors.InactiveBorder;
    //                cell.Style.ForeColor = Color.Black;
    //                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
    //                cell.Style.SelectionForeColor = Color.Black;
    //                return true;
    //            }
    //        }
    //        else
    //        {
    //            if (column == 9 && cell.Value == null)
    //            {

    //                cell.Style.BackColor = Color.FromArgb(255, 255, 243);
    //                cell.Style.ForeColor = Color.Black;
    //                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 243);
    //                cell.Style.SelectionForeColor = Color.Black;
    //                return true;
    //            }
    //            else
    //            {
    //                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
    //                cell.Style.ForeColor = Color.Black;
    //                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
    //                cell.Style.SelectionForeColor = Color.Black;
    //                return true;
    //            }
    //        }
    //    }


    //    private void 하부항목작성(string CeType)
    //    {
    //        if (CeType == "Zone")
    //        {
    //            int sum = 0;
    //            for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++) //아래체계 만들기
    //            {
    //                int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_"); //해당인덱스 위치정보를 반환함
    //                if (index > 0)
    //                {
    //                    String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

    //                    if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
    //                    {
    //                        sum += 1;
    //                    }
    //                }
    //            }
    //            if (sum > 0) //이미 입력된게 있을 경우 
    //            {
    //                if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
    //                {
    //                    for (int i = 0; i < sum; i++)
    //                    {
    //                        ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
    //                    }
    //                }
    //                else { return; }
    //            }

    //            int 대수 = Convert.ToInt16(Program.UTIL.dataGridView_doubleComa(ceZone_dataGridView, e.RowIndex, 7, 0));

    //            for (int k = (대수 - 1); k > -1; k--)
    //            {
    //                ceZone_dataGridView.Rows.Add();
    //                int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

    //                DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();

    //                string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
    //                if (일람표.Length > 0)
    //                {
    //                    for (int j = 0; j < 일람표.Length; j++)
    //                    {
    //                        일람표comboBox.Items.Add(일람표[j][0]);
    //                    }
    //                }
    //                ceZone_dataGridView.Rows[AddRowNum].Cells[9] = 일람표comboBox;

    //                DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

    //            }
    //        }
    //        else if (e.ColumnIndex == 8 && CE_TYPE == "Ahu")
    //        {
    //            int sum = 0;
    //            for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++) //아래체계 만들기
    //            {
    //                int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_"); //해당인덱스 위치정보를 반환함
    //                if (index > 0)
    //                {
    //                    String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

    //                    if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
    //                    {
    //                        sum += 1;
    //                    }
    //                }
    //            }
    //            if (sum > 0) //이미 입력된게 있을 경우 
    //            {
    //                if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
    //                {
    //                    for (int i = 0; i < sum; i++)
    //                    {
    //                        ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
    //                    }
    //                }
    //                else { return; }
    //            }

    //            int 대수 = Convert.ToInt16(ceZone_dataGridView.Rows[e.RowIndex].Cells[7].Value);

    //            for (int k = (대수 - 1); k > -1; k--)
    //            {
    //                ceZone_dataGridView.Rows.Add();
    //                int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

    //                DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();

    //                if (ceZone_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "변풍량")
    //                {
    //                    string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "(종류 = 'VAV유닛' OR 종류 = '팬파워유닛') AND 난방냉방 !='난방'");
    //                    if (일람표.Length > 0)
    //                    {
    //                        for (int j = 0; j < 일람표.Length; j++)
    //                        {
    //                            일람표comboBox.Items.Add(일람표[j][0]);
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = 'CAV유닛' AND 난방냉방 !='난방'");
    //                    if (일람표.Length > 0)
    //                    {
    //                        for (int j = 0; j < 일람표.Length; j++)
    //                        {
    //                            일람표comboBox.Items.Add(일람표[j][0]);
    //                        }
    //                    }
    //                }
    //                ceZone_dataGridView.Rows[AddRowNum].Cells[9] = 일람표comboBox;

    //                DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

    //            }
    //        }
    //    }

    //    private void ceZone_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    //    {
    //        if (e.RowIndex >= 0)
    //        {
    //            if (e.ColumnIndex == 8 && CE_TYPE == "Zone")
    //            {
    //                int sum = 0;
    //                for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++) //아래체계 만들기
    //                {
    //                    int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_"); //해당인덱스 위치정보를 반환함
    //                    if (index > 0)
    //                    {
    //                        String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

    //                        if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
    //                        {
    //                            sum += 1;
    //                        }
    //                    }
    //                }
    //                if (sum > 0) //이미 입력된게 있을 경우 
    //                {
    //                    if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
    //                    {
    //                        for (int i = 0; i < sum; i++)
    //                        {
    //                            ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
    //                        }
    //                    }
    //                    else { return; }
    //                }

    //                int 대수 = Convert.ToInt16(Program.UTIL.dataGridView_doubleComa(ceZone_dataGridView, e.RowIndex, 7, 0));

    //                for (int k = (대수 - 1); k > -1; k--)
    //                {
    //                    ceZone_dataGridView.Rows.Add();
    //                    int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

    //                    DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();

    //                    string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
    //                    if (일람표.Length > 0)
    //                    {
    //                        for (int j = 0; j < 일람표.Length; j++)
    //                        {
    //                            일람표comboBox.Items.Add(일람표[j][0]);
    //                        }
    //                    }
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[9] = 일람표comboBox;

    //                    DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                    ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

    //                }
    //            }
    //            else if (e.ColumnIndex == 8 && CE_TYPE == "Ahu")
    //            {
    //                int sum = 0;
    //                for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++) //아래체계 만들기
    //                {
    //                    int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_"); //해당인덱스 위치정보를 반환함
    //                    if (index > 0)
    //                    {
    //                        String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);

    //                        if (substring == ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
    //                        {
    //                            sum += 1;
    //                        }
    //                    }
    //                }
    //                if (sum > 0) //이미 입력된게 있을 경우 
    //                {
    //                    if ((MessageBox.Show("입력하신 " + ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + "의 공급설비 정보를 리셋하시겠습니까?", ceZone_dataGridView.Rows[e.RowIndex].Cells[1].Value + " 공급설비 정보 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes))
    //                    {
    //                        for (int i = 0; i < sum; i++)
    //                        {
    //                            ceZone_dataGridView.Rows.RemoveAt(e.RowIndex + 1);
    //                        }
    //                    }
    //                    else { return; }
    //                }

    //                int 대수 = Convert.ToInt16(ceZone_dataGridView.Rows[e.RowIndex].Cells[7].Value);

    //                for (int k = (대수 - 1); k > -1; k--)
    //                {
    //                    ceZone_dataGridView.Rows.Add();
    //                    int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[e.RowIndex].Cells[0].Value + "_" + (k + 1).ToString();

    //                    DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();

    //                    if (ceZone_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "변풍량")
    //                    {
    //                        string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "(종류 = 'VAV유닛' OR 종류 = '팬파워유닛') AND 난방냉방 !='난방'");
    //                        if (일람표.Length > 0)
    //                        {
    //                            for (int j = 0; j < 일람표.Length; j++)
    //                            {
    //                                일람표comboBox.Items.Add(일람표[j][0]);
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = 'CAV유닛' AND 난방냉방 !='난방'");
    //                        if (일람표.Length > 0)
    //                        {
    //                            for (int j = 0; j < 일람표.Length; j++)
    //                            {
    //                                일람표comboBox.Items.Add(일람표[j][0]);
    //                            }
    //                        }
    //                    }
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[9] = 일람표comboBox;

    //                    DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                    ceZone_dataGridView.Rows.Insert(e.RowIndex + 1, AddRow);

    //                }
    //            }
    //        }
    //    }

    //    private void Save_button_Click(object sender, EventArgs e)
    //    {
    //        string[][] Size = new string[0][];
    //        if(CE_TYPE == "Ahu")
    //        {
    //            Size = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + SystemNum + "' And (공급설비종류 = 'VAV유닛' OR 공급설비종류 = 'CAV유닛' OR 공급설비종류 = '파워팬유닛')");
    //        }    
    //        else
    //        {
    //             Size = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
    //        }

    //        if (Size.Length > 0)
    //        {
    //            if (CE_TYPE == "Ahu")
    //            {
    //                Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + SystemNum + "' And (공급설비종류 = 'VAV유닛' OR 공급설비종류 = 'CAV유닛' OR 공급설비종류 = '파워팬유닛')");
    //            }
    //            else Program.DB.deleteValue(DB.type.ProjDB, "Cooling_ce_Form", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
    //        }

    //        int sum = 1;
    //        for (int i = 0; i < ceZone_dataGridView.Rows.Count; i++)
    //        {
    //            int index = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().IndexOf("_");
    //            if (index > 0)
    //            {
    //                String substring = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(0, index);
    //                String substring2 = ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Substring(index + 1, ceZone_dataGridView.Rows[i].Cells[0].Value.ToString().Length - index - 1);
    //                String 존번호 = null;
    //                for (int k = 0; k < ceZone_dataGridView.Rows.Count; k++)
    //                {
    //                    if (ceZone_dataGridView.Rows[k].Cells[0].Value.ToString() == substring)
    //                    {
    //                       존번호 = ceZone_dataGridView.Rows[k].Cells[3].Value.ToString();
    //                    }
    //                }

    //                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");

    //                if(CE_TYPE == "Zone")
    //                {
    //                    string[][] 공급설비일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호", "명칭 = '" + ceZone_dataGridView.Rows[i].Cells[9].Value + "'");
    //                    if (공급설비일람표.Length > 0)
    //                    {
    //                        Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비종류,공급설비",
    //                         "'" + 존번호 + "','" + 프로젝트유형[0][0] + "','"
    //                         + SystemNum + "','"
    //                         + ceType + "','"
    //                         + 공급설비일람표[0][0] + "_" + substring2 + "'", ""); //공급설비에 설치대수를 포함함
    //                    }
    //                }
    //                else if(CE_TYPE == "Ahu")
    //                {
    //                    string[][] 공급설비일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,종류", "명칭 = '" + ceZone_dataGridView.Rows[i].Cells[9].Value + "'");
    //                    if (공급설비일람표.Length > 0)
    //                    {
    //                        Program.DB.setValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,프로젝트유형,냉방시스템,공급설비종류,공급설비",
    //                         "'" + 존번호 + "','" + 프로젝트유형[0][0] + "','"
    //                         + SystemNum + "','"
    //                         + 공급설비일람표[0][1] + "','"
    //                         + 공급설비일람표[0][0] + "_" + substring2 + "'", "");
    //                    }
    //                }     
    //            }
    //        }
    //        
    //        this.DialogResult = DialogResult.OK;
    //        this.Close();
    //    }
    //    private void reset()
    //    {            
    //        Select_split.Clear();
    //    }

    //    private void Load_SaveValue(string ceType)
    //    {
    //        reset();
    //        if (CE_TYPE == "Ahu") //공조기인경우
    //        {   
    //           String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비", "냉방시스템 = '" + SystemNum + "' And (공급설비종류 = 'VAV유닛' OR 공급설비종류 = 'CAV유닛' OR 공급설비종류 = '파워팬유닛')");
    //            if (Value.Length > 0)
    //            {
    //                for (int k = 0; k < Value.Length; k++)
    //                {
    //                    string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,용도프로필,냉난방유무,선택열회수기", "존번호 = '" + Value[k][0] + "'");
    //                    if (ZoneValue.Length>0)
    //                    {
    //                        int h = ceZone_dataGridView.Rows.Add();
    //                        String[][] 층 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + Value[k][0] + "'");
    //                        ceZone_dataGridView.Rows[h].Cells[0].Value = h + 1; //번호
    //                        ceZone_dataGridView.Rows[h].Cells[1].Value = ZoneValue[k][3]; //공조기번호

    //                        String[][] 공조방식 = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "공조방식", "번호 = '" + ZoneValue[k][3] + "'"); //변풍량
    //                        ceZone_dataGridView.Rows[h].Cells[2].Value = 공조방식[0][0]; //공조방식
    //                        ceZone_dataGridView.Rows[h].Cells[3].Value = Value[k][0]; //존번호
    //                        ceZone_dataGridView.Rows[h].Cells[4].Value = ZoneValue[k][0]; //존이름
    //                        ceZone_dataGridView.Rows[h].Cells[5].Value = 층[0][0]; //층
    //                        ceZone_dataGridView.Rows[h].Cells[6].Value = ZoneValue[k][1]; //용도프로필
    //                        string 설치대수 = Value[k][3].ToString().Split('_')[1];
    //                        ceZone_dataGridView.Rows[h].Cells[7].Value = 설치대수; //설치대수
    //                        DataGridViewButtonCell ButtonCell = new DataGridViewButtonCell();
    //                        ceZone_dataGridView.Rows[h].Cells[8] = ButtonCell;//적용 
    //                        ButtonCell.Value = "+";
    //                    }
    //                }

    //                //for (int n = 0; n < Value.Length; n++)
    //                //{
    //                //    int ZoneRow = 0;
    //                //    ceZone_dataGridView.Rows.Add();
    //                //    int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                //    for (int a = 0; a < ceZone_dataGridView.Rows.Count; a++)
    //                //    {
    //                //        if (ceZone_dataGridView.Rows[a].Cells[2].Value != null && ceZone_dataGridView.Rows[a].Cells[3].Value.ToString() == Value[n][0].ToString())
    //                //        {
    //                //            ZoneRow = a;
    //                //        }
    //                //    }
    //                //    String[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "공급설비", "존번호 = '" + ceZone_dataGridView.Rows[ZoneRow].Cells[2].Value.ToString() + "' And  냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
    //                //    ceZone_dataGridView.Rows[ZoneRow].Cells[6].Value = Value2.Length;
    //                //    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[ZoneRow].Cells[0].Value + "_" + Value[n][3].Substring(Value[n][3].IndexOf("_") + 1, 1);

    //                DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
    //                String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
    //                if (일람표.Length > 0)
    //                {
    //                    for (int j = 0; j < 일람표.Length; j++)
    //                    {
    //                        일람표comboBox.Items.Add(일람표[j][0]);
    //                    }
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[8] = 일람표comboBox;
    //                    일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "번호 = '" + Value[n][3].Substring(0, Value[n][3].IndexOf("_")) + "'");
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[8].Value = 일람표[0][0];
    //                }
    //                DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                //    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                //    ceZone_dataGridView.Rows.Insert(ZoneRow + 1, AddRow);
    //                //}
    //            }
    //            //공급설비가 있는데 공조기부분은 하나도 없는경우
    //            else { load_table_DB(CE_TYPE); }
    //        }
    //        else//그외의 경우 
    //        {
    //            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "존번호,냉방시스템,공급설비종류,공급설비", "냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
    //            if (Value.Length > 0)
    //            {
    //                for (int n = 0; n < Value.Length; n++)
    //                {
    //                    int ZoneRow = 0;
    //                    ceZone_dataGridView.Rows.Add();
    //                    int AddRowNum = ceZone_dataGridView.Rows.Count - 1;
    //                    for (int a = 0; a < ceZone_dataGridView.Rows.Count; a++)
    //                    {
    //                        if (ceZone_dataGridView.Rows[a].Cells[1].Value != null && ceZone_dataGridView.Rows[a].Cells[1].Value.ToString() == Value[n][0].ToString())
    //                        {
    //                            ZoneRow = a;
    //                        }
    //                    }
    //                    String[][] Value2 = Program.DB.getValue(DB.type.ProjDB, "Cooling_ce_Form", "공급설비", "존번호 = '" + ceZone_dataGridView.Rows[ZoneRow].Cells[1].Value.ToString() + "' And  냉방시스템 = '" + SystemNum + "' And 공급설비종류 = '" + ceType + "'");
    //                    ceZone_dataGridView.Rows[ZoneRow].Cells[5].Value = Value2.Length;
    //                    ceZone_dataGridView.Rows[AddRowNum].Cells[0].Value = ceZone_dataGridView.Rows[ZoneRow].Cells[0].Value + "_" + Value[n][3].Substring(Value[n][3].IndexOf("_") + 1, 1);

    //                    DataGridViewComboBoxCell 일람표comboBox = new DataGridViewComboBoxCell();
    //                    String[][] 일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "종류 = '" + ceType + "' AND 난방냉방 !='난방'");
    //                    if (일람표.Length > 0)
    //                    {
    //                        for (int j = 0; j < 일람표.Length; j++)
    //                        {
    //                            일람표comboBox.Items.Add(일람표[j][0]);
    //                        }
    //                        ceZone_dataGridView.Rows[AddRowNum].Cells[7] = 일람표comboBox;
    //                        일람표 = Program.DB.getValue(DB.type.ProjDB, "User_ce", "명칭", "번호 = '" + Value[n][3].Substring(0, Value[n][3].IndexOf("_")) + "'");
    //                        ceZone_dataGridView.Rows[AddRowNum].Cells[7].Value = 일람표[0][0];
    //                    }
    //                    DataGridViewRow AddRow = ceZone_dataGridView.Rows[AddRowNum];
    //                    ceZone_dataGridView.Rows.RemoveAt(AddRowNum);
    //                    ceZone_dataGridView.Rows.Insert(ZoneRow + 1, AddRow);
    //                }
    //            }
    //        }
    //    }
    //}
}
