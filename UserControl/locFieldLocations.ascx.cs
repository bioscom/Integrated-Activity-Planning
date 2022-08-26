using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_fieldLocations : aspnetUserControl
{
    string sfield = "REQUEST_NUMBER";

    public void Page_Init()
    {
        grdView.DataSource = IAPPEC.dtGetPEC(); 
        grdView.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public HiddenField RequestId
    {
        get
        {
            return RequestIDHF;
        }
    }
    public GridView GrdView
    {
        get
        {
            return grdView;
        }
    }

    public void initiateRequestId(long lRequestId)
    {
        RequestIDHF.Value = lRequestId.ToString();
    }

    public void DisableDropDowns()
    {
        foreach (GridViewRow grdRows in GrdView.Rows)
        {
            ASP.usercontrol_statusselectorcontrol_ascx editLink = (ASP.usercontrol_statusselectorcontrol_ascx)grdRows.FindControl("statusSelectorControl1");
            editLink.ThisRadioButtonList.Enabled = false;
        }
    }

    public void Retrieve_Data(long lRequestId)
    {
        RequestIDHF.Value = lRequestId.ToString();
        List<LocationFieldActivityInfo> MyLocationFieldActivities = LocationFieldActivityInfo.lstGetLocationFieldByRequestId(lRequestId);

        foreach (LocationFieldActivityInfo MyLocationFieldActivity in MyLocationFieldActivities)
        {
            foreach (GridViewRow grdRow in grdView.Rows)
            {
                Label lbFieldLocation = (Label)grdRow.FindControl("lblFieldLocation");
                int iIdPec = int.Parse(lbFieldLocation.Attributes["IDPEC"].ToString());

                ASP.usercontrol_statusselectorcontrol_ascx MyStatus = (ASP.usercontrol_statusselectorcontrol_ascx)grdRow.FindControl("statusSelectorControl1");

                if (iIdPec == MyLocationFieldActivity.m_iIDPEC)
                {
                    MyStatus.rdbSelectedValue(MyLocationFieldActivity.m_iSTATUS);
                }
            }
        }
    }


    //Expose control to the outside world
    public Button MySaveButton
    {
        get
        {
            return btnSave;
        }
    }
    public Button MyUpdateButton
    {
        get
        {
            return updateButton;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool success = false;
        try
        {                
            long lRequestId = long.Parse(RequestIDHF.Value);
            foreach (GridViewRow grdRow in grdView.Rows)
            {
                Label lbFieldLocation = (Label)grdRow.FindControl("lblFieldLocation");
                int iIdPec = int.Parse(lbFieldLocation.Attributes["IDPEC"].ToString());

                ASP.usercontrol_statusselectorcontrol_ascx MyStatus = (ASP.usercontrol_statusselectorcontrol_ascx)grdRow.FindControl("statusSelectorControl1");
                success = LocationFieldActivityInfo.createRequestPecStatus(iIdPec, lRequestId, int.Parse(MyStatus.SelectedStatus2));
            }

            if (success)
            {
                btnSave.Visible = false;
                updateButton.Visible = true;

                ForwardRequestToFunctionalAuthoriser(lRequestId);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void ForwardRequestToFunctionalAuthoriser(long lRequestId)
    {
        //Forward a mail to the selected Functional Authority
        var oMail = new SendMail(oSessnx.getOnlineUser.structUserIdx);

        aRequest o = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);
        //ApproversComment oFunctionalAuthorizerComment = ApprovalWorkFlow.objGetApproversComments((int) appUsersRoles.userRole.FunctionalAuthorizer, lRequestId);
        Users oFunctionalAuthoriser = UsersActions.objGetUserByUserId(o.m_iAuthoriserId);
        
        //Note: Functional Authorizer mails IAP Planner
        oMail.ActivityOwnerMailsIAPPlanner(oFunctionalAuthoriser.structUserIdx, oSessnx.getOnlineUser.m_sFullName, o.m_sRequestNumber, o.m_sProjectActivity, Utilities.AppURL());

        string message = "IAP request form successfully submitted and notification mail sent to " + oFunctionalAuthoriser.m_sFullName + " Functional Authoriser.";
        ajaxWebExtension.showJscriptAlert(Page, this, message);
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        
        bool success = false;
        try
        {
            long lRequestId = long.Parse(RequestIDHF.Value);

            foreach (GridViewRow grdRow in grdView.Rows)
            {
                Label lbFieldLocation = (Label)grdRow.FindControl("lblFieldLocation");
                int iIdPec = int.Parse(lbFieldLocation.Attributes["IDPEC"].ToString());

                ASP.usercontrol_statusselectorcontrol_ascx MyStatus = (ASP.usercontrol_statusselectorcontrol_ascx)grdRow.FindControl("statusSelectorControl1");
                success = LocationFieldActivityInfo.updateRequestPecStatus(iIdPec, lRequestId, int.Parse(MyStatus.SelectedStatus2));
            }

            if (success)
            {
                btnSave.Visible = false;
                updateButton.Visible = true;

                string Message = "Plan Execution Criteria successfully updated.";
                ajaxWebExtension.showJscriptAlert(Page, this, Message);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}
