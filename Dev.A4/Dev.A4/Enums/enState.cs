using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
    /// States of an object
    /// </summary>
    public enum enState
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Active
        /// </summary>
        Active = 10,
        /// <summary>
        /// Deleted (soft deleted)
        /// </summary>
        Deleted = 20,
        /// <summary>
        /// Old version
        /// </summary>
        OldVersion = 200
    }
}