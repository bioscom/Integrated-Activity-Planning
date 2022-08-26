using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Reporting.WebForms;

public partial class Report_IAPReport : System.Web.UI.Page
{
    long lRequestId = 0;
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["RequestId"].ToString()))
            {
                lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());

                ReportGenerator("IAPReportSNEPCO.rdlc", "Report_RequestFormReport", IAPReport(lRequestId), lRequestId);
                //SubreportProcessingEvent Event
                rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEvent);
                rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PersonnelInformationSubReportProcessingEvent);
                rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PlanExecutionCriteriaSubReportProcessingEvent);

                //if (int.Parse(Request.QueryString["ouId"].ToString()) == (int)appOUs.ous.SNEPCO)
                //{
                //    ReportGenerator("IAPReportSNEPCO.rdlc", "Report_RequestFormReport", IAPReport(lRequestId), lRequestId);
                //    //SubreportProcessingEvent Event
                //    rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEvent);
                //    rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PersonnelInformationSubReportProcessingEvent);
                //    rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(PlanExecutionCriteriaSubReportProcessingEvent);
                //}
                //else
                //{
                //    ReportGenerator("IAPReport.rdlc", "Report_RequestFormReport", IAPReport(lRequestId), lRequestId);
                //    //SubreportProcessingEvent Event
                //    rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEvent);
                //}

                this.rptViewer.LocalReport.Refresh();
            }
        }
    }

    private DataTable IAPReport(long lRequestId)
    {
        string sql = "SELECT IAP_REQUESTFORM.REQUEST_NUMBER, IAP_OU.OU, IAP_AREA.AREA, IAP_LOCATIONS.LOCATION, IAP_REQUESTFORM.PROJECT_ACTIVITY, IAP_PLANTYPES.PLANTYPE, ";
        sql += "IAP_REQUESTFORM.PLANISSUE, IAP_CHANGE_TYPE.TYPE, IAP_REQUESTFORM.WO_NO, IAP_REQUESTFORM.ORIGINATOR, IAP_REQUESTFORM.PROPOSAL, IAP_REQUESTFORM.BENEFIT, ";
        sql += "TO_CHAR(IAP_REQUESTFORM.REQUESTDATE, 'DD-MON-YYYY') AS REQUESTDATE, IAP_REQUESTFORM.RISK, IAP_REQUESTFORM.PVOL, IAP_REQUESTFORM.NVOL, ";
        sql += "IAP_REQUESTFORM.PVOLGAS, IAP_REQUESTFORM.NVOLGAS, IAP_REQUESTFORM.PCOST, IAP_REQUESTFORM.NCOST, IAP_REQUESTFORM.IMPACT_ANALYSIS, IAP_USERS.FULLNAME, ";
        sql += "IAP_REQUESTFORM.ORIGINALFROM, IAP_REQUESTFORM.ORIGINALTO, IAP_REQUESTFORM.REQUESTEDFROM, IAP_REQUESTFORM.REQUESTEDTO, IAP_REQUESTFORM.STATUS, ";
        sql += "IAP_REQUESTFORM.PRIMAVERA_ACTIVITYID ";
        sql += "FROM IAP_REQUESTFORM ";
        sql += "INNER JOIN IAP_USERS ON IAP_REQUESTFORM.IDUSER = IAP_USERS.IDUSER ";
        sql += "INNER JOIN IAP_CHANGE_TYPE ON IAP_REQUESTFORM.IDCHANGE = IAP_CHANGE_TYPE.IDCHANGE ";
        sql += "INNER JOIN IAP_LOCATIONS ON IAP_REQUESTFORM.LOCATIONID = IAP_LOCATIONS.LOCATIONID ";
        sql += "INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA ";
        sql += "INNER JOIN IAP_PLANTYPES ON IAP_REQUESTFORM.PLANTYPEID = IAP_PLANTYPES.PLANTYPEID ";
        sql += "INNER JOIN IAP_OU ON IAP_AREA.IDOU = IAP_OU.IDOU ";
        sql += "WHERE (IAP_REQUESTFORM.IDREQUEST = :lRequestId)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    private DataTable SubReport(long lRequestId)
    {
        string sql = "SELECT IAP_COMMENTS.COMMENTS, IAP_COMMENTS.IDREQUEST, TO_CHAR(IAP_COMMENTS.COMMENTSDATE, 'DD-MON-YYYY')COMMENTSDATE, IAP_COMMENTS.STATUS, ";
        sql += "TO_CHAR(IAP_COMMENTS.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, IAP_USERS.FULLNAME ||' - '|| IAP_FUNCTIONS.FUNCTION AS NAMEFUNCTION, IAP_USERROLES.ROLE ";
        sql += "FROM IAP_USERS, IAP_USERROLES, IAP_COMMENTS, IAP_REQUESTFORM, IAP_FUNCTIONS WHERE IAP_USERS.ROLEID = IAP_USERROLES.ROLEID ";
        sql += "AND IAP_USERS.IDUSER = IAP_COMMENTS.IDUSER AND IAP_COMMENTS.IDREQUEST = IAP_REQUESTFORM.IDREQUEST ";
        sql += "AND IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID AND IAP_COMMENTS.IDREQUEST = :lRequestId ORDER BY IAP_USERROLES.ROLEID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    private DataTable SubReportPersonnelInformation(long lRequestId)
    {
        string sql = "SELECT IAP_REQUESTFORM.IDREQUEST, IAP_FPSO_CONTACT.ID_CONTACT, IAP_FPSO_CONTACT.CONTACT_PERSON, IAP_LAST_VISIT.ID_LAST_VISIT, ";
        sql += "IAP_LAST_VISIT.LAST_VISIT, IAP_VISA_TYPE.ID_VISA, IAP_VISA_TYPE.VISA_TYPE, IAP_PERSONNEL.ID_PERSONNEL, IAP_PERSONNEL.EMPLOYEE_NAME, ";
        sql += "IAP_PERSONNEL.GENDER, IAP_PERSONNEL.COMPANY, IAP_PERSONNEL.BOSIET_VALID, IAP_PERSONNEL.MEDICAL ";
        sql += "FROM IAP_REQUESTFORM, IAP_PERSONNEL, IAP_FPSO_CONTACT, IAP_LAST_VISIT, IAP_VISA_TYPE ";
        sql += "WHERE IAP_REQUESTFORM.IDREQUEST = IAP_PERSONNEL.IDREQUEST AND IAP_PERSONNEL.ID_CONTACT = IAP_FPSO_CONTACT.ID_CONTACT AND ";
        sql += "IAP_PERSONNEL.ID_LAST_VISIT = IAP_LAST_VISIT.ID_LAST_VISIT AND IAP_PERSONNEL.ID_VISA = IAP_VISA_TYPE.ID_VISA ";
        sql += "AND IAP_REQUESTFORM.IDREQUEST = :lRequestId";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    private DataTable SubReportPlanExecutionCriteria(long lRequestId)
    {
        string sql = "SELECT IAP_REQUESTFORM.IDREQUEST, IAP_PEC.IDPEC, IAP_PEC.PEC, IAP_PEC_STATUS.STATUS FROM IAP_REQUESTFORM ";
        sql += "INNER JOIN IAP_PEC_STATUS ON IAP_REQUESTFORM.IDREQUEST = IAP_PEC_STATUS.IDREQUEST ";
        sql += "INNER JOIN IAP_PEC ON IAP_PEC.IDPEC = IAP_PEC_STATUS.IDPEC ";
        sql += "AND IAP_REQUESTFORM.IDREQUEST = :lRequestId";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    void myDrillthroughEventHandler(object sender, DrillthroughEventArgs e)
    {
        LocalReport localReport = (LocalReport)e.Report;
    }

    private void ReportGenerator(string ReportFileName, string rptDataSource, DataTable rptDataTable, long lRequestId)
    {
        rptViewer.Reset();
        rptViewer.LocalReport.DataSources.Clear();
        rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report/rptDocument/" + ReportFileName);

        // Add the handler for drillthrough.
        rptViewer.Drillthrough += new DrillthroughEventHandler(myDrillthroughEventHandler);

        // Supply a DataTable corresponding to each report data source.
        rptViewer.LocalReport.DataSources.Add(new ReportDataSource(rptDataSource, rptDataTable));

        //Generate Parameters to pass to the report
        ReportParameter[] oReportParams = new ReportParameter[1];
        string rptTitle = "";
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(lRequestId, sfield);

        if (oRequest.m_sOU.ToUpper() == appOUs.ouDesc((appOUs.ous)appOUs.ous.SNEPCO).ToUpper())
        {
            rptTitle = appOUs.ouDesc((appOUs.ous)appOUs.ous.SNEPCO);
        }
        else
        {
            rptTitle = appOUs.ouDesc((appOUs.ous)appOUs.ous.SPDC);
        }

        rptTitle += " Integrated Activity Plan Change Request Form";
        oReportParams[0] = new ReportParameter("Report_Title", rptTitle);
        rptViewer.LocalReport.SetParameters(oReportParams);
        rptViewer.LocalReport.Refresh();
    }


    //'''  <summary>
    //''' When the subreport is being processed/loaded, fills the datasource
    //'''  </summary>
    //SubreportProcessingEvent Event
    void SubreportProcessingEvent(Object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            //DataTable dt = SubReport(sRequestNumber);
            lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            ReportDataSource rptDataSource = new ReportDataSource("Report_ApproversComments", SubReport(lRequestId)); //programmer's code
            e.DataSources.Add(rptDataSource);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    void PersonnelInformationSubReportProcessingEvent(Object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            ReportDataSource rptDataSource = new ReportDataSource("Report_RequestPersonnelInformation", SubReportPersonnelInformation(lRequestId));
            e.DataSources.Add(rptDataSource);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    void PlanExecutionCriteriaSubReportProcessingEvent(Object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            lRequestId = long.Parse(Request.QueryString["RequestId"].ToString());
            ReportDataSource rptDataSource = new ReportDataSource("Report_RequestPECStatus", SubReportPlanExecutionCriteria(lRequestId));
            e.DataSources.Add(rptDataSource);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}
