<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacationBookings.aspx.cs" Inherits="LegacyInternational.VacationBookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width:fit-content">
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
                    Period
                </h5>
                <asp:TextBox runat="server" ID="SDate" placeholder="Start Date"></asp:TextBox>
                 <br />
                <asp:TextBox runat="server" ID="EDate" placeholder="End Date"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="NAdults" placeholder="Number Of Adults"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell HorizontalAlign="Center">
  
               <asp:Button CssClass="btn btn-outline-primary" runat="server" Text="Search" ID="SearchSubmit" OnClick="SearchSubmit_Click" />

           </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Table runat="server" ID="DepartureFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                <asp:Table runat="server" ID="ReturnFlights" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>
                <asp:Table runat="server" ID="Cruises" CssClass="table table-dark table-striped table-bordered" BorderStyle="Solid" ForeColor="WhiteSmoke"  BorderWidth="3px" >

                </asp:Table>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        </div>
</asp:Content>
