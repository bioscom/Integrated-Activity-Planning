using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ChangeTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadChangeTypes()
    {
        ChangeTypeGridView.DataSource = RequestFormRequirement.retrieveChangeTypes();
        ChangeTypeGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = RequestFormRequirement.InsertChangeType(txtChangeType.Text);
        if (success == true)
        {
            LoadChangeTypes();
            mssgLabel.Text = txtChangeType.Text + " successfully created.";
        }
    }
}
