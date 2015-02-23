using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.Interfaces;
using Dev.A4.Exceptions;

namespace Dev.A4
{
    public class cSystem
    {
        private static iApplication m_oApplication = null;
        /// <summary>
        /// Returns the global application instance
        /// </summary>
        public static iApplication oApplication
        {
            get { return m_oApplication; }
            set { m_oApplication = value; }
        }

        /// <summary>
        /// Throws cApplicationIsNotActiveException()
        /// </summary>
        public static void MakeSureApplicationIsActive()
        {
            if (cSystem.oApplication.bIsOffline)
            {
                throw new cApplicationIsNotActiveException(string.Empty);
            }
        }
    }
}