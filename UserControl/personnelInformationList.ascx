<%@ Control Language="C#" AutoEventWireup="true" CodeFile="personnelInformationList.ascx.cs" Inherits="UserControl_personnelInformationList" %>
<%--<%@ Reference VirtualPath="~/App/FieldVisit/UserControl/SEPCiN/activityHeader.ascx" %>--%>

<div style="position: static">
    <div style="overflow: auto">
        <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" ShowFooter="true"
            OnPageIndexChanging="grdView_PageIndexChanging" OnRowCommand="grdView_RowCommand"
            OnRowCancelingEdit="grdView_RowCancelingEdit"
            OnRowDataBound="grdView_RowDataBound" OnRowDeleting="grdView_RowDeleting"
            OnRowEditing="grdView_RowEditing" OnRowUpdating="grdView_RowUpdating" DataKeyNames="ID_PERSONNEL"
            Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="..." ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update" OnClientClick="return confirm('Update?')" ValidationGroup="iPersonnel"></asp:LinkButton>
                        <asp:ValidationSummary ID="vsUpdate" runat="server" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="iPersonnel" Enabled="true" HeaderText="Validation Summary..." />
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                            ValidationGroup="iPersonnel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit" ValidationGroup="iPersonnel"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelActivityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gender">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpGender" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GENDER") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Company">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCompanyName" runat="server" TextMode="MultiLine" Height="25px" Width="250px" Text='<%# Eval("COMPANY") %>'></asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelCompanyName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COMPANY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact person on FPSO">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpContactPerson" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelContactPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CONTACT_PERSON") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Last visit on FPSO">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpLastVisit" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelDateTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LAST_VISIT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Valid BOSIET?">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpBOSIET" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblBosiet" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "BOSIET_VALID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Valid HUET?">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpHUET" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblHuet" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "HUET_VALID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Valid Medical">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpMedical" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblMedical" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "MEDICAL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="VISA Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpVisaType" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelDateTo3" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "VISA_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="PPE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpPPE" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="lblPPE" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "PPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%--<asp:TemplateField HeaderText="BAGGAGE">
                    <EditItemTemplate>
                        <asp:DropDownList ID="drpBaggage" runat="server"></asp:DropDownList>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <asp:Label ID="labelBaggage" runat="server"
                            Text='<%# DataBinder.Eval(Container.DataItem, "BAGGAGE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="..." ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                            ValidationGroup="iPersonnel" Text="Delete" OnClientClick="return confirm('Delete?')"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </div>
    <br />
    <asp:LinkButton ID="addLinkButton" ValidationGroup="oooo" runat="server" OnClick="addLinkButton_Click">Add Personnel</asp:LinkButton>
    <br />
</div>
<br />
<asp:Panel ID="infoPanel" runat="server">
    <table class="tSubGray" style="width: 100%; border: none">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Employee Name:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmployeeName" ErrorMessage="Employee Name is required" ValidationGroup="Personnel">*</asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEmployeeName" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Gender:<asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="drpGender" ErrorMessage="Gender is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="drpGender" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Company:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompany" ErrorMessage="Company Name is required" ValidationGroup="Personnel">*</asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCompany" runat="server" Height="70px" TextMode="MultiLine"
                                Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Who is your contact person(s) in the Field/FPSO?:<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpContactPerson" ErrorMessage="Field/FPSO contact is person required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpContactPerson" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">When was your last visit to the Field/FPSO?:<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpLastVisit" ErrorMessage="When Last Visit is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpLastVisit" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Medical:<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpMedical" ErrorMessage="Medical is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpMedical" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <%--<td>Valid HUET?:<asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="drpHUET" ErrorMessage="HUET Validity is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpHUET" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>Visa Type:<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="drpVisaType" ErrorMessage="Visa Type is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpVisaType" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <%--<td>PPE:<asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="drpPPE" ErrorMessage="PPE is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPPE" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>Valid BOSIET?:<asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="drpBOSIET" ErrorMessage="BOSIET Validity is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBOSIET" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <%--<td>Baggage:<asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="drpBaggage" ErrorMessage="Baggage is required" Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="Personnel">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpBaggage" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add"
                                Width="100px" ValidationGroup="Personnel" />
                            &nbsp;<asp:Button ID="cancleButton" runat="server" Text="Cancel" Width="100px" OnClick="cancleButton_Click" ValidationGroup="cancel" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Personnel" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="RequestIDHF" runat="server" />
