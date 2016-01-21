using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterDataManagement_CommissionRate : System.Web.UI.Page
{
    // here we keep the SqlParameters that we will use to insert new row
    private List<SqlParameter> _insertParameters = new List<SqlParameter>();
    protected List<SqlParameter> InsertParameters
    {
        get { return _insertParameters; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grvCommissionRate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((e.CommandName == "InsertNew") || (e.CommandName == "NoDataInsert"))
        {
            string brokerTypeID = string.Empty;
            string clientTypeID = string.Empty;
            string sLowerLimit = string.Empty;
            string sUpperLimit = string.Empty;            
            string sCommissionRate = string.Empty;
         
            long lowerLimit = 0;
            long upperLimit = 0;
            double commissionRate = 0.0;
           
            bool isInputOK = true;

            string updateBy = (Session[Constants.SESSION_KEY_USER] as BrokerManager.DataObjects.UserDTO).Id;

            if (e.CommandName == "InsertNew")
            {
                brokerTypeID = (grvCommissionRate.FooterRow.FindControl("ddlAddBrokerTypeID") as DropDownList).SelectedValue;
                clientTypeID = (grvCommissionRate.FooterRow.FindControl("ddlAddClientType") as DropDownList).SelectedValue;

                sLowerLimit = (grvCommissionRate.FooterRow.FindControl("txbAddLowerLimit") as TextBox).Text;
                sUpperLimit = (grvCommissionRate.FooterRow.FindControl("txbAddUpperLimit") as TextBox).Text;
                isInputOK = long.TryParse(sLowerLimit, out lowerLimit);
                isInputOK = long.TryParse(sUpperLimit, out upperLimit);

                sCommissionRate = (grvCommissionRate.FooterRow.FindControl("txbAddCommissionRate") as TextBox).Text;
                isInputOK = double.TryParse(sCommissionRate, out commissionRate);

            }
            else
            {
                brokerTypeID = (grvCommissionRate.Controls[0].Controls[0].FindControl("ddlNoDataBrokerTypeID") as DropDownList).SelectedValue;
                clientTypeID = (grvCommissionRate.Controls[0].Controls[0].FindControl("ddlNoDataClientType") as DropDownList).SelectedValue;

                sLowerLimit = (grvCommissionRate.Controls[0].Controls[0].FindControl("txbNoDataLowerLimit") as TextBox).Text;
                sUpperLimit = (grvCommissionRate.Controls[0].Controls[0].FindControl("txbNoDataUpperLimit") as TextBox).Text;
                isInputOK = long.TryParse(sLowerLimit, out lowerLimit);
                isInputOK = long.TryParse(sUpperLimit, out upperLimit);

                sCommissionRate = (grvCommissionRate.Controls[0].Controls[0].FindControl("txbNoDataCommissionRate") as TextBox).Text;
                isInputOK = double.TryParse(sCommissionRate, out commissionRate);
            }

            InsertParameters.Clear();

            SqlParameter paramClientTypeID = new SqlParameter("@ClientTypeID", SqlDbType.VarChar);
            paramClientTypeID.Direction = ParameterDirection.Input;
            paramClientTypeID.Value = clientTypeID;
            InsertParameters.Add(paramClientTypeID);

            SqlParameter paramBrokerTypeID = new SqlParameter("@BrokerTypeID", SqlDbType.VarChar);
            paramBrokerTypeID.Direction = ParameterDirection.Input;
            paramBrokerTypeID.Value = brokerTypeID;
            InsertParameters.Add(paramBrokerTypeID);

            SqlParameter paramLowLim = new SqlParameter("@LowerLimit", SqlDbType.BigInt);
            paramLowLim.Direction = ParameterDirection.Input;
            paramLowLim.Value = lowerLimit;
            InsertParameters.Add(paramLowLim);

            SqlParameter paramUpLim = new SqlParameter("@UpperLimit", SqlDbType.BigInt);
            paramUpLim.Direction = ParameterDirection.Input;
            paramUpLim.Value = upperLimit;
            InsertParameters.Add(paramUpLim);

            SqlParameter paramCommissionRate = new SqlParameter("@CommissionRate", SqlDbType.Float);
            paramCommissionRate.Direction = ParameterDirection.Input;
            paramCommissionRate.Value = commissionRate;
            InsertParameters.Add(paramCommissionRate);

            SqlParameter paramUpdateBy = new SqlParameter("@UpdateBy", SqlDbType.VarChar);
            paramUpdateBy.Direction = ParameterDirection.Input;
            paramUpdateBy.Value = updateBy;
            InsertParameters.Add(paramUpdateBy);

            DataSourceCommissionRate.Insert();
        }
    }
    protected void DataSourceCommissionRate_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters.Clear();
        foreach (SqlParameter p in InsertParameters) e.Command.Parameters.Add(p);
    }
}
