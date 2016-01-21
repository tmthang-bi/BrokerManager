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
    public class Branch
    {
       
        #region Constants
        public const string BRANCH_ID_HCM = "HCM";
        public const string BRANCH_ID_HN = "HN";
        #endregion


        #region Fields
        private string _id;
        private string _name;
        private string _description;
        private string _updateBy;
        private DateTime _updateTime;

        private BranchCalculationResult _result = null;
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
          get { return _description; }
          set { _description = value; }
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

        public BranchCalculationResult Result
        {
            get { return _result; }
            set { _result = value; }
        }
        #endregion

        #region Constructor
        public Branch(string id)
        {
            this.Id = id;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public bool IsBranch(string branchID)
        {
            if (this.Id != null)
            {
                if (this.Id.Equals(branchID))
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

        static void main() 
        {
            int result = 0;
            for (int i = 0; i <= 500; i++) 
            {
                if (i % 3 == 0) 
                {
                    result += i;
                }
            }
            Console.WriteLine("Tong la: {0}", result);
        }
        #endregion

    }
}