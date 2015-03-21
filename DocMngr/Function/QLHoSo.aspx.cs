using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Logic;
using FunctionGroup.utils;

namespace Function
{
    public partial class QLHoSo : System.Web.UI.Page
    {
        HoSo sec;
        HoSoLogic um = new HoSoLogic();
        string SecID = "";
        string classobject = "QLHoSo.aspx.cs";
        bool bindPagingRow = false;
        int totalRow = 0;
        int pageCount = 1;
        int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            dgvApprover.PageSize = Constants.GRIDVIEW_PAGE_SIZE;
            if (!IsPostBack)
            {
                hidPageIndex.Value = "1";
                try
                {

                    bindingDDLPhong();
                    //listAllSec(ddlPhongLuuTru.SelectedValue, 1);
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
                }
            }
            Int32.TryParse(hidPageIndex.Value, out pageIndex);
        }

        public void bindingDDLPhong()
        {
            try
            {
                PhongLuuTruLogic phongLogic = new PhongLuuTruLogic();
                DataTable dt = phongLogic.getAllSec();

                ListItem root = new ListItem("--Chọn phông --", "");
                string Code = "";
                string Name = "";

                // them đòng dầu tiên 
                ddlPhongLuuTru.Items.Add(root);
                foreach (DataRow r in dt.Rows)
                {
                    Code = r["MaPhong"].ToString();
                    Name = r["TenPhong"].ToString();
                    ListItem item = new ListItem(Name, Code);
                    ddlPhongLuuTru.Items.Add(item);
                }
                ddlPhongLuuTru.DataBind();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bindingDDLPhong", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void listAllSec(string maphong, int page)
        {
            try
            {

                DataTable dt = um.search(maphong, page);
                //dgvApprover.PageCount = totalRow;
                dgvApprover.DataSource = dt;

                dgvApprover.DataBind();
                dgvApprover.Columns[0].Visible = false;
                if (dgvApprover.BottomPagerRow != null)
                {
                    dgvApprover.BottomPagerRow.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "listAllSec", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void listAllSec()
        {

            try
            {
                DataTable dt = um.getAllSec();
                dgvApprover.DataSource = dt;
                dgvApprover.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "listAllSec", ex.Message + ex.StackTrace); Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void goToDetail(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = sender as LinkButton;
                Response.Redirect("QLVanBanHoSo.aspx?HoSoID=" + lbtn.CommandArgument.Trim(), false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "goToDetail", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void changing(object sender, EventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                Session[HoSoLogic.SESSION_SEC_ID] = lbtn.CommandArgument.Trim();
                Response.Redirect("ThemMoiHoSo.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "changing", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }
        protected void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in dgvApprover.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("cbChoose");
                    if (chk.Checked)
                    {
                        int ID = Int32.Parse(row.Cells[0].Text.ToString().Trim());
                        um.deleteHoSo(ID);
                    }
                }
                Session[HoSoLogic.SESSION_SEC_ID] = null;
                Response.Redirect("QLHoSo.aspx", false);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            try
            {
                listAllSec(ddlPhongLuuTru.SelectedValue, 1);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "btSearch_Click", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            Session[HoSoLogic.SESSION_SEC_ID] = null;
            string selectedPhong = ddlPhongLuuTru.SelectedValue;
            Response.Redirect("ThemMoiHoSo.aspx?phongCode=" + selectedPhong, false);
        }

        protected void bt_addVanBan_Add(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = sender as ImageButton;
                Session[VanBanTrongHoSoLogic.SESSION_HOSO_ID] = lbtn.CommandArgument.Trim();
                Session[VanBanTrongHoSoLogic.SESSION_SEC_ID] = null;
                string url = "ThemVBTrongHoSo.aspx?hid=" + lbtn.CommandArgument.Trim();
                Response.Redirect(url, false);
                //ClientScript.RegisterStartupScript(this.Page.GetType(), "",
                //        "window.open('" + url + "','Graph','height=700,width=900');", true);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bt_addVanBan_Add", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void dgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                listAllSec(ddlPhongLuuTru.SelectedValue, dgvApprover.PageIndex + 1);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "bt_addVanBan_Add", ex.Message + ex.StackTrace);
                Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void dgvApprover_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView item = (DataRowView)e.Row.DataItem;
                if (item != null && !bindPagingRow)
                {
                    totalRow = (int)item["row_count"];
                    pageCount = totalRow / Constants.GRIDVIEW_PAGE_SIZE;
                    int tPageCount = totalRow % Constants.GRIDVIEW_PAGE_SIZE;
                    if (tPageCount > 0)
                    {
                        pageCount++;
                    }
                    lblRecordCount.Text = "Tổng số hồ sơ: " + totalRow.ToString();
                    bindPagingRow = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Pager)
            {

                // Retrieve the DropDownList and Label controls from the row.
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
        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = dgvApprover.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            dgvApprover.PageIndex = pageList.SelectedIndex;
            pageIndex = pageList.SelectedIndex + 1;
            listAllSec(ddlPhongLuuTru.SelectedValue, pageList.SelectedIndex + 1);

        }

        //protected void btDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool flag = false;
        //        foreach (GridViewRow row in dgvApprover.Rows)
        //        {
        //            CheckBox chk = (CheckBox)row.FindControl("cbChoose");
        //            if (chk.Checked)
        //            {
        //                flag = true;
        //                int ID = Int32.Parse(row.Cells[0].Text.ToString().Trim());
        //                um.deleteHoSo(ID);
        //            }
        //        }
        //        if (flag)
        //        {
        //            Response.Redirect("QLHoSo.aspx", false);
        //        }
        //        else { Response.Write("<script language='javascript'> { alert('Hãy chọn phông muốn xóa');}</script>"); }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.logmessage(classobject, "btDelete_Click", ex.Message + ex.StackTrace);
        //        Response.Redirect("~/ThongBaoLoi.aspx", false);
        //    }

        //}



    }
}