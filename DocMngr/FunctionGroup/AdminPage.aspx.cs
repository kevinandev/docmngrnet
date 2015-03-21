using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.utils;
using QTSecurity;
using System.Configuration;

namespace FunctionGroup
{
    public partial class AdminPage : System.Web.UI.Page
    {
        private WebProxy proxy = new WebProxy();
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.Params[Constants.REQUEST_TOKEN];
            proxy.init(ConfigurationManager.ConnectionStrings["appDB"].ConnectionString);
            if (!proxy.validateToken(token))
            {
                Response.Redirect("~/AccesDenied.aspx",false);
            }
        }

        protected void BtnRoleConfig_Click(object sender, ImageClickEventArgs e)
        {
            string token = proxy.getToken("ABC");
            Response.Redirect("~/Function/QLRole.aspx?" + Constants.REQUEST_TOKEN + "=" + token,false);
        }

        protected void BtnMenuConfig_Click(object sender, ImageClickEventArgs e)
        {
            string token = proxy.getToken("ABC");
            Response.Redirect("~/Function/QLMenu.aspx?" + Constants.REQUEST_TOKEN + "=" + token,false);
        }

        protected void BtnFunction_Click(object sender, ImageClickEventArgs e)
        {
            string token = proxy.getToken("ABC");
            Response.Redirect("~/Function/QLPhanQuyen.aspx?" + Constants.REQUEST_TOKEN + "=" + token,false);

        }

        protected void BtnUnitConfig_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Function/QLPhongBan.aspx",false);
        }

        protected void BtnUserConfig_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Function/QLUser.aspx", false);
        }
    }
}