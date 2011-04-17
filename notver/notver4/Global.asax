<%@ Application Language="C#" %>


<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        Exception myError = null;
        if (HttpContext.Current.Server.GetLastError() != null)
        {
            string eventLog = "My Websites";
            string eventSource = "www.MySite.Com";
            string myErrorMessage = "";

            myError = Server.GetLastError();

            while (myError.InnerException != null)
            {
                myErrorMessage += "Message\r\n" +
                    myError.Message.ToString() + "\r\n\r\n";
                myErrorMessage += "Source\r\n" +
                    myError.Source + "\r\n\r\n";
                myErrorMessage += "Target site\r\n" +
                    myError.TargetSite.ToString() + "\r\n\r\n";
                myErrorMessage += "Stack trace\r\n" +
                    myError.StackTrace + "\r\n\r\n";
                myErrorMessage += "ToString()\r\n\r\n" +
                    myError.ToString();

                // Assign the next InnerException
                // to catch the details of that exception as well
                myError = myError.InnerException;
            }

            // Make sure the Eventlog Exists
            if (System.Diagnostics.EventLog.SourceExists(eventSource))
            {
                // Create an EventLog instance and assign its source.
                System.Diagnostics.EventLog myLog = new System.Diagnostics.EventLog(eventLog);
                myLog.Source = eventSource;

                // Write the error entry to the event log.    
                myLog.WriteEntry("Uygulamada hata olustu "
                  + eventSource + "\r\n\r\n" + myErrorMessage,
                    System.Diagnostics.EventLogEntryType.Error);
            }
        }

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
        //Response.Redirect(Util.ResolveUrl("~/Default.aspx?timeout=true"));
    }
       
</script>
