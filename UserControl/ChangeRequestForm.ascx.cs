using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ChangeRequestForm : aspnetUserControl
{
    //RequestHelper request = new RequestHelper();
    public string url;
    string sRequestNumber = "";
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    private void PopulateTreeViewControl()
    {
        try
        {
            TreeNode parentNode = null;
            var lstAreas = RequestFormRequirement.getAreas();
            foreach (Area oArea in lstAreas)
            {
                if (oArea.m_sAREA.ToUpper() != "N/A".ToUpper())
                {
                    parentNode = new TreeNode(oArea.m_sAREA, oArea.m_iIDAREA.ToString());

                    var lstResponsibleParties = UsersActions.LstLocationResponsiblePartysByHub(oArea.m_iIDAREA);
                    foreach (Users oUser in lstResponsibleParties)
                    {
                        var childNode = new TreeNode(oUser.m_sFullName, oUser.m_iUserId.ToString());
                        parentNode.ChildNodes.Add(childNode);
                    }

                    parentNode.ExpandAll();
                    mnuTreeView.Nodes.Add(parentNode);
                }
            }
            PopulateFunctionsTreeViewControl();
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void PopulateFunctionsTreeViewControl()
    {
        try
        {
            TreeNode parentNode = null;
            List<Functions> lstFunctions = IAPFunctions.getFunctions(); //RequestFormRequirement.getAreas();
            foreach (Functions o in lstFunctions)
            {
                if (o.m_sFUNCTION.ToUpper() != "N/A".ToUpper())
                {
                    parentNode = new TreeNode(o.m_sFUNCTION, o.m_iFUNCTIONID.ToString());

                    List<Users> lstResponsibleParties = UsersActions.LstFunctionResponsiblePartysByFunction(o.m_iFUNCTIONID);
                    foreach (Users oUser in lstResponsibleParties)
                    {
                        var childNode = new TreeNode(oUser.m_sFullName, oUser.m_iUserId.ToString());
                        parentNode.ChildNodes.Add(childNode);
                    }

                    parentNode.ExpandAll();
                    mnuTreeView.Nodes.Add(parentNode);
                }
            }
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void LoadImpactedParties(long lRequestId)
    {
        List<ApproversComment> oComments = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(lRequestId);
        foreach (TreeNode n in mnuTreeView.Nodes)
        {
            foreach (TreeNode cn in n.ChildNodes)
            {
                ApproversComment oT = oComments.Find(c => c.m_iUserId == int.Parse(cn.Value));
                if (oT != null)
                {
                    if (oT.m_iUserId == int.Parse(cn.Value)) cn.Checked = true;
                }
            }
        }
    }

    private void SendToImpactedParties(aRequest oRequest)
    {
        try
        {
            List<structUserMailIdx> eTos = new List<structUserMailIdx>();
            foreach (TreeNode n in mnuTreeView.Nodes)
            {
                foreach (TreeNode cn in n.ChildNodes)
                {
                    if (cn.Checked)
                    {
                        ApprovalWorkFlow.ForwardsRequestToImpactedParties(oRequest.lRequestId, int.Parse(cn.Value));//Assign request to selected impacted/supporting parties
                        eTos.Add(UsersActions.objGetUserByUserId(int.Parse(cn.Value)).structUserIdx); // Collect their email address, to forward them notification emails

                        ////before assign request, check if the cn.Value already exists
                        //List<ApproversComment> oComments = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(oRequest.lRequestId);
                        //ApproversComment oT = oComments.Find(c => c.m_iUserId == int.Parse(cn.Value));
                        //if (oT == null)
                        //{
                        //    ApprovalWorkFlow.ForwardsRequestToImpactedParties(oRequest.lRequestId, int.Parse(cn.Value));//Assign request to selected impacted/supporting parties
                        //    eTos.Add(UsersActions.objGetUserByUserId(int.Parse(cn.Value)).structUserIdx); // Collect their email address, to forward them notification emails
                        //}
                    }
                }
            }

            List<structUserMailIdx> cc = new List<structUserMailIdx>();
            cc.Add(UsersActions.objGetUserByUserId(oRequest.m_iUserId).structUserIdx);

            // Copy some Special People
            List<Users> Corporate = UsersActions.lstGetContacts();
            foreach(var t in Corporate)
            {
                cc.Add(UsersActions.objGetUserByUserId(t.m_iUserId).structUserIdx);
            }

            SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);
            o.NotifyImpactedPaties(eTos, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);

            //ajaxWebExtension.showJscriptAlert(Page, this, "Change Request sent to all selected Impacted/Supporting Parties.");
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Page_Init()
    {
        try
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["RequestId"]))
                {
                    long lRequestId = long.Parse(Request.QueryString["RequestId"]);
                    submitButton.Visible = false;
                    InitialiseControls(lRequestId);
                    PopulateTreeViewControl();
                    LoadImpactedParties(lRequestId);
                }
                else
                {
                    UpdateButton.Enabled = false;
                    UpdateButton.Visible = false;


                    LoadControls();
                    PopulateTreeViewControl();
                    personnelInformationList1.Init_Page();
                    personnelInformationList1.MySaveButton.Visible = false;

                    locFieldLocations1.Page_Init();
                    locFieldLocations1.MySaveButton.Visible = false;
                    locFieldLocations1.MyUpdateButton.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }   

    protected void ddlOU_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load IAP, Planner, Functional Authoriser, and Area depending on the Organisation Unit selected
        LoadAreas(int.Parse(ddlOU.SelectedValue));
        //GetIAPPlanners(int.Parse(ddlOU.SelectedValue));

        //Get functional Authorisers
        List<Users> functionalAuth = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.FunctionalAuthorizer);
        foreach (Users _user in functionalAuth)
        {
            ddlFunctionalAuthoriser.Items.Add(new ListItem(_user.m_sFullName, _user.m_iUserId.ToString()));
        }
    }

    private void GetIAPPlanners()
    {
        string sUserFullName = "";
        List<Users> IAPPlanners = UsersActions.getIAPPlanners();
        foreach (Users o in IAPPlanners)
        {
            if (o.m_iOuId == 1)
            {
                sUserFullName = o.m_sFullName + " " + "(Onshore)";
                ddlPlanner.Items.Add(new ListItem(sUserFullName, o.m_iUserId.ToString()));
            }
            else if (o.m_iOuId == 2)
            {
                sUserFullName = o.m_sFullName + " " + "(Offshore)";
                ddlPlanner.Items.Add(new ListItem(sUserFullName, o.m_iUserId.ToString()));
            }
        }
    }

    private void GetIAPPlanners(int iOuId)
    {
        List<Users> IAPPlanners = UsersActions.getIAPPlannersByOU(iOuId);
        foreach (Users _user in IAPPlanners)
        {
            ddlPlanner.Items.Add(new ListItem(_user.m_sFullName, _user.m_iUserId.ToString()));
        }
    }

    private void LoadControls()
    {
        List<OU> OUS = RequestFormRequirement.getOU();
        foreach (OU ou in OUS)
        {
            ddlOU.Items.Add(new ListItem(ou.m_sOU, ou.m_iIDOU.ToString()));
        }

        GetIAPPlanners();

        List<ChangeType> ChangeTypes = RequestFormRequirement.getChangeTypes();
        foreach (ChangeType changeType in ChangeTypes)
        {
            changeTypeList.Items.Add(new ListItem(changeType.m_sTYPE, changeType.m_iIDCHANGE.ToString()));
        }

        List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
        foreach (PlanType planType in PlanTypes)
        {
            ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
        }
    }

    private void LoadAreas(int iOu)
    {
        //Load Areas
        areaList.Items.Clear();
        areaList.Items.Add(new ListItem("Please Select", "-1"));
        List<Area> MyAreas = RequestFormRequirement.getAreasByOU(iOu);
        foreach (Area MyArea in MyAreas)
        {
            areaList.Items.Add(new ListItem(MyArea.m_sAREA, MyArea.m_iIDAREA.ToString()));
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        aRequest oRequest = GetFormData();

        //Check if an Impacted/Supporting party was selected
        if (IsImpactedPartySelected()) RunWorkFlow(oRequest);
        else ajaxWebExtension.showJscriptAlertCx(Page, this, "Please, select Impacted parties!!!");
    }

    private aRequest GetFormData()
    {
        string dRequestDate = DateTime.Now.Date.ToShortDateString();

        aRequest oRequest = new aRequest();
        oRequest.m_iAreaId = int.Parse(areaList.SelectedValue);
        oRequest.m_iChangeId = int.Parse(changeTypeList.SelectedValue);
        oRequest.m_iOuId = int.Parse(ddlOU.SelectedValue);
        oRequest.m_iLocationId = int.Parse(ddlLocation.SelectedValue);
        oRequest.m_iPlanTypeId = int.Parse(ddlPlanType.SelectedValue);
        oRequest.m_iRefPlanId = int.Parse(ddlRefPlan.SelectedValue);
        oRequest.m_sBenefit = txtBenefit.Text;
        oRequest.m_sORIGINALFROM = dtDateOriginalFrom.SelectedDate;
        oRequest.m_sORIGINALTO = dtDateOriginalTo.SelectedDate;
        oRequest.m_sPLANISSUE = "";
        oRequest.m_sPRIMAVERA_ACTIVITYID = txtPrimaveraId.Text;
        oRequest.m_sProjectActivity = txtProjectActivity.Text;
        oRequest.m_sProposal = txtProposal.Text;

        oRequest.m_sNCOST = txtNewCost.Text;
        oRequest.m_sNVOL = txtNewVOL.Text;
        oRequest.m_sNVOLGAS = txtNewVOLgas.Text;

        oRequest.m_sPVOL = txtPlanVOL.Text;
        oRequest.m_sPVOLGAS = txtPlanVOLgas.Text;
        oRequest.m_sPCOST = txtPlanCost.Text;

        oRequest.m_sREQUESTEDFROM = dtDateScheduleFrom.SelectedDate;
        oRequest.m_sREQUESTEDTO = dtDateScheduleTo.SelectedDate;
        oRequest.m_sRisk = txtRisk.Text;
        oRequest.m_iUserId = oSessnx.getOnlineUser.m_iUserId;
        oRequest.m_sWO_NO = txtWONO.Text;

        oRequest.m_sGH20 = txtGH20.Text;
        oRequest.m_sLH20 = txtLH20.Text;
        oRequest.m_iPlannerId = int.Parse(ddlPlanner.SelectedValue);
        oRequest.m_iAuthoriserId = int.Parse(ddlFunctionalAuthoriser.SelectedValue);

        return oRequest;
    }

    private bool IsImpactedPartySelected()
    {
        bool found = false;
        foreach (TreeNode n in mnuTreeView.Nodes)
        {
            foreach (TreeNode cn in n.ChildNodes)
            {
                found = cn.Checked;
                if (found) break;
            }
            if (found) break;
        }

        return found;
    }

    private void RunWorkFlow(aRequest oRequest)
    {
        RequestHelper newRequest = new RequestHelper();
        long lRequestId = 0;
        string sRequestNumber = "";

        try
        {
            if (ValidateApprovers())
            {
                //Goes to Fucntional Representative..
                bool bRet = newRequest.CreateRequest(ddlPlanner, oRequest, RequestStatus.m_iFunctionalRepresentatives, ref lRequestId, ref sRequestNumber);
                if (bRet)
                {
                    oRequest.lRequestId = lRequestId;
                    oRequest.m_sRequestNumber = sRequestNumber;

                    SendToImpactedParties(oRequest);

                    personnelInformationList1.RequestId.Value = lRequestId.ToString();
                    locFieldLocations1.RequestId.Value = lRequestId.ToString();

                    personnelInformationList1.MySaveButton.Visible = true;
                    locFieldLocations1.MySaveButton.Visible = true;

                    //Assign the IAP Request to the selected Functional Authoriser in the IAP_COMMENTS table.
                    //Activity Owner forwards the request to the Functional Authoriser
                    bRet = ApprovalWorkFlow.ForwardsRequestToApprovers(lRequestId, int.Parse(ddlFunctionalAuthoriser.SelectedValue));
                    if (bRet)
                    {
                        string message = "Continue to the next tab. Please note, IAP request not completed and notification not sent to Functional Authoriser until you fill the last form.";
                        ajaxWebExtension.showJscriptAlert(Page, this, message);

                        submitButton.Enabled = false; submitButton.Visible = false;
                        UpdateButton.Enabled = true; UpdateButton.Visible = true;
                        FormMultiView.ActiveViewIndex = 1;
                        var lblMsg = (Label)Parent.FindControl("lblMsg");
                        lblMsg.Text = "Personnel Information";
                    }
                }
                else
                {
                    string message = "Can not continue...";
                    ajaxWebExtension.showJscriptAlert(Page, this, message);
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private bool ValidateApprovers()
    {
        bool bRet = false;

        if (ddlFunctionalAuthoriser.SelectedValue != "-1")
        {
            bRet = true;
        }
        else
        {
            string message = "Please select funtional authoriser.";
            ajaxWebExtension.showJscriptAlert(Page, this, message);
        }

        //if (ddlFinance.SelectedValue != "-1")
        //{
        //    bRet = true;
        //}
        //else
        //{
        //    string message = "Please select Finance authoriser";
        //    ajaxWebExtension.showJscriptAlert(Page, this, message);
        //}

        if (ddlPlanner.SelectedValue != "-1")
        {
            bRet = true;
        }
        else
        {
            string message = "Please select funtional planner.";
            ajaxWebExtension.showJscriptAlert(Page, this, message);
        }

        return bRet;
    }

    private void Clear()
    {
        txtProjectActivity.Text = ""; txtWONO.Text = ""; //txtPrimaveraID.Text = "";
        //ddlOU.ClearSelection(); 
        ddlLocation.ClearSelection(); ddlRefPlan.ClearSelection();
        areaList.ClearSelection(); ddlPlanType.ClearSelection(); changeTypeList.ClearSelection();
        ddlPlanner.ClearSelection();

        txtProposal.Text = "";
        txtBenefit.Text = ""; //txtImpact.Text = ""; 
        txtRisk.Text = ""; txtPlanVOL.Text = "";
        txtNewVOL.Text = ""; txtPlanCost.Text = "";
        txtPlanVOLgas.Text = ""; txtNewVOLgas.Text = "";
        txtNewCost.Text = "";

        //txtOriginalFrom.Text = ""; txtOriginalTo.Text = "";
        //txtRequestedFrom.Text = ""; txtRequestedTo.Text = "";
    }

    protected void ddlPlanner_SelectedIndexChanged(object sender, EventArgs e)
    {
        RequestHelper.ReferencePlan(ddlPlanner, ddlRefPlan);
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/myIAPRequestList.aspx");
    }

    protected void impactLB_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/RequestFormImpactAnalysis.aspx?RequestNumber=" + sRequestNumber + "&IAPPlanner=" + ddlPlanner.SelectedValue);
    }
    
    protected void areaList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load Locations
        ddlLocation.Items.Clear();
        ddlLocation.Items.Add(new ListItem("Please Select", "-1"));
        List<Location> Locations = RequestFormRequirement.getLocationsByArea(int.Parse(areaList.SelectedValue));
        foreach (Location locatn in Locations)
        {
            ddlLocation.Items.Add(new ListItem(locatn.m_sLOCATION, locatn.m_iLOCATIONID.ToString()));
        }
    }

    protected void pnlCloseButton_Click(object sender, EventArgs e)
    {
        //pnlRating.Visible = false;
    }

    protected void clearButton_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void PersInfoNext_Click(object sender, EventArgs e)
    {
        FormMultiView.ActiveViewIndex = 2;
        var lblMsg = (Label)Parent.FindControl("lblMsg");
        lblMsg.Text = "Execution Criteria";
    }

    protected void PreviousBtnt_Click(object sender, EventArgs e)
    {
        FormMultiView.ActiveViewIndex = 0;
        var lblMsg = (Label)Parent.FindControl("lblMsg");
        lblMsg.Text = "Request Change Form";
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        //Update code should come here.
        Update();

        FormMultiView.ActiveViewIndex = 1;
        var lblMsg = (Label)Parent.FindControl("lblMsg");
        lblMsg.Text = "Personnel Information";
    }

    private void Update()
    {
        try
        {
            aRequest oRequest = new aRequest();
            RequestHelper oRequestHelper = new RequestHelper();

            long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            aRequest xRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);

            //The Request number should change if Plan type is changed
            if (int.Parse(ddlPlanType.SelectedValue) != xRequest.m_iPlanTypeId) sRequestNumber = oRequestHelper.GenerateRequestNumber(ddlPlanType);
            else sRequestNumber = requestNumberLabel.Text;

            oRequest.lRequestId = lRequestId;
            oRequest.m_sRequestNumber = sRequestNumber;
            oRequest.m_iAreaId = int.Parse(areaList.SelectedValue);
            oRequest.m_iChangeId = int.Parse(changeTypeList.SelectedValue);
            oRequest.m_iOuId = int.Parse(ddlOU.SelectedValue);
            oRequest.m_iLocationId = int.Parse(ddlLocation.SelectedValue);
            oRequest.m_iPlanTypeId = int.Parse(ddlPlanType.SelectedValue);
            oRequest.m_sBenefit = txtBenefit.Text;
            oRequest.m_sORIGINALFROM = dtDateOriginalFrom.SelectedDate; //dtDateOriginalFrom.dtSelectedDate;
            oRequest.m_sORIGINALTO = dtDateOriginalTo.SelectedDate; //.dtSelectedDate;
            oRequest.m_sPLANISSUE = "";
            oRequest.m_sPRIMAVERA_ACTIVITYID = "";
            oRequest.m_sProjectActivity = txtProjectActivity.Text;
            oRequest.m_sProposal = txtProposal.Text;
            oRequest.m_sNCOST = txtNewCost.Text;
            oRequest.m_sNVOL = txtNewVOL.Text;
            oRequest.m_sNVOLGAS = txtNewVOLgas.Text;
            oRequest.m_sPVOL = txtPlanVOL.Text;
            oRequest.m_sPVOLGAS = txtPlanVOLgas.Text;
            oRequest.m_sPCOST = txtPlanCost.Text;
            oRequest.m_sREQUESTEDFROM = dtDateScheduleFrom.SelectedDate;
            oRequest.m_sREQUESTEDTO = dtDateScheduleTo.SelectedDate;
            oRequest.m_sRisk = txtRisk.Text;
            oRequest.m_iUserId = oSessnx.getOnlineUser.m_iUserId;
            oRequest.m_sWO_NO = txtWONO.Text;
            oRequest.m_iRefPlanId = int.Parse(ddlRefPlan.SelectedValue);

            //These two lines are not required by SPDC IAP Process only for SNEPCO Process
            oRequest.m_sGH20 = txtGH20.Text;
            oRequest.m_sLH20 = txtLH20.Text;

            //bool bRet = oRequestHelper.UpdateRequest(oRequest);
            //if (bRet) SendToImpactedParties(oRequest);


            //Check if an Impacted/Supporting party was selected
            if (IsImpactedPartySelected())
            {
                bool bRet = oRequestHelper.UpdateRequest(oRequest);
                if (bRet) SendToImpactedParties(oRequest);
            }
            else ajaxWebExtension.showJscriptAlertCx(Page, this, "Please, select Impacted parties!!!");
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
       Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
    }

    protected void Previous2Btn_Click(object sender, EventArgs e)
    {
        FormMultiView.ActiveViewIndex = 1;
        var lblMsg = (Label)Parent.FindControl("lblMsg");
        lblMsg.Text = "Personnel Information";
    }



    #region ===================================== Load Request =====================================

    private void InitialiseControls(long lRequestId)
    {
        List<OU> OUS = RequestFormRequirement.getOU();
        foreach (OU ou in OUS)
        {
            ddlOU.Items.Add(new ListItem(ou.m_sOU, ou.m_iIDOU.ToString()));
        }

        GetIAPPlanners();

        //List<Users> IAPPlanners = UsersActions.getIAPPlanners();
        //foreach (Users _user in IAPPlanners)
        //{
        //    ddlPlanner.Items.Add(new ListItem(_user.m_sFullName, _user.m_iUserId.ToString()));
        //}

        //Load Functional Authorisers
        List<Users> oFunctionalAuthorisers = UsersActions.lstGetUsersByRoleId((int)appUsersRoles.userRole.FunctionalAuthorizer);     //Functional Authorizers();
        foreach (Users _user in oFunctionalAuthorisers)
        {
            ddlFunctionalAuthoriser.Items.Add(new ListItem(_user.m_sFullName, _user.m_iUserId.ToString()));
        }

        List<Area> Areas = RequestFormRequirement.getAreas();
        foreach (Area area in Areas)
        {
            areaList.Items.Add(new ListItem(area.m_sAREA, area.m_iIDAREA.ToString()));
        }

        //Load Locations
        ddlLocation.Items.Clear();
        List<Location> Locations = RequestFormRequirement.getLocations();
        foreach (Location locatn in Locations)
        {
            ddlLocation.Items.Add(new ListItem(locatn.m_sLOCATION, locatn.m_iLOCATIONID.ToString()));
        }

        //Load Change Types
        List<ChangeType> ChangeTypes = RequestFormRequirement.getChangeTypes();
        foreach (ChangeType changeType in ChangeTypes)
        {
            changeTypeList.Items.Add(new ListItem(changeType.m_sTYPE, changeType.m_iIDCHANGE.ToString()));
        }

        //Load Plan Types
        List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
        foreach (PlanType planType in PlanTypes)
        {
            ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
        }

        //PopulateTreeViewControl();

        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);
        //Load Requests
        LoadRequest(oRequest);

        //Load Reference Plan
        RequestHelper.ReferencePlan(ddlPlanner, ddlRefPlan);
        ddlRefPlan.SelectedValue = oRequest.m_iRefPlanId.ToString();

        personnelInformationList1.Init_Page();
        personnelInformationList1.initiateActivityId(lRequestId);
        personnelInformationList1.Retrieve_Data(lRequestId);

        ///-=============
        
        locFieldLocations1.initiateRequestId(lRequestId);
        locFieldLocations1.Retrieve_Data(lRequestId);
        locFieldLocations1.MySaveButton.Visible = false;
        locFieldLocations1.MyUpdateButton.Visible = false;

        var oReadiness = LocationFieldActivityInfo.lstGetLocationFieldByRequestId(lRequestId);
        if (oReadiness.Count == 0)
        {
            locFieldLocations1.MySaveButton.Visible = true;
            locFieldLocations1.MyUpdateButton.Visible = false;
        }
        else
        {
            locFieldLocations1.MySaveButton.Visible = false;
            locFieldLocations1.MyUpdateButton.Visible = true;
        }
    }

    private void LoadRequest(aRequest oRequest)
    {
        requestNumberLabel.Text = oRequest.m_sRequestNumber;
        ddlOU.SelectedValue = oRequest.m_iOuId.ToString();
        areaList.SelectedValue = oRequest.m_iAreaId.ToString();
        ddlLocation.SelectedValue = oRequest.m_iLocationId.ToString();
        ddlPlanType.SelectedValue = oRequest.m_iPlanTypeId.ToString();
        txtProjectActivity.Text = oRequest.m_sProjectActivity;
        //txtPlanIssue.Text = MyRequest.m_sPLANISSUE;
        changeTypeList.SelectedValue = oRequest.m_iChangeId.ToString();
        txtWONO.Text = oRequest.m_sWO_NO;

        //Get the Funtional Authoriser from the approvals comment table
        ApproversComment oCommentFuntionalAuthoriser = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.FunctionalAuthorizer, oRequest.lRequestId);
        ddlFunctionalAuthoriser.SelectedValue = oCommentFuntionalAuthoriser.m_iUserId.ToString();

        //Get the planner from the approvals comment table
        ApproversComment oCommentPlanner = ApprovalWorkFlow.objGetApproversComments((int)appUsersRoles.userRole.IAPPlanner, oRequest.lRequestId);
        ddlPlanner.SelectedValue = oCommentPlanner.m_iUserId.ToString();
        ddlFunctionalAuthoriser.SelectedValue = oRequest.m_iAuthoriserId.ToString();

        //ddlFinance.SelectedValue = oRequest.m_iFinanceId.ToString();

        txtProposal.Text = oRequest.m_sProposal;
        txtBenefit.Text = oRequest.m_sBenefit;
        txtRisk.Text = oRequest.m_sRisk;
        txtPlanVOL.Text = oRequest.m_sPVOL;
        txtNewVOL.Text = oRequest.m_sNVOL;
        txtPlanCost.Text = oRequest.m_sPCOST;
        txtNewCost.Text = oRequest.m_sNCOST;
        txtPlanVOLgas.Text = oRequest.m_sPVOLGAS;
        txtNewVOLgas.Text = oRequest.m_sNVOLGAS;
        dtDateOriginalFrom.SelectedDate = oRequest.m_sORIGINALFROM;
        dtDateOriginalTo.SelectedDate = oRequest.m_sORIGINALTO;
        dtDateScheduleFrom.SelectedDate = oRequest.m_sREQUESTEDFROM;
        dtDateScheduleTo.SelectedDate = oRequest.m_sREQUESTEDTO;

        //These two lines are nor required by SPDC IAP Process only for SNEPCO Process
        txtGH20.Text = oRequest.m_sGH20;
        txtLH20.Text = oRequest.m_sLH20;

        RequestHelper.ReferencePlan(ddlPlanner, ddlRefPlan);
        ddlRefPlan.SelectedValue = oRequest.m_iRefPlanId.ToString();
    }

    #endregion
}