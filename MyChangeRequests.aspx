<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="MyChangeRequests.aspx.cs" Inherits="MyChangeRequests" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register src="~/UserControl/oPendingRequests.ascx" tagname="oPendingRequests" tagprefix="uc3" %>
<%@ Register src="~/UserControl/oApprovedRequests.ascx" tagname="oApprovedRequests" tagprefix="uc4" %>
<%@ Register src="~/UserControl/oRejectedRequests.ascx" tagname="oRejectedRequests" tagprefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="color: Black; width: 99%">
        <h2>My Integrated Activity Planning Change Requests Work space</h2>

        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" Skin="Silk" ID="RadTabStrip1" Align="Left" AutoPostBack="true" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="My Requests Pending Approval"></telerik:RadTab>
                <telerik:RadTab Text="My Requests Approved"></telerik:RadTab>
                <telerik:RadTab Text="My Requests Rejected"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div class="contentWrapper">
                    <uc3:oPendingRequests ID="oPendingRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div class="contentWrapper">
                    <uc4:oApprovedRequests ID="oApprovedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="RadPageView3">
                <div class="contentWrapper">
                    <uc5:oRejectedRequests ID="oRejectedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <br /><br /><br />
    </div>
</asp:Content>