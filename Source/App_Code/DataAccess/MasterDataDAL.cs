using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using BrokerManager.DataObjects;

/// <summary>
/// Summary description for UserData
/// </summary>
namespace BrokerManager.DataAccess
{
    public class MasterDataDAL : BaseDAL
    {
        public MasterDataDAL(): base()
        {}

        public Hashtable GetAllOffice()
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT [ID], [BranchID], [Name], [UpdateBy], [UpdateTime] FROM [Office]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Office office = new Office(dataSet.Tables[0].Rows[i][0].ToString());
                    string branchID = null;
                    if (dataSet.Tables[0].Rows[i][1] != DBNull.Value)
                    {
                        branchID = dataSet.Tables[0].Rows[i][1].ToString();
                        office.Branch = new Branch(branchID);
                    }
                    else
                    {
                        office.Branch = null;
                    }
                    
                    office.Name = dataSet.Tables[0].Rows[i][2].ToString();

                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        office.UpdateBy = dataSet.Tables[0].Rows[i][3].ToString();
                    }
                    else
                    {
                        office.UpdateBy = null;
                    }

                    if (dataSet.Tables[0].Rows[i][4] != DBNull.Value)
                    {
                        office.UpdateTime = (DateTime)dataSet.Tables[0].Rows[i][4];
                    }
                    else
                    {
                        office.UpdateTime = DateTime.MaxValue;
                    }

