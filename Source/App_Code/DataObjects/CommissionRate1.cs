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
    public class CommissionRate1
    {
        #region Fields
        private int _id;
        private long _lowerLimit = long.MinValue;
        private long _upperLimit = long.MaxValue;
        private long _limit = long.MinValue;
        private double _lowerRate = 0.0;
        private double _upperRate = 0.0;
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
        public long Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
        public double LowerRate
        {
            get { return _lowerRate; }
            set { _lowerRate = value; }
        }
        public double UpperRate
        {
            get { return _upperRate; }
            set { _upperRate = value; }
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
        public CommissionRate1() { }

        public CommissionRate1(int clientTypeId, string code)
        {
            ClientTypeId = clientTypeId;
            Code = code;
        }
        #endregion

    }
}