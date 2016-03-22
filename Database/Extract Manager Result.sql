SELECT r.BrokerID as ID, b.BrokerTypeID, o.BranchID, 
b.OfficeID , 0 as groupNum, b.[Name], b2.[Name] as SupervisorName, 
 r.TradingValue, r.GrossRevenue, r.NetRevenue,ofr.TradingValue as 'OfficeTradingValue', 
ofr.NetRevenue as 'OfficeRevenue', r.ManagementBonusRate, r.SupervisorPayment, Payment
FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID INNER JOIN OfficeCalculationResult ofr ON b.OfficeID = ofr.OfficeID WHERE (r.PeriodId = '201603') AND (b.BrokerTypeID = 'MAN') AND (ofr.PeriodId = '201603') ORDER BY ID