using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Admin_EditUser : System.Web.UI.Page
{
    private int Userid = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["UserID"] != null)
        {
            Userid = int.Parse(Request.QueryString["UserID"].ToString());
        }
        
        if (!IsPostBack)
        {
            List<Functions> getFunctions = IAPFunctions.getFunctions();
            foreach (Functions func in getFunctions)
            {
                functionDropDownList.Items.Add(new ListItem(func.m_sFUNCTION, func.m_iFUNCTIONID.ToString()));
            }

            appUsersRoles.getUserRoles(userroleList);

            List<ImpactAnalysisWindowDetail> getImpactAnalysisWindows = ImpactAnalysis.getImpactAnalysisWindow();
            foreach (ImpactAnalysisWindowDetail ImpactWindowDetails in getImpactAnalysisWindows)
            {
                iapPlannerTypeDdl.Items.Add(new ListItem(ImpactWindowDetails.m_sWINDOWTYPE, ImpactWindowDetails.m_iWINDOWID.ToString()));
            }

            iapPlannerTypeDdl.Enabled = false;
            LoadUserDetails(Userid);
        }
    }

    private void LoadUserDetails(int UserID)
    {
        Users thisUser = UsersActions.objGetUserByUserId(UserID);

        lblUser.Text = thisUser.m_sFullName;
        functionDropDownList.SelectedValue = thisUser.m_iFunctionId.ToString();
        userroleList.SelectedValue = thisUser.m_iUserRole.ToString();

        UserRoleTest();

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        bool success = UsersActions.EditUser(Userid, iapPlannerTypeDdl.SelectedValue, int.Parse(functionDropDownList.SelectedValue), int.Parse(userroleList.SelectedValue), txtPhoneNo.Text);
        if (success == true)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
        }
        else
        {
            string message = "Change not successful, please try again!!!";
            ajaxWebExtension.showJscriptAlert(Page, this, message);
        }
    }

    protected void userroleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        UserRoleTest();
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
    }

    private void UserRoleTest()
    {
        if (int.Parse(userroleList.SelectedValue) == (int)appUsersRoles.userRole.IAPPlanner)
        {
            iapPlannerTypeDdl.Enabled = true;
        }
        else
        {
            iapPlannerTypeDdl.Enabled = false;
        }
    }
}
