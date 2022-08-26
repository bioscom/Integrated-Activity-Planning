using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Functions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadFunctions()
    {
        functionGridView.DataSource = IAPFunctions.retrieveFunctions();
        functionGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = IAPFunctions.InsertFunction(txtFunction.Text);
        if (success == true)
        {
            LoadFunctions();
            mssgLabel.Text = txtFunction.Text + " successfully created.";
        }
    }

    protected void functionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        functionGridView.PageIndex = e.NewPageIndex;
        LoadFunctions();
    }
}
