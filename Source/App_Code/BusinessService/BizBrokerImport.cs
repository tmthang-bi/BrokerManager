using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataObjects;
using BrokerManager.DataAccess;
using log4net;
using log4net.Config;
using System.IO;

namespace BrokerManager.BusinessService
{
/// <summary>
/// Summary description for BizClientTradeDataImport
/// </summary>
    public class BizBrokerImport
    {
        private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);
        private MasterDataDAL _masterDataAccess = null;
        

        private MasterDataDAL MasterDataAccess
        {
            get
            {
                if (_masterDataAccess == null)
                {
                    _masterDataAccess = new MasterDataDAL();
                }
                return _masterDataAccess;
            }
        }

        public BizBrokerImport(){}

        public int DeleteAllBroker()
        {
            int affected = 0;
            BrokerDAL dal = new BrokerDAL();
            affected = dal.DeleteAll();
            return affected;
        }

        private string AeCode_Convert(string ae)
        {
            switch (ae){
                case "AE": return "AE2";
                case "SUP": return "AE2";
                case "MG": return "HV";
                case "NVMG": return "HV";
                case "HV": return "HV";
                default: return "error";
            }
        }

        private string SUPCode_Convert(string sup)
        {
            switch (sup)
            {
                case "SUP": return "SUP2";                
                default: return "error";
            }
        }

        private string Branch_Convert(string branch)
        {
            switch (branch)
            {
                case "CHO LON": return "CL";
                case "BA TRIEU": return "BT";
                case "LANG HA 1": return "LH1";
                case "LANG HA 2": return "LH2";
                case "LE LAI 1": return "LL1";
                case "LE LAI 2": return "LL2";
                case "LE LAI 3": return "LL3";
                case "LE THANH TONG": return "LTT";
                case "NGUYEN VAN TROI": return "NVT";
                case "NGUYEN VAN TROI 1": return "NVT";
                case "NGUYEN VAN TROI 2": return "NVT2";
                case "TRAN HUNG DAO": return "THD";
                case "THAI VAN LUNG": return "TVL";
                default: return "error";
            }
        }

        private List<Broker> ReadData(DataTable dataTable)
        {
            List<Broker> result = new List<Broker>();
            Hashtable officeMap = MasterDataAccess.GetAllOffice();
            try
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    string brkID = dataTable.Rows[i][0].ToString().Trim().ToUpper();
                    string brkType = dataTable.Rows[i][1].ToString().Trim().ToUpper();
                    string office = Branch_Convert(dataTable.Rows[i][2].ToString().Trim().ToUpper());


                    if (Utils.IsNullOrEmpty(brkID) || Utils.IsNullOrEmpty(brkType) || Utils.IsNullOrEmpty(office))
                    {
                        throw new InvalidDataException("Invalid required field (ID/BrokerTypeID/OfficeID) at row " + i);
                    }

                    string supID = null;
                    if (dataTable.Rows[i][3] != DBNull.Value)
                    {
                        supID = dataTable.Rows[i][3].ToString().Trim().ToUpper();
                        if (Utils.IsNullOrEmpty(supID)) supID = null;
                    }

                    string name = string.Empty;
                    if (dataTable.Rows[i][4] != DBNull.Value)
                    {
                        name = dataTable.Rows[i][4].ToString().Trim();
                    }

                    string sEntranceDate;
                    DateTime entranceDate = DateTime.MinValue;
                    if (dataTable.Rows[i][5] != DBNull.Value)
                    {
                        sEntranceDate = dataTable.Rows[i][5].ToString().Trim();
                        entranceDate = DateTime.Parse(sEntranceDate);
                    }

                    string email = null;
                    if (dataTable.Columns.Count > 6)
                    {
                        if (dataTable.Rows[i][6] != DBNull.Value)
                        {
                            email = dataTable.Rows[i][6].ToString().Trim();
                        }
                    }

                    string AECommisionCode = null;
                    if (dataTable.Columns.Count > 7)
                    {
                        if (dataTable.Rows[i][7] != DBNull.Value)
                        {
                            AECommisionCode = AeCode_Convert(dataTable.Rows[i][7].ToString().Trim());
                        }
                    }

                    string SUPCommisionCode = null;
                    if (dataTable.Columns.Count > 8)
                    {
                        if (dataTable.Rows[i][8] != DBNull.Value)
                        {
                            SUPCommisionCode = SUPCode_Convert(dataTable.Rows[i][8].ToString().Trim());
                        }
                    }

                    string MANCommisionCode = null;
                    if (dataTable.Columns.Count > 9)
                    {
                        if (dataTable.Rows[i][9] != DBNull.Value)
                        {
                            MANCommisionCode = dataTable.Rows[i][9].ToString().Trim();
                        }
                    }
                    string supporterID = null;
                    if (dataTable.Rows[i][10] != DBNull.Value)
                    {
                        supporterID = dataTable.Rows[i][10].ToString().Trim().ToUpper();
                        if (Utils.IsNullOrEmpty(supporterID)) supporterID = null;
                    }

                    // The broker object
                    Broker brk = new Broker(brkID);
                    // Broker Type 
                    if (brkType.Equals("ACCOUNT EXECUTIVE"))
                    {
                        brk.BrokerTypeId = Constants.BROKERTYPE_AE;
                    }                       
                    else if (brkType.Equals("SUPERVISOR"))
                    {
                        brk.BrokerTypeId = Constants.BROKERTYPE_SUP;
                    }
                    else if (brkType.Equals("MANAGER"))
                    {
                        brk.BrokerTypeId = Constants.BROKERTYPE_MAN;
                    }
                    else if (brkType.Equals("MANAGING DIRECTOR"))
                    {
                        brk.BrokerTypeId = Constants.BROKERTYPE_MD;
                    }
                    else
                    {
                        throw new InvalidDataException("Broker Type NOT recognized at row " + i + ", value=" + brkType);
                    }

                    // Office
                    Office objOffice = (Office)officeMap[office];
                    if (objOffice == null)
                    {
                        throw new InvalidDataException("Office NOT recognized at row " + i + ", value=" + office);
                    }
                    brk.Office = objOffice;
                    //Supervisor
                    if (!Utils.IsNullOrEmpty(supID))
                    {
                        brk.SuppervisorId = supID;
                    }
                    //Supporter
                    if (!Utils.IsNullOrEmpty(supporterID))
                    {
                        brk.SupporterId = supporterID;
                    }
                    
                    // Name
                    brk.Name = name;

                    // Entrance Date
                    brk.EntranceDate = entranceDate;

                    // Email
                    brk.Email = email;

                    brk.IsActive = true;

                    brk.AECommissionCode = AECommisionCode;
                    brk.SUPCommissionCode = SUPCommisionCode;
                    brk.MANCommissionCode = MANCommisionCode;

                    // Add the broker to list
                    result.Add(brk);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Invalid Data " + ex.Message);
            }
            return result;
        }

        public void Import(DataTable brokerTable, string updatedBy)
        {
            List<Broker> brokerList = ReadData(brokerTable);
            BrokerDAL dal = new BrokerDAL();

            for (int i = 0; i < brokerList.Count; i++)
            {
                Broker rec = brokerList[i];
                rec.UpdateBy = updatedBy;
                int updated = dal.Update(rec);
                if (updated == 0)
                {
                    int inserted = dal.Insert(rec);
                    _logger.Debug("Inserted=" + inserted + " - " + rec.ToString());
                }
                else
                {
                    _logger.Debug("Updated=" + updated + " - " + rec.ToString());
                }
            }
        }
    }
}
