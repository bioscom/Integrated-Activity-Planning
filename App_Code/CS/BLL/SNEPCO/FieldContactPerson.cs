using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for FieldContactPerson
/// </summary>

public class FieldContactPerson
{
    public int m_iID_CONTACT { get; set; }
    public string m_sCONTACT_PERSON { get; set; }

    public FieldContactPerson()
    {

    }

    public FieldContactPerson(DataRow dr)
    {
        m_iID_CONTACT = int.Parse(dr["ID_CONTACT"].ToString());
        m_sCONTACT_PERSON = dr["CONTACT_PERSON"].ToString();
    }

    public static DataTable dtGetContactPerson()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getContactPerson();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetContactPersonById(int ContactPersonId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getContactPersonById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_CONTACT";
        param.Value = ContactPersonId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static FieldContactPerson objGetContactPersonById(int ContactPersonId)
    {
        DataTable dt = dtGetContactPersonById(ContactPersonId);

        FieldContactPerson xRows = new FieldContactPerson();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new FieldContactPerson(dr);
        }
        return xRows;
    }

    public static List<FieldContactPerson> lstGetContactPerson()
    {
        DataTable dt = dtGetContactPerson();

        List<FieldContactPerson> xRows = new List<FieldContactPerson>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new FieldContactPerson(dr));
        }
        return xRows;
    }

    public static bool createContactPerson(string sCONTACT_PERSON)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createContactPerson();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":CONTACT_PERSON";
        param.Value = sCONTACT_PERSON;
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

    public static bool updateContactPerson(int iCONTACT, string sCONTACT_PERSON)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updateContactPerson();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_CONTACT";
        param.Value = iCONTACT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":CONTACT_PERSON";
        param.Value = sCONTACT_PERSON;
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
