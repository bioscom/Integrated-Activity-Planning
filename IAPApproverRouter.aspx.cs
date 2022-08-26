using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IAPApproverRouter : aspnetPage
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadApprovers();
            LoadControls();
        }
    }

    private void LoadApprovers()
    {
        if (Request.QueryString["RequestId"] != null)
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            RequestInformation1.LoadRequest(lRequestId);

            grdView.DataSource = ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId);
            grdView.DataBind();
            getRoles();
        }
    }

    private void getRoles()
    {
        foreach (GridViewRow grdRow in grdView.Rows)
        {
            Label lbRole = (Label)grdRow.FindControl("lbRole");
            
            int iRoleId = int.Parse(lbRole.Text);
            if (iRoleId == (int)appUsersRoles.userRole.ChangeReviewBoardChairman)
            {
                lbRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.FunctionalAuthorizer)
            {
                lbRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.ProductionAssetAuthorizer)
            {
                lbRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
            }
            else if (iRoleId == (int)appUsersRoles.userRole.IAPPlanner)
            {
                lbRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId);
            }

            DropDownList ddlRole = (DropDownList)grdRow.FindControl("ddlRole");
            Label Status = (Label)grdRow.FindControl("lblStatus");
            Status.Font.Bold = true;

            int iStatus = int.Parse(Status.Text);
            if (iStatus == RequestStatus.m_iApproved)
            {
                Status.ForeColor = System.Drawing.Color.Green;
                Status.Text = RequestStatus.m_sApproved;
                ddlRole.Enabled = false;
            }
            else if (iStatus == RequestStatus.m_iNotApproved)
            {
                Status.ForeColor = System.Drawing.Color.Red;
                Status.Text = RequestStatus.m_sNotApproved;
            }
            else if (iStatus == RequestStatus.m_iDefault)
            {
                Status.ForeColor = System.Drawing.Color.Red;
                Status.Text = "Pending Review";
            }
        }
    }

    private void LoadControls()
    {
        foreach (GridViewRow grdRow in grdView.Rows)
        {
            DropDownList ddlRole = (DropDownList)grdRow.FindControl("ddlRole");
            int iRoleId = int.Parse(ddlRole.Attributes["ROLEID"].ToString());
            if (iRoleId == (int)appUsersRoles.userRole.ChangeReviewBoardChairman)
            {
                ddlRole.Items.Add(new ListItem("Select Change Review Chair...", "-1"));
                List<Users> CRB = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.ChangeReviewBoardChairman);
                foreach (Users xCRB in CRB)
                {
                    ddlRole.Items.Add(new ListItem(xCRB.m_sFullName, xCRB.m_iUserId.ToString()));
                }
            }
            else if (iRoleId == (int)appUsersRoles.userRole.FunctionalAuthorizer)
            {
                ddlRole.Items.Add(new ListItem("Select Functional Authorizer...", "-1"));
                List<Users> FunctionalRepresentatives = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.FunctionalAuthorizer);
                foreach (Users oFunctionalRepresentative in FunctionalRepresentatives)
                {
                    ddlRole.Items.Add(new ListItem(oFunctionalRepresentative.m_sFullName, oFunctionalRepresentative.m_iUserId.ToString()));
                }
            }
            else if (iRoleId == (int)appUsersRoles.userRole.ProductionAssetAuthorizer)
            {
                ddlRole.Items.Add(new ListItem("Select Asset Authorizer...", "-1"));
                List<Users> PAR = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.ProductionAssetAuthorizer);
                foreach (Users oPAR in PAR)
                {
                    ddlRole.Items.Add(new ListItem(oPAR.m_sFullName, oPAR.m_iUserId.ToString()));
                }
            }
            else if (iRoleId == (int)appUsersRoles.userRole.IAPPlanner)
            {
                ddlRole.Items.Add(new ListItem("Select Planner...", "-1"));
                List<Users> Planners = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.IAPPlanner);
                foreach (Users oPlanner in Planners)
                {
                    ddlRole.Items.Add(new ListItem(oPlanner.m_sFullName, oPlanner.m_iUserId.ToString()));
                }
            }
        }
    }
    protected void btnForward_Click(object sender, EventArgs e)
    {
        string[] toEmailAddress = { "" };
        bool success = false;
        long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);
        Users oActivityOwner = UsersActions.objGetIapOwnerByUserId(oRequest.m_iUserId);

        foreach (GridViewRow grdRow in grdView.Rows)
        {
            DropDownList ddlRole = (DropDownList)grdRow.FindControl("ddlRole");
            if (ddlRole.Enabled == true)
            {
                if (ddlRole.SelectedValue != "-1")
                {
                    Label lblApprover = (Label)grdRow.FindControl("lblApprover");
                    int iOldUserId = int.Parse(lblApprover.Attributes["IDUSER"].ToString());
                    success = ApprovalWorkFlow.RerouteRequestApprover(lRequestId, int.Parse(ddlRole.SelectedValue), iOldUserId);

                    if (success)
                    {
                        if (oRequest.m_iStatus == RequestStatus.m_iChangeReviewBoardChairman)
                        {
                            mailNotification(ddlRole, oActivityOwner, toEmailAddress, oRequest);
                        }
                        else if (oRequest.m_iStatus == RequestStatus.m_iFunctionalRepresentatives)
                        {
                            mailNotification(ddlRole, oActivityOwner, toEmailAddress, oRequest);
                        }
                        else if (oRequest.m_iStatus == RequestStatus.m_iProductionAssetRepresentative)
                        {
                            mailNotification(ddlRole, oActivityOwner, toEmailAddress, oRequest);
                        }
                        else if (oRequest.m_iStatus == RequestStatus.m_iIAPPlanner)
                        {
                            mailNotification(ddlRole, oActivityOwner, toEmailAddress, oRequest);
                        }
                    }
                }
            }
        }

        string message = "Approvers re-route successful, notification mails sent.";
        ajaxWebExtension.showJscriptAlert(Page, this, message);
    }

    private void mailNotification(DropDownList ddlRole, Users oActivityOwner, string[] toEmailAddress, aRequest oRequest)
    {
        SendMail oMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);
        List<structUserMailIdx> cc = new List<structUserMailIdx>();
        cc.Add(oActivityOwner.structUserIdx);

        Users approver = UsersActions.objGetUserByUserId(int.Parse(ddlRole.SelectedValue));
        oMail.NotifyNextApprover(approver.structUserIdx, oActivityOwner.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);
    }
}
