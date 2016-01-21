using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataObjects;
using BrokerManager.DataAccess;
using log4net;
using log4net.Config;

namespace BrokerManager.BusinessService
{

    /// <summary>
    /// Summary description for CommissionCalculator
    /// </summary>
    public class CommissionCalculationService
    {

        #region Fields
        private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);

        // hardcode for HOSE/HNX fee that AE need to pay, 0.04% on commission
        private const double MARKET_FEE_RATIO = 0.0004;
        //new calculation method will be applied if the net revenue exceeds this limit
        private const long NEW_CALCULATION_LIMIT = 150000000;
        #endregion

        #region Properties
        private CalculationDAL _calculationDAL = null;
        private ClientTradeDataDAL _clientTradeDataDAL = null;
        public CalculationDAL CalculationDataAccess
        {
            get
            {
                if (_calculationDAL == null)
                {
                    _calculationDAL = new CalculationDAL();
                }
                return _calculationDAL;
            }
        }
        public ClientTradeDataDAL ClientTradeDataAccess
        {
            get
            {
                if (_clientTradeDataDAL == null)
                {
                    _clientTradeDataDAL = new ClientTradeDataDAL();
                }
                return _clientTradeDataDAL;
            }
        }

        #endregion

        #region Constructor

        public CommissionCalculationService() { }
                
        #endregion

        public void Calculate(string calculPeriodId, DateTime calculPeriod, UserDTO byUser) 
        {
            _logger.Info("Calculate - ENTER");

            // Load all master data();
            BrokerCommissionModal brokerDataModal = new BrokerCommissionModal();
            brokerDataModal.LoadData();

            //Get average 3 months commision
            //Hashtable hash3MonthAvgResult = GetAvgNetRevenue(calculPeriod);
            //_logger.Info("3 Month AvgNetRevenue data load completed. Count=" + hash3MonthAvgResult.Count);

            List<ClientTradeData> listClientTradeData = ClientTradeDataAccess.GetAllClientTradeData(calculPeriodId);
            _logger.Info("ClientTradeData data load completed. Count=" + listClientTradeData.Count);

            // Link client trade data and their brokers together
            for (int i = 0; i < listClientTradeData.Count; i++)
            {
                ClientTradeData clientTradeData = listClientTradeData[i];

                if (clientTradeData.BrokerId != null)
                {
                    if (brokerDataModal.HashBroker[clientTradeData.BrokerId] != null)
                    {
                        Broker brk = (Broker)brokerDataModal.HashBroker[clientTradeData.BrokerId];
                        brk.ClientTradeData.Add(clientTradeData);
                    }
                }
            }


            foreach (string brkID in brokerDataModal.HashBroker.Keys)
            {
                Broker broker = (Broker)brokerDataModal.HashBroker[brkID];
                // Create the ResultData object to hold calculation data for the broker (brk)
                BrokerCalculationResult result = new BrokerCalculationResult();
                result.BrokerId = broker.Id;
                result.PeriodId = calculPeriodId;
                result.CalculatePeriod = calculPeriod;
                broker.PaymentResult = result;

                //// check the last 3 month commmision average to identify if this broker have allowance or not
                //if (hash3MonthAvgResult[broker.Id] != null)
                //{
                //    result.AverageNetRevenue = (decimal)hash3MonthAvgResult[broker.Id]; ;
                //}
                //else
                //{
                //    // Mark this to know that there is NO DATA about Average Net Revenue
                //    result.AverageNetRevenue = -1;
                //}
                
                CalculateAE(broker, calculPeriodId, brokerDataModal);
            }
            //Supervisor
            foreach (string brkID in brokerDataModal.HashBroker.Keys)
            {
                Broker broker = (Broker)brokerDataModal.HashBroker[brkID];
                if (broker.IsSupervisor())
                {
                    CalculateSupervisor(broker, brokerDataModal);
                }
            }
            //Supporter
            foreach (string brkID in brokerDataModal.HashBroker.Keys)
            {
                Broker broker = (Broker)brokerDataModal.HashBroker[brkID];
                if (broker.IsSupporter())
                {
                    CalculateSupporter(broker, brokerDataModal);
                }
            }

            AccumulateOfficeRevenue(brokerDataModal, calculPeriodId, calculPeriod);

            foreach (string brkID in brokerDataModal.HashBroker.Keys)
            {
                Broker brk = (Broker)brokerDataModal.HashBroker[brkID];
                if (brk.IsManager()) 
                {
                    CalculateManager(brk,brokerDataModal);
                } 
            }

            StoreCalculationData(brokerDataModal,calculPeriodId);
            _logger.Info("Calculate - LEAVE");
        }

