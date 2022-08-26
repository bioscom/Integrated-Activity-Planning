using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_OU : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadOUs()
    {
       OUGridView.DataSource = RequestFormRequirement.retrieveOUS();
       OUGridView.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = RequestFormRequirement.InsertOU(txtOU.Text);
        if (success == true)
        {
            LoadOUs();
            mssgLabel.Text = txtOU.Text + " successfully created.";
        }
    }
}
