using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using LegacyInternational.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace LegacyInternational.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)//Forces securelink if the link isn't currently secure
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Account/Login.aspx";
                Response.Redirect(url);
            }
            RegisterHyperLink.NavigateUrl = "Register";
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
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            Session["user"] = manager.FindByName(Email.Text);//Assigns the current user's info to a session
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["~/Default.aspx"], Response);//Redirect to Homepage
                        }
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}