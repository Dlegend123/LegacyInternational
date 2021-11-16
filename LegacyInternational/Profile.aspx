<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="LegacyInternational.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Table>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Table>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Username-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Profile Pic -->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--First Name-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Last Name -->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Email-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Contact Number-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Date of Birth-->
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Table>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button Text="Add new Booking" runat="server" />

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <!--Current Bookings-->
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <!--Previous Bookings-->
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>
</asp:Content>
