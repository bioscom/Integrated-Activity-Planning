using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BPMoC_NewForm : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<OU> OUS = RequestFormRequirement.getOU();
            foreach (OU ou in OUS)
            {
                ddlOU.Items.Add(new ListItem(ou.m_sOU, ou.m_iIDOU.ToString()));
            } 

            //TODO: Connect Plan Issue to its Table

            List<ChangeType> ChangeTypes = RequestFormRequirement.getChangeTypes();
            foreach (ChangeType changeType in ChangeTypes)
            {
                ddlChangeType.Items.Add(new ListItem(changeType.m_sTYPE, changeType.m_iIDCHANGE.ToString()));
            }

            List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
            foreach (PlanType planType in PlanTypes)
            {
                ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
            }

            ////Activity Executors
            //lstUsers = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.ChangeReviewBoardChairman);
            //foreach (Users oUser in lstUsers)
            //{
            //    ddlDE.Items.Add(new ListItem(oUser.m_sFullName, oUser.m_iUserId.ToString()));
            //}

            //Business Opportunity Managers
            List<Users> lstUsers = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.BusinessOpportunityManager);
            foreach (Users oUser in lstUsers)
            {
                ddlBOM.Items.Add(new ListItem(oUser.m_sFullName, oUser.m_iUserId.ToString()));
            }

            //Project Managers
            lstUsers = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.ProjectManager);
            foreach (Users oUser in lstUsers)
            {
                ddlProjMgr.Items.Add(new ListItem(oUser.m_sFullName, oUser.m_iUserId.ToString()));
            }

            //Decision Executive Managers
            lstUsers = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.ChangeReviewBoardChairman);
            foreach (Users oUser in lstUsers)
            {
                ddlDE.Items.Add(new ListItem(oUser.m_sFullName, oUser.m_iUserId.ToString()));
            }

            lblOriginator.Text = oSessnx.getOnlineUser.m_sFullName; 
            lblOriginator2.Text = oSessnx.getOnlineUser.m_sFullName; 
            lblDate.Text = DateTime.Today.Date.ToLongDateString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ajaxWebExtension.showJscriptAlert(Page, this, "Request successfully submitted.");
    }
    protected void ddlOU_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Area> MyAreas = RequestFormRequirement.getAreasByOU(int.Parse(ddlOU.SelectedValue));
        foreach (Area MyArea in MyAreas)
        {
            ddlArea.Items.Add(new ListItem(MyArea.m_sAREA, MyArea.m_iIDAREA.ToString()));
        }
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load Locations
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add(new ListItem("Please Select", "-1"));
        List<Location> Locations = RequestFormRequirement.getLocationsByArea(int.Parse(ddlArea.SelectedValue));
        foreach (Location locatn in Locations)
        {
            ddlLocation.Items.Add(new ListItem(locatn.m_sLOCATION, locatn.m_iLOCATIONID.ToString()));
        }
    }
}