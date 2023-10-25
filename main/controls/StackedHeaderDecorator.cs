using main;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

public class StackedHeaderDecorator
{
    private readonly IStackedHeaderGenerator objStackedHeaderGenerator = StackedHeaderGenerator.Instance;
    private Graphics objGraphics;
    private readonly DataGridView objDataGrid;
    private Header objHeaderTree;
    private int iNoOfLevels;
    private bool filtered = false;
    private readonly StringFormat objFormat;

    public delegate bool RenderProc(DataGridViewCell cell, int column, int row);

    private RenderProc renderProc = null;

    public StackedHeaderDecorator(DataGridView objDataGrid, DataGridViewAutoSizeColumnsMode mode = DataGridViewAutoSizeColumnsMode.ColumnHeader, RenderProc proc = null, bool filter = false )
    {
        this.objDataGrid = objDataGrid;
        objFormat = new StringFormat();
        objFormat.Alignment = StringAlignment.Center;
        objFormat.LineAlignment = StringAlignment.Center;

        Type dgvType = objDataGrid.GetType();
        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
        pi.SetValue(objDataGrid, true, null);

        objDataGrid.Scroll += (objDataGrid_Scroll);
        objDataGrid.Paint += objDataGrid_Paint;
        objDataGrid.ColumnRemoved += objDataGrid_ColumnRemoved;
        objDataGrid.ColumnAdded += objDataGrid_ColumnAdded;
        objDataGrid.ColumnWidthChanged += objDataGrid_ColumnWidthChanged;
        objDataGrid.CurrentCellDirtyStateChanged += objDataGrid_CurrentCellDirtyStateChanged;

        objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);

        objDataGrid.AutoSizeColumnsMode = mode;
//        objDataGrid.AutoSizeColumnsMode = fixedHeader ? DataGridViewAutoSizeColumnsMode.Fill : DataGridViewAutoSizeColumnsMode.AllCells;//.ColumnHeader;

        DataGridViewCellStyle defCellStyle = new DataGridViewCellStyle();

        defCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        defCellStyle.BackColor = Color.FromArgb(251, 251, 251);
        defCellStyle.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        defCellStyle.ForeColor = SystemColors.WindowText;
        defCellStyle.SelectionBackColor = Color.White;
        defCellStyle.SelectionForeColor = Color.Black;
        defCellStyle.WrapMode = DataGridViewTriState.False;
        objDataGrid.ColumnHeadersDefaultCellStyle = defCellStyle;

        objDataGrid.GridColor = Color.FromArgb(220, 220, 220);//Row 선 색
        objDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        objDataGrid.CellPainting += dataGridView1_CellPainting;

        //    objDataGrid.RowsAdded += objDataGrid_RowsAdded;
        //    objDataGrid..RowsRemoved += objDataGrid_RowsRemoved;

        renderProc = proc;

        filtered = filter;
    }

    private void objDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        objDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }


    public StackedHeaderDecorator(IStackedHeaderGenerator objStackedHeaderGenerator, DataGridView objDataGrid, DataGridViewAutoSizeColumnsMode mode = DataGridViewAutoSizeColumnsMode.ColumnHeader, RenderProc proc = null)
            : this(objDataGrid, mode, proc)
    {
        this.objStackedHeaderGenerator = objStackedHeaderGenerator;
    }

    void objDataGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
        Refresh();
    }

    void objDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {
        RegenerateHeaders();
        Refresh();
    }

    void objDataGrid_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
    {
        RegenerateHeaders();
        Refresh();
    }
    private bool isSelectedRow(int ridx)
    {
        DataGridViewRow row = objDataGrid.Rows[ridx];

        if (row.Cells.Count > 0)
        {
            var val = row.Cells[0].Value;

            //if (val != null)
            //{
            //    Debug.Print(string.Format("{0} {1}", ridx, val.ToString()));

            //}


            if (val != null && val.Equals(true))
            {
                return true;
            }
        }

        //int i = -1, cnt = objDataGrid.GetCellCount(DataGridViewElementStates.Selected);

        //while (++i < cnt)
        //{
        //    if (objDataGrid.SelectedCells[i].RowIndex == ridx)
        //    {
        //        return true;
        //    }
        //}
        return false;
    }
    private void renderColors()
    {
        int i;

        for (int k = 0; k < objDataGrid.RowCount; k++)
        {
            DataGridViewRow row = objDataGrid.Rows[k];

            i = -1;
            while (++i < row.Cells.Count)
            {
                var cell = row.Cells[i];

                if (isSelectedRow(k))
                {
                    cell.Style.BackColor = SystemColors.GradientInactiveCaption;
                }
                else
                {
                    if (renderProc != null && renderProc(cell, i, k)) 
                    {
                        continue;
                    }                    
                    else if (cell.GetType() == typeof(DataGridViewTextBoxCell))
                    {
                        cell.Style.BackColor = (cell.Value == null || cell.Value.ToString() == "") ? Color.FromArgb(255, 255, 243) : Color.FromArgb(255, 255, 255); //입력 셀에 입력되어 있을 경우 회색, 안되어 있을 경우 노랑색 
                    }
                    else if (cell.GetType() == typeof(DataGridViewComboBoxCell)) 
                    {
                         //cell.Style.BackColor = (cell.Value == null || cell.Value.ToString() == "") ? SystemColors.InactiveBorder : Color.FromArgb(255, 255, 255);//콤보박스 셀에 입력되어 있을 경우 회색, 안되어 있을 경우 주황색
                         cell.Style.BackColor = (cell.Value == null || cell.Value.ToString() == "") ? Color.FromArgb(255, 255, 243) : Color.FromArgb(255, 255, 255);//콤보박스 셀에 입력되어 있을 경우 회색, 안되어 있을 경우 주황색
                    }
                    else
                    {
                        cell.Style.BackColor = SystemColors.Window;
                    }
                }
            }
        }
    }

