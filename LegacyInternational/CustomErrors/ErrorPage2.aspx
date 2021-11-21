<%@ Page Title="Error Page 2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage2.aspx.cs" Inherits="LegacyInternational.CustomErrors.ErrorPage2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <br />
        <br />
    <table class="table table-dark" style="text-align:left">
            <tr>
            <td>
        <h1>Status : 403 Forbidden</h1>
        <h2>Description: The client does not have access rights to the content; that is, it is unauthorized, so the server is refusing to give the requested resource.</h2>
        <h4 id="ErrorSource" runat="server"></h4>
        <h5 id="InnerEx" runat="server"></h5>
        <h6 id="StackTrace" runat="server"></h6>
        <p runat="server" id="ErrorMessage"><br /> We are very sorry for the inconvenience caused to you... </p>
        
    </td>
        </tr>
        </table>
        </div>
</asp:Content>