                    retData[office.Id] = office;
                }
            }

            return retData;
        }

        public Hashtable GetAllBranch()
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT [ID], [Name], [Description], [UpdateBy], [UpdateTime] FROM [Branch] ";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Branch branch = new Branch(dataSet.Tables[0].Rows[i][0].ToString());
                    branch.Name = dataSet.Tables[0].Rows[i][1].ToString();

                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        branch.Description = dataSet.Tables[0].Rows[i][2].ToString();
                    }
                    else
                    {
                        branch.Description = null;
                    }

                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        branch.UpdateBy = dataSet.Tables[0].Rows[i][3].ToString();
                    }
                    else
                    {
                        branch.UpdateBy = null;
                    }

                    if (dataSet.Tables[0].Rows[i][4] != DBNull.Value)
                    {
                        branch.UpdateTime = (DateTime)dataSet.Tables[0].Rows[i][4];
                    }
                    else
                    {
                        branch.UpdateTime = DateTime.MaxValue;
                    }

                    retData[branch.Id] = branch;
                }
            }

            return retData;
        }

        public List<CommissionRate> GetAllCommissionRate()
        {
            List<CommissionRate> retData = new List<CommissionRate>();
            string query = @"SELECT [ID], [ClientTypeID], [Code], [LowerLimit], [UpperLimit], 
                                [CommissionRate]  
                            FROM [CommissionRate] 
                            ORDER BY [Code], [ClientTypeID], [LowerLimit]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // ClientType 
                    CommissionRate rate = new CommissionRate();

                    rate.ClientTypeId = Int32.Parse(dataSet.Tables[0].Rows[i][1].ToString());
                    rate.Code = dataSet.Tables[0].Rows[i][2].ToString();
                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        rate.LowerLimit = (long)dataSet.Tables[0].Rows[i][3];
                    }

                    if (dataSet.Tables[0].Rows[i][4] != DBNull.Value)
                    {
                        rate.UpperLimit = (long)dataSet.Tables[0].Rows[i][4];
                    }

                    if (dataSet.Tables[0].Rows[i][5] != DBNull.Value)
                    {
                        rate.Commissionrate = (double)dataSet.Tables[0].Rows[i][5];
                    }

                    retData.Add(rate);
                }
            }

            return retData;
        }

        public List<CommissionRate1> GetAllCommissionRate1()
        {
            List<CommissionRate1> retData = new List<CommissionRate1>();
            string query = @"SELECT [ID], [ClientTypeID], [Code], [LowerLimit], [UpperLimit], 
                                [LowerRate],[UpperRate],[Limit]  
                            FROM [CommissionRate1] 
                            ORDER BY [Code], [ClientTypeID], [LowerLimit]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // ClientType 
                    CommissionRate1 rate = new CommissionRate1();

                    rate.ClientTypeId = Int32.Parse(dataSet.Tables[0].Rows[i][1].ToString());
                    rate.Code = dataSet.Tables[0].Rows[i][2].ToString();
                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        rate.LowerLimit = (long)dataSet.Tables[0].Rows[i][3];
                    }

                    if (dataSet.Tables[0].Rows[i][4] != DBNull.Value)
                    {
                        rate.UpperLimit = (long)dataSet.Tables[0].Rows[i][4];
                    }

                    if (dataSet.Tables[0].Rows[i][5] != DBNull.Value)
                    {
                        rate.LowerRate = (double)dataSet.Tables[0].Rows[i][5];
                    }
                    if (dataSet.Tables[0].Rows[i][6] != DBNull.Value)
                    {
                        rate.UpperRate = (double)dataSet.Tables[0].Rows[i][6];
                    }
                    if (dataSet.Tables[0].Rows[i][7] != DBNull.Value)
                    {
                        rate.Limit = (long)dataSet.Tables[0].Rows[i][7];
                    }

                    retData.Add(rate);
                }
            }

            return retData;
        }

        public Hashtable GetAllClientType()
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT [ID], [Name], [Description] FROM [ClientType]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    ClientType type = new ClientType();
                    type.Id = Int32.Parse(dataSet.Tables[0].Rows[i][0].ToString());
                    type.Name = dataSet.Tables[0].Rows[i][1].ToString();

                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        type.Description = dataSet.Tables[0].Rows[i][2].ToString();
                    }
                    else
                    {
                        type.Description = null;
                    }

                    retData[type.Id] = type;
                }
            }

            return retData;
        }

        public Hashtable GetAllBrokerType()
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT [ID], [Name], [Description], [Level_] FROM [BrokerType]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    BrokerType brkType = new BrokerType(dataSet.Tables[0].Rows[i][0].ToString());
                    brkType.Name = dataSet.Tables[0].Rows[i][1].ToString();

                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        brkType.Description = dataSet.Tables[0].Rows[i][2].ToString();
                    }
                    else
                    {
                        brkType.Description = null;
                    }
                    brkType.Level = dataSet.Tables[0].Rows[i][3].ToString();

                    retData[brkType.Id] = brkType;
                }
            }

            return retData;
        }

        public List<AllowanceDTO> GetAllowanceList()
        {
            List<AllowanceDTO> result = new List<AllowanceDTO>();
            string query = @"SELECT [ID], [BrokerTypeId],[LowerLimit], [UpperLimit], [Allowance],[Salary] FROM [Allowance]  ORDER BY [BrokerTypeId], [LowerLimit]";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    // Allowance 
                    AllowanceDTO allowance = new AllowanceDTO();

                    allowance.Id = Int32.Parse(dataSet.Tables[0].Rows[i][0].ToString());
                    allowance.Code = dataSet.Tables[0].Rows[i][1].ToString().Trim();
                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        allowance.LowerLimit = (long)dataSet.Tables[0].Rows[i][2];
                    }

                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        allowance.UpperLimit = (long)dataSet.Tables[0].Rows[i][3];
                    }

                    if (dataSet.Tables[0].Rows[i][4] != DBNull.Value)
                    {
                        allowance.Allowance = (int)dataSet.Tables[0].Rows[i][4];
                    }

                    if (dataSet.Tables[0].Rows[i][5] != DBNull.Value)
                    {
                        allowance.Salary = (int)dataSet.Tables[0].Rows[i][5];
                    }

                    result.Add(allowance);
                }
            }

            return result;
        }
    }
}