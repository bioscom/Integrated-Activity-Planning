<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OU.ascx.cs" Inherits="UserControl_OU" %>

<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Organisation Unit:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOU" runat="server" Width="200px"></asp:TextBox>
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
<asp:GridView ID="OUGridView" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
	<ItemStyle Width="20px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Organisational Unit">
            <ItemTemplate>
                <asp:Label ID="labelOU" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "OU") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>