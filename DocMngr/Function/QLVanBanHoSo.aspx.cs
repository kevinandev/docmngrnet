using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.Dao;
using log4net;
using Logic;
using FunctionGroup.Logic;
using FunctionGroup.utils;

namespace FunctionGroup.Function
{
    public partial class QLVanBanHoSo : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        List<Su_PhongLuuTru> lstPhong = new List<Su_PhongLuuTru>();
        List<Su_HoSo> lstHoSo = new List<Su_HoSo>();
        List<Su_DoMat> lstDoMat = new List<Su_DoMat>();
        List<Su_CoQuanLuuTru> lstCoQuan = new List<Su_CoQuanLuuTru>();
        List<Su_LoaiVanban> lstLoaiVanBan = new List<Su_LoaiVanban>();
        VanBanHoSoLogic logic = null;
        AttachmentLogic attachLogic = null;
        bool bindPagingRow = false;
        int totalRow = 0;
        int pageCount = 1;
        int pageIndex = 1;
        string currentHS = "";
        int stt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtHop.Attributes.Add("OnKeyPress", "if (event.keyCode == 13) {" + ClientScript.GetPostBackEventReference(txtHop, String.Empty) + "}");
                logic = new VanBanHoSoLogic();
                attachLogic = new AttachmentLogic();
                lstCoQuan = logic.getAllCoQuan();
                lstDoMat = logic.getAllDoMat();
                lstLoaiVanBan = logic.getAllLoaiVanBan();
                dgvApprover.PageSize = Constants.GRIDVIEW_PAGE_SIZE;
                if (!IsPostBack)
                {
                    string hoSoId = Request["HoSoID"];
                    string phongId = Request["PhongID"];
                    string hop = Request["Hop"];
                    hidPageIndex.Value = "1";

                    hidHoSoID.Value = hoSoId;
                    if (hoSoId != null && hoSoId.Trim().Length > 0)
                    {
                        Su_HoSo hoSoObj = logic.findHoSoById(hoSoId);
                        if (hoSoObj != null)
                        {
                            string maP = hoSoObj.MaPhong;
                            Su_PhongLuuTru phongObj = logic.findPhongByMaPhong(maP);
                            if (phongObj != null)
                            {
                                phongId = phongObj.MaPhong;
                            }
                            hop = hoSoObj.HopSo;
                        }
                    }
                    resetField(phongId, hop, hoSoId);
                    currentHS = hoSoId;
                }
                else
                {
                    Control postBackCtrl = GetPostBackControl(Page);
                    if (postBackCtrl!=null && "txtHop".Equals(postBackCtrl.ID))
                    {
                        string phongId = ddlPhong.SelectedValue;
                        string hop = txtHop.Text;
                        string hoSoId = "";
                        resetField(phongId, hop, hoSoId);
                    }
                }
                Int32.TryParse(hidPageIndex.Value, out pageIndex);
                if (currentHS != null && currentHS.Trim().Length > 0)
                {
                    loadDataToGridView(currentHS);
                }


            }
            catch (Exception ex)
            {
                logger.Error("PageLoad error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        private void resetField(string phong, string hop, string hoSo)
        {
            lstPhong = logic.getAllPhong();
            ddlPhong.Items.Clear();
            ddlPhong.DataSource = lstPhong;
            ddlPhong.DataTextField = "TenPhong";
            ddlPhong.DataValueField = "MaPhong";
            ddlPhong.DataBind();
            if (phong != null && phong.Trim().Length > 0)
            {
                ddlPhong.SelectedValue = phong;
            }
            else
            {
                ddlPhong.SelectedValue = "0";
            }
            logger.Info("QLVanBanHoSo search HS by phong: "+phong+"; and Hop: "+hop);
            lstHoSo = logic.findHoSoByPhongHop(phong, hop);
            logger.Info("QLVanBanHoSo search HS result: " + lstHoSo.Count.ToString());

            ddlHoSo.Items.Clear();
            ddlHoSo.DataSource = lstHoSo;
            ddlHoSo.DataTextField = "HoSoSo";
            ddlHoSo.DataValueField = "ID";
            ddlHoSo.DataBind();

            if (hoSo != null && hoSo.Trim().Length > 0)
            {
                logger.Info("QLVanBanHoSo hoSo: "+ hoSo);
                ddlHoSo.SelectedValue = hoSo;
                Su_HoSo tempHs = logic.findHoSoById(hoSo);
                if (tempHs != null)
                {
                    txtHop.Text = tempHs.HopSo;
                }
            }
            else
            {
                ddlHoSo.SelectedValue = "0";
                txtHop.Text = hop;
                dgvApprover.DataSource = null;
                dgvApprover.DataBind();
            }


        }
        private Control GetPostBackControl(Page page)
        {
            Control control = null;

            string ctrlname = page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname != null && ctrlname != string.Empty)
            {
                control = page.FindControl(ctrlname);
            }
            return control;
        }
        private void loadDataToGridView(string soHS)
        {
            List<Su_VanbanTrongHoSo> lstVB = new List<Su_VanbanTrongHoSo>();
            lstVB = logic.getVanBanOfHoSo(soHS, pageIndex, out totalRow);
            pageCount = totalRow / Constants.GRIDVIEW_PAGE_SIZE;
            int tPageCount = totalRow % Constants.GRIDVIEW_PAGE_SIZE;
            if (tPageCount > 0)
            {
                pageCount++;
            }
            dgvApprover.DataSource = lstVB;
            dgvApprover.DataBind();
            if (dgvApprover.BottomPagerRow != null)
            {
                dgvApprover.BottomPagerRow.Visible = true;
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
        protected void changing(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = sender as ImageButton;
                Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = btn.CommandArgument.Trim();
                Response.Redirect("ThemVBTrongHoSo.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("PageLoad error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void onDgvDatabind(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Su_VanbanTrongHoSo item = (Su_VanbanTrongHoSo)e.Row.DataItem;
                    if (item != null)
                    {
                        List<Su_Attachment> lstAttachment = attachLogic.getAttachment(AttachmentLogic.ATTACHMENT_TYPE_VB, item.ID);
                        Repeater rpt = (Repeater)e.Row.Cells[6].FindControl("rptAttachment");
                        if (rpt != null)
                        {
                            rpt.DataSource = lstAttachment;
                            rpt.DataBind();
                        }
                        Label lblCoQuan = null, lblLoaiVanBan = null, lblDoMat = null, lblSTT = null;
                        lblCoQuan = (Label)e.Row.FindControl("lblCoQuan");
                        lblLoaiVanBan = (Label)e.Row.FindControl("lblLoaiVanBan");
                        lblDoMat = (Label)e.Row.FindControl("lblDoMat");
                        lblSTT = (Label)e.Row.FindControl("lblSTT");
                        if (lblSTT != null)
                        {
                            stt++;
                            lblSTT.Text = stt.ToString();
                        }
                        if (lblCoQuan != null)
                        {
                            lblCoQuan.Text = item.TacGia;
                            //foreach (Su_CoQuanLuuTru cq in lstCoQuan)
                            //{
                            //    if (cq.Code.Equals(item.CoQuan))
                            //    {
                            //        lblCoQuan.Text = cq.Name;
                            //        break;
                            //    }
                            //}
                        }
                        if (lblLoaiVanBan != null)
                        {
                            foreach (Su_LoaiVanban cq in lstLoaiVanBan)
                            {
                                if (cq.ID.ToString().Equals(item.LoaiVanBan))
                                {
                                    lblLoaiVanBan.Text = cq.Name;
                                    break;
                                }
                            }
                        }
                        if (lblDoMat != null)
                        {
                            foreach (Su_DoMat cq in lstDoMat)
                            {
                                if (cq.ID.ToString().Equals(item.DoMat))
                                {
                                    lblDoMat.Text = cq.Name;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Pager)
                {
                    e.Row.Visible = true;
                    DropDownList pageList = (DropDownList)e.Row.FindControl("PageDropDownList");
                    Label pageLabel = (Label)e.Row.FindControl("CurrentPageLabel");

                    if (pageList != null)
                    {

                        // Create the values for the DropDownList control based on 
                        // the  total number of pages required to display the data
                        // source.
                        for (int i = 0; i < pageCount; i++)
                        {

                            // Create a ListItem object to represent a page.
                            int pageNumber = i + 1;
                            ListItem li = new ListItem(pageNumber.ToString());

                            // If the ListItem object matches the currently selected
                            // page, flag the ListItem object as being selected. Because
                            // the DropDownList control is recreated each time the pager
                            // row gets created, this will persist the selected item in
                            // the DropDownList control.   
                            if (i == pageIndex - 1)
                            {
                                li.Selected = true;
                            }

                            // Add the ListItem object to the Items collection of the 
                            // DropDownList.
                            pageList.Items.Add(li);

                        }

                    }

                    if (pageLabel != null)
                    {

                        // Calculate the current page number.
                        int currentPage = pageIndex;

                        // Update the Label control with the current page information.
                        pageLabel.Text = "Page " + currentPage.ToString() +
                          " of " + pageCount;

                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Dgv data bind error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);

            }

        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            string selectedHoSoID = ddlHoSo.SelectedValue;
            Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = null;
            Response.Redirect("ThemVBTrongHoSo.aspx?hid=" + selectedHoSoID, false);
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvApprover.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("cbChoose");
                        if (chk.Checked)
                        {
                            string sID = dgvApprover.DataKeys[row.RowIndex].Value.ToString();
                            int iId = 0;
                            Int32.TryParse(sID, out iId);
                            logic.deleteVB(iId);
                        }
                    }
                }
                //Session[LoaiVanBanLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QLVanBanHoSo.aspx?HoSoID=" + hidHoSoID.Value, false);
            }
            catch (Exception ex)
            {
                logger.Error("Delete VanBan error: ", ex);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }

        }

        protected void ddlPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.Info("ddlPhong_SelectedIndexChanged");
        }

        protected void txtHop_TextChanged(object sender, EventArgs e)
        {
            logger.Info("txtHop_TextChanged");

        }

        protected void ddlHoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.Info("ddlHoSo_SelectedIndexChanged");
            currentHS = ddlHoSo.SelectedValue;

            if (currentHS != null && currentHS.Trim().Length > 0)
            {
                pageIndex = 1;
                loadDataToGridView(currentHS);
            }

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string phongId = ddlPhong.SelectedValue;
            string hop = txtHop.Text;
            string hoSoId = "";
            resetField(phongId, hop, hoSoId);

        }
        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = dgvApprover.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            dgvApprover.PageIndex = pageList.SelectedIndex;
            pageIndex = pageList.SelectedIndex;
            //listAllSec(ddlPhongLuuTru.SelectedValue, pageList.SelectedIndex + 1);
            loadDataToGridView(ddlHoSo.SelectedValue);
        }

    }
}