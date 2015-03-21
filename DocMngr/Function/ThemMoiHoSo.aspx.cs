using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Logic;
using FunctionGroup.Logic;
using FunctionGroup.Dao;

namespace Function
{
    public partial class ThemMoiHoSo : System.Web.UI.Page
    {
        HoSo sec;
        HoSoLogic um = new HoSoLogic();
        Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
        AttachmentLogic attachLogic = new AttachmentLogic();
        int attachCount = 0;
        string SecID = "";
        string classobject = "ThemMoiHoSo.aspx.cs";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    bindingDDLs();
                    string selectedPhong = Request["phongCode"];
                    if (Session[HoSoLogic.SESSION_SEC_ID] != null)
                    {
                        btAddApprover.Text = "Cập nhật";
                        ldhead.InnerText = "CHỈNH SỬA HỒ SƠ";
                        sec = um.getHoSo(Int32.Parse(Session[HoSoLogic.SESSION_SEC_ID].ToString()));
                        hidIdPhong.Value = sec.ID.ToString();
                        if (sec != null)
                        {

                            ddlCoQuan.SelectedValue = sec.Coquan;
                            ddlPhong.SelectedValue = sec.MaPhong;
                            //ddlMucLuc.SelectedValue = sec.MucLucSo;
                            txtMucLuc.Text = sec.MucLucSo;
                            txtHopSo.Text= sec.HopSo;
                            txtHoSoSo.Text = sec.HoSoSo;
                            ddlNgonNgu.SelectedValue = sec.NgonNgu;
                            txtKyHieu.Text = sec.KyHieu;
                            txtTieude.Text = sec.TieuDe;
                            txtGhiChu.Text = sec.GhiChu;
                            txtThoiGianBatDau.Text = sec.ThoiGianBatDau;
                            txtThoiGiankT.Text = sec.ThoiGianKetThuc;

                            txtButtich.Text = sec.ButTich;
                            txtSoluongto.Text = sec.SoLuong;
                            ddlThoiHan.SelectedValue = sec.ThoiHanBaoQuan;
                            ddlChedoSD.SelectedValue = sec.CheDoSuDung;
                            ddlTinhTrang.SelectedValue = sec.TinhTrangVatLy;
                            //loadDatabase
                            List<Su_Attachment> lstAttach = attachLogic.getAttachment(AttachmentLogic.ATTACHMENT_TYPE_HS, sec.ID);
                            dgvAttachment.DataSource = lstAttach;
                            dgvAttachment.DataBind();


                        }
                    }
                    else
                    {
                        ldhead.InnerText = "THÊM MỚI HỒ SƠ";
                        if (selectedPhong != null && selectedPhong.Trim().Length > 0)
                        {
                            ddlPhong.SelectedValue = selectedPhong;
                        }
                        sec = new HoSo();
                    }
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
            else
            {
                int tId = 0;
                Int32.TryParse(hidIdPhong.Value, out tId);
                sec = um.getHoSo(tId);
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
                            "attachment; filename=" + HttpUtility.UrlEncode(fileNames[fileNames.Length-1],System.Text.Encoding.UTF8));
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
                Logger.logmessage(classobject, "downloadAttach","Download error: "+ex.Message);
            }
        }
        protected void bindingDDLs()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                string com = "Select * from Su_CoQuan order by GroupID";
                SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
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

                com = "Select Name,ID from Su_QuanLyCheDoSuDung order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlChedoSD.DataSource = dt;
                ddlChedoSD.DataBind();
                ddlChedoSD.DataTextField = "Name";
                ddlChedoSD.DataValueField = "ID";
                ddlChedoSD.DataBind();

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

                if (!isUpdate())
                {
                    sec = new HoSo();

                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = txtMucLuc.Text;
                    sec.HopSo = txtHopSo.Text;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;

                    sec.KyHieu = txtKyHieu.Text;
                    sec.TieuDe = txtTieude.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ThoiGianBatDau = Request.Form[txtThoiGianBatDau.UniqueID];
                    sec.ThoiGianKetThuc = Request.Form[txtThoiGiankT.UniqueID];

                    sec.ButTich = txtButtich.Text;
                    sec.SoLuong = txtSoluongto.Text;
                    sec.ThoiHanBaoQuan = ddlThoiHan.SelectedValue;
                    sec.CheDoSuDung = ddlChedoSD.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                }
                else
                {
                    sec = um.getHoSo(Int32.Parse(Session[HoSoLogic.SESSION_SEC_ID].ToString()));
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = txtMucLuc.Text;
                    sec.HopSo =txtHopSo.Text;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;

                    sec.KyHieu = txtKyHieu.Text;
                    sec.TieuDe = txtTieude.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ThoiGianBatDau = Request.Form[txtThoiGianBatDau.UniqueID];
                    sec.ThoiGianKetThuc = Request.Form[txtThoiGiankT.UniqueID];

                    sec.ButTich = txtButtich.Text;
                    sec.SoLuong = txtSoluongto.Text;
                    sec.ThoiHanBaoQuan = ddlThoiHan.SelectedValue;
                    sec.CheDoSuDung = ddlChedoSD.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                }
                if (!isUpdate())
                {
                    if (validateObject(sec))
                    {
                        int newId = um.addHoSo(sec);
                        sec.ID = newId;
                        if (fulAttach.HasFile)
                        {
                            string filePath = attachLogic.createAttachment(Server.MapPath("~/files"), AttachmentLogic.ATTACHMENT_TYPE_HS, fulAttach.PostedFile.FileName, fulAttach, newId);
                            Logger.logmessage(classobject, "btAddApprover_Click", "Attach success " + fulAttach.PostedFile.FileName);
                        }
                    }
                    else
                    {
                        Logger.logmessage(classobject, "btAddApprover_Click", "Ho so validate fail " + sec.HoSoSo);
                    }
                    // listAllSec();
                }
                else
                {
                    if (validateObject(sec))
                    {
                        sec.ID = Int32.Parse(SecID);
                        um.updateHoSo(sec);
                        if (fulAttach.HasFile)
                        {
                            string filePath = attachLogic.createAttachment(Server.MapPath("~/files"), AttachmentLogic.ATTACHMENT_TYPE_HS, fulAttach.PostedFile.FileName, fulAttach, sec.ID);
                            Logger.logmessage(classobject, "btAddApprover_Click", "Attach success " + fulAttach.PostedFile.FileName);
                        }
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
                                    Int32.TryParse(sK,out k);
                                    attachLogic.deleteAttachmentFromHoSo(sec.ID, k);
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.logmessage(classobject, "btAddApprover_Click", "Ho so validate fail " + sec.HoSoSo);
                    }
                }
                if (chkUploadMore.Checked)
                {
                    Response.Redirect("QLFileDinhKem.aspx?ObjectID=" + sec.ID.ToString() + "&ObjectType=" + AttachmentLogic.ATTACHMENT_TYPE_HS, false);
                }
                else
                {
                    Response.Redirect("QLHoSo.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAddApprover_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected bool validateObject(HoSo sec)
        {
            bool result = true;

            if (!um.validateSecNameNull(sec.TieuDe))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tiêu đề không được phép để trống');}</script>");
            }
            return result;
        }

        private bool isUpdate()
        {
            if (Session[HoSoLogic.SESSION_SEC_ID] != null)
            {
                SecID = Session[HoSoLogic.SESSION_SEC_ID].ToString();
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