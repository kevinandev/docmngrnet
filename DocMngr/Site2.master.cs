using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Diagnostics;

namespace FunctionGroup
{
    public partial class Site2Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = (string)Session["CurrentUser"];
            bool authenticated = false;
            if (userName != null && userName.Trim().Length > 0)
            {
                HttpCookie aCK = FormsAuthentication.GetAuthCookie(userName, true);
                if (aCK != null)
                {
                    authenticated = true;
                }

            }
            if (authenticated)
            {
                anonymous.Visible = false;
                logedIn.Visible = true;
                lblUserName.Text = userName;
                //string[] currentRole = Roles.GetRolesForUser(userName);
            }
            else
            {
                anonymous.Visible = true;
                logedIn.Visible = false;
                userName = "anonymous";
            }
        }
        protected void onSignOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
