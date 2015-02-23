using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
    public enum enUserType
    {
        /// <summary>
        /// System
        /// </summary>
        System = -1,
        /// <summary>
        /// Guest
        /// </summary>
        Guest = 0,
        /// <summary>
        /// Standard User
        /// </summary>
        StandardUser = 1,
        /// <summary>
        /// Administrator
        /// </summary>
        Administrator = 2
    }
}