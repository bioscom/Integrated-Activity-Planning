using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

public partial class UserControl_OffShoreDashBoard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        //RadHtmlChart1.DataSource = GetData();
        //RadHtmlChart1.DataBind();
    }

    public void Init_Page(int iOu, int iYear)
    {
        DataTable dt = dtGetPlottingData(iOu, iYear);
        RadHtmlChart1.DataSource = dt;
        RadHtmlChart1.DataBind();
    }

    private DataTable dtGetPlottingData(int iOu, int iYear)
    {
        DataTable dt = dtGetData(iOu, iYear);

        int iJan = 0, iFeb = 0, iMar = 0, iApr = 0, iMay = 0, iJun = 0, iJul = 0, iAug = 0, iSep = 0, iOct = 0, iNov = 0, iDec = 0;

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["MMONTH"].ToString() == "JAN") iJan += 1;
            else if (dr["MMONTH"].ToString() == "FEB") iFeb += 1;
            else if (dr["MMONTH"].ToString() == "MAR") iMar += 1;
            else if (dr["MMONTH"].ToString() == "APR") iApr += 1;
            else if (dr["MMONTH"].ToString() == "MAY") iMay += 1;
            else if (dr["MMONTH"].ToString() == "JUN") iJun += 1;
            else if (dr["MMONTH"].ToString() == "JUL") iJul += 1;
            else if (dr["MMONTH"].ToString() == "AUG") iAug += 1;
            else if (dr["MMONTH"].ToString() == "SEP") iSep += 1;
            else if (dr["MMONTH"].ToString() == "OCT") iOct += 1;
            else if (dr["MMONTH"].ToString() == "NOV") iNov += 1;
            else if (dr["MMONTH"].ToString() == "DEC") iDec += 1;
        }

        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("MMONTH", typeof(string));
        dtReport.Columns.Add("CRNO", typeof(int));

        dtReport.Rows.Add("January", iJan);
        dtReport.Rows.Add("February", iFeb);
        dtReport.Rows.Add("March", iMar);
        dtReport.Rows.Add("April", iApr);
        dtReport.Rows.Add("May", iMay);
        dtReport.Rows.Add("June", iJun);
        dtReport.Rows.Add("July", iJul);
        dtReport.Rows.Add("August", iAug);
        dtReport.Rows.Add("September", iSep);
        dtReport.Rows.Add("October", iOct);
        dtReport.Rows.Add("November", iNov);
        dtReport.Rows.Add("December", iDec);

        return dtReport;
    }

    private DataTable dtGetData(int OuId, int iYear)
    {
        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DashBoard();

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":OuId";
        param.Value = OuId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iYear";
        param.Value = iYear;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    static DataTable GetTable()
    {
        // Here we create a DataTable with four columns.
        DataTable table = new DataTable();
        table.Columns.Add("Weight", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Breed", typeof(string));
        table.Columns.Add("Date", typeof(DateTime));

        // Here we add five DataRows.
        table.Rows.Add(57, "Koko", "Shar Pei",
            DateTime.Now);
        table.Rows.Add(130, "Fido", "Bullmastiff",
            DateTime.Now);
        table.Rows.Add(92, "Alex", "Anatolian Shepherd Dog",
            DateTime.Now);
        table.Rows.Add(25, "Charles", "Cavalier King Charles Spaniel",
            DateTime.Now);
        table.Rows.Add(7, "Candy", "Yorkshire Terrier",
            DateTime.Now);
        return table;
    }

    private DataSet GetData()
    {
        DataSet ds = new DataSet("Bookstore");
        DataTable dt = new DataTable("Products");
        dt.Columns.Add("Id", Type.GetType("System.Int32"));
        dt.Columns.Add("Name", Type.GetType("System.String"));
        dt.Columns.Add("Price", Type.GetType("System.Double"));
        dt.Rows.Add(1, "Pen", 5.45);
        dt.Rows.Add(2, "Audio book", 9.95);
        dt.Rows.Add(3, "Pencil", 1.99);
        dt.Rows.Add(4, "Book", 15.95);
        dt.Rows.Add(5, "Newspaper", 0.95);
        dt.Rows.Add(6, "Magazine", 3.95);
        ds.Tables.Add(dt);
        return ds;
    }

    public int Year
    {
        get
        {
            if (ViewState["Year"] == null)
            {
                return DateTime.Today.Year;
            }
            return (int) ViewState["Year"];
        }

        set
        {
            ViewState["Year"] = value;
        }
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        string seriesName = RadHtmlChart1.PlotArea.Series[0].Name;

        if (seriesName == "Years")
        {
            Year = int.Parse(e.Argument);
            //SqlDataSource2.SelectParameters[0].DefaultValue = Year.ToString();
            RadHtmlChart1.PlotArea.XAxis.DataLabelsField = "Quarter";
            RadHtmlChart1.PlotArea.Series[0].DataFieldY = "Req";
            RadHtmlChart1.PlotArea.Series[0].Name = "Quarters";

            //RadHtmlChart1.DataSourceID = "SqlDataSource2";
            //RadHtmlChart1.DataSource = dtGetDataForQuarter(1, Year);
        }
        else
        {
            if (seriesName == "Quarters")
            {
                int quarter = int.Parse(e.Argument);
                int iYear = Year;

                //SqlDataSource3.SelectParameters[0].DefaultValue = Year.ToString();
                //SqlDataSource3.SelectParameters[1].DefaultValue = quarter.ToString();
                RadHtmlChart1.PlotArea.XAxis.DataLabelsField = "Month";
                RadHtmlChart1.PlotArea.Series[0].DataFieldY = "Req";
                RadHtmlChart1.PlotArea.Series[0].Name = "Months";
                //"SqlDataSource3"

                //RadHtmlChart1.DataSource = dtGetDataForMonth(1, iYear, quarter);
            }
        }
    }

    //private DataTable dtGetDataForQuarter(int OuId, int iYear)
    //{
    //    string sql = "SELECT COUNT(*) Req, TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR FROM IAP_OU ";
    //    sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
    //    sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
    //    sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
    //    sql += "WHERE IAP_OU.IDOU = :OuId AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') = :iYear GROUP BY IAP_REQUESTFORM.requestdate ORDER BY YEAR";

    //    //SELECT Sum(Revenue) as Rev, Quarter FROM Revenue WHERE Year = @Year GROUP BY Quarter

    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":OuId";
    //    param.Value = OuId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iYear";
    //    param.Value = iYear;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    //private DataTable dtGetDataForMonth(int OuId, int iYear, int iQuarter)
    //{
    //    string sql = "SELECT COUNT(*) Req, TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR, TO_CHAR(IAP_REQUESTFORM.requestdate, 'Q') Quarter FROM IAP_OU ";
    //    sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
    //    sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
    //    sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
    //    sql += "WHERE IAP_OU.IDOU = :OuId ";
    //    sql += "AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') = :iYear ";
    //    sql += "AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'Q') = :iQuarter ";
    //    sql += " GROUP BY IAP_REQUESTFORM.requestdate ORDER BY YEAR";

    //    OracleCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    OracleParameter param = comm.CreateParameter();
    //    param.ParameterName = ":OuId";
    //    param.Value = OuId;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iYear";
    //    param.Value = iYear;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":iQuarter";
    //    param.Value = iQuarter;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    //protected void Refresh_Click(object sender, EventArgs e)
    //{
    //    RadHtmlChart1.PlotArea.XAxis.DataLabelsField = "Year";
    //    RadHtmlChart1.PlotArea.Series[0].DataFieldY = "Req";
    //    RadHtmlChart1.PlotArea.Series[0].Name = "Years";

    //    DataTable dt = dtGetDataForYear(1);
    //    RadHtmlChart1.DataSource = dt;
    //}
}