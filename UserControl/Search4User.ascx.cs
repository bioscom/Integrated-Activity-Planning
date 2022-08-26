using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_userFinder_Search4User : aspnetUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void InitPage()
    {
        if (!IsPostBack)
        {
            drpUserx.Visible = false;
            imbEdit.Visible = false;
        }
    }

    public CompleteStaffDetailsInfo selectedUserDetails
    {
        get
        {
            CompleteStaffDetailsInfo thisUser = UsersActions.GetStaffFromCompleteStaffDetails(drpUserx.SelectedValue);
            return thisUser;
        }
    }

    //public appUsers selectedUserDetailsFromLocalTable
    //{
    //    get
    //    {
    //        appUsers oAppUser = appUsersHelper.objGetAppUserByUserName(drpUserx.SelectedValue);
    //        return oAppUser;
    //    }
    //}

    public void CreateNewUser(DropDownList ddlIAPPlannerType, int iFunction, int iRoleId, string sPhoneNo, Users CurrentUser)
    {
        bool success = false;
        SendMail o = new SendMail(oSessnx.getOnlineUser.structUserIdx);

        string IAPPlannerType = "";
        if (ddlIAPPlannerType.SelectedValue == RequestStatus.m_iDropDownListFirstIndexValue.ToString())
            IAPPlannerType = "";
        else
            IAPPlannerType = ddlIAPPlannerType.SelectedValue;

        CompleteStaffDetailsInfo newUser = UsersActions.lstGetStaffInfoByStaffNumber(drpUserx.SelectedValue.ToUpper());

        success = UsersActions.CreateUser(newUser.m_sUserName.ToUpper(), newUser.m_sFullName, ddlIAPPlannerType.SelectedValue, newUser.m_sUserMail, iFunction, iRoleId, (int)appUsersRoles.userStatus.activeUser, sPhoneNo);

        if (success == true)
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, newUser.m_sFullName + " successfully created for the role " + appUsersRoles.userRoleDesc((appUsersRoles.userRole)iRoleId));
            //MessageBox.Show(newUser.m_sFullName + " successfully created for the role " + ddlUserRoleList.SelectedItem.Text);

            //success = MyMail.mailIAPUser(emailAdress, CurrentUser.m_sUserMail, Utilities.AppURL());
            success = o.mailIAPUser(newUser.structUserIdx, Utilities.AppURL(), iRoleId);
            if (success == true)
            {
                ajaxWebExtension.showJscriptAlertCx(Page, this, "Notification mail has been sent to the user");
                //AppStatusMessages.UserRegistrationNotification();
            }

            //Clear();
        }
        else
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, "Sorry, " + newUser.m_sFullName + " user already exists.");
        }
    }

    protected void imbEdit_Click(object sender, ImageClickEventArgs e)
    {
        txtUserx.Text = "";
        txtUserx.Visible = true;
        imbFind.Visible = true;
        drpUserx.Visible = false;
        imbEdit.Visible = false;

        if (drpUserx.Items.Count > 1)
        {
            resetUserInfo();
        }        
    }
    
    protected void imbFind_Click(object sender, ImageClickEventArgs e)
    {
        drpUserx.Visible = true;
        imbEdit.Visible = true;

        txtUserx.Visible = false;
        imbFind.Visible = false;

        drpUserx.Items.Clear();
        drpUserx.Items.Add(new ListItem("Please Select...", "-1"));
        List<CompleteStaffDetailsInfo> staffs = UsersActions.lstGetStaffInfoBySearch(txtUserx.Text);
        foreach (CompleteStaffDetailsInfo staff in staffs)
        {
            drpUserx.Items.Add(new ListItem(staff.m_sFullName, staff.m_sUserName));
        }

        if (staffs.Count == 0)
        {
            ajaxWebExtension.showJscriptAlertCx(Page, this, "[" + txtUserx.Text + "] no match found!!!");
            resetUserInfo();
        }
    }

    public void resetUserInfo()
    {
        txtUserx.Visible = true;
        txtUserx.Text = "";
        imbFind.Visible = true;

        drpUserx.Visible = false;
        drpUserx.Items.Clear();
        imbEdit.Visible = false;

        ListItem oDefItem = new ListItem();
        oDefItem.Value = "0";
        if (drpUserx.ToolTip == "")
        {
            oDefItem.Text = "[Select GID User Name]";
        }
        else
        {
            oDefItem.Text = "[" + drpUserx.ToolTip + "]";
        }
        drpUserx.Items.Add(oDefItem);
    }

    public void initUserInfo(string sToolTip, int xWidth)
    {
        txtUserx.Width = (Unit) (xWidth - (imbEdit.Width.Value * 1.6));
        drpUserx.Width = txtUserx.Width;
        drpUserx.ToolTip = sToolTip;

        resetUserInfo();
    }

    public void editUserInfo(string sToolTip, int xWidth)
    {
        txtUserx.Width = (Unit)(xWidth - (imbEdit.Width.Value * 1.6));
        drpUserx.Width = txtUserx.Width;
        drpUserx.ToolTip = sToolTip;

        UnResetUserInfo();
    }

    public DropDownList thisDropDownList
    {
        get
        {
            return drpUserx;
        }
    }

    public void editMode()
    {
        UnResetUserInfo();
    }

    public void UnResetUserInfo()
    {
        txtUserx.Visible = false;
        txtUserx.Text = "";
        imbFind.Visible = false;

        drpUserx.Visible = true;
        drpUserx.Items.Clear();
        imbEdit.Visible = true;

        ListItem oDefItem = new ListItem();
        oDefItem.Value = "0";
        if (drpUserx.ToolTip == "")
        {
            oDefItem.Text = "[Select GID User Name]";
        }
        else
        {
            oDefItem.Text = "[" + drpUserx.ToolTip + "]";
        }
        drpUserx.Items.Add(oDefItem);
    }

    public bool userIsValid
    {
        get
        {
            if (drpUserx.SelectedValue == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public string _SelectedValue
    {
        get
        {
            return drpUserx.SelectedValue;
        }
    }

    public int setWidth
    {
        set
        {
            txtUserx.Width = (Unit) (value - (imbEdit.Width.Value * 1.6));
            drpUserx.Width = txtUserx.Width;
        }
    }
}