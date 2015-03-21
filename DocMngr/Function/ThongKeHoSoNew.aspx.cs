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
    public partial class ThongKeHoSoNew : System.Web.UI.Page
    {

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();
        string classobject = "ThongKeHoSoNew.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bindingDDLs();
                    bindingDDLCoQuan();
                    bindingDDLPhong();
                    searchAll();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }
        public void bindingDDLPhong()
        {
            try
            {
                PhongLuuTruLogic phongLogic = new PhongLuuTruLogic();
                DataTable dt = phongLogic.getAllSec();

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
        public void bindingDDLs()
        {
            try
            {
                Su_ThoiHanBaoQuanLogic bqLogic = new Su_ThoiHanBaoQuanLogic();
                DataTable dt = bqLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlThoiHan);

                Su_TinhTrangVatLyLogic vlLogic = new Su_TinhTrangVatLyLogic();
                dt = vlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlTinhTrangVatLy);

                Su_MucLucLogic mlLogic = new Su_MucLucLogic();
                dt = mlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlMucluc);

                QuanLyCheDoSuDungLogic cdLogic = new QuanLyCheDoSuDungLogic();
                dt = cdLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlCheDoSuDung);


            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLs", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        public void bindingDDLCoQuan()
        {
            try
            {
                Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
                ListItem root = new ListItem("-- All --", "");
                string Code = "";
                string Name = "";
                DataTable dtCoquan = coquanLogic.getAllSec();
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
                string ThoiHan = ddlThoiHan.SelectedValue;
                string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
                string Mucluc = ddlMucluc.SelectedValue;
                string CheDoSuDung = ddlCheDoSuDung.SelectedValue;
                string Keyword = txtKeyword.Text;
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                dt = searcher.timkiemHoSo(conn, CoQuan, Phong, ThoiHan, TinhTrangVatLy, Mucluc, CheDoSuDung, Keyword);
                dgrResult.DataSource = dt;
                dgrResult.DataBind();

                if (dt.Rows.Count > 0) { dgrResult.Columns[0].Visible = false; }


            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "search", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        private void searchAll()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                dt = searcher.timkiemHoSo(conn, "", "", "", "", "", "", "");
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
                        Session[HoSoLogic.SESSION_SEC_ID] = ID;
                       // Response.Redirect("ViewHoSo.aspx", false);
                        ClientScript.RegisterStartupScript(this.Page.GetType(), "",
 "window.open('ViewHoSo.aspx','Graph','height=700,width=900');", true);
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "changing", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }

}