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
            return (v == "0" ? "0" : Double.Parse(v).ToString("#.##"));
        }
        private void onVisibleChanged(object sender, EventArgs e)
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

                    DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();
                    TypeCombo.Items.Add("커튼월창");
                    TypeCombo.Items.Add("외벽");
                    TypeCombo.Items.Add("지붕");
                    TypeCombo.Items.Add("최하층바닥");
                    TypeCombo.Items.Add("창호");
                    TypeCombo.Items.Add("외부출입문");
                    TypeCombo.Items.Add("내벽");
                    TypeCombo.Items.Add("층간바닥");


                    DataGridViewComboBoxCell CWTypeCombo = new DataGridViewComboBoxCell();
                    CWTypeCombo.Items.Add("유리부분");
                    CWTypeCombo.Items.Add("패널부분");
                    CWTypeCombo.Items.Add("출입문부분");
                    CWTypeCombo.Items.Add("");

                    TypeCombo.Value = rec[i][3];
                    dataGridView1.Rows[i].Cells[4] = TypeCombo;

                    CWTypeCombo.Value = rec[i][4];
                    dataGridView1.Rows[i].Cells[5] = CWTypeCombo;

                    String Type = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    Load_ConstructionList(i, Type);
                }
            }
        }
        private void Load_ConstructionList(int n, String Type)
        {
            string[][] Value = null;


            if (Type == "커튼월창")
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭", "");
            }
            else if (Type == "창호")
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭", "");
            }
            else
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall", "번호,명칭", "");
            }
            DataGridViewComboBoxCell ConstructionCombo = new DataGridViewComboBoxCell();
            ConstructionCombo.Items.Clear();
            for (int k = 0; k < Value.Length; k++)
            {
                ConstructionCombo.Items.Add(Value[k][1]);
            }
            dataGridView1.Rows[n].Cells[10] = ConstructionCombo;
        }

        private void onDataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

            string num, Type, CWType, ConsType, ret = "";
            int i = -1;

            while(++i < dataGridView1.RowCount)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    num = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    Type = dataGridView1.Rows[i].Cells[4].Value.ToString();

                    if (dataGridView1.Rows[i].Cells[5].Value == null)
                    {
                        CWType = "";
                    }
                    else
                    {
                        CWType = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    }

                    if (dataGridView1.Rows[i].Cells[10].Value == null)
                    {
                        ConsType = "";
                    }
                    else
                    {
                        ConsType = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    }

                    ret += "{\"id\":\"" + num + "\",\"type\":\"" + Type + "\",\"wtype\":\"" + CWType + "\"},";
                    Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,외피유형,커튼월부위,구조체", "'" + num + "','" + Type + "','" + CWType + "','" + ConsType + "'", "번호");
                }
            }
            MessageBox.Show("저장되었습니다.");

            return "[" + ret + "]";
        }
    }
}
