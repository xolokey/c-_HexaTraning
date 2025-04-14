using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceApp.Entities;
using EcommerceApp.Util;
using Microsoft.Data.SqlClient;

namespace EcommerceApp.DAO
{
    public class UserDao:IUserDao<UserInfo>
    {
        SqlConnection sqlcon = DBConnectionUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public bool AuthenticateUser(UserInfo userInfo)
        {
            try
            {
                cmd.Connection = sqlcon;
                StringBuilder queryBuilder= new StringBuilder();
                queryBuilder =queryBuilder.Append($"Select * from UserInfo where Email='{userInfo.EmailId}'and Password='{userInfo.Password}'");
                cmd.CommandText = queryBuilder.ToString();


                if(sqlcon.State == System.Data.ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                dr = cmd.ExecuteReader();
                var isExist = dr.HasRows;
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch(SqlException)
            {
                return false;
            }
            finally
            {
                dr?.Close();
                sqlcon.Close();
                // Close the connection
                // Dispose of the connection
            }

        }
    }
    
    
}
