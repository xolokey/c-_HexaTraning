using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.DAO;
using CarConnectApp.Entities;
using CarConnectApp.Services;

namespace CarConnectApp.Main
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            // Initialize services
            AuthenticationService authService = new AuthenticationService();
            VehicleService vehicleService = new VehicleService();
            ReservationService reservationService = new ReservationService();
            CustomerService customerService = new CustomerService();
            AdminService adminService = new AdminService();

            bool exit = false;
            Console.WriteLine("======= Welcome to CarConnect =======");
            //While loop for main menu

            while (!exit)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Customer Login");
                Console.WriteLine("3. Register as New Customer");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string mainChoice = Console.ReadLine() ?? string.Empty;

                switch (mainChoice)
                {
                    case "1":
                        Console.Write("Admin Username: ");
                        string adminUsername = Console.ReadLine() ?? string.Empty;
                        Console.Write("Admin Password: ");
                        string adminPassword = Console.ReadLine() ?? string.Empty;

                        var admin = authService.AuthenticateAdmin(adminUsername, adminPassword);
                        if (admin != null)
                        {
                            bool adminMenu = true;
                            while (adminMenu)
                            {
                                Console.WriteLine("\n--- Admin Menu ---");
                                Console.WriteLine("1. Add Vehicle");
                                Console.WriteLine("2. Update Vehicle");
                                Console.WriteLine("3. View Available Vehicles");
                                Console.WriteLine("4. Register New Admin");
                                Console.WriteLine("5. Update Reservation");
                                Console.WriteLine("6. Cancel Reservation");
                                Console.WriteLine("7. Update Customer");
                                Console.WriteLine("8. Get Customer By ID");
                                Console.WriteLine("9. Get Customer By Username");
                                Console.WriteLine("10. Delete Customer");
                                Console.WriteLine("11. Exit Admin Menu");                                
                                Console.Write("Choose option: ");
                                string adminChoice = Console.ReadLine() ?? string.Empty;

                                switch (adminChoice)
                                {
                                    case "1":
                                        Console.WriteLine("Enter new vehicle details:");
                                        Console.Write("Vehicle ID: ");
                                        int newVehicleId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Console.Write("Make: ");
                                        string make = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Model: ");
                                        string model = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Year (e.g., 2022): ");

                                        DateTime manufactureDate;
                                        if (int.TryParse(Console.ReadLine(), out int year))
                                        {
                                            // Set the manufacture date with January 1st of the provided year
                                            manufactureDate = new DateTime(year, 1, 1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a valid numeric year.");
                                            return; // Exit if the year is invalid
                                        }

                                        Console.Write("Color: ");
                                        string color = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Registration Number: ");
                                        string regNo = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Is Available (true/false): ");
                                        bool available = bool.Parse(Console.ReadLine() ?? string.Empty);
                                        Console.Write("Daily Rate: ");
                                        decimal rate = decimal.Parse(Console.ReadLine() ?? string.Empty);

                                        Vehicle newVehicle = new Vehicle
                                        {
                                            VehicleID = newVehicleId,
                                            Make = make,
                                            Model = model,
                                            Year = manufactureDate, // Use the full date (January 1st of the year)
                                            Color = color,
                                            RegistrationNumber = regNo,
                                            Availability = available,
                                            DailyRate = rate
                                        };

                                        // Assuming you have a vehicleService to add the new vehicle
                                        vehicleService.AddVehicle(newVehicle);
                                        Console.WriteLine("Vehicle added successfully.");

                                        break;

                                    case "2":
                                        Console.Write("Enter Vehicle ID to update: ");
                                        int updateId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        vehicleService.UpdateVehicle(updateId);
                                        break;

                                    case "3":
                                        var availableVehicles = vehicleService.GetAvailableVehicles();
                                        Console.WriteLine("--- Available Vehicles ---");
                                        foreach (var v in availableVehicles)
                                        {
                                            Console.WriteLine($"{v.VehicleID} - {v.Make} {v.Model}, {v.Color}, {v.Year}, ${v.DailyRate}");
                                        }
                                        break;

                                    case "4":
                                        Console.WriteLine("Enter new admin details:");
                                        Console.Write("First Name: "); string afn = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Last Name: "); string aln = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Email: "); string aemail = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Phone: "); string aphone = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Username: "); string auser = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Password: "); string apass = Console.ReadLine() ?? string.Empty;
                                        Console.Write("Role: "); string role = Console.ReadLine() ?? string.Empty;

                                        Admin newAdmin = new Admin
                                        {
                                            FirstName = afn,
                                            LastName = aln,
                                            Email = aemail,
                                            PhoneNumber = aphone,
                                            Username = auser,
                                            Password = apass,
                                            Role = role,
                                            JoinDate = DateTime.Now
                                        };
                                        adminService.RegisterAdmin(newAdmin);
                                        Console.WriteLine("Admin registered successfully....");
                                        break;

                                    case "5":
                                        Console.Write("Enter Reservation ID to update: ");
                                        int resId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Reservation existingRes = reservationService.GetReservationById(resId);
                                        if (existingRes != null)
                                        {
                                            Console.Write("New Start Date (yyyy-MM-dd): ");
                                            existingRes.StartDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                            Console.Write("New End Date (yyyy-MM-dd): ");
                                            existingRes.EndDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                            existingRes.TotalCost = (decimal)(existingRes.EndDate - existingRes.StartDate).TotalDays * vehicleService.GetVehicleById(existingRes.VehicleID)?.DailyRate ?? 0;
                                            Console.Write("New Status: ");
                                            existingRes.Status = Console.ReadLine();
                                            reservationService.UpdateReservation(resId, existingRes);
                                            Console.WriteLine("Reservation updated successfully....");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Reservation not found!!!!.");
                                        }
                                        break;

                                    case "6":
                                        Console.Write("Enter Reservation ID to cancel: ");
                                        int cancelId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Reservation cancelRes = reservationService.GetReservationById(cancelId);
                                        if (cancelRes != null)
                                        {
                                            reservationService.CancelReservation(cancelId);
                                            Console.WriteLine("Reservation canceled successfully....");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Reservation not found!!!!.");
                                        }
                                        break;

                                    case "7":
                                        Console.Write("Enter Customer ID to update: ");
                                        int updateCustomerId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Customer customerToUpdate = customerService.GetCustomerById(updateCustomerId);
                                        if (customerToUpdate != null)
                                        {
                                            Console.Write("First Name: "); customerToUpdate.FirstName = Console.ReadLine();
                                            Console.Write("Last Name: "); customerToUpdate.LastName = Console.ReadLine();
                                            Console.Write("Email: "); customerToUpdate.Email = Console.ReadLine();
                                            Console.Write("Phone Number: "); customerToUpdate.PhoneNumber = Console.ReadLine();
                                            Console.Write("Address: "); customerToUpdate.Address = Console.ReadLine();
                                            Console.Write("Username: "); customerToUpdate.UserName = Console.ReadLine();
                                            Console.Write("Password: "); customerToUpdate.Password = Console.ReadLine();
                                            customerService.UpdateCustomer(customerToUpdate);
                                            Console.WriteLine("Customer updated successfully....");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Customer not found!!!!.");
                                        }
                                        break;

                                    case "8":
                                        Console.Write("Enter Customer ID: ");
                                        int custSearchId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        var custById = customerService.GetCustomerById(custSearchId);
                                        if (custById != null)
                                        {
                                            Console.WriteLine($"Customer: {custById.FirstName} {custById.LastName}, Email: {custById.Email}, Phone: {custById.PhoneNumber}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Customer not found!!!!.");
                                        }
                                        break;

                                    case "9":
                                        Console.Write("Enter Username: ");
                                        string custUsernameSearch = Console.ReadLine() ?? string.Empty;
                                        var custByUsername = customerService.GetCustomerByUserName(custUsernameSearch);
                                        if (custByUsername != null)
                                        {
                                            Console.WriteLine($"Customer: {custByUsername.FirstName} {custByUsername.LastName}, Email: {custByUsername.Email}, Phone: {custByUsername.PhoneNumber}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Customer not found!!!!.");
                                        }
                                        break;

                                    case "10":
                                        Console.Write("Enter Customer ID to delete: ");
                                        int delCustId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        customerService.DeleteCustomer(delCustId);
                                        Console.WriteLine("Customer deleted successfully ....\n");
                                        break;

                                    case "11":
                                        adminMenu = false;
                                        break;


                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                            }
                        }
                        break;

                    case "2":
                        Console.Write("Customer Username: ");
                        string custUsername = Console.ReadLine() ?? string.Empty;
                        Console.Write("Customer Password: ");
                        string custPassword = Console.ReadLine() ?? string.Empty;

                        var customer = authService.AuthenticateCustomer(custUsername, custPassword);
                        if (customer != null)
                        {
                            bool customerMenu = true;
                            while (customerMenu)
                            {
                                Console.WriteLine("\n--- Customer Menu ---");
                                Console.WriteLine("1. View Available Vehicles");
                                Console.WriteLine("2. View My Reservations");
                                Console.WriteLine("3. Create New Reservation");
                                Console.WriteLine("4. Update Reservation");
                                Console.WriteLine("5. Cancel Reservation");
                                Console.WriteLine("6. Exit Customer Menu");
                                Console.Write("Choose option: ");
                                string custChoice = Console.ReadLine() ?? string.Empty;

                                switch (custChoice)
                                {
                                    case "1":
                                        var vehicles = vehicleService.GetAvailableVehicles();
                                        Console.WriteLine("--- Available Vehicles ---");
                                        foreach (var v in vehicles)
                                        {
                                            Console.WriteLine($"{v.VehicleID} - {v.Make} {v.Model}, {v.Color}, {v.Year}, ${v.DailyRate}");
                                        }
                                        break;

                                    case "2":
                                        var reservations = reservationService.GetReservationsByCustomerId(customer.CustomerID);
                                        Console.WriteLine("--- Your Reservations ---");
                                        foreach (var r in reservations)
                                        {
                                            Console.WriteLine($"Reservation ID: {r.ReservationID}, Vehicle ID: {r.VehicleID}, Start: {r.StartDate}, End: {r.EndDate}, Status: {r.Status}");
                                        }
                                        break;

                                    case "3":
                                        Console.Write("Enter Vehicle ID to reserve: ");
                                        int vehicleId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Console.Write("Enter Start Date (yyyy-MM-dd): ");
                                        DateTime start = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                        Console.Write("Enter End Date (yyyy-MM-dd): ");
                                        DateTime end = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                        decimal vRate = vehicleService.GetVehicleById(vehicleId)?.DailyRate ?? 0;
                                        decimal totalCost = (decimal)(end - start).TotalDays * vRate;

                                        Reservation newReservation = new Reservation
                                        {
                                            CustomerID = customer.CustomerID,
                                            VehicleID = vehicleId,
                                            StartDate = start,
                                            EndDate = end,
                                            TotalCost = totalCost,
                                            Status = "Pending"
                                        };

                                        reservationService.CreateReservation(newReservation);
                                        Console.WriteLine("Reservation created successfully....");
                                        break;

                                    case "4":
                                        Console.Write("Enter Reservation ID to update: ");
                                        int cresId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Reservation cexistingRes = reservationService.GetReservationById(cresId);
                                        if (cexistingRes != null && cexistingRes.CustomerID == customer.CustomerID)
                                        {
                                            Console.Write("Enter New Start Date (yyyy-MM-dd): ");
                                            cexistingRes.StartDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                            Console.Write("Enter New End Date (yyyy-MM-dd): ");
                                            cexistingRes.EndDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);
                                            cexistingRes.TotalCost = (decimal)(cexistingRes.EndDate - cexistingRes.StartDate).TotalDays * vehicleService.GetVehicleById(cexistingRes.VehicleID)?.DailyRate ?? 0;
                                            Console.Write("Enter New Status: ");
                                            cexistingRes.Status = Console.ReadLine();
                                            reservationService.UpdateReservation(cresId, cexistingRes);
                                            Console.WriteLine("Reservation updated successfully....");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Reservation not found or does not belong to you!!!....");
                                        }
                                        break;

                                    case "5":
                                        Console.Write("Enter Reservation ID to cancel: ");
                                        int ccancelId = int.Parse(Console.ReadLine() ?? string.Empty);
                                        Reservation ccancelRes = reservationService.GetReservationById(ccancelId);
                                        if (ccancelRes != null && ccancelRes.CustomerID == customer.CustomerID)
                                        {
                                            reservationService.CancelReservation(ccancelId);
                                            Console.WriteLine("Reservation canceled successfully....");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Reservation not found or does not belong to you!!!.....");
                                        }
                                        break;

                                    case "6":
                                        customerMenu = false;
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter new customer details:");
                        Console.Write("Customer ID: "); int custId = int.Parse(Console.ReadLine() ?? string.Empty);
                        Console.Write("First Name: "); string firstName = Console.ReadLine() ?? string.Empty;
                        Console.Write("Last Name: "); string lastName = Console.ReadLine() ?? string.Empty;
                        Console.Write("Email: "); string email = Console.ReadLine() ?? string.Empty;
                        Console.Write("Phone: "); string phone = Console.ReadLine() ?? string.Empty;
                        Console.Write("Address: "); string address = Console.ReadLine() ?? string.Empty;
                        Console.Write("Username: "); string username = Console.ReadLine() ?? string.Empty;
                        Console.Write("Password: "); string password = Console.ReadLine() ?? string.Empty;

                        Customer newCustomer = new Customer
                        {
                            CustomerID = custId,
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            PhoneNumber = phone,
                            Address = address,
                            UserName = username,
                            Password = password,
                            RegistrationDate = DateTime.Now
                        };
                        customerService.RegisterCustomer(newCustomer);
                        Console.WriteLine("Customer registered successfully....");
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("------Thank you for using CarConnect. VisitAgain!!!---------");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
    
