﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for DataSorter
/// </summary>
public static class DataSorter
{
	static DataSorter()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string MySortExpression(GridViewCommandEventArgs e)
    {
        int result;
        if (Int32.TryParse(e.CommandArgument.ToString(), out result) == false)
        {
            HttpContext.Current.Session["SortExpression"] = e.CommandArgument.ToString();
        }
        return HttpContext.Current.Session["SortExpression"].ToString();
    }
}
