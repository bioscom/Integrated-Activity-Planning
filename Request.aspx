<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Request" %>
<%@ Register Src="UserControl/ChangeRequestForm.ascx" TagName="ChangeRequestForm" TagPrefix="uc1" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JavaScript/iap.js"></script>
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release"></ajaxToolkit:ToolkitScriptManager>--%>
    <br />
    <h3 style="border-top:solid 20px gold; margin-top:0; color:black">SCiN Integrated Activity Planning Change Request Form</h3>
    <hr />
    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="Warning"></asp:Label>
    <uc1:ChangeRequestForm ID="ChangeRequestForm1" runat="server" />
    <br />
</asp:Content>

