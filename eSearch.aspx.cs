using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class eSearch : System.Web.UI.Page
{
    long lRequestId = 0;
    string SortExpression = "";
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["lRequestId"] !=null)
        {
            LoadData(long.Parse(Request.QueryString["lRequestId"].ToString()));
        }      
    }

    private void LoadData(long lRequestId)
    {
        if (Request.QueryString["lRequestId"] != null)
        {
            grdView.DataSource = RequestHelper.dtGetRequestByRequestId(lRequestId, sfield);
        }
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            if (Request.QueryString["lRequestId"] != null)
            {
                string DataField = Session["DataField"].ToString();
                (sender as RadGrid).DataSource = RequestHelper.dtGetRequestByRequestId(lRequestId, DataField);
            }
        }
    }

    protected void grdView_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var viewCommentLink = (HyperLink) e.Item.FindControl("ViewCommentLink");
            var reRouteLink = (HyperLink) e.Item.FindControl("ReRouteLink");

            if (viewCommentLink != null)
            {
                viewCommentLink.Attributes["href"] = "javascript:void(0);";
                viewCommentLink.Attributes["onclick"] = string.Format("return ShowCommentForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDREQUEST"], e.Item.ItemIndex);
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
            var lbl = e.Item.FindControl("numberLabel") as Label;
            if (lbl != null)
                lbl.Text = (e.Item.ItemIndex + 1).ToString();

            var item = e.Item as GridDataItem;
            RequestStatus.StatusReporting(item, 9);
        }

        //if (e.Item is GridDataItem)
        //{
        //    bool found = (from d in grdView.MasterTableView.RenderColumns select d).Any(d => d.UniqueName == "ActionColumn");
        //}
    }

    protected void grdView_ItemCommand(object sender, GridCommandEventArgs e)
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
}
