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
    public class CalculationResultDAL : BaseDAL
    {
        public CalculationResultDAL()
            : base()
        {
        }

        public int InsertBrokerCalculationResult(BrokerCalculationResult rec)
        {
            int affected = 0;
            string query = @"
                INSERT INTO BrokerCalculationResult
                    (PeriodId, 
                    BrokerID, 
                    CalculatePeriod, 
                    Payment, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue,
                    PrivateGrossRevenue, 
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue, 
                    AverageNetRevenue, 
                    InheritedCommissionRate, 
                    InheritedCommissionPayment, 
                    PrivateCommissionRate, 
                    PrivateCommissionPayment, 
                    ManagementBonusRate, 
                    SupervisorPayment,
                    OtherPayment,  
                    SubtotalNetRevenue, 
                    SubtotalInheritedNetRevenue, 
                    SubtotalPrivateNetRevenue,
                    SubtotalTradingValue, 
                    SubtotalInheritedTradingValue, 
                    SubtotalPrivateTradingValue,
                    PrivateCommissionRate1,
                    PrivateCommissionRate2)
                VALUES
                    (@PeriodId, 
                    @BrokerID, 
                    @CalculatePeriod, 
                    @Payment,
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue, 
                    @PrivateGrossRevenue,
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue, 
                    @AverageNetRevenue, 
                    @InheritedCommissionRate, 
                    @InheritedCommissionPayment,
                    @PrivateCommissionRate, 
                    @PrivateCommissionPayment,
                    @ManagementBonusRate,  
                    @SupervisorPayment, 
                    @OtherPayment, 
                    @SubtotalNetRevenue, 
                    @SubtotalInheritedNetRevenue, 
                    @SubtotalPrivateNetRevenue,
                    @SubtotalTradingValue, 
                    @SubtotalInheritedTradingValue,
                    @SubtotalPrivateTradingValue,
                    @PrivateCommissionRate1,
                    @PrivateCommissionRate2)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Payment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@AverageNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedCommissionRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedCommissionPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ManagementBonusRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupervisorPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OtherPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalInheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalPrivateNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalInheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalPrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate1", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate2", SqlDbType.Float);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerID"].Value = rec.BrokerId;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@Payment"].Value = rec.Payment;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@AverageNetRevenue"].Value = rec.AverageNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedCommissionRate"].Value = rec.InheritedCommissionRate;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedCommissionPayment"].Value = rec.InheritedCommissionPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate"].Value = rec.PrivateCommissionRate;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionPayment"].Value = rec.PrivateCommissionPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@ManagementBonusRate"].Value = rec.ManagementBonusRate;
            ((SqlCommand)DataAccess.Command).Parameters["@SupervisorPayment"].Value = rec.SupervisorPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@OtherPayment"].Value = rec.OtherPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalNetRevenue"].Value = rec.SubtotalNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalInheritedNetRevenue"].Value = rec.SubtotalInheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalPrivateNetRevenue"].Value = rec.SubtotalPrivateNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalTradingValue"].Value = rec.SubtotalTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalInheritedTradingValue"].Value = rec.SubtotalInheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalPrivateTradingValue"].Value = rec.SubtotalPrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate1"].Value = rec.PrivateCommissionRate1;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate2"].Value = rec.PrivateCommissionRate2;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int InsertBrokerCalculationResultLog(BrokerCalculationResult rec, DateTime calculTime)
        {
            int affected = 0;
            string query = @"
                INSERT INTO BrokerCalculationResultLog 
                    (PeriodId, 
                    CalculateTime,
                    BrokerID, 
                    CalculatePeriod, 
                    Payment, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue,
                    PrivateGrossRevenue, 
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue, 
                    AverageNetRevenue, 
                    InheritedCommissionRate, 
                    InheritedCommissionPayment, 
                    PrivateCommissionRate, 
                    PrivateCommissionPayment, 
                    ManagementBonusRate, 
                    SupervisorPayment,
                    OtherPayment,  
                    SubtotalNetRevenue, 
                    SubtotalInheritedNetRevenue, 
                    SubtotalPrivateNetRevenue,
                    SubtotalTradingValue, 
                    SubtotalInheritedTradingValue, 
                    SubtotalPrivateTradingValue,
                    PrivateCommissionRate1,PrivateCommissionRate2)
                VALUES
                    (@PeriodId, 
                    @CalculateTime,
                    @BrokerID, 
                    @CalculatePeriod, 
                    @Payment,
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue,
                    @PrivateGrossRevenue, 
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue, 
                    @AverageNetRevenue, 
                    @InheritedCommissionRate, 
                    @InheritedCommissionPayment,
                    @PrivateCommissionRate, 
                    @PrivateCommissionPayment,
                    @ManagementBonusRate, 
                    @SupervisorPayment, 
                    @OtherPayment, 
                    @SubtotalNetRevenue, 
                    @SubtotalInheritedNetRevenue, 
                    @SubtotalPrivateNetRevenue,
                    @SubtotalTradingValue, 
                    @SubtotalInheritedTradingValue,
                    @SubtotalPrivateTradingValue,
                    @PrivateCommissionRate1,@PrivateCommissionRate2)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculateTime", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@Payment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@AverageNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedCommissionRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedCommissionPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ManagementBonusRate", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SupervisorPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OtherPayment", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalInheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalPrivateNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalInheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@SubtotalPrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate1", SqlDbType.Float);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateCommissionRate2", SqlDbType.Float);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculateTime"].Value = calculTime;
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerID"].Value = rec.BrokerId;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@Payment"].Value = rec.Payment;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@AverageNetRevenue"].Value = rec.AverageNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedCommissionRate"].Value = rec.InheritedCommissionRate;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedCommissionPayment"].Value = rec.InheritedCommissionPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate"].Value = rec.PrivateCommissionRate;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionPayment"].Value = rec.PrivateCommissionPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@ManagementBonusRate"].Value = rec.ManagementBonusRate;
            ((SqlCommand)DataAccess.Command).Parameters["@SupervisorPayment"].Value = rec.SupervisorPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@OtherPayment"].Value = rec.OtherPayment;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalNetRevenue"].Value = rec.SubtotalNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalInheritedNetRevenue"].Value = rec.SubtotalInheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalPrivateNetRevenue"].Value = rec.SubtotalPrivateNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalTradingValue"].Value = rec.SubtotalTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalInheritedTradingValue"].Value = rec.SubtotalInheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@SubtotalPrivateTradingValue"].Value = rec.SubtotalPrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate1"].Value = rec.PrivateCommissionRate1;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateCommissionRate2"].Value = rec.PrivateCommissionRate2;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public Hashtable GetAvgNetRevenue(DateTime targetMonth)
        {
            Hashtable retData = new Hashtable();
            string query = @"SELECT BrokerID, AVG(NetRevenue) as AverageNetRevenue, COUNT(PeriodId) as Count
                            FROM BrokerCalculationResult
                            WHERE (0 < DATEDIFF(MONTH, CalculatePeriod, @TargetMonth)) AND (DATEDIFF(MONTH, CalculatePeriod, @TargetMonth) <= 3)
                            GROUP BY BrokerID";

            ((SqlCommand)DataAccess.Command).Parameters.Add("@TargetMonth", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters["@TargetMonth"].Value = targetMonth;

            DataSet dataSet = DataAccess.ExecuteQuery(query);

            if ((dataSet != null) && (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (dataSet.Tables[0].Rows[i][2] != DBNull.Value)
                    {
                        int count = (int) dataSet.Tables[0].Rows[i][2];
                        if (count >= 3)
                        {
                            //Broker brkTemp = new Broker(dataSet.Tables[0].Rows[i][0].ToString());
                            //BrokerCalculationResult data = new BrokerCalculationResult(brkTemp);
                            //data.AverageNetRevenue = (decimal)dataSet.Tables[0].Rows[i][1];
                            retData[dataSet.Tables[0].Rows[i][0].ToString()] = (decimal)dataSet.Tables[0].Rows[i][1];
                        }
                    }
                }
            }

            return retData;
        }

        public int DeleteBrokerCalculationResult(string id)
        {
            int affected = 0;
            string query = @"DELETE BrokerCalculationResult WHERE PeriodId = @PeriodId";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = id;
            affected = DataAccess.ExecuteNonQuery(query);
            return affected;
        }

        public int UpdateAllowance(BrokerCalculationResult rec)
        {
            int affected = 0;
            string query = @"
                UPDATE BrokerCalculationResult
                SET OtherPayment = @OtherPayment
                WHERE BrokerID = @BrokerID ";

            ((SqlCommand)DataAccess.Command).Parameters.Add("@BrokerID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OtherPayment", SqlDbType.Money);
            
            ((SqlCommand)DataAccess.Command).Parameters["@BrokerID"].Value = rec.BrokerId;
            ((SqlCommand)DataAccess.Command).Parameters["@OtherPayment"].Value = rec.OtherPayment;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }
        public int DeleteOfficeCalculResult(string id)
        {
            int affected = 0;
            string query = @"DELETE OfficeCalculationResult WHERE PeriodId = @ID";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ID", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters["@ID"].Value = id;
            affected = DataAccess.ExecuteNonQuery(query);
            return affected;
        }

        public int InsertOfficeCalculationResult(OfficeCalculationResult rec)
        {
            int affected = 0;
            string query = @"
                INSERT INTO OfficeCalculationResult
                    (periodId, 
                    OfficeID, 
                    CalculatePeriod, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue, 
                    PrivateGrossRevenue,
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue)
                VALUES
                    (@PeriodId, 
                    @OfficeID, 
                    @CalculatePeriod, 
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue, 
                    @PrivateGrossRevenue,
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OfficeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@OfficeID"].Value = rec.Office.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int InsertOfficeCalculationResultLog(OfficeCalculationResult rec, DateTime calculTime)
        {
            int affected = 0;
            string query = @"
                INSERT INTO OfficeCalculationResultLog
                    (PeriodId, 
                    CalculateTime, 
                    OfficeID, 
                    CalculatePeriod, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue, 
                    PrivateGrossRevenue,
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue)
                VALUES
                    (@PeriodId, 
                    @CalculateTime, 
                    @OfficeID, 
                    @CalculatePeriod, 
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue, 
                    @PrivateGrossRevenue,
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculateTime", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@OfficeID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculateTime"].Value = calculTime;
            ((SqlCommand)DataAccess.Command).Parameters["@OfficeID"].Value = rec.Office.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }


        public int DeleteBranchCalculResult(string id)
        {
            int affected = 0;
            string query = @"DELETE BranchCalculationResult WHERE PeriodId = @ID";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@ID", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters["@ID"].Value = id;
            affected = DataAccess.ExecuteNonQuery(query);
            return affected;
        }

        public int InsertBranchCalculationResult(BranchCalculationResult rec)
        {
            int affected = 0;
            string query = @"
                INSERT INTO BranchCalculationResult
                    (PeriodId, 
                    BranchID, 
                    CalculatePeriod, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue, 
                    PrivateGrossRevenue,
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue)
                VALUES
                    (@PeriodId, 
                    @BranchID, 
                    @CalculatePeriod, 
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue, 
                    @PrivateGrossRevenue,
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BranchID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@BranchID"].Value = rec.Branch.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

        public int InsertBranchCalculationResultLog(BranchCalculationResult rec, DateTime calculTime)
        {
            int affected = 0;
            string query = @"
                INSERT INTO BranchCalculationResultLog
                    (PeriodId, 
                    CalculateTime, 
                    BranchID, 
                    CalculatePeriod, 
                    TradingValue, 
                    InheritedTradingValue, 
                    PrivateTradingValue, 
                    GrossRevenue, 
                    InheritedGrossRevenue, 
                    PrivateGrossRevenue,
                    NetRevenue, 
                    InheritedNetRevenue, 
                    PrivateNetRevenue)
                VALUES
                    (@PeriodId, 
                    @CalculateTime, 
                    @BranchID, 
                    @CalculatePeriod, 
                    @TradingValue, 
                    @InheritedTradingValue, 
                    @PrivateTradingValue, 
                    @GrossRevenue, 
                    @InheritedGrossRevenue, 
                    @PrivateGrossRevenue,
                    @NetRevenue, 
                    @InheritedNetRevenue, 
                    @PrivateNetRevenue)";
            ((SqlCommand)DataAccess.Command).Parameters.Clear();
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PeriodId", SqlDbType.Char, 6);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculateTime", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@BranchID", SqlDbType.VarChar);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@CalculatePeriod", SqlDbType.DateTime);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@TradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateTradingValue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@GrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateGrossRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@NetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@InheritedNetRevenue", SqlDbType.Money);
            ((SqlCommand)DataAccess.Command).Parameters.Add("@PrivateNetRevenue", SqlDbType.Money);

            ((SqlCommand)DataAccess.Command).Parameters["@PeriodId"].Value = rec.PeriodId;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculateTime"].Value = calculTime;
            ((SqlCommand)DataAccess.Command).Parameters["@BranchID"].Value = rec.Branch.Id;
            ((SqlCommand)DataAccess.Command).Parameters["@CalculatePeriod"].Value = rec.CalculatePeriod;
            ((SqlCommand)DataAccess.Command).Parameters["@TradingValue"].Value = rec.TradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedTradingValue"].Value = rec.InheritedTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateTradingValue"].Value = rec.PrivateTradingValue;
            ((SqlCommand)DataAccess.Command).Parameters["@GrossRevenue"].Value = rec.GrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedGrossRevenue"].Value = rec.InheritedGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateGrossRevenue"].Value = rec.PrivateGrossRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@NetRevenue"].Value = rec.NetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@InheritedNetRevenue"].Value = rec.InheritedNetRevenue;
            ((SqlCommand)DataAccess.Command).Parameters["@PrivateNetRevenue"].Value = rec.PrivateNetRevenue;

            affected = DataAccess.ExecuteNonQuery(query);

            return affected;
        }

    }
}