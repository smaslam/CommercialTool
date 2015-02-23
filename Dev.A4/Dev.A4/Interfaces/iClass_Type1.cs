using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev.A4.Interfaces
{
    public interface iClass_Type1:iClass_Type0
    {
        /// <summary>
        /// ID or Primary Key
        /// </summary>
        int iID { get; }
        /// <summary>
        /// Name
        /// </summary>
        string sName { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        //int iVersion { get; }
        /// <summary>
        /// Created On
        /// </summary>
        DateTime dtCreatedOn { get; }
        /// <summary>
        /// Last Updated On
        /// </summary>
        DateTime dtLastUpdatedOn { get; }
        /// <summary>
        /// Created By
        /// </summary>
       // int iCreatedBy { get; set; }
        /// <summary>
        /// Last Updated By
        /// </summary>
      //  int iLastUpdatedBy { get; set; }


    }
}