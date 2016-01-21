<%@ Page Language="C#" StylesheetTheme="BrokerManagementTheme" AutoEventWireup="true" masterpagefile="~/MasterPage/Main.master" CodeFile="AEReport.aspx.cs" Inherits="Report_AEReport" %>

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
                            <uc:ucBrokerFilter ID="ucBrokerFilter" runat="server" BrokerType="AE"/>
                        </td>
                        <td style="vertical-align:bottom;padding-bottom:3px">
                            <asp:Button ID="btnFilter" runat="server" Text="Search" Height="25px" OnClick="btnFilter_Click" />
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
                <asp:GridView ID="gvBrokerResult" SkinID="Professional" runat="server" AllowPaging="True" AllowSorting="True"
                    DataKeyNames="ResultID,ID" DataSourceID="DataSourceBrokerCalculationResult">
                    
                    <Columns>
                        <asp:BoundField DataField="ResultID" HeaderText="ResultID" SortExpression="ResultID" Visible="False" ReadOnly="True" />
                        <asp:BoundField DataField="BranchID" HeaderText="Location" ReadOnly="True"
                            SortExpression="BranchID" />
                        <asp:BoundField DataField="OfficeID" HeaderText="Office" ReadOnly="True" 
                            SortExpression="OfficeID" />
                        <asp:BoundField DataField="ID" HeaderText="Emp Code" SortExpression="ID" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Full Name" ReadOnly="True" 
                            SortExpression="Name" />
                        <asp:BoundField DataField="NetRevenue" HeaderText="PGDR (KH Moi + Cu)" SortExpression="NetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedTradingValue" HeaderText="GTGD (KH Cu)" SortExpression="InheritedTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedGrossRevenue" HeaderText="PGD (KH Cu)" SortExpression="InheritedGrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedNetRevenue" HeaderText="PGDR (KH Cu)" SortExpression="InheritedNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedCommissionRate" HeaderText="Ty le thuong (KH cu)" 
                            SortExpression="InheritedCommissionRate" ReadOnly="True" DataFormatString="{0:#,0.00}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedCommissionPayment" HeaderText="Hoa hong (KH cu)" 
                            SortExpression="InheritedCommissionPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateTradingValue" HeaderText="GTGD (KH Moi)" SortExpression="PrivateTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateGrossRevenue" HeaderText="PGD (KH Moi)" SortExpression="PrivateGrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateNetRevenue" HeaderText="PGDR (KH Moi)" SortExpression="PrivateNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateCommissionRate" HeaderText="Ty le thuong (KH moi)" 
                            SortExpression="PrivateCommissionRate" ReadOnly="True" DataFormatString="{0:#,0.00}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateCommissionRate2" HeaderText="Ty le thuong luy tien (KH moi)" 
                            SortExpression="PrivateCommissionRate2" ReadOnly="True" DataFormatString="{0:#,0.00}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateCommissionPayment" HeaderText="Hoa hong (KH moi)" 
                            SortExpression="PrivateCommissionPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OtherPayment" HeaderText="Allowance" 
                            SortExpression="OtherPayment" DataFormatString="{0:#,0}" 
                            ApplyFormatInEditMode="True" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Payment" HeaderText="Total" SortExpression="Payment" 
                            ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SupervisorName" HeaderText="Manager" ReadOnly="True"
                            SortExpression="SupervisorName" />
                    </Columns>
                    
                </asp:GridView>
                
                <asp:SqlDataSource ID="DataSourceBrokerCalculationResult" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
                    SelectCommand="SELECT r.PeriodId as ResultID, r.BrokerID as ID, b.BrokerTypeID, o.BranchID, b.OfficeID, b.[Name], b2.[Name] as SupervisorName, Payment, TradingValue, InheritedTradingValue, PrivateTradingValue, GrossRevenue, InheritedGrossRevenue, PrivateGrossRevenue, NetRevenue, InheritedNetRevenue, PrivateNetRevenue, AverageNetRevenue, InheritedCommissionRate, InheritedCommissionPayment, PrivateCommissionRate, PrivateCommissionPayment, SupervisorPayment, OtherPayment, SubtotalNetRevenue, SubtotalInheritedNetRevenue, SubtotalPrivateNetRevenue, SubtotalTradingValue, SubtotalInheritedTradingValue, SubtotalPrivateTradingValue,PrivateCommissionRate1,PrivateCommissionRate2,NetRevenue FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID WHERE (r.PeriodId = @ID) and (b.BrokerTypeID = 'AE')" 
                    OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:ControlParameter Name="ID" ControlID="lblResultIDParam" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
            </td>
        </tr>
    </table>
</asp:Content>
