<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oRequestChangeRating.ascx.cs" Inherits="UserControl_oRequestChangeRating" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Label ID="mssgLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>

<style>
    .ImageButtons {
        padding-top: 30px;
        padding-right: 1px;
    }

    div.qsfConfig {
        margin: 0px;
    }

    div.cfgContent {
        padding-bottom: 0px;
    }

    .auto-style1 {
        color: #FF0000;
        font-weight: bold;
    }
</style>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript">

        function RowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }

        function ShowInsertForm() {
            var wndw = window.radopen("Request2.aspx", "InputDialog", 1100, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function ShowCommentFormPending(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("ViewComments2.aspx?RequestId=" + id, "CommentDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function ShowEditForm(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("Request2.aspx?RequestId=" + id, "EditDialog", 1100, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function ShowActionForm(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("PlannerRequestApproval.aspx?RequestId=" + id, "ActionDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function ShowActionFormApprovers(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("RequestApproval.aspx?RequestId=" + id, "ApproverActionDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        //function openRadWin() {
        //    var wndw = radopen("../RequestForChange.aspx", "AddRequest", 1100, 500);
        //    wndw.Center();
        //}

        function ShowRerouteForm(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("IAPApproverRouter.aspx?RequestId=" + id, "RerouteDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function RowDblClick(sender, eventArgs) {
            var wndw = window.radopen("ViewComments2.aspx?lRequestId=" + eventArgs.getDataKeyValue("IDREQUEST"), "CommentDialog");
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }

        (function () {
            var demo = window.demo = {};
            demo.onRequestStart = function (sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        })();

    </script>
</telerik:RadCodeBlock>

<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">--%>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>

        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdView" LoadingPanelID="gridLoadingPanel"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="grdView">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdView" LoadingPanelID="gridLoadingPanel"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>

    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel runat="server" ID="gridLoadingPanel"></telerik:RadAjaxLoadingPanel>

<div style="float: right">
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Excel_XLSX.png"
        OnClick="ImageButton_Click" AlternateText="Html" ToolTip="Export to Excel" />

    <%-- <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/Excel_ExcelML.png"
        OnClick="ImageButton_Click" AlternateText="ExcelML" />

    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="Images/Excel_BIFF.png"
        OnClick="ImageButton_Click" AlternateText="Biff" />

    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="Images/Excel_XLSX.png"
        OnClick="ImageButton_Click" AlternateText="Xlsx" />--%>
</div>

<div style="float: left; width: 100%">
    <span class="auto-style1">***Pending Requests in the last six months***</span>
    <telerik:RadGrid RenderMode="Lightweight" ID="grdView" AllowSorting="true" AllowFilteringByColumn="true" runat="server" FilterType="HeaderContext" EnableHeaderContextMenu="true"
        AllowPaging="True" PagerStyle-AlwaysVisible="true" PageSize="15" Font-Size="11px" EnableHeaderContextFilterMenu="true" OnColumnCreated="grdView_ColumnCreated"
        OnItemCreated="grdView_ItemCreated" OnItemCommand="grdView_ItemCommand" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource"
        OnDetailTableDataBind="grdView_DetailTableDataBind" OnFilterCheckListItemsRequested="grdView_FilterCheckListItemsRequested"
        OnHTMLExporting="grdView_HtmlExporting" OnBiffExporting="grdView_BiffExporting"
        OnExcelMLWorkBookCreated="grdView_ExcelMLWorkBookCreated" Width="100%">

        <AlternatingItemStyle BackColor="#FFFF99" />
        <ItemStyle BackColor="#FFFFFF" />

        <PagerStyle PageSizes="5,10,20,50,100" PagerTextFormat="{4}<strong>{5}</strong> items matching your search criteria" PageSizeLabelText="Items per page:" />

        <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="IDREQUEST, IDOU" AutoGenerateColumns="false">
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />

            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <asp:Label ID="numberLabel" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn ItemStyle-Width="90px" ItemStyle-ForeColor="#003366" ItemStyle-Font-Bold="true" SortExpression="REQUEST_NUMBER" HeaderText="Request Number" HeaderButtonType="TextButton" DataField="REQUEST_NUMBER" AutoPostBackOnFilter="true" CurrentFilterFunction="StartsWith"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ItemStyle-Width="300px" SortExpression="PROJECT_ACTIVITY" HeaderText="Activity" HeaderButtonType="TextButton" DataField="PROJECT_ACTIVITY"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="LOCATION" HeaderText="Location" HeaderButtonType="TextButton" DataField="LOCATION"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="PLANTYPE" HeaderText="Plan Type" HeaderButtonType="TextButton" DataField="PLANTYPE"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="FULLNAME" HeaderText="Originator" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="REQUESTDATE" HeaderText="Request Date" HeaderButtonType="TextButton" DataField="REQUESTDATE" DataFormatString="{0:dd-MMM-yyyy}"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ItemStyle-Width="150px" UniqueName="status" SortExpression="STATUS" HeaderText="Status" HeaderButtonType="TextButton" DataField="STATUS" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="AUTHORISERNAME" HeaderText="Functional Authorizer" HeaderButtonType="TextButton" DataField="AUTHORISERNAME"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="PLANNERNAME" HeaderText="IAP Planner" HeaderButtonType="TextButton" DataField="PLANNERNAME"></telerik:GridBoundColumn>


                <telerik:GridTemplateColumn UniqueName="ViewColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="ViewCommentLink" runat="server" Text="View Comment"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="EditColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="editLink" runat="server" Text="Edit"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ActionColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="actionLink" runat="server" Text="Action..."></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ApproverActionColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="approverLink" runat="server" Text="Action..."></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ReRouteColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="ReRouteLink" runat="server" Text="Re-Route"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="CancelColumn">
                    <ItemTemplate>
                        <asp:LinkButton ID="CancelLink" OnClick="CancelLink_Click" OnClientClick="return confirm('Are you sure you want to cancel this request?');" runat="server">Cancel</asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>

            <CommandItemTemplate>
                <a href="#" onclick="return ShowInsertForm();"><b style="font-size: 14px">+ Create New Request</b></a>
            </CommandItemTemplate>

        </MasterTableView>

        <ClientSettings>
            <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
        </ClientSettings>

    </telerik:RadGrid>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow RenderMode="Lightweight" ID="InputDialog" runat="server" Title="IAP CRET Request Form"
                Height="500px" Width="1100px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="EditDialog" runat="server" Title="Edit IAP Request"
                Height="500px" Width="1100px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="CommentDialog" runat="server" Title="View Comments"
                Height="500px" Width="900px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="ActionDialog" runat="server" Title="Support Request"
                Height="500px" Width="900px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="ApproverActionDialog" runat="server" Title="Support Request"
                Height="500px" Width="900px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="RerouteDialog" runat="server" Title="Reroute Request"
                Height="500px" Width="900px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
    </telerik:RadAjaxLoadingPanel>
</div>

<asp:HiddenField ID="RequestorTypeHF" runat="server" />

<%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
