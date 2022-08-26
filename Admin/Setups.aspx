<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="Setups.aspx.cs" Inherits="Admin_Setups" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="../UserControl/OU.ascx" TagName="OU" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Areas.ascx" TagName="Areas" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ChangeTypes.ascx" TagName="ChangeTypes" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/Locations.ascx" TagName="Locations" TagPrefix="uc4" %>
<%@ Register Src="../UserControl/PlanTypes.ascx" TagName="PlanTypes" TagPrefix="uc5" %>
<%@ Register Src="../UserControl/Functions.ascx" TagName="Functions" TagPrefix="uc6" %>
<%@ Register Src="../UserControl/ImpactAnalysisWindow.ascx" TagName="ImpactAnalysisWindow" TagPrefix="uc7" %>
<%@ Register Src="../UserControl/SupportContacts.ascx" TagName="SupportContacts" TagPrefix="uc8" %>
<%@ Register Src="../UserControl/Readiness.ascx" TagPrefix="uc1" TagName="Readiness" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <style type="text/css">
        #example .demo-container {
            min-height: 203px;
            max-width: 700px;
            width: 500px;
        }

        .demo-content {
            display: inline-block;
            *display: inline;
            zoom: 1;
        }

        .multiPage {
            display: inline-block;
            *display: inline;
            zoom: 1;
        }

        .demo-container .RadTabStrip.rtsHorizontal.rtsBottom {
            margin-top: -5px;
        }

        .demo-container .RadTabStrip.rtsVertical.rtsLeft {
            margin-right: -10px;
        }

        .demo-container .RadTabStrip.rtsVertical.rtsRight {
            margin-left: -10px;
        }

        * html .demo-container div.RadTabStripVertical .rtsLevel {
            width: 93px;
        }

        .demo-container img {
            display: block;
        }

        .demo-container .rtsLevel.rtsCenter {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div style="color: Black" class="demo-container no-bg size-narrow">
        <%--<div class="demo-content">--%>
            <div style=" background-color:skyblue; width: 795px; border-left: solid 3px silver; border-right: solid 3px silver; border-top: solid 3px silver; padding:0.8em">
                <b style="font:bold">Backend Data Management</b>
            </div>
            <div style="float: left; width: 170px; border-left: solid 3px silver; border-top: solid 3px silver; border-bottom: solid 3px silver;">
                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" Skin="Silk" ID="RadTabStrip1" Width="170px" Orientation="VerticalLeft" Align="Left" AutoPostBack="true" MultiPageID="RadMultiPage1" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab Text="Asset Areas"></telerik:RadTab>
                        <telerik:RadTab Text="Change Types"></telerik:RadTab>
                        <telerik:RadTab Text="Define Support Contacts"></telerik:RadTab>
                        <telerik:RadTab Text="Functions"></telerik:RadTab>
                        <telerik:RadTab Text="Impact Analysis"></telerik:RadTab>
                        <telerik:RadTab Text="Locations"></telerik:RadTab>
                        <telerik:RadTab Text="Organisational Unit"></telerik:RadTab>
                        <telerik:RadTab Text="Plan Execution Criteria"></telerik:RadTab>
                        <telerik:RadTab Text="Plan Types"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </div>
            <div style="background-color: white; float: left; width: 630px; border: solid 3px silver; padding: 0.5em; height: 590px; overflow: auto">
                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" Width="600px">

                    <telerik:RadPageView runat="server" ID="RadPageView1">
                        <div class="contentWrapper">
                            <uc2:Areas ID="Areas1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView2">
                        <div class="contentWrapper">
                            <uc3:ChangeTypes ID="ChangeTypes1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView3" CssClass="RadPageView3">
                        <div class="contentWrapper">
                            <uc8:SupportContacts ID="SupportContacts1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView4" CssClass="RadPageView3">
                        <div class="contentWrapper">
                            <uc6:Functions ID="Functions1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView5">
                        <div class="contentWrapper">
                            <uc7:ImpactAnalysisWindow ID="ImpactAnalysisWindow1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView6">
                        <div class="contentWrapper">
                            <uc4:Locations ID="Locations1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView7" CssClass="RadPageView3">
                        <div class="contentWrapper">
                            <uc1:OU ID="OU1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView8" CssClass="RadPageView3">
                        <div class="contentWrapper">
                            <uc1:Readiness runat="server" ID="Readiness1"></uc1:Readiness>
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="RadPageView9" CssClass="RadPageView3">
                        <div class="contentWrapper">
                            <uc5:PlanTypes ID="PlanTypes1" runat="server" />
                        </div>
                    </telerik:RadPageView>

                </telerik:RadMultiPage>
            </div>
        <%--</div>--%>
    </div>
</asp:Content>

