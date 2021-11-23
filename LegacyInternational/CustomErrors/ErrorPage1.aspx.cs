using LegacyInternational.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LegacyInternational.CustomErrors
{
    public partial class ErrorPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "~/CustomErrors/ErrorPage1.aspx";
                Response.Redirect(url);
            }

            Exception err = Session["LastError"] as Exception;
            //Exception err = Server.GetLastError();
            if ( err!= null)
            {
                err = err.GetBaseException();
                ErrorMessage.InnerText = err.Message;
                ErrorSource.InnerText = err.Source;
                InnerEx.InnerText = (err.InnerException != null) ? err.InnerException.ToString() : "";
                StackTrace.InnerText = err.StackTrace;
                Session["LastError"] = null;
            }
        }
    }
}