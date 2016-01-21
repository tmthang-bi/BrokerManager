using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class BrokerManagement_BrokerList : System.Web.UI.Page
{
    // here we keep the SqlParameters that we will use to insert new row
    private List<SqlParameter> _insertParameters = new List<SqlParameter>();
    protected List<SqlParameter> InsertParameters
    {
        get { return _insertParameters; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string CorrectAndGetText(TextBox txb)
    {
        if (txb == null)
        {
            return string.Empty;
        }

        txb.Text = txb.Text.ToUpper();
        return txb.Text;
    }

    protected void grvBroker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void DataSourceBrokerList_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string filter = ucBrokerFilter.GetFilterString();
        DataSourceBrokerList.FilterExpression = filter;
        grvBroker.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        grvBroker.AllowPaging = false;
        //DataSourceBrokerList.FilterExpression = ucBrokerFilter.GetFilterString();
        grvBroker.DataBind();

        ArrayList msoNum2TextColList = new ArrayList();
        msoNum2TextColList.Add(0);
        GridViewExportUtil.Export2Excel("BrokerList.xls", grvBroker, null, msoNum2TextColList, false);

        grvBroker.AllowPaging = true;
        grvBroker.DataBind();
    }
    
}
