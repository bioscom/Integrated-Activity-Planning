<%@ Control Language="C#" AutoEventWireup="true" CodeFile="supportContact.ascx.cs" Inherits="UserControl_supportContact" %>

<%--<table style="width:13.5em; margin-top:0.3em" class="tSubGray">--%>

<table style="width: 100%; background-color:white; font-size:11px">
    <tr>
        <td>
            <b style="color:red">Support Contacts:</b>
        </td>
        <td>
            <asp:RadioButtonList ID="supportBlst" runat="server" RepeatDirection="Horizontal" BulletStyle="CustomImage" BulletImageUrl="~/Images/i_winPeople2.gif" BorderStyle="None"></asp:RadioButtonList>
        </td>
    </tr>
</table>
