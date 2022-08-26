using System.Drawing;
using Telerik.Web.UI;

/// <summary>
/// Summary description for ctrStatus
/// </summary>
public static class RequestStatus
{
    public static string m_sActivityOwner;
    public static string m_sIAPPlanner;
    public static string m_sFunctionalRepresentatives;
    public static string m_sFinance;
    public static string m_sProductionAssetRepresentative;
    public static string m_sChangeReviewBoardChairman;
    public static string m_sApproved;
    public static string m_sNotApproved;
    public static string m_sNotApprovedByPlanner;
    public static string m_sNotApprovedByFunctionalAuthoriser;
    public static string m_sNotApprovedByProductionAssetAuthorizer;
    public static string m_sNotApprovedByChairmanReviewBoard;
    public static string m_sCancelled;
    public static string m_sRestored;

    public static int m_iActivityOwner;
    public static int m_iIAPPlanner;
    public static int m_iFunctionalRepresentatives;
    public static int m_iFinance;
    public static int m_iProductionAssetRepresentative;
    public static int m_iChangeReviewBoardChairman;
    public static int m_iApproved;
    public static int m_iNotApproved;
    public static int m_iNotApprovedByPlanner;
    public static int m_iNotApprovedByFunctionalAuthoriser;
    public static int m_iNotApprovedByProductionAssetAuthorizer;
    public static int m_iNotApprovedByChairmanReviewBoard;
    public static int m_iCancelled;
    public static int m_iDefault;
    public static int m_iRestored;

    public static int m_iDropDownListFirstIndexValue;

    public static string m_sBadChange;
    public static string m_sGoodChange;
    public static int m_iBadChange;
    public static int m_iGoodChange;
    public static string m_sNeutral;
    public static int m_iNeutral;



    static RequestStatus()
    {
        m_sActivityOwner = "Activity Owner yet to forward request";
        m_sIAPPlanner = "Pending Asset IAP Planner's Support";
        m_sFunctionalRepresentatives = "Pending Functional Authorizer's support";
        m_sFinance = "Pending Finance Support";
        m_sProductionAssetRepresentative = "Pending Production Asset Authorizer's support";
        m_sChangeReviewBoardChairman = "Pending Change Review Board Chairman's approval";
        m_sNotApprovedByPlanner = "Rejected By Planner";
        m_sNotApprovedByFunctionalAuthoriser = "Rejected By Functional Authoriser";
        m_sNotApprovedByProductionAssetAuthorizer = "Rejected By Production Asset Authorizer";
        m_sNotApprovedByChairmanReviewBoard = "Rejected By Chairman Review Board";

        m_sApproved = "Approved";
        m_sNotApproved = "Rejected";
        m_sCancelled = "Cancelled";
        m_sRestored = "Restored";

        m_iActivityOwner = 1;
        m_iIAPPlanner = 2;
        m_iFunctionalRepresentatives = 3;
        m_iFinance = -3;
        m_iProductionAssetRepresentative = 4;
        m_iChangeReviewBoardChairman = 5;
        m_iApproved = 6;

        m_iNotApproved = 7;
        m_iCancelled = 8;
        m_iNotApprovedByPlanner = 9;
        m_iNotApprovedByFunctionalAuthoriser = 10;
        m_iNotApprovedByProductionAssetAuthorizer = 11;
        m_iNotApprovedByChairmanReviewBoard = 12;
        m_iDefault = 0;

        m_iRestored = -1;
        m_iDropDownListFirstIndexValue = -1;


        //Jan 2020 update
        m_sBadChange = "Bad Change";
        m_sGoodChange = "Good Change";
        m_sNeutral = "Neutral";

        m_iBadChange = 14;
        m_iGoodChange = 15;
        m_iNeutral = 16;
    }

