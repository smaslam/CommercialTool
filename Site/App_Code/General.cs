using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.App_Code
{
    public class General
    {
        public enum enOrderStatus
        {
            NewOrder = 3,
            Failure = 2,
            Success = 1
            //Order Status 3 for New Order 
            //Order Status 2 for Failure
            //Order Status 1 for Success
        }
    }
}