<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangeRequestForm.ascx.cs" Inherits="UserControl_ChangeRequestForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="Search4User.ascx" TagName="search4user" TagPrefix="uc3" %>

<%@ Register Src="personnelInformationList.ascx" TagName="personnelInformationList" TagPrefix="uc1" %>
<%@ Register Src="Locations.ascx" TagName="Locations" TagPrefix="uc4" %>
<%@ Register Src="locFieldLocations.ascx" TagName="locFieldLocations" TagPrefix="uc5" %>

<script type="text/javascript">
    function SetMinDate(sender, eventArgs) {
        var pickUp = $find('<%= dtDateOriginalFrom.ClientID %>');
        var dropOff = $find('<%= dtDateOriginalTo.ClientID %>');

        var date = pickUp.get_selectedDate();
        if (date != null) {
            dropOff.set_minDate(date);

            if (dropOff.get_selectedDate() == null) {
                dropOff.set_selectedDate(date);
            }
        }

        var pickUp1 = $find("<%= dtDateScheduleFrom.ClientID %>");
        var dropOff1 = $find("<%= dtDateScheduleTo.ClientID %>");

        var date1 = pickUp1.get_selectedDate();
        if (date1 != null) {
            dropOff1.set_minDate(date);

            if (dropOff1.get_selectedDate() == null) {
                dropOff1.set_selectedDate(date1);
            }
        }
    }

    function LogOff() {
        top.location.href = "MyChangeRequests.aspx";
    }
</script>

