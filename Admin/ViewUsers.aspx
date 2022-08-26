<%@ Page Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="ViewUsers.aspx.cs" Inherits="Admin_ViewUsers" Title="IAP Change Request" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadWindowManager RenderMode="Lightweight" ID="mnuRadWindowManager" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow RenderMode="Lightweight" ID="AddUser" runat="server" ShowContentDuringLoad="false" Width="400px"
                Height="400px" Title="Telerik RadWindow" Behaviors="Default">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function ShowEditForm(id, rowIndex) {
                var grid = $find("<%= grdView.ClientID %>");

                var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
                grid.get_masterTableView().selectItem(rowControl, true);

                window.radopen("EditUser.aspx?UserID=" + id, "InputEditDialog");
                return false;
            }


            function ShowInsertForm() {
                window.radopen("Users.aspx", "InputEditDialog");
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

            function RowDblClick(sender, eventArgs) {
                window.radopen("ViewDetails.aspx.aspx?lCommitmentId=" + eventArgs.getDataKeyValue("lCommitmentId"), "UserListDialog");
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("btnDocument") >= 0)
                    args.set_enableAjax(false);
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
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

    <script language="javascript" type="text/javascript">

        (function (global, undefined) {

            function openRadWin() {
                var wndw = radopen("Users.aspx", "AddUser", 500, 280);
                wndw.Center();
            }

            global.openRadWin = openRadWin;

        })(window);

    </script>

    <h3>IAP Users Management</h3>
    <div style="color: Black; border: solid 1px Silver; width: 100%">
        <table style="width: 100%">
            <tr>
                <td>
                    <div>
                        <div style="float: left">
                            <telerik:RadButton RenderMode="Lightweight" runat="server" AutoPostBack="false" OnClientClicked="openRadWin" Text="Add New User" />
                        </div>
                        <div style="float: right">
                            <telerik:RadComboBox RenderMode="Lightweight" ID="ddlUsers" runat="server" Height="200" Width="430px" Font-Size="10pt"
                                DropDownWidth="500" EmptyMessage="Search for user..." HighlightTemplatedItems="true" AutoPostBack="true"
                                EnableLoadOnDemand="true" Filter="Contains" OnItemsRequested="ddlUsers_ItemsRequested"
                                Skin="Office2010Silver" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">

                                <HeaderTemplate>
                                    <table style="width: 475px; font-size: 9pt" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 165px;">Name</td>
                                            <td style="width: 310px;">Email Address</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <table style="width: 475px; font-size: 9pt" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 165px;"><%# DataBinder.Eval(Container, "Text")%></td>
                                            <td style="width: 310px;"><%# DataBinder.Eval(Container, "Attributes['USERMAIL']")%></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid RenderMode="Lightweight" ID="grdView" AllowSorting="true" runat="server" PageSize="25" AllowAutomaticInserts="true"
                        AllowAutomaticUpdates="true" AllowAutomaticDeletes="true" AllowPaging="True" Font-Size="11px"
                        OnItemCreated="grdView_ItemCreated" OnPreRender="grdView_PreRender"
                        OnItemCommand="grdView_ItemCommand" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource" Width="100%">
                        <AlternatingItemStyle BackColor="#FFFF99" />
                        <ItemStyle BackColor="#FFFFFF" />

                        <PagerStyle PageSizes="5,10,25,50,100" PagerTextFormat="{4}<strong>{5}</strong> users matching your search criteria" PageSizeLabelText="User per page:" />

                        <MasterTableView Width="100%" CommandItemDisplay="None" DataKeyNames="IDUSER" AutoGenerateColumns="false" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">

                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S/No">
                                    <ItemTemplate>
                                        <asp:Label ID="numberLabel" runat="server" Width="20px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="editLink" runat="server" Text="Edit"></asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn SortExpression="FULLNAME" HeaderText="Full Name" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="ROLEID" HeaderText="User Role" HeaderButtonType="TextButton" DataField="ROLEID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="WINDOW_TYPE" HeaderText="IAP Type" HeaderButtonType="TextButton" DataField="WINDOW_TYPE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="FUNCTION" HeaderText="Function" HeaderButtonType="TextButton" DataField="FUNCTION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="USERMAIL" HeaderText="Email Address" HeaderButtonType="TextButton" DataField="USERMAIL"></telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="DeleteLink" runat="server" OnClick="btnDelete_Click">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>

                            <CommandItemTemplate>
                                <a href="#" onclick="return ShowInsertForm();">Add New User</a>
                            </CommandItemTemplate>

                        </MasterTableView>

                        <ClientSettings>
                            <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
                        </ClientSettings>

                    </telerik:RadGrid>
                </td>
            </tr>
        </table>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
            <Windows>

                <telerik:RadWindow RenderMode="Lightweight" ID="InputEditDialog" runat="server" Title="Update User's profile" Height="280px" Width="500px"
                    Left="50px" ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                </telerik:RadWindow>

            </Windows>
        </telerik:RadWindowManager>

        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
        </telerik:RadAjaxLoadingPanel>
    </div>
</asp:Content>
