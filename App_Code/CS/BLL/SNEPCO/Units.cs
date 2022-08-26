using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for Units
/// </summary>

public class Units
{
    public int m_iID_UNIT { get; set; }
    public string m_sUNITS { get; set; }

    public Units()
    {

    }

    public Units(DataRow dr)
    {
        m_iID_UNIT = int.Parse(dr["ID_UNIT"].ToString());
        m_sUNITS = dr["UNITS"].ToString();
    }

    public static DataTable dtGetUnits()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUnits();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetUnitsById(int UnitsId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUnitsById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_UNIT";
        param.Value = UnitsId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static Units objGetUnitsById(int UnitsId)
    {
        DataTable dt = dtGetUnitsById(UnitsId);

        Units xRows = new Units();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new Units(dr);
        }
        return xRows;
    }

    public static List<Units> lstGetUnits()
    {
        DataTable dt = dtGetUnits();

        List<Units> xRows = new List<Units>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new Units(dr));
        }
        return xRows;
    }

    public static bool createUnits(string sUNITS)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createUnits();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":UNITS";
        param.Value = sUNITS;
        param.DbType = DbType.String;
        param.Size = 100;
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

    public static bool updateUnits(int iID_UNIT, string sUNITS)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateUnits();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_UNIT";
        param.Value = iID_UNIT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":UNITS";
        param.Value = sUNITS;
        param.DbType = DbType.String;
        param.Size = 100;
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
