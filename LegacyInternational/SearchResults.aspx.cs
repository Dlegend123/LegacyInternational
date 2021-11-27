using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LegacyInternational
{
    public partial class SearchResults : System.Web.UI.Page
    {
        JTBDBModel JTBDBModel;
        List<cruiselist> CruiseList;
        string EDate;
        string SDate;
        ApplicationUser user;
        int count;
        int Increment;
        protected void Page_Load(object sender, EventArgs e)
        {
            Increment = 0;
            JTBDBModel = new JTBDBModel();
            user = Session["user"] as ApplicationUser;
            count = (int)Session["count"];
            SDate = Session["SDate"] as string;
            EDate = Session["EDate"] as string;

            if (Session["DF"] != null)
            {
                (Session["DF"] as List<flightlist>).ForEach(p => QuickFunction(p, 0, DepartureFlights));
                (Session["RF"] as List<flightlist>).ForEach(p => QuickFunction(p, 1, ReturnFlights));
                (Session["CR"] as List<cruiselist>).ForEach(p => QuickFunction(p, 2, Cruises));

                if (Cruises.Rows.Count == 0 || ReturnFlights.Rows.Count == 0 || DepartureFlights.Rows.Count == 0)
                {
                    if (Cruises.Rows.Count == 0)
                    {
                        TableCell tableCell = new TableCell();
                        TableRow tableRow = new TableRow();
                        tableCell.Controls.Add(new LiteralControl("<br/>No Results Found<br/>"));
                        tableRow.Cells.Add(tableCell);
                        Cruises.Rows.Add(tableRow);

                    }
                    if (ReturnFlights.Rows.Count == 0)
                    {
                        TableCell tableCell = new TableCell();
                        TableRow tableRow = new TableRow();
                        tableCell.Controls.Add(new LiteralControl("<br /> No Results Found <br />"));
                        tableRow.Cells.Add(tableCell);
                        ReturnFlights.Rows.Add(tableRow);
                    }
                    if (DepartureFlights.Rows.Count == 0)
                    {
                        TableCell tableCell = new TableCell();
                        TableRow tableRow = new TableRow();
                        tableCell.Controls.Add(new LiteralControl("<br /> No Results Found <br />"));
                        tableRow.Cells.Add(tableCell);
                        DepartureFlights.Rows.Add(tableRow);
                    }
                }
            }
        }
        protected void DFSelect_Click(object sender, EventArgs e)//Event handler for buttons assigned to flight
        {
            Button button = sender as Button;
            TableRow tableRow = button.Parent.Parent as TableRow;
            AirlineServiceRef.AirlineService airlineService = new AirlineServiceRef.AirlineService();
            TableCell tableCell = tableRow.Cells[0];
            airlineService.CreateBooking(Int32.Parse(button.ID.Split(':')[0]), JTBDBModel.users.Where(x => x.email == user.UserName).First().username, JTBDBModel.users.Where(x => x.email == user.UserName).First().dob, count);
            Response.Redirect("~/Default.aspx", false);
            JTBDBModel = new JTBDBModel();
        }

        protected void CRSelect_Click(object sender, EventArgs e)//event handler for buttons assigned to cruises
        {
            Button button = sender as Button;
            CruiseService cruiseService = new CruiseService();
            TableCell tableCell = button.Parent as TableCell;
            var Cruise = CruiseList.Where(x => x.cruiserooms.Any(v => v.room_num == Int32.Parse(button.ID.Split(':')[0])) && x.cruise_id == Int32.Parse(button.ID.Split(':')[1])).First();
            CruiseServiceRef.CruiseService cruiseService1 = new CruiseServiceRef.CruiseService();
            cruiseService1.CreateBooking(JTBDBModel.users.Where(x => x.email == user.UserName).First().username,
                string.IsNullOrEmpty(SDate) ? Cruise.start_datetime : SDate,
                string.IsNullOrEmpty(EDate) ? Cruise.end_datetime : EDate,
                Int32.Parse(button.ID.Split(':')[2]),
                JTBDBModel.bookcruises.AsEnumerable().Count() + 1,
                Int32.Parse(button.ID.Split(':')[1]));
            Response.Redirect("~/Default.aspx", false);
            JTBDBModel = new JTBDBModel();
        }
        void QuickFunction(object x, int k, Table AddTo)//Adds cruise info and flight info to tables
        {

            TableRow tableRow = new TableRow
            {
                HorizontalAlign = HorizontalAlign.Justify,
                BorderStyle = BorderStyle.Solid,
                BorderWidth = Unit.Pixel(3)
            };

            if (k == 0)//Departure Flight
            {
                flightlist p = x as flightlist;
                Button button = new Button()
                {
                    CssClass = "btn btn-outline-primary",
                    Text = "Select",
                    ID = p.flight_id.ToString() + ":" + (Increment++).ToString(),
                    CausesValidation = false,
                    UseSubmitBehavior = false,
                };
                button.Click += DFSelect_Click;

                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("Flight ID: " + p.flight_id));

                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Departure Airport ID: " + p.departure_airport_id));
                TableCell tableCell2 = new TableCell();
                tableCell2.Controls.Add(new LiteralControl("Departure Date/Time: " + p.departure_datetime));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("Airline ID: " + p.airlinelist.airline_id));
                TableCell tableCell4 = new TableCell();
                tableCell4.Controls.Add(new LiteralControl("Airline: " + p.airlinelist.airline_name));
                TableCell tableCell6 = new TableCell();
                tableCell6.Controls.Add(new LiteralControl("Price: $" + p.cost));
                tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
                tableRow.Cells.Add(tableCell);
                tableRow.Cells.Add(tableCell1);
                tableRow.Cells.Add(tableCell2);
                tableRow.Cells.Add(tableCell3);
                tableRow.Cells.Add(tableCell4);
                tableRow.Cells.Add(tableCell6);
                button.Visible = true;
                TableCell tableCell5 = new TableCell();
                tableCell5.Controls.Add(button);
                tableRow.Cells.Add(tableCell5);
                AddTo.Rows.Add(tableRow);
            }
            if (k == 1)//Return Flights
            {
                flightlist p = x as flightlist;
                Button button = new Button()
                {
                    CssClass = "btn btn-outline-primary",
                    Text = "Select",
                    ID = p.flight_id.ToString(),
                    CausesValidation = false,
                    UseSubmitBehavior = false
                };
                button.Command += DFSelect_Click;
                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("Flight ID: " + p.flight_id));

                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Arrival Airport ID: " + p.arrival_airport_id));
                TableCell tableCell2 = new TableCell();
                tableCell2.Controls.Add(new LiteralControl("Arrival Date/Time: " + p.arrival_datetime));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("Airline ID: " + p.airlinelist.airline_id));
                TableCell tableCell4 = new TableCell();
                tableCell4.Controls.Add(new LiteralControl("Airline: " + p.airlinelist.airline_name));
                TableCell tableCell6 = new TableCell();
                tableCell6.Controls.Add(new LiteralControl("Price: $" + p.cost));
                tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
                button.Visible = true;
                tableRow.Cells.Add(tableCell);
                tableRow.Cells.Add(tableCell1);
                tableRow.Cells.Add(tableCell2);
                tableRow.Cells.Add(tableCell3);
                tableRow.Cells.Add(tableCell4);
                tableRow.Cells.Add(tableCell6);
                TableCell tableCell5 = new TableCell();
                tableCell5.Controls.Add(button);
                tableRow.Cells.Add(tableCell5);
                AddTo.Rows.Add(tableRow);
            }

            if (k == 2)
            {//Cruises
                cruiselist p = x as cruiselist;
                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("Cruise ID: " + p.cruise_id));
                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Cruesline ID: " + p.cruiseline.cruiseline_id));
                TableCell tableCell2 = new TableCell();
                tableCell2.Controls.Add(new LiteralControl("Start Date/Time: " + p.start_datetime));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("End Date/Time: " + p.end_datetime));
                Table table = new Table
                {
                    CssClass = "table table-dark table-striped table-bordered"
                };
                TableCell tableCell4 = new TableCell();
                p.cruiserooms.ToList().ForEach(i =>
                {
                    Button button1 = new Button()
                    {
                        CssClass = "btn btn-outline-primary",
                        Text = "Select",
                        ID = (Increment++).ToString() + ":" + i.room_num.ToString(),
                        CausesValidation = false,
                        UseSubmitBehavior = false
                    };
                    button1.Click += CRSelect_Click;

                    TableRow tableRow1 = new TableRow();
                    TableCell tableCell6 = new TableCell();
                    tableCell6.Controls.Add(new LiteralControl("Room #: " + i.room_num + "<br />"));
                    tableCell6.Controls.Add(new LiteralControl("Room Type: " + i.type + "<br />"));
                    button1.ID += ":" + p.cruise_id.ToString();
                    button1.Visible = true;
                    tableCell6.Controls.Add(new LiteralControl("Number Of Adults: " + i.num_of_adults + "<br />"));
                    tableCell6.Controls.Add(button1);
                    tableRow1.Cells.Add(tableCell6);
                    table.Rows.Add(tableRow1);
                });
                tableCell4.Controls.Add(table);
                tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
                tableRow.Cells.Add(tableCell);
                tableRow.Cells.Add(tableCell1);
                tableRow.Cells.Add(tableCell2);
                tableRow.Cells.Add(tableCell3);
                tableRow.Cells.Add(tableCell4);
                AddTo.Rows.Add(tableRow);
            }
        }
    }
}