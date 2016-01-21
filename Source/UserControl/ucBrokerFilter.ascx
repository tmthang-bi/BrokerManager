<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBrokerFilter.ascx.cs" Inherits="UserControl_ucBrokerFilter" %>

<table border="0" cellpadding="3" cellspacing="0">
    <tr style="font-weight: bold; background-color: #006AD0; color:White">
        <td>Emp Code</td>
        <td>Level</td>
        <td>Office</td>
        <td>Name</td>
        <td>Manager</td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="txbFilterID" runat="server" Width="100px"></asp:TextBox>
        </td>
        <td>
            <asp:DropDownList ID="ddlFilterBrokerTypeID" runat="server" DataSourceID="DataSourceBrokerType" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="ddlFilterOffice" runat="server" DataSourceID="DataSourceOffice" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="txbFilterName" runat="server" Width="100px"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txbFilterSupervisor" runat="server" Width="100px"></asp:TextBox>
        </td>
    </tr>
</table>
<asp:SqlDataSource ID="DataSourceBrokerType" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [BrokerType] UNION SELECT '' AS ID, '' AS Name"></asp:SqlDataSource>
        
<asp:SqlDataSource ID="DataSourceOffice" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
    SelectCommand="SELECT [ID], [Name] FROM [Office] UNION SELECT '' AS ID, '' AS Name"></asp:SqlDataSource>