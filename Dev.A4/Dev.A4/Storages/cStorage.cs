using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Storages
{
    public abstract class cStorage
    {
        protected string m_sName = string.Empty;
        public string sName 
        {
            get { return m_sName; }
        }
    }
}