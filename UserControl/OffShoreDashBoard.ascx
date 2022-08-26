<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OffShoreDashBoard.ascx.cs" Inherits="UserControl_OffShoreDashBoard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%--<script type="text/javascript">

    (function (global, undefined) {
        global.OnClientSeriesClicked = function (sender, args) {

            var ajaxManager = global.getAjaxManager();
            if (args.get_seriesName() !== "Months") {
                ajaxManager.ajaxRequest(args.get_category());
            }
        }
    })(window);

</script>

<telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />--%>

<div class="demo-container size-wide">
    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1">
        <PlotArea>
            <Series>
                <telerik:ColumnSeries Name="Change Requests" DataFieldY="No of Requests">
                    <%--<TooltipsAppearance DataFormatString="${0}" />--%>
                    <LabelsAppearance Visible="true" />
                </telerik:ColumnSeries>
            </Series>
            <XAxis DataLabelsField="Name">
            </XAxis>
            <YAxis>
                <LabelsAppearance DataFormatString="${0}" />
            </YAxis>
        </PlotArea>
        <Legend>
            <Appearance Visible="false" />
        </Legend>
        <ChartTitle Text="IAP Change Requests">
        </ChartTitle>
    </telerik:RadHtmlChart>
</div>
