using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Logic;

namespace Function
{
    public partial class QLMucLuc : System.Web.UI.Page
    {

         MucLuc sec;
        Su_MucLucLogic um = new Su_MucLucLogic();
        string SecID = "";
        static string ID_Delete = "";
        string classobject = "QLMucLuc.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session[Su_MucLucLogic.SESSION_SEC_ID] != null)
                    {

                        btAddApprover.Text = "Cập nhật";
                        sec = um.getMucLuc(Int32.Parse(Session[Su_MucLucLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {
                            tbxName.Text = sec.Name;
                            tbxDescription.Text = sec.Description;
                            ntbLayer.Text = sec.Layer.ToString().Trim();
                        }
                    }
                    else
                    {
                        sec = new MucLuc();
                    }

                    listAllSec();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }

        protected void editApprover_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;

                foreach (GridViewRow row in dgvApprover.Rows)
                {

                    string ID = row.Cells[0].Text.Trim();
                    if (ID == lbtn.CommandArgument.Trim())
                    {
                        Session[Su_MucLucLogic.SESSION_SEC_ID] = ID;
                        Response.Redirect("QLMucLuc.aspx", false);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "editApprover_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btAddApprover_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isUpdate())
                {
                    sec = new MucLuc();
                    sec.Name = tbxName.Text;
                    sec.Description = tbxDescription.Text;
                    sec.Layer = Int32.Parse(ntbLayer.Text);

                }
                else
                {
                    sec = um.getMucLuc(Int32.Parse(Session[Su_MucLucLogic.SESSION_SEC_ID].ToString()));
                    sec.Name = tbxName.Text;
                    sec.Description = tbxDescription.Text;
                    sec.Layer = Int32.Parse(ntbLayer.Text);

                }
                if (validateSecurity(sec))
                {
                    if (!isUpdate())
                    {
                        um.addMucLuc(sec);
                        tbxDescription.Text = "";
                        tbxName.Text = "";
                        ntbLayer.Text = "0";
                    }
                    else
                    {
                        um.updateMucLuc(sec);
                        tbxDescription.Text = "";
                        tbxName.Text = "";
                        ntbLayer.Text = "0";
                    }
                }
                Session[Su_MucLucLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QLMucLuc.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAddApprover_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);

            }
        }
        protected void btCancel_Click(object sender, EventArgs e)
        {
            backTo();
        }

        private bool isUpdate()
        {
            if (Session[Su_MucLucLogic.SESSION_SEC_ID] != null)
            {
                SecID = Session[Su_MucLucLogic.SESSION_SEC_ID].ToString();
                return true;
            }
            return false;
        }

        protected void dgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvApprover.DataSource = um.getAllSec();
                dgvApprover.PageIndex = e.NewPageIndex;
                dgvApprover.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "dgvApprover_PageIndexChanging", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        private void backTo()
        {
            Session[Su_MucLucLogic.SESSION_SEC_ID] = null;
            Response.Redirect("Default.aspx", false);
        }

        protected bool validateSecurity(MucLuc sec)
        {
            bool result = true;
            if (!um.validateSecName(sec.Name))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tên này đã có trong hệ thống. Xin chọn một tên khác');}</script>");
            }
            if (!um.validateSecNameNull(sec.Name))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tên không được phép để trống');}</script>");
            }

            if (!um.validateSecLayerNull(sec.Layer.ToString()))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Cấp mục lục không được phép để trống');}</script>");
            }
            if (!um.validateSecNameNull(sec.Description))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Mô tả không được phép để trống');}</script>");
            }
            /*
            if (!um.validateSecLayer(sec.Layer))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Cấp mục lục này  đã có trong hệ thống. Xin chọn một mức khác khác');}</script>");
            }*/
            return result;
        }

        protected void listAllSec()
        {
            try
            {
                DataTable dt = um.getAllSec();
                dgvApprover.DataSource = dt;
                dgvApprover.DataBind();
                dgvApprover.Columns[0].Visible = false;
                dgvApprover.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "listAllSec", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvApprover.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("cbChoose");
                    if (chk.Checked)
                    {
                        string sID = row.Cells[0].Text.Trim();
                        if (!sID.Equals(""))
                        {
                            int ID = Int32.Parse(sID);
                            um.deleteMucLuc(ID);
                        }
                    }
                }
                Session[Su_MucLucLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QLMucLuc.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }
}