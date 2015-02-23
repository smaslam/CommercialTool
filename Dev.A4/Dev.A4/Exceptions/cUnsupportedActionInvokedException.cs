using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cUnsupportedActionInvokedException:Exception
    {
        public cUnsupportedActionInvokedException(string i_sMessage)
            : base("UnsupportedActionInvokedException: " + i_sMessage)
        {
        }
    }
}