using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Admin_Users : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search4User1.InitPage();
            List<Functions> getFunctions = IAPFunctions.getFunctions();
            foreach (Functions func in getFunctions)
            {
                functionDropDownList.Items.Add(new ListItem(func.m_sFUNCTION, func.m_iFUNCTIONID.ToString()));
            }

            appUsersRoles.getUserRoles(userroleList);

            //List<UserRoles> getRoles = UserRolesMgt.xUserRoles();
            //foreach (UserRoles roles in getRoles)
            //{
            //   userroleList.Items.Add(new ListItem(roles.sRole, roles.iRoleID.ToString()));
            //}

            List<ImpactAnalysisWindowDetail> getImpactAnalysisWindows = ImpactAnalysis.getImpactAnalysisWindow();
            foreach (ImpactAnalysisWindowDetail ImpactWindowDetails in getImpactAnalysisWindows)
            {
                iapPlannerTypeDdl.Items.Add(new ListItem(ImpactWindowDetails.m_sWINDOWTYPE, ImpactWindowDetails.m_iWINDOWID.ToString()));
            }

            iapPlannerTypeDdl.Enabled = false;
        }
    }
     protected void submitButton_Click(object sender, EventArgs e)
    {
        if ((int.Parse(userroleList.SelectedValue) == (int)appUsersRoles.userRole.IAPPlanner) && (iapPlannerTypeDdl.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString()))
        {
           ajaxWebExtension.showJscriptAlert(Page, this, "Please select IAP Planner Type.");
        }
        else
        {
            //Users CurrentUser = UsersActions.getUserByDomainLoginID(Apps.LoginIDNoDomain(User.Identity.Name));

            CreateNewUser(iapPlannerTypeDdl, functionDropDownList, userroleList, txtPhoneNo, oSessnx.getOnlineUser);

        }
    }

     public void CreateNewUser(DropDownList ddlIAPPlannerType, DropDownList ddlFunctionDropDownList, DropDownList ddlUserRoleList, TextBox txtPhoneNo, Users CurrentUser)
     {
         bool success = false;
         SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

         string IAPPlannerType = "";
         if (ddlIAPPlannerType.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
             IAPPlannerType = "";
         else
             IAPPlannerType = ddlIAPPlannerType.SelectedValue;

         success = UsersActions.CreateUser(Search4User1.selectedUserDetails.m_sUserName.ToUpper(), Search4User1.selectedUserDetails.m_sFullName, 
             ddlIAPPlannerType.SelectedValue, Search4User1.selectedUserDetails.m_sUserMail, int.Parse(ddlFunctionDropDownList.SelectedValue), 
             int.Parse(ddlUserRoleList.SelectedValue), (int)appUsersRoles.userStatus.activeUser, txtPhoneNo.Text);

         if (success == true)
         {
             success = MyMail.mailIAPUser(Search4User1.selectedUserDetails.structUserIdx, Utilities.AppURL(), int.Parse(ddlUserRoleList.SelectedValue));
             ajaxWebExtension.showJscriptAlert(Page, this, Search4User1.selectedUserDetails.m_sFullName + " successfully created for the role " + ddlUserRoleList.SelectedItem.Text);
         }
         else
         {
             ajaxWebExtension.showJscriptAlert(Page, this, "Sorry, " + Search4User1.selectedUserDetails.m_sFullName + " user already exists.");
         }
     }


    private void Clear()
    {
        userroleList.ClearSelection();
        iapPlannerTypeDdl.ClearSelection();
        functionDropDownList.ClearSelection();
        userroleList.ClearSelection();
        txtPhoneNo.Text = "";
    }

    protected void userroleList_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
        //Response.Redirect("~/Admin/ViewUsers.aspx");
    }
}
