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

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        form1.Attributes.Add("onload", "return disableBackButton()");
        
        Session.Abandon();
        FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage("Msg=" & sRet);
        Session.Clear();
        Session.Abandon();
        string httpHost = HttpContext.Current.Request.ServerVariables["http_host"].ToString() + "/PEC";
        Response.Redirect(httpHost);
        //FormsAuthentication.RedirectFromLoginPage(Session["UserName"], false);
    }
}
