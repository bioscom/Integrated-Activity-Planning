using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sRedirect = "";

            if (!IsPostBack)
            {
                string sMsg = "";
                string sRet = "";
                switch (sMsg)
                {
                    case "sLO":
                        sRedirect = "~/Support/logout.aspx?Msg=sLO";
                        break;

                    case "sTl":
                        sRet = UseWindowsAuth();
                        if (sRet != "")
                        {
                            sRedirect = "~/Support/pageDenied.aspx?Msg=" + sRet;
                        }
                        break;

                    default:
                        sRet = UseWindowsAuth();

                        if (sRet == "Relogin")
                        {
                            Response.Redirect("~/Default.aspx");
                        }

                        else if (sRet == "NotFound")
                        {
                            sRedirect = "~/Support/pageDenied.aspx?Msg=" + sRet;
                            //Response.Redirect("~/Login.aspx"); //Old
                        }

                        else if (!string.IsNullOrEmpty(sRet))
                        {
                            sRedirect = "~/Support/pageDenied.aspx?Msg=" + sRet;
                        }

                        break;
                }
            }

            if (sRedirect != "")
            {
                Response.Redirect(sRedirect);
            }
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions("Login Error", ex.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    
    private string UseWindowsAuth()
    {
        //Note: User Authentication, in this portal, is based on the application the user wants to log into.
        //Tokenes are used from the URL of the application to know where the user wants to go.
        //Where string sToken determines where the user is going
        //"Lean" for Lean Projects Management application

        string sRet = "Err";
        try
        {
            loginUser oLogin = new loginUser();
            loginUser.loginRet me = oLogin.verifyAppUser();

            //Note: loginRet, a member of the loginUser Class, returns three objects; statusx, eAppUserInfo and eIWHUserInfo
            //User to be authenticated could be found on eAppUserInfo or eIWHUserInfo object, 
            //therefore test eAppUserInfo and eIWHUserInfo to see where login user is found.

            if (me.eAppUserInfo != null)
            {
                sRet = LogMeIn(me, sRet);
            }
            else if (me.eAppUserInfo == null)
            {
                if (me.eIWHUserInfo != null)
                {
                    //If the above condition is true, automatically register this user in the corresponding user's table based on the application Login Token
                    //Call the funtion or method that register user from the add user page
                    //int iUserId = 0;

                    //1. Check if the User already exist but disabled, let it be re enabled automatically.
                    bool bRet = false;
                    Users o = UsersActions.objGetUserByUserName(me.eIWHUserInfo.m_sUserName);

                    if (!string.IsNullOrEmpty(o.m_sUserName))
                    {
                        //Activate the status
                        bRet = UsersActions.EnableUser(o.m_sUserName);
                    }
                    else
                    {
                        bRet = UsersActions.CreateUser(me.eIWHUserInfo.m_sUserName, me.eIWHUserInfo.m_sFullName, "0", me.eIWHUserInfo.m_sUserMail, 1, (int) appUsersRoles.userRole.ActivityOwner, (int) appUsersRoles.userStatus.activeUser, "");

                        if (bRet)
                        {
                            ajaxWebExtension.showJscriptAlert(Page, this, "Welcome, in case you need to change your profile, please call the support shown on this web page.");

                            //TODO:    Revisit this place
                            //Relogin to recreate session
                            //sRet = LogMeIn(me, sRet);
                            sRet = "Relogin";
                        }
                    }                    
                }
                else if (me.eIWHUserInfo == null)
                {
                    sRet = "NotFound";

                    //sendMail loginUserSendMail = new sendMail(new structUserMailIdx(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name), HttpContext.Current.User.Identity.Name, ""));

                    //List<structUserMailIdx> receivers = new List<structUserMailIdx>();
                    //receivers.Add(new structUserMailIdx(AppConfiguration.AdminUserName, AppConfiguration.adminEmail, ""));
                    //receivers.Add(new structUserMailIdx(AppConfiguration.ProdAdminUserName, AppConfiguration.prodAdminEmail, ""));
                    //receivers.Add(new structUserMailIdx(AppConfiguration.ProdSupportUserName, AppConfiguration.prodSupportEmail, ""));

                    //loginUserSendMail.ProfileNotFound(receivers);

                    //ajaxWebExtension.showJscriptAlert(Page, this, "Profile not found, login through please contact administrator");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    private string LogMeIn(loginUser.loginRet me, string sRet)
    {
        switch (me.eStatus)
        {
            case loginUser.statusx.loginSucceed:

                if (me.eAppUserInfo == null)
                {
                    FormsAuthentication.RedirectFromLoginPage(me.eIWHUserInfo.m_sUserName, true);
                    sRet = "Relogin";
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(me.eAppUserInfo.m_sUserName, true);
                    sRet = "";
                }

                break;
            case loginUser.statusx.idIsNotFound:
                sRet = "eId";
                break;
            case loginUser.statusx.loginFailed:
                sRet = "Err";
                break;
            case loginUser.statusx.statusDisabld:
                sRet = "nId";
                break;
            case loginUser.statusx.statusUnKnown:
                sRet = "nId";
                break;
            case loginUser.statusx.statusLocked:
                sRet = "lId";
                break;
        }
        return sRet;
    }
}
