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
    public class CalculationDAL : BaseDAL
    {
        public CalculationDAL(): base()
        {
        }

        public int InsertCalculationPeriod(CalculationDTO cal)
        {
            int affected = 0;
            string query = @"
                INSERT INTO Calculation
                    (id, freeze, calculatedBy, calculPeriod, verifyTradingData) 
                VALUES
                    (@id,@freeze, @calculatedBy, @calculPeriod, @verifyTradingData)";

            ((SqlCommand)DataAccess.Command).Parameters.Add("@id", SqlDbType.Char);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@freeze", SqlDbType.Bit);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@calculatedBy", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@calculPeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@verifyTradingData", SqlDbType.Bit);

            ((SqlCommand)DataAccess.Command).Parameters["@id"].Value = cal.ID;
            ((SqlCommand)DataAccess.Command).Parameters["@freeze"].Value = cal.Freeze;
            ((SqlCommand)DataAccess.Command).Parameters["@calculatedBy"].Value = cal.CalculatedBy;
            ((SqlCommand)DataAccess.Command).Parameters["@calculPeriod"].Value = cal.CalculPeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@verifyTradingData"].Value = cal.VerifyTradingData;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public CalculationDTO GetCalculationPeriod(string id)
        {
            string query = "SELECT id, freeze, verifyTradingData, calculPeriod FROM Calculation WHERE id = $ID$";

            query = query.Replace("$ID$", id);

            DataSet ds = DataAccess.ExecuteQuery(query);
            
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CalculationDTO result = new CalculationDTO();
                    result.ID = ds.Tables[0].Rows[0][0].ToString();
                    if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                    {
                        result.Freeze = Convert.ToBoolean(ds.Tables[0].Rows[0][1]);
                    }
                    else 
                    {
                        result.Freeze = false;
                    }

                    if (ds.Tables[0].Rows[0][2] != DBNull.Value)
                    {
                        result.VerifyTradingData = Convert.ToBoolean(ds.Tables[0].Rows[0][2]);
                    }
                    else 
                    {
                        result.VerifyTradingData = false;
                    }
                    result.CalculPeriod = Convert.ToDateTime(ds.Tables[0].Rows[0][3]);
                    return result;
                }
            }

            return null;
        }

    }
}