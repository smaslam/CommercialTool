using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.General
{
    public class cObjectProxy
    {
        public string sID = string.Empty;
        public string sName = string.Empty;

        public cObjectProxy()
        {
        }

        public cObjectProxy(string i_sID, string i_sName)
        {
            sID = i_sID;
            sName = i_sName;
        }
    }
}