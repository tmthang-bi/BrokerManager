<%@ Page Language="C#" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="ImportBrokerList.aspx.cs" Inherits="BrokerManagement_ImportBrokerList" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
<table>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        Broker list file:&nbsp;
                    </td>
                    <td>
                        <asp:FileUpload ID="fupData" runat="server" Width="450px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="btnImport_Click"/>
                    </td>
                    <td>
                        <asp:Button ID="btnDeleteCurrentData" runat="server" Text="Delete all broker" OnClick="btnDeleteCurrentData_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

