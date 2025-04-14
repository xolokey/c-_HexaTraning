using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.Entities
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public Students? StudentID { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaymentDate { get; set; }

    }
}
