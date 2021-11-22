<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacationBookings.aspx.cs" Inherits="LegacyInternational.VacationBookings" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width:fit-content;min-width:70vw">
        <br />
        <br />
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableHeaderRow>
            <asp:TableCell ColumnSpan="2">
                
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
           <asp:TableCell>
               <h5>
                   Departure Location
               </h5>
                <asp:TextBox runat="server" ID="ACity" placeholder="City"></asp:TextBox>
               <br />
               <asp:TextBox runat="server" ID="ACountry" placeholder="Country"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableHeaderRow>
            <asp:TableCell ColumnSpan="2">
                
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>
                    Destination
                </h5>
                <asp:TextBox runat="server" ID="City" placeholder="City"></asp:TextBox>
                <br />
                <asp:TextBox runat="server" ID="Country" placeholder="Country"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <h5>
                    Start Date
                </h5>
                <asp:TextBox runat="server" ID="SDate" placeholder="mm/dd/yyyy"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <h5>
                    End Date
                </h5>
                <asp:TextBox runat="server" ID="EDate" placeholder="mm/dd/yyyy"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server">
                <asp:TextBox runat="server" ID="NAdults" placeholder="Number Of Adults" TextMode="Number"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell HorizontalAlign="Center">
  
               <asp:Button CssClass="btn btn-outline-primary" runat="server" Text="Search" ID="SearchSubmit" OnClick="SearchSubmit_Click" />

           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Departure Flights</h5>
                <asp:Table runat="server" ID="DepartureFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Return Flights</h5>
                <asp:Table runat="server" ID="ReturnFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >
                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                <asp:Table runat="server" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" BorderWidth="3px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <h5 style="text-align:left">Cruises and Rooms</h5>
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
