using System;
using System.Collections.Generic;

public partial class RequestApproval : aspnetPage
{
    aRequest o = new aRequest();
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            userRoleLabel.Text = oSessnx.getOnlineUser.m_sFullName + " (" + appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole) + ")";
                                 
            ApprovalWorkFlow.FillApprovalDropDownList(ddlStand);

            if (!string.IsNullOrEmpty(Request.QueryString["RequestId"]))
            {
                long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
                RequestInformation1.LoadRequest(lRequestId);

                ApproversComment oComment = ApprovalWorkFlow.GetApproverCommentByUser(oSessnx.getOnlineUser.m_iUserId, lRequestId);

                ddlStand.SelectedValue = oComment.m_iStatus.ToString();
                txtComment.Text = oComment.m_sComments;
            }
        }
    }


    protected void forwardButton_Click(object sender, EventArgs e)
    {
        try
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            o = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);            

            Users Planner = UsersActions.objGetUserByUserId(o.m_iPlannerId);
            Users FunctnAuthoriser = UsersActions.objGetUserByUserId(o.m_iAuthoriserId);
            //Users PAR = UsersActions.objGetUserByUserId(o.m_iAssetAuthoriserId);
            Users CRB = UsersActions.objGetUserByUserId(o.m_iDrbChairId);
            Users Finance = UsersActions.objGetUserByUserId(o.m_iFinanceId);

            //Get all Impacted parties email address
            List<structUserMailIdx> ImpactedPaties = new List<structUserMailIdx>();
            List<ApproversComment> oComments = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(lRequestId);
            foreach(ApproversComment d in oComments)
            {
                ImpactedPaties.Add(UsersActions.objGetUserByUserId(d.m_iUserId).structUserIdx);
            }


            //List<Users> ImpactedParties = ImpactedParties

            Users ActivityOwner = UsersActions.objGetIapOwnerByUserId(o.m_iUserId);

            if (ddlStand.SelectedValue == RequestStatus.m_iApproved.ToString())
            {
                if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.FunctionalAuthorizer) FunctionalAuthoriser(Planner, ActivityOwner);
                else if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.FinanceAuthorizer) FinanceAuthoriser(CRB, ActivityOwner, Planner, ImpactedPaties);
                //else if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.ProductionAssetAuthorizer) ProductionAssetAuthoriser(CRB, ActivityOwner, Planner, Finance, ImpactedPaties);
                else if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.ChangeReviewBoardChairman) ChangeChairmanReviewBoard(CRB, ActivityOwner, FunctnAuthoriser, Planner, Finance, ImpactedPaties);
            }
            else if (ddlStand.SelectedValue == RequestStatus.m_iNotApproved.ToString())
            {
                List<structUserMailIdx> ReceiversEmail = new List<structUserMailIdx>();
                //Update the comment on the IAP_COMMENTS table
                ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, o.lRequestId, int.Parse(ddlStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

                //Update the IAP Request Status in the IAP_REQUESTFORM table
                if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.FunctionalAuthorizer)
                {
                    ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iNotApprovedByFunctionalAuthoriser);
                    ReceiversEmail.Add(ActivityOwner.structUserIdx);
                    ReceiversEmail.Add(Planner.structUserIdx);
                }

                //else if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.ProductionAssetAuthorizer)
                //{
                //    ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iNotApprovedByProductionAssetAuthorizer);
                //    ReceiversEmail.Add(ActivityOwner.structUserIdx);
                //    ReceiversEmail.Add(Planner.structUserIdx);
                //}

                else if (oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.ChangeReviewBoardChairman)
                {
                    ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iNotApprovedByChairmanReviewBoard);
                    ReceiversEmail.Add(ActivityOwner.structUserIdx);
                    ReceiversEmail.Add(Planner.structUserIdx);
                    //ReceiversEmail.Add(PAR.structUserIdx);
                }

                //Mail the Activity Owner that his/her Change request was not approved.
                SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

                bool sent = MyMail.ChangeRequestNotApproved(ReceiversEmail, oSessnx.getOnlineUser.m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, txtComment.Text, appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
                if (sent == true)
                {
                    string message = "Notification has been sent to the activity owner.";
                    ajaxWebExtension.showJscriptAlert(Page, this, message);
                    //this.ClientScript.RegisterClientScriptBlock(typeof(string), "key3", string.Format("alert('{0}');", message), true);
                }
                forwardButton.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(AppConfiguration.AppNameId, ex.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    //Step 1
    private void FunctionalAuthoriser(Users Planner, Users ActivityOwner)
    {
        ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, o.lRequestId, int.Parse(ddlStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

        //Assign request to Asset IAP Planner 
        bool bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(o.lRequestId, o.m_iPlannerId);
        bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iIAPPlanner);

        SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

        List<structUserMailIdx> cc = new List<structUserMailIdx>();
        cc.Add(ActivityOwner.structUserIdx);

        MyMail.NotifyNextApprover(Planner.structUserIdx, UsersActions.objGetUserByUserId(o.m_iUserId).m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), cc);
        forwardButton.Enabled = false;

        ajaxWebExtension.showJscriptAlert(Page, this, "Request successfully sent to IAP Planner.");
    }

    //Step 2
    private void FinanceAuthoriser(Users CRB, Users ActivityOwner, Users Planner, List<structUserMailIdx> ImpactedPaties)
    {
        ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, o.lRequestId, int.Parse(ddlStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

        //Assign request to DRB Chairman
        bool bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(o.lRequestId, o.m_iDrbChairId);
        ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iChangeReviewBoardChairman);

        // Send mail to DRB Chairman
        SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

        List<structUserMailIdx> cc = ImpactedPaties;
        cc.Add(ActivityOwner.structUserIdx);
        cc.Add(Planner.structUserIdx);

        MyMail.NotifyNextApprover(CRB.structUserIdx, UsersActions.objGetUserByUserId(o.m_iUserId).m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), cc);
        forwardButton.Enabled = false;

        ajaxWebExtension.showJscriptAlert(Page, this, "Request successfully sent to Change Review Board Chairman.");
    }

    //Step 3
    private void ChangeChairmanReviewBoard(Users CRB, Users ActivityOwner, Users FunctnAuthoriser, Users Planner, Users Finance, List<structUserMailIdx> ImpactedPaties)
    {
        ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, o.lRequestId, int.Parse(ddlStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);//, CurrentUser.m_iUserRole
        ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iApproved);

        // Send mail to Activity Owner and copy Everybody in the approval line that the request has been approved
        SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

        List<structUserMailIdx> cc = ImpactedPaties;
        MyMail.NotifyNextApprover(CRB.structUserIdx, UsersActions.objGetUserByUserId(o.m_iUserId).m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), cc);

        //Users IAPPlanner = ApprovalWorkFlow.GetApproverByRequestId(ThisRequest.lRequestId, (int)appUsersRoles.userRole.IAPPlanner);                    
        if (o.m_sOU.ToUpper() == appOUs.ouDesc(appOUs.ous.SNEPCO).ToUpper())
        {
            cc.Add(FunctnAuthoriser.structUserIdx);
            cc.Add(Planner.structUserIdx);
            //cc.Add(PAR.structUserIdx);
            MyMail.ChangeRequestApproved(ActivityOwner.structUserIdx, cc, oSessnx.getOnlineUser.m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
        }
        else
        {
            cc.Add(FunctnAuthoriser.structUserIdx);
            cc.Add(Planner.structUserIdx);
            cc.Add(Finance.structUserIdx);
            //cc.Add(PAR.structUserIdx);
            cc.Add(CRB.structUserIdx);
            MyMail.ChangeRequestApproved(ActivityOwner.structUserIdx, cc, oSessnx.getOnlineUser.m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
        }

        forwardButton.Enabled = false;

        ajaxWebExtension.showJscriptAlert(Page, this, "Request successfully approved, activity owner alerted.");
    }

    ////NOTE:  Step 0 (Obsolute: has been depricated)
    //private void ProductionAssetAuthoriser(Users CRB, Users ActivityOwner, Users Planner, Users Finance, List<structUserMailIdx> ImpactedPaties)
    //{
    //    ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, o.lRequestId, int.Parse(ddlStand.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

    //    //Assign request to Change Review Board Chairman
    //    bool bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(o.lRequestId, o.m_iDrbChairId);
    //    ApprovalWorkFlow.UpdateRequestFlowStatus(o.lRequestId, RequestStatus.m_iChangeReviewBoardChairman);

    //    // Send mail to Change Review Board Chairman
    //    SendMail MyMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

    //    List<structUserMailIdx> cc = ImpactedPaties;
    //    cc.Add(ActivityOwner.structUserIdx);
    //    cc.Add(Planner.structUserIdx);
    //    cc.Add(Finance.structUserIdx);

    //    MyMail.NotifyNextApprover(CRB.structUserIdx, UsersActions.objGetUserByUserId(o.m_iUserId).m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL(), cc);
    //    forwardButton.Enabled = false;

    //    ajaxWebExtension.showJscriptAlert(Page, this, "Request successfully sent to Change Review Board Chairman.");
    //}

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RequestList.aspx");
    }
}