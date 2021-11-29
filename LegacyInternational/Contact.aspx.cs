using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LegacyInternational.Models;
namespace LegacyInternational
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)//Prevents guests from seeing the VacationBookings page 
            {
                if (new JTBDBModel().users.AsEnumerable().Where(x => x.email == (Session["user"] as ApplicationUser).UserName).Count() == 0)//Allow users to finish setting up their profile
                    Response.Redirect("SetUpProfile.aspx", false);
                if ((Session["user"] as ApplicationUser).UserName == "Default")
                    Page.Master.FindControl("BookingsPage").Visible = false;
            }
            else
            {
                Page.Master.FindControl("BookingsPage").Visible = false;
            }
        }
    }
}