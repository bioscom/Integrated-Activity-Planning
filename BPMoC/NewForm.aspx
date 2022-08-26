<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="NewForm.aspx.cs" Inherits="BPMoC_NewForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="SheetContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></ajaxToolkit:ToolkitScriptManager>

    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <table class="tMainBorder" style="width: 950px">
                <tr>
                    <td colspan="4">
                        <div>
                            <div style="float: left; margin: auto">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/logo.gif" />
                            </div>
                            <div style="margin-left: 10em; margin-top: 0.5em; width: 583px">
                                <center>
                                    <h1>SPDC Integrated Activity Plan Change Request Form</h1>
                                </center>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px">
                        <asp:Label ID="Label2" runat="server" Text="REQUEST NO:"></asp:Label>
                    </td>
                    <td style="width: 350px">&nbsp;</td>
                    <td style="width: 150px">
                        <asp:Label ID="Label3" runat="server" Text="APPROVAL STATUS:"></asp:Label>
                    </td>
                    <td style="width: 250px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="OU:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOU" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOU_SelectedIndexChanged" Width="300px">
                            <asp:ListItem Value="-1">Select OU...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="PLAN TYPE:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlanType" runat="server" Width="250px">
                            <asp:ListItem Value="-1">Select Plan Type...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="AREA:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Width="300px">
                            <asp:ListItem Value="-1">Select Area...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="PLAN ISSUE:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlanIssue" runat="server" Width="250px">
                            <asp:ListItem Value="-1">Select Plan Issue...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="LOCATION:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLocation" runat="server" Width="300px">
                            <asp:ListItem Value="-1">Select Location...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="CHANGE TYPE:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlChangeType" runat="server" Width="250px">
                            <asp:ListItem Value="-1">Select Change Type...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="PROJECT/ACTIVITY:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtProject" runat="server" Width="750px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="ORIGINATOR:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblOriginator" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="WO NO(Req.):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWONO" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="REQUEST DATE:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">Description of Change Request</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtChangeReqDesc" runat="server" Width="950px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">Description of Reasons for Change</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtReason" runat="server" Width="950px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Technical Integrity:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTechInt" runat="server" Width="500px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="HSE:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHSE" runat="server" Width="500px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Operational/Production:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOperationalProd" runat="server" Width="500px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="Business Impact(Savings/Loss):"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBusinessImpact" runat="server" Width="500px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </center>

                    </td>
                </tr>
                <tr>
                    <td class="cHeadTile" colspan="4">Impact of Change</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="auto-style3">
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Text="ACTIVITIES NEGATIVELY IMPACTED BY CHANGE:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text="PRODUCTION IMPACT:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtActivityImpacted" runat="server" Width="550px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProdImpact" runat="server" Width="380px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" Text="Cost Exposure:"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCostExposure" runat="server" Width="550px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label23" runat="server" Text="Oil:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOil" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label25" runat="server" Text="bopd"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label24" runat="server" Text="Gas:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGas" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="MMscf/d"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">Recovery Plan:</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtRecoveryPlan" runat="server" Width="950px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">Evaluation of Change for Strategic Fit</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtChangeEvaluation" runat="server" Width="950px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">Remarks:</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="950px" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">Activity Executors Impacted:</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="height: 100px; width: 99%; overflow: auto; border: solid 1px silver">
                            <asp:CheckBoxList ID="ckbActivityExecutors" runat="server" RepeatColumns="4">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="cHeadTile">APPROVALS</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="tSubGray" style="width: 100%">
                            <tr class="cHeadTile">
                                <td style="width: 250px">Change Champions:</td>
                                <td>&nbsp;</td>
                                <td>Date Received</td>
                                <td>Date Reviewed</td>
                                <td style="width: 300px">Comment</td>
                                <td>Status</td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Text="Activity Owner:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblOriginator2" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label28" runat="server" Text="Project Manager:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProjMgr" runat="server" Width="200px">
                                        <asp:ListItem Value="-1">Select Project Mgr</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblPMDateRecvd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPMDateRevd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPMComment" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPMStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Text="Business Opportunity Manager:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBOM" runat="server" Width="200px">
                                        <asp:ListItem Value="-1">Select BOM</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblBOMDateRecvd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBOMDateRevd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBOMComment" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBOMStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="Decision Executive:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDE" runat="server" Width="200px">
                                        <asp:ListItem Value="-1">Select DE...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblDEDateRecvd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDEDateRevd" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDEComment" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDEStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">CHANGE REVIEW BOARDS COMMENTS:</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblChangeDRBComment" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:right">
                        
                    </td>
                </tr>
            </table>
            <div style="width:950px; text-align:right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
            
            <br />
            <br />
            <br />
        </ContentTemplate>

    </asp:UpdatePanel>

    <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="update">
    </ajaxToolkit:UpdatePanelAnimationExtender>

    <asp:UpdateProgress ID="upgAjaxBloc" runat="server" DisplayAfter="100" Visible="true" DynamicLayout="true">
        <ProgressTemplate>
            <div id="IMGDIV" align="center" valign="middle" runat="server" style="position: absolute; visibility: visible; left: 50%; top: 55%; vertical-align: middle; border-color: #FFFFFF; background-color: #FFFFFF;">
                <asp:Image ID="imgAjaxWait" runat="server" ImageUrl="~/Images/i_ajaxRoller.gif" Width="50" Height="50" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="SheetContentPlaceHolder">
</asp:Content>
