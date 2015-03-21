using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.Dao;
using FunctionGroup.Logic;
using log4net;

namespace Function
{
    public partial class QLMenuRole : System.Web.UI.Page
    {
        private int stt = 0;
        private MenuRoleLogic logic = new MenuRoleLogic();
        private MenuLogic menuLogic = new MenuLogic();
        private RoleLogic roleLogic = new RoleLogic();
        ILog logger = log4net.LogManager.GetLogger("File");
        aspnet_Role cRole = null;
        menu cMenu = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<menu> lstMenu = menuLogic.getAll();
                    ddlMenu.DataSource = lstMenu;

                    ddlMenu.DataTextField = "text";
                    ddlMenu.DataValueField = "id";
                    ddlMenu.DataBind();
                    List<aspnet_Role> lstRole = roleLogic.getAll();
                    ddlRole.DataSource = lstRole;
                    ddlRole.DataTextField = "RoleName";
                    ddlRole.DataValueField = "RoleId";
                    ddlRole.DataBind();
                }
                else
                {
                    string sMenu = ddlMenu.SelectedValue;
                    int iMenu = 0;
                    Int32.TryParse(sMenu, out iMenu);
                    cMenu = menuLogic.findById(iMenu);
                    string sRole = ddlRole.SelectedValue;
                    cRole = roleLogic.findById(sRole);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Pageload error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void dgvOnDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                stt++;
                aspnet_Role item = (aspnet_Role)e.Row.DataItem;
                if (item != null)
                {
                    Label lblSTT = (Label)e.Row.FindControl("lblSTT");
                    if (lblSTT != null)
                    {
                        lblSTT.Text = stt.ToString();
                    }
                }
            }
        }

        protected void btnMenuSubmit_Click(object sender, EventArgs e)
        {
            if (cMenu != null)
            {
                List<aspnet_Role> lstRole = logic.getRoleOfMenu(cMenu.id, cMenu.code);
                grdRoleOfMenu.DataSource = lstRole;
                grdRoleOfMenu.DataBind();
            }
        }

        protected void btnRoleAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cMenu != null && cRole != null)
                {
                    logic.addRoleToMenu(cMenu, cRole);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Pageload error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

    }
}