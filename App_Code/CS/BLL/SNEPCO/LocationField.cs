using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for LocationField
/// </summary>

public class IAPPEC
{
    public int iIDPEC { get; set; }
    public string sPEC { get; set; }

    public enum PEC { VST = 1, ST = 2, MT = 3 };

    public static string PECDesc(PEC eWindow)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eWindow)
            {
                case PEC.MT: sRet = "MT 2 Years"; break;
                case PEC.ST: sRet = "ST 90 Days"; break;
                case PEC.VST: sRet = "VST 14/28"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    //public iapWindows eWindows
    //{
    //    get
    //    {
    //        return iapWindows.objGetIAPWindowById(m_iIAPWindowId);
    //    }
    //}

    public IAPPEC()
    {

    }

    public IAPPEC(DataRow dr)
    {
        iIDPEC = int.Parse(dr["IDPEC"].ToString());
        sPEC = dr["PEC"].ToString();
    }

    public static DataTable dtGetPec()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPec();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetPecById(int PecId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPecById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPEC";
        param.Value = PecId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetPEC()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPEC();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static IAPPEC objGetLocationFieldById(int LocationFieldId)
    {
        DataTable dt = dtGetPecById(LocationFieldId);

        IAPPEC xRows = new IAPPEC();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new IAPPEC(dr);
        }
        return xRows;
    }

    public static List<IAPPEC> lstGetLocationField()
    {
        DataTable dt = dtGetPec();

        List<IAPPEC> xRows = new List<IAPPEC>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new IAPPEC(dr));
        }
        return xRows;
    }

    public static bool createLocationField(string sPec)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createPec();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":PEC";
        param.Value = sPec;
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

    public static bool updateLocationField(int iPec, string sPec)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updatePec();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPEC";
        param.Value = iPec;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PEC";
        param.Value = sPec;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return (result != -1);
    }
}







public class LocationFieldActivityInfo
{
    public int m_iIDSTATUS { get; set; }
    public int m_iSTATUS { get; set; }
    public int m_iIDPEC { get; set; }
    public int m_iREQUESTID { get; set; }
   

    public IAPPEC eLocationField
    {
        get
        {
            return IAPPEC.objGetLocationFieldById(m_iIDPEC);
        }
    }

    public LocationFieldActivityInfo()
    {

    }

    public LocationFieldActivityInfo(DataRow dr)
    {
        m_iIDSTATUS = int.Parse(dr["IDSTATUS"].ToString());
        m_iIDPEC = int.Parse(dr["IDPEC"].ToString());
        m_iREQUESTID = int.Parse(dr["IDREQUEST"].ToString());
        m_iSTATUS = int.Parse(dr["STATUS"].ToString());
    }

    public static DataTable dtGetLocationFieldInfo()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLocationFieldActivityInfo();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetLocationFieldById(int LocationFieldId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLocationFieldActivityInfoById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_FIELD";
        param.Value = LocationFieldId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static LocationFieldActivityInfo objGetLocationFieldById(int LocationFieldId)
    {
        DataTable dt = dtGetLocationFieldById(LocationFieldId);

        LocationFieldActivityInfo xRows = new LocationFieldActivityInfo();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new LocationFieldActivityInfo(dr);
        }
        return xRows;
    }

    public static List<LocationFieldActivityInfo> lstGetLocationField()
    {
        DataTable dt = dtGetLocationFieldInfo();

        List<LocationFieldActivityInfo> xRows = new List<LocationFieldActivityInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new LocationFieldActivityInfo(dr));
        }
        return xRows;
    }

    public static DataTable dtGetIapPecStatusByRequestId(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getIapPecStatusByRequestId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //public static DataTable dtGetLocationFieldByRequestId(long lRequestId)
    //{
    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getLocationFieldByRequestId();

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":lRequestId";
    //    param.Value = lRequestId;
    //    param.DbType = DbType.Int64;
    //    comm.Parameters.Add(param);

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    public static LocationFieldActivityInfo objGetLocationFieldByActivityId(long iActivityId)
    {
        DataTable dt = dtGetIapPecStatusByRequestId(iActivityId);

        LocationFieldActivityInfo xRows = new LocationFieldActivityInfo();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new LocationFieldActivityInfo(dr);
        }
        return xRows;
    }

    public static List<LocationFieldActivityInfo> lstGetIapPecStatusByRequestId(long lRequestId)
    {
        DataTable dt = dtGetIapPecStatusByRequestId(lRequestId);

        List<LocationFieldActivityInfo> xRows = new List<LocationFieldActivityInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new LocationFieldActivityInfo(dr));
        }
        return xRows;
    }

    public static List<LocationFieldActivityInfo> lstGetLocationFieldByRequestId(long lRequestId)
    {
        DataTable dt = dtGetIapPecStatusByRequestId(lRequestId);

        List<LocationFieldActivityInfo> xRows = new List<LocationFieldActivityInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new LocationFieldActivityInfo(dr));
        }
        return xRows;
    }

    public static bool createRequestPecStatus(int iIDPEC, long iRequestId, int iSTATUS)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createRequestPecStatus();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPEC";
        param.Value = iIDPEC;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDREQUEST";
        param.Value = iRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iSTATUS;
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

    public static bool updateRequestPecStatus(int iIDPEC, long iRequestId, int iSTATUS)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateRequestPecStatus();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPEC";
        param.Value = iIDPEC;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iRequestId";
        param.Value = iRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iSTATUS;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return (result != -1);
    }
}