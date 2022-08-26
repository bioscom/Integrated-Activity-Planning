using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ImpactAnalysisWindow : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadImpactAnalysisWindow()
    {
        impactAnalWinGridView.DataSource = ImpactAnalysis.retrieveImpactAnalysisWin();
        impactAnalWinGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = ImpactAnalysis.InsertImpactAnalysisWin(txtWinType.Text, txtWinTypeDesc.Text, txtType.Text, int.Parse(txtRange.Text));
        if (success == true)
        {
            LoadImpactAnalysisWindow();
            mssgLabel.Text = txtWinType.Text + " successfully created.";
        }
    }
}
