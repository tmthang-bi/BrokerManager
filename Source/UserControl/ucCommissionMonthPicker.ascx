<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommissionMonthPicker.ascx.cs" Inherits="UserControl_ucCommissionMonthPicker" %>
<table>
    <tr>
        <td colspan="2">
        <b style="text-decoration: underline">Calculation period</b>
        </td>
    </tr>
    <tr>
        <td>
            Year: 
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True">
                <asp:ListItem>2010</asp:ListItem>
                <asp:ListItem>2011</asp:ListItem>
                <asp:ListItem>2012</asp:ListItem>
                <asp:ListItem>2013</asp:ListItem>
                <asp:ListItem>2014</asp:ListItem>
                <asp:ListItem>2015</asp:ListItem>
                <asp:ListItem>2016</asp:ListItem>
                <asp:ListItem>2017</asp:ListItem>
                <asp:ListItem>2018</asp:ListItem>
                <asp:ListItem>2019</asp:ListItem>
                <asp:ListItem>2020</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            Month: 
            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True">
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>