using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using main.subcontents;
using main.subcontents.ConstructionCW;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.Web.WebView2.Core;
using static main.MainContents;

namespace main.contents
{
    public partial class Model : Form
    {
        int SelectRow;
        bool scriptable = false;

        public Model()
        {
            InitializeComponent();
            Create_table();

            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }

        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
            }
            catch (Exception ex)
            {

            }
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        private void Create_table()
        {

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.ColumnCount = 11;
            dataGridView1.Columns[1].HeaderText = "번호";
            dataGridView1.Columns[2].HeaderText = "층";
            dataGridView1.Columns[3].HeaderText = "존";
            dataGridView1.Columns[4].HeaderText = "외피유형";
            dataGridView1.Columns[5].HeaderText = "커튼월부위";
            dataGridView1.Columns[6].HeaderText = "인접존";
            dataGridView1.Columns[7].HeaderText = "면적" + Environment.NewLine + "[m²]";
            dataGridView1.Columns[8].HeaderText = "방위" + Environment.NewLine + " - ";
            dataGridView1.Columns[9].HeaderText = "기울기" + Environment.NewLine + "[°]";
            dataGridView1.Columns[10].HeaderText = "구조체";
            Load_table();
        }


        private void Load_table()
        {
            dataGridView1.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,인접존,면적,방위,기울기", "");
            for (int n = 0; n < Value.Length; n++)
            {
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

                dataGridView1.Rows.Add(null, Value[n][0], Value[n][1], Value[n][2], null, null, Value[n][5], Value[n][6], Value[n][7], Value[n][8]);

                TypeCombo.Value = Value[n][3];
                dataGridView1.Rows[n].Cells[4] = TypeCombo;

                CWTypeCombo.Value = Value[n][4];
                dataGridView1.Rows[n].Cells[5] = CWTypeCombo;

                String Type = dataGridView1.Rows[n].Cells[4].Value.ToString();
                Load_ConstructionList(n, Type);
            }
            
        }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String num = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (e.ColumnIndex == 4)
                { 
                    String Type= dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D","번호,외피유형","'"+ num + "','"+Type+"'","번호");
                    if(Type !="커튼월창")
                    {
                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,커튼월부위", "'" + num + "','" + "" + "'", "번호");
                    }
                    else { return; }
                    Load_ConstructionList(e.RowIndex, Type);
                }

                if (e.ColumnIndex == 5)
                {
                    String CWType;
                    if (dataGridView1.Rows[e.RowIndex].Cells[5].Value == null)
                    {
                        CWType = "";
                    }
                    else
                    {
                        CWType = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    }                   
                    Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,커튼월부위", "'" + num + "','" + CWType + "'", "번호");
                }

                if(e.ColumnIndex == 10) {
                    String ConsType;
                    if (dataGridView1.Rows[e.RowIndex].Cells[10].Value == null)
                    {
                        ConsType = "";
                    }
                    else
                    {
                        ConsType = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }
                    Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,구조체", "'" + num + "','" + ConsType + "'", "번호");
                }               
            }
        }

        private void Load_ConstructionList(int n, String Type)
        {
            string[][] Value =null;

                          
                if (Type == "커튼월창")
                {
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭", "");
                }
                else if(Type == "창호")
                {
                    Value = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭", "");
                }
                else
                {
                    Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭", "");
                }      
              DataGridViewComboBoxCell ConstructionCombo = new DataGridViewComboBoxCell();
              ConstructionCombo.Items.Clear();
              for (int k = 0;  k< Value.Length; k++)
              {              
                  ConstructionCombo.Items.Add(Value[k][1]);
              }
              dataGridView1.Rows[n].Cells[10] = ConstructionCombo;
        }
    }
}