// Paint the cell when not in edit mode.
private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
{
    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
    {
        renderColors();

        //Rectangle rect2 = new Rectangle(9, e.CellBounds.Y + e.CellBounds.Height, 9999, e.CellBounds.Height + 1);

        //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0)), rect2);

        if (filtered && e.RowIndex == 0 && e.ColumnIndex == 0)
        {
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentBackground);

            using (Brush backbrush = new SolidBrush(SystemColors.Window))
            {
                Rectangle rect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 2, e.CellBounds.Height - 2);

                e.Graphics.FillRectangle(backbrush, rect);
            }

            e.Handled = true;
        }
        else if (objDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
        {
            var cell = objDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
            var foreColor = cell.Style.ForeColor.Name == "0" ? Color.Black : cell.Style.ForeColor;

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentBackground);

            using (Brush forebrush = new SolidBrush(foreColor))
            using (Brush backbrush = new SolidBrush(cell.Style.BackColor))
            using (StringFormat format = new StringFormat())
            {
                Rectangle rect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 19, e.CellBounds.Height - 3);
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                e.Graphics.FillRectangle(backbrush, rect);
                e.Graphics.DrawString(cell.FormattedValue.ToString(), e.CellStyle.Font, forebrush, rect, format);
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.ErrorIcon);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
            e.Handled = true;
        }
            else if (objDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                var cell = objDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                var foreColor = cell.Style.ForeColor.Name == "0" ? Color.Black : cell.Style.ForeColor;

                e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
                e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentBackground);

                using (Brush forebrush = new SolidBrush(foreColor))
                using (Brush backbrush = new SolidBrush(Color.FromArgb(192, 208, 226))) //버튼 색상
                using (StringFormat format = new StringFormat())
                {
                    Rectangle rect0 = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    Rectangle rect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 2, e.CellBounds.Height - 2);
                    format.Alignment = StringAlignment.Center;

                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Window), rect0);// 버튼 배경 색상
                    e.Graphics.FillRectangle(backbrush, rect);
                    e.Graphics.DrawString(cell.FormattedValue.ToString(), e.CellStyle.Font, forebrush, rect, format);
                }

                e.Paint(e.ClipBounds, DataGridViewPaintParts.ErrorIcon);
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
                e.Handled = true;
            }
        }
    }

bool isMultiLined()
{
    int i = -1;

    while(++i < objDataGrid.Columns.Count)
    {
        var s = objDataGrid.Columns[i].HeaderText;

        if (s.IndexOf('\n') >= 0)
        {
            return true;
        }
    }
    return false;
}

