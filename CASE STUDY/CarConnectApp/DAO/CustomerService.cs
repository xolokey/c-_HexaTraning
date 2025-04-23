using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Util;
using CarConnectApp.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Pkcs;
using CarConnectApp.Exception;

namespace CarConnectApp.DAO
{
    public class CustomerService: ICustomerService<Customer>
    {
        //To Register a Customer
        public Customer RegisterCustomer(Customer customer)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (CustomerID, FirstName, LastName, Email, PhoneNumber, Address,UserName, Password, RegistrationDate) VALUES (@CustomerID, @FirstName,@LastName, @Email, @PhoneNumber, @Address,@Username,@Password, @RegistrationDate)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@Username", customer.UserName);
                        cmd.Parameters.AddWithValue("@Password", customer.Password);
                        cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                        cmd.ExecuteNonQuery();
                        return customer;
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
        //Get Customer by ID
        public Customer GetCustomerById(int customerID)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    Customer customer = null;
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer= new Customer
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    RegistrationDate = (DateTime)reader["RegistrationDate"]
                                };
                                return customer;
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

        //Get Customer by UserName
        public Customer GetCustomerByUserName(string userName)
        {
            try
            {
                using (SqlConnection sqlConnection = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    Customer customer = null;
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE UserName = @UserName", sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer= new Customer
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    RegistrationDate = (DateTime)reader["RegistrationDate"]
                                };
                                return customer;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error:"+ ex.Message);

            }
            return null;

        }

        //To Update Customer Details

        public Customer UpdateCustomer(Customer customer)
        {
            try
            {
                using SqlConnection con = DBConnUtil.GetConnection("AppSettings.json");
                con.Open();

                string query = @"UPDATE Customer 
                         SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                             PhoneNumber = @PhoneNumber, Address = @Address, 
                             Username = @Username, Password = @Password 
                         WHERE CustomerID = @CustomerID";

                using SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Username", customer.UserName);
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer update successful.");
                    return customer; // Return updated customer
                }
                else
                {
                    Console.WriteLine("No rows affected.");
                    return null; //  This leads to your test failing
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return null;
            }
        }

        //To Delete Customer
        public Customer DeleteCustomer(int customerID)
        {
            try
            {
                using (SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @CustomerID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        int rowsAffected=cmd.ExecuteNonQuery();
                        if (rowsAffected>0)
                        {  
                            Console.WriteLine("Customer with ID " + customerID + " deleted successfully.");  
                        }
                        else
                        {
                            Console.WriteLine("Customer with ID " + customerID + " not found."); 
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
    }
}
