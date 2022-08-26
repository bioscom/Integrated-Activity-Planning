using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Readiness : aspnetUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        //try
        //{
        //    string[] sPageAccess = { appUsersRoles.userRole.admin.ToString() };
        //    appUsersRoles oAccess = new appUsersRoles();
        //    bRet = oAccess.grantPageAccess(sPageAccess, (appUsersRoles.userRole)this.oSessnx.getOnlineUser.m_iRoleIdFieldVisit);
        //}
        //catch (Exception ex)
        //{
        //    appMonitor.logAppExceptions(ex);
        //}
        //if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");

        if (!IsPostBack)
        {
            loadData();
        }
    }
    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;
        loadData();

    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        if (ButtonClicked == "editThis")
        {
            LinkButton lbEdit = (LinkButton)grdView.Rows[index].FindControl("editLinkButton");
            idPECHF.Value = lbEdit.Attributes["IDPEC"].ToString();
            //IDPEC, PEC

            if (!string.IsNullOrEmpty(idPECHF.Value))
            {
                IAPPEC pec = IAPPEC.objGetLocationFieldById(int.Parse(idPECHF.Value));
                txtPEC.Text = pec.sPEC;
            }
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(idPECHF.Value))
        {
            bool bRet = IAPPEC.createLocationField(txtPEC.Text);
            if (bRet == true)
            {
                loadData();

                string message = txtPEC.Text + " successfully submitted.";
                ajaxWebExtension.showJscriptAlert(Page, this, message);
            }
        }
        else
        {
            bool bRet = IAPPEC.updateLocationField(int.Parse(idPECHF.Value), txtPEC.Text);
            if (bRet == true)
            {
                loadData();

                string updateMessage = txtPEC.Text + " successfully updated.";
                ajaxWebExtension.showJscriptAlert(Page, this, updateMessage);
            }
        }
        Clear();
    }

    private void loadData()
    {
        grdView.DataSource = IAPPEC.dtGetPec();
        grdView.DataBind();

        //x();
    }

    //void x()
    //{
    //    foreach (GridViewRow grdRow in grdView.Rows)
    //    {
    //        Label lblWindow = (Label)grdRow.FindControl("labelWindow");
    //        LinkButton lbEdit = (LinkButton)grdRow.FindControl("editLinkButton");

    //        int iWindow = ((lbEdit.Attributes["ID_WINDOW"].ToString() != "") ? int.Parse(lbEdit.Attributes["ID_WINDOW"].ToString()) : 0);

    //        if (iWindow == (int)LocationField.PEC.MT)
    //        {
    //            lblWindow.Text = LocationField.PECDesc(LocationField.PEC.MT);
    //        }
    //        else if (iWindow == (int)LocationField.PEC.ST)
    //        {
    //            lblWindow.Text = LocationField.PECDesc(LocationField.PEC.ST);
    //        }
    //        else if (iWindow == (int)LocationField.PEC.VST)
    //        {
    //            lblWindow.Text = LocationField.PECDesc(LocationField.PEC.VST);
    //        }
    //    }
    //}

    private void Clear()
    {
        //drpWindow.ClearSelection();
        txtPEC.Text = "";
        idPECHF.Value = "";
    }
    //protected void drpSuperintendents_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (drpWindow.SelectedValue == "0")
    //    {
    //        loadData();
    //    }
    //    else if (drpWindow.SelectedValue != "-1")
    //    {
    //        grdView.DataSource = LocationField.dtGetLocationFieldByWindowId(int.Parse(drpWindow.SelectedValue));
    //        grdView.DataBind();
    //        x();
    //    }
    //}
    protected void resetButton_Click(object sender, EventArgs e)
    {
        Clear();
    }
}