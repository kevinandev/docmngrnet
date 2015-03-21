using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.Logic;
using log4net;
using Logic;
using FunctionGroup.Dao;
using System.Data.SqlClient;
using System.Data;
using Function;

namespace FunctionGroup.Function
{
    public partial class QLFileDinhKem : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        AttachmentLogic logic = new AttachmentLogic();
        DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        Su_CoQuanLuuTruLogic coquanLogic = new Su_CoQuanLuuTruLogic();
        Su_HoSo curHoSo = null;
        Su_VanbanTrongHoSo curVanBan = null;
        string classobject = "QLFileDinhKem.aspx.cs";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request["ObjectID"];
                string type = Request["ObjectType"];
                string idRoot = Request["RootId"];
                
                int iId = 0;
                Int32.TryParse(id, out iId);
                hidId.Value = id;
                hidPrefix.Value = type;
                if (idRoot != null)
                {
                    hidRootId.Value = idRoot;
                }
                //load dropbox
                bindingDDLs();
                if (AttachmentLogic.ATTACHMENT_TYPE_HS.Equals(type))
                {
                    try
                    {
                        curHoSo = dataContext.Su_HoSos.Where(p => p.ID == iId).First();
                        row1VB.Visible = false;
                        row2VB.Visible = false;
                        row3VB.Visible = false;
                        vbTaiLieu.Visible = false;
                        ddlCoQuan.SelectedValue = curHoSo.Coquan;
                        ddlPhong.SelectedValue = curHoSo.MaPhong;
                        ddlMucLuc.SelectedValue = curHoSo.MucLucSo;
                        txtHoSoSo.Text = curHoSo.HoSoSo;
                        //txtKyHieu.Text = curHoSo.KyHieu;
                        //txtSoluongto.Text = curHoSo.SoLuong;
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Page_Load error: ", ex);
                    }

                }
                else if (AttachmentLogic.ATTACHMENT_TYPE_VB.Equals(type))
                {
                    try
                    {
                        curVanBan = dataContext.Su_VanbanTrongHoSos.Where(p => p.ID == iId).First();
                        int hsId = 0;
                        Int32.TryParse(curVanBan.Hoso_ID, out hsId);
                        curHoSo = dataContext.Su_HoSos.Where(p => p.ID == hsId).First();
                        ddlCoQuan.SelectedValue = curHoSo.Coquan;
                        ddlPhong.SelectedValue = curHoSo.MaPhong;
                        ddlMucLuc.SelectedValue = curHoSo.MucLucSo;
                        txtHoSoSo.Text = curHoSo.HoSoSo;
                        txtKyHieu.Text = curVanBan.KyHieuVanBan;
                        txtSoluongto.Text = curVanBan.SoLuongTo;
                        vbHoSo.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        logger.Error("Page_Load error: ", ex);
                    }

                }
                else
                {
                    throw new Exception("Request data error");
                }
                List<Su_Attachment> lstAttach = logic.getAttachment(type, iId);
                rptDocs.DataSource = lstAttach;
                rptDocs.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("Page load error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
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
                    Su_Attachment at = logic.getAttachmentByID(iAttachmentID);
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
                logger.Error("Binding dropdown book error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string fileStored = Server.MapPath("~/files");
                int id = 0;
                Int32.TryParse(hidId.Value, out id);
                logic.createAttachment(fileStored, hidPrefix.Value, fulAttach.PostedFile.FileName, fulAttach, id);
                Button btn= sender as Button;
                string cmd = "";
                if (btn != null)
                {
                    cmd = btn.CommandArgument.ToUpper();
                }
                if (cmd.Equals("CONTINUE"))
                {
                    Response.Redirect("QLFileDinhKem.aspx?ObjectID=" + hidId.Value + "&ObjectType=" + hidPrefix.Value+"&RootId = "+hidRootId.Value, false);
                }
                else
                {
                    if (AttachmentLogic.ATTACHMENT_TYPE_HS.Equals(hidPrefix.Value))
                    {
                        Session[HoSoLogic.SESSION_SEC_ID] = null;
                        Response.Redirect("ThemMoiHoSo.aspx", false);
                    }
                    else if (AttachmentLogic.ATTACHMENT_TYPE_VB.Equals(hidPrefix.Value))
                    {
                        Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = null;
                        Response.Redirect("ThemVBTrongHoSo.aspx?hid="+hidRootId.Value, false);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("btnUpload_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (AttachmentLogic.ATTACHMENT_TYPE_HS.Equals(hidPrefix.Value))
                {
                    Session[HoSoLogic.SESSION_SEC_ID] = hidId.Value;
                    Response.Redirect("ThemMoiHoSo.aspx", false);
                }
                else if (AttachmentLogic.ATTACHMENT_TYPE_VB.Equals(hidPrefix.Value))
                {
                    //Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = hidId.Value;
                    Response.Redirect("ChiTietVBTrongHoSo.aspx", false);
                }
            }
            catch (Exception ex)
            {
                logger.Error("btnCancel_Click error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);

            }

        }
    }
}