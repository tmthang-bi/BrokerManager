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

public partial class UserControl_ucCommissionMonthPicker : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitGUI();
        }
    }

    private void InitGUI()
    {
        string sYear = DateTime.Now.ToString("yyyy");
        string sMonth = DateTime.Now.ToString("MM");
        ddlMonth.SelectedValue = sMonth;
        ddlYear.SelectedValue = sYear;
    }

    public string GetCurrentMonthString()
    {
        string sYear = DateTime.Now.ToString("yyyy");
        string sMonth = DateTime.Now.ToString("MM");
        return (sYear + sMonth);
    }

    public string GetSelectedMonthString()
    {
        string ret = string.Empty;
        string sYear = ddlYear.SelectedValue;
        string sMonth = ddlMonth.SelectedValue;
        ret = sYear + sMonth;
        return ret;
    }
}
