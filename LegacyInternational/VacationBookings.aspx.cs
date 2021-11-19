﻿using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegacyInternational
{
    public partial class VacationBookings : System.Web.UI.Page
    {
        JTBDBModel JTBDBModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "VacationBookings.aspx";
                Response.Redirect(url);
            }
            JTBDBModel = new JTBDBModel();
        }
        protected void DFSelect_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            TableRow tableRow = button.Parent.Parent as TableRow;
            AirlineService airlineService = new AirlineService();
            ApplicationUser user = Session["User"] as ApplicationUser;
            TableCell tableCell = tableRow.Cells[0];
            var Result = airlineService.CreateBooking(Int32.Parse((tableCell.Controls[0] as LiteralControl).Text.Split(':')[1].Split('<')[0].Trim()), user.UserName, JTBDBModel.users.Where(x => x.username == user.UserName).First().dob);
        }

        protected void CRSelect_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            CruiseService cruiseService = new CruiseService();
            ApplicationUser user = Session["User"] as ApplicationUser;
            TableCell tableCell = button.Parent as TableCell;
            bookcruise bookcruise = new bookcruise
            {
                username = user.UserName,
                check_in_date = SDate.Text,
                check_out_date = EDate.Text
            };
            bookcruise.cruiseroom.room_num = Int32.Parse((tableCell.Controls[0] as LiteralControl).Text.Split(':')[1].Trim());
            bookcruise.cruiseroom.type = (tableCell.Controls[1] as LiteralControl).Text.Split(':')[1].Trim();
            cruiseService.CreateBooking(bookcruise);
        }
        protected void SearchSubmit_Click(object sender, EventArgs e)
        {
            //Departure Flights
            DepartureFlights.Rows.Clear();
            DataCollect(1).ForEach(p => QuickFunction(p,0, DepartureFlights));
            //Return Flights
            ReturnFlights.Rows.Clear();
            DataCollect(2).ForEach(p => QuickFunction(p, 1, ReturnFlights));
            //Cruises and Room types
            Cruises.Rows.Clear();
            DataCollect().ForEach(p => QuickFunction(p, 2, Cruises));
        }
        List<cruiselist> DataCollect()
        {
            List<cruiselist> Cruises;
            List<portlist> portlists = JTBDBModel.portlists.Where(x=>x.location.country==ACountry.Text&&x.location.city==ACity.Text).ToList();
            int count= Int32.Parse(NAdults.Text);
            Cruises = JTBDBModel.cruiselists.Where(x =>x.cruiserooms.Any(b=>b.num_of_adults>= count)&& portlists.Any(l=>l.port_id==x.departure_port_id && x.start_datetime==SDate.Text&&x.end_datetime==EDate.Text))
                .ToList();
           
            return Cruises;
        }
        List<flightlist> DataCollect(int y)
        {
            List<airportlist> Airports = JTBDBModel.airportlists.Where(x => x.location.city == City.Text && x.location.country == Country.Text).ToList();
            if (y == 1)
                return JTBDBModel.flightlists.Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id &&x.departure_datetime==SDate.Text)).ToList();
            else
                return JTBDBModel.flightlists.Where(x => Airports.Any(l => l.airport_id == x.arrival_airport_id &&x.arrival_datetime==EDate.Text)).ToList();
        }
        void QuickFunction(object x,int k,Table AddTo)
        {

            Button button = new Button();
            TableRow tableRow = new TableRow
            {
                HorizontalAlign = HorizontalAlign.Justify,
                BorderStyle = BorderStyle.Solid,
                BorderWidth = Unit.Pixel(3)
            };

            if (k==0)//Departure Flight
            {
                flightlist p = x as flightlist;
                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("<br /> Flight ID: " + p.flight_id + "<br />"));
                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Departure Airport ID: " + p.departure_airport_id + "<br />"));
                TableCell tableCell2 = new TableCell();
                tableCell2.Controls.Add(new LiteralControl("Departure Date/Time: " + p.departure_datetime + "<br />"));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("Airline ID: " + p.airlinelist.airline_id + "<br />"));
                TableCell tableCell4 = new TableCell();
                tableCell4.Controls.Add(new LiteralControl("Airline: " + p.airlinelist.airline_name + "<br />"));
                tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
                tableRow.Cells.Add(tableCell);
                tableRow.Cells.Add(tableCell1);
                tableRow.Cells.Add(tableCell2);
                tableRow.Cells.Add(tableCell3);
                tableRow.Cells.Add(tableCell4);
                button.Click += DFSelect_Click;
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
                    TableCell tableCell = new TableCell();
                    tableCell.Controls.Add(new LiteralControl("<br /> Flight ID: " + p.flight_id + "<br />"));
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Controls.Add(new LiteralControl("Arrival Airport ID: " + p.arrival_airport_id + "<br />"));
                    TableCell tableCell2 = new TableCell();
                    tableCell2.Controls.Add(new LiteralControl("Arrival Date/Time: " + p.arrival_datetime + "<br />"));
                    TableCell tableCell3 = new TableCell();
                    tableCell3.Controls.Add(new LiteralControl("Airline ID: " + p.airlinelist.airline_id + "<br />"));
                    TableCell tableCell4 = new TableCell();
                    tableCell4.Controls.Add(new LiteralControl("Airline: " + p.airlinelist.airline_name + "<br />"));
                    tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                    tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                    tableCell.HorizontalAlign = HorizontalAlign.Center;
                    tableRow.Visible = true;
                    tableRow.Cells.Add(tableCell);
                    tableRow.Cells.Add(tableCell1);
                    tableRow.Cells.Add(tableCell2);
                    tableRow.Cells.Add(tableCell3);
                    tableRow.Cells.Add(tableCell4);
                    button.Click += DFSelect_Click;
                    TableCell tableCell5 = new TableCell();
                    tableCell5.Controls.Add(button);
                    tableRow.Cells.Add(tableCell5);
                    AddTo.Rows.Add(tableRow);
                }
                else
                {//Cruises
                    cruiselist p = x as cruiselist;
                    TableCell tableCell = new TableCell();
                    tableCell.Controls.Add(new LiteralControl("<br /> Cruise ID: " + p.cruise_id + "<br />"));
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Controls.Add(new LiteralControl("Cruesline ID: " + p.cruiseline.cruiseline_id + "<br />"));
                    TableCell tableCell2 = new TableCell();
                    tableCell2.Controls.Add(new LiteralControl("Start Date/Time: " + p.start_datetime + "<br />"));
                    TableCell tableCell3 = new TableCell();
                    tableCell3.Controls.Add(new LiteralControl("End Date/Time: " + p.end_datetime + "<br />"));
                    Table table = new Table
                    {
                        CssClass = "table table-dark table-striped table-bordered"
                    };
                    TableCell tableCell4 = new TableCell();
                    p.cruiserooms.ToList().ForEach(i =>
                    {
                        Button button1 = new Button();
                        button1.Click += CRSelect_Click;
                        TableRow tableRow1 = new TableRow();
                        TableCell tableCell6 = new TableCell();
                        tableCell6.Controls.Add(new LiteralControl("Room #: " + i.room_num + "<br />"));
                        tableCell6.Controls.Add(new LiteralControl("Room Type: " + i.type + "<br />"));
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
}