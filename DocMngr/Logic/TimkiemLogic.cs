using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using log4net;

/// <summary>
/// Summary description for Timkiem
/// </summary>
namespace Logic
{
public  class TimkiemLogic
{
    ILog logger = log4net.LogManager.GetLogger("File");
    public TimkiemLogic()
    { 
    }
 
    public  DataTable timkiemHoSo(SqlConnection conn, string CoQuan, string MaPhong, string ThoiHanBaoQuan, string TinhTrangVatLy, string MucLucSo,
        string CheDoSuDung, string Keyword)
    {
        DataTable dt = new DataTable();
        string sql = @"SELECT h.*, ml.Name as MucLucSov,hs.Name as HopSov, th.Name as ThoiHanBaoQuanv FROM Su_HoSo h left join Su_MucLuc ml on ml.ID = h.MucLucSo
        left join Su_HopHoSo hs on hs.ID = h.HopSo left join Su_ThoiHanBaoQuan th on th.ID = h.ThoiHanBaoQuan WHERE 1=1  ";
        if (!CoQuan.Equals("")) {
            sql += " AND h.Coquan =N'" + CoQuan + "'";
        }
        if (!MaPhong.Equals(""))
        {
            sql += " AND h.MaPhong =N'" + MaPhong + "'";
        }
        if (!ThoiHanBaoQuan.Equals(""))
        {
            sql += " AND h.ThoiHanBaoQuan =N'" + ThoiHanBaoQuan + "'";
        }
        if (!TinhTrangVatLy.Equals(""))
        {
            sql += " AND h.TinhTrangVatLy =N'" + TinhTrangVatLy + "'";
        }
        if (!MucLucSo.Equals(""))
        {
            sql += " AND h.MucLucSo =N'" + MucLucSo + "'";
        }
        if (!CheDoSuDung.Equals(""))
        {
            sql += " AND h.CheDoSuDung =N'" + CheDoSuDung + "'";
        }
        
        if (!Keyword.Equals(""))
        {
            sql += " AND upper(TieuDe) like N'%" + Keyword.ToUpper().Trim() + "%'";
        }

     
        dt = getDataByConnection(sql, conn);
        // add stt column 
        DataColumn dc = new DataColumn("stt", typeof(String));
        dt.Columns.Add(dc);
        int index = 1;
        foreach (DataRow d in dt.Rows)
        {
            d["stt"] = index.ToString();
            index++;
        }
        return dt;
    }
    public DataTable timkiemHoSoAdvance(SqlConnection conn, string CoQuan, string MaPhong, string ThoiHanBaoQuan, string TinhTrangVatLy, string MucLucSo,
        string CheDoSuDung, string Keyword, string ThoiGianStartFrom, string ThoiGianStartTo, string ThoiGianEndFrom, string ThoiGianEndTo)
    {
        DataTable dt = new DataTable();
        string sql = @"SELECT h.*, ml.Name as MucLucSov,hs.Name as HopSov, th.Name as ThoiHanBaoQuanv FROM Su_HoSo h left join Su_MucLuc ml on cast(ml.ID as nvarchar) = h.MucLucSo
        left join Su_HopHoSo hs on cast(hs.ID as nvarchar) = h.HopSo left join Su_ThoiHanBaoQuan th on th.ID = h.ThoiHanBaoQuan WHERE 1=1  ";
        if (!CoQuan.Equals(""))
        {
            sql += " AND h.Coquan ='" + CoQuan + "'";
        }
        if (!MaPhong.Equals(""))
        {
            sql += " AND h.MaPhong ='" + MaPhong + "'";
        }
        if (!ThoiHanBaoQuan.Equals(""))
        {
            sql += " AND h.ThoiHanBaoQuan ='" + ThoiHanBaoQuan + "'";
        }
        if (!TinhTrangVatLy.Equals(""))
        {
            sql += " AND h.TinhTrangVatLy ='" + TinhTrangVatLy + "'";
        }
        if (!MucLucSo.Equals(""))
        {
            sql += " AND h.MucLucSo ='" + MucLucSo + "'";
        }
        if (!CheDoSuDung.Equals(""))
        {
            sql += " AND h.CheDoSuDung ='" + CheDoSuDung + "'";
        }

        if (!Keyword.Equals(""))
        {
            sql += " AND upper(TieuDe) like N'%" + Keyword.ToUpper().Trim() + "%'";
        }

        if (!ThoiGianStartFrom.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGianBatDau],103) >= Convert(datetime," + ThoiGianStartFrom + ",103) ";
        }
        if (!ThoiGianStartTo.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGianBatDau],103) <= Convert(datetime," + ThoiGianStartTo + ",103) ";
        }
        if (!ThoiGianEndFrom.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGianKetThuc],103) >= Convert(datetime," + ThoiGianEndFrom + ",103) ";
        }
        if (!ThoiGianEndTo.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGianKetThuc],103) <= Convert(datetime," + ThoiGianEndTo + ",103) ";
        }
        dt = getDataByConnection(sql, conn);
        // add stt column 
        DataColumn dc = new DataColumn("stt", typeof(String));
        dt.Columns.Add(dc);
        int index = 1;
        foreach (DataRow d in dt.Rows)
        {
            d["stt"] = index.ToString();
            index++;
        }
        return dt;
    }
    public  DataTable timkiemVanBan(SqlConnection conn, string CoQuan, string MaPhong, string TinhTrangVatLy, string LoaiVanBan,
        string KyHieuVanBan, string TacGia, string ThoiGianFrom, string ThoiGianTo, string trichYeu)
    {
        DataTable dt = new DataTable();
        string sql = @"SELECT h.*, lvb.Name as LoaiVanBanv, th.Name as MucDoTinCayv , hs.HopSo, cq.Name as TenCoQuan
            FROM Su_VanbanTrongHoSo h 
            left join Su_HoSo hs on hs.HoSoSo = h.HoSoSo
            left join Su_CoQuanLuuTru cq on hs.Coquan = cq.Code
            left join Su_LoaiVanban lvb on lvb.ID = h.LoaiVanBan 
            left join Su_MucDoTinCay th on th.ID = h.MucDoTinCay  
            WHERE 1=1 ";
        if (!CoQuan.Equals(""))
        {
            sql += " AND h.Coquan =N'" + CoQuan + "'";
        }
        if (!MaPhong.Equals(""))
        {
            sql += " AND h.MaPhong =N'" + MaPhong + "'";
        }
        if (!TinhTrangVatLy.Equals(""))
        {
            sql += " AND h.TinhTrangVatLy =N'" + TinhTrangVatLy + "'";
        }

        if (!LoaiVanBan.Equals(""))
        {
            sql += " AND h.LoaiVanBan =N'" + LoaiVanBan + "'";
        }
        
        if (!TacGia.Equals(""))
        {
            sql += " AND upper(h.TacGia) like N'%" + TacGia.ToUpper().Trim() + "%'";
        }

        if (!KyHieuVanBan.Equals(""))
        {
            sql += " AND upper(h.KyHieuVanBan) like N'%" + KyHieuVanBan.ToUpper().Trim() + "%'";
        }
        if (!ThoiGianFrom.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGian],103) >= Convert(datetime," + ThoiGianFrom + ",103) ";
        }
        if (!ThoiGianTo.Equals(""))
        {
            sql += " AND Convert(datetime,[ThoiGian],103) <= Convert(datetime," + ThoiGianTo + ",103) ";
        }
        if (!"".Equals(trichYeu))
        {
            sql += " AND upper(h.TrichYeu) like N'%"+trichYeu.ToUpper()+"%' ";
        }
        logger.Info("Search VB Query: "+sql);
        dt = getDataByConnection(sql, conn);
        // add stt column 
        DataColumn dc = new DataColumn("stt", typeof(String));
        dt.Columns.Add(dc);
        int index = 1;
        foreach (DataRow d in dt.Rows)
        {
            d["stt"] = index.ToString();
            index++;
        }
        return dt;
    }
    public  DataTable getData(string query)
    {
        DataTable dataTable = new DataTable();
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
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

    public  DataTable getDataByConnection(string query, SqlConnection conn)
    {
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.CommandTimeout = 0;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dataTable);
        da.Dispose();
        return dataTable;
    }

    public  DataTable getDataByConnectionTrans(string query, SqlConnection conn, SqlTransaction trans)
    {
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand(query, conn, trans);
        cmd.CommandTimeout = 0;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dataTable);
        da.Dispose();
        return dataTable;
    }
}
}