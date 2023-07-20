using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.contents
{
    public partial class sub3dZoneInfo : Form
    {
        public sub3dZoneInfo()
        {
            InitializeComponent();

            dataGridView1.Columns[7].HeaderText = "면적" + Environment.NewLine + "[m²]";
            dataGridView1.Columns[8].HeaderText = "방위" + Environment.NewLine + " - ";
            dataGridView1.Columns[9].HeaderText = "기울기" + Environment.NewLine + "[°]";
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
        private void redrawList()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "존번호,바닥면적,주향,주광너비,주광깊이,상인방높이");

                while (++i < rec.Length)
                {
                    dataGridView2.Rows.Add(null, rec[i][0], _fixed(rec[i][1]), rec[i][2], _fixed(rec[i][3]), _fixed(rec[i][4]), _fixed(rec[i][5]));
                }
            }
            {
                int i = -1;
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기");

                while (++i < rec.Length)
                {
                    dataGridView1.Rows.Add(null, rec[i][0], rec[i][1], rec[i][2], null, null, rec[i][6], _fixed(rec[i][5]), rec[i][7], _fixed(rec[i][8]));

                    if (isWinType(rec[i][3]))
                    {
                        DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();
                        TypeCombo.Items.Add("커튼월창");
                        TypeCombo.Items.Add("창호");
                        TypeCombo.Items.Add("외부출입문");

                        TypeCombo.Value = rec[i][3];
                        dataGridView1.Rows[i].Cells[4] = TypeCombo;
                        Load_ConstructionList(i, rec[i][3]);
                    }
                    else
                    {
                        DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                        TypeLabel.Value = rec[i][3];
                        dataGridView1.Rows[i].Cells[4] = TypeLabel;
                        TypeLabel.ReadOnly = true;
                        Load_ConstructionList(i, rec[i][3]);
                    }
                    if (isCWallType(rec[i][4]))
                    {
                        DataGridViewComboBoxCell CWTypeCombo = new DataGridViewComboBoxCell();
                        CWTypeCombo.Items.Add("유리부분");
                        CWTypeCombo.Items.Add("패널부분");
                        CWTypeCombo.Items.Add("출입문부분");

                        CWTypeCombo.Value = rec[i][4];
                        dataGridView1.Rows[i].Cells[5] = CWTypeCombo;
                    }
                    else
                    {
                        DataGridViewTextBoxCell TypeLabel = new DataGridViewTextBoxCell();
                        TypeLabel.Value = "";
                        dataGridView1.Rows[i].Cells[5] = TypeLabel;
                        TypeLabel.ReadOnly = true;
                    }
                }
            }
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            redrawList();
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

        private void Load_ConstructionList(int n, String Type)
        {
            string[][] Value = null;

            switch(Type)
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
                dataGridView1.Rows[n].Cells[10] = ConstructionCombo;
            }
            else { }
           
        }

        private void onDataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호", "번호='" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");

                if (rec.Length > 0)
                {
                    Program.UTIL.sendMessage("board-" + rec[0][0]);
                }
            }

            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int SelectRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < dataGridView1.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView1.Rows[k];
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
        public string Save()
        {

            string num, num0, Type, CWType, ConsType, ret = "", tcode;
            int i = -1;

            while (++i < dataGridView1.RowCount)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    num0 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    num = num0;
                    Type = dataGridView1.Rows[i].Cells[4].Value.ToString();

                    if (dataGridView1.Rows[i].Cells[10].Value == null)
                    {
                        ConsType = "";
                    }
                    else
                    {
                        ConsType = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    }

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


                    if (dataGridView1.Rows[i].Cells[10].Value == null)
                    {
                        ConsType = "";
                    }
                    else
                    {
                        ConsType = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    }


                    if (isWinType(Type) && (tcode = getTCode(Type)) != "")
                    {
                        CWType = dataGridView1.Rows[i].Cells[5].Value.ToString();

                        if (Type != "커튼월창") CWType = Type;
                        else if (CWType == "") CWType = "유리부분";

                        num = num.Replace("_WIN_", "__");
                        num = num.Replace("_DR_", "__");
                        num = num.Replace("_CW_", "__");
                        num = num.Replace("__", tcode);

                        string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디", "번호='" + num0 + "'");

                        ret += "{\"id0\":\"" + num0 + "\",\"id\":\"" + num + "\",\"type\":\"" + Type + "\",\"wtype\":\"" + CWType + "\"},";
                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호,외피유형,커튼월부위,구조체,구조체번호", "'" + rec[0][0] + "','" + num + "','" + Type + "','" + CWType + "','" + ConsType + "','" + ConsType + "'", "아이디");
                    }
                }
            }

            redrawList();

            MessageBox.Show("저장되었습니다.");

            return "[" + ret + "]";
        }

        private string getTCode(string type)
        {
            switch(type)
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

                    n = Int32.Parse(ID) - 1;
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
    }
}
