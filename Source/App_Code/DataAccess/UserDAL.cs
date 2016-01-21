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
    public class UserDAL : BaseDAL
    {
        public UserDAL() : base()
        {
        }

        public Hashtable GetAllUser()
        {
            Hashtable retData = new Hashtable();
            string query = "SELECT ID, Password FROM [User] ";
            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    BrokerManager.DataObjects.UserDTO user = new BrokerManager.DataObjects.UserDTO();
                    user.Id = dataSet.Tables[0].Rows[i][0].ToString();
                    user.Password = dataSet.Tables[0].Rows[i][1].ToString();
                    if (!retData.Contains(user.Id))
                    {
                        retData.Add(user.Id, user);
                    }
                    else
                    {
                        retData[user.Id] = user;
                    }
                }
            }

            return retData;
        }

        public Hashtable GetAllUserInformation()
        {
            Hashtable retData = GetAllUser();

            if ((retData != null) && (retData.Count > 0))
            {
                string query = "SELECT UserID, PrivilegeID, p.Name, p.Description FROM UserPrivilege u JOIN Privilege p ON u.PrivilegeID = p.ID ";
                DataSet dataSet = DataAccess.ExecuteQuery(query);

                if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        string userID = dataSet.Tables[0].Rows[i][0].ToString();
                        BrokerManager.DataObjects.UserDTO user = (BrokerManager.DataObjects.UserDTO) retData[userID];
                        if (user != null)
                        {
                            Privilege privilege = new Privilege();

                            privilege.Id = dataSet.Tables[0].Rows[i][1].ToString();
                            privilege.Name = dataSet.Tables[0].Rows[i][2].ToString();
                            if (dataSet.Tables[0].Rows[i][3] != DBNull.Value)
                            {
                                privilege.Description = dataSet.Tables[0].Rows[i][3].ToString();
                            }
                            user.ListPrivilege.Add(privilege);
                        }
                    }
                }

            }

            return retData;
        }
    }
}