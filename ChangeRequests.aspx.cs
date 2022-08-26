using System;

public partial class ChangeRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            oPendingRequests1.Pending_Requests((int) commonEnums.RequestorType.AllRequests);
            oApprovedRequests1.Approved_Requests((int) commonEnums.RequestorType.AllRequests);
            oRejectedRequests1.Rejected_Requests((int) commonEnums.RequestorType.AllRequests);
        }
    }
}
