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
using System;
using System.Text;
using System.IO;
using Logic;
using System.Collections.Generic;
using FunctionGroup.Bean;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Function
{
    public partial class TimKiemHoso : System.Web.UI.Page
    {
        string classobject = "TimKiemHoso.aspx.cs";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindingDDLs();
                bindingDDLCoQuan();
                bindingDDLPhong();
                //searchAll();
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
                string ThoiGianStartFrom = Request.Form[txtThoiGianStartFrom.UniqueID];
                string ThoiGianStartTo = Request.Form[txtThoiGianStartTo.UniqueID];
                string ThoiGianEndFrom = Request.Form[txtThoiGianEndFrom.UniqueID];
                string ThoiGianEndTo = Request.Form[txtThoiGianEndTo.UniqueID];
                string Keyword = txtKeyword.Text;
                System.Data.DataTable dtresult = new System.Data.DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                dtresult = searcher.timkiemHoSoAdvance(conn, CoQuan, Phong, ThoiHan, TinhTrangVatLy, Mucluc, CheDoSuDung, Keyword,
                    ThoiGianStartFrom, ThoiGianStartTo, ThoiGianEndFrom, ThoiGianEndTo);
                dgrResult.DataSource = dtresult;
                dgrResult.DataBind();
                if (dtresult.Rows.Count > 0) 
                { 
                    dgrResult.Columns[0].Visible = false;
                }
                conn.Close();
                conn.Dispose();
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
                System.Data.DataTable dtresult = new System.Data.DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                dtresult = searcher.timkiemHoSoAdvance(conn, "", "", "", "", "", "", "", "", "", "", "");
                dgrResult.DataSource = dtresult;
                dgrResult.DataBind();
                if (dtresult.Rows.Count > 0)
                { 
                    dgrResult.Columns[0].Visible = false; 
                }
                conn.Close();
                conn.Dispose();
            
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

                DataTable dt = new DataTable();


                Su_TinhTrangVatLyLogic vlLogic = new Su_TinhTrangVatLyLogic();
                dt = vlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlTinhTrangVatLy);

                Su_ThoiHanBaoQuanLogic THLogic = new Su_ThoiHanBaoQuanLogic();
                dt = THLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlThoiHan);

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
                ListItem root = new ListItem("-- All --", "");
                string Code = "";
                string Name = "";
                Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
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
                Session[HoSoLogic.SESSION_SEC_ID] = ibtn1.CommandArgument.Trim();
                ClientScript.RegisterStartupScript(this.Page.GetType(), "",
               "window.open('ViewHoSo.aspx','Graph','height=700,width=900');", true);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "changing", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btexport_Click(object sender, EventArgs e)
        {
            try
            {
                string CoQuan = ddlCoQuan.SelectedValue;
                string Phong = ddlPhong.SelectedValue;
                string ThoiHan = ddlThoiHan.SelectedValue;
                string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
                string Mucluc = ddlMucluc.SelectedValue;
                string CheDoSuDung = ddlCheDoSuDung.SelectedValue;
                string ThoiGianStartFrom = Request.Form[txtThoiGianStartFrom.UniqueID];
                string ThoiGianStartTo = Request.Form[txtThoiGianStartTo.UniqueID];
                string ThoiGianEndFrom = Request.Form[txtThoiGianEndFrom.UniqueID];
                string ThoiGianEndTo = Request.Form[txtThoiGianEndTo.UniqueID];
                string Keyword = txtKeyword.Text;
                System.Data.DataTable dtresult = new System.Data.DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                dtresult = searcher.timkiemHoSoAdvance(conn, CoQuan, Phong, ThoiHan, TinhTrangVatLy, Mucluc, CheDoSuDung, Keyword,
                    ThoiGianStartFrom, ThoiGianStartTo, ThoiGianEndFrom, ThoiGianEndTo);
                List<ReportHoSoBean> lst=new List<ReportHoSoBean>();
                int count = 0;
                foreach (DataRow r in dtresult.Rows)
                {
                    ReportHoSoBean bean = new ReportHoSoBean();
                    bean.DenNgay = r["ThoiGianKetThuc"].ToString();
                    bean.HopSo = r["HopSo"].ToString();
                    bean.HoSoSo = r["HoSoSo"].ToString();
                    bean.MaPhong = r["MaPhong"].ToString();
                    string tSL = r["SoLuong"].ToString();
                    int iSL = 0;
                    Int32.TryParse(tSL, out iSL);
                    bean.SoTo = iSL;
                    bean.STT = count + 1;
                    count++;
                    bean.ThoiHanBaoQuan = r["ThoiHanBaoQuanv"].ToString();
                    bean.TieuDe = r["TieuDe"].ToString();
                    bean.TuNgay = r["ThoiGianBatDau"].ToString();
                    lst.Add(bean);
                }
                conn.Close();
                conn.Dispose();
                ReportDocument rpDoc = new ReportDocument();
                rpDoc.Load(Server.MapPath("~/Report/ReportHoSo.rpt"));
                rpDoc.SetDataSource(lst);
                Response.ClearContent();
                rpDoc.ExportToHttpResponse(ExportFormatType.Excel,Response,true,"hoso.xlsx");
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btexport_Click", ex.Message + ex.StackTrace);
                //Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
    }
}