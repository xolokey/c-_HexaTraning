using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.DAO;
using CarConnectApp.Entities;
using CarConnectApp.Util;


namespace CarConnectApp.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomerService customerService = new CustomerService();
            Customer customer = new Customer
            {
                CustomerID = 101,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@gmail.com",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                UserName = "johndoe",
                Password = "password123",
                RegistrationDate = DateTime.Now
            };
            var regcustomer = customerService.RegisterCustomer(customer);
            Console.WriteLine(regcustomer != null ? "Customer Registered Successfully" : "Customer Registration Failed");








        }
    }
}