        #region Private Methods
       
        /// <summary>
        /// Prepare data for calculating commission
        /// </summary>
        /// <param name="brk"></param>
        private void PrepareTradingData(Broker brk)
        {
            
            BrokerCalculationResult result = brk.PaymentResult;
            
            // Collect Revenue data
            for (int i = 0; i < brk.ClientTradeData.Count; i++)
            {
                ClientTradeData clientTradeData = brk.ClientTradeData[i];
                if (clientTradeData != null)
                {
                    // Trading Value
                    result.TradingValue += clientTradeData.TradingValue;

                    // Gross Revenue
                    result.GrossRevenue += clientTradeData.Commission;

                    // HSC Clients or old clients
                    if (clientTradeData.ClientTypeId.Equals(ClientType.CLIENTTYPE_ID_FROMHSC))
                    {
                        // Inherited Trading Value
                        result.InheritedTradingValue += clientTradeData.TradingValue;

                        // Inherited Gross Revenue
                        result.InheritedGrossRevenue += clientTradeData.Commission;

                        // Inherited Net Revenue
                        result.InheritedNetRevenue += (clientTradeData.Commission - (clientTradeData.TradingValue * (decimal)MARKET_FEE_RATIO));
                        if (result.InheritedNetRevenue < 0) result.InheritedNetRevenue = 0;
                    }

                    // New Clients or client from AE/BR
                    if (clientTradeData.ClientTypeId.Equals(ClientType.CLIENTTYPE_ID_BYBROKER))
                    {
                        // Private Trading Value
                        result.PrivateTradingValue += clientTradeData.TradingValue;

                        // Private Gross Revenue
                        result.PrivateGrossRevenue += clientTradeData.Commission;

                        // Private Net Revenue
                        result.PrivateNetRevenue += (clientTradeData.Commission - (clientTradeData.TradingValue * (decimal)MARKET_FEE_RATIO));
                        if (result.PrivateNetRevenue < 0) result.PrivateNetRevenue = 0;
                    }
                }
            }

            result.NetRevenue = result.InheritedNetRevenue + result.PrivateNetRevenue;

        }

