﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SPDC.Init_Page(1, 2016);
        SNEPCO.Init_Page(2, 2016);
    }
}