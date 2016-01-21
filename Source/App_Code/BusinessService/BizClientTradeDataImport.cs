using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataObjects;
using BrokerManager.DataAccess;
using log4net;
using log4net.Config;
using System.IO;

namespace BrokerManager.BusinessService
{
    /// <summary>
    /// Summary description for BizClientTradeDataImport
    /// </summary>
    public class BizClientTradeDataImport
    {
        private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);

        private List<ClientTradeData> _importData = new List<ClientTradeData>();

        private ClientTradeDataDAL _clientTradeDataDAL = null;

        public ClientTradeDataDAL ClientTradeDataAccess
        {
            get
            {
                if (_clientTradeDataDAL == null)
                {
                    _clientTradeDataDAL = new ClientTradeDataDAL();
                }
                return _clientTradeDataDAL;
            }
        }

        public bool IsExistingData(string calculationPeriodId) 
        {
            return ClientTradeDataAccess.IsExistingData(calculationPeriodId);
        }


        public void ImportTradeData(DataTable dataTable, string calculPeriodId)
        {
            if (dataTable.Columns.Count < 5)
            {
                throw new InvalidDataException("Imported data column count missmatched! Required 5 columns but available " + dataTable.Columns.Count);
            }
            
            List<ClientTradeData> dataList = new List<ClientTradeData>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][0] != DBNull.Value)
                {
                    string brokerId = dataTable.Rows[i][0].ToString().Trim();
                    if (brokerId.Length > 0)
                    {
                        ClientTradeData oldClientTrade = new ClientTradeData();
                        oldClientTrade.CalculPeriodId = calculPeriodId;
                        oldClientTrade.BrokerId = brokerId;
                        oldClientTrade.ClientTypeId = ClientType.CLIENTTYPE_ID_FROMHSC;

                        decimal oldTrdValueHN = 0;
                        decimal oldCommissionHN = 0;
                        bool isHN = false;
                        if (dataTable.Rows[i][5] != DBNull.Value && Convert.ToString(dataTable.Rows[i][5]).Trim().ToUpper() == "NORTHERN")
                        {
                            isHN = true;
                        }
                        
                        if (dataTable.Rows[i][1] != DBNull.Value)
                        {
                            if (isHN)
                            {
                                oldClientTrade.TradingValue = 0;
                                oldTrdValueHN = Convert.ToDecimal(dataTable.Rows[i][1]);
                            }
                            else oldClientTrade.TradingValue = Convert.ToDecimal(dataTable.Rows[i][1]);
                        }
                        else
                        {
                            oldClientTrade.TradingValue = 0;
                        }
                        if (dataTable.Rows[i][2] != DBNull.Value)
                        {
                            if (isHN)
                            {
                                oldClientTrade.Commission = 0;
                                oldCommissionHN = Convert.ToDecimal(dataTable.Rows[i][2]);
                            }
                            else oldClientTrade.Commission = Convert.ToDecimal(dataTable.Rows[i][2]);
                        }
                        else
                        {
                            oldClientTrade.Commission = 0;
                        }
                        oldClientTrade.Date = DateTime.Now;
                        dataList.Add(oldClientTrade);

                        ClientTradeData newClientTrade = new ClientTradeData();
                        newClientTrade.BrokerId = brokerId;
                        newClientTrade.CalculPeriodId = calculPeriodId;
                        newClientTrade.ClientTypeId = ClientType.CLIENTTYPE_ID_BYBROKER;
                        if (dataTable.Rows[i][3] != DBNull.Value)
                        {
                            newClientTrade.TradingValue = Convert.ToDecimal(dataTable.Rows[i][3]);
                            if (isHN) newClientTrade.TradingValue += oldTrdValueHN;
                        }
                        else
                        {
                             newClientTrade.TradingValue = 0;
                            if (isHN) newClientTrade.TradingValue += oldTrdValueHN;
                        }
                        
                        if (dataTable.Rows[i][4] != DBNull.Value)
                        {
                            newClientTrade.Commission = Convert.ToDecimal(dataTable.Rows[i][4]);
                            if (isHN) newClientTrade.Commission += oldCommissionHN;
                        }
                        else
                        {
                            newClientTrade.Commission = 0;
                            if (isHN) newClientTrade.Commission += oldCommissionHN;
                        }
                        newClientTrade.Date = DateTime.Now;
                        dataList.Add(newClientTrade);
                    }
                }
            }

            for (int i = 0; i < dataList.Count; i++)
            {
                int inserted = ClientTradeDataAccess.InsertClientTradeData(dataList[i]);
                _logger.Debug("Inserted=" + inserted + " - " + dataList[i].ToString());
            }

        }


        public int DeleteTradingData(string calculPeriodId)
        {
            return ClientTradeDataAccess.DeleteTradingData(calculPeriodId);
        }
        
    }
}
