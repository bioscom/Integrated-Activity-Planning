using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for FieldVisaType
/// </summary>

public class FieldVisaType
{
    public int m_iID_VISA { get; set; }
    public string m_sVISA_TYPE { get; set; }

    public FieldVisaType()
    {

    }

    public FieldVisaType(DataRow dr)
    {
        m_iID_VISA = int.Parse(dr["ID_VISA"].ToString());
        m_sVISA_TYPE = dr["VISA_TYPE"].ToString();
    }

    public static DataTable dtGetVisaType()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getVisaType();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetVisaTypeById(int VisaTypeId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getVisaTypeById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_VISA";
        param.Value = VisaTypeId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static FieldVisaType objGetVisaTypeId(int VisaTypeId)
    {
        DataTable dt = dtGetVisaTypeById(VisaTypeId);

        FieldVisaType xRows = new FieldVisaType();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new FieldVisaType(dr);
        }
        return xRows;
    }

    public static List<FieldVisaType> lstGetVisaType()
    {
        DataTable dt = dtGetVisaType();

        List<FieldVisaType> xRows = new List<FieldVisaType>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new FieldVisaType(dr));
        }
        return xRows;
    }

    public static bool createVisaType(string sVISA_TYPE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createVisaType();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":VISA_TYPE";
        param.Value = sVISA_TYPE;
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

    public static bool updateVisaType(int iVISA, string sVISA_TYPE)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateVisaType();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_VISA";
        param.Value = iVISA;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":VISA_TYPE";
        param.Value = sVISA_TYPE;
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
