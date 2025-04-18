using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class AdminNotFoundException: System.Exception
    {
        public AdminNotFoundException(string message) : base(message) { }
    }
}
