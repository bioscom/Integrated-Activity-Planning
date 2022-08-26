using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

/// <summary>
/// Summary description for NumericFormatter
/// </summary>
public class NumericFormatter
{
	public NumericFormatter()
	{
		//
		// 
		//
	}

    public string NumericSeparator(object theValue)
    {
        string FormattedValue = "";
        if ((theValue == DBNull.Value) || (theValue.ToString() == ""))
        {
            FormattedValue = "***";
        }
        else if ((theValue != DBNull.Value) && (theValue.ToString() != ""))
        {
            FormattedValue = Convert.ToDecimal(theValue).ToString("N", CultureInfo.InvariantCulture);
        }
        
        return FormattedValue;
    }
}