<asp:MultiView ID="FormMultiView" runat="server" ActiveViewIndex="0">
    <asp:View ID="MainFormTab" runat="server">
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" CssClass="Warning" Text="Request Number:"></asp:Label>
                    &nbsp;
                    <asp:Label ID="requestNumberLabel" runat="server" CssClass="Warning"></asp:Label>
                </td>
            </tr>

            <%--<tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>--%>

            <tr>
                <td>

                    <fieldset>
                        <legend>
                            <b style="color: green">Original Schedule</b>
                        </legend>

                        <table>
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="dtDateOriginalFrom" runat="server" DateInput-DisplayDateFormat="dd-MMM-yyyy" DateInput-Label="Start Date:" DateInput-LabelWidth="100px" RenderMode="Lightweight" Width="250px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dtDateOriginalTo" runat="server" DateInput-DisplayDateFormat="dd-MMM-yyyy" DateInput-Label="End Date:" DateInput-LabelWidth="100px" RenderMode="Lightweight" Width="250px">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
                <td>
                    <fieldset>
                        <legend>
                            <b style="color: green">Requested Schedule</b>
                        </legend>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="dtDateScheduleFrom" runat="server" DateInput-DisplayDateFormat="dd-MMM-yyyy" DateInput-Label="Start Date:" DateInput-LabelWidth="100px" RenderMode="Lightweight" Width="250px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dtDateScheduleTo" runat="server" DateInput-DisplayDateFormat="dd-MMM-yyyy" DateInput-Label="End Date:" DateInput-LabelWidth="100px" RenderMode="Lightweight" Width="250px">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top">
                    <table style="width: 100%">
                        <tr>
                            <td style="vertical-align: top">
                                <fieldset>
                                    <legend><b style="color: green">Change Criteria</b> </legend>
                                    <table style="width: 100%; height: 243px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label47" runat="server" Text="Organisation Unit:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOU" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOU_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="-1">Select Organisation Unit</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label48" runat="server" Text="Area:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="areaList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="areaList_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label49" runat="server" Text="Location:"></asp:Label>
                                                <asp:CompareValidator ID="changeTypeCompareValidator2" runat="server" ControlToValidate="ddlLocation" ErrorMessage="Location is required" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label52" runat="server" Text="Plan Type:"></asp:Label>
                                                <asp:CompareValidator ID="changeTypeCompareValidator3" runat="server" ControlToValidate="ddlPlanType" ErrorMessage="Plan Type is required" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanType" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label53" runat="server" Text="Change Type:"></asp:Label>
                                                <asp:CompareValidator ID="changeTypeCompareValidator4" runat="server" ControlToValidate="changeTypeList" ErrorMessage="Change Type is required" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="changeTypeList" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFunctionalAuthoriser" runat="server" Text="Functional Authoriser:"></asp:Label>
                                                <asp:CompareValidator ID="FunctionalAuthoriserCompareValidator" runat="server" ControlToValidate="ddlFunctionalAuthoriser" ErrorMessage="Please select Functional Authoriser" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFunctionalAuthoriser" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Select Authoriser</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label50" runat="server" Text="Asset IAP Planner:"></asp:Label>
                                                <asp:CompareValidator ID="IAPPlannerCompareValidator" runat="server" ControlToValidate="ddlPlanner" ErrorMessage="IAP Planner is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPlanner" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlanner_SelectedIndexChanged" Width="200px">
                                                    <asp:ListItem Value="-1">Select Asset IAP Planner</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <asp:Label ID="Label91" runat="server" Text="Finance Authority:"></asp:Label>
                                                <asp:CompareValidator ID="IAPPlannerCompareValidator0" runat="server" ControlToValidate="ddlFinance" ErrorMessage="Finance Planner is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFinance" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Select Finance Authority</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label51" runat="server" Text="Reference Plan:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRefPlan" runat="server" Width="200px">
                                                    <asp:ListItem Value="-1">Please Select Reference Plan</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label54" runat="server" Text="WO NO:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWONO" runat="server" Width="196px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label106" runat="server" Text="Primaver Activity ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPrimaveraId" runat="server" Width="196px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset> </td>
                            <td style="vertical-align: top">
                                <fieldset>
                                    <legend><b style="color: green">Impacted/Supporting Parties</b> </legend><%--<table style="width: 100%; height: 250px">
                                        <tr>
                                            <td>--%>
                                    <div style="height: 243px; width: 100%; overflow: auto">
                                        <asp:TreeView ID="mnuTreeView" runat="server" ExpandDepth="1" ImageSet="Arrows" ParentNodeStyle-Font-Bold="true" 
                                            ParentNodeStyle-ForeColor="Green" ShowCheckBoxes="Leaf" ShowLines="True">
                                            <ParentNodeStyle Font-Bold="True" ForeColor="Green" />
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                        </asp:TreeView>
                                    </div>
                                    <%--</td>
                                        </tr>
                                    </table>--%>
                                </fieldset> 
                                <br />
                            </td>
                            <td style="vertical-align: top">
                                <fieldset>
                                    <legend><b style="color: green">Gain Plan</b> </legend>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label92" runat="server" Text="Oil Gain:"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlanVOL" ErrorMessage="Oil Gain is required">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPlanVOL" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                    <NegativeStyle Resize="None" />
                                                    <EmptyMessageStyle Resize="None" />
                                                    <ReadOnlyStyle Resize="None" />
                                                    <FocusedStyle Resize="None" />
                                                    <DisabledStyle Resize="None" />
                                                    <InvalidStyle Resize="None" />
                                                    <HoveredStyle Resize="None" />
                                                    <EnabledStyle Resize="None" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="Label93" runat="server" Text="(Mbopd)"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label94" runat="server" Text="Water Injection Gain:"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGH20" ErrorMessage="Water Injection Gain is required">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtGH20" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                    <NegativeStyle Resize="None" />
                                                    <EmptyMessageStyle Resize="None" />
                                                    <ReadOnlyStyle Resize="None" />
                                                    <FocusedStyle Resize="None" />
                                                    <DisabledStyle Resize="None" />
                                                    <InvalidStyle Resize="None" />
                                                    <HoveredStyle Resize="None" />
                                                    <EnabledStyle Resize="None" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="Label95" runat="server" Text="(Mbwpd)"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label96" runat="server" Text="Gas Gain:"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPlanVOLgas" ErrorMessage="Gas Gain is required">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPlanVOLgas" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                    <NegativeStyle Resize="None" />
                                                    <EmptyMessageStyle Resize="None" />
                                                    <ReadOnlyStyle Resize="None" />
                                                    <FocusedStyle Resize="None" />
                                                    <DisabledStyle Resize="None" />
                                                    <InvalidStyle Resize="None" />
                                                    <HoveredStyle Resize="None" />
                                                    <EnabledStyle Resize="None" />
                                                </telerik:RadNumericTextBox>
                                                <asp:Label ID="Label97" runat="server" Text="(MMscfpd)"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label98" runat="server" Text="Savings(F$):"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPlanCost" ErrorMessage="Savings(F$) is required">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtPlanCost" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                    <NegativeStyle Resize="None" />
                                                    <EmptyMessageStyle Resize="None" />
                                                    <ReadOnlyStyle Resize="None" />
                                                    <FocusedStyle Resize="None" />
                                                    <DisabledStyle Resize="None" />
                                                    <InvalidStyle Resize="None" />
                                                    <HoveredStyle Resize="None" />
                                                    <EnabledStyle Resize="None" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset>
                                                    <legend><b style="color: green">Loss Plan</b> </legend>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label99" runat="server" Text="Oil Loss:"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNewVOL" ErrorMessage="Oil Loss is required">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNewVOL" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                                    <NegativeStyle Resize="None" />
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </telerik:RadNumericTextBox>
                                                                <asp:Label ID="Label100" runat="server" Text="(Mbopd)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label101" runat="server" Text="Water Injection Loss:"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLH20" ErrorMessage="Water Injection Loss is required">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtLH20" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                                    <NegativeStyle Resize="None" />
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </telerik:RadNumericTextBox>
                                                                <asp:Label ID="Label102" runat="server" Text="(Mbwpd)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label103" runat="server" Text="Gas Loss:"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNewVOLgas" ErrorMessage="Gas Loss is required">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNewVOLgas" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                                    <NegativeStyle Resize="None" />
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </telerik:RadNumericTextBox>
                                                                <asp:Label ID="Label104" runat="server" Text="(MMscfpd)"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label105" runat="server" Text="Extra Cost(F$):"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNewCost" ErrorMessage="Extra Cost(F$) is required">*</asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtNewCost" runat="server" DbValueFactor="1" Font-Size="12px" Height="22px" NumberFormat-DecimalDigits="2" Type="Number" Width="100px">
                                                                    <NegativeStyle Resize="None" />
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset> </td>
                                        </tr>
                                    </table>
                                </fieldset> </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset>
                        <legend><b style="color: green">Other Details</b> </legend>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label87" runat="server" Text="Activity:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtProjectActivity" runat="server" Height="100px" RenderMode="Lightweight" TextMode="MultiLine" Width="400px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label88" runat="server" Text="Benefit:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtBenefit" runat="server" Height="100px" RenderMode="Lightweight" TextMode="MultiLine" Width="420px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label89" runat="server" Text="Justification for Change:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtProposal" runat="server" Height="100px" RenderMode="Lightweight" TextMode="MultiLine" Width="400px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label90" runat="server" Text="Risk:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtRisk" runat="server" Height="100px" RenderMode="Lightweight" TextMode="MultiLine" Width="420px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>

        </table>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" />
        <br />
        <div style="width: 100%; text-align: right">
            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="Next  &gt;&gt;" Width="150px" />
            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" Text="Next  &gt;&gt;." Width="150px" />            
        </div>
    </asp:View>
    <asp:View ID="PersonnelInfoTab" runat="server">

        <br />
        <br />
        <uc1:personnelInformationList ID="personnelInformationList1" runat="server" />
        <asp:Label ID="lblInfo" runat="server" CssClass="Warning" Text="Click Add Personnel to add new personnel. Then click next &gt;&gt; to move to the next page to complete your request. Click next &gt;&gt; if personnel not required."></asp:Label>
        <hr />
        <div style="width: 100%">
            <div style="float: left">
                <asp:Button ID="PreviousBtn" runat="server" OnClick="PreviousBtnt_Click" Text="&lt;&lt; Previous" Width="100px" ValidationGroup="xxxx" />
            </div>
            <div style="float: right">
                <asp:Button ID="PersInfoNext" runat="server" OnClick="PersInfoNext_Click" Text="Next  &gt;&gt;" Width="100px" ValidationGroup="xxxx" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="ReadinessTab" runat="server">
        <uc5:locFieldLocations ID="locFieldLocations1" runat="server" />
        <hr />
        <div style="width: 100%">
            <div style="float: left">
                <asp:Button ID="Previous2Btn" runat="server" OnClick="Previous2Btn_Click" Text="&lt;&lt; Previous" ValidationGroup="xxxx" Width="100px" />
            </div>
            <div style="float: right">
                <%--<asp:Button ID="btnFinish" runat="server"  OnClientClicked="LogOff" Text="Finish" ValidationGroup="xxxx" Width="100px" />--%>
                <telerik:RadButton RenderMode="Lightweight" runat="server" AutoPostBack="false" OnClientClicked="LogOff" Text="Finish" Width="100px" />
            </div>
        </div>
    </asp:View>
</asp:MultiView>


