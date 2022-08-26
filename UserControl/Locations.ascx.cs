using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Locations : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Area> Areas = RequestFormRequirement.getAreas();
            foreach (Area area in Areas)
            {
                areaList.Items.Add(new ListItem(area.m_sAREA, area.m_iIDAREA.ToString()));
            }
        }
    }

    public void LoadLocations()
    {
        LocationGridView.DataSource = RequestFormRequirement.retrieveLocations();
        LocationGridView.DataBind();
    }

    public void LoadLocationsByArea()
    {
        LocationGridView.DataSource = RequestFormRequirement.getLocationByArea(Convert.ToInt32(areaList.SelectedValue));
        LocationGridView.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool success = RequestFormRequirement.InsertLocation(txtLocation.Text, int.Parse(areaList.SelectedValue));
        if (success == true)
        {
            if (Convert.ToInt32(areaList.SelectedValue) > -1)
            {
                LoadLocationsByArea();
            }
            else
            {
                LoadLocations();
                mssgLabel.Text = txtLocation.Text + " successfully created."; LoadLocations();
            }
        }
    }

    protected void LocationGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LocationGridView.PageIndex = e.NewPageIndex;
        LoadLocationsByLogic();
    }

    protected void areaList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLocationsByLogic();
    }

    protected void LocationGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int LOCATIONID = 0;
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

	if (ButtonClicked == "DeleteThis")
	{

        	LinkButton delete = (LinkButton)LocationGridView.Rows[index].FindControl("deleteLinkButton");
        	LOCATIONID = Convert.ToInt32(delete.Attributes["LOCATIONID"].ToString());
        	ScriptManager.RegisterStartupScript(delete, delete.GetType(), "msg", "alert('Are you sure you want to delete this Location?');", true);

            bool deleted = RequestFormRequirement.DeleteLocation(LOCATIONID);
            if (deleted == true)
            {
                LoadLocationsByLogic();
            }
            else
            {
                ScriptManager.RegisterStartupScript(delete, delete.GetType(), "msg", "alert('Location in use, cannot be deleted.');", true);
            }
        }
    }

    private void LoadLocationsByLogic()
    {
        if (Convert.ToInt32(areaList.SelectedValue) > -1)
        {
            LoadLocationsByArea();
        }
        else
        {
            LoadLocations();
        }
    }
}
