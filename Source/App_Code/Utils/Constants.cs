using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
    #region Configuration ID
    public const string CONF_ID_LOG_APPENDER = "LogAppender";
    public const string CONF_ID_LOGGER = "Logger";
    public const string CONF_ID_AUTHENTICATION_DOMAIN = "AuthenticationDomain";
    public const string CONF_ID_CONNECTIONSTRING_MYSQL = "MySQL";
    public const string CONF_ID_CONNECTIONSTRING_MSSQLSERVER = "MSSQLServer";
    public const string CONF_ID_CACHE_INTERVAL_SEC = "CacheDataRefreshIntervalSecond";
    #endregion

    #region Global cache ID
    public const string GLOBAL_ID_ACCOUNT_PORTFOLIO = "AccountPortfolioData";    
    #endregion

    #region Session ID
    public const string SESSION_KEY_USER = "Session key User";
    public const string SESSION_KEY_USERNAME = "Session key Username";
    #endregion

    #region Default values
    public const int CACHE_DATA_REFRESH_INTERVAL_SEC = 1200;
    #endregion

    #region Log texts
    public const string TXT_LOG_APP_START = "------------- Application starts -------------";
    public const string TXT_LOG_APP_END = "------------- Application ends  -------------";
    #endregion

    #region BrokerTypeMapping
    public const string BROKERTYPE_AE = "AE";
    public const string BROKERTYPE_SUP = "SUP";
    public const string BROKERTYPE_MAN = "MAN";
    public const string BROKERTYPE_MD = "MD";
    public const string BROKERTYPE_SUPPORTER = "HT0";
    #endregion

    public Constants()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Configuration ID
    
    public const string URL_PREV_PAGE_IMG = "~\\Images\\prev_page.gif";
    public const string URL_NEXT_PAGE_IMG = "~\\Images\\next_page.gif";
    public const string URL_PREV_PAGE_SET_IMG = "~\\Images\\prev_page_set.gif";
    public const string URL_NEXT_PAGE_SET_IMG = "~\\Images\\next_page_set.gif";
    public const string ASCENDING = "ASC";
    public const string DESCENDING = "DES";
    public const string SORT_ASC_IMG_URL = "~\\Images\\prev_page.gif";
    public const string SORT_DES_IMG_URL = "~\\Images\\prev_page.gif";

    public static string DOUBLE_VALUE_FORMAT = "#,#0.00";
    public static string INT_VALUE_FORMAT = "#,#0";
    public static string LONG_VALUE_FORMAT = "#,#0";

    public static string ErrorMessageResourceFileName = "ErrorMessages";
    public static string InforMessageResourceFileName = "InforMessages";
    public static string ErrorMessage = "ERROR_MESS_SESS";
    public static string InforMessage = "INFOR_MESS_SESS";
    
    #endregion

    /// <summary>
    /// Method gets list of pair (monthvalue, monthText).
    /// </summary>
    /// <returns></returns>
    public static ListItem[] GetDataTradeStatus()
    {
        ListItem[] result = new ListItem[3];
        result[0] = new ListItem("", "-1");
        result[1] = new ListItem("Raw Data", "0");
        result[2] = new ListItem("Consolidated", "1");
        return result;
    }

    
}
