<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Add New User" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="../UserControl/Search4User.ascx" TagName="Search4User" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="color: Black">
            <fieldset>
                <legend><b style="color: green">Add User</b></legend>
                <table>
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Search User:"></asp:Label>
                        </td>
                        <td>
                            <uc1:Search4User ID="Search4User1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Function:"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server"
                                ControlToValidate="functionDropDownList" ErrorMessage="Function is required"
                                Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="functionDropDownList" runat="server" Width="200px">
                                <asp:ListItem Value="-1">Select Function...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="User Role:"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator2" runat="server"
                                ControlToValidate="userroleList" ErrorMessage="User Role is required"
                                Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="userroleList" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="userroleList_SelectedIndexChanged" Width="200px">
                                <asp:ListItem Value="-1">Select role...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" CssClass="label"
                                Text="IAP Planner (MT, ST):"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="iapPlannerTypeDdl" runat="server" Width="200px">
                                <asp:ListItem Value="-1">Select Term...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Phone No.:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
                            &nbsp;<asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                                Text="Close" ValidationGroup="xxxx" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
