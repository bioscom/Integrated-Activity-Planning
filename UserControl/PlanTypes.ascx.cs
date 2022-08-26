using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_PlanTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadPlanTypes()
    {
        planTypeGridView.DataSource = RequestFormRequirement.retrievePlanTypes();
        planTypeGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = RequestFormRequirement.InsertPlanType(txtPlanType.Text);
        if (success == true)
        {
            LoadPlanTypes();
            mssgLabel.Text = txtPlanType.Text + " successfully created.";
        }
    }
}
