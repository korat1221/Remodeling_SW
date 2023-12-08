using main.contents._3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace main.contents
{
    public partial class sub3dZoneInfo : Form
    { String ConsType, ConsNum;
        string sid = "";
        public void resetSID()
        {
            sid = "";

            fillFilterCombos();

            comboBox1.SetLoaded();
            comboBox2.SetLoaded();
            comboBox3.SetLoaded();

        }
        public sub3dZoneInfo()
        {
            Program.DB.initTables(DB.type.ProjDB);
            InitializeComponent();
            create_datagridview1();
            create_datagridview2();
        }
        private void create_datagridview1()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.AllCells, dataGridView1_RowHandle, true);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "층");
            dataGridView1.Columns.Add("A3", "존");
            dataGridView1.Columns.Add("A4", "외피유형");
            dataGridView1.Columns.Add("A5", "커튼월부위");
            dataGridView1.Columns.Add("A6", "인접존");
            dataGridView1.Columns.Add("A7", "면적[m²]");
            dataGridView1.Columns.Add("A8", "방위");
            dataGridView1.Columns.Add("A9", "기울기");
            dataGridView1.Columns.Add("A10", "구조체");
            dataGridView1.Columns.Add("A11", "천창유무");
            dataGridView1.Columns.Add("A12", "차양적용");
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[10].Width = 100;
            dataGridView1.Columns[11].Width = 100;

            fillFilterCombos();

            comboBox1.SetLoaded();
            comboBox2.SetLoaded();
            comboBox3.SetLoaded();
        }
        private void create_datagridview2()
        {
            new StackedHeaderDecorator(dataGridView2, DataGridViewAutoSizeColumnsMode.AllCells, dataGridView2_RowHandle);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView2.Columns.Add(checkBoxColumn);

            dataGridView2.Columns.Add("B1", "존 번호");
            dataGridView2.Columns.Add("B2", "바닥면적");

            dataGridView2.Columns[0].Width = 30;
            dataGridView2.Columns[1].Width = 100;
            dataGridView2.Columns[2].Width = 100;
        }
        private bool dataGridView1_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                if (column == 1 || column == 2 || column == 3 || column == 7 || column == 8 || column == 9)
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else if (column == 4 && cell.GetType() == typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.Info;
                    //  cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    return true;
                }
                else if (column == 4 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;//연한 파랑
                    return true;
                }
                if (column == 5 || column == 6)
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else if (column == 10 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else if (column == 11 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else if (column == 12 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else return false;
            }
            else
            {
                if (column == 1 || column == 2 || column == 3 || column == 7 || column == 8 || column == 9)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else if (column == 4 && cell.GetType() == typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = SystemColors.Info;
                    // cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    return true;
                }
                else if (column == 4 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255); //흰색
                    return true;
                }
                if (column == 5 || column == 6)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else if (column == 10 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else if (column == 11 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else if (column == 12 && cell.GetType() != typeof(DataGridViewComboBoxCell))
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else return false;
            }
        }
        private bool dataGridView2_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = Color.FromArgb(251, 251, 251);
                return true;
            }
            else return false;
        }

        private String _fixed(string v)
        {
            try
            {
                return (v == "0" ? "0" : Double.Parse(v).ToString("#.##"));
            }
            catch (Exception e) { }

            return "0";
        }
        private void fillFilterCombos()
        {
            int i = -1;
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층,존,외피유형");

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            comboBox1.Add("All");
            comboBox2.Add("All");
            comboBox3.Add("All");

            while (++i < rec.Length)
            {
                comboBox1.Add(rec[i][0]);
                comboBox2.Add(rec[i][1]);
                comboBox3.Add(rec[i][2]);
            }
        }
        private void redrawList()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "존번호,바닥면적,주향,주광너비,주광깊이,상인방높이");

                while (++i < rec.Length)
                {
                    dataGridView2.Rows.Add(null, rec[i][0], _fixed(rec[i][1]));
                    //  dataGridView2.Rows.Add(null, rec[i][0], _fixed(rec[i][1]), rec[i][2], _fixed(rec[i][3]), _fixed(rec[i][4]), _fixed(rec[i][5]));
                }
            }
            {
                int i = -1, idx;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,천창유무,차양적용");

                while (++i < rec.Length)
                {
                    if (comboBox1.IsChecked(rec[i][1]) && comboBox2.IsChecked(rec[i][2]) && comboBox3.IsChecked(rec[i][3]))
                    {
                        idx = dataGridView1.Rows.Add(null, rec[i][0], rec[i][1], rec[i][2], null, null, rec[i][6], _fixed(rec[i][5]), rec[i][7], _fixed(rec[i][8]), null);

                        if (isWinType(rec[i][3]))
                        {
                            DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();
                            TypeCombo.Items.Add("커튼월창");
                            TypeCombo.Items.Add("창호");
                            TypeCombo.Items.Add("외부출입문");

                            TypeCombo.Value = rec[i][3];
                            dataGridView1.Rows[idx].Cells[4] = TypeCombo;
                            Load_ConstructionList(idx, rec[i][3]);
                        }
                        else
                        {
                            DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                            TypeLabel.Value = rec[i][3];
                            dataGridView1.Rows[idx].Cells[4] = TypeLabel;
                            TypeLabel.ReadOnly = true;
                            Load_ConstructionList(idx, rec[i][3]);
                        }
                        if (isCWallType(rec[i][4]))
                        {
                            DataGridViewComboBoxCell CWTypeCombo = new DataGridViewComboBoxCell();
                            CWTypeCombo.Items.Add("유리부분");
                            CWTypeCombo.Items.Add("패널부분");
                            CWTypeCombo.Items.Add("출입문부분");

                            CWTypeCombo.Value = rec[i][4];
                            dataGridView1.Rows[idx].Cells[5] = CWTypeCombo;
                        }
                        else
                        {
                            DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                            TypeLabel.Value = "";
                            dataGridView1.Rows[idx].Cells[5] = TypeLabel;
                            TypeLabel.ReadOnly = true;
                        }
                        if (isWinCW(rec[i][3])) //천창유무 
                        {
                            DataGridViewComboBoxCell RoofWinCombo = new DataGridViewComboBoxCell();
                            RoofWinCombo.Items.Add("천창있음");
                            RoofWinCombo.Items.Add("");

                            RoofWinCombo.Value = rec[i][9];
                            dataGridView1.Rows[idx].Cells[11] = RoofWinCombo;
                        }
                        if (isWinCW(rec[i][3])) //차양적용
                        {
                            DataGridViewComboBoxCell BlindCombo = new DataGridViewComboBoxCell();
                            string[][] BlindValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "번호");
                            for (int a = 0; a < BlindValue.Length; a++) { BlindCombo.Items.Add(BlindValue[a][0].ToString()); }

                            BlindCombo.Value = rec[i][10];
                            dataGridView1.Rows[idx].Cells[12] = BlindCombo;
                        }
                        else
                        {
                            DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                            TypeLabel.Value = "";
                            dataGridView1.Rows[idx].Cells[12] = TypeLabel;
                            TypeLabel.ReadOnly = true;
                        }
                    }
                }
            }
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID.IndexOf("999999") < 0 && main.MainContents.selID != sid)
            {
                sid = main.MainContents.selID;
                redrawList();
            }
        }

        private bool isWinType(string type)
        {
            switch (type)
            {
                case "커튼월창":
                case "창호":
                case "외부출입문":
                    return true;
            }
            return false;
        }

        private bool isCWallType(string type)
        {
            switch (type)
            {
                case "유리부분":
                case "패널부분":
                case "출입문부분":
                    return true;
            }
            return false;
        }
        private bool isWinCW(string type)
        {
            switch (type)
            {
                case "커튼월창":
                case "창호":
                    return true;
            }
            return false;
        }
        private void Load_ConstructionList(int n, String Type)
        {
            string[][] Value = null;

            switch (Type)
            {
                case "커튼월창":
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭", "");
                    break;
                case "외벽":
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭", "");
                    break;
                case "지붕":
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호,명칭", "");
                    break;
                case "최하층바닥":
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "번호,명칭", "");
                    break;
                case "창호":
                    Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭", "");
                    break;
                case "외부출입문":
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭", ""); //출입문으로 나중에 바꿔야함 
                    break;
            }

            DataGridViewComboBoxCell ConstructionCombo = new DataGridViewComboBoxCell();
            ConstructionCombo.Items.Clear();
            if (Value != null)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    ConstructionCombo.Items.Add(Value[k][1]);
                }
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,구조체번호,구조체", "번호 = '" + dataGridView1.Rows[n].Cells[1].Value + "'");

                int a = 0;
                for (int k = 0; k < Value.Length; k++)
                {
                    if (rec[0][1] == Value[k][0])
                    {
                        a = a + 1;
                        break;
                    }
                }
                if (a > 0)
                {
                    ConstructionCombo.Value = rec[0][2];
                }
                else
                {
                    ConstructionCombo.Value = null;
                }
                dataGridView1.Rows[n].Cells[10] = ConstructionCombo;
            }
            else { }

        }

        private void onDataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호", "번호='" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");

                    if (rec.Length > 0)
                    {
                        Program.UTIL.sendMessage("board-" + rec[0][0]);
                    }
                }

                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int SelectRow = e.RowIndex;
            }
        }
        public string Save()
        {

            string num, num0, id, Type, CWType, ret = "", tcode, RoofWin = "",Blind ="";
            int i = -1;
            while (++i < dataGridView1.RowCount)
            {
                if (dataGridView1.Rows[i].Cells[4].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString() != "내벽" && dataGridView1.Rows[i].Cells[4].Value.ToString() != "층간바닥")
                    {
                        if (dataGridView1.Rows[i].Cells[10].Value == null || dataGridView1.Rows[i].Cells[10].Value.ToString() == "")
                        {
                            MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString() + "의 구조체를 선택하세요.");
                            return "[" + ret + "]";
                        }
                        else { }
                    }
                    else { }
                }
                else { }
            }

            i = -1;
            while (++i < dataGridView1.RowCount)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    num0 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    num = num0;
                    Type = dataGridView1.Rows[i].Cells[4].Value.ToString();

                    string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디", "번호='" + num0 + "'");

                    id = rec[0][0];

                    if (isWinType(Type) && (tcode = getTCode(Type)) != "")
                    {
                        CWType = dataGridView1.Rows[i].Cells[5].Value.ToString();

                        if (Type != "커튼월창") CWType = Type;
                        else if (CWType == "") CWType = "유리부분";

                        num = num.Replace("_WIN_", "__");
                        num = num.Replace("_DR_", "__");
                        num = num.Replace("_CW_", "__");
                        num = num.Replace("__", tcode);

                        ret += "{\"id0\":\"" + id + "\",\"id\":\"" + num + "\",\"type\":\"" + Type + "\",\"wtype\":\"" + CWType + "\"},";

                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호,외피유형,커튼월부위", "'" + id + "','" + num + "','" + Type + "','" + CWType + "'", "아이디");

                    }

                    if (dataGridView1.Rows[i].Cells[10].Value == null)
                    {
                        ConsType = "";
                    }
                    else
                    {
                        ConsType = dataGridView1.Rows[i].Cells[10].Value.ToString();
                        string[][] Value = null;
                        switch (Type)
                        {
                            case "커튼월창":
                                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호", "명칭 = '" + ConsType + "'");
                                break;
                            case "외벽":
                                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호", "명칭 = '" + ConsType + "'");
                                break;
                            case "지붕":
                                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호", "명칭 = '" + ConsType + "'");
                                break;
                            case "최하층바닥":
                                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionFloor", "번호", "명칭 = '" + ConsType + "'");
                                break;
                            case "창호":
                                Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호", "명칭 = '" + ConsType + "'");
                                break;
                            case "외부출입문":
                                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호", "명칭 = '" + ConsType + "'"); ; //출입문으로 나중에 바꿔야함 
                                break;

                                
                        }
                        if (Value.Length > 0)
                        {
                            ConsNum = Value[0][0];
                            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                            Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호,프로젝트유형,구조체,구조체번호", "'" + id + "','" + num + "','" + 프로젝트유형[0][0] + "','" + ConsType + "','" + ConsNum + "'", "아이디");
                        }
                        else { }
                    }
                    if (dataGridView1.Rows[i].Cells[11].Value == null)
                    {
                        RoofWin = "";
                    }
                    else
                    {
                        RoofWin = dataGridView1.Rows[i].Cells[11].Value.ToString();
                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,천창유무", "'" + id + "','" + RoofWin + "'", "아이디");
                    }
                    if (dataGridView1.Rows[i].Cells[12].Value == null)
                    {
                        Blind = "";
                    }
                    else
                    {
                        Blind = dataGridView1.Rows[i].Cells[12].Value.ToString();
                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,차양적용", "'" + id + "','" + Blind + "'", "아이디");
                        String[][] SubLoad = Program.DB.querySQL(DB.type.ProjDB, "select a.상위창호번호 FROM SubWindow AS a INNER JOIN ZoneEnvelope_3D AS b ON b.구조체번호 = a.번호 where b.아이디 = '" + id + "' AND b.외피유형 = '창호'");
                        String[][] BlindValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "설치,외부반사율,투과율,흡수율", "번호 = '" + Blind + "'");
                        if (SubLoad.Length > 0)
                        {
                            String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "유리종류,태양열취득률,빛투과율,유리열관류율", "번호 = '" + SubLoad[0][0] + "'");

                            double SHGC_on = Calc_Blind_SHGC(Convert.ToDouble(MainLoad[0][1]), Convert.ToDouble(BlindValue[0][1]), Convert.ToDouble(BlindValue[0][2]), Convert.ToDouble(BlindValue[0][3]), Convert.ToDouble(MainLoad[0][3]), BlindValue[0][0]);

                            double Glass_Ex, Glass_In;
                            if (MainLoad[0][0].Contains("+"))
                            {
                                string[][] glass = Program.DB.getValue(DB.type.ProjDB, "User_DoubleGlass", "외부반사율,내부반사율", "제품명 ='" + MainLoad[0][0] + "'");
                                Glass_Ex = Convert.ToDouble(glass[0][0]);
                                Glass_In = Convert.ToDouble(glass[0][1]);
                            }
                            else
                            {

                                string[][] glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "외부반사율,내부반사율", "제품명 ='" + MainLoad[0][0] + "'");
                                if (glass.Length == 0)
                                {
                                    glass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "외부반사율,내부반사율", "제품명 ='" + MainLoad[0][0] + "'");
                                }

                                Glass_Ex = Convert.ToDouble(glass[0][0]);
                                Glass_In = Convert.ToDouble(glass[0][1]);
                            }
                            double Tao_on = Calc_Blind_Tao(Convert.ToDouble(MainLoad[0][2]), Convert.ToDouble(BlindValue[0][1]), Convert.ToDouble(BlindValue[0][2]), Glass_Ex, Glass_In, BlindValue[0][0]);
                            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                            Program.DB.setValue(DB.type.ProjDB, "Blind_3D", "아이디,번호,프로젝트유형,차양번호,차양포함태양열취득률,차양포함빛투과율", "'" + id + "','" + num + "','" + 프로젝트유형[0][0] + "','" + Blind + "','" + SHGC_on.ToString() + "','" + Tao_on.ToString() + "'", "아이디");
                        }
                        String[][] CWValue = Program.DB.querySQL(DB.type.ProjDB, "select a.고정유리종류,a.태양열취득률,a.빛투과율,a.고정유리열관류율 FROM ConstructionCW AS a INNER JOIN ZoneEnvelope_3D AS b ON b.구조체번호 = a.번호 where b.아이디 = '" + id + "' AND b.외피유형 = '커튼월창'");
                        if (CWValue.Length > 0)
                        {
                            double SHGC_on = Calc_Blind_SHGC(Convert.ToDouble(CWValue[0][1]), Convert.ToDouble(BlindValue[0][1]), Convert.ToDouble(BlindValue[0][2]), Convert.ToDouble(BlindValue[0][3]), Convert.ToDouble(CWValue[0][3]), BlindValue[0][0]);

                            double Glass_Ex, Glass_In;

                                string[][] glass = Program.DB.getValue(DB.type.ProjDB, "User_Glass", "외부반사율,내부반사율", "제품명 ='" + CWValue[0][0] + "'");
                                if (glass.Length == 0)
                                {
                                    glass = Program.DB.getValue(DB.type.BaseDB_HCneed, "유리", "외부반사율,내부반사율", "제품명 ='" + CWValue[0][0] + "'");
                                }

                                Glass_Ex = Convert.ToDouble(glass[0][0]);
                                Glass_In = Convert.ToDouble(glass[0][1]);

                            double Tao_on = Calc_Blind_Tao(Convert.ToDouble(CWValue[0][2]), Convert.ToDouble(BlindValue[0][1]), Convert.ToDouble(BlindValue[0][2]), Glass_Ex, Glass_In, BlindValue[0][0]);
                            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                            Program.DB.setValue(DB.type.ProjDB, "Blind_3D", "아이디,번호,프로젝트유형,차양번호,차양포함태양열취득률,차양포함빛투과율", "'" + id + "','" + num + "','" + 프로젝트유형[0][0] + "','" + Blind + "','" + SHGC_on.ToString() + "','" + Tao_on.ToString() + "'", "아이디");
                        }



                    }
                }
            }
            //           redrawList();
            MessageBox.Show("저장되었습니다.");

            return "[" + ret + "]";
        }
        private double Calc_Blind_SHGC(double SHGC, double Ex, double Trans, double Alpha, double Ug, string Install)
        {
            double SHGC_on =0;
            switch (Install)
            {
                case "외부측":
                    SHGC_on = Alpha * SHGC + Trans * Math.Pow(1 / Ug + 1 / 5 + 1 / 10, -1) / 10 + Alpha * (1 - SHGC) * Math.Pow(1 / Ug + 1 / 5 + 1 / 10, -1) / 5;
                    break;

                case "중간":
                    SHGC_on = SHGC * Alpha + (Trans + (1 - SHGC) * Ex) * Math.Pow(1 / Ug + 1 / 3, -1) / 3;
                    break;

                case "내부측":
                    SHGC_on = SHGC * (1 - SHGC * Ex - Trans * Math.Pow(1 / Ug + 1 / 30, -1) / 30);
                    break; ;
            }
            return SHGC_on;
        }
        private double Calc_Blind_Tao(double Tao, double Ex, double Trans, double Glass_Ex, double Glass_In, string Install)
        {
            double Tao_on =0;
            switch (Install)
            {
                case "외부측":
                    Tao_on = Tao * Trans / (1 - Glass_Ex * Ex);
                    break;

                case "중간":
                    Tao_on = (Tao * Trans / (1 - Glass_Ex * Ex) + Tao * Trans / (1 - Glass_In * Ex) + Tao) / 3;
                    break;

                case "내부측":
                    Tao_on = Tao * Trans / (1 - Glass_In * Ex);
                    break; ;
            }
            return Tao_on;
        }
        private string getTCode(string type)
        {
            switch (type)
            {
                case "창호":
                    return "_WIN_";
                case "커튼월창":
                    return "_CW_";
                case "외부출입문":
                    return "_DR_";
            }
            return "";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string ID = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                int n = ID.IndexOf("_Zone");
                if (n > 0)
                {
                    ID = ID.Substring(n + 5);

                    n = Int32.Parse(ID);
                    Program.UTIL.sendMessage("space-" + n);
                }
            }
            if (e.RowIndex >= 0)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int SelectRow = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < dataGridView2.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView2.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView2.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = dataGridView1.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (e.ColumnIndex == 4)
                {
                    DataGridViewCell cell = row.Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                    DataGridViewComboBoxCell cell2 = row.Cells[5] as DataGridViewComboBoxCell;

                    Load_ConstructionList(e.RowIndex, dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                    if (cell.Value.ToString() != "커튼월창")
                    {
                        DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                        TypeLabel.Value = "";
                        row.Cells[5] = TypeLabel;
                        TypeLabel.ReadOnly = true;
                    }
                    else
                    {
                        DataGridViewComboBoxCell CWTypeCombo = new DataGridViewComboBoxCell();
                        CWTypeCombo.Items.Add("유리부분");
                        CWTypeCombo.Items.Add("패널부분");
                        CWTypeCombo.Items.Add("출입문부분");

                        CWTypeCombo.Value = "유리부분";

                        row.Cells[5] = CWTypeCombo;
                        CWTypeCombo.ReadOnly = false;
                    }
                }
                if (e.ColumnIndex == 4) //커튼월창이거나 창호 일경우 천창유무 콤보박스 
                {
                    DataGridViewCell cell = row.Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                    DataGridViewComboBoxCell cell2 = row.Cells[10] as DataGridViewComboBoxCell;

                    if (cell.Value.ToString() != "커튼월창" && cell.Value.ToString() != "창호")
                    {
                        DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                        TypeLabel.Value = "";
                        row.Cells[11] = TypeLabel;
                        row.Cells[12] = TypeLabel;
                        TypeLabel.ReadOnly = true;
                    }
                    else
                    {
                        DataGridViewComboBoxCell RoofWinCombo = new DataGridViewComboBoxCell();
                        RoofWinCombo.Items.Add("천창있음");
                        RoofWinCombo.Items.Add("");

                        row.Cells[11] = RoofWinCombo;
                        RoofWinCombo.ReadOnly = false;

                        DataGridViewComboBoxCell BlindCombo = new DataGridViewComboBoxCell();
                        string[][] BlindValue = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "번호");
                        for(int i = 0; i < BlindValue.Length; i++) { BlindCombo.Items.Add(BlindValue[i][0].ToString()); }

                        row.Cells[12] = BlindCombo;
                        BlindCombo.ReadOnly = false; 

                    }
                }

            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.RowIndex1 > 0 && e.RowIndex2 > 0 && (e.CellValue1 != null || e.CellValue2 != null))
            {
                if (e.CellValue1 == null && e.CellValue2 != null)
                {
                    e.SortResult = -1;
                }
                else if (e.CellValue1 != null && e.CellValue2 == null)
                {
                    e.SortResult = 1;
                }
                else
                {
                    e.SortResult = System.String.Compare(
                        e.CellValue1.ToString(), e.CellValue2.ToString());
                }
            }
            e.Handled = true;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                int cellX = dataGridView1.Location.X + e.CellBounds.X;
                int cellY = dataGridView1.Location.Y + e.CellBounds.Y;

                if (e.ColumnIndex == 2)
                {
                    if (!comboBox1.Visible)
                    {
                        comboBox1.Location = new Point(cellX, cellY);
                        comboBox1.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        comboBox1.Show();
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (!comboBox2.Visible)
                    {
                        comboBox2.Location = new Point(cellX, cellY);
                        comboBox2.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        comboBox2.Show();
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (!comboBox3.Visible)
                    {
                        comboBox3.Location = new Point(cellX, cellY);
                        comboBox3.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        comboBox3.Show();
                    }
                }
                else if (e.ColumnIndex == 8)
                {
                    if (!button2.Visible)
                    {
                        button2.Location = new Point(cellX, cellY - 1);
                        button2.Size = new Size(e.CellBounds.Width + 10, e.CellBounds.Height);
                        button2.Show();
                    }
                }
                else if (e.ColumnIndex == 10)
                {
                    if (!button1.Visible)
                    {
                        button1.Location = new Point(cellX, cellY - 1);
                        button1.Size = new Size(e.CellBounds.Width, e.CellBounds.Height);
                        button1.Show();
                    }
                }
            }
        }

        private void sub3dZoneInfo_Deactivate(object sender, EventArgs e)
        {
            comboBox1.Hide();
            comboBox2.Hide();
            comboBox3.Hide();
            button1.Hide();
            button2.Hide();
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.ValueChanged || comboBox2.ValueChanged || comboBox3.ValueChanged)
            {
                redrawList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StruFill modal = new StruFill(this);

            modal.StartPosition = FormStartPosition.CenterParent;

            modal.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cardinal modal = new Cardinal();

            modal.StartPosition = FormStartPosition.CenterParent;

            DialogResult res = modal.ShowDialog();

            if (res.Equals(DialogResult.OK))
            {
                redrawList();

                Program.UTIL.modelScript("setRotation(" + modal.rotation + ")");
            }
        }
    }
}
