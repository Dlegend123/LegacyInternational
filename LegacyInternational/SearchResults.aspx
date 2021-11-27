<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="LegacyInternational.SearchResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width:fit-content;min-width:70vw">
        <br />
        <br />
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Departure Flights</h5>
                            <asp:Button ID="tempDF" runat="server" OnClick="DFSelect_Click" Visible="false" CssClass = "btn btn-outline-primary" />
                <asp:Table runat="server" ID="DepartureFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Return Flights</h5>
                            <asp:Button ID="tempRF" runat="server" OnClick="DFSelect_Click" Visible="false" CssClass = "btn btn-outline-primary"/>
                <asp:Table runat="server" ID="ReturnFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Cruises and Rooms</h5>
                            <asp:Button ID="tempCR" runat="server" OnClick="CRSelect_Click" Visible="false" CssClass = "btn btn-outline-primary"/>
                            <asp:Table runat="server" ID="Cruises" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                            </asp:Table>
                         </asp:TableCell>
                     </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            
        </asp:TableRow>
        </asp:Table>
            
        <br />
        <br />
        </div>
</asp:Content>
