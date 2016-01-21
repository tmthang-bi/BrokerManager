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
using HSCBrokerManagement.Utils;

public partial class MasterDataManagement_BrokerType : BasePage
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
    protected void grvBrokerType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((e.CommandName == "InsertNew") || (e.CommandName == "NoDataInsert"))
        {
            string code = string.Empty;
            string name = string.Empty;
            string level = string.Empty;
            string description = string.Empty;

            string updateBy = CurrentUser.Id;

            if (e.CommandName == "InsertNew")
            {
                code = (grvBrokerType.FooterRow.FindControl("txtAddId") as TextBox).Text;
                name = (grvBrokerType.FooterRow.FindControl("txtAddName") as TextBox).Text;
                level = (grvBrokerType.FooterRow.FindControl("txtAddLevel") as TextBox).Text;
                description = (grvBrokerType.FooterRow.FindControl("txtAddDescription") as TextBox).Text;
            }
            else
            {
                code = (grvBrokerType.FooterRow.FindControl("txtNoDataId") as TextBox).Text;
                name = (grvBrokerType.FooterRow.FindControl("txtNoDataName") as TextBox).Text;
                level = (grvBrokerType.FooterRow.FindControl("txtNoDataLevel") as TextBox).Text;
                description = (grvBrokerType.FooterRow.FindControl("txtNoDataDescription") as TextBox).Text;
            }

            InsertParameters.Clear();

            SqlParameter paramId = new SqlParameter("@ID", SqlDbType.VarChar);
            paramId.Direction = ParameterDirection.Input;
            paramId.Value = code;
            InsertParameters.Add(paramId);

            SqlParameter paramName = new SqlParameter("@Name", SqlDbType.VarChar);
            paramName.Direction = ParameterDirection.Input;
            paramName.Value = name;
            InsertParameters.Add(paramName);

            SqlParameter paramLevel = new SqlParameter("@Level_", SqlDbType.VarChar);
            paramLevel.Direction = ParameterDirection.Input;
            paramLevel.Value = level;
            InsertParameters.Add(paramLevel);

            SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.VarChar);
            paramDescription.Direction = ParameterDirection.Input;
            paramDescription.Value = description;
            InsertParameters.Add(paramDescription);

            DataSourceBrokerType.Insert();
        }
    }
    protected void DataSourceBrokerType_Inserting(object sender, SqlDataSourceCommandEventArgs e)
    {
        e.Command.Parameters.Clear();
        foreach (SqlParameter p in InsertParameters) e.Command.Parameters.Add(p);
    }
}
