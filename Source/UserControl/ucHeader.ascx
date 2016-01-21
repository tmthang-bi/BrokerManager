<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeader.ascx.cs" Inherits="UserControl_ucHeader" %>

<table style="width:100%; " border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background-image: url(../Images/hsc_logo_bg.jpg); width: 100%; background-repeat: repeat-x">
            <asp:image runat="server" id="logo" ImageUrl="~/Images/hsc_en.jpg" />
        </td>
    </tr>
    <tr>
        <td style="width:100%;">
            <asp:menu id="MainMenu" runat="server" skinid="SampleMenuVertical" datasourceid="BrokerManagerSiteMap" />
            <asp:sitemapdatasource runat="server" id="BrokerManagerSiteMap" sitemapprovider="BrokerManagementMapProvider" showstartingnode="false" />
        </td>
    </tr>
</table>