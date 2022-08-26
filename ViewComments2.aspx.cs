using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewComments2 : aspnetPage
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["RequestId"]))
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"]);
            oImpactedPartiesComments1.GetImpactecdPartiesComments(lRequestId);

            int iOu = RequestHelper.objGetRequestByRequestId(lRequestId, sfield).m_iOuId;
            if (iOu == (int)appOUs.ous.SNEPCO)
            {
                Snepco(lRequestId);
                DivSpdc.Visible = false;
            }
            else
            {
                SPDC(lRequestId);
                DivSnepco.Visible = false;
            }            
        }
    }

    private void SPDC(long lRequestId)
    {
        if (Request.QueryString["RequestId"] != null)
        {
            RequestInformation1.LoadRequest(lRequestId);

            IAPPlannerCommentSpdc.GetApproversComments(lRequestId);

            //IAPPlannerCommentSpdc.getIAPPlannerComment((int)appUsersRoles.userRole.IAPPlanner, lRequestId);
            //FunctionalRepresentativesCommentsSpdc.getFunctionalRepresentativesComments((int)appUsersRoles.userRole.FunctionalAuthorizer, lRequestId);
            //ProductionRepresentativeCommentSpdc.getProductionRepsComment((int)appUsersRoles.userRole.ProductionAssetAuthorizer, lRequestId);
            //ChangeReviewBoardCommentSpdc.getChangeReviewBoardChairComment((int)appUsersRoles.userRole.ChangeReviewBoardChairman, lRequestId);

            personnelInformationListSpdc.Init_Page();
            personnelInformationListSpdc.Retrieve_Data(lRequestId);
            personnelInformationListSpdc.AddLinkButton.Visible = false;

            //Hide the Edit and Delete buttons
            int iColumns = personnelInformationListSpdc.GrdView.Columns.Count;
            personnelInformationListSpdc.GrdView.Columns[iColumns - (iColumns - 1)].Visible = false;
            personnelInformationListSpdc.GrdView.Columns[iColumns - 1].Visible = false;

            //locFieldLocations
            locFieldLocationsSpdc.MySaveButton.Visible = false;
            locFieldLocationsSpdc.MyUpdateButton.Visible = false;
            locFieldLocationsSpdc.Retrieve_Data(lRequestId);

            locFieldLocationsSpdc.DisableDropDowns();
        }
    }
    private void Snepco(long lRequestId)
    {
        RequestInformation1.LoadRequest(lRequestId);

        IAPPlannerComment1.GetApproversComments(lRequestId);

        //IAPPlannerComment1.getIAPPlannerComment((int)appUsersRoles.userRole.IAPPlanner, lRequestId);
        //FunctionalRepresentativesComments1.getFunctionalRepresentativesComments((int)appUsersRoles.userRole.FunctionalAuthorizer, lRequestId);
        //ChangeReviewBoardComment1.getChangeReviewBoardChairComment((int)appUsersRoles.userRole.ChangeReviewBoardChairman, lRequestId);

        personnelInformationList.Init_Page();
        personnelInformationList.Retrieve_Data(lRequestId);
        personnelInformationList.AddLinkButton.Visible = false;

        //Hide the Edit and Delete buttons
        int iColumns = personnelInformationList.GrdView.Columns.Count;
        personnelInformationList.GrdView.Columns[iColumns - (iColumns - 1)].Visible = false;
        personnelInformationList.GrdView.Columns[iColumns - 1].Visible = false;

        //locFieldLocations
        locFieldLocations.MySaveButton.Visible = false;
        locFieldLocations.MyUpdateButton.Visible = false;
        locFieldLocations.Retrieve_Data(lRequestId);

        locFieldLocations.DisableDropDowns();
    }
}