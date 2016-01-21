<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HSC - Broker Management</title>
</head>
<body>
    <form id="frmLogin" runat="server">
    <div>
        <table>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red">Please login!</asp:Label>
                    <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>   
            <tr>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Text="Username:" Width="84px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbUsername" runat="server"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </td>
            </tr>                      
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblErrorDetail" runat="server"></asp:Label></td>
            </tr> 
        </table>
    
    </div>
    </form>
</body>
</html>
