using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlannerRequestApproval : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //pnlImpactedParties.Visible = false;
        //pnlApprovers.Visible = false;

        //Check if all Impacted/Supporting parties have reviewed?
        if (!string.IsNullOrEmpty(Request.QueryString["RequestId"]))
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"]);
            List<ApproversComment> olst = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(lRequestId);

            //var found = olst.FindIndex(c => c.m_iStatus == RequestStatus.m_iDefault);
            //if (found > 0)
            //{
            //    pnlImpactedParties.Visible = true;
            //    pnlApprovers.Visible = false;
            //}
            //else
            //{
            //    pnlImpactedParties.Visible = false;
            //    pnlApprovers.Visible = true;
            //}

            ////foreach (ApproversComment o in olst)
            ////{
            ////    if (o.m_iStatus == RequestStatus.m_iDefault)
            ////    {
            ////        pnlImpactedParties.Visible = true;
            ////        pnlApprovers.Visible = false;
            ////        break;
            ////    }
            ////    else
            ////    {
            ////        pnlImpactedParties.Visible = false;
            ////        pnlApprovers.Visible = true;
            ////    }
            ////}
            oImpactedPartiesComments.GetImpactecdPartiesComments(lRequestId);
        }
    }

    //private aRequest oRequest = new aRequest();
    //private SendMail MyMail = new SendMail();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["RequestId"] != null)
    //    {
    //        long lRequestId = long.Parse(Request.QueryString["RequestId"]);
    //        RequestInformation1.LoadRequest(lRequestId);

    //        if (!IsPostBack)
    //        {
    //            ApprovalWorkFlow.FillApprovalDropDownList(standddl);

    //            List<Users> CRB = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ChangeReviewBoardChairman);
    //            foreach (Users xCRB in CRB)
    //            {
    //                crbddl.Items.Add(new ListItem(xCRB.m_sFullName, xCRB.m_iUserId.ToString()));
    //            }

    //            List<Users> PAR = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ProductionAssetAuthorizer);
    //            foreach (Users xPAR in PAR)
    //            {
    //                parddl.Items.Add(new ListItem(xPAR.m_sFullName, xPAR.m_iUserId.ToString()));
    //            }

    //            //List<Users> FunctionalRepresentatives = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.FunctionalAuthorizer);
    //            //foreach (Users xFunctionalRepresentatives in FunctionalRepresentatives)
    //            //{
    //            //    FRCheckBoxList.Items.Add(new ListItem(xFunctionalRepresentatives.m_sFullName, xFunctionalRepresentatives.m_iUserId.ToString()));
    //            //}

    //            if (ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId).Rows.Count > 0)
    //            {
    //                LoadApproval(lRequestId);
    //            }

    //        }
    //    }
    //}

    //protected void forwardButton_Click(object sender, EventArgs e)
    //{
    //    bool bRet = false;
    //    long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
    //    oRequest = RequestHelper.objGetRequestByRequestId(lRequestId);
    //    Users oActivityOwner = UsersActions.objGetIapOwnerByUserId(oRequest.m_iUserId);

    //    if (standddl.SelectedValue == RequestStatus.m_iApproved.ToString())
    //    {
    //        if (crbddl.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
    //        {
    //            string message = "Please select Change Review Board Chairman";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key0",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //        else if (parddl.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
    //        {
    //            string message = "Please select Production Asset Authorizer";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key1",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //        else if (((crbddl.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString()) ||
    //                  (parddl.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString())))
    //        {
    //            bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

    //            bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(crbddl.SelectedValue));//Assign request to Change Review Board Chairman                    
    //            bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(parddl.SelectedValue));//Assign request to Asset Authoriser

    //            bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iProductionAssetRepresentative);

    //            //Send a notification email to Asset Authoriser and copy the Activity Owner
    //            Users AssetAuthoriser = UsersActions.objGetUserByUserId(int.Parse(parddl.SelectedValue));
    //            string[] toEmail = { AssetAuthoriser.m_sUserMail };

    //            MyMail.NotifyNextApprover(toEmail, oSessnx.getOnlineUser.m_sUserMail, oRequest.m_sORIGINATOR,
    //                oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), oActivityOwner.m_sUserMail);

    //            Response.Redirect("~/Common/IAPRequestList.aspx");


    //            //if (ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId).Rows.Count > 0)
    //            //{
    //            //    bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
    //            //    //Assign request to Change Review Board Chairman
    //            //   ApproversComment oCommentDRB = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.ChangeReviewBoardChairman, lRequestId);

    //            //    if (int.Parse(crbddl.SelectedValue) != oCommentDRB.m_iUserId)
    //            //    {
    //            //        bRet = ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId, int.Parse(crbddl.SelectedValue), oCommentDRB.m_iUserId);
    //            //    }

    //            //    //Assign request to Production Asset Representative
    //            //    ApproversComment oCommentPAR = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.ProductionAssetAuthorizer, lRequestId);
    //            //    if (int.Parse(parddl.SelectedValue) != oCommentPAR.m_iUserId)
    //            //    {
    //            //        bRet = ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId,
    //            //            int.Parse(parddl.SelectedValue), oCommentPAR.m_iUserId);
    //            //    }

    //            //    #region Unifiaction overruled codes
    //            //    ////.Assign request to Functional Representative approvers and send notification emails
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        ApproversComment oCommentFR = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.FunctionalAuthorizer, lRequestId);
    //            //    //        if (int.Parse(li.Value) != oCommentFR.m_iUserId)
    //            //    //        {
    //            //    //            ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId, int.Parse(li.Value),
    //            //    //                oCommentFR.m_iUserId);
    //            //    //        }
    //            //    //    }
    //            //    //}
    //            //    #endregion
    //            //}
    //            //else
    //            //{
    //            //    bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

    //            //    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(crbddl.SelectedValue));//Assign request to Change Review Board Chairman                    
    //            //    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(parddl.SelectedValue));//Assign request to Asset Authoriser

    //            //    bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iProductionAssetRepresentative);

    //            //    //Send a notification email to Asset Authoriser and copy the Activity Owner
    //            //    Users AssetAuthoriser = UsersActions.objGetUserByUserId(int.Parse(parddl.SelectedValue));
    //            //    string[] toEmail = { AssetAuthoriser.m_sUserMail };

    //            //    MyMail.NotifyNextApprover(toEmail, oSessnx.getOnlineUser.m_sUserMail, oRequest.m_sORIGINATOR,
    //            //        oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), oActivityOwner.m_sUserMail);

    //            //    #region Unifiaction overruled codes
    //            //    ////.Assign request to Functional Representative approvers and send notification emails
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(li.Value));
    //            //    //    }
    //            //    //}

    //            //    ////Determine the number of selected Functional Approvers
    //            //    //int kount = FRCheckBoxList.Items.Cast<ListItem>().Count(li => li.Selected);
    //            //    ////get the email address of all the Functional Representatives inside the toEmail array.
    //            //    //Users FunctionalReps = new Users();
    //            //    //int i = 0;
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        FunctionalReps = UsersActions.objGetUserByUserId(int.Parse(li.Value));
    //            //    //        toEmail[i] = FunctionalReps.m_sUserMail;
    //            //    //        i++;
    //            //    //    }
    //            //    //}
    //            //    #endregion
    //            //}
    //            //#region Unifiaction overruled codes
    //            ////else if (ValidateFunctionalRepSelection() == 0)
    //            ////{
    //            ////    string message = "Please select Change Functional Authorizer.";
    //            ////    this.ClientScript.RegisterClientScriptBlock(typeof (string), "key2",
    //            ////        string.Format("alert('{0}');", message), true);
    //            ////}
    //            //#endregion


    //            //Response.Redirect("~/Common/IAPRequestList.aspx");
    //        }
    //    }

    //    else if (standddl.SelectedValue == RequestStatus.m_iNotApproved.ToString())
    //    {
    //        ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
    //        bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iNotApprovedByPlanner);

    //        string[] ActivityOwnerEmail = { oActivityOwner.m_sUserMail };

    //        //Mail the Activity Owner that his/her Change request was not approved.
    //        bool sent = MyMail.ChangeRequestNotApproved(ActivityOwnerEmail, oSessnx.getOnlineUser.m_sUserMail,
    //            oSessnx.getOnlineUser.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity,
    //            Utilities.AppURL(),
    //            appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
    //        if (sent == true)
    //        {
    //            string message = "Notification has been sent to the activity owner.";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key3",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //    }
    //}

    ////private int ValidateFunctionalRepSelection()
    ////{
    ////    return FRCheckBoxList.Items.Cast<ListItem>().Count(li => li.Selected);
    ////}

    //protected void closeButton_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Common/IAPRequestList.aspx");
    //}


    ////If request is called up by Planner, retrieve the approval details and present for further actions

    //private void LoadApproval(long lRequestId)
    //{
    //    List<ApproversComment> lstApprover = ApprovalWorkFlow.lstGetApproversCommentsByRequestId(lRequestId);
    //    foreach (ApproversComment oApprover in lstApprover)
    //    {
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.IAPPlanner)
    //        {
    //            txtComment.Text = oApprover.m_sComments;
    //            standddl.SelectedValue = oApprover.m_iStatus.ToString();
    //        }
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ChangeReviewBoardChairman)
    //        {
    //            crbddl.SelectedValue = oApprover.m_iUserId.ToString();
    //        }
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ProductionAssetAuthorizer)
    //        {
    //            parddl.SelectedValue = oApprover.m_iUserId.ToString();
    //        }

    //        //if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.FunctionalAuthorizer)
    //        //{
    //        //    foreach (ListItem li in FRCheckBoxList.Items)
    //        //    {
    //        //        if (li.Value == oApprover.m_iUserId.ToString())
    //        //        {
    //        //            li.Selected = true;
    //        //        }
    //        //    }
    //        //}
    //    }
    //}
    //private aRequest oRequest = new aRequest();
    //private SendMail MyMail = new SendMail();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["RequestId"] != null)
    //    {
    //        long lRequestId = long.Parse(Request.QueryString["RequestId"]);
    //        RequestInformation1.LoadRequest(lRequestId);

    //        if (!IsPostBack)
    //        {
    //            ApprovalWorkFlow.FillApprovalDropDownList(standddl);

    //            List<Users> CRB = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ChangeReviewBoardChairman);
    //            foreach (Users xCRB in CRB)
    //            {
    //                crbddl.Items.Add(new ListItem(xCRB.m_sFullName, xCRB.m_iUserId.ToString()));
    //            }

    //            List<Users> PAR = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.ProductionAssetAuthorizer);
    //            foreach (Users xPAR in PAR)
    //            {
    //                parddl.Items.Add(new ListItem(xPAR.m_sFullName, xPAR.m_iUserId.ToString()));
    //            }

    //            //List<Users> FunctionalRepresentatives = UsersActions.lstGetUsersByRoleId((int) appUsersRoles.userRole.FunctionalAuthorizer);
    //            //foreach (Users xFunctionalRepresentatives in FunctionalRepresentatives)
    //            //{
    //            //    FRCheckBoxList.Items.Add(new ListItem(xFunctionalRepresentatives.m_sFullName, xFunctionalRepresentatives.m_iUserId.ToString()));
    //            //}

    //            if (ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId).Rows.Count > 0)
    //            {
    //                LoadApproval(lRequestId);
    //            }

    //        }
    //    }
    //}

    //protected void forwardButton_Click(object sender, EventArgs e)
    //{
    //    bool bRet = false;
    //    long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
    //    oRequest = RequestHelper.objGetRequestByRequestId(lRequestId);
    //    Users oActivityOwner = UsersActions.objGetIapOwnerByUserId(oRequest.m_iUserId);

    //    if (standddl.SelectedValue == RequestStatus.m_iApproved.ToString())
    //    {
    //        if (crbddl.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
    //        {
    //            string message = "Please select Change Review Board Chairman";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key0",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //        else if (parddl.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
    //        {
    //            string message = "Please select Production Asset Authorizer";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key1",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //        else if (((crbddl.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString()) ||
    //                  (parddl.SelectedValue != RequestStatus.m_iDropDownListFirstIndexValue.ToString())))
    //        {
    //            bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

    //            bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(crbddl.SelectedValue));//Assign request to Change Review Board Chairman                    
    //            bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(parddl.SelectedValue));//Assign request to Asset Authoriser

    //            bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iProductionAssetRepresentative);

    //            //Send a notification email to Asset Authoriser and copy the Activity Owner
    //            Users AssetAuthoriser = UsersActions.objGetUserByUserId(int.Parse(parddl.SelectedValue));
    //            string[] toEmail = { AssetAuthoriser.m_sUserMail };

    //            MyMail.NotifyNextApprover(toEmail, oSessnx.getOnlineUser.m_sUserMail, oRequest.m_sORIGINATOR,
    //                oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), oActivityOwner.m_sUserMail);

    //            Response.Redirect("~/Common/IAPRequestList.aspx");


    //            //if (ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId).Rows.Count > 0)
    //            //{
    //            //    bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
    //            //    //Assign request to Change Review Board Chairman
    //            //   ApproversComment oCommentDRB = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.ChangeReviewBoardChairman, lRequestId);

    //            //    if (int.Parse(crbddl.SelectedValue) != oCommentDRB.m_iUserId)
    //            //    {
    //            //        bRet = ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId, int.Parse(crbddl.SelectedValue), oCommentDRB.m_iUserId);
    //            //    }

    //            //    //Assign request to Production Asset Representative
    //            //    ApproversComment oCommentPAR = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.ProductionAssetAuthorizer, lRequestId);
    //            //    if (int.Parse(parddl.SelectedValue) != oCommentPAR.m_iUserId)
    //            //    {
    //            //        bRet = ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId,
    //            //            int.Parse(parddl.SelectedValue), oCommentPAR.m_iUserId);
    //            //    }

    //            //    #region Unifiaction overruled codes
    //            //    ////.Assign request to Functional Representative approvers and send notification emails
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        ApproversComment oCommentFR = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.FunctionalAuthorizer, lRequestId);
    //            //    //        if (int.Parse(li.Value) != oCommentFR.m_iUserId)
    //            //    //        {
    //            //    //            ApprovalWorkFlow.RerouteRequestApprover(oRequest.lRequestId, int.Parse(li.Value),
    //            //    //                oCommentFR.m_iUserId);
    //            //    //        }
    //            //    //    }
    //            //    //}
    //            //    #endregion
    //            //}
    //            //else
    //            //{
    //            //    bRet = ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);

    //            //    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(crbddl.SelectedValue));//Assign request to Change Review Board Chairman                    
    //            //    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(parddl.SelectedValue));//Assign request to Asset Authoriser

    //            //    bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iProductionAssetRepresentative);

    //            //    //Send a notification email to Asset Authoriser and copy the Activity Owner
    //            //    Users AssetAuthoriser = UsersActions.objGetUserByUserId(int.Parse(parddl.SelectedValue));
    //            //    string[] toEmail = { AssetAuthoriser.m_sUserMail };

    //            //    MyMail.NotifyNextApprover(toEmail, oSessnx.getOnlineUser.m_sUserMail, oRequest.m_sORIGINATOR,
    //            //        oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), oActivityOwner.m_sUserMail);

    //            //    #region Unifiaction overruled codes
    //            //    ////.Assign request to Functional Representative approvers and send notification emails
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        ApprovalWorkFlow.ForwardsRequestToApprovers(oRequest.lRequestId, int.Parse(li.Value));
    //            //    //    }
    //            //    //}

    //            //    ////Determine the number of selected Functional Approvers
    //            //    //int kount = FRCheckBoxList.Items.Cast<ListItem>().Count(li => li.Selected);
    //            //    ////get the email address of all the Functional Representatives inside the toEmail array.
    //            //    //Users FunctionalReps = new Users();
    //            //    //int i = 0;
    //            //    //foreach (ListItem li in FRCheckBoxList.Items)
    //            //    //{
    //            //    //    if (li.Selected)
    //            //    //    {
    //            //    //        FunctionalReps = UsersActions.objGetUserByUserId(int.Parse(li.Value));
    //            //    //        toEmail[i] = FunctionalReps.m_sUserMail;
    //            //    //        i++;
    //            //    //    }
    //            //    //}
    //            //    #endregion
    //            //}
    //            //#region Unifiaction overruled codes
    //            ////else if (ValidateFunctionalRepSelection() == 0)
    //            ////{
    //            ////    string message = "Please select Change Functional Authorizer.";
    //            ////    this.ClientScript.RegisterClientScriptBlock(typeof (string), "key2",
    //            ////        string.Format("alert('{0}');", message), true);
    //            ////}
    //            //#endregion


    //            //Response.Redirect("~/Common/IAPRequestList.aspx");
    //        }
    //    }

    //    else if (standddl.SelectedValue == RequestStatus.m_iNotApproved.ToString())
    //    {
    //        ApprovalWorkFlow.RequestApprovalProcess(txtComment.Text, oRequest.lRequestId, int.Parse(standddl.SelectedValue), oSessnx.getOnlineUser.m_iUserId);
    //        bRet = ApprovalWorkFlow.UpdateRequestFlowStatus(oRequest.lRequestId, RequestStatus.m_iNotApprovedByPlanner);

    //        string[] ActivityOwnerEmail = { oActivityOwner.m_sUserMail };

    //        //Mail the Activity Owner that his/her Change request was not approved.
    //        bool sent = MyMail.ChangeRequestNotApproved(ActivityOwnerEmail, oSessnx.getOnlineUser.m_sUserMail,
    //            oSessnx.getOnlineUser.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity,
    //            Utilities.AppURL(),
    //            appUsersRoles.userRoleDesc((appUsersRoles.userRole) oSessnx.getOnlineUser.m_iUserRole));
    //        if (sent == true)
    //        {
    //            string message = "Notification has been sent to the activity owner.";
    //            this.ClientScript.RegisterClientScriptBlock(typeof (string), "key3",
    //                string.Format("alert('{0}');", message), true);
    //        }
    //    }
    //}

    ////private int ValidateFunctionalRepSelection()
    ////{
    ////    return FRCheckBoxList.Items.Cast<ListItem>().Count(li => li.Selected);
    ////}

    //protected void closeButton_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Common/IAPRequestList.aspx");
    //}


    ////If request is called up by Planner, retrieve the approval details and present for further actions

    //private void LoadApproval(long lRequestId)
    //{
    //    List<ApproversComment> lstApprover = ApprovalWorkFlow.lstGetApproversCommentsByRequestId(lRequestId);
    //    foreach (ApproversComment oApprover in lstApprover)
    //    {
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.IAPPlanner)
    //        {
    //            txtComment.Text = oApprover.m_sComments;
    //            standddl.SelectedValue = oApprover.m_iStatus.ToString();
    //        }
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ChangeReviewBoardChairman)
    //        {
    //            crbddl.SelectedValue = oApprover.m_iUserId.ToString();
    //        }
    //        if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.ProductionAssetAuthorizer)
    //        {
    //            parddl.SelectedValue = oApprover.m_iUserId.ToString();
    //        }

    //        //if (UsersActions.objGetUserByUserId(oApprover.m_iUserId).m_iUserRole == (int) appUsersRoles.userRole.FunctionalAuthorizer)
    //        //{
    //        //    foreach (ListItem li in FRCheckBoxList.Items)
    //        //    {
    //        //        if (li.Value == oApprover.m_iUserId.ToString())
    //        //        {
    //        //            li.Selected = true;
    //        //        }
    //        //    }
    //        //}
    //    }
    //}
}