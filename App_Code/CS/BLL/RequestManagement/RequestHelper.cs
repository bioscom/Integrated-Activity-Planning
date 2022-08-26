using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Security.Application;

/// <summary>
/// Summary description for RequestHelper
/// </summary>

public class RequestHelper
{
    public RequestHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string getRequestNo(string IAPPlannerType)
    {
        string NewRequestNo = "";
        string sql = "SELECT IAP_REQUESTNO_SEQ.NEXTVAL FROM DUAL";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        try
        {
            if (dt.Rows.Count > 0)
            {
                string OOO = "000";
                string Valuess = dt.Rows[0]["NEXTVAL"].ToString();
                NewRequestNo = IAPPlannerType + "-IAP" + OOO + Valuess;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return NewRequestNo;
    }

    public bool CreateRequest(DropDownList ddlPlannerType, aRequest oRequest, int iStatus, ref long lRequestId, ref string sRequestNumber)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.CreateNewRequest();

        sRequestNumber = GenerateRequestNumber(ddlPlannerType);   //get the Request Number for a New Request, based on the selected IAP Planner
        lRequestId = getRequestId();

        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LOCATIONID";
        param.Value = oRequest.m_iLocationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iRefPlanId";
        param.Value = oRequest.m_iRefPlanId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPEID";
        param.Value = oRequest.m_iPlanTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDCHANGE";
        param.Value = oRequest.m_iChangeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUEST_NUMBER";
        param.Value = sRequestNumber;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJECT_ACTIVITY";
        param.Value = oRequest.m_sProjectActivity;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PLANISSUE";
        param.Value = oRequest.m_sPLANISSUE;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":WO_NO";
        param.Value = oRequest.m_sWO_NO;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTDATE";
        param.Value = DateTime.Now;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROPOSAL";
        param.Value = oRequest.m_sProposal;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":BENEFIT";
        param.Value = oRequest.m_sBenefit;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":RISK";
        param.Value = oRequest.m_sRisk;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PVOL";
        param.Value = oRequest.m_sPVOL;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NVOL";
        param.Value = oRequest.m_sNVOL;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PCOST";
        param.Value = oRequest.m_sPCOST;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NCOST";
        param.Value = oRequest.m_sNCOST;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PVOLGAS";
        param.Value = oRequest.m_sPVOLGAS;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NVOLGAS";
        param.Value = oRequest.m_sNVOLGAS;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GH20";
        param.Value = oRequest.m_sGH20;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LH20";
        param.Value = oRequest.m_sLH20;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ORIGINALFROM";
        param.Value = oRequest.m_sORIGINALFROM;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ORIGINALTO";
        param.Value = oRequest.m_sORIGINALTO;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTEDFROM";
        param.Value = oRequest.m_sREQUESTEDFROM;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTEDTO";
        param.Value = oRequest.m_sREQUESTEDTO;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTTIME";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        //Note: This is the Change Requestor
        param = comm.CreateParameter();
        param.ParameterName = ":IDUSER";
        param.Value = oRequest.m_iUserId;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iAuthoriserId";
        param.Value = oRequest.m_iAuthoriserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iPlannerId";
        param.Value = oRequest.m_iPlannerId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sPrimaveraId";
        param.Value = oRequest.m_sPRIMAVERA_ACTIVITYID;
        param.DbType = DbType.String;
        param.Size = 1000;
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

    public bool UpdateRequest(aRequest oRequest)
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateRequest();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":LOCATIONID";
        param.Value = oRequest.m_iLocationId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = oRequest.lRequestId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iRefPlanId";
        param.Value = oRequest.m_iRefPlanId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPEID";
        param.Value = oRequest.m_iPlanTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDCHANGE";
        param.Value = oRequest.m_iChangeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJECT_ACTIVITY";
        param.Value = oRequest.m_sProjectActivity;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PLANISSUE";
        param.Value = oRequest.m_sPLANISSUE;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":WO_NO";
        param.Value = oRequest.m_sWO_NO;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROPOSAL";
        param.Value = oRequest.m_sProposal;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":BENEFIT";
        param.Value = oRequest.m_sBenefit;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":RISK";
        param.Value = oRequest.m_sRisk;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PVOL";
        param.Value = oRequest.m_sPVOL;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NVOL";
        param.Value = oRequest.m_sNVOL;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PCOST";
        param.Value = oRequest.m_sPCOST;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NCOST";
        param.Value = oRequest.m_sNCOST;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PVOLGAS";
        param.Value = oRequest.m_sPVOLGAS;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":NVOLGAS";
        param.Value = oRequest.m_sNVOLGAS;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GH20";
        param.Value = oRequest.m_sGH20;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LH20";
        param.Value = oRequest.m_sLH20;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ORIGINALFROM";
        param.Value = oRequest.m_sORIGINALFROM;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ORIGINALTO";
        param.Value = oRequest.m_sORIGINALTO;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTEDFROM";
        param.Value = oRequest.m_sREQUESTEDFROM;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTEDTO";
        param.Value = oRequest.m_sREQUESTEDTO;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        //If planner changes, there is a need for the Request number to change, but requet number will be changed if Planner type is different
        param = comm.CreateParameter();
        param.ParameterName = ":REQUEST_NUMBER";
        param.Value = oRequest.m_sRequestNumber;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REQUESTTIME";
        param.Value = DateTime.Now.ToShortTimeString();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sPrimaveraId";
        param.Value = oRequest.m_sPRIMAVERA_ACTIVITYID;
        param.DbType = DbType.String;
        param.Size = 1000;
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

    private long getRequestId()
    {
        string sql = "SELECT IAP_REQUEST_SEQ.NEXTVAL FROM DUAL";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        long lRequestId = long.Parse(dt.Rows[0]["NEXTVAL"].ToString());

        return lRequestId;
    }

    public string GenerateRequestNumber(DropDownList ddlPlannerType)
    {
        string plannerType = "";
        Users IAPPlanner = UsersActions.objGetUserByUserId(int.Parse(ddlPlannerType.SelectedValue));

        if (!string.IsNullOrEmpty(IAPPlanner.m_iIAPPlannerType.ToString()))
        {
            //iapTypeHF.Value = IAPPlanner.m_sIAPPlannerType;
            if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.mt)
            {
                plannerType = appUsersRoles.PlannerTypeDesc(PlannerType.mt);
            }
            else if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.st)
            {
                plannerType = appUsersRoles.PlannerTypeDesc(PlannerType.st);
            }
            else if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.vst)
            {
                plannerType = appUsersRoles.PlannerTypeDesc(PlannerType.vst);
            }

            plannerType = getRequestNo(plannerType);
        }

