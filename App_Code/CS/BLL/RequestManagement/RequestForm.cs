using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Oracle.ManagedDataAccess.Client;

public class Yearly
{
    public int m_iYear { get; set; }

    public Yearly()
    {

    }

    public Yearly(DataRow dr)
    {
        m_iYear = int.Parse(dr["YYEAR"].ToString());
    }
}

public class RequestLst
{
    public long m_lRequestId { get; set; }
    public string m_sRequestNo { get; set; }
    public string m_sProject { get; set; }
   
    public RequestLst()
    {

    }

    public RequestLst(DataRow dr)
    {
        m_lRequestId = long.Parse(dr["IDREQUEST"].ToString());
        m_sRequestNo = dr["REQUEST_NUMBER"].ToString();
        m_sProject = dr["PROJECT_ACTIVITY"].ToString();
    }
}

public class Monthly
{
    public string m_sMonth { get; set; }
    public int m_iMonth { get; set; }
}

public class MonthlyIAP
{
    public string m_sMonth { get; set; }
}

public class QuaterlyIAP
{
    public string m_sQuarter { get; set; }
}

public class OU
{
    public int m_iIDOU { get; set; }
    public string m_sOU { get; set; }
}

public class ChangeType
{
    public int m_iIDCHANGE { get; set; }
    public string m_sTYPE { get; set; }
}

public class PlanType
{
    public int m_iPLANTYPEID { get; set; }
    public string m_sPLANTYPE { get; set; }
}

public class ImpactAnalysisWindowDetail
{
    public int m_iWINDOWID { get; set; }
    public string m_sWINDOWTYPE { get; set; }
    public string m_sXTYPE { get; set; }
    public int m_iXRANGE { get; set; }

    public ImpactAnalysisWindowDetail()
    {

    }

    public ImpactAnalysisWindowDetail(DataRow dr)
    {
        m_iWINDOWID = int.Parse(dr["WINDOWID"].ToString());
        m_sWINDOWTYPE = dr["WINDOW_TYPE"].ToString();
        m_sXTYPE = dr["XTYPE"].ToString();
        m_iXRANGE = int.Parse(dr["XRANGE"].ToString());
    }
}

public class xImpactAnalysis
{
    public int m_iIMPACTID { get; set; }
    public string m_sIMPACT { get; set; }
    public string m_sREQUEST_NUMBER { get; set; }
    public string m_sACTIVTY { get; set; }
    public string m_sREASONFORCHANGE { get; set; }
    public string m_sOVERALLRATING { get; set; }
    public string m_sMBOPD { get; set; }
    public string m_sMMSCFPD { get; set; }
    public string m_sREMARKS { get; set; }
    public string m_sLOGIMPACT { get; set; }
    public string m_sCPIMPACT { get; set; }
    public string m_sSECIMPACT { get; set; }
    public string m_sHSSEIMPACT { get; set; }
    public string m_sFTOLTOIMPACT { get; set; }
    public string m_sLEGALIMPACT { get; set; }
    public string m_sFUNCTIONALIMPACT { get; set; }

    public xImpactAnalysis()
    {

    }
    public xImpactAnalysis(DataRow dr)
    {
        m_iIMPACTID = int.Parse(dr["IMPACTID"].ToString());
        m_sREQUEST_NUMBER = dr["REQUEST_NUMBER"].ToString();
        m_sACTIVTY = dr["ACTIVTY"].ToString();
        m_sREASONFORCHANGE = dr["REASONFORCHANGE"].ToString();
        m_sOVERALLRATING = dr["OVERALLRATING"].ToString();
        m_sMBOPD = dr["MBOPD"].ToString();
        m_sMMSCFPD = dr["MMSCFPD"].ToString();
        m_sREMARKS = dr["REMARKS"].ToString();
        m_sLOGIMPACT = dr["LOGIMPACT"].ToString();
        m_sCPIMPACT = dr["CPIMPACT"].ToString();
        m_sSECIMPACT = dr["SECIMPACT"].ToString();
        m_sHSSEIMPACT = dr["HSSEIMPACT"].ToString();
        m_sFTOLTOIMPACT = dr["FTOLTOIMPACT"].ToString();
        m_sLEGALIMPACT = dr["LEGALIMPACT"].ToString();
        m_sFUNCTIONALIMPACT = dr["FUNCTIONALIMPACT"].ToString();
    }
}

