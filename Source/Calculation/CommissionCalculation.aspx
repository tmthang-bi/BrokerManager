<%@ Page Language="C#" StylesheetTheme="BrokerManagementTheme" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="CommissionCalculation.aspx.cs" Inherits="Calculation_CommissionCalculation" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    
    <table style="top: 0px; width:100%"; border = "0";>
    <tr>
      <td valign ="top">
        <table>
          <tr>
            <td colspan="2">
              <b style="text-decoration: underline">Calculation period</b>
            </td>
            
          </tr>
          <tr>     
            <td>
              <b>Month</b>
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
            <td width = "150">
                <b>Year</b>
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
          </tr>  
          <tr>
            <td align="center" style="height: 26px">
                <asp:Button ID="btnCreate" runat="server" Text="New Period" Width="110px" OnClick="BtnNew_OnClick" />
            </td>
            <td align="center" style="height: 26px">
                <asp:Button ID="btnView" runat="server" Text="Load" Width="110px" OnClick="BtnView_OnClick" />
            </td>
          </tr>
         </table>    
        <br />
      </td>  
    </tr>
    <tr>
        <td valign ="top" style="color:Blue; font-size:medium; font-weight:bold"; >
            <b>The calculation workflow status</b>
        </td>
    </tr>
    <tr>
      <td valign ="top">
        <asp:GridView ID="grdCalculationPeriod" runat="server" EnableViewState="true" SkinID = "Professional" AutoGenerateColumns="false" OnRowCommand="grdCalculationPeriod_RowCommand">
            <Columns> 
                <asp:TemplateField HeaderText="Stept"  HeaderStyle-HorizontalAlign="Left" ControlStyle-Width = "50px"> 
                    <ItemTemplate> 
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Stept") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Description"  HeaderStyle-HorizontalAlign="Left" ControlStyle-Width = "300px"> 
                    <ItemTemplate> 
                        <asp:Label ID="lblDes" runat="server" Text='<%# Bind("Description") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Status"  HeaderStyle-HorizontalAlign="Left"  ControlStyle-Width = "150px"> 
                    <ItemTemplate> 
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkCheck" runat="server" CausesValidation="False" CommandName="Execute" CommandArgument='<%# Bind("Stept")%>' Text="Execute"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView> 
      </td>  
    </tr>
    </table>
    
   
</asp:Content>

