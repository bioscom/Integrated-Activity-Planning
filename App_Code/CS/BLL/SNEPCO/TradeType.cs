using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for TradeType
/// </summary>

public class TradeType
{
    public int m_iID_TRADE_TYPE { get; set; }
    public string m_sTRADE_TYPE { get; set; }

    public TradeType()
    {

    }

    public TradeType(DataRow dr)
    {
        m_iID_TRADE_TYPE = int.Parse(dr["ID_TRADE_TYPE"].ToString());
        m_sTRADE_TYPE = dr["TRADE_TYPE"].ToString();
    }

    public static DataTable dtGetTradeType()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getTradeType();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetTradeTypeById(int TradeTypeId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getTradeTypeById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_TRADE_TYPE";
        param.Value = TradeTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static TradeType objGetTradeTypeById(int TradeTypeId)
    {
        DataTable dt = dtGetTradeTypeById(TradeTypeId);

        TradeType xRows = new TradeType();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new TradeType(dr);
        }
        return xRows;
    }

    public static List<TradeType> lstGetTradeType()
    {
        DataTable dt = dtGetTradeType();

        List<TradeType> xRows = new List<TradeType>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new TradeType(dr));
        }
        return xRows;
    }

    public static bool createTradeType(string sTRADE_TYPE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createTradeType();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":TRADE_TYPE";
        param.Value = sTRADE_TYPE;
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

    public static bool updateTradeType(int iID_TRADE_TYPE, string sTRADE_TYPE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateTradeType();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_TRADE_TYPE";
        param.Value = iID_TRADE_TYPE;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TRADE_TYPE";
        param.Value = sTRADE_TYPE;
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
