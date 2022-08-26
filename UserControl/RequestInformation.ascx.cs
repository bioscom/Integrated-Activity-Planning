using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_RequestInformation : System.Web.UI.UserControl
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadRequest(long lRequestId)
    {
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);

        requestNumberLabel.Text = oRequest.m_sRequestNumber;
        OULabel.Text = oRequest.m_sOU;
        locationLabel.Text = oRequest.m_sLOCATION;
        projectActivityLabel.Text = oRequest.m_sProjectActivity;
        areaLabel.Text = oRequest.m_sAREA;
        planTypeLabel.Text = oRequest.m_sPLANTYPE;
        planIssueLabel.Text = oRequest.m_sPLANISSUE;
        changeTypeLabel.Text = oRequest.m_sTYPE;
        WONOLabel.Text = oRequest.m_sWO_NO;

        originatorLabel.Text = UsersActions.objGetIapOwnerByUserId(oRequest.m_iUserId).m_sFullName;
        proposalLabel.Text = oRequest.m_sProposal;
        PrimaveraIDLabel.Text = oRequest.m_sPRIMAVERA_ACTIVITYID;
        //impactLabel.Text = ThisRequest.m_sIMPACT_ANALYSIS;

        requestDateLabel.Text = dateRoutine.dateStandard(oRequest.m_sREQUESTDATE);
        requestTimeLabel.Text = dateRoutine.dateStandard(oRequest.m_sREQUESTTIME);

        benefitLabel.Text = oRequest.m_sBenefit;
        riskLabel.Text = oRequest.m_sRisk;

        planVOLLabel.Text = oRequest.m_sPVOL;
        NewVOLLabel.Text = oRequest.m_sNVOL;

        planVOLGASLabel.Text = oRequest.m_sPVOLGAS;
        NewVOLGASLabel.Text = oRequest.m_sNVOLGAS;

        planCostLabel.Text = oRequest.m_sPCOST;
        NewCostLabel.Text = oRequest.m_sNCOST;

        originalFrom.Text =  oRequest.m_sORIGINALFROM.ToString();
        originalTo.Text = oRequest.m_sORIGINALTO.ToString();
        requestFrom.Text = oRequest.m_sREQUESTEDFROM.ToString();
        requestTo.Text = oRequest.m_sREQUESTEDTO.ToString();
    }
}
