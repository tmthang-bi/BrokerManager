using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for BrokerType
/// </summary>
namespace BrokerManager.DataObjects
{
    public class OfficeCalculationResult
    {
        #region Global Static Data
        #endregion

        #region Constants
        #endregion

        #region Fields & Properties
        private string _periodId;
        public string PeriodId
        {
            get { return _periodId; }
            set { _periodId = value; }
        }

        private Office _office;
        public Office Office
        {
          get { return _office; }
          set { _office = value; }
        }

        private DateTime _calculatePeriod;
        public DateTime CalculatePeriod
        {
            get { return _calculatePeriod; }
            set { _calculatePeriod = value; }
        }

        private decimal _tradingValue;
        public decimal TradingValue
        {
            get { return _tradingValue; }
            set { _tradingValue = value; }
        }

        private decimal _inheritedTradingValue;
        public decimal InheritedTradingValue
        {
            get { return _inheritedTradingValue; }
            set { _inheritedTradingValue = value; }
        }

        private decimal _privateTradingValue;
        public decimal PrivateTradingValue
        {
            get { return _privateTradingValue; }
            set { _privateTradingValue = value; }
        }

        private decimal _grossRevenue;
        public decimal GrossRevenue
        {
            get { return _grossRevenue; }
            set { _grossRevenue = value; }
        }

        private decimal _inheritedGrossRevenue;
        public decimal InheritedGrossRevenue
        {
            get { return _inheritedGrossRevenue; }
            set { _inheritedGrossRevenue = value; }
        }

        private decimal _privateGrossRevenue;
        public decimal PrivateGrossRevenue
        {
            get { return _privateGrossRevenue; }
            set { _privateGrossRevenue = value; }
        }

        private decimal _netRevenue;
        public decimal NetRevenue
        {
          get { return _netRevenue; }
          set { _netRevenue = value; }
        }

        private decimal _inheritedNetRevenue;
        public decimal InheritedNetRevenue
        {
            get { return _inheritedNetRevenue; }
            set { _inheritedNetRevenue = value; }
        }

        private decimal _privateNetRevenue;
        public decimal PrivateNetRevenue
        {
            get { return _privateNetRevenue; }
            set { _privateNetRevenue = value; }
        }
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public OfficeCalculationResult(Office office)
        {
            this.Office = office;
        }
        #endregion

        #region Private Methods
        #endregion

        
            
        
    }
}