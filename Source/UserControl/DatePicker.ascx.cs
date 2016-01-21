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
using System.Globalization;
using System.ComponentModel;
using System.Text;

[ValidationPropertyAttribute("CalendarDateString")]
public partial class DatePicker : System.Web.UI.UserControl
{
    public enum Weekday
    {
        Sunday = 0,
        Monday = 1
    }

    // date format used by the calendar control
    private string dateFormat = "dd/MM/yyyy";

    // whether the control is enabled
    private bool enabled = true;

    // day to start week with
    private int startAt = (int)Weekday.Monday;

    /// <summary>
    /// Register JavaScripts.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("datepickerctrl"))
        {
            Page.ClientScript.RegisterClientScriptInclude("datepickerctrl", "../JavaScript/popcalendar.js");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "dtpck_start", "initPicker('" + ResolveUrl("cal") + "/');", true);

            // localized month names
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                Page.ClientScript.RegisterArrayDeclaration("monthName", "\"" + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i] + "\"");
            }

            // localized day abbreviations
            sb = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                Page.ClientScript.RegisterArrayDeclaration("dayName", "\"" + CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[i] + "\"");
            }
        }
    }

    /// <summary>
    /// Gets or sets the CSS class to apply to the textbox.
    /// </summary>
    [Category("Appearance")]
    [Description("CSS class name applied to the text box.")]
    [Browsable(true)]
    public string TextCssClass
    {
        get { return txt_Date.CssClass; }
        set { txt_Date.CssClass = value; }
    }

    /// <summary>
    /// Gets or sets the day to start the week with.
    /// </summary>
    [Category("Appearance")]
    [Description("Day to start week with.")]
    [Browsable(true)]
    [DefaultValue(Weekday.Monday)]
    public Weekday StartWeekWithDay
    {
        get { return (Weekday)startAt; }
        set { startAt = (int)value; }
    }

    /// <summary>
    /// Gets or sets the content of the textbox which represents a date.
    /// </summary>
    [Category("Appearance")]
    [Bindable(true, BindingDirection.TwoWay)]
    [Browsable(true)]
    public string CalendarDateString
    {
        get { return txt_Date.Text; }
        set
        {
            txt_Date.Text = value;
            DateTime date;
            if (DateTime.TryParseExact(value, dateFormat, null, System.Globalization.DateTimeStyles.None, out date))
            {
                if (date.Date == DateTime.MaxValue.Date)
                {
                    txt_Date.Text = "";
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a DateTime representation of the currently selected date.
    /// </summary>
    [Category("Appearance")]
    [Bindable(true, BindingDirection.TwoWay)]
    [Browsable(true)]
    public DateTime CalendarDate
    {
        get
        {
            DateTime date;
            if (DateTime.TryParseExact(txt_Date.Text, dateFormat, null, System.Globalization.DateTimeStyles.None, out date))
            {
                return date;
            }
            return DateTime.MaxValue;
        }
        set 
        { 

            txt_Date.Text = value.ToString(dateFormat); 
        }
    }

    /// <summary>
    /// Gets a flag indicating whether a valid date has been selected.
    /// </summary>
    public bool IsValidDate
    {
        get
        {
            DateTime date;
            return DateTime.TryParseExact(txt_Date.Text, dateFormat, null, System.Globalization.DateTimeStyles.None, out date);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the date picker control is enabled.
    /// </summary>
    [Category("Behavior")]
    [Bindable(true, BindingDirection.TwoWay)]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool Enabled
    {
        get { return enabled; }
        set { enabled = value; }
    }

    /// <summary>
    /// Gets or sets the date format to use, e.g. "dd.MM.yyyy" or "MM/dd/yyyy".
    /// </summary>
    [Category("Appearance")]
    [Description("Date format, e.g. 'dd.MM.yyyy' or 'MM/dd/yyyy'.")]
    [Browsable(true)]
    public string DateFormat
    {
        get { return dateFormat.Replace("\\/", "/"); }
        set { dateFormat = value.Replace("/", "\\/"); }
    }

    /// <summary>
    /// Set some properties of the child controls before rendering.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        txt_Date.Enabled = enabled;
        imgCalendar.ImageUrl = enabled ? "cal/calendar.gif" : "cal/calendar_disabled.gif";

        if (enabled)
        {
            string scriptStr = "javascript:return popUpCalendar(this, document.getElementById('" + txt_Date.ClientID + @"'), '" + DateFormat + "', " + startAt.ToString() + ")";
            imgCalendar.Attributes.Add("onclick", scriptStr);
        }

        base.OnPreRender(e);
    }

    /// <summary>
    /// Save custom control properties in viewstate.
    /// </summary>
    /// <returns></returns>
    protected override object SaveViewState()
    {
        object[] allStates = new object[2];
        allStates[0] = base.SaveViewState();
        allStates[1] = enabled;
        return allStates;
    }

    /// <summary>
    /// Load custom control properties from viewstate.
    /// </summary>
    /// <param name="savedState"></param>
    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            // Load State from the array of objects that was saved at SavedViewState
            object[] myState = (object[])savedState;
            if (myState[0] != null)
            {
                base.LoadViewState(myState[0]);
            }
            if (myState[1] != null)
            {
                enabled = (bool)myState[1];
            }
        }
    }
}
