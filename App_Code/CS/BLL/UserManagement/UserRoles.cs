using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public enum PlannerType
{
    vst = 1,
    st = 2,
    mt = 3,
};

   
/// <summary>
/// Summary description for UserRoles
/// </summary>

public class UserRolesMgt
{
    public int iRoleID { get; set; }
    public string sRole { get; set; }

    public UserRolesMgt()
    {

    }

    public UserRolesMgt(DataRow dr)
    {
        iRoleID = Int32.Parse(dr["ROLEID"].ToString());
        sRole = dr["ROLE"].ToString();
    }

    public static UserRolesMgt xUserRolesMgt(string RoleID)
    {
        string sql = "SELECT ROLEID, ROLE FROM IAP_USERROLES WHERE ROLEID = :ROLEID";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":ROLEID";
        param.Value = RoleID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

        UserRolesMgt thisRole = new UserRolesMgt();
        foreach (DataRow dr in dt.Rows)
        {
            thisRole = new UserRolesMgt(dr);
        }
        return thisRole;
    }

    //public static List<UserRoles> xUserRoles()
    //{
    //    string sql = "SELECT ROLEID, ROLE FROM IAP_USERROLES";

    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);

    //    List<UserRoles> result = new List<UserRoles>();
    //    foreach (DataRow row in dt.Rows)
    //    {
    //        UserRoles rowData = new UserRoles();
    //        rowData.iRoleID = int.Parse(row["ROLEID"].ToString());
    //        rowData.sRole = row["ROLE"].ToString();

    //        result.Add(rowData);
    //    }
    //    return result;
    //}
}

public class appUsersRoles
{
    public enum userRole
    {
        ActivityOwner = 1,
        IAPPlanner = 2,
        FunctionalAuthorizer = 3,
        ProductionAssetAuthorizer = 4,
        admin = 5,
        ProjectManager = 6,
        BusinessOpportunityManager = 7,
        FinanceAuthorizer = 8,
        ChangeReviewBoardChairman = 21,
        
    };

    public static string userRoleDesc(userRole eUserRole)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eUserRole)
            {
                case userRole.admin: sRet = "Administrator"; break;
                case userRole.ActivityOwner: sRet = "Activity Owner"; break;
                case userRole.FunctionalAuthorizer: sRet = "Functional Authorizer"; break;
                case userRole.IAPPlanner: sRet = "IAP Planner"; break;
                case userRole.ProductionAssetAuthorizer: sRet = "Production Asset Authorizer"; break;
                case userRole.ChangeReviewBoardChairman: sRet = "Change Review Board Chairman"; break;
                case userRole.ProjectManager: sRet = "Project Manager"; break;
                case userRole.BusinessOpportunityManager: sRet = "Business Opportunity Manager"; break;
                case userRole.FinanceAuthorizer: sRet = "Finance Authorizer"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public static void UserRoleReporting(GridDataItem item, int iColumn)
    {
        if (item.Cells[iColumn].Text == ((int) userRole.admin).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.admin);
        else if (item.Cells[iColumn].Text == ((int) userRole.ActivityOwner).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.ActivityOwner);
        else if (item.Cells[iColumn].Text == ((int) userRole.BusinessOpportunityManager).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.BusinessOpportunityManager);
        else if (item.Cells[iColumn].Text == ((int) userRole.ChangeReviewBoardChairman).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.ChangeReviewBoardChairman);
        else if (item.Cells[iColumn].Text == ((int) userRole.FinanceAuthorizer).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.FinanceAuthorizer);
        else if (item.Cells[iColumn].Text == ((int) userRole.FunctionalAuthorizer).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.FunctionalAuthorizer);
        else if (item.Cells[iColumn].Text == ((int) userRole.IAPPlanner).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.IAPPlanner);
        else if (item.Cells[iColumn].Text == ((int) userRole.ProductionAssetAuthorizer).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.ProductionAssetAuthorizer);
        else if (item.Cells[iColumn].Text == ((int) userRole.ProjectManager).ToString())
            item.Cells[iColumn].Text = userRoleDesc(userRole.ProjectManager);

        item.Cells[iColumn].ForeColor = System.Drawing.Color.Red;
        item.Cells[iColumn].Font.Bold = true;
    }


    public enum userStatus
    {
        activeUser = 1,
        lockedUser = 2,
        disabledMe = 3,
        unKnownMe = 4
    };

    public enum eDeligation
    {
        Deligated = 1,
        Undeligated = 0
    };

    public static string userStatusDesc(userStatus eUserStatus)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eUserStatus)
            {
                case userStatus.activeUser: sRet = "Active Account"; break;
                case userStatus.disabledMe: sRet = "Deleted Account"; break;
                case userStatus.lockedUser: sRet = "Locked Account"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public static void getUserRoles(DropDownList ddlUserRole)
    {
        ddlUserRole.Items.Clear();
        ddlUserRole.Items.Add(new ListItem("Please Select Role...", "-1"));
        addRoleToDropDown(appUsersRoles.userRole.admin, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.ActivityOwner, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.FunctionalAuthorizer, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.IAPPlanner, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.FinanceAuthorizer, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.ProductionAssetAuthorizer, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.ChangeReviewBoardChairman, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.ProjectManager, ddlUserRole);
        addRoleToDropDown(appUsersRoles.userRole.BusinessOpportunityManager, ddlUserRole);
    }


    public static string PlannerTypeDesc(PlannerType ePlannerType)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (ePlannerType)
            {
                case PlannerType.mt: sRet = "MT"; break;
                case PlannerType.st: sRet = "ST"; break;
                case PlannerType.vst: sRet = "VST"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public bool grantPageAccess(string[] sAccountRole, userRole eMyRole)
    {
        bool bRet = false;
        try
        {
            foreach (string sId in sAccountRole)
            {
                if (eMyRole.ToString() == sId)
                {
                    bRet = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }
    public bool grantPageAccessAdminInit(string[] sAccountRole, userRole eMyRole)
    {
        bool bRet = false;
        try
        {
            if ((eMyRole == userRole.admin) || (eMyRole == userRole.ActivityOwner))
            {
                bRet = true;
            }
            else
            {
                foreach (string sId in sAccountRole)
                {
                    if (eMyRole.ToString() == sId)
                    {
                        bRet = true;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }
    public bool grantPageAccess(string sAccountRole, userRole eMyRole)
    {
        bool bRet = false;
        try
        {
            if (eMyRole.ToString() == sAccountRole)
            {
                bRet = true;
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }
    private static void addRoleToDropDown(appUsersRoles.userRole eRole, DropDownList ddlUserRole)
    {
        try
        {
            ListItem oItem = new ListItem();
            oItem.Text = appUsersRoles.userRoleDesc(eRole);
            oItem.Value = ((int)eRole).ToString();
            ddlUserRole.Items.Add(oItem);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }


        //public bool grantPageAccess(string[] sAccountRole, userRole eMyRole)
        //{
        //    bool bRet = false;
        //    try
        //    {
        //        if (eMyRole == userRole.admin)
        //        {
        //            bRet = true;
        //        }
        //        else
        //        {
        //            foreach (string sId in sAccountRole)
        //            {
        //                if (eMyRole.ToString() == sId)
        //                {
        //                    bRet = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        appMonitor.logAppExceptions(ex);
        //    }

        //    return bRet;
        //}
    }
