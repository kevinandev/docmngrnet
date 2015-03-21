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
    public partial class TimKiemCacVanBan : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
        TimkiemLogic searcher = new TimkiemLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                searchAll();
            }

        }
        private void search()
        {
            string CoQuan = ddlCoQuan.SelectedValue;
            string Phong = ddlPhong.SelectedValue;
            string ThoiHan = ddlThoiHan.SelectedValue;
            string TinhTrangVatLy = ddlTinhTrangVatLy.SelectedValue;
            string KyHieuVanBan = txtKyHieuVanBan.Text;
            string sFrom = txtThoiGianFrom.Text;
            string sTo = txtThoiGianTo.Text;
            string TacGia = txtTacGia.Text;
            string Keyword = txtKeyword.Text;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            dt = searcher.timkiemVanBan(conn, CoQuan, Phong, TinhTrangVatLy,"", KyHieuVanBan, TacGia, sFrom, sTo,Keyword);
            dgrResult.DataSource = dt;
            dgrResult.DataBind();
            dgrResult.Columns[0].Visible = false;


        }
        private void searchAll()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            dt = searcher.timkiemVanBan(conn, "", "", "", "","", "", "", "","");
            dgrResult.DataSource = dt;
            dgrResult.DataBind();
            dgrResult.Columns[0].Visible = false;


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