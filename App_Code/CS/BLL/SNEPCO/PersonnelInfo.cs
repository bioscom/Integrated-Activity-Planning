using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// Summary description for PersonnelInfo
/// </summary>
public class PersonnelInfo
{
    public int m_iID_PERSONNEL { get; set; }
    public long m_iIDREQUEST { get; set; }
    public string m_sEMPLOYEE_NAME { get; set; }
    public string m_sGENDER { get; set; }
    public string m_sCOMPANY { get; set; }
    public int m_iID_CONTACT { get; set; }
    public int m_iID_LAST_VISIT { get; set; }
    public string m_sBOSIET_VALID { get; set; }
    //public string m_sHUET_VALID { get; set; }
    public string m_sMEDICAL { get; set; }
    public int m_iID_VISA { get; set; }
    //public string m_sPPE { get; set; }
    //public int m_iID_BAG { get; set; }

    public FieldLastVisit eLastVisit
    {
        get
        {
            return FieldLastVisit.objGetLastVisitById(m_iID_LAST_VISIT);
        }
    }

    public FieldVisaType eFieldVisaType
    {
        get
        {
            return FieldVisaType.objGetVisaTypeId(m_iID_VISA);
        }
    }

    public FieldContactPerson eFieldContactPerson
    {
        get
        {
            return FieldContactPerson.objGetContactPersonById(m_iID_CONTACT);
        }
    }

    //public FieldBaggage eFieldBaggage
    //{
    //    get
    //    {
    //        return FieldBaggage.objGetBaggageId(m_iID_BAG);
    //    }
    //}

	public PersonnelInfo()
	{
		
	}

    public PersonnelInfo(DataRow dr)
    {
        m_iID_PERSONNEL = int.Parse(dr["ID_PERSONNEL"].ToString());
        m_iIDREQUEST = long.Parse(dr["IDREQUEST"].ToString());
        m_sEMPLOYEE_NAME = dr["EMPLOYEE_NAME"].ToString();
        m_sGENDER = dr["GENDER"].ToString();
        m_sCOMPANY = dr["COMPANY"].ToString();
        m_iID_CONTACT = int.Parse(dr["ID_CONTACT"].ToString());
        m_iID_LAST_VISIT = int.Parse(dr["ID_LAST_VISIT"].ToString());
        m_sBOSIET_VALID = dr["BOSIET_VALID"].ToString();
        //m_sHUET_VALID = dr["HUET_VALID"].ToString();
        m_sMEDICAL = dr["MEDICAL"].ToString();
        m_iID_VISA = int.Parse(dr["ID_VISA"].ToString());
        //m_sPPE = dr["PPE"].ToString();
        //m_iID_BAG = int.Parse(dr["ID_BAG"].ToString());
    }

    public static DataTable dtGetPersonnelInfo()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getAllPersonnelInformation();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetPersonnelInfoById(int PersonnelInfoId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPersonnelInformationById();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_PERSONNEL";
        param.Value = PersonnelInfoId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static PersonnelInfo objGetPersonnelInfoById(int PersonnelInfoId)
    {
        DataTable dt = dtGetPersonnelInfoById(PersonnelInfoId);

        PersonnelInfo xRows = new PersonnelInfo();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new PersonnelInfo(dr);
        }
        return xRows;
    }

    public static List<PersonnelInfo> lstGetPersonnelInfo()
    {
        DataTable dt = dtGetPersonnelInfo();

        List<PersonnelInfo> xRows = new List<PersonnelInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new PersonnelInfo(dr));
        }
        return xRows;
    }

    public static DataTable dtGetPersonnelInfoByRequestId(long iRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPersonnelInformationByRequestId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iRequestId";
        param.Value = iRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static PersonnelInfo objGetPersonnelInfoByActivityId(long iRequestId)
    {
        DataTable dt = dtGetPersonnelInfoByRequestId(iRequestId);

        PersonnelInfo xRows = new PersonnelInfo();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new PersonnelInfo(dr);
        }
        return xRows;
    }

    public static List<PersonnelInfo> lstGetPersonnelInfoByRequestId(long iRequestId)
    {
        DataTable dt = dtGetPersonnelInfoByRequestId(iRequestId);

        List<PersonnelInfo> xRows = new List<PersonnelInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new PersonnelInfo(dr));
        }
        return xRows;
    }

    public static bool createPersonnelInfo(long lRequestId, string sEMPLOYEE_NAME, int iGENDER, int iID_CONTACT, int iID_LAST_VISIT, 
      int iBOSIET_VALID,  int iMEDICAL, int iID_VISA,  string sCOMPANY)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.createPersonnelInformation();
         
        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EMPLOYEE_NAME";
        param.Value = sEMPLOYEE_NAME;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GENDER";
        param.Value = iGENDER;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMPANY";
        param.Value = sCOMPANY;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_CONTACT";
        param.Value = iID_CONTACT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_LAST_VISIT";
        param.Value = iID_LAST_VISIT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":BOSIET_VALID";
        param.Value = iBOSIET_VALID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":HUET_VALID";
        //param.Value = iHUET_VALID;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":MEDICAL";
        param.Value = iMEDICAL;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_VISA";
        param.Value = iID_VISA;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":PPE";
        //param.Value = iPPE;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":ID_BAG";
        //param.Value = iID_BAG;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

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

    public static bool updatePersonnelInfo(PersonnelInfo ePersonnelInfo)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.updatePersonnelInformation();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_PERSONNEL";
        param.Value = ePersonnelInfo.m_iID_PERSONNEL;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EMPLOYEE_NAME";
        param.Value = ePersonnelInfo.m_sEMPLOYEE_NAME;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GENDER";
        param.Value = ePersonnelInfo.m_sGENDER;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMPANY";
        param.Value = ePersonnelInfo.m_sCOMPANY;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_CONTACT";
        param.Value = ePersonnelInfo.m_iID_CONTACT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_LAST_VISIT";
        param.Value = ePersonnelInfo.m_iID_LAST_VISIT;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":BOSIET_VALID";
        param.Value = ePersonnelInfo.m_sBOSIET_VALID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":HUET_VALID";
        //param.Value = ePersonnelInfo.m_sHUET_VALID;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":MEDICAL";
        param.Value = ePersonnelInfo.m_sMEDICAL;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ID_VISA";
        param.Value = ePersonnelInfo.m_iID_VISA;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":PPE";
        //param.Value = ePersonnelInfo.m_sPPE;
        //param.DbType = DbType.String;
        //param.Size = 1000;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":ID_BAG";
        //param.Value = ePersonnelInfo.m_iID_BAG;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

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

    public static bool deletePersonnelInfo(Int64 iPersonnelId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.deletePersonnel();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ID_PERSONNEL";
        param.Value = iPersonnelId;
        param.DbType = DbType.Int64;
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