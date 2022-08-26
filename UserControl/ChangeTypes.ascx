<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangeTypes.ascx.cs" Inherits="UserControl_ChangeTypes" %>

<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Change Type:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtChangeType" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />
        </td>
    </tr>
</table>

<asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>

<asp:GridView ID="ChangeTypeGridView" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
	<ItemStyle Width="20px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Change Types">
            <ItemTemplate>
                <asp:Label ID="labelChangeType" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>