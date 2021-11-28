using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using LegacyInternational.AirlineServiceRef;
using LegacyInternational.CruiseServiceRef;
using System.Web.UI.WebControls;
namespace LegacyInternational
{
    public partial class VacationBookings : System.Web.UI.Page
    {
        JTBDBModel JTBDBModel;
        int count;
        CruiseServiceRef.CruiseService cruiseService1;
        List<cruiselist> CruiseList;
        int Increment;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)//Forces securelink if the link isn't currently secure
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "VacationBookings.aspx";
                Response.Redirect(url);
            }
            Increment = 0;
            CruiseServiceRef.CruiseService cruiseService1 = new CruiseServiceRef.CruiseService();
            cruiseService1.RoomTypes(SDate.Text, EDate.Text).ToList().ForEach(x => DropDownItems(x));
            if(RoomTypes.Items.Count>0)
                RoomTypes.Items[0].Selected = true;
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
            JTBDBModel = new JTBDBModel();
            count = 0;
        }
        public void DropDownItems(string rType)
        {
            RoomTypes.Items.Add(new ListItem(rType));
        }
        bool ValidDate(string Date)//Validates the date
        {
            bool validDate = DateTime.TryParseExact(
                Date,
                "MM/dd/yyyy",
                DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.None,
                out DateTime scheduleDate);
            return validDate;
        }
        protected void SearchSubmit_Click(object sender, EventArgs e)//Search based on user input
        {
            if ((!string.IsNullOrEmpty(SDate.Text) || !string.IsNullOrEmpty(EDate.Text)) && (!ValidDate(SDate.Text) && !ValidDate(EDate.Text)))
            {
                ErrorMess.Visible = true;
            }
            else
            {
                var t = DataCollect(1);
                Session["DF"] = t;
                //Return Flights
                var q = DataCollect(2);
                Session["RF"] = q;
                //Cruises and Room types
                var m = DataCollect();
                Session["CR"] = m;
                Response.Redirect("~/VacationBookings.aspx", false);
            }
        }
        List<cruiselist> DataCollect()//Retrieves cruises from database
        {
            List<portlist> portlists = new List<portlist>();
            if (string.IsNullOrEmpty(Country.Text) || string.IsNullOrEmpty(City.Text))
            {
                if (string.IsNullOrEmpty(Country.Text) && string.IsNullOrEmpty(City.Text))
                    portlists = JTBDBModel.portlists.ToList();
                else
                {
                    if (string.IsNullOrEmpty(Country.Text))
                        portlists = JTBDBModel.portlists.Where(x => x.location.city == City.Text).ToList();
                    else
                        portlists = JTBDBModel.portlists.Where(x => x.location.country == Country.Text).ToList();
                }
            }
            else
                portlists = JTBDBModel.portlists.Where(x => x.location.country == Country.Text && x.location.city == City.Text).ToList();
            count = string.IsNullOrEmpty(NAdults.Text) ? 0 : Int32.Parse(NAdults.Text);
            if (string.IsNullOrEmpty(SDate.Text) || string.IsNullOrEmpty(EDate.Text))
            {
                if (string.IsNullOrEmpty(SDate.Text) && string.IsNullOrEmpty(EDate.Text))
                    CruiseList = JTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(b => b.num_of_adults >= count) && portlists.Any(l => l.port_id == x.departure_port_id))
                .ToList();
                else
                {
                    if (string.IsNullOrEmpty(SDate.Text))
                        CruiseList = JTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(b => b.num_of_adults >= count) && portlists.Any(l => l.port_id == x.departure_port_id && DateTime.ParseExact(x.end_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(EDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture)))
                    .ToList();
                    else
                        CruiseList = JTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(b => b.num_of_adults >= count) && portlists.Any(l => l.port_id == x.departure_port_id && DateTime.ParseExact(x.start_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(SDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture)))
                    .ToList();
                }
            }
            else
                CruiseList = JTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(b => b.num_of_adults >= count) && portlists.Any(l => l.port_id == x.departure_port_id && DateTime.ParseExact(x.start_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(SDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture) && DateTime.ParseExact(x.end_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(EDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture)))
                    .ToList();
            return CruiseList;
        }
        List<flightlist> DataCollect(int y)//Retrieve flights from database
        {
            List<airportlist> Airports = new List<airportlist>();
            if (y == 1)
            {
                if (string.IsNullOrEmpty(ACountry.Text) || string.IsNullOrEmpty(ACity.Text) || string.IsNullOrEmpty(SDate.Text))
                {
                    if (string.IsNullOrEmpty(ACountry.Text) && string.IsNullOrEmpty(ACity.Text) && string.IsNullOrEmpty(SDate.Text))
                        return JTBDBModel.flightlists.AsEnumerable().ToList();
                    else
                    {
                        if (string.IsNullOrEmpty(ACountry.Text) && string.IsNullOrEmpty(ACity.Text))
                        {
                            Airports = JTBDBModel.airportlists.ToList();
                            return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && DateTime.ParseExact(x.departure_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(SDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(ACountry.Text) && string.IsNullOrEmpty(SDate.Text))
                            {
                                Airports = JTBDBModel.airportlists.Where(x => x.location.city == ACity.Text).ToList();
                                return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id)).ToList();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ACity.Text) && string.IsNullOrEmpty(SDate.Text))
                                {
                                    Airports = JTBDBModel.airportlists.Where(x => x.location.country == ACountry.Text).ToList();
                                    return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id)).ToList();
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(ACountry.Text))
                                        Airports = JTBDBModel.airportlists.Where(x => x.location.city == ACity.Text).ToList();
                                    else
                                    {
                                        if (string.IsNullOrEmpty(ACity.Text))
                                            Airports = JTBDBModel.airportlists.Where(x => x.location.country == ACountry.Text).ToList();
                                    }
                                    return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && DateTime.ParseExact(x.departure_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(SDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
                                }
                            }
                        }
                    }
                }
                return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && DateTime.ParseExact(x.departure_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(SDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(Country.Text) || string.IsNullOrEmpty(City.Text) || string.IsNullOrEmpty(EDate.Text))
                {
                    if (string.IsNullOrEmpty(Country.Text) && string.IsNullOrEmpty(City.Text) && string.IsNullOrEmpty(EDate.Text))
                        return JTBDBModel.flightlists.AsEnumerable().ToList();
                    else
                    {
                        if (string.IsNullOrEmpty(Country.Text) && string.IsNullOrEmpty(City.Text))
                        {
                            Airports = JTBDBModel.airportlists.ToList();
                            return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id && DateTime.ParseExact(x.arrival_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(EDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Country.Text) && string.IsNullOrEmpty(EDate.Text))
                            {
                                Airports = JTBDBModel.airportlists.Where(x => x.location.city == City.Text).ToList();
                                return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id)).ToList();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(City.Text) && string.IsNullOrEmpty(EDate.Text))
                                {
                                    Airports = JTBDBModel.airportlists.Where(x => x.location.country == Country.Text).ToList();
                                    return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id)).ToList();
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(Country.Text))
                                        Airports = JTBDBModel.airportlists.Where(x => x.location.city == City.Text).ToList();
                                    else
                                    {
                                        if (string.IsNullOrEmpty(City.Text))
                                            Airports = JTBDBModel.airportlists.Where(x => x.location.country == Country.Text).ToList();
                                    }
                                    return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id && DateTime.ParseExact(x.arrival_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(EDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
                                }
                            }
                        }
                    }
                }
                return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id && DateTime.ParseExact(x.arrival_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(EDate.Text, "d", System.Globalization.CultureInfo.InvariantCulture))).ToList();
            }
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
                    PostBackUrl = "~/Account/Profile.aspx?id=" + (Increment - 1).ToString() + ":" + p.flight_id.ToString() + "&count=" + count.ToString()
                };

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
            else
            {
                if (k == 1)//Return Flights
                {
                    flightlist p = x as flightlist;
                    Button button = new Button()
                    {
                        CssClass = "btn btn-outline-primary",
                        Text = "Select",
                        ID = p.flight_id.ToString() + ":" + (Increment++).ToString(),
                        CausesValidation = false,
                        UseSubmitBehavior = false,
                        PostBackUrl = "~/Account/Profile.aspx?id=" + (Increment - 1).ToString() + ":" + p.flight_id.ToString() + "&count=" + count.ToString()
                    };

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
                else
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
                            UseSubmitBehavior = false,
                            PostBackUrl = "~/Account/Profile.aspx?id=" + (Increment - 1).ToString() + ":" + i.room_num.ToString()
                        };

                        TableRow tableRow1 = new TableRow();
                        TableCell tableCell6 = new TableCell();
                        tableCell6.Controls.Add(new LiteralControl("Room #: " + i.room_num + "<br />"));
                        tableCell6.Controls.Add(new LiteralControl("Room Type: " + i.type + "<br />"));
                        button1.ID += ":" + p.cruise_id.ToString();
                        button1.PostBackUrl += ":" + p.cruise_id.ToString() + "&SDate=" + SDate.Text + "&EDate=" + EDate.Text + "&cruise_id=" + p.cruise_id.ToString();
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
        void AddCruiseByType(object x)//Adds cruise info and flight info to tables
        {

            TableRow tableRow = new TableRow
            {
                HorizontalAlign = HorizontalAlign.Justify,
                BorderStyle = BorderStyle.Solid,
                BorderWidth = Unit.Pixel(3)
            };

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
                if (i.type == RoomTypes.SelectedItem.Text)
                {
                    Button button1 = new Button()
                    {
                        CssClass = "btn btn-outline-primary",
                        Text = "Select",
                        ID = (Increment++).ToString() + ":" + i.room_num.ToString(),
                        CausesValidation = false,
                        UseSubmitBehavior = false,
                        PostBackUrl = "~/Account/Profile.aspx?id=" + (Increment - 1).ToString() + ":" + i.room_num.ToString()
                    };

                    TableRow tableRow1 = new TableRow();
                    TableCell tableCell6 = new TableCell();
                    tableCell6.Controls.Add(new LiteralControl("Room #: " + i.room_num + "<br />"));
                    tableCell6.Controls.Add(new LiteralControl("Room Type: " + i.type + "<br />"));
                    button1.ID += ":" + p.cruise_id.ToString();
                    button1.PostBackUrl += ":" + p.cruise_id.ToString() + "&SDate=" + SDate.Text + "&EDate=" + EDate.Text + "&cruise_id=" + p.cruise_id.ToString();
                    tableCell6.Controls.Add(new LiteralControl("Number Of Adults: " + i.num_of_adults + "<br />"));
                    tableCell6.Controls.Add(button1);


                    tableRow1.Cells.Add(tableCell6);
                    table.Rows.Add(tableRow1);
                }
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
            Cruises.Rows.Add(tableRow);
        }
        protected void RoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cruises.Rows.Clear();
            (Session["CR"] as List<cruiselist>).ForEach(p => AddCruiseByType(p)); ;
        }
    }
}