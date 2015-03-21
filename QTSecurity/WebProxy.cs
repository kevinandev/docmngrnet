using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTSecurity
{
    public class WebProxy
    {
        private static string appDB;
        public void init(string db)
        {
            appDB = db;
        }
        public string getToken(string functionCode)
        {
            return "P@ssw0rd"; 
        }
        public bool validateToken(string token)
        {
            return true;
        }
    }
}