        private void CalculateAE(Broker brk, string calculPeriodId, BrokerCommissionModal masterData)
        {   
            // prepare data for Calculation
            PrepareTradingData(brk);

            BrokerCalculationResult calculResult = brk.PaymentResult;
            if (calculResult.NetRevenue < NEW_CALCULATION_LIMIT) //using OLD METHOD
            #region OLD METHOD
            {
                // Find the Commission Rate for OLD CLIENTS
                CommissionRate hscClientRate = masterData.FindCommissionRate(brk.AECommissionCode,
                        ClientType.CLIENTTYPE_ID_FROMHSC, calculResult.NetRevenue);
                if (hscClientRate != null)
                {
                    calculResult.InheritedCommissionRate = hscClientRate.Commissionrate;
                }
                else
                {
                    calculResult.InheritedCommissionRate = 0;
                }
                // Commission payment for hsc clients
                calculResult.InheritedCommissionPayment = (calculResult.InheritedNetRevenue * (decimal)calculResult.InheritedCommissionRate);

                // Find the Commission Rate for New clients
                CommissionRate aeClientRate = masterData.FindCommissionRate(brk.AECommissionCode,
                        ClientType.CLIENTTYPE_ID_BYBROKER, calculResult.NetRevenue);

                if (aeClientRate != null)
                {
                    calculResult.PrivateCommissionRate = aeClientRate.Commissionrate;
                    calculResult.PrivateCommissionRate1 = aeClientRate.Commissionrate;
                    calculResult.PrivateCommissionRate2 = 0;
                }
                else
                {
                    calculResult.PrivateCommissionRate = 0;
                    calculResult.PrivateCommissionRate1 = 0;
                    calculResult.PrivateCommissionRate2 = 0;
                }

                // Commission payment for hsc clients
                calculResult.PrivateCommissionPayment = (calculResult.PrivateNetRevenue * (decimal)calculResult.PrivateCommissionRate);
                calculResult.OtherPayment = 0;
                if (brk.IsAccountExecutive())
                {
                    AllowanceDTO allowanceRate = masterData.GetAllowance(brk.AECommissionCode, calculResult.NetRevenue);
                    if (allowanceRate != null)
                    {
                        calculResult.OtherPayment = allowanceRate.Allowance;
                    }
                }

                // Total payment
                calculResult.Payment += calculResult.InheritedCommissionPayment + calculResult.PrivateCommissionPayment + calculResult.OtherPayment;
            }
            #endregion
            else //using NEW METHOD
            #region NEW METHOD
            {
                // Find the Commission Rate for OLD CLIENTS
                CommissionRate1 hscClientRate = masterData.FindCommissionRate1(brk.AECommissionCode,
                        ClientType.CLIENTTYPE_ID_FROMHSC, calculResult.NetRevenue);
                if (hscClientRate != null)
                {
                    calculResult.InheritedCommissionRate = hscClientRate.LowerRate;
                }
                else
                {
                    calculResult.InheritedCommissionRate = 0;
                }
                // Commission payment for hsc clients
                calculResult.InheritedCommissionPayment = (calculResult.InheritedNetRevenue * (decimal)calculResult.InheritedCommissionRate);
                
                // Find the Commission Rate for New clients
                CommissionRate1 aeClientRate = masterData.FindCommissionRate1(brk.AECommissionCode,
                        ClientType.CLIENTTYPE_ID_BYBROKER, calculResult.NetRevenue);

                calculResult.PrivateCommissionPayment = 0;
                if (aeClientRate != null)
                {
                    calculResult.PrivateCommissionRate = aeClientRate.LowerRate;
                    calculResult.PrivateCommissionRate1 = aeClientRate.LowerRate;
                    calculResult.PrivateCommissionRate2 = aeClientRate.UpperRate;
                    if (calculResult.PrivateNetRevenue < aeClientRate.Limit)
                    {
                        calculResult.PrivateCommissionPayment = (decimal)aeClientRate.LowerRate * calculResult.PrivateNetRevenue;
                    }
                    else
                    {
                        calculResult.PrivateCommissionPayment = (decimal)aeClientRate.LowerRate * aeClientRate.Limit + (calculResult.PrivateNetRevenue - aeClientRate.Limit) * (decimal)aeClientRate.UpperRate;
                    }
                }
                else
                {
                    calculResult.PrivateCommissionRate = 0;
                    calculResult.PrivateCommissionRate1 = 0;
                    calculResult.PrivateCommissionRate2 = 0;
                }
                                              
                calculResult.OtherPayment = 0;
                if (brk.IsAccountExecutive())
                {
                    AllowanceDTO allowanceRate = masterData.GetAllowance(brk.AECommissionCode, calculResult.NetRevenue);
                    if (allowanceRate != null)
                    {
                        calculResult.OtherPayment = allowanceRate.Allowance;
                    }
                }
                // Total payment
                calculResult.Payment += calculResult.InheritedCommissionPayment + calculResult.PrivateCommissionPayment + calculResult.OtherPayment;

            }
            #endregion
        }