// Paint the cell in edit mode.
void objDataGrid_Paint(object sender, PaintEventArgs e)
    {
        iNoOfLevels = NoOfLevels(objHeaderTree);
        if (iNoOfLevels > 1 && !isMultiLined())
        {
            iNoOfLevels--;
        }
        objGraphics = e.Graphics;
        objDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        objDataGrid.ColumnHeadersHeight = iNoOfLevels * 20;
        if (filtered)
        {
            objDataGrid.ColumnHeadersHeight += 23;
        }
        if (null != objHeaderTree)
        {
            RenderColumnHeaders();
        }
    }

    void objDataGrid_Scroll(object sender, ScrollEventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        Rectangle rtHeader = objDataGrid.DisplayRectangle;
        objDataGrid.Invalidate(rtHeader);
    }

    private void RegenerateHeaders()
    {
        objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid);
    }

    private void RenderColumnHeaders()
    {
        objGraphics.FillRectangle(new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.BackColor),
                                    new Rectangle(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y,
                                                objDataGrid.DisplayRectangle.Width, objDataGrid.ColumnHeadersHeight));

        foreach (Header objChild in objHeaderTree.Children)
        {
            objChild.Measure(objDataGrid, 0, objDataGrid.ColumnHeadersHeight/iNoOfLevels);
            objChild.AcceptRenderer(this);
        }

        Point pt01 = new Point(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y);
        Point pt02 = new Point(objDataGrid.DisplayRectangle.X + objDataGrid.DisplayRectangle.Width, objDataGrid.DisplayRectangle.Y);
        Point pt11 = new Point(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y + objDataGrid.ColumnHeadersHeight);
        Point pt12 = new Point(objDataGrid.DisplayRectangle.X + objDataGrid.DisplayRectangle.Width, objDataGrid.DisplayRectangle.Y + objDataGrid.ColumnHeadersHeight);

        objGraphics.DrawLine(Pens.Gray, pt01, pt02);
        objGraphics.DrawLine(Pens.Gray, pt11, pt12);
    }

public void Render(Header objHeader)
    {
        if (objHeader.Children.Count == 0)
        {
            Rectangle r1 = objDataGrid.GetColumnDisplayRectangle(objHeader.ColumnId, true), r0, r2;
            if (r1.Width == 0)
            {
                return;
            }
            r1.Y = objHeader.Y;
            r1.Width += 1;
            r1.X -= 1;
            r1.Height = filtered ? (objHeader.Height - 23) : objHeader.Height;
            objGraphics.SetClip(r1);

            r0 = r1;

            if (r1.X + objDataGrid.Columns[objHeader.ColumnId].Width < objDataGrid.DisplayRectangle.Width)
            {
                r1.X -= (objDataGrid.Columns[objHeader.ColumnId].Width - r1.Width);
            }
            r1.X -= 1;
            r1.Width = objDataGrid.Columns[objHeader.ColumnId].Width + 1;

            if (filtered) {
                r2 = r1;

                r2.Height = objHeader.Height;
                objGraphics.SetClip(r2);
                objGraphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 1), r2);
                objGraphics.SetClip(r0);
            }
            else {
                objGraphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 1), r1);
            }
            objGraphics.DrawString(objHeader.Name,
                                    objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                                    new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor),
                                    r1,
                                    objFormat);
            objGraphics.ResetClip();
        }
        else
        {
            int x = objDataGrid.RowHeadersWidth;
            for (int i = 0; i < objHeader.Children[0].ColumnId; ++i)
            {
                if (objDataGrid.Columns[i].Visible)
                {
                    x += objDataGrid.Columns[i].Width;
                }
            }
            if (x > (objDataGrid.HorizontalScrollingOffset + objDataGrid.DisplayRectangle.Width - 5))
            {
                return;
            }

            //Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.Children[0].ColumnId, -1, true);
            Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.ColumnId, -1, true);

            r1.Y = objHeader.Y;
            r1.Height = objHeader.Height;
            r1.Width = objHeader.Width  + 1;
            if (r1.X < objDataGrid.RowHeadersWidth)
            {
                r1.X = objDataGrid.RowHeadersWidth;
            }
            r1.X -= 1;
            objGraphics.SetClip(r1);
        //      r1.X = x - objDataGrid.HorizontalScrollingOffset;
        //     r1.Width -= 1;
            objGraphics.DrawRectangle(new Pen(Color.FromArgb(220, 220, 220), 1), r1);
            r1.X -= 1;
            objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font,
                                    new SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor),
                                    r1, objFormat);
            objGraphics.ResetClip();
        }
    }

    private int NoOfLevels(Header header)
    {
        int level = 0;
        foreach (Header child in header.Children)
        {
            int temp = NoOfLevels(child);
            level = temp > level ? temp : level;
        }
        return level + 1;
    }
}
