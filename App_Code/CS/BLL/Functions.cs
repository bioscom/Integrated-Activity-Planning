using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for Functions
/// </summary>
public class Functions
{
    public int m_iFUNCTIONID { get; set; }
    public string m_sFUNCTION { get; set; }

	public Functions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Functions(DataRow dr)
    {
        m_iFUNCTIONID = Int32.Parse(dr["FUNCTIONID"].ToString());
        m_sFUNCTION = dr["FUNCTION"].ToString();
    }
}


public static class IAPFunctions
{
    static IAPFunctions()
    {

    }

    public static List<Functions> getFunctions()
    {
        string sql = "SELECT FUNCTIONID, FUNCTION FROM IAP_FUNCTIONS ORDER BY FUNCTION";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Functions> result = new List<Functions>();
        foreach (DataRow row in dt.Rows)
        {
            Functions rowData = new Functions();
            rowData.m_iFUNCTIONID = int.Parse(row["FUNCTIONID"].ToString());
            rowData.m_sFUNCTION = row["FUNCTION"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static DataTable retrieveFunctions()
    {
        string sql = "SELECT FUNCTIONID, FUNCTION FROM IAP_FUNCTIONS";
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool InsertFunction(string sFunction)
    {
        string sql = "INSERT INTO IAP_FUNCTIONS (FUNCTION) VALUES (:FUNCTION)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTION";
        param.Value = sFunction;
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

    public static bool UpdateFunction(int iFunctionID, string sFunction)
    {
        string sql = "UPDATE IAP_FUNCTIONS SET FUNCTION = :FUNCTION WHERE FUNCTIONID = :FUNCTIONID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = iFunctionID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FUNCTION";
        param.Value = sFunction;
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

    public static bool DeleteFunction(int iFunctionID)
    {
        string sql = "DELETE FROM IAP_FUNCTIONS WHERE FUNCTIONID = :FUNCTIONID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = iFunctionID;
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