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
using System.Data.OleDb;
using log4net;
using log4net.Config;
using BrokerManager.DataObjects;
using HSCBrokerManagement.Utils;

public partial class BrokerManagement_ImportBrokerList : BasePage
{
    public const int DEFAULT_MAX_UPLOADED_FILE_SIZE_KB = 4096;
    public const int DEFAULT_MAX_UPLOADED_FILE_SIZE_BYTE = (DEFAULT_MAX_UPLOADED_FILE_SIZE_KB * 1024);
    private static string INCORRECT_FORMAT_DATA_FILE = "BrokerImport_IncorrectFormatDataFile";

    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fupData.HasFile)
        {
            try
            {
                if ((fupData.PostedFile.ContentType == "application/ms-excel") ||
                    (fupData.PostedFile.ContentType == "application/vnd.ms-excel") ||
                    (fupData.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    if (fupData.PostedFile.ContentLength < DEFAULT_MAX_UPLOADED_FILE_SIZE_BYTE)
                    {
                        string filename = Path.GetFileName(fupData.FileName);
                        string serverFile = Server.MapPath("~/Upload/") + filename;

                        fupData.SaveAs(serverFile);
                        _logger.Info("Imported file uploaded to " + serverFile + " - By: " + CurrentUser.Id);

                        DataTable dsData = ExcelUtils.ReadExcelData(serverFile, "Sheet1");
                        if (dsData != null) 
                        {
                            BusinessService.ImportBroker(dsData, CurrentUser.Id);
                            _logger.Info("Data in file '" + serverFile + "' imported to DB by: " + CurrentUser.Id);
                        }
                    }
                    else
                    {
                        _logger.Error("The file has to be less than " + DEFAULT_MAX_UPLOADED_FILE_SIZE_KB + " KB!");
                    }
                }
                else
                {
                    AddErrorMessage(INCORRECT_FORMAT_DATA_FILE, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Broker Import error: " + ex.ToString());
            }
        }
    }

    protected void btnDeleteCurrentData_Click(object sender, EventArgs e)
    {
        try
        {
            int deleted = BusinessService.DeleteAllBroker();
            _logger.Info("Deleted count=" + deleted + " - By " + CurrentUser.Id);
        }
        catch (Exception ex)
        {
            _logger.Error("Delete Current Data error: " + ex.ToString());
        }
    }
}

