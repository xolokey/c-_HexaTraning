using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.DAO;
using PetPalsApp.Entity;
using PetPalsApp.Exception;

namespace PetPalsApp.Main
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            // Initialize DAO implementations
            IPetDao petDao = new PetDaoImpl();
            IDonationDao donationDao = new DonationDaoImpl();
            IAdoptionEventDao eventDao = new AdoptionEventDaoImpl();


            bool running = true;
            // Main menu loop

            while (running)
            {
                //Menu Options and Menu Picking ReadLine
                Console.WriteLine("\n--- PetPals Main Menu ---");
                Console.WriteLine("1. View Available Pets");
                Console.WriteLine("2. Make Cash Donation");
                Console.WriteLine("3. Make Item Donation");
                Console.WriteLine("4. Adopt Pet");
                Console.WriteLine("5. Add New Pet");

                Console.WriteLine("6. Exit");

                Console.Write("Choose The Operation That Need To be Performed: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    // Display available pets
                    case "1":
                        try
                        {
                            var pets = petDao.GetAvailablePets();

                            if (pets == null || pets.Count == 0)
                            {
                                Console.WriteLine("No pets available for adoption.");
                            }
                            else
                            {
                                Console.WriteLine("\nAvailable Pets:");
                                foreach (var pet in pets)
                                {
                                    try
                                    {

                                        Console.WriteLine(pet.ToString());
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                        Console.WriteLine("[NullReferenceException] Pet data is incomplete or missing.");
                                    }
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("[Error] " + ex.Message);
                        }
                        break;
                    // Make a cash donation
                    case "2":
                        try
                        {
                            Console.Write("Enter Donor Name: ");
                            string cashDonor = Console.ReadLine();

                            Console.Write("Enter Amount To be Deposited (minimum $10): ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                            if (amount < 10)
                                throw new InsufficientFundsException("Minimum Donation Amount is $10.");

                            CashDonation cashDonation = new CashDonation(cashDonor, amount, DateTime.Now);
                            donationDao.InsertCashDonation(cashDonation);
                        }
                        catch (InsufficientFundsException ex)
                        {
                            Console.WriteLine("[InsufficientFundsException] " + ex.Message);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("[Error] " + ex.Message);
                        }
                        break;
                    // Make an item donation
                    case "3":
                        try
                        {
                            Console.Write("Enter Donor Name: ");
                            string itemDonor = Console.ReadLine();

                            Console.Write("Enter Item Name: ");
                            string itemName = Console.ReadLine();

                            if (string.IsNullOrEmpty(itemName))
                                throw new FileHandlingException("Donation item cannot be Empty.");

                            ItemDonation itemDonation = new ItemDonation(itemDonor, itemName);
                            donationDao.InsertItemDonation(itemDonation);
                        }
                        catch (FileHandlingException ex)
                        {
                            Console.WriteLine("[FileHandlingException] " + ex.Message);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("[Error] " + ex.Message);
                        }
                        break;
                    // Register for an event
                    case "4":
                        try
                        {
                            Console.Write("Enter Pet Name to Adopt: ");
                            string adoptName = Console.ReadLine();

                            if (string.IsNullOrEmpty(adoptName))
                                throw new AdoptionException("Pet Name cannot be Empty.");

                            petDao.AdoptPet(adoptName);
                        }
                        catch (AdoptionException ex)
                        {
                            Console.WriteLine("[AdoptionException] " + ex.Message);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("[Error] " + ex.Message);
                        }
                        break;

                    case "5":
                        try
                        {
                            Console.Write("Enter Pet Name: ");
                            string petName = Console.ReadLine();

                            Console.Write("Enter Pet Age: ");
                            string ageInput = Console.ReadLine();

                            if (!int.TryParse(ageInput, out int petAge))
                                throw new InvalidPetAgeException("Pet Age must be a valid Number.");

                            if (petAge <= 0)
                                throw new InvalidPetAgeException("Pet Age must be a positive Number.");

                            Console.Write("Enter Pet Breed: ");
                            string petBreed = Console.ReadLine();


                            petDao.AddNewPet(new Pet(petName, petAge, petBreed));
                        }
                        catch (InvalidPetAgeException ex)
                        {
                            Console.WriteLine("[InvalidPetAgeException] " + ex.Message);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine("[Error] " + ex.Message);
                        }
                        break;



                    case "6":
                        running = false;
                        Console.WriteLine("Thank you for using PetPals!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
    }
}
