﻿using System;
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
    public class Su_MucDoTinCayLogic
    {

        public const string USER_TYPE_ADMIN = "Admin";
        public const string USER_TYPE_APPROVER = "Approver";
        public const string EMAIL_POSTFIX = "@techcombank.com.vn";
        public const string APPROVER_AREA_NORTH = "N";
        public const string APPROVER_AREA_SOUTH = "S";
        public const string SESSION_SEC_ID = "";

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
        public Su_MucDoTinCayLogic() { }

        public MucDoTinCay getMucDoTinCay(int ID)
        {
            MucDoTinCay result = null;
            string query = "SELECT ID, Name, Description FROM Su_MucDoTinCay WHERE ID = " + ID + "";
            DataTable dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = new MucDoTinCay();
                result.ID = ID;
                result.Name = dt.Rows[0][1].ToString();
                result.Description = dt.Rows[0][2].ToString();
            }
            return result;
        }


        public DataTable getAllSec()
        {
            DataTable dt = null;
            string query = "SELECT ID, Name, Description FROM Su_MucDoTinCay ORDER BY ID";
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


        public bool addMucDoTinCay(MucDoTinCay newSec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                string query = "INSERT INTO Su_MucDoTinCay (Name, Description) VALUES(N'"
                                + newSec.Name
                                + "', N'" + newSec.Description
                                + "');";
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("addApprover()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool updateMucDoTinCay(MucDoTinCay Sec)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE Su_MucDoTinCay SET Name = N'" + Sec.Name
                                + "', Description = N'" + Sec.Description
                                + "' WHERE ID = " + Sec.ID + "";
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("updateMucDoTinCay()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool deleteMucDoTinCay(int ID)
        {
            bool result = false;
            String query = "DELETE FROM Su_MucDoTinCay WHERE ID = " + ID + ";";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("deleteApprover()", e.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public bool deleteListMucDoTinCay(string whereID)
        {
            bool result = false;
            String query = "DELETE FROM Su_MucDoTinCay WHERE ID  in( " + whereID + ");";
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                executeDataByQuery(query, conn, null);
                result = true;
            }
            catch (Exception e)
            {
                logUserManagement("deleteApprover()", e.Message);
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
            string query = "SELECT Name FROM Su_MucDoTinCay WHERE upper(Name) = '" + Name.ToUpper() + "'";
            dt = getData(query);
            if (dt.Rows.Count > 0)
            {
                result = false;
            }
            return result;
        }

        public bool validateSecName4Update(string Name, int id)
        {
            DataTable dt = new DataTable();
            bool result = true;
            string query = "SELECT Name FROM Su_MucDoTinCay WHERE upper(Name) = '" + Name.ToUpper() + "' and ID <> " + id.ToString();
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
            string fileSource = "MucDoTinCay.cs";
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


    public class MucDoTinCay
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public MucDoTinCay() : this(0, "", "") { }

        public MucDoTinCay(int ID, string Name, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
        }
    }
}