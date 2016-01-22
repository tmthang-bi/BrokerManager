SELECT 
r.BrokerID as ID, b.BrokerTypeID, b.AECommissionCode,
o.BranchID, b.OfficeID, 
b.[Name], TradingValue, InheritedTradingValue, PrivateTradingValue, 
GrossRevenue, InheritedGrossRevenue, PrivateGrossRevenue, NetRevenue, 
InheritedNetRevenue, PrivateNetRevenue, 
InheritedCommissionRate, InheritedCommissionPayment, 
PrivateCommissionRate , PrivateCommissionRate2, PrivateCommissionPayment, OtherPayment, SubtotalNetRevenue, 
SubtotalInheritedNetRevenue, SubtotalPrivateNetRevenue, 
SubtotalTradingValue, SubtotalInheritedTradingValue, SubtotalPrivateTradingValue,SupervisorPayment,Payment,b2.[Name] as SupervisorName 
FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID WHERE r.PeriodId = '201601'