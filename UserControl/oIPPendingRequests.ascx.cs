using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;

public partial class UserControl_oIPPendingRequests : aspnetUserControl
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

            RadMultiPage MP = (RadMultiPage) Parent.FindControl("RadPageView1");

            if (MP.SelectedIndex == 0) (sender as RadGrid).DataSource = RequestHelper.dtLoadRequestsPendingMyReview(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
    }

    public void Requests_Pending_My_Approval()
    {
        try
        {
            //Note: Loads request pending Impacted/Supporting party's review,. He/she cannot see any request not assigned to him/her for review
            grdView.DataSource = RequestHelper.dtLoadRequestsPendingMyReview(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void CancelLink_Click(object sender, EventArgs e)
    {
        using (GridDataItem dataItem = (GridDataItem) ((LinkButton) sender).Parent.Parent)
        {
            long lRequestId = long.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["IDREQUEST"].ToString());
            ApprovalWorkFlow.UpdateRequestFlowStatus(lRequestId, RequestStatus.m_iCancelled);

            grdView.Rebind();
            ajaxWebExtension.showJscriptAlertCx(Page, this, "Request successfully cancelled!!!");
        }        
        //Pending_Requests();
    }


    protected void grdView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    protected void grdView_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var actionLink = (HyperLink) e.Item.FindControl("actionLink");
            var viewCommentLink = (HyperLink) e.Item.FindControl("ViewCommentLink");

            if (viewCommentLink != null)
            {
                viewCommentLink.Attributes["href"] = "javascript:void(0);";
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }

            if (actionLink != null)
            {
                actionLink.Attributes["href"] = "javascript:void(0);";
                actionLink.Attributes["onclick"] = string.Format("return ShowActionForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
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

    

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Rebind")
        {
            grdView.MasterTableView.SortExpressions.Clear();
            grdView.MasterTableView.GroupByExpressions.Clear();
            grdView.Rebind();
        }
        else if (e.Argument == "RebindAndNavigate")
        {
            grdView.MasterTableView.SortExpressions.Clear();
            grdView.MasterTableView.GroupByExpressions.Clear();
            grdView.MasterTableView.CurrentPageIndex = grdView.MasterTableView.PageCount - 1;
            grdView.Rebind();
        }
    }

    protected void grdView_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {

    }
}