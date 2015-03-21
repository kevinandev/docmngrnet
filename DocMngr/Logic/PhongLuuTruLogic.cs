using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// Summary description for UserManagement
/// </summary>
namespace Logic
{
    public class PhongLuuTruLogic
    {
        public const string USER_TYPE_ADMIN = "Admin";
        public const string USER_TYPE_APPROVER = "Approver";
        public const string EMAIL_POSTFIX = "@techcombank.com.vn";
        public const string APPROVER_AREA_NORTH = "N";
        public const string APPROVER_AREA_SOUTH = "S";
        public const string SESSION_SEC_ID = "umID";

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
        public PhongLuuTruLogic() { }

        public PhongLuuTru getPhongLuuTru(int ID)
        {
            PhongLuuTru result = null;
            string query = "SELECT * FROM Su_PhongLuuTru WHERE ID = " + ID + "";
            DataTable dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = new PhongLuuTru();
                result.ID = ID;
                result.Coquan = dt.Rows[0]["Coquan"].ToString();
                result.MaPhong = dt.Rows[0]["MaPhong"].ToString();
                result.TenPhong = dt.Rows[0]["TenPhong"].ToString();
                result.LichSu = dt.Rows[0]["LichSu"].ToString();
                result.ThoigianTaiLieu = dt.Rows[0]["ThoigianTaiLieu"].ToString();
                result.TongSoTaiLieu = dt.Rows[0]["TongSoTaiLieu"].ToString();
                result.TaiLieuChinhLy = dt.Rows[0]["TaiLieuChinhLy"].ToString();
                result.TaiLieuChuaChinhLy = dt.Rows[0]["TaiLieuChuaChinhLy"].ToString();
                result.CacNhomTaiLieu = dt.Rows[0]["CacNhomTaiLieu"].ToString();
                result.NhomTaiLieuKhac = dt.Rows[0]["NhomTaiLieuKhac"].ToString();
                result.ThoiGianNhap = dt.Rows[0]["ThoiGianNhap"].ToString();
                result.NgonNgu = dt.Rows[0]["NgonNgu"].ToString();
                result.CongCu = dt.Rows[0]["CongCu"].ToString();
                result.BanSaoBaoHiem = dt.Rows[0]["BanSaoBaoHiem"].ToString();
                result.GhiChu = dt.Rows[0]["GhiChu"].ToString();

            }
            return result;
        }


