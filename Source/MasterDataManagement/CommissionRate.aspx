<%@ Page Language="C#" Theme="BrokerManagementTheme" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="CommissionRate.aspx.cs" Inherits="MasterDataManagement_CommissionRate" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    Client Type: 
    <asp:DropDownList ID="ddlClientTypeFilter" runat="server" DataSourceID="DataSourceClientType" DataTextField="Name" DataValueField="ID">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
    Level: 
    <asp:DropDownList ID="ddlBrokerTypeFilter" runat="server" DataSourceID="DataSourceBrokerType" DataTextField="Name" DataValueField="ID">
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="btnFilter" runat="server" Text="Filter" />
    
    <br />
    <br />
    
    <asp:GridView ID="grvCommissionRate" SkinID="Professional" runat="server" ShowFooter="True" DataSourceID="DataSourceCommissionRate" DataKeyNames="ID" OnRowCommand="grvCommissionRate_RowCommand">
        <Columns>
                    
            <asp:TemplateField HeaderText="Client Type" SortExpression="ClientTypeID">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlEditClientType" runat="server" DataSourceID="DataSourceClientType" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("ClientTypeID") %>'></asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ClientTypeName") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAddClientType" runat="server" DataSourceID="DataSourceClientType" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("ClientTypeID") %>'></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>                       
            
            <asp:TemplateField HeaderText="Lower Limit" SortExpression="LowerLimit">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LowerLimit") %>' Width="90px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LowerLimit", "{0:#,0}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddLowerLimit" runat="server" Text='<%# Bind("LowerLimit") %>' Width="90px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Upper Limit" SortExpression="UpperLimit">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("UpperLimit") %>' Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("UpperLimit", "{0:#,0}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddUpperLimit" runat="server" Text='<%# Bind("UpperLimit") %>' Width="100px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Commission Rate" SortExpression="CommissionRate">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("CommissionRate") %>' Width="50px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("CommissionRate", "{0:#,0.00}") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddCommissionRate" runat="server" Text='<%# Bind("CommissionRate") %>' Width="50px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="UpdateBy" HeaderText="Update By" SortExpression="UpdateBy" ReadOnly="True" />
            <asp:BoundField DataField="UpdateTime" HeaderText="Update Time" SortExpression="UpdateTime" ReadOnly="True" />            
            
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon-pencil.gif" CommandName="Edit" ToolTip="Edit" />
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon-delete.gif" CommandName="Delete" ToolTip="Delete"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon-floppy.gif" CommandName="Update" ToolTip="Update" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon-cancel.PNG" CommandName="Cancel" ToolTip="Cancel"/>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="True" CommandName="InsertNew" Text="Add"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        
        <EmptyDataTemplate>
            Create first Commission Rate record:<br />
            Client Type:<asp:DropDownList ID="ddlNoDataClientType" runat="server" DataSourceID="DataSourceClientType" DataTextField="Name" DataValueField="ID" ></asp:DropDownList>
            Level:<asp:DropDownList ID="ddlNoDataBrokerTypeID" runat="server" DataSourceID="DataSourceBrokerType" DataTextField="Name" DataValueField="ID" ></asp:DropDownList>
            Lower Limit:<asp:TextBox runat="server" ID="txbNoDataLowerLimit" Text="0" Width="90px" />
            Upper Limit:<asp:TextBox runat="server" ID="txbNoDataUpperLimit" Width="100px" />
            Commission Rate:<asp:TextBox runat="server" ID="txbNoDataCommissionRate" Width="50px" />
            <asp:Button runat="server" ID="btnNoDataInsertCommissionRate" CommandName="NoDataInsert" Text="Insert"/>
        </EmptyDataTemplate>
    </asp:GridView>
    
    <asp:SqlDataSource ID="DataSourceCommissionRate" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT a.[ID], [ClientTypeID], b.[Name] as ClientTypeName, [Code], [LowerLimit], [UpperLimit], [CommissionRate], [UpdateBy], [UpdateTime] &#13;&#10;FROM [CommissionRate] a inner join ClientType b on a.ClientTypeID = b.ID&#13;&#10;ORDER BY [ClientTypeID], [LowerLimit]" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE CommissionRate SET ClientTypeID = @ClientTypeID, LowerLimit = @LowerLimit, UpperLimit = @UpperLimit, CommissionRate = @CommissionRate, UpdateBy = @UpdateBy, UpdateTime = GETDATE() WHERE (ID = @original_ID)" InsertCommand="INSERT INTO CommissionRate(ClientTypeID, LowerLimit, UpperLimit, CommissionRate, BasicSalary, Allowance, UpdateBy, UpdateTime) VALUES (@ClientTypeID, @LowerLimit, @UpperLimit, @CommissionRate, @UpdateBy, GETDATE())" OnInserting="DataSourceCommissionRate_Inserting" DeleteCommand="DELETE FROM CommissionRate WHERE (ID = @original_ID)">
        <UpdateParameters>
            <asp:Parameter Name="ClientTypeID" />
            <asp:Parameter Name="Code" />
            <asp:Parameter Name="LowerLimit" DefaultValue="0" />
            <asp:Parameter Name="UpperLimit" DefaultValue="0" />
            <asp:Parameter Name="CommissionRate" DefaultValue="0" />
            <asp:SessionParameter Name="UpdateBy" SessionField="Session key Username" />
            <asp:Parameter Name="original_ID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ClientTypeID" />
            <asp:Parameter Name="Code" />
            <asp:Parameter Name="LowerLimit" DefaultValue="0" />
            <asp:Parameter Name="UpperLimit" DefaultValue="0" />
            <asp:Parameter Name="CommissionRate" DefaultValue="0" />
            <asp:Parameter Name="UpdateBy" />
        </InsertParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_ID" />
        </DeleteParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="DataSourceClientType" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [ClientType] ORDER BY [Name] DESC"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="DataSourceBrokerType" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [BrokerType] ORDER BY [ID]"></asp:SqlDataSource>
    
</asp:Content>
