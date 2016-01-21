using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
    public class BrokerDAL : BaseDAL
    {
        public BrokerDAL()
            : base()
        {
        }

        public Hashtable GetAllBroker()
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT a.[ID], a.[BrokerTypeID], a.[OfficeID], a.[SupervisorBrokerID], b.[Name] as SupervisorName, a.[Name], a.[EntranceDate], a.[LeaveDate], a.[IsActive], a.[UpdateBy], a.[UpdateTime], a.[Email], 
                                 a.[AECommissionCode],a.[SUPCommissionCode],a.[MANCommissionCode], c.AdjustRate , a.[SupporterID] 
                             FROM [Broker] a left outer join [Broker] b on a.SupervisorBrokerID = b.ID 
                                             left outer join [BrokerRate] c on a.ID = c.BrokerID 
                             WHERE a.IsActive = 1";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Broker broker = new Broker(dataSet.Tables[0].Rows[i][0].ToString());

                    // Broker Type
                    broker.BrokerTypeId = dataSet.Tables[0].Rows[i][1].ToString();
                    
                    // Office
                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        // Dummy office to hold the OfficeID
                        string officeID = dataSet.Tables[0].Rows[i][2].ToString();
                        Office office = new Office(officeID);
                        broker.Office = office;
                    }
                    
                    if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                    {
                        // Dummy broker to store only Supervisor Broker ID
                        Broker supervisor = new Broker(dataSet.Tables[0].Rows[i][3].ToString());
                        broker.Supervisor = supervisor;
                    }
                    
                    broker.Name = dataSet.Tables[0].Rows[i][5].ToString();
                    broker.IsActive = true;

                    if (dataSet.Tables[0].Rows[i][12] != DBNull.Value)
                    {
                        broker.AECommissionCode = dataSet.Tables[0].Rows[i][12].ToString().Trim();
                    }
                    if (dataSet.Tables[0].Rows[i][13] != DBNull.Value)
                    {
                        broker.SUPCommissionCode = dataSet.Tables[0].Rows[i][13].ToString().Trim();
                    }
                    if (dataSet.Tables[0].Rows[i][14] != DBNull.Value)
                    {
                        broker.MANCommissionCode = dataSet.Tables[0].Rows[i][14].ToString().Trim();
                    }

                    if (dataSet.Tables[0].Rows[i][15] != DBNull.Value)
                    {
                        broker.AdjustRate = new BrokerRate(broker);
                        broker.AdjustRate.AdjustRate = (double)dataSet.Tables[0].Rows[i][15];
                    }
                    if (dataSet.Tables[0].Rows[i][16] != DBNull.Value)
                    {
                        // Dummy broker to store only Supporter Broker ID
                        Broker supporter = new Broker(dataSet.Tables[0].Rows[i][16].ToString());
                        broker.Supporter = supporter;
                    }
                    else
                    {
                        broker.AdjustRate = null;
                    }

                    retData[broker.Id] = broker;
                }

                // Re iterate the broker list to link to the real supervisor objects (instead of dummies which only store the id) 
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string brokerID = dataSet.Tables[0].Rows[i][0].ToString();
                    Broker broker = (Broker)retData[brokerID];
                    if ((broker != null) && (broker.Supervisor != null))
                    {
                        Broker supervisor = (Broker)retData[broker.Supervisor.Id];
                        if (supervisor != null)
                        {
                            //broker.Supervisor = supervisor;
                            broker.JoinGroup(supervisor);
                        }
                        else
                        {
                            broker.Supervisor = null;
                        }
                    }
                }
            }

            return retData;
        }

        public int Update(Broker rec)
        {
            int affected = 0;
            string query = @"
                UPDATE Broker
                SET
                    BrokerTypeID = @BrokerTypeID, 
                    OfficeID = @OfficeID, 
                    SupervisorBrokerID = @SupervisorBrokerID, 
                    Name = @Name, 
                    EntranceDate = @EntranceDate, 
                    LeaveDate = @LeaveDate, 
                    IsActive = @IsActive, 
                    Email = @Email, 
                    UpdateBy = @UpdateBy, 
                    UpdateTime = getdate(),
                    SupporterId = @SupporterId
                WHERE (ID = @ID)";

            ((SqlCommand)DataAccess.Command).Parameters.Clear();

            ((SqlCommand)DataAccess.Command).Parameters.Add("@ID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerTypeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OfficeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupervisorBrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Name", SqlDbType.NVarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@EntranceDate", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@LeaveDate", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@IsActive", SqlDbType.Bit);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Email", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@UpdateBy", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupporterId", SqlDbType.VarChar);

            // Parameters' values
            ((SqlCommand)DataAccess.Command).Parameters["@ID"].Value = rec.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerTypeID"].Value = rec.BrokerTypeId;
            ((SqlCommand)DataAccess.Command).Parameters["@OfficeID"].Value = rec.Office.Id;

            if (!Utils.IsNullOrEmpty(rec.SuppervisorId))
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupervisorBrokerID"].Value = rec.SuppervisorId;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupervisorBrokerID"].Value = DBNull.Value;
            }

            if (!Utils.IsNullOrEmpty(rec.SupporterId))
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupporterId"].Value = rec.SupporterId;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupporterId"].Value = DBNull.Value;
            }

            if (rec.Name == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Name"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Name"].Value = rec.Name;
            }

            if (rec.EntranceDate > DateTime.MinValue)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@EntranceDate"].Value = rec.EntranceDate;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@EntranceDate"].Value = DBNull.Value;
            }

            if (rec.LeaveDate > DateTime.MinValue)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@LeaveDate"].Value = rec.LeaveDate;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@LeaveDate"].Value = DBNull.Value;
            }

            ((SqlCommand)DataAccess.Command).Parameters["@IsActive"].Value = rec.IsActive;

            if (rec.Email == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Email"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Email"].Value = rec.Email;
            }

            if (rec.UpdateBy == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@UpdateBy"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@UpdateBy"].Value = rec.UpdateBy;
            }

            

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int Insert(Broker rec)
        {
            int affected = 0;
            string query = @"
                INSERT INTO Broker
                    (ID, 
                    BrokerTypeID, 
                    OfficeID, 
                    SupervisorBrokerID, 
                    Name, 
                    EntranceDate, 
                    LeaveDate, 
                    IsActive, 
                    Email,
                    AECommissionCode,
                    SUPCommissionCode,
                    ManCommissionCode, 
                    UpdateBy, 
                    UpdateTime,
                    SupporterId)
                VALUES
                    (@ID, 
                    @BrokerTypeID, 
                    @OfficeID, 
                    @SupervisorBrokerID, 
                    @Name, 
                    @EntranceDate, 
                    @LeaveDate, 
                    @IsActive, 
                    @Email, 
                    @AECommissionCode,
                    @SUPCommissionCode,
                    @ManCommissionCode,
                    @UpdateBy, 
                    getdate(),
                    @SupporterId)";
                    
            ((SqlCommand)DataAccess.Command).Parameters.Clear();

            ((SqlCommand)DataAccess.Command).Parameters.Add("@ID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerTypeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OfficeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupervisorBrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Name", SqlDbType.NVarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@EntranceDate", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@LeaveDate", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@IsActive", SqlDbType.Bit);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Email", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@AECommissionCode", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SUPCommissionCode", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ManCommissionCode", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@UpdateBy", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupporterId", SqlDbType.VarChar);

            // Parameters' values
            ((SqlCommand)DataAccess.Command).Parameters["@ID"].Value = rec.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerTypeID"].Value = rec.BrokerTypeId;
            ((SqlCommand)DataAccess.Command).Parameters["@OfficeID"].Value = rec.Office.Id;

            if (!Utils.IsNullOrEmpty(rec.SuppervisorId))
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupervisorBrokerID"].Value = rec.SuppervisorId;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupervisorBrokerID"].Value = DBNull.Value;
            }

            if (!Utils.IsNullOrEmpty(rec.SupporterId))
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupporterId"].Value = rec.SupporterId;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SupporterId"].Value = DBNull.Value;
            }

            if (rec.Name == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Name"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Name"].Value = rec.Name;
            }

            if (rec.EntranceDate > DateTime.MinValue)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@EntranceDate"].Value = rec.EntranceDate;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@EntranceDate"].Value = DBNull.Value;
            }

            if (rec.LeaveDate > DateTime.MinValue)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@LeaveDate"].Value = rec.LeaveDate;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@LeaveDate"].Value = DBNull.Value;
            }

            ((SqlCommand)DataAccess.Command).Parameters["@IsActive"].Value = rec.IsActive;

            if (rec.Email == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Email"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@Email"].Value = rec.Email;
            }
            if (rec.AECommissionCode == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@AECommissionCode"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@AECommissionCode"].Value = rec.AECommissionCode;
            }
            if (rec.SUPCommissionCode == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SUPCommissionCode"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@SUPCommissionCode"].Value = rec.SUPCommissionCode;
            }
            if (rec.MANCommissionCode == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@MANCommissionCode"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@MANCommissionCode"].Value = rec.MANCommissionCode;
            }
            if (rec.UpdateBy == null)
            {
                ((SqlCommand)DataAccess.Command).Parameters["@UpdateBy"].Value = DBNull.Value;
            }
            else
            {
                ((SqlCommand)DataAccess.Command).Parameters["@UpdateBy"].Value = rec.UpdateBy;
            }

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int DeleteAll()
        {
            int affected = 0;
            string query = "DELETE Broker ";

            ((SqlCommand)DataAccess.Command).Parameters.Clear();

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }


        public string GetFilterString(string id, string type, string office, string name,  string supervisorName)
        {
            string ret = string.Empty;
            if (!Utils.IsNullOrEmpty(id))
            {
                ret += " ID LIKE '%" + id + "%'";
            }

            if (!Utils.IsNullOrEmpty(type))
            {
                if (ret.Length > 0) ret += " AND";
                ret += " BrokerTypeID = '" + type + "'";
            }

            if (!Utils.IsNullOrEmpty(office))
            {
                if (ret.Length > 0) ret += " AND";
                ret += " OfficeID = '" + office + "'";
            }

            if (!Utils.IsNullOrEmpty(name))
            {
                if (ret.Length > 0) ret += " AND";
                ret += " Name LIKE '%" + name + "%'";
            }

            if (!Utils.IsNullOrEmpty(supervisorName))
            {
                if (ret.Length > 0) ret += " AND";
                ret += " SupervisorName LIKE '%" + supervisorName + "%'";
            }
            //if (ret.Length > 0) ret = "WHERE" + ret;


            return ret;
        }
    }
}