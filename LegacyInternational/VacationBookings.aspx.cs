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
        ApplicationUser user;
        List<cruiselist> CruiseList;

        protected void Page_Load(object sender, EventArgs e)
        {

            user = Session["user"] as ApplicationUser;
            if (!Request.IsSecureConnection)//Forces securelink if the link isn't currently secure
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "VacationBookings.aspx";
                Response.Redirect(url);
            }
            JTBDBModel = new JTBDBModel();
            count = 0;
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
                Session["EDate"] = EDate.Text;
                Session["SDate"] = SDate.Text;
                ErrorMess.Visible = false;
                //Departure Flights
                var t = DataCollect(1);
                Session["DF"] = t;
                //Return Flights
                var q = DataCollect(2);
                Session["RF"] = q;
                //Cruises and Room types
                var m = DataCollect();
                Session["CR"]=m;
                Session["count"] = count;
                Response.Redirect("~/SearchResults.aspx", true);
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
        
    }
}