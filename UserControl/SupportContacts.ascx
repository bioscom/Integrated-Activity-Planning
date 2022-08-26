<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupportContacts.ascx.cs" Inherits="UserControl_SupportContacts" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="height:500px">
    <%--<div style="overflow:scroll">--%>
        <telerik:RadGrid RenderMode="Lightweight" ID="grdView" AllowSorting="true" runat="server" PageSize="15" AllowAutomaticInserts="true"
            AllowAutomaticUpdates="true" AllowAutomaticDeletes="true" AllowPaging="False" Font-Size="11px"
            OnItemCreated="grdView_ItemCreated" OnPreRender="grdView_PreRender"
            OnItemCommand="grdView_ItemCommand" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource" Width="100%">
            <AlternatingItemStyle BackColor="#FFFF99" />
            <ItemStyle BackColor="#FFFFFF" />

            <PagerStyle PageSizes="5,15,25,50,100" PagerTextFormat="{4}<strong>{5}</strong> users" PageSizeLabelText="User per page:" />

            <MasterTableView Width="100%" CommandItemDisplay="None" DataKeyNames="IDUSER, CURRENT_ROLE_HOLDER" AutoGenerateColumns="false" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S/No">
                        <ItemTemplate>
                            <asp:Label ID="numberLabel" runat="server" Width="20px" />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                        <ItemTemplate>
                            <asp:CheckBox ID="supportContact" runat="server"
                                USERID='<%# DataBinder.Eval(Container.DataItem, "IDUSER") %>'
                                CURRENT_ROLE_HOLDER='<%# DataBinder.Eval(Container.DataItem, "CURRENT_ROLE_HOLDER") %>'></asp:CheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn SortExpression="FULLNAME" HeaderText="Full Name" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="ROLEID" HeaderText="User Role" HeaderButtonType="TextButton" DataField="ROLEID"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="WINDOW_TYPE" HeaderText="IAP Type" HeaderButtonType="TextButton" DataField="WINDOW_TYPE"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="FUNCTION" HeaderText="Function" HeaderButtonType="TextButton" DataField="FUNCTION"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    <%--</div>--%>
</div>
<div style="margin-top: 3px; margin-bottom:0px">
    <asp:Button ID="submitBtn" runat="server" OnClick="submitBtn_Click" Text="Update" Width="100px" />
</div>