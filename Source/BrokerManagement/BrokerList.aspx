<%@ Page Language="C#" StylesheetTheme="BrokerManagementTheme" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="BrokerList.aspx.cs" Inherits="BrokerManagement_BrokerList" %>
<%@ Register Src="~/UserControl/ucBrokerFilter.ascx" TagName="ucBrokerFilter" TagPrefix="uc" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    <table style="padding-bottom:15px">
        <tr>
            <td>
                <uc:ucBrokerFilter ID="ucBrokerFilter" runat="server" />
            </td>
            <td style="vertical-align:bottom;padding-bottom:3px">
                <asp:Button ID="btnFilter" runat="server" Text="Search" OnClick="btnFilter_Click" Height="25px"/>
            </td>
            <td style="vertical-align:bottom; padding-bottom:3px">
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" Height="25px"/>
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="grvBroker"  SkinID="Professional" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="ID" DataSourceID="DataSourceBrokerList" OnRowCommand="grvBroker_RowCommand">
        <Columns>
           
            <asp:TemplateField HeaderText="Location" SortExpression="BranchID">
                <ItemTemplate>
                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Office" SortExpression="OfficeName">
                <ItemTemplate>
                    <asp:Label ID="lblOffice" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Emp Code" SortExpression="ID" >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Level" SortExpression="BrokerTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblBrokerTypeID" runat="server" Text='<%# Eval("BrokerTypeID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Full Name" SortExpression="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Direct Manager" 
                SortExpression="SupervisorBrokerID">
                <ItemTemplate>
                    <asp:Label ID="lblSupervisor" runat="server" Text='<%# Eval("SupervisorName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Entrance Date" SortExpression="EntranceDate">
                <ItemTemplate>
                    <asp:Label ID="lblEntranceDate" runat="server" Text='<%# Eval("EntranceDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Leave Date" SortExpression="LeaveDate">
                <ItemTemplate>
                    <asp:Label ID="lblLeaveDate" runat="server" Text='<%# Eval("LeaveDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    <asp:SqlDataSource ID="DataSourceBrokerType" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [BrokerType]"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="DataSourceOffice" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [Office]"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="DataSourceBrokerList" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT a.[ID], a.[BrokerTypeID], o.BranchID, a.[OfficeID], o.[Name] as 'OfficeName', a.[SupervisorBrokerID], b.[Name] as SupervisorName, a.[Name],  a.[Email], a.[EntranceDate], a.[LeaveDate], a.[IsActive], a.[UpdateBy], a.[UpdateTime] FROM [Broker] a left outer join [Broker] b on a.SupervisorBrokerID = b.ID inner join [Office] o on a.OfficeID = o.ID ">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="DataSourceBrokerManagerList" runat="server"
        ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>" SelectCommand="SELECT [ID], [BrokerTypeID], [Name] FROM [Broker] WHERE (BrokerTypeID = 'SUP') OR (BrokerTypeID = 'MAN') UNION SELECT NULL, NULL, 'N/A' ORDER BY [BrokerTypeID], [Name], [ID]">
    </asp:SqlDataSource>
    
</asp:Content>
