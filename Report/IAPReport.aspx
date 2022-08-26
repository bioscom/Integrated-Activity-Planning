<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IAPReport.aspx.cs" Inherits="Report_IAPReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="Radscriptmanager1" runat="server"></telerik:RadScriptManager>
        <div>
            <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" Font-Size="10pt"
                Height="650px" Width="98%" ZoomMode="Percent">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>

