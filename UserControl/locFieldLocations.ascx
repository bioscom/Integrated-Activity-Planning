<%@ Control Language="C#" AutoEventWireup="true" CodeFile="locFieldLocations.ascx.cs" Inherits="UserControl_fieldLocations" %>

<%@ Reference VirtualPath="~/UserControl/statusSelectorControl.ascx" %>

<%@ Register Src="statusSelectorControl.ascx" TagName="statusSelectorControl" TagPrefix="uc4" %>
<table style="width: 99%" class="tSubGray">
    <tr>
        <td style="width: 31%; background-color: #FF0000;">
            <asp:Label ID="lbMT" runat="server" Font-Bold="true"></asp:Label>
        </td>
        <%--<td style="width: 31%; background-color: #66CCFF;">
            <asp:Label ID="lbST" runat="server" Font-Bold="true"></asp:Label>
        </td>
        <td style="background-color: #008000">
            <asp:Label ID="lbVST" runat="server" Font-Bold="true"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" Width="100%">
                <RowStyle BackColor="#FFFFFF" />
                <AlternatingRowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Execution Preparation">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldLocation" runat="server"
                                IDPEC='<%# DataBinder.Eval(Container.DataItem, "IDPEC") %>'
                                Text='<%# DataBinder.Eval(Container.DataItem, "PEC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <uc4:statusSelectorControl ID="statusSelectorControl1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
        <%--<td>
            <asp:GridView ID="grdViewOffShoreLocations" runat="server" AutoGenerateColumns="False" Width="100%">
                <RowStyle BackColor="#FFFFFF" />
                <AlternatingRowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Execution Preparation">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldLocation" runat="server"
                                FIELD_LOC='<%# DataBinder.Eval(Container.DataItem, "ID_FIELD_LOC") %>'
                                Text='<%# DataBinder.Eval(Container.DataItem, "FIELD_LOCATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <uc4:statusSelectorControl ID="statusSelectorControl1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </td>
        <td>
            <asp:GridView ID="grdViewSwampLocations" runat="server" AutoGenerateColumns="False" Width="100%">
                <RowStyle BackColor="#FFFFFF" />
                <AlternatingRowStyle BackColor="#FFFFFF" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Execution Preparation">
                        <ItemTemplate>
                            <asp:Label ID="lblFieldLocation" runat="server"
                                FIELD_LOC='<%# DataBinder.Eval(Container.DataItem, "ID_FIELD_LOC") %>'
                                Text='<%# DataBinder.Eval(Container.DataItem, "FIELD_LOCATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <uc4:statusSelectorControl ID="statusSelectorControl1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </td>--%>
    </tr>
    <tr>
        <td style="text-align: right" colspan="3">
            <hr />
        </td>
    </tr>
    <tr>
        <td style="text-align: right" colspan="3">
            <asp:HiddenField ID="RequestIDHF" runat="server" />
            <asp:Button ID="btnSave" runat="server" Text="Submit" Width="100px" OnClick="btnSave_Click" ValidationGroup="PEC" />
            <asp:Button ID="updateButton" runat="server" OnClick="updateButton_Click" Text="Update" Width="100px" ValidationGroup="PEC" />
        </td>
    </tr>
</table>



<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="PEC" />




