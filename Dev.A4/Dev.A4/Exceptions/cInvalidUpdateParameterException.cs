using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidUpdateParameterException:Exception
    {
        public cInvalidUpdateParameterException(string i_sMessage)
            : base("InvalidUpdateParameterException: " + i_sMessage)
        {

        }
    }
}