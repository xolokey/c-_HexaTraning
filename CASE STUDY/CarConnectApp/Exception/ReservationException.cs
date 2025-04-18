using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class ReservationException : System.Exception
    {
        public ReservationException(string message) : base(message){ }
    }
}
