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
using BrokerManager.DataAccess;

public partial class UserControl_ucBrokerFilter : System.Web.UI.UserControl
{
    string defaultBrokerType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (defaultBrokerType.Equals("AE")) 
        {
            ddlFilterBrokerTypeID.SelectedValue = "AE";
            ddlFilterBrokerTypeID.Enabled = false;
        }
        else if (defaultBrokerType.Equals("MAN")) 
        {
            ddlFilterBrokerTypeID.SelectedValue = "MAN";
            ddlFilterBrokerTypeID.Enabled = false;
        }
        else if (defaultBrokerType.Equals("MD"))
        {
            ddlFilterBrokerTypeID.SelectedValue = "MD";
            ddlFilterBrokerTypeID.Enabled = false;
        }
        else if (defaultBrokerType.Equals("SUP"))
        {
            ddlFilterBrokerTypeID.SelectedValue = "SUP";
            ddlFilterBrokerTypeID.Enabled = false;
        }
    }
    
    public string GetFilterString()
    {
        BrokerDAL dal = new BrokerDAL();
        string ret = string.Empty;
        try
        {
            ret = dal.GetFilterString(txbFilterID.Text.Trim(),
                ddlFilterBrokerTypeID.SelectedValue,
                ddlFilterOffice.SelectedValue,
                txbFilterName.Text.Trim(),
                txbFilterSupervisor.Text.Trim());
        }
        catch (Exception)
        {
        }
        return ret;
    }

    public String BrokerType
    {
        get { return defaultBrokerType; }
        set { defaultBrokerType = value; }
    }
}
