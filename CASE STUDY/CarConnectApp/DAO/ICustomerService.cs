using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Entities;

namespace CarConnectApp.DAO
{
    public interface ICustomerService<T>
    {
        T RegisterCustomer(T customer);
        T GetCustomerById(int customerID);
        T GetCustomerByUserName(string userName);
        T UpdateCustomer(T customer);
        T DeleteCustomer(int customerID);
    }
}
