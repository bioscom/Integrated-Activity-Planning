<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oRequestArchive.ascx.cs" Inherits="UserControl_oRequestArchive" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
    //Put your JavaScript code here.

    function NodeClientClicked(sender, args) {
        //var nodeText = args.get_node().get_text();
        var nodeValue = args.get_node().get_value();
        var oWnd = window.radopen("ViewComments2.aspx?RequestId=" + nodeValue);
        oWnd.SetTitle("View Request Details");
        oWnd.setSize(900, 500);
        oWnd.set_visibleStatusbar(false);
        oWnd.Center();

    }
</script>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
</telerik:RadAjaxLoadingPanel>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="700px" HorizontalAlign="NotSet">

    <div style="color:black">
        <strong>History</strong>
    </div>
    <hr />
    <telerik:RadTreeView ID="mnuRadTreeView" OnClientNodeClicked="NodeClientClicked" runat="server"></telerik:RadTreeView>

</telerik:RadAjaxPanel>

<telerik:RadWindow ID="RadWindow1" runat="server"></telerik:RadWindow>
