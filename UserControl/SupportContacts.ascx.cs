using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class UserControl_SupportContacts : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindUserData()
    {
        grdView.DataSource = UsersActions.getIAPUsers();
    }

    protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    //protected void UsersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    UsersGridView.PageIndex = e.NewPageIndex;
    //    BindUserData();
    //}

    protected void submitBtn_Click(object sender, EventArgs e)
    {
        string UserId = "";
        UsersActions.ResetSupportContacts();

        //foreach (GridViewRow grdRow in UsersGridView.Rows)
        //{
        //    CheckBox ckbSupportContacts = (CheckBox)grdRow.FindControl("supportContact");
        //    UserId = ckbSupportContacts.Attributes["USERID"].ToString();
        //    if (ckbSupportContacts.Checked == true)
        //    {
        //        UsersActions.UpdateSupportContacts(UserId);
        //    }
        //}

        foreach(GridDataItem o in grdView.Items)
        {
            CheckBox ckbSupport = (CheckBox) o.FindControl("supportContact");
            UserId = o.OwnerTableView.DataKeyValues[o.ItemIndex]["IDUSER"].ToString();
            if (ckbSupport.Checked == true)
            {
                UsersActions.UpdateSupportContacts(UserId);
            }
        }
        grdView.Rebind();
        //BindUserData();
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
        int iSupportContact = -1;

        if (e.Item is GridDataItem)
        {
            var editLink = (HyperLink) e.Item.FindControl("editLink");
            CheckBox ckbSupport = (CheckBox) e.Item.FindControl("supportContact");
            Label lbUserRole = (Label) e.Item.FindControl("lblUserRole");

            if (editLink != null)
            {
                editLink.Attributes["href"] = "javascript:void(0);";
                editLink.Attributes["onclick"] = string.Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IDUSER"], e.Item.ItemIndex);
            }

            if(ckbSupport != null)
            {
                iSupportContact = int.Parse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CURRENT_ROLE_HOLDER"].ToString());

                if (iSupportContact == 1)
                {
                    ckbSupport.Checked = true;
                   e.Item.BackColor = System.Drawing.Color.Green;
                }
            }

            if (lbUserRole != null)
            {
                int iRoleId = int.Parse(lbUserRole.Text);
                lbUserRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole) iRoleId);
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
            int rowCounter = new int();
            Label lbl = e.Item.FindControl("numberLabel") as Label;
            rowCounter = grdView.MasterTableView.PageSize * grdView.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

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
}
