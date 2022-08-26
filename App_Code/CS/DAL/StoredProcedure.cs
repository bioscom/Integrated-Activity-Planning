using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StoredProcedure
/// </summary>
public static class StoredProcedure
{
    static StoredProcedure()
    {

    }

    public static string CreateNewRequest()
    {
        string sql = "INSERT INTO IAP_REQUESTFORM (IDREQUEST, REQUEST_NUMBER, LOCATIONID, IDCHANGE, PLANTYPEID, PROJECT_ACTIVITY, PLANISSUE, ";
        sql += "WO_NO, REQUESTDATE, PROPOSAL, BENEFIT, RISK, IDUSER, PVOL, NVOL, PCOST, NCOST, PVOLGAS, NVOLGAS, ORIGINALFROM, ORIGINALTO, ";
        sql += "REQUESTEDFROM, REQUESTEDTO, STATUS, IDREFPLAN, REQUESTTIME, GH20, LH20, PLANNERID, AUTHORISERID, PRIMAVERA_ACTIVITYID) ";
        sql += "VALUES (:lRequestId, :REQUEST_NUMBER, :LOCATIONID, :IDCHANGE, :PLANTYPEID, :PROJECT_ACTIVITY, :PLANISSUE, :WO_NO, :REQUESTDATE, ";
        sql += ":PROPOSAL, :BENEFIT, :RISK, :IDUSER, :PVOL, :NVOL, :PCOST, :NCOST, :PVOLGAS, :NVOLGAS, :ORIGINALFROM, :ORIGINALTO, :REQUESTEDFROM, ";
        sql += ":REQUESTEDTO, :STATUS, :iRefPlanId, :REQUESTTIME, :GH20, :LH20, :iPlannerId, :iAuthoriserId, :sPrimaveraId)";

        return sql;
    }

    public static string UpdateRequest()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET REQUEST_NUMBER = :REQUEST_NUMBER, LOCATIONID = :LOCATIONID, IDCHANGE = :IDCHANGE, PLANTYPEID = :PLANTYPEID, PROJECT_ACTIVITY = :PROJECT_ACTIVITY, ";
        sql += "PLANISSUE = :PLANISSUE, WO_NO = :WO_NO, PROPOSAL = :PROPOSAL, BENEFIT = :BENEFIT, RISK = :RISK, PVOL = :PVOL, NVOL = :NVOL, PCOST = :PCOST, NCOST = :NCOST, PVOLGAS = :PVOLGAS, ";
        sql += "NVOLGAS = :NVOLGAS, ORIGINALFROM = :ORIGINALFROM, ORIGINALTO = :ORIGINALTO, REQUESTEDFROM = :REQUESTEDFROM, REQUESTEDTO = :REQUESTEDTO, IDREFPLAN = :iRefPlanId, ";
        sql += "REQUESTTIME = :REQUESTTIME, GH20 = :GH20, LH20 = :LH20, PRIMAVERA_ACTIVITYID = :sPrimaveraId ";
        sql += "WHERE IDREQUEST = :lRequestId";

