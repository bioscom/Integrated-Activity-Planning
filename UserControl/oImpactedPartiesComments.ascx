<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oImpactedPartiesComments.ascx.cs" Inherits="UserControl_oImpactedPartiesComments" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</telerik:RadCodeBlock>


<fieldset>
    <legend><b style="color: green">Impacted/Supporting Parties Comments</b> </legend>

    <div id="demo" class="demo-container no-bg" style="color:black; width: 100%">

        <telerik:RadGrid RenderMode="Lightweight" ID="grdView" runat="server" PagerStyle-AlwaysVisible="true" Font-Size="11px" OnNeedDataSource="grdView_NeedDataSource" Width="100%">
            <AlternatingItemStyle BackColor="#FFFF99" />
            <ItemStyle BackColor="#FFFFFF" />
            <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <%--<PagerStyle PageSizes="5,10" PagerTextFormat="{4}<strong>{5}</strong> cars matching your search criteria" PageSizeLabelText="Cars per page:" />--%>

            <MasterTableView Width="100%" DataKeyNames="IDREQUEST, IDUSER" AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="numberLabel" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="chkBoxColumn">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBox" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn ItemStyle-Font-Bold="true" SortExpression="FULLNAME" HeaderText="Full Name" HeaderButtonType="TextButton" DataField="FULLNAME"></telerik:GridBoundColumn>
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

    <asp:Button ID="btnReminder" runat="server" Text="Forward Reminder Mail" OnClick="btnReminder_Click" />

    <asp:HiddenField ID="requestIdHF" runat="server" />
</fieldset>
