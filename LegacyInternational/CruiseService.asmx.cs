﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
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
        public void CreateBooking(bookcruise bookcruise)//Adds a cruise booking to the database
        {
            using (var JTBDBModel = new JTBDBModel())
            {
                JTBDBModel.bookcruises.Add(bookcruise);
                
                JTBDBModel.SaveChanges();
            }
            /* using (SqlConnection conn = new SqlConnection
             {
                 ConnectionString = ConfigurationManager.ConnectionStrings["JTBDBConnectionString"].ConnectionString
             })
             {
                 using (var sqlCommand = new SqlCommand("INSERT INTO bookcruise(booking_id,cruise_id,username,check_in_date,check_out_date,room_num,num_of_adults) Values(@booking_id,@cruise_id,@username,@check_in_date,@check_out_date,@room_num,@num_of_adults)", conn))
                 {
                     sqlCommand.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookcruise.booking_id;
                     sqlCommand.Parameters.Add("@cruise_id", SqlDbType.Int).Value = bookcruise.cruise_id;
                     sqlCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = bookcruise.username;
                     sqlCommand.Parameters.Add("@check_in_date", SqlDbType.NVarChar).Value = bookcruise.check_in_date;
                     sqlCommand.Parameters.Add("@check_out_date", SqlDbType.NVarChar).Value = bookcruise.check_out_date;
                     sqlCommand.Parameters.Add("@room_num", SqlDbType.Int).Value = bookcruise.room_num;
                     sqlCommand.Parameters.Add("@num_of_adults", SqlDbType.Int).Value = bookcruise.num_of_adults;
                     conn.Open();
                     sqlCommand.ExecuteNonQueryAsync().Wait();
                 }
             }*/
        }
    }
}
