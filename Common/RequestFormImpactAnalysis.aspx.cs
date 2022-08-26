using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_RequestFormImpactAnalysis : System.Web.UI.Page
{
    string Mbopd = "";
    string Mmscfpd = "";

    string production = "";
    string logistic = "";
    string hsse = "";
    string cp = "";
    string security = "";
    string legal = "";
    string ftolto = "";
    string functional = "";

    protected void page_init(object sender, EventArgs e)
    {
        validateNumeric();
        initPanel2();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    private void validateNumeric()
    {
        txtMbopd.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtMmscfpd.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtCostVar.Attributes.Add("onkeypress", "return isNumberKey(event)");

        txtMbopd0.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtMmscfpd0.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtCostVar0.Attributes.Add("onkeypress", "return isNumberKey(event)");

        txtMbopd1.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtMmscfpd1.Attributes.Add("onkeypress", "return isNumberKey(event)");
        txtCostVar1.Attributes.Add("onkeypress", "return isNumberKey(event)");
    }

    private void initPanel2()
    {
        try
        {
            Users IAPPlanner = UsersActions.objGetUserByUserId(int.Parse(Request.QueryString["IAPPlanner"].ToString()));
            LoadMyDDLS(); //Load values into the dropdownlist
            EnableRequiredDDLs(IAPPlanner.m_iIAPPlannerType); //Enable the required dropdownlist based on the selected IAP Planner
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    void LoadMyDDLS()
    {
        List<ImpactAnalysisWindowDetail> getImpactAnalysisWindows = ImpactAnalysis.getImpactAnalysisWindow();
        foreach (ImpactAnalysisWindowDetail ImpactWindowDetails in getImpactAnalysisWindows)
        {
            if (ImpactWindowDetails.m_sWINDOWTYPE == appUsersRoles.PlannerTypeDesc(PlannerType.vst))
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    vstProductionddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstLogisticddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstHsseddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstCpddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstSecurityddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstlegalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstftoltoddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    vstfunctionalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
            else if (ImpactWindowDetails.m_sWINDOWTYPE == appUsersRoles.PlannerTypeDesc(PlannerType.st))
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    stProductionddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stLogisticddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stHsseddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stCpddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stSecurityddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stlegalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stftoltoddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    stfunctionalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
            else if (ImpactWindowDetails.m_sWINDOWTYPE == appUsersRoles.PlannerTypeDesc(PlannerType.mt))
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    mtProductionddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtLogisticddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtHsseddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtCpddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtSecurityddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtlegalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtftoltoddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    mtfunctionalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
        }
    }

    private void EnableRequiredDDLs(int iapType)
    {
        try
        {
            if (iapType == (int)PlannerType.vst)
            {
                stProductionddl.Enabled = false;
                stLogisticddl.Enabled = false;
                stHsseddl.Enabled = false;
                stCpddl.Enabled = false;
                stSecurityddl.Enabled = false;
                stlegalddl.Enabled = false;
                stftoltoddl.Enabled = false;
                stfunctionalddl.Enabled = false;

                mtProductionddl.Enabled = false;
                mtLogisticddl.Enabled = false;
                mtHsseddl.Enabled = false;
                mtCpddl.Enabled = false;
                mtSecurityddl.Enabled = false;
                mtlegalddl.Enabled = false;
                mtftoltoddl.Enabled = false;
                mtfunctionalddl.Enabled = false;
                
            }
            else if (iapType == (int)PlannerType.st)
            {
                vstProductionddl.Enabled = false;
                vstLogisticddl.Enabled = false;
                vstHsseddl.Enabled = false;
                vstCpddl.Enabled = false;
                vstSecurityddl.Enabled = false;
                vstlegalddl.Enabled = false;
                vstftoltoddl.Enabled = false;
                vstfunctionalddl.Enabled = false;

                mtProductionddl.Enabled = false;
                mtLogisticddl.Enabled = false;
                mtHsseddl.Enabled = false;
                mtCpddl.Enabled = false;
                mtSecurityddl.Enabled = false;
                mtlegalddl.Enabled = false;
                mtftoltoddl.Enabled = false;
                mtfunctionalddl.Enabled = false;
            }
            else if (iapType == (int)PlannerType.mt)
            {
                vstProductionddl.Enabled = false;
                vstLogisticddl.Enabled = false;
                vstHsseddl.Enabled = false;
                vstCpddl.Enabled = false;
                vstSecurityddl.Enabled = false;
                vstlegalddl.Enabled = false;
                vstftoltoddl.Enabled = false;
                vstfunctionalddl.Enabled = false;

                stProductionddl.Enabled = false;
                stLogisticddl.Enabled = false;
                stHsseddl.Enabled = false;
                stCpddl.Enabled = false;
                stSecurityddl.Enabled = false;
                stlegalddl.Enabled = false;
                stftoltoddl.Enabled = false;
                stfunctionalddl.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private static KeyValuePair<string, string> FindMax(IEnumerable<KeyValuePair<string, string>> lsd)
    {
        var comparer = StringComparer.OrdinalIgnoreCase;
        var best = default(KeyValuePair<string, string>);

        bool isFirst = true;
        foreach (KeyValuePair<string, string> kvp in lsd)
        {
            if (isFirst || comparer.Compare(kvp.Value, best.Value) > 0)
            {
                isFirst = false;
                best = kvp;
            }
        }
        return best;
    }

    private string Rate()
    {
        string Result = "";
        string[] words = new string[] 
        { 
            production, 
            logistic, 
            hsse, 
            cp, 
            security, 
            legal, 
            ftolto, 
            functional 
        };

        Array.Sort(words);
        foreach (string s in words)
        {
            Result = s;
        }

        return Result;
    }

    protected void insertButton_Click(object sender, EventArgs e)
    {
        try
        {
            Users IAPPlanner = UsersActions.objGetUserByUserId(int.Parse(Request.QueryString["IAPPlanner"].ToString()));
            if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.vst)
            {
                Mbopd = txtMbopd.Text;
                Mmscfpd = txtMmscfpd.Text;

                production = vstProductionddl.SelectedItem.Text;
                logistic = vstLogisticddl.SelectedItem.Text;
                hsse = vstHsseddl.SelectedItem.Text;
                cp = vstCpddl.SelectedItem.Text;
                security = vstSecurityddl.SelectedItem.Text;
                legal = vstlegalddl.SelectedItem.Text;
                ftolto = vstftoltoddl.SelectedItem.Text;
                functional = vstfunctionalddl.SelectedItem.Text;
            }
            else if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.st)
            {
                Mbopd = txtMbopd0.Text;
                Mmscfpd = txtMmscfpd0.Text;

                production = stProductionddl.SelectedItem.Text;
                logistic = stLogisticddl.SelectedItem.Text;
                hsse = stHsseddl.SelectedItem.Text;
                cp = stCpddl.SelectedItem.Text;
                security = stSecurityddl.SelectedItem.Text;
                legal = stlegalddl.SelectedItem.Text;
                ftolto = stftoltoddl.SelectedItem.Text;
                functional = stfunctionalddl.SelectedItem.Text;
            }
            else if (IAPPlanner.m_iIAPPlannerType == (int)PlannerType.mt)
            {
                Mbopd = txtMbopd1.Text;
                Mmscfpd = txtMmscfpd1.Text;

                production = mtProductionddl.SelectedItem.Text;
                logistic = mtLogisticddl.SelectedItem.Text;
                hsse = mtHsseddl.SelectedItem.Text;
                cp = mtCpddl.SelectedItem.Text;
                security = mtSecurityddl.SelectedItem.Text;
                legal = mtlegalddl.SelectedItem.Text;
                ftolto = mtftoltoddl.SelectedItem.Text;
                functional = mtfunctionalddl.SelectedItem.Text;
            }

            //Rate();

            bool success = ImpactAnalysis.InsertImpactAnalysis(Request.QueryString["RequestNumber"].ToString(), txtActivity.Text, txtChangeReason.Text, txtOverallRating.Text, Mbopd, Mmscfpd, txtOtherRemarks.Text, logistic, cp, security, hsse, ftolto, legal, functional);
            if (success == true)
            {
                Response.Redirect("~/Common/RequestForm.aspx?RequestNumber=" + Request.QueryString["RequestNumber"].ToString() + "&IAPPlanner=" + Request.QueryString["IAPPlanner"].ToString() + "&Rate=" + Rate());
            }
            else
            {
                string message = "Not successful, please try again!!!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", String.Format("alert('{0}');", message), true);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    protected void pnlCloseButton_Click(object sender, EventArgs e)
    {

    }
}