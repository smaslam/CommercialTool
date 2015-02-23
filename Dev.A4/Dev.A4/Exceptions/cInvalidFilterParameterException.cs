using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidFilterParameterException:Exception
    {
        public cInvalidFilterParameterException(string i_sMessage)
            : base("InvalidFilterParameterException: " + i_sMessage)
        {

        }
    }
}