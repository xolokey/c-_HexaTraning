
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace EcommerceApp.Util
{
    public static class DBConnectionUtil
    {
        public static SqlConnection GetConnection(string congfigFile)
        {
            SqlConnection sqlconnection;
            string connstr= DBUtil.GetConnectionString(congfigFile);
            return new SqlConnection(connstr);
        }
    }
   
    
}
