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
    public partial class QLRole : System.Web.UI.Page
    {
        private int stt = 0;
        private RoleLogic logic = new RoleLogic();
        ILog logger = log4net.LogManager.GetLogger("File");
        aspnet_Role sec = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<aspnet_Role> lstLoaiTaiLieu = logic.getAll();
                    dgvApprover.DataSource = lstLoaiTaiLieu;
                    dgvApprover.DataBind();
                    if (Session[RoleLogic.ROLE_LOGIC_SEC_ID] != null)
                    {

                        sec = logic.findById(Session[RoleLogic.ROLE_LOGIC_SEC_ID].ToString());
                        if (sec != null)
                        {
                            txtCode.ReadOnly = true;
                            txtCode.Text = sec.Code;
                            tbxName.Text = sec.RoleName;
                            tbxDescription.Text = sec.Description;
                            btAddApprover.Text = "Cập nhật";
                        }
                    }
                    else
                    {
                        sec = new aspnet_Role();
                        txtCode.ReadOnly = false;
                        txtCode.Text = "";
                        tbxName.Text = "";
                        tbxDescription.Text = "";
                        btAddApprover.Text = "Thêm mới";
                    }

                }
                else
                {
                    if (Session[RoleLogic.ROLE_LOGIC_SEC_ID] != null)
                    {

                        sec = logic.findById(Session[RoleLogic.ROLE_LOGIC_SEC_ID].ToString());
                        if (sec != null)
                        {
                            txtCode.ReadOnly = true;
                            sec.RoleName = tbxName.Text;
                            sec.Description = tbxDescription.Text;
                        }
                    }
                    else
                    {
                        sec = new aspnet_Role();
                        sec.Code = txtCode.Text;
                        sec.RoleName = tbxName.Text;
                        sec.Description = tbxDescription.Text;
                        //sec.Active = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Pageload error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btAddApprover_Click(object sender, EventArgs e)
        {
            try
            {
                Guid defaulGuid = new Guid();
                if (sec.RoleId == null || defaulGuid.Equals(sec.RoleId))
                {
                    //Thêm mới
                    if (logic.validateInsert(sec))
                    {
                        logic.insert(sec);
                    }
                    else
                    {
                        logger.Info("Validate before insert " + sec.Code + " fail.");
                    }
                }
                else
                {
                    //Cập nhật
                    if (logic.validateUpdate(sec))
                    {
                        logic.update(sec);
                    }
                    else
                    {
                        logger.Info("Validate before update " + sec.Code + " fail.");
                    }
                }
                Session[RoleLogic.ROLE_LOGIC_SEC_ID] = null;
                Response.Redirect("QLRole.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("editApprover_Click error: ", ex);
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
                    ImageButton btnEdit = (ImageButton)e.Row.FindControl("bt_changing");
                    if (btnEdit != null)
                    {
                        btnEdit.CommandArgument = item.RoleId.ToString();
                    }
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvApprover.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("cbChoose");
                        if (chk != null && chk.Checked)
                        {
                            string id = dgvApprover.DataKeys[row.RowIndex].Value.ToString();
                            aspnet_Role r = logic.findById(id);
                            logic.delete(r.RoleName);
                        }
                    }
                }
                Session[RoleLogic.ROLE_LOGIC_SEC_ID] = null;
                Response.Redirect("QLRole.aspx", false);                
            }
            catch (Exception ex)
            {
                logger.Error("btDelete_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Session[RoleLogic.ROLE_LOGIC_SEC_ID] = null;
                Response.Redirect("QLRole.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("btCancel_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void editApprover_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgBtn = sender as ImageButton;
                Session[RoleLogic.ROLE_LOGIC_SEC_ID] = imgBtn.CommandArgument.Trim();
                Response.Redirect("QLRole.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("editApprover_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }
}