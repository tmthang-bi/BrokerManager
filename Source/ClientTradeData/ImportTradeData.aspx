<%@ Page Language="C#" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="ImportTradeData.aspx.cs" Inherits="ClientTradeData_ImportTradeData" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">

<table style="top: 0px; width:100%"; border = "0";>
    <tr>
      <td valign ="top">
        <table>
          <tr>
            <td colspan = "2">
              <b style="text-decoration: underline">Calculation period</b>
            </td>
            <td>
                <b style="text-decoration: underline">Locate trading data</b>
            </td>
          </tr>
          <tr>
            <td><b>Month</b></td>     
            <td>
              <asp:DropDownList ID="cmbMonth" runat="server" EnableViewState ="true">
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
            <td>
                <input id="uploadTradingData" type="file" runat = "server"/>
            </td>
          </tr>  
          <tr>
            <td><strong>Year</strong></td>
            <td>
                <asp:DropDownList ID="cmbYear" runat="server" EnableViewState ="true">
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
            </td>
            <td align="right">
                <asp:Button ID="btnRead" runat="server" Text="ReadFile" Width="110px" OnClick="BtnRead_OnClick" />
            </td>
          </tr>
          <tr>
            <td colspan = "3" align="left">
                <asp:LinkButton ID="lnkBack" runat="server" Text = "Return"></asp:LinkButton>
            </td>
            
          </tr>
         </table>    
        <br />
      </td>  
    </tr>
    <tr>
        <td>
            <asp:Panel ID = "pnlImport" runat = "server">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnImport" runat="server" Text="Import" Width="110px" OnClick="BtnImport_OnClick" />        
                            <asp:HiddenField ID = "hiddenDataExit" runat="server" Value = "" EnableViewState="true" />                
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="scroll_box" style="height: 500px; width: 500px;overflow: auto;">
                                <asp:GridView ID="grdTradingData" runat="server" AutoGenerateColumns="true" 
                                CellPadding="4" ForeColor="Black" GridLines="None" ShowFooter="True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"> 
                                <FooterStyle BackColor="Tan" />    
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                </asp:GridView> 
                             </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>

</asp:Content>
