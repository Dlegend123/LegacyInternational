using System;
using System.Collections.Generic;
using System.Configuration;
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
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Contact.aspx";
                Response.Redirect(url);
            }
            if (Session["user"] != null)//Prevents guests from seeing the VacationBookings page 
            {
                if ((Session["user"] as ApplicationUser).UserName != "Default")
                {
                    if (new JTBDBModel().users.AsEnumerable().Where(x => x.email == (Session["user"] as ApplicationUser).UserName).Count() == 0)//Allow users to finish setting up their profile
                        Response.Redirect("Account/SetUpProfile.aspx", false);
                }
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