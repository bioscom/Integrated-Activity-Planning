using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ImpactAnalysis : System.Web.UI.UserControl
{
    string Mbopd = "";
    string Mmscfpd = "";

    string logistic = "";
    string hsse = "";
    string cp = "";
    string security = "";
    string legal = "";
    string ftolto = "";
    string functional = "";
    string iapType = "";
    string requestNumber = "";


    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //

        if (Session["xWindow_Type"] != null)
        {
            InitializeComponent();
            base.OnInit(e);
        }
    }

    /// <summary>
    ///		Required method for Designer support - do not modify
    ///		the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //this.DataBinding += new System.EventHandler(this.OpexCostActivityDetails_DataBinding);
        initPage();
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
     
    }

    public void initPage()
    {
        if (Session["xWindow_Type"] != null)
        {
            string[] xyParameters = Session["xWindow_Type"].ToString().Split('.');
            iapType = xyParameters[0];
            requestNumber = xyParameters[1];


            LoadMyDDLS();
            DisableAllDDLs();
            EnableRequiredDDLs(iapType);
        }

        RateVSTImpact();
        RateSTImpact();
        RateMTImpact();
    }

    void LoadMyDDLS()
    {
        List<ImpactAnalysisWindowDetail> getImpactAnalysisWindows = ImpactAnalysis.getImpactAnalysisWindow();
        foreach (ImpactAnalysisWindowDetail ImpactWindowDetails in getImpactAnalysisWindows)
        {
            if (ImpactWindowDetails.m_sWINDOWTYPE == "VST")
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    logisticddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    hsseddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    cpddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    securityddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    legalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    ftoltoddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    functionalddl.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
            else if (ImpactWindowDetails.m_sWINDOWTYPE == "ST")
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    logisticddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    hsseddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    cpddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    securityddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    legalddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    ftoltoddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    functionalddl0.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
            else if (ImpactWindowDetails.m_sWINDOWTYPE == "MT")
            {
                for (int i = 0; i <= ImpactWindowDetails.m_iXRANGE; i++)
                {
                    logisticddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    hsseddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    cpddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    securityddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    legalddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    ftoltoddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                    functionalddl1.Items.Add(new ListItem(ImpactWindowDetails.m_sXTYPE + i, ImpactWindowDetails.m_iWINDOWID.ToString()));
                }
            }
        }
    }

    void EnableRequiredDDLs(string iapType)
    {
        if (iapType == "VST")
        {
            logisticddl.Enabled = true;
            hsseddl.Enabled = true;
            cpddl.Enabled = true;
            securityddl.Enabled = true;
            legalddl.Enabled = true;
            ftoltoddl.Enabled = true;
            functionalddl.Enabled = true;
        }
        else if (iapType == "ST")
        {
            logisticddl0.Enabled = true;
            hsseddl0.Enabled = true;
            cpddl0.Enabled = true;
            securityddl0.Enabled = true;
            legalddl0.Enabled = true;
            ftoltoddl0.Enabled = true;
            functionalddl0.Enabled = true;
        }
        else if (iapType == "MT")
        {
            logisticddl1.Enabled = true;
            hsseddl1.Enabled = true;
            cpddl1.Enabled = true;
            securityddl1.Enabled = true;
            legalddl1.Enabled = true;
            ftoltoddl1.Enabled = true;
            functionalddl1.Enabled = true;
        }
    }

    void DisableAllDDLs()
    {
        logisticddl.Enabled = false;
        hsseddl.Enabled = false;
        cpddl.Enabled = false;
        securityddl.Enabled = false;
        legalddl.Enabled = false;
        ftoltoddl.Enabled = false;
        functionalddl.Enabled = false;
        logisticddl0.Enabled = false;
        hsseddl0.Enabled = false;
        cpddl0.Enabled = false;
        securityddl0.Enabled = false;
        legalddl0.Enabled = false;
        ftoltoddl0.Enabled = false;
        functionalddl0.Enabled = false;
        logisticddl1.Enabled = false;
        hsseddl1.Enabled = false;
        cpddl1.Enabled = false;
        securityddl1.Enabled = false;
        legalddl1.Enabled = false;
        ftoltoddl1.Enabled = false;
        functionalddl1.Enabled = false;
    }

    void RateVSTImpact()
    {
        logistic = logisticddl.SelectedItem.Text;
        hsse = hsseddl.SelectedItem.Text;
        cp = cpddl.SelectedItem.Text;
        security = securityddl.SelectedItem.Text;
        legal = legalddl.SelectedItem.Text;
        ftolto = ftoltoddl.SelectedItem.Text;
        functional = functionalddl.SelectedItem.Text;

        logisticddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        hsseddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        cpddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        securityddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        legalddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        ftoltoddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        functionalddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
    }

    void RateSTImpact()
    {
        logistic = logisticddl0.SelectedItem.Text;
        hsse = hsseddl0.SelectedItem.Text;
        cp = cpddl0.SelectedItem.Text;
        security = securityddl0.SelectedItem.Text;
        legal = legalddl0.SelectedItem.Text;
        ftolto = ftoltoddl0.SelectedItem.Text;
        functional = functionalddl0.SelectedItem.Text;

        logisticddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        hsseddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        cpddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        securityddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        legalddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        ftoltoddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        functionalddl0.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
    }

    void RateMTImpact()
    {
        logistic = logisticddl1.SelectedItem.Text;
        hsse = hsseddl1.SelectedItem.Text;
        cp = cpddl1.SelectedItem.Text;
        security = securityddl1.SelectedItem.Text;
        legal = legalddl1.SelectedItem.Text;
        ftolto = ftoltoddl1.SelectedItem.Text;
        functional = functionalddl1.SelectedItem.Text;

        logisticddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        hsseddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        cpddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        securityddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        legalddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        ftoltoddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
        functionalddl1.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);
    }


    void SelectedIndexChanged(Object sender, EventArgs e)
    {
        string[] words = { logistic, hsse, cp, security, legal, ftolto, functional };

        IEnumerable<string> query = from word in words
                                    orderby word.Length
                                    select word;
        foreach (string str in query)
        {
            Rating.Text = str;
        }


        //if (functionalddl.SelectedIndex > -1)
        //{
        //    //find the Highest rating selected by calling your bubbleSort class

        //    BubbleSort MySort = new BubbleSort();
        //    MySort.getValues();

        //    //Assign the Highest rating in a variable
        //    string HighestImpact = "";


        //    Rating.Text = HighestImpact;
        //}
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (iapType == "VST")
        {
            Mbopd = txtMbopd.Text;
            Mmscfpd = txtMmscfpd.Text;

            logistic = logisticddl.SelectedItem.Text;
            hsse = hsseddl.SelectedItem.Text;
            cp = cpddl.SelectedItem.Text;
            security = securityddl.SelectedItem.Text;
            legal = legalddl.SelectedItem.Text;
            ftolto = ftoltoddl.SelectedItem.Text;
            functional = functionalddl.SelectedItem.Text;

        }
        else if (iapType == "ST")
        {
            Mbopd = txtMbopd0.Text;
            Mmscfpd = txtMmscfpd0.Text;

            logistic = logisticddl0.SelectedItem.Text;
            hsse = hsseddl0.SelectedItem.Text;
            cp = cpddl0.SelectedItem.Text;
            security = securityddl0.SelectedItem.Text;
            legal = legalddl0.SelectedItem.Text;
            ftolto = ftoltoddl0.SelectedItem.Text;
            functional = functionalddl0.SelectedItem.Text;
        }
        else if (iapType == "MT")
        {
            Mbopd = txtMbopd1.Text;
            Mmscfpd = txtMmscfpd1.Text;

            logistic = logisticddl1.SelectedItem.Text;
            hsse = hsseddl1.SelectedItem.Text;
            cp = cpddl1.SelectedItem.Text;
            security = securityddl1.SelectedItem.Text;
            legal = legalddl1.SelectedItem.Text;
            ftolto = ftoltoddl1.SelectedItem.Text;
            functional = functionalddl1.SelectedItem.Text;
        }

        ImpactAnalysis.InsertImpactAnalysis(requestNumber, txtActivity.Text, txtChangeReason.Text,
            txtOverallRating.Text, Mbopd, Mmscfpd, txtOtherRemarks.Text, logistic, cp, security, hsse, ftolto, legal, functional);
    }


    public event EventHandler BubbleClick;

    protected void OnBubbleClick(EventArgs e)
    {
        if (BubbleClick != null)
        {
            BubbleClick(this, e);
        }
    }
}