        private void CalculateSupervisor(Broker brk, BrokerCommissionModal masterData)
        {
            // Calculate the total of all subordinates
            foreach (string key in brk.HashSubordinate.Keys)
            {
                Broker subBrk = (Broker)brk.HashSubordinate[key];

                brk.PaymentResult.SubtotalNetRevenue += subBrk.PaymentResult.NetRevenue;
                brk.PaymentResult.SubtotalInheritedNetRevenue += subBrk.PaymentResult.InheritedNetRevenue;
                brk.PaymentResult.SubtotalPrivateNetRevenue += subBrk.PaymentResult.PrivateNetRevenue;

                brk.PaymentResult.SubtotalTradingValue += subBrk.PaymentResult.TradingValue;
                brk.PaymentResult.SubtotalInheritedTradingValue += subBrk.PaymentResult.InheritedTradingValue;
                brk.PaymentResult.SubtotalPrivateTradingValue += subBrk.PaymentResult.PrivateTradingValue;
            }

            // Find the Commission Rates
            // Find the Commission Rate for ClientType = 1 --> Client from HSC
            CommissionRate rate4OldClient = masterData.FindCommissionRate(brk.SUPCommissionCode,
                ClientType.CLIENTTYPE_ID_FROMHSC, brk.PaymentResult.SubtotalNetRevenue);

            // Find the Commission Rate for ClientType = 2 --> Client found by broker
            CommissionRate rate4NewClient = masterData.FindCommissionRate(brk.SUPCommissionCode,
                    ClientType.CLIENTTYPE_ID_BYBROKER, brk.PaymentResult.SubtotalNetRevenue);

            if (rate4NewClient == null)
            {
                _logger.Error("CalculateSupervisor - rate4NewClient not found. BrokerID=" + brk.Id);
                rate4NewClient = new CommissionRate(ClientType.CLIENTTYPE_ID_BYBROKER, Constants.BROKERTYPE_SUP);
            }

            if (rate4OldClient == null)
            {
                _logger.Error("CalculateSupervisor - rate4OldClient not found. BrokerID=" + brk.Id);
                rate4OldClient = new CommissionRate(ClientType.CLIENTTYPE_ID_FROMHSC, Constants.BROKERTYPE_SUP);
            }

            Decimal a = (brk.PaymentResult.SubtotalInheritedNetRevenue * (decimal)rate4OldClient.Commissionrate); // A
            Decimal b = (brk.PaymentResult.SubtotalPrivateNetRevenue * (decimal)rate4NewClient.Commissionrate); // B1 or B2 depends on the matched rate
            brk.PaymentResult.SupervisorPayment = a + b;
            brk.PaymentResult.Payment += brk.PaymentResult.SupervisorPayment;
            //_logger.Debug("CalculateSupervisor - LEAVE");
        }

        private void CalculateSupporter(Broker brk, BrokerCommissionModal masterData)
        {
            // Calculate the total of all supported
            foreach (string key in brk.HashSupporting.Keys)
            {
                Broker subBrk = (Broker)brk.HashSupporting[key];

                brk.PaymentResult.SubtotalNetRevenue += subBrk.PaymentResult.NetRevenue;
                brk.PaymentResult.SubtotalInheritedNetRevenue += subBrk.PaymentResult.InheritedNetRevenue;
                brk.PaymentResult.SubtotalPrivateNetRevenue += subBrk.PaymentResult.PrivateNetRevenue;

                brk.PaymentResult.SubtotalTradingValue += subBrk.PaymentResult.TradingValue;
                brk.PaymentResult.SubtotalInheritedTradingValue += subBrk.PaymentResult.InheritedTradingValue;
                brk.PaymentResult.SubtotalPrivateTradingValue += subBrk.PaymentResult.PrivateTradingValue;
            }

            // Find the Commission Rates
            // Find the Commission Rate for ClientType = 1 --> Client from HSC
           //CommissionRate rate4OldClient = masterData.FindCommissionRate(brk.SUPCommissionCode,
           //     ClientType.CLIENTTYPE_ID_FROMHSC, brk.PaymentResult.SubtotalNetRevenue);

            // Find the Commission Rate for ClientType = 2 --> Client found by broker
           // CommissionRate rate4NewClient = masterData.FindCommissionRate(brk.SUPCommissionCode,
            //        ClientType.CLIENTTYPE_ID_BYBROKER, brk.PaymentResult.SubtotalNetRevenue);

            //if (rate4NewClient == null)
            //{
            //    _logger.Error("CalculateSupervisor - rate4NewClient not found. BrokerID=" + brk.Id);
            //    rate4NewClient = new CommissionRate(ClientType.CLIENTTYPE_ID_BYBROKER, Constants.BROKERTYPE_SUP);
            //}

            //if (rate4OldClient == null)
            //{
            //    _logger.Error("CalculateSupervisor - rate4OldClient not found. BrokerID=" + brk.Id);
            //    rate4OldClient = new CommissionRate(ClientType.CLIENTTYPE_ID_FROMHSC, Constants.BROKERTYPE_SUP);
            //}

            Decimal a = (brk.PaymentResult.SubtotalInheritedNetRevenue * (decimal)0.01); // A
            Decimal b = (brk.PaymentResult.SubtotalPrivateNetRevenue * (decimal)0.02); // B1 or B2 depends on the matched rate
            brk.PaymentResult.SupervisorPayment = a + b;
            brk.PaymentResult.Payment += brk.PaymentResult.SupervisorPayment;
            //_logger.Debug("CalculateSupervisor - LEAVE");
        }

