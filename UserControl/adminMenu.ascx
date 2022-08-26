<%@ Control Language="C#" AutoEventWireup="true" CodeFile="adminMenu.ascx.cs" Inherits="UserControl_adminMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadMenu RenderMode="Lightweight" ID="RadMenu1" CssClass="mainMenu" Skin="Office2010Silver" runat="server" DataSourceID="SMapDS" ShowToggleHandle="true">
</telerik:RadMenu>
<br />
<asp:SiteMapDataSource ID="SMapDS" runat="server" ShowStartingNode="false" />