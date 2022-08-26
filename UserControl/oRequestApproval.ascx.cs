using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControl_oRequestApproval : aspnetUserControl
{
    private aRequest oRequest = new aRequest();
    string sfield = "REQUEST_NUMBER";
    //private SendMail MyMail = new SendMail();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RequestId"] != null)
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"]);
            RequestInformation1.LoadRequest(lRequestId);

            if (!IsPostBack)
            {
                IsFinanceRequired();
                pnlFinanceYN.Visible = false;
                forwardButton.Enabled = false;

                ApprovalWorkFlow.FillApprovalDropDownList(ddlPlannerStand);

                //Update on Jan 2020
                ApprovalWorkFlow.FillChangeTypeDropDownList(ddlChangeType);

                List<Users> CRB = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ChangeReviewBoardChairman);
                foreach (Users xCRB in CRB)
                {
                    ddlCrb.Items.Add(new ListItem(xCRB.m_sFullName, xCRB.m_iUserId.ToString()));
                }

                //List<Users> PAR = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ProductionAssetAuthorizer);
                //foreach (Users xPAR in PAR)
                //{
                //    ddlPar.Items.Add(new ListItem(xPAR.m_sFullName, xPAR.m_iUserId.ToString()));
                //}

                //Get Finance Authorisers
                List<Users> FinanceAuth = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.FinanceAuthorizer);
                foreach (Users _user in FinanceAuth)
                {
                    ddlFinance.Items.Add(new ListItem(_user.m_sFullName, _user.m_iUserId.ToString()));
                }

                if (ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId).Rows.Count > 0)
                {
                    LoadApproval(lRequestId);
                }

            }
        }
    }

    protected void forwardButton_Click(object sender, EventArgs e)
    {
        bool bRet = false;
        long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
        oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);
        Users oActivityOwner = UsersActions.objGetIapOwnerByUserId(oRequest.m_iUserId);

        if (ddlPlannerStand.SelectedValue == RequestStatus.m_iApproved.ToString())
        {
            if (rdbLstFinanceYN.SelectedValue == "1")
            {
                if (ddlFinance.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
                {
                    string message = "Please select Finance support.";
                    ajaxWebExtension.showJscriptAlertCx(Page, this, message);
                }
            }
            else if (ddlCrb.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
            {
                string message = "Please select Change Review Board Chairman.";
                ajaxWebExtension.showJscriptAlertCx(Page, this, message);
            }
            //else if (ddlPar.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
            //{
            //    string message = "Please select Production Asset Authorizer";
            //    ajaxWebExtension.showJscriptAlertCx(Page, this, message);
            //}


            //else if (((ddlCrb.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString()) || (ddlPar.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString())))
            else if (ddlCrb.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString())
            {
                bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(ddlPlannerStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
                bRet = ApprovalWorkFlow.RequestUpdateChangeType(oRequest.lRequestId, int.Parse(ddlChangeType.SelectedValue));

                if (rdbLstFinanceYN.SelectedValue == "1")
                {
                    bRet = ApprovalWorkFlow.AssignApproversToRequest1(lRequestId, int.Parse(ddlCrb.SelectedValue), int.Parse(ddlFinance.SelectedValue));

                    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(ddlFinance.SelectedValue));//Assign request to Finance
                    bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iFinance);

                    //Send a notification email to Finance and copy the Activity Owner
                    Users FinanceAuthoriser = UsersActions.objGetUserByUserId(int.Parse(ddlFinance.SelectedValue));

                    List<structUserMailIdx> cc = new List<structUserMailIdx>();
                    cc.Add(oActivityOwner.structUserIdx);

                    SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);
                    MyMail.NotifyNextApprover(FinanceAuthoriser.structUserIdx, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);
                }
                else if(rdbLstFinanceYN.SelectedValue == "2")
                {
                    //bRet = ApprovalWorkFlow.AssignApproversToRequest2(lRequestId, int.Parse(ddlPar.SelectedValue), int.Parse(ddlCrb.SelectedValue));

                    bRet = ApprovalWorkFlow.AssignApproversToRequest3(lRequestId, int.Parse(ddlCrb.SelectedValue));
                    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(ddlCrb.SelectedValue));//Assign request to DRB Chairman
                    bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iChangeReviewBoardChairman);

                    //Send a notification email to DRB Chairman and copy the Activity Owner
                    Users DRBChairman = UsersActions.objGetUserByUserId(int.Parse(ddlCrb.SelectedValue));

                    List<structUserMailIdx> cc = new List<structUserMailIdx>();
                    cc.Add(oActivityOwner.structUserIdx);

                    SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);
                    MyMail.NotifyNextApprover(DRBChairman.structUserIdx, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);
                }

                if (bRet) Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
                else ajaxWebExtension.showJscriptAlertCx(Page, this, "Not Successful, try again later!!!");
            }
        }

        else if (ddlPlannerStand.SelectedValue == RequestStatus.m_iNotApproved.ToString())
        {
            ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(ddlPlannerStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
            bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iNotApprovedByPlanner);
            bRet = ApprovalWorkFlow.RequestUpdateChangeType(oRequest.lRequestId, int.Parse(ddlChangeType.SelectedValue));

            SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);
            List<structUserMailIdx> toEmail = new List<structUserMailIdx>();
            toEmail.Add(oActivityOwner.structUserIdx);

            //Mail the Activity Owner that his/her Change request was not approved.
            bool sent = MyMail.ChangeRequestNotApproved(toEmail, oSessnx.getOnlineUser.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity,
                Utilities.AppURL(), appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
               
            if (sent)
            {
                string message = "Notification has been sent to the activity owner.";
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "key3",
                    string.Format("alert('{0}');", message), true);
            }
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RequestList.aspx");
    }

    //If request is called up by Planner, retrieve the approval details and present for further actions

    private void LoadApproval(long lRequestId)
    {
        List<ApproversComment> lstApprover = ApprovalWorkFlow.lstGetApproversCommentsByRequestId(lRequestId);
        foreach (ApproversComment oApprover in lstApprover)
        {
            if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.IAPPlanner)
            {
                txtComment.Text = oApprover.m_sComments;
                ddlPlannerStand.SelectedValue = oApprover.m_iStatus.ToString();
            }
            if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ChangeReviewBoardChairman)
            {
                ddlCrb.SelectedValue = oApprover.m_iUserId.ToString();
            }
            //if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ProductionAssetAuthorizer)
            //{
            //    ddlPar.SelectedValue = oApprover.m_iUserId.ToString();
            //}
        }
    }

    private void IsFinanceRequired()
    {
        rdbLstFinanceYN.Items.Clear();

        var item = new ButtonListItem();
        item.Text = "Yes";
        item.Value = "1";
        rdbLstFinanceYN.Items.Add(item);

        item = new ButtonListItem();
        item.Text = "No";
        item.Value = "2";
        rdbLstFinanceYN.Items.Add(item);
    }

    protected void rdbLstFinanceYN_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbLstFinanceYN.SelectedValue == "1") { ddlFinance.Visible = true; lblFinance.Visible = true; }
        else { ddlFinance.Visible = false; lblFinance.Visible = false; }

        pnlFinanceYN.Visible = true;
        forwardButton.Enabled = true;
    }
}