        private void CalculateManager(Broker brk, BrokerCommissionModal masterData)
        {
            //_logger.Debug("CalculateManager - ENTER");
            if ((brk.Office == null) || (brk.Office.Id == null) || (brk.Office.Branch == null)) return;

            decimal revenue = 0;
            decimal value = 0;
            decimal value4FindingRate = 0;
            if (brk.Office.IsBranch(Office.BRANCH_ID_HN))
            {
                revenue = brk.Office.Branch.Result.NetRevenue;
                value = brk.Office.Branch.Result.TradingValue;
                value4FindingRate = value;
            }
            else if (brk.Office.IsBranch(Office.BRANCH_ID_HCM))
            {
                revenue = brk.Office.Result.NetRevenue;
                value = brk.Office.Result.TradingValue;
                value4FindingRate = (value - 1000000000000); // 1K Bil
                if (value4FindingRate < 0) value4FindingRate = 0;
            }

            CommissionRate rate4Manager = masterData.FindCommissionRate(brk.MANCommissionCode,
                ClientType.CLIENTTYPE_ID_FROMHSC, value4FindingRate);

            if (rate4Manager == null)
            {
                _logger.Error("CalculateManager - rate4OldClient not found. BrokerID=" + brk.Id);
                rate4Manager = new CommissionRate(ClientType.CLIENTTYPE_ID_FROMHSC, Constants.BROKERTYPE_MAN);
            }

            brk.PaymentResult.ManagementBonusRate = rate4Manager.Commissionrate;
            brk.PaymentResult.SupervisorPayment = (revenue * (decimal)rate4Manager.Commissionrate);
            if (brk.AdjustRate != null)
            {
                brk.PaymentResult.SupervisorPayment = (brk.PaymentResult.SupervisorPayment * ((decimal)brk.AdjustRate.AdjustRate));
            }

            brk.PaymentResult.Payment += brk.PaymentResult.SupervisorPayment;

            //_logger.Debug("CalculateManager - LEAVE");
        }

        private void AccumulateOfficeRevenue(BrokerCommissionModal brokerDataModal, 
            string calculPeriodId, DateTime calculPeriod)
        {
            // Init office result data
            foreach (string officeID in brokerDataModal.HashOffice.Keys)
            {
                Office office = (Office)brokerDataModal.HashOffice[officeID];
                if (office != null)
                {
                    OfficeCalculationResult officeResult = new OfficeCalculationResult(office);
                    officeResult.PeriodId = calculPeriodId;
                    officeResult.CalculatePeriod = calculPeriod;
                    officeResult.TradingValue = 0;
                    officeResult.NetRevenue = 0;

                    office.Result = officeResult;
                }
            }

            // Accumulate broker data into offices
            foreach (string brkID in brokerDataModal.HashBroker.Keys)
            {
                Broker brk = (Broker)brokerDataModal.HashBroker[brkID];
                if ((brk != null) && (brk.Office != null) && (brk.Office.Id != null))
                {
                    if (brk.Office.Result != null)
                    {
                        brk.Office.Result.TradingValue += brk.PaymentResult.TradingValue;
                        brk.Office.Result.InheritedTradingValue += brk.PaymentResult.InheritedTradingValue;
                        brk.Office.Result.PrivateTradingValue += brk.PaymentResult.PrivateTradingValue;

                        brk.Office.Result.GrossRevenue += brk.PaymentResult.GrossRevenue;
                        brk.Office.Result.InheritedGrossRevenue += brk.PaymentResult.InheritedGrossRevenue;
                        brk.Office.Result.PrivateGrossRevenue += brk.PaymentResult.PrivateGrossRevenue;

                        brk.Office.Result.NetRevenue += brk.PaymentResult.NetRevenue;
                        brk.Office.Result.InheritedNetRevenue += brk.PaymentResult.InheritedNetRevenue;
                        brk.Office.Result.PrivateNetRevenue += brk.PaymentResult.PrivateNetRevenue;
                    }
                }
            }

            // Travel on each office to accumulate the branch data
            foreach (string officeID in brokerDataModal.HashOffice.Keys)
            {
                Office office = (Office)brokerDataModal.HashOffice[officeID];
                if (office != null)
                {
                    _logger.Info("Office " + office.Id + ": TradingValue=" + office.Result.TradingValue + " Revenue=" + office.Result.NetRevenue);

                    if ((office.Branch != null) && (office.Branch.Id != null))
                    {
                        if (office.Branch.Result == null)
                        {
                            office.Branch.Result = new BranchCalculationResult(office.Branch);
                            office.Branch.Result.PeriodId = calculPeriodId;
                            office.Branch.Result.CalculatePeriod = calculPeriod;
                        }
                        office.Branch.Result.TradingValue += office.Result.TradingValue;
                        office.Branch.Result.InheritedTradingValue += office.Result.InheritedTradingValue;
                        office.Branch.Result.PrivateTradingValue += office.Result.PrivateTradingValue;

                        office.Branch.Result.GrossRevenue += office.Result.GrossRevenue;
                        office.Branch.Result.InheritedGrossRevenue += office.Result.InheritedGrossRevenue;
                        office.Branch.Result.PrivateGrossRevenue += office.Result.PrivateGrossRevenue;

                        office.Branch.Result.NetRevenue += office.Result.NetRevenue;
                        office.Branch.Result.InheritedNetRevenue += office.Result.InheritedNetRevenue;
                        office.Branch.Result.PrivateNetRevenue += office.Result.PrivateNetRevenue;
                    }
                }
            }
        }

