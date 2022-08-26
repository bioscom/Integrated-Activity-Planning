using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using System.Linq;

/// <summary>
/// Summary description for RequestsApprovalWorkFlow
/// </summary>

public static class ApprovalWorkFlow
{
    static ApprovalWorkFlow()
    {

    }

    #region ========  Service Level Agreement

    public static void RunSLA()
    {        
        DataTable dt = GetSLAStatus();

        DateTime LastWeek = DateTime.Parse(dt.Rows[0]["TODAYSDATE"].ToString());
        DateTime ThisWeek = DateTime.Today.Date;

        if ((ThisWeek - LastWeek).TotalDays == 7)
        {
            //NOTE: Please respect this order
            PerformanceReports();   //Step 1:
            ImpactedpartiesSLA();   //Step 2:
            SlaRefresh();           //Step 3:
        }
    }

    private static bool SlaRefresh()
    {
        //Update the date for the next seven days.
        string sql = "UPDATE IAP_SLA SET TODAYSDATE = :dTodaysDate, RUNED = :iRunned WHERE TODAYSDATE <> :dTodaysDate";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":dTodaysDate";
        param.Value = DateTime.Now.AddDays(7);
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iRunned";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    private static DataTable GetSLAStatus()
    {
        string sql = "SELECT TODAYSDATE, RUNED FROM IAP_SLA";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static void PerformanceReports()
    {
        List<ApproversComment> oLst = lstGetPerformanceReporting();
        //Send mail
        PerformanceReports(oLst);


        //int iHour = int.Parse(DateTime.Now.ToString("HH").ToString());
        //if (iHour >= 7)
        //{
        //    List<ApproversComment> oLst = lstGetPerformanceReporting();
        //    //Send mail
        //    PerformanceReports(oLst);
        //    //oMail.ImpactedPatiesSLAExpired(UsersActions.objGetUserByUserId(o.m_iUserId).structUserIdx, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc, o.m_sDateReceived);
        //}
    }

    public static List<ApproversComment> lstGetPerformanceReporting()
    {
        DataTable dt = dtPerformanceReporting();
        return (from DataRow dr in dt.Rows select new ApproversComment(dr)).ToList();
    }

    private static void PerformanceReports(List<ApproversComment> oLst)
    {
        string sField = "REQUEST_NUMBER";
        SendMail oMail = new SendMail(SendMail.eManager());
        aRequest o = new aRequest();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        List<ApproversComment> oLst1 = oLst;
        List<ApproversComment> oLst2 = oLst;

        //Select Distinct UserID
        //var list = callList.GroupBy(x => x.ApplicationID).Select(x => x.First()).ToList();
        var list = oLst1.GroupBy(x => x.m_iUserId).Select(x => x.First()).ToList();

        list.ForEach(x =>
        {
            //<!-- Table Header -->

            sb.Append("<table style='border:solid 1px silver; font-size:12px; width:800px; border-collapse:collapse'>");
            sb.Append("<tr>");
            sb.Append("<td style='border:solid 1px silver'><b>SNo</b></td>");
            sb.Append("<td style='width:100px;border:solid 1px silver'><b>Request Number</b></td>");
            sb.Append("<td style='width:200px;border:solid 1px silver'><b>Activity</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Date Received</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Time Received</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Initiator</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Functional Authoriser</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Planner</b></td>");
            sb.Append("<td style='border:solid 1px silver'><b>Plan Owner</b></td>");
            sb.Append("<td style='width:200px;border:solid 1px silver'><b>Status</b></td>");
            sb.Append("<tr>");

            int i = 1;
            var olist = oLst2.FindAll(t => t.m_iUserId == x.m_iUserId);
            foreach (ApproversComment oc in olist)
            {
                //o = RequestHelper.objGetRequestByRequestId(x.m_lRequestId, sField);
                o = RequestHelper.objGetRequestByRequestId(oc.m_lRequestId, sField);

                sb.Append("<tr>");
                sb.Append("<td style='border:solid 1px silver'>" + i + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + o.m_sRequestNumber + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + o.m_sProjectActivity + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + String.Format("{0:MM-dd-yyyy}", oc.m_sDateReceived) + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + String.Format("{0:MM-dd-yyyy}", oc.m_sTimeReceived) + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + UsersActions.objGetUserByUserId(o.m_iUserId).m_sFullName + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + UsersActions.objGetUserByUserId(o.m_iAuthoriserId).m_sFullName + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + UsersActions.objGetUserByUserId(o.m_iPlannerId).m_sFullName + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + UsersActions.objGetUserByUserId(o.m_iDrbChairId).m_sFullName + "</td>");
                sb.Append("<td style='border:solid 1px silver'>" + RequestStatus.StatusReporting(o.m_iStatus) + "</td>");
                sb.Append("<tr>");
                i++;
            }
            sb.Append("</table>");

            oMail.PerformanceReporting(UsersActions.objGetUserByUserId(x.m_iUserId).structUserIdx, sb.ToString());
            sb.Clear();
        });

        //return sb.ToString();
    }

    private static void ImpactedpartiesSLA()
    {
        //Note: this method should run once a day. Check the IAP_SLA, if RUNED = 1 for today's date
        string sField = "REQUEST_NUMBER";
        aRequest oRequest = new aRequest();
        SendMail oMail = new SendMail(SendMail.eManager());

        // Copy some Special People
        List<structUserMailIdx> cc = new List<structUserMailIdx>();
        List<Users> Corporate = UsersActions.lstGetContacts();
        foreach (var t in Corporate)
        {
            cc.Add(UsersActions.objGetUserByUserId(t.m_iUserId).structUserIdx);
        }

        //int iHour = DateTime.Now.Date.Hour;

        int iHour = int.Parse(DateTime.Now.ToString("HH").ToString());

        //if ((iHour >= 7) && (iHour <= 12))
        //{
        //    //Send warning mail

        //    List<ApproversComment> oLst = lstGetImpactedPartyPendingAction();
        //    foreach (ApproversComment o in oLst)
        //    {
        //        oRequest = RequestHelper.objGetRequestByRequestId(o.m_lRequestId, sField);
        //        oMail.WarnImpactedPaties(UsersActions.objGetUserByUserId(o.m_iUserId).structUserIdx, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc, o.m_sDateReceived);
        //    }
        //}
        if (iHour >= 7)
        {
            List<ApproversComment> oLst = lstGetImpactedPartyPendingAction();
            foreach (ApproversComment o in oLst)
            {
                //Update the items automatically and send mail to notify user of the closing.
                RequestSLAExpired("Authomatically agreed on your behalf. Request has exceeded the Service Level Agreement", o.m_lRequestId, RequestStatus.m_iApproved, o.m_iUserId);

                //Send mail
                oRequest = RequestHelper.objGetRequestByRequestId(o.m_lRequestId, sField);
                oMail.ImpactedPatiesSLAExpired(UsersActions.objGetUserByUserId(o.m_iUserId).structUserIdx, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc, o.m_sDateReceived);
            }

            // The IAP_SLA table will be updated here for the day
            SlaRunForTheDay();
        }
    }

    private static bool SlaRunForTheDay()
    {
        //Ran for the day.
        string sql = "UPDATE IAP_SLA SET RUNED = :iRunned";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iRunned";
        param.Value = 1;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    #endregion

    public static bool ForwardsRequestToImpactedParties(long lRequestId, int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.forwardRequestToImpactedParties();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_RECEIVED";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMERECEIVED";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static DataTable GetApproversCommentByRole(int iRoleId, long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getApproverCommentByRole();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iRoleId";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable GetImpactecdPartiesComment(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getImpactedPartiesComments();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getApproversCommentByRequestId(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getApproversCommentByRequestId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getImpactedPartyCommentByRequestId(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getImpactedPatyCommentByRequestId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable GetApproversCommentByUser(int iUserId, long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getApproverCommentByUser();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetIAPCommentsByStatus(long lRequestId, int iStatus)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIAPCommentsByStatus();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //RequestStatus.m_iNotApproved;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<ApproversComment> lstGetImpactedPartyCommentsByRequestId(long lRequestId)
    {
        DataTable dt = getImpactedPartyCommentByRequestId(lRequestId);
        return (from DataRow dr in dt.Rows select new ApproversComment(dr)).ToList();
    }

    public static DataTable dtGetImpactedPartyPendingAction()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getImpactedPartyPendingAction();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iDefault;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<ApproversComment> lstGetImpactedPartyPendingAction()
    {
        DataTable dt = dtGetImpactedPartyPendingAction();
        return (from DataRow dr in dt.Rows select new ApproversComment(dr)).ToList();
    }

    

    public static DataTable dtPerformanceReporting()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.PerformanceReporting();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatusa";
        param.Value = RequestStatus.m_iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatusb";
        param.Value = RequestStatus.m_iDefault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool RerouteRequestApprover(long lRequestId, int iNewUserId, int iOldUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateForwardRequestForApproval();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iNewUserId";
        param.Value = iNewUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iOldUserId";
        param.Value = iOldUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_RECEIVED";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMERECEIVED";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool RequestSLAExpired(string sComments, long lRequestId, int iStatus, int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.SLAExpired();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sComments";
        param.Value = sComments;
        param.DbType = DbType.String;
        param.Size = 4000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sCommentDate";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMECOMMENTS";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }



    public static bool RequestApprovalProcess(string sComments, long lRequestId, int iStatus, int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.approveRejectRequest();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sComments";
        param.Value = sComments;
        param.DbType = DbType.String;
        param.Size = 4000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sCommentDate";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMECOMMENTS";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    //Update Change Type. New update to IAP Request Tool Jan 2020
    public static bool RequestUpdateChangeType(long lRequestId, int iChangeType)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateChangeType();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iChangeType";
        param.Value = iChangeType;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    public static bool IPRequestReviewProcess(string sComments, long lRequestId, int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.IPReviewRequest();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sComments";
        param.Value = sComments;
        param.DbType = DbType.String;
        param.Size = 4000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sCommentDate";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMECOMMENTS";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static ApproversComment GetApproverCommentByUser(int iUserId, long lRequestId)
    {
        DataTable dt = GetApproversCommentByUser(iUserId, lRequestId);

        ApproversComment oComment = new ApproversComment();
        foreach (DataRow dr in dt.Rows)
        {
            oComment = new ApproversComment(dr);
        }

        return oComment;
    }

    public static ApproversComment objGetApproversComments(int iRoleId, long lRequestId)
    {
        DataTable dt = GetApproversCommentByRole(iRoleId, lRequestId);

        ApproversComment oComment = new ApproversComment();
        foreach (DataRow dr in dt.Rows)
        {
            oComment = new ApproversComment(dr);
        }

        return oComment;
    }

    public static bool ForwardsRequestToApprovers(long lRequestId, int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.forwardRequestForApproval();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_RECEIVED";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TIMERECEIVED";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static List<ApproversComment> lstGetApproversCommentsByStatus(long lRequestId, int iStatus)
    {
        DataTable dt = dtGetIAPCommentsByStatus(lRequestId, iStatus);
        return (from DataRow dr in dt.Rows select new ApproversComment(dr)).ToList();

        //List<ApproversComment> lstComments = new List<ApproversComment>();
        //DataTable dt = dtGetIAPCommentsByStatus(lRequestId, iStatus);
        //foreach (DataRow dr in dt.Rows)
        //{
        //    lstComments.Add(new ApproversComment(dr));
        //}
        //return lstComments;
    }

    public static List<ApproversComment> lstGetApproversCommentsByRequestId(long lRequestId)
    {
        DataTable dt = getApproversCommentByRequestId(lRequestId);
        return (from DataRow dr in dt.Rows select new ApproversComment(dr)).ToList();
    }

    public static bool HasAllFunctionalRepresentativesApproved(long lRequestId)
    {
        bool status = false;
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.checkFunctionalRepresentativeApprovals();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iRoleId";
        param.Value = (int) appUsersRoles.userRole.FunctionalAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        foreach (DataRow dr in dt.Rows)
        {
            if ((int.Parse(dr["STATUS"].ToString()) != RequestStatus.m_iNotApproved) && (int.Parse(dr["STATUS"].ToString()) != RequestStatus.m_iDefault))
                status = true;
            else
            {
                status = false;
                break;
            }
        }
        return status;
    }

    public static void FillApprovalDropDownList(DropDownList ApprovalStandDropDownList)
    {
        string[] listItemText = new string[2];
        string[] listItemValue = new string[2];


        listItemText[0] = RequestStatus.m_sApproved; listItemValue[0] = RequestStatus.m_iApproved.ToString();
        listItemText[1] = RequestStatus.m_sNotApproved; listItemValue[1] = RequestStatus.m_iNotApproved.ToString();

        for (int i = 0; i <= 1; i++)
        {
            ApprovalStandDropDownList.Items.Add(new ListItem(listItemText[i], listItemValue[i]));
        }
    }

    public static void FillChangeTypeDropDownList(DropDownList ChangeTypeDropDownList)
    {
        string[] listItemText = new string[3];
        string[] listItemValue = new string[3];


        listItemText[0] = RequestStatus.m_sBadChange; listItemValue[0] = RequestStatus.m_iBadChange.ToString();
        listItemText[1] = RequestStatus.m_sGoodChange; listItemValue[1] = RequestStatus.m_iGoodChange.ToString();
        listItemText[2] = RequestStatus.m_sNeutral; listItemValue[2] = RequestStatus.m_iNeutral.ToString();

        for (int i = 0; i <= 2; i++)
        {
            ChangeTypeDropDownList.Items.Add(new ListItem(listItemText[i], listItemValue[i]));
        }
    }

    public static bool AssignApproversToRequest1(long lRequestId, int iDRBChairId, int iFinanceId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AssignFinalApprovers1();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iDRBChairId";
        param.Value = iDRBChairId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iFinanceId";
        param.Value = iFinanceId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool AssignApproversToRequest(long lRequestId, int iAssetAuthoriserId, int iDRBChairId, int iFinanceId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AssignFinalApprovers();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iAssetAuthoriserId";
        param.Value = iAssetAuthoriserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iDRBChairId";
        param.Value = iDRBChairId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iFinanceId";
        param.Value = iFinanceId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool AssignApproversToRequest2(long lRequestId, int iAssetAuthoriserId, int iDRBChairId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AssignFinalApprovers2();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iAssetAuthoriserId";
        param.Value = iAssetAuthoriserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iDRBChairId";
        param.Value = iDRBChairId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool AssignApproversToRequest3(long lRequestId, int iDRBChairId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AssignFinalApprovers3();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iDRBChairId";
        param.Value = iDRBChairId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    public static bool UpdateRequestFlowStatus(long lRequestId, int iStatus)
    {
        //int iStatus = 0;

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateRequestStatus();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    //public static Users GetApproverByRequestId(long lRequestId, int iRoleId)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getApproverByRequest();

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iRoleId";
    //    param.Value = iRoleId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":lRequestId";
    //    param.Value = lRequestId;
    //    param.DbType = DbType.Int64;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

    //    Users ProductionAssetRep = new Users();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        ProductionAssetRep = new Users(dr);
    //    }
    //    return ProductionAssetRep;
    //}


    //public static bool AssignApproversToRequest(long lRequestId, int i)
    //{
    //    //int iStatus = 0;

    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.UpdateRequestStatus();

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = iStatus;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":lRequestId";
    //    param.Value = lRequestId;
    //    param.DbType = DbType.Int64;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
    //    int result = -1;
    //    try
    //    {
    //        // execute the stored procedure
    //        result = GenericDataAccess.ExecuteNonQuery(comm);
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //    // result will be 1 in case of success
    //    return (result != -1);
    //}

    //public static bool ActivityOwnerForwardsRequestToIAPPlanner(long lRequestId, int iUserId)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.forwardRequestForApproval();

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":lRequestId";
    //    param.Value = lRequestId;
    //    param.DbType = DbType.Int64;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":DATE_RECEIVED";
    //    param.Value = DateTime.Now;
    //    param.DbType = DbType.Date;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
    //    int result = -1;
    //    try
    //    {
    //        // execute the stored procedure
    //        result = GenericDataAccess.ExecuteNonQuery(comm);
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //    // result will be 1 in case of success
    //    return (result != -1);
    //}

}
