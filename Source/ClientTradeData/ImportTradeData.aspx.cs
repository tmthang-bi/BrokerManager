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
using System.Text;

public partial class ClientTradeData_ImportTradeData : BasePage
{
    public const int DEFAULT_MAX_UPLOADED_FILE_SIZE_KB = 4096;
    public const int DEFAULT_MAX_UPLOADED_FILE_SIZE_BYTE = (DEFAULT_MAX_UPLOADED_FILE_SIZE_KB * 1024);

    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);

    private static string REQUIRED_DATA_FILE = "ClientTradeData_RequireDataFile";
    private static string INCORRECT_FORMAT_DATA_FILE = "ClientTradeData_IncorrectFormatDataFile";

    // a temp path on sever to upload data excel file before handling
    private const string TEMP_PATH = "\\tradingdata_temp.xls";
    private const string OVERRIDE = "OVERRIDE";
    private const string NONE_OVERRIDE = "NONE_OVERRIDE";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //InitGUI();

        if (!IsPostBack)
        {
            InitDataForControls();
        }
    }

    public DataTable TradingData
    {
        get
        {
            return Session["ImportTradeData_TradingDataTable"] as DataTable;
        }
        set
        {
            Session["ImportTradeData_TradingDataTable"] = value;
        }
    }
    
    /// <summary>
    /// Register confirm message incase existing trading data for current period
    /// </summary>
    private void RegisterScript()
    {
        if (!this.ClientScript.IsClientScriptBlockRegistered("clientScript"))
        {
            // build the script that is to be registered at client side.
            string hiddenId = hiddenDataExit.ClientID;
            string btnImportId = btnImport.ClientID;
            string confirmMsg = "The trading data is already existed, do you want to override?";

            StringBuilder scriptString = new StringBuilder("<script language=JavaScript>");
            scriptString.Append("if(window.confirm(\"" + confirmMsg + "\"))");
            scriptString.Append(" document.getElementById(\"" + hiddenId
                        + "\").value = \"" + OVERRIDE + "\";");
            scriptString.Append(" else ");
            scriptString.Append(" document.getElementById(\"" + hiddenId
                        + "\").value = \"" + NONE_OVERRIDE + "\";");
            scriptString.Append(" document.getElementById(\"" + btnImportId
                        + "\").click();");
            scriptString.Append("</");
            scriptString.Append("script>");
            ClientScript.RegisterStartupScript(GetType(), "clientScript", scriptString.ToString());
        }
    }

    private void InitDataForControls()
    {
        //lnkBack.PostBackUrl = ReturnUrl;
        //ReturnUrl = GetRequestParamValue("returnURL");
        cmbMonth.SelectedValue = DateTime.Now.ToString("MM");
        cmbYear.SelectedValue = DateTime.Now.ToString("yyyy"); 
        pnlImport.Visible = false;

    }
    
    protected void BtnRead_OnClick(object sender, EventArgs e)
    {
        // check if the input file is excel format file
        if (uploadTradingData.PostedFile.FileName == string.Empty)
        {
            // show message that the imported file is not an excel file
            AddErrorMessage(REQUIRED_DATA_FILE, null);
            return;
        }
        else if (!uploadTradingData.PostedFile.FileName.EndsWith(".xls"))
        {
            // show message that the imported file is not an excel file
            AddErrorMessage(INCORRECT_FORMAT_DATA_FILE, null);
            return;
        }
        else
        {

            string filename = Path.GetFileName(uploadTradingData.PostedFile.FileName);
            string serverFile = Server.MapPath("~/Upload/") + filename;

            uploadTradingData.PostedFile.SaveAs(serverFile);
            _logger.Info("Imported file uploaded to " + serverFile + " - By: " + CurrentUser.Id);

            DataTable tradingDataTB = new DataTable();
            try
            {
                DataTable dsTradeData = ExcelUtils.ReadExcelData(serverFile, "Sheet1");
                //tradingDataTB.Columns.Add(new DataColumn());
                //store in session
                TradingData = dsTradeData;
                UploadGridData();
            }
            catch (Exception ex)
            {
                //add error msg
                AddErrorMessage(REQUIRED_DATA_FILE, null);
                _logger.Fatal("Error: " + ex.ToString());
            }

            pnlImport.Visible = true;
        }

    }

    private void UploadGridData()
    {
        grdTradingData.DataSource = TradingData;
        grdTradingData.DataBind();
    }
    
    public void BtnImport_OnClick(object sender, EventArgs e)
    {
        string calculationPeriodId = cmbYear.SelectedValue + cmbMonth.SelectedValue;
        
        if (OVERRIDE.Equals(hiddenDataExit.Value))
        {
            // incase user choose "yes" of confirm message
            hiddenDataExit.Value = "";
            BusinessService.DeleteTradingData(calculationPeriodId);
            BusinessService.ImportTradeData(TradingData, calculationPeriodId);
            UploadGridData();
            AddInforMessage("ClientTradeDataImport_Success", null);
        }
        else if (NONE_OVERRIDE.Equals(hiddenDataExit.Value))
        {
            // incase user choose "no" of confirm message, just display 
            // data from database
            hiddenDataExit.Value = "";

        }
        else
        {
            // check if the trading data has been imported to DB
            if (BusinessService.IsExistingData(calculationPeriodId))
            {
                // show confirm message
                RegisterScript();
                //EnableControls(false);
                return;
            }
            else
            {
                // import data from excel file
                BusinessService.ImportTradeData(TradingData, calculationPeriodId);
                UploadGridData();
                AddInforMessage("ClientTradeDataImport_Success", null);
            }
        }
        
        
    }

}
