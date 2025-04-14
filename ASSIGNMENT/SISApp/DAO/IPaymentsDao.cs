using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.DAO
{
    public interface IPaymentsDao<T>
    {
        //T SavePayment(T payment);
        void RecordPayment(int studentId, decimal amount, DateTime paymentDate);


    }
}
