using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using log4net;

namespace DocMngr
{

    public partial class login : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void loginClick(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                string target = Request.Params["target"];
                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {
                    string pass = user.GetPassword();
                    logger.Info("User load");
                    if (pass != null && pass.Equals(password))
                    {
                        FormsAuthentication.SetAuthCookie(userName, true);
                        //bool rs = Membership.ValidateUser(userName, password);
                        //FormsAuthentication.Authenticate
                        Session.Add("CurrentUser", userName);
                        if (target != null && target.Trim().Length > 0)
                        {
                            Response.Redirect(target,false);
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx",false);
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Login fail!";
                    }
                    //lblMessage.Text = "Get user success pass: "+mss;
                    //bool authenticate = FormsAuthentication.Authenticate(userName, password);

                    //if (authenticate)
                    //{
                    //    lblMessage.Text = "Login success";
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Login fail!";
                    //}
                }
                else
                {
                    lblMessage.Text = "Login fail!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Execute error!";
                logger.Error("Login error: ", ex);
            }
        }
    }
}