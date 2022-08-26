using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for loginUser
/// </summary>
//public class loginUser
//{
//    public loginUser()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

public class loginUser
{
    public enum statusx
    {
        loginFailed = 0,
        loginSucceed = 1,
        idIsNotFound = 2,
        statusDisabld = 3,
        statusLocked = 4,
        statusUnKnown = 5
    };

    public struct loginRet
    {
        public statusx eStatus;
        public Users eAppUserInfo;
        public CompleteStaffDetailsInfo eIWHUserInfo;
    }

    public loginRet verifyAppUser()
    {
        loginRet eRet = new loginRet();

        eRet.eStatus = statusx.loginFailed;
        eRet.eAppUserInfo = new Users();
        eRet.eIWHUserInfo = new CompleteStaffDetailsInfo();

        try
        {
            //Check if the user is registered on the application's User Management Table, if not found there then check the Information Ware House
            Users FoundOnAppUserMgt = new Users();
            FoundOnAppUserMgt = UsersActions.getUserByDomainLoginID(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name));

            if (!string.IsNullOrEmpty(FoundOnAppUserMgt.m_sUserName))
            {
                eRet = AuthenticationVerification(FoundOnAppUserMgt);
            }
            else if (string.IsNullOrEmpty(FoundOnAppUserMgt.m_sUserName))
            {
                //Check for the user on the Information Ware House
                CompleteStaffDetailsInfo FoundOnIWH = UsersActions.getStaffFromCompleteStaffDetails(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name));
                if (string.IsNullOrEmpty(FoundOnIWH.m_sUserName))
                {
                    eRet.eStatus = statusx.idIsNotFound;
                }
                else if (!string.IsNullOrEmpty(FoundOnIWH.m_sUserName))
                {
                    eRet = AuthenticationVerification(FoundOnIWH);
                }
            }
            
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return eRet;
    }

    //Authentification Verification from Application Users Table
    private loginRet AuthenticationVerification(Users FoundOnAppUserMgt)
    {
        loginRet eRet = new loginRet();
        string p_sUserId = FoundOnAppUserMgt.m_sUserName;

        eRet.eAppUserInfo = FoundOnAppUserMgt;
        httpSessionx oInitSessn = new httpSessionx(HttpContext.Current.Session, eRet.eAppUserInfo);

        switch (FoundOnAppUserMgt.m_iStatus)
        {
            case (int)appUsersRoles.userStatus.activeUser:
                eRet.eStatus = statusx.loginSucceed;
                eRet.eAppUserInfo = FoundOnAppUserMgt;
                oInitSessn = new httpSessionx(HttpContext.Current.Session, eRet.eAppUserInfo);
                break;

            case (int)appUsersRoles.userStatus.disabledMe:
                eRet.eStatus = statusx.statusDisabld;
                break;

            case (int)appUsersRoles.userStatus.lockedUser:
                eRet.eStatus = statusx.statusLocked;
                break;

            default:
                eRet.eStatus = statusx.statusUnKnown;
                break;
        }
        return eRet;
    }

    //Authentification Verification from Information Ware House
    private loginRet AuthenticationVerification(CompleteStaffDetailsInfo FoundOnIWH)
    {
        loginRet eRet = new loginRet();
        string p_sUserId = FoundOnIWH.m_sUserName;

        eRet.eStatus = statusx.loginSucceed;
        eRet.eIWHUserInfo = FoundOnIWH;

        //Create a session
        httpSessionx oInitSessn = new httpSessionx(HttpContext.Current.Session, eRet.eIWHUserInfo, false);
        return eRet;
    }

    public static string msgQueryString(string sQueryData, httpSessionx mySession)
    {
        string mssg = "";
        try
        {
            if (sQueryData != "")
            {
                switch (sQueryData)
                {

                    case "SerErr": mssg = "The Specified Task could NOT be Processed, Please try Again"; break;
                    case "RptErr": mssg = "The Specified Report Information could NOT be Generated, Please try Again"; break;
                    case "Cxn": mssg = "The Specified Task was Cancelled by Application User"; break;
                    case "sWb": mssg = mySession.getOnlineUser.m_sFullName + ", Welcome back to Your Account"; break; //", Your Account Sign-In was Successful"

                    #region Close these codes until when required
                    //case "sWl" :    mssg = this.oSessnx.getOnlineUser.sNames + ", Continue from Your Task Page"; break; 
                    //    //Call msgAlertNotice()
                    //case "sWb":
                    //    mssg = this.oSessnx.getOnlineUser.sNames + ", Welcome back to Your Account"; break; //", Your Account Sign-In was Successful"
                    //    //Call msgAlertNotice()
                    //    //Call checkUnPhasedOpexItems(sUserId, opexSettings.bpPlanYearId)
                    //case "sVd":
                    //    mssg = "Session was Validated for '" + this.oSessnx.getOnlineUser.sNames + "' Account, Please Continue from Here";
                    //    //Call msgAlertNotice()
                    //    //'Call checkUnPhasedOpexItems(sUserId)
                    //    //'add opex phasing
                    //case "fEdOpD" : mssg = "Specified Phasing Details Addition to the " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "sEdOpD" : mssg = "Specified Phasing Details was Successfully Added to the " + globalCodes.c_sWebAppNamex + " Database";
                    //case "nEdOpD" : mssg = "Unable to find the OPEX Information for the Specified Phasing Details. Task is Aborted";
                    //case "kEdOpD" : mssg = "You do NOT have the Necessary Credentials to Modify the Specified Phasing Details. Task is Aborted";
                    //case "dEdOpD" : mssg = "Specified Year Phasing Details has NOT Function Dollar Value attached to it. Task is Aborted";
                    //    //'exchange rate
                    //case "fEdExR" : mssg = "New Dollar-to-Naira Exchange Rate Modification in the " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "sEdExR" : mssg = "New Dollar-to-Naira Exchange Rate was Successfully Modified in the " + globalCodes.c_sWebAppNamex + " Database";
                    //case "nEdExR" : mssg = "No Dollar-to-Naira Exchange Rate is Currently Defined. Task is Aborted";
                    //    //'cost element
                    //case "sNwCEl" : mssg = "New Cost Element Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwCEl" : mssg = "New Cost Element Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwCEl" : mssg = "The Specifed New Cost Element Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config asset
                    //case "sNwAsT" : mssg = "New Asset Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwAsT" : mssg = "New Asset Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwAsT" : mssg = "The Specifed New Asset Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config uapc
                    //case "sNwUaT" : mssg = "New UAPC Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database"
                    //case "fNwUaT" : mssg = "New UAPC Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again"
                    //case "xNwUaT" : mssg = "The Specifed New UAPC Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database"
                    //    //'congig level
                    //case "sNwLev" : mssg = "New VP Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database"
                    //case "fNwLev" : mssg = "New VP Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again"
                    //case "xNwLev" : mssg = "The Specifed New VP Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database"
                    //    //'config category
                    //case "sNwVtT" : mssg = "New Venture Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database"
                    //case "fNwVtT" : mssg = "New Venture Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again"
                    //case "xNwVtT" : mssg = "The Specifed New Venture Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database"
                    //   // 'config venture
                    //case "sNwCaT" : mssg = "New Category Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwCaT" : mssg = "New Category Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwCaT" : mssg = "The Specifed New Category Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //    //'config budget
                    //case "sNwBgT" : mssg = "New Budget Holder was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwBgT" : mssg = "New Budget Holder Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwBgT" : mssg = "The Specifed New Budget Holder Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config sap
                    //case "sNwSap" : mssg = "New SAP Object Cost was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwSap" : mssg = "New SAP Object Cost Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwSap" : mssg = "The Specifed New SAP Object Cost Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config nnpc code
                    //case "sNwNcT" : mssg = "New NNPC Code was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwNcT" : mssg = "New NNPC Code Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwNcT" : mssg = "The Specifed New NNPC Code Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config devcom
                    //case "sNwDvT" : mssg = "New Devcom Code was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwDvT" : mssg = "New Devcom Code Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwDvT" : mssg = "The Specifed New Devcom Code Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config team
                    //case "sNwT" : mssg = "New Team was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwT" : mssg = "New Team Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwT" : mssg = "The Specifed New Team Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //    //'config asset group
                    //case "sNwAsG" : mssg = "New Asset Group was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database"
                    //case "fNwAsG" : mssg = "New Asset Group Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwAsG" : mssg = "The Specifed New Asset Group Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config district
                    //case "sNwDisT" : mssg = "New District was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwDisT" : mssg = "New District Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwDisT" : mssg = "The Specifed New District Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config facility
                    //case "sNwFacT" : mssg = "New Facility was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwFacT" : mssg = "New Facility Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwFacT" : mssg = "The Specifed New Facility Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //    //'config OML
                    //case "sNwOML" : mssg = "New OML was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwOML" : mssg = "New OML Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwOML" : mssg = "The Specifed New OML Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config Opex Type
                    //case "sNwOpxT" : mssg = "New Opex Type was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database"
                    //case "fNwOpxT" : mssg = "New Opex Type Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwOpxT" : mssg = "The Specifed New Opex Type Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config Opex Group
                    //case "sNwOpxG" : mssg = "New Opex Group was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwOpxG" : mssg = "New Opex Group Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwOpxG" : mssg = "The Specifed New Opex Group Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //    //'config Status
                    //case "sNwStaT" : mssg = "New Status was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwStaT" : mssg = "New Status Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwStaT" : mssg = "The Specifed New Status Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //   // 'config Unit
                    //case "sNwUnT" : mssg = "New Unit was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwUnT" : mssg = "New Unit Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwUnT" : mssg = "The Specifed New Unit Name already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";

                    //   // 'app Users
                    //   // 'application users
                    //case "sNwUsa" : mssg = "New Application User Account was Successfully Created to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwUsa" : mssg = "New Application User Account Creation to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "xNwUsa" : mssg = "The Specifed New Application User Account already 'EXIST' in the " + globalCodes.c_sWebAppNamex + " Database";
                    //case "nNwUsa" : mssg = "The Specifed New Application User Account could NOT be Located is Shell Database!"
                    //case "mNwUsa" : mssg = "The Specifed New Application User Account's Email could NOT be retrieved from Shell Database!. Please, Try Again";

                    //case "sEdUsa" : mssg = "Application User Account Information was Successfully Updated to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fEdUsa" : mssg = "Application User Account Information Update to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "nEdUsa" : mssg = "The Specifed New Application User Account could NOT be Located is Shell Database!"
                    //case "mEdUsa" : mssg = "The Specifed New Application User Account's Email could NOT be retrieved from Shell Database!. Please, Try Again";

                    //case "sEdUsF" : mssg = "Application User Account 'Function' was Successfully Updated to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fEdUsF" : mssg = "Application User Account 'Function' Update to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";

                    //case "sEdUsS" : mssg = "Application User Account Status was Successfully Updated to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fEdUsS" : mssg = "Application User Account Status Update to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //   // 'edit user role
                    //case "sEdUsR" : mssg = "Application User Access Role was Successfully Updated to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fEdUsR" : mssg = "Application User Access Role Update to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //   // 'add cost element
                    //case "sNwOpx" : mssg = "New OPEX Cost Element(s) was Successfully Added to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fNwOpx" : mssg = "New OPEX Cost Element(s) Addition to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "eNwOpx" : mssg = "There was an Error Adding New OPEX Cost Element(s) to " + globalCodes.c_sWebAppNamex + " Database, Please Try Again";
                    //case "dNwOpx" : mssg = "Unable to Process Specified OPEX Cost Element(s). Please, Try Again and Select Your SAP Cost Object";
                    //    //'delete cost element
                    //case "sDlOpx" : mssg = "Existing OPEX Cost Element(s) was Successfully Deleted from " + globalCodes.c_sWebAppNamex + " Database";
                    //case "eDlOpx" : mssg = "There was an Error Deleting Existing OPEX Cost Element(s) from " + globalCodes.c_sWebAppNamex + " Database, Please Try Again";
                    //case "sDlOpxBk" : mssg = "Your OPEX Cost Element Entries had been DELETED from " + globalCodes.c_sWebAppNamex + " Database.";
                    //case "nDlOpxBk" : mssg = "No OPEX Cost Element Entries is Currently Listed for You"

                    //case "sEdOpx" : mssg = "Existing OPEX Cost Element was Successfully Upadted to " + globalCodes.c_sWebAppNamex + " Database";
                    //case "fEdOpx" : mssg = "Existing OPEX Cost Element Update to " + globalCodes.c_sWebAppNamex + " Database Failed, Please Try Again";
                    //case "eEdOpx" : mssg = "There was an Error Updating Existing OPEX Cost Element to " + globalCodes.c_sWebAppNamex + " Database, Please Try Again";
                    //case "xEdOpx" : mssg = "The Specifed Existing OPEX Cost Element was NOT Found in the " + globalCodes.c_sWebAppNamex + " Database";
                    //case "kEdOpx" : mssg = "You do NOT have the Necessary Credentials to Modify the Specified OPEX Cost Element. Task is Aborted";
                    //    //'config corporate user
                    //case "sEdCor" : mssg = "Changes in New Corporate Account User was Successful";
                    //case "nEdCor" : mssg = "No List of " + appUsers.userRoleDesc(appUsers.userRole.focalPoint) + " Account Users was Found";
                    //case "fEdCor" : mssg = "There was an Error in Saving New Corporate Account User";
                    //    //'bulk uploads
                    //case "sPrdBul" :
                    //    string sData = this.reqQuerys("Dat");
                    //    if (sData.Length == 0) sData = "0";
                    //    mssg = sData + " Total OPEX Cost Element Entries was Successfully Uploaded";
                    //case "sCasBul":
                    //    sData = this.reqQuerys("Dat");
                    //    if (sData.Length == 0) sData = "0";
                    //    mssg = sData + " Total OPEX Cost Element Entries was Successfully Cascaded";
                    //case "vCasPrev" : mssg = "Your Previous Year OPEX Data is more than 30. On-Screen Cascade Tool is NOT Supported for this Task, Please, Use the 'Microsoft Excel Download' Tool to Migrate Your Data";
                    //case "xCasPrev" : mssg = "You have some OPEX Data Entry for this Year. You need to 'BULK Delete' in order to Cascade Your Previous Year OPEX Data";
                    //case "ePrdBul" : mssg = "There was an Error Processing OPEX Entries Bulk-Load Operation. Task is Aborted";
                    //case "nPrdBul" : mssg = "No File is Provided for the Requested Bulk-Load Operation. Task is Aborted";
                    //case "bPrdBul" : mssg = "An Invalid Microsoft Excel File (*.xls|xlsx) is Provided for the Requested Bulk-Load Operation. Task is Aborted";
                    //    //'global
                    //case "nQry" : mssg = "No Search Criteria was Specified for the Requested Query Task";
                    //case "eRpot" : mssg = "Unable to Generate Report for the Specified Requested Query Task";
                    ////case default(
                    //Debug.Fail(sQueryData);
                    //mssg = "Requested Service MAY have been Processed. Confirm Processing Results";

                    #endregion
                }
                //LblTaskRetMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return mssg;
    }
}