using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidObjectException:Exception
    {
        public cInvalidObjectException(string i_sError)
            : base("InvalidObjectException: " + i_sError)
        {
        }
    }
}