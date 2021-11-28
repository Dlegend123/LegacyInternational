using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegacyInternational.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        JTBDBModel jTBDBModel;
        ApplicationUser user;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                user = Session["user"] as ApplicationUser;
                if (Request.QueryString["id"].Split(':').Count() == 3)
                    CRSelect_Click(sender, e);
                else
                {
                    if (Request.QueryString["id"].Split(':').Count() == 2)
                        DFSelect_Click(sender, e);
                }
            }
        }
        protected void DFSelect_Click(object sender, EventArgs e)//Event handler for buttons assigned to flight
        {
            jTBDBModel = new JTBDBModel();
            AirlineServiceRef.AirlineService airlineService = new AirlineServiceRef.AirlineService();
            airlineService.CreateBooking(Int32.Parse(Request.QueryString["id"].Split(':')[0]), jTBDBModel.users.Where(x => x.email == user.UserName).First().username, jTBDBModel.users.Where(x => x.email == user.UserName).First().dob, Int32.Parse(Request.QueryString["count"]));
        }

        protected void CRSelect_Click(object sender, EventArgs e)//event handler for buttons assigned to cruises
        {
            CruiseService cruiseService = new CruiseService();
            jTBDBModel = new JTBDBModel();
            var Cruise = jTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(v => v.room_num == Int32.Parse(Request.QueryString["id"].Split(':')[1])) && x.cruise_id == Int32.Parse(Request.QueryString["cruise_id"])).First();
            CruiseServiceRef.CruiseService cruiseService1 = new CruiseServiceRef.CruiseService();
            cruiseService1.CreateBooking(jTBDBModel.users.Where(x => x.email == user.UserName).First().username,
                string.IsNullOrEmpty(Request.QueryString["SDate"]) ? Cruise.start_datetime : Request.QueryString["SDate"],
                string.IsNullOrEmpty(Request.QueryString["EDate"]) ? Cruise.end_datetime : Request.QueryString["EDate"],
                Int32.Parse(Request.QueryString["id"].Split(':')[2]),
                jTBDBModel.bookcruises.AsEnumerable().Count() + 1,
                Int32.Parse(Request.QueryString["id"].Split(':')[1]));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["user"] as ApplicationUser;
            if (!Request.IsSecureConnection)//Forces securelink if the link isn't currently secure 
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Account/Profile.aspx";
                Response.Redirect(url);
            }
            if (Session["user"] != null)//Prevents guests from seeing the VacationBookings page 
            {
                if ((Session["user"] as ApplicationUser).UserName == "Default")
                    Page.Master.FindControl("BookingsPage").Visible = false;
            }
            else
            {
                Page.Master.FindControl("BookingsPage").Visible = false;
            }
            jTBDBModel = new JTBDBModel();
            if (jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).Count() == 0)//Allow users to finish setting up their profile
                Response.Redirect("SetUpProfile.aspx", false);
            else
            {// Display all user's info
                UsernameCell.Controls.Add(new LiteralControl(user.UserName));
                FNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().first_name));
                LNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().last_name));
                CNumber.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().contact_num));
                DOBCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().dob));
                EmailCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().email));
                if (jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().ProfilePic!=null)
                {
                    Image image = new Image
                    {
                        CssClass = "img-fluid",
                        ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().ProfilePic)
                    };
                    image.Style.Add("max-height", "25vh");
                    image.Style.Add("max-width", "25vw");
                    ProfilePicCell.Controls.Add(image);
                }
                else
                {
                    ProfilePicCell.Controls.Add(new LiteralControl("<br /> No Image <br />"));
                }
                //Display user's booking details
                CruiseCollect().ForEach(l => QuickFunction(l, 1, CBookings));
                FlightCollect().ForEach(l => QuickFunction(l, 0, PBookings));
            }
        }
        List<bookcruise> CruiseCollect()//Get all cruise bookings for the user
        {
            var result = jTBDBModel.users.AsEnumerable().Where(k => k.email == user.UserName).First().bookcruises.AsEnumerable().ToList();
            //var result = jTBDBModel.bookcruises.AsEnumerable().ToList();
            return result;
        }
        List<bookflight> FlightCollect()//Get all flight bookings for the user 
        {
            //var result = jTBDBModel.bookflights.AsEnumerable().ToList();
            var result = jTBDBModel.users.AsEnumerable().Where(k=>k.email==user.UserName).First().bookflights.AsEnumerable().ToList();
            return result;
        }
        void QuickFunction(object x, int k, Table AddTo)
        {
            if (k == 0)//Book Flight
            {
                TableRow tableRow = new TableRow
                {
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow1 = new TableRow
                {
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow2 = new TableRow
                {
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow3 = new TableRow
                {
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };

                bookflight p = x as bookflight;
                TableCell tableCell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left
                };
                tableCell.Controls.Add(new LiteralControl("Flight ID: " + p.flight_id));
                TableCell tableCell1 = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left
                };
                tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id));
                TableCell tableCell3 = new TableCell{ HorizontalAlign = HorizontalAlign.Left };
                tableCell3.Controls.Add(new LiteralControl("Number of Adults: " + p.num_of_adults));
                tableCell3.HorizontalAlign = HorizontalAlign.Left;
                TableCell tableCell4 = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left
                };
                tableRow.Cells.Add(tableCell);
                AddTo.Rows.Add(tableRow);
                tableRow1.Cells.Add(tableCell1);
                AddTo.Rows.Add(tableRow1);
                tableRow2.Cells.Add(tableCell3);
                AddTo.Rows.Add(tableRow2);
                tableRow3.Cells.Add(tableCell4);
                AddTo.Rows.Add(tableRow3);
            }
            else
            {
                if (k == 1)//Book Cruise
                {
                    TableRow tableRow = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow1 = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow2 = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow3 = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow4 = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow5 = new TableRow
                    {
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    bookcruise p = x as bookcruise;
                    TableCell tableCell = new TableCell{ HorizontalAlign = HorizontalAlign.Left };
                    tableCell.Controls.Add(new LiteralControl("Cruise ID: " + p.cruise_id));
                    TableCell tableCell1 = new TableCell
                    {
                        HorizontalAlign = HorizontalAlign.Left
                    };
                    tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id));
                    TableCell tableCell2 = new TableCell
                    {
                        HorizontalAlign = HorizontalAlign.Left
                    };
                    tableCell2.Controls.Add(new LiteralControl("Check In Date: " + p.check_in_date));
                    TableCell tableCell3 = new TableCell
                    {
                        HorizontalAlign = HorizontalAlign.Left
                    };
                    tableCell3.Controls.Add(new LiteralControl("Check Out Date: " + p.check_out_date));
                    TableCell tableCell4 = new TableCell{ HorizontalAlign = HorizontalAlign.Left };
                    tableCell4.Controls.Add(new LiteralControl("Room #: " + p.room_num));
                    tableCell4.HorizontalAlign = HorizontalAlign.Left;
                    TableCell tableCell5 = new TableCell
                    {
                        HorizontalAlign = HorizontalAlign.Left
                    };
                    tableCell5.Controls.Add(new LiteralControl("Room Type: " + jTBDBModel.cruiserooms.Where(b=>b.room_num==p.room_num).First().type));
                    tableRow.Cells.Add(tableCell);
                    AddTo.Rows.Add(tableRow);
                    tableRow1.Cells.Add(tableCell1);
                    AddTo.Rows.Add(tableRow1);
                    tableRow2.Cells.Add(tableCell2);
                    AddTo.Rows.Add(tableRow2);
                    tableRow3.Cells.Add(tableCell3);
                    AddTo.Rows.Add(tableRow3);
                    tableRow4.Cells.Add(tableCell4);
                    AddTo.Rows.Add(tableRow4);
                    tableRow5.Cells.Add(tableCell5);
                    AddTo.Rows.Add(tableRow5);
                }
            }
        }

        protected void ABooking_Click(object sender, EventArgs e)//Redirects to the VacationBookings page
        {
            Response.Redirect("~/VacationBookings.aspx", false);
        }
    }
}