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
using FunctionGroup.utils;
using FunctionGroup.Util;

namespace Function
{
    public partial class ThemVBTrongHoSo : System.Web.UI.Page
    {

        VanBanTrongHoSo sec;
        VanBanTrongHoSoLogic um = new VanBanTrongHoSoLogic();
        HoSo hs = new HoSo();
        HoSoLogic hsLogic = new HoSoLogic();
        AttachmentLogic attachLogic = new AttachmentLogic();
        string SecID = "";
        public string fileUrl = "";
        public string fileName = "";
        string classobject = "ThemVBTrongHoSo.aspx.cs";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        List<string> lstInfo = new List<string>();
        List<string> lstError = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    bindingDDLs();
                    if (Request.QueryString["hid"] != null)
                    {
                        hs = hsLogic.getHoSo(Int32.Parse(Request.QueryString["hid"].ToString()));
                        hidHoSo.Value = Request.QueryString["hid"].ToString();
                        ddlCoQuan.SelectedValue = hs.Coquan;
                        txtMucLuc.Text = hs.MucLucSo;
                        ddlPhong.SelectedValue = hs.MaPhong;
                        txtHoSoSo.Text = hs.HoSoSo;
                    }
                    if (Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] != null)
                    {
                        btAddApprover.Text = "Cập nhật tiếp tục đính kèm";
                        ldhead.InnerText = "CHỈNH SỬA VĂN BẢN TRONG HỒ SƠ";

                        sec = um.getVanBanTrongHoSo(Int32.Parse(Session[VanBanTrongHoSoLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {
                            hidHoSo.Value = sec.Hoso_ID;
                            ddlCoQuan.SelectedValue = sec.Coquan;
                            ddlPhong.SelectedValue = sec.MaPhong;
                            txtMucLuc.Text = hs.MucLucSo;
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
                            List<Su_Attachment> lstAttach = attachLogic.getAttachment(AttachmentLogic.ATTACHMENT_TYPE_VB, sec.ID);
                            dgvAttachment.DataSource = lstAttach;
                            dgvAttachment.DataBind();
                            if (sec.ThoiHanBaoQuan != null && sec.ThoiHanBaoQuan.Length > 0)
                            {
                                ddlThoiHan.SelectedValue = sec.ThoiHanBaoQuan;
                            }
                        }
                        else
                        {
                            string mss = HtmlUtil.getErrorTag("Không tìm thấy văn bản.");
                            Logger.logmessage(classobject, "Page_Load", "Khong tim thay van ban: " + Session[VanBanTrongHoSoLogic.SESSION_SEC_ID].ToString()); 
                        }
                    }
                    else
                    {
                        ldhead.InnerText = "THÊM MỚI VĂN BẢN TRONG HỒ SƠ";
                        sec = new VanBanTrongHoSo();
                        if (Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE] != null)
                        {
                            VanBanTrongHoSo temp = (VanBanTrongHoSo)Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE];
                            if (temp != null)
                            {
                                txtKyhieuThongtin.Text = temp.KiHieuThongTin;
                                txtSoluongto.Text = temp.SoLuongTo;
                                txtThoiGian.Text = temp.ThoiGian;
                                if (temp.NgonNgu != null && temp.NgonNgu.Length > 0)
                                {
                                    ddlNgonNgu.SelectedValue = temp.NgonNgu;
                                }
                                txtToSo.Text = temp.ToSo;
                                txtTrichYeu.Text = temp.TrichYeu;
                                if (temp.LoaiVanBan != null && temp.LoaiVanBan.Length > 0)
                                {
                                    ddlLoaivanban.SelectedValue = temp.LoaiVanBan;
                                }
                                if (temp.DoMat != null && temp.DoMat.Length > 0)
                                {
                                    ddlDomat.SelectedValue = temp.DoMat;
                                }
                                txtTacgia.Text = temp.TacGia;
                                txtButtich.Text = temp.ButTich;
                                if (temp.MucDoTinCay != null && temp.MucDoTinCay.Length > 0)
                                {
                                    ddlMucDoTinCay.SelectedValue = temp.MucDoTinCay;
                                }
                                if (temp.ThoiHanBaoQuan != null && temp.ThoiHanBaoQuan.Length > 0)
                                {
                                    ddlThoiHan.SelectedValue = temp.ThoiHanBaoQuan;
                                }
                                txtGhiChu.Text = temp.GhiChu;
                                if (temp.TinhTrangVatLy != null && temp.TinhTrangVatLy.Length > 0)
                                {
                                    ddlTinhTrang.SelectedValue = temp.TinhTrangVatLy;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
        }

        public string uploadfile()
        {

            String path = Server.MapPath("~/files/");
            string FilePath = "";
            if (Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] == null)
            {
                if (FileUpload1.HasFile)
                {

                    FilePath = FileUpload1.FileName;
                    try
                    {
                        FileUpload1.PostedFile.SaveAs(path
                            + FileUpload1.FileName);
                        FilePath = FileUpload1.FileName;
                    }
                    catch (Exception ex)
                    {
                        Logger.logmessage(classobject, "uploadfile", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                    }
                }
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    // cap nhat lai file moi vao db va xoa file cu o dia vat ly 
                    FilePath = FileUpload1.FileName;
                    try
                    {
                        FileUpload1.PostedFile.SaveAs(path
                            + FileUpload1.FileName);
                        FilePath = FileUpload1.FileName;
                    }
                    catch (Exception ex)
                    {
                        Logger.logmessage(classobject, "uploadfile", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                    }
                }
            }
            return FilePath;
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


                //com = "Select Name,ID from Su_MucLuc order by ID";
                //adpt = new SqlDataAdapter(com, conn);
                //dt = new DataTable();
                //adpt.Fill(dt);
                //ddlMucLuc.DataSource = dt;
                //ddlMucLuc.DataBind();
                //ddlMucLuc.DataTextField = "Name";
                //ddlMucLuc.DataValueField = "ID";
                //ddlMucLuc.DataBind();

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
                Logger.logmessage(classobject, "bindingDDLs", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btAddApprover_Click(object sender, EventArgs e)
        {

            try
            {
                bool updateVB=false;
                bool executeSuccess = false;
                if (!isUpdate())
                {
                    sec = new VanBanTrongHoSo();
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = txtMucLuc.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.KyHieuVanBan = txtKyHieu.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ButTich = txtButtich.Text;
                    sec.SoLuongTo = txtSoluongto.Text;
                    sec.ThoiGian = Request.Form[txtThoiGian.UniqueID];
                    sec.KiHieuThongTin = txtKyhieuThongtin.Text;
                    sec.ToSo = txtToSo.Text;
                    sec.TrichYeu = txtTrichYeu.Text;
                    sec.TacGia = txtTacgia.Text;
                    sec.LoaiVanBan = ddlLoaivanban.SelectedValue;
                    sec.MucDoTinCay = ddlMucDoTinCay.SelectedValue;
                    sec.DoMat = ddlDomat.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                    sec.Hoso_ID = Request.QueryString["hid"].ToString();
                    sec.ThoiHanBaoQuan = ddlThoiHan.SelectedValue;
                    //sec.FilePath = uploadfile();
                }
                else
                {
                    updateVB = true;
                    sec = um.getVanBanTrongHoSo(Int32.Parse(Session[VanBanTrongHoSoLogic.SESSION_SEC_ID].ToString()));
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = txtMucLuc.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.KyHieuVanBan = txtKyHieu.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ButTich = txtButtich.Text;
                    sec.SoLuongTo = txtSoluongto.Text;
                    sec.ThoiGian = Request.Form[txtThoiGian.UniqueID];
                    sec.KiHieuThongTin = txtKyhieuThongtin.Text;
                    sec.ToSo = txtToSo.Text;
                    sec.TrichYeu = txtTrichYeu.Text;
                    sec.TacGia = txtTacgia.Text;
                    sec.LoaiVanBan = ddlLoaivanban.SelectedValue;
                    sec.MucDoTinCay = ddlMucDoTinCay.SelectedValue;
                    sec.DoMat = ddlDomat.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                    sec.ThoiHanBaoQuan = ddlThoiHan.SelectedValue;
                    //sec.ID = 

                }
                if (!isUpdate())
                {
                    string validateMessage = "";
                    if (um.validateVanBan(sec,out validateMessage))
                    {
                        int newID = um.addVanBanTrongHoSo(sec);
                        sec.ID = newID;
                        executeSuccess = true;
                        lstInfo.Add("Thêm mới văn bản thành công.");
                        Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE] = sec;

                    }
                    else
                    {
                        Logger.logmessage(classobject, "btAddApprover_Click", "Validate fail 4 insert VB " + sec.KyHieuVanBan);
                        lstError.Add(validateMessage);
                        executeSuccess = false;
                        Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE] = sec;
                    }
                }
                else
                {
                    string validateMessage = "";
                    if (um.validateVanBan(sec,out validateMessage))
                    {
                        sec.ID = Int32.Parse(SecID);
                        um.updateVanBanTrongHoSo(sec);
                        //Xóa đính kèm
                        foreach (GridViewRow r in dgvAttachment.Rows)
                        {
                            if (r.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkDel = (CheckBox)r.FindControl("chkDeleteAttachment");
                                if (chkDel != null && chkDel.Checked)
                                {
                                    int k = 0;
                                    string sK = dgvAttachment.DataKeys[r.RowIndex].Value.ToString();
                                    Int32.TryParse(sK, out k);
                                    attachLogic.deleteAttachmentFromVanBan(sec.ID, k);
                                }
                            }
                        }
                        lstInfo.Add("Cập nhật văn bản thành công.");
                        executeSuccess = true;
                        Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE] = sec;

                    }
                    else
                    {
                        executeSuccess = false;
                        Logger.logmessage(classobject, "btAddApprover_Click", "Validate fail 4 update VB " + sec.KyHieuVanBan);
                        lstError.Add("Văn bản chưa được cập nhật, bạn hãy kiểm tra lại các thông tin đầu vào.");
                    }
                    //Response.Redirect("ThongKeTaiLieu.aspx", false);
                }
                Session[Constants.SESSION_INFO] = lstInfo;
                Session[Constants.SESSION_ERROR] = lstError;
                if (FileUpload1.HasFile)
                {
                    attachLogic.createAttachment(Server.MapPath("~/files"), AttachmentLogic.ATTACHMENT_TYPE_VB, FileUpload1.PostedFile.FileName, FileUpload1, sec.ID);
                    Logger.logmessage(classobject, "btAddApprover_Click", "Uploaded file " + FileUpload1.PostedFile.FileName);
                }
                Button btn = sender as Button;
                string cmd = "";
                if (btn != null)
                {
                    cmd = btn.CommandArgument;
                }
                if (cmd.ToUpper().Equals("CONTINUE"))
                {
                    Response.Redirect("QLFileDinhKem.aspx?ObjectID=" + sec.ID.ToString() + "&ObjectType=" + AttachmentLogic.ATTACHMENT_TYPE_VB + "&RootId=" + hidHoSo.Value, false);
                }
                else
                {
                    if (executeSuccess)
                    {
                        Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = null;
                        Session[VanBanTrongHoSoLogic.SESSION_VANBAN_CACHE] = sec;
                    }
                    Response.Redirect("ThemVBTrongHoSo.aspx?hid=" + hidHoSo.Value, false);
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAddApprover_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
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
        protected void dgvAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


    }
}