        return plannerType;
    }

    public static DataTable dtSearchRequest(string sText, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.SearchRequest(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":Search";
        param.Value = "%" + Encoder.HtmlEncode(sText.ToUpper()) + "%";
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetRequestByRequestId(long lRequestId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getRequestByRequestId(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static aRequest objGetRequestByRequestId(long lRequestId, string field)
    {
        DataTable dt = dtGetRequestByRequestId(lRequestId, field);

        aRequest o = new aRequest();
        foreach (DataRow dr in dt.Rows)
        {
            o = new aRequest(dr);
        }
        return o;
    }

    public static DataTable dtRequestsByStatus(int iStatus, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getRequestsByStatus(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtLoadPendingRequestForms(string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPendingRequests(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus2";
        param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus3";
        param.Value = RequestStatus.m_iNotApprovedByPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus4";
        param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus5";
        param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus6";
        param.Value = RequestStatus.m_iCancelled;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtLoadRequestsPendingMyApproval(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iDefault;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtLoadRequestsPendingMyReview(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadRequestsPendingMyReview(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iDefault;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtRequestsIReviewed(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadRequestsPendingMyReview(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtRequestsIApproved(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtRequestsIDidNotApprove(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iNotApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtLoadMyPendingRequest(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.myPendingRequestsForApproval(field);

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
        param.ParameterName = ":iStatus2";
        param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus3";
        param.Value = RequestStatus.m_iNotApprovedByPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus4";
        param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus5";
        param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus6";
        param.Value = RequestStatus.m_iCancelled;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
        
    }

    public static DataTable dtMyApprovedRequests(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.loadMyApprovedRequests(field);

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

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtRejectedRequests(string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.LoadRejectedRequests(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus2";
        param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus3";
        param.Value = RequestStatus.m_iNotApprovedByPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus4";
        param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus6";
        param.Value = RequestStatus.m_iCancelled;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtMyRejectedRequests(int iUserId, string field)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.LoadMyRejectedRequests(field);

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus2";
        param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus3";
        param.Value = RequestStatus.m_iNotApprovedByPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus4";
        param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static void ReferencePlan(DropDownList ddlPlanner, DropDownList ddlRefPlan)
    {
        try
        {
            int iRefPlanId = 0;
            Users IAPPlanner = UsersActions.objGetUserByUserId(int.Parse(ddlPlanner.SelectedValue));

            if (IAPPlanner.m_iIAPPlannerType != 0)
            {
                ddlRefPlan.Items.Clear();
                ddlRefPlan.Items.Add(new ListItem("Please Select", "-1"));

                if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.st)
                {
                    //January of the begining of a year references December of the Last Year
                    iRefPlanId += 1;
                    ddlRefPlan.Items.Add(new ListItem("December" + "-" + (DateTime.Now.Year - 1) + " ST-IAP", iRefPlanId.ToString()));
                    List<MonthlyIAP> mMonthlyIAP = RequestFormRequirement.getSTIAPReferencePlan();
                    foreach (MonthlyIAP mMonthly in mMonthlyIAP)
                    {
                        iRefPlanId += 1;
                        ddlRefPlan.Items.Add(new ListItem(mMonthly.m_sMonth, iRefPlanId.ToString()));
                    }
                }
                else if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.mt)
                {
                    //January - February of a year references December as the last quarter of Last Year until March when a new quarter begins
                    iRefPlanId += 1;
                    //ddlRefPlan.Items.Add(new ListItem("December" + "-" + (DateTime.Now.Year - 1) + " MT-IAP", iRefPlanId.ToString()));
                    List<QuaterlyIAP> mQuaterlyIAP = RequestFormRequirement.getMTIAPReferencePlan();
                    foreach (QuaterlyIAP mQuaterly in mQuaterlyIAP)
                    {
                        iRefPlanId += 1;
                        ddlRefPlan.Items.Add(new ListItem(mQuaterly.m_sQuarter, iRefPlanId.ToString()));
                    }
                    //ddlRefPlan.Items.Add(new ListItem("January" + "-" + (DateTime.Now.Year + 1) + " MT-IAP", iRefPlanId.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }





    //public static DataTable dtGetRequestByRequestNumber(string sRequestNumber, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getRequestByRequestNumber(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":sRequestNumber";
    //    param.Value = sRequestNumber;
    //    param.DbType = DbType.String;
    //    param.Size = 100;
    //    comm.Parameters.Add(param);

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    //public static aRequest objGetRequestByRequestNumber(string requestNumber, string field)
    //{
    //    //trim(TO_CHAR(ECTR_CTR.TOTALCOST, '999,999,999,999.99')) AS TOTALCOST

    //    DataTable dt = dtGetRequestByRequestNumber(requestNumber, field);

    //    aRequest o = new aRequest();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        o = new aRequest(dr);
    //    }
    //    return o;
    //}


    //public static void LoadRequestFormsByStatus(GridView requestsGridView, string field, int iStatus)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getRequestsByStatus(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = iStatus;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}


    //public static void LoadPendingRequestForms(GridView requestsGridView, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getPendingRequests(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus5";
    //    param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void LoadRequestsPendingMyApproval(GridView requestsGridView, int iUserId, string SortExpression, Label recordLabel, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iDefault;
    //    param.DbType = DbType.String;
    //    param.Size = 200;
    //    comm.Parameters.Add(param);

    //    //param = comm.CreateParameter();
    //    //param.ParameterName = ":STATUS2";
    //    //param.Value = RequestStatus.m_iDefault;
    //    //param.DbType = DbType.String;
    //    //param.Size = 200;
    //    //comm.Parameters.Add(param);

    //    //param = comm.CreateParameter();
    //    //param.ParameterName = ":REQUESTSTATUS";
    //    //param.Value = RequestStatus.m_iNotApproved;
    //    //param.DbType = DbType.String;
    //    //param.Size = 200;
    //    //comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, SortExpression);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        recordLabel.Text = "Records 1 to " + dt.Rows.Count + " of " + requestsGridView.PageCount + "";
    //    }
    //    else
    //    {
    //        recordLabel.Text = "No request found pending your approval/support.";
    //    }
    //}


    //public static void LoadRequestFormsIApproved(GridView requestsGridView, int iUserId, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        //requestsGridView.DataSource = dt;
    //        //requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}


    //public static void LoadRequestFormsIDidNotApprove(GridView requestsGridView, int iUserId, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.loadRequestsPendingMyApprovalOrIApproved(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iNotApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        //requestsGridView.DataSource = dt;
    //        //requestsGridView.DataBind();

    //        //StatusReporter.StatusReporterNotApproved(requestsGridView, dt, SortExpression);
    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //Request Owners


    //public static void LoadMyPendingRequestForms(GridView requestsGridView, int iUserId, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.myPendingRequestsForApproval(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus5";
    //    param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    //

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();
    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //    else
    //    {
    //        requestsGridView.Visible = false;
    //    }
    //}


    //public static void LoadMyApprovedRequestForms(GridView requestsGridView, int iUserId, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.loadMyApprovedRequests(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    //

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();
    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //    else
    //    {
    //        requestsGridView.Visible = false;
    //    }

    //}


    //public static void RejectedRequests(GridView requestsGridView, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.LoadRejectedRequests(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    //

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();
    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //    else
    //    {
    //        requestsGridView.Visible = false;
    //    }

    //}


    //public static void MyRejectedRequests(GridView requestsGridView, int iUserId, string field)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.LoadMyRejectedRequests(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iUserId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iNotApprovedByChairmanReviewBoard;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    //

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();
    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //    else
    //    {
    //        requestsGridView.Visible = false;
    //    }

    //}


    ////Filter Pending Requests
    //public static void FilterPendingRequest(GridView requestsGridView, string field, int iArea, int iLocation, int iPlanType, int iOriginator, int iFilteredStatus)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequests(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iOriginator;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iFilteredStatus";
    //    param.Value = iFilteredStatus;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequest(GridView requestsGridView, string field, int iArea, int iLocation, int iPlanType, int iOriginator)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByAreaLocationPlanTypeOriginator(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iOriginator;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequest(GridView requestsGridView, string field, int iArea, int iLocation, int iPlanType)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByAreaLocationPlanType(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequest(GridView requestsGridView, string field, int iArea, int iLocation)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByAreaLocation(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequest(GridView requestsGridView, string field, int iArea)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByArea(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequestByPlanType(GridView requestsGridView, string field, int iPlanType)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByPlanType(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequestByOriginator(GridView requestsGridView, string field, int iOriginator)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByOriginator(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iOriginator;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}

    //public static void FilterPendingRequestByStatus(GridView requestsGridView, string field, int iFilteredStatus)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredPendingRequestsByFilteredStatus(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus2";
    //    param.Value = RequestStatus.m_iNotApprovedByFunctionalAuthoriser;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus3";
    //    param.Value = RequestStatus.m_iNotApprovedByPlanner;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus4";
    //    param.Value = RequestStatus.m_iNotApprovedByProductionAssetAuthorizer;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iFilteredStatus";
    //    param.Value = iFilteredStatus;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //        //recordLabel.Text = "Records " + requestsGridView.SelectedIndex + " to " + rows + " of " + requestsGridView.PageCount + "";
    //        //recordLabel.Text = "Records 1 to " + rows + " of " + requestsGridView.PageCount + "";
    //    }
    //}


    ////Filter Approved Requests
    //public static void FilterApprovedRequestByArea(GridView requestsGridView, string field, int iArea)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByArea(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void FilterApprovedRequestByAreaLocation(GridView requestsGridView, string field, int iArea, int iLocation)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByAreaLocation(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void FilterApprovedRequestByAreaLocationPlanType(GridView requestsGridView, string field, int iArea, int iLocation, int iPlanType)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByAreaLocationPlanType(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void FilterApprovedRequestByAreaLocationPlanTypeOriginator(GridView requestsGridView, string field, int iArea, int iLocation, int iPlanType, int iOriginator)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByAreaLocationPlanTypeOriginator(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iArea";
    //    param.Value = iArea;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iLocation";
    //    param.Value = iLocation;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iOriginator;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void FilterApprovedRequestByPlanType(GridView requestsGridView, string field, int iPlanType)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByPlanType(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iPlanType";
    //    param.Value = iPlanType;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}

    //public static void FilterApprovedRequestByOriginator(GridView requestsGridView, string field, int iOriginator)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getFilteredApprovedRequestsByOriginator(field);

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":iStatus";
    //    param.Value = RequestStatus.m_iApproved;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iUserId";
    //    param.Value = iOriginator;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
    //    requestsGridView.Visible = false;
    //    if (dt.Rows.Count > 0)
    //    {
    //        requestsGridView.Visible = true;
    //        requestsGridView.DataSource = dt;
    //        requestsGridView.DataBind();

    //        StatusReporter.ExtStatusReporter(requestsGridView, dt, field);
    //    }
    //}


    //public static void LoadControls(DropDownList ddlArea, DropDownList ddlPlanType, DropDownList ddlOriginator, DropDownList ddlStatus)
    //{
    //    try
    //    {
    //        List<Area> lstAreas = RequestFormRequirement.getAreas();
    //        foreach (Area oArea in lstAreas)
    //        {
    //            ddlArea.Items.Add(new ListItem(oArea.m_sAREA, oArea.m_iIDAREA.ToString()));
    //        }

    //        List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
    //        foreach (PlanType planType in PlanTypes)
    //        {
    //            ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
    //        }

    //        List<Users> lstOriginators = UsersActions.lstGetOriginators();
    //        foreach (Users sOriginator in lstOriginators)
    //        {
    //            ddlOriginator.Items.Add(new ListItem(sOriginator.m_sFullName, sOriginator.m_iUserId.ToString()));
    //        }

    //        //Load Status
    //        //ddlStatus.Items.Add(new ListItem(RequestStatus.m_sActivityOwner, RequestStatus.m_iActivityOwner.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sIAPPlanner, RequestStatus.m_iIAPPlanner.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sFunctionalRepresentatives, RequestStatus.m_iFunctionalRepresentatives.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sProductionAssetRepresentative, RequestStatus.m_iProductionAssetRepresentative.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sChangeReviewBoardChairman, RequestStatus.m_iChangeReviewBoardChairman.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sNotApprovedByPlanner, RequestStatus.m_iNotApprovedByPlanner.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sNotApprovedByFunctionalAuthoriser, RequestStatus.m_iNotApprovedByFunctionalAuthoriser.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sNotApprovedByProductionAssetAuthorizer, RequestStatus.m_iNotApprovedByProductionAssetAuthorizer.ToString()));
    //        ddlStatus.Items.Add(new ListItem(RequestStatus.m_sNotApprovedByChairmanReviewBoard, RequestStatus.m_iNotApprovedByChairmanReviewBoard.ToString()));
    //        //ddlStatus.Items.Add(new ListItem(RequestStatus.m_sApproved, RequestStatus.m_iApproved.ToString()));
    //        //ddlStatus.Items.Add(new ListItem(RequestStatus.m_sCancelled, RequestStatus.m_iCancelled.ToString()));

    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}

    //public static void LoadControls(DropDownList ddlArea, DropDownList ddlPlanType, DropDownList ddlOriginator)
    //{
    //    try
    //    {
    //        List<Area> lstAreas = RequestFormRequirement.getAreas();
    //        foreach (Area oArea in lstAreas)
    //        {
    //            ddlArea.Items.Add(new ListItem(oArea.m_sAREA, oArea.m_iIDAREA.ToString()));
    //        }

    //        List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
    //        foreach (PlanType planType in PlanTypes)
    //        {
    //            ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
    //        }

    //        List<Users> lstOriginators = UsersActions.lstGetOriginators();
    //        foreach (Users sOriginator in lstOriginators)
    //        {
    //            ddlOriginator.Items.Add(new ListItem(sOriginator.m_sFullName, sOriginator.m_iUserId.ToString()));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}

    //public static void LoadControls(DropDownList ddlArea, DropDownList ddlPlanType)
    //{
    //    try
    //    {
    //        List<Area> lstAreas = RequestFormRequirement.getAreas();
    //        foreach (Area oArea in lstAreas)
    //        {
    //            ddlArea.Items.Add(new ListItem(oArea.m_sAREA, oArea.m_iIDAREA.ToString()));
    //        }

    //        List<PlanType> PlanTypes = RequestFormRequirement.getPlanTypes();
    //        foreach (PlanType planType in PlanTypes)
    //        {
    //            ddlPlanType.Items.Add(new ListItem(planType.m_sPLANTYPE, planType.m_iPLANTYPEID.ToString()));
    //        }           
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}
}