    public static void StatusReporting(GridDataItem item, int iColumn)
    {
        if (item.Cells[iColumn].Text == m_iActivityOwner.ToString())
        {
            item.Cells[iColumn].Text = m_sActivityOwner;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iIAPPlanner.ToString())
        {
            item.Cells[iColumn].Text = m_sIAPPlanner;
            item.Cells[iColumn].ForeColor = Color.Orange;
        }
        else if (item.Cells[iColumn].Text == m_iFunctionalRepresentatives.ToString())
        {
            item.Cells[iColumn].Text = m_sFunctionalRepresentatives;
            item.Cells[iColumn].ForeColor = Color.Brown;
        }

        else if (item.Cells[iColumn].Text == m_iFinance.ToString())
        {
            item.Cells[iColumn].Text = m_sFinance;
            item.Cells[iColumn].ForeColor = Color.Brown;
        }

        else if (item.Cells[iColumn].Text == m_iProductionAssetRepresentative.ToString())
        {
            item.Cells[iColumn].Text = m_sProductionAssetRepresentative;
            item.Cells[iColumn].ForeColor = Color.Navy;
        }
        else if (item.Cells[iColumn].Text == m_iChangeReviewBoardChairman.ToString())
        {
            item.Cells[iColumn].Text = m_sChangeReviewBoardChairman;
            item.Cells[iColumn].ForeColor = Color.Teal;
        }

        else if (item.Cells[iColumn].Text == m_iApproved.ToString())
        {
            item.Cells[iColumn].Text = m_sApproved;
            item.Cells[iColumn].ForeColor = Color.Green;
        }
        else if (item.Cells[iColumn].Text == m_iRestored.ToString())
        {
            item.Cells[iColumn].Text = m_sRestored;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iCancelled.ToString())
        {
            item.Cells[iColumn].Text = m_sCancelled;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iNotApprovedByChairmanReviewBoard.ToString())
        {
            item.Cells[iColumn].Text = m_sNotApprovedByChairmanReviewBoard;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iNotApprovedByFunctionalAuthoriser.ToString())
        {
            item.Cells[iColumn].Text = m_sNotApprovedByFunctionalAuthoriser;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iNotApprovedByPlanner.ToString())
        {
            item.Cells[iColumn].Text = m_sNotApprovedByPlanner;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iNotApprovedByProductionAssetAuthorizer.ToString())
        {
            item.Cells[iColumn].Text = m_sNotApprovedByProductionAssetAuthorizer;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
    }

    public static void StatusReporting2(GridDataItem item, int iColumn)
    {
        if (item.Cells[iColumn].Text == m_iBadChange.ToString())
        {
            item.Cells[iColumn].Text = m_sBadChange;
            item.Cells[iColumn].ForeColor = Color.Red;
        }
        else if (item.Cells[iColumn].Text == m_iGoodChange.ToString())
        {
            item.Cells[iColumn].Text = m_sGoodChange;
            item.Cells[iColumn].ForeColor = Color.Green;
        }
        else if (item.Cells[iColumn].Text == m_iNeutral.ToString())
        {
            item.Cells[iColumn].Text = m_sNeutral;
            item.Cells[iColumn].ForeColor = Color.DarkGray;
        }
        else
        {
            item.Cells[iColumn].Text = "";
        }
    }


    public static string StatusReporting(int iStatus)
    {
        string sResult = "";
        if (iStatus == m_iActivityOwner) sResult = m_sActivityOwner;
        else if (iStatus == m_iIAPPlanner) sResult = m_sIAPPlanner;
        else if (iStatus == m_iFunctionalRepresentatives) sResult = m_sFunctionalRepresentatives;
        else if (iStatus == m_iFinance) sResult = m_sFinance;
        else if (iStatus == m_iProductionAssetRepresentative) sResult = m_sProductionAssetRepresentative;
        else if (iStatus == m_iChangeReviewBoardChairman) sResult = m_sChangeReviewBoardChairman;
        else if (iStatus == m_iApproved) sResult = m_sApproved;
        else if (iStatus == m_iRestored) sResult = m_sRestored;
        else if (iStatus == m_iCancelled) sResult = m_sCancelled;
        else if (iStatus == m_iNotApprovedByChairmanReviewBoard) sResult = m_sNotApprovedByChairmanReviewBoard;
        else if (iStatus == m_iNotApprovedByFunctionalAuthoriser) sResult = m_sNotApprovedByFunctionalAuthoriser;
        else if (iStatus == m_iNotApprovedByPlanner) sResult = m_sNotApprovedByPlanner;
        else if (iStatus == m_iNotApprovedByProductionAssetAuthorizer) sResult = m_sNotApprovedByProductionAssetAuthorizer;

        return sResult;
    }
}


public static class RequestStatusNotApproved
{
    public static string m_sActivityOwner;
    public static string m_sIAPPlanner;
    public static string m_sFunctionalRepresentatives;
    public static string m_sProductionAssetRepresentative;
    public static string m_sChangeReviewBoardChairman;
    public static string m_sApproved;
    public static string m_sNotApproved;
    public static string m_sCancelled;
    public static string m_sRestored;

    public static int m_iActivityOwner;
    public static int m_iIAPPlanner;
    public static int m_iFunctionalRepresentatives;
    public static int m_iProductionAssetRepresentative;
    public static int m_iChangeReviewBoardChairman;
    public static int m_iApproved;
    public static int m_iNotApproved;
    public static int m_iCancelled;
    public static int m_iDefault;
    public static int m_iRestored;

    public static int m_iDropDownListFirstIndexValue;


    static RequestStatusNotApproved()
    {
        m_sActivityOwner = "Activity Owner yet to forward request";
        m_sIAPPlanner = "Not supported by IAP Planner";
        m_sFunctionalRepresentatives = "Not supported by Functional Authorizers";
        m_sProductionAssetRepresentative = "Not supported by Production Asset Authorizer";
        m_sChangeReviewBoardChairman = "Not approved by Change Review Board Chairman";

        m_sApproved = "Approved";
        m_sNotApproved = "Rejected";
        m_sCancelled = "Cancelled";
        m_sRestored = "Restored";

        m_iActivityOwner = 1;
        m_iIAPPlanner = 2;
        m_iFunctionalRepresentatives = 3;
        m_iProductionAssetRepresentative = 4;
        m_iChangeReviewBoardChairman = 5;
        m_iApproved = 6;
        m_iNotApproved = 7;
        m_iCancelled = 8;
        m_iDefault = 0;

        m_iRestored = -1;
        m_iDropDownListFirstIndexValue = -1;
    }
}


public static class IAPPlannerType
{
    public static string m_sVST;
    public static string m_sST;
    public static string m_sMT;

    static IAPPlannerType()
    {
        m_sVST = "VST";
        m_sST = "ST";
        m_sMT = "MT";
    }
}