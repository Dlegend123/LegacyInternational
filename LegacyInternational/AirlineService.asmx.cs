using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Data.Entity.Migrations;

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
                //booking_id = JTBDBModel.bookflights.AsEnumerable().ToList().Count()+1
            };
            bookflight.num_of_adults = num;
            bookflight.seat_num = JTBDBModel.bookflights.AsEnumerable().ToList().Last().seat_num+"6";
            //JTBDBModel.bookflights.Add(bookflight);
            //JTBDBModel.Entry(bookflight).State = EntityState.Added;
            using(var db = new JTBDBModel())
            {
                db.Set<bookflight>().Add(bookflight);
                db.SaveChanges();
            }
            
            //JTBDBModel.Database.ExecuteSqlCommand("Insert into bookflight(booking_id,flight_id,username,email,dob,seat_num,num_of_adults) Values(" + bookflight.booking_id + "," + bookflight.flight_id + ",'" + bookflight.username + "','" + bookflight.email + "','" + bookflight.dob + "','" + bookflight.seat_num + "'," + bookflight.num_of_adults.ToString() + ");");
        }
    }
}
