using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EcommerceApp.Util
{
    public static class DBUtil
    {
        public static string GetConnectionString(string filePath)
        {
            var builder =new ConfigurationBuilder().SetBasePath(Directory .GetCurrentDirectory()).AddJsonFile(filePath);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
