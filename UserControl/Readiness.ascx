<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Readiness.ascx.cs" Inherits="UserControl_Readiness" %>

<table class="tSubGray">
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Plan Execution Criteria:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPEC" ErrorMessage="Plan Execution Criteria is required" ValidationGroup="superintendent">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtPEC" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click"
                Text="Submit" ValidationGroup="superintendent" />
            &nbsp;<asp:Button ID="resetButton" runat="server" OnClick="resetButton_Click"
                Text="Reset" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:HiddenField ID="idPECHF" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="grdView" runat="server"
                AutoGenerateColumns="False"
                OnPageIndexChanging="grdView_PageIndexChanging"
                OnRowCommand="grdView_RowCommand" Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="editLinkButton" runat="server"
                                CommandArgument="<%# Container.DisplayIndex %>" CommandName="editThis"
                                IDPEC='<%# DataBinder.Eval(Container.DataItem, "IDPEC") %>'>Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plan Execution Criteria">
                        <ItemTemplate>
                            <asp:Label ID="labelPEC" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "PEC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="superintendent" />

