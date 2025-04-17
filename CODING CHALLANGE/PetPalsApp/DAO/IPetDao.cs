using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.Entity;

namespace PetPalsApp.DAO
{
    public interface IPetDao
    {
        List<Pet> GetAvailablePets();
        void AdoptPet(string petName);
        void AddNewPet(Pet pet);


    }
}
