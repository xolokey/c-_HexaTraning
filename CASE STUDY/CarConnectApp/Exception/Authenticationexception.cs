﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class Authenticationexception : System.Exception
    {
        public Authenticationexception(string message) : base(message){ }
        
    }
}
