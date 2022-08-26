using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImpactedPartySupport : aspnetPage
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RequestId"] != null)
        {
            long lRequestId = long.Parse(Request.QueryString["RequestId"]);
            RequestInformation1.LoadRequest(lRequestId);
        }
    }

    protected void forwardButton_Click(object sender, EventArgs e)
    {
        long lRequestId = long.Parse(Request.QueryString["RequestId"]);
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);

        bool bRet = ApprovalWorkFlow.IPRequestReviewProcess(txtComment.Text, oRequest.lRequestId, oSessnx.getOnlineUser.m_iUserId);

        if (bRet)
        {
            //Send mail to the IAP Planner and copy the Activity Owner
            SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);
            structUserMailIdx eTo = UsersActions.objGetUserByUserId(oRequest.m_iPlannerId).structUserIdx; //Send mail to Planner
            structUserMailIdx eCC = UsersActions.objGetUserByUserId(oRequest.m_iUserId).structUserIdx; //Copy Request Owner

            var supporters = ApprovalWorkFlow.lstGetImpactedPartyCommentsByRequestId(lRequestId);
            bRet = supporters.Any(c => c.m_iStatus != RequestStatus.m_iDefault);

            if (bRet) o.IPReviewdChangeRequest(eTo, eCC, oSessnx.getOnlineUser.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL());
            else o.AllIPReviewdChangeRequest(eTo, eCC, oSessnx.getOnlineUser.m_sFullName, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL());

            if (bRet) ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
            else ajaxWebExtension.showJscriptAlertCx(Page, this, "Not Successful, try again later!!!");
        }

    }
}