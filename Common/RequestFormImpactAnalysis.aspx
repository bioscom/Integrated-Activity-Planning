<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="RequestFormImpactAnalysis.aspx.cs" Inherits="Common_RequestFormImpactAnalysis" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../JavaScript/iap.js"></script>
    <table class="tSubGray">
        <tr>
            <td>
                <table id="tblRating" runat="server" class="tSubGray">
                    <tr>
                        <td class="cHeadTile" style="vertical-align:middle; width:100px">
                            &nbsp;</td>
                        <td class="cHeadTile" colspan="2">
                            <div style="text-align:center">
                                Very Short-Term</div>
                        </td>
                        <td class="cHeadTile" colspan="2">
                            <div style="text-align:center">
                                Short-Term</div>
                        </td>
                        <td class="cHeadTile" colspan="2">
                            <div style="text-align:center">
                                Medium-Term</div>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle; width:100px">
                            &nbsp;</td>
                        <td class="style1">
                            Mbopd:</td>
                        <td class="style1">
                            MMscfpd:</td>
                        <td class="style2">
                            Mbopd:</td>
                        <td class="style2">
                            MMscfpd:</td>
                        <td class="style3">
                            Mbopd:</td>
                        <td class="style3">
                            MMscfpd:</td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle; width:100px">
                            Production Var:</td>
                        <td class="style1">
                            <asp:TextBox ID="txtMbopd" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtMmscfpd" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtMbopd0" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtMmscfpd0" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtMbopd1" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtMmscfpd1" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cost Variance:</td>
                        <td class="style1" colspan="2">
                            <asp:TextBox ID="txtCostVar" runat="server" Width="110px"></asp:TextBox>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:TextBox ID="txtCostVar0" runat="server" Width="110px"></asp:TextBox>
                        </td>
                        <td class="style3" colspan="2">
                            <asp:TextBox ID="txtCostVar1" runat="server" Width="110px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Production:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstProductionddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stProductionddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtProductionddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Logistic Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstLogisticddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stLogisticddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtLogisticddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CP Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstCpddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stCpddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtCpddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Security Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstSecurityddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stSecurityddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtSecurityddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            HSSE Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstHsseddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stHsseddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtHsseddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            FTO/LTO Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstftoltoddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stftoltoddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtftoltoddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Legal Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstlegalddl" runat="server" ValidationGroup="impactAnalysis" 
                                Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stlegalddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtlegalddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Functional Impact:</td>
                        <td class="style1" colspan="2">
                            <asp:DropDownList ID="vstfunctionalddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2" colspan="2">
                            <asp:DropDownList ID="stfunctionalddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="mtfunctionalddl" runat="server" 
                                ValidationGroup="impactAnalysis" Width="100px">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; &nbsp;</td>
                        <td class="style1" colspan="2">
                            &nbsp;</td>
                        <td class="style2" colspan="2">
                            &nbsp;</td>
                        <td class="style3" colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                <table id="tblRating2" runat="server" class="tSubGray">
                    <tr>
                        <td class="cHeadTile" align="right" colspan="2">
                            Rating:&nbsp;&nbsp;
                            <asp:Label ID="Rating" runat="server" CssClass="Warning"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" Text="Activity Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtActivity" runat="server" Height="55px" TextMode="MultiLine" 
                                Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reason for Change:</td>
                        <td>
                            <asp:TextBox ID="txtChangeReason" runat="server" Width="350px" Height="55px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Overall Rating:</td>
                        <td>
                            <asp:TextBox ID="txtOverallRating" runat="server" Width="350px" Height="52px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Any other Remark:</td>
                        <td>
                            <asp:TextBox ID="txtOtherRemarks" runat="server" Height="52px" 
                                TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <center>
                 <asp:Button ID="insertButton" runat="server" onclick="insertButton_Click" Text="Ok" ValidationGroup="impactAnalysis" Width="100px" />
                    &nbsp;
                 <asp:Button ID="pnlCloseButton" runat="server" onclick="pnlCloseButton_Click" Text="Close" ValidationGroup="impactAnalysis" Width="100px" />
                </center>
            </td>
        </tr>
    </table>
    <br /><br /><br />
    <br /><br /><br />
</asp:Content>