using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for ClientTradeData
/// </summary>
namespace BrokerManager.DataObjects
{
    public class ClientTradeData
    {
        public ClientTradeData(){}
                
        #region Fields
        private ClientType _clientType;
        private Broker _broker;
        private Decimal _tradingValue;
        private Decimal _commission;
        private DateTime _date;
        private string _calculPeriodId;

        private int _clienTypeId;
        private string _brokerId;

        #endregion

        #region Properties
        public ClientType ClientType
        {
            get { return _clientType; }
            set { _clientType = value; }
        }
        public Broker Broker
        {
            get { return _broker; }
            set { _broker = value; }
        }
        public Decimal TradingValue
        {
            get { return _tradingValue; }
            set { _tradingValue = value; }
        }
        public Decimal Commission
        {
            get { return _commission; }
            set { _commission = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public String CalculPeriodId 
        {
            get { return _calculPeriodId; }
            set { _calculPeriodId = value; }
        }
        public String BrokerId 
        {
            get { return _brokerId; }
            set { _brokerId = value; }
        }
        public int ClientTypeId 
        {
            get { return _clienTypeId; }
            set { _clienTypeId = value; }
        }
        #endregion

        #region Constructor
       
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public override string ToString()
        {
            string ret = "ClientType=" + ClientTypeId +
                " BrokerID=" + BrokerId +
                " TradingValue=" + TradingValue.ToString() +
                " Commission=" + Commission.ToString();
            return ret;
        }
        #endregion
    }
}