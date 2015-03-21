using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using FunctionGroup.utils;
using log4net;
using Function;
using FunctionGroup.Dao;

/// <summary>
/// Summary description for UserManagement
/// </summary>
namespace Logic
{
    public class HoSoLogic
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        public const string USER_TYPE_ADMIN = "Admin";
        public const string USER_TYPE_APPROVER = "Approver";
        public const string EMAIL_POSTFIX = "@techcombank.com.vn";
        public const string APPROVER_AREA_NORTH = "N";
        public const string APPROVER_AREA_SOUTH = "S";
        public const string SESSION_SEC_ID = "HO_SO_LO_GIC_SEC_ID";
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        public HoSoLogic() { }

        public HoSo getHoSo(int ID)
        {
            HoSo result = null;
            string query = "SELECT * FROM Su_HoSo WHERE ID = " + ID + "";
            DataTable dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = new HoSo();
                result.ID = ID;
                result.Coquan = dt.Rows[0]["Coquan"].ToString();
                result.MaPhong = dt.Rows[0]["MaPhong"].ToString();
                result.MucLucSo = dt.Rows[0]["MucLucSo"].ToString();
                result.HopSo = dt.Rows[0]["HopSo"].ToString();
                result.HoSoSo = dt.Rows[0]["HoSoSo"].ToString();
                result.NgonNgu = dt.Rows[0]["NgonNgu"].ToString();
                result.KyHieu = dt.Rows[0]["KyHieu"].ToString();
                result.TieuDe = dt.Rows[0]["TieuDe"].ToString();
                result.GhiChu = dt.Rows[0]["GhiChu"].ToString();
                result.ThoiGianBatDau = dt.Rows[0]["ThoiGianBatDau"].ToString();
                result.ThoiGianKetThuc = dt.Rows[0]["ThoiGianKetThuc"].ToString();
                result.ButTich = dt.Rows[0]["ButTich"].ToString();
                result.SoLuong = dt.Rows[0]["SoLuong"].ToString();
                result.CheDoSuDung = dt.Rows[0]["CheDoSuDung"].ToString();
                result.ThoiHanBaoQuan = dt.Rows[0]["ThoiHanBaoQuan"].ToString();
                result.TinhTrangVatLy = dt.Rows[0]["TinhTrangVatLy"].ToString();

            }
            return result;
        }


        public DataTable getAllSec()
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_HoSo ORDER BY ID";
            dt = getData(query);
            int stt = 1;
            dt.Columns.Add("stt", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }

        public DataTable getAllSec(string coquan, string maphong, string thoihan, string mucluc, string hopso, string textsearch)
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_HoSo ORDER BY ID";
            dt = getData(query);
            int stt = 1;
            dt.Columns.Add("stt", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }


        public DataTable search(string coquan, string maphong, string thoihan, string MucLucSo, string HopSo, string textsearch)
        {
            DataTable dt = null;
            string sql = @"SELECT h.*,p.TenPhong FROM Su_HoSo h inner join Su_PhongLuuTru p on p.MaPhong = h.MaPhong 
                           where 1 =1  ";
            if (!coquan.Equals(""))
            {
                sql += " AND h.CoQuan ='" + coquan + "'";
            }
            if (!thoihan.Equals(""))
            {
                sql += " AND h.ThoiHanBaoQuan ='" + thoihan + "'";
            }

            if (!MucLucSo.Equals(""))
            {
                sql += " AND upper(h.MucLucSo) like N'%" + MucLucSo.ToUpper().Trim() + "%'";
            }

            if (!HopSo.Equals(""))
            {
                sql += " AND upper(HopSo) like'%" + HopSo.ToUpper().Trim() + "%'";
            }

            if (!textsearch.Equals(""))
            {
                sql += " AND upper(TieuDe) like'%" + textsearch.ToUpper().Trim() + "%'";
            }
            dt = getData(sql);
            int stt = 1;
            dt.Columns.Add("stt", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }
        public DataTable search(string maphong, int page)
        {
            DataTable dt = null;
            string sql = @"SELECT row_number() over(order by h.HoSoSoNumber) row_num, count(h.id) over (partition by null) row_count, h.*,p.TenPhong FROM Su_HoSo h inner join Su_PhongLuuTru p on p.MaPhong = h.MaPhong 
                         where 1 =1  ";
            if (!maphong.Equals(""))
            {
                sql += " AND h.MaPhong  ='" + maphong + "'";
            }
            int start = 0, end = 0;
            start = (page - 1) * Constants.GRIDVIEW_PAGE_SIZE + 1;
            end = page * Constants.GRIDVIEW_PAGE_SIZE;
            string pSql = "SELECT * FROM (" + sql + ") tmp WHERE row_num BETWEEN " + start.ToString() + " AND " + end.ToString();
            logger.Info("HoSoLogic search query: "+pSql);
            dt = getData(pSql);
            int stt = (page - 1) * Constants.GRIDVIEW_PAGE_SIZE + 1;
            dt.Columns.Add("stt", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }

        public int addHoSo(HoSo newSec)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                int hoSoSoNumber = DatabaseUtil.extractNumber(newSec.HoSoSo);
                string query = "INSERT INTO Su_HoSo(CoQuan, MaPhong, MucLucSo, HopSo, HoSoSo,NgonNgu, KyHieu, "
                                +"TieuDe, GhiChu, ThoiGianBatDau, ThoiGianKetThuc, ButTich, SoLuong, "
                                +"CheDoSuDung, ThoiHanBaoQuan, TinhTrangVatLy, HoSoSoNumber) VALUES('"
                                + newSec.Coquan
                                + "', '" + newSec.MaPhong
                                 + "', '" + newSec.MucLucSo
                                + "', '" + newSec.HopSo
                                + "', '" + newSec.HoSoSo
                                + "', N'" + newSec.NgonNgu
                                + "', N'" + newSec.KyHieu
                                + "', N'" + newSec.TieuDe
                                + "', N'" + newSec.GhiChu
                                + "', '" + newSec.ThoiGianBatDau
                                + "', '" + newSec.ThoiGianKetThuc
                                + "', N'" + newSec.ButTich
                                + "', '" + newSec.SoLuong
                                + "',N'" + newSec.CheDoSuDung
                                + "', '" + newSec.ThoiHanBaoQuan
                                + "', '" + newSec.TinhTrangVatLy
                                + "',"+hoSoSoNumber.ToString()+") SELECT SCOPE_IDENTITY() as new_id;";
                conn.Open();
                DataTable tempTbl = getData(query);
                //executeDataByQuery(query, conn, null);
                int newId = 0;
                if (tempTbl != null && tempTbl.Rows.Count > 0)
                {
                    newId = (int)tempTbl.Rows[0][0];
                }
                result = newId;
            }
            catch (Exception e)
            {
                logUserManagement("addHoSo()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool updateHoSo(HoSo Sec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "UPDATE Su_HoSo SET Coquan = '" + Sec.Coquan
                                + "', MaPhong = '" + Sec.MaPhong
                                 + "', MucLucSo = '" + Sec.MucLucSo
                                  + "', HopSo = '" + Sec.HopSo
                                  + "', HoSoSo = '" + Sec.HoSoSo
                                 + "', NgonNgu = N'" + Sec.NgonNgu
                                  + "', KyHieu = N'" + Sec.KyHieu
                                   + "', TieuDe = N'" + Sec.TieuDe
                                    + "', GhiChu = N'" + Sec.GhiChu
                                    + "', ThoiGianBatDau = '" + Sec.ThoiGianBatDau
                                    + "', ThoiGianKetThuc = '" + Sec.ThoiGianKetThuc
                                    + "', ButTich = N'" + Sec.ButTich
                                    + "', SoLuong = '" + Sec.SoLuong
                                    + "', CheDoSuDung = N'" + Sec.CheDoSuDung
                                    + "', ThoiHanBaoQuan = N'" + Sec.ThoiHanBaoQuan
                                    + "', TinhTrangVatLy = N'" + Sec.TinhTrangVatLy
                                + "' WHERE ID = " + Sec.ID + "";
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
                updateHosoInVanBan(Sec, conn);
            }
            catch (Exception e)
            {
                logUserManagement("updateHoSo()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// Cap nhat thong tin ho so trong cac van ban lien quan 
        /// </summary>
        /// <param name="Hoso_ID"></param>
        public void updateHosoInVanBan(HoSo Sec, SqlConnection conn)
        {


            string query = "UPDATE Su_VanBanTrongHoSo SET HoSoSo = N'" + Sec.HoSoSo
                                  + "', Coquan = N'" + Sec.Coquan
                                  + "', MaPhong = N'" + Sec.MaPhong
                                    + "', MucLucSo = N'" + Sec.MucLucSo
                                    + "' WHERE Hoso_ID = " + Sec.ID + "";
            try
            {

                executeDataByQuery(query, conn, null);

            }
            catch (Exception e)
            {
                logUserManagement("updateHosoInVanBan()", e.Message);
            }

        }
        public bool deleteHoSo(int ID)
        {
            bool result = false;
            String query = "DELETE FROM Su_HoSo WHERE ID = " + ID + ";";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("deleteHoSo()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool validateSecName(string Name)
        {
            DataTable dt = new DataTable();
            bool result = true;
            string query = "SELECT Name FROM Su_HoSo WHERE upper(Name) = '" + Name.ToUpper() + "'";
            dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = false;
            }
            return result;
        }


        public bool validateSecNameNull(string Name)
        {
            bool result = true;
            if (Name.Trim().Equals(""))
            {
                result = false;
            }
            return result;
        }
        public bool executeDataByQuery(string query, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand cmd = null;

            if (trans != null)
            {
                cmd = new SqlCommand(query, conn, trans);
            }
            else
            {
                cmd = new SqlCommand(query, conn);
            }
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            return true;
        }

        private void logUserManagement(string method, string message)
        {
            string fileSource = "HoSo.cs";
            try
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory + "Logs";
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }
                string file = dir + "\\" + DateTime.Today.ToString("ddMMyyyy") + ".log";
                if (File.Exists(file) == false)
                {
                    File.Create(file).Dispose();
                }
                using (StreamWriter writer = File.AppendText(file))
                {
                    writer.WriteLine("\r\n" + DateTime.Now.ToString() + "\r\n" + "[" + fileSource + "]" + "(" + method + ")" + message);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {

            }
        }

        private static DataTable getData(string query)
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 0;
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;
        }
        public bool validateHoSo(HoSo hs, out string message)
        {
            bool rs = true;
            message = "";
            if(hs.Coquan==null || hs.Coquan.Trim().Length==0){
                rs = false;
                message += "Chưa nhập trường: Cơ quan lưu trữ, ";
            }
            if (hs.MaPhong == null || hs.MaPhong.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Phông lưu trữ, ";
            }
            if (hs.HoSoSo == null || hs.HoSoSo.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Số hồ sơ, ";
            }
            if (hs.TieuDe == null || hs.TieuDe.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Tiêu đề hồ sơ, ";
            }
            if (hs.ID > 0)
            {
                //Validate 4 update
            }
            return rs;
        }

    }

    public class HoSo
    {
        public int ID { get; set; }
        public string Coquan { get; set; }
        public string MaPhong { get; set; }
        public string MucLucSo { get; set; }
        public string HopSo { get; set; }
        public string HoSoSo { get; set; }
        public string NgonNgu { get; set; }
        public string KyHieu { get; set; }
        public string TieuDe { get; set; }

        public string GhiChu { get; set; }
        public string ThoiGianBatDau { get; set; }
        public string ThoiGianKetThuc { get; set; }
        public string ButTich { get; set; }
        public string SoLuong { get; set; }
        public string CheDoSuDung { get; set; }
        public string ThoiHanBaoQuan { get; set; }
        public string TinhTrangVatLy { get; set; }

        public HoSo() : this(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "") { }

        public HoSo(int ID, string Coquan, string MaPhong, string MucLucSo, string HopSo, string HoSoSo,
           string NgonNgu, string KyHieu, string TieuDe, string GhiChu, string ThoiGianBatDau,
           string ThoiGianKetThuc, string ButTich, string SoLuong, string CheDoSuDung, string ThoiHanBaoQuan, string TinhTrangVatLy)
        {
            this.ID = ID;
            this.Coquan = Coquan;
            this.MaPhong = MaPhong;
            this.MucLucSo = MucLucSo;
            this.HopSo = HopSo;
            this.HoSoSo = HoSoSo;
            this.NgonNgu = NgonNgu;
            this.KyHieu = KyHieu;
            this.TieuDe = TieuDe;
            this.GhiChu = GhiChu;
            this.ThoiGianBatDau = ThoiGianBatDau;
            this.ThoiGianKetThuc = ThoiGianKetThuc;
            this.ButTich = ButTich;
            this.SoLuong = SoLuong;
            this.CheDoSuDung = CheDoSuDung;
            this.ThoiHanBaoQuan = ThoiHanBaoQuan;
            this.TinhTrangVatLy = TinhTrangVatLy;
        }
    }
}