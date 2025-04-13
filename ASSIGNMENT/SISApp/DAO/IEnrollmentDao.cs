using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.DAO
{
    interface IEnrollmentDao<T>
    {
        T SaveEnrollment(T enrollment);
    }
}
