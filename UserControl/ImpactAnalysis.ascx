<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImpactAnalysis.ascx.cs" Inherits="UserControl_ImpactAnalysis" %>

        <style type="text/css">
            .style1
            {
                background-color: #3399FF;
            }
            .style2
            {
                background-color: #FFFFFF;
            }
            .style3
            {
                background-color: #3399FF;
            }
        </style>
        
        <%--<div style="float:left;overflow:scroll; width:800px; margin-left:10px">--%>
            <table style="height:500px; width:800px">
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td valign="top">
                                <table style="margin-right:5px" class="tMainBorder">
                                    <tr>
                                        <td align="right">
                                            Rating:&nbsp;&nbsp; </td>
                                        <td>
                                            <asp:Label ID="Rating" runat="server" CssClass="Warning"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Activity Description:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtActivity" runat="server" Width="219px" TextMode="MultiLine" 
                                                Height="70px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Reason for Change:</td>
                                        <td>
                                            <asp:TextBox ID="txtChangeReason" runat="server" Width="216px" 
                                                TextMode="MultiLine" Height="70px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Overall Rating:</td>
                                        <td>
                                            <asp:TextBox ID="txtOverallRating" runat="server" Width="219px" 
                                                TextMode="MultiLine" Height="70px"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Any other Remark:</td>
                                        <td>
                                            <asp:TextBox ID="txtOtherRemarks" runat="server" Width="215px" Height="70px" 
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                                <td>
                                <table class="tMainBorder">
                                    <tr>
                                        <td rowspan="2" style="vertical-align:middle; width:100px">
                                            &nbsp;</td>
                                        <td colspan="2">
                                            <div style="text-align:center">Very Short-Term</div>
                                        </td>
                                        <td colspan="2">
                                            <div style="text-align:center">Short-Term</div>
                                        </td>
                                        <td colspan="2">
                                            <div style="text-align:center">Medium-Term</div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
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
                                        <td colspan="2" class="style1">
                                            <asp:TextBox ID="txtCostVar" runat="server" Width="105px"></asp:TextBox>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:TextBox ID="txtCostVar0" runat="server" Width="105px"></asp:TextBox>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:TextBox ID="txtCostVar1" runat="server" Width="105px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Production:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="productionddl" runat="server" AutoPostBack="True" Width="100px" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="productionddl0" runat="server" AutoPostBack="True" Width="100px" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="productionddl1" runat="server" AutoPostBack="True" Width="100px" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Logistic Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="logisticddl" runat="server" AutoPostBack="True" Width="100px" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="logisticddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="logisticddl1" runat="server" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CP Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="cpddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="cpddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="cpddl1" runat="server" ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Security Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="securityddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="securityddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="securityddl1" runat="server" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            HSSE Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="hsseddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="hsseddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="hsseddl1" runat="server" ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            FTO/LTO Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="ftoltoddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="ftoltoddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="ftoltoddl1" runat="server" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Legal Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="legalddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="legalddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="legalddl1" runat="server" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Functional Impact:</td>
                                        <td colspan="2" class="style1">
                                            <asp:DropDownList ID="functionalddl" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style2">
                                            <asp:DropDownList ID="functionalddl0" runat="server" AutoPostBack="True" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2" class="style3">
                                            <asp:DropDownList ID="functionalddl1" runat="server" 
                                                ValidationGroup="impactAnalysis">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan="6">
                    
                    <asp:Button ID="submitButton" runat="server" Text="Submit" 
                            onclick="submitButton_Click" ValidationGroup="impactAnalysis" Width="100px" />
                        &nbsp; <asp:Button ID="closeButton" runat="server" Text="Close" 
                            ValidationGroup="impactAnalysis" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/impactAnalysis.jpg"/>
                    </td>
                </tr>
            </table>
        <%--</div>--%>