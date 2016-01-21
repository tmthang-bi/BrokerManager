using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using log4net.Config;


public partial class Login : System.Web.UI.Page, ILoginView
{
    #region Private Members
    private string _logHeader = "Login - ";
    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER); //log4net _logger
    #endregion

    #region Interface View Implementation
    public string UsernameText
    {
        get { return txbUsername.Text.Trim(); }
        set { txbUsername.Text = value; }
    }
    public string PasswordText
    {
        get { return txbPassword.Text.Trim(); }
        set { txbPassword.Text = value; }
    }
    public string MessageText
    {
        get { return lblMsg.Text; }
        set { lblMsg.Text = value; }
    }
    public HttpSessionState ClientSession
    {
        get { return Session; }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //btnLogin_Click(null, null);
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            LoginPresenter presenter = new LoginPresenter(this);
            if (presenter.AuthenticateUser(UsernameText, PasswordText))
            {
                _logger.Info(this._logHeader + "User login authentication success. Username=" + UsernameText);
                Response.Redirect("~/Report/AEReport.aspx");
            }
            else
            {
                _logger.Warn(this._logHeader + "User login authentication fail. Username=" + UsernameText +
                    " Password=" + PasswordText);
            }
        }
        catch (System.Threading.ThreadAbortException taEx)
        {
            _logger.Warn(this._logHeader + "Redirect causing ThreadAbortException: " + taEx.Message);
        }
        catch (Exception ex)
        {
            DisplayErrorInformation(ex);
            _logger.Fatal(this._logHeader + "User login authentication error: " + ex.ToString());
        }

    }

    private void DisplayErrorInformation(Exception exception)
    {
        if (exception != null)
        {
            lblMsg.Text = "Error";
            lblError.Text = exception.Message;
            lblErrorDetail.Text = exception.ToString();
        }
    }
}
