<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IAPApproverRouter.aspx.cs" Inherits="IAPApproverRouter" %>

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
        <br />
        <fieldset>
            <legend><b style="color: green">Reroute approval to another user</b></legend>

            <asp:GridView ID="grdView" runat="server" Width="100%" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Approvers">
                        <ItemTemplate>
                            <asp:Label ID="lblApprover" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'
                                IDUSER='<%# DataBinder.Eval(Container.DataItem, "IDUSER") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:Label ID="lbRole" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "ROLEID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Function">
                        <ItemTemplate>
                            <asp:Label ID="labelFunction" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "FUNCTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stand">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Received">
                        <ItemTemplate>
                            <asp:Label ID="labelDate" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_RECEIVED") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Approved">
                        <ItemTemplate>
                            <asp:Label ID="labelApprover" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "COMMENTSDATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reroute">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlRole" Width="200px" runat="server" ROLEID='<%# DataBinder.Eval(Container.DataItem, "ROLEID") %>'></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <div style="float: right; margin-top: 0.5em">
                <asp:Button ID="btnForward" runat="server" Text="Re Route" OnClick="btnForward_Click" />
            </div>
        </fieldset>
        <br />
        <uc1:RequestInformation ID="RequestInformation1" runat="server" />
    </form>
</body>
</html>
