<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Locations.ascx.cs" Inherits="UserControl_Locations" %>
<style type="text/css">


select
{
    font-family: Verdana, Arial, Helvetica, sans-serif;
    font-size: 100%;
}

</style>


<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Area:"></asp:Label>
        </td>
        <td>
                            <asp:DropDownList ID="areaList" runat="server" Width="200px" 
                                onselectedindexchanged="areaList_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Location:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtLocation" runat="server" Width="200px"></asp:TextBox>
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

<asp:GridView ID="LocationGridView" runat="server" Width="100%" 
    AutoGenerateColumns="False" 
    onpageindexchanging="LocationGridView_PageIndexChanging" 
    onrowcommand="LocationGridView_RowCommand"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField>
            <ItemTemplate>
               <asp:LinkButton id="deleteLinkButton" runat="server" Text="Delete" ValidationGroup="xxxx" 
                    CommandArgument="<%# Container.DisplayIndex %>" CommandName="DeleteThis" 
                    Font-Bold="True" Font-Size="Small" 
                    LOCATIONID='<%# DataBinder.Eval(Container.DataItem, "LOCATIONID") %>'>
               </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Area">
            <ItemTemplate>
                <asp:Label ID="labelArea" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "AREA") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Location">
            <ItemTemplate>
                <asp:Label ID="labelLocation" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "LOCATION") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>