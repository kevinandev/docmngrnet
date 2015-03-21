using System;
using System.Data;
using Logic;
using Function;

namespace DocMngr
{
    public partial class Home : System.Web.UI.Page
    {
        public DataTable dtVanBan = new DataTable();// select top 10 
        VanBanTrongHoSoLogic logic = new VanBanTrongHoSoLogic();
        string classobject = "Home.aspx.cs";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dtVanBan = logic.getAllSecByTop("10");
             }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "Page_Load", ex.Message + ex.StackTrace);  Response.Redirect("~/ThongBaoLoi.aspx", false);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}