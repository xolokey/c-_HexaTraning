using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PetPalsApp.Entity;
using PetPalsApp.Util;

namespace PetPalsApp.DAO
{
    public class PetDaoImpl : IPetDao
    {
        private int GetNextPetId(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(petid), 0) + 1 FROM pets", conn);
            return (int)cmd.ExecuteScalar();
        }

        public void AddNewPet(Pet pet)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    int nextId = GetNextPetId(conn);

                    string query = "INSERT INTO pets (petid, name, age, breed, type, availableforadoption) " +
                                   "VALUES (@id, @name, @age, @breed, 'Generic', 1)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", nextId);
                    cmd.Parameters.AddWithValue("@name", pet.Name);
                    cmd.Parameters.AddWithValue("@age", pet.Age);
                    cmd.Parameters.AddWithValue("@breed", pet.Breed);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Pet added successfully.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR - Add Pet] " + ex.Message);
            }
        }



        public void AdoptPet(string petName)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    string query = "UPDATE pets SET availableforadoption = 0 WHERE name = @name";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", petName);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine($"Pet '{petName}' marked as adopted.");
                    else
                        Console.WriteLine($"No available pet found with name '{petName}'.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR - Adopt Pet] " + ex.Message);
            }
        }

        public List<Pet> GetAvailablePets()
        {
            List<Pet> pets = new List<Pet>();

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    string query = "SELECT name, age, breed FROM pets WHERE availableforadoption = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        int age = Convert.ToInt32(reader["age"]);
                        string breed = reader["breed"].ToString();

                        pets.Add(new Pet(name, age, breed));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR] " + ex.Message);
            }

            return pets;
        }
    }
}
