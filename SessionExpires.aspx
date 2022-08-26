<%@ Page Language="C#" MasterPageFile="~/MasterPages/IAPMasterPage.master" AutoEventWireup="true" CodeFile="SessionExpires.aspx.cs" Inherits="SessionExpires" Title="IAP Change Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="color:Black">
    <table style="width:100%">
    <tr>
        <td>
        </td>
    </tr>
    
    <tr>
    <td>
        <p>
            Your Session for this application has expired. 
        </p>
        <p>
            Please, always save your work at every point in this application.
            Any unsaved work may be lost. <br />
        </p>
        <p>
            Click 
            <asp:LinkButton ID="hereLinkButton" runat="server" 
                PostBackUrl="~/Index.aspx">here</asp:LinkButton>
                &nbsp;to continue.
        </p>
    </td>
    </tr>
    
    <tr>
    <td>
        &nbsp;</td>
    </tr>
    
    <tr>
    <td>
    <p>
            In case of any problem, please contact, <a href="mailto:isaac.bejide@shell.com">
            isaac.bejide@shell.com</a>&nbsp; ext.: 24772.
        </p>
        <p>
            Thank you.
        </p>    
        
    </td>
    </tr>
    
    <tr>
    <td>
        
    </td>
    </tr>
    
    </table>
</div>
</asp:Content>

