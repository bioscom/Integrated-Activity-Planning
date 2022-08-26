<%@ Page Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="RequestList.aspx.cs" Inherits="RequestList" Title="IAP Change Request" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/oPendingRequests.ascx" TagName="oPendingRequests" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/oApprovedRequests.ascx" TagName="oApprovedRequests" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/oRejectedRequests.ascx" TagName="oRejectedRequests" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="color: Black; width: 99%">
        
        <h2>Integrated Activity Planning Change Requests For My Support/Approval</h2>

        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" Skin="Silk" ID="RadTabStrip1" Align="Left" AutoPostBack="true" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="Change Requests Pending My Approval"></telerik:RadTab>
                <telerik:RadTab Text="Approved Change Requests"></telerik:RadTab>
                <telerik:RadTab Text="Rejected Change Requests"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div class="contentWrapper">
                    <uc1:oPendingRequests ID="oPendingRequests1" runat="server"></uc1:oPendingRequests>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div class="contentWrapper">
                    <uc2:oApprovedRequests ID="oApprovedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="RadPageView3">
                <div class="contentWrapper">
                    <uc3:oRejectedRequests ID="oRejectedRequests1" runat="server" />
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
