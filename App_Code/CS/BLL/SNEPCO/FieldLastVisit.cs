using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for FieldLastVisit
/// </summary>

public class FieldLastVisit
{
    public int m_iID_LAST_VISIT { get; set; }
    public string m_sLAST_VISIT { get; set; }

    public FieldLastVisit()
    {

    }

    public FieldLastVisit(DataRow dr)
    {
        m_iID_LAST_VISIT = int.Parse(dr["ID_LAST_VISIT"].ToString());
        m_sLAST_VISIT = dr["LAST_VISIT"].ToString();
    }

    public static DataTable dtGetLastVisit()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLastVisit();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetLastVisitById(int LastVisitId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLastVisitById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_LAST_VISIT";
        param.Value = LastVisitId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static FieldLastVisit objGetLastVisitById(int LastVisitId)
    {
        DataTable dt = dtGetLastVisitById(LastVisitId);

        FieldLastVisit xRows = new FieldLastVisit();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new FieldLastVisit(dr);
        }
        return xRows;
    }

    public static List<FieldLastVisit> lstGetLastVisit()
    {
        DataTable dt = dtGetLastVisit();

        List<FieldLastVisit> xRows = new List<FieldLastVisit>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new FieldLastVisit(dr));
        }
        return xRows;
    }

    public static bool createLastVisit(string sLAST_VISIT)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createLastVisit();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":LAST_VISIT";
        param.Value = sLAST_VISIT;
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

    public static bool updateLastVisit(int iLAST_VISIT, string sLAST_VISIT)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateLastVisit();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_LAST_VISIT";
        param.Value = iLAST_VISIT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":LAST_VISIT";
        param.Value = sLAST_VISIT;
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

}