public class Location
{
    public int m_iLOCATIONID { get; set; }
    public string m_sLOCATION { get; set; }
    public int m_iIDAREA { get; set; }
    public string m_sAREA { get; set; }
}

public class Area
{
    public int m_iIDAREA { get; set; }
    public string m_sAREA { get; set; }
}

public class RequestFormRequirement
{
    public RequestFormRequirement()
    {

    }

    public static DataTable retrieveOUS()
    {
        string sql = "SELECT IDOU, OU FROM IAP_OU";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable retrieveAreas()
    {
        string sql = "SELECT IDAREA, AREA FROM IAP_AREA ORDER BY AREA";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable retrieveLocations()
    {
        string sql = "SELECT IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_AREA.IDAREA, IAP_AREA.AREA ";
        sql += "FROM  IAP_LOCATIONS INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA ORDER BY IAP_AREA.AREA";
                         
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getLocationByArea(int IDArea)
    {
        string sql = "SELECT IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_LOCATIONS.IDAREA, IAP_AREA.AREA ";
        sql += "FROM  IAP_LOCATIONS INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA WHERE IAP_AREA.IDAREA = :IDAREA ORDER BY IAP_LOCATIONS.LOCATION";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = IDArea;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable retrievePlanTypes()
    {
        string sql = "SELECT PLANTYPEID, PLANTYPE FROM IAP_PLANTYPES ORDER BY PLANTYPE";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable retrieveChangeTypes()
    {
        string sql = "SELECT IDCHANGE, TYPE FROM IAP_CHANGE_TYPE ORDER BY TYPE";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<OU> getOU()
    {
        string sql = "SELECT IDOU, OU FROM IAP_OU";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<OU> result = new List<OU>();
        foreach (DataRow row in dt.Rows)
        {
            OU rowData = new OU();
            rowData.m_iIDOU = int.Parse(row["IDOU"].ToString());
            rowData.m_sOU = row["OU"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Area> getAreas()
    {
        string sql = "SELECT IDAREA, AREA FROM IAP_AREA ORDER BY AREA";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Area> result = new List<Area>();
        foreach (DataRow row in dt.Rows)
        {
            Area rowData = new Area();
            rowData.m_iIDAREA = int.Parse(row["IDAREA"].ToString());
            rowData.m_sAREA = row["AREA"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Area> getAreasByOU(int IDOU)
    {
        string sql = "SELECT IDAREA, AREA FROM IAP_AREA WHERE IDOU = :IDOU";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDOU";
        param.Value = IDOU;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Area> result = new List<Area>();
        foreach (DataRow row in dt.Rows)
        {
            Area rowData = new Area();
            rowData.m_iIDAREA = int.Parse(row["IDAREA"].ToString());
            rowData.m_sAREA = row["AREA"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Location> getLocations()
    {
        string sql = "SELECT IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_LOCATIONS.IDAREA, IAP_AREA.AREA ";
        sql += "FROM IAP_AREA INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ORDER BY IAP_LOCATIONS.LOCATION";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Location> result = new List<Location>();
        foreach (DataRow row in dt.Rows)
        {
            Location rowData = new Location();

            rowData.m_iLOCATIONID = int.Parse(row["LOCATIONID"].ToString());
            rowData.m_sLOCATION = row["LOCATION"].ToString();
            rowData.m_iIDAREA = int.Parse(row["IDAREA"].ToString());
            rowData.m_sAREA = row["AREA"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Location> getLocationsByArea(int IDArea)
    {
        string sql = "SELECT IAP_LOCATIONS.LOCATIONID, IAP_LOCATIONS.LOCATION, IAP_LOCATIONS.IDAREA, IAP_AREA.AREA ";
        sql += "FROM  IAP_LOCATIONS INNER JOIN IAP_AREA ON IAP_LOCATIONS.IDAREA = IAP_AREA.IDAREA WHERE IAP_AREA.IDAREA = :IDAREA ORDER BY IAP_LOCATIONS.LOCATION";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = IDArea;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Location> result = new List<Location>();
        foreach (DataRow row in dt.Rows)
        {
            Location rowData = new Location();

            rowData.m_iLOCATIONID = int.Parse(row["LOCATIONID"].ToString());
            rowData.m_sLOCATION = row["LOCATION"].ToString();
            rowData.m_iIDAREA = int.Parse(row["IDAREA"].ToString());
            rowData.m_sAREA = row["AREA"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<ChangeType> getChangeTypes()
    {
        string sql = "SELECT IDCHANGE, TYPE FROM IAP_CHANGE_TYPE";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<ChangeType> result = new List<ChangeType>();
        foreach (DataRow row in dt.Rows)
        {
            ChangeType rowData = new ChangeType();
            rowData.m_iIDCHANGE = int.Parse(row["IDCHANGE"].ToString());
            rowData.m_sTYPE = row["TYPE"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<PlanType> getPlanTypes()
    {
        string sql = "SELECT PLANTYPEID, PLANTYPE FROM IAP_PLANTYPES ORDER BY PLANTYPE";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<PlanType> result = new List<PlanType>();
        foreach (DataRow row in dt.Rows)
        {
            PlanType rowData = new PlanType();
            rowData.m_iPLANTYPEID = int.Parse(row["PLANTYPEID"].ToString());
            rowData.m_sPLANTYPE = row["PLANTYPE"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<MonthlyIAP> getSTIAPReferencePlan()
    {
        int i = 0;
        string[] sMonthlyIAP = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        List<MonthlyIAP> result = new List<MonthlyIAP>();
        foreach (String s in sMonthlyIAP)
        {
            MonthlyIAP rowData = new MonthlyIAP();

            rowData.m_sMonth = sMonthlyIAP[i] + "-" + DateTime.Now.Year + " ST-IAP";
            result.Add(rowData);
            i++;
        }
        return result;
    }

    public static DataTable getYear()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getYear();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<Yearly> LoadYears()
    {
        DataTable dt = getYear();

        List<Yearly> xRows = new List<Yearly>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new Yearly(dr));
        }
        return xRows;
    }

    public static List<Monthly> LoadMonths()
    {
        int i = 0;
        string[] sMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        int[] iValues = {01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12 };

        List<Monthly> result = new List<Monthly>();
        foreach (String s in sMonths)
        {
            Monthly xRows = new Monthly();

            xRows.m_sMonth = sMonths[i];
            xRows.m_iMonth = iValues[i];

            result.Add(xRows);
            i++;
        }
        return result;
    }

    public static DataTable getRequestByMonthYear(int iYear, int iMonth)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getRequetByMonthYear();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iYear";
        param.Value = iYear;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iMonth";
        param.Value = iMonth;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);        
    }

    public static List<RequestLst> lstGetRequestByMonthYear(int iYear, int iMonth)
    {
        DataTable dt = getRequestByMonthYear(iYear, iMonth);

        List<RequestLst> xRows = new List<RequestLst>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new RequestLst(dr));
        }
        return xRows;
    }

    public static List<QuaterlyIAP> getMTIAPReferencePlan()
    {
        //This is a new update specified by Alepaye Babatunde on 26/06/2015
        int i = 0;
        //string[] sQuaterlyIAP = {"March", "June", "September", "December" };
        string[] sQuaterlyIAP = { "January", "April", "July", "October" };

        
        List<QuaterlyIAP> result = new List<QuaterlyIAP>();
        foreach (String s in sQuaterlyIAP)
        {
            QuaterlyIAP rowData = new QuaterlyIAP();
            rowData.m_sQuarter = sQuaterlyIAP[i] + "-" + DateTime.Now.Year + " MT-IAP";
            result.Add(rowData);
            i++;
        }

        i = 0;
        foreach (String s in sQuaterlyIAP)
        {
            QuaterlyIAP rowData = new QuaterlyIAP();
            rowData.m_sQuarter = sQuaterlyIAP[i] + "-" + (DateTime.Now.Year + 1) + " MT-IAP";
            result.Add(rowData);
            i++;
        }

        i = 0;
        foreach (String s in sQuaterlyIAP)
        {
            QuaterlyIAP rowData = new QuaterlyIAP();
            rowData.m_sQuarter = sQuaterlyIAP[i] + "-" + (DateTime.Now.Year + 2) + " MT-IAP";
            result.Add(rowData);
            i++;
        }

        i = 0;
        foreach (String s in sQuaterlyIAP)
        {
            QuaterlyIAP rowData = new QuaterlyIAP();
            rowData.m_sQuarter = sQuaterlyIAP[i] + "-" + (DateTime.Now.Year + 3) + " MT-IAP";
            result.Add(rowData);
            break;
        }


        return result;
    }

    public static bool InsertOU(string sOU)
    {
        string sql = "INSERT INTO IAP_OU (OU) VALUES (:OU)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":OU";
        param.Value = sOU;
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

    public static bool InsertChangeType(string sType)
    {
        string sql = "INSERT INTO IAP_CHANGE_TYPE (TYPE) VALUES (:TYPE)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":TYPE";
        param.Value = sType;
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

    public static bool InsertPlanType(string sPlanType)
    {
        string sql = "INSERT INTO IAP_PLANTYPES (PLANTYPE) VALUES (:PLANTYPE)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPE";
        param.Value = sPlanType;
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

    public static bool InsertLocation(string sLocation, int AreaID)
    {
        string sql = "INSERT INTO IAP_LOCATIONS (LOCATION, IDAREA) VALUES (:LOCATION, :IDAREA)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":LOCATION";
        param.Value = sLocation;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = AreaID;
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

    public static bool InsertArea(string sArea)
    {
        string sql = "INSERT INTO IAP_AREA (AREA) VALUES (:AREA)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":AREA";
        param.Value = sArea;
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

    public static bool UpdateOU(int OUID, string sOU)
    {
        string sql = "UPDATE IAP_OU SET OU = :OU WHERE IDOU = :IDOU";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDOU";
        param.Value = OUID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":OU";
        param.Value = sOU;
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

    public static bool UpdateChangeType(int ChangeID, string sType)
    {
        string sql = "UPDATE IAP_CHANGE_TYPE SET TYPE = :TYPE WHERE IDCHANGE = :IDCHANGE";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDCHANGE";
        param.Value = ChangeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TYPE";
        param.Value = sType;
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

    public static bool UpdatePlanType(string sPlanType, int PlanTypeID)
    {
        string sql = "UPDATE IAP_PLANTYPES SET PLANTYPE = :PLANTYPE WHERE PLANTYPE = :PLANTYPEID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPEID";
        param.Value = PlanTypeID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPE";
        param.Value = sPlanType;
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

    public static bool UpdateLocation(int LocationID, string sLocation, int AreaID)
    {
        string sql = "UPDATE IAP_LOCATIONS SET LOCATION = :LOCATION, IDAREA = :IDAREA WHERE LOCATIONID = :LOCATIONID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":LOCATION";
        param.Value = sLocation;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = AreaID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LOCATIONID";
        param.Value = LocationID;
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

    public static bool UpdateArea(int AreaID, string sArea)
    {
        string sql = "UPDATE IAP_AREA SET AREA = :AREA WHERE IDAREA = :IDAREA";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = AreaID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":AREA";
        param.Value = sArea;
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

    public static bool DeleteOU(int OUID)
    {
        string sql = "DELETE FROM IAP_OU WHERE IDOU = :IDOU";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDOU";
        param.Value = OUID;
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

    public static bool DeleteChangeType(int ChangeID)
    {
        string sql = "DELETE FROM IAP_CHANGE_TYPE WHERE IDCHANGE = :IDCHANGE";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDCHANGE";
        param.Value = ChangeID;
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

    public static bool DeletePlanType(int PlanTypeID)
    {
        string sql = "DELETE FROM IAP_PLANTYPES WHERE PLANTYPEID = :PLANTYPEID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":PLANTYPEID";
        param.Value = PlanTypeID;
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

    public static bool DeleteLocation(int LocationID)
    {
        string sql = "DELETE FROM IAP_LOCATIONS WHERE LOCATIONID = :LOCATIONID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":LOCATIONID";
        param.Value = LocationID;
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

    public static bool DeleteArea(int AreaID)
    {
        string sql = "DELETE FROM IAP_AREA WHERE IDAREA = :IDAREA";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDAREA";
        param.Value = AreaID;
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

}

public class ImpactAnalysis
{

    public static List<xImpactAnalysis> getImpactAnalysis()
    {
        string sql = "SELECT IMPACTID, REQUEST_NUMBER, ACTIVTY, REASONFORCHANGE, OVERALLRATING, MBOPD, MMSCFPD, REMARKS, ";
        sql += "LOGIMPACT, CPIMPACT, SECIMPACT, HSSEIMPACT, FTOLTOIMPACT, LEGALIMPACT, FUNCTIONALIMPACT FROM IAP_IMPACT_ANALYSIS";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<xImpactAnalysis> result = new List<xImpactAnalysis>();
        foreach (DataRow row in dt.Rows)
        {
            xImpactAnalysis rowData = new xImpactAnalysis();
            rowData.m_iIMPACTID = int.Parse(row["IMPACTID"].ToString());

            rowData.m_sREQUEST_NUMBER = row["REQUEST_NUMBER"].ToString();
            rowData.m_sACTIVTY = row["ACTIVTY"].ToString();
            rowData.m_sREASONFORCHANGE = row["REASONFORCHANGE"].ToString();
            rowData.m_sOVERALLRATING = row["OVERALLRATING"].ToString();
            rowData.m_sMBOPD = row["MBOPD"].ToString();
            rowData.m_sMMSCFPD = row["MMSCFPD"].ToString();
            rowData.m_sREMARKS = row["REMARKS"].ToString();
            rowData.m_sLOGIMPACT = row["LOGIMPACT"].ToString();
            rowData.m_sCPIMPACT = row["CPIMPACT"].ToString();
            rowData.m_sSECIMPACT = row["SECIMPACT"].ToString();
            rowData.m_sHSSEIMPACT = row["HSSEIMPACT"].ToString();
            rowData.m_sFTOLTOIMPACT = row["FTOLTOIMPACT"].ToString();
            rowData.m_sLEGALIMPACT = row["LEGALIMPACT"].ToString();
            rowData.m_sFUNCTIONALIMPACT = row["FUNCTIONALIMPACT"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static DataTable retrieveImpactAnalysis(string sRequest_Nunber)
    {
        string sql = "SELECT IMPACTID, REQUEST_NUMBER, ACTIVTY, REASONFORCHANGE, OVERALLRATING, MBOPD, MMSCFPD, REMARKS, ";
        sql += "LOGIMPACT, CPIMPACT, SECIMPACT, HSSEIMPACT, FTOLTOIMPACT, LEGALIMPACT, FUNCTIONALIMPACT FROM IAP_IMPACT_ANALYSIS WHERE REQUEST_NUMBER = :REQUEST_NUMBER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":REQUEST_NUMBER";
        param.Value = sRequest_Nunber;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool InsertImpactAnalysis(string sREQUEST_NUMBER, string sACTIVTY, string sREASONFORCHANGE, string sOVERALLRATING, 
        string sMBOPD, string sMMSCFPD, string sREMARKS, string sLOGIMPACT, string sCPIMPACT, string sSECIMPACT, string sHSSEIMPACT, 
        string sFTOLTOIMPACT, string sLEGALIMPACT, string sFUNCTIONALIMPACT)
    {
        string sql = "INSERT INTO IAP_IMPACT_ANALYSIS (REQUEST_NUMBER, ACTIVTY, REASONFORCHANGE, OVERALLRATING, MBOPD, ";
        sql += "MMSCFPD, REMARKS, LOGIMPACT, CPIMPACT, SECIMPACT, HSSEIMPACT, FTOLTOIMPACT, LEGALIMPACT, FUNCTIONALIMPACT) ";
        sql += "VALUES (:REQUEST_NUMBER, :ACTIVTY, :REASONFORCHANGE, :OVERALLRATING, :MBOPD, :MMSCFPD, :REMARKS, :LOGIMPACT, ";
        sql += ":CPIMPACT, :SECIMPACT, :HSSEIMPACT, :FTOLTOIMPACT, :LEGALIMPACT, :FUNCTIONALIMPACT)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":REQUEST_NUMBER";
        param.Value = sREQUEST_NUMBER;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ACTIVTY";
        param.Value = sACTIVTY;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REASONFORCHANGE";
        param.Value = sREASONFORCHANGE;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":OVERALLRATING";
        param.Value = sOVERALLRATING;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":MBOPD";
        param.Value = sMBOPD;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":MMSCFPD";
        param.Value = sMMSCFPD;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REMARKS";
        param.Value = sREMARKS;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LOGIMPACT";
        param.Value = sLOGIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":CPIMPACT";
        param.Value = sCPIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SECIMPACT";
        param.Value = sSECIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":HSSEIMPACT";
        param.Value = sHSSEIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FTOLTOIMPACT";
        param.Value = sFTOLTOIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LEGALIMPACT";
        param.Value = sLEGALIMPACT;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONALIMPACT";
        param.Value = sFUNCTIONALIMPACT;
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

    public static bool UpdateImpactAnalysis(int ImpactID, string sImpact)
    {
        string sql = "UPDATE IAP_IMPACT_ANALYSIS SET IMPACT = :IMPACT WHERE IMPACTID = :IMPACTID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IMPACTID";
        param.Value = ImpactID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IMPACT";
        param.Value = sImpact;
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

    public static bool DeleteImpactAnalysis(int ImpactID)
    {
        string sql = "DELETE FROM IAP_IMPACT_ANALYSIS WHERE IMPACTID = :IMPACTID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IMPACTID";
        param.Value = ImpactID;
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


    public static List<ImpactAnalysisWindowDetail> getImpactAnalysisWindow()
    {
        string sql = "SELECT WINDOWID, WINDOW_TYPE, XTYPE, XRANGE FROM IAP_IMPACTANALYSIS_WINDOW";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<ImpactAnalysisWindowDetail> result = new List<ImpactAnalysisWindowDetail>();
        foreach (DataRow row in dt.Rows)
        {
            ImpactAnalysisWindowDetail rowData = new ImpactAnalysisWindowDetail();
            rowData.m_iWINDOWID = int.Parse(row["WINDOWID"].ToString());
            rowData.m_sWINDOWTYPE = row["WINDOW_TYPE"].ToString();
            rowData.m_sXTYPE = row["XTYPE"].ToString();
            rowData.m_iXRANGE = int.Parse(row["XRANGE"].ToString());

            result.Add(rowData);
        }
        return result;
    }

    public static bool InsertImpactAnalysisWin(string sWinType, string sWinDesc, string sType, int iRange)
    {
        string sql = "INSERT INTO IAP_IMPACTANALYSIS_WINDOW (WINDOW_TYPE, WINDESC, XTYPE, XRANGE) VALUES (:WINDOW_TYPE, :XTYPE, :XRANGE)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":WINDOW_TYPE";
        param.Value = sWinType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":WINDESC";
        param.Value = sWinDesc;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":XTYPE";
        param.Value = sType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":XRANGE";
        param.Value = iRange;
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

    public static bool UpdateImpactAnalysisWin(int WindowID, string sWinType, string sWinDesc, string WinDesc, string sType, int iRange)
    {
        string sql = "UPDATE IAP_IMPACTANALYSIS_WINDOW SET WINDOW_TYPE = :WINDOW_TYPE, WINDESC = :WINDESC XTYPE = :XTYPE, XRANGE = :XRANGE WHERE WINDOWID = :WINDOWID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":WINDOWID";
        param.Value = WindowID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":WINDOW_TYPE";
        param.Value = sType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":WINDESC";
        param.Value = sWinDesc;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":XTYPE";
        param.Value = sType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":XRANGE";
        param.Value = sType;
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

    public static DataTable retrieveImpactAnalysisWin()
    {
        string sql = "SELECT WINDOWID, WINDOW_TYPE, WINDESC, XTYPE, XRANGE FROM IAP_IMPACTANALYSIS_WINDOW";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}

public class appOUs
{
    public enum ous
    {
        SPDC = 1,
        SNEPCO = 2,
    };

    public static string ouDesc(ous eOU)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eOU)
            {
                case ous.SPDC: sRet = "SPDC"; break;
                case ous.SNEPCO: sRet = "SNEPCO"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}
