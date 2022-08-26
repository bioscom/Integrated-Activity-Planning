<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanTypes.ascx.cs" Inherits="UserControl_PlanTypes" %>


<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Plan Type:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPlanType" runat="server" Width="200px"></asp:TextBox>
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


<asp:GridView ID="planTypeGridView" runat="server" Width="100%" 
     AutoGenerateColumns="False"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
	<ItemStyle Width="20px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Plan Type">
            <ItemTemplate>
                <asp:Label ID="labelPlanType" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "PLANTYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>