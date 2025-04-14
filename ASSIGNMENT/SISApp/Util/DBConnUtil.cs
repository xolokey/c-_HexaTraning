﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SISApp.Util
{
    public class DBConnUtil
    {
       public static SqlConnection GetConnection(string configFile)
       {
           SqlConnection sqlConnection;
             string connstr = DBPropertyUtil.GetConnectionString(configFile);
           return new SqlConnection(connstr);
        }
  

    }
}