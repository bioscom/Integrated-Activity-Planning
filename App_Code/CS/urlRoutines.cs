using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for urlRoutines
/// </summary>
public class urlRoutines
{
    public urlRoutines()
    {
        //
        // 
        //
    }


    public static string getAppResourceUrl(string sTargetPage)
    {
        string sRet = "";
        try
        {
            sTargetPage = sTargetPage.Replace(@"\", "/");
            string sCurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            string sTempUrl = HttpContext.Current.Request.Url.Query;
            if (sTempUrl.Length > 0) sCurrentUrl = sCurrentUrl.Replace(sTempUrl, "");
            sTempUrl = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
            byte tPos = (byte)sTempUrl.IndexOf("/");

            if (tPos > 0) sTempUrl = sTempUrl.Substring(tPos);
            sCurrentUrl = sCurrentUrl.Replace(sTempUrl, "");
            tPos = (byte)sTargetPage.IndexOf("/");
            if (tPos > 0) sRet = sCurrentUrl + sTargetPage.Substring(tPos);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    private static string appendPathSlash(string sPath, string sSlash)
    {
        string sRet = "";
        try
        {
            if (sPath.Substring(sPath.Length - 1) == "/") sPath.Substring(0, sPath.Length - 1);
            if (sPath.Substring(sPath.Length - 1) == @"\") sPath.Substring(0, sPath.Length - 1);
            sRet = sPath + sSlash;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public static string appendWebSlash(string sPath)
    {
        string sRet = "";
        try
        {
            sRet = appendPathSlash(sPath, "/");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public static string appendFileSysSlash(string sPath)
    {
        string sRet = "";
        try
        {
            sRet = appendPathSlash(sPath, @"\");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}
