using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DocMngr
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            MembershipCreateStatus status = new MembershipCreateStatus();
            //MembershipUser newUser = Membership.CreateUser(userName, password, "", "", "", true, out  status);
            MembershipUser newUser = Membership.CreateUser(userName, password, "Default", "Default", "Default", true, out  status);
            if (MembershipCreateStatus.Success.Equals(status))
            {
                lblMessage.Text = "Success";
            }
            else
            {
                lblMessage.Text = status.ToString();
            }
        }
    }
}