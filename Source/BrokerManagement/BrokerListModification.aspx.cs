using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class BrokerManagement_BrokerListModification : System.Web.UI.Page
{
    private ListItem _editingRowSupervisorItem = null;

    // here we keep the SqlParameters that we will use to insert new row
    private List<SqlParameter> _insertParameters = new List<SqlParameter>();
    protected List<SqlParameter> InsertParameters
    {
        get { return _insertParameters; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string CorrectAndGetText(TextBox txb)
    {
        if (txb == null)
        {
            return string.Empty;
        }

        txb.Text = txb.Text.ToUpper();
        return txb.Text;
    }

    private void DoFilter()
    {
        string filter = ucBrokerFilter.GetFilterString();
        DataSourceBrokerList.FilterExpression = filter;
        try
        {
            grvBroker.DataBind();
        }
        catch (Exception) { }
    }

    protected void grvBroker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((e.CommandName == "InsertNew") || (e.CommandName == "NoDataInsert"))
        {
            string id = string.Empty;
            string brokerTypeID = string.Empty;
            string name = string.Empty;
            string office = string.Empty;
            string supervisor = string.Empty;
            bool isActive = true;
            string email = string.Empty;
            string updateBy = (Session[Constants.SESSION_KEY_USER] as BrokerManager.DataObjects.UserDTO).Id;
            DateTime entranceDate;

            if (e.CommandName == "InsertNew")
            {
                id = CorrectAndGetText(grvBroker.FooterRow.FindControl("txbAddID") as TextBox);
                brokerTypeID = (grvBroker.FooterRow.FindControl("ddlAddBrokerTypeID") as DropDownList).SelectedValue;
                name = (grvBroker.FooterRow.FindControl("txbAddName") as TextBox).Text;
                office = (grvBroker.FooterRow.FindControl("ddlAddOffice") as DropDownList).SelectedValue;
                supervisor = (grvBroker.FooterRow.FindControl("ddlAddSupervisor") as DropDownList).SelectedValue;
                isActive = (grvBroker.FooterRow.FindControl("chkAddIsActive") as CheckBox).Checked;
                email = (grvBroker.FooterRow.FindControl("txbAddEmail") as TextBox).Text;
                entranceDate = Utils.ConvertToDBDateTime((grvBroker.FooterRow.FindControl("txbAddEntranceDate") as TextBox).Text, "dd/MM/yyyy"); 
            }
            else
            {
                id = CorrectAndGetText(grvBroker.Controls[0].Controls[0].FindControl("txbNoDataID") as TextBox);
                brokerTypeID = CorrectAndGetText(grvBroker.Controls[0].Controls[0].FindControl("txbNoDataBrokerTypeID") as TextBox);
                name = (grvBroker.Controls[0].Controls[0].FindControl("txbNoDataName") as TextBox).Text;
                office = (grvBroker.Controls[0].Controls[0].FindControl("ddlNoDataOffice") as DropDownList).SelectedValue;
                supervisor = string.Empty;
                isActive = (grvBroker.Controls[0].Controls[0].FindControl("chkNoDataIsActive") as CheckBox).Checked;
                email = (grvBroker.Controls[0].Controls[0].FindControl("txbNoDataEmail") as TextBox).Text;
                entranceDate = Utils.ConvertToDBDateTime((grvBroker.Controls[0].FindControl("txbNoDataEntranceDate") as TextBox).Text, "dd/MM/yyyy"); 
            }

            InsertParameters.Clear();

            SqlParameter paramID = new SqlParameter("@ID", SqlDbType.VarChar);
            paramID.Direction = ParameterDirection.Input;
            paramID.Value = id;
            InsertParameters.Add(paramID);

            SqlParameter paramBrokerTypeID = new SqlParameter("@BrokerTypeID", SqlDbType.VarChar);
            paramBrokerTypeID.Direction = ParameterDirection.Input;
            paramBrokerTypeID.Value = brokerTypeID;
            InsertParameters.Add(paramBrokerTypeID);

            SqlParameter paramOfficeID = new SqlParameter("@OfficeID", SqlDbType.VarChar);
            paramOfficeID.Direction = ParameterDirection.Input;
            paramOfficeID.Value = office;
            InsertParameters.Add(paramOfficeID);

            SqlParameter paramSupervisorID = new SqlParameter("@SupervisorBrokerID", SqlDbType.VarChar);
            paramSupervisorID.Direction = ParameterDirection.Input;
            paramSupervisorID.Value = supervisor;
            InsertParameters.Add(paramSupervisorID);

            SqlParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar);
            paramName.Direction = ParameterDirection.Input;
            paramName.Value = name;
            InsertParameters.Add(paramName);

            SqlParameter paramEntranceDate = new SqlParameter("@EntranceDate", SqlDbType.DateTime);
            paramEntranceDate.Direction = ParameterDirection.Input;
            if (entranceDate.Date == DateTime.MaxValue.Date)
            {
                paramEntranceDate.Value = DBNull.Value;
            }
            else 
            {
                paramEntranceDate.Value = entranceDate.Date;
            }
            
            InsertParameters.Add(paramEntranceDate);

            SqlParameter paramLeaveDate = new SqlParameter("@LeaveDate", SqlDbType.DateTime);
            paramLeaveDate.Direction = ParameterDirection.Input;
            paramLeaveDate.Value = DBNull.Value;
            InsertParameters.Add(paramLeaveDate);

            SqlParameter paramIsActive = new SqlParameter("@IsActive", SqlDbType.Bit);
            paramIsActive.Direction = ParameterDirection.Input;
            paramIsActive.Value = isActive;
            InsertParameters.Add(paramIsActive);

            SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
            paramEmail.Direction = ParameterDirection.Input;
            paramEmail.Value = email;
            InsertParameters.Add(paramEmail);

            SqlParameter paramUpdateBy = new SqlParameter("@UpdateBy", SqlDbType.VarChar);
            paramUpdateBy.Direction = ParameterDirection.Input;
            paramUpdateBy.Value = updateBy;
            InsertParameters.Add(paramUpdateBy);

            DataSourceBrokerList.Insert();
        }

        DoFilter();
    }

    protected void DataSourceBrokerList_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters.Clear();
        foreach (SqlParameter p in InsertParameters) e.Command.Parameters.Add(p);
    }
    
    protected void grvBroker_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label lblSupName = (grvBroker.Rows[e.NewEditIndex].FindControl("lblSupervisor") as Label);
        Label lblSupID = (grvBroker.Rows[e.NewEditIndex].FindControl("lblSupervisorBrokerID") as Label);
        string supID = lblSupID.Text.Trim();
        if (!Utils.IsNullOrEmpty(supID))
        {
            string supName = lblSupName.Text.Trim();
            if (supName.Length <= 0)
            {
                supName = "Unknown (not found) ID=" + supID + ". MUST select OTHERS!";
            }

            _editingRowSupervisorItem = new ListItem(supName, supID);
        }
    }

    protected void ddlEditSupervisor_DataBinding(object sender, EventArgs e)
    {
        if (_editingRowSupervisorItem != null)
        {
            DropDownList ddlSupervisors = (sender as DropDownList);
            ddlSupervisors.Items.Insert(0, _editingRowSupervisorItem);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DoFilter();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        grvBroker.AllowPaging = false;
        //DataSourceBrokerList.FilterExpression = ucBrokerFilter.GetFilterString();
        grvBroker.DataBind();

        ArrayList removedColumnList = new ArrayList();
        removedColumnList.Add(0);
        ArrayList msoNum2TextColList = new ArrayList();
        msoNum2TextColList.Add(1);
        msoNum2TextColList.Add(6);
        GridViewExportUtil.Export2Excel("BrokerList.xls", grvBroker, removedColumnList, msoNum2TextColList, false);

        grvBroker.AllowPaging = true;
        grvBroker.DataBind();
    }
}

