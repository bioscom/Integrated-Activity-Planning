<%@ Control Language="C#" AutoEventWireup="true" CodeFile="statusSelectorControl.ascx.cs" Inherits="UserControl_statusSelectorControl" %>
<%--<asp:DropDownList ID="statusDrp" runat="server" Width="120px">
    <asp:ListItem Value="-1">Status</asp:ListItem>
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="2">In Progress</asp:ListItem>
    <asp:ListItem Value="3">Not Done</asp:ListItem>
    <asp:ListItem Value="4">Not Applicable</asp:ListItem>
</asp:DropDownList>--%>
<%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="statusDrp" ErrorMessage="Please Select Status" Operator="NotEqual" Type="Integer" ValidationGroup="PEC" ValueToCompare="-1">*</asp:CompareValidator>--%>

<asp:RadioButtonList ID="statusRdb" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="2">In Progress</asp:ListItem>
    <asp:ListItem Value="3">Not Done</asp:ListItem>
    <asp:ListItem Value="4">Not Applicable</asp:ListItem>
</asp:RadioButtonList>