using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Setups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        OU1.LoadOUs();
        Areas1.LoadAreas();
        ChangeTypes1.LoadChangeTypes();
        Locations1.LoadLocations();
        PlanTypes1.LoadPlanTypes();
        Functions1.LoadFunctions();
        ImpactAnalysisWindow1.LoadImpactAnalysisWindow();
        if (!IsPostBack)
        {
            SupportContacts1.BindUserData();
        }
    }
}
