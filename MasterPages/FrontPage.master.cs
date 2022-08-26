using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPages_FrontPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loggedinUserLabel.Text = HttpContext.Current.User.Identity.Name;
        roleLabel.Text = "Anonymous";
        dateLabel.Text = DateTime.Today.Date.ToLongDateString();
    }
}
