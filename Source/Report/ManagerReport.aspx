<%@ Page Language="C#" StylesheetTheme="BrokerManagementTheme" AutoEventWireup="true" masterpagefile="~/MasterPage/Main.master" CodeFile="ManagerReport.aspx.cs" Inherits="Report_ManagerReport" %>

<%@ Register Src="~/UserControl/ucCommissionMonthPicker.ascx" TagName="ucCommissionMonthPicker" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ucBrokerFilter.ascx" TagName="ucBrokerFilter" TagPrefix="uc" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    <table>
        <tr>
            <td>
                <table style="padding-bottom:15px">
                    <tr>
                        <td style="width:200px;">
                            <uc:ucCommissionMonthPicker ID="ucCommissionMonthPicker" runat="server" />
                            <asp:Label ID="lblResultIDParam" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <uc:ucBrokerFilter ID="ucBrokerFilter" runat="server" BrokerType="MAN" />
                        </td>
                        <td style="vertical-align:bottom;padding-bottom:3px">
                            <asp:Button ID="btnFilter" runat="server" Text="Search" Height="25px" OnClick="btnFilter_Click"/>
                        </td>
                        <td style="vertical-align:bottom;padding-bottom:3px">
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Height="25px" OnClick="btnExport_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBrokerResult" runat="server" DataKeyNames="ResultID,ID" SkinID="Professional"
                    DataSourceID="DataSourceBrokerCalculationResult">
                    <Columns>
                        <asp:BoundField DataField="ResultID" HeaderText="ResultID" SortExpression="ResultID" Visible="False" ReadOnly="True" />
                        <asp:BoundField DataField="BranchID" HeaderText="Location" ReadOnly="True"
                            SortExpression="BranchID" />
                        <asp:BoundField DataField="OfficeID" HeaderText="Office" ReadOnly="True" 
                            SortExpression="OfficeID" />
                        <asp:BoundField DataField="ID" HeaderText="Emp Code" SortExpression="ID" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
                            SortExpression="Name" />
                        <asp:BoundField DataField="TradingValue" HeaderText="GTGD" SortExpression="TradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GrossRevenue" HeaderText="PGD" SortExpression="GrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NetRevenue" HeaderText="PGDR" SortExpression="NetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedCommissionRate" HeaderText="Tỉ lệ thưởng (KH cu)" SortExpression="InheritedCommissionRate" ReadOnly="True" DataFormatString="{0:#,0.00}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedCommissionPayment" HeaderText="Tiền thưởng (KH cu)" SortExpression="InheritedCommissionPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         <asp:BoundField DataField="PrivateCommissionRate" HeaderText="Tỉ lệ thưởng (KH moi)" SortExpression="PrivateCommissionRate" ReadOnly="True" DataFormatString="{0:#,0.00}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateCommissionPayment" HeaderText="Tiền thưởng (KH moi)" SortExpression="PrivateCommissionPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OfficeTradingValue" DataFormatString="{0:#,0}" HeaderText="GTGD P.GD"
                            ReadOnly="True" SortExpression="OfficeTradingValue">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OfficeRevenue" DataFormatString="{0:#,0}" HeaderText="PGDR P.GD"
                            ReadOnly="True" SortExpression="OfficeRevenue">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ManagementBonusRate" DataFormatString="{0:#,0.000}" HeaderText="Bonus Rate"
                            ReadOnly="True" SortExpression="ManagementBonusRate" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SupervisorPayment" HeaderText="Bonus" 
                            ReadOnly="True" SortExpression="SupervisorPayment" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Payment" HeaderText="Total" SortExpression="Payment" 
                            ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    
                </asp:GridView>
                <asp:SqlDataSource ID="DataSourceBrokerCalculationResult" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
                    SelectCommand="SELECT r.PeriodId as ResultID, r.BrokerID as ID, b.BrokerTypeID, o.BranchID, b.OfficeID, ofr.TradingValue as 'OfficeTradingValue', ofr.NetRevenue as 'OfficeRevenue', b.[Name], b2.[Name] as SupervisorName, Payment, r.TradingValue, r.InheritedTradingValue, r.PrivateTradingValue, r.GrossRevenue, r.InheritedGrossRevenue, r.PrivateGrossRevenue, r.NetRevenue, r.InheritedNetRevenue, r.PrivateNetRevenue, r.AverageNetRevenue, r.InheritedCommissionRate, r.InheritedCommissionPayment, r.PrivateCommissionRate, r.PrivateCommissionPayment, r.ManagementBonusRate, r.SupervisorPayment, r.OtherPayment, r.SubtotalNetRevenue, r.SubtotalInheritedNetRevenue, r.SubtotalPrivateNetRevenue, r.SubtotalTradingValue, r.SubtotalInheritedTradingValue, r.SubtotalPrivateTradingValue FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID INNER JOIN OfficeCalculationResult ofr ON b.OfficeID = ofr.OfficeID WHERE (r.PeriodId = @ID) AND (b.BrokerTypeID = 'MAN') AND (ofr.PeriodId = @ID) ORDER BY o.BranchID" 
                    OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:ControlParameter Name="ID" ControlID="lblResultIDParam" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
            </td>
        </tr>
    </table>
</asp:Content>

