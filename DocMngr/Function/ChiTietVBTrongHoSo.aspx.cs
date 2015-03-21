using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Logic;
using FunctionGroup.Dao;
using FunctionGroup.Logic;

namespace Function
{
    public partial class ChiTietVBTrongHoSo : System.Web.UI.Page
    {
        DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        VanBanTrongHoSo sec;
        VanBanTrongHoSoLogic um = new VanBanTrongHoSoLogic();
        AttachmentLogic attachLogic = new AttachmentLogic();
       string classobject = "ChiTietVBTrongHoSo.aspx.cs";
        string SecID = "";
        public static string fileUrl = "";
        public static string fileName = "";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        //private List<su_dinhkiemvanban> lstAttachment = new List<su_dinhkiemvanban>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    bindingDDLs();
                    if (Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] != null)
                    {

                        btAddApprover.Text = "Cập nhật";
                        ldhead.InnerText = "CHI TIẾT VĂN BẢN TRONG HỒ SƠ";

                        sec = um.getVanBanTrongHoSo(Int32.Parse(Session[VanBanTrongHoSoLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {
                            ddlCoQuan.SelectedValue = sec.Coquan;
                            ddlPhong.SelectedValue = sec.MaPhong;
                            ddlMucLuc.SelectedValue = sec.MucLucSo;
                            ddlNgonNgu.SelectedValue = sec.NgonNgu;
                            txtHoSoSo.Text = sec.HoSoSo;
                            txtKyHieu.Text = sec.KyHieuVanBan;
                            txtGhiChu.Text = sec.GhiChu;
                            txtButtich.Text = sec.ButTich;
                            txtSoluongto.Text = sec.SoLuongTo;
                            txtThoiGian.Text = sec.ThoiGian;
                            txtKyhieuThongtin.Text = sec.KiHieuThongTin;
                            txtToSo.Text = sec.ToSo;
                            txtTrichYeu.Text = sec.TrichYeu;
                            txtTacgia.Text = sec.TacGia;
                            ddlLoaivanban.SelectedValue = sec.LoaiVanBan;
                            ddlMucDoTinCay.SelectedValue = sec.MucDoTinCay;
                            ddlDomat.SelectedValue = sec.DoMat;
                            ddlTinhTrang.SelectedValue = sec.TinhTrangVatLy;
                            // fileUrl = "http://" +  Request.Url.Host + ":" + Request.Url.Port + "/files/" + sec.FilePath;                      
                            //fileUrl = "files/" + sec.FilePath;
                            //fileName = sec.FilePath;
                            List<Su_Attachment> lstAttach = attachLogic.getAttachment(AttachmentLogic.ATTACHMENT_TYPE_VB, sec.ID);
                            rptDocs.DataSource = lstAttach;
                            rptDocs.DataBind();
                        }
                    }
                    else
                    {
                        ldhead.InnerText = "THÊM MỚI VĂN BẢN TRONG HỒ SƠ";
                        sec = new VanBanTrongHoSo();
                    }
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace);  Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }
         
        public string uploadfile()
        {

            return "";
            
        }


        protected void bindingDDLs()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                string com = "Select * from Su_CoQuan order by GroupID";
                SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
                Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
                DataTable dt = coquanLogic.getAllSec();

                ddlCoQuan.DataSource = dt;
                ddlCoQuan.DataBind();
                ddlCoQuan.DataTextField = "Name";
                ddlCoQuan.DataValueField = "Code";
                ddlCoQuan.DataBind();


                com = "Select TenPhong, MaPhong from Su_PhongLuuTru ";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlPhong.DataSource = dt;
                ddlPhong.DataBind();
                ddlPhong.DataTextField = "TenPhong";
                ddlPhong.DataValueField = "MaPhong";
                ddlPhong.DataBind();

                com = "Select Name,ID from Su_NgonNgu order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlNgonNgu.DataSource = dt;
                ddlNgonNgu.DataBind();
                ddlNgonNgu.DataTextField = "Name";
                ddlNgonNgu.DataValueField = "ID";
                ddlNgonNgu.DataBind();

                com = "Select Name,ID from Su_ThoiHanBaoQuan order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlThoiHan.DataSource = dt;
                ddlThoiHan.DataBind();
                ddlThoiHan.DataTextField = "Name";
                ddlThoiHan.DataValueField = "ID";
                ddlThoiHan.DataBind();

                com = "Select Name,ID from Su_TinhTrangVatLy order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlTinhTrang.DataSource = dt;
                ddlTinhTrang.DataBind();
                ddlTinhTrang.DataTextField = "Name";
                ddlTinhTrang.DataValueField = "ID";
                ddlTinhTrang.DataBind();


                com = "Select Name,ID from Su_MucLuc order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlMucLuc.DataSource = dt;
                ddlMucLuc.DataBind();
                ddlMucLuc.DataTextField = "Name";
                ddlMucLuc.DataValueField = "ID";
                ddlMucLuc.DataBind();

                com = "Select Name,ID from Su_LoaiVanban order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlLoaivanban.DataSource = dt;
                ddlLoaivanban.DataBind();
                ddlLoaivanban.DataTextField = "Name";
                ddlLoaivanban.DataValueField = "ID";
                ddlLoaivanban.DataBind();


                com = "Select Name,ID from Su_DoMat order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlDomat.DataSource = dt;
                ddlDomat.DataBind();
                ddlDomat.DataTextField = "Name";
                ddlDomat.DataValueField = "ID";
                ddlDomat.DataBind();

                com = "Select Name,ID from Su_MucDoTinCay order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlMucDoTinCay.DataSource = dt;
                ddlMucDoTinCay.DataBind();
                ddlMucDoTinCay.DataTextField = "Name";
                ddlMucDoTinCay.DataValueField = "ID";
                ddlMucDoTinCay.DataBind();

                adpt.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLs", ex.Message + ex.StackTrace);  Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btAddApprover_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isUpdate())
                {
                    sec = new VanBanTrongHoSo();
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = ddlMucLuc.SelectedValue;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.KyHieuVanBan = txtKyHieu.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ButTich = txtButtich.Text;
                    sec.SoLuongTo = txtSoluongto.Text;
                    sec.ThoiGian = txtThoiGian.Text;
                    sec.KiHieuThongTin = txtKyhieuThongtin.Text;
                    sec.ToSo = txtToSo.Text;
                    sec.TrichYeu = txtTrichYeu.Text;
                    sec.TacGia = txtTacgia.Text;
                    sec.LoaiVanBan = ddlLoaivanban.SelectedValue;
                    sec.MucDoTinCay = ddlMucDoTinCay.SelectedValue;
                    sec.DoMat = ddlDomat.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                    //sec.FilePath = uploadfile();
                }
                else
                {
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = ddlMucLuc.SelectedValue;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.KyHieuVanBan = txtKyHieu.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ButTich = txtButtich.Text;
                    sec.SoLuongTo = txtSoluongto.Text;
                    sec.ThoiGian = txtThoiGian.Text;
                    sec.KiHieuThongTin = txtKyhieuThongtin.Text;
                    sec.ToSo = txtToSo.Text;
                    sec.TrichYeu = txtTrichYeu.Text;
                    sec.TacGia = txtTacgia.Text;
                    sec.LoaiVanBan = ddlLoaivanban.SelectedValue;
                    sec.MucDoTinCay = ddlMucDoTinCay.SelectedValue;
                    sec.DoMat = ddlDomat.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                }
                if (!isUpdate())
                {
                    if (validateObject(sec))
                    {
                       int newId = um.addVanBanTrongHoSo(sec);
                       sec.ID = newId;
                    }
                    // listAllSec();
                }
                else
                {
                    if (validateObject(sec))
                    {
                        sec.ID = Int32.Parse(SecID);
                        um.updateVanBanTrongHoSo(sec);
                    }
                }
                Response.Redirect("QLHoSo.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAddApprover_Click", ex.Message + ex.StackTrace);  Response.Redirect("~/ThongBaoLoi.aspx", false);
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
                Logger.logmessage(classobject, "downloadAttach", "Download error: " + ex.Message);
            }
        }
        protected bool validateObject(VanBanTrongHoSo sec)
        {
            bool result = true;
            return result;
        }

        private bool isUpdate()
        {
            if (Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] != null)
            {
                SecID = Session[VanBanTrongHoSoLogic.SESSION_SEC_ID].ToString();
                return true;
            }
            return false;
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("QLHoSo.aspx", false);
        }


    }
}