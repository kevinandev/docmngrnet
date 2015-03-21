using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bean;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dao
{

    public class MenuDAO : BaseDAO
    {
        public MenuDAO()
            : base()
        {

        }
        public List<MenuBean> getListMainMenu(string user)
        {
            List<MenuBean> lstRS = new List<MenuBean>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(null, con);
                SqlDataReader rd = null;
                cmd.CommandText = "SELECT m.id FROM menu m " +
                                    "where ((m.id in (select mr.menu_id from menu_role mr " +
                                    "				where role_id in (select uir.RoleId from aspnet_UsersInRoles uir, aspnet_Users u " +
                                    "									where u.UserId=uir.UserId and u.LoweredUserName=@userName))) " +
                                    "	or (m.secure_level<1)) and m.type = 0 and m.active > 0  order by m.[order]";
                Debug.WriteLine("Get list main menu: " + cmd.CommandText);
                cmd.Parameters.Add("@userName", System.Data.SqlDbType.NVarChar);
                cmd.Parameters["@userName"].Value = user;
                //SqlParameter parUserName = new SqlParameter();
                //parUserName.DbType = System.Data.DbType.String;
                //parUserName.Value = user;
                //parUserName.ParameterName = "@userName";
                //cmd.Parameters.Add(parUserName);
                con.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    MenuBean temp = loadMenu((int)rd["id"], user);
                    if (temp != null)
                    {
                        lstRS.Add(temp);
                    }
                }
            }
            return lstRS;
        }
        private MenuBean loadMenu(int menuId, string user)
        {
            MenuBean rs = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(null, con);
                SqlDataReader rd = null;
                cmd.CommandText = "SELECT * FROM menu M WHERE M.id=@menuId";
                SqlParameter parMenuId = new SqlParameter();
                parMenuId.DbType = System.Data.DbType.Int32;
                parMenuId.Value = menuId;
                parMenuId.ParameterName = "menuId";
                cmd.Parameters.Add(parMenuId);
                con.Open();
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    rs = new MenuBean();
                    object objId = rd["id"];
                    rs.Id = (int)objId;
                    object objOrder = rd["order"];
                    rs.Order = (int)objOrder;
                    object objSecureLevel = rd["secure_level"];
                    rs.SecureLevel = (int)objSecureLevel;
                    object objSelectable = rd["selectable"];
                    int selectable = (int)objSelectable;
                    rs.Selectable = selectable > 0;
                    object objText = rd["text"];
                    rs.Text = objText != null ? (string)objText : "";
                    object objType = rd["type"];
                    rs.Type = (int)objType;
                    object objUrl = rd["url"];
                    rs.Url = !DBNull.Value.Equals(objUrl) ? (string)objUrl : "";
                }
            }
            if (rs != null)
            {
                rs.LstSub = loadChildrens(rs.Id, user);
                return rs;
            }
            else
            {
                return null;
            }
        }
        private List<MenuBean> loadChildrens(int menuId, string user)
        {
            List<MenuBean> lstRS = new List<MenuBean>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(null, con);
                SqlDataReader rd = null;
                cmd.CommandText = "SELECT m.id FROM menu M WHERE M.active > 0 and M.master_id=@masterId and m.id in (select mr.menu_id from menu_role mr " +
                                    "where role_id in (select uir.RoleId from aspnet_UsersInRoles uir, aspnet_Users u " +
                                                        "where u.UserId=uir.UserId and u.LoweredUserName=@userName))  order by m.[order]";
                SqlParameter parMasterId = new SqlParameter();
                parMasterId.DbType = System.Data.DbType.Int32;
                parMasterId.Value = menuId;
                parMasterId.ParameterName = "@masterId";
                SqlParameter parUserName = new SqlParameter();
                parUserName.DbType = System.Data.DbType.String;
                parUserName.Value = user;
                parUserName.ParameterName = "@userName";
                cmd.Parameters.Add(parMasterId);
                cmd.Parameters.Add(parUserName);
                con.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    lstRS.Add(loadMenu((int)rd["id"], user));
                }
            }
            return lstRS;
        }

    }
}