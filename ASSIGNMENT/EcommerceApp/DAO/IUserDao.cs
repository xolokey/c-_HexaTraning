
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.DAO
{
     public interface IUserDao<T>
    {
        bool AuthenticateUser(T userInfo);
        //bool AddUser(T userInfo);

    }
}
