using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.BuildingGeneral;

public partial class BlowDoorTest : Form
{
    int SelectRow;
    public double n50;
    public BlowDoorTest()
    {
        InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
        create_table();
        Load_Value();
    }
    private void create_table()
    {
        new StackedHeaderDecorator(Blow_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        Blow_dataGridView.Columns.Clear();
        checkBoxColumn.HeaderText = "선택";
        checkBoxColumn.Name = "check";
        Blow_dataGridView.Columns.Add(checkBoxColumn);

        Blow_dataGridView.Columns.Add("A1", "번호");
        Blow_dataGridView.Columns.Add("A2", "측정 위치");
        Blow_dataGridView.Columns.Add("A3", "침기량.AirFlow @50.[m³/h]");
        Blow_dataGridView.Columns.Add("A4", "침기횟수.ACH @50.[1/h]");
        Blow_dataGridView.Columns.Add("A5", "Volume.[m³]");
        Blow_dataGridView.Columns[0].Width = 30;
        Blow_dataGridView.Columns[1].Width = 30;
        Blow_dataGridView.Columns[2].Width = 80;
        Blow_dataGridView.Columns[4].Width = 80;
        Blow_dataGridView.Columns[5].Width = 80;
        Blow_dataGridView.Columns[5].Visible = false;
    }

    private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
    {
        if (column == 2)
        {
            if (cell.Value == null || cell.Value.ToString() == "")
            {
                cell.Style.BackColor = SystemColors.Info;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        if (column == 3 )
        {
            if(cell.Value == null || cell.Value.ToString() == "")
            {
                cell.Style.BackColor = SystemColors.Info;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            }           
        }
        if (column == 4 )
        {
            if (cell.Value == null || cell.Value.ToString() == "")
            {
                cell.Style.BackColor = SystemColors.Info;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        if (column == 5 )
        {
            cell.Style.BackColor = Color.FromArgb(255, 255, 255);
        }
        return true;
    }

    private void Add_button_Click(object sender, EventArgs e)
    {
        int nRow = Blow_dataGridView.Rows.Add();
        Load_Num();
    }

    private void Load_Num()
    {
        for (int k = 0; k < Blow_dataGridView.RowCount; k++)
        {
            Blow_dataGridView.Rows[k].Cells[1].Value = (k + 1).ToString();
        }
    }
    private void Delete_button_Click(object sender, EventArgs e)
    {
        Blow_dataGridView.Rows.Remove(Blow_dataGridView.Rows[SelectRow]);
        Load_Num();
    }

    private void Blow_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if(e.ColumnIndex == 3 || e.ColumnIndex == 4)
        {
            if((Blow_dataGridView.Rows[e.RowIndex].Cells[3].Value !=null && Blow_dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() != "") && (Blow_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && Blow_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "" && Blow_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "0"))
            {
                Blow_dataGridView.Rows[e.RowIndex].Cells[5].Value = (Convert.ToDouble(Blow_dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()) / Convert.ToDouble(Blow_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString("0.0");
                Cal_n50_tot();
            }
        }
    }

    private Boolean Check_Void()
    {
        Boolean check = false;
        for(int a =0; a< Blow_dataGridView.Rows.Count; a++)
        {
            if (Blow_dataGridView.Rows[a].Cells[5].Value == null)
            {
               check = false;
                goto stop;
            }
            else
            {
                check = true;
            }
        }
        stop:
        return check;
    }
    private void Cal_n50_tot()
    {
        double CMH_tot = 0; double volume_tot = 0;
        if(Check_Void() ==true)
        {
            for (int a = 0; a < Blow_dataGridView.Rows.Count; a++)
            {
                CMH_tot += Convert.ToDouble(Blow_dataGridView.Rows[a].Cells[3].Value.ToString());
                volume_tot += Convert.ToDouble(Blow_dataGridView.Rows[a].Cells[5].Value.ToString());
                n50 = CMH_tot / volume_tot;
                n50_textBox.Text = n50.ToString("0.00") ;
            }
        }       
    }
    private void Save_button_Click(object sender, EventArgs e)
    {
        Boolean check = false;
        for (int a = 0; a < Blow_dataGridView.Rows.Count; a++)
        {
            if (Blow_dataGridView.Rows[a].Cells[2].Value != null && Blow_dataGridView.Rows[a].Cells[5].Value != null && Blow_dataGridView.Rows[a].Cells[5].Value.ToString() != "")
            {
                check = true;
            }
        }

        if (check ==true)
        {
            Program.DB.deleteTable(DB.type.ProjDB, "BlowDoorTest");
            Program.DB.initTable(DB.type.ProjDB, "BlowDoorTest");
            for (int a = 0; a < Blow_dataGridView.Rows.Count; a++)
            {
                Program.DB.setValue(DB.type.ProjDB, "BlowDoorTest", "번호,측정위치,CMH,ACH,Volume",
                "'" + Blow_dataGridView.Rows[a].Cells[1].Value.ToString() + "','"+
                Blow_dataGridView.Rows[a].Cells[2].Value.ToString() + "','" +
                Blow_dataGridView.Rows[a].Cells[3].Value.ToString() + "','" +
                Blow_dataGridView.Rows[a].Cells[4].Value.ToString() + "','" +
                Blow_dataGridView.Rows[a].Cells[5].Value.ToString() 
                + "'", "번호");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        else
        {
            MessageBox.Show("모든 값을 입력해주세요.");
        }
    }
    private void Load_Value()
    {
        string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BlowDoorTest", "번호,측정위치,CMH,ACH,Volume", "");
        if(Value.Length > 0)
        {
            for(int a=0; a< Value.Length; a++)
            {
                int nRow = Blow_dataGridView.Rows.Add();
                Blow_dataGridView.Rows[a].Cells[1].Value = Value[a][0];
                Blow_dataGridView.Rows[a].Cells[2].Value = Value[a][1];
                Blow_dataGridView.Rows[a].Cells[3].Value = Value[a][2];
                Blow_dataGridView.Rows[a].Cells[4].Value = Value[a][3];
            }
        }
    }
       

    private void Blow_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            Blow_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            SelectRow = e.RowIndex;
        }
    }

}
