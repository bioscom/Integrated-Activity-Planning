using System;

public partial class MyChangeRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            oPendingRequests1.My_Pending_Requests((int) commonEnums.RequestorType.Owner);
            oApprovedRequests1.My_Approved_Requests((int) commonEnums.RequestorType.Owner);
            oRejectedRequests1.My_Requests_Rejected((int) commonEnums.RequestorType.Owner);
        }
    }
}
