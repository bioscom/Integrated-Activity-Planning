using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Areas : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadAreas()
    {
        AreaGridView.DataSource = RequestFormRequirement.retrieveAreas();
        AreaGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = RequestFormRequirement.InsertArea(txtArea.Text);
        if (success == true)
        {
            LoadAreas();
            mssgLabel.Text = txtArea.Text + " successfully created.";
        }
    }
    protected void AreaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AreaGridView.PageIndex = e.NewPageIndex;
        LoadAreas();
    }
}
