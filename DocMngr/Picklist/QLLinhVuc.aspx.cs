using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.Dao;
using FunctionGroup.Logic;
using log4net;

namespace FunctionGroup.Picklist
{
    public partial class QLLinhVuc : System.Web.UI.Page
    {

        private int stt = 0;
        private LinhVucLogic logic = new LinhVucLogic();
        ILog logger = log4net.LogManager.GetLogger("File");
        Su_LinhVuc sec = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<Su_LinhVuc> lstLinhVuc = logic.getAll();
                    dgvApprover.DataSource = lstLinhVuc;
                    dgvApprover.DataBind();
                    if (Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] != null)
                    {

                        sec = logic.findById(Int32.Parse(Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID].ToString()));
                        txtCode.ReadOnly = true;
                        txtCode.Text = sec.Code;
                        tbxName.Text = sec.Name;
                        tbxDescription.Text = sec.Description;
                        btAddApprover.Text = "Cập nhật";
                    }
                    else
                    {
                        sec = new Su_LinhVuc();
                        txtCode.ReadOnly = false;
                        txtCode.Text = "";
                        tbxName.Text = "";
                        tbxDescription.Text = "";
                        btAddApprover.Text = "Thêm mới";
                    }

                }
                else
                {
                    if (Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] != null)
                    {

                        sec = logic.findById(Int32.Parse(Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID].ToString()));
                        txtCode.ReadOnly = true;
                        sec.Name = tbxName.Text;
                        sec.Description = tbxDescription.Text;
                    }
                    else
                    {
                        sec = new Su_LinhVuc();
                        sec.Code = txtCode.Text;
                        sec.Name = tbxName.Text;
                        sec.Description = tbxDescription.Text;
                        sec.Active = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("editApprover_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btAddApprover_Click(object sender, EventArgs e)
        {
            try
            {
                if (sec.ID <= 0)
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
                Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] = null;
                Response.Redirect("QLLinhVuc.aspx", false);
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
                Su_LinhVuc item = (Su_LinhVuc)e.Row.DataItem;
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
                            int id = 0;
                            Int32.TryParse(dgvApprover.DataKeys[row.RowIndex].Value.ToString(), out id);
                            logic.delete(id);
                        }
                    }
                }
                Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] = null;
                Response.Redirect("QLLinhVuc.aspx", false);                
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
                Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] = null;
                Response.Redirect("QLLinhVuc.aspx", false);
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
                Session[LinhVucLogic.LINH_VUC_LOGIC_SEC_ID] = imgBtn.CommandArgument.Trim();
                Response.Redirect("QLLinhVuc.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("editApprover_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }
}