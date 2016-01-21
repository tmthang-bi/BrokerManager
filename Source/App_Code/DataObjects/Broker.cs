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
    public class Broker
    {
        #region Fields
        private string _id;
        private BrokerType _type;
        private Office _office;
        private Broker _supervisor;
        private Broker _supporter;
        private string _name;
        private DateTime _entranceDate;
        private DateTime _leaveDate;
        private bool _isActive;
        private string _email;
        private string _updateBy;
        private DateTime _updateTime;
        private string _brokerTypeId;
        private string _supId;
        private string _supporterId;

        private string _aeCommissionCode;
        private string _supCommissionCode;
        private string _manCommissionCode;
        
        private Hashtable _hashSubordinate = new Hashtable();
        private Hashtable _hashSupporting = new Hashtable();
        private List<ClientTradeData> _clientTradeData = new List<ClientTradeData>();

        private BrokerRate _adjustRate = null;

        private BrokerCalculationResult _paymentResult;

        
        #endregion

        #region Properties
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string AECommissionCode
        {
            get { return _aeCommissionCode; }
            set { _aeCommissionCode = value; }
        }
        public string SUPCommissionCode
        {
            get { return _supCommissionCode; }
            set { _supCommissionCode = value; }
        }
        public string MANCommissionCode
        {
            get { return _manCommissionCode; }
            set { _manCommissionCode = value; }
        }
        public string BrokerTypeId
        {
            get { return _brokerTypeId; }
            set { _brokerTypeId = value; }
        }
        public string SuppervisorId
        {
            get { return _supId; }
            set { _supId = value; }
        }
        public string SupporterId
        {
            get { return _supporterId; }
            set { _supporterId = value; }
        }
        public BrokerType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Office Office
        {
            get { return _office; }
            set { _office = value; }
        }
        public Broker Supervisor
        {
            get { return _supervisor; }
            set { _supervisor = value; }
        }
        public Broker Supporter
        {
            get { return _supporter; }
            set { _supporter = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public DateTime EntranceDate
        {
            get { return _entranceDate; }
            set { _entranceDate = value; }
        }
        public DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
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
        
        public Hashtable HashSubordinate
        {
            get { return _hashSubordinate; }
            set { _hashSubordinate = value; }
        }

        public Hashtable HashSupporting 
        {
            get { return _hashSupporting; }
            set { _hashSupporting = value; }
        }
        public List<ClientTradeData> ClientTradeData
        {
            get { return _clientTradeData; }
            set { _clientTradeData = value; }
        }

        public BrokerRate AdjustRate
        {
            get { return _adjustRate; }
            set { _adjustRate = value; }
        }

        public BrokerCalculationResult PaymentResult
        {
            get { return _paymentResult; }
            set { _paymentResult = value; }
        }
        
        #endregion

        #region Constructor
        public Broker(string id)
        {
            this.Id = id;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public bool IsBrokerType(string typeID)
        {
            if ((BrokerTypeId != null && BrokerTypeId.Equals(typeID)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAccountExecutive()
        {
            if ((_brokerTypeId != null) 
                && (_brokerTypeId == Constants.BROKERTYPE_AE))
            {
                return true;
            }
            return false;
        }
        public bool IsSupervisor()
        {
            if ((_brokerTypeId != null) && (_brokerTypeId == Constants.BROKERTYPE_SUP))
            {
                return true;
            }
            return false;
        }
        public bool IsSupporter()
        {
            if (!string.IsNullOrEmpty(_aeCommissionCode)  && (_aeCommissionCode.Trim() == Constants.BROKERTYPE_SUPPORTER))
            {
                return true;
            }
            return false;
        }
        public bool IsManager()
        {
            if ((_brokerTypeId != null) && (_brokerTypeId == Constants.BROKERTYPE_MAN))
            {
                return true;
            }
            return false;
        }
        public void JoinGroup(Broker groupSupervisor)
        {
            if (groupSupervisor != null)
            {
                this.Supervisor = groupSupervisor;
                groupSupervisor.HashSubordinate[this.Id] = this;
            }
        }
        public void JoinSupporterGroup(Broker groupSupporter)
        {
            if (groupSupporter != null)
            {
                this.Supporter = groupSupporter;
                groupSupporter.HashSupporting[this.Id] = this;
            }
        }
        #endregion

    }
}