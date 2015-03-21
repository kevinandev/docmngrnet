using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using Function;

/// <summary>
/// Summary description for UserManagement
/// </summary>
namespace Logic
{
    public class VanBanTrongHoSoLogic
    {
        public const string USER_TYPE_ADMIN = "Admin";
        public const string USER_TYPE_APPROVER = "Approver";
        public const string EMAIL_POSTFIX = "@techcombank.com.vn";
        public const string APPROVER_AREA_NORTH = "N";
        public const string APPROVER_AREA_SOUTH = "S";
        public const string SESSION_SEC_ID = "";
        public const string SESSION_HOSO_ID = "";
        public const string SESSION_VANBAN_CACHE = "SESSION_VANBAN_CACHE";

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        public VanBanTrongHoSoLogic() { }

        public VanBanTrongHoSo getVanBanTrongHoSo(int ID)
        {
            VanBanTrongHoSo result = null;
            string query = "SELECT * FROM Su_VanBanTrongHoSo WHERE ID = " + ID + "";
            DataTable dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = new VanBanTrongHoSo();
                result.ID = ID;
                result.HoSoSo = dt.Rows[0]["HoSoSo"].ToString();
                result.Hoso_ID = dt.Rows[0]["Hoso_ID"].ToString();
                result.Coquan = dt.Rows[0]["Coquan"].ToString();
                result.MaPhong = dt.Rows[0]["MaPhong"].ToString();
                result.MucLucSo = dt.Rows[0]["MucLucSo"].ToString();
                result.KyHieuVanBan = dt.Rows[0]["KyHieuVanBan"].ToString();
                result.SoLuongTo = dt.Rows[0]["SoLuongTo"].ToString();
                result.ThoiGian = dt.Rows[0]["ThoiGian"].ToString();
                result.KiHieuThongTin = dt.Rows[0]["KiHieuThongTin"].ToString();
                result.NgonNgu = dt.Rows[0]["NgonNgu"].ToString();
                result.ToSo = dt.Rows[0]["ToSo"].ToString();
                result.TrichYeu = dt.Rows[0]["TrichYeu"].ToString();
                result.TacGia = dt.Rows[0]["TacGia"].ToString();
                result.DoMat = dt.Rows[0]["DoMat"].ToString();
                result.ButTich = dt.Rows[0]["ButTich"].ToString();
                result.MucDoTinCay = dt.Rows[0]["MucDoTinCay"].ToString();
                result.GhiChu = dt.Rows[0]["GhiChu"].ToString();
                result.FilePath = dt.Rows[0]["FilePath"].ToString();
                result.TinhTrangVatLy = dt.Rows[0]["TinhTrangVatLy"].ToString();
                result.ThoiHanBaoQuan = dt.Rows[0]["ThoiHanBaoQuan"].ToString();

            }
            return result;
        }


