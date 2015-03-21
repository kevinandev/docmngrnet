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
using Logic;

namespace Function
{
    public partial class TimKiemCacHoSo : System.Web.UI.Page
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            searchAll();
        }
        private void search()
        {
            string CoQuan = ddlCoQuan.SelectedValue;
            string Phong = ddlPhong.SelectedValue;
            string ThoiHan = ddlThoiHan.SelectedValue;
            string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
            string Mucluc = ddlMucluc.SelectedValue;
            string CheDoSuDung = ddlCheDoSuDung.SelectedValue;
            string Keyword = txtKeyword.Text;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            dt = searcher.timkiemHoSo(conn, CoQuan, Phong, ThoiHan, TinhTrangVatLy, Mucluc, CheDoSuDung, Keyword);
            dgrResult.DataSource = dt;
            dgrResult.DataBind();
            dgrResult.Columns[0].Visible = false;
        }
        private void searchAll()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            dt = searcher.timkiemHoSo(conn, "", "", "", "", "", "", "");
            dgrResult.DataSource = dt;
            dgrResult.DataBind();
            dgrResult.Columns[0].Visible = false;
        }
        protected void btTimKiem_Click(object sender, EventArgs e)
        {
            search();
        }

        public void bindingDDL(DataTable dt, string head, DropDownList ddl)
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
        protected void changing(object sender, System.EventArgs e)
        {
            //downling group of document but pdf file only, another file will be passed
            try
            {
                ImageButton ibtn1 = sender as ImageButton;
                foreach (GridViewRow r in dgrResult.Rows)
                {
                    if (Server.HtmlDecode(r.Cells[0].Text.Trim()) == ibtn1.CommandArgument.Trim())
                    {
                        //Session id =   ibtn1.CommandArgument.Trim() 
                        //Response.Redirect("wl_change_kickoff.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}