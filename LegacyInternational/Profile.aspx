<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LegacyInternational.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <br />
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell runat="server" ID="UsernameCell">
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ID="ProfilePicCell" runat="server">
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
                            <!--Contact Number-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell runat="server">
                            <h5>Date Of Birth:</h5>
                        </asp:TableCell>
                        <asp:TableCell ID="DOBCell" runat="server">
                            <!--Date of Birth-->
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
                            <asp:Table runat="server" ID="CBookings" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table runat="server" ID="PBookings" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                
                        </asp:TableCell>
                        <!--Previous Bookings-->
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>
</asp:Content>