        public DataTable getAllSec()
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_VanBanTrongHoSo ORDER BY ID";
            dt = getData(query);
            return dt;
        }

        public DataTable getAllSec(string Coquan, string text)
        {
            DataTable dt = null;
            string query = "SELECT * FROM Su_VanBanTrongHoSo ORDER BY ID";
            dt = getData(query);
            return dt;
        }


        public int addVanBanTrongHoSo(VanBanTrongHoSo newSec)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                int thoiGianNumber = DatabaseUtil.extractNumberFromDateString(newSec.ThoiGian);
                string query = @"INSERT INTO Su_VanBanTrongHoSo ([HoSoSo]
                       ,[CoQuan]
                       ,[MaPhong]
                       ,[MucLucSo]
                       ,[KyHieuVanBan]
                       ,[SoLuongTo]
                       ,[ThoiGian]
                       ,[KiHieuThongTin]
                       ,[NgonNgu]
                       ,[ToSo]
                       ,[TrichYeu]
                       ,[TacGia]
                       ,[LoaiVanBan]
                       ,[DoMat]
                       ,[ButTich]
                       ,[MucDoTinCay]
                       ,[GhiChu]
                       ,[FilePath]
                       ,[TinhTrangVatLy]
                       ,[Hoso_ID],[ThoiGianNumber],[ThoiHanBaoQuan]) VALUES(N'"
                                + newSec.HoSoSo
                                + "',N'"
                                + newSec.Coquan
                                + "', N'" + newSec.MaPhong
                                + "', N'" + newSec.MucLucSo
                                + "', N'" + newSec.KyHieuVanBan
                                + "', N'" + newSec.SoLuongTo
                                + "', N'" + newSec.ThoiGian
                                + "', N'" + newSec.KiHieuThongTin
                                + "', N'" + newSec.NgonNgu
                                + "', N'" + newSec.ToSo
                                + "', N'" + newSec.TrichYeu
                                + "', N'" + newSec.TacGia
                                + "', N'" + newSec.LoaiVanBan
                                + "', N'" + newSec.DoMat
                                + "', N'" + newSec.ButTich
                                + "', N'" + newSec.MucDoTinCay
                                + "', N'" + newSec.GhiChu
                                + "', N'" + newSec.FilePath
                                + "', N'" + newSec.TinhTrangVatLy
                                + "', N'" + newSec.Hoso_ID
                                + "'," + thoiGianNumber.ToString() + ",'"+newSec.ThoiHanBaoQuan+"') SELECT SCOPE_IDENTITY() as new_id;";
                conn.Open();
                result = executeInsert(query);
                //result = true;
            }
            catch (Exception e)
            {
                logUserManagement("addVanBanTrongHoSo()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool updateVanBanTrongHoSo(VanBanTrongHoSo Sec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Su_VanBanTrongHoSo SET HoSoSo = N'" + Sec.HoSoSo
                                + "', Coquan = N'" + Sec.Coquan
                                + "', MaPhong = N'" + Sec.MaPhong
                                  + "', MucLucSo = N'" + Sec.MucLucSo
                                  + "', KyHieuVanBan = N'" + Sec.KyHieuVanBan
                                 + "', SoLuongTo = N'" + Sec.SoLuongTo
                                  + "', ThoiGian = N'" + Sec.ThoiGian
                                   + "', KiHieuThongTin = N'" + Sec.KiHieuThongTin
                                    + "', NgonNgu = N'" + Sec.NgonNgu
                                    + "', ToSo = N'" + Sec.ToSo
                                    + "', TrichYeu = N'" + Sec.TrichYeu
                                    + "', TacGia = N'" + Sec.TacGia
                                    + "', LoaiVanBan = N'" + Sec.LoaiVanBan
                                    + "', DoMat = N'" + Sec.DoMat
                                    + "', ButTich = N'" + Sec.ButTich
                                    + "', MucDoTinCay = N'" + Sec.MucDoTinCay
                                    + "', GhiChu = N'" + Sec.GhiChu
                                    + "', FilePath = '" + Sec.FilePath+"', ThoiHanBaoQuan='"+Sec.ThoiHanBaoQuan
                                + "', TinhTrangVatLy = N'" + Sec.TinhTrangVatLy + "' , Hoso_ID = N'" + Sec.Hoso_ID + "' WHERE ID = " + Sec.ID + "";
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("updateVanBanTrongHoSo()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool deleteVanBanTrongHoSo(int ID)
        {
            bool result = false;
            String query = "DELETE FROM Su_VanBanTrongHoSo WHERE ID = " + ID + ";";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("deleteVanBanTrongHoSo()", e.Message);
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
            string query = "SELECT Name FROM Su_VanBanTrongHoSo WHERE upper(Name) = '" + Name.ToUpper() + "'";
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

        public DataTable getAllSecByTop(string topn)
        {
            DataTable dt = null;
            string query = "SELECT top " + topn + " * from Su_VanbanTrongHoSo order by Convert(datetime,[ThoiGian],103) desc ";
            dt = getData(query);
            return dt;
        }
        private void logUserManagement(string method, string message)
        {
            string fileSource = "VanBanTrongHoSo.cs";
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
        private int executeInsert(string query)
        {
            int rs = 0;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 0;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                try
                {
                    string tRs = reader["new_id"].ToString();
                    rs = Int32.Parse(tRs);
                }
                catch (Exception ex)
                {
                    logUserManagement("executeInsert", "Lỗi lấy kết quả lệnh insert: " + ex.Message);
                }

            }
            conn.Close();
            return rs;

        }
        public bool validateVanBan(VanBanTrongHoSo vb, out string message)
        {
            bool rs = true;
            message = "";
            if (vb.Coquan == null || vb.Coquan.Trim().Length == 0)
            {
                rs = false;
                message += "Chưa nhập trường: Cơ quan lưu trữ, ";
            }
            if(vb.MaPhong == null || vb.MaPhong.Trim().Length == 0){
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Phông lưu trữ, ";
            }
            if (vb.HoSoSo == null || vb.HoSoSo.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Hồ sơ, ";
            }
            if (vb.KyHieuVanBan == null || vb.KyHieuVanBan.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Ký hiệu văn bản, ";
            }
            if (vb.ThoiGian == null || vb.ThoiGian.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Thời gian, ";
            }
            if (vb.LoaiVanBan == null || vb.LoaiVanBan.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Loại văn bản, ";
            }
            if (vb.DoMat == null || vb.DoMat.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Độ mật, ";
            }
            if (vb.TacGia == null || vb.TacGia.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Tác giả, ";
            }
            if (vb.MucDoTinCay == null || vb.MucDoTinCay.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Mức độ tin cậy, ";
            }
            if (vb.ThoiHanBaoQuan == null || vb.ThoiHanBaoQuan.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Thời hạn bảo quản, ";
            }
            if (vb.TinhTrangVatLy == null || vb.TinhTrangVatLy.Trim().Length == 0)
            {
                rs = false;
                if (message.Length == 0)
                {
                    message += "Chưa nhập trường: ";
                }
                message += "Tình trạng vật lý, ";
            }
            return rs;
        }
    }

    public class VanBanTrongHoSo
    {
        public int ID { get; set; }
        public string Hoso_ID { get; set; }
        public string HoSoSo { get; set; }
        public string Coquan { get; set; }
        public string MaPhong { get; set; }
        public string MucLucSo { get; set; }
        public string KyHieuVanBan { get; set; }
        public string SoLuongTo { get; set; }
        public string ThoiGian { get; set; }
        public string KiHieuThongTin { get; set; }
        public string NgonNgu { get; set; }
        public string ToSo { get; set; }
        public string TrichYeu { get; set; }
        public string TacGia { get; set; }
        public string LoaiVanBan { get; set; }
        public string DoMat { get; set; }
        public string ButTich { get; set; }
        public string MucDoTinCay { get; set; }
        public string GhiChu { get; set; }
        public string FilePath { get; set; }
        public string TinhTrangVatLy { get; set; }
        public string ThoiHanBaoQuan { get; set; }

        public VanBanTrongHoSo() : this(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "") { }
        public VanBanTrongHoSo(int ID, string Hoso_ID, string HoSoSo, string Coquan, string MaPhong, string MucLucSo, string KyHieuVanBan, string SoLuongTo,
           string ThoiGian, string KiHieuThongTin, string NgonNgu, string ToSo, string TrichYeu,
           string TacGia, string LoaiVanBan, string DoMat, string ButTich, string MucDoTinCay, string GhiChu, string FilePath, string TinhTrangVatLy)
        {
            this.ID = ID;
            this.Hoso_ID = Hoso_ID;
            this.HoSoSo = HoSoSo;
            this.Coquan = Coquan;
            this.MaPhong = MaPhong;
            this.MucLucSo = MucLucSo;
            this.KyHieuVanBan = KyHieuVanBan;
            this.SoLuongTo = SoLuongTo;
            this.ThoiGian = ThoiGian;
            this.KiHieuThongTin = KiHieuThongTin;
            this.NgonNgu = NgonNgu;
            this.ToSo = ToSo;
            this.TrichYeu = TrichYeu;
            this.TacGia = TacGia;
            this.LoaiVanBan = LoaiVanBan;
            this.DoMat = DoMat;
            this.ButTich = ButTich;
            this.MucDoTinCay = MucDoTinCay;
            this.GhiChu = GhiChu;
            this.FilePath = FilePath;
            this.TinhTrangVatLy = TinhTrangVatLy;
        }
    }
}