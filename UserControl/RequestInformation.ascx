<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequestInformation.ascx.cs" Inherits="UserControl_RequestInformation" %>

<fieldset>
    <legend><b style="color: green">Change Request Details</b></legend>

    <table style="width: 98%; font-size: 12px">
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label1" runat="server" Text="Request Number:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="requestNumberLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="Organisation Unit:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="OULabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label27" runat="server" Text="Area:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="areaLabel" runat="server" Font-Bold="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" runat="server" Text="Location:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="locationLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label26" runat="server" Text="Project Activity:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="projectActivityLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Plan Type:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="planTypeLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Reference Plan:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="planIssueLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Change Type:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="changeTypeLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="WO NO:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="WONOLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label38" runat="server" Text="Primavera ID:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="PrimaveraIDLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="Label31" runat="server" Text="Schedule Original:" Font-Bold="True"></asp:Label>
            </td>
            <td>

                <asp:Label ID="Label34" runat="server" Text="From:" Font-Bold="True"></asp:Label>
                &nbsp;<asp:Label ID="originalFrom" runat="server"></asp:Label>
                &nbsp;<asp:Label ID="Label35" runat="server" Text="To:" Font-Bold="True"></asp:Label>
                <asp:Label ID="originalTo" runat="server"></asp:Label>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label32" runat="server" Text="Schedule Requested:" Font-Bold="True"></asp:Label>
            </td>
            <td>

                <asp:Label ID="Label36" runat="server" Text="From:" Font-Bold="True"></asp:Label>
                &nbsp;<asp:Label ID="requestFrom" runat="server"></asp:Label>
                &nbsp;<asp:Label ID="Label37" runat="server" Text="To:" Font-Bold="True"></asp:Label>
                &nbsp;<asp:Label ID="requestTo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Originator:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="originatorLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label21" runat="server" Text="Date Requested:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="requestDateLabel" ForeColor="Red" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label33" runat="server" Text="Time Requested:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="requestTimeLabel" ForeColor="Red" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <hr>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <table style="width:90%">
                    <tr>
                        <td>
                            <table style="width:100%">
                                <tr>
                                    <td colspan="2"><b style="color: green">Request Plan</b></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Label ID="Label18" runat="server" Text="Plan Cost:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="planCostLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="New Cost:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="NewCostLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width:100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label29" runat="server" Text="Gain" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Label ID="Label16" runat="server" Text="Plan VOL(mbopd):" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="planVOLLabel" runat="server" Font-Bold="False" ForeColor="Black"
                                            Style="color: Green;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="New VOL(mbopd):" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="NewVOLLabel" runat="server" Font-Bold="False" ForeColor="Black"
                                            Style="color: #008000;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table style="width:100%">
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label30" runat="server" Text="Loss" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        <asp:Label ID="Label22" runat="server" Text="Plan VOL(mmscfpd):" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="planVOLGASLabel" runat="server" Font-Bold="False"
                                            ForeColor="Black" Style="color: #FF0000;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="New VOL(mmscfpd):" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="NewVOLGASLabel" runat="server" Font-Bold="False"
                                            ForeColor="Black" Style="color: #FF0000;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>

        <tr>
            <td style="width: 150px">
                <asp:Label ID="Label10" runat="server" Text="Proposal:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="proposalLabel" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="width: 150px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Benefit:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="benefitLabel" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Risk:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="riskLabel" runat="server"></asp:Label>
            </td>
        </tr>

    </table>
</fieldset>
