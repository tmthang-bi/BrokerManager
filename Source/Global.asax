<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="log4net" %>
<%@ Import Namespace="log4net.Config" %>
<%@ Import Namespace="BrokerManager.DataObjects" %>

<script runat="server">
    private static System.Threading.Timer timer;
    private static bool ScheduledJobRunning = false;
    
    private readonly ILog logger = LogManager.GetLogger(Constants.CONF_ID_LOGGER);

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        XmlConfigurator.Configure();
        logger.Debug(Constants.TXT_LOG_APP_START); 
                       
        LoadData();        
        ScheduleJob();
    }

    private void LoadData()
    {
        LoadUserData();
        //LoadDBModalData();
    }

    private void LoadUserData()
    {
        try
        {                                               
            BrokerManager.DataObjects.UserDTO.GetGlobalUserData();
            logger.Info("User list load completed. Count=" + BrokerManager.DataObjects.UserDTO.HashAllUser.Count);
        }
        catch (Exception ex)
        {
            logger.Error("User loading error: " + ex.ToString());
        }
    }

    private void ScheduleJob()
    {
        int interval = GetInterValSec();
        logger.Debug("Number value for cache refresh interval (sec): " + interval.ToString());
        if (interval > 0)
        {
            interval = interval * 1000; // second
            if (timer == null)
            {
                timer = new System.Threading.Timer(new System.Threading.TimerCallback(ScheduledWorkCallback), HttpContext.Current, interval, interval);
                logger.Info("Schedule work is ENABLED, and started");
                ScheduledWorkCallback(HttpContext.Current);
            }
        }
        else
        {
            logger.Info("INVALID Interval, Schedule work is DISABLED");
        }
    }

    private int GetInterValSec()
    {
        string sInterval = ConfigurationManager.AppSettings[Constants.CONF_ID_CACHE_INTERVAL_SEC];
        logger.Debug("Configured cache refresh interval (sec): " + sInterval);
        int IntervalSec = 0;
        if (int.TryParse(sInterval, out IntervalSec))
        {
            logger.Info("Configured cache refresh interval parsing valid");
        }
        else
        {
            IntervalSec = -1;
            logger.Warn("Configured cache refresh interval parsing INVALID");
        }
        return IntervalSec;
    }

    public void ScheduledWorkCallback(object sender)
    {
        if (!ScheduledJobRunning)
        {
            ScheduledJobRunning = true;
            try
            {
                ScheduledJob(sender);
            }
            catch (Exception ex)
            {
                logger.Fatal("Error in scheduled job: " + ex.ToString());
            }
            finally
            {
                ScheduledJobRunning = false;
            }
        }
    }

    public void ScheduledJob(object sender)
    {
        int start = Environment.TickCount;
        LoadData();
        int stop = Environment.TickCount;
        int duration = stop - start;
        logger.Info("ScheduledJob conpleted in " + duration + " milisecond(s)");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        if (timer != null) timer.Dispose();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        logger.Warn("Global.asax - Application_Error. Sender=" + sender.ToString() + " EventArgs=" + e.ToString());
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
