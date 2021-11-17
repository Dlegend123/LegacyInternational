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
            Model1 model1 = new Model1();
            List<airportlist> Airports = model1.airportlists.Where(x => x.location.city == City && x.location.country == Country).ToList();

            return model1.flightlists.Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && x.departure_datetime == Date)).ToList();
            
        }
        [WebMethod]
        public bool CreateBooking(int Flight_id, string Name, string DOB)
        {
            Model1 model1 = new Model1();
            bookflight bookflight = new bookflight();
            bookflight.username = Name;
            bookflight.dob = DOB;
            bookflight.flight_id = Flight_id;
            model1.bookflights.Add(bookflight);
            model1.SaveChangesAsync();
            return true;
        }
    }
}
