using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;

public partial class UserControl_oIPRejectedRequests : aspnetUserControl
{
    long lRequestId = 0;
    string SortExpression = "";
    string sfield = "REQUEST_NUMBER";

    protected void grdView_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
    {
        string DataField = (e.Column as IGridDataColumn).GetActiveDataField();

        DataTable GetDataTable = new DataTable();

        //GetDataTable = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
        //GetDataTable = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
        //GetDataTable = RequestHelper.dtLoadPendingRequestForms(DataField);

        RadMultiPage MP = (RadMultiPage)Parent.FindControl("RadMultiPage1");
        //RadPageView RV1 = (RadPageView)MP.FindControl("RadPageView1");
        //RadPageView RV2 = (RadPageView)MP.FindControl("RadPageView2");
        //RadPageView RV3 = (RadPageView)MP.FindControl("RadPageView3");

        if (MP.SelectedIndex == 0)
        {
            GetDataTable = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
        else if (MP.SelectedIndex == 1)
        {
            GetDataTable = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
        else if (MP.SelectedIndex == 2)
        {
            GetDataTable = RequestHelper.dtLoadPendingRequestForms(DataField);
        }

        e.ListBox.DataSource = GetDataTable;
        e.ListBox.DataKeyField = DataField;
        e.ListBox.DataTextField = DataField;
        e.ListBox.DataValueField = DataField;
        e.ListBox.DataBind();
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            string DataField = ((sender as RadGrid).Columns as IGridDataColumn).GetActiveDataField();

            RadMultiPage MP = (RadMultiPage)Parent.FindControl("RadPageView1");

            if (MP.SelectedIndex == 0)
                (sender as RadGrid).DataSource = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
            else if (MP.SelectedIndex == 1)
                (sender as RadGrid).DataSource = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
            else if (MP.SelectedIndex == 2)
                (sender as RadGrid).DataSource = RequestHelper.dtLoadPendingRequestForms(DataField);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void My_Requests_Rejected()
    {
        try
        {
            //Note: Load My Request Forms Rejected
            grdView.DataSource = RequestHelper.dtMyRejectedRequests(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Requests_I_Rejected()
    {
        try
        {
            //Note: Load Request Forms loads I did not approve.
            grdView.DataSource = RequestHelper.dtRequestsIDidNotApprove(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Rejected_Requests()
    {
        try
        {
            //Note: Load all Request Forms
            grdView.DataSource = RequestHelper.dtRejectedRequests(sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void grdView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    protected void grdView_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            var viewCommentLink = (HyperLink) e.Item.FindControl("ViewCommentLink");
            var reportLink = (HyperLink) e.Item.FindControl("reportLink");

            if (viewCommentLink != null)
            {
                viewCommentLink.Attributes["href"] = "javascript:void(0);";
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }


            if (reportLink != null)
            {
                reportLink.Attributes["href"] = "javascript:void(0);";
                reportLink.Attributes["onclick"] = string.Format("return ShowReportForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }
        }
    }

    protected void grdView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter = new int();
            Label lbl = e.Item.FindControl("numberLabel") as Label;
            rowCounter = grdView.MasterTableView.PageSize * grdView.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            var item = e.Item as GridDataItem;
            RequestStatus.StatusReporting(item, 9);
        }

        if (e.Item is GridDataItem)
        {
            bool found = (from d in grdView.MasterTableView.RenderColumns select d).Any(d => d.UniqueName == "ActionColumn");
        }
    }

    protected void grdView_ItemDeleted(object sender, Telerik.Web.UI.GridDeletedEventArgs e)
    {

    }

    protected void grdView_ItemInserted(object sender, Telerik.Web.UI.GridInsertedEventArgs e)
    {

    }

    protected void grdView_ItemUpdated(object sender, Telerik.Web.UI.GridUpdatedEventArgs e)
    {

    }

    protected void grdView_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {

    }


    public void FilterPendingRequest(int iOu, int iLocation, int iPlanType, int iOriginator, int iStatus)
    {
        //requestsGridView.Columns[9].Visible = false; //TODO: find a way to use the column name rather than using the column index umber
        
    }
}