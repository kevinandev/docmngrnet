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
    public partial class QLMenu : System.Web.UI.Page
    {
        private int stt = 0;
        private MenuLogic logic = new MenuLogic();
        ILog logger = log4net.LogManager.GetLogger("File");
        menu sec = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<menu> lstMenu = logic.getAll();
                    ddlMaster.DataSource = lstMenu;
                    ddlMaster.DataTextField = "text";
                    ddlMaster.DataValueField = "id";
                    ddlMaster.DataBind();
                    dgvApprover.DataSource = lstMenu;
                    dgvApprover.DataBind();
                    if (Session[MenuLogic.MENU_LOGIC_SEC_ID] != null)
                    {
                        int cId = 0;
                        Int32.TryParse(Session[MenuLogic.MENU_LOGIC_SEC_ID].ToString(), out cId);
                        sec = logic.findById(cId);
                        if (sec != null)
                        {
                            txtCode.ReadOnly = true;
                            txtCode.Text = sec.code;
                            tbxName.Text = sec.text;
                            ddlType.SelectedValue = sec.type.ToString();
                            if (sec.master_id > 0)
                            {
                                ddlMaster.SelectedValue = sec.master_id.ToString();
                            }
                            else
                            {
                                rowMasterId.Visible = false;
                            }
                            txtOrder.Text = sec.order.ToString();
                            tbxUrl.Text = sec.url;
                            btAddApprover.Text = "Cập nhật";
                        }
                    }
                    else
                    {
                        sec = new menu();
                        txtCode.ReadOnly = false;
                        txtCode.Text = "";
                        tbxName.Text = "";
                        ddlType.SelectedValue = "";
                        tbxUrl.Text = "";
                        ddlMaster.SelectedValue = "";
                        rowMasterId.Visible = true;
                        txtOrder.Text = "0";
                        btAddApprover.Text = "Thêm mới";
                    }

                }
                else
                {
                    if (Session[MenuLogic.MENU_LOGIC_SEC_ID] != null)
                    {
                        int cId = 0;
                        Int32.TryParse(Session[MenuLogic.MENU_LOGIC_SEC_ID].ToString(), out cId);
                        sec = logic.findById(cId);
                        if (sec != null)
                        {
                            txtCode.ReadOnly = true;
                            sec.code=txtCode.Text;
                            sec.text = tbxName.Text;
                            string sType = ddlType.SelectedValue;
                            int iType = 0;
                            Int32.TryParse(sType, out iType);
                            sec.type = iType;
                            string sMaster = ddlMaster.SelectedValue;
                            int iMaster = 0;
                            Int32.TryParse(sMaster, out iMaster);
                            sec.master_id = iMaster;
                            string sOrder = txtOrder.Text;
                            int iOrder = 0;
                            Int32.TryParse(sOrder, out iMaster);
                            sec.order = iOrder;
                            sec.url = tbxUrl.Text;
                            btAddApprover.Text = "Cập nhật";
                        }
                    }
                    else
                    {
                        sec = new menu();
                        txtCode.ReadOnly = false;
                        txtCode.Text = "";
                        tbxName.Text = "";
                        ddlType.SelectedValue = "1";
                        tbxUrl.Text = "";
                        ddlMaster.SelectedValue = "0";
                        rowMasterId.Visible = true;
                        txtOrder.Text = "0";
                        btAddApprover.Text = "Thêm mới";
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
                if (sec.id <= 0)
                {
                    //Thêm mới
                    if (logic.validateInsert(sec))
                    {
                        logic.insert(sec);
                    }
                    else
                    {
                        logger.Info("Validate before insert " + sec.code + " fail.");
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
                        logger.Info("Validate before update " + sec.code + " fail.");
                    }
                }
                Session[MenuLogic.MENU_LOGIC_SEC_ID] = null;
                Response.Redirect("QLMenu.aspx", false);
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
                menu item = (menu)e.Row.DataItem;
                if (item != null)
                {
                    Label lblSTT = (Label)e.Row.FindControl("lblSTT");
                    if (lblSTT != null)
                    {
                        lblSTT.Text = stt.ToString();
                    }
                    Label lblMaster = (Label)e.Row.FindControl("lblMaster");
                    if (lblMaster != null)
                    {
                        foreach (ListItem it in ddlMaster.Items)
                        {
                            if (it.Value.ToString().Equals(item.master_id.ToString()))
                            {
                                lblMaster.Text = it.Text;
                                break;
                            }
                        }
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
                            int iId = 0;
                            Int32.TryParse(id, out iId);
                            logic.delete(iId);
                        }
                    }
                }
                Session[MenuLogic.MENU_LOGIC_SEC_ID] = null;
                Response.Redirect("QLMenu.aspx", false);
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
                Session[MenuLogic.MENU_LOGIC_SEC_ID] = null;
                Response.Redirect("QLMenu.aspx", false);
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
                Session[MenuLogic.MENU_LOGIC_SEC_ID] = imgBtn.CommandArgument.Trim();
                Response.Redirect("QLMenu.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("editApprover_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }
}