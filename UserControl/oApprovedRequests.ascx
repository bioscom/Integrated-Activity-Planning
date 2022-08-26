<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oApprovedRequests.ascx.cs" Inherits="UserControl_oApprovedRequests" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function RowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }

        function ShowCommentFormApproved(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("ViewComments2.aspx?RequestId=" + id, "CommentDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function ShowReportFormApproved(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            var wndw = window.radopen("Report/IAPReport.aspx?RequestId=" + id, "ReportDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
            return false;
        }

        function RowDblClick(sender, eventArgs) {
            var wndw = window.radopen("ViewComments2.aspx?lRequestId=" + eventArgs.getDataKeyValue("IDREQUEST"), "CommentDialog", 900, 500);
            wndw.set_visibleStatusbar(false);
            wndw.Center();
        }

        <%--function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }--%>

    </script>
</telerik:RadCodeBlock>

<div style="float:right"> 
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Excel_XLSX.png"
        OnClick="ImageButton_Click" AlternateText="Html" ToolTip="Export to Excel" />
</div>

<div id="demo" class="demo-container no-bg" style="width: 100%">

    <telerik:RadGrid RenderMode="Lightweight" ID="grdView" AllowSorting="true" AllowFilteringByColumn="true" runat="server" FilterType="HeaderContext" EnableHeaderContextMenu="true" 
        EnableHeaderContextFilterMenu="true" AllowPaging="True" PagerStyle-AlwaysVisible="true" PageSize="15" AllowAutomaticInserts="true" 
        AllowAutomaticUpdates="true" AllowAutomaticDeletes="true" Font-Size="11px" 
        OnItemDeleted="grdView_ItemDeleted" OnItemUpdated="grdView_ItemUpdated" OnItemInserted="grdView_ItemInserted" OnItemCreated="grdView_ItemCreated"
        OnItemCommand="grdView_ItemCommand" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource" OnDetailTableDataBind="grdView_DetailTableDataBind" 
        OnFilterCheckListItemsRequested="grdView_FilterCheckListItemsRequested" 
        OnHTMLExporting="grdView_HtmlExporting" OnBiffExporting="grdView_BiffExporting"
        OnExcelMLWorkBookCreated="grdView_ExcelMLWorkBookCreated" Width="100%">

        <AlternatingItemStyle BackColor="#FFFF99" />
        <ItemStyle BackColor="#FFFFFF" />
        
        <PagerStyle PageSizes="5,10,20,50,100" PagerTextFormat="{4}<strong>{5}</strong> items matching your search criteria" PageSizeLabelText="Items per page:" />

        <MasterTableView Width="100%" CommandItemDisplay="None" DataKeyNames="IDREQUEST, IDOU" AutoGenerateColumns="false" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />

            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <asp:Label ID="numberLabel" runat="server"/>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn ItemStyle-Width="90px" ItemStyle-ForeColor="#003366" ItemStyle-Font-Bold="true" SortExpression="REQUEST_NUMBER" HeaderText="Request Number" HeaderButtonType="TextButton" DataField="REQUEST_NUMBER" AutoPostBackOnFilter="true" CurrentFilterFunction="StartsWith"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn ItemStyle-Width="300px" SortExpression="PROJECT_ACTIVITY" HeaderText="Activity" HeaderButtonType="TextButton" DataField="PROJECT_ACTIVITY"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="LOCATION" HeaderText="Location" HeaderButtonType="TextButton" DataField="LOCATION"></telerik:GridBoundColumn> 
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="PLANTYPE" HeaderText="Plan Type" HeaderButtonType="TextButton" DataField="PLANTYPE"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="FULLNAME" HeaderText="Originator" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="REQUESTDATE" HeaderText="Request Date" HeaderButtonType="TextButton" DataField="REQUESTDATE" DataFormatString="{0:dd-MMM-yyyy}"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn SortExpression="STATUS" HeaderText="Status" HeaderButtonType="TextButton" DataField="STATUS"></telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn SortExpression="CHANGETYPE" HeaderText="Change Type" HeaderButtonType="TextButton" DataField="CHANGETYPE"></telerik:GridBoundColumn>

                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="AUTHORISERNAME" HeaderText="Functional Authorizer" HeaderButtonType="TextButton" DataField="AUTHORISERNAME"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterCheckListEnableLoadOnDemand="true" SortExpression="PLANNERNAME" HeaderText="IAP Planner" HeaderButtonType="TextButton" DataField="PLANNERNAME"></telerik:GridBoundColumn>

                <telerik:GridTemplateColumn UniqueName="ViewColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="ViewCommentLink" runat="server" Text="View Comment"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ReportColumn">
                    <ItemTemplate>
                        <asp:HyperLink ID="reportLink" runat="server" Text="ViewReport"></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

            </Columns>

            <CommandItemTemplate>
                <a href="#" onclick="return ShowInsertForm();">Add New Request</a>
            </CommandItemTemplate>

        </MasterTableView>

        <ClientSettings>
            <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
        </ClientSettings>

    </telerik:RadGrid>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow RenderMode="Lightweight" ID="CommentDialog" runat="server" Title="View Comments" Height="500px"
                Width="900px" Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

            <telerik:RadWindow RenderMode="Lightweight" ID="ReportDialog" runat="server" Title="Report" Height="500px" Width="900px"
                Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
    </telerik:RadAjaxLoadingPanel>
</div>
<asp:HiddenField ID="RequestorTypeHF" runat="server" />

