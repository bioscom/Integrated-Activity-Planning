<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestForChange.aspx.cs" Inherits="RequestForChange" Title="" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="UserControl/ChangeRequestForm.ascx" TagName="ChangeRequestForm" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager ID="Radscriptmanager1" runat="server"></telerik:RadScriptManager>
            <h3 style="color: black">SCiN Integrated Activity Planning Change Request Form</h3>
            <hr />
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="Warning"></asp:Label>
            <uc1:ChangeRequestForm ID="ChangeRequestForm1" runat="server" />
            <br />
        </div>
    </form>
</body>
</html>
