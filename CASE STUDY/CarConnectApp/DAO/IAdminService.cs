using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.DAO
{
    public interface IAdminService<T>
    {
        T RegisterAdmin(T admin);
        T GetAdminById(int adminID);
        T GetAdminByUsername(string username);
        T UpdateAdmin(T admin);
        bool DeleteAdmin(int adminID);
    }
}
