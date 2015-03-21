﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Logic;
using Function;

namespace Picklist
{
    public partial class QuanLyDoMat : System.Web.UI.Page
    {
        DoMat sec;
        Su_DoMatLogic um = new Su_DoMatLogic();
        string SecID = "";
        static string ID_Delete = "";
        string classobject = "QuanLyDoMat.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session[Su_DoMatLogic.SESSION_SEC_ID] != null)
                    {

                        btAddApprover.Text = "Cập nhật";
                        sec = um.getDoMat(Int32.Parse(Session[Su_DoMatLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {
                            tbxName.Text = sec.Name;
                            tbxDescription.Text = sec.Description;
                            ntbLayer.Text = sec.Layer.ToString().Trim();
                        }
                    }
                    else
                    {
                        sec = new DoMat();
                    }

                    listAllSec();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
            else
            {
                string tId = "";
                if (Session[Su_DoMatLogic.SESSION_SEC_ID] != null)
                {
                    tId = Session[Su_DoMatLogic.SESSION_SEC_ID].ToString();
                    sec = new DoMat();
                    int iId = 0;
                    Int32.TryParse(tId, out iId);
                    sec.ID = iId;
                    sec.Name = tbxName.Text;
                    sec.Description = tbxDescription.Text;
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
                        Session[Su_DoMatLogic.SESSION_SEC_ID] = ID;
                        Response.Redirect("QuanLyDoMat.aspx", false);
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
                    sec = new DoMat();
                    sec.Name = tbxName.Text;
                    sec.Description = tbxDescription.Text;
                    sec.Layer = Int32.Parse(ntbLayer.Text);

                }
                else
                {

                    sec.Name = tbxName.Text;
                    sec.Description = tbxDescription.Text;
                    sec.Layer = Int32.Parse(ntbLayer.Text);

                }
                if (!isUpdate())
                {
                    if (validateSecurity(sec))
                    {
                        um.addDoMat(sec);
                        tbxDescription.Text = "";
                        tbxName.Text = "";
                        ntbLayer.Text = "0";
                    }
                }
                else
                {
                    if (um.validateSecName4Update(sec.Name, sec.ID))
                    {
                        um.updateDoMat(sec);
                        tbxDescription.Text = "";
                        tbxName.Text = "";
                        ntbLayer.Text = "0";
                    }
                    else
                    {
                        Logger.logmessage(classobject, "btAddApprover_Click", "Validate fail 4 update " + sec.Name);
                    }
                }

                Session[Su_DoMatLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QuanLyDoMat.aspx", false);
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
            if (Session[Su_DoMatLogic.SESSION_SEC_ID] != null)
            {
                SecID = Session[Su_DoMatLogic.SESSION_SEC_ID].ToString();
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
            Session[Su_DoMatLogic.SESSION_SEC_ID] = null;
            Response.Redirect("Default.aspx", false);
        }

        protected bool validateSecurity(DoMat sec)
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
                            um.deleteDoMat(ID);
                        }
                    }
                }
                Session[Su_DoMatLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QuanLyDoMat.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }


    }
}