        public DataTable getAllSec()
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_PhongLuuTru ORDER BY ID";
            dt = getData(query);
            dt.Columns.Add("stt", typeof(string));
            int stt = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }

        public DataTable getAllSecByConnection(SqlConnection conn)
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_PhongLuuTru ORDER BY ID";
            dt = getDataByConnection(query, conn);
            dt.Columns.Add("stt", typeof(string));
            int stt = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["stt"] = stt.ToString();
                stt++;
            }
            return dt;
        }

        public static DataTable getDataByConnection(string query, SqlConnection conn)
        {
            DataTable dataTable = new DataTable();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 0;
            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;
        }
         public DataTable getAllSec(string Coquan, string text)
        {
            DataTable dt = null;
            string sql = "SELECT * FROM Su_PhongLuuTru where 1=1  ";
            if (!Coquan.Equals(""))
            {
                sql += " AND CoQuan ='" + Coquan + "'";
            }
            if (!text.Equals(""))
            {
                sql += " AND upper(TenPhong) like  N'%" + text.ToUpper().Trim() + "%'";
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


        public bool addPhongLuuTru(PhongLuuTru newSec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {

                string query = @"INSERT INTO Su_PhongLuuTru ([Coquan]
           ,[MaPhong]
           ,[TenPhong]
           ,[LichSu]
           ,[ThoigianTaiLieu]
           ,[TongSoTaiLieu]
           ,[TaiLieuChinhLy]
           ,[TaiLieuChuaChinhLy]
           ,[CacNhomTaiLieu]
           ,[NhomTaiLieuKhac]
           ,[ThoiGianNhap]
           ,[NgonNgu]
           ,[CongCu]
           ,[BanSaoBaoHiem]
           ,[GhiChu]) VALUES('"
                                + newSec.Coquan
                                + "', N'" + newSec.MaPhong
                                + "', N'" + newSec.TenPhong
                                + "', N'" + newSec.LichSu
                                + "', N'" + newSec.ThoigianTaiLieu
                                + "', N'" + newSec.TongSoTaiLieu
                                + "', N'" + newSec.TaiLieuChinhLy
                                + "', N'" + newSec.TaiLieuChuaChinhLy
                                + "', N'" + newSec.CacNhomTaiLieu
                                + "', N'" + newSec.NhomTaiLieuKhac
                                + "', N'" + newSec.ThoiGianNhap
                                + "', N'" + newSec.NgonNgu
                                + "', N'" + newSec.CongCu
                                + "', N'" + newSec.BanSaoBaoHiem
                                + "', N'" + newSec.GhiChu

                                + "');";
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("addPhongLuuTru()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool updatePhongLuuTru(PhongLuuTru Sec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Su_PhongLuuTru SET Coquan = '" + Sec.Coquan
                            + "', MaPhong = N'" + Sec.MaPhong
                                + "', TenPhong = N'" + Sec.TenPhong
                                 + "', LichSu = N'" + Sec.LichSu
                                  + "', TongSoTaiLieu = N'" + Sec.TongSoTaiLieu
                                  + "', TaiLieuChinhLy = N'" + Sec.TaiLieuChinhLy
                                 + "', TaiLieuChuaChinhLy = N'" + Sec.TaiLieuChuaChinhLy
                                  + "', CacNhomTaiLieu = N'" + Sec.CacNhomTaiLieu
                                   + "', NhomTaiLieuKhac = N'" + Sec.NhomTaiLieuKhac
                                    + "', ThoiGianNhap = N'" + Sec.ThoiGianNhap
                                    + "', ThoigianTaiLieu = N'" + Sec.ThoigianTaiLieu
                                    + "', NgonNgu = N'" + Sec.NgonNgu
                                    + "', CongCu = N'" + Sec.CongCu
                                    + "', BanSaoBaoHiem = N'" + Sec.BanSaoBaoHiem
                                    + "', GhiChu = N'" + Sec.GhiChu
                                + "' WHERE ID = " + Sec.ID + "";
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("updatePhongLuuTru()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool deletePhongLuuTru(int ID)
        {
            bool result = false;
            String query = "DELETE FROM Su_PhongLuuTru WHERE ID = " + ID + ";";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("deletePhongLuuTru()", e.Message);
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
            string query = "SELECT Name FROM Su_PhongLuuTru WHERE upper(Name) = '" + Name.ToUpper() + "'";
            dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = false;
            }
            return result;
        }

        public bool validateFieldExistedData(string FieldName, string FieldData)
        {
            DataTable dt = new DataTable();
            bool result = true;
            string query = "SELECT " + FieldName + " FROM Su_PhongLuuTru WHERE upper(" + FieldName + ") = '" + FieldData.ToUpper() + "'";
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
            string fileSource = "PhongLuuTru.cs";
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
    }

    public class PhongLuuTru
    {
        public int ID { get; set; }
        public string Coquan { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LichSu { get; set; }
        public string ThoigianTaiLieu { get; set; }
        public string TongSoTaiLieu { get; set; }
        public string TaiLieuChinhLy { get; set; }
        public string TaiLieuChuaChinhLy { get; set; }

        public string CacNhomTaiLieu { get; set; }
        public string NhomTaiLieuKhac { get; set; }
        public string ThoiGianNhap { get; set; }
        public string NgonNgu { get; set; }
        public string CongCu { get; set; }
        public string BanSaoBaoHiem { get; set; }
        public string GhiChu { get; set; }


        public PhongLuuTru() : this(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "") { }

        public PhongLuuTru(int ID, string Coquan, string MaPhong, string TenPhong, string LichSu, string ThoigianTaiLieu,
           string TongSoTaiLieu, string TaiLieuChinhLy, string TaiLieuChuaChinhLy, string CacNhomTaiLieu, string NhomTaiLieuKhac,
           string ThoiGianNhap, string NgonNgu, string CongCu, string BanSaoBaoHiem, string GhiChu)
        {
            this.ID = ID;
            this.Coquan = Coquan;
            this.MaPhong = MaPhong;
            this.TenPhong = TenPhong;
            this.LichSu = LichSu;
            this.ThoigianTaiLieu = ThoigianTaiLieu;
            this.TongSoTaiLieu = TongSoTaiLieu;
            this.TaiLieuChinhLy = TaiLieuChinhLy;
            this.TaiLieuChuaChinhLy = TaiLieuChuaChinhLy;
            this.CacNhomTaiLieu = CacNhomTaiLieu;
            this.NhomTaiLieuKhac = NhomTaiLieuKhac;
            this.ThoiGianNhap = ThoiGianNhap;
            this.NgonNgu = NgonNgu;
            this.CongCu = CongCu;
            this.BanSaoBaoHiem = BanSaoBaoHiem;
            this.GhiChu = GhiChu;
        }
    }
}