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

public partial class Report_AEReport : System.Web.UI.Page
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

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string filter = ucBrokerFilter.GetFilterString();
        DataSourceBrokerCalculationResult.FilterExpression = filter;
        gvBrokerResult.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        gvBrokerResult.AllowPaging = false;
        //DataSourceBrokerList.FilterExpression = ucBrokerFilter.GetFilterString();
        gvBrokerResult.DataBind();

        ArrayList removedColumnList = new ArrayList();
        removedColumnList.Add(0);
        ArrayList msoNum2TextColList = new ArrayList();
        msoNum2TextColList.Add(3);
        GridViewExportUtil.Export2Excel("AEReport.xls", gvBrokerResult, removedColumnList, msoNum2TextColList, false);

        gvBrokerResult.AllowPaging = true;
        gvBrokerResult.DataBind();
    }

}
