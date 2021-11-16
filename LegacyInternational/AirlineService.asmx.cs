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
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<Flight> Flights(string Country, string City, string Date)
        {
            List<Flight> Flights = new List<Flight>();
            return Flights;
        }
        [WebMethod]
        public bool CreateBooking(string Flight_id, string Name, string DOB)
        {
            List<Flight> Flights = new List<Flight>();
            return true;
        }
    }
}
