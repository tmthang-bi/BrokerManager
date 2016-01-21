using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using log4net.Config;

public partial class CalculationResult_BrokerCalculationResult : System.Web.UI.Page
{
    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblResultIDParam.Text = ucCommissionMonthPicker.GetCurrentMonthString();
        }
        else
        {
            lblResultIDParam.Text = ucCommissionMonthPicker.GetSelectedMonthString();
        }
    }

    private void DoFilter()
    {
        string filter = ucBrokerFilter.GetFilterString();
        DataSourceBrokerCalculationResult.FilterExpression = filter;
        gvBrokerResult.DataBind();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DoFilter();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        
        DataView resultTable = (DataView) dataSourceResult.Select(DataSourceSelectArguments.Empty);
        ExcelUtils.ExportExcelResult(resultTable);


        /*
        gvBrokerResult.AllowPaging = false;
        //DataSourceBrokerList.FilterExpression = ucBrokerFilter.GetFilterString();
        gvBrokerResult.DataBind();

        ArrayList removedColumnList = new ArrayList();
        removedColumnList.Add(0);
        removedColumnList.Add(1);
        ArrayList msoNum2TextColList = new ArrayList();
        msoNum2TextColList.Add(2);
        GridViewExportUtil.Export2Excel("BrokerCommission.xls", gvBrokerResult, removedColumnList, msoNum2TextColList, false);

        gvBrokerResult.AllowPaging = true;
        gvBrokerResult.DataBind();
        */
    }

    protected void gvBrokerResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DoFilter();
    }
}
