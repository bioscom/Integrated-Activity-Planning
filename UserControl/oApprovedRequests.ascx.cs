using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Web.UI;
using System.Linq;
using System.Data;
using Telerik.Web.UI.GridExcelBuilder;
using System.Drawing;

public partial class UserControl_oApprovedRequests : aspnetUserControl
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

    protected void grdView_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
    {
        string DataField = (e.Column as IGridDataColumn).GetActiveDataField();

        DataTable GetDataTable = new DataTable();

        if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Approver)
        {
            GetDataTable = RequestHelper.dtRequestsIApproved(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
        else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Owner)
        {
            GetDataTable = RequestHelper.dtMyApprovedRequests(oSessnx.getOnlineUser.m_iUserId, DataField);
        }
        else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.AllRequests)
        {
            GetDataTable = RequestHelper.dtRequestsByStatus(RequestStatus.m_iApproved, DataField);
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
            string DataField = Session["DataField"].ToString();

            if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Approver)
                (sender as RadGrid).DataSource = RequestHelper.dtRequestsIApproved(oSessnx.getOnlineUser.m_iUserId, DataField);
            else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.Owner)
                (sender as RadGrid).DataSource = RequestHelper.dtMyApprovedRequests(oSessnx.getOnlineUser.m_iUserId, DataField);
            else if (int.Parse(RequestorTypeHF.Value) == (int) commonEnums.RequestorType.AllRequests)
                (sender as RadGrid).DataSource = RequestHelper.dtRequestsByStatus(RequestStatus.m_iApproved, DataField);
        }
    }

    public void My_Approved_Requests(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            //Note: Load Request Forms owned by the login User, cannot see anyone not owned by him/her
            grdView.DataSource = RequestHelper.dtMyApprovedRequests(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Requests_I_Approved(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            //Note: Load Request Forms I approved
            grdView.DataSource = RequestHelper.dtRequestsIApproved(oSessnx.getOnlineUser.m_iUserId, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Approved_Requests(int iRequestorType)
    {
        try
        {
            RequestorTypeHF.Value = iRequestorType.ToString();

            //Note: Load all Request Forms
            grdView.DataSource = RequestHelper.dtRequestsByStatus(RequestStatus.m_iApproved, sfield);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
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
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentFormApproved('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
            }


            if (reportLink != null)
            {
                reportLink.Attributes["href"] = "javascript:void(0);";
                reportLink.Attributes["onclick"] = string.Format("return ShowReportFormApproved('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
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
            RequestStatus.StatusReporting2(item, 10);
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
