using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LegacyInternational
{
    /// <summary>
    /// Summary description for AirlineService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AirlineService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<flightlist> Flights(string Country, string City, string Date)
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            List<airportlist> Airports = JTBDBModel.airportlists.Where(x => x.location.city == City && x.location.country == Country).ToList();

            return JTBDBModel.flightlists.Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && x.departure_datetime.Contains( Date))).ToList();
            
        }
        [WebMethod]
        public bool CreateBooking(int Flight_id, string Name, string DOB,int num)
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            bookflight bookflight = new bookflight();
            bookflight.username = Name;
            bookflight.dob = DOB;
            bookflight.flight_id = Flight_id;
            bookflight.booking_id=JTBDBModel.bookflights.Count();
            int sum = 0;
            bookflight.num_of_adults = num;
            JTBDBModel.bookflights.AsEnumerable().ToList().ForEach(x => sum += Int32.Parse(x.num_of_adults.ToString()));
            bookflight.seat_num = sum.ToString();
            JTBDBModel.bookflights.Add(bookflight);
            JTBDBModel.SaveChangesAsync();
            return true;
        }
    }
}
