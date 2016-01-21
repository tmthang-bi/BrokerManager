using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// 
/// </summary>
public class GridViewExportUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="gv"></param>
    public static void Export2Excel(string fileName, GridView gv, ArrayList removeColumnslistIndex, ArrayList msoNumberAsTextColumnListIndex, bool isFooterIncluded)
    {
        if (removeColumnslistIndex != null)
        {
            for (int j = 0; j < removeColumnslistIndex.Count; j++)
            {
                if (gv.HeaderRow != null)
                {
                    gv.HeaderRow.Cells[Convert.ToInt32(removeColumnslistIndex[j])].Visible = false;
                }

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    gv.Rows[i].Cells[Convert.ToInt32(removeColumnslistIndex[j])].Visible = false;
                }

                if (gv.FooterRow != null)
                {
                    gv.FooterRow.Cells[Convert.ToInt32(removeColumnslistIndex[j])].Visible = false;
                }
            }
        }
        
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  include the gridline settings
                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    GridViewExportUtil.PrepareControlForExport(row);

                    // Format number as text for MSO
                    if ((msoNumberAsTextColumnListIndex != null) && (msoNumberAsTextColumnListIndex.Count > 0))
                    {
                        for (int i = 0; i < msoNumberAsTextColumnListIndex.Count; i++)
                        {
                            int col = (int)msoNumberAsTextColumnListIndex[i];
                            if ((0 <= col) && (col < row.Cells.Count))
                            {
                                row.Cells[col].Style.Add("mso-number-format", @"\@");
                                //row.Cells[col].Style.Add("background-color", "#C2D69B");
                            }
                        }
                    }

                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if ((isFooterIncluded) && (gv.FooterRow != null))
                {
                    GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "Yes" : "No"));
            }

            if (current.HasControls())
            {
                GridViewExportUtil.PrepareControlForExport(current);
            }
        }
    }
}
