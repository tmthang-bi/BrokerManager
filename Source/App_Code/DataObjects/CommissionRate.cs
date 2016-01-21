using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for CommissionRate
/// </summary>
namespace BrokerManager.DataObjects
{
    public class CommissionRate
    {
        #region Fields
        private int _id;
        private long _lowerLimit = long.MinValue;
        private long _upperLimit = long.MaxValue;
        private double _commissionrate = 0.0;
        int _clientTypeId;
        string _code;
        private string _updateBy;
        private DateTime _updateTime;
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public long UpperLimit
        {
            get { return _upperLimit; }
            set { _upperLimit = value; }
        }
        public long LowerLimit
        {
            get { return _lowerLimit; }
            set { _lowerLimit = value; }
        }
        public double Commissionrate
        {
            get { return _commissionrate; }
            set { _commissionrate = value; }
        }
        public string UpdateBy
        {
            get { return _updateBy; }
            set { _updateBy = value; }
        }
        public int ClientTypeId
        {
            get { return _clientTypeId; }
            set { _clientTypeId = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
        #endregion

        #region Constructor
        public CommissionRate() { }

        public CommissionRate(int clientTypeId, string code)
        {
            ClientTypeId = clientTypeId;
            Code = code;
        }
        #endregion

    }
}