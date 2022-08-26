<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImpactAnalysisWindow.ascx.cs" Inherits="UserControl_ImpactAnalysisWindow" %>

<table class="tMainBorder" style="width:100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Window Type:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWinType" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Description:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWinTypeDesc" runat="server" Width="350px" Height="50px" 
                TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Type:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtType" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Max Range:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtRange" runat="server" Width="200px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" 
                Width="100px" />
        </td>
    </tr>
</table>

<asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>

<asp:GridView ID="impactAnalWinGridView" runat="server" Width="100%" 
    AllowPaging="True" AutoGenerateColumns="False"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="IAP Window">
            <ItemTemplate>
                <asp:Label ID="labelWinType" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "WINDOW_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="labelDescription" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "WINDESC") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Window Type">
            <ItemTemplate>
                <asp:Label ID="labelType" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "XTYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Max. Range">
            <ItemTemplate>
                <asp:Label ID="labelRange" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "XRANGE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>