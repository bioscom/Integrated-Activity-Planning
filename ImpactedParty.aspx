<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="ImpactedParty.aspx.cs" Inherits="ImpactedParty" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/UserControl/oIPPendingRequests.ascx" TagName="oIPPendingRequests" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/oIPApprovedRequests.ascx" TagName="oIPApprovedRequests" TagPrefix="uc4" %>

<asp:Content ID="Content3" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="color: Black; width: 99%">
        <h3>Integrated Activity Planning Change Requests Impacting my team's activities</h3>

        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip1" Align="Left" AutoPostBack="true" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Requests Pending Review"></telerik:RadTab>
                <telerik:RadTab Text="Requests Reviewed"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div class="contentWrapper">
                    <uc1:oIPPendingRequests ID="oIPPendingRequests1" runat="server" />
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div class="contentWrapper">
                    <uc4:oIPApprovedRequests ID="oIPApprovedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
