<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Areas.ascx.cs" Inherits="UserControl_Areas" %>


<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Area:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtArea" runat="server" Width="200px"></asp:TextBox>
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

<br />

<asp:GridView ID="AreaGridView" runat="server" Width="100%" 
    AutoGenerateColumns="False" 
    onpageindexchanging="AreaGridView_PageIndexChanging"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
	    <ItemStyle Width="20px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Asset Areas">
            <ItemTemplate>
                <asp:Label ID="labelArea" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "AREA") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>