using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cLogicalOperatorRequiredException:Exception
    {
        public cLogicalOperatorRequiredException()
            : base("LogicalOperatorRequiredException: ")
        {
        }
    }
}