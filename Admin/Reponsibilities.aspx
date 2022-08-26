<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="Reponsibilities.aspx.cs" Inherits="Admin_Reponsibilities" %>

<%@ Register Src="~/UserControl/AdminFunctionParties.ascx" TagPrefix="uc1" TagName="AdminFunctionParties" %>
<%@ Register Src="~/UserControl/AdminProductionParties.ascx" TagPrefix="uc1" TagName="AdminProductionParties" %>



<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="color: Black; width: 100%">
        <h2>Impacted/Supporting Parties</h2>
        <table style="width: 100%">
            <tr>
                <td>
                    <uc1:AdminFunctionParties runat="server" ID="AdminFunctionParties" />
                </td>
                <td>
                    <uc1:AdminProductionParties runat="server" ID="AdminProductionParties" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>