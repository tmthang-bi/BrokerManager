using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Web;

/// <summary>
/// Summary description for ExcelUtils
/// </summary>
public class ExcelUtils
{
	public ExcelUtils()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable ReadExcelData(string file, string sheetName)
    {
        DataTable ret = null;
        string fileExtension = Path.GetExtension(file);
        fileExtension = fileExtension.ToLower();

        string connectionString = string.Empty;
        if (fileExtension.Equals(".xls"))
        {
            connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + @";Extended Properties=Excel 8.0";
        }
        else if (fileExtension.Equals(".xlsx"))
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + @";Extended Properties=Excel 12.0";
        }
        else
        {
            throw new InvalidDataException("File '" + file + "' is NOT a valid MS Excel file");
        }

        // Create the connection object 
        OleDbConnection oledbConn = new OleDbConnection(connectionString);

        try
        {

            // Open connection
            oledbConn.Open();

            // Create OleDbCommand object and select data from worksheet Sheet1
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheetName + "$]", oledbConn);
            
            // Create new OleDbDataAdapter 
            OleDbDataAdapter oleda = new OleDbDataAdapter();

            oleda.SelectCommand = cmd;

            // Create a DataSet which will hold the data extracted from the worksheet.
            ret = new DataTable();

            // Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ret);
        }
        catch (Exception ex) 
        {
            
        }
        finally
        {
            if ((oledbConn != null) && (oledbConn.State != ConnectionState.Closed))
            {
                oledbConn.Close();
                oledbConn.Dispose();
            }
        }

        return ret;
    }

    public static void ExportExcelResult(DataView dtv)
    {
        using (ExcelPackage pck = new ExcelPackage())
        {
            //Create the worksheet
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Details");

            //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
            DataTable tbl = CreateTable(dtv);
            ws.Cells["A1"].LoadFromDataTable(tbl, true);

            //Format the header for column 1-3
            using (ExcelRange rng = ws.Cells["A1:AD1"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                rng.Style.Font.Color.SetColor(Color.White);
            }

            //Example how to Format Column 1 as numeric 
            using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 30])
            {
                col.Style.Numberformat.Format = "#,##0";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            using (ExcelRange col = ws.Cells[2, 16, 2 + tbl.Rows.Count, 16])
            {
                col.Style.Numberformat.Format = "#,##0.00";
            }
            using (ExcelRange col = ws.Cells[2, 18, 2 + tbl.Rows.Count, 19])
            {
                col.Style.Numberformat.Format = "#,##0.00";
            }

            //Write it back to the client
            HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Result.xlsx");
            HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
        }
    }


    private static DataTable CreateTable(DataView obDataView)
    {
        if (null == obDataView)
        {
            throw new ArgumentNullException
            ("DataView", "Invalid DataView object specified");
        }
        DataTable obNewDt = obDataView.Table.Clone();
        int idx = 0;
        string[] strColNames = new string[obNewDt.Columns.Count];
        foreach (DataColumn col in obNewDt.Columns)
        {
            strColNames[idx++] = col.ColumnName;
        }
        IEnumerator viewEnumerator = obDataView.GetEnumerator();
        while (viewEnumerator.MoveNext())
        {
            DataRowView drv = (DataRowView)viewEnumerator.Current;
            DataRow dr = obNewDt.NewRow();
            try
            {
                foreach (string strName in strColNames)
                {
                    dr[strName] = drv[strName];
                }
            }
            catch (Exception ex)
            {
               
            }
            obNewDt.Rows.Add(dr);
        }
        return obNewDt;
    }


}
