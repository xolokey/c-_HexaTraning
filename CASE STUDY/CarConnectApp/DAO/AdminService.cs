using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Entities;
using Microsoft.Data.SqlClient;
using CarConnectApp.Util;
using CarConnectApp.Exception;

namespace CarConnectApp.DAO
{
    public class AdminService : IAdminService<Admin>
    {
        //To Register a Admin
        public Admin RegisterAdmin(Admin admin)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Admin (AdminID, FirstName, LastName, Email, PhoneNumber,UserName, Password,Role,JoinDate) VALUES (@AdminID, @FirstName,@LastName, @Email, @PhoneNumber, @Address,@Username,@Password, @RegistrationDate)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                        cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                        cmd.Parameters.AddWithValue("@Email", admin.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Address", admin.Username);
                        cmd.Parameters.AddWithValue("@Password", admin.Password);
                        cmd.Parameters.AddWithValue("@Username", admin.Role);
                        cmd.Parameters.AddWithValue("@RegistrationDate", admin.JoinDate);
                        cmd.ExecuteNonQuery();
                        return admin;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("Entered Invalid Input details!!!" + ex.Message);
                return null;
            }
        }
        //Get Admin by ID
        public Admin GetAdminById(int adminID)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    Admin admin = null;
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Admin WHERE AdminID = @AdminID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@AdminID", adminID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                admin= new Admin
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
                                return admin;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
        //Get Admin by Username
        public Admin GetAdminByUsername(string username)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    Admin admin = null;
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Admin WHERE Username = @Username", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                admin= new Admin
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
                                return admin;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
        //To Update Admin
        public Admin UpdateAdmin(Admin admin)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Admin SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, UserName = @UserName, Password = @Password, Role = @Role WHERE AdminID = @AdminID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                        cmd.Parameters.AddWithValue("@FirstName", admin.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", admin.LastName);
                        cmd.Parameters.AddWithValue("@Email", admin.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                        cmd.Parameters.AddWithValue("@UserName", admin.Username);
                        cmd.Parameters.AddWithValue("@Password", admin.Password);
                        cmd.Parameters.AddWithValue("@Role", admin.Role);
                        cmd.ExecuteNonQuery();
                        return admin;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
        //To Delete Admin
        public bool DeleteAdmin(int adminID)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Admin WHERE AdminID = @AdminID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@AdminID", adminID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
