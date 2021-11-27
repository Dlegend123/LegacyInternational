<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VacationBookings.aspx.cs" Inherits="LegacyInternational.VacationBookings" EnableEventValidation="false"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width:fit-content;min-width:70vw">
        <br />
        <br />
    <asp:Table runat="server" CssClass = "table table-dark table-striped table-bordered container">
        <asp:TableHeaderRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="ErrorMess" ForeColor="Red" runat="server" Visible="false">
                    The date should be in the format 'mm/dd/yyyy'.
                </asp:Label>
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
           <asp:TableCell ColumnSpan="2">
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
            <asp:TableCell ColumnSpan="2">
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
            <asp:TableCell runat="server" ColumnSpan="2">
                <h5>
                    Number of Adults
                </h5>
                <asp:TextBox runat="server" ID="NAdults" TextMode="Number" Text="0"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
           <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
  
               <asp:Button CssClass="btn btn-outline-primary" runat="server" Text="Search" ID="SearchSubmit" OnClick="SearchSubmit_Click" />

           </asp:TableCell>
       </asp:TableRow>

        </asp:Table>
            
        <br />
        <br />
        </div>
</asp:Content>
