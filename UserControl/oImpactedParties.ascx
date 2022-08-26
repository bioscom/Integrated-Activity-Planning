<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oImpactedParties.ascx.cs" Inherits="UserControl_oImpactedParties" %>
<%@ Register Src="~/UserControl/RequestInformation.ascx" TagName="RequestInformation" TagPrefix="uc1" %>
<div>
    <table style="width: 100%">
        <tr>
            <td>
                <h4 style="color: red; font: bold">Note: Select the Impacted parties on whose activity change impacts, then click Forward to Supporting Parties button.</h4>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <b style="color: green">Impacted/Supporting Parties</b>
                    </legend>
                    <table style="width: 100%; height: 300px">
                        <tr>
                            <td>
                                <div style="height: 300px; width: 100%; overflow: auto">
                                    <asp:TreeView ID="mnuTreeView" runat="server" ExpandDepth="1" ImageSet="Arrows" OnSelectedNodeChanged="mnuTreeView_SelectedNodeChanged" ParentNodeStyle-ForeColor="Green" ParentNodeStyle-Font-Bold="true" ShowCheckBoxes="All" ShowLines="True">
                                        <ParentNodeStyle Font-Bold="True" ForeColor="Green" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <%-- <fieldset>
                    <legend>
                        <b style="color: green">Other Functions Impacted/Supporting Parties</b>
                    </legend>
                    <table style="width: 99%; height: 300px; background-color:silver">
                        <tr>
                            <td>
                                <div style="height: 300px; width: 100%; overflow: auto">
                                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="Arrows" OnSelectedNodeChanged="mnuTreeView_SelectedNodeChanged" ParentNodeStyle-ForeColor="Green" ParentNodeStyle-Font-Bold="true" ShowCheckBoxes="All" ShowLines="True">
                                        <ParentNodeStyle Font-Bold="True" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Forward to Supporting Parties" Width="220px" />
            </td>
        </tr>
    </table>
</div>
<br />
<fieldset>
    <legend><b style="color: green">Change Request Details</b> </legend>
    <uc1:RequestInformation ID="RequestInformation1" runat="server" />
</fieldset>
