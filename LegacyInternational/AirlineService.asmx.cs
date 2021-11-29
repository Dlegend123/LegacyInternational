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
using System.Web.Script.Serialization;
using System.Data;

namespace LegacyInternational
{
    /// <summary>
    /// Summary description for AirlineService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AirlineService : System.Web.Services.WebService
    {
        /*
        [WebMethod]
        public List<flightlist> Flights(string Country, string City, string Date)//Returns flights that match the criteria
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            List<airportlist> Airports = JTBDBModel.airportlists.AsEnumerable().Where(x => x.location.city == City && x.location.country == Country).ToList();

            return JTBDBModel.flightlists.AsEnumerable().Where(x => Airports.Any(l => l.airport_id == x.departure_airport_id && x.departure_datetime.Contains( Date))).ToList();
        }*/
        [WebMethod]
        public void CreateBooking(int Flight_id, string Name, string DOB, int num)//Adds a new flight booking to the database
        {
            Random rnd = new Random();//Initializes a new Instance of the Random Class
            int ascii_index = rnd.Next(65, 91); //ASCII character codes 65-90
            char myRandomUpperCase = Convert.ToChar(ascii_index); //produces any char A-Z
            JTBDBModel JTBDBModel = new JTBDBModel();// Creates and initializes a new instance of the JTBDBModel class
            var bookflight = new bookflight()// Creates new flight booking 
            {
                booking_id = JTBDBModel.bookflights.AsEnumerable().Count() + 1,
                flight_id = Flight_id,
                num_of_adults = num,
                username = Name,
                dob = DOB,
                seat_num = JTBDBModel.bookflights.AsEnumerable().Count().ToString() + myRandomUpperCase.ToString()
            };
            JTBDBModel.bookflights.Add(bookflight);//Adds new flight booking to the collection
            JTBDBModel.SaveChangesAsync().Wait();//Save changes to database
        }
    }
}
