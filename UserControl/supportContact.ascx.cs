using System;
using System.Collections.Generic;

public partial class UserControl_supportContact : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Page_Init()
    {
        supportBlst.Items.Add(AppConfiguration.adminName + " [" + AppConfiguration.adminExt + "]");

        List<Users> supportRoles = UsersActions.lstGetContacts();
        foreach (Users supportRole in supportRoles)
        {
            supportBlst.Items.Add(supportRole.m_sFullName + " [" + supportRole.m_sPhoneExtention + "]");
        }
    }
}
