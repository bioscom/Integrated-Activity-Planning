using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;

public partial class UserControl_oIPApprovedRequests : aspnetUserControl
{
    //long lRequestId = 0;
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void grdView_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
    {
        string DataField = (e.Column as IGridDataColumn).GetActiveDataField();
        DataTable GetDataTable = new DataTable();

        RadMultiPage MP = (RadMultiPage)Parent.FindControl("RadMultiPage1");

        if (MP.SelectedIndex == 0) GetDataTable = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
        else if (MP.SelectedIndex == 1) GetDataTable = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
        else if (MP.SelectedIndex == 2) GetDataTable = RequestHelper.dtLoadPendingRequestForms(DataField);

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

            RadMultiPage MP = (RadMultiPage) Parent.FindControl("RadMultiPage1");

            if (MP.SelectedIndex == 1) (sender as RadGrid).DataSource = RequestHelper.dtRequestsIReviewed(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
    }

    public void Requests_I_Approved()
    {
        try
        {
            grdView.DataSource = RequestHelper.dtRequestsIReviewed(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void grdView_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void grdView_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
           
            var viewCommentLink = (HyperLink) e.Item.FindControl("ViewCommentLink");
            var reportLink = (HyperLink) e.Item.FindControl("reportLink");

            if (viewCommentLink != null)
            {
                viewCommentLink.Attributes["href"] = "javascript:void(0);";
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentForm2('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }


            if (reportLink != null)
            {
                reportLink.Attributes["href"] = "javascript:void(0);";
                reportLink.Attributes["onclick"] = string.Format("return ShowReportForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }
        }
    }

    protected void grdView_ItemDataBound(object sender, GridItemEventArgs e)
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

    protected void grdView_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }

    protected void grdView_ItemInserted(object sender, GridInsertedEventArgs e)
    {

    }

    protected void grdView_ItemUpdated(object sender, GridUpdatedEventArgs e)
    {

    }

    protected void grdView_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        grdView.DataSource = RequestHelper.dtRequestsByStatus(RequestStatus.m_iApproved, sfield);
    }
}
