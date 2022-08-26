<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oRequestApproval.ascx.cs" Inherits="UserControl_oRequestApproval" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%@ Register Src="~/UserControl/RequestInformation.ascx" TagName="RequestInformation" TagPrefix="uc1" %>


<style type="text/css">
    .auto-style1 {
        width: 100px;
    }
</style>


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

    function CancelEdit() {
        GetRadWindow().close();
    }
</script>


<fieldset>
    <legend>
        <b style="color: green">Approvers</b>
    </legend>
    <table>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="Label7" runat="server" Text="Is Finance Support required?:" Font-Bold="True" ForeColor="Green"></asp:Label>

            </td>
            <td>
                <telerik:RadRadioButtonList ID="rdbLstFinanceYN" runat="server" Direction="Horizontal" OnSelectedIndexChanged="rdbLstFinanceYN_SelectedIndexChanged" ValidationGroup="Finance"></telerik:RadRadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlFinanceYN" runat="server">
                    <table>
                        <tr>
                            <td>

                                <asp:Label ID="lblFinance" runat="server" Text="Finance Support:"></asp:Label>

                            </td>
                            <td>

                                <asp:DropDownList ID="ddlFinance" runat="server" Width="300px">
                                    <asp:ListItem Value="-1">Finance Support...</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Production Asset Representative:"></asp:Label>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPar" runat="server" Width="300px">
                                    <asp:ListItem Value="-1">Production Asset Authouriser...</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Change Review Board Chair:"></asp:Label>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCrb" runat="server" Width="300px">
                                    <asp:ListItem Value="-1">Change Review Board Chair...</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</fieldset>
<br />
<br />
<fieldset>
    <legend>
        <b style="color: green">IAP Planner Support</b>
    </legend>
    <table>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label4" runat="server" Text="Stand:"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator3" runat="server"
                                ControlToValidate="ddlPlannerStand"
                                ErrorMessage="Your approval stand is required" Operator="NotEqual"
                                Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlannerStand" runat="server" Width="300px">
                                <asp:ListItem Value="-1">Please select your stand...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Change Type:"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator4" runat="server"
                                ControlToValidate="ddlChangeType"
                                ErrorMessage="Change type is required" Operator="NotEqual"
                                Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChangeType" runat="server" Width="300px">
                                <asp:ListItem Value="-1">Please Change Type...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Comment:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtComment" Height="100px" TextMode="MultiLine" Width="500px"></telerik:RadTextBox>
                            <%--<asp:TextBox ID="txtComments" runat="server" Height="100px" TextMode="MultiLine" Width="500px"></asp:TextBox>--%>
                               
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>

                            <asp:Button ID="forwardButton" runat="server" Text="Submit"
                                OnClick="forwardButton_Click" Width="100px" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</fieldset>
<br />
<br />
<uc1:RequestInformation ID="RequestInformation1" runat="server" />

<asp:ValidationSummary ID="ValidationSummary1" runat="server"
    ShowMessageBox="True" ShowSummary="False" />
