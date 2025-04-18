using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPalsApp.Entity
{
    public class ItemDonation : Donation
    {
        public string ItemType { get; set; }

        public ItemDonation(string donorName, string itemType)
            : base(donorName, 0)
        {
            ItemType = itemType;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Item donation ({ItemType}) by {DonorName} recorded.");
        }
    }
}
