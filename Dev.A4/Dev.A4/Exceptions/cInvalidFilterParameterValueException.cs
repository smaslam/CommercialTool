using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidFilterParameterValueException:Exception
    {
        public cInvalidFilterParameterValueException(string i_sMessage)
            : base("InvalidFilterParameterValueException: " + i_sMessage)
        {
        }
    }
}