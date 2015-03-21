using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Logic;

namespace Function
{
    public partial class ThongKeTaiLieu : System.Web.UI.Page
    {

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();
        string classobject = "ThongKeTaiLieu.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    SqlConnection conn = DatabaseUtil.getConnection();
                    bindingDDLs(conn);
                    bindingDDLCoQuan(conn);
                    bindingDDLPhong(conn);
                    searchAll(conn);
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }
        public void bindingDDLPhong(SqlConnection conn)
        {
            try
            {
                PhongLuuTruLogic phongLogic = new PhongLuuTruLogic();
                DataTable dt = phongLogic.getAllSecByConnection(conn);

                ListItem root = new ListItem("-- All --", "");
                string Code = "";
                string Name = "";

                // them đòng dầu tiên 
                ddlPhong.Items.Add(root);
                foreach (DataRow r in dt.Rows)
                {
                    Code = r["MaPhong"].ToString();
                    Name = r["TenPhong"].ToString();
                    ListItem item = new ListItem(Name, Code);
                    ddlPhong.Items.Add(item);
                }
                ddlPhong.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLPhong", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        public void bindingEachDDL(DataTable dt, string head, DropDownList ddl)
        {
            try
            {
                ListItem root = new ListItem("--" + head + "--", "");
                string ID = "";
                string Name = "";
                // them đòng dầu tiên 
                ddl.Items.Add(root);
                foreach (DataRow r in dt.Rows)
                {
                    ID = r["ID"].ToString();
                    Name = r["Name"].ToString();
                    ListItem item = new ListItem(Name, ID);
                    ddl.Items.Add(item);
                }
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingEachDDL", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        public void bindingDDLs(SqlConnection conn)
        {
            try
            {
                Su_TinhTrangVatLyLogic vlLogic = new Su_TinhTrangVatLyLogic();
                DataTable dt = vlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlTinhTrangVatLy);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLs", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }

        }
        public void bindingDDLCoQuan(SqlConnection conn)
        {
            try
            {
                Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
                ListItem root = new ListItem("-- All --", "");
                string Code = "";
                string Name = "";
                DataTable dtCoquan = coquanLogic.getAllSecByConnection(conn);
                // them đòng dầu tiên 
                ddlCoQuan.Items.Add(root);
                foreach (DataRow r in dtCoquan.Rows)
                {
                    Code = r["Code"].ToString();
                    Name = r["Name"].ToString();
                    ListItem item = new ListItem(Name, Code);
                    ddlCoQuan.Items.Add(item);
                }
                ddlCoQuan.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLCoQuan", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        private void search()
        {
            try
            {
                string CoQuan = ddlCoQuan.SelectedValue;
                string Phong = ddlPhong.SelectedValue;
                string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
                string KyHieuVanBan = txtKiHieuVanBan.Text;
                string Tacgia = txtTacgia.Text;
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                dt = searcher.timkiemVanBan(conn, CoQuan, Phong, TinhTrangVatLy, "", KyHieuVanBan, Tacgia, "", "","");
                dgrResult.DataSource = dt;
                dgrResult.DataBind();
                if (dt.Rows.Count > 0) { dgrResult.Columns[0].Visible = false; }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "search", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        private void searchAll(SqlConnection conn)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = searcher.timkiemVanBan(conn, "", "","", "", "", "", "", "","");
                dgrResult.DataSource = dt;
                dgrResult.DataBind();
                if (dt.Rows.Count > 0) { dgrResult.Columns[0].Visible = false; }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "searchAll", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btTimKiem_Click(object sender, EventArgs e)
        {
            search();
        }

        public void bindingDDL(DataTable dt, string head, DropDownList ddl)
        {
            try
            {
                ListItem root = new ListItem("--" + head + "--", "");
                string ID = "";
                string Name = "";
                // them đòng dầu tiên 
                ddl.Items.Add(root);
                foreach (DataRow r in dt.Rows)
                {
                    ID = r["ID"].ToString();
                    Name = r["Name"].ToString();
                    ListItem item = new ListItem(Name, ID);
                    ddl.Items.Add(item);
                }
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDL", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void changing(object sender, System.EventArgs e)
        {
            //downling group of document but pdf file only, another file will be passed
            try
            {
                ImageButton ibtn1 = sender as ImageButton;
                foreach (GridViewRow r in dgrResult.Rows)
                {
                    string ID = Server.HtmlDecode(r.Cells[0].Text.Trim());
                    if ( ID == ibtn1.CommandArgument.Trim())
                    {
                        Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = ID;
                        Response.Redirect("ThemVBTrongHoSo.aspx", false);
                       /* ClientScript.RegisterStartupScript(this.Page.GetType(), "",
  "window.open('ThemVBTrongHoSo.aspx','Graph','height=700,width=900');", true);*/
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "changing", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void viewing(object sender, System.EventArgs e)
        {
            //downling group of document but pdf file only, another file will be passed
            try
            {
                ImageButton ibtn1 = sender as ImageButton;
                foreach (GridViewRow r in dgrResult.Rows)
                {
                    string ID = Server.HtmlDecode(r.Cells[0].Text.Trim());
                    if (ID == ibtn1.CommandArgument.Trim())
                    {
                        Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = ID;
                        //Response.Redirect("ChiTietVBTrongHoSo.aspx", false);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "",
  "window.open('ChiTietVBTrongHoSo.aspx','Graph','height=700,width=900');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "viewing", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                VanBanTrongHoSoLogic um = new VanBanTrongHoSoLogic();

                foreach (GridViewRow row in dgrResult.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("cbChoose");
                    if (chk.Checked)
                    {
                        flag = true;
                        string id = row.Cells[0].Text.ToString().Trim();
                        int ID = Int32.Parse(id);
                        um.deleteVanBanTrongHoSo(ID);
                    }
                }
                if (flag)
                {
                    Response.Redirect("ThongKeTaiLieu.aspx", false);
                }
                else { Response.Write("<script language='javascript'> { alert('Hãy chọn tài liệu muốn xóa');}</script>"); }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }

}