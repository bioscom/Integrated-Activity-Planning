<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Functions.ascx.cs" Inherits="UserControl_Functions" %>


<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Function:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFunction" runat="server" Width="200px"></asp:TextBox>
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

<p>

<asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>

</p>

<asp:GridView ID="functionGridView" runat="server" Width="100%" AutoGenerateColumns="False" onpageindexchanging="functionGridView_PageIndexChanging"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
	<ItemStyle Width="20px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Functions">
            <ItemTemplate>
                <asp:Label ID="labelFunctions" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "FUNCTION") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>