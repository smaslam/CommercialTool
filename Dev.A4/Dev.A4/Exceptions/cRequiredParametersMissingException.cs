using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Exceptions
{
    public class cRequiredParametersMissingException:Exception
    {
        public cRequiredParametersMissingException(string i_sParameters)
            : base("One of more of the following mandatory parameters are missing : " + i_sParameters)
        {

        }
    }
}