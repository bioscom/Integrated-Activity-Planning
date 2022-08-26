using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControl_oImpactedPartiesComments : aspnetUserControl
{
    string sfield = "REQUEST_NUMBER";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void GetImpactecdPartiesComments(long lRequestId)
    {
        grdView.DataSource = ApprovalWorkFlow.GetImpactecdPartiesComment(lRequestId);
        requestIdHF.Value = lRequestId.ToString();
    }

    protected void grdView_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            (sender as RadGrid).DataSource = ApprovalWorkFlow.GetImpactecdPartiesComment(long.Parse(requestIdHF.Value));
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
        //    GridDataItem item = e.Item as GridDataItem;
        //    CheckBox checkBoxColumn = item.FindControl("CheckBoxColumn") as CheckBox;
        //    checkBoxColumn.Attributes.Add("onclick", "uncheckOther(this);");
        //}
    }

    protected void btnReminder_Click(object sender, EventArgs e)
    {
        aRequest oRequest = RequestHelper.objGetRequestByRequestId(long.Parse(requestIdHF.Value), sfield);

        SendToImpactedParties(oRequest);
    }

    private void SendToImpactedParties(aRequest oRequest)
    {
        try
        {
            List<structUserMailIdx> eTos = new List<structUserMailIdx>();
            foreach (GridDataItem item in grdView.MasterTableView.Items)
            {
                CheckBox chkBox = item.FindControl("chkBox") as CheckBox;
                if (chkBox != null && chkBox.Checked)
                {
                    int iUserId = int.Parse(item.GetDataKeyValue("IDUSER").ToString());
                    eTos.Add(UsersActions.objGetUserByUserId(iUserId).structUserIdx); // Collect their email address, to forward them notification emails
                }
            }

            List<structUserMailIdx> cc = new List<structUserMailIdx>();
            cc.Add(UsersActions.objGetUserByUserId(oRequest.m_iUserId).structUserIdx);

            SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);
            o.NotifyImpactedPaties(eTos, oRequest.m_sORIGINATOR, oRequest.m_sRequestNumber, oRequest.m_sProjectActivity, Utilities.AppURL(), cc);

            ajaxWebExtension.showJscriptAlert(Page, this, "Notification successfully sent.");
            //ajaxWebExtension.showJscriptAlert(Page, this, "Change Request sent to all selected Impacted/Supporting Parties.");
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}