<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="ImpactAnalysis.aspx.cs" Inherits="Common_ImpactAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" Runat="Server">
    
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table class="tMainBorder">
            <tr>
                <td class="cHeadTile" colspan="7">
                    Impact Analysis Table</td>
            </tr>
            <tr>
                <td style="width:100px">
                    Rating</td>
                <td colspan="6">
                    <asp:Label ID="Rating" runat="server" CssClass="Warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Activity Description:</td>
                <td colspan="6">
                    <asp:TextBox ID="txtActivity" runat="server" Width="430px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Reason for Change:</td>
                <td colspan="6">
                    <asp:TextBox ID="txtChangeReason" runat="server" Width="430px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" style="vertical-align:middle">
                    Overall Rating</td>
                <td colspan="6">
                    <div style="text-align:center">Description</div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:TextBox ID="txtOverallRating" runat="server" Width="430px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            </table>
         
        <table class="tMainBorder">
            <tr>
                <td rowspan="3" style="vertical-align:middle; width:100px">
                    Production Variance</td>
                <td colspan="2">
                    <div style="text-align:center">Month 1-3</div>
                </td>
                <td colspan="2">
                    <div style="text-align:center">Current Year</div>
                </td>
                <td colspan="2">
                    <div style="text-align:center">Next Year</div>
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
            <tr style="text-align:center">
                <td>
                    &nbsp;</td>
                <td colspan="2" class="style1">
                    Description</td>
                <td colspan="2" class="style2">
                    Cost Variance(F$)</td>
                <td colspan="2" class="style3">
                    Duration Variance/No 
                    <br />
                    of Functions Impacted</td>
            </tr>
            <tr>
                <td>
                    Logistic Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="logisticddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="logisticddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="logisticddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    CP Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="cpddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="cpddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="cpddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Security Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="securityddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="securityddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="securityddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    HSSE Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="hsseddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="hsseddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="hsseddl1" runat="server" Height="16px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    FTO/LTO Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="ftoltoddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="ftoltoddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="ftoltoddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Legal Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="legalddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="legalddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="legalddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Functional Impact</td>
                <td colspan="2" class="style1">
                    <asp:DropDownList ID="functionalddl" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style2">
                    <asp:DropDownList ID="functionalddl0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="style3">
                    <asp:DropDownList ID="functionalddl1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
            
        <table class="tMainBorder">
            <tr>
                <td style="width:100px">
                    Any other Remark:</td>
                <td>
                    <asp:TextBox ID="txtOtherRemarks" runat="server" Width="430px" Height="80px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <div style="text-align:center">
                        <asp:Button ID="submitButton" runat="server" Text="Submit" 
                            onclick="submitButton_Click" />
                        &nbsp;
                        <asp:Button ID="closeButton" runat="server" Text="Close" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />
</asp:Content>