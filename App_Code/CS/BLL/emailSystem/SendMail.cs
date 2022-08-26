using System;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;
using System.Net.Mime;

/// <summary>
/// Summary description for SendMail
/// </summary>

public class SendMail
{
    private const string c_sMailSubjet = "Integrated Activity Planning Change Request Tool";
    private const string c_sPECMailSubjet = "Business Improvement: ";
    private readonly string c_sLogoPath = HttpContext.Current.Server.MapPath(@"~/Images/p_ShellLogo.gif");

    private readonly structUserMailIdx m_eSender;
    //readonly appUsersHelper oHlp = new appUsersHelper();
    //readonly EIdd.ContractHoldersMgt ch = new EIdd.ContractHoldersMgt();
    //readonly EIdd.CategoryMgt catMgt = new EIdd.CategoryMgt();

    public SendMail(structUserMailIdx _eSender)
    {
        m_eSender = _eSender;
    }

    public static structUserMailIdx eManager()
    {
        return new structUserMailIdx(AppConfiguration.adminName, AppConfiguration.adminEmail, "");
    }

    //private bool Mailer(string fromEmailAddress, string[] toEmailAddress, string mSubject, string mBody, string mCC)
    //{
    //    bool success = true; 
    //    try
    //    {
    //        SmtpClient smtp = new SmtpClient(AppConfiguration.MailServer);
    //        MailMessage msg = new MailMessage();
    //        msg.From = new MailAddress(fromEmailAddress);

    //        //This will take care of sending mail to multiple people
    //        foreach (String s in toEmailAddress)
    //        {
    //            msg.To.Add(s);
    //        }

    //        if (mCC.Length != 0)
    //        {
    //            msg.CC.Add(mCC);
    //        }

    //        msg.Subject = mSubject;
    //        msg.Body = mBody;
    //        msg.IsBodyHtml = true;
    //        smtp.Send(msg);
    //    }
    //    catch (Exception ex)
    //    {
    //        success = false;
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //    return success;
    //}

