<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dateControl.ascx.cs" Inherits="UserControl_dateControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<div>
    <div style="float: left">
        <asp:TextBox ID="txtDate" runat="server" BackColor="#CCCCCC" Width="100px"></asp:TextBox>
    </div>

    <div style="float: left">
        <asp:ImageButton ID="imgBtnStartDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
            ValidationGroup="yyyy" Height="22px" />
    </div>

    <ajaxToolkit:CalendarExtender ID="txtDateExt" runat="server" Enabled="True" EnableViewState="true"
        PopupButtonID="imgBtnStartDate" TargetControlID="txtDate" Format="dd/MM/yyyy"
        DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
    </ajaxToolkit:CalendarExtender>

    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDate"
        ErrorMessage="Invalid date format, please use the date selector to enter date"
        ValidationExpression="^(3[01]|[12]\d|0[1-9])/(0[13578]|10|12)/((?!0000)\d{4})|(30|[12]\d|0[1-9])/(0[469]|11)/((?!0000)\d{4})|(2[0-8]|[01]\d|0[1-9])/(02)/((?!0000)\d{4})| 29/(02)/(1600|2000|2400|2800|00)|29/(02)/(\d\d)(0[48]|[2468][048]|[13579][26])">*</asp:RegularExpressionValidator>

    <asp:RequiredFieldValidator ID="valdtDateRequired" runat="server" ControlToValidate="txtDate"
        ErrorMessage="Date is required">*</asp:RequiredFieldValidator>
</div>

<%--<div>
    <div style="float: left">
        <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
    </div>
    <div style="float: left">
        <asp:ImageButton ID="imgBtnStartDate" runat="server" Height="21px"
            ImageUrl="~/Images/Calendar_scheduleHS.png" ValidationGroup="yyyy" />
    </div>
</div>
<ajaxToolkit:CalendarExtender ID="txtDateExt"
    runat="server" Enabled="True" EnableViewState="true"
    PopupButtonID="imgBtnStartDate" TargetControlID="txtDate" Format="dd-MM-yyyy">
</ajaxToolkit:CalendarExtender>--%>