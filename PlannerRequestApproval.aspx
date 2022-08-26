<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlannerRequestApproval.aspx.cs" Inherits="PlannerRequestApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/oRequestApproval.ascx" TagPrefix="uc1" TagName="oRequestApproval" %>
<%@ Register Src="~/UserControl/oImpactedParties.ascx" TagPrefix="uc1" TagName="oImpactedParties" %>
<%@ Register Src="~/UserControl/oImpactedPartiesComments.ascx" TagPrefix="uc1" TagName="oImpactedPartiesComments" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <div>
            <asp:Panel ID="pnlImpactedParties" runat="server">
                <uc1:oImpactedParties runat="server" ID="oImpactedParties" />
            </asp:Panel>
        </div>
        <br />
        <div>
            <uc1:oImpactedPartiesComments runat="server" ID="oImpactedPartiesComments" />
        </div>
        <br />
        <div>
            <asp:Panel ID="pnlApprovers" runat="server">
                <uc1:oRequestApproval runat="server" ID="oRequestApproval" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
