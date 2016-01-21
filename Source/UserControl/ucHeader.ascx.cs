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
using BrokerManager.DataObjects;

public partial class UserControl_ucHeader : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //CheckLogingSession();
        InitGUI();
    }

    private void CheckLogingSession()
    {
        UserDTO user = (UserDTO)Session[Constants.SESSION_KEY_USER];
        if (user == null) Response.Redirect("Login.aspx");
    }

    private void InitGUI()
    {
        UserDTO user = (UserDTO)Session[Constants.SESSION_KEY_USER];
        //if (user != null) lnkLogout.Text = "Logout " + user.Id;
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session[Constants.SESSION_KEY_USER] = null;
        Session[Constants.SESSION_KEY_USERNAME] = null;
        Response.Redirect("Login.aspx");
    }
}
