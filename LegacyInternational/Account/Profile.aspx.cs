using LegacyInternational.Models;
using System;
using System.Collections.Generic;
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
            if (jTBDBModel.users.Where(x => x.email == user.Email).Count() == 0)
                Response.Redirect("SetUpProfile.aspx", false);
            else
            {
                UsernameCell.Controls.Add(new LiteralControl(user.Email));
                FNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.Email).First().first_name));
                LNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.Email).First().last_name));
                CNumber.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.Email).First().contact_num));
                DOBCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.email == user.Email).First().dob));

                if (jTBDBModel.users.Where(x => x.email == user.Email).First().ProfilePic==null)
                {
                    Image image = new Image
                    {
                        CssClass = "img-fluid",
                        ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(jTBDBModel.users.Where(x => x.email == user.Email).First().ProfilePic)
                    };
                    image.Style.Add("max-height", "40vh");
                    image.Style.Add("max-width", "40vw");
                    ProfilePicCell.Controls.Add(image);
                }
                else
                {
                    ProfilePicCell.Controls.Add(new LiteralControl("<br /> No Image <br />"));
                }
                CruiseCollect().Where(x => DateTime.ParseExact(x.check_out_date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    .ToList().ForEach(l=>QuickFunction(l,0,CBookings));
                CruiseCollect().Where(x => DateTime.ParseExact(x.check_out_date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    .ToList().ForEach(l => QuickFunction(l, 0, PBookings));
            }
        }
        List<bookcruise> CruiseCollect()
        {
            var result = jTBDBModel.bookcruises.AsEnumerable().Where(x => x.username == jTBDBModel.users.AsEnumerable().Where(l => l.email == user.Email).First().username).ToList();
            return result;
        }
        List<bookflight> FlightCollect()
        {
            var result = jTBDBModel.bookflights.AsEnumerable().Where(x => x.username == jTBDBModel.users.AsEnumerable().Where(l => l.email == user.Email).First().username).ToList();
            return result;
        }
        void QuickFunction(object x, int k, Table AddTo)
        {
            if (k == 0)//Book Flight
            {
                TableRow tableRow = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Justify,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow1 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Justify,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow2 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Justify,
                    BorderStyle = BorderStyle.Solid,
                    BorderWidth = Unit.Pixel(3)
                };
                TableRow tableRow3 = new TableRow
                {
                    HorizontalAlign = HorizontalAlign.Justify,
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
                        HorizontalAlign = HorizontalAlign.Justify,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow1 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Justify,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow2 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Justify,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow3 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Justify,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow4 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Justify,
                        BorderStyle = BorderStyle.Solid,
                        BorderWidth = Unit.Pixel(3)
                    };
                    TableRow tableRow5 = new TableRow
                    {
                        HorizontalAlign = HorizontalAlign.Justify,
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
                    tableCell4.Controls.Add(new LiteralControl("Room #: " + p.cruiseroom.room_num));
                    TableCell tableCell5 = new TableCell();
                    tableCell5.Controls.Add(new LiteralControl("Room Type: " + p.cruiseroom.type));

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

        protected void ABooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/VacationBookings.aspx", false);
        }
    }
}