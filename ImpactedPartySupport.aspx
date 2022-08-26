<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImpactedPartySupport.aspx.cs" Inherits="ImpactedPartySupport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/UserControl/RequestInformation.ascx" TagPrefix="uc1" TagName="RequestInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        
        <script type="text/javascript">
            function CloseAndRebind(args) {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                return oWindow;
            }

            function CancelEdit() {
                GetRadWindow().close();
            }


            (function (global, undefined) {
                var demo = {};

                function alertCallBackFn(arg) {
                    radalert("<strong>radalert</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
                }

                function confirmCallBackFn(arg) {
                    radalert("<strong>radconfirm</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
                }

                function promptCallBackFn(arg) {
                    radalert("After 7.5 million years, <strong>Deep Thought</strong> answers:<h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Deep Thought");
                }

                Sys.Application.add_load(function () {
                    //attach a handler to radio buttons to update global variable holding image url
                    $telerik.$('input:radio').bind('click', function () {
                        demo.imgUrl = $telerik.$(this).val();
                    });
                });


                global.alertCallBackFn = alertCallBackFn;
                global.confirmCallBackFn = confirmCallBackFn;
                global.promptCallBackFn = promptCallBackFn;

                global.$dialogsDemo = demo;

            })(window);
        </script>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <div>
            
            <fieldset>
                <legend>
                    <b>Impacted/Supporting Party Review and agree impact</b>
                </legend>
                <table>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td style="width: 100px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Comment:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox RenderMode="Lightweight" ID="txtComment" runat="server" Height="100px" TextMode="MultiLine" Width="500px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>

                                        <asp:Button ID="forwardButton" runat="server" Text="Submit" Width="100px"
                                            OnClick="forwardButton_Click" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
            <fieldset>
                <legend><b>Change Request Details</b> </legend>
                <uc1:RequestInformation runat="server" ID="RequestInformation1" />
            </fieldset>

        </div>
    </form>
</body>
</html>
