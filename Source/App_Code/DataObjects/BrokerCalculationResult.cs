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
    public class BrokerCalculationResult
    {
        #region Fields & Properties
        private string _periodId;
        public string PeriodId
        {
            get { return _periodId; }
            set { _periodId = value; }
        }

        private string _brokerId;
        public string BrokerId
        {
            get { return _brokerId; }
            set { _brokerId = value; }
        }

        private DateTime _calculatePeriod;
        public DateTime CalculatePeriod
        {
            get { return _calculatePeriod; }
            set { _calculatePeriod = value; }
        }

        private decimal _payment;
        public decimal Payment
        {
            get { return _payment; }
            set { _payment = value; }
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

        private decimal _averageNetRevenue;
        public decimal AverageNetRevenue
        {
            get { return _averageNetRevenue; }
            set { _averageNetRevenue = value; }
        }

        private double _inheritedcommissionRate;
        public double InheritedCommissionRate
        {
            get { return _inheritedcommissionRate; }
            set { _inheritedcommissionRate = value; }
        }
        private double _privatecommissionRate;
        public double PrivateCommissionRate
        {
            get { return _privatecommissionRate; }
            set { _privatecommissionRate = value; }
        }
        private double _privatecommissionRate1;
        public double PrivateCommissionRate1
        {
            get { return _privatecommissionRate1; }
            set { _privatecommissionRate1 = value; }
        }
        private double _privatecommissionRate2;
        public double PrivateCommissionRate2
        {
            get { return _privatecommissionRate2; }
            set { _privatecommissionRate2 = value; }
        }

        private decimal _inheritedCommissionPayment;
        public decimal InheritedCommissionPayment
        {
            get { return _inheritedCommissionPayment; }
            set { _inheritedCommissionPayment = value; }
        }
        private decimal _privateCommissionPayment;
        public decimal PrivateCommissionPayment
        {
            get { return _privateCommissionPayment; }
            set { _privateCommissionPayment = value; }
        }

        private double _managementBonusRate;
        public double ManagementBonusRate
        {
            get { return _managementBonusRate; }
            set { _managementBonusRate = value; }
        }

        private decimal _supervisorPayment;
        public decimal SupervisorPayment
        {
            get { return _supervisorPayment; }
            set { _supervisorPayment = value; }
        }

        private decimal _otherPayment;
        public decimal OtherPayment
        {
            get { return _otherPayment; }
            set { _otherPayment = value; }
        }

        private decimal _subtotalNetRevenue;
        public decimal SubtotalNetRevenue
        {
            get { return _subtotalNetRevenue; }
            set { _subtotalNetRevenue = value; }
        }

        private decimal _subtotalInheritedNetRevenue;
        public decimal SubtotalInheritedNetRevenue
        {
            get { return _subtotalInheritedNetRevenue; }
            set { _subtotalInheritedNetRevenue = value; }
        }

        private decimal _subtotalPrivateNetRevenue;
        public decimal SubtotalPrivateNetRevenue
        {
            get { return _subtotalPrivateNetRevenue; }
            set { _subtotalPrivateNetRevenue = value; }
        }

        private decimal _subtotalTradingValue;
        public decimal SubtotalTradingValue
        {
            get { return _subtotalTradingValue; }
            set { _subtotalTradingValue = value; }
        }

        private decimal _subtotalInheritedTradingValue;
        public decimal SubtotalInheritedTradingValue
        {
            get { return _subtotalInheritedTradingValue; }
            set { _subtotalInheritedTradingValue = value; }
        }

        private decimal _subtotalPrivateTradingValue;
        public decimal SubtotalPrivateTradingValue
        {
            get { return _subtotalPrivateTradingValue; }
            set { _subtotalPrivateTradingValue = value; }
        }
                
        #endregion

        #region Properties
        
        #endregion

        #region Constructor
        public BrokerCalculationResult()
        {
            
        }
        #endregion

        #region Private Methods
        #endregion

    }
}