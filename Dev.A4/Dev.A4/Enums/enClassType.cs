using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Enums
{
    public enum enClassType
    {
        /// <summary>
        /// Light Object, no iID or sName
        /// </summary>
        Light = 0,
        /// <summary>
        /// iID, sName, iVersion, iState etc
        /// </summary>
        Standard = 1,
        /// <summary>
        /// Extended object with History
        /// </summary>
        Enhanced = 2,
        /// <summary>
        /// Workflow Object
        /// </summary>
        Workflow = 3,
        /// <summary>
        /// Content Management System Object
        /// Predefined workflows: Create > Approve > Active > Update > Approve > Active > Delete > Approve > Deleted
        /// </summary>
        CMS = 4
    }
}