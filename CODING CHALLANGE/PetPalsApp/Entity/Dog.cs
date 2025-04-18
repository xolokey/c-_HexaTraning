using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.Entity
{
    public class Dog : Pet
    {
        public string DogBreed { get; set; }

        public Dog(string name, int age, string breed, string dogBreed)
            : base(name, age, breed)
        {
            DogBreed = dogBreed;
        }

        public override string ToString()
        {
            return base.ToString() + $", Dog Breed: {DogBreed}";
        }
    }
}
