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
    public partial class ThemMoiPhongLuuTru : System.Web.UI.Page
    {
        static PhongLuuTru sec;
        PhongLuuTruLogic um = new PhongLuuTruLogic();
        string SecID = "";
        Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
        string classobject = "ThemMoiPhongLuuTru.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    bindingDDLCoQuan();
                    bindingNgonNgu();
                    if (Session[PhongLuuTruLogic.SESSION_SEC_ID] != null)
                    {
                        btAddApprover.Text = "Cập nhật";
                        ldhead.InnerText = "CHỈNH SỬA PHÔNG LƯU TRỮ";
                        sec = um.getPhongLuuTru(Int32.Parse(Session[PhongLuuTruLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {

                            ddlCoQuan.SelectedValue = sec.Coquan;
                            txtMaPhong.Text = sec.MaPhong;
                            txtTenPhong.Text = sec.TenPhong;
                            txtLichsu.Text = sec.LichSu;
                            txtThoiHanTaiLieu.Text = sec.ThoigianTaiLieu;
                            txtTongSoTaiLieu.Text = sec.TongSoTaiLieu;
                            txtTaiLieuChinhLy.Text = sec.TaiLieuChinhLy;
                            txtTaiLieuChuaChinhLy.Text = sec.TaiLieuChuaChinhLy;
                            txtNhomTaiLieu.Text = sec.CacNhomTaiLieu;
                            txtTaiLieuKhac.Text = sec.NhomTaiLieuKhac;

                            txtThoigian.Text = sec.ThoiGianNhap;
                            ddlNgonNgu.SelectedValue = sec.NgonNgu;
                            txtCongCu.Text = sec.CongCu;
                            txtBaoHiem.Text = sec.BanSaoBaoHiem;
                            txtGhiChu.Text = sec.GhiChu;
                        }
                    }
                    else
                    {
                        ldhead.InnerText = "THÊM MỚI PHÔNG LƯU TRỮ";
                        sec = new PhongLuuTru();
                    }
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("../ThongBaoLoi.aspx", false);
                }

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
        public void bindingNgonNgu() {
            try
            {
                Su_NgonNguLogic ngonnguLogic = new Su_NgonNguLogic();
                DataTable listNgongu = ngonnguLogic.getAllSec();
                bindingDDL(listNgongu, "Chọn ngôn ngữ", ddlNgonNgu);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingNgonNgu", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        public void bindingDDLCoQuan()
        {
            try
            {
                ListItem root = new ListItem("--Chọn cơ quan lưu trữ --", "");
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
        protected void btAddApprover_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isUpdate())
                {
                    sec = new PhongLuuTru();
                    sec.Coquan = ddlCoQuan.Items[ddlCoQuan.SelectedIndex].Value.ToString();
                    sec.MaPhong = txtMaPhong.Text;
                    sec.TenPhong = txtTenPhong.Text;
                    sec.LichSu = txtLichsu.Text;
                    sec.ThoigianTaiLieu = txtThoiHanTaiLieu.Text;
                    sec.TongSoTaiLieu = txtTongSoTaiLieu.Text;
                    sec.TaiLieuChinhLy = txtTaiLieuChinhLy.Text;
                    sec.TaiLieuChuaChinhLy = txtTaiLieuChuaChinhLy.Text;
                    sec.CacNhomTaiLieu = txtNhomTaiLieu.Text;
                    sec.NhomTaiLieuKhac = txtTaiLieuKhac.Text;
                    sec.ThoiGianNhap = Request.Form[txtThoigian.UniqueID];
                    sec.NgonNgu = ddlNgonNgu.Items[ddlNgonNgu.SelectedIndex].Value.ToString();
                    sec.CongCu = txtCongCu.Text;
                    sec.BanSaoBaoHiem = txtBaoHiem.Text;
                    sec.GhiChu = txtGhiChu.Text;
                }
                else
                {
                    sec.Coquan = ddlCoQuan.Items[ddlCoQuan.SelectedIndex].Value.ToString();
                    sec.MaPhong = txtMaPhong.Text;
                    sec.TenPhong = txtTenPhong.Text;
                    sec.LichSu = txtLichsu.Text;
                    sec.ThoigianTaiLieu = txtThoiHanTaiLieu.Text;
                    sec.TongSoTaiLieu = txtTongSoTaiLieu.Text;
                    sec.TaiLieuChinhLy = txtTaiLieuChinhLy.Text;
                    sec.TaiLieuChuaChinhLy = txtTaiLieuChuaChinhLy.Text;
                    sec.CacNhomTaiLieu = txtNhomTaiLieu.Text;
                    sec.NhomTaiLieuKhac = txtTaiLieuKhac.Text;
                    sec.ThoiGianNhap = Request.Form[txtThoigian.UniqueID];
                    sec.NgonNgu = ddlNgonNgu.Items[ddlNgonNgu.SelectedIndex].Value.ToString();
                    sec.CongCu = txtCongCu.Text;
                    sec.BanSaoBaoHiem = txtBaoHiem.Text;
                    sec.GhiChu = txtGhiChu.Text;
                }
                if (!isUpdate())
                {
                    if (validateObject(sec))
                    {
                        um.addPhongLuuTru(sec);
                    }
                    // listAllSec();
                }
                else
                {
                    if (validateObjectForUpdate(sec))
                    {
                        sec.ID = Int32.Parse(SecID);
                        um.updatePhongLuuTru(sec);
                    }
                }

                Response.Redirect("QLPhongLuuTru.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btAddApprover_Click", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }   

        protected bool validateObject(PhongLuuTru sec){
            bool result = true;
            if (!um.validateFieldExistedData("MaPhong", sec.MaPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Mã phông này đã có trong hệ thống. Xin chọn một tên khác');}</script>");
            }

            if (!um.validateFieldExistedData("TenPhong", sec.TenPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tên phông này đã có trong hệ thống. Xin chọn một tên khác');}</script>");
            }
            if (!um.validateSecNameNull(sec.Coquan))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Hãy chọn cơ quan lưu trữ');}</script>");
            }
            if (!um.validateSecNameNull(sec.MaPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Mã phông không được phép để trống');}</script>");
            }

            if (!um.validateSecNameNull(sec.TenPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tên phông không được phép để trống');}</script>");
            }
            if (!um.validateSecNameNull(sec.NgonNgu))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Hãy chọn ngôn ngữ hiển thị');}</script>");
            }
            return result;
            
        }

        protected bool validateObjectForUpdate(PhongLuuTru sec)
        {
            bool result = true;           
            if (!um.validateSecNameNull(sec.Coquan))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Hãy chọn cơ quan lưu trữ');}</script>");
            }
            if (!um.validateSecNameNull(sec.MaPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Mã phông không được phép để trống');}</script>");
            }

            if (!um.validateSecNameNull(sec.TenPhong))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Tên phông không được phép để trống');}</script>");
            }
            if (!um.validateSecNameNull(sec.NgonNgu))
            {
                result = false;
                Response.Write("<script language='javascript'> { alert('Hãy chọn ngôn ngữ hiển thị');}</script>");
            }
            return result;

        }
     

        private bool isUpdate()
        {
            if (Session[PhongLuuTruLogic.SESSION_SEC_ID] != null)
            {
                SecID = Session[PhongLuuTruLogic.SESSION_SEC_ID].ToString();
                return true;
            }
            return false;
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("QLPhongLuuTru.aspx", false);
        }

       
    }
}