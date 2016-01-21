using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BrokerManager.DataObjects;

namespace BrokerManager.BusinessService
{
    /// <summary>
    /// Summary description for IBusinessService
    /// </summary>
    public interface IBusinessService
    {
        
        CalculationDTO GetCalculationPeriod(string id);
        void InsertCalculationPeriod(CalculationDTO dto);
        void CalculateCommission(string calculPeriodId, DateTime calculPeriod, UserDTO byUser);

        #region Importing Data

        bool IsExistingData(string calculationPeriodId);
        int DeleteTradingData(string calculPeriodId);
        void ImportTradeData(DataTable dataTable, string calculPeriodId);
        void ImportBroker(DataTable dataTable, string updatedBy);
        int DeleteAllBroker();

        #endregion
    }
}

