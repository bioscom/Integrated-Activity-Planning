using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for YTDCalculator
/// </summary>
public class YTDCalculator
{
	public YTDCalculator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public decimal ProductionCalculator(Decimal JANACTUAL, Decimal FEBACTUAL, int YYear )
    {

        Decimal DaysInFebruary   = (DateTime.DaysInMonth(YYear, 2));
        int NumberOfDays = 365 - ((DateTime.DaysInMonth(YYear, 3)) + (DateTime.DaysInMonth(YYear, 4)) + 
                                    (DateTime.DaysInMonth(YYear, 5)) + (DateTime.DaysInMonth(YYear, 6)) + 
                                    (DateTime.DaysInMonth(YYear, 7)) + (DateTime.DaysInMonth(YYear, 8)) + 
                                    (DateTime.DaysInMonth(YYear, 9)) + (DateTime.DaysInMonth(YYear, 10)) + 
                                    (DateTime.DaysInMonth(YYear, 11)) + (DateTime.DaysInMonth(YYear, 12)));
        if (DaysInFebruary == 29)
        {
         NumberOfDays += 1;
        }

        return (((JANACTUAL * 31) + (FEBACTUAL * DaysInFebruary)) / NumberOfDays);

    }
}
