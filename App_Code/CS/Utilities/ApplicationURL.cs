using System.Web;

public class ApplicationURL
{
    private ApplicationURL()
    {

    }

    public static string MyAppURL()
    {
        string ServerURL = "";

        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();
        ServerURL = "http://" + httpHost + "/" + AppConfiguration.SiteShortName;
        //if (httpHost == AppConfiguration.LifeServerHost) 
        //{
        //    ServerURL = "http://" + httpHost + "/" + AppConfiguration.SiteShortName;
        //}
        //else if (httpHost == AppConfiguration.TestServerHost)
        //{
        //    ServerURL = "http://" + httpHost + "/" + AppConfiguration.SiteShortName;
        //}
        //else
        //{
        //    ServerURL = "http://" + httpHost + "/" + AppConfiguration.SiteShortName; //Development PC
        //}

        return ServerURL;
    }

    public static string MyAppURLWithoutPMIS()
    {
        string ServerURL = "";
        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString();
        ServerURL = "http://" + httpHost;

        //if (httpHost == AppConfiguration.LifeServerHost)
        //{
        //    ServerURL = "http://" + httpHost;
        //}
        //else if (httpHost == AppConfiguration.TestServerHost)
        //{
        //    ServerURL = "http://" + httpHost;
        //}
        //else
        //{
        //    ServerURL = "http://" + httpHost;
        //}

        return ServerURL;
    }
}
