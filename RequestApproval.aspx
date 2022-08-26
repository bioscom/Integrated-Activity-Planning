<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestApproval.aspx.cs" Inherits="RequestApproval" %>

<%@ Register Src="~/UserControl/RequestInformation.ascx" TagName="RequestInformation" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/Styles.css" type="text/css" rel="stylesheet" media="screen" />
    <link href="../CSS/navigationStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <asp:Label ID="userRoleLabel" ForeColor="Black" runat="server"></asp:Label>
            </h2>
            <ul style="list-style-type: none">
                <li>
                    <asp:Label ID="Label4" ForeColor="Black" runat="server" Text="Stand:"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                        ControlToValidate="ddlStand"
                        ErrorMessage="Your Support/approval stand is required" Operator="NotEqual"
                        Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>

                </li>
                <li>
                    <asp:DropDownList ID="ddlStand" runat="server" Width="400px">
                        <asp:ListItem Value="-1">Please select your stand...</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <br />
                </li>
                <li>
                    <asp:Label ID="Label5" ForeColor="Black" runat="server" Text="Comment:"></asp:Label>
                </li>
                <li>
                    <asp:TextBox ID="txtComment" runat="server" Height="100px" TextMode="MultiLine"
                        Width="400px"></asp:TextBox>
                </li>
                <li><br /></li>
                <li>
                    <asp:Button ID="forwardButton" runat="server" Text="Submit"
                            OnClick="forwardButton_Click" Width="100px" />
                </li>
            </ul>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            <hr />
            <uc1:RequestInformation ID="RequestInformation1" runat="server" />
        </div>
    </form>
</body>
</html>

