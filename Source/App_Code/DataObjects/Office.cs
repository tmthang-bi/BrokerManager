using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for Broker
/// </summary>
namespace BrokerManager.DataObjects
{
    public class Office
    {
        #region Constants
        public const string BRANCH_ID_HCM = "HCM";
        public const string BRANCH_ID_HN = "HN";
        #endregion
               

        #region Fields
        private string _id;
        private Branch _branch;

        
        private string _name;
        private string _updateBy;
        private DateTime _updateTime;

        private OfficeCalculationResult _result;
        
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Branch Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        public OfficeCalculationResult Result
        {
            get { return _result; }
            set { _result = value; }
        }
        #endregion

        #region Constructor
        public Office(string id)
        {
            this.Id = id;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public bool IsBranch(string branchID)
        {
            if ((this.Branch != null) && (this.Branch.Id != null))
            {
                if (this.Branch.Id.Equals(branchID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}