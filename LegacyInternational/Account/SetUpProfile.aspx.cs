using LegacyInternational.Models;
using System;
using System.Collections.Generic;
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
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            JTBDBModel jTBDBModel = new JTBDBModel();
            user user = new user
            {
                username = Username.Text,
                dob = DOB.Text,
                last_name = LName.Text,
                first_name = FName.Text,
                contact_num = CNumber.Text,
                email = (Session["user"] as ApplicationUser).Email
            };
            jTBDBModel.users.Add(user);
            jTBDBModel.SaveChangesAsync();
        }
    }
}