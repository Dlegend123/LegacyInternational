using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegacyInternational
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JTBDBModel jTBDBModel = new JTBDBModel();
            ApplicationUser user = Session["User"] as ApplicationUser;
            UsernameCell.Controls.Add(new LiteralControl(user.UserName));
            FNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.username == user.UserName).First().first_name));
            LNameCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.username == user.UserName).First().last_name));
            CNumber.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.username == user.UserName).First().contact_num));
            DOBCell.Controls.Add(new LiteralControl(jTBDBModel.users.Where(x => x.username == user.UserName).First().dob));

            if (jTBDBModel.users.Where(x => x.username == user.UserName).First().ProfilePic.Max() != 0)
            {
                Image image = new Image
                {
                    CssClass = "img-fluid",
                    ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(jTBDBModel.users.Where(x => x.username == user.UserName).First().ProfilePic)
                };
                image.Style.Add("max-height", "40vh");
                image.Style.Add("max-width", "40vw");
                ProfilePicCell.Controls.Add(image);
            }
            else
            {
                ProfilePicCell.Controls.Add(new LiteralControl("<br /> No Image <br />"));
            }
        }
        void QuickFunction(object x, int k, Table AddTo)
        {

            if (k == 0)//Departure Flight
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
                bookflight p = x as bookflight;
                TableCell tableCell = new TableCell();
                tableCell.Controls.Add(new LiteralControl("<br /> Flight ID: " + p.flight_id + "<br />"));
                TableCell tableCell1 = new TableCell();
                tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id + "<br />"));
                TableCell tableCell3 = new TableCell();
                tableCell3.Controls.Add(new LiteralControl("Number of Adults: " + p.num_of_adults + "<br />"));
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
                if (k == 1)//Return Flights
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
                    tableCell.Controls.Add(new LiteralControl("<br /> Cruise ID: " + p.cruise_id + "<br />"));
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Controls.Add(new LiteralControl("Booking ID: " + p.booking_id + "<br />"));
                    TableCell tableCell2 = new TableCell();
                    tableCell2.Controls.Add(new LiteralControl("Check In Date: " + p.check_in_date + "<br />"));
                    TableCell tableCell3 = new TableCell();
                    tableCell3.Controls.Add(new LiteralControl("Check Out Date: " + p.check_out_date + "<br />"));
                    TableCell tableCell4 = new TableCell();
                    tableCell4.Controls.Add(new LiteralControl("Room #: " + p.cruiseroom.room_num + "<br />"));
                    TableCell tableCell5 = new TableCell();
                    tableCell5.Controls.Add(new LiteralControl("Room Type: " + p.cruiseroom.type + "<br />"));

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