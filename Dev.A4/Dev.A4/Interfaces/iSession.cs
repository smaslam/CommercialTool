using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.A4.DataTypes;

namespace Dev.A4.Interfaces
{
    public interface iSession : iClass_Type1
    {
        /// <summary>
        /// User who created this log entry
        /// </summary>
        cObjectReference objUser { get; }
        /// <summary>
        /// Start time for this session
        /// </summary>
        DateTime dtStartTime { get; }
        /// <summary>
        /// End time (logout time)
        /// </summary>
        DateTime dtEndTime { get; set; }
        /// <summary>
        /// Last activity time (in case of abandoned session)
        /// </summary>
        DateTime dtLastActivityTime { get; set; }
        /// <summary>
        /// Logout
        /// </summary>
        void Logout();
    }
}