﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Services;
using LegacyInternational.Models;

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
        public List<string> RoomTypes(string CheckIn, string CheckOut)
        {
            JTBDBModel JTBDBModel = new JTBDBModel();
            List<string> RoomTypes= new List<string>();
            JTBDBModel.cruiselists.Where(x => x.start_datetime == CheckIn && x.end_datetime == CheckOut)
                .ToList().ForEach(l=>l.cruiserooms.ToList().ForEach(j=>RoomTypes.Add(j.type)));
            
            return RoomTypes;
        }
        [WebMethod]
        public void CreateBooking(bookcruise bookcruise)
        {
            using (var db = new JTBDBModel())
            {
                db.bookcruises.Add(bookcruise);
                db.SaveChangesAsync().Wait();
            }
        }
    }
}
