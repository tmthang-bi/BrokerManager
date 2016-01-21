<%@ Page Language="C#" StylesheetTheme="BrokerManagementTheme" AutoEventWireup="true" masterpagefile="~/MasterPage/Main.master" CodeFile="BrokerListModification.aspx.cs" Inherits="BrokerManagement_BrokerListModification" %>

<%@ Register Src="~/UserControl/ucBrokerFilter.ascx" TagName="ucBrokerFilter" TagPrefix="uc" %>

<asp:Content ID="cntMainFunction" ContentPlaceHolderID="cphMainFunction" Runat="Server">
    <table>
        <tr>
            <td>
                <uc:ucBrokerFilter ID="ucBrokerFilter" runat="server" />
            </td>
            <td style="vertical-align:bottom">
                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" Height="25px"/>
            </td>
            <td style="vertical-align:bottom">
                <asp:Button ID="btnExport" runat="server" Text="Excel Export" OnClick="btnExport_Click" Height="25px"/>
            </td>
        </tr>
    </table>
    <asp:GridView ID="grvBroker" SkinID="Professional" runat="server" ShowFooter="True" DataKeyNames="ID" DataSourceID="DataSourceBrokerList" OnRowCommand="grvBroker_RowCommand" PageSize="20" OnRowEditing="grvBroker_RowEditing">
        <Columns>
            <asp:TemplateField HeaderText="M&#227; NV" SortExpression="ID" >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddID" runat="server" Text='<%# Bind("ID") %>' Width="50px"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Vai tr&#242;" SortExpression="BrokerTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblBrokerTypeID" runat="server" Text='<%# Eval("BrokerTypeID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlEditBrokerTypeID" runat="server" DataSourceID="DataSourceBrokerType" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BrokerTypeID") %>'></asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAddBrokerTypeID" runat="server" DataSourceID="DataSourceBrokerType" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("BrokerTypeID") %>'></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Họ T&#234;n" SortExpression="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txbEditName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Văn ph&#242;ng" SortExpression="OfficeID">
                <ItemTemplate>
                    <asp:Label ID="lblOffice" runat="server" Text='<%# Eval("OfficeID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlEditOffice" runat="server" DataSourceID="DataSourceOffice" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("OfficeID") %>'></asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAddOffice" runat="server" DataSourceID="DataSourceOffice" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("OfficeID") %>'></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Quản l&#237;" SortExpression="SupervisorName">
                <ItemTemplate>
                    <asp:Label ID="lblSupervisor" runat="server" Text='<%# Eval("SupervisorName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlEditSupervisor" runat="server" 
                        AppendDataBoundItems="true"
                        OnDataBinding="ddlEditSupervisor_DataBinding"
                        DataSourceID="DataSourceBrokerManagerList" DataTextField="Name" DataValueField="ID" 
                        SelectedValue='<%# Bind("SupervisorBrokerID") %>'></asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAddSupervisor" runat="server" DataSourceID="DataSourceBrokerManagerList" DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("SupervisorBrokerID") %>'></asp:DropDownList>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="M&#227; QL" SortExpression="SupervisorBrokerID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblSupervisorBrokerID" runat="server" Text='<%# Eval("SupervisorBrokerID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txbEditEmail" runat="server" Text='<%# Bind("Email") %>' ></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddEmail" runat="server" Text='<%# Bind("Email") %>' ></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Active" SortExpression="IsActive">
                <ItemTemplate>
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkEditIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:CheckBox ID="chkAddIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Entrance Date" SortExpression="EntranceDate">
                <ItemTemplate>
                    <asp:Label ID="lblEntranceDate" runat="server" Text='<%# Eval("EntranceDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txbEditEntranceDate" runat="server" Text='<%# Bind("EntranceDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txbAddEntranceDate" runat="server" Text='<%# Bind("EntranceDate") %>'></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Leave Date" SortExpression="LeaveDate">
                <ItemTemplate>
                    <asp:Label ID="lblLeaveDate" runat="server" Text='<%# Eval("LeaveDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txbEditLeaveDate" runat="server" Text='<%# Bind("LeaveDate") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/icon-pencil.gif" CommandName="Edit" ToolTip="Edit" />
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/icon-delete.gif" CommandName="Delete" ToolTip="Delete"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/Images/icon-floppy.gif" CommandName="Update" ToolTip="Update" />
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/icon-cancel.PNG" CommandName="Cancel" ToolTip="Cancel"/>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="True" CommandName="InsertNew" Text="Add"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>           
        </Columns>
        <EmptyDataTemplate>
            Create first manager:<br />
            ID:<asp:TextBox runat="server" ID="txbNoDataID" />
            Type:<asp:TextBox runat="server" ID="txbNoDataBrokerTypeID" Text="MAN" Enabled="false" ReadOnly="true" />
            Name:<asp:TextBox runat="server" ID="txbNoDataName" />
            Office:<asp:DropDownList ID="ddlNoDataOffice" runat="server" DataSourceID="DataSourceOffice" DataTextField="Name" DataValueField="ID" ></asp:DropDownList>
            Email:<asp:TextBox runat="server" ID="txbNoDataEmail" />
            Active:<asp:CheckBox ID="chkNoDataIsActive" runat="server" Checked='true' />
            Entrance Date:<asp:TextBox runat="server" ID="txbNoDataEntranceDate" />
            Leave Date:<asp:TextBox runat="server" ID="txbNoDataLeaveDate" />
            <asp:Button runat="server" ID="btnBrokerNoDataInsert" CommandName="NoDataInsert" Text="Insert"/>
        </EmptyDataTemplate>
        
    </asp:GridView>
    
    <asp:SqlDataSource ID="DataSourceBrokerType" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [BrokerType]"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="DataSourceOffice" runat="server" ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        SelectCommand="SELECT [ID], [Name] FROM [Office]"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="DataSourceBrokerList" runat="server" ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>"
        DeleteCommand="DELETE FROM Broker WHERE (ID = @original_ID)"
        InsertCommand="INSERT INTO [Broker] ([ID], [BrokerTypeID], [OfficeID], [SupervisorBrokerID], [Name], [EntranceDate], [LeaveDate], [IsActive], [Email], [UpdateBy], [UpdateTime]) VALUES (@ID, @BrokerTypeID, @OfficeID, @SupervisorBrokerID, @Name, @SystemUserID, @Code, @EntranceDate, @LeaveDate, @IsActive, @Email, @UpdateBy, getdate())"
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT a.[ID], a.[BrokerTypeID], a.[OfficeID], a.[SupervisorBrokerID], b.[Name] as SupervisorName, a.[Name], a.[Email], a.[EntranceDate], a.[LeaveDate], a.[IsActive], a.[UpdateBy], a.[UpdateTime] FROM [Broker] a left outer join [Broker] b on a.SupervisorBrokerID = b.ID"
        UpdateCommand="UPDATE Broker SET BrokerTypeID = @BrokerTypeID, OfficeID = @OfficeID, SupervisorBrokerID = @SupervisorBrokerID, Name = @Name, Email = @Email, EntranceDate = @EntranceDate, LeaveDate = @LeaveDate, IsActive = @IsActive, UpdateBy = @UpdateBy, UpdateTime = getdate() WHERE (ID = @original_ID)" OnInserting="DataSourceBrokerList_Inserting">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="BrokerTypeID" Type="String" />
            <asp:Parameter Name="OfficeID" Type="String" />
            <asp:Parameter Name="SupervisorBrokerID" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="EntranceDate" Type="DateTime"/>
            <asp:Parameter Name="LeaveDate" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:SessionParameter Name="UpdateBy" SessionField="Session key Username" Type="String" />
            <asp:Parameter Name="original_ID" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="String" />
            <asp:Parameter Name="BrokerTypeID" Type="String" />
            <asp:Parameter Name="OfficeID" Type="String" />
            <asp:Parameter Name="SupervisorBrokerID" Type="String" DefaultValue="" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="EntranceDate" />
            <asp:Parameter Name="LeaveDate" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="UpdateBy" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="DataSourceBrokerManagerList" runat="server"
        ConnectionString="<%$ ConnectionStrings:MSSQLServer.BrokerManagement %>" SelectCommand="SELECT [ID], [BrokerTypeID], [Name] FROM [Broker] WHERE (BrokerTypeID = 'SUP') OR (BrokerTypeID = 'MAN') OR (BrokerTypeID = 'MD') UNION SELECT NULL, NULL, 'N/A' ORDER BY [BrokerTypeID], [Name], [ID]">
    </asp:SqlDataSource>
    
</asp:Content>
