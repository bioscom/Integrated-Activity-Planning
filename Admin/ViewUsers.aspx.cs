using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Telerik.Web.UI;

public partial class Admin_ViewUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //appUsersRoles.getUserRoles(ddlRoles);
        }
    }

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getUsersByRole(int.Parse(ddlRoles.SelectedValue));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);

        //LinkButton lnkDelete = (LinkButton)row.FindControl("deleteLinkButton");
        //int userID = int.Parse(lnkDelete.Attributes["USERID"].ToString());

        //bool success = appUsersHelper.disableUserAccount(userID);
        //if (success)
        //{
        //    ajaxWebExtension.showJscriptAlert(Page, this, "User successfully disabled.");
        //}
        try
        {
            using (GridDataItem dataItem = (GridDataItem)((LinkButton)sender).Parent.Parent)
            {
                int UserID = int.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["IDUSER"].ToString());
                bool success = UsersActions.DeleteUser(UserID);
                if (success)
                {
                    grdView.Rebind();
                    ajaxWebExtension.showJscriptAlert(Page, this, "User successfully disabled.");
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void ddlUsers_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        DataTable dt = !string.IsNullOrEmpty(e.Text) ? UsersActions.dtGetUserBySearch(e.Text) : null;
        RadComboControlLoader(dt, ddlUsers);
    }

    private void RadComboControlLoader(DataTable dt, RadComboBox RadDdl)
    {
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var item = new RadComboBoxItem();
                item.Text = (string) dr["FULLNAME"];
                item.Value = dr["IDUSER"].ToString();

                string email = dr["USERMAIL"].ToString();
                item.Attributes.Add("USERMAIL", email);

                RadDdl.Items.Add(item);
                item.DataBind();
            }
        }
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            grdView.DataSource = UsersActions.dtGetUserByUserId(int.Parse(ddlUsers.SelectedValue));
            grdView.Rebind();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            (sender as RadGrid).DataSource = UsersActions.getIAPUsers();
        }
    }

    protected void grdView_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var editLink = (HyperLink) e.Item.FindControl("editLink");

            if (editLink != null)
            {
                editLink.Attributes["href"] = "javascript:void(0);";
                editLink.Attributes["onclick"] = string.Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDUSER"], e.Item.ItemIndex);
            }
        }

        //if (e.Item is GridNestedViewItem)
        //{
        //    EIdd.eIddUsers oCP = o.CheckIfLogInUserIsCpFocalPoint(oSessnx.getOnlineUser.m_iUserId);
        //    if (oCP.m_iUserId != 0)
        //    {
        //        var panel = (Panel) e.Item.FindControl("pnlAssignAnalyst");
        //        if (panel != null)
        //        {
        //            panel.Visible = true;
        //        }

        //        var lnkRejectRequest = (LinkButton) e.Item.FindControl("lnkRejectRequest");
        //        lnkRejectRequest.Visible = true;
        //    }
        //}

        //if (e.Item is GridNestedViewItem) /**/
        //{
        //    (e.Item.FindControl("DocumentsGrid") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(DocumentsGrid_NeedDataSource);
        //    //(e.Item.FindControl("DocumentsGrid") as RadGrid).DataSource; 

        //}
    }

    protected void grdView_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = e.DetailTableView.ParentItem;

        long lRequestId = long.Parse(dataItem.GetDataKeyValue("IDUSER").ToString());
        //e.DetailTableView.DataSource = oM.dtGetDocsByRequestId(lRequestId);

        //switch (e.DetailTableView.Name)
        //{
        //    case "Docs":
        //        {
        //            long lRequestId = long.Parse(dataItem.GetDataKeyValue("REQUESTID").ToString());
        //            e.DetailTableView.DataSource = o.dtGetDocsByRequestId(lRequestId);

        //            break;
        //        }

        //    case "OrderDetails":
        //        {
        //            string OrderID = dataItem.GetDataKeyValue("OrderID").ToString();
        //            e.DetailTableView.DataSource = null;
        //            break;
        //        }
        //}
    }

    protected void grdView_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var lbl = e.Item.FindControl("numberLabel") as Label;
            if (lbl != null)
                lbl.Text = (e.Item.ItemIndex + 1).ToString();

            //Field Visit
            var item = e.Item as GridDataItem;

            appUsersRoles.UserRoleReporting(item, 5);
        }
    }

    protected void grdView_ItemCommand(object source, GridCommandEventArgs e)
    {
        //Session["RowIndex"] = e.Item.ItemIndex.ToString();

        //if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        //{
        //    var details = (ASP.app_idd_usercontrol_odetails_ascx) ((GridDataItem) e.Item).ChildItem.FindControl("oDetails");
        //    if (!e.Item.Expanded)
        //    {
        //        long lRequestId = long.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["REQUESTID"].ToString());
        //        details.ViewDetails(lRequestId);
        //    }
        //}

        //if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
        //{
        //    GridEditCommandColumn editColumn = (GridEditCommandColumn) grdView.MasterTableView.GetColumn("EditCommandColumn");
        //    editColumn.Visible = false;
        //}
        //else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
        //{
        //    e.Canceled = true;
        //}
        //else
        //{
        //    GridEditCommandColumn editColumn = (GridEditCommandColumn) grdView.MasterTableView.GetColumn("EditCommandColumn");
        //    if (!editColumn.Visible)
        //        editColumn.Visible = true;
        //}
    }

    protected void grdView_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (grdView.MasterTableView.Items.Count > 0)
            //    grdView.MasterTableView.Items[0].Expanded = true;
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        using (GridDataItem dataItem = (GridDataItem) ((LinkButton) sender).Parent.Parent)
        {
            //long lId = long.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["DOCSID"].ToString());
            //oM.deleteDocById(lId);
            ////grdView.Rebind();
            //ajaxWebExtension.showJscriptAlertCx(Page, this, "Document deleted successfully!!!");
        }
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