using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cValidationException:Exception
    {
        //Creating Constructor
        public cValidationException(string i_sError)
            : base("ValidationException: " + i_sError)
        {
        }
    }
}