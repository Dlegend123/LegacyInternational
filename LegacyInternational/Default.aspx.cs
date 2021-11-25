using LegacyInternational.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegacyInternational
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)//Forces securelink if the link isn't currently secure
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Default.aspx";
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
        }
    }
}