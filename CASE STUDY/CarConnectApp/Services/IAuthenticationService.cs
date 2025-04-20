using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Entities;

namespace CarConnectApp.Services
{
    public interface  IAuthenticationService
    {
        Customer AuthenticateCustomer(string username, string password);
        Admin AuthenticateAdmin(string username, string password);
    }
}
