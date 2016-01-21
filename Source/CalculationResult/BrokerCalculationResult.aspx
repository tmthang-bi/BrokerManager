<%@ Page Language="C#" AutoEventWireup="true" masterpagefile="~/MasterPage/Main.master" CodeFile="BrokerCalculationResult.aspx.cs" Inherits="CalculationResult_BrokerCalculationResult" %>
<%@ Register Src="~/UserControl/ucCommissionMonthPicker.ascx" TagName="ucCommissionMonthPicker" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ucBrokerFilter.ascx" TagName="ucBrokerFilter" TagPrefix="uc" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width:200px;">
                            <uc:ucCommissionMonthPicker ID="ucCommissionMonthPicker" runat="server" />
                            <asp:Label ID="lblResultIDParam" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <uc:ucBrokerFilter ID="ucBrokerFilter" runat="server" />
                        </td>
                        <td style="vertical-align:bottom">
                            <asp:Button ID="btnFilter" runat="server" Text="Filter" Height="25px" OnClick="btnFilter_Click"/>
                        </td>
                        <td style="vertical-align:bottom">
                            <asp:Button ID="btnExport" runat="server" Text="Excel Export" Height="25px" OnClick="btnExport_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBrokerResult" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="ResultID,ID"
                    DataSourceID="DataSourceBrokerCalculationResult" PageSize="30" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvBrokerResult_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ResultID" HeaderText="ResultID" SortExpression="ResultID" Visible="False" ReadOnly="True" />
                        <asp:BoundField DataField="ID" HeaderText="M&#227; NV" SortExpression="ID" ReadOnly="True" />
                        <asp:BoundField DataField="BrokerTypeID" HeaderText="Vai tr&#242;" ReadOnly="True" SortExpression="BrokerTypeID" />
                        <asp:BoundField DataField="BranchID" HeaderText="Chi nh&#225;nh" ReadOnly="True" SortExpression="BranchID" />
                        <asp:BoundField DataField="OfficeID" HeaderText="Văn ph&#242;ng" ReadOnly="True" SortExpression="OfficeID" />
                        <asp:BoundField DataField="Name" HeaderText="Họ t&#234;n" ReadOnly="True" SortExpression="Name" />
                        <asp:BoundField DataField="Payment" HeaderText="Tổng" SortExpression="Payment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CommissionPayment" HeaderText="Commission" SortExpression="CommissionPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OtherPayment" HeaderText="Phụ cấp" SortExpression="OtherPayment" DataFormatString="{0:#,0}" ApplyFormatInEditMode="True" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SupervisorPayment" HeaderText="Overriding" SortExpression="SupervisorPayment" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AverageNetRevenue" HeaderText="Ph&#237; GD TB 3 th&#225;ng" SortExpression="AverageNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SupervisorName" HeaderText="Quản l&#237;" ReadOnly="True"
                            SortExpression="SupervisorName" />
                        <asp:BoundField DataField="TradingValue" HeaderText="GTGD" SortExpression="TradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedTradingValue" HeaderText="GTGD KH cũ"
                            SortExpression="InheritedTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateTradingValue" HeaderText="GTGD KH mới"
                            SortExpression="PrivateTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GrossRevenue" HeaderText="PGD" SortExpression="GrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedGrossRevenue" HeaderText="PGD KH cũ"
                            SortExpression="InheritedGrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateGrossRevenue" HeaderText="Ph&#237; GD KH mới"
                            SortExpression="PrivateGrossRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NetRevenue" HeaderText="PGDR" SortExpression="NetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InheritedNetRevenue" HeaderText="PGDR KH cũ"
                            SortExpression="InheritedNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrivateNetRevenue" HeaderText="PGDR KH mới" SortExpression="PrivateNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalNetRevenue" HeaderText="PGDR Nh&#243;m" SortExpression="SubtotalNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalInheritedNetRevenue" HeaderText="PGDR Nh&#243;m (KH cũ)"
                            SortExpression="SubtotalInheritedNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalPrivateNetRevenue" HeaderText="PGDR Nh&#243;m (KH mới)"
                            SortExpression="SubtotalPrivateNetRevenue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalTradingValue" HeaderText="GTGD Nh&#243;m"
                            SortExpression="SubtotalTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalInheritedTradingValue" HeaderText="GTGD Nh&#243;m (KH cũ)"
                            SortExpression="SubtotalInheritedTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubtotalPrivateTradingValue" HeaderText="GTGD Nh&#243;m (KH mới)"
                            SortExpression="SubtotalPrivateTradingValue" ReadOnly="True" DataFormatString="{0:#,0}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>                        
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="DataSourceBrokerCalculationResult" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
                    SelectCommand="SELECT r.PeriodId as ResultID, r.BrokerID as ID, b.BrokerTypeID, o.BranchID, b.OfficeID, b.[Name], b2.[Name] as SupervisorName, Payment,InheritedCommissionPayment + PrivateCommissionPayment as CommissionPayment, TradingValue, InheritedTradingValue, PrivateTradingValue, GrossRevenue, InheritedGrossRevenue, PrivateGrossRevenue, NetRevenue, InheritedNetRevenue, PrivateNetRevenue, AverageNetRevenue, InheritedCommissionRate, InheritedCommissionPayment, PrivateCommissionRate, PrivateCommissionRate2, PrivateCommissionPayment, SupervisorPayment, OtherPayment, SubtotalNetRevenue, SubtotalInheritedNetRevenue, SubtotalPrivateNetRevenue, SubtotalTradingValue, SubtotalInheritedTradingValue, SubtotalPrivateTradingValue FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID WHERE r.PeriodID = @ID"
                    OldValuesParameterFormatString="original_{0}" 
                    UpdateCommand="UPDATE BrokerCalculationResult SET OtherPayment = @OtherPayment, Payment = CommissionPayment + SupervisorPayment + @OtherPayment, UpdateBy = @UpdateBy, UpdateTime = getdate() WHERE (ID = @original_ResultID) AND (BrokerID = @original_ID)">
                    <UpdateParameters>
                        <asp:Parameter Name="OtherPayment" />
                        <asp:Parameter Name="original_ID" />
                        <asp:Parameter Name="original_BrokerID" />
                        <asp:SessionParameter Name="UpdateBy" SessionField="Session key Username" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter Name="ID" ControlID="lblResultIDParam" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dataSourceResult" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
                    SelectCommand="SELECT r.BrokerID as ID, b.BrokerTypeID, b.AECommissionCode,o.BranchID, b.OfficeID, b.[Name], TradingValue, InheritedTradingValue, PrivateTradingValue, GrossRevenue, InheritedGrossRevenue, PrivateGrossRevenue, NetRevenue, InheritedNetRevenue, PrivateNetRevenue, InheritedCommissionRate, InheritedCommissionPayment, PrivateCommissionRate , PrivateCommissionRate2, PrivateCommissionPayment, OtherPayment, SubtotalNetRevenue, SubtotalInheritedNetRevenue, SubtotalPrivateNetRevenue, SubtotalTradingValue, SubtotalInheritedTradingValue, SubtotalPrivateTradingValue,SupervisorPayment,Payment,b2.[Name] as SupervisorName FROM BrokerCalculationResult r INNER JOIN Broker b ON r.BrokerID = b.ID LEFT OUTER JOIN Broker b2 on b.SupervisorBrokerID = b2.ID INNER JOIN Office o ON b.OfficeID = o.ID WHERE r.PeriodId = @ID"
                    OldValuesParameterFormatString="original_{0}">                
                    <SelectParameters>
                        <asp:ControlParameter Name="ID" ControlID="lblResultIDParam" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
