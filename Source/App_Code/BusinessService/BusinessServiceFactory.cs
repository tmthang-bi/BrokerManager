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

namespace BrokerManager.BusinessService
{
    /// <summary>
    /// Summary description for BusinessServiceFactory
    /// </summary>
    public class BusinessServiceFactory
    {
        public BusinessServiceFactory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static IBusinessService CreateBusinessService()
        {
            return new BusinessServiceImpl();
        }
    }
}
