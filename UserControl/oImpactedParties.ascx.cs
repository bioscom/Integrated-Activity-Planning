using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_oImpactedParties : aspnetUserControl
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RequestId"] != null)
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"]);
            RequestInformation1.LoadRequest(lRequestId);

            PopulateProductionTreeViewControl();
            LoadImpactedparties(lRequestId);
        }
    }

    private void PopulateProductionTreeViewControl()
    {
        try
        {
            TreeNode parentNode = null;
            List<Area> lstAreas = RequestFormRequirement.getAreas();
            foreach (Area oArea in lstAreas)
            {
                if (oArea.m_sAREA.ToUpper() != "N/A".ToUpper())
                {
                    parentNode = new TreeNode(oArea.m_sAREA, oArea.m_iIDAREA.ToString());

                    //List<Location> lstLocations = RequestFormRequirement.getLocationsByArea(oArea.m_iIDAREA);
                    List<Users> lstResponsibleParties = UsersActions.LstLocationResponsiblePartysByHub(oArea.m_iIDAREA);
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

    private void LoadImpactedparties(long lRequestId)
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
                    //TreeView1.Nodes.Add(parentNode);
                }
            }
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }


    protected void mnuTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {
        //if (mnuTreeView.SelectedNode.Depth == 0)
        //{
        //    lblBusinessUnit.Text = mnuTreeView.SelectedNode.Text;
        //    pnlDepartment.Visible = true;
        //    pnlTeam.Visible = false;
        //}
        //else if (mnuTreeView.SelectedNode.Depth == 1)
        //{
        //    lblDepartment.Text = mnuTreeView.SelectedNode.Text;
        //    pnlTeam.Visible = true;
        //    pnlDepartment.Visible = false;
        //}

        //ResponseHelper.Redirect(mnuTreeView.SelectedNode.Value, "_blank", "menubar=1,width=700,height=500");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        long lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);

        try
        {
            List<structUserMailIdx> eTos = new List<structUserMailIdx>();
            foreach (TreeNode n in mnuTreeView.Nodes)
            {
                foreach (TreeNode cn in n.ChildNodes)
                {
                    if (cn.Checked)
                    {
                        ApprovalWorkFlow.ForwardsRequestToImpactedParties(lRequestId, int.Parse(cn.Value));//Assign request to selected impacted/supporting parties
                        eTos.Add(UsersActions.objGetUserByUserId(int.Parse(cn.Value)).structUserIdx); // Collect their email address, to forward them notification emails

                        ////before assign request, check if the cn.Value already exists
                        //List<ApproversComment> oComments = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(lRequestId);
                        //ApproversComment oT = oComments.Find(c => c.m_iUserId == int.Parse(cn.Value));
                        //if (oT == null)
                        //{
                        //    ApprovalWorkFlow.ForwardsRequestToImpactedParties(lRequestId, int.Parse(cn.Value));//Assign request to selected impacted/supporting parties
                        //    eTos.Add(UsersActions.objGetUserByUserId(int.Parse(cn.Value)).structUserIdx); // Collect their email address, to forward them notification emails
                        //}
                    }
                }
            }

            List<structUserMailIdx> cc = new List<structUserMailIdx>();
            cc.Add(UsersActions.objGetUserByUserId(oRequest.m_iUserId).structUserIdx);

            SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);
            o.NotifyImpactedPaties(eTos, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);

            ajaxWebExtension.showJscriptAlert(Page, this, "Change Request sent to all selected Impacted/Supporting Parties.");
        }
        catch(Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }        
    }
}