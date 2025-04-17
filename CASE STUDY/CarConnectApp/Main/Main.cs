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
            //Registering a Customer
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

            //Total Cost of Vehicle
            var regcustomer = customerService.RegisterCustomer(customer);
            Console.WriteLine(regcustomer != null ? "Customer Registered Successfully" : "Customer Registration Failed");

            Reservation reservation = new Reservation
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3)
            };

            decimal dailyRate = 3500.0m; // Example daily rate
            decimal totalCost = reservation.CalculateTotalCost(dailyRate);

            Console.WriteLine($"Total Cost: {totalCost}");

            // Create Getting Customer By ID
            CustomerService customer1 = new CustomerService();
            Console.WriteLine("Enter Customer ID to get details:");
            int customerId = int.Parse(Console.ReadLine());
            Customer customerser = customer1.GetCustomerById(customerId);
            if (customerser != null)
            {
                Console.WriteLine($"Customer ID: {customerser.CustomerID}");
                Console.WriteLine($"First Name: {customerser.FirstName}");
                Console.WriteLine($"Last Name: {customerser.LastName}");
                Console.WriteLine($"Email: {customerser.Email}");
                Console.WriteLine($"Phone Number: {customerser.PhoneNumber}");
                Console.WriteLine($"Address: {customerser.Address}");
                Console.WriteLine($"Username: {customerser.UserName}");
                Console.WriteLine($"Registration Date: {customerser.RegistrationDate}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
            Console.WriteLine("The Customer Details are Displayed");
            //Updating Vehicle
            VehicleService vehicleService = new VehicleService();

            Console.Write("Enter Vehicle ID to update: ");
            int vehicleId = int.Parse(Console.ReadLine());
            vehicleService.UpdateVehicle(vehicleId);










        }
    }
}
