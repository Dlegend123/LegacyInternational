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
        public int CreateBooking(int Flight_id, string Name, string DOB, int num)//Creates a flight booking
        {
            Random rnd = new Random();
            int ascii_index = rnd.Next(65, 91); //ASCII character codes 65-90
            char myRandomUpperCase = Convert.ToChar(ascii_index); //produces any char A-Z
            JTBDBModel JTBDBModel = new JTBDBModel();
            var bookflight = new bookflight()
            {
                booking_id = JTBDBModel.bookflights.AsEnumerable().Count() + 1,
                flight_id = Flight_id,
                num_of_adults = num,
                username = Name,
                dob = DOB,
                seat_num = JTBDBModel.bookflights.AsEnumerable().Count().ToString() + myRandomUpperCase.ToString()
            };
            JTBDBModel.bookflights.Add(bookflight);
              return JTBDBModel.SaveChanges();
            /*
            using (SqlConnection conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["JTBDBConnectionString"].ConnectionString
            })
            {
                using (var sqlCommand = new SqlCommand("INSERT INTO bookflight([booking_id],[flight_id],[username],[email],[dob],[seat_num],[num_of_adults]) Values(@booking_id,@flight_id,@u_name,@e_mail,@dob,@seat_num,@num_of_adults)", conn))
                {
                    conn.Open();
                    sqlCommand.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookflight.booking_id;
                    sqlCommand.Parameters.Add("@flight_id", SqlDbType.Int).Value = bookflight.flight_id;
                    sqlCommand.Parameters.Add("@u_name", SqlDbType.NVarChar,50).Value = Name;
                    sqlCommand.Parameters.Add("@e_mail", SqlDbType.NVarChar).Value = bookflight.email;
                    sqlCommand.Parameters.Add("@dob", SqlDbType.NVarChar,50).Value = DOB;
                    sqlCommand.Parameters.Add("@seat_num", SqlDbType.NVarChar,50).Value = bookflight.seat_num;
                    sqlCommand.Parameters.Add("@num_of_adults", SqlDbType.Int).Value = bookflight.num_of_adults;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            */
            //JTBDBModel.Database.ExecuteSqlCommand("Insert into bookflight(booking_id,flight_id,username,email,dob,seat_num,num_of_adults) Values(" + bookflight.booking_id + "," + bookflight.flight_id + ",'" + bookflight.username + "','" + bookflight.email + "','" + bookflight.dob + "','" + bookflight.seat_num + "'," + bookflight.num_of_adults.ToString() + ");");
        }
    }
}
