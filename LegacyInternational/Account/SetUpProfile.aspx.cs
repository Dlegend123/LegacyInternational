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
    public partial class SetUpProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Account/SetUpProfile.aspx";
                Response.Redirect(url);
            }
            if (Session["user"] != null)
            {
                if((Session["user"] as ApplicationUser).UserName== "Default")
                    Page.Master.FindControl("BookingsPage").Visible = false;
            }
            else
            {
                Page.Master.FindControl("BookingsPage").Visible = false;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            JTBDBModel jTBDBModel = new JTBDBModel();
            if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(FName.Text) || string.IsNullOrEmpty(LName.Text) || string.IsNullOrEmpty(DOB.Text) || string.IsNullOrEmpty(CNumber.Text))
            {
                if (string.IsNullOrEmpty(Username.Text))
                {
                    UsernameErr.Visible = true;
                }
                else
                    UsernameErr.Visible = false;
                if (string.IsNullOrEmpty(FName.Text))
                {
                    FNameErr.Visible = true;
                }
                else
                    FNameErr.Visible = false;
                if (string.IsNullOrEmpty(LName.Text))
                {
                    LNameErr.Visible = true;
                }
                else
                    LNameErr.Visible = false;
                if (string.IsNullOrEmpty(DOB.Text))
                {
                    DOBErr.Visible = true;
                }
                else
                    DOBErr.Visible = false;
                if (string.IsNullOrEmpty(CNumber.Text))
                {
                    CNumberErr.Visible = true;
                }
                else
                    CNumberErr.Visible = false;
            }
            else
            {
                user user = new user
                {
                    username = Username.Text,
                    dob = DOB.Text,
                    last_name = LName.Text,
                    first_name = FName.Text,
                    contact_num = CNumber.Text,
                    email = (Session["user"] as ApplicationUser).UserName,
                    password = (Session["user"] as ApplicationUser).PasswordHash,
                };
                jTBDBModel.users.Add(user);
                jTBDBModel.SaveChangesAsync();
                Response.Redirect("~/Default.aspx", false);
            }
        }
    }
}