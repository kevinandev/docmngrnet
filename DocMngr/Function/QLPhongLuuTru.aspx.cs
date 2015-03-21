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
    public partial class QLPhongLuuTru : System.Web.UI.Page
    {

        PhongLuuTru sec;
        PhongLuuTruLogic um = new PhongLuuTruLogic();
        Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
        string SecID = "";
        string classobject = "QLPhongLuuTru.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bindingDDLCoQuan();
                    listAllSec();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }
        public void bindingDDLCoQuan()
        {
            try
            {
                ListItem root = new ListItem("-- All --", "");
                string Code = "";
                string Name = "";
                DataTable dtCoquan = coquanLogic.getAllSec();
                // them đòng dầu tiên 
                ddlCoquan.Items.Add(root);
                foreach (DataRow r in dtCoquan.Rows)
                {
                    Code = r["Code"].ToString();
                    Name = r["Name"].ToString();
                    ListItem item = new ListItem(Name, Code);
                    ddlCoquan.Items.Add(item);
                }
                ddlCoquan.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLCoQuan", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
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
                Logger.logmessage(classobject, "dgvApprover_PageIndexChanging", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void listAllSec(string coquan, string textsearch)
        {
            try
            {
                DataTable dt = um.getAllSec(ddlCoquan.SelectedValue, txtKeyword.Text);
                dgvApprover.DataSource = dt;
                dgvApprover.Columns[0].Visible = false;
                dgvApprover.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "listAllSec", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
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
        protected void changing(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                Session[PhongLuuTruLogic.SESSION_SEC_ID] = lbtn.CommandArgument.Trim();
                Response.Redirect("ThemMoiPhongLuuTru.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "changing", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
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
                        int ID = Int32.Parse(row.Cells[0].Text.ToString().Trim());
                        um.deletePhongLuuTru(ID);
                    }
                }
                Session[PhongLuuTruLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QLPhongLuuTru.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            try
            {
                listAllSec(ddlCoquan.SelectedValue, txtKeyword.Text);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btSearch_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Session[PhongLuuTruLogic.SESSION_SEC_ID] = null;
                Response.Redirect("ThemMoiPhongLuuTru.aspx", false);

            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAdd_Click", ex.Message + ex.StackTrace); Response.Redirect("../ThongBaoLoi.aspx", false);
            }
        }

        //protected void btDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool flag = false;

        //        foreach (GridViewRow row in dgvApprover.Rows)
        //        {
        //            CheckBox chk = (CheckBox)row.FindControl("cbChoose");
        //            if (chk.Checked)
        //            {
        //                flag = true;
        //                int ID = Int32.Parse(row.Cells[0].Text.ToString().Trim());
        //                um.deletePhongLuuTru(ID);
        //            }
        //        }
        //        if (flag)
        //        {
        //            Response.Redirect("QLPhongLuuTru.aspx", false);
        //        }
        //        else { Response.Write("<script language='javascript'> { alert('Hãy chọn phông muốn xóa');}</script>"); }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.logmessage(classobject, "btAdd_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
        //    }
        //}


    }
}