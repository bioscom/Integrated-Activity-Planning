using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_personnelInformationList : aspnetUserControl
{
    public void Init_Page()
    {
        LoadControls();
        infoPanel.Visible = false;
    }

    private void LoadControls()
    {
        drpContactPerson.Items.Add(new ListItem("Select Contact Person", "-1"));
        List<FieldContactPerson> contacts = FieldContactPerson.lstGetContactPerson();
        foreach (FieldContactPerson contact in contacts)
        {
            drpContactPerson.Items.Add(new ListItem(contact.m_sCONTACT_PERSON, contact.m_iID_CONTACT.ToString()));
        }

        drpLastVisit.Items.Add(new ListItem("When Last Visit", "-1"));
        List<FieldLastVisit> visits = FieldLastVisit.lstGetLastVisit();
        foreach (FieldLastVisit visit in visits)
        {
            drpLastVisit.Items.Add(new ListItem(visit.m_sLAST_VISIT, visit.m_iID_LAST_VISIT.ToString()));
        }

        drpVisaType.Items.Add(new ListItem("Select Visa Type", "-1"));
        List<FieldVisaType> visas = FieldVisaType.lstGetVisaType();
        foreach (FieldVisaType visa in visas)
        {
            drpVisaType.Items.Add(new ListItem(visa.m_sVISA_TYPE, visa.m_iID_VISA.ToString()));
        }

        //drpBaggage.Items.Add(new ListItem("Select Baggage", "-1"));
        //List<FieldBaggage> baggages = FieldBaggage.lstGetBaggage();
        //foreach (FieldBaggage baggage in baggages)
        //{
        //    drpBaggage.Items.Add(new ListItem(baggage.m_sBAGGAGE, baggage.m_iID_BAG.ToString()));
        //}

        drpGender.Items.Add(new ListItem("Select Gender", "-1"));
        drpGender.Items.Add(new ListItem(commonEnums.genderDesc(commonEnums.gender.male), ((int)commonEnums.gender.male).ToString()));
        drpGender.Items.Add(new ListItem(commonEnums.genderDesc(commonEnums.gender.female), ((int)commonEnums.gender.female).ToString()));

        drpBOSIET.Items.Add(new ListItem("", "-1"));
        drpBOSIET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
        drpBOSIET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));

        //drpHUET.Items.Add(new ListItem("", "-1"));
        //drpHUET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
        //drpHUET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));

        drpMedical.Items.Add(new ListItem("", "-1"));
        drpMedical.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
        drpMedical.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));

        //drpPPE.Items.Add(new ListItem("", "-1"));
        //drpPPE.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
        //drpPPE.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initiateActivityId(long lActivityId)
    {
        RequestIDHF.Value = lActivityId.ToString();
    }

    public void Retrieve_Data(long lRequestId)
    {
        RequestIDHF.Value = lRequestId.ToString();
        grdView.DataSource = PersonnelInfo.dtGetPersonnelInfoByRequestId(lRequestId);
        grdView.DataBind();

        foreach (GridViewRow grdRows in grdView.Rows)
        {
            Label gender = (Label)grdRows.FindControl("lblGender");
            Label Bosiet = (Label)grdRows.FindControl("lblBosiet");
            //Label Huet = (Label)grdRows.FindControl("lblHuet");
            Label Medical = (Label)grdRows.FindControl("lblMedical");
            //Label PPE = (Label)grdRows.FindControl("lblPPE");

            if (gender.Text == ((int)commonEnums.gender.male).ToString())
            {
                gender.Text = commonEnums.genderDesc(commonEnums.gender.male);
            }
            else
            {
                gender.Text = commonEnums.genderDesc(commonEnums.gender.female);
            }

            if (Bosiet.Text == ((int)commonEnums.YesNo.Yes).ToString())
            {
                Bosiet.Text = commonEnums.yesNoDesc(commonEnums.YesNo.Yes);
            }
            else
            {
                Bosiet.Text = commonEnums.yesNoDesc(commonEnums.YesNo.No);
            }

            //if (Huet.Text == ((int)commonEnums.YesNo.Yes).ToString())
            //{
            //    Huet.Text = commonEnums.yesNoDesc(commonEnums.YesNo.Yes);
            //}
            //else
            //{
            //    Huet.Text = commonEnums.yesNoDesc(commonEnums.YesNo.No);
            //}

            if (Medical.Text == ((int)commonEnums.YesNo.Yes).ToString())
            {
                Medical.Text = commonEnums.yesNoDesc(commonEnums.YesNo.Yes);
            }
            else
            {
                Medical.Text = commonEnums.yesNoDesc(commonEnums.YesNo.No);
            }

            //if (PPE.Text == ((int)commonEnums.YesNo.Yes).ToString())
            //{
            //    PPE.Text = commonEnums.yesNoDesc(commonEnums.YesNo.Yes);
            //}
            //else
            //{
            //    PPE.Text = commonEnums.yesNoDesc(commonEnums.YesNo.No);
            //}
        }

        infoPanel.Visible = false;
    }

    public HiddenField RequestId
    {
        get
        {
            return RequestIDHF;
        }
    }

    public GridView GrdView
    {
        get
        {
            return grdView;
        }
    }

    public LinkButton AddLinkButton
    {
        get
        {
            return addLinkButton;
        }
    } 

    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    
    protected void addButton_Click(object sender, EventArgs e)
    {
        try
        {
            long lRequestId = long.Parse(RequestIDHF.Value);

            bool bRet = PersonnelInfo.createPersonnelInfo(lRequestId, txtEmployeeName.Text, int.Parse(drpGender.SelectedValue),
                int.Parse(drpContactPerson.SelectedValue), int.Parse(drpLastVisit.SelectedValue), int.Parse(drpBOSIET.SelectedValue),
                int.Parse(drpMedical.SelectedValue), int.Parse(drpVisaType.SelectedValue), txtCompany.Text);

            //bool bRet = PersonnelInfo.createPersonnelInfo(lRequestId, txtEmployeeName.Text, int.Parse(drpGender.SelectedValue),
            //    int.Parse(drpContactPerson.SelectedValue), int.Parse(drpLastVisit.SelectedValue), int.Parse(drpBOSIET.SelectedValue),
            //    int.Parse(drpHUET.SelectedValue), int.Parse(drpMedical.SelectedValue), int.Parse(drpVisaType.SelectedValue),
            //    int.Parse(drpPPE.SelectedValue), int.Parse(drpBaggage.SelectedValue), txtCompany.Text);
            if (bRet == true)
            {
                Retrieve_Data(lRequestId);

                //addButton.Enabled = false;
                string Message = "Personnel Information successfully saved.";
                ajaxWebExtension.showJscriptAlert(Page, this, Message);
            }

            infoPanel.Visible = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public void Retrieve_Data_EditGrid(long lRequestId)
    {
        //List<PersonnelInfo> lstPersonnel = PersonnelInfo.lstGetPersonnelInfoByActivityId(iActivityInfoId);
        //grdView.DataSource = lstPersonnel;
        grdView.DataSource = PersonnelInfo.dtGetPersonnelInfoByRequestId(lRequestId);
        grdView.DataBind();
    }

    protected void addLinkButton_Click(object sender, EventArgs e)
    {
        infoPanel.Visible = true;
    }

    //Expose control to the outside world
    public Button MySaveButton
    {
        get
        {
            return addButton;
        }
    }
    protected void cancleButton_Click(object sender, EventArgs e)
    {
        infoPanel.Visible = false;
    }

    protected void grdView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        long lActivityId = long.Parse(RequestIDHF.Value);
        grdView.EditIndex = -1;
        Retrieve_Data(lActivityId);
    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PersonnelInfo personnelInfo = PersonnelInfo.objGetPersonnelInfoById(int.Parse(grdView.DataKeys[e.Row.RowIndex].Values[0].ToString()));

            DropDownList ddlGender = (DropDownList)e.Row.FindControl("drpGender");
            if (ddlGender != null)
            {
                ddlGender.Items.Add(new ListItem("Select Gender", "-1"));
                ddlGender.Items.Add(new ListItem(commonEnums.genderDesc(commonEnums.gender.male), ((int)commonEnums.gender.male).ToString()));
                ddlGender.Items.Add(new ListItem(commonEnums.genderDesc(commonEnums.gender.female), ((int)commonEnums.gender.female).ToString()));
                ddlGender.SelectedValue = personnelInfo.m_sGENDER;
            }

            DropDownList ddlContactPerson = (DropDownList)e.Row.FindControl("drpContactPerson");
            if (ddlContactPerson != null)
            {
                ddlContactPerson.Items.Add(new ListItem("Select Contact Person", "-1"));
                List<FieldContactPerson> contacts = FieldContactPerson.lstGetContactPerson();
                foreach (FieldContactPerson contact in contacts)
                {
                    ddlContactPerson.Items.Add(new ListItem(contact.m_sCONTACT_PERSON, contact.m_iID_CONTACT.ToString()));
                }
                ddlContactPerson.SelectedValue = personnelInfo.m_iID_CONTACT.ToString();
            }

            DropDownList ddlLastVisit = (DropDownList)e.Row.FindControl("drpLastVisit");
            if (ddlLastVisit != null)
            {
                ddlLastVisit.Items.Add(new ListItem("When Last Visit", "-1"));
                List<FieldLastVisit> visits = FieldLastVisit.lstGetLastVisit();
                foreach (FieldLastVisit visit in visits)
                {
                    ddlLastVisit.Items.Add(new ListItem(visit.m_sLAST_VISIT, visit.m_iID_LAST_VISIT.ToString()));
                }
                ddlLastVisit.SelectedValue = personnelInfo.m_iID_LAST_VISIT.ToString();
            }

            DropDownList ddlBOSIET = (DropDownList)e.Row.FindControl("drpBOSIET");
            if (ddlBOSIET != null)
            {
                ddlBOSIET.Items.Add(new ListItem("", "-1"));
                ddlBOSIET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
                ddlBOSIET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));
                ddlBOSIET.SelectedValue = personnelInfo.m_sBOSIET_VALID.ToString();
            }

            //DropDownList ddlHUET = (DropDownList)e.Row.FindControl("drpHUET");
            //if (ddlHUET != null)
            //{
            //    ddlHUET.Items.Add(new ListItem("", "-1"));
            //    ddlHUET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
            //    ddlHUET.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));
            //    ddlHUET.SelectedValue = personnelInfo.m_sHUET_VALID.ToString();
            //}

            DropDownList ddlMedical = (DropDownList)e.Row.FindControl("drpMedical");
            if (ddlMedical != null)
            {
                ddlMedical.Items.Add(new ListItem("", "-1"));
                ddlMedical.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
                ddlMedical.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));
                ddlMedical.SelectedValue = personnelInfo.m_sMEDICAL.ToString();
            }

            DropDownList ddlVisaType = (DropDownList)e.Row.FindControl("drpVisaType");
            if (ddlVisaType != null)
            {
                ddlVisaType.Items.Add(new ListItem("Select Visa Type", "-1"));
                List<FieldVisaType> visas = FieldVisaType.lstGetVisaType();
                foreach (FieldVisaType visa in visas)
                {
                    ddlVisaType.Items.Add(new ListItem(visa.m_sVISA_TYPE, visa.m_iID_VISA.ToString()));
                }
                ddlVisaType.SelectedValue = personnelInfo.m_iID_VISA.ToString();
            }

            //DropDownList ddlPPE = (DropDownList)e.Row.FindControl("drpPPE");
            //if (ddlPPE != null)
            //{
            //    ddlPPE.Items.Add(new ListItem("", "-1"));
            //    ddlPPE.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.Yes), ((int)commonEnums.YesNo.Yes).ToString()));
            //    ddlPPE.Items.Add(new ListItem(commonEnums.yesNoDesc(commonEnums.YesNo.No), ((int)commonEnums.YesNo.No).ToString()));
            //    ddlPPE.SelectedValue = personnelInfo.m_sPPE.ToString();
            //}

            //DropDownList ddlBaggage = (DropDownList)e.Row.FindControl("drpBaggage");
            //if (ddlBaggage != null)
            //{
            //    ddlBaggage.Items.Add(new ListItem("Select Baggage", "-1"));
            //    List<FieldBaggage> baggages = FieldBaggage.lstGetBaggage();
            //    foreach (FieldBaggage baggage in baggages)
            //    {
            //        ddlBaggage.Items.Add(new ListItem(baggage.m_sBAGGAGE, baggage.m_iID_BAG.ToString()));
            //    }
            //    ddlBaggage.SelectedValue = personnelInfo.m_iID_BAG.ToString();
            //}
        }
    }

    protected void grdView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        long lActivityId = long.Parse(RequestIDHF.Value);
        grdView.EditIndex = e.NewEditIndex;
        Retrieve_Data_EditGrid(lActivityId);

        //PersonnelInfo personnelInfo = PersonnelInfo.objGetPersonnelInfoById(int.Parse(grdView.DataKeys[e.NewEditIndex].Values[0].ToString()));
    }
    protected void grdView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            long lRequestId = long.Parse(RequestIDHF.Value);
            PersonnelInfo ePersonnelInfo = new PersonnelInfo();

            ePersonnelInfo.m_iIDREQUEST = lRequestId;
            ePersonnelInfo.m_iID_PERSONNEL = int.Parse(grdView.DataKeys[e.RowIndex].Values[0].ToString());
            //ePersonnelInfo.m_iID_BAG = Convert.ToInt32(((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpBaggage")).SelectedValue);
            ePersonnelInfo.m_iID_CONTACT = Convert.ToInt32(((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpContactPerson")).SelectedValue);
            ePersonnelInfo.m_iID_LAST_VISIT = Convert.ToInt32(((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpLastVisit")).SelectedValue);
            ePersonnelInfo.m_iID_VISA = Convert.ToInt32(((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpVisaType")).SelectedValue);
            ePersonnelInfo.m_sBOSIET_VALID = ((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpBOSIET")).SelectedValue;
            //ePersonnelInfo.m_sHUET_VALID = ((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpHUET")).SelectedValue;
            ePersonnelInfo.m_sMEDICAL = ((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpMedical")).SelectedValue;
            ePersonnelInfo.m_sGENDER = ((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpGender")).SelectedValue;
            //ePersonnelInfo.m_sPPE = ((DropDownList)grdView.Rows[e.RowIndex].FindControl("drpPPE")).SelectedValue;
            ePersonnelInfo.m_sEMPLOYEE_NAME = Convert.ToString(((TextBox)grdView.Rows[e.RowIndex].FindControl("txtEmployeeName")).Text);
            ePersonnelInfo.m_sCOMPANY = Convert.ToString(((TextBox)grdView.Rows[e.RowIndex].FindControl("txtCompanyName")).Text);

            PersonnelInfo.updatePersonnelInfo(ePersonnelInfo);

            grdView.EditIndex = -1;
            Retrieve_Data(lRequestId);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
    protected void grdView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long lRequestId = long.Parse(RequestIDHF.Value);

            bool success = PersonnelInfo.deletePersonnelInfo(Convert.ToInt64(grdView.DataKeys[e.RowIndex].Values[0].ToString()));
            Retrieve_Data(lRequestId);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}
