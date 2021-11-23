<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LegacyInternational.Account.Profile" EnableEventValidation="false" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width:fit-content;min-width:70vw">
        <br />
        <br />
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
                    <asp:TableRow>
                        <asp:TableCell runat="server" ID="UsernameCell" ColumnSpan="2">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ID="ProfilePicCell" runat="server" HorizontalAlign="Center" CssClass="w-25 h-25" ColumnSpan="2">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>First Name:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="FNameCell" runat="server">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>Last Name:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="LNameCell" runat="server">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>Email:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="EmailCell" runat="server">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>First Name:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="CNumber" runat="server">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>Date Of Birth:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="DOBCell" runat="server">
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button Text="Add new Booking" runat="server" ID="ABooking" CssClass="btn btn-outline-primary" OnClick="ABooking_Click"/>

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5>Current Bookings</h5>
                            <asp:Table runat="server" ID="CBookings" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px" >

                </asp:Table>
                
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                           <%--  <h5>Previous Bookings</h5>
                            <asp:Table runat="server" ID="PBookings" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table> --%>
                
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>
</asp:Content>
