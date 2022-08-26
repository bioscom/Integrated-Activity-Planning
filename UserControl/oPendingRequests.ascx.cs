using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.Spreadsheet;
using System.Linq;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using xi = Telerik.Web.UI.ExportInfrastructure;
using System.Drawing;

public partial class UserControl_oPendingRequests : aspnetUserControl
{
    //long lRequestId = 0;
    string sfield = "REQUEST_NUMBER";

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        string alternateText = (sender as ImageButton).AlternateText;

        grdView.ExportSettings.IgnorePaging = true;
        grdView.ExportSettings.Excel.Format = (GridExcelExportFormat) Enum.Parse(typeof(GridExcelExportFormat), alternateText);
        grdView.ExportSettings.ExportOnlyData = true;
        grdView.ExportSettings.OpenInNewWindow = true;
        grdView.MasterTableView.ExportToExcel();
    }

    protected void grdView_HtmlExporting(object sender, GridHTMLExportingEventArgs e)
    {

    }

    protected void grdView_BiffExporting(object sender, GridBiffExportingEventArgs e)
    {

    }

    protected void grdView_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
    {
        foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
        {
            row.Cells[0].StyleValue = "Style1";
        }

        StyleElement style = new StyleElement("Style1");
        style.InteriorStyle.Pattern = InteriorPatternType.Solid;
        style.InteriorStyle.Color = Color.LightGray;
        e.WorkBook.Styles.Add(style);
    }

    protected void grdView_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            grdView.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            //grdView.ExportSettings.IgnorePaging = CheckBox1.Checked;
            grdView.ExportSettings.ExportOnlyData = true;
            grdView.ExportSettings.OpenInNewWindow = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Session["DataField"] = sfield;
    }

    protected void grdView_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
    {
        string DataField = (e.Column as IGridDataColumn).GetActiveDataField();
        Session["DataField"] = DataField;

        DataTable GetDataTable = new DataTable();
        if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Approver) GetDataTable = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
        else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Owner) GetDataTable = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
        else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.AllRequests) GetDataTable = RequestHelper.dtLoadPendingRequestForms(DataField);

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
            string DataField = Session["DataField"].ToString();

            if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Approver)
            {
                (sender as RadGrid).DataSource = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, DataField);
            }
            else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Owner)
            {
                (sender as RadGrid).DataSource = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, DataField);
            }
            else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.AllRequests)
            {
                (sender as RadGrid).DataSource = RequestHelper.dtLoadPendingRequestForms(DataField);
            }
        }
    }

    public void Requests_Pending_My_Approval(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            grdView.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            GridColumn EditColumn = grdView.MasterTableView.Columns.FindByUniqueName("EditColumn");
            EditColumn.Visible = false;

            GridColumn Action = grdView.MasterTableView.Columns.FindByUniqueName("ActionColumn");
            GridColumn ApproverActionColumn = grdView.MasterTableView.Columns.FindByUniqueName("ApproverActionColumn");

            if(oSessnx.getOnlineUser.m_iUserRole == (int) appUsersRoles.userRole.IAPPlanner)
            {
                Action.Visible = true;
                ApproverActionColumn.Visible = false;

                ApprovalWorkFlow.RunSLA();
            }
            else
            {
                Action.Visible = false;
                ApproverActionColumn.Visible = true;
            }

            //Note: Loads request assigned to the login User. He/she cannot see any request not assigned to him/her
            grdView.DataSource = RequestHelper.dtLoadRequestsPendingMyApproval(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void My_Pending_Requests(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            GridColumn Action = grdView.MasterTableView.Columns.FindByUniqueName("ActionColumn");
            Action.Visible = false;

            GridColumn ApproverActionColumn = grdView.MasterTableView.Columns.FindByUniqueName("ApproverActionColumn");
            ApproverActionColumn.Visible = false;

            GridColumn ReRoute = grdView.MasterTableView.Columns.FindByUniqueName("ReRouteColumn");
            ReRoute.Visible = false;


            //Note: Load Request Forms owned by the login User, cannot see anyone not raised by him/her
            grdView.DataSource = RequestHelper.dtLoadMyPendingRequest(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Pending_Requests(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            GridColumn EditColumn = grdView.MasterTableView.Columns.FindByUniqueName("EditColumn");
            EditColumn.Visible = false;

            GridColumn Action = grdView.MasterTableView.Columns.FindByUniqueName("ActionColumn");
            Action.Visible = false;

            GridColumn ReRoute = grdView.MasterTableView.Columns.FindByUniqueName("ReRouteColumn");
            ReRoute.Visible = false;

            GridColumn Cancelling = grdView.MasterTableView.Columns.FindByUniqueName("CancelColumn");
            Cancelling.Visible = false;

            GridColumn ApproverActionColumn = grdView.MasterTableView.Columns.FindByUniqueName("ApproverActionColumn");
            ApproverActionColumn.Visible = false;

            //Note: Load all Requests
            grdView.DataSource = RequestHelper.dtLoadPendingRequestForms(sfield);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //ddlArea.ClearSelection();

        //ddlLocation.Items.Clear();
        //ddlLocation.Items.Add(new ListItem("Select Location", "-1"));
        
        //ddlPlanType.ClearSelection();
        //ddlOriginator.ClearSelection();
        //ddlStatus.ClearSelection();

        //RequestHelper.LoadPendingRequestForms(requestsGridView, SortExpression);
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
    }

    protected void grdView_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    {
        if (e.Column.UniqueName == "status")
        {
            GridBoundColumn boundColumn = e.Column as GridBoundColumn;
            if (boundColumn != null)
            {
                boundColumn.AllowFiltering = false;
                boundColumn.AllowSorting = false;
                boundColumn.ShowFilterIcon = false;
            }
        }
    }

    protected void grdView_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var editLink = (HyperLink) e.Item.FindControl("editLink");
            var actionLink = (HyperLink) e.Item.FindControl("actionLink");
            var approverLink = (HyperLink) e.Item.FindControl("approverLink");
            var viewCommentLink = (HyperLink) e.Item.FindControl("ViewCommentLink");
            var reRouteLink = (HyperLink) e.Item.FindControl("ReRouteLink");

            if (viewCommentLink != null)
            {
                viewCommentLink.Attributes["href"] = "javascript:void(0);";
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentFormPending('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }

            if (editLink != null)
            {
                editLink.Attributes["href"] = "javascript:void(0);";
                editLink.Attributes["onclick"] = string.Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }

            if (actionLink != null)
            {
                actionLink.Attributes["href"] = "javascript:void(0);";
                actionLink.Attributes["onclick"] = string.Format("return ShowActionForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }

            if (approverLink != null)
            {
                approverLink.Attributes["href"] = "javascript:void(0);";
                approverLink.Attributes["onclick"] = string.Format("return ShowActionFormApprovers('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }

            if (reRouteLink != null)
            {
                reRouteLink.Attributes["href"] = "javascript:void(0);";
                reRouteLink.Attributes["onclick"] = string.Format("return ShowRerouteForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
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

        //if (e.Item is GridDataItem)
        //{
        //    bool found = (from d in grdView.MasterTableView.RenderColumns select d).Any(d => d.UniqueName == "ActionColumn");
        //}
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