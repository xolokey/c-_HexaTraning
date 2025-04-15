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

namespace CarConnectApp.DAO
{
    public class CustomerService: ICustomerService<Customer>
    {
        public Customer RegisterCustomer(Customer customer)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer" +
                        " (CustomerID, FirstName, LastName, Email, PhoneNumber, Address,UserName, Password, RegistrationDate) VALUES (@CustomerID, @FirstName,@LastName, @Email, @PhoneNumber, @Address,@Username,@Password, @RegistrationDate)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@Username", customer.UserName);
                        cmd.Parameters.AddWithValue("Password", customer.Password);
                        cmd.Parameters.AddWithValue("@RegistrationDate", customer.RegistrationDate);
                        cmd.ExecuteNonQuery();
                        return customer;
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            //catch(InvalidCustomerDataException ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //    return null;
            //}
           

        }

        
    }
}
