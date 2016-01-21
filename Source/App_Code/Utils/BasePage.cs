using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BrokerManager.BusinessService;
using BrokerManager.DataObjects;

/// <summary>
/// Summary description for BasePage
/// </summary>
namespace HSCBrokerManagement.Utils
{
    public class BasePage : System.Web.UI.Page 
    {
        public static string BROKERLIST_UPDATE_SUPERVISOR = "UpdateSupervisor";
        public static string CLIENTTRADEDATA_MAP_BROKER = "MapRawTradeData";

        private IBusinessService _businessService;

        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        ///// <summary>
        ///// Clear the session 
        ///// </summary>
        //public bool IsClearSessionPage()
        //{
        //    if (GetRequestParamValue("sessionClear") != null)
        //    {
        //        string value = GetRequestParamValue("sessionClear");
        //        if (value.Equals("N"))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        //public string KeepCallbackSession(string callbackUrl)
        //{
        //    if (callbackUrl.EndsWith(".aspx"))
        //    {
        //        return callbackUrl + "?sessionClear=N";
        //    }
        //    else
        //    {
        //        return callbackUrl + "&sessionClear=N";
        //    }
        //}
        public UserDTO CurrentUser
        {
            get { return (UserDTO)Session[Constants.SESSION_KEY_USER]; }
            set { Session[Constants.SESSION_KEY_USER] = value; }
        }

        //public HttpSessionState ClientSession
        //{ get { return Session; } }

        public IBusinessService BusinessService
        {
            get
            {
                if (_businessService == null)
                {
                    _businessService = BusinessServiceFactory.CreateBusinessService();
                }
                return _businessService;
            }
        }

        //public String GetRequestParamValue(string paramKey)
        //{
        //    return Request[paramKey];
        //}

        /// <summary>
        /// Add error message to show on screen
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="parameters"></param>
        protected void AddErrorMessage(string msgId, string[] parameters)
        {
            if (string.IsNullOrEmpty(msgId))
            {
                return;
            }
            string mess = null;

            if (GetGlobalResourceObject(Constants.ErrorMessageResourceFileName, msgId) != null)
            {
                mess = GetGlobalResourceObject(
                   Constants.ErrorMessageResourceFileName,
                   msgId).ToString();
                if (parameters != null && parameters.Length > 0)
                {
                    mess = string.Format(mess, parameters);
                }
            }
            else
            {
                mess = "The message is not defined in resource file";
            }

            Session[Constants.ErrorMessage] = mess;
        }

        /// <summary>
        /// Add information message to show on screen
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="parameters"></param>
        protected void AddInforMessage(string msgId, string[] parameters)
        {
            if (string.IsNullOrEmpty(msgId))
            {
                return;
            }
            string mess = null;
            if (GetGlobalResourceObject(Constants.ErrorMessageResourceFileName, msgId) != null)
            {
                mess = GetGlobalResourceObject(
                   Constants.ErrorMessageResourceFileName,
                   msgId).ToString();
                if (parameters != null && parameters.Length > 0)
                {
                    mess = string.Format(mess, parameters);
                }
            }
            else
            {
                mess = "The message is not defined in resource file";
            }

            Session[Constants.InforMessage] = mess;
        }
        /// <summary>
        /// Get message content from resources
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //protected string GetMessage(string key)
        //{
        //    string message = "Can not get message from resources...";

        //    if (GetGlobalResourceObject(
        //          Constants.MessageResourceFileName, key) != null)
        //    {
        //        message = GetGlobalResourceObject(
        //           Constants.MessageResourceFileName,
        //           key).ToString();
        //    }
        //    return message;
        //}

        protected static void Redirect(string pageName)
        {
            HttpContext.Current.Response.Redirect(pageName);
        }


        ///// <summary>
        ///// Redirect a page using Response.Redirect
        ///// </summary>
        ///// <param name="request">the new page name</param>
        //protected static void RedirectToMe(HttpRequest request)
        //{
        //    HttpContext.Current.Response.Redirect
        //       (RequestUtils.GetOriginalURL(request));
        //}

        ///// <summary>
        ///// Redirect a page using Response.Redirect
        ///// </summary>
        ///// <param name="request">the new page name</param>
        //protected static void RedirectToAccessDeniedPage(HttpRequest request)
        //{
        //    HttpContext.Current.Response.Redirect(Common.AccessDeniedPage);
        //}
    }
}