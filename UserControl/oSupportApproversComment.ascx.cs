using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class UserControl_oSupportApproversComment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void getIAPPlannerComment(int iUserId, long lRequestId)
    {
        //ApproversComment MyComment = ApprovalWorkFlow.objGetApproversComments(iUserId, lRequestId);

        //iapPlannerLabel.Text = MyComment.m_sFullName;
        //dateReceivedLabel.Text = MyComment.m_sDateReceived;
        //dateReviewedLabel.Text = MyComment.m_sCommentDate;
        //lblTimeReceived.Text = MyComment.m_sTimeReceived;
        //lblTimeReviewed.Text = MyComment.m_sTimeComments;
        //commentLabel.Text = MyComment.m_sComments;
    }

    public void GetApproversComments(long lRequestId)
    {
        grdView.DataSource = ApprovalWorkFlow.getApproversCommentByRequestId(lRequestId);
        requestIdHF.Value = lRequestId.ToString();
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            (sender as RadGrid).DataSource = ApprovalWorkFlow.getApproversCommentByRequestId(long.Parse(requestIdHF.Value));
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
            appUsersRoles.UserRoleReporting(item, 4);
        }

        //if (e.Item is GridDataItem)
        //{
        //    bool found = (from d in grdView.MasterTableView.RenderColumns select d).Any(d => d.UniqueName == "ActionColumn");
        //}
    }
}