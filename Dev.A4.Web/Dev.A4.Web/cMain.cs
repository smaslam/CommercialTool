using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using Dev.A4;
using Dev.A4.Storages;
using Dev.A4.Interfaces;

namespace Dev.A4.Web
{
    public class cMain : iApplication
    {

        //---------------------------------------------------------------------------------------


        public bool bIsOffline
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["Dev.A4.Offline"]); }
        }

        public virtual void Start(string i_sConfiguration)
        {
            new cMSSQL(ConfigurationManager.ConnectionStrings["DevA4.DatabaseConnectionString"].ConnectionString);

        }

    }
}
