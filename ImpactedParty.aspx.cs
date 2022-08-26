using System;

public partial class ImpactedParty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        oIPPendingRequests1.Requests_Pending_My_Approval();
        oIPApprovedRequests1.Requests_I_Approved();
    }
}