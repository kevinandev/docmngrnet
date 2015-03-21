using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Function
{
    public static class DatabaseUtil
    {
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;

        public static bool executeDataByQuery(string query, SqlConnection conn, SqlTransaction trans)
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

        public static void logUserManagement(string method, string message)
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

        public static SqlConnection getConnection() {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public static int extractNumber(string s)
        {
            if (s == null || s.Trim().Length == 0)
            {
                return 0;
            }
            string number = "";
            for (int i=0;i<s.Length;i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    number += s[i];
                }
            }
            int rs = 0;
            if (number.Length > 0)
            {
                Int32.TryParse(number, out rs);
            }
            return rs;
        }
        public static int extractNumberFromDateString(string s)
        {
            DateTime d;
            int rs = 0;
            if (DateTime.TryParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                string t = d.Year.ToString() + d.Month.ToString() + d.Day.ToString();
                Int32.TryParse(t, out rs);
            }
            else
            {
                rs = -1;
            }
            return rs;
        }

    }
}