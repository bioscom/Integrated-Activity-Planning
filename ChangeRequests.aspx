<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="ChangeRequests.aspx.cs" Inherits="ChangeRequests" %>

<%@ Register Src="~/UserControl/oPendingRequests.ascx" TagName="oPendingRequests" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/oApprovedRequests.ascx" TagName="oApprovedRequests" TagPrefix="uc4" %>
<%@ Register Src="~/UserControl/oRejectedRequests.ascx" TagName="oRejectedRequests" TagPrefix="uc5" %>
<%@ Register Src="~/UserControl/oRequestChangeRating.ascx" TagName="oRequestChangeRating" TagPrefix="uc2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="color: Black; width: 99%">
        <h3>SPDC and SNEPCo Integrated Activity Planning Change Requests</h3>

        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" Skin="Silk" ID="RadTabStrip1" Align="Left" AutoPostBack="true" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Requests Pending Approval"></telerik:RadTab>
                <telerik:RadTab Text="Change Requests Approved"></telerik:RadTab>
                <telerik:RadTab Text="Change Requests Rejected"></telerik:RadTab>
                <telerik:RadTab Text="Change Requests Rejected"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div class="contentWrapper">
                    <uc1:oPendingRequests ID="oPendingRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div class="contentWrapper">
                    <uc4:oApprovedRequests ID="oApprovedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView4">
                <div class="contentWrapper">
                    <uc5:oRejectedRequests ID="oRejectedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>

           <%-- <telerik:RadPageView runat="server" ID="RadPageView3">
                <div class="contentWrapper">
                    <uc2:oRequestChangeRating runat="server" ID="oRequestChangeRating" />
                </div>
            </telerik:RadPageView>--%>

        </telerik:RadMultiPage>

    </div>
</asp:Content>

