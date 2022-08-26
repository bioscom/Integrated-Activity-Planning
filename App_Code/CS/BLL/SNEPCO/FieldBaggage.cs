using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for FieldBaggage
/// </summary>
public class FieldBaggage
{
    public int m_iID_BAG { get; set; }
    public string m_sBAGGAGE { get; set; }

	public FieldBaggage()
	{
		
	}

    public FieldBaggage(DataRow dr)
    {
        m_iID_BAG = int.Parse(dr["ID_BAG"].ToString());
        m_sBAGGAGE = dr["BAGGAGE"].ToString();
    }

    public static DataTable dtGetBaggage()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getBaggage();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetBaggageById(int BaggageId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getBaggageById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_BAG";
        param.Value = BaggageId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static FieldBaggage objGetBaggageId(int BaggageId)
    {
        DataTable dt = dtGetBaggageById(BaggageId);

        FieldBaggage xRows = new FieldBaggage();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new FieldBaggage(dr);
        }
        return xRows;
    }

    public static List<FieldBaggage> lstGetBaggage()
    {
        DataTable dt = dtGetBaggage();

        List<FieldBaggage> xRows = new List<FieldBaggage>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new FieldBaggage(dr));
        }
        return xRows;
    }

    public static bool createBaggage(string sBAGGAGE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createBaggage();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":BAGGAGE";
        param.Value = sBAGGAGE;
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

    public static bool updateBaggage(int iBAG, string sBAGGAGE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateBaggage();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_BAG";
        param.Value = iBAG;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":BAGGAGE";
        param.Value = sBAGGAGE;
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
