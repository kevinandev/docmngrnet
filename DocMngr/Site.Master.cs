using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Dao;
using Bean;
using System.Diagnostics;
using log4net;
using FunctionGroup.utils;
using FunctionGroup.Util;

namespace DocMngr
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                MenuDAO dao = new MenuDAO();
                List<MenuBean> lstMenu = dao.getListMainMenu(userName);
                generateMenu(lstMenu);
                Debug.WriteLine("Generate success");
                //load message
                List<string> lstError = (List<string>)Session[Constants.SESSION_ERROR];
                List<string> lstInfo = (List<string>)Session[Constants.SESSION_INFO];
                string sysMessage = "";
                if (lstError != null)
                {
                    foreach (string s in lstError)
                    {
                        sysMessage += HtmlUtil.getErrorTag(s);
                    }
                }
                if (lstInfo != null)
                {
                    foreach (string s in lstInfo)
                    {
                        sysMessage += HtmlUtil.getInfoTag(s);
                    }
                }
                if (sysMessage != null && sysMessage.Length > 0)
                {
                    divMessage.InnerHtml = sysMessage;
                    divMessage.Visible = true;
                }
                else
                {
                    divMessage.Visible = false;
                }
                Session[Constants.SESSION_INFO] = null;
                Session[Constants.SESSION_ERROR] = null;

            }
            catch (Exception ex)
            {
                logger.Error("Load MasterPage error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void onSignOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
        private void generateMenu(List<MenuBean> mainMenu)
        {
            if (mainMenu != null)
            {
                foreach (MenuBean bean in mainMenu)
                {
                    MenuItem rootItem = new MenuItem();
                    string navigateUrl = bean.Url != null && bean.Url.Trim().Length > 0 ? "~" + bean.Url.Trim() : "#";
                    rootItem.NavigateUrl = navigateUrl;
                    rootItem.Text = bean.Text;
                    List<MenuItem> lstChild = generateMenuChild(bean);
                    foreach (MenuItem c in lstChild)
                    {
                        rootItem.ChildItems.Add(c);
                    }
                    NavigationMenu.Items.Add(rootItem);
                }
            }
        }
        private List<MenuItem> generateMenuChild(MenuBean bean)
        {
            List<MenuItem> lstRS = new List<MenuItem>();
            if (bean != null && bean.LstSub != null && bean.LstSub.Count > 0)
            {
                foreach (MenuBean it in bean.LstSub)
                {
                    MenuItem mt = new MenuItem();
                    mt.Text = it.Text;
                    mt.NavigateUrl = it.Url != null && it.Url.Trim().Length > 0 ? "~" + it.Url.Trim() : "#";
                    List<MenuItem> lstChild = generateMenuChild(it);
                    foreach (MenuItem c in lstChild)
                    {
                        mt.ChildItems.Add(c);
                    }
                    lstRS.Add(mt);
                }

            }
            return lstRS;
        }
    }
}
