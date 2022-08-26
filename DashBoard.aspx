<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/UserControl/OffShoreDashBoard.ascx" TagPrefix="uc1" TagName="OffShoreDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>

    <div style="width:99%">
        <div style="float:left">
            <uc1:OffShoreDashBoard runat="server" ID="SPDC" />
        </div>
        <div style="float:left; margin-left:1.5em">
            <uc1:OffShoreDashBoard runat="server" ID="SNEPCO" />
        </div>
    </div>
</asp:Content>

