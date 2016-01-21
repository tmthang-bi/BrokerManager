using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataAccess;

/// <summary>
/// Summary description for ClientType
/// </summary>
namespace BrokerManager.DataObjects
{
    public class ClientType
    {
    
        #region Constants
        public const int CLIENTTYPE_ID_FROMHSC = 1;
        public const int CLIENTTYPE_ID_BYBROKER = 2;
        #endregion

        #region Fields
        private int _id;
        private string _name;
        private string _description;
        private string _updateBy;
        private DateTime _updateTime;
        #endregion

        #region Properties
        public int Id
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
        #endregion

        #region Constructor
        public ClientType()
        {
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public bool IsSameType(int typeId)
        {
            return this.Id == typeId;
        }
        #endregion
    }
}