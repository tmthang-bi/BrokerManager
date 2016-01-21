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
    public class AllowanceDTO
    {
        #region Fields
        private int _id;
        private string _code;
        private long _lowerLimit = long.MinValue;
        private long _upperLimit = long.MaxValue;
        private int allowance;
        private int salary;
        private string _updateBy;
        private DateTime _updateTime;
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public long LowerLimit
        {
            get { return _lowerLimit; }
            set { _lowerLimit = value; }
        }
        public long UpperLimit
        {
            get { return _upperLimit; }
            set { _upperLimit = value; }
        }
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public int Allowance
        {
            get { return allowance; }
            set { allowance = value; }
        }
        public string UpdateBy
        {
            get { return _updateBy; }
            set { _updateBy = value; }
        }
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
        #endregion

        #region Constructor
        public AllowanceDTO()
        {
        }
        #endregion

        #region Private Methods
        #endregion
    }
}