<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacationBookings.aspx.cs" Inherits="LegacyInternational.VacationBookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="City"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="Country"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell>
                <asp:TextBox runat="server" ID="ACity"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="ACountry"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="SDate"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="EDate"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="NAdults"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow BorderStyle="Solid" BorderWidth="3px">
           <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" ColumnSpan="2" >
               
               <asp:TextBox runat="server" ID="SearchInput"></asp:TextBox>
               &nbsp;
               <asp:Button CssClass="btn btn-outline-warning" runat="server" Text="Search" ID="SearchSubmit" OnClick="SearchSubmit_Click" />

           </asp:TableCell>
            <asp:TableCell>

            </asp:TableCell>
       </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
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
