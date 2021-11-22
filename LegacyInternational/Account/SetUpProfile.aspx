<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetUpProfile.aspx.cs" Inherits="LegacyInternational.Account.SetUpProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableRow>
            <asp:TableCell>
                <h5>Username</h5>
                <asp:TextBox runat="server" ID="Username"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>First Name</h5>
                <asp:TextBox runat="server" ID="FName"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Last Name</h5>
                <asp:TextBox runat="server" ID="LName"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Date of Birth</h5>
                <asp:TextBox runat="server" ID="DOB"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Contact Number</h5>
                <asp:TextBox runat="server" ID="CNumber"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell>
  
               <asp:Button CssClass="btn btn-outline-primary" runat="server" Text="Finish" ID="Submit" OnClick="Submit_Click" />

           </asp:TableCell>
       </asp:TableRow>
    </asp:Table>
</asp:Content>
