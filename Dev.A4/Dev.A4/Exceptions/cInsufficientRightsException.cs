using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cInsufficientRightsException:Exception
    {
        public cInsufficientRightsException(string i_sMessage)
            : base("InsufficientPermissionsException: " + i_sMessage)
        {
        }
    }
}