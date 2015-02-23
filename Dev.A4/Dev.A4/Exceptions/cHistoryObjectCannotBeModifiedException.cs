using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cHistoryObjectCannotBeModifiedException : Exception
    {
        public cHistoryObjectCannotBeModifiedException(string i_sMessage)
            : base("HistoryObjectCannotBeModifiedException: " + i_sMessage)
        {

        }
    }
}