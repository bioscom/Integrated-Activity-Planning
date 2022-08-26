using System;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using Microsoft.Security.Application;

//public static class UserStatus
//{
//    public static string Active;
//    public static string Deactivated;

//    static UserStatus()
//    {
//        Active = "Active";
//        Deactivated = "Deactivated";
//    }
//}


public class Users
{
    public string m_sUserName { get; set; }
    public string m_sFullName { get; set; }
    public string m_sUserMail { get; set; }
    public int m_iFunctionId { get; set; }
    public int m_iUserId { get; set; }
    public int m_iUserRole { get; set; }
    public string m_sFunction { get; set; }
    //public string m_sRole { get; set; }
    public int m_iStatus { get; set; }
    public int m_iAdministrator { get; set; }
    public string m_sPhoneExtention { get; set; }
    public int m_iIAPPlannerType { get; set; }
    public int m_iOuId { get; set; }

    public Users()
    {

    }

    public Users(DataRow dr)
    {
        m_sUserName = dr["USERNAME"].ToString();
        m_sFullName = dr["FULLNAME"].ToString();
        m_sUserMail = dr["USERMAIL"].ToString();
        m_iFunctionId = int.Parse(dr["FUNCTIONID"].ToString());
        m_iUserId = int.Parse(dr["IDUSER"].ToString());
        m_iUserRole = int.Parse(dr["ROLEID"].ToString());
        m_iOuId = int.Parse(dr["IDOU"].ToString());
        m_sFunction = dr["FUNCTION"].ToString();
        m_iStatus = int.Parse(dr["STATUS"].ToString());
        m_iAdministrator = int.Parse(dr["CURRENT_ROLE_HOLDER"].ToString());
        m_sPhoneExtention = dr["SYSADMINEXT"].ToString();
        m_iIAPPlannerType = int.Parse(dr["IAPPLANNER_TYPE"].ToString());
    }

    public structUserMailIdx structUserIdx
    {
        get
        {
            return new structUserMailIdx(m_sFullName, m_sUserMail, m_sUserName);
        }
    }
}

public class CompleteStaffDetailsInfo
{
    public string m_sUserName { get; set; }
    public string m_sFullName { get; set; }
    public string m_sUserMail { get; set; }
    public string m_sStaffNumber { get; set; }

    public CompleteStaffDetailsInfo()
    {

    }

    public CompleteStaffDetailsInfo(DataRow dr)
    {
        m_sUserName = dr["USERNAME"].ToString();
        m_sFullName = dr["FULL_NAME"].ToString();
        m_sUserMail = dr["EMAIL"].ToString();
        m_sStaffNumber = dr["STAFF_NUMBER"].ToString();
    }

    public structUserMailIdx structUserIdx
    {
        get
        {
            return new structUserMailIdx(m_sFullName, m_sUserMail, m_sUserName);
        }
    }
}

