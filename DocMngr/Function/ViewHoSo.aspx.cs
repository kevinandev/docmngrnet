using System;
using System.Data.SqlClient;
using System.Data;
using Logic;

namespace Function
{
    public partial class ViewHoSo : System.Web.UI.Page
    {
        HoSo sec;
        HoSoLogic um = new HoSoLogic();
        Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
        string SecID = "";
        string classobject = "ViewHoSo.aspx.cs";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    bindingDDLs();
                    if (Session[HoSoLogic.SESSION_SEC_ID] != null)
                    {
                        btAddApprover.Text = "Cập nhật";
                        ldhead.InnerText = "CHI TIẾT HỒ SƠ";

                        sec = um.getHoSo(Int32.Parse(Session[HoSoLogic.SESSION_SEC_ID].ToString()));
                        if (sec != null)
                        {

                            ddlCoQuan.SelectedValue = sec.Coquan;
                            ddlPhong.SelectedValue = sec.MaPhong;
                            ddlMucLuc.SelectedValue = sec.MucLucSo;
                            ddlHopSo.SelectedValue = sec.HopSo;
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

                        }
                    }
                    else
                    {
                        ldhead.InnerText = "THÊM MỚI HỒ SƠ";
                        sec = new HoSo();
                    }
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
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


                com = "Select Name,ID from Su_HopHoSo order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlHopSo.DataSource = dt;
                ddlHopSo.DataBind();
                ddlHopSo.DataTextField = "Name";
                ddlHopSo.DataValueField = "ID";
                ddlHopSo.DataBind();

                com = "Select Name,ID from Su_MucLuc order by ID";
                adpt = new SqlDataAdapter(com, conn);
                dt = new DataTable();
                adpt.Fill(dt);
                ddlMucLuc.DataSource = dt;
                ddlMucLuc.DataBind();
                ddlMucLuc.DataTextField = "Name";
                ddlMucLuc.DataValueField = "ID";
                ddlMucLuc.DataBind();

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
                    sec.MucLucSo = ddlMucLuc.SelectedValue;
                    sec.HopSo = ddlHopSo.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;

                    sec.KyHieu = txtKyHieu.Text;
                    sec.TieuDe = txtTieude.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ThoiGianBatDau = txtThoiGianBatDau.Text;
                    sec.ThoiGianKetThuc = txtThoiGiankT.Text;
                    sec.ButTich = txtButtich.Text;
                    sec.SoLuong = txtSoluongto.Text;
                    sec.ThoiHanBaoQuan = ddlThoiHan.SelectedValue;
                    sec.CheDoSuDung = ddlChedoSD.SelectedValue;
                    sec.TinhTrangVatLy = ddlTinhTrang.SelectedValue;
                }
                else
                {
                    sec.Coquan = ddlCoQuan.SelectedValue;
                    sec.MaPhong = ddlPhong.SelectedValue;
                    sec.MucLucSo = ddlMucLuc.SelectedValue;
                    sec.HopSo = ddlHopSo.SelectedValue;
                    sec.HoSoSo = txtHoSoSo.Text;
                    sec.NgonNgu = ddlNgonNgu.SelectedValue;

                    sec.KyHieu = txtKyHieu.Text;
                    sec.TieuDe = txtTieude.Text;
                    sec.GhiChu = txtGhiChu.Text;
                    sec.ThoiGianBatDau = txtThoiGianBatDau.Text;
                    sec.ThoiGianKetThuc = txtThoiGiankT.Text;
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
                        um.addHoSo(sec);
                    }
                    // listAllSec();
                }
                else
                {
                    if (validateObject(sec))
                    {
                        sec.ID = Int32.Parse(SecID);
                        um.updateHoSo(sec);
                    }
                }
                Response.Redirect("QLHoSo.aspx", false);
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

       
    }
}