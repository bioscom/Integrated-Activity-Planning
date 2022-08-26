using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_statusSelectorControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //public string SelectedStatus
    //{
    //    get
    //    {
    //        return statusDrp.SelectedValue;
    //    }
    //}

    //public void _SelectedValue(int iSelectedValue)
    //{
    //    statusDrp.SelectedValue = iSelectedValue.ToString();
    //}

    //public DropDownList ThisDropDownList
    //{
    //    get
    //    {
    //        return statusDrp;
    //    }
    //}


    ///


    public string SelectedStatus2
    {
        get
        {
            return statusRdb.SelectedValue;
        }
    }

    public void rdbSelectedValue(int iSelectedValue)
    {
        statusRdb.SelectedValue = iSelectedValue.ToString();
    }

    public RadioButtonList ThisRadioButtonList
    {
        get
        {
            return statusRdb;
        }
    }
}
