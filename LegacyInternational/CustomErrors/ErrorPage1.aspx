<%@ Page Title="Error Page 1" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage1.aspx.cs" Inherits="LegacyInternational.CustomErrors.ErrorPage1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <br />
        <br />
    <table class="table table-dark" style="text-align:left">
            <tr>
            <td>
        <h1>Status : 400 Bad Request</h1>
        <h2>Description: The server could not understand the request due to invalid syntax.</h2>
        <h4 id="ErrorSource" runat="server"></h4>
        <h5 id="InnerEx" runat="server"></h5>
        <h6 id="StackTrace" runat="server"></h6>
        <p runat="server" id="ErrorMessage"><br /> We are very sorry for the inconvenience caused to you... </p>
        
    </td>
        </tr>
        </table>
        </div>

</asp:Content>
