using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

public partial class UserControl_AdminProductionParties : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadHubs();
            LoadDataForgrdView();
        }
    }

    private void LoadDataForgrdView()
    {
        grdView.DataSource = UsersActions.dtLocationResponsiblePartys();
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            (sender as RadGrid).DataSource = UsersActions.dtLocationResponsiblePartys();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        using (GridDataItem dataItem = (GridDataItem) ((LinkButton) sender).Parent.Parent)
        {
            int lId = int.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["IDUSER"].ToString());
            int iAreaId = int.Parse(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["IDAREA"].ToString());

            bool bRet = UsersActions.DeleteLocactionResponsibleParty(lId, iAreaId);
            if (bRet) ajaxWebExtension.showJscriptAlertCx(Page, this, "Delete Successful!!!");
            else { ajaxWebExtension.showJscriptAlertCx(Page, this, "Not Successful!!!"); }
            grdView.Rebind();
        }
    }

    protected void grdView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    protected void grdView_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }

    protected void grdView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var lbl = e.Item.FindControl("numberLabel") as Label;
            if (lbl != null)
                lbl.Text = (e.Item.ItemIndex + 1).ToString();
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

    protected void grdView_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            grdView.EditIndexes.Add(0);
            grdView.Rebind();

            //grdView.MasterTableView.Items[0].Expanded = true;
            //grdView.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
        }
    }

    private void LoadHubs()
    {
        var item = new RadComboBoxItem();

        List<Area> lstAreas = RequestFormRequirement.getAreas();
        foreach (Area o in lstAreas)
        {
            item = new RadComboBoxItem();
            item.Text = o.m_sAREA;
            item.Value = o.m_iIDAREA.ToString();
            ddlHubs.Items.Add(item);
            item.DataBind();
        }
    }

    //protected void ddlHubs_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    ddlLocations.Items.Clear();
    //    var item = new RadComboBoxItem();

    //    List<Location> Locations = RequestFormRequirement.getLocationsByArea(int.Parse(ddlHubs.SelectedValue));
    //    foreach (Location o in Locations)
    //    {
    //        item = new RadComboBoxItem();
    //        item.Text = o.m_sLOCATION;
    //        item.Value = o.m_iLOCATIONID.ToString();
    //        ddlLocations.Items.Add(item);
    //        item.DataBind();
    //    }
    //}

    //private void LoadFunctions()
    //{
    //    var item = new RadComboBoxItem();

    //    List<Functions> lstFunctions = IAPFunctions.getFunctions();
    //    foreach (Functions o in lstFunctions)
    //    {
    //        item = new RadComboBoxItem();
    //        item.Text = o.m_sFUNCTION;
    //        item.Value = o.m_iFUNCTIONID.ToString();
    //        ddlLocations.Items.Add(item);
    //        item.DataBind();
    //    }
    //}

    protected void ddlAdmin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        DataTable dt = !string.IsNullOrEmpty(e.Text) ? UsersActions.dtGetUserBySearch(e.Text) : null;
        RadComboControlLoader(dt, ddlAdmin);
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
                //string refInd = dr["REFIND"].ToString();

                item.Attributes.Add("USERMAIL", email);
                //item.Attributes.Add("REFIND", refInd);
                //item.Value += ":" + refInd;

                RadDdl.Items.Add(item);
                item.DataBind();
            }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bRet = UsersActions.AddLocactionResponsibleParty(int.Parse(ddlAdmin.SelectedValue), int.Parse(ddlHubs.SelectedValue));
        LoadDataForgrdView();
        if (bRet)
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, "Successfully added!!!");
        }
        else
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, "Not Successful, please try again!!!");

        }


            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
        //if (bRet) { ajaxWebExtension.showJscriptAlertCx(Page, this, "Successfully added as " + ddlHubs.SelectedItem.Text + " Function Responsible Party!!!"); }
    }

    protected void ddlHubs_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        grdView.DataSource = UsersActions.dtLocationResponsiblePartysByHub(int.Parse(ddlHubs.SelectedValue));
        grdView.Rebind();
    }
}