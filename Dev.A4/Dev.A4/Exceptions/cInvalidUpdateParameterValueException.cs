using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidUpdateParameterValueException:Exception
    {
        public cInvalidUpdateParameterValueException(string i_sMessage)
            : base("InvalidUpdateParameterValueException: " + i_sMessage)
        {
        }
    }
}