using System;

public partial class RequestList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            oPendingRequests1.Requests_Pending_My_Approval((int) commonEnums.RequestorType.Approver);
            oApprovedRequests1.Requests_I_Approved((int) commonEnums.RequestorType.Approver);
            oRejectedRequests1.Requests_I_Rejected((int) commonEnums.RequestorType.Approver);
        }
    }
}