using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BrokerManager.DataObjects;
using log4net;
using log4net.Config;
using BrokerManager.DataAccess;

namespace BrokerManager.BusinessService
{
    /// <summary>
    /// Summary description for CommissionModal
    /// </summary>
    public class BrokerCommissionModal
    {
        #region Private Fields
        private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);
        private MasterDataDAL _masterDataDAL = null;
        private BrokerDAL _brokerDAL = null;
        private MasterDataDAL MasterDataAccess 
        {
            get 
            {
                if (_masterDataDAL == null) 
                {
                    _masterDataDAL = new MasterDataDAL();
                }
                return _masterDataDAL;
            }
        }
        private BrokerDAL BrokerDataAccess 
        {
            get 
            {
                if (_brokerDAL == null) 
                {
                    _brokerDAL = new BrokerDAL();
                }
                return _brokerDAL;
            }
        }
        #endregion

        #region Properties
        
        private Hashtable _hashClientType;
        public Hashtable HashClientType
        {
            get { return _hashClientType; }
            //set { _hashClientType = value; }
        }

        private Hashtable _hashBrokerType;
        public Hashtable HashBrokerType
        {
            get { return _hashBrokerType; }
            //set { _hashBrokerType = value; }
        }

        private Hashtable _hashBranch;
        public Hashtable HashBranch
        {
            get { return _hashBranch; }
            //set { _hashBranch = value; }
        }

        private Hashtable _hashOffice;
        public Hashtable HashOffice
        {
            get { return _hashOffice; }
            //set { _hashOffice = value; }
        }

        private List<CommissionRate> _listCommissionRate;
        public List<CommissionRate> ListCommissionRate
        {
            get { return _listCommissionRate; }
            //set { _listCommissionRate = value; }
        }

        private List<CommissionRate1> _listCommissionRate1;
        public List<CommissionRate1> ListCommissionRate1
        {
            get { return _listCommissionRate1; }
            //set { _listCommissionRate = value; }
        }

        private List<AllowanceDTO> _listAllowance;
        public List<AllowanceDTO> ListAllowance
        {
            get { return _listAllowance; }
            //set { _listCommissionRate = value; }
        }
        private Hashtable _hashBroker;
        public Hashtable HashBroker
        {
            get { return _hashBroker; }
            //set { _hashBroker = value; }
        }

        #endregion

        #region Constructor
        public BrokerCommissionModal()
        {
        }
        #endregion

