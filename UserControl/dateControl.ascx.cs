using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_dateControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ErrorMssg(string sErrMssg)
    {
        valdtDateRequired.ErrorMessage = sErrMssg;
    }

    public string SelectedDate
    {
        get
        {
            return txtDate.Text;
        }
    }

    public DateTime dtSelectedDate
    {
        get
        {
            DateTime dt = new DateTime();
            try
            {
                if (txtDate.Text != "")
                {
                    string[] PF = txtDate.Text.Split('/');
                    dt = new DateTime(int.Parse(PF[2]), int.Parse(PF[1]), int.Parse(PF[0]));
                }
            }
            catch (Exception ex)
            {
                //appMonitor.logAppExceptions(ex);
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
            return ((txtDate.Text == "") ? DateTime.Today.Date : dt.Date);
        }
    }

    public void setDate(string sDate)
    {
        //string[] PF = sDate.Split('/');
        //DateTime dt = new DateTime(int.Parse(PF[2]), int.Parse(PF[1]), int.Parse(PF[0]));

        //txtDate.Text = dt.ToString();
        txtDate.Text = sDate;
    }

    public ImageButton ImageBtn
    {
        get
        {
            return imgBtnStartDate;
        }
    }

    public TextBox txtDateTextBox
    {
        get
        {
            return txtDate;
        }
    }
}
