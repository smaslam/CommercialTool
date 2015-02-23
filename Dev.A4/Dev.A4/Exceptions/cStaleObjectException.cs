using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cStaleObjectException:Exception
    {
        public cStaleObjectException(string i_sError)
            : base("StaleObjectException: " + i_sError)
        {
        }
    }
}