﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cApplicationIsNotActiveException:Exception
    {
        public cApplicationIsNotActiveException(string i_sError)
            : base("cSystemIsNotActiveException: " + i_sError)
        { 
        
        }
    }
}