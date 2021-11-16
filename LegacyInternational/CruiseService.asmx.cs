using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LegacyInternational
{
    /// <summary>
    /// Summary description for CruiseService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CruiseService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<Cruise> Cruises()
        {
            List<Cruise> Cruises = new List<Cruise>();
            return Cruises;
        }
        [WebMethod]
        public bool CreateBooking(string RoomType, Cruise Cruise)
        {
            return true;
        }
    }
}
