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

public partial class UserControl_OnshoreDashBoards : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = dtGetDataForYear(1);

            //var result = from r in dt.AsEnumerable().Sum(c => c.Field<int>("REQ") select r) ;


            RadHtmlChart1.DataSource = dt;
            //RadHtmlChart1.DataBind();
        }
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
            RadHtmlChart1.DataSource = dtGetDataForQuarter(1, Year);
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
                
                RadHtmlChart1.DataSource = dtGetDataForMonth(1, iYear, quarter);
            }
        }
    }

    private DataTable dtGetDataForYear(int OuId)
    {
        string sql = "SELECT COUNT(*) Req, TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR FROM IAP_OU ";
        sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
        sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
        sql += "WHERE IAP_OU.IDOU = :OuId GROUP BY IAP_REQUESTFORM.requestdate ORDER BY YEAR";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        OracleParameter param = comm.CreateParameter();
        param.ParameterName = ":OuId";
        param.Value = OuId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    private DataTable dtGetDataForQuarter(int OuId, int iYear)
    {
        string sql = "SELECT COUNT(*) Req, TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR FROM IAP_OU ";
        sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
        sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
        sql += "WHERE IAP_OU.IDOU = :OuId AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') = :iYear GROUP BY IAP_REQUESTFORM.requestdate ORDER BY YEAR";

        //SELECT Sum(Revenue) as Rev, Quarter FROM Revenue WHERE Year = @Year GROUP BY Quarter

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

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

    private DataTable dtGetDataForMonth(int OuId, int iYear, int iQuarter)
    {
        string sql = "SELECT COUNT(*) Req, TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') YEAR, TO_CHAR(IAP_REQUESTFORM.requestdate, 'Q') Quarter FROM IAP_OU ";
        sql += "INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU ";
        sql += "INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA ";
        sql += "INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID ";
        sql += "WHERE IAP_OU.IDOU = :OuId ";
        sql += "AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'YYYY') = :iYear ";
        sql += "AND TO_CHAR(IAP_REQUESTFORM.requestdate, 'Q') = :iQuarter ";
        sql += " GROUP BY IAP_REQUESTFORM.requestdate ORDER BY YEAR";

        OracleCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

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

        param = comm.CreateParameter();
        param.ParameterName = ":iQuarter";
        param.Value = iQuarter;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    protected void Refresh_Click(object sender, EventArgs e)
    {
        RadHtmlChart1.PlotArea.XAxis.DataLabelsField = "Year";
        RadHtmlChart1.PlotArea.Series[0].DataFieldY = "Req";
        RadHtmlChart1.PlotArea.Series[0].Name = "Years";

        DataTable dt = dtGetDataForYear(1);
        RadHtmlChart1.DataSource = dt;
    }
}
