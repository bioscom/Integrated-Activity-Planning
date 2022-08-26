using System;
using System.Net.Mail;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
    /// Class contains miscellaneous functionality
    /// </summary>
public static class Utilities
{
    static Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // Generic method for sending emails
    private static void SendMail(string fromEmailAddress, string toEmailAddress, string mSubject, string mBody, string mCC)
    {
        try
        {
            SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmailAddress);
            msg.To.Add(toEmailAddress);

            if (mCC.Length != 0)
            {
                msg.CC.Add(mCC);
            }

            msg.Subject = mSubject;
            msg.Body = mBody;
            msg.IsBodyHtml = true;
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            Utilities.LogError(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public static void LogError(Exception ex)
    {
        // get the current date and time
        string dateTime = DateTime.Now.ToLongDateString() + ", at " + DateTime.Now.ToShortTimeString();
        // stores the error message
        string errorMessage = "Exception generated on " + dateTime;
        // obtain the page that generated the error
        HttpContext context = HttpContext.Current;
        errorMessage += "\n\n Page location: " + context.Request.RawUrl;
        // build the error message
        errorMessage += "\n\n Message: " + ex.Message;
        errorMessage += "\n\n Source: " + ex.Source;
        errorMessage += "\n\n Method: " + ex.TargetSite;
        errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace;
        // send error email in case the option is activated in web.config
        Users CurrentUser =  UsersActions.getUserByDomainLoginID(Apps.LoginIDNoDomain(context.User.Identity.Name));
        if (AppConfiguration.EnableErrorLogEmail)
        {
            string from = CurrentUser.m_sUserMail;
            string to = AppConfiguration.ErrorLogEmail;
            string subject = "Integrated Activity Planning - Error Report";
            string body = errorMessage;
            SendMail(from, to, subject, body, "");
        }
    }

    //public enum status : int { Active = 1, Inactive };

    public static string AppURL()
    {
        string ServerURL = "";

        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();

        if (httpHost == AppConfiguration.SiteHostName) //Life server
        {
            ServerURL = "http://" + httpHost + "/IAP/Default.aspx";
        }
        else if (httpHost == AppConfiguration.SiteDevelopmentEnvironment)         //Test Server
        {
            ServerURL = "http://" + httpHost + "/IAP/Default.aspx";
        }
        else
        {
            ServerURL = "http://" + httpHost + "/IAP/Default.aspx"; //Development PC
        }

        return ServerURL;
    }    

    //public static DataTable DoesUserExist(string UserName)
    //{
    //    string sql = "SELECT USERNAME, FULL_NAME, EMAIL FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD WHERE upper(USERNAME) = '" + UserName.ToUpper() + "'";
    //    return DataAccess.ExecuteQueryCommand(sql);
    //}

    public static string LiveLinkURLs()
    {
        string LiveLink = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12093961&objAction=browse&sort=name";
        return LiveLink;
    }

    public static string eIPBehaviouralGuideLines()
    {
        string behaviouralGuid2 = "http://sww-knowledge-epg.shell.com/knowtepg1/livelink.exe?func=ll&objId=12096514&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Flivelink%2Eexe%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname";
        //string behaviouralGuide = "http://sww-knowledge-epg.shell.com/knowtepg1/llisapi.dll?func=ll&objId=12102069&objAction=Open&viewType=1&nexturl=%2Fknowtepg1%2Fllisapi%2Edll%3Ffunc%3Dll%26objId%3D12093961%26objAction%3Dbrowse%26sort%3Dname%26viewType%3D1";
        return behaviouralGuid2;
    }
}