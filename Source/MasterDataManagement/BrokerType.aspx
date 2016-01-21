<%@ Page Language="C#" Theme="BrokerManagementTheme" masterpagefile="~/MasterPage/Main.master" AutoEventWireup="true" CodeFile="BrokerType.aspx.cs" Inherits="MasterDataManagement_BrokerType" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    
    <asp:GridView ID="grvBrokerType" SkinID="Professional" runat="server" 
        ShowFooter="True" DataSourceID="DataSourceBrokerType" DataKeyNames="ID" 
        OnRowCommand="grvBrokerType_RowCommand" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True">
        <Columns>
            
            <asp:TemplateField HeaderText="Code" SortExpression="ID">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddId" runat="server" Text='<%# Bind("Id") %>' Width="90px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddName" runat="server" Text='<%# Bind("Name") %>' Width="120px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Level" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtLevel" runat="server" Text='<%# Bind("Level_") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Level" runat="server" Text='<%# Eval("Level_") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddLevel" runat="server" Text='<%# Bind("Level_") %>' Width="120px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField> 
                               
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' Width="150px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddDescription" runat="server" Text='<%# Bind("Description") %>' Width="150px"></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
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
            Code:<asp:TextBox runat="server" ID="txtNoDataId" Text="0" Width="90px" />
            Name:<asp:TextBox runat="server" ID="txtNoDataName" Width="150px" />
            Level:<asp:TextBox runat="server" ID="txtNoLevel" Width="150px" />
            Description:<asp:TextBox runat="server" ID="txtNoDataDescription" Width="150px" />
            <asp:Button runat="server" ID="btnNoDataInsertCommissionRate" CommandName="NoDataInsert" Text="Insert"/>
        </EmptyDataTemplate>
    </asp:GridView>
        
    <asp:SqlDataSource ID="DataSourceBrokerType" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %> " 
        DeleteCommand="DELETE FROM [BrokerType] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [BrokerType] ([ID], [Name], [Level_],[Description]) VALUES (@ID, @Name, @Level,@Description)" OnInserting = "DataSourceBrokerType_Inserting"
        SelectCommand="SELECT [ID], [Name], [Level_],[Description] FROM [BrokerType]" 
        UpdateCommand="UPDATE [BrokerType] SET [Name] = @Name, [Level_] = @Level_,[Description] = @Description WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Level_" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Level_" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
      
    
</asp:Content>
