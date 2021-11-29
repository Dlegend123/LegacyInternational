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
            if (Request.QueryString["id"] != null)//Checks if the usrl contains id as a variable
            {
                user = Session["user"] as ApplicationUser;//Assigns the user session to a variable
                if (Request.QueryString["id"].Split(':').Count() == 3)//Checks the number of elements after spliting the string by ':'
                    CRSelect_Click(sender, e);// Calls the CRSelect_Click method to add a new cruise booking
                else
                {
                    if (Request.QueryString["id"].Split(':').Count() == 2)
                        DFSelect_Click(sender, e);//Calls the DFSelect_Click to add a new flight booking
                }
            }
        }
        protected void DFSelect_Click(object sender, EventArgs e)//Event handler for buttons assigned to flight
        {
            jTBDBModel = new JTBDBModel();
            new AirlineServiceRef.AirlineService().CreateBooking(Int32.Parse(Request.QueryString["id"].Split(':')[0]), jTBDBModel.users.Where(x => x.email == user.UserName).First().username, jTBDBModel.users.Where(x => x.email == user.UserName).First().dob, Int32.Parse(Request.QueryString["count"]));
        }

        protected void CRSelect_Click(object sender, EventArgs e)//event handler for buttons assigned to cruises
        {
            jTBDBModel = new JTBDBModel();// Initializes an instance of the JTBDBModel class
            var Cruise = jTBDBModel.cruiselists.AsEnumerable().Where(x => x.cruiserooms.Any(v => v.room_num == Int32.Parse(Request.QueryString["id"].Split(':')[1])) && x.cruise_id == Int32.Parse(Request.QueryString["cruise_id"])).First();

            new CruiseServiceRef.CruiseService().CreateBooking(jTBDBModel.users.Where(x => x.email == user.UserName).First().username,
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
                jTBDBModel = new JTBDBModel();// Initializes an instance of the JTBDBModel class
                if (jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).Count() == 0)//Allow users to finish setting up their profile
                    Response.Redirect("SetUpProfile.aspx", false);
                else
                {
                    // Display all user's info
                    UsernameCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().username));// Adds the user's username to the page
                    FNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().first_name));//Adds the user's first name to the page
                    LNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().last_name));//Adds the user's last name to the page
                    CNumber.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().contact_num));//Adds the user's contact number to the page
                    DOBCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().dob));//Adds the user's date of birth to the page
                    EmailCell.Controls.Add(new LiteralControl(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().email));//Adds the user's email to the page
                    if (jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().ProfilePic != null)//Checks if the user has any profile pic stored
                    {
                        Image image = new Image
                        {
                            CssClass = "img-fluid",
                            ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(jTBDBModel.users.AsEnumerable().Where(x => x.email == user.UserName).First().ProfilePic)
                        };
                        image.Style.Add("max-height", "25vh");//sets the max height of the image
                        image.Style.Add("max-width", "25vw");//Sets the max width
                        ProfilePicCell.Controls.Add(image);//Adds an image to the profile pic cell 
                    }
                    else
                    {
                        ProfilePicCell.Controls.Add(new LiteralControl("<br /> No Image <br />"));// Adds no image to the profilepic cell
                    }
                    CruiseCollect().ForEach(l => QuickFunction(l, 1, CBookings));//Display user's cruise booking details
                    FlightCollect().ForEach(l => QuickFunction(l, 0, PBookings));//Display user's flight booking details
                }
                if ((Session["user"] as ApplicationUser).UserName == "Default")
                    Page.Master.FindControl("BookingsPage").Visible = false;
            }
            else
            {
                Page.Master.FindControl("BookingsPage").Visible = false;
            }
        }
        List<bookcruise> CruiseCollect()//Get all cruise bookings for the user
        {
            //Retrieves the user's cruise bookings from the database
            var result = jTBDBModel.users.AsEnumerable().Where(k => k.email == user.UserName).First().bookcruises.AsEnumerable().ToList();
            return result;
        }
        List<bookflight> FlightCollect()//Get all flight bookings for the user 
        {
            //Retrieve the user's flight bookings from the database
            var result = jTBDBModel.users.AsEnumerable().Where(k=>k.email==user.UserName).First().bookflights.AsEnumerable().ToList();
            return result;
        }
        void QuickFunction(object x, int k, Table AddTo)// Aadd booking info to page
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