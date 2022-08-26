<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="eSearch.aspx.cs" Inherits="eSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function ShowCommentForm(id, rowIndex) {
                var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("ViewComments2.aspx?RequestId=" + id, "CommentDialog", 900, 500);
            wndw.Center();
            return false;
        }

        function ShowRerouteForm(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("IAPApproverRouter.aspx?RequestId=" + id, "RerouteDialog", 1100, 500);
            wndw.Center();
            return false;
        }

        function RowDblClick(sender, eventArgs) {
            var wndw = window.radopen("ViewDetails.aspx.aspx?lRequestId=" + eventArgs.getDataKeyValue("lCommitmentId"), "UserListDialog");
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }

        </script>
    </telerik:RadCodeBlock>


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

    <div style="color: Black; width: 99%">
        <h2>Integrated Activity Planning: Search Result</h2>
        <div id="demo" class="demo-container no-bg" style="width: 100%">

            <telerik:RadGrid RenderMode="Lightweight" ID="grdView" AllowSorting="true" runat="server" AllowPaging="True" PagerStyle-AlwaysVisible="true" PageSize="15" Font-Size="11px"
                OnItemCreated="grdView_ItemCreated" OnItemCommand="grdView_ItemCommand" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource" Width="100%">

                <AlternatingItemStyle BackColor="#FFFF99" />
                <ItemStyle BackColor="#FFFFFF" />
                <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                <%--<PagerStyle PageSizes="5,10" PagerTextFormat="{4}<strong>{5}</strong> cars matching your search criteria" PageSizeLabelText="Cars per page:" />--%>

                <MasterTableView Width="100%" CommandItemDisplay="None" DataKeyNames="IDREQUEST, IDOU" AutoGenerateColumns="false" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:Label ID="numberLabel" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn ItemStyle-Width="90px" ItemStyle-ForeColor="#003366" ItemStyle-Font-Bold="true" SortExpression="REQUEST_NUMBER" HeaderText="Request Number" HeaderButtonType="TextButton" DataField="REQUEST_NUMBER"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn ItemStyle-Width="300px" SortExpression="PROJECT_ACTIVITY" HeaderText="Activity" HeaderButtonType="TextButton" DataField="PROJECT_ACTIVITY"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="LOCATION" HeaderText="Location" HeaderButtonType="TextButton" DataField="LOCATION"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="PLANTYPE" HeaderText="Plan Type" HeaderButtonType="TextButton" DataField="PLANTYPE"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="FULLNAME" HeaderText="Originator" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn SortExpression="REQUESTDATE" HeaderText="Request Date" HeaderButtonType="TextButton" DataField="REQUESTDATE" DataFormatString="{0:dd-MMM-yyyy}"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn ItemStyle-Width="150px" SortExpression="STATUS" HeaderText="Status" HeaderButtonType="TextButton" DataField="STATUS"></telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn UniqueName="ViewColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="ViewCommentLink" runat="server" Text="View Comment"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ReRouteColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="ReRouteLink" runat="server" Text="Re-Route"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>

                </MasterTableView>

                <ClientSettings>
                    <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
                </ClientSettings>

            </telerik:RadGrid>

            <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Windows>
                    <telerik:RadWindow RenderMode="Lightweight" ID="InputDialog" runat="server" Title="IAP CRET Request Form" Height="500px"
                        Width="1100px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>

                    <telerik:RadWindow RenderMode="Lightweight" ID="EditDialog" runat="server" Title="Edit IAP Request" Height="500px"
                        Width="1100px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>

                    <telerik:RadWindow RenderMode="Lightweight" ID="CommentDialog" runat="server" Title="View Comments" Height="500px"
                        Width="750px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>

                    <telerik:RadWindow RenderMode="Lightweight" ID="ActionDialog" runat="server" Title="Approve/Support Request" Height="500px" Width="900px"
                        Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>

                    <telerik:RadWindow RenderMode="Lightweight" ID="RerouteDialog" runat="server" Title="Reroute Request" Height="500px" Width="900px"
                        Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                    </telerik:RadWindow>

                </Windows>
            </telerik:RadWindowManager>

            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
            </telerik:RadAjaxLoadingPanel>
        </div>
    </div>
</asp:Content>