        return sql;
    }

    public static string AssignFinalApprovers()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET FINANCEID = :iFinanceId, ASSETAUTHORISERID = :iAssetAuthoriserId, DRBCHAIRID = :iDRBChairId WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    public static string AssignFinalApprovers1()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET FINANCEID = :iFinanceId, DRBCHAIRID = :iDRBChairId WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    public static string AssignFinalApprovers2()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET ASSETAUTHORISERID = :iAssetAuthoriserId, DRBCHAIRID = :iDRBChairId WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    public static string AssignFinalApprovers3()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET DRBCHAIRID = :iDRBChairId WHERE IDREQUEST = :lRequestId";
        return sql;
    }


    #region  ================================  Selecting Queries ================================

    public static string loadRequestsPendingMyApprovalOrIApproved(string field)
    {
        //string sql = "SELECT DISTINCT IAP_REQUESTFORM.{0} FROM IAP_COMMENTS ";

        string sql = "SELECT DISTINCT {0}, IAP_REQUESTFORM.IDREQUEST, IAP_REQUESTFORM.REQUEST_NUMBER, IAP_REQUESTFORM.PROJECT_ACTIVITY, IAP_REQUESTFORM.PLANISSUE, IAP_REQUESTFORM.WO_NO, ";
        sql += "IAP_REQUESTFORM.ORIGINATOR, IAP_REQUESTFORM.REQUESTDATE, IAP_REQUESTFORM.PROPOSAL, IAP_REQUESTFORM.BENEFIT, IAP_REQUESTFORM.RISK, IAP_REQUESTFORM.PVOL, IAP_REQUESTFORM.NVOL, ";
        sql += "IAP_REQUESTFORM.PCOST, IAP_REQUESTFORM.NCOST, IAP_REQUESTFORM.PVOLGAS, IAP_REQUESTFORM.NVOLGAS, IAP_REQUESTFORM.GH20, IAP_REQUESTFORM.LH20, IAP_REQUESTFORM.ORIGINALFROM, ";
        sql += "IAP_REQUESTFORM.ORIGINALTO, IAP_REQUESTFORM.REQUESTEDFROM, IAP_REQUESTFORM.REQUESTEDTO, IAP_REQUESTFORM.PRIMAVERA_ACTIVITYID, IAP_REQUESTFORM.STATUS, ";
        sql += "OWNER.FULLNAME, OWNER.IDUSER, OWNER.USERMAIL, IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_REQUESTFORM.REQUESTTIME, IAP_AREA.IDAREA, IAP_AREA.AREA, ";
        sql += "IAP_OU.IDOU, IAP_OU.OU, IAP_PLANTYPES.PLANTYPEID, IAP_PLANTYPES.PLANTYPE, IAP_CHANGE_TYPE.IDCHANGE, IAP_CHANGE_TYPE.TYPE, IAP_REQUESTFORM.IDREFPLAN, IAP_REQUESTFORM.CHANGETYPE ";
        sql += "FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN (IAP_REQUESTFORM INNER JOIN IAP_LOCATIONS ON IAP_REQUESTFORM.LOCATIONID = IAP_LOCATIONS.LOCATIONID ";
        sql += "INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA ";
        sql += "INNER JOIN IAP_CHANGE_TYPE ON IAP_REQUESTFORM.IDCHANGE = IAP_CHANGE_TYPE.IDCHANGE ";
        sql += "INNER JOIN IAP_PLANTYPES ON IAP_REQUESTFORM.PLANTYPEID = IAP_PLANTYPES.PLANTYPEID ";
        sql += "INNER JOIN IAP_OU ON IAP_AREA.IDOU = IAP_OU.IDOU) ON IAP_COMMENTS.IDREQUEST = IAP_REQUESTFORM.IDREQUEST ";
        sql += "INNER JOIN IAP_USERS OWNER ON IAP_REQUESTFORM.IDUSER = OWNER.IDUSER ";
        sql += "WHERE ((IAP_COMMENTS.IDUSER = :iUserId) AND (IAP_COMMENTS.STATUS = :iStatus)) AND ROUND(TO_NUMBER(MONTHS_BETWEEN(SYSDATE, IAP_REQUESTFORM.REQUESTDATE))) <= '6' ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        //sql = string.Format(sql, field);
        //return sql;
        // OR (IAP_COMMENTS.STATUS = :STATUS2)) AND (IAP_REQUESTFORM.STATUS = :REQUESTSTATUS)

        return string.Format(sql, field);
    }

    public static string getRequestMaster()
    {
        string sql = "SELECT DISTINCT {0}, IAP_REQUESTFORM.IDREQUEST, IAP_REQUESTFORM.REQUEST_NUMBER, IAP_REQUESTFORM.PROJECT_ACTIVITY, ";
        sql += "IAP_REQUESTFORM.PLANISSUE, IAP_REQUESTFORM.WO_NO, IAP_REQUESTFORM.ORIGINATOR, IAP_REQUESTFORM.REQUESTDATE, ";
        sql += "IAP_REQUESTFORM.PROPOSAL, IAP_REQUESTFORM.BENEFIT, IAP_REQUESTFORM.RISK, IAP_REQUESTFORM.PVOL, IAP_REQUESTFORM.NVOL, ";
        sql += "IAP_REQUESTFORM.PCOST, IAP_REQUESTFORM.GH20, IAP_REQUESTFORM.LH20, IAP_REQUESTFORM.NCOST, IAP_REQUESTFORM.PVOLGAS, ";
        sql += "IAP_REQUESTFORM.NVOLGAS, IAP_REQUESTFORM.ORIGINALFROM, IAP_REQUESTFORM.ORIGINALTO, IAP_REQUESTFORM.REQUESTEDFROM, ";
        sql += "IAP_REQUESTFORM.REQUESTEDTO, IAP_REQUESTFORM.PRIMAVERA_ACTIVITYID, IAP_REQUESTFORM.STATUS, IAP_REQUESTFORM.CHANGETYPE, ";
        sql += "IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_AREA.IDAREA, IAP_AREA.AREA, ";

        sql += "OWNER.IDUSER, OWNER.FULLNAME, OWNER.ROLEID, ";
        sql += "PLANNER.IDUSER PLANNERID, PLANNER.FULLNAME PLANNERNAME, PLANNER.ROLEID PLANNERROLE, ";
        sql += "FINANCE.IDUSER FINANCEID, FINANCE.FULLNAME FINANCENAME, FINANCE.ROLEID FINANCEROLE, ";
        sql += "AUTHORISER.IDUSER AUTHORISERID, AUTHORISER.FULLNAME AUTHORISERNAME, AUTHORISER.ROLEID AUTHORISERROLE, ";
        sql += "ASSETAUTHORISER.IDUSER ASSETAUTHORISERID, ASSETAUTHORISER.FULLNAME ASSETAUTHORISERNAME, ASSETAUTHORISER.ROLEID ASSETAUTHORISERROLE, ";
        sql += "DRBCHAIR.IDUSER DRBCHAIRID, DRBCHAIR.FULLNAME DRBCHAIRNAME, DRBCHAIR.ROLEID DRBCHAIRROLE, ";
        sql += "IAP_OU.IDOU, IAP_OU.OU, IAP_PLANTYPES.PLANTYPEID, IAP_PLANTYPES.PLANTYPE, IAP_CHANGE_TYPE.IDCHANGE, ";
        sql += "IAP_CHANGE_TYPE.TYPE, IAP_REQUESTFORM.IDREFPLAN, IAP_REQUESTFORM.REQUESTTIME FROM IAP_REQUESTFORM ";

        sql += "INNER JOIN IAP_LOCATIONS ON IAP_REQUESTFORM.LOCATIONID = IAP_LOCATIONS.LOCATIONID ";
        sql += "INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA ";
        sql += "INNER JOIN IAP_USERS OWNER ON IAP_REQUESTFORM.IDUSER = OWNER.IDUSER ";
        sql += "INNER JOIN IAP_CHANGE_TYPE ON IAP_REQUESTFORM.IDCHANGE = IAP_CHANGE_TYPE.IDCHANGE ";
        sql += "INNER JOIN IAP_PLANTYPES ON IAP_REQUESTFORM.PLANTYPEID = IAP_PLANTYPES.PLANTYPEID ";
        sql += "INNER JOIN IAP_OU ON IAP_AREA.IDOU = IAP_OU.IDOU ";

        sql += "LEFT OUTER JOIN IAP_USERS PLANNER ON IAP_REQUESTFORM.PLANNERID = PLANNER.IDUSER ";
        sql += "LEFT OUTER JOIN IAP_USERS FINANCE ON IAP_REQUESTFORM.FINANCEID = FINANCE.IDUSER ";
        sql += "LEFT OUTER JOIN IAP_USERS AUTHORISER ON IAP_REQUESTFORM.AUTHORISERID = AUTHORISER.IDUSER ";

        sql += "LEFT OUTER JOIN IAP_USERS ASSETAUTHORISER ON IAP_REQUESTFORM.ASSETAUTHORISERID = ASSETAUTHORISER.IDUSER ";
        sql += "LEFT OUTER JOIN IAP_USERS DRBCHAIR ON IAP_REQUESTFORM.DRBCHAIRID = DRBCHAIR.IDUSER ";

        // CHANGETYPE = :iChangeType

        return sql;
    }

    public static string SearchRequest(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE (IAP_REQUESTFORM.REQUEST_NUMBER LIKE :Search) OR (IAP_REQUESTFORM.PROJECT_ACTIVITY LIKE :Search) ORDER BY IAP_REQUESTFORM.REQUEST_NUMBER";

        return String.Format(sql, field);
    }

    public static string getRequestByRequestId(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE (IAP_REQUESTFORM.IDREQUEST = :lRequestId) ORDER BY IAP_REQUESTFORM.REQUESTDATE";

        return String.Format(sql, field);
    }

    public static string getRequestsByStatus(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE IAP_REQUESTFORM.STATUS = :iStatus ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return String.Format(sql, field);
    }

    public static string getPendingRequests(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE ((IAP_REQUESTFORM.STATUS <> :iStatus) AND (IAP_REQUESTFORM.STATUS <> :iStatus2) AND (IAP_REQUESTFORM.STATUS <> :iStatus3) ";
        sql += "AND (IAP_REQUESTFORM.STATUS <> :iStatus4) AND (IAP_REQUESTFORM.STATUS <> :iStatus5) AND (IAP_REQUESTFORM.STATUS <> :iStatus6)) ";
        sql += "AND ROUND(TO_NUMBER(MONTHS_BETWEEN(SYSDATE, IAP_REQUESTFORM.REQUESTDATE))) <= '6' ";
        sql += "ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return string.Format(sql, field);
    }

    public static string myPendingRequestsForApproval(string field)
    {
        string sql = getRequestMaster();

        sql += "WHERE ((IAP_REQUESTFORM.STATUS <> :iStatus) AND (IAP_REQUESTFORM.STATUS <> :iStatus2) AND (IAP_REQUESTFORM.STATUS <> :iStatus3) ";
        sql += "AND (IAP_REQUESTFORM.STATUS <> :iStatus4) AND (IAP_REQUESTFORM.STATUS <> :iStatus5) AND (IAP_REQUESTFORM.STATUS <> :iStatus6)) ";
        sql += "AND IAP_REQUESTFORM.IDUSER = :iUserId ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return String.Format(sql, field);
    }

    public static string loadMyApprovedRequests(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE IAP_REQUESTFORM.STATUS = :iStatus AND IAP_REQUESTFORM.IDUSER = :iUserId ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return String.Format(sql, field);
    }

    public static string LoadRejectedRequests(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE ((IAP_REQUESTFORM.STATUS = :iStatus) OR (IAP_REQUESTFORM.STATUS = :iStatus2) OR (IAP_REQUESTFORM.STATUS = :iStatus3) OR (IAP_REQUESTFORM.STATUS = :iStatus4) OR (IAP_REQUESTFORM.STATUS = :iStatus6)) ";
        sql += "ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return String.Format(sql, field);
    }

    public static string LoadMyRejectedRequests(string field)
    {
        string sql = getRequestMaster();
        sql += "WHERE ((IAP_REQUESTFORM.STATUS = :iStatus) OR (IAP_REQUESTFORM.STATUS = :iStatus2) OR (IAP_REQUESTFORM.STATUS = :iStatus3) OR (IAP_REQUESTFORM.STATUS = :iStatus4)) ";
        sql += "AND IAP_REQUESTFORM.IDUSER = :iUserId ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        return String.Format(sql, field);
    }

    #endregion


    public static string forwardRequestForApproval()
    {
        string sql = "INSERT INTO IAP_COMMENTS(IDUSER, IDREQUEST, DATE_RECEIVED, TIMERECEIVED) VALUES (:iUserId, :lRequestId, :DATE_RECEIVED, :TIMERECEIVED)";
        return sql;
    }

    public static string UpdateForwardRequestForApproval()
    {
        string sql = "UPDATE IAP_COMMENTS SET IDUSER = :iNewUserId, DATE_RECEIVED = :DATE_RECEIVED, TIMERECEIVED = :TIMERECEIVED WHERE (IDREQUEST = :lRequestId AND IDUSER = :iOldUserId)";
        return sql;
    }

    public static string approveRejectRequest()
    {
        string sql = "UPDATE IAP_COMMENTS SET COMMENTS = :sComments, COMMENTSDATE = :sCommentDate, TIMECOMMENTS = :TIMECOMMENTS, STATUS = :iStatus WHERE (IDREQUEST = :lRequestId AND IDUSER = :iUserId)";

        return sql;
    }

    public static string SLAExpired()
    {
        string sql = "UPDATE IAP_COMMENTS_IMPACTEDPARTY SET COMMENTS = :sComments, COMMENTSDATE = :sCommentDate, TIMECOMMENTS = :TIMECOMMENTS, STATUS = :iStatus WHERE (IDREQUEST = :lRequestId AND IDUSER = :iUserId)";

        return sql;
    }

    public static string checkFunctionalRepresentativeApprovals()
    {
        string sql = "SELECT IAP_COMMENTS.STATUS, IAP_COMMENTS.COMMENTSDATE, IAP_COMMENTS.IDREQUEST ";
        sql += "FROM IAP_COMMENTS INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "WHERE (IAP_COMMENTS.IDREQUEST = :lRequestId AND IAP_USERS.ROLEID = :iRoleId)";

        return sql;
    }

    public static string UpdateRequestStatus()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET STATUS = :iStatus WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    public static string UpdateChangeType()
    {
        string sql = "UPDATE IAP_REQUESTFORM SET CHANGETYPE = :iChangeType WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    //public static string getApproverByRequest()
    //{
    //    string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.FUNCTIONID, IAP_USERS.IDUSER, IAP_USERS.ROLEID, ";
    //    sql += "IAP_USERS.IAPPLANNER_TYPE, IAP_USERS.STATUS, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.SYSADMINEXT, ";
    //    sql += "IAP_FUNCTIONS.FUNCTION FROM IAP_REQUESTFORM INNER JOIN IAP_COMMENTS ON IAP_REQUESTFORM.IDREQUEST = IAP_COMMENTS.IDREQUEST ";
    //    sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
    //    sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
    //    sql += "WHERE (IAP_COMMENTS.IDREQUEST = :lRequestId) AND (IAP_USERS.ROLEID = :iRoleId)";

    //    return sql;
    //}

    public static string getApproverCommentByRole()
    {
        string sql = "SELECT IAP_USERS.FULLNAME, IAP_COMMENTS.COMMENTS, TO_CHAR(IAP_COMMENTS.COMMENTSDATE, 'DD-MON-YYYY') AS COMMENTSDATE, ";
        sql += "TO_CHAR(IAP_COMMENTS.DATE_RECEIVED, 'DD-MON-YYYY') AS DATE_RECEIVED, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTION, ";
        sql += "IAP_COMMENTS.COMMENTID, IAP_COMMENTS.STATUS, IAP_COMMENTS.IDREQUEST, IAP_COMMENTS.TIMERECEIVED, IAP_COMMENTS.TIMECOMMENTS FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_USERS.ROLEID = :iRoleId AND IAP_COMMENTS.IDREQUEST = :lRequestId";

        return sql;
    }

    public static string getApproversCommentByRequestId()
    {
        string sql = "SELECT IAP_COMMENTS.IDREQUEST, IAP_COMMENTS.COMMENTID, IAP_USERS.FULLNAME, IAP_COMMENTS.COMMENTS, IAP_COMMENTS.STATUS, IAP_COMMENTS.COMMENTSDATE, ";
        sql += "IAP_COMMENTS.DATE_RECEIVED, IAP_USERS.IDUSER, IAP_USERS.ROLEID, IAP_FUNCTIONS.FUNCTION, IAP_COMMENTS.TIMERECEIVED, IAP_COMMENTS.TIMECOMMENTS FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_COMMENTS.IDREQUEST = :lRequestId";

        return sql;
    }

    public static string getApproverCommentByUser()
    {
        string sql = "SELECT IAP_USERS.FULLNAME, IAP_COMMENTS.COMMENTS, TO_CHAR(IAP_COMMENTS.COMMENTSDATE, 'DD-MON-YYYY') AS COMMENTSDATE, ";
        sql += "TO_CHAR(IAP_COMMENTS.DATE_RECEIVED, 'DD-MON-YYYY') AS DATE_RECEIVED, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTION, ";
        sql += "IAP_COMMENTS.COMMENTID, IAP_COMMENTS.STATUS, IAP_COMMENTS.IDREQUEST, IAP_COMMENTS.TIMERECEIVED, IAP_COMMENTS.TIMECOMMENTS FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_USERS.IDUSER = :iUserId AND IAP_COMMENTS.IDREQUEST = :lRequestId";

        return sql;
    }

    public static string getIAPCommentsByStatus()
    {
        string sql = "SELECT IAP_USERS.FULLNAME, IAP_COMMENTS.COMMENTS, TO_CHAR(IAP_COMMENTS.COMMENTSDATE, 'DD-MON-YYYY') AS COMMENTSDATE, ";
        sql += "TO_CHAR(IAP_COMMENTS.DATE_RECEIVED, 'DD-MON-YYYY') AS DATE_RECEIVED, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTION, ";
        sql += "IAP_COMMENTS.COMMENTID, IAP_COMMENTS.STATUS, IAP_COMMENTS.IDREQUEST, IAP_COMMENTS.TIMERECEIVED, IAP_COMMENTS.TIMECOMMENTS FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_COMMENTS.STATUS <> :STATUS AND IAP_COMMENTS.IDREQUEST = :lRequestId";


        //string sql = "SELECT COMMENTID, IDREQUEST, STATUS, IDUSER, COMMENTS, COMMENTSDATE, DATE_RECEIVED FROM IAP_COMMENTS WHERE IDREQUEST = :lRequestId AND STATUS = :STATUS";
        return sql;
    }

    #region Impacted Parties

    public static string forwardRequestToImpactedParties()
    {
        string sql = "INSERT INTO IAP_COMMENTS_IMPACTEDPARTY(IDUSER, IDREQUEST, DATE_RECEIVED, TIMERECEIVED) VALUES (:iUserId, :lRequestId, :DATE_RECEIVED, :TIMERECEIVED)";
        return sql;
    }

    public static string IPReviewRequest()
    {
        string sql = "UPDATE IAP_COMMENTS_IMPACTEDPARTY SET COMMENTS = :sComments, COMMENTSDATE = :sCommentDate, TIMECOMMENTS = :TIMECOMMENTS, STATUS = :iStatus WHERE (IDREQUEST = :lRequestId AND IDUSER = :iUserId)";

        return sql;
    }

    public static string loadRequestsPendingMyReview(string field)
    {
        string sql = "SELECT DISTINCT IAP_REQUESTFORM.{0}, IAP_REQUESTFORM.IDREQUEST, IAP_REQUESTFORM.REQUEST_NUMBER, IAP_REQUESTFORM.PROJECT_ACTIVITY, IAP_REQUESTFORM.PLANISSUE, ";
        sql += "IAP_REQUESTFORM.WO_NO, IAP_REQUESTFORM.ORIGINATOR, IAP_REQUESTFORM.REQUESTDATE, IAP_REQUESTFORM.PROPOSAL, IAP_REQUESTFORM.BENEFIT, IAP_REQUESTFORM.RISK, ";
        sql += "IAP_REQUESTFORM.PVOL, IAP_REQUESTFORM.NVOL, IAP_REQUESTFORM.PCOST, IAP_REQUESTFORM.NCOST, IAP_REQUESTFORM.PVOLGAS, IAP_REQUESTFORM.NVOLGAS, IAP_REQUESTFORM.GH20, ";
        sql += "IAP_REQUESTFORM.LH20, IAP_REQUESTFORM.ORIGINALFROM, IAP_REQUESTFORM.ORIGINALTO, IAP_REQUESTFORM.REQUESTEDFROM, IAP_REQUESTFORM.REQUESTEDTO, ";
        sql += "IAP_REQUESTFORM.PRIMAVERA_ACTIVITYID, IAP_REQUESTFORM.STATUS, OWNER.FULLNAME, OWNER.IDUSER, OWNER.USERMAIL, IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, ";
        sql += "IAP_REQUESTFORM.REQUESTTIME, IAP_AREA.IDAREA, IAP_AREA.AREA, IAP_OU.IDOU, IAP_OU.OU, IAP_PLANTYPES.PLANTYPEID, IAP_PLANTYPES.PLANTYPE, IAP_CHANGE_TYPE.IDCHANGE, ";
        sql += "IAP_CHANGE_TYPE.TYPE, IAP_REQUESTFORM.IDREFPLAN FROM IAP_COMMENTS_IMPACTEDPARTY ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS_IMPACTEDPARTY.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN (IAP_REQUESTFORM INNER JOIN IAP_LOCATIONS ON IAP_REQUESTFORM.LOCATIONID = IAP_LOCATIONS.LOCATIONID ";
        sql += "INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA ";
        sql += "INNER JOIN IAP_CHANGE_TYPE ON IAP_REQUESTFORM.IDCHANGE = IAP_CHANGE_TYPE.IDCHANGE ";
        sql += "INNER JOIN IAP_PLANTYPES ON IAP_REQUESTFORM.PLANTYPEID = IAP_PLANTYPES.PLANTYPEID ";
        sql += "INNER JOIN IAP_OU ON IAP_AREA.IDOU = IAP_OU.IDOU) ON IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST = IAP_REQUESTFORM.IDREQUEST ";
        sql += "INNER JOIN IAP_USERS OWNER ON IAP_REQUESTFORM.IDUSER = OWNER.IDUSER ";
        sql += "WHERE ((IAP_COMMENTS_IMPACTEDPARTY.IDUSER = :iUserId) AND (IAP_COMMENTS_IMPACTEDPARTY.STATUS = :iStatus)) ORDER BY IAP_REQUESTFORM.REQUESTDATE DESC";

        sql = string.Format(sql, field);

        return sql;
    }

    public static string getImpactedPatyCommentByRequestId()
    {
        string sql = "SELECT IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST, IAP_COMMENTS_IMPACTEDPARTY.COMMENTID, IAP_USERS.FULLNAME, IAP_COMMENTS_IMPACTEDPARTY.COMMENTS, IAP_COMMENTS_IMPACTEDPARTY.STATUS, IAP_COMMENTS_IMPACTEDPARTY.COMMENTSDATE, ";
        sql += "IAP_COMMENTS_IMPACTEDPARTY.DATE_RECEIVED, IAP_USERS.IDUSER, IAP_USERS.ROLEID, IAP_FUNCTIONS.FUNCTION, IAP_COMMENTS_IMPACTEDPARTY.TIMERECEIVED, IAP_COMMENTS_IMPACTEDPARTY.TIMECOMMENTS FROM IAP_COMMENTS_IMPACTEDPARTY ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS_IMPACTEDPARTY.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST = :lRequestId";

        return sql;
    }

    public static string getImpactedPartyPendingAction()
    {
        string sql = "SELECT IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST, IAP_COMMENTS_IMPACTEDPARTY.COMMENTID, IAP_USERS.FULLNAME, IAP_COMMENTS_IMPACTEDPARTY.COMMENTS, IAP_COMMENTS_IMPACTEDPARTY.STATUS, IAP_COMMENTS_IMPACTEDPARTY.COMMENTSDATE, ";
        sql += "IAP_COMMENTS_IMPACTEDPARTY.DATE_RECEIVED, IAP_USERS.IDUSER, IAP_USERS.ROLEID, IAP_FUNCTIONS.FUNCTION, IAP_COMMENTS_IMPACTEDPARTY.TIMERECEIVED, IAP_COMMENTS_IMPACTEDPARTY.TIMECOMMENTS FROM IAP_COMMENTS_IMPACTEDPARTY ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS_IMPACTEDPARTY.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_COMMENTS_IMPACTEDPARTY.STATUS = :iStatus AND (ROUND((SYSDATE - IAP_COMMENTS_IMPACTEDPARTY.DATE_RECEIVED)) = 2)";

        return sql;
    }

    public static string getImpactedPartiesComments()
    {
        string sql = "SELECT IAP_USERS.FULLNAME, IAP_COMMENTS_IMPACTEDPARTY.COMMENTS, IAP_COMMENTS_IMPACTEDPARTY.COMMENTSDATE, ";
        sql += "IAP_COMMENTS_IMPACTEDPARTY.DATE_RECEIVED, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTION, ";
        sql += "IAP_COMMENTS_IMPACTEDPARTY.COMMENTID, IAP_COMMENTS_IMPACTEDPARTY.STATUS, IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST, ";
        sql += "IAP_COMMENTS_IMPACTEDPARTY.TIMERECEIVED, IAP_COMMENTS_IMPACTEDPARTY.TIMECOMMENTS FROM IAP_COMMENTS_IMPACTEDPARTY ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS_IMPACTEDPARTY.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_COMMENTS_IMPACTEDPARTY.IDREQUEST = :lRequestId";

        return sql;
    }


    #endregion





    #region ************************** Field IAP PEC **********************

    public static string getPec()
    {
        string sql = "SELECT IDPEC, PEC FROM IAP_PEC ORDER BY PEC";
        return sql;
    }

    public static string getPecById()
    {
        string sql = "SELECT IDPEC, PEC FROM IAP_PEC FROM IAP_PEC WHERE IDPEC = :IDPEC ORDER BY PEC";
        return sql;
    }

    public static string getPEC()
    {
        string sql = "SELECT IDPEC, PEC FROM IAP_PEC";
        return sql;
    }

    public static string createPec()
    {
        string sql = "INSERT INTO IAP_PEC (PEC) VALUES (:PEC)";
        return sql;
    }

    public static string updatePec()
    {
        string sql = "UPDATE IAP_PEC SET PEC = :PEC WHERE IDPEC = :IDPEC";

        return sql;
    }

    //**************** Field Activity Information ************************

    public static string getLocationFieldActivityInfo()
    {
        string sql = "SELECT ID_FIELD, ID_FIELD_LOC, ID_ACTIVITYINFO, STATUS FROM PROD_SEPCIN_LOC_F_ACTIVITYINFO";
        return sql;
    }

    public static string getLocationFieldActivityInfoById()
    {
        string sql = "SELECT ID_FIELD, ID_FIELD_LOC, ID_ACTIVITYINFO, STATUS FROM PROD_SEPCIN_LOC_F_ACTIVITYINFO WHERE ID_FIELD = :ID_FIELD";
        return sql;
    }

    public static string getIapPecStatusByRequestId()
    {
        string sql = "SELECT IDSTATUS, STATUS, IDPEC, IDREQUEST FROM IAP_PEC_STATUS WHERE IDREQUEST = :lRequestId";
        return sql;
    }

    public static string createRequestPecStatus()
    {
        string sql = "INSERT INTO IAP_PEC_STATUS (IDPEC, IDREQUEST, STATUS) ";
        sql += "VALUES (:IDPEC, :IDREQUEST, :STATUS)";

        return sql;
    }

    public static string updateRequestPecStatus()
    {
        string sql = "UPDATE IAP_PEC_STATUS SET STATUS = :STATUS WHERE (IDPEC = :IDPEC AND IDREQUEST = :iRequestId)";
        return sql;
    }

    public static string DeleteRequestPecStatus()
    {
        string sql = "DELETE FROM IAP_PEC_STATUS WHERE IDREQUEST = :iRequestId";
        return sql;
    }

    //IAP Window

    public static string CreateIAPWindow()
    {
        string sql = "INSERT INTO PROD_SEPCIN_IAPWINDOW (WINDOW) VALUES (:WINDOW)";
        return sql;
    }

    public static string UpdateIAPWindow()
    {
        string sql = "UPDATE PROD_SEPCIN_IAPWINDOW SET WINDOW = :WINDOW WHERE ID_WINDOW = :ID_WINDOW";
        return sql;
    }

    public static string getIAPWindows()
    {
        string sql = "SELECT ID_WINDOW, WINDOW FROM PROD_SEPCIN_IAPWINDOW";
        return sql;
    }

    public static string getIAPWindowsById()
    {
        string sql = "SELECT ID_WINDOW, WINDOW FROM PROD_SEPCIN_IAPWINDOW WHERE ID_WINDOW = :ID_WINDOW";
        return sql;
    }

    #endregion

    #region ************************** Units **********************

    public static string getUnits()
    {
        string sql = "SELECT ID_UNIT, UNITS FROM PROD_SEPCIN_UNIT";
        return sql;
    }

    public static string getUnitsById()
    {
        string sql = "SELECT ID_UNIT, UNITS FROM PROD_SEPCIN_UNIT WHERE ID_UNIT = :ID_UNIT";
        return sql;
    }

    public static string createUnits()
    {
        string sql = "INSERT INTO PROD_SEPCIN_UNIT (UNITS) VALUES (:UNITS)";
        return sql;
    }

    public static string updateUnits()
    {
        string sql = "UPDATE PROD_SEPCIN_UNIT SET UNITS = :UNITS WHERE ID_UNIT = :ID_UNIT";

        return sql;
    }

    #endregion

    #region ************************** Trade Types **********************

    public static string getTradeType()
    {
        string sql = "SELECT ID_TRADE_TYPE, TRADE_TYPE FROM PROD_SEPCIN_TRADE_TYPE";
        return sql;
    }

    public static string getTradeTypeById()
    {
        string sql = "SELECT ID_TRADE_TYPE, TRADE_TYPE FROM PROD_SEPCIN_TRADE_TYPE WHERE ID_TRADE_TYPE = :ID_TRADE_TYPE";
        return sql;
    }

    public static string createTradeType()
    {
        string sql = "INSERT INTO PROD_SEPCIN_TRADE_TYPE (TRADE_TYPE) VALUES (:TRADE_TYPE)";
        return sql;
    }

    public static string updateTradeType()
    {
        string sql = "UPDATE PROD_SEPCIN_TRADE_TYPE SET TRADE_TYPE = :TRADE_TYPE WHERE ID_TRADE_TYPE = :ID_TRADE_TYPE";

        return sql;
    }

    #endregion

    #region ************************** Baggages **********************

    public static string getBaggage()
    {
        string sql = "SELECT ID_BAG, BAGGAGE FROM PROD_SEPCIN_FIELD_BAGGAGE";
        return sql;
    }

    public static string getBaggageById()
    {
        string sql = "SELECT ID_BAG, BAGGAGE FROM PROD_SEPCIN_FIELD_BAGGAGE WHERE ID_BAG = :ID_BAG";
        return sql;
    }

    public static string createBaggage()
    {
        string sql = "INSERT INTO PROD_SEPCIN_FIELD_BAGGAGE (BAGGAGE) VALUES (:BAGGAGE)";
        return sql;
    }

    public static string updateBaggage()
    {
        string sql = "UPDATE PROD_SEPCIN_FIELD_BAGGAGE SET BAGGAGE = :BAGGAGE WHERE ID_BAG = :ID_BAG";

        return sql;
    }

    #endregion

    #region ************************** Contact Person **********************

    public static string getContactPerson()
    {
        string sql = "SELECT ID_CONTACT, CONTACT_PERSON FROM IAP_FPSO_CONTACT ORDER BY CONTACT_PERSON";
        return sql;
    }

    public static string getContactPersonById()
    {
        string sql = "SELECT ID_CONTACT, CONTACT_PERSON FROM IAP_FPSO_CONTACT WHERE ID_CONTACT = :ID_CONTACT";
        return sql;
    }

    public static string createContactPerson()
    {
        string sql = "INSERT INTO IAP_FPSO_CONTACT (CONTACT_PERSON) VALUES (:CONTACT_PERSON)";
        return sql;
    }

    public static string updateContactPerson()
    {
        string sql = "UPDATE IAP_FPSO_CONTACT SET CONTACT_PERSON = :CONTACT_PERSON WHERE ID_CONTACT = :ID_CONTACT";
        return sql;
    }

    #endregion

    #region ************************** Last Visit **********************

    public static string getLastVisit()
    {
        string sql = "SELECT ID_LAST_VISIT, LAST_VISIT FROM IAP_LAST_VISIT";
        return sql;
    }

    public static string getLastVisitById()
    {
        string sql = "SELECT ID_LAST_VISIT, LAST_VISIT FROM IAP_LAST_VISIT WHERE ID_LAST_VISIT = :ID_LAST_VISIT";
        return sql;
    }

    public static string createLastVisit()
    {
        string sql = "INSERT INTO IAP_LAST_VISIT (LAST_VISIT) VALUES (:LAST_VISIT)";
        return sql;
    }

    public static string updateLastVisit()
    {
        string sql = "UPDATE IAP_LAST_VISIT SET LAST_VISIT = :LAST_VISIT WHERE ID_LAST_VISIT = :ID_LAST_VISIT";
        return sql;
    }

    #endregion

    #region ************************** Visa Type **********************

    public static string getVisaType()
    {
        string sql = "SELECT ID_VISA, VISA_TYPE FROM IAP_VISA_TYPE";
        return sql;
    }

    public static string getVisaTypeById()
    {
        string sql = "SELECT ID_VISA, VISA_TYPE FROM IAP_VISA_TYPE WHERE ID_VISA = :ID_VISA";
        return sql;
    }

    public static string createVisaType()
    {
        string sql = "INSERT INTO IAP_VISA_TYPE (VISA_TYPE) VALUES (:VISA_TYPE)";
        return sql;
    }

    public static string updateVisaType()
    {
        string sql = "UPDATE IAP_VISA_TYPE SET VISA_TYPE = :VISA_TYPE WHERE ID_VISA = :ID_VISA";
        return sql;
    }

    #endregion

    #region ************************** SEPCiN Personnel Information *****************************

    public static string getPersonnelInformationReportByActivityID()
    {
        string sql = "SELECT ID_PERSONNEL, ID_ACTIVITYINFO, EMPLOYEE_NAME, GENDER, COMPANY, BOSIET_VALID, HUET_VALID, MEDICAL, PPE, ";
        sql += "ID_VISA, VISA_TYPE, ID_LAST_VISIT, LAST_VISIT, ID_BAG, BAGGAGE, ID_CONTACT, CONTACT_PERSON ";
        sql += "FROM PROD_VIW_PERSONNEL WHERE ID_ACTIVITYINFO = :ID_ACTIVITYINFO";

        return sql;
    }

    public static string getAllPersonnelInformation()
    {
        string sql = "SELECT ID_PERSONNEL, ID_ACTIVITYINFO, EMPLOYEE_NAME, GENDER, COMPANY, ID_CONTACT, ID_LAST_VISIT, ";
        sql += "BOSIET_VALID, HUET_VALID, MEDICAL, ID_VISA, PPE, ID_BAG FROM PROD_SEPCIN_PERSONNEL_INFO";

        return sql;
    }

    public static string getPersonnelInformationById()
    {
        string sql = "SELECT ID_PERSONNEL, IDREQUEST, EMPLOYEE_NAME, GENDER, COMPANY, ID_CONTACT, ID_LAST_VISIT, ";
        sql += "BOSIET_VALID, HUET_VALID, MEDICAL, ID_VISA, PPE, ID_BAG FROM IAP_PERSONNEL ";
        sql += "WHERE ID_PERSONNEL = :ID_PERSONNEL";

        return sql;
    }

    public static string getPersonnelInformationByRequestId()
    {
        //string sql = "SELECT IAP_PERSONNEL.ID_PERSONNEL, IAP_PERSONNEL.IDREQUEST, IAP_PERSONNEL.EMPLOYEE_NAME, IAP_PERSONNEL.GENDER, IAP_PERSONNEL.COMPANY, ";
        //sql += "IAP_PERSONNEL.BOSIET_VALID, IAP_PERSONNEL.MEDICAL, IAP_PERSONNEL.ID_VISA, IAP_PERSONNEL.VISA_TYPE, IAP_FPSO_CONTACT.ID_CONTACT, ";
        //sql += "IAP_LAST_VISIT.ID_LAST_VISIT, IAP_LAST_VISIT.LAST_VISIT, IAP_FPSO_CONTACT.CONTACT_PERSON FROM IAP_PERSONNEL ";
        //sql += "INNER JOIN IAP_LAST_VISIT ON PROD_SEPCIN_PERSONNEL_INFO.ID_LAST_VISIT = PROD_SEPCIN_FIELD_LAST_VISIT.ID_LAST_VISIT ";
        //sql += "INNER JOIN IAP_VISA_TYPE ON IAP_PERSONNEL.ID_VISA = IAP_VISA_TYPE.ID_VISA ";
        //sql += "INNER JOIN IAP_FPSO_CONTACT ON IAP_PERSONNEL.ID_CONTACT = IAP_FPSO_CONTACT.ID_CONTACT ";
        //sql += "WHERE IAP_PERSONNEL.IDREQUEST = :iRequestId";

        string sql = "SELECT IAP_PERSONNEL.ID_PERSONNEL, IAP_PERSONNEL.IDREQUEST, IAP_PERSONNEL.EMPLOYEE_NAME, IAP_PERSONNEL.GENDER, IAP_PERSONNEL.COMPANY, ";
        sql += "IAP_PERSONNEL.BOSIET_VALID, IAP_PERSONNEL.MEDICAL, IAP_PERSONNEL.ID_VISA, IAP_VISA_TYPE.VISA_TYPE, IAP_FPSO_CONTACT.ID_CONTACT, ";
        sql += "IAP_LAST_VISIT.ID_LAST_VISIT, IAP_LAST_VISIT.LAST_VISIT, IAP_FPSO_CONTACT.CONTACT_PERSON FROM IAP_PERSONNEL ";
        sql += "INNER JOIN IAP_LAST_VISIT ON IAP_PERSONNEL.ID_LAST_VISIT = IAP_LAST_VISIT.ID_LAST_VISIT ";
        sql += "INNER JOIN IAP_VISA_TYPE ON IAP_PERSONNEL.ID_VISA = IAP_VISA_TYPE.ID_VISA ";
        sql += "INNER JOIN IAP_FPSO_CONTACT ON IAP_PERSONNEL.ID_CONTACT = IAP_FPSO_CONTACT.ID_CONTACT ";
        sql += "WHERE IAP_PERSONNEL.IDREQUEST = :iRequestId";

        return sql;
    }

    public static string createPersonnelInformation()
    {
        string sql = "INSERT INTO IAP_PERSONNEL (IDREQUEST, EMPLOYEE_NAME, GENDER, COMPANY, ID_CONTACT, ID_LAST_VISIT, BOSIET_VALID, MEDICAL, ID_VISA) ";
        sql += "VALUES (:lRequestId, :EMPLOYEE_NAME, :GENDER, :COMPANY, :ID_CONTACT, :ID_LAST_VISIT, :BOSIET_VALID, :MEDICAL, :ID_VISA)";

        //string sql = "INSERT INTO IAP_PERSONNEL (IDREQUEST, EMPLOYEE_NAME, GENDER, COMPANY, ";
        //sql += "ID_CONTACT, ID_LAST_VISIT, BOSIET_VALID, HUET_VALID, MEDICAL, ID_VISA, PPE, ID_BAG) ";
        //sql += "VALUES (:lRequestId, :EMPLOYEE_NAME, :GENDER, :COMPANY, :ID_CONTACT, :ID_LAST_VISIT, ";
        //sql += ":BOSIET_VALID, :HUET_VALID, :MEDICAL, :ID_VISA, :PPE, :ID_BAG) ";

        return sql;
    }

    public static string updatePersonnelInformation()
    {
        string sql = "UPDATE IAP_PERSONNEL SET EMPLOYEE_NAME = :EMPLOYEE_NAME, GENDER = :GENDER, ";
        sql += "COMPANY = :COMPANY, ID_CONTACT = :ID_CONTACT, ID_LAST_VISIT = :ID_LAST_VISIT, BOSIET_VALID = :BOSIET_VALID, ";
        sql += "MEDICAL = :MEDICAL, ID_VISA = :ID_VISA WHERE ID_PERSONNEL = :ID_PERSONNEL";

        return sql;
    }

    public static string deletePersonnel()
    {
        string sql = "DELETE FROM IAP_PERSONNEL WHERE ID_PERSONNEL = :ID_PERSONNEL";
        return sql;
    }

    public static string DeletePersonnelByActivity()
    {
        string sql = "DELETE FROM IAP_PERSONNEL WHERE IDREQUEST = :IDREQUEST";
        return sql;
    }


    #endregion

    #region ====================== User Management Queries ============================================
    public static string getUserFromCompleteStaffDetails()
    {
        string sql = "SELECT USERNAME, FULL_NAME, EMAIL, STAFF_NUMBER FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD ";
        sql += "WHERE lower(substr(EMAIL,1,instr(EMAIL,'@')-1)) = lower(:EMAIL)";

        return sql;
    }

    public static string getUserFromCompleteStaffDetailsByUserName()
    {
        string sql = "SELECT USERNAME, FULL_NAME, EMAIL, STAFF_NUMBER FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD WHERE upper(USERNAME) = :USERNAME";
        return sql;
    }

    public static string CompleteStafDetailsBySurName()
    {
        string sql = "SELECT STAFF_NUMBER, USERNAME, FULL_NAME, EMAIL FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD WHERE upper(SURNAME) LIKE :SURNAME";
        return sql;
    }

    public static string CompleteStafDetailsByStaffNumber()
    {
        string sql = "SELECT STAFF_NUMBER, USERNAME, FULL_NAME, EMAIL FROM COMPLETE_STAFF_DETAILS@IWH_LINK.WORLD WHERE upper(STAFF_NUMBER) = :STAFF_NUMBER";
        return sql;
    }

    public static string MySQL()
    {
        //Note: the logic behind this query is that, anybody can be Activity owner, but not all activity owner can be anyother role at a point in time.
        //when someone in a registered role creeates a change request, he can not act in any role other than Activity owner. So this query checks if the Activity owner
        // is the creator of the change request in action 

        string sql = "SELECT DISTINCT IAP_USERS.IDUSER, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT, IAP_USERS.IAPPLANNER_TYPE, ";
        sql += "IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, IAP_USERS.IDOU FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_USERS.IDUSER NOT IN (SELECT IDUSER FROM IAP_REQUESTFORM WHERE IDREQUEST = :lRequestId) ";
        sql += "AND (IAP_USERS.ROLEID = :ROLEID AND STATUS = :STATUS) ";

        return sql;
    }

    public static string MySQL4IAPPlanner()
    {
        //Note: the logic behind this query is that, anybody can be Activity owner, but not all activity owner can be anyother role at a point in time.
        //when someone in a registered role creeates a change request, he can not act in any role other than Activity owner. So this query checks if the Activity owner
        //is the creator of the change request in action 

        string sql = "SELECT DISTINCT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT, ";
        sql += "IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, IAP_USERS.IDOU FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE (IAP_USERS.ROLEID = :ROLEID AND STATUS = :STATUS) ORDER BY IAP_USERS.FULLNAME";

        return sql;
    }

    public static string MySQL4IAPPlannerByOU()
    {
        string sql = "SELECT DISTINCT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT, ";
        sql += "IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, IAP_USERS.IDOU FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE (IAP_USERS.ROLEID = :ROLEID AND STATUS = :STATUS AND IDOU = :IDOU) ORDER BY IAP_USERS.FULLNAME";

        return sql;
    }

    public static string supportContacts()
    {
        //Note: the logic behind this query is that, anybody can be Activity owner, but not all activity owner can be anyother role at a point in time.
        //when someone in a registered role creeates a change request, he can not act in any role other than Activity owner. So this query checks if the Activity owner
        // is the creator of the change request in action 

        string sql = "SELECT DISTINCT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_USERS.IDOU, ";
        sql += "IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, IAP_USERROLES.ROLEID, IAP_USERROLES.ROLE, IAP_USERS.SYSADMINEXT, IAP_USERS.STATUS FROM IAP_USERS ";
        sql += "INNER JOIN IAP_USERROLES ON IAP_USERS.ROLEID = IAP_USERROLES.ROLEID ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE (CURRENT_ROLE_HOLDER = :CURRENT_ROLE_HOLDER AND STATUS = :STATUS) ORDER BY FULLNAME";

        return sql;
    }


    public static string getUserByDomainLoginID()
    {
        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTIONID, IAP_USERS.IAPPLANNER_TYPE, ";
        sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERROLES.ROLEID, IAP_USERROLES.ROLE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT, IAP_USERS.IDOU ";
        sql += "FROM IAP_USERS INNER JOIN IAP_USERROLES ON IAP_USERS.ROLEID = IAP_USERROLES.ROLEID INNER JOIN ";
        sql += "IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE lower(substr(IAP_USERS.USERMAIL,1,instr(IAP_USERS.USERMAIL,'@')-1)) = lower(:USERNAME) AND STATUS = :sStatus";

        return sql;
    }
    private static string getUser()
    {
        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, ";
        sql += "IAP_USERS.ROLEID, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT, IAP_USERS.IAPPLANNER_TYPE, IAP_USERS.IDOU ";
        sql += "FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";

        //string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, ";
        //sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER FROM IAP_USERS ";
        //sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        //sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";

        return sql;
    }
    public static string getUserByUserId()
    {
        string sql = getUser();
        sql += "WHERE IAP_USERS.IDUSER = :iUserId";

        return sql;
    }

    public static string getUserByUserName()
    {
        string sql = getUser();
        sql += "WHERE IAP_USERS.USERNAME = :sUserName";

        return sql;
    }

    public static string searchUser()
    {
        string sql = "SELECT USERNAME, FULLNAME, USERMAIL, IDUSER, ROLEID, CURRENT_ROLE_HOLDER, STATUS, SYSADMINEXT, IAPPLANNER_TYPE, IDOU ";
        sql += "FROM IAP_USERS WHERE (upper(FULLNAME) LIKE upper(:SEARCHKEY) OR upper(USERNAME) LIKE upper(:SEARCHKEY)) AND STATUS = :sStatus";
        return sql;
    }


    public static string getUsersByRoleId()
    {
        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, IAP_USERS.SYSADMINEXT, ";
        sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.IDOU FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        sql += "WHERE IAP_USERS.ROLEID = :iRoleId AND IAP_USERS.STATUS = :iStatus ORDER BY IAP_USERS.FULLNAME";
        //sql += "WHERE STATUS = :STATUS AND UPPER(IAP_USERS.FULLNAME) LIKE UPPER(:SEARCHKEY)";
        //string sql = getUser();

        return sql;
    }

    public static string getOriginators()
    {
        string sql = "SELECT DISTINCT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, ";
        sql += "IAP_USERS.SYSADMINEXT, IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.IDOU ";
        sql += "FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_REQUESTFORM.IDUSER = IAP_USERS.IDUSER ORDER BY IAP_USERS.FULLNAME";

        return sql;
    }

    #endregion


    #region  ************************** IAP Production Reponsible Parties **********************

    public static string getLocationsParties()
    {
        string sql = "SELECT IAP_LOCATIONS_PARTIES.ID, IAP_LOCATIONS_PARTIES.IDUSER, IAP_LOCATIONS_PARTIES.IDAREA, ";
        sql += "IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.IAPPLANNER_TYPE, IAP_USERS.USERMAIL, IAP_USERS.ROLEID, IAP_USERS.IDOU, IAP_USERS.FUNCTIONID, ";
        sql += "IAP_AREA.AREA AS FUNCTION, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT ";
        sql += "FROM IAP_LOCATIONS_PARTIES ";
        sql += "INNER JOIN IAP_USERS ON IAP_LOCATIONS_PARTIES.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_AREA ON IAP_LOCATIONS_PARTIES.IDAREA = IAP_AREA.IDAREA ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";

        return sql;
    }

    public static string getLocationsPartiesByHub()
    {
        string sql = getLocationsParties();
        sql += "WHERE IAP_LOCATIONS_PARTIES.IDAREA = :iAreaId";
        return sql;
    }

    public static string AddLocationParties()
    {
        string sql = "INSERT INTO IAP_LOCATIONS_PARTIES (IDUSER, IDAREA) VALUES (:iUserId, :iAreaId)";
        return sql;
    }

    public static string RemoveLocationsParties()
    {
        string sql = "DELETE FROM IAP_LOCATIONS_PARTIES WHERE IDUSER = :iUserId AND IDAREA = :iAreaId";
        return sql;
    }

    #endregion


    #region  ************************** IAP Functions Reponsible Parties **********************

    public static string getFunctionsParties()
    {
        string sql = "SELECT IAP_FUNCTIONS_PARTIES.ID, IAP_FUNCTIONS_PARTIES.IDUSER, IAP_FUNCTIONS_PARTIES.FUNCTIONID, ";
        sql += "IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.IAPPLANNER_TYPE, IAP_USERS.USERMAIL, IAP_USERS.ROLEID, IAP_USERS.IDOU, ";
        sql += "IAP_FUNCTIONS.FUNCTION, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS, IAP_USERS.SYSADMINEXT ";
        sql += "FROM IAP_FUNCTIONS_PARTIES ";
        sql += "INNER JOIN IAP_USERS ON IAP_FUNCTIONS_PARTIES.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_FUNCTIONS_PARTIES.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";

        return sql;
    }
    public static string getFunctionsPartiesByFunction()
    {
        string sql = getFunctionsParties();
        sql += "WHERE IAP_FUNCTIONS_PARTIES.FUNCTIONID = :iFunctionId";
        return sql;
    }

    public static string AddFunctionsParties()
    {
        string sql = "INSERT INTO IAP_FUNCTIONS_PARTIES (IDUSER, FUNCTIONID) VALUES (:iUserId, :iFunctionId)";
        return sql;
    }

    public static string RemoveFunctionsParties()
    {
        string sql = "DELETE FROM IAP_FUNCTIONS_PARTIES WHERE IDUSER = :iUserId AND FUNCTIONID = :iFunctionId";
        return sql;
    }

    #endregion


    public static string DashBoard()
    {
        string sql = "SELECT COUNT(*) CRNO, TO_CHAR(IAP_REQUESTFORM.requestdate, 'MON') MMONTH, ";
        sql += "TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR FROM IAP_OU ";
        sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
        sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
        sql += "WHERE IAP_OU.IDOU = :OuId AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') = :iYear GROUP BY IAP_REQUESTFORM.requestdate ORDER BY MMONTH";

        return sql;
    }

    public static string PerformanceReporting()
    {
        string sql = "SELECT IAP_COMMENTS.IDREQUEST, IAP_REQUESTFORM.PROJECT_ACTIVITY, IAP_USERS.FULLNAME, IAP_COMMENTS.COMMENTS, IAP_COMMENTS.COMMENTSDATE, ";
        sql += "IAP_COMMENTS.DATE_RECEIVED, IAP_COMMENTS.IDUSER, IAP_FUNCTIONS.FUNCTION, IAP_COMMENTS.COMMENTID, IAP_COMMENTS.STATUS, IAP_COMMENTS.TIMERECEIVED, ";
        sql += "IAP_COMMENTS.TIMECOMMENTS FROM IAP_COMMENTS ";
        sql += "INNER JOIN IAP_USERS ON IAP_COMMENTS.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_COMMENTS.IDREQUEST = IAP_REQUESTFORM.IDREQUEST ";
        sql += "WHERE IAP_REQUESTFORM.STATUS < :iStatusa AND IAP_COMMENTS.STATUS = :iStatusb AND ROUND(TO_NUMBER(MONTHS_BETWEEN(SYSDATE, IAP_COMMENTS.DATE_RECEIVED))) <= '2'";

        return sql;
    }

    public static string getYear()
    {
        string sql = "SELECT DISTINCT TO_CHAR(IAP_REQUESTFORM.REQUESTDATE, 'yyyy') AS YYEAR FROM IAP_REQUESTFORM ORDER BY YYEAR DESC";
        return sql;
    }

    public static string getRequetByMonthYear()
    {
        string sql = "SELECT IAP_REQUESTFORM.IDREQUEST, IAP_REQUESTFORM.REQUEST_NUMBER, IAP_REQUESTFORM.PROJECT_ACTIVITY, IAP_REQUESTFORM.PROJECT_ACTIVITY FROM IAP_REQUESTFORM ";
        sql += "WHERE TO_CHAR(IAP_REQUESTFORM.REQUESTDATE, 'YYYY') = :iYear AND TO_CHAR(IAP_REQUESTFORM.REQUESTDATE, 'MM') = :iMonth";
        return sql;
    }
}