        #region Private Methods
        private void LinkOffice2Branch()
        {
            // Link offices to their branches
            foreach (string id in this.HashOffice.Keys)
            {
                Office office = (Office)this.HashOffice[id];
                if (office != null)
                {
                    if ((office.Branch != null) && (office.Branch.Id != null))
                    {
                        Branch branch = (Branch)this.HashBranch[office.Branch.Id];
                        if (branch != null)
                        {
                            office.Branch = branch;
                        }
                        else
                        {
                            throw new InvalidDataException("Null branch object on BranchID=" + office.Branch.Id + " in the HashBranch of the BrokerCommissionModal data");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("Office's branch information is invalid on OfficeID=" + id + " in the HashOffice of the BrokerCommissionModal data");
                    }
                }
                else
                {
                    throw new InvalidDataException("Null office object on OfficeID=" + id + " in the HashOffice of the BrokerCommissionModal data");
                }
            }
        }

        private void LinkBroker()
        {
            // Link Brokers to their data
            foreach (string id in this.HashBroker.Keys)
            {
                Broker brk = (Broker)this.HashBroker[id];
                if (brk != null)
                {
                    // Office
                    if ((brk.Office != null) && (brk.Office.Id != null))
                    {
                        Office office = (Office)this.HashOffice[brk.Office.Id];
                        if (office != null)
                        {
                            brk.Office = office;
                        }
                        else
                        {
                            throw new InvalidDataException("Null Office object on OfficeID=" + brk.Office.Id + " in the HashOffice of the BrokerCommissionModal data");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("Broker's Office information is invalid on BrokerID=" + id + " in the HashBroker of the BrokerCommissionModal data");
                    }

                    // Supervisor 
                    if ((brk.Supervisor != null) && (brk.Supervisor.Id != null))
                    {
                        Broker sup = (Broker)this.HashBroker[brk.Supervisor.Id];
                        if (sup != null)
                        {
                            brk.JoinGroup(sup);
                        }
                        else
                        {
                            throw new InvalidDataException("Null Broker object on BrokerID=" + brk.Supervisor.Id + " in the HashBroker of the BrokerCommissionModal data. The ID is referenced by BrokerID=" + brk.Id + " as his supervisor");
                        }
                    }
                    // Supporter
                    if ((brk.Supporter != null) && (brk.Supporter.Id != null))
                    {
                        Broker supporter = (Broker)this.HashBroker[brk.Supporter.Id];
                        if (supporter != null)
                        {
                            brk.JoinSupporterGroup(supporter);
                        }
                        else
                        {
                            throw new InvalidDataException("Null Broker object on BrokerID=" + brk.Supporter.Id + " in the HashBroker of the BrokerCommissionModal data. The ID is referenced by BrokerID=" + brk.Id + " as his supporter");
                        }
                    }
                }
                else
                {
                    throw new InvalidDataException("Null broker object on BrokerID=" + id + " in the HashBroker of the BrokerCommissionModal data");
                }
            }
        }

        private void LinkData()
        {
            LinkOffice2Branch();
            LinkBroker();
        }
        #endregion

        #region Public Methods
        public void LoadData()
        {
            _hashClientType = MasterDataAccess.GetAllClientType();
            _hashBrokerType = MasterDataAccess.GetAllBrokerType();
            _hashBranch = MasterDataAccess.GetAllBranch();
            _hashOffice = MasterDataAccess.GetAllOffice();
            _listCommissionRate = MasterDataAccess.GetAllCommissionRate();
            _listCommissionRate1 = MasterDataAccess.GetAllCommissionRate1();
            _hashBroker = BrokerDataAccess.GetAllBroker();
            _listAllowance = MasterDataAccess.GetAllowanceList();
            LinkData();

            _logger.Info("Data object link completed");
        }

        public CommissionRate FindCommissionRate(string rateCode, int clientTypeId, decimal revenue)
        {
            for (int i = 0; i < ListCommissionRate.Count; i++)
            {
                CommissionRate rate = ListCommissionRate[i];

                if (rateCode != null)
                {
                    if (rateCode.Equals(rate.Code) && (clientTypeId == rate.ClientTypeId))
                    {
                        if ((rate.LowerLimit <= revenue) && (revenue < rate.UpperLimit))
                        {
                            return rate;
                        }
                    }
                }
            }

            return null;
        }
        public CommissionRate1 FindCommissionRate1(string rateCode, int clientTypeId, decimal revenue)
        {
            for (int i = 0; i < ListCommissionRate1.Count; i++)
            {
                CommissionRate1 rate = ListCommissionRate1[i];

                if (rateCode != null)
                {
                    if (rateCode.Equals(rate.Code) && (clientTypeId == rate.ClientTypeId))
                    {
                        if ((rate.LowerLimit <= revenue) && (revenue < rate.UpperLimit))
                        {
                            return rate;
                        }
                    }
                }
            }

            return null;
        }

        public AllowanceDTO GetAllowance(string code, decimal netRevenue)
        {
            for (int i = 0; i < ListAllowance.Count; i++)
            {
                AllowanceDTO allowance = ListAllowance[i];
                if (code != null
                    && (code.Equals(allowance.Code)
                    && (allowance.LowerLimit <= netRevenue)
                    && (netRevenue < allowance.UpperLimit)))
                {
                    return allowance;
                }
            }

            return null;
        }
        #endregion
    }
}
