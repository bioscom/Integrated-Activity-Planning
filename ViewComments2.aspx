<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewComments2.aspx.cs" Inherits="ViewComments2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="~/UserControl/RequestInformation.ascx" TagName="RequestInformation" TagPrefix="uc4" %>
<%@ Register Src="~/UserControl/oSupportApproversComment.ascx" TagName="oSupportApproversComment" TagPrefix="uc5" %>
<%@ Register Src="~/UserControl/personnelInformationList.ascx" TagPrefix="uc1" TagName="personnelInformationList" %>
<%@ Register Src="~/UserControl/locFieldLocations.ascx" TagPrefix="uc1" TagName="locFieldLocations" %>

<%@ Register Src="UserControl/oImpactedPartiesComments.ascx" TagName="oImpactedPartiesComments" TagPrefix="uc6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/Styles.css" type="text/css" rel="stylesheet" media="screen" />
    <link href="../CSS/navigationStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 850px">
            <telerik:RadScriptManager ID="Radscriptmanager1" runat="server"></telerik:RadScriptManager>
            <br />
            <uc6:oImpactedPartiesComments ID="oImpactedPartiesComments1" runat="server" />
            <br />
            <br />
            <fieldset>
                <legend><b style="color: green">Approval Workflow</b></legend>

                <div runat="server" style="color: Black; float: left" id="DivSpdc">
                    <br />
                    <uc5:oSupportApproversComment ID="IAPPlannerCommentSpdc" runat="server" />
                    <br />
                    <fieldset>
                        <legend><b style="color: green">Personnel Information</b></legend>
                        <uc1:personnelInformationList runat="server" ID="personnelInformationListSpdc" />
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend><b style="color: green">Plan Execution Criteria</b></legend>
                        <uc1:locFieldLocations runat="server" ID="locFieldLocationsSpdc" />
                    </fieldset>
                </div>
            <%--</fieldset>

            <fieldset>
                <legend><b style="color: green">SNEPCo Approval Workflow</b></legend>--%>

                <div runat="server" style="color: Black; float: left" id="DivSnepco">
                    <br />
                    <uc5:oSupportApproversComment ID="IAPPlannerComment1" runat="server" />
                    <br />
                    <fieldset>
                        <legend><b style="color: green">Personnel Information</b></legend>
                        <uc1:personnelInformationList runat="server" ID="personnelInformationList" />
                    </fieldset>
                    <br />
                    <br />
                    <fieldset>
                        <legend><b style="color: green">Plan Execution Criteria</b></legend>
                        <uc1:locFieldLocations runat="server" ID="locFieldLocations" />
                    </fieldset>
                </div>
                
                <br /><br />
                <uc4:RequestInformation ID="RequestInformation1" runat="server" />
            </fieldset>
        </div>

        <%--<telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="RadPanelBar1" Height="430px" Width="100%" ExpandMode="FullExpandedItem">
            <Items>
                <telerik:RadPanelItem Text="Mail" ImageUrl="images/mail.gif" Expanded="True">
                    <Items>

                    </Items>
                </telerik:RadPanelItem>

                <telerik:RadPanelItem Text="Calendar" ImageUrl="images/calendar.gif">
                    <Items>
                        <telerik:RadPanelItem>
                            <ItemTemplate>


                            </ItemTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>

                <telerik:RadPanelItem Text="Folders List" ImageUrl="images/folderList.gif">
                    <Items>
                        
                    </Items>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>--%>
    </form>
</body>
</html>
