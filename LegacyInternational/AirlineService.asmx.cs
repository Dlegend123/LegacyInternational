using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public void CreateBooking(int Flight_id, string Name, string DOB,int num)
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            bookflight bookflight = new bookflight
            {
                username = Name,
                dob = DOB,
                flight_id = Flight_id,
                booking_id = JTBDBModel.bookflights.AsEnumerable().ToList().Count()+1
            };
            SqlConnection conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["JTBDBConnectionString"].ConnectionString
            };
            bookflight.num_of_adults = num;
            bookflight.seat_num = JTBDBModel.bookflights.AsEnumerable().ToList().Last().seat_num+"6";
            JTBDBModel.bookflights.Add(bookflight);
            JTBDBModel.SaveChangesAsync().Wait();
        }
    }
}
