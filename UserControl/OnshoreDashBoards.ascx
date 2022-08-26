<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnshoreDashBoards.ascx.cs" Inherits="UserControl_OnshoreDashBoards" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<script type="text/javascript">

    (function (global, undefined) {
        global.OnClientSeriesClicked = function (sender, args) {

            var ajaxManager = global.getAjaxManager();
            if (args.get_seriesName() !== "Months") {
                ajaxManager.ajaxRequest(args.get_category());
            }
        }
    })(window);

</script>

<div class="demo-container size-wide">
    <telerik:RadHtmlChart ID="RadHtmlChart1" runat="server" OnClientSeriesClicked="OnClientSeriesClicked">
        <ChartTitle Text="Onshore"></ChartTitle>

        <PlotArea>
            <Series>
                <telerik:ColumnSeries DataFieldY="Req" Name="Years">
                    <TooltipsAppearance Color="White" />
                </telerik:ColumnSeries>
            </Series>

            <XAxis DataLabelsField="Year"></XAxis>
        </PlotArea>

    </telerik:RadHtmlChart>


    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TelerikConnectionString %>"        
         SelectCommand="SELECT COUNT(*), to_char(IAP_REQUESTFORM.requestdate, 'YYYY') Year FROM IAP_OU 
                        INNER JOIN IAP_AREA ON IAP_OU.IDOU = IAP_AREA.IDOU
                        INNER JOIN IAP_LOCATIONS ON IAP_AREA.IDAREA = IAP_LOCATIONS.IDAREA
                        INNER JOIN IAP_REQUESTFORM ON IAP_LOCATIONS.LOCATIONID = IAP_REQUESTFORM.LOCATIONID
                        WHERE IAP_OU.IDOU = '1' GROUP BY IAP_REQUESTFORM.requestdate ORDER BY year"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TelerikConnectionString %>"
        SelectCommand="SELECT Sum(Revenue) as Rev, Quarter FROM Revenue WHERE Year=@Year GROUP BY Quarter">
        <SelectParameters>
            <asp:Parameter Name="Year"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:TelerikConnectionString %>"
        SelectCommand="SELECT Sum(Revenue) as Rev, Month FROM Revenue WHERE Year=@Year AND Quarter = @Quarter GROUP BY Month">
        <SelectParameters>
            <asp:Parameter Name="Year"></asp:Parameter>
            <asp:Parameter Name="Quarter"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>--%>


    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadHtmlChart1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Refresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadHtmlChart1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" Height="77px" Width="113px" runat="server"></telerik:RadAjaxLoadingPanel>
    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function getAjaxManager()
            {
                return $find("<%=RadAjaxManager1.ClientID%>");
            }
        </script>
    </telerik:RadCodeBlock>

</div>
