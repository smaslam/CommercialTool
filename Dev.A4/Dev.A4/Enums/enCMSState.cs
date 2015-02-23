using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
    /// <summary>
    /// CMS States of an object
    /// </summary>
    public enum enCMSState
    {
        Undefined = 0,
        Create_Waiting = 1,
        Create_Rejected = 2,
        Active = 3,
        Update_Waiting = 4,
        Update_Rejected = 5,
        Delete_Waiting = 6,
        Deleted = 7,
        Delete_Rejected = 8,
        Old_Version = 9,
        /// <summary>
        /// Either Active or Update_Waiting or Delete_Waiting
        /// </summary>
        Valid = 10

    }
}