        private void StoreCalculationData(BrokerCommissionModal brokerDataModal, string calculPeriodId)
        {
            _logger.Debug("StoreCalculationData - ENTER");

            // Delete if there is already records of this calculation
            int deleted = 0;
            _logger.Info("Delete Result Data, id=" + calculPeriodId);
            CalculationResultDAL calculationResultDAL = new CalculationResultDAL();
            deleted = calculationResultDAL.DeleteBrokerCalculationResult(calculPeriodId);
            _logger.Info("Delete BrokerCalculationResult, count=" + deleted);

            deleted = calculationResultDAL.DeleteOfficeCalculResult(calculPeriodId);
            _logger.Info("Delete OfficeCalculationResult, count=" + deleted);

            deleted = calculationResultDAL.DeleteBranchCalculResult(calculPeriodId);

            DateTime calculateTime = DateTime.Now;

            foreach (string key in brokerDataModal.HashBroker.Keys)
            {
                Broker brk = (Broker)brokerDataModal.HashBroker[key];
                calculationResultDAL.InsertBrokerCalculationResultLog(brk.PaymentResult, calculateTime);
                int inserted = calculationResultDAL.InsertBrokerCalculationResult(brk.PaymentResult);
                _logger.Debug("StoreCalculationData - BrokerID=" + brk.Id + " InsertedRecord=" + inserted);
            }

            foreach (string officeID in brokerDataModal.HashOffice.Keys)
            {
                Office office = (Office)brokerDataModal.HashOffice[officeID];
                if ((office != null) && (office.Result != null))
                {
                    calculationResultDAL.InsertOfficeCalculationResultLog(office.Result, calculateTime);
                    int inserted = calculationResultDAL.InsertOfficeCalculationResult(office.Result);
                    _logger.Debug("StoreCalculationData - OfficeID=" + office.Id + " InsertedRecord=" + inserted);
                }
            }

            foreach (string branchID in brokerDataModal.HashBranch.Keys)
            {
                Branch branch = (Branch)brokerDataModal.HashBranch[branchID];
                if ((branch != null) && (branch.Result != null))
                {
                    calculationResultDAL.InsertBranchCalculationResultLog(branch.Result, calculateTime);
                    int inserted = calculationResultDAL.InsertBranchCalculationResult(branch.Result);
                    _logger.Debug("StoreCalculationData - BranchID=" + branch.Id + " InsertedRecord=" + inserted);
                }
            }
            _logger.Debug("StoreCalculationData - LEAVE");
        }

        
        //private Hashtable GetAvgNetRevenue(DateTime targetMonth)
        //{
        //    CalculationResultDAL dal = new CalculationResultDAL();
        //    Hashtable retData = dal.GetAvgNetRevenue(targetMonth);
        //    return retData;
        //}
        #endregion

        #region Public Methods

        

        public CalculationDTO GetCalculationPeriod(string id)
        {
            return CalculationDataAccess.GetCalculationPeriod(id);
        }

        public void InsertCalculationPeriod(CalculationDTO dto)
        {
            CalculationDataAccess.InsertCalculationPeriod(dto);
        }
        
        #endregion
    }
}
