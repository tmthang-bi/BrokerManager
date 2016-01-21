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

public partial class MasterPage_Main : System.Web.UI.MasterPage
{
    public string PAGE_NAME = "Master page";

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogingSession();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        HandleMessages();
    }

    private Label LblMessage
    {
        get
        {
            return (Label)this.FindControl("_lblMessage");
        }
    }

    /// <summary>
    /// Handle message information
    /// </summary>
    private void HandleMessages()
    {
        if (Session[Constants.ErrorMessage] != null)
        {
            // display error message
            LblMessage.Text = Session[Constants.ErrorMessage] as string;
            LblMessage.CssClass = "error_message";
        }
        else if (Session[Constants.InforMessage] != null)
        {
            // display infor message
            //LblInfoMessage.Text = mess;
            LblMessage.Text = Session[Constants.InforMessage] as string;
            LblMessage.CssClass = "infor_message";
        }
        else
        {
            LblMessage.Text = "";
        }

        Session.Remove(Constants.ErrorMessage);
        Session.Remove(Constants.InforMessage);
    }

  
    
    private void CheckLogingSession()
    {
        UserDTO user = (UserDTO)Session[Constants.SESSION_KEY_USER];
        if (user == null) Response.Redirect("~/Login.aspx");
    }
}
