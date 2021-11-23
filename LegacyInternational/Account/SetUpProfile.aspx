<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetUpProfile.aspx.cs" Inherits="LegacyInternational.Account.SetUpProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="container" style="width:fit-content;min-width:70vw">
       <br />
       <br />
   
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableRow>
            <asp:TableCell>
                <h5>Username</h5>
                <asp:TextBox runat="server" ID="Username"></asp:TextBox>
                <asp:Label runat="server" ID="UsernameErr" ForeColor="Red" Visible="false">
                    Username must be entered
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>First Name</h5>
                <asp:TextBox runat="server" ID="FName"></asp:TextBox>
                <asp:Label runat="server" ID="FNameErr" ForeColor="Red" Visible="false">
                    First Name must be entered.
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Last Name</h5>
                <asp:TextBox runat="server" ID="LName"></asp:TextBox>
                <asp:Label runat="server" ID="LNameErr" ForeColor="Red" Visible="false">
                    Last Name must be entered.
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Date of Birth</h5>
                <asp:TextBox runat="server" ID="DOB"></asp:TextBox>
                <asp:Label runat="server" ID="DOBErr" ForeColor="Red" Visible="false">
                    Date of Birth must be entered.
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>Contact Number</h5>
                <asp:TextBox runat="server" ID="CNumber"></asp:TextBox>
                <asp:Label runat="server" ID="CNumberErr" ForeColor="Red" Visible="false">
                    Contact Number must be entered.
                </asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell>
  
               <asp:Button CssClass="btn btn-outline-primary" runat="server" Text="Finish" ID="Submit" OnClick="Submit_Click" />

           </asp:TableCell>
       </asp:TableRow>
    </asp:Table>
       <br />
       <br />
       </div>
</asp:Content>
