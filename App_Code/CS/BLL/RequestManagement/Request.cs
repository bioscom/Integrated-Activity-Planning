using System.Data;
using System;

/// <summary>
/// Summary description for Request
/// </summary>
//public class Request
//{
//    public Request()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

public class aRequest
{
    public long lRequestId { get; set; }
    public int m_iOuId { get; set; }
    public int m_iAreaId { get; set; }
    public int m_iChangeId { get; set; }
    public int m_iLocationId { get; set; }
    public int m_iPlanTypeId { get; set; }
    public int m_iUserId { get; set; }

    public string m_sOU { get; set; }
    public string m_sAREA { get; set; }
    public string m_sTYPE { get; set; }
    public string m_sLOCATION { get; set; }
    public string m_sPLANTYPE { get; set; }
    public string m_sRequestNumber { get; set; }
    public string m_sProjectActivity { get; set; }
    public string m_sPLANISSUE { get; set; }
    public string m_sWO_NO { get; set; }
    public string m_sORIGINATOR { get; set; }
    public string m_sProposal { get; set; }
    public string m_sBenefit { get; set; }
    public string m_sRisk { get; set; }
    public string m_sPVOL { get; set; }
    public string m_sNVOL { get; set; }
    public string m_sPVOLGAS { get; set; }
    public string m_sNVOLGAS { get; set; }
    public string m_sPCOST { get; set; }
    public string m_sNCOST { get; set; }
    public string m_sGH20 { get; set; }
    public string m_sLH20 { get; set; }
    public DateTime m_sREQUESTDATE { get; set; }
    public string m_sPRIMAVERA_ACTIVITYID { get; set; }
    //public string m_sIMPACT_ANALYSIS { get; set; } 
    //public DateTime m_sORIGINALFROM { get; set; }
    //public DateTime m_sORIGINALTO { get; set; }
    //public DateTime m_sREQUESTEDFROM { get; set; }
    //public DateTime m_sREQUESTEDTO { get; set; }

    public DateTime? m_sORIGINALFROM { get; set; }
    public DateTime? m_sORIGINALTO { get; set; }
    public DateTime? m_sREQUESTEDFROM { get; set; }
    public DateTime? m_sREQUESTEDTO { get; set; }

    public int m_iRefPlanId { get; set; }
    public int m_iStatus { get; set; }
    public DateTime m_sREQUESTTIME { get; set; }

    public int m_iPlannerId { get; set; }
    public int m_iFinanceId { get; set; }
    public int m_iAuthoriserId { get; set; }
    public int m_iAssetAuthoriserId { get; set; }
    public int m_iDrbChairId { get; set; }


    public aRequest()
    {

    }

    public aRequest(DataRow dr)
    {
        lRequestId = long.Parse(dr["IDREQUEST"].ToString());
        m_iUserId = int.Parse(dr["IDUSER"].ToString());

        m_iOuId = int.Parse(dr["IDOU"].ToString());
        m_iAreaId = int.Parse(dr["IDAREA"].ToString());
        m_iChangeId = int.Parse(dr["IDCHANGE"].ToString());
        m_iLocationId = int.Parse(dr["LOCATIONID"].ToString());
        m_iPlanTypeId = int.Parse(dr["PLANTYPEID"].ToString());

        m_sOU = dr["OU"].ToString();
        m_sAREA = dr["AREA"].ToString();
        m_sTYPE = dr["TYPE"].ToString();
        m_sLOCATION = dr["LOCATION"].ToString();
        m_sPLANTYPE = dr["PLANTYPE"].ToString();

        m_sRequestNumber = dr["REQUEST_NUMBER"].ToString();
        m_sPLANISSUE = dr["PLANISSUE"].ToString();
        m_sREQUESTDATE = DateTime.Parse(dr["REQUESTDATE"].ToString());
        m_sProjectActivity = dr["PROJECT_ACTIVITY"].ToString();
        m_sWO_NO = dr["WO_NO"].ToString();
        m_sORIGINATOR = dr["ORIGINATOR"].ToString();
        m_sProposal = dr["PROPOSAL"].ToString();
        m_sBenefit = dr["BENEFIT"].ToString();
        m_sRisk = dr["RISK"].ToString();
        m_sPVOL = dr["PVOL"].ToString();
        m_sNVOL = dr["NVOL"].ToString();
        m_sPVOLGAS = dr["PVOLGAS"].ToString();
        m_sNVOLGAS = dr["NVOLGAS"].ToString();
        m_sPCOST = dr["PCOST"].ToString();
        m_sNCOST = dr["NCOST"].ToString();
        m_sPRIMAVERA_ACTIVITYID = dr["PRIMAVERA_ACTIVITYID"].ToString();
        //m_sIMPACT_ANALYSIS = dr["IMPACT_ANALYSIS"].ToString();
        m_sORIGINALFROM = DateTime.Parse(dr["ORIGINALFROM"].ToString());
        m_sORIGINALTO = DateTime.Parse(dr["ORIGINALTO"].ToString());
        m_sREQUESTEDFROM = DateTime.Parse(dr["REQUESTEDFROM"].ToString());
        m_sREQUESTEDTO = DateTime.Parse(dr["REQUESTEDTO"].ToString());
        m_iRefPlanId = int.Parse(dr["IDREFPLAN"].ToString());
        m_iStatus = int.Parse(dr["STATUS"].ToString());
        m_sREQUESTTIME = string.IsNullOrEmpty(dr["REQUESTTIME"].ToString()) ? DateTime.Now.Date : DateTime.Parse(dr["REQUESTTIME"].ToString());

        m_sGH20 = dr["GH20"].ToString();
        m_sLH20 = dr["LH20"].ToString();

        m_iAuthoriserId = !string.IsNullOrEmpty(dr["AUTHORISERID"].ToString()) ? int.Parse(dr["AUTHORISERID"].ToString()) : 0;
        m_iPlannerId = !string.IsNullOrEmpty(dr["PLANNERID"].ToString()) ? int.Parse(dr["PLANNERID"].ToString()) : 0;

        m_iFinanceId = !string.IsNullOrEmpty(dr["FINANCEID"].ToString()) ? int.Parse(dr["FINANCEID"].ToString()) : 0;
        m_iAssetAuthoriserId = !string.IsNullOrEmpty(dr["ASSETAUTHORISERID"].ToString()) ? int.Parse(dr["ASSETAUTHORISERID"].ToString()) : 0;
        m_iDrbChairId = !string.IsNullOrEmpty(dr["DRBCHAIRID"].ToString()) ? int.Parse(dr["DRBCHAIRID"].ToString()) : 0;

        //sql += "OWNER.IDUSER, OWNER.FULLNAME, OWNER.ROLEID, ";
        //sql += "PLANNER.IDUSER PLANNERID, PLANNER.FULLNAME PLANNERNAME, PLANNER.ROLEID PLANNERROLE, ";
        //sql += "FINANCE.IDUSER FINANCEID, FINANCE.FULLNAME FINANCENAME, FINANCE.ROLEID FINANCEROLE, ";
        //sql += "AUTHORISER.IDUSER AUTHORISERID, AUTHORISER.FULLNAME AUTHORISERNAME, AUTHORISER.ROLEID AUTHORISERROLE, ";
    }
}
