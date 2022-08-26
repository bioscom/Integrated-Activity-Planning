<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminProductionParties.ascx.cs" Inherits="UserControl_AdminProductionParties" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        function RowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }

        function ShowEditForm(id, rowIndex) {
            var grid = $find("<%= grdView.ClientID %>");

            var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
            grid.get_masterTableView().selectItem(rowControl, true);

            window.radopen("MakeRequest.aspx?lRequestId=" + id, "InputEditDialog");
            return false;
        }

        function ShowInsertForm() {
            window.radopen("MakeRequest.aspx", "InputEditDialog");
            return false;
        }

        function RowDblClick(sender, eventArgs) {
            window.radopen("ViewDetails.aspx.aspx?lCommitmentId=" + eventArgs.getDataKeyValue("lCommitmentId"), "UserListDialog");
        }

        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnDocument") >= 0)
                args.set_enableAjax(false);
        }

    </script>
</telerik:RadCodeBlock>--%>

<script type="text/javascript">
    function CloseAndRebind(args) {
        GetRadWindow().BrowserWindow.refreshGrid(args);
        GetRadWindow().close();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

        return oWindow;
    }

    function CancelEdit() {
        GetRadWindow().close();
    }

    (function (global, undefined) {
        var demo = {};

        function alertCallBackFn(arg) {
            radalert("<strong>radalert</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
        }

        function confirmCallBackFn(arg) {
            radalert("<strong>radconfirm</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
        }

        function promptCallBackFn(arg) {
            radalert("After 7.5 million years, <strong>Deep Thought</strong> answers:<h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Deep Thought");
        }

        Sys.Application.add_load(function () {
            //attach a handler to radio buttons to update global variable holding image url
            $telerik.$('input:radio').bind('click', function () {
                demo.imgUrl = $telerik.$(this).val();
            });
        });


        global.alertCallBackFn = alertCallBackFn;
        global.confirmCallBackFn = confirmCallBackFn;
        global.promptCallBackFn = promptCallBackFn;

        global.$dialogsDemo = demo;

    })(window);
</script>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
</telerik:RadAjaxLoadingPanel>
<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" DecorationZoneID="demo" EnableRoundedCorners="false" RenderMode="Lightweight" />

<%--div style="width: 99%" class="demo-container size-narrow">--%>

    <fieldset>
        <legend>
            <b style="color: green">Production Locations Supporting Parties</b>
        </legend>
        <table style="color: black">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLocation4" runat="server" Text="HUBS:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="ddlHubs" runat="server" AutoPostBack="true" EmptyMessage="Select Hub..." RenderMode="Lightweight" Skin="Office2010Silver" Width="300px" OnSelectedIndexChanged="ddlHubs_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLocation2" runat="server" Text="Responsible Party:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox RenderMode="Lightweight" ID="ddlAdmin" runat="server" Height="200" Width="430px" Font-Size="10pt"
                        DropDownWidth="500" EmptyMessage="Search Responsible Party..." HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="Contains" OnItemsRequested="ddlAdmin_ItemsRequested"
                        Skin="Office2010Silver">

                        <HeaderTemplate>
                            <table style="width: 385px; font-size: 9pt" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 165px;">Name</td>
                                    <td style="width: 220px;">Email Address</td>
                                </tr>
                            </table>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <table style="width: 385px; font-size: 9pt" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 165px;"><%# DataBinder.Eval(Container, "Text")%></td>
                                    <td style="width: 220px;"><%# DataBinder.Eval(Container, "Attributes['USERMAIL']")%></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Add" OnClick="btnSave_Click" Width="80px" />
                </td>
            </tr>
        </table>

        <br />
        <telerik:RadGrid ID="grdView" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="True" AllowSorting="true" Font-Size="11px"
            OnItemCommand="grdView_ItemCommand" OnItemCreated="grdView_ItemCreated" OnItemDataBound="grdView_ItemDataBound" OnItemDeleted="grdView_ItemDeleted"
            OnItemInserted="grdView_ItemInserted" OnItemUpdated="grdView_ItemUpdated" OnNeedDataSource="grdView_NeedDataSource" PageSize="15" RenderMode="Lightweight">

            <AlternatingItemStyle BackColor="#FFFF99" />
            <ItemStyle BackColor="#FFFFFF" />
            <%--<PagerStyle Mode="NumericPages" />--%>
            <PagerStyle PageSizes="5,10,15,20,50" PagerTextFormat="{4}<strong>{5}</strong> matching your search criteria" PageSizeLabelText="per page:" />

            <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="None" DataKeyNames="IDUSER, IDAREA" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="S/No" UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="numberLabel" runat="server" Width="10px" />
                        </ItemTemplate>
                        <HeaderStyle Width="10px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="FUNCTION" HeaderButtonType="TextButton" HeaderText="Hub" SortExpression="FUNCTION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FULLNAME" HeaderButtonType="TextButton" HeaderText="Responsible Party" SortExpression="FULLNAME"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="USERNAME" HeaderButtonType="TextButton" HeaderText="User Name" SortExpression="USERNAME"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="USERMAIL" HeaderButtonType="TextButton" HeaderText="Email Address" SortExpression="USERMAIL"></telerik:GridBoundColumn>

                    <telerik:GridTemplateColumn UniqueName="DeleteColumn">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteLink" runat="server" OnClick="lnkDelete_Click" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                </Columns>
            </MasterTableView>

            <%--<ClientSettings>
            <ClientEvents OnRowDblClick="RowDblClick" />
        </ClientSettings>--%>
        </telerik:RadGrid>
    </fieldset>

<%# DataBinder.Eval(Container, "Text")%>

<br />

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" RenderMode="Lightweight">
    <Windows>
        <telerik:RadWindow ID="UserListDialog" runat="server" Height="700px" Left="50px" Modal="true" ReloadOnShow="true" RenderMode="Lightweight" ShowContentDuringLoad="false" Title="View Details and Compare Commitments" Width="950px">
        </telerik:RadWindow>
        <telerik:RadWindow ID="InputEditDialog" runat="server" Height="700px" Left="50px" Modal="true" ReloadOnShow="true" RenderMode="Lightweight" ShowContentDuringLoad="false" Title="Insert/Edit Commitment" Width="950em">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
</telerik:RadAjaxLoadingPanel>