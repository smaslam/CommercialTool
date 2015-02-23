using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInvalidOutputParameterException:Exception
    {
        public cInvalidOutputParameterException(string i_sMessage)
            : base("InvalidOutputParameterException: " + i_sMessage)
        {

        }
    }
}