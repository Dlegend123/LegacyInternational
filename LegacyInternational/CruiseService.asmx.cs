using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using LegacyInternational.Models;

namespace LegacyInternational
{
    /// <summary>
    /// Summary description for CruiseService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CruiseService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> RoomTypes(string CheckIn, string CheckOut)//Function to get all roomtypes from database
        {
            JTBDBModel JTBDBModel = new JTBDBModel();// Creates and initializes a new instance of the JTBDBModel class
            List<string> RoomTypes = new List<string>();//Creates and initializes a new list to store the room types
            if (String.IsNullOrEmpty(CheckIn) || String.IsNullOrEmpty(CheckOut))//Finds room type based on the dates collected
            {
                if (String.IsNullOrEmpty(CheckIn) && String.IsNullOrEmpty(CheckOut))
                {
                    JTBDBModel.cruiselists.AsEnumerable().ToList().ForEach(l => l.cruiserooms.ToList().ForEach(j => RoomTypes.Add(j.type)));
                }
                else
                {
                    if (String.IsNullOrEmpty(CheckIn))
                    {
                        JTBDBModel.cruiselists.AsEnumerable().Where(x => DateTime.ParseExact(x.start_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(CheckIn, "d", System.Globalization.CultureInfo.InvariantCulture))
                .ToList().ForEach(l => l.cruiserooms.ToList().ForEach(j => RoomTypes.Add(j.type)));
                    }
                    else
                    {
                        JTBDBModel.cruiselists.AsEnumerable().Where(x => DateTime.ParseExact(x.end_datetime, "d", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(CheckOut, "d", System.Globalization.CultureInfo.InvariantCulture))
                .ToList().ForEach(l => l.cruiserooms.ToList().ForEach(j => RoomTypes.Add(j.type)));
                    }
                }
            }
            else
                JTBDBModel.cruiselists.AsEnumerable().Where(x => x.start_datetime == CheckIn && x.end_datetime == CheckOut)
                    .ToList().ForEach(l => l.cruiserooms.ToList().ForEach(j => RoomTypes.Add(j.type)));

            return RoomTypes;//returns the room types
        }
        [WebMethod]
        public void CreateBooking(string username,string check_in_date, string check_out_date, int cruise_id, int booking_id, int room_num)//Adds a new cruise booking to the database
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            bookcruise bookcruise = new bookcruise
            {
                username = username,
                check_in_date = check_in_date,
                check_out_date = check_out_date,
                cruise_id = cruise_id,
                booking_id = booking_id,
                room_num = room_num
            };
            JTBDBModel.bookcruises.Add(bookcruise);
            JTBDBModel.SaveChangesAsync().Wait();

        }
    }
}
