using System;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for StatusReporter
/// </summary>
public class StatusReporter
{
    public StatusReporter()
    {
        //
        // 
        //
    }

    public static void ExtStatusReporter(GridView theGridView, DataTable dt, string sortExpression)
    {
        //bool deletePrivilege = UsersActions.DeletePrivilege(activityOwnerInfo);

        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = sortExpression;

            theGridView.DataSource = dv;
            theGridView.DataBind();

            ApproversComment oComment = new ApproversComment();
            try
            {
                foreach (GridViewRow grdRow in theGridView.Rows)
                {
                    LinkButton CancelCTR = (LinkButton)theGridView.Rows[grdRow.RowIndex].FindControl("CancelLinkButton");
                    LinkButton DeleteCTR = (LinkButton)theGridView.Rows[grdRow.RowIndex].FindControl("DeleteLinkButton");
                    Label lbCTRTitle = (Label)grdRow.FindControl("labelCTRTitle");

                    Label Status = (Label)grdRow.FindControl("labelStatus");

                    LinkButton lbREQUESTNUMBER = (LinkButton)grdRow.FindControl("ViewStatusLinkButton");
                    LinkButton lbSTATUS = (LinkButton)grdRow.FindControl("ViewStatusLinkButton");
                    LinkButton lbReroute = (LinkButton)grdRow.FindControl("RerouteLinkButton");

                    Status.Text = "";
                    int STATUS = Convert.ToInt32(lbSTATUS.Attributes["STATUS"]);
                    long lRequestId = long.Parse(lbSTATUS.Attributes["IDREQUEST"].ToString());
                    Status.Font.Bold = true;

                    if (lbReroute != null)
                    {
                        if (STATUS == RequestStatus.m_iApproved)
                        {
                            lbReroute.Enabled = false;
                        }
                    }                    

                    if (STATUS == RequestStatus.m_iActivityOwner)
                    {
                        Status.ForeColor = System.Drawing.Color.Red; 
                        Status.Text = RequestStatus.m_sActivityOwner;
                       
                    }
                    else if (STATUS == RequestStatus.m_iIAPPlanner)
                    {
                        Status.ForeColor = System.Drawing.Color.Orange;
                        Status.Text = RequestStatus.m_sIAPPlanner;
                        
                    }
                    else if (STATUS == RequestStatus.m_iFunctionalRepresentatives)
                    {
                        Status.ForeColor = System.Drawing.Color.Brown;
                        Status.Text = RequestStatus.m_sFunctionalRepresentatives;
                       
                    }
                    else if (STATUS == RequestStatus.m_iProductionAssetRepresentative)
                    {
                        Status.ForeColor = System.Drawing.Color.Navy;
                        Status.Text = RequestStatus.m_sProductionAssetRepresentative;
                    }
                    else if (STATUS == RequestStatus.m_iChangeReviewBoardChairman)
                    {
                        Status.ForeColor = System.Drawing.Color.Teal; 
                        Status.Text = RequestStatus.m_sChangeReviewBoardChairman;
                    }
                    else if (STATUS == RequestStatus.m_iApproved)
                    {
                        Status.ForeColor = System.Drawing.Color.Green;
                        Status.Text = RequestStatus.m_sApproved;
                    }
                    else if (STATUS == RequestStatus.m_iRestored)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sRestored;
                    }
                    else if (STATUS == RequestStatus.m_iCancelled)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sCancelled;
                    }
                    else if (STATUS == RequestStatus.m_iNotApprovedByChairmanReviewBoard)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sNotApprovedByChairmanReviewBoard;
                    }
                    else if (STATUS == RequestStatus.m_iNotApprovedByFunctionalAuthoriser)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sNotApprovedByFunctionalAuthoriser;
                    }
                    else if (STATUS == RequestStatus.m_iNotApprovedByPlanner)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sNotApprovedByPlanner;
                    }
                    else if (STATUS == RequestStatus.m_iNotApprovedByProductionAssetAuthorizer)
                    {
                        Status.ForeColor = System.Drawing.Color.Red;
                        Status.Text = RequestStatus.m_sNotApprovedByProductionAssetAuthorizer;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
        }
    }
}