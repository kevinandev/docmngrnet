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

using System.Reflection; // For Missing.Value and BindingFlags
using System.Diagnostics; // to ensure EXCEL process is really killed
using System.IO;
using System.Text;
using Logic;
using FunctionGroup.Dao;
using FunctionGroup.Logic;
using log4net;
using System.Collections.Generic;

namespace Function
{
    public partial class TimKiemVanBan : System.Web.UI.Page
    {

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();
        AttachmentLogic attachLogic = null;
        ILog logger = log4net.LogManager.GetLogger("File");

        string classobject = "TimKiemVanBan.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                attachLogic = new AttachmentLogic();

                if (!IsPostBack)
                {
                    try
                    {
                        bindingDDLs();
                        bindingDDLCoQuan();
                        bindingDDLPhong();
                        //searchAll();
                    }
                    catch (Exception ex)
                    {
                        Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("PageLoad error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
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
        protected void onDgvDatabind(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView item = (DataRowView)e.Row.DataItem;
                    if (item != null)
                    {
                        int id = (int)item["ID"];
                        List<Su_Attachment> lstAttachment = attachLogic.getAttachment(AttachmentLogic.ATTACHMENT_TYPE_VB, id);
                        Repeater rpt = (Repeater)e.Row.FindControl("rptAttachment");
                        if (rpt != null)
                        {
                            rpt.DataSource = lstAttachment;
                            rpt.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Dgv data bind error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);

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

                DataTable dt = new DataTable();


                Su_TinhTrangVatLyLogic vlLogic = new Su_TinhTrangVatLyLogic();
                dt = vlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlTinhTrangVatLy);

                LoaiVanBanLogic mlLogic = new LoaiVanBanLogic();
                dt = mlLogic.getAllSec();
                bindingEachDDL(dt, "All", ddlLoaiVanBan);


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
                string LoaiVanBan = ddlLoaiVanBan.SelectedValue;
                string ThoiGianFrom = Request.Form[txtThoiGianFrom.UniqueID];
                string ThoiGianTo = Request.Form[txtThoiGianTo.UniqueID];
                string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
                string KyHieuVanBan = txtKyHieuVanBan.Text;
                string TacGia = txtTacGia.Text;
                string Keyword = txtKeyword.Text;
                // System.Data.DataTable dt = new System.Data.DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                System.Data.DataTable dtresult = new System.Data.DataTable();
                dtresult = searcher.timkiemVanBan(conn, CoQuan, Phong, TinhTrangVatLy, LoaiVanBan, KyHieuVanBan, TacGia, ThoiGianFrom, ThoiGianTo,Keyword);
                dgrResult.DataSource = dtresult;
                dgrResult.DataBind();
                if (dtresult.Rows.Count > 0) { dgrResult.Columns[0].Visible = false; }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "search", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }

        }
        protected void downloadAttach(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                string attachmentID = btn.CommandArgument.Trim();
                int iAttachmentID = 0;
                Int32.TryParse(attachmentID, out iAttachmentID);
                if (iAttachmentID > 0)
                {
                    Su_Attachment at = attachLogic.getAttachmentByID(iAttachmentID);
                    string[] fileNames = at.path.Split('\\');
                    if (at.mime_type == null || at.mime_type.Trim().Length == 0)
                    {
                        Response.ContentType = "application/unknown";
                    }
                    else
                    {
                        Response.ContentType = at.mime_type;
                    }
                    Response.AppendHeader("content-disposition",
                            "attachment; filename=" + HttpUtility.UrlEncode(fileNames[fileNames.Length - 1], System.Text.Encoding.UTF8));
                    Response.TransmitFile(at.path);
                    Response.End();
                }
                else
                {
                    throw new Exception("Attachment not found.");
                }
            }
            catch (Exception ex)
            {
                logger.Error("downloadAttach error: ", ex);
            }
        }
        private void searchAll()
        {
            try
            {
                System.Data.DataTable dtresult = new System.Data.DataTable();
                dtresult = new System.Data.DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                dtresult = searcher.timkiemVanBan(conn, "", "", "", "", "", "", "", "","");
                dgrResult.DataSource = dtresult;
                dgrResult.DataBind();
                if (dtresult.Rows.Count > 0) { dgrResult.Columns[0].Visible = false; }
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "searchAll", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }


        }
        public void bindingDDL(System.Data.DataTable dt, string head, DropDownList ddl)
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
        protected void btTimKiem_Click(object sender, EventArgs e)
        {
            search();
        }
        protected void changing(object sender, System.EventArgs e)
        {
            //downling group of document but pdf file only, another file will be passed
            try
            {
                ImageButton ibtn1 = sender as ImageButton;
                string IDv = ibtn1.CommandArgument.Trim();
                Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = IDv;
                ClientScript.RegisterStartupScript(this.Page.GetType(), "",
                        "window.open('ChiTietVBTrongHoSo.aspx','Graph','scrollbars=yes, resizable=yes,height=700,width=900');", true);
                //              Response.Redirect("ChiTietVBTrongHoSo.aspx", false);
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
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dgrResult.Columns.Count; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dgrResult.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dgrResult.Rows.Count; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dgrResult.Rows[i].Cells.Count; j++)
                        stLine = stLine.ToString() + Convert.ToString(dgrResult.Rows[i].Cells[j].Text) + "\t";
                    stOutput += stLine + "\r\n";
                }


                Encoding utf8 = Encoding.GetEncoding("utf-8");
                byte[] output = utf8.GetBytes(stOutput);
                string filename = "vanban.xls";
                string filePath = "D:\\vanban.xls";
                FileStream fs = new FileStream(filePath, FileMode.Create);
                StreamWriter bw = new StreamWriter(fs, Encoding.Unicode);
                bw.Write(stOutput); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();

                Response.ClearContent();
                Response.ContentEncoding = Encoding.Unicode;
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
                // Response.ContentType = "application/excel";
                Response.ContentType = "xls/xlsx";
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btexport_Click", ex.Message + ex.StackTrace);
                //Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        public string GridViewToHtml(GridView grid)
        {
            StringBuilder sTringBuilder = new StringBuilder();
            StringWriter sTringWriter = new StringWriter(sTringBuilder);
            HtmlTextWriter hTmlTextWriter = new HtmlTextWriter(sTringWriter);
            grid.RenderControl(hTmlTextWriter);
            return sTringBuilder.ToString();
        }

    }
}