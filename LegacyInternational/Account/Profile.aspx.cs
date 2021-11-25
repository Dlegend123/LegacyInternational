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
        protected void Page_Load(object sender, EventArgs e)
        {
            jTBDBModel = new JTBDBModel();
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
            if (jTBDBModel.users.Where(x => x.email == user.UserName).Count() == 0)//Allow users to finish setting up their profile
                Response.Redirect("SetUpProfile.aspx", false);
            else
            {// Display all user's info
                UsernameCell.Controls.Add(new LiteralControl(user.UserName));
                FNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.UserName).First().first_name));
                LNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.UserName).First().last_name));
                CNumber.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.UserName).First().contact_num));
                DOBCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.UserName).First().dob));
                EmailCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.UserName).First().email));
                if (jTBDBModel.users.Where(x => x.email == user.UserName).First().ProfilePic!=null)
                {
                    Image image = new Image
                    {
                        CssClass = "img-fluid",
                        ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(jTBDBModel.users.Where(x => x.email == user.UserName).First().ProfilePic)
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
            var result = jTBDBModel.users.Where(k => k.email == user.UserName).First().bookcruises.AsEnumerable().ToList();
            //var result = jTBDBModel.bookcruises.AsEnumerable().ToList();
            return result;
        }
        List<bookflight> FlightCollect()//Get all flight bookings for the user 
        {
            //var result = jTBDBModel.bookflights.AsEnumerable().ToList();
            var result = jTBDBModel.users.Where(k=>k.email==user.UserName).First().bookflights.AsEnumerable().ToList();
            return result;
        }
        void QuickFunction(object x, int k, Table AddTo)
        {
            if (k == 0)//Book Flight
            {
                TableRow tableRow = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow1 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow2 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow3 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };

                bookflight p = x as bookflight;
                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("Flight ID: " + p.flight_id));
                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("Number of Adults: " + p.num_of_adults));
                TableCell tableCell4 = new TableCell();
                tableCell.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                tableCell.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                tableCell.HorizontalAlign = HorizontalAlign.Center;
                tableRow.Visible = true;
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
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow1 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow2 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow3 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow4 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow5 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Left,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    bookcruise p = x as bookcruise;
                    TableCell tableCell = new TableCell();
                    tableCell.Controls.Add(new LiteralControl("Cruise ID: " + p.cruise_id));
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id));
                    TableCell tableCell2 = new TableCell();
                    tableCell2.Controls.Add(new LiteralControl("Check In Date: " + p.check_in_date));
                    TableCell tableCell3 = new TableCell();
                    tableCell3.Controls.Add(new LiteralControl("Check Out Date: " + p.check_out_date));
                    TableCell tableCell4 = new TableCell();
                    tableCell4.Controls.Add(new LiteralControl("Room #: " + p.room_num));
                    TableCell tableCell5 = new TableCell();
                    tableCell5.Controls.Add(new LiteralControl("Room Type: " + jTBDBModel.cruiserooms.Where(b=>b.room_num==p.room_num).First().type));
                    tableCell.VerticalAlign = tableCell2.VerticalAlign = tableCell1.VerticalAlign = tableCell3.VerticalAlign = tableCell4.VerticalAlign = VerticalAlign.Middle;
                    tableCell.HorizontalAlign = tableCell2.HorizontalAlign = tableCell1.HorizontalAlign = tableCell3.HorizontalAlign = tableCell4.HorizontalAlign = HorizontalAlign.Center;
                    tableCell.HorizontalAlign = HorizontalAlign.Center;
                    tableRow.Visible = true;
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