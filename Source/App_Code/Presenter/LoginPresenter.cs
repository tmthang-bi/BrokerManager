using System;
using System.Data;
using System.Configuration;
using log4net;
using log4net.Config;
using BrokerManager.DataObjects;

/// <summary>
/// Summary description for LoginPresenter
/// </summary>
public class LoginPresenter
{
    private ILoginView _loginView;
    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER); //log4net _logger

	public LoginPresenter(ILoginView loginView)
	{
        _loginView = loginView;
	}

    public bool AuthenticateUser(string username, string password)
    {
        bool ret = false;
        try
        {
            UserDTO user = new UserDTO(username, password);
            string authenticationMessage = null;
            UserDTO systemUser = UserDTO.Authenticate(user, out authenticationMessage);
            if (systemUser != null)
            {
                _loginView.ClientSession[Constants.SESSION_KEY_USER] = systemUser;
                _loginView.ClientSession[Constants.SESSION_KEY_USERNAME] = systemUser.Id;
                ret = true;
            }
            else
            {
                ret = false;
                _loginView.ClientSession[Constants.SESSION_KEY_USER] = null;
                _loginView.ClientSession[Constants.SESSION_KEY_USERNAME] = null;
                if (authenticationMessage != null) _loginView.MessageText = authenticationMessage;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ret;
    }
}