    private bool Mailer(string mfrom, string mTo, string mSubject, string mBody, string mBcc)
    {
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(mfrom);
        msg.To.Add(mTo);
        msg.Bcc.Add(mBcc);
        msg.Subject = mSubject;
        msg.Body = mBody;
        msg.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient(AppConfiguration.smtpHost);

        bool success = true; 
        try
        {
            smtp.Send(msg);
        }
        catch (Exception ex)
        {
            success = false;
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return success;
    }

    public bool ApplicationError(string toEmailAddress, string fromEmailAddress, string errorMessage)
    {
        string mFrom, mTo, mSubject, mBody;

        mFrom = fromEmailAddress;
        mTo = toEmailAddress;
        mSubject = "Integrated Activity Planning";
        mBody = "Dear System Admnistrator, <br/> <br/>";
        mBody += errorMessage;
        mBody += "<br/>";

        return Mailer(mFrom, mTo, mSubject, mBody, mFrom);
    }

    public bool mailIAPUser(structUserMailIdx toEmailAddress, string url, int iRoleId)
    {
        bool bRet = false;
        //string[] toEmail = { toEmailAddress };

        string mSubject = "IAP Change Request Tool";

        string mBody = Resources.Resource.DefineUser;
        mBody = mBody.Replace("@@ROLE", appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId));

        if (iRoleId == (int)appUsersRoles.userRole.ChangeReviewBoardChairman)
        {
            mBody = mBody.Replace("@@ActivityOwner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.ActivityOwner) + ", ");
            mBody = mBody.Replace("@@IAPPlanner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.IAPPlanner) + ", ");
            mBody = mBody.Replace("@@FunctionalRepresentative", appUsersRoles.userRoleDesc(appUsersRoles.userRole.FunctionalAuthorizer) + "s, ");
            mBody = mBody.Replace("@@AssetProductionRepresentative", "and " + appUsersRoles.userRoleDesc(appUsersRoles.userRole.ProductionAssetAuthorizer) + ".");
        }
        else if (iRoleId == (int)appUsersRoles.userRole.ProductionAssetAuthorizer)
        {
            mBody = mBody.Replace("@@ActivityOwner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.ActivityOwner) + ", ");
            mBody = mBody.Replace("@@IAPPlanner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.IAPPlanner) + ", ");
            mBody = mBody.Replace("@@FunctionalRepresentative", "and " + appUsersRoles.userRoleDesc(appUsersRoles.userRole.FunctionalAuthorizer) + "s.");
            mBody = mBody.Replace("@@AssetProductionRepresentative", "");
        }
        else if (iRoleId == (int)appUsersRoles.userRole.FunctionalAuthorizer)
        {
            mBody = mBody.Replace("@@ActivityOwner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.ActivityOwner) + ", ");
            mBody = mBody.Replace("@@IAPPlanner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.IAPPlanner) + ". ");
            mBody = mBody.Replace("@@FunctionalRepresentative", "");
            mBody = mBody.Replace("@@AssetProductionRepresentative", "");
        }
        else if (iRoleId == (int)appUsersRoles.userRole.IAPPlanner)
        {
            mBody = mBody.Replace("@@ActivityOwner", appUsersRoles.userRoleDesc(appUsersRoles.userRole.ActivityOwner) + ". ");
            mBody = mBody.Replace("@@IAPPlanner", "");
            mBody = mBody.Replace("@@FunctionalRepresentative", "");
            mBody = mBody.Replace("@@AssetProductionRepresentative", "");
        }

        string contacts = "";
        List<Users> supportRoles = UsersActions.lstGetContacts();
        foreach (Users supportRole in supportRoles)
        {
            contacts += (supportRole.m_sFullName + " (" + supportRole.m_sPhoneExtention + " or " + supportRole.m_sUserMail + "); ");
        }

        mBody = mBody.Replace("@@Contacts", contacts);

        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool ActivityOwnerMailsIAPPlanner(structUserMailIdx toEmailAddress, string Originator, string requestNumber, string project_activity, string url)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request";

        string mBody = Resources.Resource.ActivityOwnerSendsRequestToIAPPlanner;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool ActivityOwnerNotifiesIAPUpdate(List<structUserMailIdx> toEmailAddress, string Originator, string requestNumber, string project_activity, string url)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request Updated";

        string mBody = Resources.Resource.ActivityOwnerSendsRequestIAPOnChangeToIAPPlanner;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool NotifyNextApprover(structUserMailIdx toEmailAddress, string Originator, string requestNumber, string project_activity, string url, List<structUserMailIdx> CopyEmailAddress)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request";

        string mBody = Resources.Resource.IAPPlannerSendRequestToApprovers;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, CopyEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool NotifyImpactedPaties(List<structUserMailIdx> toEmailAddress, string Originator, string requestNumber, string project_activity, string url, List<structUserMailIdx> CopyEmailAddress)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request: CR Impacting your activity";

        string mBody = Resources.Resource.IAPPlannerSendRequestForIPReview; //(Where IP = Impacted/Supporting Parties)
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@TODAY", DateTime.Today.Date.ToLongDateString());
        mBody = mBody.Replace("@@NEXT2DAYS", DateTime.Today.Date.AddDays(2).ToLongDateString());
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, CopyEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool WarnImpactedPaties(structUserMailIdx toEmailAddress, string Originator, string requestNumber, string project_activity, string url, List<structUserMailIdx> CopyEmailAddress, string sDateReceived)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request: Reminder!!! CR Impacting your activity";

        string mBody = Resources.Resource.SystemAutoMailToImpactedParty;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@DATE_RECEIVED", sDateReceived);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, CopyEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool ImpactedPatiesSLAExpired(structUserMailIdx toEmailAddress, string Originator, string requestNumber, string project_activity, string url, List<structUserMailIdx> CopyEmailAddress, string sDateReceived)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request Service Level Agreement has expired";

        string mBody = Resources.Resource.SystemAutoMailToImpactedPartySLAXpired;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@ORIGINATOR", Originator);
        mBody = mBody.Replace("@@DATE_RECEIVED", sDateReceived);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, CopyEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool PerformanceReporting(structUserMailIdx toEmailAddress, string sBody)
    {
        bool bRet = false;
        string mSubject = "Weekly Report - Pending IAP Change Request Performance in the last Two Months!!!";

        string mBody = Resources.Resource.PerformanceReporting;
        mBody = mBody.Replace("@@REPORTING", sBody);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool ChangeRequestApproved(structUserMailIdx toEmailAddress, List<structUserMailIdx> cc, string Approver, string requestNumber, string project_activity, string url, string approverRole)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request approved";

        string mBody = Resources.Resource.ChangeRequestApproved;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@APPROVER", Approver);
        mBody = mBody.Replace("@@ROLE", approverRole);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool IPReviewdChangeRequest(structUserMailIdx toEmailAddress, structUserMailIdx copyEmailAddress, string Reviewer, string requestNumber, string project_activity, string url)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request Reviewed by an Impacted Party";

        string mBody = Resources.Resource.ChangeRequestApproved;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@APPROVER", Reviewer);
        mBody = mBody.Replace("@@ROLE", "Impacted Party");
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool AllIPReviewdChangeRequest(structUserMailIdx toEmailAddress, structUserMailIdx copyEmailAddress, string Reviewer, string requestNumber, string project_activity, string url)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request Reviewed by all Impacted Parties";

        string mBody = Resources.Resource.ChangeRequestApproved;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@APPROVER", Reviewer);
        mBody = mBody.Replace("@@ROLE", "All Impacted Parties have reviewed the request.");
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    public bool ChangeRequestNotApproved(List<structUserMailIdx> toEmailAddress, string Approver, string requestNumber, string project_activity, string ReasonNotApproved, string approverRole)
    {
        bool bRet = false;
        string mSubject = requestNumber + " - IAP Change Request not approved";

        string mBody = Resources.Resource.ChangeRequestNotApproved;
        mBody = mBody.Replace("@@REQUEST_NUMBER", requestNumber);
        mBody = mBody.Replace("@@APPROVER", Approver);
        mBody = mBody.Replace("@@ROLE", approverRole);
        mBody = mBody.Replace("@@PROJECT_ACTIVITY", project_activity);
        mBody = mBody.Replace("@@REASON", ReasonNotApproved);
        mBody = mBody.Replace("@@URL", Utilities.AppURL());
        //mBody = SystemAdmin(mBody);

        AlternateView altView = CommonContent(mBody, Utilities.AppURL());
        var oMail = new emailClientExPic(m_eSender);
        bRet = oMail.sendShellMail(toEmailAddress, mSubject, mBody, altView);

        return bRet;
    }

    //private string SystemAdmin(string mBody)
    //{
    //    Users Admin = UsersActions.getAdministrators();
    //    mBody = mBody.Replace("@SSSADMIN", AppConfiguration.adminName);
    //    mBody = mBody.Replace("@@EXT", AppConfiguration.adminExt);
    //    mBody = mBody.Replace("@@EMAIL", AppConfiguration.adminEmail);

    //    return mBody;
    //}

    private AlternateView CommonContent(string mBody, string httpHost)
    {
        Users Admin = UsersActions.getAdministrators();
        mBody = mBody.Replace("@SSSADMIN", AppConfiguration.adminName);
        mBody = mBody.Replace("@@EXT", AppConfiguration.adminExt);
        mBody = mBody.Replace("@@EMAIL", AppConfiguration.adminEmail);


        //sBody = sBody.Replace("@=EEEE", httpHost);
        //sBody = sBody.Replace("@=TTTT", DateTime.Today.ToLongDateString());

        //sBody = sBody.Replace("@=L1SUPPORT", AppConfiguration.prodAdminName + "[" + AppConfiguration.prodAdminEmail + "]");
        //sBody = sBody.Replace("@=L2SUPPORT", AppConfiguration.prodSupportName + "[" + AppConfiguration.prodSupportEmail + "]");

        AlternateView altView = AlternateView.CreateAlternateViewFromString(mBody, null, MediaTypeNames.Text.Html);
        var pic = new LinkedResource(c_sLogoPath, MediaTypeNames.Image.Gif);
        pic.ContentId = "Logo";
        altView.LinkedResources.Add(pic);

        return altView;
    }
}