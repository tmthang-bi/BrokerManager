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
using log4net;
using log4net.Config;
using BrokerManager.DataObjects;
using HSCBrokerManagement.Utils;

public partial class Calculation_CommissionCalculation : BasePage
{
    private readonly ILog _logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);
    private const string CALCULATION_SUCCESS = "CommissionCalculation_Success";
    private const string CALCULATION_FAIL = "CommissionCalculation_Fail";

    private const int VERIFY_TRADING_DATA = 1;
    private const int CALCULATE = 2;
    private const int FREEZE = 3;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitGUI();
            LoadCalculationPeriod();
        }
    }

    private CalculationDTO CalculationPeriod
    {
        get { return Session["CommissionCalculation_CalculationPeriod"] as CalculationDTO; }
        set { Session["CommissionCalculation_CalculationPeriod"] = value; }
    }

    private void InitGUI()
    {
        //load
        cmbMonth.SelectedValue = DateTime.Now.ToString("MM"); 
        cmbYear.SelectedValue = DateTime.Now.ToString("yyyy"); 
    }
  
    private void LoadCalculationPeriod()
    {
        string id = cmbYear.SelectedValue + cmbMonth.SelectedValue;

        CalculationDTO calculPeriod = BusinessService.GetCalculationPeriod(id);
        if (calculPeriod != null)
        {
            CalculationPeriod = calculPeriod;
            FillData();
        }
        else
        {
            grdCalculationPeriod.Visible = false;
            AddInforMessage("CommissionCalculation_CalculationPeriod_Not_Exist", null);
        }
    }
    protected void BtnNew_OnClick(object sender, EventArgs e)
    {
        int iYear = Int32.Parse(cmbYear.SelectedValue);
        int iMonth = Int32.Parse(cmbMonth.SelectedValue);
        DateTime calculationTargetMonth = new DateTime(iYear, iMonth, 20);

        CalculationDTO newPeriod = new CalculationDTO();
        newPeriod.ID = calculationTargetMonth.ToString("yyyyMM");
        newPeriod.Freeze = false;
        newPeriod.VerifyTradingData = false;
        newPeriod.CalculatedBy = CurrentUser.Id;
        newPeriod.CalculPeriod = calculationTargetMonth;

        try
        {
            BusinessService.InsertCalculationPeriod(newPeriod);
        }
        catch (Exception ex)
        {
            ////if (ex.GetType().IsInstanceOfType(new UniqueConstraintException()))
            ////{
            ////    AddErrorMessage("CommissionCalculation_DuplicateUniqueConstraintException", null);
            //}
        }

        LoadCalculationPeriod();
    }

    public void FillData()
    {
        if (CalculationPeriod == null)
        {
            return;
        }

        DataTable tb = new DataTable();

        tb.Columns.Add(new DataColumn("Stept"));
        tb.Columns.Add(new DataColumn("Description"));
        tb.Columns.Add(new DataColumn("Status"));

        //Verify the client trade data
        DataRow row = tb.NewRow();
        row[0] = VERIFY_TRADING_DATA;
        row[1] = "Verify the client trade data";

        if (CalculationPeriod.VerifyTradingData)
        {
            row[2] = "SUCCESS";
        }
        else
        {
            row[2] = "NOT YET";
        }
        tb.Rows.Add(row);

        //Verify the client trade data
        DataRow row2 = tb.NewRow();
        row2[0] = CALCULATE;
        row2[1] = "Calculate the commission";
        row2[2] = "DONE";
        tb.Rows.Add(row2);

        DataRow row3 = tb.NewRow();
        row3[0] = FREEZE;
        row3[1] = "Freeze the calculation result";

        if (CalculationPeriod.Freeze)
        {
            row3[2] = "Freeze";
        }
        else
        {
            row3[2] = "NOT YET";
        }
        tb.Rows.Add(row3);

        grdCalculationPeriod.DataSource = tb;
        grdCalculationPeriod.DataBind();
        grdCalculationPeriod.Visible = true;

    }

    protected void grdCalculationPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Execute"))
        {
            int stept = int.Parse(e.CommandArgument.ToString());

            //VERIFY THE INPUT TRADING DATA 
            if (stept == VERIFY_TRADING_DATA)
            {
                //if (!BusinessService.IsAllTradeDataConsolidated(CalculationPeriod.Month, CalculationPeriod.Year))
                //{
                //    AddErrorMessage("CommissionCalculation_Verifytradedata_failed", null);
                //}
                //else
                //{
                //    //update the calculation period
                //    CalculationPeriod.TradeDataVerify = 1;
                //    BusinessService.UpdateCalculationPeriod(CalculationPeriod);
                //    LoadCalculationPeriod();
                //}
            }
            else if (stept == CALCULATE) 
            {
                Calculation();
            }
            else if (stept == FREEZE)
            {
                
            }
        }
    }
    protected void BtnView_OnClick(object sender, EventArgs e)
    {
        LoadCalculationPeriod();
    }
   
    private void Calculation() 
    {
        try
        {
            BusinessService.CalculateCommission(CalculationPeriod.ID,CalculationPeriod.CalculPeriod, CurrentUser);

            AddInforMessage(CALCULATION_SUCCESS,null);
        }
        catch (Exception ex)
        {
            _logger.Fatal("Error: " + ex.ToString());
            AddErrorMessage(CALCULATION_FAIL,null);
        }
    }
}
