using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPalsApp.Entity;

namespace PetPalsApp.DAO
{
    public interface IDonationDao
    {
        //Collection fro DonationDaoImpl
        void InsertCashDonation(CashDonation donation);
        void InsertItemDonation(ItemDonation donation);
    }
}
