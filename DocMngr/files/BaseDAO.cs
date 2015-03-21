using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Dao
{
    public class BaseDAO
    {
        protected static string connectionString = "";
        public BaseDAO()
        {
            if (connectionString == null || connectionString.Length == 0)
            {
                connectionString = ConfigurationManager.ConnectionStrings["appDB"].ConnectionString;
            }
        }

    }
}