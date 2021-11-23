<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LegacyInternational.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="container">
        <br />
        <br />
        <asp:Table class="table table-dark table-bordered table-striped" runat="server">
        <asp:TableRow>
            <asp:TableCell>
        <h4>Log in</h4>
                    <hr />
                </asp:TableCell>
         </asp:TableRow>
            <asp:TableRow>
            <asp:TableCell>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                </asp:TableCell>
         </asp:TableRow>
                    <asp:TableRow>
            <asp:TableCell>
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </asp:TableCell>
         </asp:TableRow>
                     <asp:TableRow>
            <asp:TableCell>
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </asp:TableCell>
         </asp:TableRow>
                    <asp:TableRow>
            <asp:TableCell>
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />&nbsp;
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                            </div>
                        </asp:TableCell>
         </asp:TableRow>
                    <asp:TableRow>
            <asp:TableCell>
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-outline-primary" />
                        </asp:TableCell>
         </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    --%>
                </p>
            </asp:TableCell>
         </asp:TableRow>
            </asp:Table>
        <br />
        <br />
    </div>
</asp:Content>
