using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Diagnostics;

namespace DocMngr
{
    public partial class CreateRole : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Roles.CreateRole(TxtRoleName.Text);
                Debug.WriteLine("Role created");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("QuangException: " + ex.Message);
            }
        }

        protected void BtnAddToAdminRole_Click(object sender, EventArgs e)
        {
            try
            {
                Roles.AddUsersToRole(new string[] { TxtRoleName.Text }, "Administrator");
                Debug.WriteLine("Role created");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("QuangException: " + ex.Message);
            }
        }

    }
}