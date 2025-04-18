using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.Entity
{
    public class PetShelter
    {
        private List<Pet> availablePets = new List<Pet>();

        public void AddPet(Pet pet)
        {
            if (pet != null)
                availablePets.Add(pet);
        }

        public void RemovePet(Pet pet)
        {
            if (availablePets.Contains(pet))
                availablePets.Remove(pet);
        }

        public void ListAvailablePets()
        {
            if (availablePets.Count == 0)
            {
                Console.WriteLine("No pets currently available for adoption.");
                return;
            }

            Console.WriteLine("Available Pets:");
            foreach (var pet in availablePets)
            {
                Console.WriteLine(pet.ToString());
            }
        }
    }
}
