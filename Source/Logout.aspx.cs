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
using log4net;
using log4net.Config;

public partial class Logout : System.Web.UI.Page
{
    public string PAGE_DETAIL_TITLE = " - Logout page";
    public string LOG_HEADER = "Logout - ";

    private readonly ILog logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER); //log4net logger

    protected void Page_Load(object sender, EventArgs e)
    {
        Session[Constants.SESSION_KEY_USER] = null;
        Session[Constants.SESSION_KEY_USERNAME] = null;
        Response.Redirect("Login.aspx");
    }
}
