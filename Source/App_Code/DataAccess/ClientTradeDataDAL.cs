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
    public class ClientTradeDataDAL : BaseDAL
    {
        public ClientTradeDataDAL()
            : base()
        {
        }

        public bool IsExistingData(string calculationPeriodId)
        {
            string query = "SELECT 1 FROM ClientTradeData WHERE calculationPeriodId = $CALCULATIONID$ ";
            // delete existing value
            query = query.Replace("$CALCULATIONID$", calculationPeriodId);

            DataSet ds = DataAccess.ExecuteQuery(query);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<ClientTradeData> GetAllClientTradeData(string calculPeriodId)
        {
            List<ClientTradeData> retData = new List<ClientTradeData>();
            string query = @"SELECT [ClientTypeID], [BrokerID], [TotalTradingValue], [TotalCommission] FROM [ClientTradeData] WHERE calculationPeriodId = $CALCULATIONID$";
            query = query.Replace("$CALCULATIONID$", calculPeriodId);

            DataSet dataSet = DataAccess.ExecuteQuery(query);
            
            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                ClientTradeData tradeData = null;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    tradeData = new ClientTradeData();
                    
                    // Client Type
                    tradeData.ClientTypeId = (int)dataSet.Tables[0].Rows[i][0];

                    // Broker
                    tradeData.BrokerId = dataSet.Tables[0].Rows[i][1].ToString();
                    
                    // TradingValue
                    tradeData.TradingValue = (decimal) dataSet.Tables[0].Rows[i][2];

                    // Gross Revenue
                    tradeData.Commission = (decimal) dataSet.Tables[0].Rows[i][3];

                    retData.Add(tradeData);
                }
            }

            return retData;
        }

        public int Update(ClientTradeData rec)
        {
            int affected = 0;
            string query = @"
                UPDATE ClientTradeData
                SET
                    TotalTradingValue = @TotalTradingValue, 
                    TotalCommission = @TotalCommission 
                WHERE 
                    (ClientTypeID = @ClientTypeID) AND (BrokerID = @BrokerID) AND (datediff(month, [Date], getdate()) = 0)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ClientTypeID", SqlDbType.Int);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TotalTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TotalCommission", SqlDbType.Money);


            ((SqlCommand)DataAccess.Command).Parameters["@ClientTypeID"].Value = rec.ClientType.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerID"].Value = rec.Broker.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@TotalTradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@TotalCommission"].Value = rec.Commission;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int InsertClientTradeData(ClientTradeData rec)
        {
            int affected = 0;
            string query = @"
                INSERT INTO ClientTradeData
                    (ClientTypeID, 
                    BrokerID, 
                    TotalTradingValue, 
                    TotalCommission, 
                    calculationPeriodId,
                    updatedTime)
                VALUES
                    (@ClientTypeID, 
                    @BrokerID, 
                    @TotalTradingValue, 
                    @TotalCommission, 
                    @CalculationPeriodId,
                    getdate())";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ClientTypeID", SqlDbType.Int);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TotalTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TotalCommission", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculationPeriodId", SqlDbType.NChar);

            ((SqlCommand)DataAccess.Command).Parameters["@ClientTypeID"].Value = rec.ClientTypeId;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerID"].Value = rec.BrokerId;
            ((SqlCommand)DataAccess.Command).Parameters["@TotalTradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@TotalCommission"].Value = rec.Commission;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculationPeriodId"].Value = rec.CalculPeriodId;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int DeleteTradingData(string calculPeriodId)
        {
            int affected = 0;
            string query = "DELETE ClientTradeData WHERE calculationPeriodId = $CALCULATIONID$";
            query = query.Replace("$CALCULATIONID$", calculPeriodId);
            affected = DataAccess.ExecuteNonQuery(query);
            return affected;
        }

    }
}