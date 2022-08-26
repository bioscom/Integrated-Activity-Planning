<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Request2.aspx.cs" Inherits="Request2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="~/UserControl/ChangeRequestForm.ascx" TagName="ChangeRequestForm" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script language="javascript" type="text/javascript" src="../JavaScript/iap.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="Radscriptmanager1" runat="server"></telerik:RadScriptManager>
            <h3 style="margin-top: 0; color: black">SEPCiN Integrated Activity Planning Change Request Form</h3>
            <hr />
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="Warning"></asp:Label>
            <uc1:ChangeRequestForm ID="ChangeRequestForm1" runat="server" />
            <br />
        </div>
    </form>
</body>
</html>
