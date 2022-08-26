using System;
using System.Web.UI;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Telerik.Web.UI;

public partial class IAPMasterPage : aspnetMaster
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            bindMenu();
            //logoutHyperLink.Attributes.Add("onclick", "return LogoutMessage()");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void ddlSearch_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        DataTable dt = !string.IsNullOrEmpty(e.Text) ? RequestHelper.dtSearchRequest(e.Text, sfield) : null;
        RadComboControlLoader(dt, ddlSearch);
    }

    public static void RadComboControlLoader(DataTable dt, RadComboBox RadDdl)
    {
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var item = new RadComboBoxItem();
                item.Text = (string) dr["REQUEST_NUMBER"];
                item.Value = dr["IDREQUEST"].ToString();

                string email = dr["REQUEST_NUMBER"].ToString();
                string refInd = dr["PROJECT_ACTIVITY"].ToString();

                item.Attributes.Add("REQUEST_NUMBER", email);
                item.Attributes.Add("PROJECT_ACTIVITY", refInd);

                RadDdl.Items.Add(item);
                item.DataBind();
            }
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Response.Redirect("~/eSearch.aspx?lRequestId=" + e.Value);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(oSessnx.getOnlineUser.m_iUserId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void bindMenu()
    {
        string uriString = "";
        //Users CurrentUser = UsersActions.getUserByDomainLoginID(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name));

        if (oSessnx.getOnlineUser.m_iUserRole == (int)appUsersRoles.userRole.admin)
        {
            adminMenu1.Init_Page(AppConfiguration.AdminMenu);
            //uriString = Server.MapPath("~/AppMenuXML/Administrator.xml");
        }
        else if (oSessnx.getOnlineUser.m_iUserRole == (int)appUsersRoles.userRole.IAPPlanner)
        {
            adminMenu1.Init_Page(AppConfiguration.PlannersMenu);
            //uriString = Server.MapPath("~/AppMenuXML/IAPPlanner.xml");
        }
        else if ((oSessnx.getOnlineUser.m_iUserRole == (int)appUsersRoles.userRole.FunctionalAuthorizer)
            || (oSessnx.getOnlineUser.m_iUserRole == (int)appUsersRoles.userRole.ProductionAssetAuthorizer)
            || (oSessnx.getOnlineUser.m_iUserRole == (int)appUsersRoles.userRole.ChangeReviewBoardChairman))
        {
            adminMenu1.Init_Page(AppConfiguration.ApproversMenu);
            //uriString = Server.MapPath("~/AppMenuXML/ApproversMenu.xml");
        }
        else
        {
            //Where Activity Owner could be anybody
            adminMenu1.Init_Page(AppConfiguration.ActivityOwnerMenu);
            //uriString = Server.MapPath("~/AppMenuXML/ActivityOwnerMenu.xml");
        }

        //DataSet ds = new DataSet();
        //ds.ReadXml(uriString);
        //Menu1.DataSource = ds;
        //Menu1.DataBind();

        userProfile(oSessnx.getOnlineUser.m_sFullName, oSessnx.getOnlineUser.m_iUserRole);

        //if (oSessnx.getOnlineUser.m_iUserId == 0)
        //{
        //    userProfile(HttpContext.Current.User.Identity.Name, IAPRoles.m_sActivityOwner);
        //}
        //else
        //{
        //    userProfile(oSessnx.getOnlineUser.m_sFullName, oSessnx.getOnlineUser.m_sRole);
        //}
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/eSearch.aspx?RequestNumber=" + requestNoTextBox.Text);
    }

    private void userProfile(string logInUserFullName, int iRoleId)
    {
        loggedinUserLabel.Text = logInUserFullName;
        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
        dateLabel.Text = DateTime.Today.Date.ToLongDateString();
    }
    protected void adminLinkButton_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "Application/pdf";
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", "AdminManual.pdf"));
        Response.Flush();
        Response.WriteFile(Server.MapPath("~/Help/AdminManual.pdf"));
    }
    protected void aoLinkButton_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "Application/pdf";
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", "ActivityOwnerManual.pdf"));
        Response.Flush();
        Response.WriteFile(Server.MapPath("~/Help/ActivityOwnerManual.pdf"));
    }

    protected void approversLinkButton_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "Application/pdf";
        Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", "ApproverManual.pdf"));
        Response.Flush();
        Response.WriteFile(Server.MapPath("~/Help/ApproverManual.pdf"));
    }
}