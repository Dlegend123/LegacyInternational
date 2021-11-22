using LegacyInternational.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace LegacyInternational
{
    public class Global : HttpApplication
    {
        void Session_Start(object sender, EventArgs e)
        {
            Exception err = new Exception();
            Session["LastError"] = err; //initialize the session
            ApplicationUser Account;
            IdentityUserRole role = new IdentityUserRole
            {
                RoleId = "Visitor",
                UserId = "Random"
            };
            Account = new ApplicationUser
            {
                UserName = "Default",
                Id = "Random"
            };
            Account.Roles.Add(role);
            Session["user"] = Account;
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception err = Server.GetLastError();
            HttpContext context = HttpContext.Current;

            if (context != null && context.Session != null)
                if (Session["LastError"] != null)
                    Session["LastError"] = err;
        }
    }
}