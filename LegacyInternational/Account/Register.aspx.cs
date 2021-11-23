using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using LegacyInternational.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace LegacyInternational.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Account/Register.aspx";
                Response.Redirect(url);
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            user.Id = (manager.Users.Count() + 1).ToString();
            
            IdentityUserRole userRole = new IdentityUserRole
            {
                UserId = user.Id,
                RoleId = "Cust"
            };
            user.Roles.Add(userRole);
            IdentityResult result = manager.CreateAsync(user, Password.Text).Result;
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                signInManager.SignIn(user, isPersistent: true, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["~/Account/SetUpProfile.aspx"], Response);
            }
        }
    }
}