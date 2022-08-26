<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oSupportApproversComment.ascx.cs" Inherits="UserControl_oSupportApproversComment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%--<table style="width:100%" class="tMainBorder">
    <tr>
        <td class="cHeadTile" colspan="4">
            IAP Planner&#39;s Comment</td>
    </tr>
    <tr>
        <td style="width:150px">
            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="IAP Planner:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="iapPlannerLabel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Date Received:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="dateReceivedLabel" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Time Received:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblTimeReceived" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Date Reviewed:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="dateReviewedLabel" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Time Reviewed:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblTimeReviewed" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Comment:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="commentLabel" runat="server"></asp:Label>
        </td>
    </tr>
</table>--%>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</telerik:RadCodeBlock>


<fieldset>
    <legend><b style="color: green">Approvers Comments</b> </legend>
    <br />
    <div id="demo" class="demo-container no-bg" style="color:black; width: 100%">

        <telerik:RadGrid RenderMode="Lightweight" ID="grdView" runat="server" PagerStyle-AlwaysVisible="true" Font-Size="11px" OnItemDataBound="grdView_ItemDataBound" OnNeedDataSource="grdView_NeedDataSource" Width="100%">
            <AlternatingItemStyle BackColor="#FFFF99" />
            <ItemStyle BackColor="#FFFFFF" />
            <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <%--<PagerStyle PageSizes="5,10" PagerTextFormat="{4}<strong>{5}</strong> cars matching your search criteria" PageSizeLabelText="Cars per page:" />--%>

            <MasterTableView Width="100%" DataKeyNames="IDREQUEST" AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="numberLabel" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn ItemStyle-Font-Bold="true" SortExpression="FULLNAME" HeaderText="Full Name" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="ROLEID" HeaderText="Role" HeaderButtonType="TextButton" DataField="ROLEID"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="FUNCTION" HeaderText="Function" HeaderButtonType="TextButton" DataField="FUNCTION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="COMMENTS" HeaderText="Comments" HeaderButtonType="TextButton" DataField="COMMENTS"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="DATE_RECEIVED" HeaderText="Date Received" HeaderButtonType="TextButton" DataField="DATE_RECEIVED" DataFormatString="{0:dd-MMM-yyyy}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="TIMERECEIVED" HeaderText="Time Received" HeaderButtonType="TextButton" DataField="TIMERECEIVED"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="COMMENTSDATE" HeaderText="Date Comments" HeaderButtonType="TextButton" DataField="COMMENTSDATE" DataFormatString="{0:dd-MMM-yyyy}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn SortExpression="TIMECOMMENTS" HeaderText="Time Comments" HeaderButtonType="TextButton" DataField="TIMECOMMENTS"></telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <asp:HiddenField ID="requestIdHF" runat="server" />
</fieldset>
