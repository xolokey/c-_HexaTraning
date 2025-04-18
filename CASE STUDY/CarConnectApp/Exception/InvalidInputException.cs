using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class InvalidInputException: System.Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
}
