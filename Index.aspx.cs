using System;

public partial class _Index : aspnetPage
{
    string RequestNumber = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (oSessnx.getOnlineUser.m_iUserId != 0)
        {
            if (Request.QueryString["RequestNumber"] == null)
            {
                Response.Redirect("~/RequestList.aspx");
            }
            else if (Request.QueryString["RequestNumber"] != null)
            {
                RequestNumber = Request.QueryString["RequestNumber"].ToString();
                Response.Redirect("~/RequestList.aspx");
            }
        }
        else
        {
            if (appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole).ToUpper() == "UnKnown Account".ToUpper())
            {
                //Creates an account for a user as a Change Requestor
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                //This assumes you are an IAP Change Request user
                Response.Redirect("~/RequestList.aspx");
            }
        }
    }
}