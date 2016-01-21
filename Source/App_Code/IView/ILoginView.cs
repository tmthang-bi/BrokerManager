using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ILoginView
/// </summary>
public interface ILoginView
{
    string UsernameText { get; set;}
    string PasswordText { get; set;}
    string MessageText { get; set;}
    HttpSessionState ClientSession { get;}

}
