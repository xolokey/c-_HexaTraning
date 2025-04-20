using System;
using CarConnectApp.Entities;
using CarConnectApp.Util;
using Microsoft.Data.SqlClient;

namespace CarConnectApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Customer AuthenticateCustomer(string username, string password)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                sqlCon.Open();

                string query = "SELECT * FROM Customer WHERE Username = @Username AND Password = @Password";
                using SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Customer authentication successful!");
                    return new Customer
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Address = reader.GetString(5),
                        UserName = reader.GetString(6),
                        Password = reader.GetString(7),
                        RegistrationDate = reader.GetDateTime(8)
                    };
                }
                else
                {
                    throw new CarConnectApp.Exception.AuthenticationException("Invalid username or password for customer.");
                }
            }
            catch (SqlException ex)
            {
                throw new CarConnectApp.Exception.AuthenticationException("Database error during customer authentication.", ex);
            }
            catch (Exception ex)
            {
                throw new CarConnectApp.Exception.AuthenticationException("Unexpected error during customer authentication.", ex);
            }
        }

        public Admin AuthenticateAdmin(string username, string password)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                sqlCon.Open();

                string query = "SELECT * FROM Admin WHERE Username = @Username AND Password = @Password";
                using SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Admin authentication successful!");
                    return new Admin
                    {
                        AdminID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Username = reader.GetString(5),
                        Password = reader.GetString(6),
                        Role = reader.GetString(7),
                        JoinDate = reader.GetDateTime(8)
                    };
                }
                else
                {
                    throw new CarConnectApp.Exception.AuthenticationException("Invalid username or password for admin.");
                }
            }
            catch (SqlException ex)
            {
                throw new CarConnectApp.Exception.AuthenticationException("Database error during admin authentication.", ex);
            }
            catch (Exception ex)
            {
                throw new CarConnectApp.Exception.AuthenticationException("Unexpected error during admin authentication.", ex);
            }
        }
    }
}

