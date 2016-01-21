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
    public class BrokerType
    {
        #region Constants
        public const string BROKERTYPE_ID_AE = "AE";
        public const string BROKERTYPE_ID_SUPERVISOR = "SUP";
        public const string BROKERTYPE_ID_MANAGER = "MAN";
        public const string BROKERTYPE_ID_MANAGINGDIRECTOR = "MD";
        #endregion

        #region Fields
        private string _id;
        private string _name;
        private string _description;
        private string _level;
        private string _updateBy;
        private DateTime _updateTime;
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
        public string Level
        {
            get { return _level; }
            set { _level = value; }
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
        public BrokerType(string id)
        {
            this.Id = id;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public bool IsSameType(string typeId)
        {
            return this.Id.Equals(typeId);
        }
        #endregion
    }
}