public static class UsersActions
{
    static UsersActions()
    {

    }
    // For single sign on
    public static Users getUserByDomainLoginID(string UserName)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByDomainLoginID();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sStatus";
        param.Value = (int) appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        Users thisUser = new Users();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new Users(dr);
        }
        return thisUser;
    }

    public static DataTable dtGetUserByUserId(int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static Users objGetUserByUserId(int iUserId)
    {
        DataTable dt = dtGetUserByUserId(iUserId);
        Users thisUser = new Users();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new Users(dr);
        }
        return thisUser;
    }
    public static DataTable dtGetUserByUserName(string sUserName)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserName();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":sUserName";
        param.Value = sUserName;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtGetUserBySearch(string SearchCriteria)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.searchUser();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":SEARCHKEY";
        param.Value = "%" + Encoder.HtmlEncode(SearchCriteria) + "%";
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sStatus";
        param.Value = (int) appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }




    public static Users objGetUserByUserName(string sUserName)
    {
        DataTable dt = dtGetUserByUserName(sUserName);
        Users thisUser = new Users();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new Users(dr);
        }
        return thisUser;
    }
    public static bool LoginMeIn(string UserName)
    {
        Users Users = new Users();
        bool results = false;

        try
        {
            Users thisUser = getUserByDomainLoginID(Apps.LoginIDNoDomain(UserName));
            if (thisUser.m_iUserId != 0)
            {
                results = true;
            }
            else
            {
                results = false;
            }
        }
        catch (Exception ex)
        {
            // System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return results;
    }
    public static bool DeletePrivilege(Users CurrentUser)
    {
        bool DeletePrivilege = false;

        if ((CurrentUser.m_iUserId == (int)appUsersRoles.userRole.IAPPlanner) || (CurrentUser.m_iUserId == (int)appUsersRoles.userRole.admin))
        {
            DeletePrivilege = true;
        }

        return DeletePrivilege;
    }
    public static List<Users> lstGetContacts()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.supportContacts();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":CURRENT_ROLE_HOLDER";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int) appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();
            rowData.m_sPhoneExtention = row["SYSADMINEXT"].ToString();

            result.Add(rowData);
        }
        return result;
    }
    public static DataTable dtGetUsersByRoleId(int iRoleId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUsersByRoleId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iRoleId";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStatus";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static List<Users> lstGetUsersByRoleId(int iRoleId)
    {
        DataTable dt = dtGetUsersByRoleId(iRoleId);
        List<Users> result = new List<Users>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new Users(dr));
        }

        return result;
    }

    public static DataTable dtGetOriginators()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getOriginators();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<Users> lstGetOriginators()
    {
        DataTable dt = dtGetOriginators();
        List<Users> result = new List<Users>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new Users(dr));
        }

        return result;
    }
    public static List<Users> getIAPPlanners()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MySQL4IAPPlanner();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = (int)appUsersRoles.userRole.IAPPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iOuId = int.Parse(row["IDOU"].ToString());
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Users> getIAPPlannersByOU(int iOuId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MySQL4IAPPlannerByOU();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = (int)appUsersRoles.userRole.IAPPlanner;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDOU";
        param.Value = iOuId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static List<Users> getChangeReviewBoards(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MySQL();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = (int)appUsersRoles.userRole.ChangeReviewBoardChairman;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();

            result.Add(rowData);
        }
        return result;
    }
    public static List<Users> getProductionAssetReviwers(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MySQL();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = (int)appUsersRoles.userRole.ProductionAssetAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();

            result.Add(rowData);
        }
        return result;
    } 
    public static List<Users> getFunctionalRepresentatives(long lRequestId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MySQL();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = (int)appUsersRoles.userRole.FunctionalAuthorizer;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":lRequestId";
        param.Value = lRequestId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        List<Users> result = new List<Users>();
        foreach (DataRow row in dt.Rows)
        {
            Users rowData = new Users();
            rowData.m_iUserId = int.Parse(row["IDUSER"].ToString());
            rowData.m_sFullName = row["FULLNAME"].ToString();
            rowData.m_sUserMail = row["USERMAIL"].ToString();

            result.Add(rowData);
        }
        return result;
    }

    public static Users getAdministrators()
    {
        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, ";
        sql += "IAP_FUNCTIONS.FUNCTIONID, IAP_FUNCTIONS.FUNCTION, IAP_USERROLES.ROLEID, IAP_USERROLES.ROLE, IAP_USERS.SYSADMINEXT ";
        sql += "FROM IAP_USERS INNER JOIN IAP_USERROLES ON IAP_USERS.ROLEID = IAP_USERROLES.ROLEID INNER JOIN ";
        sql += "IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE IAP_USERS.IDUSER = :IDUSER AND STATUS = :STATUS AND CURRENT_ROLE_HOLDER = :CURRENT_ROLE_HOLDER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSER";
        param.Value = (int)appUsersRoles.userRole.admin;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":CURRENT_ROLE_HOLDER";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        Users thisUser = new Users();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new Users(dr);
        }
        return thisUser;
    }

    public static CompleteStaffDetailsInfo GetStaffFromCompleteStaffDetails(string UserName)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserFromCompleteStaffDetailsByUserName();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        CompleteStaffDetailsInfo thisUser = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new CompleteStaffDetailsInfo(dr);
        }
        return thisUser;
    }
    public static CompleteStaffDetailsInfo getStaffFromCompleteStaffDetails(string UserName)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserFromCompleteStaffDetails();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":EMAIL";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        CompleteStaffDetailsInfo thisUser = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            thisUser = new CompleteStaffDetailsInfo(dr);
        }
        return thisUser;
    }
    public static List<CompleteStaffDetailsInfo> lstGetStaffInfoBySearch(string UserName)
    {
        // get a configured OracleCommand object
        OracleCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = StoredProcedure.CompleteStafDetailsBySurName();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":SURNAME";
        param.Value = UserName.ToUpper();
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // obtain the results
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        List<CompleteStaffDetailsInfo> result = new List<CompleteStaffDetailsInfo>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new CompleteStaffDetailsInfo(dr));
        }

        return result;
    }
    public static CompleteStaffDetailsInfo lstGetStaffInfoByStaffNumber(string staffNumber)
    {
        // get a configured OracleCommand object
        OracleCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = StoredProcedure.CompleteStafDetailsByStaffNumber();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":STAFF_NUMBER";
        param.Value = staffNumber.ToUpper();
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // obtain the results
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        CompleteStaffDetailsInfo result = new CompleteStaffDetailsInfo();
        foreach (DataRow dr in dt.Rows)
        {
            result = new CompleteStaffDetailsInfo(dr);
        }

        return result;
    }
    public static bool CreateUser(string UserName, string FullName, string IAPPlannerType, string UserMail, int function, int Role, int iStatus, string PhoneNo)
    {
        string sql = "INSERT INTO IAP_USERS (FUNCTIONID, ROLEID, USERNAME, FULLNAME, IAPPLANNER_TYPE, USERMAIL, STATUS, SYSADMINEXT) VALUES ";
        sql += "(:FUNCTIONID, :ROLEID, :USERNAME, :FULLNAME, :IAPPLANNER_TYPE, :USERMAIL, :STATUS, :SYSADMINEXT)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = function;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = Role;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":FULLNAME";
        param.Value = FullName;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IAPPLANNER_TYPE";
        param.Value = IAPPlannerType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERMAIL";
        param.Value = UserMail;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SYSADMINEXT";
        param.Value = PhoneNo;
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
    public static bool EditUser(int UserID, string IAPPlannerType, int function, int Role, string PhoneNo)
    {
        string sql = "UPDATE IAP_USERS SET FUNCTIONID = :FUNCTIONID, ROLEID = :ROLEID, IAPPLANNER_TYPE = :IAPPLANNER_TYPE, SYSADMINEXT = :SYSADMINEXT WHERE IDUSER = :IDUSER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":FUNCTIONID";
        param.Value = function;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSER";
        param.Value = UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = Role;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IAPPLANNER_TYPE";
        param.Value = IAPPlannerType;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SYSADMINEXT";
        param.Value = PhoneNo;
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
    public static bool DeleteUser(int UserID)
    {
        string sql = "UPDATE IAP_USERS SET STATUS = :STATUS WHERE IDUSER = :IDUSER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSER";
        param.Value = UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.disabledMe;
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

    public static bool EnableUser(string UserName)
    {
        string sql = "UPDATE IAP_USERS SET STATUS = :STATUS WHERE USERNAME = :USERNAME";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":USERNAME";
        param.Value = UserName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int) appUsersRoles.userStatus.activeUser;
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

    public static DataTable getIAPUsers()
    {
        //string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, ";
        //sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER FROM IAP_USERS ";
        ////sql += "INNER JOIN IAP_USERROLES ON IAP_USERS.ROLEID = IAP_USERROLES.ROLEID ";
        //sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        ////sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        //sql += "WHERE STATUS = :STATUS ORDER BY IAP_USERS.ROLEID";

        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, ";
        sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER FROM IAP_USERS ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "WHERE STATUS = :STATUS ORDER BY IAP_USERS.FULLNAME";


        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getIAPUserByUserId(int iUserId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByUserId();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static Users objGetIapOwnerByUserId(int iUserId)
    {
        DataTable dt = getIAPUserByUserId(iUserId);

        Users oUsers = new Users();
        foreach (DataRow dr in dt.Rows)
        {
            oUsers = new Users(dr);
        }
        return oUsers;
    }
    public static DataTable searchIAPUser(string searchCriteria)
    {
        string sql = "SELECT IAP_USERS.USERNAME, IAP_USERS.FULLNAME, IAP_USERS.USERMAIL, IAP_USERS.IDUSER, IAP_USERS.IAPPLANNER_TYPE, IAP_FUNCTIONS.FUNCTIONID, ";
        sql += "IAP_FUNCTIONS.FUNCTION, IAP_USERS.ROLEID, IAP_IMPACTANALYSIS_WINDOW.WINDOW_TYPE, IAP_USERS.CURRENT_ROLE_HOLDER, IAP_USERS.STATUS FROM IAP_USERS ";
        sql += "INNER JOIN IAP_FUNCTIONS ON IAP_USERS.FUNCTIONID = IAP_FUNCTIONS.FUNCTIONID ";
        sql += "LEFT OUTER JOIN IAP_IMPACTANALYSIS_WINDOW ON IAP_IMPACTANALYSIS_WINDOW.WINDOWID = IAP_USERS.IAPPLANNER_TYPE ";
        sql += "WHERE STATUS = :STATUS AND UPPER(IAP_USERS.FULLNAME) LIKE UPPER(:SEARCHKEY)";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SEARCHKEY";
        param.Value = "%" + searchCriteria + "%"; //Encoder.HtmlEncode(
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool ResetSupportContacts()
    {
        //Reset Contacts
        string sql = "UPDATE IAP_USERS SET CURRENT_ROLE_HOLDER = :CURRENT_ROLE_HOLDER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":CURRENT_ROLE_HOLDER";
        param.Value = (int)appUsersRoles.userStatus.disabledMe;
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
    public static bool UpdateSupportContacts(string UserId)
    {
        //Update new settings
        string sql = "UPDATE IAP_USERS SET CURRENT_ROLE_HOLDER = :CURRENT_ROLE_HOLDER WHERE IDUSER = :IDUSER";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":CURRENT_ROLE_HOLDER";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSER";
        param.Value = UserId;
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


    public static bool AddFunctionResponsibleParty(int iUserId, int iFunctionId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AddFunctionsParties();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iFunctionId";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;

        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            // any errors are logged in GenericDataAccess, we ignore them here
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool DeleteFunctionResponsibleParty(int iUserId, int iFunctionId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.RemoveFunctionsParties();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iFunctionId";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;

        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            // any errors are logged in GenericDataAccess, we ignore them here
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    public static DataTable dtFunctionResponsiblePartys()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getFunctionsParties();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtFunctionResponsiblePartysByFunction(int iFunctionId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getFunctionsPartiesByFunction();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iFunctionId";
        param.Value = iFunctionId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<Users> LstFunctionResponsiblePartysByFunction(int iFunctionId)
    {
        DataTable dt = dtFunctionResponsiblePartysByFunction(iFunctionId);

        var o = new List<Users>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            o.Add(new Users(dr));
        }

        return o;
    }



    public static bool AddLocactionResponsibleParty(int iUserId, int iAreaId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AddLocationParties();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iAreaId";
        param.Value = iAreaId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;

        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            // any errors are logged in GenericDataAccess, we ignore them here
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public static bool DeleteLocactionResponsibleParty(int iUserId, int iAreaId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.RemoveLocationsParties();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iAreaId";
        param.Value = iAreaId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;

        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            // any errors are logged in GenericDataAccess, we ignore them here
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    public static DataTable dtLocationResponsiblePartys()
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLocationsParties();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable dtLocationResponsiblePartysByHub(int iAreaId)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getLocationsPartiesByHub();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":iAreaId";
        param.Value = iAreaId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static List<Users> LstLocationResponsiblePartysByHub(int iAreaId)
    {
        DataTable dt = dtLocationResponsiblePartysByHub(iAreaId);

        var o = new List<Users>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            o.Add(new Users(dr));
        }

        return o;
    }



    //public static RequestDocs objGetRequestDocByDocId(long lDocsId)
    //{
    //    DataTable dt = dtGetRequestDocByDocId(lDocsId);

    //    var o = new RequestDocs();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        o = new RequestDocs(dr);
    //    }

    //    return o;
    //}
}