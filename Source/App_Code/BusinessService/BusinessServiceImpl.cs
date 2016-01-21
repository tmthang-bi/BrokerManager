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
    /// Summary description for BusinessServiceImpl
    /// </summary>
    public class BusinessServiceImpl : IBusinessService
    {
        public BusinessServiceImpl()
        {}

        private CommissionCalculationService _calculationService = null;
        private BizClientTradeDataImport _clientTradeDataService = null;
        private BizBrokerImport _brokerImportingService = null;
        private CommissionCalculationService CalculationService
        {
            get
            {
                if (_calculationService == null)
                {
                    _calculationService = new CommissionCalculationService();
                }
                return _calculationService;
            }
        }

        private BizClientTradeDataImport ClientTradeDataImport
        {
            get
            {
                if (_clientTradeDataService == null)
                {
                    _clientTradeDataService = new BizClientTradeDataImport();
                }
                return _clientTradeDataService;
            }
        }

        private BizBrokerImport BrokerImportingService 
        {
            get 
            {
                if (_brokerImportingService == null) 
                {
                    _brokerImportingService = new BizBrokerImport();
                }
                return _brokerImportingService;
            }
        }

        public void CalculateCommission(string calculPeriodId, DateTime calculPeriod, UserDTO byUser) 
        {
            CalculationService.Calculate(calculPeriodId, calculPeriod, byUser);
        }

        public CalculationDTO GetCalculationPeriod(string id)
        {
            return CalculationService.GetCalculationPeriod(id);
        }

        public void InsertCalculationPeriod(CalculationDTO dto)
        {
            CalculationService.InsertCalculationPeriod(dto);
        }

        #region Importing Data

        public bool IsExistingData(string calculationPeriodId) 
        {
            return ClientTradeDataImport.IsExistingData(calculationPeriodId);
        }
        public int DeleteTradingData(string calculPeriodId) 
        {
            return ClientTradeDataImport.DeleteTradingData(calculPeriodId);
        }
        public void ImportTradeData(DataTable dataTable, string calculPeriodId) 
        {
            ClientTradeDataImport.ImportTradeData(dataTable, calculPeriodId);
        }
        public void ImportBroker(DataTable dataTable, string updatedBy) 
        {
            BrokerImportingService.Import(dataTable, updatedBy);
        }
        public int DeleteAllBroker() 
        {
            return BrokerImportingService.DeleteAllBroker();
        }
        #endregion
               
    }
}
