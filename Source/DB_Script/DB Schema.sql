USE [BrokerManagement]
GO
/****** Object:  Table [dbo].[BrokerCalculationResult]    Script Date: 07/06/2010 14:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BrokerCalculationResult](
	[ID] [char](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CalculationID] [datetime] NOT NULL,
	[BrokerID] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CalculateMonth] [datetime] NOT NULL,
	[Payment] [money] NOT NULL,
	[TradingValue] [money] NOT NULL,
	[InheritedTradingValue] [money] NULL,
	[PrivateTradingValue] [money] NULL,
	[GrossRevenue] [money] NOT NULL,
	[NetRevenue] [money] NOT NULL,
	[InheritedNetRevenue] [money] NOT NULL,
	[PrivateNetRevenue] [money] NOT NULL,
	[AverageNetRevenue] [money] NULL,
	[CommissionRate] [float] NULL,
	[CommissionPayment] [money] NOT NULL,
	[SupervisorPayment] [money] NOT NULL,
	[OtherPayment] [money] NOT NULL,
	[SubtotalNetRevenue] [money] NOT NULL,
	[SubtotalInheritedNetRevenue] [money] NOT NULL,
	[SubtotalPrivateNetRevenue] [money] NOT NULL,
	[SubtotalTradingValue] [money] NULL,
	[SubtotalInheritedTradingValue] [money] NULL,
	[SubtotalPrivateTradingValue] [money] NULL,
 CONSTRAINT [PK_ResultData_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[BrokerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[BrokerCalculationResultLog]    Script Date: 07/06/2010 14:11:01 ******/
CREATE TABLE [dbo].[BrokerCalculationResultLog](
	[ID] [char](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CalculationID] [datetime] NOT NULL,
	[BrokerID] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CalculateMonth] [datetime] NOT NULL,
	[Payment] [money] NOT NULL,
	[TradingValue] [money] NOT NULL,
	[InheritedTradingValue] [money] NULL,
	[PrivateTradingValue] [money] NULL,
	[GrossRevenue] [money] NOT NULL,
	[NetRevenue] [money] NOT NULL,
	[InheritedNetRevenue] [money] NOT NULL,
	[PrivateNetRevenue] [money] NOT NULL,
	[AverageNetRevenue] [money] NULL,
	[CommissionRate] [float] NULL,
	[CommissionPayment] [money] NOT NULL,
	[SupervisorPayment] [money] NOT NULL,
	[OtherPayment] [money] NOT NULL,
	[SubtotalNetRevenue] [money] NOT NULL,
	[SubtotalInheritedNetRevenue] [money] NOT NULL,
	[SubtotalPrivateNetRevenue] [money] NOT NULL,
	[SubtotalTradingValue] [money] NULL,
	[SubtotalInheritedTradingValue] [money] NULL,
	[SubtotalPrivateTradingValue] [money] NULL,
 CONSTRAINT [PK_BrokerCalculationResultLog] PRIMARY KEY CLUSTERED 
(
	[CalculationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]




