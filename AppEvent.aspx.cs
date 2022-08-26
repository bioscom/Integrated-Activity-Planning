using System;

public partial class AppEvent : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Users CurrentUser = UsersActions.getUserByDomainLoginID(Apps.LoginIDNoDomain(User.Identity.Name));
        ////Code that runs when an unhandled error occurs
        Exception objErr = Server.GetLastError().GetBaseException();
        string err = "<b>Error Caught in Application_Error event<b/> <br/> <br/>";
        err += "Error in: " + Request.Url.ToString() + "<br/>";
        err += "<br/>Error Message: " + objErr.Message.ToString() + "";
        err += "<br/> <br/>Stack Trace: " + objErr.StackTrace.ToString();

        Server.ClearError();

        SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);
        o.ApplicationError(AppConfiguration.ErrorLogEmail, AppConfiguration.ErrorLogEmail, err);

        Response.Redirect("~/Index.aspx");
    }
}
